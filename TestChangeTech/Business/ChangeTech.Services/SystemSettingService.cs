using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ethos.DependencyInjection;
using ChangeTech.Contracts;
using ChangeTech.IDataRepository;

namespace ChangeTech.Services
{
    public class SystemSettingService : ServiceBase, ISystemSettingService
    {
        public string GetSettingValue(string settingKey)
        {
            return Resolve<ISystemSettingRepository>().GetSettingValue(settingKey);
        }
    }
}
