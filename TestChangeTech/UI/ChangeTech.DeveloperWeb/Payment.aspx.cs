using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ChangeTech.Models;
using Ethos.DependencyInjection;
using ChangeTech.Contracts;

namespace ChangeTech.DeveloperWeb
{
    public partial class Payment : PageBase<PaymentOrderModel>
    {
        private string ProgramGUID { get { return Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]; } }
        private string UserGUID { get { return Request.QueryString[Constants.QUERYSTR_USER_GUID]; } }

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(ProgramGUID) && !string.IsNullOrEmpty(UserGUID))
            {
                if(!IsPostBack)
                {
                    ProgramPropertyModel ppmodel = Resolve<IProgramService>().GetProgramProperty(new Guid(ProgramGUID));
                    PaymentOrderModel pomodel = new PaymentOrderModel
                    {
                        ProgramGUID = new Guid(ProgramGUID),
                        UserGUID = new Guid(UserGUID),
                        Amount = Convert.ToDecimal(ppmodel.Price),
                        CurrencyCode = "NOK"
                    };
                    long orderid = Resolve<IPaymentService>().AddPaymentOrder(pomodel);
                    Label1.Text = ppmodel.Price;

                    string merchantID = Resolve<ISystemSettingService>().GetSettingValue("MerchantID");
                    string token = Resolve<ISystemSettingService>().GetSettingValue("Token");
                    string orderdescription = GetProgramOrderDescription(ProgramGUID);
                    EPaymentService.NetaxeptClient client = new ChangeTech.DeveloperWeb.EPaymentService.NetaxeptClient();
                    var response = client.Register(merchantID, token, new EPaymentService.RegisterRequest
                    {
                        Terminal = new ChangeTech.DeveloperWeb.EPaymentService.Terminal
                        {
                            OrderDescription = orderdescription,
                            RedirectUrl = Resolve<ISystemSettingService>().GetSettingValue("WebServerPath") + "CompletePayment.aspx"
                        },
                        Environment = new ChangeTech.DeveloperWeb.EPaymentService.Environment
                        {
                            WebServicePlatform = "WCF"
                        },
                        Order = new ChangeTech.DeveloperWeb.EPaymentService.Order
                        {
                            Amount = pomodel.Amount.ToString(),
                            CurrencyCode = pomodel.CurrencyCode,
                            OrderNumber = orderid.ToString()
                        }
                    });

                    Label2.Text = response.TransactionId;
                    //paybutton.PostBackUrl = string.Format(Resolve<ISystemSettingService>().GetSettingValue("CollectCardPage") + "?merchantId={0}&transactionId={1}", merchantID, response.TransactionId);
                    Resolve<IPaymentService>().SetTransationID(orderid, response.TransactionId);
                    Response.Redirect(string.Format(Resolve<ISystemSettingService>().GetSettingValue("CollectCardPage") + "?merchantId={0}&transactionId={1}", merchantID, response.TransactionId));
                }
            }
        }

        private string GetProgramOrderDescription(string ProgramGUID)
        {
            string description = string.Empty;
            PaymentTemplateModel payModel = Resolve<IPaymentService>().GetPaymentTemplate(new Guid(ProgramGUID));
            if(payModel != null)
            {
                description = payModel.OrderDescription;
            }

            return description;
        }
    }
}
