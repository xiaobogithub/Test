using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChangeTech.Contracts
{
    public interface IUserPageVariablePerDayService
    {
        double GetPageVariableValueOnDay(Guid userGUID, Guid programGUID, string pageVariableName, int day);
        //int GetPageVariableValueOnWeek(Guid userGUID, Guid programGUID, string pageVariableName, int week);
        string GetPageVariableValueOnWeek(Guid userGUID, Guid programGUID, string pageVariableName, int week, int currentDay, int lastWeek,bool isOperatedInCurrentSession);
    }
}
