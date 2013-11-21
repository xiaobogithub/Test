using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Models;

namespace ChangeTech.Contracts
{
    public interface ICTPPService
    {
        CTPPModel GetCTPP(Guid programGUID);
        void UpdateCTPP(CTPPModel Model, bool isSetPresenter);
        Guid AddCTPPTemplate(CTPPModel Model);
        CTPPModel GetCTPPByBrandAndProgram(string brandUrl, string programUrl);
        List<CTPPModel> GetCTPPInBrandNotThisProgram(string brandName, string programName);

        List<string> GetSessionResource(Guid sessionGUID);
        string GetCTPPModelAsXML(Guid programGuid);
        string GetCTPPModelAsXMLByProgramGuidAndUserGuid(Guid programGuid, Guid userGuid);

        bool IsExistCTPPWithBrandUrlAndProgramUrl(string brandUrl, string programUrl);
        bool BindCTPPRelapse(Guid CTPPGuid, Guid RelapseGuid, CTPPRelapseEnum relapseType);
        bool UnBindCTPPRelapse(Guid CTPPGuid, CTPPRelapseEnum relapseType);

        //the services provider for ctpp
        List<string> GetSessionResource(Guid sessionGUID, string serverPath, List<CTPPSessionPageBodyModel> sPageBodyList, List<CTPPSessionPageMediaResourceModel> sPageMediaResourceList);
    }
}
