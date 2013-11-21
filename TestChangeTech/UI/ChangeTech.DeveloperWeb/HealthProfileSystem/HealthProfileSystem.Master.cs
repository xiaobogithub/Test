using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using ChangeTech.Models;

namespace ChangeTech.DeveloperWeb.HealthProfileSystem
{
    public partial class HealthProfileSystem : System.Web.UI.MasterPage
    {
        public string VersionNumber
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["ProjectVersion"];
            }
        }
        protected void Page_Init(object sender, EventArgs e)
        {
            UserModel currentAccount = ContextService.CurrentAccount;
            //if (ContextService.CurrentAccount != null)
            if (currentAccount != null
                && currentAccount.UserType != UserTypeEnum.User
                && currentAccount.UserType != UserTypeEnum.Tester
                && currentAccount.UserType != UserTypeEnum.Customer
                && currentAccount.UserType != UserTypeEnum.ProjectManager
                && currentAccount.UserType != UserTypeEnum.ProgramEditor)
            {
                switch (ContextService.CurrentAccount.UserType)
                {
                    case UserTypeEnum.Administrator:
                    case UserTypeEnum.ProgramAdministrator:
                        Response.Redirect(Constants.URL_DEVELOPER_HOME);
                        break;
                    case UserTypeEnum.SupportPersonell:
                        Response.Redirect(Constants.URL_MAINTENANCE_HOME);
                        break;
                    case UserTypeEnum.Translator:
                        Response.Redirect(Constants.URL_TRANSLATOR_HOME);
                        break;
                    case UserTypeEnum.Monitor:
                        Response.Redirect(Constants.URL_MONITOR_HOME);
                        break;
                    case UserTypeEnum.Reseller:
                        Response.Redirect(Constants.URL_ORDERSYSTEM_HOME);
                        break;
                    case UserTypeEnum.HPReseller:
                        break;
                    default:
                        //Don't know why redirect to http://changetech.no
                        Response.Redirect(Constants.URL_CHANGETECH);
                        break;
                }
            }
            else
            {
                FormsAuthentication.RedirectToLoginPage();
                FormsAuthentication.SignOut();
                ContextService.ClearAccount();
                Response.End();
            }

            if (!IsPostBack)
            {
                footerLocalize.Text = string.Format(Convert.ToString(GetGlobalResourceObject("Share", "Footer")), DateTime.UtcNow.Year);
            }
        }

        protected void logoutLnkBtn_Click(object sender, EventArgs e)
        {
            FormsAuthentication.RedirectToLoginPage();
            FormsAuthentication.SignOut();
            ContextService.ClearAccount();
            Response.End();
        }
    }
}