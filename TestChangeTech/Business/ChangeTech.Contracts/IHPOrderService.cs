using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Models;

namespace ChangeTech.Contracts
{
    public interface IHPOrderService
    {
        void InsertHPOrder(HPOrderModel order);
        void UpdateHPOrder(HPOrderModel order);
        List<HPOrderModel> GetHPOrders();
        HPOrderModel GetHPOrderModelByHPOrderGuid(Guid hpOrderGuid);
        HPOrderModel GetHPOrderModelByCode(string code);
        string GetHPProgramLink(HPOrderModel orderModel);
        //HPOrderParam
        List<HPOrderParamModel> GetHPOrderParamsByType(string type);
        HPOrderParamModel GetHPOrderParamByParamName(string paramName);
    }
}
