using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Entities;

namespace ChangeTech.IDataRepository
{
    public interface ILanguageRepository
    {
        IQueryable<Language> GetAllLanguages();
        void UpdateLanguage(Language language);
        void DeleteLanguage(Guid languageGUID);
        void AddLanguage(Language language);
        Language GetLanguage(Guid languageGUID);
        Language GetLanguage(string languageName);
    }
}
