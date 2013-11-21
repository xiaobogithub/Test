using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Models;

namespace ChangeTech.Contracts
{
    public interface IHPOrderEmailService
    {
        void InsertHPOrderEmail(HPOrderModel orderModel);
        void Update(HPOrderEmailModel orderEmail);
        void Delete(Guid orderEmailGuid);
        List<HPOrderEmailModel> GetOrderEmailsByHPOrderGuid(Guid orderGuid);
        List<HPOrderEmailModel> GetOrderEmailsByCurrentDate();
        string GetHPOrderEmailBodyContent(HPOrderModel orderModel);
    }
}
