using System;
using System.Collections.Generic;
using ChangeTech.Entities;
using ChangeTech.IDataRepository;
using System.Linq;

namespace ChangeTech.SQLServerRepository
{
    public class PageSequenceRepository : RepositoryBase, IPageSequenceRepository
    {
        #region IPageSequenceRepository Members

        public List<PageSequence> GetPageSequenceByInterventCategoryGuid(Guid interventCategoryGuid)
        {
            return GetEntities<PageSequence>(p => p.Intervent.InterventCategory.InterventCategoryGUID == interventCategoryGuid &&
                (p.IsDeleted.HasValue ? p.IsDeleted.Value == false : true)).OrderBy(p => p.Name).ToList<PageSequence>();
        }

        public List<PageSequence> GetPageSequenceByInterventGuid(Guid interventGuid)
        {
            return GetEntities<PageSequence>(p => p.Intervent.InterventGUID == interventGuid &&
                (p.IsDeleted.HasValue ? p.IsDeleted.Value == false : true)).ToList<PageSequence>();
        }

        public IQueryable<PageSequence> GetPageSequenceBySessionGuid(Guid sessionGuid)
        {
            return GetEntities<PageSequence>(p => p.SessionContent.FirstOrDefault().Session.SessionGUID == sessionGuid && (p.IsDeleted.HasValue ? p.IsDeleted.Value == false : true)).OrderBy(p => p.SessionContent.FirstOrDefault().PageSequenceOrderNo);
        }

        public PageSequence GetPageSequenceByGuid(Guid SeqGuid)
        {
            return GetEntities<PageSequence>(p => p.PageSequenceGUID == SeqGuid &&
                (p.IsDeleted.HasValue ? p.IsDeleted.Value == false : true)).FirstOrDefault();
        }

        public void InstertPageSequence(PageSequence pageSequence)
        {
            InsertEntity(pageSequence);
        }

        public void DeletePageSequence(Guid seqGuid)
        {
            PageSequence ps = new PageSequence();
            DeleteEntity<PageSequence>(p => p.PageSequenceGUID == seqGuid, new Guid());
        }

        public void UpdatePageSequence(PageSequence pageSequence)
        {
            UpdateEntity(pageSequence);
        }

        public IQueryable<PageSequence> GetPageSequenceByProgramGuid(Guid programGuid)
        {
            return GetEntities<PageSequence>(p => !(p.IsDeleted.HasValue && p.IsDeleted.Value) && (p.SessionContent.Where(s => !(s.Session.IsDeleted.HasValue && s.Session.IsDeleted.Value) && !(s.IsDeleted.HasValue && s.IsDeleted.Value) && s.Session.Program.ProgramGUID == programGuid).Count() > 0));
        }
        #endregion
    }
}
