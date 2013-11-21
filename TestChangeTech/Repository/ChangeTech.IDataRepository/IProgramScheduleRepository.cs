using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ChangeTech.Entities;

namespace ChangeTech.IDataRepository
{
    public interface IProgramScheduleRepository
    {
        ProgramSchedule GetProgramScheduleByProgramWeekWeekday(Guid programGuid, int week, int weekday);
        ProgramSchedule GetProgramScheduleByProgramAndDay(Guid programGuid, int day);
        IQueryable<ProgramSchedule> GetProgramSchedule(Guid programGuid);
        IQueryable<ProgramSchedule> GetProgramScheduleAndWeek(Guid programGuid, int week);
        ProgramSchedule GetProgramScheduleByProgramScheduleGuid(int programScheduleID);
        ProgramSchedule GetFirstDayScheduleOfProgram(Guid programGuid);
    }
}
