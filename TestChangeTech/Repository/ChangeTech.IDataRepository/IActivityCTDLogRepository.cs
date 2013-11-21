using System;
using System.Collections.Generic;
using ChangeTech.Entities;

namespace ChangeTech.IDataRepository
{
    public interface IActivityCTDLogRepository
    {
        void Insert(ActivityCTDLog log);
        List<ActivityCTDLog> GetAll();
        ActivityCTDLog GetItem(Guid logGuid);
    }
}
