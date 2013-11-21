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
    public class WindowsLiveService : ServiceBase, IWindowsLiveService
    {
        public List<WindowsLiveModel> GetWindowsLiveByWindowsLiveId(string windowsLiveId)
        {
            List<WindowsLiveModel> windowsLiveModels = new List<WindowsLiveModel>();
            List<WindowsLive>windowsLiveEntities = Resolve<IWindowsLiveRepository>().GetWindowsLiveByWindowsLiveId(windowsLiveId).ToList();
            foreach (WindowsLive windowsLiveEntity in windowsLiveEntities)
            {
                WindowsLiveModel windowsLiveModel = new WindowsLiveModel
                {
                    WindowsLiveGuid = windowsLiveEntity.WindowsLiveGUID,
                    ProgramUserGuid = windowsLiveEntity.ProgramUserGUID.Value,
                    WindowsLiveId = windowsLiveEntity.WindowsLiveID
                };

                if (!windowsLiveModels.Contains(windowsLiveModel))
                {
                    windowsLiveModels.Add(windowsLiveModel);
                }
            }
            
            return windowsLiveModels;
        }
    }
}
