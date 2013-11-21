using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.IDataRepository;
using ChangeTech.Entities;

namespace ChangeTech.SQLServerRepository
{
    public class PageRepository : RepositoryBase, IPageRepository
    {
        #region IPageRepository Members

        public void InstertPage(Page page)
        {  
            InsertEntity(page);
        }

        public void DeletePage(Guid guid)
        {
            DeleteEntity<Page>(p => p.PageGUID == guid, new Guid());
        }

        public void DeletePage(System.Data.Objects.DataClasses.EntityCollection<Page> pages)
        {
            DeleteEntities<Page>(pages, new Guid());
        }

        public void UpdatePage(Page page)
        {
            UpdateEntity(page);
        }

        public IQueryable<Page> GetPagesByPageVariable(Guid variableGuid)
        {
            return GetEntities<Page>(p => p.PageVariable.PageVariableGUID == variableGuid);
        }

        public List<Page> GetPagesByPageSequenceGuid(Guid pageSequenceGuid)
        {
            return GetEntities<Page>(p => p.PageSequence.PageSequenceGUID == pageSequenceGuid && (p.IsDeleted.HasValue? p.IsDeleted == false: true)).OrderBy(p => p.PageOrderNo).ToList<Page>();
        }

        public List<Page> GetPagesBySessionGuid(Guid sessionGuid)
        {
            return GetEntities<Page>(p => p.PageSequence.SessionContent.Where(sc => sc.Session.SessionGUID == sessionGuid && (sc.IsDeleted.HasValue ? sc.IsDeleted == false : true)).Count() > 0 && (p.IsDeleted.HasValue ? p.IsDeleted == false : true)).OrderBy(p => p.PageOrderNo).ToList<Page>();
        }

        public Page GetPageByPageGuid(Guid pageGuid)
        {
            return GetEntities<Page>(p => p.PageGUID == pageGuid && (p.IsDeleted.HasValue ? p.IsDeleted == false : true)).FirstOrDefault();
        }

        public Page GetPageByPageSequenceAndOrderNo(Guid pageSequenceGuid, int orderNo)
        {
            return GetEntities<Page>(p => p.PageSequence.PageSequenceGUID == pageSequenceGuid && p.PageOrderNo == orderNo && (p.IsDeleted.HasValue ? p.IsDeleted == false : true)).FirstOrDefault();
        }

        public Page GetLastPageOfPageSequence(Guid pageSequenceGuid)
        {
            return GetEntities<Page>(p => p.PageSequence.PageSequenceGUID == pageSequenceGuid && (p.IsDeleted.HasValue ? p.IsDeleted == false : true)).OrderByDescending(p => p.PageOrderNo).FirstOrDefault();
        }

        public IQueryable<Page> GetPageVariableCountByProgramGuidAndPageVariableGuid(Guid programGuid, Guid pageVaribleGuid)
        {
            return GetEntities<Page>(p => p.PageSequence.SessionContent.Where(sc => sc.Session.Program.ProgramGUID == programGuid &&
                 !(sc.IsDeleted.HasValue && sc.IsDeleted.Value) &&
                 !(sc.Session.IsDeleted.HasValue && sc.IsDeleted.Value) &&
                 !(sc.Session.Program.IsDeleted.HasValue && sc.Session.Program.IsDeleted.Value)).Count() > 0 &&
                  (p.PageQuestion.Where(pq => pq.PageVariable.PageVariableGUID == pageVaribleGuid &&
                     pq.PageVariable.PageVariableGUID != null &&
                     !(pq.IsDeleted.HasValue && pq.IsDeleted.Value)).Count() > 0
                     || p.PageSequence.Relapse.Where(r => r.Program.ProgramGUID == programGuid).Count() > 0)
                     &&
                     !(p.IsDeleted.HasValue && p.IsDeleted.Value) &&
                     !(p.PageSequence.IsDeleted.HasValue && p.PageSequence.IsDeleted.Value)).OrderBy(p => p.PageOrderNo);
        }
        #endregion
    }
}
