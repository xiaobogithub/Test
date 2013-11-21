using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Entities;

namespace ChangeTech.IDataRepository
{
    public interface IPageVariableGroupRepository
    {
        void Insert(PageVariableGroup group);
        void Update(PageVariableGroup group);
        void Delete(Guid groupGuid);
        void DeleteGroupOfProgram(Guid programGuid);
        PageVariableGroup Get(Guid groupGuid);
        PageVariableGroup GetPageVariableByProgramAndParentGroupGUID(Guid programGuid, Guid parentPageVariableGroupGuid);
        IQueryable<PageVariableGroup> GetGroupByProgram(Guid programGuid);
    }
}
