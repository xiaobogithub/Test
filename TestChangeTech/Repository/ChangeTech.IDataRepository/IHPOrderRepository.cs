using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Entities;

namespace ChangeTech.IDataRepository
{
    public interface IHPOrderRepository
    {
        void Insert(HPOrder order);
        void Update(HPOrder order);
        void Delete(Guid orderGuid);
        IQueryable<HPOrder> GetOrders();
        IQueryable<HPOrder> GetOrdersByHPResellerGuid(Guid resellerGuid);
        HPOrder GetHPOrderByOrderGuid(Guid orderGuid);
        HPOrder GetHPOrderByOrderCode(string code);
        //HPOrderParam
        IQueryable<HPOrderParam> GetHPOrderParamsByType(string type);
        HPOrderParam GetHPOrderParamByParamName(string paramName);
    }
}
