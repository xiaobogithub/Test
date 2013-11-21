using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ethos.DependencyInjection;
using ChangeTech.Contracts;
using ChangeTech.IDataRepository;
using ChangeTech.Entities;
using ChangeTech.Models;

namespace ChangeTech.Services
{
    public class ActivityMonitorEmailLogService : ServiceBase, IActivityMonitorEmailLogService
    {
        public const string MONITOREMAILSUCCESSMESSAGE = "Monitor email for WorkerRole is sent successfuly";
        public const string REMINDEREMAILCONTENTOFSUCCESS = "Reminder email is sent successfuly";
        public const string REMINDEREMAILCONTENTOFFAIL = "Fail to send the reminder email";

        public Models.ActivityMonitorEmailLogModel GetLastLogByLogTypeAndMessage(int logType)
        {
            ActivityMonitorEmailLogModel logModel = new ActivityMonitorEmailLogModel();
            ActivityMonitorEmailLog log = Resolve<IActivityMonitorEmailLogRepository>().GetLatestItemByLogTypeAndMessage(logType, MONITOREMAILSUCCESSMESSAGE);
            if (log != null)
            {
                logModel.ActivityDateTime = log.ActivityDateTime == null ? DateTime.MinValue : (DateTime)log.ActivityDateTime;
                logModel.ActivityMonitorEmailLogGuid = log.ActivityMonitorEmailLogGuid;
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
                    Guid = log.ProgramGuid.Value
                };
                logModel.Session = new SessionModel
                {
                    ID = (Guid)log.SessionGuid
                };
                logModel.User = new UserModel
                {
                    UserGuid = log.UserGuid.Value
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

            count = Resolve<IActivityMonitorEmailLogRepository>().GetAllReminderEmailsLogOfToday().Count();

            return count;
        }

        public int GetAllReminderEmailsCountOfTodaySuccess()
        {
            int count = 0;
            count = Resolve<IActivityMonitorEmailLogRepository>().GetAllReminderEmailsLogOfToday().Where(a => a.Message.Contains(REMINDEREMAILCONTENTOFSUCCESS)).Count();
            return count;
        }

        public List<string> GetAllReminderFailedEmailsOfToday()
        {
            List<string> failEmails = new List<string>();

            failEmails = Resolve<IActivityMonitorEmailLogRepository>().GetAllReminderEmailsLogOfToday().Join(Resolve<IUserRepository>().GetAllUsers(), al => al.UserGuid, u => u.UserGUID, (al, u) => (new { al.Message, u.Email })).Where(al => al.Message.Contains(REMINDEREMAILCONTENTOFFAIL)).Select(u => u.Email).ToList();

            return failEmails;
        }
    }
}
