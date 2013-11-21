using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Entities;
using ChangeTech.IDataRepository;
using System.Data.Objects.DataClasses;

namespace ChangeTech.SQLServerRepository
{
    public class PreferencesRepository : RepositoryBase, IPreferencesRepository
    {
        #region IPreferencesRepository Members
        public Preferences GetPreference(Guid preferenceGuid)
        {
            return GetEntities<Preferences>(p => p.PreferencesGUID == preferenceGuid && 
                (p.IsDeleted.HasValue? p.IsDeleted.Value == false: true)).FirstOrDefault();
        }

        //public IQueryable<Preferences> GetPreferencesOfPage(Guid pageGuid, Guid languageGuid)
        //{
        //    return GetEntities<Preferences>(p => p.Page.PageGUID == pageGuid && p.Language.LanguageGUID == languageGuid &&
        //        (p.IsDeleted.HasValue ? p.IsDeleted.Value == false : true));
        //}

        public IQueryable<Preferences> GetPreferenceByProgramGuid(Guid programGuid)
        {
            return GetEntities<Preferences>(p => !(p.IsDeleted.HasValue && p.IsDeleted.Value) &&
                !(p.Page.IsDeleted.HasValue && p.Page.IsDeleted.Value) &&
                !(p.Page.PageSequence.IsDeleted.HasValue && p.Page.PageSequence.IsDeleted.Value) &&
                p.Page.PageSequence.SessionContent.Where(s=> s.Session.Program.ProgramGUID == programGuid &&
                    !(s.IsDeleted.HasValue && s.IsDeleted.Value) &&
                    !(s.Session.IsDeleted.HasValue && s.Session.IsDeleted.Value) &&
                    !(s.Session.Program.IsDeleted.HasValue && s.Session.Program.IsDeleted.Value)).Count() > 0);
        }

        public IQueryable<Preferences> GetPreferenceByPageGuid(Guid pageGuid)
        {
            return GetEntities<Preferences>(p => p.Page.PageGUID == pageGuid && (p.IsDeleted.HasValue? p.IsDeleted.Value == false: true));
        }

        public void DeletePreferences(Guid preferenceGuid)
        {
            DeleteEntity<Preferences>(p => p.PreferencesGUID == preferenceGuid, Guid.Empty);
        }

        public void DeletePreferences(EntityCollection<Preferences> preferences)
        {
            DeleteEntities<Preferences>(preferences, Guid.Empty);
        }

        public void InsertPreference(Preferences preferenceEntity)
        {
            InsertEntity(preferenceEntity);
        }

        public void UpdatePreference(Preferences preferenceEntity)
        {
            UpdateEntity(preferenceEntity);
        }
        #endregion
    }
}
