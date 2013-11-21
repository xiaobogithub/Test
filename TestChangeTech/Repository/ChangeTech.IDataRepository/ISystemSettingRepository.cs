using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChangeTech.IDataRepository
{
    public interface ISystemSettingRepository
    {
        string GetSettingValue(string settingKey);
    }
}
