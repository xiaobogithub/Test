using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Entities;
using ChangeTech.IDataRepository;

namespace ChangeTech.SQLServerRepository
{
    public class Win8ProgramUserRepository : RepositoryBase, IWin8ProgramUserRepository
    {

        public void Insert(Win8ProgramUser win8ProgramUser)
        {
            InsertEntity(win8ProgramUser);
        }

        public void Delete(string windowsLiveId)
        {
            DeleteEntities<Win8ProgramUser>(w => w.WindowsLiveId == windowsLiveId, Guid.Empty);
        }

        public IQueryable<Win8ProgramUser> GetWindowsLiveByWindowsLiveId(string windowsLiveId)
        {
            return GetEntities<Win8ProgramUser>().Where(wl => (!wl.IsDeleted.HasValue || wl.IsDeleted.HasValue && wl.IsDeleted.Value == false) && wl.WindowsLiveId == windowsLiveId);
        }
        public Win8ProgramUser GetWin8ProgramUserByProgramUserGuid(Guid programUserGuid)
        {
            return GetEntities<Win8ProgramUser>().Where(wl => (!wl.IsDeleted.HasValue || wl.IsDeleted.HasValue && wl.IsDeleted.Value == false) && wl.ProgramUserGUID == programUserGuid).FirstOrDefault<Win8ProgramUser>();
        }
    }
}
