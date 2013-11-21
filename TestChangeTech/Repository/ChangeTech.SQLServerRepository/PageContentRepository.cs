using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Entities;
using ChangeTech.IDataRepository;
using System.Data.Objects.DataClasses;

namespace ChangeTech.SQLServerRepository
{
    public class PageContentRepository : RepositoryBase, IPageContentRepository
    {
        #region IPageContentRepository Members

        public void Add(PageContent newPageContent)
        {
            InsertEntity(newPageContent);
        }

        //public void Add(List<PageContent> list)
        //{
        //    InsertEntities<PageContent>(list, Guid.NewGuid());
        //}

        //public PageContent Get(Guid page, Guid language)
        //{
        //    return GetEntities<PageContent>(p=>p.PageGUID == page && p.LanguageGUID == language && 
        //        (p.IsDeleted.HasValue? p.IsDeleted.Value == false: true)).FirstOrDefault();
        //}

        public List<PageContent> Get(Guid page)
        {
            return GetEntities<PageContent>(p => p.PageGUID == page &&
                (p.IsDeleted.HasValue ? p.IsDeleted.Value == false : true)).ToList<PageContent>();
        }

        public IQueryable<PageContent> GetByProgramGUID(Guid programguid, int startDay, int endDay)
        {
            return GetEntities<PageContent>(p =>
                (p.Page.PageSequence.SessionContent.Where(s => s.Session.Program.ProgramGUID == programguid &&
                    (s.Session.Day.Value >= startDay && s.Session.Day.Value <= endDay) &&
                    !(s.IsDeleted.HasValue && s.IsDeleted.Value) &&
                    !(s.Session.IsDeleted.HasValue && s.Session.IsDeleted.Value) &&
                    !(s.Session.Program.IsDeleted.HasValue && s.Session.Program.IsDeleted.Value)).Count() > 0
                    || p.Page.PageSequence.Relapse.Where(r => r.Program.ProgramGUID == programguid).Count() > 0
                    )
                    &&
                    !(p.IsDeleted.HasValue && p.IsDeleted.Value) &&
                    !(p.Page.IsDeleted.HasValue && p.Page.IsDeleted.Value) &&
                    !(p.Page.PageSequence.IsDeleted.HasValue && p.Page.PageSequence.IsDeleted.Value)).OrderBy(p => p.Page.PageOrderNo);
        }


        //public void Delete(EntityCollection<PageContent> pageContents)
        //{
        //    DeleteEntities<PageContent>(pageContents, Guid.Empty);
        //}

        public void Delete(Guid PageGuid)
        {
            DeleteEntity<PageContent>(p => p.PageGUID == PageGuid, Guid.Empty);
        }

        public void Update(PageContent pageContent)
        {
            UpdateEntity(pageContent);
        }

        public PageContent GetPageContentByPageGuid(Guid pageGuid)
        {
            return GetEntities<PageContent>(pc => pc.Page.PageGUID == pageGuid && (!pc.IsDeleted.HasValue || pc.IsDeleted == false)).FirstOrDefault();
        }

        //public IQueryable<PageContent> GetByPagesequenceGUID(Guid pagesequenceGuid)
        //{
        //    return GetEntities<PageContent>(pc => pc.Page.PageSequence.PageSequenceGUID == pagesequenceGuid && (pc.IsDeleted.HasValue ? pc.IsDeleted.Value == false : true)).OrderBy(pc => pc.Page.PageOrderNo);
        //}

        public IQueryable<PageContent> GetBySessionGUID(Guid sessionGuid)
        {
            return GetEntities<PageContent>(pc => pc.Page.PageSequence.SessionContent.Where(sc => sc.Session.SessionGUID == sessionGuid).Count() > 0 && (!pc.IsDeleted.HasValue || pc.IsDeleted.Value == false));
        }

        public IQueryable<PageContent> GetPageContentInPageSequenceGUIDList(List<Guid> pageSequenceGuidList)
        {
            return GetEntities<PageContent>(pc => pageSequenceGuidList.Contains(pc.Page.PageSequence.PageSequenceGUID) && (!pc.IsDeleted.HasValue || pc.IsDeleted.Value == false));
        }

        public IQueryable<PageContent> GetPageContentByTemplateGUID(Guid templateGUID)
        {
            return GetEntities<PageContent>(pc => pc.Page.PageTemplate.PageTemplateGUID == templateGUID && (!pc.IsDeleted.HasValue || pc.IsDeleted == false));
        }

        public IQueryable<PageContent> GetPageContentsByProgramGUID(Guid programguid)
        {
            //TODO: PageContents might be used in many programs.
            //Now follow :  When modified a page variable , update used this variable in page of programs. Not only is the current program.
            return GetEntities<PageContent>(p =>
                (
                    p.Page.PageSequence.SessionContent.Where(s => s.Session.Program.ProgramGUID == programguid &&
                        !(s.IsDeleted.HasValue && s.IsDeleted.Value) &&
                        !(s.Session.IsDeleted.HasValue && s.Session.IsDeleted.Value) &&
                        !(s.Session.Program.IsDeleted.HasValue && s.Session.Program.IsDeleted.Value)).Count() > 0
                    || p.Page.PageSequence.Relapse.Where(r => r.Program.ProgramGUID == programguid).Count() > 0
                )
                && !(p.IsDeleted.HasValue && p.IsDeleted.Value) 
                && !(p.Page.IsDeleted.HasValue && p.Page.IsDeleted.Value) 
                && !(p.Page.PageSequence.IsDeleted.HasValue && p.Page.PageSequence.IsDeleted.Value))
                .OrderBy(p => p.Page.PageOrderNo);
        }
        #endregion
    }
}
