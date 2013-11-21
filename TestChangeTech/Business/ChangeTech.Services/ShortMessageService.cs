using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ethos.DependencyInjection;
using ChangeTech.Contracts;
using ChangeTech.Models;
using ChangeTech.Entities;
using ChangeTech.IDataRepository;
using System.Data;

namespace ChangeTech.Services
{
    public class ShortMessageService : ServiceBase, IShortMessageService
    {
        #region IShortMessageService Members

        public void AddShortMessage(ShortMessageModel model)
        {
            ShortMessageQueue message = new ShortMessageQueue();
            message.Mobile = model.MobileNo;
            message.Time = model.SendDateTime.Hour * 60 + model.SendDateTime.Minute;
            message.Date = model.SendDateTime; // DateTime.UtcNow.AddDays(model.SendDateTime);
            message.Content = model.Text;
            message.UserGUID = model.UserGUID;
            message.ProgramGUID = model.ProgramGUID;
            message.SessionGUID = model.SessionGUID;

            Resolve<IShortMessageRepository>().AddShortMessageRepository(message);
        }

        public void AddMessage(MessageModel model)
        {
            ShortMessage message = new ShortMessage
            {
                Program = Resolve<IProgramRepository>().GetProgramByGuid(model.ProgramGUID),
                ShortMessageGUID = Guid.NewGuid(),
                Text = model.Text,
                Type = model.Type
            };

            Resolve<IShortMessageRepository>().AddMessage(message);
        }

        public MessageModel GetMessageByProgramAndType(Guid programGuid, string type)
        {
            ShortMessage message = Resolve<IShortMessageRepository>().GetMessageByProgramAndType(programGuid, type);
            MessageModel model = new MessageModel();
            if (message != null)
            {
                model.MessageGUID = message.ShortMessageGUID;
                model.Text = message.Text;
                model.Type = message.Type;
            }

            return model;
        }

        public void UpdateMessage(MessageModel model)
        {
            ShortMessage message = Resolve<IShortMessageRepository>().GetMessageByProgramAndType(model.ProgramGUID, model.Type);
            message.Text = model.Text;

            Resolve<IShortMessageRepository>().Update(message);
        }

        public string GetMessageTextByProgramAndType(Guid programGuid, string type)
        {
            string text = string.Empty;
            ShortMessage message = Resolve<IShortMessageRepository>().GetMessageByProgramAndType(programGuid, type);
            if (message != null)
            {
                text = message.Text;
            }

            return text;
        }

        //public List<GetMessageModel> GetShortMessageNeedSend(DateTime time)
        //{
        //    List<GetMessageModel> messagelist = new List<GetMessageModel>();
        //    List<ShortMessageQueue> entitylist = Resolve<IShortMessageRepository>().GetShortMessageShouldSend();
        //    foreach (ShortMessageQueue smentity in entitylist)
        //    {
        //        if (ShouldSendSM(time, smentity))
        //        {
        //            GetMessageModel model = new GetMessageModel
        //            {
        //                sMobileNumber = smentity.Mobile,
        //                sMessage = smentity.Content
        //            };
        //            messagelist.Add(model);
        //        }
        //    }

        //    return messagelist;
        //}

        private bool ShouldSendSM(DateTime currentTime, ShortMessageQueue smentity)
        {
            bool flag = false;
            if (smentity.Date.HasValue && smentity.Time.HasValue && currentTime.Date == smentity.Date.Value.Date && ((currentTime.Hour * 60) + currentTime.Minute) >= smentity.Time.Value)
            {
                flag = true;
            }

            return flag;
        }

        public bool SendSM(GetMessageModel model)
        {
            return Ethos.SMSModule.SMSService.SendSM(model.sMobileNumber, model.sMessage, model.sOrigrinator, model.sForeignID, model.sUser, model.sPass);
        }

        private string FillSMSToEmailBody(ShortMessageQueue emailEntity, Guid emailTemplateTypeGuid, User userEntity)
        {
            EmailTemplateTypeContent emailTemplateTypeContent = Resolve<IEmailTemplateTypeRepository>().GetEmailTemplateTypeContent(emailTemplateTypeGuid);
            string emailBody = emailTemplateTypeContent.HtmlContent;
            emailBody = emailBody.Replace(PlaceHolderConstants.USER_NAME, string.Format("{0} {1}", userEntity.FirstName, userEntity.LastName));
            //emailBody = emailBody.Replace(PlaceHolderConstants.USER_NAME, "");
            emailBody = emailBody.Replace(PlaceHolderConstants.BODY, emailEntity.Content);
            emailBody = emailBody.Replace(PlaceHolderConstants.LINK, "");
            return emailBody;
        }

        public void SendShortMessageQueue()
        {
            DateTime time = DateTime.UtcNow;// DateTime.UtcNow.AddHours(2);//The previous is 2 hours in advance but don't know why. Now change it to be on time as Bent's demand.
            string user = Resolve<ISystemSettingService>().GetSettingValue("sUser");
            string pass = Resolve<ISystemSettingService>().GetSettingValue("sPass");
            string Originator = Resolve<ISystemSettingService>().GetSettingValue("sOriginator");

            List<ShortMessageQueue> smsList = Resolve<IShortMessageRepository>().GetShortMessageListShouldSend(time);
            List<ShortMessageQueue> emailList = Resolve<IShortMessageRepository>().GetEmailListShouldSend(time);
            
            //send sms
            if (smsList.Count > 0)
            {
                foreach (ShortMessageQueue smEnitity in smsList)
                {
                    GetMessageModel model = new GetMessageModel
                            {
                                sMobileNumber = smEnitity.Mobile,
                                sMessage = smEnitity.Content,
                                sOrigrinator = Originator,
                                sPass = pass,
                                sUser = user,
                                sForeignID = "0"
                            };
                    if (SendSM(model))
                    {
                        smEnitity.IsSent = true;
                        Resolve<IShortMessageRepository>().UpdateMessageQueue(smEnitity);

                        // add log
                        InsertLogModel log = new InsertLogModel
                        {
                            ActivityLogType = (int)LogTypeEnum.SendShortMessage,
                            Message = "send short message",
                            ProgramGuid = smEnitity.ProgramGUID.Value,
                            SessionGuid = smEnitity.SessionGUID.Value,
                            UserGuid = smEnitity.UserGUID.Value
                        };
                        Resolve<IActivityLogService>().Insert(log);
                    }
                }
            }

            if (emailList.Count > 0)
            {
                foreach (ShortMessageQueue emailEntity in emailList)
                {
                    User userEntity = Resolve<IUserRepository>().GetUserByGuid(emailEntity.UserGUID.Value);
                    Guid emailTemplateTypeGuid = new Guid(Resolve<ISystemSettingService>().GetSettingValue("ReminderTemplateGUID"));
                    string emailBody = FillSMSToEmailBody(emailEntity, emailTemplateTypeGuid, userEntity);
                    EmailTemplateModel emailTemplateModel = Resolve<IEmailTemplateService>().GetByProgramEmailTemplageType(emailEntity.ProgramGUID.Value, emailTemplateTypeGuid);
                    ReminderEmailInfoModel reminderEmailInfoModel = new ReminderEmailInfoModel()
                    {
                        Body = emailBody,
                        FromAddress = Resolve<ISystemSettingRepository>().GetSettingValue("EmailFromAddress"),
                        FromName = Resolve<ISystemSettingRepository>().GetSettingValue("EmailFromName"),
                        Password = userEntity.Password,
                        ProgramGuid = emailEntity.ProgramGUID.Value,
                        ReminderType = LogTypeEnum.SendReminderEmail,
                        Subject = emailTemplateModel.Subject,
                        ToAddress = userEntity.Email,
                        UserGuid = userEntity.UserGUID
                    };
                    //send SMS content for Email Format
                    //if send success , and IsSent set true. 
                    if (Resolve<IEmailService>().SendProgramRemainderEmail(reminderEmailInfoModel))
                    {
                        emailEntity.IsSent = true;
                        Resolve<IShortMessageRepository>().UpdateMessageQueue(emailEntity);

                        //add log
                        InsertLogModel insertLogModel = new InsertLogModel
                        {
                            ActivityLogType = (int)LogTypeEnum.SendReminderEmail,
                            Browser = string.Empty,
                            From = string.Empty,
                            IP = string.Empty,
                            Message = "Send Short Message To Email Format",
                            PageGuid = Guid.Empty,
                            PageSequenceGuid = Guid.Empty,
                            SessionGuid = emailEntity.SessionGUID.Value,
                            ProgramGuid = emailEntity.ProgramGUID.Value,
                            UserGuid = emailEntity.UserGUID.Value
                        };
                        Resolve<IActivityLogService>().Insert(insertLogModel);
                    }
                }
            }
        }
        public List<ShortMessageQueueModel> GetSMQList(Guid programGuid, int currentPage, int pageSize, string email)
        {
            IQueryable<ShortMessageQueue> smsList = Resolve<IShortMessageRepository>().GetShortMessageQueueByProgram(programGuid);
            if (!string.IsNullOrEmpty(email))
            {
                User user = Resolve<IUserRepository>().GetUserByEmail(email, programGuid);
                if (user != null)
                {
                    smsList = smsList.Where(s => s.UserGUID == user.UserGUID);
                }
            }
            List<ShortMessageQueue> smsEntityList = smsList.OrderBy(s => s.ID).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
            List<ShortMessageQueueModel> modelList = new List<ShortMessageQueueModel>();
            foreach (ShortMessageQueue queue in smsEntityList)
            {
                User userEntity = Resolve<IUserRepository>().GetUserByGuid(queue.UserGUID.Value);
                ShortMessageQueueModel model = new ShortMessageQueueModel
                {
                    Email = userEntity!=null?userEntity.Email:string.Empty,
                    IsSent = queue.IsSent.HasValue ? queue.IsSent.Value : false,
                    Message = queue.Content,
                    MobilePhone = queue.Mobile,
                    SendDate = GetSendDateTime(queue.Date.Value, queue.Time.Value)
                };

                modelList.Add(model);
            }

            return modelList;
        }

        private string GetSendDateTime(DateTime dateTime, int minites)
        {
            return Convert.ToDateTime((dateTime.ToString("yyyy-MM-dd") + " 00:00")).AddMinutes(minites).ToString("yyyy-MM-dd hh:mm tt");
        }

        public int GetSMQListCount(Guid programGuid, string email)
        {
            IQueryable<ShortMessageQueue> smsList = Resolve<IShortMessageRepository>().GetShortMessageQueueByProgram(programGuid);
            if (!string.IsNullOrEmpty(email))
            {
                User user = Resolve<IUserRepository>().GetUserByEmail(email, programGuid);
                if (user != null)
                {
                    smsList = smsList.Where(s => s.UserGUID == user.UserGUID);
                }
            }

            return smsList.Count();
        }

        public void AddSMSRecord(SMSRecordModel model)
        {
            SMSRecord record = new SMSRecord
            {
                ProgramShortName = model.ProgramShortName,
                UserMobile = model.UserMobile,
                SendTime = DateTime.UtcNow
            };
            Resolve<IShortMessageRepository>().AddSMSRecord(record);
            // add log
            InsertLogModel logModel = new InsertLogModel
            {
                ActivityLogType = (int)LogTypeEnum.SendShortMessage,
                Browser = string.Empty,
                IP = string.Empty,
                Message = string.Format("Add SMSRecord Successful. ProgramShortName: {0} , Mobile: {1}.", record.ProgramShortName, record.UserMobile),
                PageGuid = Guid.Empty,
                ProgramGuid = Guid.Empty,
                SessionGuid = Guid.Empty,
                UserGuid = Guid.Empty,
                PageSequenceGuid = Guid.Empty
            };
            Resolve<IActivityLogService>().Insert(logModel);
        }

        public List<DailySMSContentModel> GetDailySMSContentList(Guid programGuid)
        {
            List<DailySMSContentModel> dailysmsModelList = new List<DailySMSContentModel>();

            DataTable programDailySMSContentTable = Resolve<IStoreProcedure>().GetProgramDailySMSContentList(programGuid);
            if (programDailySMSContentTable != null && programDailySMSContentTable.Rows.Count > 0)
            {
                for (int i = 0; i < programDailySMSContentTable.Rows.Count; i++)
                {
                    DailySMSContentModel dailySMSModel = new DailySMSContentModel();
                    dailySMSModel.DailySMSContent = programDailySMSContentTable.Rows[i]["SMSContent"].ToString();
                    Guid dailySMSGuid = Guid.Empty;
                    Guid.TryParse(programDailySMSContentTable.Rows[i]["ProgramDailySMSGUID"].ToString(), out dailySMSGuid);
                    dailySMSModel.ProgramDailySMSGuid = dailySMSGuid;
                    dailySMSModel.SessionDescription = programDailySMSContentTable.Rows[i]["SessionDescription"].ToString();
                    dailySMSModel.SessionGuid = Guid.Parse(programDailySMSContentTable.Rows[i]["SessionGuid"].ToString());
                    dailySMSModel.SessionNum = programDailySMSContentTable.Rows[i]["DayNum"].ToString();

                    dailysmsModelList.Add(dailySMSModel);
                }
            }
            else
            {
                dailysmsModelList = null;
            }
            return dailysmsModelList;
        }

        //until now, this function has no use. Because the list should be existed by "session table left join programDailySMS",not all list item has valid programDailySMSGuid
        //The reason for left join is: If one session has never content,it does not need add an item in the programDailySMS table. So the number in programDailySMS <= session list
        public void UpdateProgramDailySMSContent(Guid programDailySMSGuid, string newContent)
        {
            ProgramDailySMS dailySMS = Resolve<IShortMessageRepository>().GetProgramDailySMS(programDailySMSGuid);
            if (dailySMS != null)
            {
                dailySMS.SMSContent = newContent;
                Resolve<IShortMessageRepository>().UpdateProgramDailySMS(dailySMS);
            }
        }

        //Now the update operation should use this function.
        public void UpdateProgramDailySMSContentBySessionGuid(Guid sessionGuid, string newContent)
        {
            if (sessionGuid != Guid.Empty)
            {
                ProgramDailySMS dailySMS = Resolve<IShortMessageRepository>().GetProgramDailySMSBySessionGuid(sessionGuid);
                if (dailySMS == null)
                {
                    Session sessionEntity = Resolve<ISessionRepository>().GetSessionBySessionGuid(sessionGuid);
                    if (sessionEntity != null)
                    {
                        dailySMS = new ProgramDailySMS
                        {
                            ProgramDailySMSGUID = Guid.NewGuid(),
                            Session = sessionEntity,
                            SMSContent = newContent,
                            IsDeleted = null,
                        };
                        Resolve<IShortMessageRepository>().AddProgramDailySMS(dailySMS);
                    }
                }
                else
                {
                    dailySMS.SMSContent = newContent;
                    Resolve<IShortMessageRepository>().UpdateProgramDailySMS(dailySMS);
                }
            }
        }

        #endregion
    }
}
