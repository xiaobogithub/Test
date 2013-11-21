using System.Collections.Generic;
using ChangeTech.Entities;

namespace ChangeTech.IDataRepository
{
    public interface IActivityLogPriorityRepository
    {
        List<ActivityLogPriority> GetAll();
        ActivityLogPriority GetItem(int logPriorityID);
    }
}
