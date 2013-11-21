using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Contracts;
using ChangeTech.Entities;
using ChangeTech.IDataRepository;
using Ethos.EmailModule.EmailService;
using Ethos.EmailModule.TemplateEngine;
using System.Net.Mail;
using Ethos.DependencyInjection;
using System.Configuration;
using System.Web.Configuration;
using Ethos.Utility;
using System.IO;
using ChangeTech.Models;
using System.Diagnostics;
using System.Threading;

namespace ChangeTech.Services
{
    public class EmailService : ServiceBase, IEmailService
    {
        public static readonly string MD5_KEY = "psycholo";
        private bool _isClientApplication;
        private MailDispatcher mailDispater;
        public const string CHANGETECHPAGE = "ChangeTech.html";
        public const string SECURITYSTRINGINREMINDEREMAILSP = "{securitystring}";
        private const int ResendEmailTimesWhenFailed = 2;

        public EmailService()
            : this(false)
        {
        }

        public EmailService(bool isClientApplication)
        {
            this._isClientApplication = isClientApplication;
            mailDispater = new MailDispatcher(ResendEmailTimesWhenFailed);
            mailDispater.AfterMailSend += new EventHandler<MailEventArgs>(EmailServicer_AfterMailSend);
            mailDispater.BeforeMailSend += new EventHandler<MailEventArgs>(EmailService_BeforeMailSend);
            mailDispater.MailFailed += new EventHandler<MailEventArgs>(EmailService_MailFailed);

            if (isClientApplication)
            {
                MailConfiguration.MailServer = Resolve<ISystemSettingRepository>().GetSettingValue("MailServer");// ConfigurationManager.AppSettings["MailServer"];
                MailConfiguration.UserName = Resolve<ISystemSettingRepository>().GetSettingValue("UserToAcessMailServer");// ConfigurationManager.AppSettings["UserToAcessMailServer"];
                MailConfiguration.Password = Resolve<ISystemSettingRepository>().GetSettingValue("PasswordToAccessMailServer");// ConfigurationManager.AppSettings["PasswordToAccessMailServer"];
                MailConfiguration.LogFileDirectory = Resolve<ISystemSettingRepository>().GetSettingValue("LogFileDirectory");// ConfigurationManager.AppSettings["LogFileDirectory"];
                MailConfiguration.LogFileName = Resolve<ISystemSettingRepository>().GetSettingValue("LogFileName");// ConfigurationManager.AppSettings["LogFileName"];
                MailConfiguration.Port = Convert.ToInt32(Resolve<ISystemSettingRepository>().GetSettingValue("Port")); // Convert.ToInt32(ConfigurationManager.AppSettings["Port"]);                
                //ConfigurationManager.AppSettings["EnableSSL"]
                if (Resolve<ISystemSettingRepository>().GetSettingValue("EnableSSL").Equals("true"))
                {
                    MailConfiguration.EnableSSL = true;
                }
                else
                {
                    MailConfiguration.EnableSSL = false;
                }
            }
            else
            {
                MailConfiguration.MailServer = Resolve<ISystemSettingRepository>().GetSettingValue("MailServer");// WebConfigurationManager.AppSettings["MailServer"];
                MailConfiguration.UserName = Resolve<ISystemSettingRepository>().GetSettingValue("UserToAcessMailServer"); //WebConfigurationManager.AppSettings["UserToAcessMailServer"];
                MailConfiguration.Password = Resolve<ISystemSettingRepository>().GetSettingValue("PasswordToAccessMailServer"); // WebConfigurationManager.AppSettings["PasswordToAccessMailServer"];
                MailConfiguration.LogFileDirectory = Resolve<ISystemSettingRepository>().GetSettingValue("LogFileDirectory");
                MailConfiguration.LogFileName = Resolve<ISystemSettingRepository>().GetSettingValue("LogFileName");
                MailConfiguration.Port = Convert.ToInt32(Resolve<ISystemSettingRepository>().GetSettingValue("Port")); //Convert.ToInt32(WebConfigurationManager.AppSettings["Port"]);
                //WebConfigurationManager.AppSettings["EnableSSL"]
                if (Resolve<ISystemSettingRepository>().GetSettingValue("EnableSSL").Equals("true"))
                {
                    MailConfiguration.EnableSSL = true;
                }
                else
                {
                    MailConfiguration.EnableSSL = false;
                }
            }
        }

        #region IEmailService Members

        public void SendRegisterEmail(Guid userGUID, Guid programGUID)
        {
            SendEmail(RegisterEmail(userGUID, programGUID, LogTypeEnum.NoLog));
        }

        public void SendForgetPasswordEmail(string username, Guid programGUID)
        {
            User user = Resolve<IUserRepository>().GetUserByEmail(username, programGUID);
            if (user != null)
            {
                EMail email = ForgetPasswordEmail(user, programGUID, LogTypeEnum.NoLog);
                if (email != null) SendEmail(email);
            }
        }

        #region sendReminderEmail
        /// <summary>
        /// Sync send email
        /// </summary>
        /// <param name="UserEntity">User</param>
        /// <param name="ProgramGuid">ProgramGuid</param>
        /// <returns></returns>
        public bool SendProgramRemainderEmail(User UserEntity, Guid ProgramGuid, LogTypeEnum logType = LogTypeEnum.SendReminderEmail)
        {
            bool flag = false;
            try
            {
                //don't send remind email to Super Admin.
                if (UserEntity.UserType == null || UserEntity.UserType != (int)UserTypeEnum.Administrator)
                {
                    EMail email = ProgramRemainderEmail(UserEntity, ProgramGuid, logType);
                    SendEmail(email);
                    flag = true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, string.Format("{0}::{1}", System.Reflection.MethodBase.GetCurrentMethod().Name, UserEntity.Email));
                Trace.TraceError(string.Format("Send Reminder Error::{0}::{1}::{2}", UserEntity.Email, ex.ToString(), DateTime.UtcNow));
            }
            return flag;
        }

        //Send Email according ReminderEmailInfoModel Object.
        public bool SendProgramRemainderEmail(ReminderEmailInfoModel reminderEmailInfoModel)
        {
            bool flag = false;
            string emailAddress = string.Empty;
            try
            {
                emailAddress = reminderEmailInfoModel.ToAddress;
                EMail email = CreateEmail(reminderEmailInfoModel);
                //SendEmail(email);
                SendEmailAsync(email);
                flag = true;
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, string.Format("{0}::{1}", System.Reflection.MethodBase.GetCurrentMethod().Name, emailAddress));
                Trace.TraceError(string.Format("Send Reminder Error::{0}::{1}::{2}", emailAddress, ex.ToString(), DateTime.UtcNow));

                //add log.
                InsertLogModel insertLogModel = new InsertLogModel
                {
                    ActivityLogType = (int)LogTypeEnum.SendReminderEmail,
                    Browser = string.Empty,
                    From = string.Empty,
                    IP = string.Empty,
                    Message = string.Format("Fail to send the Monitor email for workerrole."),
                    PageGuid = Guid.Empty,
                    PageSequenceGuid = Guid.Empty,
                    SessionGuid = Guid.Empty,
                    ProgramGuid = reminderEmailInfoModel.ProgramGuid,
                    UserGuid = reminderEmailInfoModel.UserGuid
                };
                Resolve<IActivityLogService>().Insert(insertLogModel);
            }
            return flag;
        }

        /// <summary>
        /// Sync send email list
        /// </summary>
        /// <param name="UserEntity">User</param>
        /// <param name="ProgramGuid"></param>
        /// <returns></returns>
        public bool SendProgramRemainderEmailList(List<ReminderEmailInfoModel> reminderEmailInfoList)
        {
            bool flag = false;
            string emailAddress = "";
            try
            {
                List<EMail> emails = new List<EMail>();
                foreach (ReminderEmailInfoModel reminderInfoItem in reminderEmailInfoList)
                {
                    emailAddress = reminderInfoItem.ToAddress;
                    emails.Add(CreateEmail(reminderInfoItem));
                }
                SendEmail(emails);
                flag = true;
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, string.Format("{0}::{1}", System.Reflection.MethodBase.GetCurrentMethod().Name, emailAddress));
                Trace.TraceError(string.Format("Send Reminder Error::{0}::{1}::{2}", emailAddress, ex.ToString(), DateTime.UtcNow));
                InsertLogModel insertLogModel = new InsertLogModel
                {
                    ActivityLogType = (int)LogTypeEnum.SendReminderEmail,
                    Browser = string.Empty,
                    From = string.Empty,
                    IP = string.Empty,
                    Message = string.Format("Fail to send the Monitor email for workerrole."),
                    PageGuid = Guid.Empty,
                    PageSequenceGuid = Guid.Empty,
                    SessionGuid = Guid.Empty,
                    ProgramGuid = Guid.Empty,
                    UserGuid = Guid.Empty
                };
                Resolve<IActivityLogService>().Insert(insertLogModel);
            }
            return flag;
        }

        /// <summary>
        /// Async send email
        /// </summary>
        /// <param name="UserEntity">User</param>
        /// <param name="ProgramGuid">Programguid</param>
        /// <returns></returns>
        public bool SendProgramRemainderEmailAsync(ReminderEmailInfoModel reminderEmailItem)
        {
            bool flag = false;
            try
            {
                EMail email = CreateEmail(reminderEmailItem);
                SendEmailAsync(email);
                flag = true;
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, string.Format("{0}::{1}", System.Reflection.MethodBase.GetCurrentMethod().Name, reminderEmailItem.ToAddress));
                Trace.TraceError(string.Format("Send Reminder Error::{0}::{1}::{2}", reminderEmailItem.ToAddress, ex.ToString(), DateTime.UtcNow));
            }
            return flag;
        }

        #region the async sending mail is wrong,don't use it
        /// <summary>
        /// Async send email list. the method may be wrong, don't use it
        /// </summary>
        /// <param name="userTokenList"></param>
        /// <returns></returns>
        public bool SendProgramRemainderEmailListAsync(List<ReminderEmailInfoModel> reminderEmailInfoList)
        {
            bool flag = false;
            string emailAddress = "";
            try
            {
                List<EMail> emails = new List<EMail>();
                foreach (ReminderEmailInfoModel reminderEmailItem in reminderEmailInfoList)
                {
                    emailAddress = reminderEmailItem.ToAddress;
                    emails.Add(CreateEmail(reminderEmailItem));
                }
                SendEmailAsync(emails);
                flag = true;
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, string.Format("{0}::{1}", System.Reflection.MethodBase.GetCurrentMethod().Name, emailAddress));
                Trace.TraceError(string.Format("Send Reminder Error::{0}::{1}::{2}", emailAddress, ex.ToString(), DateTime.UtcNow));
            }
            return flag;
        }
        #endregion

        /// <summary>
        /// Async sending email list.
        /// </summary>
        /// <param name="userTokenList"></param>
        /// <returns></returns>
        public bool AsyncSendProgramRemainderEmailList(List<ReminderEmailInfoModel> reminderEmailInfoList)
        {
            bool flag = false;
            string emailAddress = "";
            try
            {
                List<EMail> emails = new List<EMail>();
                foreach (ReminderEmailInfoModel reminderEmailItem in reminderEmailInfoList)
                {
                    emailAddress = reminderEmailItem.ToAddress;
                    emails.Add(CreateEmail(reminderEmailItem));
                }
                AsyncSendEmail(emails);
                flag = true;
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, string.Format("{0}::{1}", System.Reflection.MethodBase.GetCurrentMethod().Name, emailAddress));
                Trace.TraceError(string.Format("Asynchronously send Reminder Error::{0}::{1}::{2}", emailAddress, ex.ToString(), DateTime.UtcNow));
                InsertLogModel insertLogModel = new InsertLogModel
                {
                    ActivityLogType = (int)LogTypeEnum.SendReminderEmail,
                    Browser = string.Empty,
                    From = string.Empty,
                    IP = string.Empty,
                    Message = string.Format("Fail to Asynchronously send the reminder email for workerrole:{0},{1},{2}", emailAddress, ex.ToString(), DateTime.UtcNow),
                    PageGuid = Guid.Empty,
                    PageSequenceGuid = Guid.Empty,
                    SessionGuid = Guid.Empty,
                    ProgramGuid = Guid.Empty,
                    UserGuid = Guid.Empty
                };
                Resolve<IActivityLogService>().Insert(insertLogModel);

            }
            return flag;
        }

        #endregion

        public bool SendMonitorWorkerRoleEmail()
        {
            string emailAddress = ConfigurationManager.AppSettings["DefaultEmailForMonitorWorkerRole"];
            if (string.IsNullOrEmpty(emailAddress)) emailAddress = "test@changetech.no";

            bool flag = false;
            try
            {
                string subject = "Monitor WorkerRole Email";
                string body = string.Format("This is an email for monitor workerrole.It is sent at {0}", DateTime.UtcNow);
                string toAddress = emailAddress;
                EMail email = CreateEmail(subject, body, toAddress, Guid.Empty, new User(), LogTypeEnum.MonitorWorkerRoleEmail);
                SendEmail(email);
                flag = true;
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, string.Format("{0}::{1}", System.Reflection.MethodBase.GetCurrentMethod().Name, emailAddress));
                Trace.TraceError(string.Format("Monitor worker role email error::{0}::{1}::{2}", emailAddress, ex.ToString(), DateTime.UtcNow));
            }
            return flag;
        }

        public void SendInvitiationEmail(Guid userGuid, Guid programGuid, string invitee)
        {
            Program programEntity = Resolve<IProgramRepository>().GetProgramByGuid(programGuid);
            if (!programEntity.LanguageReference.IsLoaded)
            {
                programEntity.LanguageReference.Load();
            }

            User userEntity = Resolve<IUserRepository>().GetUserByGuid(userGuid);
            EMail invitationEmail = FillEMail(new Guid(Resolve<ISystemSettingRepository>().GetSettingValue("InviteFriendTemplateGUID")), userEntity, programGuid, LogTypeEnum.NoLog);
            invitationEmail.To.Clear();
            invitationEmail.To.Add(invitee);
            SendEmail(invitationEmail);
        }

        public FailEmail GetProgramRemainderEmail(User programUser, Guid ProgramGuid)
        {
            FailEmail fEmail = null;
            EMail email = new EMail();
            string errorMsg = string.Empty;
            try
            {
                email = ProgramRemainderEmail(programUser, ProgramGuid, LogTypeEnum.NoLog);
                if (email == null) email = new EMail();//If the email is null, in the area of finally{} , the "" and "" can cause System.NullReferenceException.
            }
            catch (Exception ex)
            {
                errorMsg = ex.ToString();
                Trace.TraceError("GetProgramRemainderEmail::{0}::{1}::{2}", programUser.Email, programUser.UserGUID, ex.ToString());
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            finally
            {
                fEmail = new FailEmail
                {
                    EmailContext = email.Body,
                    EmailGuid = Guid.NewGuid(),
                    EmailSubject = email.Subject,
                    EmailTo = programUser.Email,
                    ExceptionContext = errorMsg,
                };
            }
            return fEmail;
        }

        public void SendLoginInfoToNewAdmin(Guid userGUID)
        {
            User userEntity = Resolve<IUserRepository>().GetUserByGuid(userGUID);
            EMail mail = new EMail();
            string body = string.Format("Hi, {0} {1} <br/>You have been added as a user of ChangeTech system, here is your account information. <br/> Account Name: {2} <br/> Password: {3}. <br/> Best Regards, ChangeTech Team", userEntity.FirstName, userEntity.LastName, userEntity.Email, userEntity.Password);
            mail.IsBodyHtml = true;
            mail.Subject = "Personal security information";
            mail.From = new MailAddress(Resolve<ISystemSettingRepository>().GetSettingValue("EmailFromAddress"), Resolve<ISystemSettingRepository>().GetSettingValue("EmailFromName"));
            //mail.From = new MailAddress(ConfigurationManager.AppSettings["EmailFromAddress"], ConfigurationManager.AppSettings["EmailFromName"]);
            mail.Body = body;
            mail.To.Add(new MailAddress(userEntity.Email));
            SendEmail(mail);
        }

        public void SendLFSupportEmail(LFSupportEmailModel supportEmailModel)
        {
            EMail email = new EMail();
            email.IsBodyHtml = true;
            email.Subject = supportEmailModel.Subject;
            email.Body = supportEmailModel.Body;
            email.To.Add(new MailAddress(supportEmailModel.EmailTo));
            email.From = new MailAddress(supportEmailModel.EmailFromAddress, supportEmailModel.EmailFromName);
            SendEmail(email);
        }

        #endregion

        #region Private methods
        void EmailService_MailFailed(object sender, MailEventArgs e)
        {
            lock (this)
            {
                //Fail. Judge the fail count is over 3 or not. If has failed 3 times, not failed.
                EMail mail = sender as EMail;
                if (mail != null)
                {
                    UserTokenEmailSenderModel userTokenModel = mail.UserToken as UserTokenEmailSenderModel;
                    if (userTokenModel != null)
                    {
                        userTokenModel.FailCount += 1;
                        switch (userTokenModel.logType)
                        {
                            case LogTypeEnum.NoLog:
                                //Will not note any log
                                break;
                            case LogTypeEnum.SendReminderEmail:
                                if (userTokenModel.FailCount > ResendEmailTimesWhenFailed)
                                {
                                    //Fail log for reminder email
                                    //ChangeTech.Entities.FailEmail fMail = Resolve<IEmailService>().GetProgramRemainderEmail(userTokenModel.UserEntity, userTokenModel.ProgramGuid);
                                    //if (fMail != null)
                                    //{
                                    //    Resolve<IFailEmailRepository>().Insert(fMail);
                                    //}
                                    InsertLogModel insertLogModel = new InsertLogModel
                                    {
                                        ActivityLogType = (int)LogTypeEnum.SendReminderEmail,
                                        Browser = string.Empty,
                                        From = string.Empty,
                                        IP = string.Empty,
                                        Message = string.Format("Fail to send the reminder email"),
                                        PageGuid = Guid.Empty,
                                        PageSequenceGuid = Guid.Empty,
                                        SessionGuid = Guid.Empty,
                                        ProgramGuid = userTokenModel.ProgramGuid,
                                        UserGuid = userTokenModel.UserGuid,
                                    };
                                    Resolve<IActivityLogService>().Insert(insertLogModel);
                                    Trace.TraceInformation(string.Format("Fail Send Reminder::User::{0}::{1}::{2}", userTokenModel.Email, userTokenModel.UserGuid, DateTime.UtcNow));
                                    LogUtility.LogUtilityIntance.LogMessage(string.Format("Fail Send Reminder::User::{0}::{1}::{2}", userTokenModel.Email, userTokenModel.UserGuid, DateTime.UtcNow));
                                }
                                else
                                {
                                    //Retry log and re send this email. For reminder email
                                    mail.UserToken = userTokenModel;
                                    InsertLogModel insertLogModel = new InsertLogModel
                                    {
                                        ActivityLogType = (int)LogTypeEnum.SendReminderEmail,
                                        Browser = string.Empty,
                                        From = string.Empty,
                                        IP = string.Empty,
                                        Message = string.Format("Retry to send email at the {0} time", userTokenModel.FailCount),
                                        PageGuid = Guid.Empty,
                                        PageSequenceGuid = Guid.Empty,
                                        SessionGuid = Guid.Empty,
                                        ProgramGuid = userTokenModel.ProgramGuid,
                                        UserGuid = userTokenModel.UserGuid,
                                    };
                                    Resolve<IActivityLogService>().Insert(insertLogModel);
                                    Trace.TraceInformation(string.Format("Retry Send Reminder::User::{0}::{1}::{2}", userTokenModel.Email, userTokenModel.UserGuid, DateTime.UtcNow));
                                    LogUtility.LogUtilityIntance.LogMessage(string.Format("Retry Send Reminder::User::{0}::{1}::{2}", userTokenModel.Email, userTokenModel.UserGuid, DateTime.UtcNow));
                                    //SendEmail(mail);// re send. should noT be executed
                                }
                                break;
                            case LogTypeEnum.MonitorWorkerRoleEmail:
                                if (userTokenModel.FailCount > 2)
                                {
                                    //Fail log. for monitor email
                                    InsertLogModel insertLogModel = new InsertLogModel
                                    {
                                        ActivityLogType = (int)LogTypeEnum.SendReminderEmail,
                                        Browser = string.Empty,
                                        From = string.Empty,
                                        IP = string.Empty,
                                        Message = string.Format("Fail to send the Monitor email for workerrole"),
                                        PageGuid = Guid.Empty,
                                        PageSequenceGuid = Guid.Empty,
                                        SessionGuid = Guid.Empty,
                                        ProgramGuid = Guid.Empty,
                                        UserGuid = Guid.Empty
                                    };
                                    Resolve<IActivityLogService>().Insert(insertLogModel);
                                }
                                else
                                {
                                    //Retry log and re send this email.  for monitor email
                                    mail.UserToken = userTokenModel;
                                    InsertLogModel insertLogModel = new InsertLogModel
                                    {
                                        ActivityLogType = (int)LogTypeEnum.MonitorWorkerRoleEmail,
                                        Browser = string.Empty,
                                        From = string.Empty,
                                        IP = string.Empty,
                                        Message = string.Format("Retry to send Monitor email at the {0} time", userTokenModel.FailCount),
                                        PageGuid = Guid.Empty,
                                        PageSequenceGuid = Guid.Empty,
                                        SessionGuid = Guid.Empty,
                                        ProgramGuid = Guid.Empty,
                                        UserGuid = Guid.Empty
                                    };
                                    Resolve<IActivityLogService>().Insert(insertLogModel);
                                    //SendEmail(mail);// re send. should no be executed
                                }
                                break;
                        }
                    }
                }
            }
        }

        void EmailServicer_AfterMailSend(object sender, MailEventArgs e)
        {
            lock (this)
            {
                //success log. and SetLastSendEmailTimeOfProgramUser
                EMail mail = sender as EMail;
                if (mail != null)
                {
                    UserTokenEmailSenderModel userTokenModel = new UserTokenEmailSenderModel();
                    userTokenModel = mail.UserToken as UserTokenEmailSenderModel;
                    Guid programGuid = Guid.Empty;
                    Guid userGuie = Guid.Empty;
                    try
                    {
                        if (userTokenModel != null)
                        {
                            InsertLogModel insertLogModel;
                            switch (userTokenModel.logType)
                            {
                                case LogTypeEnum.NoLog:
                                    //no log need be inserted.
                                    break;
                                case LogTypeEnum.SendReminderEmail:
                                    insertLogModel = new InsertLogModel
                                    {
                                        ActivityLogType = (int)LogTypeEnum.SendReminderEmail,
                                        Browser = string.Empty,
                                        From = string.Empty,
                                        IP = string.Empty,
                                        Message = "Reminder email is sent successfuly",
                                        PageGuid = Guid.Empty,
                                        PageSequenceGuid = Guid.Empty,
                                        SessionGuid = Guid.Empty,
                                        ProgramGuid = userTokenModel.ProgramGuid,
                                        UserGuid = userTokenModel.UserGuid,
                                    };
                                    Resolve<IActivityLogService>().Insert(insertLogModel);
                                    Resolve<IProgramUserService>().SetLastSendEmailTimeOfProgramUser(insertLogModel.ProgramGuid, insertLogModel.UserGuid);
                                    break;
                                case LogTypeEnum.MonitorWorkerRoleEmail:
                                    insertLogModel = new InsertLogModel
                                    {
                                        ActivityLogType = (int)LogTypeEnum.MonitorWorkerRoleEmail,
                                        Browser = string.Empty,
                                        From = string.Empty,
                                        IP = string.Empty,
                                        Message = "Monitor email for WorkerRole is sent successfuly",
                                        PageGuid = Guid.Empty,
                                        PageSequenceGuid = Guid.Empty,
                                        SessionGuid = Guid.Empty,
                                        ProgramGuid = Guid.Empty,
                                        UserGuid = Guid.Empty
                                    };
                                    Resolve<IActivityLogService>().Insert(insertLogModel);
                                    break;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        //LogUtility.LogUtilityIntance.LogException(ex, string.Format("{0}::{1}", System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message));
                        InsertLogModel insertLogModel = new InsertLogModel
                        {
                            ActivityLogType = (int)LogTypeEnum.SendReminderEmail,
                            Browser = string.Empty,
                            From = string.Empty,
                            IP = string.Empty,
                            Message = string.Format("Method:{0} | ErrorMessage:{1} |from :{2}|to:{3}", "EmailServicer_AfterMailSend", ex.Message, mail.From, mail.To.ToString()),
                            PageGuid = Guid.Empty,
                            PageSequenceGuid = Guid.Empty,
                            SessionGuid = Guid.Empty,
                            ProgramGuid = userTokenModel.ProgramGuid,
                            UserGuid = userTokenModel.ProgramGuid
                        };
                        Resolve<IActivityLogService>().Insert(insertLogModel);
                    }
                }
            }
        }

        void EmailService_BeforeMailSend(object sender, MailEventArgs e)
        {
            lock (this)
            {
                //no handle needed before mail sent.
            }

        }


        private EMail RegisterEmail(Guid userGUID, Guid programGUID, LogTypeEnum logType)
        {
            User user = Resolve<IUserRepository>().GetUserByGuid(userGUID);

            return FillEMail(new Guid(Resolve<ISystemSettingRepository>().GetSettingValue("WelcomeTemplateGUID")), user, programGUID, logType);
        }

        private EMail ForgetPasswordEmail(User user, Guid programGUID, LogTypeEnum logType)
        {
            if (!user.ProgramUser.IsLoaded)
            {
                user.ProgramUser.Load();
            }

            if (programGUID != Guid.Empty)
            {
                ProgramUser programUser = Resolve<IProgramUserRepository>().GetProgramUserByProgramGuidAndUserGuid(programGUID, user.UserGUID);
                if (programUser != null)
                {
                    //ProgramUser programUser = user.ProgramUser.Where(pu => pu.Program.ProgramGUID == programGUID).FirstOrDefault();
                    //if (!programUser.LanguageReference.IsLoaded)
                    //{
                    //    programUser.LanguageReference.Load();
                    //}
                    return FillEMail(new Guid(Resolve<ISystemSettingRepository>().GetSettingValue("ResendPasswordTempalteGUID")), user, programGUID, logType);
                }
                else
                {
                    string logMsg = string.Format("Error: Cannot find user for program {0} of {1} language.", programGUID, user.UserGUID);
                    //Console.WriteLine(logMsg);
                    LogUtility.LogUtilityIntance.LogMessage(logMsg);
                    return null;
                }
            }
            else //if (user.UserType.Value != (int)UserType.User)
            {
                string subject = "Forget password";
                string body = Resolve<ISystemSettingRepository>().GetSettingValue("DefaultForgetPasswordTemplate");// WebConfigurationManager.AppSettings["DefaultForgetPasswordTemplate"];
                body = string.Format(body, user.FirstName + " " + user.LastName, user.Password);
                return CreateEmail(subject, body, user.Email, programGUID, user, logType);
            }
        }

        private EMail ProgramRemainderEmail(User programUser, Guid programGuid, LogTypeEnum logType)
        {
            return FillEMail(new Guid(Resolve<ISystemSettingRepository>().GetSettingValue("ReminderTemplateGUID")), programUser, programGuid, logType);
        }

        //Send one email sync
        private void SendEmail(EMail mail)
        {
            if (mail != null)
            {
                mailDispater.Add(mail);
                mailDispater.Process();
            }
        }

        //Send email list sync
        private void SendEmail(List<EMail> mails)
        {
            foreach (EMail mail in mails)
            {
                mailDispater.Add(mail);
            }
            mailDispater.Process();
        }

        //Send one email async
        private void SendEmailAsync(EMail mail)
        {
            if (mail != null)
            {
                mailDispater.Add(mail);
                mailDispater.ProcessAsync();
            }
        }

        //Send email list async,the method may be wrong, don't use it
        private void SendEmailAsync(List<EMail> mails)
        {
            foreach (EMail mail in mails)
            {
                mailDispater.Add(mail);
            }
            mailDispater.ProcessAsync();
        }

        //async sending email list 
        private void AsyncSendEmail(List<EMail> mails)
        {
            foreach (EMail mail in mails)
            {
                mailDispater.Add(mail);
            }
            mailDispater.AsyncProcess();
        }

        private EMail FillEMail(Guid emailTemplateTypeGuid, User userEntity, Guid programGuid, LogTypeEnum logType)
        {
            EMail mail = null;
            EmailTemplate emailTemplate = Resolve<IEmailTemplateRepository>().GetByProgramEmailTemplateType(
                programGuid, emailTemplateTypeGuid);
            if (emailTemplate != null)
            {
                if (!emailTemplate.EmailTemplateTypeReference.IsLoaded)
                {
                    emailTemplate.EmailTemplateTypeReference.Load();
                }

                string serverPath = GetServerPath(programGuid);
                string programCode = Resolve<IProgramRepository>().GetProgramByGuid(programGuid).Code;
                EmailTemplateTypeContent emailTemplateTypeContent = Resolve<IEmailTemplateTypeRepository>().GetEmailTemplateTypeContent(emailTemplateTypeGuid);
                string body = emailTemplateTypeContent.HtmlContent;
                if (body == null)
                    body = string.Empty;
                string name = Resolve<IProgramService>().GetProgramLogo(programGuid);
                string attachPath = "";
                if (name != string.Empty)
                {
                    string azureAccountName = Resolve<ISystemSettingService>().GetSettingValue("AzureStorageAccountName");
                    attachPath = ServiceUtility.GetBlobPath(azureAccountName) + @"/logocontainer/" + name;
                }
                //string attachPathLoginPic = System.Configuration.ConfigurationManager.AppSettings["WebServerAbsolutePath"] + @"\Images\loginbutton.png";
                //string attachPathLoginPic = Resolve<ISystemSettingService>().GetSettingValue("WebServerPath") + @"Images/loginbutton.png";
                string attachPathLoginPic = serverPath + @"Images/loginbutton.png";
                string securityStr = StringUtility.MD5Encrypt(string.Format("{0};{1}", userEntity.Email, userEntity.Password), MD5_KEY);
                if (!string.IsNullOrEmpty(attachPath))
                {
                    body = body.Replace(PlaceHolderConstants.LOGO_PICTURE, @"<br /><img src=""" + attachPath + @"""/>");
                }
                else
                {
                    body = body.Replace(PlaceHolderConstants.LOGO_PICTURE, "");
                }

                body = body.Replace(PlaceHolderConstants.BODY, emailTemplate.Body);
                body = body.Replace(PlaceHolderConstants.USER_NAME, string.Format("{0} {1}", userEntity.FirstName, userEntity.LastName));
                //not use '{UserName}'
                //body = body.Replace(PlaceHolderConstants.USER_NAME, "");
                if (!string.IsNullOrEmpty(emailTemplate.LinkText))
                {
                    body = body.Replace(PlaceHolderConstants.LINK_NAME, @"<a href=""" + serverPath
                       + CHANGETECHPAGE + @"?P=" + programCode + @"&Mode=Live&Security=" + securityStr
                       + @""">" + (string.IsNullOrEmpty(emailTemplate.LinkText) ? "Change Tech" : emailTemplate.LinkText) + @"</a>");
                    body = body.Replace(PlaceHolderConstants.LINK, "");
                }
                else
                {
                    body = body.Replace(PlaceHolderConstants.LINK, @"<a href=""" + serverPath
                        + CHANGETECHPAGE + @"?P=" + programCode + @"&Mode=Live&Security=" + securityStr
                        + @"""><img src=""" + attachPathLoginPic + @"""/></a>");
                }
                body = body.Replace(PlaceHolderConstants.PASSWORD, userEntity.Password);
                body = body.Replace(PlaceHolderConstants.INVITE_LINK, @"<a href=""" + serverPath + CHANGETECHPAGE +
                    @"?P=" + programCode + @"&Mode=Trial""/>" + (string.IsNullOrEmpty(emailTemplate.LinkText) ? "Change Tech" : emailTemplate.LinkText) + "</a>");
                body = body.Replace("\r\n", "<br/>");

                mail = CreateEmail(emailTemplate.Subject, body, userEntity.Email, programGuid, userEntity, logType);
            }
            else
            {
                Program program = Resolve<IProgramRepository>().GetProgramByGuid(programGuid);
                //Language language = Resolve<ILanguageRepository>().GetLanguage(languageGuid);

                string logMsg = string.Format("Error: Cannot find email template {0} for program {1}", emailTemplateTypeGuid, program.Name);
                Console.WriteLine(logMsg);
                LogUtility.LogUtilityIntance.LogMessage(logMsg);
            }
            return mail;
        }

        //click add new order later : send emial to customer.(included all program's link)
        public void SendEmailToCustomer(OrderModel orderModel)
        {
            //OrderEmailTemplateGUID
            string orderEmailTemplateGuid = Resolve<ISystemSettingService>().GetSettingValue("OrderEmailTemplateGUID");
            EmailTemplateTypeModel ettModel = Resolve<IEmailTemplateTypeService>().GetItem(new Guid(orderEmailTemplateGuid));//.GetEmailTemplateTypeByTypeID(Convert.ToInt32(EmailTemplateTypeEnum.OrderEmail));
            if (ettModel != null)
            {
                Guid languageGuid = orderModel.LanguageGUID;
                EmailTemplateTypeContentModel emailContentModel = Resolve<IEmailTemplateTypeService>().GetEmailTemplateTypeContentByTypeGuid(ettModel.EmailTemplateTypeGuid);
                OrderEmailTemplateModel orderEmailTemplateModel = Resolve<IOrderEmailTemplateService>().GetOrderEmailTemplateByEmailTemplateTypeGuidAndLanguageGuid(ettModel.EmailTemplateTypeGuid, languageGuid);
                string emailBody = emailContentModel.HtmlContent;
                if (emailBody.Contains(PlaceHolderConstants.BODY) && orderEmailTemplateModel != null)
                {
                    emailBody = emailBody.Replace(PlaceHolderConstants.BODY, orderEmailTemplateModel.Body).Replace("\r\n", "<br />");
                }

                StringBuilder programLinks = new StringBuilder(1000);
                foreach (var ocModel in orderModel.orderContents)
                {
                    programLinks.Append(GetProgramLink(ocModel));
                }

                if (emailBody.Contains(PlaceHolderConstants.PROGRAM_LINK) && !string.IsNullOrEmpty(programLinks.ToString()))
                {
                    emailBody = emailBody.Replace(PlaceHolderConstants.PROGRAM_LINK, programLinks.ToString());
                }
                //Send Email to customer.
                ReminderEmailInfoModel reminderEmailInfoModel = new ReminderEmailInfoModel()
                {
                    Body = emailBody,
                    FromAddress = Resolve<ISystemSettingService>().GetSettingValue("EmailFromAddress"),
                    FromName = Resolve<ISystemSettingService>().GetSettingValue("EmailFromName"),
                    Password = string.Empty,
                    ProgramGuid = Guid.Empty,
                    ReminderType = LogTypeEnum.SendEmailToCustomer,
                    Subject = orderEmailTemplateModel != null ? orderEmailTemplateModel.Subject : string.Empty,
                    ToAddress = orderModel.CustomerEmail,
                    UserGuid = Guid.Empty
                };
                try
                {
                    Resolve<IEmailService>().SendProgramRemainderEmailAsync(reminderEmailInfoModel);
                }
                catch (Exception ex)
                {
                    LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                    throw ex;
                }
            }
        }

        public void SendHPOrderEmails(List<HPOrderEmailModel> orderEmailModels)
        {
            foreach (var orderEmailModel in orderEmailModels)
            {
                if (orderEmailModel.HPOrderGUID != null && orderEmailModel.HPOrderGUID != Guid.Empty)
                {
                    HPOrderModel hpOrderModel = Resolve<IHPOrderService>().GetHPOrderModelByHPOrderGuid(orderEmailModel.HPOrderGUID);
                    string emailBody = Resolve<IHPOrderEmailService>().GetHPOrderEmailBodyContent(hpOrderModel);
                    orderEmailModel.HPEmailBody = emailBody;
                    Resolve<IHPOrderEmailService>().Update(orderEmailModel);
                    ReminderEmailInfoModel reminderEmailInfoModel = new ReminderEmailInfoModel()
                    {
                        Body = emailBody,
                        FromAddress = Resolve<ISystemSettingService>().GetSettingValue("EmailFromAddress"),
                        FromName = Resolve<ISystemSettingService>().GetSettingValue("EmailFromName"),
                        Password = string.Empty,
                        ProgramGuid = orderEmailModel.ProgramGUID,
                        ReminderType = LogTypeEnum.SendEmailToCustomer,
                        Subject = orderEmailModel.HPEmailSubject,
                        ToAddress = orderEmailModel.HPContactEmail,
                        UserGuid = Guid.Empty
                    };
                    try
                    {
                        bool isSend = Resolve<IEmailService>().SendProgramRemainderEmailAsync(reminderEmailInfoModel);
                        if (isSend)
                        {
                            orderEmailModel.IsSend = true;
                            Resolve<IHPOrderEmailService>().Update(orderEmailModel);
                        }
                    }
                    catch (Exception ex)
                    {
                        LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                        throw ex;
                    }
                }
            }

        }

        private string GetProgramLink(OrderContentModel ocModel)
        {
            string programLink = string.Empty;
            string serverPath = GetServerPath(ocModel.ProgramGUID);
            ProgramModel programModel = Resolve<IProgramService>().GetProgramByGUID(ocModel.ProgramGUID);
            string programName = programModel.ProgramName;
            string progranDescription = !string.IsNullOrEmpty(programModel.OrderProgramText) ? programModel.OrderProgramText : string.Empty; ;
            string programCode = string.Empty;
            string securityStr = StringUtility.MD5Encrypt(string.Format("{0};{1};{2};{3}", "", "", UserTaskTypeEnum.RegisteOfOrderSystem, ocModel.OrderContentGUID), MD5_KEY);
            if (programModel != null && !string.IsNullOrEmpty(programModel.Code))
            {
                programCode = programModel.Code;
                programLink = string.Format("&nbsp;&nbsp;&nbsp;<a href='{0}ChangeTech.html?P={1}&Mode=Trial&Security={2}' >{3}</a> : {4}<br />", serverPath, programCode, securityStr, programName, progranDescription);
            }
            return programLink;
        }

        public string GetServerPath(Guid programGuid)
        {
            string serverPath = string.Empty;
            Program programEntity = Resolve<IProgramRepository>().GetProgramByGuid(programGuid);
            if (programEntity != null && programEntity.IsSupportHttps.HasValue && programEntity.IsSupportHttps.Value == true)
            {
                serverPath = Resolve<ISystemSettingService>().GetSettingValue("WebServerPathHttps");
            }
            else
            {
                serverPath = Resolve<ISystemSettingService>().GetSettingValue("WebServerPath");
            }

            return serverPath;
        }

        private EMail CreateEmail(string subject, string body, string toAddress, Guid programGuid, User userEntity, LogTypeEnum logType)
        {
            EMail mail = new EMail();
            mail.IsBodyHtml = true;
            mail.Subject = subject;
            Program programEntity = Resolve<IProgramRepository>().GetProgramByGuid(programGuid);
            if (programGuid == Guid.Empty || string.IsNullOrEmpty(programEntity.SupportEmail) || string.IsNullOrEmpty(programEntity.SupportName))
            {
                mail.From = new MailAddress(Resolve<ISystemSettingRepository>().GetSettingValue("EmailFromAddress"), Resolve<ISystemSettingRepository>().GetSettingValue("EmailFromName"));
            }
            else
            {
                mail.From = new MailAddress(programEntity.SupportEmail, programEntity.SupportName);
            }
            //mail.From = new MailAddress(ConfigurationManager.AppSettings["EmailFromAddress"], ConfigurationManager.AppSettings["EmailFromName"]);

            mail.Body = body;
            mail.To.Add(new MailAddress(toAddress));
            UserTokenEmailSenderModel userTakenModel = new UserTokenEmailSenderModel
            {
                FailCount = 0,
                UserGuid = userEntity.UserGUID,
                ProgramGuid = programGuid,
                logType = logType,
                Email = toAddress,
            };
            mail.UserToken = userTakenModel;
            return mail;
        }

        private EMail CreateEmail(ReminderEmailInfoModel emailInfoItem)
        {
            EMail mail = new EMail();
            mail.IsBodyHtml = true;
            mail.Subject = emailInfoItem.Subject;
            mail.Body = emailInfoItem.Body;
            mail.From = new MailAddress(emailInfoItem.FromAddress, emailInfoItem.FromName);
            mail.To.Add(new MailAddress(emailInfoItem.ToAddress));
            mail.UserToken = new UserTokenEmailSenderModel
            {
                Email = emailInfoItem.ToAddress,
                FailCount = 0,
                logType = emailInfoItem.ReminderType,
                ProgramGuid = emailInfoItem.ProgramGuid,
                UserGuid = emailInfoItem.UserGuid,
            };

            return mail;
        }

        #endregion
    }




    public static class PlaceHolderConstants
    {
        public const string LOGO_PICTURE = "{LogoPicture}";
        public const string USER_NAME = "{UserName}";
        public const string BODY = "{EmailBody}";
        public const string LINK = "{LoginLink}";
        public const string LINK_NAME = "{LinkName}";
        public const string PASSWORD = "{UserPassword}";
        public const string INVITE_LINK = "{InviteLink}";
        public const string PROGRAM_LINK = "{ProgramLink}";
    }
}
