using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Models;

namespace ChangeTech.Contracts
{
    public interface IActivityMonitorEmailLogService
    {
        ActivityMonitorEmailLogModel GetLastLogByLogTypeAndMessage(int logType);
        int GetAllReminderEmailsCountOfToday();
        int GetAllReminderEmailsCountOfTodaySuccess();
        List<string> GetAllReminderFailedEmailsOfToday();
    }
}
