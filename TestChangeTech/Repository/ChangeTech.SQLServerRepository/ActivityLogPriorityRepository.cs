using System.Collections.Generic;
using System.Linq;
using ChangeTech.Entities;
using ChangeTech.IDataRepository;

namespace ChangeTech.SQLServerRepository
{
    public class ActivityLogPriorityRepository : RepositoryBase, IActivityLogPriorityRepository
    {
        public List<ActivityLogPriority> GetAll()
        {
            return GetEntities<ActivityLogPriority>().ToList<ActivityLogPriority>();
        }

        public ActivityLogPriority GetItem(int logPriorityID)
        {
            return GetEntities<ActivityLogPriority>(a => a.LogPriorityID == logPriorityID).FirstOrDefault();
        }
    }
}
