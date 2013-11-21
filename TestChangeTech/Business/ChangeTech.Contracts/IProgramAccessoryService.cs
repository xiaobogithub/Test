using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Models;
using ChangeTech.Entities;

namespace ChangeTech.Contracts
{
    public interface IProgramAccessoryService
    {
        AccessoryPageModel GetProgramAccessoryByProgarm(Guid programGuid, string type);
        void AddAccessroy(AccessoryPageModel pageModel);
        void UpdateAccessory(AccessoryPageModel pageModel);
        void DeleteAccessory(Guid AccessoryGuid);
        AccessoryPageModel GetAccessory(Guid accessoryGuid);
        bool IsExist(Guid programGuid, string type);
        string GetProgramAccessoryModelAsXML(Guid programGuid, Guid languageGuid);
        string GetSessionEndingPageModelAsXML(Guid programGuid, Guid languageGuid, Guid userGuid);
        string GetPinCodePageModelAsXML(Guid programGuid, Guid lanaugeGuid, Guid userGuid);
        string GetPaymentModelAsXML(Guid programGuid, Guid languageGuid, Guid userGuid);
        AccessoryTemplate CloneAccessoryTemplate(AccessoryTemplate accessoryTemplate);
        AccessoryTemplate SetDefaultGuidForAccessoryTemplate(AccessoryTemplate needSetDefaultEntity, CloneProgramParameterModel cloneParameterModel);
    }
}
