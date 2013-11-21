using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Models;

namespace ChangeTech.Contracts
{
    public interface ICompanyService
    {
        List<CompanyRightModel> GetCompanyRightListByProgram(Guid programGuid);
        List<CompanyModel> GetCompanyListNotJoinProgram(Guid programGuid);
        void AddCompany(CompanyModel model);
        void AddCompanyRight(CompanyRightModel rightmodel);
        CompanyRightModel GetCompanyRightModelByGuid(Guid companyrightguid);
        void UpdateCompanyRightOverdueTime(DateTime time, Guid companyguid);
        void DeleteCompanyRight(Guid rightguid);
        bool ValidateCompanyRight(Guid programguid, Guid companyguid);
        void AddCompany(CompanyRightModel model);
        void UpdateCompanyRight(CompanyRightModel model);
        int GetCompanyCountByProgram(Guid programGuid);
        List<CompanyRightModel> GetCompanyRightListByProgram(Guid programGuid, int currentPage, int pageSize);

        void SetCompanyCodeForCompany(Guid companyGuid);
        Guid GetCompanyGuidByCode(string code);
    }
}
