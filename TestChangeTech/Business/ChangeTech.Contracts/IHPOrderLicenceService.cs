using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Models;

namespace ChangeTech.Contracts
{
    public interface IHPOrderLicenceService
    {
        void Insert(HPOrderLicenceModel hpOrderLicenceModel);
        void Update(HPOrderLicenceModel hpOrderLicenceModel);
        void Delete(Guid hpOrderLicenceGuid);
        void DeleteEntityByProgramUserGuid(Guid programUserGuid);
        HPOrderLicenceModel GetOrderLicenceByOrderLicenceGuid(Guid hpOrderLicenceGuid);
        HPOrderLicenceModel GetOrderLicenceByProgramUserGuid(Guid programUserGuid);
        int GetUsedOrderLicencesByResellerGuidAndHPOrderGuid(Guid hpOrderGuid);
        ValidateOrderLicenceResponseModel ValidateHPOrderLicence(Guid orderGuid, Guid programGuid);
    }
}
