using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Entities;
using ChangeTech.Models;

namespace ChangeTech.Contracts
{
    public interface IWindowsLiveService
    {
        List<WindowsLiveModel> GetWindowsLiveByWindowsLiveId(string windowsLiveId);
    }
}
