using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ChangeTech.Models;
using ChangeTech.Entities;

namespace ChangeTech.Contracts
{
    public interface IProgramScheduleService
    {
        ProgramSchedule CloneProgramSchedule(ProgramSchedule schedule);
        ProgramSchedule SetDefaultGuidForProgramSchedule(ProgramSchedule needSetDefaultEntity, CloneProgramParameterModel cloneParameterModel);
        List<ProgramScheduleModel> GetProgramScheduleByProgram(Guid programGuid);
        ProgramScheduleModel GetProgramScheduleByProgramAndWeek(Guid programGuid, int week);
        void SaveProgramSchedule(ProgramScheduleModel model, Guid programGuid);
    }
}
