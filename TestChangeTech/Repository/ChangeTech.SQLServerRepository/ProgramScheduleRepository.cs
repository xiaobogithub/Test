using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ChangeTech.Entities;
using ChangeTech.IDataRepository;

namespace ChangeTech.SQLServerRepository
{
    public class ProgramScheduleRepository : RepositoryBase, IProgramScheduleRepository
    {
        public ProgramSchedule GetProgramScheduleByProgramWeekWeekday(Guid programGuid, int week, int weekday)
        {
            return GetEntities<ProgramSchedule>(p => p.Program.ProgramGUID == programGuid && p.WeekDay == weekday && p.Week == week).FirstOrDefault();
        }

        public ProgramSchedule GetProgramScheduleByProgramAndDay(Guid programGuid, int day)
        {
            IQueryable<ProgramSchedule> schedules = GetProgramSchedule(programGuid);
            ProgramSchedule schedule = schedules.OrderBy(p => p.Week).ThenBy(p => p.WeekDay).Skip(day).Take(1).FirstOrDefault();
            return schedule;
        }

        public IQueryable<ProgramSchedule> GetProgramSchedule(Guid programGuid)
        {
            return GetEntities<ProgramSchedule>(p => p.Program.ProgramGUID == programGuid).OrderBy(p => p.Week).ThenBy(p => p.WeekDay);
        }

        public IQueryable<ProgramSchedule> GetProgramScheduleAndWeek(Guid programGuid, int week)
        {
            return GetEntities<ProgramSchedule>(p => p.Program.ProgramGUID == programGuid && p.Week == week);
        }

        public ProgramSchedule GetProgramScheduleByProgramScheduleGuid(int programScheduleID)
        {
            return GetEntities<ProgramSchedule>(p => p.ID == programScheduleID).FirstOrDefault();
        }

        public ProgramSchedule GetFirstDayScheduleOfProgram(Guid programGuid)
        {
            return GetEntities<ProgramSchedule>(p => p.Program.ProgramGUID == programGuid).OrderBy(p => p.Week).ThenBy(p => p.WeekDay).FirstOrDefault();
        }
    }
}
