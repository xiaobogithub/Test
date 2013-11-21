using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Entities;

namespace ChangeTech.IDataRepository
{
    public interface IProgramStatusRepository
    {
        IQueryable<ProgramStatus> GetAllProgramStatus();
        ProgramStatus GetProgramStatusByStatusGuid(Guid statusGuid);
        //ProgramStatus Get(Guid statusGuid);
        void UpdateProgramStatus(ProgramStatus status);
        void DeleteProgramStatus(Guid statusGuid);
        void InstertProgramStatus(ProgramStatus status);
    }
}
