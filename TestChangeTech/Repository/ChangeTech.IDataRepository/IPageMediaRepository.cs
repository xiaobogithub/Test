using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Entities;
using System.Data.Objects.DataClasses;

namespace ChangeTech.IDataRepository
{
    public interface IPageMediaRepository
    {
        //PageMedia GetPageMedia(Guid pageGuid, Guid languageGuid);
        PageMedia GetPageMediaByPageGuid(Guid pageGuid);
        IQueryable<PageMedia> GetPageMediasBySessionGuid(Guid sessionGuid);
        void AddPageMedia(PageMedia newPageMedia);
        void UpdatePageMedia(PageMedia pageMeadia);
        void DeletePageMedia(Guid pageGuid);
        void DeletePageMedia(EntityCollection<PageMedia> pageMedias);
    }
}
