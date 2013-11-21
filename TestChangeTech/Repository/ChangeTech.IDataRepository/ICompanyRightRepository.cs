using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Entities;

namespace ChangeTech.IDataRepository
{
    public interface ICompanyRightRepository
    {
        void Add(CompanyRight entity);
        void Update(CompanyRight entity);
        void Delete(Guid rightGuid);
        CompanyRight GetConpanyRightByGuid(Guid rightGuid);
        CompanyRight GetCompanyRightByProgramAndCompany(Guid programguid, Guid companyguid);
        IQueryable<CompanyRight> GetCompanyRightByProgram(Guid programGuid);
    }
}
