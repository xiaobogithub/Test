using System;
using System.Linq;
using System.Collections.Generic;
using ChangeTech.Entities;


namespace ChangeTech.IDataRepository
{
    public interface IProgramRepository
    {
        IQueryable<Program> GetAllPrograms();
        IQueryable<Program> GetProgramsByStatus(Guid statusGuid);
        void Update(Program program);
        void DeleteProgram(Guid programGuid);
        Guid InsertProgram(Program program);
        Program GetProgramByGuid(Guid programGuid);
        Program GetProgramByProgramDefaultGUIDAndLanguageGUID(Guid ProgramDefaultGUID, Guid languageGUID);
        LayoutSetting GetLayoutSettingByProgramGUID(Guid programGuid);
        void UpdateLayout(LayoutSetting layout);
        void DeleteProgramSchedule(Guid programGuid, int week);
        IQueryable<Program> GetProgramByParentProgramGUID(Guid programGuid);
        IQueryable<Program> GetProgramByDefaultGUID(Guid programGuid);
        int GetProgramCountByLanguageGUID(Guid languageGUID);
        IQueryable<Program> GetProgramsNoTipMessage();
        IQueryable<Program> GetProgramsNoButtonColor();
        IQueryable<Program> GetProgramsByShortName(string shortName);
        IQueryable<Program> GetProgramByProgramCode(string code);
        IQueryable<Program> GetProgramsByLanguageGuid(Guid languageGuid);
    }
}