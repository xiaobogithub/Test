using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChangeTech.Contracts
{
    public interface ISystemSettingService
    {
        string GetSettingValue(string settingKey);
    }
}
