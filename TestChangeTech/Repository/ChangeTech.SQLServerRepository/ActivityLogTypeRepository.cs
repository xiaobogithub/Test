using System.Collections.Generic;
using System.Linq;
using ChangeTech.Entities;
using ChangeTech.IDataRepository;

namespace ChangeTech.SQLServerRepository
{
    public class ActivityLogTypeRepository : RepositoryBase, IActivityLogTypeRepository
    {
        public List<ActivityLogType> GetAll()
        {
            return GetEntities<ActivityLogType>().ToList<ActivityLogType>();
        }

        public ActivityLogType GetItem(int logTypeID)
        {
            return GetEntities<ActivityLogType>(a => a.LogTypeID == logTypeID).FirstOrDefault();
        }

        public void Update(ActivityLogType entity)
        {
            UpdateEntity(entity);
        }
    }
}
