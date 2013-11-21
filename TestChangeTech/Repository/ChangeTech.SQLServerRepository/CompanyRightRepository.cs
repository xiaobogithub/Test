using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Entities;
using ChangeTech.IDataRepository;

namespace ChangeTech.SQLServerRepository
{
    public class CompanyRightRepository : RepositoryBase, ICompanyRightRepository
    {
        #region ICompanyRightRepository Members

        public void Add(CompanyRight entity)
        {
            InsertEntity(entity);
        }

        public void Update(CompanyRight entity)
        {
            UpdateEntity(entity);
        }

        public void Delete(Guid rightGuid)
        {
            DeleteEntity<CompanyRight>(cr => cr.CompanyRightGUID == rightGuid, Guid.Empty);
        }

        public IQueryable<CompanyRight> GetCompanyRightByProgram(Guid programGuid)
        {
            return GetEntities<CompanyRight>(cr => cr.Program.ProgramGUID == programGuid);
        }

        public CompanyRight GetConpanyRightByGuid(Guid rightGuid)
        {
            return GetEntities<CompanyRight>(cr => cr.CompanyRightGUID == rightGuid).FirstOrDefault();
        }

        public CompanyRight GetCompanyRightByProgramAndCompany(Guid programguid, Guid companyguid)
        {
            return GetEntities<CompanyRight>(cr => cr.Program.ProgramGUID == programguid && cr.Company.CompanyGUID == companyguid).FirstOrDefault();
        }

        #endregion
    }
}
