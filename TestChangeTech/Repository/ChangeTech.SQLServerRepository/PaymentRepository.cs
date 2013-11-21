using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.IDataRepository;
using ChangeTech.Entities;

namespace ChangeTech.SQLServerRepository
{
    public class PaymentRepository : RepositoryBase, IPaymentRepository
    {
        public long Insert(PaymentOrder order)
        {
            InsertEntity(order);
            return order.OrderID;
        }

        public void Update(PaymentOrder order)
        {
            UpdateEntity(order);
        }

        public PaymentOrder GetPaymentOrder(long orderid)
        {
            return GetEntities<PaymentOrder>(p => p.OrderID == orderid).FirstOrDefault();
        }

        public PaymentOrder GetPaymentOrderByTransationID(string ID)
        {
            return GetEntities<PaymentOrder>(p => p.TransactionId == ID).FirstOrDefault();
        }


        public void AddPayment(PaymentTemplate payment)
        {
            InsertEntity(payment);
        }

        public void UpdatePayment(PaymentTemplate payment)
        {
            UpdateEntity(payment);
        }

        public PaymentTemplate GetPaymentTemplateByProgramGUID(Guid programGuid)
        {
            return GetEntities<PaymentTemplate>(p => p.Program.ProgramGUID == programGuid).FirstOrDefault();
        }

        public IQueryable<PaymentOrder> GetPaymentOrderlist(Guid programGuid)
        {
            return GetEntities<PaymentOrder>(p => p.ProgramGUID == programGuid && p.IsPaid == true);
        }
    }
}
