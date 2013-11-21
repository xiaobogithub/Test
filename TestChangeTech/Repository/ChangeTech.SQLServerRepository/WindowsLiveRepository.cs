using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Entities;
using ChangeTech.IDataRepository;

namespace ChangeTech.SQLServerRepository
{
    public class WindowsLiveRepository : RepositoryBase, IWindowsLiveRepository
    {
        public IQueryable<WindowsLive> GetWindowsLiveByWindowsLiveId(string windowsLiveId)
        {
            return GetEntities<WindowsLive>().Where(wl => (!wl.IsDeleted.HasValue || wl.IsDeleted.HasValue && wl.IsDeleted.Value == false) && wl.WindowsLiveID == windowsLiveId);
        }
    }
}
