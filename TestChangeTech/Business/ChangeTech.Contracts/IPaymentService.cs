using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Models;

namespace ChangeTech.Contracts
{
    public interface IPaymentService
    {
        long AddPaymentOrder(PaymentOrderModel model);
        void SetTransationID(long orderID, string transatihonID);
        void CompleteOrder(string transationID);
        bool ShoulPayForProgram(Guid userguid, Guid programguid);
        string GetProgramGUIDByTransactionID(string transactionid);
        string GetUserSecurityByTransactionID(string transactionid);
        PaymentTemplateModel GetCompletePaymentTip(string transationID);
        string GetPaymentExceptionTip(string transationID);
        void AddPaymentTemplate(PaymentTemplateModel paymentTemplatemodel);
        void UpdatePaymentTemplate(PaymentTemplateModel paymenttemplatemodel);
        PaymentTemplateModel GetPaymentTemplate(Guid programGUID);
        string GetProgramLogoNameOnServer(string transactionID);
        bool ShouldShowLoginLink(string transactionid);
        int GetNumberOfPayOrder(Guid programGuid);
        List<ManageOrderModel> GetOrderList(Guid programGuid, int pageNumber, int pageSize);
    }
}
