using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects.DataClasses;
using ChangeTech.Entities;

namespace ChangeTech.IDataRepository
{
    public interface IPreferencesRepository
    {
        void DeletePreferences(Guid preferenceGuid);
        void DeletePreferences(EntityCollection<Preferences> preferences);
        void InsertPreference(Preferences preferenceEntity);
        void UpdatePreference(Preferences preferenceEntity);
        Preferences GetPreference(Guid preferenceGuid);
        IQueryable<Preferences> GetPreferenceByPageGuid(Guid pageGuid);
        IQueryable<Preferences> GetPreferenceByProgramGuid(Guid programGuid);
        //IQueryable<Preferences> GetPreferencesOfPage(Guid pageGuid, Guid languageGuid);
    }
}
