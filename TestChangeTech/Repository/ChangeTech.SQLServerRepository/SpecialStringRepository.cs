using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ChangeTech.Entities;
using ChangeTech.IDataRepository;

namespace ChangeTech.SQLServerRepository
{
    public class SpecialStringRepository : RepositoryBase, ISpecialStringRepository
    {
        public SpecialString GetSpecialString(Guid languageGUID, string name)
        {
            return GetEntities<SpecialString>(s=>s.Language.LanguageGUID == languageGUID && s.Name.Equals(name)).FirstOrDefault();
        }

        public Setting GetSettingsByName(string name)
        {
            return GetEntities<Setting>(s => s.Name == name).FirstOrDefault(); 
        }

        public IQueryable<SpecialString> GetSpecialStringListOfLanguage(Guid lanaguageGUID)
        {
            return GetEntities<SpecialString>(s => s.Language.LanguageGUID == lanaguageGUID);
        }

        public void AddSpecialString(SpecialString specialString)
        {
            InsertEntity(specialString);
        }

        public void Update(SpecialString specialString)
        {
            UpdateEntity(specialString);
        }

        public IQueryable<string> GetAllSpecialStringKeys()
        {
            return GetEntities<SpecialString>().OrderBy(s=>s.Name).Select(s=>s.Name).Distinct();
        }

        public bool ExistSpecialString(string name)
        {
            return GetEntities<SpecialString>().Where(s => s.Name == name).FirstOrDefault() == null ? false : true;
        }
    }
}
