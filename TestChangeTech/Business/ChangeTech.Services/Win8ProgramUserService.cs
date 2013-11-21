using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ethos.DependencyInjection;
using ChangeTech.Contracts;
using ChangeTech.Entities;
using ChangeTech.IDataRepository;
using ChangeTech.Models;

namespace ChangeTech.Services
{
    public class Win8ProgramUserService : ServiceBase, IWin8ProgramUserService
    {
        public List<Win8ProgramUserModel> GetWin8ProgramUserModelByWindowsLiveId(string windowsLiveId)
        {
            List<Win8ProgramUserModel> windowsLiveModels = new List<Win8ProgramUserModel>();
            List<Win8ProgramUser> win8ProgramUserEntities = Resolve<IWin8ProgramUserRepository>().GetWindowsLiveByWindowsLiveId(windowsLiveId).ToList();
            foreach (Win8ProgramUser win8ProgramUserEntity in win8ProgramUserEntities)
            {
                Win8ProgramUserModel win8ProgramUserModel = new Win8ProgramUserModel
                {
                    Win8ProgramUserGuid = win8ProgramUserEntity.Win8ProgramUserGUID,
                    ProgramUserGuid = win8ProgramUserEntity.ProgramUserGUID,
                    WindowsLiveId = win8ProgramUserEntity.WindowsLiveId
                };

                if (!windowsLiveModels.Contains(win8ProgramUserModel))
                {
                    windowsLiveModels.Add(win8ProgramUserModel);
                }
            }
            
            return windowsLiveModels;
        }

        public Win8ProgramUserModel GetWin8ProgramUserModelByProgramUserGuid(Guid programUserGuid)
        {
            Win8ProgramUser win8ProgramUserEntity = Resolve<IWin8ProgramUserRepository>().GetWin8ProgramUserByProgramUserGuid(programUserGuid);
            Win8ProgramUserModel win8ProgramUserModel = null;
            if (win8ProgramUserEntity!=null)
            {
                win8ProgramUserModel = new Win8ProgramUserModel
                {
                    ProgramUserGuid = win8ProgramUserEntity.ProgramUserGUID,
                    Win8ProgramUserGuid = win8ProgramUserEntity.Win8ProgramUserGUID,
                    WindowsLiveId = win8ProgramUserEntity.WindowsLiveId
                };
            }

            return win8ProgramUserModel;
        }
    }
}
