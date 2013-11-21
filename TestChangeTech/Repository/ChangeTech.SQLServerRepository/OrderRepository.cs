using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.IDataRepository;
using ChangeTech.Entities;

namespace ChangeTech.SQLServerRepository
{
    public class OrderRepository : RepositoryBase,IOrderRepository
    {
        public void Insert(Order order)
        {
            InsertEntity(order);
        }

        public void Delete(Guid orderGuid)
        {
            DeleteEntity<Order>(o => o.OrderGUID == orderGuid, new Guid());
        }

        public void Update(Order order)
        {
            UpdateEntity(order);
        }

        public IQueryable<Order> GetOrdersByResellerGuid(Guid resellerGuid)
        {
            return GetEntities<Order>().Where(o => (!o.IsDeleted.HasValue || o.IsDeleted.HasValue && o.IsDeleted.Value == false) && o.UpdatedBy == resellerGuid).OrderByDescending(o => o.Created);
        }

        public Order GetOrderByOrderGuid(Guid orderGuid)
        {
            return GetEntities<Order>(o => (!o.IsDeleted.HasValue || o.IsDeleted.HasValue && o.IsDeleted.Value == false) && o.OrderGUID == orderGuid).FirstOrDefault();
        }

        public IQueryable<OrderLicenceType> GetAllOrderTypes()
        {
            return GetEntities<OrderLicenceType>().Where(olt => (!olt.IsDeleted.HasValue || olt.IsDeleted.HasValue && olt.IsDeleted.Value == false));
        }
        public OrderLicenceType GetOrderTypeByTypeGuid(Guid orderLicenceTypeGuid)
        {
            return GetEntities<OrderLicenceType>().Where(olt => olt.OrderLicenceTypeGUID == orderLicenceTypeGuid && (!olt.IsDeleted.HasValue || olt.IsDeleted.HasValue && olt.IsDeleted.Value == false)).FirstOrDefault();
        }
    }
}
