using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ChangeTech.Contracts;
using Ethos.DependencyInjection;
using ChangeTech.Models;
using System.Web.Security;

namespace ChangeTech.DeveloperWeb
{
    public partial class _rdParty : System.Web.UI.MasterPage
    {
        public string VersionNumber
        {
            get
            {
                string version = System.Configuration.ConfigurationManager.AppSettings["ProjectVersion"];
                return version;
            }
        }
        public bool ManagePermission
        {
            get
            {
                if (ContextService.CurrentAccount != null)
                {
                    return ((PermissionEnum)ContextService.CurrentAccount.Security & PermissionEnum.ApplicationAdmin) == PermissionEnum.ApplicationAdmin;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool SuperAdminPermission
        {
            get
            {
                if (ContextService.CurrentAccount != null)
                {
                    return ((PermissionEnum)ContextService.CurrentAccount.Security & PermissionEnum.ApplicationSuperAdmin) == PermissionEnum.ApplicationSuperAdmin;
                }
                else
                {
                    return false;
                }
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
                        Response.Redirect(Constants.URL_HPSYSTEM_MAINPAGE);
                        break;
                    default:
                        //Don't know why redirect to http://changetech.no
                        Response.Redirect(Constants.URL_CHANGETECH);
                        break;
                }
            }
            else
            {
                RedirectToLoginPage();
            }

            if (!IsPostBack)
            {
                footerLocalize.Text = string.Format(Convert.ToString(GetGlobalResourceObject("Share", "Footer")), DateTime.UtcNow.Year);
            }
        }

        protected void logoutLnkBtn_Click(object sender, EventArgs e)
        {
            RedirectToLoginPage();
        }

        private void RedirectToLoginPage()
        {
            FormsAuthentication.RedirectToLoginPage();
            FormsAuthentication.SignOut();
            ContextService.ClearAccount();
            Response.End();
        }
    }
}
