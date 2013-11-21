using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Entities;

namespace ChangeTech.IDataRepository
{
    public interface IPageRepository
    {
        void InstertPage(Page page);
        void DeletePage(Guid guid);
        void DeletePage(System.Data.Objects.DataClasses.EntityCollection<Page> pages);
        void UpdatePage(Page page);
        List<Page> GetPagesByPageSequenceGuid(Guid pageSequenceGuid);
        List<Page> GetPagesBySessionGuid(Guid sessionGuid);
        IQueryable<Page> GetPagesByPageVariable(Guid variableGuid);
        Page GetPageByPageGuid(Guid pageGuid);
        Page GetPageByPageSequenceAndOrderNo(Guid pageSequenceGuid, int orderNo);
        Page GetLastPageOfPageSequence(Guid pageSequenceGuid);
        IQueryable<Page> GetPageVariableCountByProgramGuidAndPageVariableGuid(Guid programGuid, Guid pageVaribleGuid);
    }
}
