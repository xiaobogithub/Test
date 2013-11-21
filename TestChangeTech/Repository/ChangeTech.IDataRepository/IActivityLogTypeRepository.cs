using System.Collections.Generic;
using ChangeTech.Entities;

namespace ChangeTech.IDataRepository
{
    public interface IActivityLogTypeRepository
    {
        List<ActivityLogType> GetAll();
        ActivityLogType GetItem(int logTypeID);
        void Update(ActivityLogType entity);
    }
}
