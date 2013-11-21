using System;
using System.Collections.Generic;
using System.Data;
using ChangeTech.Models;

namespace ChangeTech.Contracts
{
    public interface IReportService
    {
        ReportModel GetFormsData(Guid programGuid, Guid usergroupGuid, string gender, bool containOnCompany);
        List<ReportItem> GetReportItems(Guid programGuid, Guid companyGuid, string gender, bool containOnCompany);
        DataTable GetActivityLogReport(string emaillistStr, string fileds, string days, string programGuid);
        List<ProgramUserReportModel> GetProgramUserReport();
    }
}
