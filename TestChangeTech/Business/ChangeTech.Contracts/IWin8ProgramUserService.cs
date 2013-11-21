using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Entities;
using ChangeTech.Models;

namespace ChangeTech.Contracts
{
    public interface IWin8ProgramUserService
    {
        List<Win8ProgramUserModel> GetWin8ProgramUserModelByWindowsLiveId(string windowsLiveId);
        Win8ProgramUserModel GetWin8ProgramUserModelByProgramUserGuid(Guid programUserGuid);
    }
}
