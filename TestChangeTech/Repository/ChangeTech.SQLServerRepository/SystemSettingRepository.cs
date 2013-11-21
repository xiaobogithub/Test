using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Entities;
using ChangeTech.IDataRepository;
using System.Data;

namespace ChangeTech.SQLServerRepository
{
    public class SystemSettingRepository: RepositoryBase, ISystemSettingRepository
    {
        public string GetSettingValue(string settingKey)
        {
            using (DynamicObjectContext context = new DynamicObjectContext(DefaultEntitiesConnectionString, DefaultEntitiesContainerName))
            {
                try
                {
                    return GetEntities<SystemSetting>(context).Where(s => s.Name.Equals(settingKey)).FirstOrDefault().Value;
                }
                catch (UpdateException ex)
                {
                    Console.WriteLine(ex.ToString());
                    throw ex;
                }
            }
        }
    }
}
