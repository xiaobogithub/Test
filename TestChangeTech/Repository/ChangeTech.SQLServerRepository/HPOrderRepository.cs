using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Entities;
using ChangeTech.IDataRepository;

namespace ChangeTech.SQLServerRepository
{
    public class HPOrderRepository : RepositoryBase, IHPOrderRepository
    {

        public void Insert(HPOrder order)
        {
            InsertEntity(order);
        }

        public void Update(HPOrder order)
        {
            UpdateEntity(order);
        }

        public void Delete(Guid orderGuid)
        {
            DeleteEntity<HPOrder>(o => o.HPOrderGUID == orderGuid, new Guid());
        }

        public IQueryable<HPOrder> GetOrdersByHPResellerGuid(Guid resellerGuid)
        {
            return GetEntities<HPOrder>().Where(o => (!o.IsDeleted.HasValue || o.IsDeleted.HasValue && o.IsDeleted.Value == false) && o.UpdatedBy == resellerGuid);
        }

        public IQueryable<HPOrder> GetOrders()
        {
            return GetEntities<HPOrder>().Where(o => (!o.IsDeleted.HasValue || o.IsDeleted.HasValue && o.IsDeleted.Value == false));
        }

        public HPOrder GetHPOrderByOrderGuid(Guid orderGuid)
        {
            return GetEntities<HPOrder>().Where(o => (!o.IsDeleted.HasValue || o.IsDeleted.HasValue && o.IsDeleted.Value == false) && o.HPOrderGUID == orderGuid).FirstOrDefault();
        }

        public IQueryable<HPOrderParam> GetHPOrderParamsByType(string type)
        {
            return GetEntities<HPOrderParam>().Where(op => op.HPOrderParamType == type);
        }

        public HPOrderParam GetHPOrderParamByParamName(string paramName)
        {
            return GetEntities<HPOrderParam>().Where(op => op.HPOrderParamName == paramName).FirstOrDefault();
        }

        public HPOrder GetHPOrderByOrderCode(string code)
        {
            return GetEntities<HPOrder>().Where(o => o.Code == code).FirstOrDefault();
        }
    }
}
