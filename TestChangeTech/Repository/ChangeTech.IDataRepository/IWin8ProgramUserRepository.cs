using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Entities;

namespace ChangeTech.IDataRepository
{
    public interface IWin8ProgramUserRepository
    {
        void Insert(Win8ProgramUser win8ProgramUser);
        void Delete(string windowsLiveId);
        IQueryable<Win8ProgramUser> GetWindowsLiveByWindowsLiveId(string windowsLiveId);
        Win8ProgramUser GetWin8ProgramUserByProgramUserGuid(Guid programUserGuid);
    }
}
