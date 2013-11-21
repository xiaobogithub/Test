using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ethos.DependencyInjection;
using ChangeTech.Contracts;
using ChangeTech.Entities;
using ChangeTech.IDataRepository;
using ChangeTech.Models;

namespace ChangeTech.Services
{
    public class PaymentService : ServiceBase, IPaymentService
    {
        public long AddPaymentOrder(ChangeTech.Models.PaymentOrderModel model)
        {
            PaymentOrder entity = new PaymentOrder();
            entity.ProgramGUID = model.ProgramGUID;
            entity.UserGUID = model.UserGUID;
            entity.Amount = model.Amount;
            entity.CurrencyCode = model.CurrencyCode;

            return Resolve<IPaymentRepository>().Insert(entity);
        }

        public void SetTransationID(long orderID, string transatihonID)
        {
            PaymentOrder orderentity = Resolve<IPaymentRepository>().GetPaymentOrder(orderID);
            orderentity.TransactionId = transatihonID;
            Resolve<IPaymentRepository>().Update(orderentity);
        }

        public void CompleteOrder(string transationID)
        {
            PaymentOrder orderentity = Resolve<IPaymentRepository>().GetPaymentOrderByTransationID(transationID);
            DateTime setCurrentTimeByTimeZone = Resolve<IProgramUserService>().GetCurrentTimeByTimeZone(orderentity.ProgramGUID.Value, orderentity.UserGUID.Value, DateTime.UtcNow);
            orderentity.PayTime = setCurrentTimeByTimeZone; //DateTime.UtcNow;
            orderentity.IsPaid = true;
            Resolve<IPaymentRepository>().Update(orderentity);

            ProgramUser pu = Resolve<IProgramUserRepository>().GetProgramUserByProgramGuidAndUserGuid(orderentity.ProgramGUID.Value, orderentity.UserGUID.Value);
            if (!pu.UserReference.IsLoaded)
            {
                pu.UserReference.Load();
            }
            pu.Status = ProgramUserStatusEnum.Registered.ToString();
            pu.User.IsPaid = true;
            Resolve<IProgramUserRepository>().Update(pu);
        }

        public PaymentTemplateModel GetCompletePaymentTip(string transationID)
        {
            PaymentOrder orderentity = Resolve<IPaymentRepository>().GetPaymentOrderByTransationID(transationID);
            PaymentTemplate temp = Resolve<IPaymentRepository>().GetPaymentTemplateByProgramGUID(orderentity.ProgramGUID.Value);
            PaymentTemplateModel model = new PaymentTemplateModel
            {
                ProgramGUID = orderentity.ProgramGUID.Value,
                UserGUID = orderentity.UserGUID.Value,
                SuccessfulTip = temp.SuccessfulTip,
                ExceptionTip = temp.ExceptionTip, 
                LoginText = temp.LoginLinkText,
                IsPaid = orderentity.IsPaid.HasValue ? orderentity.IsPaid.Value : false
            };

            return model;
        }

        public string GetPaymentExceptionTip(string transationID)
        {
            PaymentOrder orderentity = Resolve<IPaymentRepository>().GetPaymentOrderByTransationID(transationID);
            PaymentTemplate temp = Resolve<IPaymentRepository>().GetPaymentTemplateByProgramGUID(orderentity.ProgramGUID.Value);

            return temp.ExceptionTip;
        }

        public bool ShoulPayForProgram(Guid userguid, Guid programguid)
        {
            bool flag = false;
            Program programentity = Resolve<IProgramRepository>().GetProgramByGuid(programguid);
            if(programentity.IsWithPay.HasValue && programentity.IsWithPay.Value == true)
            {
                User userentity = Resolve<IUserRepository>().GetUserByGuid(userguid);
                if(userentity.IsPaid.HasValue == false || userentity.IsPaid.Value == false)
                {
                    flag = true;
                }
            }

            return flag;
        }

        public string GetProgramGUIDByTransactionID(string transactionid)
        {
            PaymentOrder orderentity = Resolve<IPaymentRepository>().GetPaymentOrderByTransationID(transactionid);

            return orderentity.ProgramGUID.ToString();
        }

        public string GetUserSecurityByTransactionID(string transactionid)
        {
            PaymentOrder orderentity = Resolve<IPaymentRepository>().GetPaymentOrderByTransationID(transactionid);
            return Resolve<IUserService>().GetUserSecrity(orderentity.UserGUID.Value);
        }

        public void AddPaymentTemplate(PaymentTemplateModel paymentTemplatemodel)
        {
            PaymentTemplate templateEntity = new PaymentTemplate
            {
                PaymentTemplateGUID = paymentTemplatemodel.PaymentTemplateGUID,
                Program = Resolve<IProgramRepository>().GetProgramByGuid(paymentTemplatemodel.ProgramGUID),
                SuccessfulTip = paymentTemplatemodel.SuccessfulTip,
                ExceptionTip = paymentTemplatemodel.ExceptionTip,
                OrderDescription = paymentTemplatemodel.OrderDescription,
                LoginLinkText = paymentTemplatemodel.LoginText
            };
            Resolve<IPaymentRepository>().AddPayment(templateEntity);
        }

        public void UpdatePaymentTemplate(PaymentTemplateModel paymenttemplatemodel)
        {
            PaymentTemplate templateEntity = Resolve<IPaymentRepository>().GetPaymentTemplateByProgramGUID(paymenttemplatemodel.ProgramGUID);
            templateEntity.OrderDescription = paymenttemplatemodel.OrderDescription;
            templateEntity.SuccessfulTip = paymenttemplatemodel.SuccessfulTip;
            templateEntity.ExceptionTip = paymenttemplatemodel.ExceptionTip;
            templateEntity.PrimaryButtonCaption = paymenttemplatemodel.PrimaryButtonCaption;
            templateEntity.Text = paymenttemplatemodel.Text;
            templateEntity.Heading = paymenttemplatemodel.Heading;
            templateEntity.LoginLinkText = paymenttemplatemodel.LoginText;

            Resolve<IPaymentRepository>().UpdatePayment(templateEntity);
        }

        public PaymentTemplateModel GetPaymentTemplate(Guid programGUID)
        {
            PaymentTemplate templateEntity = Resolve<IPaymentRepository>().GetPaymentTemplateByProgramGUID(programGUID);
            PaymentTemplateModel model = new PaymentTemplateModel();
            if(templateEntity != null)
            {
                model.ProgramGUID = programGUID;
                model.PaymentTemplateGUID = templateEntity.PaymentTemplateGUID;
                model.SuccessfulTip = templateEntity.SuccessfulTip;
                model.ExceptionTip = templateEntity.ExceptionTip;
                model.OrderDescription = templateEntity.OrderDescription;
                model.PrimaryButtonCaption = templateEntity.PrimaryButtonCaption;
                model.Heading = templateEntity.Heading;
                model.Text = templateEntity.Text;
                model.LoginText = templateEntity.LoginLinkText;
            }

            return model;
        }

        public string GetProgramLogoNameOnServer(string transactionID)
        {
            string logoName = string.Empty;
            PaymentOrder orderentity = Resolve<IPaymentRepository>().GetPaymentOrderByTransationID(transactionID);
            Program programentity = Resolve<IProgramRepository>().GetProgramByGuid(orderentity.ProgramGUID.Value);
            if(!programentity.ResourceReference.IsLoaded)
            {
                programentity.ResourceReference.Load();
            }
            if(programentity.Resource != null)
            {
                logoName = programentity.Resource.NameOnServer;
            }

            return logoName;
        }

        public bool ShouldShowLoginLink(string transactionid)
        {
            bool flag = false;
            PaymentOrder orderentity = Resolve<IPaymentRepository>().GetPaymentOrderByTransationID(transactionid);
            DateTime setCurrentTimeByTimeZone = Resolve<IProgramUserService>().GetCurrentTimeByTimeZone(orderentity.ProgramGUID.Value, orderentity.UserGUID.Value, DateTime.UtcNow);
            int result = Resolve<IProgramUserService>().IsThereClassToday(orderentity.UserGUID.Value, orderentity.ProgramGUID.Value, setCurrentTimeByTimeZone);
            if(result == 0)
            {
                flag = true;
            }

            return flag;
        }

        public int GetNumberOfPayOrder(Guid programGuid)
        {
            return Resolve<IPaymentRepository>().GetPaymentOrderlist(programGuid).Count();
        }

        public List<ManageOrderModel> GetOrderList(Guid programGuid, int pageNumber, int pageSize)
        {
            IQueryable<PaymentOrder> orderList = Resolve<IPaymentRepository>().GetPaymentOrderlist(programGuid);
            List<PaymentOrder> orderEntityList = orderList.OrderBy(o => o.PayTime).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            List<ManageOrderModel> modelList = new List<ManageOrderModel>();
            foreach(PaymentOrder order in orderEntityList)
            {
                ManageOrderModel model = new ManageOrderModel
                {
                    Amount = order.Amount.ToString(),
                    CurrencyCode = order.CurrencyCode,
                    OrderID = order.OrderID,
                    PayTime = order.PayTime.Value,
                    TransationID = order.TransactionId,
                    UserEmail = Resolve<IUserRepository>().GetUserByGuid(order.UserGUID.Value).Email
                };
                modelList.Add(model);
            }

            return modelList;
        }
    }
}
