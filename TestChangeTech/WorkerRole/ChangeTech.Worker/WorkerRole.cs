using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using ChangeTech.Contracts;
using ChangeTech.Models;
using ChangeTech.Services;
using Ethos.DependencyInjection;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.StorageClient;

namespace ChangeTech.Worker
{
    public class WorkerRole : RoleEntryPoint
    {
        readonly List<Thread> Threads = new List<Thread>();
        private string versionNumber
        {
            get
            {
                string version = System.Configuration.ConfigurationManager.AppSettings["ProjectVersionWithoutDot"];
                return string.IsNullOrEmpty(version) ? "" : version.ToLower();
            }
        }
        private IContainerContext context;
        private const int SEND_SMS_RATE_MINITE = 5;
        private const int REPORT_SMS_TIMESPAN_MINUTES = 5;
        private const int EMAIL_INTERVAL_MINUTES = 15;
        public override void Run()
        {
            try
            {
                ServiceUtility.ResetObjectContext();
                context = ContainerManager.GetContainer("container");

                Trace.TraceInformation(string.Format("ChangeTech Worker Start:: {0}", DateTime.UtcNow));

                DateTime emailCheckTime = DateTime.UtcNow;
                DateTime cutConnectLastCheckTime = DateTime.MinValue;
                DateTime monitorWorkerRoleCheckTime = DateTime.MinValue;
                DateTime monitorProgramUserCheckTime = DateTime.MinValue;

                Thread dailySMSThread = new Thread(new ThreadStart(ExistDailySMSThread));
                Threads.Add(dailySMSThread);

                Thread reportSMSThread = new Thread(new ThreadStart(ExistReportSMSThread));
                Threads.Add(reportSMSThread);

                Thread sendSMSThread = new Thread(new ThreadStart(SendSMSThread));
                Threads.Add(sendSMSThread);

                foreach (Thread thread in Threads)
                {
                    thread.Start();
                }

                do
                {

                    WebOperationProcessor();

                    #region the test codes, to note the reminder emails in the testlog dt in the old flow. So we can compare the two flows by compare the data in activitylog and testlog
                    //try
                    //{
                    //    //For the test log flow
                    //    if (emailCheckTimeForTestLogFlow == DateTime.MinValue || emailCheckTimeForTestLogFlow <= DateTime.UtcNow)
                    //    {
                    //        context.Resolve<IProgramUserService>().SendEmailToUserInsertIntoTestLogTableForTest(emailCheckTimeForTestLogFlow);
                    //        emailCheckTimeForTestLogFlow = DateTime.UtcNow.AddMinutes(60 - DateTime.UtcNow.Minute);
                    //    }
                    //}
                    //catch (Exception ex)
                    //{
                    //    Trace.TraceError(string.Format("Reminder Email test log error: {0} :: {1}", DateTime.UtcNow, ex.ToString()));
                    //}
                    #endregion

                    try
                    {
                        if (emailCheckTime == DateTime.MinValue || emailCheckTime <= DateTime.UtcNow)
                        {
                            Trace.TraceInformation(string.Format("Reminder Email Check Process::{0}:: emailCheckTime:: {1}", DateTime.UtcNow, emailCheckTime));
                            context.Resolve<IProgramUserService>().SendEmailToUser(emailCheckTime);
                            //now all steps are in the same main thread, so modify the function of step 2,3 and move them to here.
                            CreateReminderEmailMonitor();
                            //send reminder email asynchronously
                            AsyncReminderEmailMonitor();

                            //send hp order email 
                            List<HPOrderEmailModel> orderEmailModels = context.Resolve<IHPOrderEmailService>().GetOrderEmailsByCurrentDate();
                            if (orderEmailModels.Count() > 0)
                            {
                                context.Resolve<IEmailService>().SendHPOrderEmails(orderEmailModels);
                            }

                            //update programuser switchmessagetime
                            context.Resolve<IProgramUserService>().CheckPuSwitchMessageTimeForTwoPartProgram();

                            emailCheckTime = DateTime.UtcNow.AddMinutes(EMAIL_INTERVAL_MINUTES - DateTime.UtcNow.Minute % EMAIL_INTERVAL_MINUTES);
                        }
                    }
                    catch (Exception ex)
                    {
                        Trace.TraceError(string.Format("Reminder Email Error: {0} :: {1}", DateTime.UtcNow, ex.ToString()));
                    }

                    WebOperationProcessor();

                    try
                    {
                        if (monitorProgramUserCheckTime == DateTime.MinValue || monitorProgramUserCheckTime <= DateTime.UtcNow)
                        {
                            //update programuser switchmessagetime
                            context.Resolve<IProgramUserService>().CheckPuSwitchMessageTimeForTwoPartProgram();
                            monitorProgramUserCheckTime = DateTime.UtcNow.AddMinutes(EMAIL_INTERVAL_MINUTES - DateTime.UtcNow.Minute % EMAIL_INTERVAL_MINUTES);
                        }
                    }
                    catch (Exception ex)
                    {
                        Trace.TraceError(string.Format("Update ProgramUser SwitchMessageTime Error: {0} :: {1}", DateTime.UtcNow, ex.ToString()));
                    }

                    WebOperationProcessor();

                    try
                    {
                        //Trace.TraceInformation(string.Format("Cut Connection Woker Start:: {0}", DateTime.UtcNow));
                        if (cutConnectLastCheckTime == DateTime.MinValue || cutConnectLastCheckTime.AddDays(1) <= DateTime.UtcNow)
                        {
                            cutConnectLastCheckTime = DateTime.UtcNow;
                            context.Resolve<IProgramUserService>().CutConnection();
                        }
                        //Trace.TraceInformation(string.Format("Cut Connection Woker End:: {0}", UtcNow.UtcNow));
                    }
                    catch (Exception ex)
                    {
                        Trace.TraceError(string.Format("Cut Connection Error: {0} :: {1}", DateTime.UtcNow, ex.ToString()));
                    }

                    WebOperationProcessor();

                    //send email and add log to monitor the status of worker role.
                    try
                    {
                        if (monitorWorkerRoleCheckTime == DateTime.MinValue || monitorWorkerRoleCheckTime.AddMinutes(30) <= DateTime.UtcNow)
                        {
                            monitorWorkerRoleCheckTime = DateTime.UtcNow;
                            context.Resolve<IProgramUserService>().SendEmailToUserForMonitorWorkerRole();
                        }
                    }
                    catch (Exception ex)
                    {
                        Trace.TraceError(string.Format("Monitor worker role email error: {0} :: {1}", DateTime.UtcNow, ex.ToString()));
                    }

                    WebOperationProcessor();

                    Thread.Sleep(30000);
                } while (true);
            }
            catch (Exception ex)
            {
                Trace.TraceError(string.Format("ChangeTech Worker Error: {0} :: {1}", DateTime.UtcNow, ex.ToString()));

                //Close all the child threads.
                foreach (Thread thread in Threads)
                {
                    thread.Abort();
                }
            }
        }

        public override bool OnStart()
        {
            AppDomain appDomain = AppDomain.CurrentDomain;
            appDomain.UnhandledException +=
              new UnhandledExceptionEventHandler(appDomain_UnhandledException);

            // Set the maximum number of concurrent connections 
            ServicePointManager.DefaultConnectionLimit = 12;

            //DiagnosticMonitor.Start("DiagnosticsConnectionString");

            // For information on handling configuration changes
            // see the MSDN topic at http://go.microsoft.com/fwlink/?LinkId=166357.
            RoleEnvironment.Changing += RoleEnvironmentChanging;

            DiagnosticMonitorConfiguration diagConfig = DiagnosticMonitor.GetDefaultInitialConfiguration();
            diagConfig.Logs.ScheduledTransferLogLevelFilter = LogLevel.Verbose;
            diagConfig.Logs.ScheduledTransferPeriod = System.TimeSpan.FromMinutes(1);
            DiagnosticMonitor.Start("Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString", diagConfig);

            return base.OnStart();
        }

        void appDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            // Retrieve last exception.   
            Exception ex = e.ExceptionObject as Exception;
            if (ex != null)
            {
                Trace.TraceInformation(string.Format("Worker Role exception. Time: {0};Msg: {1}", DateTime.UtcNow, ex.ToString()));
                InsertLogModel insertLogModel = new InsertLogModel
                {
                    ActivityLogType = (int)LogTypeEnum.MonitorWorkerRoleStatus,
                    Browser = string.Empty,
                    From = string.Empty,
                    IP = string.Empty,
                    Message = string.Format("Monitor Worker Role Status. Exception:{0}", ex.ToString()),
                    PageGuid = Guid.Empty,
                    PageSequenceGuid = Guid.Empty,
                    SessionGuid = Guid.Empty,
                    ProgramGuid = Guid.Empty,
                    UserGuid = Guid.Empty
                };
                context.Resolve<IActivityLogService>().Insert(insertLogModel);
            }
        }


        private void RoleEnvironmentChanging(object sender, RoleEnvironmentChangingEventArgs e)
        {
            // If a configuration setting is changing
            if (e.Changes.Any(change => change is RoleEnvironmentConfigurationSettingChange))
            {
                // Set e.Cancel to true to restart this role instance
                e.Cancel = true;
            }
        }

        private void WebOperationProcessor()
        {
            CloudQueue operationQueue = context.Resolve<IAzureQueueService>().GetCloudQueue(string.Format("operationqueue{0}", versionNumber));
            try
            {
                if (operationQueue != null && operationQueue.Exists())
                {
                    CloudQueueMessage cloudQueueMessage = operationQueue.PeekMessage();
                    if (cloudQueueMessage != null)
                    {
                        string operationMsg = cloudQueueMessage.AsString;
                        string[] operationParameter = operationMsg.Split(new char[] { ';' });
                        Trace.TraceInformation(string.Format("Listen web operation: {0} :: {1}", DateTime.UtcNow, operationMsg));
                        switch (operationParameter[0])
                        {
                            case "AddProgramLanguage":
                                context.Resolve<IProgramService>().AddLanguageForProgram(
                                    new Guid(operationParameter[1]),
                                    new Guid(operationParameter[2]),
                                    new Guid(operationParameter[3]));
                                break;
                            case "RemoveProgramLanguage":
                                context.Resolve<IProgramLanguageService>().RemoveProgramLanguage(
                                    new Guid(operationParameter[1]),
                                    new Guid(operationParameter[2]));
                                break;
                            case "ExportProgram":
                                context.Resolve<IExportService>().ExportProgram(
                                    operationParameter[3],
                                    new Guid(operationParameter[1]),
                                    new Guid(operationParameter[2]),
                                    Convert.ToInt32(operationParameter[4]),
                                    Convert.ToInt32(operationParameter[5]),
                                    Convert.ToBoolean(operationParameter[6]),
                                    Convert.ToBoolean(operationParameter[7]),
                                    Convert.ToBoolean(operationParameter[8]),
                                    Convert.ToBoolean(operationParameter[9]),
                                    Convert.ToBoolean(operationParameter[10]),
                                    Convert.ToBoolean(operationParameter[11]),
                                    Convert.ToBoolean(operationParameter[12]),
                                    Convert.ToBoolean(operationParameter[13]));
                                break;
                            case "ReportProgram":
                                context.Resolve<IExportService>().ReportProgram(operationParameter[3], new Guid(operationParameter[1]), new Guid(operationParameter[2]));
                                break;
                            case "ProgramUserVariable":
                                context.Resolve<IExportService>().ExportUserPageVariable(operationParameter[2], new Guid(operationParameter[1]));
                                break;
                            case "ProgramUserVariableExtension":
                                context.Resolve<IExportService>().ExportUserPageVariableExtension(operationParameter[2], new Guid(operationParameter[1]));
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                CloudQueue statusQueue = context.Resolve<IAzureQueueService>().GetCloudQueue("errorqueue");
                string statusMsg = string.Format(ex.ToString());
                context.Resolve<IAzureQueueService>().AddQueueMessage(statusQueue, statusMsg, false);
                Trace.TraceError("Web Operation Process Exception: {0} :: {1}", DateTime.UtcNow, ex.ToString());
            }
            finally
            {
                if (operationQueue != null && operationQueue.Exists())
                {
                    CloudQueueMessage cloudQueueMessage = operationQueue.GetMessage();
                    if (cloudQueueMessage != null)
                    {
                        operationQueue.DeleteMessage(cloudQueueMessage);
                    }
                }
                else
                {
                    operationQueue.CreateIfNotExist();
                }
            }
            //Thread.Sleep(30000);
        }

        /// <summary>
        /// in a separate thread. Check whether there is a list which need be created into emails to send.
        /// Include reminder emails and monitor emails.
        /// </summary>
        private void EmailOperationProcessor()
        {
            do
            {
                DateTime emailCheckTime = DateTime.UtcNow;
                DateTime monitorWorkerRoleCheckTime = DateTime.MinValue;
                //ServiceUtility.ResetObjectContext();
                //context = ContainerManager.GetContainer("container");
                try
                {
                    if (emailCheckTime == DateTime.MinValue || emailCheckTime <= DateTime.UtcNow)
                    {
                        Trace.TraceInformation(string.Format("Reminder Email Check Process::{0}:: emailCheckTime:: {1}", DateTime.UtcNow, emailCheckTime));

                        CloudQueue emailQueueBeforeProcess = context.Resolve<IAzureQueueService>().GetCloudQueue(string.Format("emailqueuebeforeprocess{0}", versionNumber));
                        CloudQueue emailQueue = context.Resolve<IAzureQueueService>().GetCloudQueue(string.Format("emailqueue{0}", versionNumber));
                        int emailQueueMessageCount = 0;
                        if (emailQueue != null && emailQueue.Exists())
                        {
                            emailQueueMessageCount = emailQueue.RetrieveApproximateMessageCount();
                        }
                        int emailQueueBeforeProcessMessageCount = 0;
                        if (emailQueueBeforeProcess != null && emailQueueBeforeProcess.Exists())
                        {
                            emailQueueBeforeProcessMessageCount = emailQueueBeforeProcess.RetrieveApproximateMessageCount();
                        }
                        if (emailQueueMessageCount == 0 && emailQueueBeforeProcessMessageCount == 0)// No email count on the way. So can get emails list now.
                        {
                            context.Resolve<IProgramUserService>().SendEmailToUser(emailCheckTime);
                            emailCheckTime = DateTime.UtcNow.AddMinutes(EMAIL_INTERVAL_MINUTES - DateTime.UtcNow.Minute % EMAIL_INTERVAL_MINUTES);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Trace.TraceError(string.Format("Reminder Email Error: {0} :: {1}", DateTime.UtcNow, ex.ToString()));
                }

                try
                {
                    //Trace.TraceInformation(string.Format("Cut Connection Woker Start:: {0}", DateTime.UtcNow));
                    if (monitorWorkerRoleCheckTime == DateTime.MinValue || monitorWorkerRoleCheckTime.AddMinutes(30) <= DateTime.UtcNow)
                    {
                        monitorWorkerRoleCheckTime = DateTime.UtcNow;
                        context.Resolve<IProgramUserService>().SendEmailToUserForMonitorWorkerRole();
                    }
                    //Trace.TraceInformation(string.Format("Cut Connection Woker End:: {0}", DateTime.UtcNow));
                }
                catch (Exception ex)
                {
                    Trace.TraceError(string.Format("Monitor worker role email error: {0} :: {1}", DateTime.UtcNow, ex.ToString()));
                }

                Thread.Sleep(30000);
            } while (true);
        }

        /// <summary>
        /// in a separate thread. Check if there is a message in queue to create email.
        /// </summary>
        private void CreateReminderEmailMonitor()
        {
            //do
            //{
            //ServiceUtility.ResetObjectContext();
            //context = ContainerManager.GetContainer("container");
            CloudQueue emailQueueBeforeProcess = context.Resolve<IAzureQueueService>().GetCloudQueue(string.Format("emailqueuebeforeprocess{0}", versionNumber));
            //try
            //{
            if (emailQueueBeforeProcess != null && emailQueueBeforeProcess.Exists())
            {
                bool flag = false;
                do
                {
                    CloudQueueMessage cloudQueueMessage = emailQueueBeforeProcess.GetMessage();
                    try
                    {
                        if (cloudQueueMessage != null)
                        {
                            flag = true;
                            string emailMsg = cloudQueueMessage.AsString;
                            string[] emailParameter = emailMsg.Split('|');
                            if (emailParameter.Length == 9)//have all the info, it is a usual message
                            {
                                //List<ReminderEmailInfoModel> emailList = new List<ReminderEmailInfoModel>();
                                ReminderEmailInfoModel emailItemBeforeProcess = new ReminderEmailInfoModel
                                {
                                    Body = emailParameter[0],
                                    FromAddress = emailParameter[1],
                                    FromName = emailParameter[2],
                                    Password = emailParameter[3],
                                    ProgramGuid = new Guid(emailParameter[4]),
                                    ReminderType = (LogTypeEnum)int.Parse(emailParameter[5]),     // (LogType)emailParameter[5];// Enum.TryParse(  Enum.GetName(LogType,emailParameter[5])  .Parse(typeof(LogType) , emailParameter[5],true),
                                    Subject = emailParameter[6],
                                    ToAddress = emailParameter[7],
                                    UserGuid = new Guid(emailParameter[8]),
                                };
                                context.Resolve<IProgramUserService>().TranferForBeforeProcessReminderEmail(emailItemBeforeProcess);
                            }
                        }
                        else// there is no message, can sleep.
                        {
                            //Thread.Sleep(30000);
                            flag = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        CloudQueue statusQueue = context.Resolve<IAzureQueueService>().GetCloudQueue("errorqueue");
                        string statusMsg = string.Format(ex.ToString());
                        context.Resolve<IAzureQueueService>().AddQueueMessage(statusQueue, statusMsg, false);
                        Trace.TraceError("CreateReminderEmailMonitor Exception: {0} :: {1}", DateTime.UtcNow, ex.ToString());
                    }
                    finally
                    {
                        if (emailQueueBeforeProcess != null && emailQueueBeforeProcess.Exists())
                        {
                            //CloudQueueMessage cloudQueueMessage = emailQueueBeforeProcess.GetMessage();
                            if (cloudQueueMessage != null)
                            {
                                emailQueueBeforeProcess.DeleteMessage(cloudQueueMessage);
                            }
                        }
                        else
                        {
                            emailQueueBeforeProcess.CreateIfNotExist();
                        }
                    }
                }
                while (flag);
            }
            else
            {
                emailQueueBeforeProcess.CreateIfNotExist();
            }
            //}
            //catch (Exception ex)
            //{
            //    CloudQueue statusQueue = context.Resolve<IAzureQueueService>().GetCloudQueue("errorqueue");
            //    string statusMsg = string.Format(ex.ToString());
            //    context.Resolve<IAzureQueueService>().AddQueueMessage(statusQueue, statusMsg, false);
            //    Trace.TraceError("Web Operation Process Exception: {0} :: {1}", DateTime.UtcNow, ex.ToString());
            //}
            //finally
            //{
            //    if (emailQueueBeforeProcess != null && emailQueueBeforeProcess.Exists())
            //    {
            //        CloudQueueMessage cloudQueueMessage = emailQueueBeforeProcess.GetMessage();
            //        if (cloudQueueMessage != null)
            //        {
            //            emailQueueBeforeProcess.DeleteMessage(cloudQueueMessage);
            //        }
            //    }
            //    else
            //    {
            //        emailQueueBeforeProcess.CreateIfNotExist();
            //    }
            //}
            //} while (true);

            Trace.TraceInformation(string.Format("CreateReminderEmailMonitor EndTime::{0}", DateTime.UtcNow));
        }

        #region send email in sync is old way
        /// <summary>
        /// in a separate thread. Check if there is a message in which there is an email to send. If there is one, send it.
        /// </summary>
        //private void ReminderEmailMonitor()
        //{

        //    CloudQueue emailQueue = context.Resolve<IAzureQueueService>().GetCloudQueue(string.Format("emailqueue{0}", versionNumber));
        //    if (emailQueue != null && emailQueue.Exists())
        //    {
        //        bool flag = false;
        //        do
        //        {
        //            CloudQueueMessage cloudQueueMessage = emailQueue.GetMessage();
        //            try
        //            {
        //                if (cloudQueueMessage != null)
        //                {
        //                    flag = true;
        //                    string emailMsg = cloudQueueMessage.AsString;
        //                    string[] emailParameter = emailMsg.Split('|');
        //                    if (emailParameter.Length == 9)//have all the info, it is a usual message
        //                    {
        //                        List<ReminderEmailInfoModel> emailList = new List<ReminderEmailInfoModel>();
        //                        emailList.Add(new ReminderEmailInfoModel
        //                        {
        //                            Body = emailParameter[0].Replace("$*%", "|"),
        //                            FromAddress = emailParameter[1],
        //                            FromName = emailParameter[2].Replace("$*%", "|"),
        //                            Password = emailParameter[3].Replace("$*%", "|"),
        //                            ProgramGuid = new Guid(emailParameter[4]),
        //                            ReminderType = LogTypeEnum.SendReminderEmail,     // (LogType)emailParameter[5];// Enum.TryParse(  Enum.GetName(LogType,emailParameter[5])  .Parse(typeof(LogType) , emailParameter[5],true),
        //                            Subject = emailParameter[6].Replace("$*%", "|"),
        //                            ToAddress = emailParameter[7],
        //                            UserGuid = new Guid(emailParameter[8]),
        //                        });
        //                        context.Resolve<IEmailService>().SendProgramRemainderEmailList(emailList);
        //                    }
        //                }
        //                else// there is no message, can sleep.
        //                {                           
        //                    flag = false;
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                CloudQueue statusQueue = context.Resolve<IAzureQueueService>().GetCloudQueue("errorqueue");
        //                string statusMsg = string.Format(ex.ToString());
        //                context.Resolve<IAzureQueueService>().AddQueueMessage(statusQueue, statusMsg, false);
        //                Trace.TraceError("ReminderEmailMonitor Exception: {0} :: {1}", DateTime.UtcNow, ex.ToString());
        //            }
        //            finally
        //            {
        //                if (emailQueue != null && emailQueue.Exists())
        //                {                           
        //                    if (cloudQueueMessage != null)
        //                    {
        //                        emailQueue.DeleteMessage(cloudQueueMessage);
        //                    }
        //                }
        //                else
        //                {
        //                    emailQueue.CreateIfNotExist();
        //                }
        //            }
        //        }
        //        while (flag);
        //    }
        //    else
        //    {
        //        emailQueue.CreateIfNotExist();
        //    }       

        //    Trace.TraceInformation(string.Format("ReminderEmailMonitor EndTime::{0}", DateTime.UtcNow));
        //}

        #endregion

        #region send email in async
        private void AsyncReminderEmailMonitor()
        {
            CloudQueue emailQueue = context.Resolve<IAzureQueueService>().GetCloudQueue(string.Format("emailqueue{0}", versionNumber));

            if (emailQueue != null && emailQueue.Exists())
            {
                bool flag = false;
                List<ReminderEmailInfoModel> emailList = new List<ReminderEmailInfoModel>();
                do
                {
                    CloudQueueMessage cloudQueueMessage = emailQueue.GetMessage();
                    try
                    {
                        if (cloudQueueMessage != null)
                        {
                            flag = true;
                            string emailMsg = cloudQueueMessage.AsString;
                            string[] emailParameter = emailMsg.Split('|');
                            if (emailParameter.Length == 9)//have all the info, it is a usual message
                            {
                                emailList.Add(new ReminderEmailInfoModel
                                {
                                    Body = emailParameter[0].Replace("$*%", "|"),
                                    FromAddress = emailParameter[1],
                                    FromName = emailParameter[2].Replace("$*%", "|"),
                                    Password = emailParameter[3].Replace("$*%", "|"),
                                    ProgramGuid = new Guid(emailParameter[4]),
                                    ReminderType = LogTypeEnum.SendReminderEmail,     // (LogType)emailParameter[5];// Enum.TryParse(  Enum.GetName(LogType,emailParameter[5])  .Parse(typeof(LogType) , emailParameter[5],true),
                                    Subject = emailParameter[6].Replace("$*%", "|"),
                                    ToAddress = emailParameter[7],
                                    UserGuid = new Guid(emailParameter[8]),
                                });
                            }
                        }
                        else// there is no message, can sleep.
                        {
                            flag = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        CloudQueue statusQueue = context.Resolve<IAzureQueueService>().GetCloudQueue("errorqueue");
                        string statusMsg = string.Format(ex.ToString());
                        context.Resolve<IAzureQueueService>().AddQueueMessage(statusQueue, statusMsg, false);
                        Trace.TraceError("AsyncReminderEmailMonitor Exception: {0} :: {1}", DateTime.UtcNow, ex.ToString());
                    }
                    finally
                    {
                        if (emailQueue != null && emailQueue.Exists())
                        {
                            if (cloudQueueMessage != null)
                            {
                                emailQueue.DeleteMessage(cloudQueueMessage);
                            }
                        }
                        else
                        {
                            emailQueue.CreateIfNotExist();
                        }
                    }
                }
                while (flag);

                if (emailList.Count > 0)
                {
                    //async send email
                    try
                    {
                        context.Resolve<IEmailService>().AsyncSendProgramRemainderEmailList(emailList);
                    }
                    catch (Exception ex)
                    {
                        Trace.TraceError("Async Send Email in AsyncReminderEmailMonitor Exception: {0} :: {1}", DateTime.UtcNow, ex.ToString());
                    }
                }
            }
            else
            {
                emailQueue.CreateIfNotExist();
            }

            Trace.TraceInformation(string.Format("AsyncReminderEmailMonitor EndTime::{0}", DateTime.UtcNow));
        }
        #endregion

        private void ExistDailySMSThread()
        {
            Trace.TraceInformation(string.Format("ExistDailySMSThread Thread Start:: {0}", DateTime.UtcNow));

            DateTime smsCheckTimeForProgramDailySMS = DateTime.UtcNow;
            //smsCheckTimeForProgramDailySMS = DateTime.Parse("2012-05-04 18:00:00");
            do
            {
                try
                {
                    //Program Daily SMS
                    if (smsCheckTimeForProgramDailySMS == DateTime.MinValue || smsCheckTimeForProgramDailySMS <= DateTime.UtcNow)
                    {
                        context.Resolve<IProgramUserService>().ExistProgramDailySMSListIntoShortMessageQueue(smsCheckTimeForProgramDailySMS);
                        smsCheckTimeForProgramDailySMS = DateTime.UtcNow.AddMinutes(60 - DateTime.UtcNow.Minute);
                    }
                }
                catch (Exception ex)
                {
                    Trace.TraceError(string.Format("ExistProgramDailySMSListIntoShortMessageQueue error: {0} :: {1}", DateTime.UtcNow, ex.ToString()));
                }
                Thread.Sleep(30000);
            }
            while (true);
        }

        private void ExistReportSMSThread()
        {
            Trace.TraceInformation(string.Format("ExistDailySMSThread Thread Start:: {0}", DateTime.UtcNow));

            DateTime smsCheckTimeForRemindReportSMS = DateTime.UtcNow;
            //smsCheckTimeForRemindReportSMS = DateTime.Parse("2012-05-04 18:00:00");
            do
            {
                try
                {
                    //Remind report SMS
                    if (smsCheckTimeForRemindReportSMS == DateTime.MinValue || smsCheckTimeForRemindReportSMS <= DateTime.UtcNow)
                    {
                        context.Resolve<IProgramUserService>().ExistRemindReportSMSListIntoShortMessageQueue(smsCheckTimeForRemindReportSMS, REPORT_SMS_TIMESPAN_MINUTES);
                        //The ReportSMSTimSpanMinutes(10 minutes in default) is a solid time span, for that in the stored procedure of ExistRemindReportSMSListIntoShortMessageQueue
                        //There is a condition that reportTime<=now<reportTime+ReportSMSTimSpanMinutes, so this must be executed every ReportSMSTimSpanMinutes minutes.
                        //If this ReportSMSTimSpanMinutes minutes should be changed, change the ReportSMSTimSpanMinutes value in the comparision in the sp.
                        smsCheckTimeForRemindReportSMS = DateTime.UtcNow.AddMinutes(REPORT_SMS_TIMESPAN_MINUTES - (DateTime.UtcNow.Minute % REPORT_SMS_TIMESPAN_MINUTES));
                    }
                }
                catch (Exception ex)
                {
                    Trace.TraceError(string.Format("ExistRemindReportSMSListIntoShortMessageQueue error: {0} :: {1}", DateTime.UtcNow, ex.ToString()));
                }
                Thread.Sleep(30000);
            }
            while (true);
        }

        private void SendSMSThread()
        {
            Trace.TraceInformation(string.Format("ExistDailySMSThread Thread Start:: {0}", DateTime.UtcNow));

            DateTime shortMessageLastCheckTime = DateTime.MinValue;
            //shortMessageLastCheckTime = DateTime.Parse("2012-05-04 18:00:00");
            do
            {
                try
                {
                    //Trace.TraceInformation(string.Format("SMS Email Woker Start:: {0}", DateTime.UtcNow));
                    if (shortMessageLastCheckTime == DateTime.MinValue || shortMessageLastCheckTime.AddMinutes(SEND_SMS_RATE_MINITE) <= DateTime.UtcNow)
                    {
                        shortMessageLastCheckTime = DateTime.UtcNow;
                        context.Resolve<IShortMessageService>().SendShortMessageQueue();
                    }
                    Trace.TraceInformation(string.Format("SMS Email Woker End:: {0}", DateTime.UtcNow));
                }
                catch (Exception ex)
                {
                    Trace.TraceError(string.Format("SMS Error: {0} :: {1}", DateTime.UtcNow, ex.ToString()));
                }
                Thread.Sleep(30000);
            }
            while (true);
        }


    }
}