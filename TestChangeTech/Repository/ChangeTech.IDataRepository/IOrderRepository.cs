using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Entities;

namespace ChangeTech.IDataRepository
{
    public interface IOrderRepository
    {
        void Update(Order order);
        void Insert(Order order);
        void Delete(Guid orderGuid);
        IQueryable<Order> GetOrdersByResellerGuid(Guid resellerGuid);
        Order GetOrderByOrderGuid(Guid orderGuid);
        IQueryable<OrderLicenceType> GetAllOrderTypes();
        OrderLicenceType GetOrderTypeByTypeGuid(Guid orderLicenceTypeGuid);
    }
}
