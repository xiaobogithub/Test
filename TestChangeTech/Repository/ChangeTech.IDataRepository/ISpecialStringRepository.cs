using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ChangeTech.Entities;

namespace ChangeTech.IDataRepository
{
    public interface ISpecialStringRepository
    {
        SpecialString GetSpecialString(Guid languageGUID, string name);
        Setting GetSettingsByName(string name);
        IQueryable<SpecialString> GetSpecialStringListOfLanguage(Guid lanaguageGUID);
        void AddSpecialString(SpecialString specialString);
        void Update(SpecialString specialString);
        IQueryable<string> GetAllSpecialStringKeys();
        bool ExistSpecialString(string name);
    }
}
