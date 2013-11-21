using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Models;

namespace ChangeTech.Contracts
{
    public interface IProgramStatusService
    {
        List<ProgramStatusModel> GetAllProgramStatus();
        ProgramStatusModel GetProgramStatusByStatusGuid(Guid statusGuid);
        bool CanDeleteProgramStatus(Guid statusGuid);
        void DeleteProgramStatus(Guid statusGuid);
        void UpdateProgramStatus(ProgramStatusModel statusModel);
        void InstertProgramStatus(ProgramStatusModel statusModel);
    }
}
