using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Models;

namespace ChangeTech.Contracts
{
    public interface IOrderService
    {
        void InsertOrder(OrderModel orderModel);
        List<OrderModel> GetOrdersByResellerGuid(Guid resellerGuid);
        OrderModel GetOrderByOrderGuid(Guid orderGuid);
        void UpdateOrder(OrderModel orderModel);
        OrderLicenceTypeModel GetOrderLicenceTypeByTypeGuid(Guid orderTypeGuid);
        List<OrderLicenceTypeModel> GetAllOrderLicenceTypes();
    }
}
