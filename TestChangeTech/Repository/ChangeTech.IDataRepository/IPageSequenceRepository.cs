using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Entities;

namespace ChangeTech.IDataRepository
{
    public interface IPageSequenceRepository
    {
        List<PageSequence> GetPageSequenceByInterventCategoryGuid(Guid interventCategoryGuid);
        List<PageSequence> GetPageSequenceByInterventGuid(Guid interventGuid);
        IQueryable<PageSequence> GetPageSequenceBySessionGuid(Guid sessionGuid);
        IQueryable<PageSequence> GetPageSequenceByProgramGuid(Guid programGuid);
        PageSequence GetPageSequenceByGuid(Guid seqGuid);
        void DeletePageSequence(Guid seqGuid);
        void UpdatePageSequence(PageSequence pageSequence);
        void InstertPageSequence(PageSequence pageSequence);        
    }
}
