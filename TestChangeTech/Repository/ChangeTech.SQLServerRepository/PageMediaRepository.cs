using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Entities;
using ChangeTech.IDataRepository;
using System.Data.Objects.DataClasses;

namespace ChangeTech.SQLServerRepository
{
    public class PageMediaRepository : RepositoryBase, IPageMediaRepository
    {
        #region IPageMediaRepository Members
        public PageMedia GetPageMediaByPageGuid(Guid pageGuid)
        {
            return GetEntities<PageMedia>(pm => pm.Page.PageGUID == pageGuid).FirstOrDefault();
        }

        public IQueryable<PageMedia> GetPageMediasBySessionGuid(Guid sessionGuid)
        {
            return GetEntities<PageMedia>(pm => (pm.IsDeleted.HasValue ? pm.IsDeleted.Value == false : true) && pm.Page.PageSequence.SessionContent.Where(sc => sc.Session.SessionGUID == sessionGuid).Count() > 0);
        }

        public void AddPageMedia(PageMedia newPageMedia)
        {
            InsertEntity(newPageMedia);
        }

        public void UpdatePageMedia(PageMedia pageMeadia)
        {
            UpdateEntity(pageMeadia);
        }

        public void DeletePageMedia(Guid pageGuid)
        {
            DeleteEntity<PageMedia>(p => p.PageGUID == pageGuid, Guid.Empty);
        }

        public void DeletePageMedia(EntityCollection<PageMedia> pageMedios)
        {
            DeleteEntities<PageMedia>(pageMedios, Guid.Empty);
        }

        #endregion
    }
}
