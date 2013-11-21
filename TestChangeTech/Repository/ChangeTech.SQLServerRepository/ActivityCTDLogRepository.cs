using System.Collections.Generic;
using System.Linq;
using ChangeTech.Entities;
using ChangeTech.IDataRepository;
using System;
using System.Data;

namespace ChangeTech.SQLServerRepository
{
    public class ActivityCTDLogRepository : RepositoryBase, IActivityCTDLogRepository
    {
        public void Insert(ActivityCTDLog log)
        {
            using (DynamicObjectContext context = new DynamicObjectContext(DefaultEntitiesConnectionString, DefaultEntitiesContainerName))
            {
                try
                {
                    // set log priority.
                    if (IsLogTypeFitPrioritySetting(log.ActivityLogType, context))
                    {
                        InsertEntity(log, context);
                    }
                }
                catch (UpdateException ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            //InsertEntity(log);
        }

        public List<ActivityCTDLog> GetAll()
        {
            return GetEntities<ActivityCTDLog>().ToList<ActivityCTDLog>();
        }

        public ActivityCTDLog GetItem(Guid logGuid)
        {
            return GetEntityById<ActivityCTDLog>(logGuid);
        }
    }
}
