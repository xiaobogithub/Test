using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Entities;

namespace ChangeTech.IDataRepository
{
    public interface IPaymentRepository
    {
        long Insert(PaymentOrder order);
        void Update(PaymentOrder order);
        PaymentOrder GetPaymentOrder(long orderID);
        PaymentOrder GetPaymentOrderByTransationID(string ID);
        void AddPayment(PaymentTemplate payment);
        void UpdatePayment(PaymentTemplate payment);
        PaymentTemplate GetPaymentTemplateByProgramGUID(Guid programGuid);
        IQueryable<PaymentOrder> GetPaymentOrderlist(Guid programGuid);
    }
}
