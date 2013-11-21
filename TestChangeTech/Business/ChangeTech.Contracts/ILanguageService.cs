using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Models;

namespace ChangeTech.Contracts
{
    public interface ILanguageService
    {
        LanguagesModel GetLanguagesModel();
        void UpdateLanguage(Guid languageGUID, string name);
        void DeleteLanguage(Guid languageGUID);
        void AddLanguage(string name);
        LanguageModel GetLanguageMode(Guid languageGUID);
        bool IsValidLanguageName(string name);
        List<ManageLanguageModel> GetAllProgramLanguageModel();
    }
}
