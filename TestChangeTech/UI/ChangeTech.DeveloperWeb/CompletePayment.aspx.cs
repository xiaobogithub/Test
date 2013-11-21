using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ethos.DependencyInjection;
using ChangeTech.Models;
using ChangeTech.Contracts;
using ChangeTech.Services;
using Ethos.Utility;

namespace ChangeTech.DeveloperWeb
{
    public partial class CompletePayment : PageBase<PaymentOrderModel>
    {
        private string TransationID { get { return Request.QueryString["transactionId"]; } }
        private string returnCode { get { return Request.QueryString["responseCode"]; } }
        private string merchantID { get { return Resolve<ISystemSettingService>().GetSettingValue("MerchantID"); } }
        private string token { get { return Resolve<ISystemSettingService>().GetSettingValue("Token"); } }
        private string ExceptionTipMessage { get { return Resolve<IPaymentService>().GetPaymentExceptionTip(TransationID); } }

        protected void Page_Load(object sender, EventArgs e)
        {
            string result = string.Empty;
            if (!IsPostBack)
            {
                try
                {
                    PaymentTemplateModel paymodel = Resolve<IPaymentService>().GetCompletePaymentTip(TransationID);
                    if (returnCode.Equals("OK"))
                    {
                        EPaymentService.NetaxeptClient client = new ChangeTech.DeveloperWeb.EPaymentService.NetaxeptClient();
                        var saleResponse = client.Process(merchantID, token, new EPaymentService.ProcessRequest
                        {
                            TransactionId = TransationID,
                            Operation = "Sale"
                        });
                        if (saleResponse.ResponseCode.Equals("OK"))
                        {
                            Resolve<IPaymentService>().CompleteOrder(TransationID);

                            result = paymodel.SuccessfulTip;
                            if (Resolve<IPaymentService>().ShouldShowLoginLink(TransationID))
                            {
                                loginHyperLink.Visible = true;
                                loginHyperLink.NavigateUrl = GetLoginURL(TransationID);
                                loginHyperLink.Text = paymodel.LoginText;
                            }

                            Resolve<IEmailService>().SendRegisterEmail(paymodel.UserGUID, paymodel.ProgramGUID);
                        }
                        else
                        {
                            result = ExceptionTipMessage;
                            //ClientScript.RegisterStartupScript(this.GetType(), "delay", "function delayer(){}", true);
                        }
                    }
                    else
                    {
                        result = ExceptionTipMessage;
                        //ClientScript.RegisterStartupScript(this.GetType(), "delay", "function delayer(){}", true);
                    }
                    if (Resolve<IProgramService>().IsProgramCTPPEnable(paymodel.ProgramGUID))
                    {
                        string transationId = StringUtility.MD5Encrypt(TransationID, Constants.MD5_KEY);
                        Response.Redirect(string.Format("CTPP.aspx?{0}={1}", Constants.QUERYSTR_PAYMENT_TRANSATION_ID, transationId), false);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    result = ExceptionTipMessage + ex.Message;
                }

                resultLabel.Text = result == null ? string.Empty : result.Replace("\r\n", "<br />");
                LoadProgramLogo();
            }
        }

        private string GetLoginURL(string TransationID)
        {
            string loginURL = Resolve<ISystemSettingService>().GetSettingValue("WebServerPath");
            return string.Format("{0}ChangeTech.html?Mode=Live&{1}={2}&{3}={4}", loginURL, Constants.QUERYSTR_PROGRAM_GUID, Resolve<IPaymentService>().GetProgramGUIDByTransactionID(TransationID), Constants.QUERYSTR_SECURITY, Resolve<IPaymentService>().GetUserSecurityByTransactionID(TransationID));
        }

        private void LoadProgramLogo()
        {
            string logoName = Resolve<IPaymentService>().GetProgramLogoNameOnServer(TransationID);
            string accountName = Resolve<ISystemSettingService>().GetSettingValue("AzureStorageAccountName");
            string bolbPath = ServiceUtility.GetBlobPath(accountName);
            if (!string.IsNullOrEmpty(logoName))
            {
                LogoImage.ImageUrl = bolbPath + BlobContainerTypeEnum.LogoContainer.ToString().ToLower() + "/" + logoName;
            }
        }
    }
}
