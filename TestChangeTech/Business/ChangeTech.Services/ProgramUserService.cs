using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Models;
using Ethos.DependencyInjection;
using ChangeTech.Contracts;
using ChangeTech.Entities;
using ChangeTech.IDataRepository;
using Ethos.Utility;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;
using Ethos.EmailModule.EmailService;
using System.Net.Mail;
using System.Data;
using Microsoft.WindowsAzure.StorageClient;

namespace ChangeTech.Services
{
    public class ProgramUserService : ServiceBase, IProgramUserService
    {
        class EmailSender
        {
            public User User { get; set; }
            public Guid ProgramGuid { get; set; }
            public Guid LanguageGuid { get; set; }
        }

        public const string MD5_KEY = "psycholo";
        public const string SECURITYSTRINGINREMINDEREMAILSP = "{securitystring}";
        public const int DEFAULTTIMESPANMINUTES = 10;
        public const string DEFAULT_PASSWORD = "Win8ProgramUser";
        public string SUPPORT_NAME = "ChangeTech Service";
        public string SUPPORT_EMAIL = "changetechmail@gmail.com";

        private string versionNumber
        {
            get
            {
                string version = System.Configuration.ConfigurationManager.AppSettings["ProjectVersionWithoutDot"];
                return string.IsNullOrEmpty(version) ? "" : version.ToLower();
            }
        }

        //this function is for test whether the reminder emails match between old flow(this function use) and the new stored procedure flow.
        #region the test codes, to note the reminder emails in the testlog dt in the old flow. So we can compare the two flows by compare the data in activitylog and testlog
        public void SendEmailToUserInsertIntoTestLogTableForTest(DateTime time)
        {
            #region old flow
            if (time.Hour == 1)
            {
                time = time.AddHours(21);
                int hour = time.Hour;
                List<ProgramUser> list = Resolve<IProgramUserRepository>().GetProgramUserForSendMailBeforeMailtime(hour).ToList();
                //Define a queue of email sender,,,,LastSendEmail means last check time
                List<TestLog> queueSenderForTestLog = new List<TestLog>();
                list = list.Where(p => (p.LastSendEmailTime.HasValue == false ||
                    (p.LastSendEmailTime.HasValue && p.LastSendEmailTime.Value.Date < time.Date)) &&
                     !string.IsNullOrEmpty(p.Status) &&
                    (p.Status.Equals(ProgramUserStatusEnum.Registered.ToString()) ||
                     p.Status.Equals(ProgramUserStatusEnum.Active.ToString()) ||
                     (p.Status.Equals(ProgramUserStatusEnum.Paused.ToString()) && p.LastPauseDate.HasValue && p.LastPauseDay.HasValue && p.LastPauseDate.Value.AddDays(p.LastPauseDay.Value).Date <= time.Date))).ToList();
                foreach (ProgramUser pu in list)
                {
                    if (!pu.ProgramReference.IsLoaded)
                    {
                        pu.ProgramReference.Load();
                    }
                    if (!pu.UserReference.IsLoaded)
                    {
                        pu.UserReference.Load();
                    }
                    if (IsShouldSendReminderEmail(pu, time))
                    {
                        if ((pu.Program.IsDeleted.HasValue && !pu.Program.IsDeleted.Value) || !pu.Program.IsDeleted.HasValue)
                        {
                            //Update on 2012/02/02 by JiaoGuangxin. If usertype==Administrator, don't send remainder email.
                            if (pu.User.UserType == null || pu.User.UserType != (int)UserTypeEnum.Administrator)
                            {
                                queueSenderForTestLog.Add(new TestLog
                                {
                                    Email = pu.User.Email,
                                    LogTime = time,
                                    LogType = (int)LogTypeEnum.SendReminderEmail,
                                    TableGUID = Guid.NewGuid(),
                                    ProgramGuid = pu.Program.ProgramGUID,
                                    UserGuid = pu.User.UserGUID,
                                    UserPassword = pu.User.Password,
                                });
                            }
                        }
                    }
                }
                if (queueSenderForTestLog != null)
                    Resolve<IActivityLogRepository>().InsertIntoTestLogTableForReminderEmailsTest(queueSenderForTestLog);
            }
            #endregion
        }
        #endregion

        public bool AddShouldReminderEmailTableBeforeNowInQueue(DateTime now)
        {
            bool flag = false;
            DataTable reminderEmailInfoTable = Resolve<IStoreProcedure>().GetAllInformationForReminderEmailBeforeMailtime(now);
            ISystemSettingService systemSetting = Resolve<ISystemSettingService>();
            if (systemSetting != null)
            {
                SUPPORT_NAME = systemSetting.GetSettingValue("EmailFromName");
                SUPPORT_EMAIL = systemSetting.GetSettingValue("EmailFromAddress");
            }

            if (reminderEmailInfoTable != null)
            {
                CloudQueue emailQueueBeforeProcess = Resolve<IAzureQueueService>().GetCloudQueue(string.Format("emailqueuebeforeprocess{0}", versionNumber));
                emailQueueBeforeProcess.CreateIfNotExist();

                for (int i = 0; i < reminderEmailInfoTable.Rows.Count; i++)
                {
                    if (reminderEmailInfoTable.Rows[i]["EmailBody"] == null)
                        reminderEmailInfoTable.Rows[i]["EmailBody"] = string.Empty;
                    else
                        reminderEmailInfoTable.Rows[i]["EmailBody"] = reminderEmailInfoTable.Rows[i]["EmailBody"].ToString().Replace("\r\n", "<br/>");
                    if (reminderEmailInfoTable.Rows[i]["FromEmail"] == null || string.IsNullOrEmpty(reminderEmailInfoTable.Rows[i]["FromEmail"].ToString().Trim()))
                    {
                        reminderEmailInfoTable.Rows[i]["FromEmail"] = SUPPORT_EMAIL;
                    }
                    if (reminderEmailInfoTable.Rows[i]["FromName"] == null || string.IsNullOrEmpty(reminderEmailInfoTable.Rows[i]["FromName"].ToString().Trim()))
                    {
                        reminderEmailInfoTable.Rows[i]["FromName"] = SUPPORT_NAME;
                    }
                    if (reminderEmailInfoTable.Rows[i]["UserPassword"] == null)
                        reminderEmailInfoTable.Rows[i]["UserPassword"] = string.Empty;
                    if (reminderEmailInfoTable.Rows[i]["ProgramGuid"] == null)
                        reminderEmailInfoTable.Rows[i]["ProgramGuid"] = string.Empty;
                    if (reminderEmailInfoTable.Rows[i]["LogType"] == null)
                        reminderEmailInfoTable.Rows[i]["LogType"] = string.Empty;
                    if (reminderEmailInfoTable.Rows[i]["EmailSubject"] == null)
                        reminderEmailInfoTable.Rows[i]["EmailSubject"] = string.Empty;
                    if (reminderEmailInfoTable.Rows[i]["Email"] == null)
                        reminderEmailInfoTable.Rows[i]["Email"] = string.Empty;
                    if (reminderEmailInfoTable.Rows[i]["UserGuid"] == null)
                        reminderEmailInfoTable.Rows[i]["UserGuid"] = string.Empty;

                    string emailMsg = string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}",
                        reminderEmailInfoTable.Rows[i]["EmailBody"].ToString().Replace("|", "$*%"), reminderEmailInfoTable.Rows[i]["FromEmail"].ToString(),
                        reminderEmailInfoTable.Rows[i]["FromName"].ToString().Replace("|", "$*%"), reminderEmailInfoTable.Rows[i]["UserPassword"].ToString().Replace("|", "$*%"),
                        reminderEmailInfoTable.Rows[i]["ProgramGuid"].ToString(), reminderEmailInfoTable.Rows[i]["LogType"].ToString(), reminderEmailInfoTable.Rows[i]["EmailSubject"].ToString().Replace("|", "$*%"),
                        reminderEmailInfoTable.Rows[i]["Email"].ToString(), reminderEmailInfoTable.Rows[i]["UserGuid"].ToString());

                    Resolve<IAzureQueueService>().AddQueueMessageWithoutClearAndPeek(emailQueueBeforeProcess, emailMsg);
                }
                Trace.TraceInformation(string.Format("Reminder Email::{0}::Total Send Count {1}", DateTime.UtcNow, reminderEmailInfoTable.Rows.Count));
                flag = true;
            }
            else
            {
                Trace.TraceInformation(string.Format("Reminder Email::{0}::Total Send Count {1}", DateTime.UtcNow, 0));
            }

            return flag;
        }

        //SendEmailToUserForMonitorWorkerRole is from this func,so when need change ,need both.
        public void SendEmailToUser(DateTime time)
        {
            Trace.TraceInformation(string.Format("SendEmailToUser Begin::{0}:: TimeParameterValue:: {1}", DateTime.UtcNow, time));
            //Get developer set time in configurationSetting.
            string manuallySetting = RoleEnvironment.GetConfigurationSettingValue("ManualyEmailDateTime");
            if (!string.IsNullOrEmpty(manuallySetting))
            {
                //Add this to fix that: if yesterday's emails are not sent, 
                //we can modify the ManualyEmailDateTime to yesterday's datetime to send the emails.
                DateTime.TryParse(manuallySetting, out time);
            }

            AddShouldReminderEmailTableBeforeNowInQueue(time);
        }

        public bool TranferForBeforeProcessReminderEmail(ReminderEmailInfoModel reminderEmailModel)
        {
            bool flag = false;

            if (reminderEmailModel != null)
            {
                CloudQueue emailQueueProcess = Resolve<IAzureQueueService>().GetCloudQueue(string.Format("emailqueue{0}", versionNumber));
                emailQueueProcess.CreateIfNotExist();

                //Replace the securitystring to MD5Encrypt string
                string securityStr = StringUtility.MD5Encrypt(string.Format("{0};{1}", reminderEmailModel.ToAddress, reminderEmailModel.Password), "psycholo");
                if (reminderEmailModel.Body != null)
                    reminderEmailModel.Body = reminderEmailModel.Body.ToString().Replace(SECURITYSTRINGINREMINDEREMAILSP, securityStr);

                //note this rule to transfer emailMsg
                string emailMsg = string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}",
                    reminderEmailModel.Body, reminderEmailModel.FromAddress,
                    reminderEmailModel.FromName, reminderEmailModel.Password,
                    reminderEmailModel.ProgramGuid, reminderEmailModel.ReminderType, reminderEmailModel.Subject,
                    reminderEmailModel.ToAddress, reminderEmailModel.UserGuid);
                Resolve<IAzureQueueService>().AddQueueMessageWithoutClearAndPeek(emailQueueProcess, emailMsg);
                flag = true;
            }
            return flag;
        }

        public void SendEmailToUserForMonitorWorkerRole()
        {
            //If the email send fail, send it again. If more than three times, drop it.
            Resolve<IEmailService>().SendMonitorWorkerRoleEmail();
        }

        public string UpdateLastSendEmailTimeAndInsertLog(Guid programGuid, Guid userGuid)
        {
            return Resolve<IStoreProcedure>().UpdateLastSendEmailTimeAndInsertLog(programGuid, userGuid);
        }


        public void SetLastSendEmailTimeOfProgramUser(Guid programGuid, Guid userGuid)
        {
            try
            {
                ProgramUser programUserEntity = Resolve<IProgramUserRepository>().GetProgramUserByProgramGuidAndUserGuid(programGuid, userGuid);
                DateTime lastSendEmailTimeByTimeZone = GetCurrentTimeByTimeZone(programGuid, userGuid, DateTime.UtcNow);
                programUserEntity.LastSendEmailTime = lastSendEmailTimeByTimeZone; // DateTime.UtcNow;

                // if the user is active or missed class as a registerd user, set active days
                //if (programUserEntity.ActiveDays > 0)
                //{
                //    programUserEntity.ActiveDays = programUserEntity.ActiveDays.Value + 1;
                //}
                Resolve<IProgramUserRepository>().Update(programUserEntity);
            }
            catch (Exception ex)
            {
                //LogUtility.LogUtilityIntance.LogException(ex, string.Format("{0}::{1}", System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message));
                InsertLogModel insertLogModel = new InsertLogModel
                {
                    ActivityLogType = (int)LogTypeEnum.UpdateProgramUser,
                    Browser = string.Empty,
                    From = string.Empty,
                    IP = string.Empty,
                    Message = string.Format("Method:{0} | ErrorMessage:{1}", "SetLastSendEmailTimeOfProgramUser", ex.Message),
                    PageGuid = Guid.Empty,
                    PageSequenceGuid = Guid.Empty,
                    SessionGuid = Guid.Empty,
                    ProgramGuid = programGuid,
                    UserGuid = userGuid
                };
                Resolve<IActivityLogService>().Insert(insertLogModel);
                throw ex;
            }
        }

        public DateTime? GetLastReportRelapseTime(Guid programGuid, Guid userGuid)
        {
            DateTime? lastReportTime = null;
            ProgramUser pu = Resolve<IProgramUserRepository>().GetProgramUserByProgramGuidAndUserGuid(programGuid, userGuid);
            if (pu != null)
            {
                lastReportTime = pu.LastFinishReportTime;
            }
            return lastReportTime;
        }

        public void SetLastReportRelapseTime(Guid programGuid, Guid userGuid)
        {
            ProgramUser programUserEntity = Resolve<IProgramUserRepository>().GetProgramUserByProgramGuidAndUserGuid(programGuid, userGuid);
            DateTime lastFinishReportTime = GetCurrentTimeByTimeZone(programGuid, userGuid, DateTime.UtcNow);
            programUserEntity.LastFinishReportTime = lastFinishReportTime;

            Resolve<IProgramUserRepository>().Update(programUserEntity);
        }

        public void ExistProgramDailySMSListIntoShortMessageQueue(DateTime time)
        {
            //if (time.Hour == 1)//Only when hour =1 ,this is to ensure to exist the daily sms queue one time every day and insert it into queue every utc DD:01:ss time.
            //{
            //time = time.AddHours(22);//Make the hour to 23 to ensure all today's daily sms are inserted into the queue.
            Resolve<IStoreProcedure>().ExistProgramDailySMSListIntoShortMessageQueue(time);
            //}
        }

        public void ExistRemindReportSMSListIntoShortMessageQueue(DateTime time, int timeSpanMinutes)
        {
            //time = DateTime.Parse("2012-04-30 17:00:00");//For Test
            if (timeSpanMinutes <= 0 || timeSpanMinutes >= 60)
                timeSpanMinutes = DEFAULTTIMESPANMINUTES;
            Resolve<IStoreProcedure>().ExistRemindReportSMSListIntoShortMessageQueue(time, timeSpanMinutes);
        }


        /// <summary>
        /// ToDo: Send email to tester
        /// After tester finished the test of session system will send email to let them test next session at once.
        /// If they finished the program test, system will reset the data of tester like the program begin, to let them test again
        /// </summary>
        /// <param name="UserGuid"></param>
        /// <param name="ProgramGuid"></param>
        public void SendEmailToTester(Guid UserGuid, Guid ProgramGuid)
        {
            ProgramUser user = Resolve<IProgramUserRepository>().GetProgramUserByProgramGuidAndUserGuid(ProgramGuid, UserGuid);
            if (!user.ProgramReference.IsLoaded)
            {
                user.ProgramReference.Load();
            }
            if (!user.UserReference.IsLoaded)
            {
                user.UserReference.Load();
            }
            if (user.User.UserType.Value == (int)UserTypeEnum.Tester)
            {
                //ToDo: Update the date of session like program begin.
                //If they finished the session(Which not the end session), change the date to yesterday, to let them test next session quickly
                //If they finished the program test, system will reset the data of tester like the program begin, to let them test again
                DateTime setCurrentTimeByTimeZone = GetCurrentTimeByTimeZone(ProgramGuid, UserGuid, DateTime.UtcNow);
                if (!Resolve<ISessionService>().IsSessionEnd(ProgramGuid, UserGuid))
                {
                    user.LastFinishDate = null;
                    user.Day = null;
                }
                else
                {
                    user.LastFinishDate = setCurrentTimeByTimeZone.AddDays(-1);
                }
                Resolve<IProgramUserRepository>().Update(user);

                EmailSender sender = new EmailSender
                {
                    User = user.User,
                    ProgramGuid = user.Program.ProgramGUID,
                    //LanguageGuid = user.Language.LanguageGUID,
                };

                //if a tester test one program's day 0 and not
                bool isUserAddressValid = false;
                try
                {
                    MailAddress testAddressOfTheUser = new MailAddress(sender.User.Email);
                    isUserAddressValid = true;
                }
                catch
                {
                    Trace.TraceInformation(string.Format("SendEmailToTester error.::User::{0}::{1}::{2}", sender.User.Email, sender.User.UserGUID, DateTime.UtcNow));
                    isUserAddressValid = false;
                }

                if (isUserAddressValid)
                {
                    int i = 0;
                    do
                    {
                        if (Resolve<IEmailService>().SendProgramRemainderEmail(sender.User, sender.ProgramGuid, LogTypeEnum.NoLog))
                        {
                            i = 5;
                        }
                        else
                        {
                            i++;
                        }
                        if (i == 3)
                        {
                            ChangeTech.Entities.FailEmail fMail = Resolve<IEmailService>().GetProgramRemainderEmail(sender.User, sender.ProgramGuid);
                            if (fMail != null)
                            {
                                Resolve<IFailEmailRepository>().Insert(fMail);
                            }
                        }
                    }
                    while (i < 3);
                }
            }
        }

        public bool HasUserInProgram(Guid userGuid, Guid programGuid)
        {
            bool flag = false;
            ProgramUser programuser = Resolve<IProgramUserRepository>().GetProgramUserByProgramGuidAndUserGuid(programGuid, userGuid);
            if (programuser != null)
            {
                flag = true;
            }

            return flag;
        }

        public bool IsUserMissedDay(Guid userGuid, Guid programGuid)
        {
            bool flug = false;
            ProgramUser programUserEntity = Resolve<IProgramUserRepository>().GetProgramUserByProgramGuidAndUserGuid(programGuid, userGuid);
            if (programUserEntity.Status != ProgramUserStatusEnum.Screening.ToString())
            {
                // find the schedule after the current day
                // Chen Pu comment: If user need to do next day also on today, then he must have missed some classes
                ProgramSchedule schedule = Resolve<IProgramScheduleRepository>().GetProgramScheduleByProgramAndDay(programGuid, programUserEntity.Day.Value + 1);
                if (schedule != null)
                {
                    if (!programUserEntity.ProgramReference.IsLoaded)
                    {
                        programUserEntity.ProgramReference.Load();
                    }
                    ProgramSchedule firstSchedule = Resolve<IProgramScheduleRepository>().GetFirstDayScheduleOfProgram(programUserEntity.Program.ProgramGUID);

                    DateTime activeDate = GetShouldActiveDate(programUserEntity, firstSchedule);
                    //DateTime WouldDoDate = activeDate.AddDays(Convert.ToDouble(schedule.WeekDay - 1 + (schedule.Week - 1) * 7));
                    // add pause date to calculte WouldDoDate, Add by Chen Pu on 2011-07-25
                    DateTime WouldDoDate;
                    if (programUserEntity.PauseDay != null)
                        WouldDoDate = activeDate.AddDays(Convert.ToDouble(schedule.WeekDay - 1 + (schedule.Week - 1) * 7) + Convert.ToDouble(programUserEntity.PauseDay));
                    else
                        WouldDoDate = activeDate.AddDays(Convert.ToDouble(schedule.WeekDay - 1 + (schedule.Week - 1) * 7));

                    //set user's currentTime by TimeZone.
                    DateTime setCurrentTimeByTimeZone = GetCurrentTimeByTimeZone(programGuid, userGuid, DateTime.UtcNow);
                    if (WouldDoDate.Date < setCurrentTimeByTimeZone.Date)
                    {
                        flug = true;
                    }
                }
            }

            return flug;
        }


        /// <summary>
        /// /// It is not the same as IsShouldSendReminderEmail.
        /// In this function, if a user has missed one or more days, and  there is no schedule for DateTime now, return 0 (True)
        /// </summary>
        /// <param name="userGuid"></param>
        /// <param name="programGuid"></param>
        /// <param name="now"></param>
        /// <returns></returns>
        public int IsThereClassToday(Guid userGuid, Guid programGuid, DateTime now)
        {
            int status = 0;
            try
            {
                ProgramUser programUserEntity = Resolve<IProgramUserRepository>().GetProgramUserByProgramGuidAndUserGuid(programGuid, userGuid);
                if (programUserEntity == null)
                {
                    LogUtility.LogUtilityIntance.LogMessage(string.Format("Error in IsThereClassToday::Time: {0} ::More information::{1}", DateTime.UtcNow, "programUserEntity is null"));
                    status = -1;// has error
                }
                else
                {
                    if (!programUserEntity.UserReference.IsLoaded)
                    {
                        programUserEntity.UserReference.Load();
                    }
                    if (!programUserEntity.ProgramReference.IsLoaded)
                    {
                        programUserEntity.ProgramReference.Load();
                    }

                    if (programUserEntity.Program.IsContainTwoParts.HasValue && programUserEntity.Program.IsContainTwoParts.Value == true)
                    {
                        status = IsThereClassTodayProgramContainTwoParts(programUserEntity, now);
                    }
                    else
                    {
                        status = IsThereClassTodayNormalProgram(programUserEntity, now);
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogMessage(string.Format("Error in IsThereClassToday::Time: {0} ::More information::{1}", DateTime.UtcNow, ex.ToString()));
                status = -1;// has error
            }

            return status;
        }

        private void ActiveSMSKickStartForTester(ProgramUser programUserEntity)
        {
            //Active SMS KickStart for Tester in the Contain two parts programs.
            if ((!programUserEntity.User.UserType.HasValue
                || (programUserEntity.User.UserType.HasValue && programUserEntity.User.UserType.Value == (int)UserTypeEnum.Tester))
                && programUserEntity.Day.Value + 1 >= programUserEntity.Program.SwitchDay.Value)
            {
                if (programUserEntity != null && programUserEntity.SwitchMessageTime == null && (programUserEntity.Status != ProgramUserStatusEnum.Completed.ToString() && programUserEntity.Status != ProgramUserStatusEnum.Terminated.ToString()))
                {
                    if (!programUserEntity.ProgramReference.IsLoaded) programUserEntity.ProgramReference.Load();
                    if (!programUserEntity.UserReference.IsLoaded) programUserEntity.UserReference.Load();
                    DateTime switchMessageTime = GetCurrentTimeByTimeZone(programUserEntity.Program.ProgramGUID, programUserEntity.User.UserGUID, DateTime.UtcNow);
                    programUserEntity.SwitchMessageTime = switchMessageTime; //DateTime.UtcNow;
                    programUserEntity.LastPauseDate = null;//If kick start, user will go into second part, the previous last pause info is no use.
                    programUserEntity.LastPauseDay = null;
                    programUserEntity.Status = ProgramUserStatusEnum.Active.ToString();
                    Resolve<IProgramUserRepository>().Update(programUserEntity);
                    // add log
                    InsertLogModel logmodel = new InsertLogModel
                    {
                        ActivityLogType = (int)LogTypeEnum.UpdateProgramUser,
                        Browser = string.Empty,
                        IP = string.Empty,
                        Message = string.Format("Update programuser's switchMessageTime successful.ProgramUserGuid:{0}", programUserEntity.ProgramUserGUID),
                        ProgramGuid = programUserEntity.Program.ProgramGUID,
                        SessionGuid = programUserEntity.User.UserGUID,
                        UserGuid = Guid.Empty
                    };
                    Resolve<IActivityLogService>().Insert(logmodel);
                }
            }
        }

        private int IsThereClassTodayProgramContainTwoParts(ProgramUser programUserEntity, DateTime now)
        {
            int status = 0;

            //Active SMS KickStart for Tester in the Contain two parts programs.
            ActiveSMSKickStartForTester(programUserEntity);
            if (programUserEntity.SwitchMessageTime.HasValue)
            {
                status = IsThereClassHasSwitchProgram(programUserEntity, now);
            }
            else
            {
                // not switch
                if (programUserEntity.Day != null && programUserEntity.Day.Value + 1 >= programUserEntity.Program.SwitchDay.Value)
                {
                    // have finished first part class
                    status = 4;
                }
                else
                {
                    status = IsThereClassTodayNormalProgram(programUserEntity, now);
                }
            }

            return status;
        }

        private int IsThereClassHasSwitchProgram(ProgramUser programUserEntity, DateTime now)
        {
            int status = 0;

            DateTime activeDate = GetShouldActiveDateAfterSwitch(programUserEntity);
            if (!programUserEntity.User.UserType.HasValue ||
              (programUserEntity.User.UserType.HasValue && programUserEntity.User.UserType.Value != (int)UserTypeEnum.Tester) ||
               programUserEntity.Status.Equals(ProgramUserStatusEnum.Completed.ToString()))
            {
                //ProgramSchedule shulddoschedule = Resolve<IProgramScheduleRepository>().GetProgramScheduleByProgramAndDay(programUserEntity.Program.ProgramGUID, programUserEntity.Day.Value);
                //ProgramSchedule activityschedule = Resolve<IProgramScheduleRepository>().GetProgramScheduleByProgramAndDay(programUserEntity.Program.ProgramGUID, programUserEntity.Program.SwitchDay.Value - 1);

                //DateTime WouldDoDate = activeDate.AddDays(Convert.ToDouble(shulddoschedule.WeekDay - activityschedule.WeekDay + (shulddoschedule.Week - activityschedule.Week) * 7));
                ProgramSchedule shulddoschedule = null;
                ProgramSchedule activityschedule = null;


                if (programUserEntity.Status.Equals(ProgramUserStatusEnum.Registered.ToString()))
                {
                    if (IsUserPaidProgram(programUserEntity))
                    {
                        if (activeDate.Date > now.Date)
                        {
                            if (!IsThereOutlineClass(programUserEntity, now))
                            {
                                status = 1;
                            }
                            else
                            {
                                status = 5; // means should do outline work DTD-1001
                            }
                        }
                    }
                    else
                    {
                        status = 6; // need pay for program
                    }
                }
                else if (programUserEntity.Status.Equals(ProgramUserStatusEnum.Completed.ToString()))
                {
                    status = 4;
                }
                else if (programUserEntity.Status.Equals(ProgramUserStatusEnum.Active.ToString()))
                {
                    if (programUserEntity.Day != null && programUserEntity.Program.SwitchDay != null)
                    {
                        shulddoschedule = Resolve<IProgramScheduleRepository>().GetProgramScheduleByProgramAndDay(programUserEntity.Program.ProgramGUID, programUserEntity.Day.Value);
                        activityschedule = Resolve<IProgramScheduleRepository>().GetProgramScheduleByProgramAndDay(programUserEntity.Program.ProgramGUID, programUserEntity.Program.SwitchDay.Value - 1);
                    }
                    else
                    {
                        shulddoschedule = null;
                        activityschedule = null;
                    }
                    if (shulddoschedule != null && activityschedule != null)
                    {
                        DateTime WouldDoDate;
                        // add pause date to calculte WouldDoDate
                        if (programUserEntity.SwitchPauseDay != null)
                            WouldDoDate = activeDate.AddDays(Convert.ToDouble(shulddoschedule.WeekDay - activityschedule.WeekDay + (shulddoschedule.Week - activityschedule.Week) * 7) + Convert.ToDouble(programUserEntity.SwitchPauseDay));
                        else
                            WouldDoDate = activeDate.AddDays(Convert.ToDouble(shulddoschedule.WeekDay - activityschedule.WeekDay + (shulddoschedule.Week - activityschedule.Week) * 7));

                        if (WouldDoDate.Date > now.Date)
                        {
                            status = 2; // need wait for next work day
                        }
                    }
                    else
                    {
                        status = 8;//No schedule for current session
                    }
                }
                else if (programUserEntity.Status.Equals(ProgramUserStatusEnum.Screening.ToString()))
                {
                    if (IsUserPaidProgram(programUserEntity))
                    {
                        status = 3;
                    }
                    else
                    {
                        status = 6; // need pay for program
                    }
                }
                else if (programUserEntity.Status.Equals(ProgramUserStatusEnum.Paused.ToString()))
                {
                    if (programUserEntity.LastPauseDate.HasValue && programUserEntity.LastPauseDate.Value.AddDays(programUserEntity.LastPauseDay.Value).Date <= now.Date)
                    {
                        programUserEntity.Status = ProgramUserStatusEnum.Active.ToString();
                        Resolve<IProgramUserRepository>().Update(programUserEntity);

                        if (programUserEntity.Day != null && programUserEntity.Program.SwitchDay != null)
                        {
                            shulddoschedule = Resolve<IProgramScheduleRepository>().GetProgramScheduleByProgramAndDay(programUserEntity.Program.ProgramGUID, programUserEntity.Day.Value);
                            activityschedule = Resolve<IProgramScheduleRepository>().GetProgramScheduleByProgramAndDay(programUserEntity.Program.ProgramGUID, programUserEntity.Program.SwitchDay.Value - 1);
                        }
                        else
                        {
                            shulddoschedule = null;
                            activityschedule = null;
                        }
                        if (shulddoschedule != null && activityschedule != null)
                        {
                            DateTime WouldDoDate;
                            // add pause date to calculte WouldDoDate
                            if (programUserEntity.SwitchPauseDay != null)
                                WouldDoDate = activeDate.AddDays(Convert.ToDouble(shulddoschedule.WeekDay - activityschedule.WeekDay + (shulddoschedule.Week - activityschedule.Week) * 7) + Convert.ToDouble(programUserEntity.SwitchPauseDay));
                            else
                                WouldDoDate = activeDate.AddDays(Convert.ToDouble(shulddoschedule.WeekDay - activityschedule.WeekDay + (shulddoschedule.Week - activityschedule.Week) * 7));

                            if (WouldDoDate.Date > now.Date)
                            {
                                status = 2; // need wait for next work day
                            }
                        }
                        else
                        {
                            status = 8;//No schedule for current session
                        }
                    }
                    else
                    {
                        status = 2;//The same as WouldDoDate.Date > now.Date
                    }
                }
            }
            return status;
        }

        private DateTime GetShouldActiveDateAfterSwitch(ProgramUser programUser)
        {
            DateTime shouldActiveDay = DateTime.UtcNow;
            ProgramSchedule schedule = Resolve<IProgramScheduleRepository>().GetProgramScheduleByProgramAndDay(programUser.Program.ProgramGUID, programUser.Program.SwitchDay.Value - 1);
            int startWeekday = programUser.SwitchMessageTime.Value.DayOfWeek != DayOfWeek.Sunday ? (int)programUser.SwitchMessageTime.Value.DayOfWeek : 7;
            if (startWeekday > schedule.WeekDay)
            {
                shouldActiveDay = programUser.SwitchMessageTime.Value.AddDays(Convert.ToDouble(7 - (startWeekday - schedule.WeekDay)));
            }
            else if (startWeekday < schedule.WeekDay)
            {
                shouldActiveDay = programUser.SwitchMessageTime.Value.AddDays(Convert.ToDouble(schedule.WeekDay - startWeekday));
            }
            else if (startWeekday == schedule.WeekDay)
            {
                shouldActiveDay = programUser.SwitchMessageTime.Value;
            }

            return shouldActiveDay;
        }

        private int IsThereClassTodayNormalProgram(ProgramUser programUserEntity, DateTime now)
        {
            int status = 0;
            if (!programUserEntity.ProgramReference.IsLoaded)
            {
                programUserEntity.ProgramReference.Load();
            }
            ProgramSchedule schedule = Resolve<IProgramScheduleRepository>().GetFirstDayScheduleOfProgram(programUserEntity.Program.ProgramGUID);
            if (schedule != null)
            {
                DateTime activeDate = GetShouldActiveDate(programUserEntity, schedule);
                if (programUserEntity.User.UserType.HasValue && programUserEntity.User.UserType.Value == (int)UserTypeEnum.User)
                {
                    if (programUserEntity.Status.Equals(ProgramUserStatusEnum.Registered.ToString()))
                    {
                        if (IsUserPaidProgram(programUserEntity))
                        {
                            if (activeDate.Date > now.Date)
                            {
                                // if there has pause data, status = 1 ,wait for next monday. Don't need to implement this, as user will pause when active. 
                                if (!IsThereOutlineClass(programUserEntity, now))
                                {
                                    status = 1;// wait for next monday
                                }
                                else
                                {
                                    status = 5; // means should do outline work DTD-1001
                                }
                            }
                            else
                            {
                                status = 0;
                            }
                        }
                        else
                        {
                            status = 6; // need pay for program
                        }
                    }
                    // The status 'Completed' is set in SessionEnding feedback
                    else if (programUserEntity.Status.Equals(ProgramUserStatusEnum.Completed.ToString()))
                    {
                        status = 4; // has complete program
                    }
                    else if (programUserEntity.Status.Equals(ProgramUserStatusEnum.Active.ToString()))
                    {
                        if (programUserEntity.Day != null)
                        {
                            schedule = Resolve<IProgramScheduleRepository>().GetProgramScheduleByProgramAndDay(programUserEntity.Program.ProgramGUID, programUserEntity.Day.Value);
                            if (schedule == null)
                            {
                                Session lastSession = Resolve<ISessionRepository>().GetLastSessionOfProgram(programUserEntity.Program.ProgramGUID);
                                if (programUserEntity.Day == lastSession.Day.Value)
                                {
                                    status = 9; // has complete program,but programuser status should be changed to complete
                                    return status;
                                }
                            }
                        }
                        else
                        {
                            schedule = null;
                        }
                        if (schedule != null)
                        {
                            DateTime WouldDoDate;
                            // add pause date to calculte WouldDoDate
                            if (programUserEntity.PauseDay != null)
                                WouldDoDate = activeDate.AddDays(Convert.ToDouble(schedule.WeekDay - 1 + (schedule.Week - 1) * 7) + Convert.ToDouble(programUserEntity.PauseDay));
                            else
                                WouldDoDate = activeDate.AddDays(Convert.ToDouble(schedule.WeekDay - 1 + (schedule.Week - 1) * 7));

                            if (WouldDoDate.Date > now.Date)
                            {
                                status = 2; // need wait for next work day
                            }
                        }
                        else
                        {
                            status = 8;//No schedule for current session
                        }
                    }
                    else if (programUserEntity.Status.Equals(ProgramUserStatusEnum.Screening.ToString()))
                    {
                        if (IsUserPaidProgram(programUserEntity))
                        {
                            status = 3;
                        }
                        else
                        {
                            status = 6; // need pay for program
                        }
                    }
                    else if (programUserEntity.Status.Equals(ProgramUserStatusEnum.Paused.ToString()))
                    {
                        if (programUserEntity.LastPauseDate.HasValue && programUserEntity.LastPauseDate.Value.AddDays(programUserEntity.LastPauseDay.Value).Date <= now.Date)
                        {
                            programUserEntity.Status = ProgramUserStatusEnum.Active.ToString();
                            Resolve<IProgramUserRepository>().Update(programUserEntity);

                            schedule = Resolve<IProgramScheduleRepository>().GetProgramScheduleByProgramAndDay(programUserEntity.Program.ProgramGUID, programUserEntity.Day.Value);
                            if (schedule != null)
                            {
                                DateTime WouldDoDate;
                                // add pause date to calculte WouldDoDate
                                if (programUserEntity.PauseDay != null)
                                    WouldDoDate = activeDate.AddDays(Convert.ToDouble(schedule.WeekDay - 1 + (schedule.Week - 1) * 7) + Convert.ToDouble(programUserEntity.PauseDay));
                                else
                                    WouldDoDate = activeDate.AddDays(Convert.ToDouble(schedule.WeekDay - 1 + (schedule.Week - 1) * 7));

                                if (WouldDoDate.Date > now.Date)
                                {
                                    status = 2; // need wait for next work day
                                }
                            }
                            else
                            {
                                status = 8;//No schedule for current session
                            }
                        }
                        else
                        {
                            status = 2;//The same as WouldDoDate.Date > now.Date
                        }
                    }
                }
            }
            else
            {
                status = 7;//no schedule at all.
            }
            return status;
        }


        private bool IsUserPaidProgram(ProgramUser programUserEntity)
        {
            bool flag = true;
            try
            {
                if (!programUserEntity.UserReference.IsLoaded)
                {
                    programUserEntity.UserReference.Load();
                }
                if (!programUserEntity.ProgramReference.IsLoaded)
                {
                    programUserEntity.ProgramReference.Load();
                }
                OrderLicenceModel orderLicenceModel = Resolve<IOrderLicenceService>().GetOrderLicenceByProgramUserGuid(programUserEntity.ProgramUserGUID);
                if (programUserEntity.Program.IsWithPay.HasValue
                    && programUserEntity.Program.IsWithPay.Value == true
                    && (!programUserEntity.User.IsPaid.HasValue || programUserEntity.User.IsPaid.Value == false)
                    && !programUserEntity.User.Email.Contains("ChangeTechTemp")
                    && orderLicenceModel == null)
                //&& programUserEntity.OrderLicence.Where(ol=>ol.IsDeleted == null || ol.IsDeleted == false).Count()==0)
                {
                    flag = false;
                }
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogMessage(string.Format("Error in IsUserPaidProgram::Time: {0} ::More information::{1}", DateTime.UtcNow, ex.ToString()));
                flag = true;
            }
            return flag;
        }

        private bool IsThereOutlineClass(ProgramUser programUserEntity, DateTime dateTime)
        {
            bool flug = false;
            try
            {
                if (programUserEntity.LastFinishDate.Value.Date != dateTime.Date)
                {
                    if (!programUserEntity.ProgramReference.IsLoaded)
                    {
                        programUserEntity.ProgramReference.Load();
                    }
                    int day = GetOutlineDay(dateTime);
                    if (day != 0)
                    {
                        Session shouldDoDay = Resolve<ISessionRepository>().GetSessionByProgramGuidAndDay(programUserEntity.Program.ProgramGUID, day);
                        if (shouldDoDay != null)
                        {
                            flug = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogMessage(string.Format("Error in IsThereOutlineClass::Time: {0} ::More information::{1}", DateTime.UtcNow, ex.ToString()));
                flug = false;
            }
            return flug;
        }

        //DTD-1001
        public int GetOutlineDay(DateTime dateTime)
        {
            int das = 0;
            switch (dateTime.DayOfWeek)
            {
                case DayOfWeek.Wednesday: das = (int)OutlineDaysEnum.Wed; break;
                case DayOfWeek.Thursday: das = (int)OutlineDaysEnum.Thu; break;
                case DayOfWeek.Friday: das = (int)OutlineDaysEnum.Fri; break;
                case DayOfWeek.Saturday: das = (int)OutlineDaysEnum.Sta; break;
                case DayOfWeek.Sunday: das = (int)OutlineDaysEnum.Sun; break;
            }

            return das;
        }

        //Get CurrentDay for No Catch Up or Catch Up Day
        //CurrentDay means : If today have schedule,return today's Day and if not,return the previous Day.
        public int GetShouldDoDay(Guid programGuid, Guid userGuid, DateTime now)
        {
            int currentDay = int.MinValue;
            //Get ReturnCode
            int isThereSessionReturnCode = Resolve<IProgramUserService>().IsThereClassToday(userGuid, programGuid, now);
            if (isThereSessionReturnCode == 5)
            {
                currentDay = Resolve<IProgramUserService>().GetOutlineDay(now);
            }
            else
            {
                ProgramUser programUserEntity = Resolve<IProgramUserRepository>().GetProgramUserByProgramGuidAndUserGuid(programGuid, userGuid);
                if (programUserEntity != null)
                {
                    if (Resolve<IProgramService>().GetProgramByGUID(programGuid).IsNoCatchUp == false)
                    {
                        currentDay = (programUserEntity.Day.HasValue ? programUserEntity.Day.Value : 0) + 1;
                    }
                    else //No Catch Up Day , Get should do day now
                    {
                        currentDay = Resolve<IStoreProcedure>().ShouldDoDay(programUserEntity.ProgramUserGUID, now);
                    }
                }
            }
            return currentDay;
        }

        public bool PauseProgram(Guid userGuid, Guid programGuid, int day)
        {
            bool isSucceed = false;
            ProgramUser programUser = Resolve<IProgramUserRepository>().GetProgramUserByProgramGuidAndUserGuid(programGuid, userGuid);
            if (programUser.Status != ProgramUserStatusEnum.Paused.ToString())
            {
                PauseProgram(programUser, day);
                isSucceed = true;
            }
            return isSucceed;
        }

        private void PauseProgram(ProgramUser programUser, int day)
        {
            if (!programUser.ProgramReference.IsLoaded) programUser.ProgramReference.Load();
            if (!programUser.UserReference.IsLoaded) programUser.UserReference.Load();
            //set user's current time according to TimeZone.
            DateTime setCurrentTimeByTimeZone = GetCurrentTimeByTimeZone(programUser.Program.ProgramGUID, programUser.User.UserGUID, DateTime.UtcNow);
            programUser.Status = Enum.GetName(typeof(ProgramUserStatusEnum), ProgramUserStatusEnum.Paused);
            programUser.LastPauseDate = setCurrentTimeByTimeZone;
            programUser.LastPauseDay = day;
            // Update PauseDay
            if (programUser.SwitchMessageTime != null)
            {
                if (programUser.SwitchPauseDay != null)
                    programUser.SwitchPauseDay += day;
                else
                    programUser.SwitchPauseDay = day;
            }
            else
            {
                if (programUser.PauseDay != null)
                    programUser.PauseDay += day;
                else
                    programUser.PauseDay = day;
            }
            Resolve<IProgramUserRepository>().Update(programUser);
        }

        private string HPOrderLicenceRegistHandler(string hpSecurity, Guid programGuid, Guid userGuid)
        {
            string returnMessage = string.Empty;
            try
            {
                string[] logstr = StringUtility.MD5Decrypt(hpSecurity, MD5_KEY).Split(';');
                if (logstr.Length >= 4)
                {
                    string hpOrderGuidStr = logstr[3];
                    if (!string.IsNullOrEmpty(hpOrderGuidStr))
                    {
                        Guid hpOrderGuid = new Guid(hpOrderGuidStr);

                        ValidateOrderLicenceResponseModel responseModel = Resolve<IHPOrderLicenceService>().ValidateHPOrderLicence(hpOrderGuid, programGuid);
                        switch (responseModel.Result)
                        {
                            case ResultEnum.Success:
                                HPOrderModel orderModel = Resolve<IHPOrderService>().GetHPOrderModelByHPOrderGuid(hpOrderGuid);
                                ProgramUser pu = Resolve<IProgramUserService>().GetProgramUserByProgramGuidAndUserGuid(orderModel.ProgramGUID, userGuid);
                                if (orderModel != null && pu != null)
                                {
                                    //insert data to HPOrderLicence table
                                    HPOrderLicenceModel orderLicenceModel = new HPOrderLicenceModel
                                    {
                                        HPOrderLicenceGUID = Guid.NewGuid(),
                                        HPOrderGUID = orderModel.HPOrderGUID,
                                        ProgramUserGUID = pu.ProgramUserGUID
                                    };
                                    Resolve<IHPOrderLicenceService>().Insert(orderLicenceModel);
                                }
                                break;
                            case ResultEnum.Error:
                                #region MyRegion
                                //ProgramModel programModel = Resolve<IProgramService>().GetProgramByGUID(new Guid(programGuid));
                                //if (programModel.IsCTPPEnable)
                                //{
                                //    returnMessage = string.Format("{0};{1};{2};{3}", Convert.ToInt32(ReturnMessageCodeEnum.ErrorCode).ToString(), programGuid, userGuid, responseModel.LoginFailedType);
                                //    returnMessage = Convert.ToInt32(ReturnMessageCodeEnum.CtppCode).ToString() + ";" + StringUtility.MD5Encrypt(returnMessage, MD5_KEY);
                                //}
                                //else
                                //{
                                //    returnMessage = Convert.ToInt32(ReturnMessageCodeEnum.ErrorCode).ToString() + ";" + Resolve<ITipMessageService>().GetTipMessageText(new Guid(programGuid), responseModel.LoginFailedType.ToString());
                                //} 
                                #endregion
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, string.Format("Method Name:{0}", "HPOrderLicenceRegistHandler"));
                // add log
                InsertLogModel logmodel = new InsertLogModel
                {
                    ActivityLogType = (int)LogTypeEnum.UpdateProgramUser,
                    Browser = string.Empty,
                    IP = string.Empty,
                    Message = "MethodName : HPOrderLicenceRegistHandler , Exception : " + ex,
                    ProgramGuid = programGuid,
                    SessionGuid = Guid.Empty,
                    UserGuid = userGuid
                };
                Resolve<IActivityLogService>().Insert(logmodel);
            }

            return returnMessage;
        }

        public void SetUserClassStatus(string hpSecurity, Guid userGuid, Guid programGuid, Guid sessionGuid)
        {
            try
            {
                ProgramUser programUser = Resolve<IProgramUserRepository>().GetProgramUserByProgramGuidAndUserGuid(programGuid, userGuid);
                DateTime lastFinishDate = GetCurrentTimeByTimeZone(programGuid, userGuid, DateTime.UtcNow);
                programUser.LastFinishDate = lastFinishDate; //DateTime.UtcNow;
                Session session = Resolve<ISessionRepository>().GetSessionBySessionGuid(sessionGuid);
                if (session.Day.HasValue)
                {
                    if (session.Day >= 0)
                    {
                        programUser.Day = session.Day;
                        Session lastSession = Resolve<ISessionRepository>().GetLastSessionOfProgram(programGuid);

                        //List<ProgramUserSession> programUserSessionList = Resolve<IProgramUserSessionRepository>().GetProgramUserSessionListByProgramUserGuid(programUser.ProgramUserGUID).ToList();
                        //if (programUserSessionList.Count() == lastSession.Day + 1)
                        //{
                        //    programUser.Status = ProgramUserStatus.Completed.ToString();
                        //}
                        if (session.Day.Value == lastSession.Day.Value)
                        {
                            if (!string.IsNullOrEmpty(hpSecurity))
                            {
                                HPOrderLicenceRegistHandler(hpSecurity, programGuid, userGuid);
                            }
                            programUser.Status = ProgramUserStatusEnum.Completed.ToString();
                        }
                        else if (session.Day.Value != 0 && programUser.Status.Equals(ProgramUserStatusEnum.Registered.ToString()))
                        {
                            programUser.Status = ProgramUserStatusEnum.Active.ToString();
                        }
                    }
                }
                else
                {
                    programUser.Day = 0;
                }
                programUser.LastUpdatedBy = userGuid;
                Resolve<IProgramUserRepository>().Update(programUser);
                // add log
                InsertLogModel logmodel = new InsertLogModel
                {
                    ActivityLogType = (int)LogTypeEnum.UpdateProgramUser,
                    Browser = string.Empty,
                    IP = string.Empty,
                    Message = string.Format("Update programuser successful.ProgramUserGuid:{0},Day:{1}", programUser.ProgramUserGUID, programUser.Day.Value),
                    ProgramGuid = programGuid,
                    SessionGuid = sessionGuid,
                    UserGuid = userGuid
                };
                Resolve<IActivityLogService>().Insert(logmodel);

                ProgramUserSession programUserSession = new ProgramUserSession();
                programUserSession.ProgramUserSessionGUID = Guid.NewGuid();
                programUserSession.ProgramUserGUID = programUser.ProgramUserGUID;
                programUserSession.ProgramUser = Resolve<IProgramUserRepository>().GetProgramUserByProgramUserGuid(programUser.ProgramUserGUID);
                programUserSession.SessionGUID = sessionGuid;
                programUserSession.LastUpdated = lastFinishDate;
                programUserSession.IsDeleted = false;
                ProgramUserSession programUserSessionEntity = Resolve<IProgramUserSessionRepository>().GetProgramUserSessionByProgramUserGuidAndSessionGuid(programUserSession.ProgramUserGUID, programUserSession.SessionGUID);
                if (programUserSessionEntity == null)
                {
                    Resolve<IProgramUserSessionRepository>().Insert(programUserSession);
                    // add log
                    InsertLogModel insertLogmodel = new InsertLogModel
                    {
                        ActivityLogType = (int)LogTypeEnum.UpdateProgramUser,
                        Browser = string.Empty,
                        IP = string.Empty,
                        Message = string.Format("Insert programusersession successful.ProgramUserGuid:{0},SessionGuid:{1}", programUser.ProgramUserGUID, sessionGuid),
                        ProgramGuid = programGuid,
                        SessionGuid = sessionGuid,
                        UserGuid = userGuid
                    };
                    Resolve<IActivityLogService>().Insert(logmodel);
                }
                //else
                //{
                //    Resolve<IProgramUserSessionRepository>().Update(programUserSessionEntity);
                //    // add log
                //    InsertLogModel insertLogmodel = new InsertLogModel
                //    {
                //        ActivityLogType = (int)LogTypeEnum.UpdateProgramUser,
                //        Browser = string.Empty,
                //        IP = string.Empty,
                //        Message = string.Format("Update programusersession successful.ProgramUserGuid:{0},SessionGuid:{1}", programUser.ProgramUserGUID, sessionGuid),
                //        ProgramGuid = programGuid,
                //        SessionGuid = sessionGuid,
                //        UserGuid = userGuid
                //    };
                //    Resolve<IActivityLogService>().Insert(insertLogmodel);
                //}
            }
            catch (Exception ex)
            {
                // add log
                InsertLogModel logmodel = new InsertLogModel
                {
                    ActivityLogType = (int)LogTypeEnum.UpdateProgramUser,
                    Browser = string.Empty,
                    IP = string.Empty,
                    Message = "MethodName : SetUserClassStatus , Exception : " + ex,
                    ProgramGuid = programGuid,
                    SessionGuid = sessionGuid,
                    UserGuid = userGuid
                };
                Resolve<IActivityLogService>().Insert(logmodel);
                throw ex;
            }
        }

        #region the test codes, to note the reminder emails in the testlog dt in the old flow. So we can compare the two flows by compare the data in activitylog and testlog
        /// <summary>
        /// It is not the same as IsThereClassToday.
        /// In this function, if a user has missed one or more days, and  there is no schedule for DateTime now, return false
        /// </summary>
        /// <param name="programUser"></param>
        /// <param name="now"></param>
        /// <returns></returns>
        private bool IsShouldSendReminderEmail(ProgramUser programUser, DateTime now)
        {
            bool yes = false;
            try
            {
                if (programUser.Status == ProgramUserStatusEnum.Registered.ToString()
                    || programUser.Status == ProgramUserStatusEnum.Active.ToString()
                    || (programUser.Status.Equals(ProgramUserStatusEnum.Paused.ToString()) && programUser.LastPauseDate.HasValue && programUser.LastPauseDay.HasValue && programUser.LastPauseDate.Value.AddDays(programUser.LastPauseDay.Value).Date <= now.Date))
                {

                    if (!programUser.UserReference.IsLoaded)
                    {
                        programUser.UserReference.Load();
                    }
                    if (!programUser.ProgramReference.IsLoaded)
                    {
                        programUser.ProgramReference.Load();
                    }

                    if (programUser.Program.IsContainTwoParts.HasValue && programUser.Program.IsContainTwoParts.Value == true)
                    {
                        // From Bent:I think we make it easy by saying that in a two-part program there will be no pause option. 
                        if (programUser.SwitchMessageTime.HasValue)
                        {
                            DateTime activeDate = GetShouldActiveDateAfterSwitch(programUser);
                            if (activeDate.Date == now.Date)
                            {
                                yes = true;
                            }
                            else if (now.Date > activeDate.Date)
                            {
                                int activedDays;//It should be the same as that in ShouldSendRemindEmailForNormalProgram function.
                                if (programUser.SwitchPauseDay != null)
                                    activedDays = (now.Date - activeDate.Date).Days - Convert.ToInt32(programUser.SwitchPauseDay);
                                else
                                    activedDays = (now.Date - activeDate.Date).Days;

                                int week = GetActiveWeeks(activedDays, activeDate);
                                int firstPartWeeks = Resolve<IProgramScheduleRepository>().GetProgramScheduleByProgramAndDay(programUser.Program.ProgramGUID, programUser.Program.SwitchDay.Value - 1).Week.Value;

                                //int weekday = activedDays % 7;
                                int weekday = now.DayOfWeek != DayOfWeek.Sunday ? (int)now.DayOfWeek : 7;
                                ProgramSchedule shceduleshould = Resolve<IProgramScheduleRepository>().GetProgramScheduleByProgramWeekWeekday(programUser.Program.ProgramGUID, week + firstPartWeeks, weekday);
                                if (shceduleshould != null)
                                {
                                    yes = true;
                                }
                            }
                            else
                            {
                                // for outlines ( days before start program)
                                if (IsThereOutlineClass(programUser, now))
                                {
                                    yes = true;
                                }
                            }

                            return yes;
                        }
                        else
                        {
                            // not switch
                            // need judge whether switchTime is null in sp.
                            DateTime switchTime = GetShouldSwichTime(programUser);
                            if (now < switchTime)
                            {
                                yes = ShouldSendRemindEmailForNormalProgram(programUser, now);
                            }
                        }
                    }
                    else
                    {
                        yes = ShouldSendRemindEmailForNormalProgram(programUser, now);
                    }
                }
            }
            catch (Exception ex)
            {
                if (!programUser.UserReference.IsLoaded)
                {
                    programUser.UserReference.Load();
                }

                Console.WriteLine("Error occurs when check user {0}.", programUser.User.Email);
                Console.WriteLine("Detail exception: {0}", ex.ToString());
            }

            return yes;
        }
        #endregion

        private DateTime GetShouldSwichTime(ProgramUser programUser)
        {
            if (!programUser.ProgramReference.IsLoaded)
            {
                programUser.ProgramReference.Load();
            }
            ProgramSchedule schedule = Resolve<IProgramScheduleRepository>().GetFirstDayScheduleOfProgram(programUser.Program.ProgramGUID);
            DateTime activeDate = GetShouldActiveDate(programUser, schedule);
            ProgramSchedule lastScheduleOfFirstPart = Resolve<IProgramScheduleRepository>().GetProgramScheduleByProgramAndDay(programUser.Program.ProgramGUID, programUser.Program.SwitchDay.Value - 1);
            return activeDate.AddDays(Convert.ToDouble((lastScheduleOfFirstPart.Week - 1) * 7 + lastScheduleOfFirstPart.WeekDay - 1));
        }

        #region #region the test codes, to note the reminder emails in the testlog dt in the old flow. So we can compare the two flows by compare the data in activitylog and testlog
        private bool ShouldSendRemindEmailForNormalProgram(ProgramUser programUser, DateTime now)
        {
            bool yes = false;
            if (!programUser.ProgramReference.IsLoaded)
            {
                programUser.ProgramReference.Load();
            }
            ProgramSchedule schedule = Resolve<IProgramScheduleRepository>().GetFirstDayScheduleOfProgram(programUser.Program.ProgramGUID);
            if (schedule != null)
            {
                DateTime activeDate = GetShouldActiveDate(programUser, schedule);

                if (activeDate.Date == now.Date)
                {
                    yes = true;
                }
                else if (now.Date > activeDate.Date)
                {
                    int activedDays;
                    if (programUser.PauseDay != null)
                        activedDays = (now.Date - activeDate.Date).Days - Convert.ToInt32(programUser.PauseDay);
                    else
                        activedDays = (now.Date - activeDate.Date).Days;

                    int week = GetActiveWeeks(activedDays, activeDate);
                    //int weekday = activedDays % 7;
                    int weekday = now.DayOfWeek != DayOfWeek.Sunday ? (int)now.DayOfWeek : 7;
                    ProgramSchedule shceduleshould = Resolve<IProgramScheduleRepository>().GetProgramScheduleByProgramWeekWeekday(programUser.Program.ProgramGUID, week, weekday);
                    if (shceduleshould != null)
                    {
                        yes = true;
                    }
                }
                else
                {
                    // for outlines ( days before start program)
                    if (IsThereOutlineClass(programUser, now))
                    {
                        yes = true;
                    }
                }
            }
            return yes;
        }
        #endregion

        private int GetActiveWeeks(int activedDays, DateTime activeDate)
        {
            int week = 0;
            int activeweekday = activeDate.DayOfWeek != DayOfWeek.Sunday ? (int)activeDate.DayOfWeek : 7;
            if ((activeweekday + activedDays) % 7 == 0)
            {
                week = (activeweekday + activedDays) / 7;
            }
            else
            {
                week = (activeweekday + activedDays) / 7 + 1;
            }
            return week;
        }

        public DateTime? ExpectSessionDate(Guid programGuid, Guid userGuid, int currentDay)
        {
            DateTime? expectSessionDate = null;
            ProgramUser pu = Resolve<IProgramUserRepository>().GetProgramUserByProgramGuidAndUserGuid(programGuid, userGuid);
            Win8ProgramUser win8Pu = Resolve<IWin8ProgramUserRepository>().GetWin8ProgramUserByProgramUserGuid(pu.ProgramUserGUID);
            if (win8Pu == null)
            {
                if (!pu.ProgramReference.IsLoaded) pu.ProgramReference.Load();
                ProgramSchedule schedule = Resolve<IProgramScheduleRepository>().GetFirstDayScheduleOfProgram(pu.Program.ProgramGUID);
                DateTime activeDate = GetShouldActiveDate(pu, schedule);

                if (currentDay > 0)
                {
                    ProgramSchedule currentDaySchedule = Resolve<IProgramScheduleRepository>().GetProgramScheduleByProgramAndDay(pu.Program.ProgramGUID, currentDay - 1);
                    if (currentDaySchedule != null)
                    {
                        if (pu.PauseDay != null)
                            expectSessionDate = activeDate.AddDays(Convert.ToDouble(currentDaySchedule.WeekDay - 1 + (currentDaySchedule.Week - 1) * 7) + Convert.ToDouble(pu.PauseDay));
                        else
                            expectSessionDate = activeDate.AddDays(Convert.ToDouble(currentDaySchedule.WeekDay - 1 + (currentDaySchedule.Week - 1) * 7));
                    }
                }
            }
            else
            {
                int sessionCount = Resolve<ISessionService>().GetLastSessionDay(programGuid);
                if (pu != null)
                {
                    if (pu.StartDate.HasValue && pu.Day.Value < sessionCount + 1)
                    {
                        expectSessionDate = pu.StartDate.Value.AddDays(currentDay);//pu.Day.Value + 1
                        expectSessionDate = Resolve<IProgramUserService>().GetCurrentTimeByTimeZone(programGuid, userGuid, expectSessionDate.Value);
                        if (pu.PauseDay != null)
                        {
                            expectSessionDate = expectSessionDate.Value.AddDays(-pu.PauseDay.Value);
                        }
                    }
                }
            }

            return expectSessionDate;
        }

        private DateTime GetShouldActiveDate(ProgramUser programUser, ProgramSchedule schedule)
        {
            if (!programUser.ProgramReference.IsLoaded)
            {
                programUser.ProgramReference.Load();
            }
            if (!programUser.UserReference.IsLoaded)
            {
                programUser.UserReference.Load();
            }

            DateTime shouldActiveDay = DateTime.UtcNow;
            //ProgramSchedule schedule = Resolve<IProgramRepository>().GetFirstDayScheduleOfProgram(programUser.Program.ProgramGUID);
            int startWeekday = programUser.StartDate.Value.DayOfWeek != DayOfWeek.Sunday ? (int)programUser.StartDate.Value.DayOfWeek : 7;
            if (startWeekday > schedule.WeekDay)
            {
                shouldActiveDay = programUser.StartDate.Value.AddDays(Convert.ToDouble(7 - (startWeekday - schedule.WeekDay)));
            }
            else if (startWeekday < schedule.WeekDay)
            {
                shouldActiveDay = programUser.StartDate.Value.AddDays(Convert.ToDouble(schedule.WeekDay - startWeekday));
            }
            else if (startWeekday == schedule.WeekDay)
            {
                shouldActiveDay = programUser.StartDate.Value;
            }

            return shouldActiveDay;
        }


        private bool IsHasReceiveLastDayEmailTwice(ProgramUser programUser)
        {
            bool flug = false;
            if (programUser.LastSendEmailTime.HasValue)
            {
                if (!programUser.ProgramReference.IsLoaded)
                {
                    programUser.ProgramReference.Load();
                }
                Session lastSession = Resolve<ISessionRepository>().GetLastSessionOfProgram(programUser.Program.ProgramGUID);
                if (((programUser.Day + 1) >= lastSession.Day) && (programUser.LastFinishDate.Value.AddDays(2).Date <= programUser.LastSendEmailTime.Value.Date))
                {
                    flug = true;
                }
            }
            return flug;
        }

        private bool IsLongTimeNotBeenIn(ProgramUser programUser)
        {
            bool flug = false;
            Setting settingday = Resolve<ISpecialStringRepository>().GetSettingsByName("AbsentDay");
            if (programUser.LastFinishDate.HasValue && programUser.LastSendEmailTime.HasValue && (programUser.LastSendEmailTime.Value.Date >= programUser.LastFinishDate.Value.AddDays(Convert.ToInt32(settingday.Value)).Date))
            {
                flug = true;
            }

            return flug;
        }

        /// <remarks>
        /// 2010-04-22: [Chen Pu] Once registered user enter Day 1, set its status to Active instead of after he finish Day 1
        /// </remarks>
        public string GetUserClass(Guid userGuid, Guid programGuid, Guid languageGuid)
        {
            string liveSessionModelXML = string.Empty;
            ProgramUser programUser = Resolve<IProgramUserRepository>().GetProgramUserByProgramGuidAndUserGuid(programGuid, userGuid);
            if (programUser.Day.HasValue)
            {
                liveSessionModelXML = Resolve<IStoreProcedure>().GetLiveSessionModelAsXML(userGuid, programGuid, languageGuid, programUser.Day.Value + 1);

                if (programUser.Day.Value == 1 && programUser.Status.Equals(ProgramUserStatusEnum.Registered.ToString()))
                {
                    programUser.Status = ProgramUserStatusEnum.Active.ToString();
                    Resolve<IProgramUserRepository>().Update(programUser);
                }
            }
            return liveSessionModelXML;
        }

        public void ExitProgram(Guid userGuid, Guid programGuid)
        {
            ProgramUser programUserEntity = Resolve<IProgramUserRepository>().GetProgramUserByProgramGuidAndUserGuid(programGuid, userGuid);
            programUserEntity.Status = ProgramUserStatusEnum.Terminated.ToString();
            Resolve<IProgramUserRepository>().Update(programUserEntity);
        }

        public RegisterMessageModel Win8EndUserJoinProgram(RegisterWin8ProgramUsersModel registerWin8ProgramUser)
        {
            bool flag = false;
            RegisterMessageModel registerMessageModel = new RegisterMessageModel();
            registerMessageModel.Result = JoinProgramResultEnum.Success;

            //win8 Test LiveID.
            List<string> testWin8Users = new List<string> { "win8test2000", "0d8ad8785efc61f7", "9003ed19c8d3898d", "f589aaf2fd5d94f5", "214d8ef1d0652fe8", "dbf8b112fc78ffb4" };
            if (testWin8Users.Contains(registerWin8ProgramUser.WindowsLiveId))
            {
                string windowsLiveIdTestEmail = registerWin8ProgramUser.WindowsLiveId + "@changetech.no";
                List<Win8ProgramUser> win8Pus = Resolve<IWin8ProgramUserRepository>().GetWindowsLiveByWindowsLiveId(registerWin8ProgramUser.WindowsLiveId).ToList();

                foreach (string programGuidStr in registerWin8ProgramUser.ProgramIds)
                {
                    foreach (Win8ProgramUser win8Pu in win8Pus)
                    {
                        //initial programuser 
                        if (!win8Pu.ProgramUserReference.IsLoaded) win8Pu.ProgramUserReference.Load();
                        if (!win8Pu.ProgramUser.ProgramReference.IsLoaded) win8Pu.ProgramUser.ProgramReference.Load();
                        if (!win8Pu.ProgramUser.UserReference.IsLoaded) win8Pu.ProgramUser.UserReference.Load();
                        if (programGuidStr.ToUpper() == win8Pu.ProgramUser.Program.ProgramGUID.ToString().ToUpper())
                        {
                            flag = true;
                        }
                    }

                    if (flag)
                    {
                        foreach (Win8ProgramUser win8Pu in win8Pus)
                        {
                            if (programGuidStr.ToUpper() == win8Pu.ProgramUser.Program.ProgramGUID.ToString().ToUpper())
                            {
                                ProgramUser pu = win8Pu.ProgramUser;
                                pu.StartDate = Resolve<IProgramUserService>().GetCurrentTimeByTimeZone(win8Pu.ProgramUser.Program.ProgramGUID, win8Pu.ProgramUser.User.UserGUID, DateTime.UtcNow);
                                pu.LastFinishDate = DateTime.UtcNow;
                                pu.Day = 0;
                                pu.Status = Enum.GetName(typeof(ProgramUserStatusEnum), ProgramUserStatusEnum.Registered);
                                pu.LastUpdated = null;
                                Resolve<IProgramUserRepository>().Update(pu);
                                //delete programusersession's infos
                                Resolve<IProgramUserSessionRepository>().DeleteEntities(win8Pu.ProgramUser.ProgramUserGUID);
                            }
                        }
                        flag = false;
                    }
                    else
                    {
                        Guid programGuid = GetProgramGuid(programGuidStr);
                        UserModel userModel = new UserModel
                        {
                            UserGuid = Guid.NewGuid(),
                            UserName = windowsLiveIdTestEmail,
                            PassWord = DEFAULT_PASSWORD,
                            ProgramGuid = programGuid,
                            PhoneNumber = "temp",
                            UserType = UserTypeEnum.User
                        };
                        if (Regex.IsMatch(windowsLiveIdTestEmail.ToLower(), "^[_a-z0-9-]+(.[_a-z0-9-]+)*@[a-z0-9-]+(.[a-z0-9-]+)*$"))
                        {
                            RegisterProgramUserModel registerProgramUser = new RegisterProgramUserModel()
                            {
                                UserName = userModel.UserName,
                                Password = userModel.PassWord,
                                ProgramGuid = userModel.ProgramGuid,
                                UserGuid = userModel.UserGuid
                            };
                            User existedUser = Resolve<IUserRepository>().GetUserByEmail(windowsLiveIdTestEmail, programGuid);
                            if (existedUser != null)
                            {
                                if (existedUser.UserType == (int)UserTypeEnum.Tester)
                                {
                                    existedUser.IsDeleted = true;
                                    Resolve<IUserRepository>().UpdateUser(existedUser);
                                    UpdateRegisterUser(registerProgramUser);
                                }
                                else
                                {
                                    ProgramUser programuser = Resolve<IProgramUserRepository>().GetProgramUserByProgramGuidAndUserGuid(programGuid, existedUser.UserGUID);
                                    if (programuser != null)
                                    {
                                        if (programuser.Status == ProgramUserStatusEnum.Completed.ToString() || programuser.Status == ProgramUserStatusEnum.Screening.ToString())
                                        {
                                            Resolve<IProgramUserRepository>().DeleteProgramUserByProgramGuidAndUserGuid(programGuid, existedUser.UserGUID);
                                            // delete existed usere
                                            existedUser.IsDeleted = true;
                                            Resolve<IUserRepository>().UpdateUser(existedUser);
                                            UpdateRegisterUser(registerProgramUser);
                                        }
                                        else
                                        {
                                            registerMessageModel.Result = JoinProgramResultEnum.RegisteredInProgram;
                                            registerMessageModel.ErrorMessage += string.Format("{0};", programGuid);
                                        }
                                    }
                                    else
                                    {
                                        // delete existed user
                                        existedUser.IsDeleted = true;
                                        Resolve<IUserRepository>().UpdateUser(existedUser);
                                    }
                                }
                            }
                            else
                            {
                                if (RegisterWin8UserAndJoinProgram(registerWin8ProgramUser.WindowsLiveId, userModel))
                                {
                                    registerMessageModel.Result = JoinProgramResultEnum.Success;
                                }
                                else
                                {
                                    registerMessageModel.Result = JoinProgramResultEnum.RegisteredFailed;
                                }
                            }
                        }
                        else
                        {
                            registerMessageModel.Result = JoinProgramResultEnum.InvalidEmailAddress;
                        }
                        // if success, send register email
                        if (registerMessageModel.Result == JoinProgramResultEnum.Success)
                        {
                            if (!Resolve<IProgramService>().IsProgramNeedPay(programGuid))
                            {
                                Resolve<IEmailService>().SendRegisterEmail(userModel.UserGuid, programGuid);
                            }
                        }
                    }
                    #region Old follows
                    //foreach (Win8ProgramUser win8Pu in win8Pus)
                    //{
                    //    //initial programuser 
                    //    if (!win8Pu.ProgramUserReference.IsLoaded) win8Pu.ProgramUserReference.Load();
                    //    if (!win8Pu.ProgramUser.ProgramReference.IsLoaded) win8Pu.ProgramUser.ProgramReference.Load();
                    //    if (programGuidStr.ToUpper() == win8Pu.ProgramUser.Program.ProgramGUID.ToString().ToUpper())
                    //    {
                    //        ProgramUser pu = win8Pu.ProgramUser;
                    //        pu.LastFinishDate = pu.StartDate.Value;
                    //        pu.Day = 0;
                    //        pu.Status = Enum.GetName(typeof(ProgramUserStatusEnum), ProgramUserStatusEnum.Registered);
                    //        pu.LastUpdated = null;
                    //        Resolve<IProgramUserRepository>().Update(pu);
                    //        //delete programusersession's infos
                    //        Resolve<IProgramUserSessionRepository>().DeleteEntities(win8Pu.ProgramUser.ProgramUserGUID);
                    //    }
                    //    else
                    //    {
                    //        Guid programGuid = GetProgramGuid(programGuidStr);
                    //        UserModel userModel = new UserModel
                    //        {
                    //            UserGuid = Guid.NewGuid(),
                    //            UserName = windowsLiveIdTestEmail,
                    //            PassWord = DEFAULT_PASSWORD,
                    //            ProgramGuid = programGuid,
                    //            PhoneNumber = "temp",
                    //            UserType = UserTypeEnum.User
                    //        };
                    //        if (Regex.IsMatch(windowsLiveIdTestEmail.ToLower(), "^[_a-z0-9-]+(.[_a-z0-9-]+)*@[a-z0-9-]+(.[a-z0-9-]+)*$"))
                    //        {
                    //            RegisterProgramUserModel registerProgramUser = new RegisterProgramUserModel()
                    //            {
                    //                UserName = userModel.UserName,
                    //                Password = userModel.PassWord,
                    //                ProgramGuid = userModel.ProgramGuid,
                    //                UserGuid = userModel.UserGuid
                    //            };
                    //            User existedUser = Resolve<IUserRepository>().GetUserByEmail(windowsLiveIdTestEmail, programGuid);
                    //            if (existedUser != null)
                    //            {
                    //                if (existedUser.UserType == (int)UserTypeEnum.Tester)
                    //                {
                    //                    existedUser.IsDeleted = true;
                    //                    Resolve<IUserRepository>().UpdateUser(existedUser);
                    //                    UpdateRegisterUser(registerProgramUser);
                    //                }
                    //                else
                    //                {
                    //                    ProgramUser programuser = Resolve<IProgramUserRepository>().GetProgramUserByProgramGuidAndUserGuid(programGuid, existedUser.UserGUID);
                    //                    if (programuser != null)
                    //                    {
                    //                        if (programuser.Status == ProgramUserStatusEnum.Completed.ToString() || programuser.Status == ProgramUserStatusEnum.Screening.ToString())
                    //                        {
                    //                            Resolve<IProgramUserRepository>().DeleteProgramUserByProgramGuidAndUserGuid(programGuid, existedUser.UserGUID);
                    //                            // delete existed usere
                    //                            existedUser.IsDeleted = true;
                    //                            Resolve<IUserRepository>().UpdateUser(existedUser);
                    //                            UpdateRegisterUser(registerProgramUser);
                    //                        }
                    //                        else
                    //                        {
                    //                            registerMessageModel.Result = JoinProgramResultEnum.RegisteredInProgram;
                    //                            registerMessageModel.ErrorMessage += string.Format("{0};", programGuid);
                    //                        }
                    //                    }
                    //                    else
                    //                    {
                    //                        // delete existed user
                    //                        existedUser.IsDeleted = true;
                    //                        Resolve<IUserRepository>().UpdateUser(existedUser);
                    //                    }
                    //                }
                    //            }
                    //            else
                    //            {
                    //                if (RegisterWin8UserAndJoinProgram(registerWin8ProgramUser.WindowsLiveId, userModel))
                    //                {
                    //                    registerMessageModel.Result = JoinProgramResultEnum.Success;
                    //                }
                    //                else
                    //                {
                    //                    registerMessageModel.Result = JoinProgramResultEnum.RegisteredFailed;
                    //                }
                    //            }
                    //        }
                    //        else
                    //        {
                    //            registerMessageModel.Result = JoinProgramResultEnum.InvalidEmailAddress;
                    //        }
                    //        // if success, send register email
                    //        if (registerMessageModel.Result == JoinProgramResultEnum.Success)
                    //        {
                    //            if (!Resolve<IProgramService>().IsProgramNeedPay(programGuid))
                    //            {
                    //                Resolve<IEmailService>().SendRegisterEmail(userModel.UserGuid, programGuid);
                    //            }
                    //        }
                    //    }
                    //} 
                    #endregion
                }
            }
            else //The register funtionally by normal win8 end-users .
            {
                string windowsLiveIdEmail = registerWin8ProgramUser.WindowsLiveId + "@changetech.no";
                EMail mail = new EMail();
                try
                {
                    mail.To.Add(new MailAddress(windowsLiveIdEmail));
                }
                catch
                {
                    registerMessageModel.Result = JoinProgramResultEnum.InvalidEmailAddress;
                    return registerMessageModel;
                }
                foreach (string programId in registerWin8ProgramUser.ProgramIds)
                {
                    Guid programGuid = GetProgramGuid(programId);
                    UserModel userModel = new UserModel
                    {
                        UserGuid = Guid.NewGuid(),
                        UserName = windowsLiveIdEmail,
                        PassWord = DEFAULT_PASSWORD,
                        ProgramGuid = programGuid,
                        PhoneNumber = "temp",
                        UserType = UserTypeEnum.User
                    };
                    if (Regex.IsMatch(windowsLiveIdEmail.ToLower(), "^[_a-z0-9-]+(.[_a-z0-9-]+)*@[a-z0-9-]+(.[a-z0-9-]+)*$"))
                    {
                        RegisterProgramUserModel registerProgramUser = new RegisterProgramUserModel()
                        {
                            UserName = userModel.UserName,
                            Password = userModel.PassWord,
                            ProgramGuid = userModel.ProgramGuid,
                            UserGuid = userModel.UserGuid
                        };
                        User existedUser = Resolve<IUserRepository>().GetUserByEmail(windowsLiveIdEmail, programGuid);
                        if (existedUser != null)
                        {
                            if (existedUser.UserType == (int)UserTypeEnum.Tester)
                            {
                                existedUser.IsDeleted = true;
                                Resolve<IUserRepository>().UpdateUser(existedUser);
                                UpdateRegisterUser(registerProgramUser);
                            }
                            else
                            {
                                ProgramUser programuser = Resolve<IProgramUserRepository>().GetProgramUserByProgramGuidAndUserGuid(programGuid, existedUser.UserGUID);
                                if (programuser != null)
                                {
                                    if (programuser.Status == ProgramUserStatusEnum.Completed.ToString() || programuser.Status == ProgramUserStatusEnum.Screening.ToString())
                                    {
                                        Resolve<IProgramUserRepository>().DeleteProgramUserByProgramGuidAndUserGuid(programGuid, existedUser.UserGUID);
                                        // delete existed usere
                                        existedUser.IsDeleted = true;
                                        Resolve<IUserRepository>().UpdateUser(existedUser);
                                        UpdateRegisterUser(registerProgramUser);
                                    }
                                    else
                                    {
                                        registerMessageModel.Result = JoinProgramResultEnum.RegisteredInProgram;
                                        registerMessageModel.ErrorMessage += string.Format("{0};", programGuid);
                                    }
                                }
                                else
                                {
                                    // delete existed user
                                    existedUser.IsDeleted = true;
                                    Resolve<IUserRepository>().UpdateUser(existedUser);
                                }
                            }
                        }
                        else
                        {
                            if (RegisterWin8UserAndJoinProgram(registerWin8ProgramUser.WindowsLiveId, userModel))
                            {
                                registerMessageModel.Result = JoinProgramResultEnum.Success;
                            }
                            else
                            {
                                registerMessageModel.Result = JoinProgramResultEnum.RegisteredFailed;
                            }
                        }
                    }
                    else
                    {
                        registerMessageModel.Result = JoinProgramResultEnum.InvalidEmailAddress;
                    }
                    // if success, send register email
                    if (registerMessageModel.Result == JoinProgramResultEnum.Success)
                    {
                        if (!Resolve<IProgramService>().IsProgramNeedPay(programGuid))
                        {
                            Resolve<IEmailService>().SendRegisterEmail(userModel.UserGuid, programGuid);
                        }
                    }
                }
            }

            return registerMessageModel;
        }

        private bool RegisterWin8UserAndJoinProgram(string windowsLiveId, UserModel user)
        {
            bool flag = false;
            try
            {
                Program programentity = Resolve<IProgramRepository>().GetProgramByGuid(user.ProgramGuid);
                //DateTime setCurrentByTimeZone = Resolve<IProgramUserService>().GetCurrentTimeByTimeZone(programGuid, user.UserGuid, DateTime.UtcNow);
                if (programentity != null)
                {
                    User newuser = new User();
                    newuser.UserGUID = user.UserGuid;
                    newuser.Email = user.UserName;
                    newuser.Password = user.PassWord;
                    newuser.MobilePhone = user.PhoneNumber;
                    newuser.Gender = user.Gender.ToString().Trim();
                    newuser.FirstName = user.FirstName;
                    newuser.LastName = user.LastName;
                    newuser.Security = 0;
                    newuser.LastLogon = DateTime.UtcNow;
                    newuser.UserType = (int)user.UserType;
                    newuser.ProgramGUID = user.ProgramGuid;
                    if (programentity.IsWithPay.HasValue && programentity.IsWithPay.Value == true)
                    {
                        newuser.IsPaid = true;
                    }
                    // if program need pincode, create a pincode for user when registering
                    if (programentity.IsNeedPinCode.HasValue && programentity.IsNeedPinCode.Value == true)
                    {
                        newuser.PinCode = ServiceUtility.GetPinCode();
                    }
                    try
                    {
                        Resolve<IUserRepository>().Register(newuser);
                    }
                    catch (Exception ex)
                    {
                        InsertLogModel insertLogModel = new InsertLogModel
                        {
                            ActivityLogType = (int)LogTypeEnum.RegisterWin8ProgramUser,
                            Browser = string.Empty,
                            From = string.Empty,
                            IP = string.Empty,
                            Message = "Register Win8User Failed!" + ex.Message,
                            PageGuid = Guid.Empty,
                            PageSequenceGuid = Guid.Empty,
                            SessionGuid = Guid.Empty,
                            ProgramGuid = user.ProgramGuid,
                            UserGuid = user.UserGuid
                        };
                        Resolve<IActivityLogService>().Insert(insertLogModel);
                    }

                    DateTime setCurrentTimeByTimeZone = Resolve<IProgramUserService>().GetCurrentTimeByTimeZone(user.ProgramGuid, newuser.UserGUID, DateTime.UtcNow);
                    ProgramUser win8ProgramUser = Resolve<IProgramUserRepository>().GetProgramUserByProgramGuidAndUserGuid(user.ProgramGuid, newuser.UserGUID);
                    if (win8ProgramUser == null)
                    {
                        ProgramUser pu = new ProgramUser();
                        pu.ProgramUserGUID = Guid.NewGuid();
                        pu.Security = (int)PermissionEnum.ProgramView;
                        pu.Program = Resolve<IProgramRepository>().GetProgramByGuid(user.ProgramGuid);
                        pu.User = Resolve<IUserRepository>().GetUserByGuid(newuser.UserGUID);
                        pu.MailTime = 3;
                        pu.StartDate = setCurrentTimeByTimeZone; //DateTime.UtcNow;
                        pu.Day = 0;
                        pu.LastFinishDate = DateTime.UtcNow;
                        pu.LastUpdatedBy = pu.ProgramUserGUID;
                        pu.RegisteredIP = string.Empty;
                        pu.Status = Enum.GetName(typeof(ProgramUserStatusEnum), ProgramUserStatusEnum.Registered);
                        //if (companyguid != Guid.Empty)
                        //{
                        //    pu.Company = Resolve<ICompanyRepository>().GetCompanyByGuid(companyguid);
                        //}

                        //if (!string.IsNullOrEmpty(studyContent))
                        //{
                        //    pu.StudyContent = Resolve<IStudyContentRepository>().Get(new Guid(studyContent));
                        //}
                        try
                        {
                            Resolve<IProgramUserRepository>().Insert(pu);
                        }
                        catch (Exception ex)
                        {
                            InsertLogModel insertLogModelByPu = new InsertLogModel
                            {
                                ActivityLogType = (int)LogTypeEnum.RegisterWin8ProgramUser,
                                Browser = string.Empty,
                                From = string.Empty,
                                IP = string.Empty,
                                Message = "Register Win8ProgramUser Failed!" + ex.Message,
                                PageGuid = Guid.Empty,
                                PageSequenceGuid = Guid.Empty,
                                SessionGuid = Guid.Empty,
                                ProgramGuid = user.ProgramGuid,
                                UserGuid = user.UserGuid
                            };
                            Resolve<IActivityLogService>().Insert(insertLogModelByPu);
                        }

                        try
                        {
                            //insert Win8ProgramUser entity.
                            Win8ProgramUser win8Pu = new Win8ProgramUser
                            {
                                Win8ProgramUserGUID = Guid.NewGuid(),
                                WindowsLiveId = windowsLiveId,
                                ProgramUser = pu,
                                ProgramUserGUID = pu.ProgramUserGUID,
                            };
                            Resolve<IWin8ProgramUserRepository>().Insert(win8Pu);
                        }
                        catch (Exception ex)
                        {
                            InsertLogModel insertLogModelByPu = new InsertLogModel
                            {
                                ActivityLogType = (int)LogTypeEnum.RegisterWin8ProgramUser,
                                Browser = string.Empty,
                                From = string.Empty,
                                IP = string.Empty,
                                Message = "Insert Win8ProgramUser Failed!" + ex.Message,
                                PageGuid = Guid.Empty,
                                PageSequenceGuid = Guid.Empty,
                                SessionGuid = Guid.Empty,
                                ProgramGuid = user.ProgramGuid,
                                UserGuid = user.UserGuid
                            };
                            Resolve<IActivityLogService>().Insert(insertLogModelByPu);
                        }
                        flag = true;
                    }
                }
                return flag;
            }
            catch (Exception ex)
            {
                InsertLogModel insertLogModel = new InsertLogModel
                {
                    ActivityLogType = (int)LogTypeEnum.RegisterWin8ProgramUser,
                    Browser = string.Empty,
                    From = string.Empty,
                    IP = string.Empty,
                    Message = "Register Win8ProgramUser Failed!" + ex.Message,
                    PageGuid = Guid.Empty,
                    PageSequenceGuid = Guid.Empty,
                    SessionGuid = Guid.Empty,
                    ProgramGuid = user.ProgramGuid,
                    UserGuid = user.UserGuid
                };
                Resolve<IActivityLogService>().Insert(insertLogModel);
                throw ex;
            }
        }

        private void UpdateRegisterWin8User(RegisterProgramUserModel registerUserModel)
        {
            // update user
            User registerUser = Resolve<IUserRepository>().GetUserByGuid(registerUserModel.UserGuid);
            registerUser.Email = registerUserModel.UserName;
            registerUser.Password = registerUserModel.Password;
            Program programentity = Resolve<IProgramRepository>().GetProgramByGuid(registerUserModel.ProgramGuid);
            if (programentity.IsWithPay.HasValue && programentity.IsWithPay.Value == true)
            {
                registerUser.IsPaid = true;
            }
            if (!string.IsNullOrEmpty(registerUserModel.PhoneNumber))
            {
                registerUser.MobilePhone = registerUserModel.PhoneNumber;
            }
            if (!string.IsNullOrEmpty(registerUserModel.SerialNumber))
            {
                registerUser.SerialNumber = registerUserModel.SerialNumber;
            }
            registerUser.IsDeleted = false;
            Resolve<IUserRepository>().UpdateUser(registerUser);

            // update program user
            ProgramUser pu = Resolve<IProgramUserRepository>().GetProgramUserByProgramGuidAndUserGuid(registerUserModel.ProgramGuid, registerUserModel.UserGuid);
            //set user's currenttime according to TimeZone.
            DateTime setCurrentTimeByTimeZone = DateTime.UtcNow.AddMinutes(Convert.ToDouble(registerUserModel.UserTimeZone) * 60);
            if (pu != null)
            {
                if (!pu.ProgramReference.IsLoaded)
                {
                    pu.ProgramReference.Load();
                }
                pu.Status = ProgramUserStatusEnum.Active.ToString();
                pu.StartDate = setCurrentTimeByTimeZone;//DateTime.UtcNow;
                pu.LastUpdated = setCurrentTimeByTimeZone;//DateTime.UtcNow;
                pu.MailTime = 3;
                pu.Day = 0;
                pu.LastFinishDate = setCurrentTimeByTimeZone;//DateTime.UtcNow;
                pu.LastUpdatedBy = registerUserModel.UserGuid;
                pu.TimeZone = registerUserModel.UserTimeZone;

                //add log for timezone changed
                string logMessage = " TimeZone : " + pu.TimeZone.Value + " ---> TimeZone : " + registerUserModel.UserTimeZone;
                InsertLogModel insertLogModel = new InsertLogModel
                {
                    ActivityLogType = (int)LogTypeEnum.ModifyProgram,
                    Browser = string.Empty,
                    From = string.Empty,
                    IP = string.Empty,
                    Message = logMessage,
                    PageGuid = Guid.Empty,
                    PageSequenceGuid = Guid.Empty,
                    SessionGuid = Guid.Empty,
                    ProgramGuid = registerUserModel.ProgramGuid,
                    UserGuid = registerUserModel.UserGuid
                };
                Resolve<IActivityLogService>().Insert(insertLogModel);

                Resolve<IProgramUserRepository>().Update(pu);
            }
        }

        private bool IsRegisterUserByProgram(RegisterProgramUserModel registerProgramUser)
        {
            bool flag = false;
            User userEntity = Resolve<IUserRepository>().GetUserByEmailAndProgramNotSA(registerProgramUser.UserName, registerProgramUser.ProgramGuid);
            ProgramUser puEntity = Resolve<IProgramUserRepository>().GetProgramUserByProgramGuidAndUserGuid(registerProgramUser.ProgramGuid, userEntity.UserGUID);
            if (puEntity != null && puEntity.Status == ProgramUserStatusEnum.Terminated.ToString())
            {
                flag = true;
            }

            return flag;
        }

        public JoinProgramResultEnum EndUserJoinProgram(RegisterProgramUserModel registerProgramUser)
        {
            JoinProgramResultEnum resultType = JoinProgramResultEnum.Success;
            EMail mail = new EMail();
            try
            {
                mail.To.Add(new MailAddress(registerProgramUser.UserName));
            }
            catch
            {
                return JoinProgramResultEnum.InvalidEmailAddress;
            }
            if (Regex.IsMatch(registerProgramUser.UserName.ToLower(), "^[_a-z0-9-]+(.[_a-z0-9-]+)*@[a-z0-9-]+(.[a-z0-9-]+)*$"))
            {
                if (IsUniqueMobile(registerProgramUser.ProgramGuid.ToString(), registerProgramUser.UserGuid.ToString(), registerProgramUser.PhoneNumber))
                {
                    User existedUser = Resolve<IUserRepository>().GetUserByEmail(registerProgramUser.UserName, registerProgramUser.ProgramGuid);

                    // nomal user
                    if (existedUser != null)
                    {
                        bool isRegisterUserByProgram = IsRegisterUserByProgram(registerProgramUser);
                        if (existedUser.UserType == (int)UserTypeEnum.Tester || isRegisterUserByProgram)
                        {   // for tester user
                            // delete existed user
                            existedUser.IsDeleted = true;
                            Resolve<IUserRepository>().UpdateUser(existedUser);
                            UpdateRegisterUser(registerProgramUser); //(new Guid(registerUser.ProgramGuid), new Guid(registerUser.UserGuid), registerUser.RegisterEmail, registerUser.RegisterPassword, registerUser.Mobile, registerUser.SerialNumber, registerUser.TimeZone);
                        }
                        else
                        {
                            ProgramUser programuser = Resolve<IProgramUserRepository>().GetProgramUserByProgramGuidAndUserGuid(registerProgramUser.ProgramGuid, existedUser.UserGUID);
                            if (programuser != null)
                            {
                                if (programuser.Status == ProgramUserStatusEnum.Completed.ToString() || programuser.Status == ProgramUserStatusEnum.Screening.ToString())
                                {
                                    Resolve<IProgramUserRepository>().DeleteProgramUserByProgramGuidAndUserGuid(registerProgramUser.ProgramGuid, existedUser.UserGUID);
                                    // delete existed usere
                                    existedUser.IsDeleted = true;
                                    Resolve<IUserRepository>().UpdateUser(existedUser);
                                    UpdateRegisterUser(registerProgramUser); //(new Guid(registerUser.ProgramGuid), new Guid(registerUser.UserGuid), registerUser.RegisterEmail, registerUser.RegisterPassword, registerUser.Mobile, registerUser.SerialNumber, registerUser.TimeZone);
                                }
                                else
                                {
                                    resultType = JoinProgramResultEnum.RegisteredInProgram;
                                }
                            }
                            else
                            {
                                // delete existed user
                                existedUser.IsDeleted = true;
                                Resolve<IUserRepository>().UpdateUser(existedUser);
                            }
                        }
                    }
                    else
                    {
                        UpdateRegisterUser(registerProgramUser);//(new Guid(registerUser.ProgramGuid), new Guid(registerUser.UserGuid), registerUser.RegisterEmail, registerUser.RegisterPassword, registerUser.Mobile, registerUser.SerialNumber, registerUser.TimeZone);
                    }
                }
                else
                {
                    resultType = JoinProgramResultEnum.InvalidMobile;
                }
            }
            else
            {
                resultType = JoinProgramResultEnum.InvalidEmailAddress;
            }

            // if success, send register email
            if (resultType == JoinProgramResultEnum.Success)
            {
                if (!Resolve<IProgramService>().IsProgramNeedPay(registerProgramUser.ProgramGuid))
                {
                    Resolve<IEmailService>().SendRegisterEmail(registerProgramUser.UserGuid, registerProgramUser.ProgramGuid);
                }
            }

            return resultType;
        }

        private bool IsUniqueMobile(string programGuid, string userGuid, string mobile)
        {
            bool flag = true;
            Program programEntity = Resolve<IProgramRepository>().GetProgramByGuid(new Guid(programGuid));
            if (programEntity.IsContainTwoParts.HasValue && programEntity.IsContainTwoParts.Value == true)
            {
                if (!string.IsNullOrEmpty(mobile))
                {
                    List<User> userList = Resolve<IUserRepository>().GetUserListByMobileAndProgram(new Guid(programGuid), mobile).ToList();
                    foreach (User userEntity in userList)
                    {
                        if (userEntity.UserGUID != new Guid(userGuid))
                        {
                            flag = false;
                            break;
                        }
                    }
                }
            }

            return flag;
        }

        private void UpdateRegisterUser(RegisterProgramUserModel registerUserModel)
        {
            // update user
            User registerUser = Resolve<IUserRepository>().GetUserByGuid(registerUserModel.UserGuid);
            registerUser.Email = registerUserModel.UserName;
            registerUser.Password = registerUserModel.Password;
            if (!string.IsNullOrEmpty(registerUserModel.PhoneNumber))
            {
                registerUser.MobilePhone = registerUserModel.PhoneNumber;
            }
            if (!string.IsNullOrEmpty(registerUserModel.SerialNumber))
            {
                registerUser.SerialNumber = registerUserModel.SerialNumber;
            }
            registerUser.IsDeleted = false;
            Resolve<IUserRepository>().UpdateUser(registerUser);

            // update program user
            ProgramUser pu = Resolve<IProgramUserRepository>().GetProgramUserByProgramGuidAndUserGuid(registerUserModel.ProgramGuid, registerUserModel.UserGuid);
            //set user's currenttime according to TimeZone.
            DateTime setCurrentTimeByTimeZone = DateTime.UtcNow.AddMinutes(Convert.ToDouble(registerUserModel.UserTimeZone) * 60);
            if (pu != null)
            {
                if (!pu.ProgramReference.IsLoaded)
                {
                    pu.ProgramReference.Load();
                }
                if (!pu.Program.IsWithPay.HasValue || pu.Program.IsWithPay.HasValue && pu.Program.IsWithPay.Value == false)
                {
                    pu.Status = ProgramUserStatusEnum.Registered.ToString();
                }
                pu.StartDate = setCurrentTimeByTimeZone;//DateTime.UtcNow;
                pu.LastUpdated = setCurrentTimeByTimeZone;//DateTime.UtcNow;
                pu.MailTime = 3;
                pu.Day = 0;
                pu.LastFinishDate = setCurrentTimeByTimeZone;//DateTime.UtcNow;
                pu.LastUpdatedBy = registerUserModel.UserGuid;
                pu.TimeZone = registerUserModel.UserTimeZone;

                //add log for timezone changed
                string logMessage = " TimeZone : " + pu.TimeZone.Value + " ---> TimeZone : " + registerUserModel.UserTimeZone;
                InsertLogModel insertLogModel = new InsertLogModel
                {
                    ActivityLogType = (int)LogTypeEnum.ModifyProgram,
                    Browser = string.Empty,
                    From = string.Empty,
                    IP = string.Empty,
                    Message = logMessage,
                    PageGuid = Guid.Empty,
                    PageSequenceGuid = Guid.Empty,
                    SessionGuid = Guid.Empty,
                    ProgramGuid = registerUserModel.ProgramGuid,
                    UserGuid = registerUserModel.UserGuid
                };
                Resolve<IActivityLogService>().Insert(insertLogModel);

                Resolve<IProgramUserRepository>().Update(pu);
            }
        }

        private bool HasJoinedThisProgram(Guid userguid, Guid programGuid)
        {
            return Resolve<ProgramUserService>().HasUserInProgram(userguid, programGuid);
        }

        public void AddProgramUser(Guid programGUID, Guid userGUID)
        {
            User userEntity = Resolve<IUserRepository>().GetUserByGuid(userGUID);
            ProgramUser pu = new ProgramUser();
            pu.ProgramUserGUID = Guid.NewGuid();
            pu.LastUpdated = DateTime.UtcNow;
            pu.Program = Resolve<IProgramRepository>().GetProgramByGuid(programGUID);
            pu.User = userEntity;
            switch (userEntity.UserType.Value)
            {
                case (int)ChangeTech.Models.UserTypeEnum.Administrator:
                case (int)ChangeTech.Models.UserTypeEnum.ProgramAdministrator:
                    pu.Security = (int)(PermissionEnum.ProgramAdmin | PermissionEnum.ProgramCreate | PermissionEnum.ProgramDelete | PermissionEnum.ProgramEdit | PermissionEnum.ProgramView);
                    break;
                case (int)ChangeTech.Models.UserTypeEnum.ProgramEditor:
                    pu.Security = (int)(PermissionEnum.ProgramView | PermissionEnum.ProgramAdmin);
                    break;
                case (int)ChangeTech.Models.UserTypeEnum.Customer:
                    pu.Security = (int)(PermissionEnum.ProgramView);
                    break;
            }
            Resolve<IProgramUserRepository>().Insert(pu);
        }

        public void DeleteProgramUser(Guid programGUID, Guid userGUID)
        {
            Resolve<IProgramUserRepository>().DeleteProgramUserByProgramGuidAndUserGuid(programGUID, userGUID);
        }

        public void CutConnection()
        {
            List<ProgramUser> pUserList = GetShouldCutConnectUsers();
            foreach (ProgramUser puser in pUserList)
            {
                if (!puser.UserReference.IsLoaded)
                {
                    puser.UserReference.Load();
                }
                puser.User.Email = "xxx";
                puser.User.MobilePhone = "xxx";
                puser.User.FirstName = "xxx";
                puser.User.LastName = "xxx";

                Resolve<IUserRepository>().UpdateUser(puser.User);
            }
        }

        private List<ProgramUser> GetShouldCutConnectUsers()
        {
            string completeStatus = ProgramUserStatusEnum.Completed.ToString();
            List<ProgramUser> resultList = new List<ProgramUser>();
            IQueryable<ProgramUser> userlist = Resolve<IProgramUserRepository>().GetAllProgramUser();
            userlist = userlist.Where(p => p.Status == completeStatus);
            userlist = userlist.Where(p => p.Program.NeedCutConnect.HasValue && p.Program.NeedCutConnect.Value == true);
            List<ProgramUser> entityusers = userlist.ToList();
            foreach (ProgramUser userentity in entityusers)
            {
                if (ShouldCutConnection(userentity))
                {
                    resultList.Add(userentity);
                }
            }

            return resultList;
        }

        private bool ShouldCutConnection(ProgramUser pu)
        {
            bool flag = false;
            if (!pu.ProgramReference.IsLoaded)
            {
                pu.ProgramReference.Load();
            }
            if (!pu.UserReference.IsLoaded)
            {
                pu.UserReference.Load();
            }
            DateTime setCurrentTimeByTimeZone = Resolve<IProgramUserService>().GetCurrentTimeByTimeZone(pu.Program.ProgramGUID, pu.User.UserGUID, DateTime.UtcNow);
            if (pu.Program.CutConnectWeek.HasValue && pu.LastFinishDate.HasValue && (pu.LastFinishDate.Value.AddDays(pu.Program.CutConnectWeek.Value * 7) < setCurrentTimeByTimeZone))
            {
                flag = true;
            }

            return flag;
        }

        public int GetCountOfUserNotOnCompany(Guid programGuid)
        {
            return Resolve<IProgramUserRepository>().GetProgramUser(programGuid).Where(pu => !pu.User.Email.Contains("ChangeTechTemp") && pu.Company == null).Count();
        }

        public int GetCountOfUserByEmailOrMobile(Guid programGuid, string email, string mobile)
        {
            return Resolve<IProgramUserRepository>().GetProgramUser(programGuid).Where(pu => pu.User.Email.Contains(email) && pu.User.MobilePhone.Contains(mobile)).Count();
        }

        public List<CompanyUserInfoModel> GetUsersNotOnCompany(Guid programGuid, int pageNumber, int pageSize)
        {
            List<ProgramUser> programUserList = Resolve<IProgramUserRepository>().GetProgramUser(programGuid).Where(pu => !pu.User.Email.Contains("ChangeTechTemp") && pu.Company == null).OrderByDescending(pu => pu.StartDate).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            List<CompanyUserInfoModel> companyUserInfoList = new List<CompanyUserInfoModel>();
            foreach (ProgramUser programUser in programUserList)
            {
                CompanyUserInfoModel companyUserInfoModel = new CompanyUserInfoModel();
                if (!programUser.UserReference.IsLoaded)
                {
                    programUser.UserReference.Load();
                }
                companyUserInfoModel.ProgramUserGUID = programUser.ProgramUserGUID;
                companyUserInfoModel.Email = programUser.User.Email;
                companyUserInfoModel.MobilePhone = programUser.User.MobilePhone.Trim();
                companyUserInfoModel.RegisterDate = programUser.StartDate.HasValue ? programUser.StartDate.Value.ToString("yyyy-MM-dd") : "";
                companyUserInfoModel.Status = programUser.Status;
                companyUserInfoModel.CurrentDay = programUser.Day.HasValue ? programUser.Day.Value.ToString() : "";
                companyUserInfoModel.FirstName = programUser.User.FirstName;
                companyUserInfoModel.LastName = programUser.User.LastName;
                companyUserInfoList.Add(companyUserInfoModel);
            }

            return companyUserInfoList;
        }

        public List<CompanyUserInfoModel> GetUsersByEmailOrMobile(Guid programGuid, string email, string mobile, int pageNumber, int pageSize)
        {
            List<ProgramUser> programUserList = Resolve<IProgramUserRepository>().GetProgramUser(programGuid).Where(pu => pu.User.Email.Contains(email) && pu.User.MobilePhone.Contains(mobile)).OrderBy(pu => pu.User.Email).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            List<CompanyUserInfoModel> companyUserInfoList = new List<CompanyUserInfoModel>();
            foreach (ProgramUser programUser in programUserList)
            {
                CompanyUserInfoModel companyUserInfoModel = new CompanyUserInfoModel();
                if (!programUser.UserReference.IsLoaded)
                {
                    programUser.UserReference.Load();
                }
                companyUserInfoModel.ProgramUserGUID = programUser.ProgramUserGUID;
                companyUserInfoModel.Email = programUser.User.Email;
                companyUserInfoModel.MobilePhone = programUser.User.MobilePhone.Trim();
                companyUserInfoModel.RegisterDate = programUser.StartDate.HasValue ? programUser.StartDate.Value.ToString("yyyy-MM-dd") : "";
                companyUserInfoModel.Status = programUser.Status;
                companyUserInfoModel.CurrentDay = programUser.Day.HasValue ? programUser.Day.Value.ToString() : "";
                companyUserInfoModel.FirstName = programUser.User.FirstName;
                companyUserInfoModel.LastName = programUser.User.LastName;
                companyUserInfoList.Add(companyUserInfoModel);
            }

            return companyUserInfoList;
        }

        public List<CompanyUserInfoModel> GetCompanyUserList(Guid companyRightGuid, int pageNumber, int pageSize)
        {
            List<CompanyUserInfoModel> companyUserInfoList = new List<CompanyUserInfoModel>();
            CompanyRight companyRightEntity = Resolve<ICompanyRightRepository>().GetConpanyRightByGuid(companyRightGuid);
            if (!companyRightEntity.CompanyReference.IsLoaded)
            {
                companyRightEntity.CompanyReference.Load();
            }
            if (!companyRightEntity.ProgramReference.IsLoaded)
            {
                companyRightEntity.ProgramReference.Load();
            }
            string userStatusStr = ProgramUserStatusEnum.Screening.ToString();
            List<ProgramUser> programUserList = Resolve<IProgramUserRepository>().GetProgramUserByProgramAndCompany(companyRightEntity.Program.ProgramGUID, companyRightEntity.Company.CompanyGUID).Where(pu => !pu.Status.Equals(userStatusStr) && !pu.User.Email.Contains("ChangeTechTemp")).OrderBy(pu => pu.User.Email).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            foreach (ProgramUser programUser in programUserList)
            {
                if (!programUser.Status.Equals(ProgramUserStatusEnum.Screening.ToString()))
                {
                    CompanyUserInfoModel companyUserInfoModel = new CompanyUserInfoModel();
                    if (!programUser.UserReference.IsLoaded)
                    {
                        programUser.UserReference.Load();
                    }
                    companyUserInfoModel.ProgramUserGUID = programUser.ProgramUserGUID;
                    companyUserInfoModel.Email = programUser.User.Email;
                    companyUserInfoModel.MobilePhone = programUser.User.MobilePhone.Trim();
                    companyUserInfoModel.RegisterDate = programUser.StartDate.HasValue ? programUser.StartDate.Value.ToString("yyyy-MM-dd") : "";
                    companyUserInfoModel.Status = programUser.Status;
                    companyUserInfoModel.CurrentDay = programUser.Day.HasValue ? programUser.Day.Value.ToString() : "";
                    companyUserInfoModel.FirstName = programUser.User.FirstName;
                    companyUserInfoModel.LastName = programUser.User.LastName;
                    companyUserInfoList.Add(companyUserInfoModel);
                }
            }

            return companyUserInfoList;
        }

        public int GetCountOfCompanyUser(Guid companyRightGuid)
        {
            CompanyRight companyRightEntity = Resolve<ICompanyRightRepository>().GetConpanyRightByGuid(companyRightGuid);
            if (!companyRightEntity.CompanyReference.IsLoaded)
            {
                companyRightEntity.CompanyReference.Load();
            }
            if (!companyRightEntity.ProgramReference.IsLoaded)
            {
                companyRightEntity.ProgramReference.Load();
            }
            string userStatusStr = ProgramUserStatusEnum.Screening.ToString();
            List<ProgramUser> pus = Resolve<IProgramUserRepository>().GetProgramUserByProgramAndCompany(companyRightEntity.Program.ProgramGUID, companyRightEntity.Company.CompanyGUID).Where(pu => !pu.Status.Equals(userStatusStr)).ToList();
            return pus.Count();
        }

        public CompanyUserInfoModel GetCompanyUserInfo(Guid programUserGuid)
        {
            CompanyUserInfoModel companyUserInfoModel = new CompanyUserInfoModel();
            ProgramUser programUser = Resolve<IProgramUserRepository>().GetProgramUserByProgramUserGuid(programUserGuid);
            if (!programUser.UserReference.IsLoaded)
            {
                programUser.UserReference.Load();
            }
            companyUserInfoModel.ProgramUserGUID = programUser.ProgramUserGUID;
            companyUserInfoModel.Email = programUser.User.Email;
            companyUserInfoModel.MobilePhone = programUser.User.MobilePhone.Trim();
            companyUserInfoModel.RegisterDate = programUser.StartDate.HasValue ? programUser.StartDate.Value.ToString("yyyy-MM-dd") : "";
            companyUserInfoModel.Status = programUser.Status;
            companyUserInfoModel.CurrentDay = programUser.Day.HasValue ? programUser.Day.Value.ToString() : "";
            companyUserInfoModel.FirstName = programUser.User.FirstName;
            companyUserInfoModel.LastName = programUser.User.LastName;
            companyUserInfoModel.Gender = programUser.User.Gender;
            companyUserInfoModel.PinCode = programUser.User.PinCode;
            return companyUserInfoModel;
        }

        public void UpdateCompanyUserInfo(CompanyUserInfoModel companyUserInfo)
        {
            ProgramUser programUser = Resolve<IProgramUserRepository>().GetProgramUserByProgramUserGuid(companyUserInfo.ProgramUserGUID);
            bool hasChange = false;
            if (!programUser.UserReference.IsLoaded)
            {
                programUser.UserReference.Load();
            }
            if (!string.IsNullOrEmpty(programUser.Status) && !programUser.Status.Equals(companyUserInfo.Status))
            {
                programUser.Status = companyUserInfo.Status;
                hasChange = true;
            }
            if (hasChange)
            {
                Resolve<IProgramUserRepository>().Update(programUser);
            }

            hasChange = false;
            User userEntity = Resolve<IUserRepository>().GetUserByGuid(programUser.User.UserGUID);
            if (userEntity.FirstName != companyUserInfo.FirstName)
            {
                userEntity.FirstName = companyUserInfo.FirstName;
                hasChange = true;
            }
            if (userEntity.LastName != companyUserInfo.LastName)
            {
                userEntity.LastName = companyUserInfo.LastName;
                hasChange = true;
            }
            if (!userEntity.Email.Equals(companyUserInfo.Email))
            {
                userEntity.Email = companyUserInfo.Email;
                hasChange = true;
            }
            if (!userEntity.Gender.Trim().Equals(companyUserInfo.Gender.Trim()))
            {
                userEntity.Gender = companyUserInfo.Gender;
                hasChange = true;
            }
            if (!userEntity.MobilePhone.Equals(companyUserInfo.MobilePhone))
            {
                userEntity.MobilePhone = companyUserInfo.MobilePhone;
                hasChange = true;
            }
            if (userEntity.PinCode != (companyUserInfo.PinCode))
            {
                userEntity.PinCode = companyUserInfo.PinCode;
                hasChange = true;
            }
            if (hasChange)
            {
                Resolve<IUserRepository>().UpdateUser(userEntity);
            }
        }

        public void ActiveSMSKickStart(string programShortName, string userMobile)
        {
            //Program programEntity = Resolve<IProgramRepository>().GetProgramsByShortName(programShortName).FirstOrDefault();
            //if(programEntity != null)
            //{
            //    User userEntity = Resolve<IUserRepository>().GetUserListByMobileAndProgram(programEntity.ProgramGUID, userMobile).FirstOrDefault();
            //    if(userEntity != null)
            //    {
            ProgramUser programUserEntity = Resolve<IProgramUserRepository>().GetProgramUserByProgramShortNameAndUserMobile(programShortName, userMobile);
            if (programUserEntity == null)
            {
                string exceptionMessage = string.Format("ActiveSMSKickStart ProgramUser entity is null. Shortname : {0},Mobile : {1}", programShortName, userMobile);
                throw new NullReferenceException(exceptionMessage);
            }
            try
            {
                if (!programUserEntity.ProgramReference.IsLoaded) programUserEntity.ProgramReference.Load();
                if (!programUserEntity.UserReference.IsLoaded) programUserEntity.UserReference.Load();
                //The SwitchMessageTime only can be updated once for one user.
                if (programUserEntity != null && programUserEntity.SwitchMessageTime == null && (programUserEntity.Status != ProgramUserStatusEnum.Completed.ToString() && programUserEntity.Status != ProgramUserStatusEnum.Terminated.ToString()))
                {
                    DateTime switchMessageTime = GetCurrentTimeByTimeZone(programUserEntity.Program.ProgramGUID, programUserEntity.User.UserGUID, DateTime.UtcNow);
                    programUserEntity.SwitchMessageTime = switchMessageTime; //DateTime.UtcNow;
                    programUserEntity.LastPauseDate = null;//If kick start, user will go into second part, the previous last pause info is no use.
                    programUserEntity.LastPauseDay = null;
                    programUserEntity.Status = ProgramUserStatusEnum.Active.ToString();
                    if (programUserEntity.Program.SwitchDay.HasValue && (programUserEntity.Program.SwitchDay.Value - 1 > programUserEntity.Day.Value))
                    {
                        programUserEntity.Day = programUserEntity.Program.SwitchDay - 1;
                    }
                    Resolve<IProgramUserRepository>().Update(programUserEntity);
                    // add log
                    InsertLogModel logmodel = new InsertLogModel
                    {
                        ActivityLogType = (int)LogTypeEnum.UpdateProgramUser,
                        Browser = string.Empty,
                        IP = string.Empty,
                        Message = string.Format("ActiveSMSKickStart successful.ProgramUserGuid:{0}", programUserEntity.ProgramUserGUID),
                        ProgramGuid = programUserEntity.Program.ProgramGUID,
                        SessionGuid = programUserEntity.User.UserGUID,
                        UserGuid = Guid.Empty
                    };
                    Resolve<IActivityLogService>().Insert(logmodel);
                }
                //    }
                //}
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        public void CheckPuSwitchMessageTimeForTwoPartProgram()
        {
            List<ProgramUser> puEntities = Resolve<IProgramUserRepository>().GetProgramUsersForTwoPartProgram().ToList();
            //Testing used some test data. user email like 'kicktest%'
            //List<ProgramUser> puEntities = Resolve<IProgramUserRepository>().GetAllProgramUser().Where(pu => pu.User.Email.Contains("mamatest") && pu.Program.IsContainTwoParts == true && (!pu.SwitchMessageTime.HasValue)).ToList();
            if (puEntities.Count() > 0)
            {
                foreach (var puEntity in puEntities)
                {
                    if (!puEntity.ProgramReference.IsLoaded) puEntity.ProgramReference.Load();
                    //Program programEntity = Resolve<IProgramRepository>().GetProgramByGuid(puEntity.Program.ProgramGUID);
                    if (puEntity.Program != null && puEntity.Program.SwitchDay.HasValue)
                    {
                        if ((puEntity.Program.SwitchDay - 1) == puEntity.Day)
                        {
                            if (!puEntity.UserReference.IsLoaded) puEntity.UserReference.Load();
                            ProgramSchedule schedule = Resolve<IProgramScheduleRepository>().GetFirstDayScheduleOfProgram(puEntity.Program.ProgramGUID);
                            if (schedule != null)
                            {
                                DateTime activeDate = GetShouldActiveDate(puEntity, schedule);
                                if (puEntity.User.UserType.HasValue && puEntity.User.UserType.Value == (int)UserTypeEnum.User)
                                {
                                    if (puEntity.Day != null)
                                    {
                                        schedule = Resolve<IProgramScheduleRepository>().GetProgramScheduleByProgramAndDay(puEntity.Program.ProgramGUID, puEntity.Day.Value);
                                    }
                                    else
                                    {
                                        schedule = null;
                                    }
                                    if (schedule != null)
                                    {
                                        DateTime WouldDoDate;
                                        // add pause date to calculte WouldDoDate
                                        if (puEntity.PauseDay != null)
                                            WouldDoDate = activeDate.AddDays(Convert.ToDouble(schedule.WeekDay - 1 + (schedule.Week - 1) * 7) + Convert.ToDouble(puEntity.PauseDay));
                                        else
                                            WouldDoDate = activeDate.AddDays(Convert.ToDouble(schedule.WeekDay - 1 + (schedule.Week - 1) * 7));

                                        if (WouldDoDate.DayOfWeek != DayOfWeek.Sunday)
                                        {
                                            // need wait for next work day
                                            if (WouldDoDate.AddDays(-6).Date <= DateTime.UtcNow.Date && DateTime.UtcNow.Date < WouldDoDate || WouldDoDate < DateTime.UtcNow.Date)
                                            {
                                                DateTime switchDate = WouldDoDate.AddDays(-6);
                                                puEntity.SwitchMessageTime = Resolve<IProgramUserService>().GetCurrentTimeByTimeZone(puEntity.Program.ProgramGUID, puEntity.User.UserGUID, switchDate);
                                                Resolve<IProgramUserRepository>().Update(puEntity);
                                                InsertLogModel insertLogModel = new InsertLogModel
                                                {
                                                    ActivityLogType = (int)LogTypeEnum.UpdateProgramUser,
                                                    Browser = string.Empty,
                                                    From = string.Empty,
                                                    IP = string.Empty,
                                                    Message = string.Format("Update programuser switchmessagetime successfully! ProgramUserGuid : {0}", puEntity.ProgramUserGUID),
                                                    PageGuid = Guid.Empty,
                                                    PageSequenceGuid = Guid.Empty,
                                                    SessionGuid = Guid.Empty,
                                                    ProgramGuid = puEntity.Program.ProgramGUID,
                                                    UserGuid = puEntity.User.UserGUID
                                                };
                                                Resolve<IActivityLogService>().Insert(insertLogModel);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }


        public void UpdateProgramUser(EditProgramUserModel editProgramUserModel)
        {
            ProgramUser pu = Resolve<IProgramUserRepository>().GetProgramUserByProgramUserGuid(editProgramUserModel.ProgramUsreGUID);
            pu.Status = editProgramUserModel.Status;

            if (!pu.UserReference.IsLoaded)
            {
                pu.UserReference.Load();
            }
            if (pu.User != null)
            {
                pu.User.Email = editProgramUserModel.Email;
                pu.User.FirstName = editProgramUserModel.FirstName;
                pu.User.LastName = editProgramUserModel.LastName;
                pu.User.MobilePhone = editProgramUserModel.MobilePhone;
                pu.User.Gender = editProgramUserModel.Gender;
                pu.User.PinCode = editProgramUserModel.Pincode;
                pu.User.SerialNumber = editProgramUserModel.SerialNumber;
            }

            Resolve<IProgramUserRepository>().Update(pu);
        }

        public int GetInactiveUserNumber(int inactiveDays)
        {
            return Resolve<IStoreProcedure>().GetInactiveUserNumber(inactiveDays);
        }

        public List<UserModel> GetInactiveUsers(int inactiveDays)
        {
            List<UserModel> userInfoModelList = new List<UserModel>();
            List<ProgramUser> programUserList = Resolve<IStoreProcedure>().GetInactiveUsers(inactiveDays);
            foreach (ProgramUser programUser in programUserList)
            {
                //if (!programUser.UserReference.IsLoaded)
                //{
                //    programUser.UserReference.Load();
                //}
                userInfoModelList.Add(new UserModel
                {
                    FirstName = programUser.User.FirstName,
                    Gender = programUser.User.Gender.Trim().Equals(GenderEnum.Male.ToString().Trim()) ? GenderEnum.Male : GenderEnum.Female,//programUser.User.Gender,
                    LastName = programUser.User.LastName,
                    PhoneNumber = programUser.User.MobilePhone,
                    PinCode = programUser.User.PinCode,
                    Security = programUser.User.Security,
                    UserGuid = programUser.User.UserGUID,
                    UserName = programUser.User.Email,
                    UserType = (UserTypeEnum)Enum.Parse(typeof(UserTypeEnum), (programUser.User.UserType.HasValue ? programUser.User.UserType.Value : 1).ToString())
                });
            }
            return userInfoModelList;
        }

        public int GetLoginUserNumber(int loginMinutes)
        {
            return Resolve<IStoreProcedure>().GetLoginUserNumber(loginMinutes);
        }

        public int GetRegisteredUserNumber(int registeredDays)
        {
            return Resolve<IStoreProcedure>().GetRegisteredUserNumber(registeredDays);
        }

        // so far only get the uses who has missed class
        public MissedClassUsersModel GetMissedClassUsers(int missedDays, int pageNumber, int pageSize)
        {
            MissedClassUsersModel missedClassUsersModel = new MissedClassUsersModel();
            int totalCount = 0;
            List<UserModel> userInfoModelList = new List<UserModel>();
            List<ProgramUser> programUserList = Resolve<IStoreProcedure>().GetMissedClassUsers(missedDays, pageNumber, pageSize, out totalCount);
            foreach (ProgramUser programUser in programUserList)
            {
                userInfoModelList.Add(new UserModel
                {
                    FirstName = programUser.User.FirstName,
                    Gender = programUser.User.Gender.Trim().Equals(GenderEnum.Male.ToString().Trim()) ? GenderEnum.Male : GenderEnum.Female,//programUser.User.Gender,
                    LastName = programUser.User.LastName,
                    PhoneNumber = programUser.User.MobilePhone,
                    PinCode = programUser.User.PinCode,
                    Security = programUser.User.Security,
                    UserGuid = programUser.User.UserGUID,
                    UserName = programUser.User.Email,
                    UserType = (UserTypeEnum)Enum.Parse(typeof(UserTypeEnum), (programUser.User.UserType.HasValue ? programUser.User.UserType.Value : 1).ToString())
                });
            }
            missedClassUsersModel.MissedClassUsers = userInfoModelList;
            missedClassUsersModel.TotalCount = totalCount;
            return missedClassUsersModel;
        }

        public string GetProgramUserStatus(Guid programGuid, Guid userGuid)
        {
            string status = string.Empty;
            ProgramUser pu = Resolve<IProgramUserRepository>().GetProgramUserByProgramGuidAndUserGuid(programGuid, userGuid);
            if (pu != null)
            {
                status = pu.Status;
            }
            return status;
        }

        #region Update programUser
        ////Update User's TimeZone
        //public void UpdateProgramUserTimeZone(ProgramUserModel programUserModel)
        //{
        //    ProgramUser pu = Resolve<IProgramUserRepository>().GetProgramUserByProgramGuidAndUserGuid(programUserModel.ProgramGUID, programUserModel.UserGUID);
        //    if (pu!=null)
        //    {
        //        pu.TimeZone = programUserModel.UserTimeZone;
        //    }

        //    Resolve<IProgramUserRepository>().Update(pu);
        //}

        ////update User's IsSMSToEmail.
        //public void UpdateProgramUserSMSToEmail(ProgramUserModel puModel)
        //{
        //    ProgramUser pu = Resolve<IProgramUserRepository>().GetProgramUserByProgramGuidAndUserGuid(puModel.ProgramGUID, puModel.UserGUID);
        //    if (pu!=null)
        //    {
        //        pu.IsSMSToEmail = puModel.IsSMSToEmail;
        //    }

        //    Resolve<IProgramUserRepository>().Update(pu);
        //} 
        #endregion

        //Update ProgramUser's properties.
        public void UpdateProgramUserProperty(ProgramUserModel programUserModel)
        {
            ProgramUser pu = Resolve<IProgramUserRepository>().GetProgramUserByProgramGuidAndUserGuid(programUserModel.ProgramGUID, programUserModel.UserGUID);
            bool hasChange = false;
            if (pu != null)
            {
                if (programUserModel.UserTimeZone.HasValue)
                {
                    pu.TimeZone = programUserModel.UserTimeZone;
                    hasChange = true;
                }
                if (programUserModel.IsSMSToEmail.HasValue)
                {
                    pu.IsSMSToEmail = programUserModel.IsSMSToEmail;
                    hasChange = true;
                }
            }
            if (hasChange)
            {
                Resolve<IProgramUserRepository>().Update(pu);
            }
        }

        /// <summary>
        ///  Get CurrentTime According User's timezone or program's timezone.
        /// </summary>
        /// <param name="programGuid">programGuid</param>
        /// <param name="userGuid">userGuid</param>
        /// <param name="utcTime">UTC Time</param>
        /// <returns>changed time by timezone</returns>
        public DateTime GetCurrentTimeByTimeZone(Guid programGuid, Guid userGuid, DateTime utcTime)
        {
            try
            {
                DateTime currentTimeByTimeZone = utcTime;
                ProgramUser pu = Resolve<IProgramUserRepository>().GetProgramUserByProgramGuidAndUserGuid(programGuid, userGuid);
                Program program = Resolve<IProgramRepository>().GetProgramByGuid(programGuid);
                if (program != null)
                {
                    // judge whether to support timezone Function for current program 
                    if (program.IsSupportTimeZone.HasValue && program.IsSupportTimeZone.Value == true)
                    {
                        if (pu != null)
                        {
                            // if user's timezone is not null,and use it.
                            if (pu.TimeZone.HasValue && pu.TimeZone.Value != 0)
                            {
                                currentTimeByTimeZone = utcTime.AddMinutes(Convert.ToDouble(pu.TimeZone.Value * 60));
                            }
                            // if user's timezone is null, and use program's timezone.
                            else if (program.TimeZone.HasValue && program.TimeZone.Value != 0)
                            {
                                currentTimeByTimeZone = utcTime.AddMinutes(Convert.ToDouble(program.TimeZone.Value * 60));
                            }
                        }
                        else
                        {
                            currentTimeByTimeZone = utcTime.AddMinutes(Convert.ToDouble(program.TimeZone.Value * 60));
                        }
                    }
                }

                return currentTimeByTimeZone;
            }
            catch (Exception ex)
            {
                //LogUtility.LogUtilityIntance.LogException(ex, string.Format("{0}::{1}", System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message));
                InsertLogModel insertLogModel = new InsertLogModel
                {
                    ActivityLogType = (int)LogTypeEnum.GetCurrentTimeByTimeZone,
                    Browser = string.Empty,
                    From = string.Empty,
                    IP = string.Empty,
                    Message = string.Format("Method:{0} | ErrorMessage:{1}", "GetCurrentTimeByTimeZone", ex.Message),
                    PageGuid = Guid.Empty,
                    PageSequenceGuid = Guid.Empty,
                    SessionGuid = Guid.Empty,
                    ProgramGuid = programGuid,
                    UserGuid = userGuid
                };
                Resolve<IActivityLogService>().Insert(insertLogModel);
                throw ex;
            }
        }

        public DateTime GetCurrentTimeFromTimeZoneToUtc(Guid programGuid, Guid userGuid, DateTime timeZoneTime)
        {
            DateTime currentTimeByTimeZone = timeZoneTime;
            ProgramUser pu = Resolve<IProgramUserRepository>().GetProgramUserByProgramGuidAndUserGuid(programGuid, userGuid);
            Program program = Resolve<IProgramRepository>().GetProgramByGuid(programGuid);
            if (program != null)
            {
                // judge whether to support timezone Function for current program 
                if (program.IsSupportTimeZone.HasValue && program.IsSupportTimeZone.Value == true)
                {
                    if (pu != null)
                    {
                        // if user's timezone is not null,and use it.
                        if (pu.TimeZone.HasValue && pu.TimeZone.Value != 0)
                        {
                            currentTimeByTimeZone = timeZoneTime.AddMinutes(Convert.ToDouble(pu.TimeZone.Value * 60 * (-1)));
                        }
                        // if user's timezone is null, and use program's timezone.
                        else if (program.TimeZone.HasValue && program.TimeZone.Value != 0)
                        {
                            currentTimeByTimeZone = timeZoneTime.AddMinutes(Convert.ToDouble(program.TimeZone.Value * 60 * (-1)));
                        }
                    }
                    else
                    {
                        currentTimeByTimeZone = timeZoneTime.AddMinutes(Convert.ToDouble(program.TimeZone.Value * 60 * (-1)));
                    }
                }
            }

            return currentTimeByTimeZone;
        }

        public ProgramUser GetProgramUserByProgramGuidAndUserGuid(Guid programGuid, Guid userGuid)
        {
            return Resolve<IProgramUserRepository>().GetProgramUserByProgramGuidAndUserGuid(programGuid, userGuid);
        }

        public int GetShouldDoDayCountByWin8(Guid programGuid, Guid userGuid)
        {
            int shouldDoDayCount = 0;
            ProgramUser pu = GetProgramUserByProgramGuidAndUserGuid(programGuid, userGuid);
            if (pu != null)
            {
                int sessionCount = Resolve<ISessionService>().GetLastSessionDay(programGuid);
                DateTime utcNow = DateTime.UtcNow;
                DateTime startDate = pu.StartDate.HasValue ? pu.StartDate.Value : utcNow;
                shouldDoDayCount = (utcNow.Date - startDate.Date).Days - pu.Day.Value + 1;
                if (shouldDoDayCount + pu.Day.Value > sessionCount)
                {
                    shouldDoDayCount = sessionCount - pu.Day.Value;
                }
            }
            return shouldDoDayCount;
        }

        public DateTime? GetCurrentSessionDate(Guid programGuid, Guid userGuid)
        {
            DateTime? currentSessionDate = null;
            ProgramUser pu = GetProgramUserByProgramGuidAndUserGuid(programGuid, userGuid);
            int sessionCount = Resolve<ISessionService>().GetLastSessionDay(programGuid);
            if (pu != null)
            {
                if (pu.StartDate.HasValue && pu.Day.Value < sessionCount)
                {
                    currentSessionDate = pu.StartDate.Value.AddDays(pu.Day.Value + 1);
                    currentSessionDate = Resolve<IProgramUserService>().GetCurrentTimeByTimeZone(programGuid, userGuid, currentSessionDate.Value);
                }
            }

            return currentSessionDate;
        }

        private Guid GetProgramGuid(string programId)
        {
            //Get ProgramGuid
            Guid programGuid = Guid.Empty;
            if (!Guid.TryParse(programId, out programGuid))
            {
                //Program code format : 26Z28B
                if (programId.Length == 6)
                {
                    programGuid = Resolve<IProgramService>().GetProgramGUIDByProgramCode(programId);
                }
            }
            return programGuid;
        }

    }
}
