using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ChangeTech.IDataRepository;
using ChangeTech.Entities;

namespace ChangeTech.SQLServerRepository
{
    public class ProgramLanguageRepository : RepositoryBase, IProgramLanguageRepository
    {
        public void DeleteAllLangaugeOfProgram(Guid programGuid)
        {
            DeleteEntities<ProgramLanguage>(pl=>pl.Program.ProgramGUID == programGuid, Guid.Empty);
        }

        public void AddProgramLanguage(ProgramLanguage programLanguage)
        {
            InsertEntity(programLanguage);
        }

        public void RemoveProgramLanaguage(Guid programGuid, Guid languageGuid)
        {
            DeleteEntity<ProgramLanguage>(pl=>pl.Program.ProgramGUID == programGuid && pl.Language.LanguageGUID == languageGuid, Guid.Empty);
        }

        public IQueryable<ProgramLanguage> GetLanguagesOfProgram(Guid programGuid)
        {
            return GetEntities<ProgramLanguage>().Where(pl=>pl.Program.ProgramGUID == programGuid);
        }


        public ProgramLanguage GetProgramLanguage(Guid programGuid, Guid languageGuid)
        {
            return GetEntities<ProgramLanguage>().Where(pl => pl.Program.ProgramGUID == programGuid && pl.Language.LanguageGUID == languageGuid).FirstOrDefault();
        }
    }
}
