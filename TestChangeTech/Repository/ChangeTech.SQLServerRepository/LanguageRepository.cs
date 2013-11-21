using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.IDataRepository;
using ChangeTech.Entities;

namespace ChangeTech.SQLServerRepository
{
    public class LanguageRepository: RepositoryBase, ILanguageRepository
    {
        #region ILanguageRepository Members

        public IQueryable<Language> GetAllLanguages()
        {
            return GetEntities<Language>();
        }

        public void UpdateLanguage(Language language)
        {
            UpdateEntity(language);
        }

        public void DeleteLanguage(Guid languageGUID)
        {
            DeleteEntity<Language>(language => language.LanguageGUID == languageGUID, new Guid());
        }

        public void AddLanguage(Language language)
        {
            InsertEntity(language);
        }

        public Language GetLanguage(Guid languageGUID)
        {
            return GetEntities<Language>(l => l.LanguageGUID == languageGUID && 
                (l.IsDeleted.HasValue? l.IsDeleted.Value == false:true)).FirstOrDefault();
        }

        public Language GetLanguage(string languageName)
        {
            return GetEntities<Language>(l => l.Name.Equals(languageName) && 
                (l.IsDeleted.HasValue? l.IsDeleted.Value == false : true)).FirstOrDefault();
        }
        #endregion
    }
}
