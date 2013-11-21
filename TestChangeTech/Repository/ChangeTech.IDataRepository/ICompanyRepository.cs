using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Entities;

namespace ChangeTech.IDataRepository
{
    public interface ICompanyRepository
    {
        void AddCompany(Company entity);
        void DeleteCompany(Guid companyGUID);
        void UpdateCompany(Company entity);
        Company GetCompanyByGuid(Guid companyGuid);
        Company GetCompanyByCode(string code);
        IQueryable<Company> GetAll();
        IQueryable<Company> GetCompanyNotJoinProgram(Guid programGuid);
    }
}
