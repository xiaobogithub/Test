using System;
using System.Linq;
using System.Collections.Generic;
using ChangeTech.IDataRepository;
using ChangeTech.Entities;

namespace ChangeTech.SQLServerRepository
{
    public class ProgramRepository : RepositoryBase, IProgramRepository
    {
        #region IProgramRepository Members

        public IQueryable<Program> GetAllPrograms()
        {
            //return GetEntities<Program>().Where(p => (p.IsDeleted.HasValue ? p.IsDeleted.Value == false : true) &&
            //    !p.ParentProgramGUID.HasValue);
            return GetEntities<Program>().Where(p => (p.IsDeleted.HasValue ? p.IsDeleted.Value == false : true)).OrderBy(p => p.Name);
        }

        public Program GetProgramByGuid(Guid programGuid)
        {
            return GetEntities<Program>().Where(p => p.ProgramGUID == programGuid && (p.IsDeleted.HasValue ? p.IsDeleted.Value == false : true)).FirstOrDefault();
        }

        public IQueryable<Program> GetProgramsByStatus(Guid statusGuid)
        {
            return GetEntities<Program>(p => p.ProgramStatus.ProgramStatusGUID == statusGuid &&
                (p.IsDeleted.HasValue ? p.IsDeleted.Value == false : true)).OrderBy(p => p.Name);
        }

        public void Update(ChangeTech.Entities.Program program)
        {
            UpdateEntity(program);
        }

        public void DeleteProgram(Guid programGuid)
        {
            DeleteEntity<Program>(p => p.ProgramGUID == programGuid, new Guid());
        }

        public Guid InsertProgram(ChangeTech.Entities.Program program)
        {
            InsertEntity(program);

            return program.ProgramGUID;
        }

        public Program GetProgramByProgramDefaultGUIDAndLanguageGUID(Guid ProgramDefaultGUID, Guid languageGUID)
        {
            return GetEntities<Program>(p => p.DefaultGUID == ProgramDefaultGUID &&
                p.Language.LanguageGUID == languageGUID &&
                (!(p.IsDeleted.HasValue && p.IsDeleted.Value == true))).OrderByDescending(p => p.Created).FirstOrDefault();
        }

        public LayoutSetting GetLayoutSettingByProgramGUID(Guid programGuid)
        {
            return GetEntities<LayoutSetting>(l => l.ProgramGUID == programGuid).FirstOrDefault();
        }

        public void UpdateLayout(LayoutSetting layout)
        {
            UpdateEntity(layout);
        }


        public void DeleteProgramSchedule(Guid programGuid, int week)
        {
            DeleteEntities<ProgramSchedule>(p => p.Program.ProgramGUID == programGuid && p.Week == week, Guid.Empty);
        }

        public IQueryable<Program> GetProgramByParentProgramGUID(Guid programGuid)
        {
            return GetEntities<Program>().Where(p => p.ParentProgramGUID == programGuid);
        }

        public IQueryable<Program> GetProgramByDefaultGUID(Guid programGuid)
        {
            return GetEntities<Program>().Where(p => p.DefaultGUID == programGuid);
        }

        public int GetProgramCountByLanguageGUID(Guid languageGUID)
        {
            return GetEntities<Program>(p => p.Language.LanguageGUID == languageGUID && (!(p.IsDeleted.HasValue && p.IsDeleted.Value == true))).Count();
        }

        public IQueryable<Program> GetProgramsNoTipMessage()
        {
            return GetEntities<Program>(p => p.TipMessage.Count == 0 && (!p.IsDeleted.HasValue || p.IsDeleted.HasValue && p.IsDeleted == false));
        }

        public IQueryable<Program> GetProgramsNoButtonColor()
        {
            return GetEntities<Program>(p => p.PrimaryButtonColorDisable == null && (!p.IsDeleted.HasValue || p.IsDeleted.HasValue && p.IsDeleted == false));
        }

        public IQueryable<Program> GetProgramsByShortName(string shortName)
        {
            return GetEntities<Program>(p => p.ShortName == shortName && (!p.IsDeleted.HasValue || p.IsDeleted.HasValue && p.IsDeleted == false));
        }

        public IQueryable<Program> GetProgramByProgramCode(string code)
        {
            return GetEntities<Program>(p => p.Code == code && (!p.IsDeleted.HasValue || p.IsDeleted.HasValue && p.IsDeleted == false));
        }

        public IQueryable<Program> GetProgramsByLanguageGuid(Guid languageGuid)
        {
            return GetEntities<Program>(p => (!p.IsDeleted.HasValue || p.IsDeleted.HasValue && p.IsDeleted == false) && p.Language.LanguageGUID == languageGuid).OrderBy(p=>p.Name);
        }

        #endregion
    }
}
