using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Entities;

namespace ChangeTech.IDataRepository
{
    public interface IProgramLanguageRepository
    {
        void DeleteAllLangaugeOfProgram(Guid ProgramGuid);
        void AddProgramLanguage(ProgramLanguage programLanguage);
        void RemoveProgramLanaguage(Guid programGuid, Guid languageGuid);
        IQueryable<ProgramLanguage> GetLanguagesOfProgram(Guid programGuid);
        ProgramLanguage GetProgramLanguage(Guid programGuid, Guid languageGuid);
    }
}
