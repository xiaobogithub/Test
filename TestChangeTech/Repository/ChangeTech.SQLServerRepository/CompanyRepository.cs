using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Entities;
using ChangeTech.IDataRepository;

namespace ChangeTech.SQLServerRepository
{
    public class CompanyRepository : RepositoryBase, ICompanyRepository
    {
        #region ICompanyRepository Members

        public void AddCompany(Company entity)
        {
            InsertEntity(entity);
        }

        public Company GetCompanyByCode(string code)
        {
            return GetEntities<Company>(c => c.Code == code).FirstOrDefault();
        }

        public void DeleteCompany(Guid companyGUID)
        {
            DeleteEntity<Company>(c => c.CompanyGUID == companyGUID, Guid.Empty);
        }

        public void UpdateCompany(Company entity)
        {
            UpdateEntity(entity);
        }

        public Company GetCompanyByGuid(Guid companyGuid)
        {
            return GetEntities<Company>(c => c.CompanyGUID == companyGuid).FirstOrDefault();
        }

        public IQueryable<Company> GetAll()
        {
            return GetEntities<Company>();
        }

        public IQueryable<Company> GetCompanyNotJoinProgram(Guid programGuid)
        {
            return GetEntities<Company>(c => c.CompanyRight.Where(cr => cr.Program.ProgramGUID == programGuid).Count() == 0);
        }

        #endregion
    }
}
