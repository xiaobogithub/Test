using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Entities;
using System.Data;

namespace ChangeTech.IDataRepository
{
    public interface IActivityMonitorEmailLogRepository
    {
        void Insert(ActivityMonitorEmailLog log);
        List<ActivityMonitorEmailLog> GetAll();
        ActivityMonitorEmailLog GetItem(Guid logGuid);
        ActivityMonitorEmailLog GetLatestItemByLogTypeAndMessage(int ActivityLogType, string Message);
        List<ActivityMonitorEmailLog> GetItems(Guid UserGuid, Guid ProgramGuid, Guid SessionGuid, DateTime? Begin, DateTime? End, int ActivityLogType);
        DataTable GetActivityMonitorEmailLogReport(string emaillistStr, string fileds, string days, string programGuid);
        ActivityMonitorEmailLog GetLastActivityMonitorEmailLog(Guid userGuid, int logType);
        IQueryable<ActivityMonitorEmailLog> GetAllReminderEmailsLogOfToday();
    }
}
