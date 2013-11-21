using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Entities;
using ChangeTech.IDataRepository;

namespace ChangeTech.SQLServerRepository
{
    public class PageVariableGroupRepository : RepositoryBase, IPageVariableGroupRepository
    {
        public void Insert(PageVariableGroup group)
        {
            InsertEntity(group);
        }

        public void Update(PageVariableGroup group)
        {
            UpdateEntity(group);
        }

        public void Delete(Guid groupGuid)
        {
            DeleteEntity<PageVariableGroup>(g => g.PageVariableGroupGUID == groupGuid, Guid.Empty);
        }

        public PageVariableGroup Get(Guid groupGuid)
        {
            return GetEntities<PageVariableGroup>(g => g.PageVariableGroupGUID == groupGuid).FirstOrDefault();
        }

        public IQueryable<PageVariableGroup> GetGroupByProgram(Guid programGuid)
        {
            return GetEntities<PageVariableGroup>(g => g.Program.ProgramGUID == programGuid);
        }

        public PageVariableGroup GetPageVariableByProgramAndParentGroupGUID(Guid programGuid, Guid parentPageVariableGroupGuid)
        {
            return GetEntities<PageVariableGroup>(p=>p.Program.ProgramGUID == programGuid).FirstOrDefault();
        }

        public void DeleteGroupOfProgram(Guid programGuid)
        {
            DeleteEntities<PageVariableGroup>(pv=>pv.Program.ProgramGUID == programGuid, Guid.Empty);
        }
    }
}
