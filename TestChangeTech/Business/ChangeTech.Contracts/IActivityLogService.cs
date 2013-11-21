using System;
using System.Data;
using ChangeTech.Models;
using System.Collections.Generic;


namespace ChangeTech.Contracts
{
    public interface IActivityLogService
    {
        void Insert(Guid userGuid, Guid programGuid, int day, LogTypeEnum type, string Message);
        void Insert(Guid userGuid, Guid programGuid, Guid SessionGuid, LogTypeEnum type, string Message);
        void Insert(InsertLogModel model);
        ActivityLogModels GetAll();
        ActivityLogModel GetItem(Guid logGuid);
        ActivityLogModels GetItems(Guid UserGuid, Guid ProgramGuid, Guid SessionGuid, DateTime Begin, DateTime End, int ActivityLogType, int currentPageNumber, int pageSize, string userStatus);
        ActivityLogModels GetItems(string email, Guid programGuid, Guid sessionGuid, DateTime Begin, DateTime End, int activityLogType, int currentPageNumber, int pageSize, string userStatus);
        int GetItemsCount(string email, Guid programGuid, Guid sessionGuid, DateTime Begin, DateTime End, int activityLogType, string userStatus);
        ActivityLogTypeModels GetLogTypes();
        ActivityLogTypeModel GetLogType(int logTypeID);
        ActivityLogPriorityModels GetLogPriorities();
        ActivityLogPriorityModel GetLogPriority(int logPriorityID);
        void UpdateLogType(ActivityLogTypeModel model);
        ActivityLogModel GetLastLogByLogTypeAndMessage(int logType);
        int GetAllReminderEmailsCountOfToday();
        int GetAllReminderEmailsCountOfTodaySuccess();
        List<string> GetAllReminderFailedEmailsOfToday();
    }
}
