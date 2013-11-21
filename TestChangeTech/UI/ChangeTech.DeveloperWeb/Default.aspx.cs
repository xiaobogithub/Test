using System;
using System.Web;
using System.Web.Security;
using System.Web.UI.WebControls;
using ChangeTech.Contracts;
using ChangeTech.Models;
using Ethos.DependencyInjection;
using Ethos.Utility;
using System.IO;
using System.Net;

namespace ChangeTech.DeveloperWeb
{
    public partial class Default : PageBase<ModelBase>
    {
        #region VersionNumber
        public string VersionNumber
        {
            get
            {
                string version = System.Configuration.ConfigurationManager.AppSettings["ProjectVersion"];
                return version;
            }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                if (ContextService.CurrentAccount != null &&
                    ContextService.CurrentAccount.UserType == UserTypeEnum.User)
                {
                    ContextService.ClearAccount();
                    FormsAuthentication.SignOut();
                    Response.Redirect(Request.Url.AbsoluteUri);
                    return;
                }
                else
                {
                    Response.Redirect(FormsAuthentication.DefaultUrl);
                }
            }

            //Resolve<IUserService>().GetUserStatistics(new Guid("D44F484C-2DA4-4A86-99AC-77E2C3CACC13"));
            //Response.Write("Finish");
        }

        protected void ChangeTechLogin_Authenticate(object sender, AuthenticateEventArgs e)
        {
            try
            {
                //if (HttpContext.Current.Session["CurrentAccount"] == null && ContextService.CurrentAccount == null)
                ContextService.CurrentAccount = Resolve<IUserService>().Logon(ChangeTechLogin.UserName.Trim(), ChangeTechLogin.Password.Trim(), Guid.Empty);
                UserModel um = ContextService.CurrentAccount;
                if (um != null && um.UserType != UserTypeEnum.Tester && um.UserType != UserTypeEnum.User)
                {
                    if (um.IsSMSLogin)
                    {
                        if (HttpContext.Current.Session["SMSCode"] == null)
                        {
                            HttpContext.Current.Session["SMSCode"] = GenerateSMSCode();
                            bool flag = SendLoginSM(um, HttpContext.Current.Session["SMSCode"].ToString());
                            sendSMS.Visible = true;
                            ChangeTechLogin.Visible = false;
                            HttpContext.Current.Session["AuthenticateEventArgs"] = e;
                        }
                        else if (txtSMSCode.Value == HttpContext.Current.Session["SMSCode"].ToString())
                        {
                            Authenticated(e, um);
                        }
                    }
                    else
                    {
                        Authenticated(e, um);
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }
        }

        private static bool SendLoginSM(UserModel um, string message)
        {
            IContainerContext containerContext = ContainerManager.GetContainer("container");
            string user = containerContext.Resolve<ISystemSettingService>().GetSettingValue("sUser");
            string pass = containerContext.Resolve<ISystemSettingService>().GetSettingValue("sPass");
            string Originator = containerContext.Resolve<ISystemSettingService>().GetSettingValue("sOriginator");
            GetMessageModel getMessageModel = new GetMessageModel
            {
                sMobileNumber = um.PhoneNumber,
                sMessage = "ChangeTech Login Code:" + message,
                sOrigrinator = Originator,
                sPass = pass,
                sUser = user,
                sForeignID = "0"
            };
            bool flag = false;
            flag = containerContext.Resolve<IShortMessageService>().SendSM(getMessageModel);
            if (flag)
            {
                // add log
                InsertLogModel insertLogModel = new InsertLogModel
                {
                    ActivityLogType = (int)LogTypeEnum.SendPinCodeSM,
                    Message = "Send LoginCode Sucessful",
                    UserGuid = um.UserGuid
                };
                containerContext.Resolve<IActivityLogService>().Insert(insertLogModel);
            }
            return flag;
        }

        private void Authenticated(AuthenticateEventArgs e, UserModel um)
        {
            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                1,
                um.UserName,
                DateTime.UtcNow, DateTime.UtcNow.AddMinutes(2880),
                ChangeTechLogin.RememberMeSet,
                um.Security.ToString());
            string encyptedTicket = FormsAuthentication.Encrypt(authTicket);
            HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encyptedTicket);
            Response.Cookies.Add(authCookie);

            e.Authenticated = true;
            ContextService.CurrentAccount = um;
        }

        protected void btnValidateSMS_Click(object sender, EventArgs e)
        {
            UserModel um = ContextService.CurrentAccount;
            if (um != null && um.UserType != UserTypeEnum.Tester && um.UserType != UserTypeEnum.User)
            {
                if (um.IsSMSLogin)
                {
                    if (txtSMSCode.Value == HttpContext.Current.Session["SMSCode"].ToString())
                    {
                        AuthenticateEventArgs eventArgs = (AuthenticateEventArgs)HttpContext.Current.Session["AuthenticateEventArgs"];
                        Authenticated(eventArgs, um);
                        Response.Redirect(FormsAuthentication.DefaultUrl);
                    }
                    else
                    {
                        errorMessageDiv.Visible = true;
                        txtSMSCode.Value = string.Empty;
                    }
                }
            }
        }

        private string GenerateSMSCode()
        {
            int strLen = 4;
            string randomString = null;

            // Instantiate random number generator using system-supplied value as seed.
            Random rand = new Random();
            // Generate and display random byte (integer) values.
            byte[] bytes = new byte[strLen - 1];
            rand.NextBytes(bytes);
            // Generate and display random integers between 0 and 9.//
            for (int ctr = 0; ctr < strLen; ctr++)
                randomString += rand.Next(10).ToString();

            //randomString += (char)rand.Next(33, 125);

            return randomString;
        }

        protected void btnRetrieve_Click(object sender, EventArgs e)
        {
            try
            {
                Label PwdSendLabel = (Label)Page.FindControl("PwdSendLabel");
                Label LoginErrorLabel = (Label)Page.FindControl("LoginErrorLabel");
                if (Resolve<IUserService>().IsValidUserRetrievePassword(txtUserEmail.Text, txtMobilePhone.Text, Guid.Empty))
                {
                    Resolve<IEmailService>().SendForgetPasswordEmail(txtUserEmail.Text, Guid.Empty);
                    PwdSendLabel.Text = "Password is sent to your E-mail address.";
                    PwdSendLabel.Visible = true;
                    LoginErrorLabel.Visible = false;
                }
                else
                {
                    //Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "alert('The information your typed are wrong!');", true);
                    //MsgLbl.Text = "We cannot find the registered user according what you input, please check whether you have provider correct information.";
                    LoginErrorLabel.Text = "We cannot find the registered user according what you input, please check whether you have provider correct information.";
                    LoginErrorLabel.Visible = true;
                    PwdSendLabel.Visible = false;

                }
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }
    }
}
