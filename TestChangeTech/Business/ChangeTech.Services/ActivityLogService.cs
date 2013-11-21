using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using ChangeTech.Contracts;
using ChangeTech.Entities;
using ChangeTech.IDataRepository;
using ChangeTech.Models;
using Ethos.DependencyInjection;

namespace ChangeTech.Services
{
    public class ActivityLogService : ServiceBase, IActivityLogService
    {
        public const string REMINDEREMAILCONTENTOFSUCCESS = "Reminder email is sent successfuly";
        public const string REMINDEREMAILCONTENTOFFAIL = "Fail to send the reminder email";

        public void Insert(Guid userGuid, Guid programGuid, int day, LogTypeEnum type, string Message)
        {
            // move IsLogTypeFitPrioritySetting to RepositoryBase
            //if (IsLogTypeFitPrioritySetting((int)type))
            //{
                Session session = Resolve<ISessionRepository>().GetSessionByProgramGuidAndDay(programGuid, day);
                Insert(userGuid, programGuid, session.SessionGUID, type, Message);
            //}
        }

        public void Insert(Guid userGuid, Guid programGuid, Guid SessionGuid, LogTypeEnum type, string Message)
        {
            // move IsLogTypeFitPrioritySetting to RepositoryBase
            //if (IsLogTypeFitPrioritySetting((int)type))
            //{
                ActivityLog model = new ActivityLog
                {
                    ActivityDateTime = DateTime.UtcNow,
                    ActivityLogGuid = Guid.NewGuid(),
                    ActivityLogType = (int)type,
                    Message = Message,
                    //PageGuid = log.PageGuid,
                    //PageSequenceGuid = log.PageSequence.PageSequenceID,
                    ProgramGuid = programGuid,
                    SessionGuid = SessionGuid,
                    UserGuid = userGuid,
                };
                Insert(model);
            //}
        }

        public void Insert(InsertLogModel model)
        {
            // move IsLogTypeFitPrioritySetting to RepositoryBase
            //if (IsLogTypeFitPrioritySetting(model.ActivityLogType))
            //{
                ActivityLog log = new ActivityLog
                {
                    ActivityDateTime = DateTime.UtcNow,
                    ActivityLogGuid = Guid.NewGuid(),
                    ActivityLogType = model.ActivityLogType,
                    Browser = model.Browser,
                    IP = model.IP,
                    Message = model.Message,
                    PageGuid = model.PageGuid,
                    PageSequenceGuid = model.PageSequenceGuid,
                    ProgramGuid = model.ProgramGuid,
                    SessionGuid = model.SessionGuid,
                    UserGuid = model.UserGuid,
                    From = model.From,
                };
                Insert(log);
            //}
        }

        private void Insert(ActivityLog log) {
            try
            {
                switch (log.ActivityLogType)
                {
                    //ActivityCTDLog
                    case (int)LogTypeEnum.ModifyDay:
                    case (int)LogTypeEnum.ModifyProgram:
                    case (int)LogTypeEnum.AddProgram:
                        ActivityCTDLog cTDLog = new ActivityCTDLog
                        {
                            ActivityDateTime = log.ActivityDateTime,
                            ActivityLogGuid = log.ActivityLogGuid,
                            ActivityLogType = log.ActivityLogType,
                            Browser = log.Browser,
                            IP = log.IP,
                            Message = log.Message,
                            PageGuid = log.PageGuid,
                            PageSequenceGuid = log.PageSequenceGuid,
                            ProgramGuid = log.ProgramGuid,
                            SessionGuid = log.SessionGuid,
                            UserGuid = log.UserGuid,
                            From = log.From,
                        };
                        Resolve<IActivityCTDLogRepository>().Insert(cTDLog);
                        break;
                    //ActivityMonitorEmailLog
                    case (int)LogTypeEnum.MonitorWorkerRoleEmail:
                    //case (int)LogTypeEnum.SendReminderEmail:
                    //case (int)LogTypeEnum.MonitorWorkerRoleStatus:
                        ActivityMonitorEmailLog monitorLog = new ActivityMonitorEmailLog
                        {
                            ActivityDateTime = log.ActivityDateTime,
                            ActivityMonitorEmailLogGuid = Guid.NewGuid(),
                            ActivityLogType = log.ActivityLogType,
                            Browser = log.Browser,
                            IP = log.IP,
                            Message = log.Message,
                            PageGuid = log.PageGuid,
                            PageSequenceGuid = log.PageSequenceGuid,
                            ProgramGuid = log.ProgramGuid,
                            SessionGuid = log.SessionGuid,
                            UserGuid = log.UserGuid,
                            From = log.From,
                        };
                        //insert log to ActivityMonitorEmailLog table.
                        Resolve<IActivityMonitorEmailLogRepository>().Insert(monitorLog);
                        break;
                    //Default add to ActivityLog table.
                    default:
                        Resolve<IActivityLogRepository>().Insert(log);
                        break;
                }
            }
            catch (Exception ex)
            {
                 ActivityMonitorEmailLog monitorLog = new ActivityMonitorEmailLog
                {
                    ActivityMonitorEmailLogGuid = Guid.NewGuid(),
                    ActivityDateTime = DateTime.UtcNow,
                    ActivityLogType = (int)LogTypeEnum.MonitorWorkerRoleStatus,
                    Browser = string.Empty,
                    From = string.Empty,
                    IP = string.Empty,
                    Message = string.Format("Insert Log Failed. Details:{0}", ex.ToString()),
                    PageGuid = Guid.Empty,
                    PageSequenceGuid = Guid.Empty,
                    SessionGuid = Guid.Empty,
                    ProgramGuid = Guid.Empty,
                    UserGuid = Guid.Empty,
                };
                Resolve<IActivityMonitorEmailLogRepository>().Insert(monitorLog);
                throw ex;
            }
        }

        private bool IsLogTypeFitPrioritySetting(int logTypeID)
        {
            ActivityLogSetting activityLogSetting = Resolve<IActivityLogSettingRepository>().GetItem();
            ActivityLogType activityLogType = Resolve<IActivityLogTypeRepository>().GetItem(logTypeID);
            if (!activityLogType.ActivityLogPriorityReference.IsLoaded)
            {
                activityLogType.ActivityLogPriorityReference.Load();
            }
            if (activityLogSetting.LogPriorityID > activityLogType.ActivityLogPriority.LogPriorityID)
                return false;
            else
                return true;
        }

        public ActivityLogModels GetAll()
        {
            return GetList(Resolve<IActivityLogRepository>().GetAll().OrderBy(a => a.ActivityDateTime).ToList<ActivityLog>());
        }

        public ActivityLogModel GetItem(Guid logGuid)
        {
            ActivityLog item = Resolve<IActivityLogRepository>().GetItem(logGuid);
            ActivityLogModel model = new ActivityLogModel
            {
                ActivityDateTime = item.ActivityDateTime.Value,
                ActivityLogGuid = item.ActivityLogGuid,
                Message = item.Message,
                PageGuid = item.PageGuid.Value,
                PageSequence = new PageSequenceModel
                {
                    PageSequenceID = item.PageSequenceGuid.Value,
                },
                Program = new ProgramModel
                {
                    Guid = item.ProgramGuid,
                },
                Session = new SessionModel
                {
                    ID = item.SessionGuid.Value,
                },
                User = new UserModel
                {
                    UserGuid = item.UserGuid,
                },
                Type = (LogTypeEnum)item.ActivityLogType,
            };
            return model;
        }

        public int GetItemsCount(string email, Guid programGuid, Guid sessionGuid, DateTime Begin, DateTime End, int activityLogType, string userStatus)
        {
            string userCondition = GetUserCondition(email, programGuid);

            string condition = @"where 1=1";

            if (!string.IsNullOrEmpty(email))
            {
                if (!string.IsNullOrEmpty(userCondition))
                {
                    condition += " and ActivityLog.UserGuid in " + "(" + userCondition + ")";
                }
            }
            if (programGuid != Guid.Empty)
            {
                condition += " and ActivityLog.ProgramGuid = " + "'" + programGuid + "'";
            }
            if (sessionGuid != Guid.Empty)
            {
                condition += " and ActivityLog.SessionGuid = " + "'" + sessionGuid + "'";
            }
            if (Begin != null && Begin != DateTime.MinValue)
            {
                condition += " and ActivityLog.ActivityDateTime >= " + "'" + Begin + "'";
            }
            if (End != null && End != DateTime.MinValue)
            {
                condition += " and ActivityLog.ActivityDateTime <= " + "'" + End + "'";
            }
            if (activityLogType != 0)
            {
                condition += " and ActivityLog.ActivityLogType = " + activityLogType;
            }
            if (!string.IsNullOrEmpty(userStatus))
            {
                condition += " and ProgramUser.Status= " + "'" + userStatus + "'";
            }

            return Resolve<IStoreProcedure>().SearchActivityLogNumber(condition);
        }

        private string GetUserCondition(string email, Guid programGuid)
        {
            string userCondition = string.Empty;
            if (!string.IsNullOrEmpty(email))
            {
                IQueryable<User> userlist = Resolve<IUserRepository>().GetUserListByEmail(email);
                if (programGuid != Guid.Empty)
                {
                    userlist = userlist.Where(u => u.ProgramGUID == programGuid);
                }
                List<User> users = userlist.ToList();
                if (users.Count > 0)
                {
                    foreach (User user in users)
                    {
                        if (!string.IsNullOrEmpty(userCondition))
                        {
                            userCondition += ",'" + user.UserGUID + "'";
                        }
                        else
                        {
                            userCondition += "'" + user.UserGUID + "'";
                        }
                    }
                }
                else
                {
                    userCondition += "'" + Guid.NewGuid() + "'";
                }
            }

            return userCondition;
        }

        public ActivityLogModels GetItems(string email, Guid programGuid, Guid sessionGuid, DateTime Begin, DateTime End, int activityLogType, int currentPageNumber, int pageSize, string userStatus)
        {
            string userCondition = GetUserCondition(email, programGuid);

            string condition = @"where 1=1";

            if (!string.IsNullOrEmpty(email))
            {
                if (!string.IsNullOrEmpty(userCondition))
                {
                    condition += " and ActivityLog.UserGuid in " + "(" + userCondition + ")";
                }
            }
            if (programGuid != Guid.Empty)
            {
                condition += " and ActivityLog.ProgramGuid = " + "'" + programGuid + "'";
            }
            if (sessionGuid != Guid.Empty)
            {
                condition += " and ActivityLog.SessionGuid = " + "'" + sessionGuid + "'";
            }
            if (Begin != null && Begin != DateTime.MinValue)
            {
                condition += " and ActivityLog.ActivityDateTime >= " + "'" + Begin + "'";
            }
            if (End != null && End != DateTime.MinValue)
            {
                condition += " and ActivityLog.ActivityDateTime <= " + "'" + End + "'";
            }
            if (activityLogType != 0)
            {
                condition += " and ActivityLog.ActivityLogType = " + activityLogType;
            }
            if (!string.IsNullOrEmpty(userStatus))
            {
                condition += " and ProgramUser.Status= " + "'" + userStatus + "'";
            }

            return GetList(Resolve<IStoreProcedure>().SearchActivityLog(condition, (currentPageNumber - 1) * pageSize + 1, currentPageNumber * pageSize));

            //Guid userGuid = Guid.Empty;
            //if(!string.IsNullOrEmpty(email))
            //{
            //    User userEntity = Resolve<IUserRepository>().GetUserByEmail(email, programGuid);
            //    if(userEntity != null)
            //    {
            //        userGuid = userEntity.UserGUID;
            //    }
            //    else
            //    {
            //        userGuid = Guid.NewGuid();
            //    }
            //}
            //return GetItems(userGuid, programGuid, sessionGuid, Begin, End, activityLogType, currentPageNumber, pageSize, userStatus);
        }

        public ActivityLogModels GetItems(Guid UserGuid, Guid ProgramGuid, Guid SessionGuid, DateTime Begin, DateTime End, int ActivityLogType, int currentPageNumber, int pageSize, string userStatus)
        {
            string condition = @"where 1=1";
            if (UserGuid != Guid.Empty)
            {
                condition += " and ActivityLog.UserGuid = " + "'" + UserGuid + "'";
            }
            if (ProgramGuid != Guid.Empty)
            {
                condition += " and ActivityLog.ProgramGuid = " + "'" + ProgramGuid + "'";
            }
            if (SessionGuid != Guid.Empty)
            {
                condition += " and ActivityLog.SessionGuid = " + "'" + SessionGuid + "'";
            }
            if (Begin != null && Begin != DateTime.MinValue)
            {
                condition += " and ActivityLog.ActivityDateTime >= " + "'" + Begin.ToString("yyyy-MM-dd") + "'";
            }
            if (End != null && End != DateTime.MinValue)
            {
                condition += " and ActivityLog.ActivityDateTime <= " + "'" + End.ToString("yyyy-MM-dd") + "'";
            }
            if (ActivityLogType != 0)
            {
                condition += " and ActivityLog.ActivityLogType = " + ActivityLogType;
            }
            if (!string.IsNullOrEmpty(userStatus))
            {
                condition += " and ProgramUser.Status= " + "'" + userStatus + "'";
            }
            return GetList(Resolve<IStoreProcedure>().SearchActivityLog(condition, (currentPageNumber - 1) * pageSize + 1, currentPageNumber * pageSize));
        }

        private ActivityLogModels GetList(List<ActivityLog> list)
        {
            ActivityLogModels models = new ActivityLogModels();
            foreach (ActivityLog item in list)
            {
                ActivityLogModel model = new ActivityLogModel
                {
                    ActivityDateTime = item.ActivityDateTime.Value,
                    ActivityLogGuid = item.ActivityLogGuid,
                    Message = item.Message,
                    Program = new ProgramModel(),
                    Session = new SessionModel(),
                    User = new UserModel(),
                    Type = (LogTypeEnum)item.ActivityLogType,
                    From = item.From,
                };

                if (item.ProgramGuid != Guid.Empty)
                {
                    Program programEntity = Resolve<IProgramRepository>().GetProgramByGuid(item.ProgramGuid);
                    if (programEntity != null)
                    {
                        model.Program.Guid = programEntity.ProgramGUID;
                        model.Program.ProgramName = programEntity.Name;
                    }
                }

                if (item.UserGuid != Guid.Empty)
                {
                    model.User.UserGuid = item.UserGuid;
                    model.User.UserName = Resolve<UserService>().GetUserByUserGuid(item.UserGuid).UserName;
                }

                if (item.SessionGuid.HasValue && item.SessionGuid != Guid.Empty)
                {
                    Session sessionEntity = Resolve<ISessionRepository>().GetSessionBySessionGuid(item.SessionGuid.Value);
                    model.Session.ID = item.SessionGuid.Value;
                    if (sessionEntity != null)
                    {
                        model.Session.Name = sessionEntity.Name;
                    }
                }

                if (item.PageGuid.HasValue && item.PageGuid != Guid.Empty)
                {
                    model.PageGuid = item.PageGuid.Value;
                }

                if (item.PageSequenceGuid.HasValue && item.PageSequenceGuid != Guid.Empty)
                {
                    model.PageSequence = new PageSequenceModel
                   {
                       PageSequenceID = item.PageSequenceGuid.Value,
                   };
                }
                models.Add(model);
            }
            return models;
        }

        public ActivityLogTypeModels GetLogTypes() {
            ActivityLogTypeModels models = new ActivityLogTypeModels();
            List<ActivityLogType> list = Resolve<IActivityLogTypeRepository>().GetAll().OrderBy(a => a.LogTypeName).ToList<ActivityLogType>();
            foreach (ActivityLogType item in list)
            {
                if (!item.ActivityLogPriorityReference.IsLoaded)
                {
                    item.ActivityLogPriorityReference.Load();
                }
           
                ActivityLogTypeModel model = new ActivityLogTypeModel
                {
                    ID = item.LogTypeID,
                    Name = item.LogTypeName,
                    LogPriority = new ActivityLogPriorityModel{
                        ID=item.ActivityLogPriority.LogPriorityID,
                        Name=item.ActivityLogPriority.LogPriorityName
                    }
                };
                models.Add(model);
            }
            return models;
        }

        public ActivityLogTypeModel GetLogType(int logTypeID) {
            ActivityLogType logType = Resolve<IActivityLogTypeRepository>().GetItem(logTypeID);
            if (!logType.ActivityLogPriorityReference.IsLoaded)
            {
                logType.ActivityLogPriorityReference.Load();
            }
            ActivityLogTypeModel model = new ActivityLogTypeModel
            {
                ID = logType.LogTypeID,
                Name = logType.LogTypeName,
                LogPriority = new ActivityLogPriorityModel
                {
                    ID = logType.ActivityLogPriority.LogPriorityID,
                    Name = logType.ActivityLogPriority.LogPriorityName
                }
            };
            return model;
        }

        public ActivityLogPriorityModels GetLogPriorities()
        {
            ActivityLogPriorityModels models = new ActivityLogPriorityModels();
            List<ActivityLogPriority> list = Resolve<IActivityLogPriorityRepository>().GetAll().OrderBy(a => a.LogPriorityID).ToList<ActivityLogPriority>();
            foreach (ActivityLogPriority item in list)
            {
                ActivityLogPriorityModel model = new ActivityLogPriorityModel
                {
                    ID = item.LogPriorityID,
                    Name = item.LogPriorityName
                };
                models.Add(model);
            }
            return models;
        }

        public ActivityLogPriorityModel GetLogPriority(int logPriorityID)
        {
            ActivityLogPriority logPriority = Resolve<IActivityLogPriorityRepository>().GetItem(logPriorityID);
            ActivityLogPriorityModel model = new ActivityLogPriorityModel
            {
                ID = logPriority.LogPriorityID,
                Name = logPriority.LogPriorityName
            };
            return model;
        }

        public void UpdateLogType(ActivityLogTypeModel model)
        {
            ActivityLogType logType = Resolve<IActivityLogTypeRepository>().GetItem(model.ID);
            logType.ActivityLogPriority = Resolve<IActivityLogPriorityRepository>().GetItem(model.LogPriority.ID);

            Resolve<IActivityLogTypeRepository>().Update(logType);
        }

        public ActivityLogModel GetLastLogByLogTypeAndMessage(int logType)
        {
            ActivityLogModel logModel = new ActivityLogModel();
            ActivityLog log = Resolve<IActivityLogRepository>().GetLatestItemByLogTypeAndMessage(logType, REMINDEREMAILCONTENTOFSUCCESS);
            if (log != null)
            {
                logModel.ActivityDateTime = log.ActivityDateTime == null ? DateTime.MinValue : (DateTime)log.ActivityDateTime;
                logModel.ActivityLogGuid = log.ActivityLogGuid;
                if (!log.ActivityLogType1Reference.IsLoaded) log.ActivityLogType1Reference.Load();
                logModel.Type = (LogTypeEnum)log.ActivityLogType;
                logModel.From = log.From;
                logModel.Message = log.Message;
                logModel.PageGuid = (Guid)log.PageGuid;
                logModel.PageSequence = new PageSequenceModel
                {
                    PageSequenceID = (Guid)log.PageSequenceGuid
                };
                logModel.Program = new ProgramModel
                {
                    Guid = log.ProgramGuid
                };
                logModel.Session = new SessionModel
                {
                    ID = (Guid)log.SessionGuid
                };
                logModel.User = new UserModel
                {
                    UserGuid = log.UserGuid
                };
            }
            else
            {
                logModel = null;
            }
            return logModel;
        }


        public int GetAllReminderEmailsCountOfToday()
        {
            int count = 0;

            count = Resolve<IActivityLogRepository>().GetAllReminderEmailsLogOfTodayByMessages(REMINDEREMAILCONTENTOFSUCCESS,REMINDEREMAILCONTENTOFFAIL).Count();

            return count;
        }

        public int GetAllReminderEmailsCountOfTodaySuccess()
        {
            int count = 0;
            count = Resolve<IActivityLogRepository>().GetAllReminderEmailsLogOfTodayByMessages(REMINDEREMAILCONTENTOFSUCCESS, REMINDEREMAILCONTENTOFFAIL).Where(a => a.Message.Contains(REMINDEREMAILCONTENTOFSUCCESS)).Count();
            return count;
        }

        public List<string> GetAllReminderFailedEmailsOfToday()
        {
            List<string> failEmails = new List<string>();

            failEmails = Resolve<IActivityLogRepository>().GetAllReminderEmailsLogOfTodayByMessages(REMINDEREMAILCONTENTOFSUCCESS, REMINDEREMAILCONTENTOFFAIL).Join(Resolve<IUserRepository>().GetAllUsers(), al => al.UserGuid, u => u.UserGUID, (al, u) => (new { al.Message, u.Email })).Where(al => al.Message.Contains(REMINDEREMAILCONTENTOFFAIL)).Select(u => u.Email).ToList();

            return failEmails;
        }

    }
}
