using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Models;

namespace ChangeTech.Contracts
{
    public interface IProgramLanguageService
    {
        LanguagesModel GetLanguagesNotSupportByProgram(Guid programGuid);
        LanguagesModel GetLanguagesSupportByProgram(Guid programGuid);
        void AddProgramLanguage(Guid programGuid, Guid languageGuid);
        void RemoveProgramLanguage(Guid programGuid, Guid languageGuid);
        Guid GetLanguageOfProgramBySessionGUID(Guid sessionGuid);
        Guid GetLanguageOfProgramByProgramGUID(Guid programGuid);
        Guid GetDefaultProgramGUID(Guid programGUID);             
    }
}
