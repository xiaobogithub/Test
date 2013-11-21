using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects.DataClasses;
using ChangeTech.Entities;

namespace ChangeTech.IDataRepository
{
    public interface IPageContentRepository
    {
        void Add(PageContent newPageContent);
        //void Add(List<PageContent> list);
        //PageContent Get(Guid page, Guid language);
        PageContent GetPageContentByPageGuid(Guid pageGuid);
        List<PageContent> Get(Guid page);
        void Delete(Guid PageGuid);
        void Update(PageContent pageContent);
        IQueryable<PageContent> GetByProgramGUID(Guid programguid, int startDay, int endDay);
        //IQueryable<PageContent> GetByPagesequenceGUID(Guid pagesequenceGuid);
        IQueryable<PageContent> GetBySessionGUID(Guid sessionGuid);
        IQueryable<PageContent> GetPageContentInPageSequenceGUIDList(List<Guid> pageSequenceGuidList);
        IQueryable<PageContent> GetPageContentByTemplateGUID(Guid templateGUID);
        IQueryable<PageContent> GetPageContentsByProgramGUID(Guid programguid);
    }
}
