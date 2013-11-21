using System.Linq;
using ChangeTech.Entities;
using ChangeTech.IDataRepository;

namespace ChangeTech.SQLServerRepository
{
    public class ActivityLogSettingRepository : RepositoryBase, IActivityLogSettingRepository
    {
        public ActivityLogSetting GetItem()
        {
            return GetEntities<ActivityLogSetting>().FirstOrDefault();
        }
    }
}
