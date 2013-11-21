using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using ChangeTech.Entities;

namespace ChangeTech.IDataRepository
{
    public interface IActivityLogRepository
    {
        void InsertIntoTestLogTableForReminderEmailsTest(List<TestLog> testLogs);
        void Insert(ActivityLog log);
        List<ActivityLog> GetAll();
        ActivityLog GetItem(Guid logGuid);
        ActivityLog GetLatestItemByLogTypeAndMessage(int ActivityLogType, string Message);
        List<ActivityLog> GetItems(Guid UserGuid, Guid ProgramGuid, Guid SessionGuid, DateTime? Begin, DateTime? End, int ActivityLogType);
        DataTable GetActivityLogReport(string emaillistStr, string fileds, string days, string programGuid);
        ActivityLog GetLastActivity(Guid userGuid, int logType);
        IQueryable<ActivityLog> GetAllReminderEmailsLogOfTodayByMessages(string successMessage, string failMessage);
    }
}
