using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ethos.DependencyInjection;
using ChangeTech.IDataRepository;
using ChangeTech.Entities;
using ChangeTech.Models;
using ChangeTech.Contracts;


namespace ChangeTech.Services
{
    public class ProgramStatusService : ServiceBase,IProgramStatusService
    {
        #region IProgramStatusService Members

        public List<ChangeTech.Models.ProgramStatusModel> GetAllProgramStatus()
        {
            List<ProgramStatusModel> statusModelList = new List<ProgramStatusModel>();

            IQueryable<ProgramStatus> statusList = Resolve<IProgramStatusRepository>().GetAllProgramStatus();
            foreach (ProgramStatus status in statusList)
            {
                ProgramStatusModel statusModel = new ProgramStatusModel();
                statusModel.ID = status.ProgramStatusGUID;
                statusModel.Name = status.Name;
                statusModel.Description = status.Description;

                statusModelList.Add(statusModel);
            }

            return statusModelList;
        }

        public ProgramStatusModel GetProgramStatusByStatusGuid(Guid statusGuid)
        {
            ProgramStatus status = Resolve<IProgramStatusRepository>().GetProgramStatusByStatusGuid(statusGuid);
            ProgramStatusModel statusModel = new ProgramStatusModel();
            statusModel.Name = status.Name;
            statusModel.Description = status.Description;
            statusModel.ID = status.ProgramStatusGUID;

            return statusModel;
        }

        public void DeleteProgramStatus(Guid statusGuid)
        {
            Resolve<IProgramStatusRepository>().DeleteProgramStatus(statusGuid);
        }

        public void UpdateProgramStatus(ChangeTech.Models.ProgramStatusModel statusModel)
        {
            ProgramStatus status = Resolve<IProgramStatusRepository>().GetProgramStatusByStatusGuid(statusModel.ID);
            status.Name = statusModel.Name;
            status.Description = statusModel.Description;

            Resolve<IProgramStatusRepository>().UpdateProgramStatus(status);
        }

        public void InstertProgramStatus(ChangeTech.Models.ProgramStatusModel statusModel)
        {
            ProgramStatus status = new ProgramStatus();
            status.Name = statusModel.Name;
            status.Description = statusModel.Description;
            status.ProgramStatusGUID = Guid.NewGuid();
            Resolve<IProgramStatusRepository>().InstertProgramStatus(status);
        }

        public bool CanDeleteProgramStatus(Guid statusGuid)
        {
            bool flag = true;
            if (Resolve<IProgramRepository>().GetProgramsByStatus(statusGuid).Count() > 0)
            {
                flag = false;
            }

            return flag;
        }

        #endregion
    }
}
