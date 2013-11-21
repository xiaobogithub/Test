using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Models;

namespace ChangeTech.Contracts
{
    public interface IOrderLicenceService
    {
        void InsertOrderLicence(OrderLicenceModel orderLicenceModel);
        void UpdateOrderLicence(OrderLicenceModel orderLicenceModel);
        int GetUsedOrderLicencesByResellerGuidAndOrderContentGuid(Guid resellerGuid, Guid orderContentGuid);
        OrderLicenceModel GetOrderLicenceByOrderContentGuid(Guid orderContentGuid);
        OrderLicenceModel GetOrderLicenceByProgramUserGuid(Guid programUserGuid);
        OrderLicenceModel GetOrderLicenceByOrderContentGuidAndProgramUserGuid(Guid orderContentGuid,Guid puGuid);
        List<OrderLicenceModel> GetUsedOrderLicencesByOrderContentGuidAndProgramGuid(Guid orderContentGuid, Guid programGuid);
        OrderLicenceModel GetLastRegistedUserByOrderContentGuidAndProgramGuid(Guid orderContentGuid, Guid programGuid);
        ValidateOrderLicenceResponseModel ValidateOrderLicence(Guid orderContentGuid, Guid programGuid);
    }
}
