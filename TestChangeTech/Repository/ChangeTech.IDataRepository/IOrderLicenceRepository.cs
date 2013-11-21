using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Entities;

namespace ChangeTech.IDataRepository
{
    public interface IOrderLicenceRepository
    {
        void Insert(OrderLicence orderLicenceEntity);
        void Update(OrderLicence orderLicenceEntity);
        void Delete(Guid orderLicenceGuid);
        void DeleteEntityByProgramUserGuid(Guid programUserGuid);
        OrderLicence GetOrderLicenceByOrderLicenceGuid(Guid orderLicenceGuid);
        OrderLicence GetOrderLicenceByOrderContentGuid(Guid orderContentGuid);
        OrderLicence GetOrderLicenceByProgramUserGuid(Guid programUserGuid);
        OrderLicence GetOrderLicenceByOrderContentGuidAndProgramUserGuid(Guid orderContentGuid, Guid puGuid);
        IQueryable<OrderLicence> GetUsedOrderLicencesByResellerGuidAndOrderContentGuid(Guid resellerGuid, Guid orderContentGuid);
        IQueryable<OrderLicence> GetUsedOrderLicencesByOrderContentGuidAndProgramGuid(Guid resellerGuid, Guid programGuid);
        OrderLicence GetLastRegistedUserByOrderContentGuidAndProgramGuid(Guid orderContentGuid, Guid programGuid);
    }
}
