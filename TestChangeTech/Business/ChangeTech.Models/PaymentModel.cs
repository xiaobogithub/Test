using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChangeTech.Models
{
    public class PaymentOrderModel
    {
        public long OrderID { get; set; }
        public Guid UserGUID { get; set; }
        public Guid ProgramGUID { get; set; }
        public decimal Amount { get; set; }
        public string CurrencyCode { get; set; }
        public string TransationID { get; set; }
    }

    public class PaymentTemplateModel
    {
        public Guid PaymentTemplateGUID { get; set; }
        public Guid ProgramGUID { get; set; }
        public Guid UserGUID { get; set; }
        public string Heading { get; set; }
        public string Text { get; set; }
        public string OrderDescription { get; set; }
        public string SuccessfulTip { get; set; }
        public string ExceptionTip { get; set; }
        public string PrimaryButtonCaption { get; set; }
        public string LoginText { get; set; }
        public bool IsPaid { get; set; }
    }

    public class ManageOrderModel
    {
        public long OrderID { get; set; }
        public string UserEmail { get; set; }
        public string Amount { get; set; }
        public string CurrencyCode { get; set; }
        public string TransationID { get; set; }
        public DateTime PayTime { get; set; }
    }
}
