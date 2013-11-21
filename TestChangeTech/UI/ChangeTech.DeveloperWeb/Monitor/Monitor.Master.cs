using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using ChangeTech.Contracts;
using Ethos.DependencyInjection;
using ChangeTech.Models;

namespace ChangeTech.DeveloperWeb.Monitor
{
    public partial class Monitor : System.Web.UI.MasterPage
    {
        private static int lastSelectOperate;
        public string VersionNumber
        {
            get
            {
                string version = System.Configuration.ConfigurationManager.AppSettings["ProjectVersion"];
                return version;
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
                FormsAuthentication.RedirectToLoginPage();
                FormsAuthentication.SignOut();
                ContextService.ClearAccount();
                Response.End();
            }

            if (!IsPostBack)
            {
                InitOperateDDL();
                footerLocalize.Text = string.Format(Convert.ToString(GetGlobalResourceObject("Share", "Footer")), DateTime.UtcNow.Year);
            }
        }

        private void InitOperateDDL()
        {
            List<OperateModel> operateList = new List<OperateModel>();
            DropDownList ddlist = ((DropDownList)menuLoginView.FindControl("OperateDDL"));

            operateList.Add(new OperateModel { Name = "--Please select--", Value = "" });
            operateList.Add(new OperateModel { Name = "· Log", Value = "" });
            operateList.Add(new OperateModel { Name = "--User Log", Value = Constants.URL_Monitor_LIST_USERLOG });
            operateList.Add(new OperateModel { Name = "--Inactive User", Value = Constants.URL_Monitor_LIST_INACTIVEUSER });
            operateList.Add(new OperateModel { Name = "--Login User", Value = Constants.URL_Monitor_LIST_LOGINUSER });
            operateList.Add(new OperateModel { Name = "--Registered User", Value = Constants.URL_Monitor_LIST_REGISTEREDUSER });
            operateList.Add(new OperateModel { Name = "--Missed Class User", Value = Constants.URL_Monitor_LIST_MISSEDCLASSUSER });
            //operateList.Add(new OperateModel { Name = "CTD Log", Value = "" });
            operateList.Add(new OperateModel { Name = "· CTD Log", Value = Constants.URL_Monitor_LIST_CTDLOG });
            //operateList.Add(new OperateModel { Name = "Error Log", Value ="" });
            operateList.Add(new OperateModel { Name = "· Error Log", Value = Constants.URL_Monitor_LIST_ERRORLOG });
            //operateList.Add(new OperateModel { Name = "System Log", Value = "" });
            operateList.Add(new OperateModel { Name = "· System Log", Value = Constants.URL_Monitor_LIST_SYSTEMLOG });
            //operateList.Add(new OperateModel { Name = "SQL Azure", Value = "" });
            operateList.Add(new OperateModel { Name = "· SQL Azure", Value = Constants.URL_Monitor_LIST_SQLAZURE });
            //operateList.Add(new OperateModel { Name = "Setting", Value = "" });
            operateList.Add(new OperateModel { Name = "· Log Type", Value = Constants.URL_Monitor_LIST_LOGTYPE });
            if (ddlist != null)
            {
                ddlist.DataSource = operateList;
                ddlist.DataTextField = "Name";
                ddlist.DataValueField = "Value";
                ddlist.DataBind();
            }

            #region Selected oprate
            if (Request.PhysicalPath.Equals(Server.MapPath(Constants.URL_Monitor_LIST_USERLOG)))
            {
                ddlist.SelectedIndex = 2;
            }
            else if (Request.PhysicalPath.Equals(Server.MapPath(Constants.URL_Monitor_LIST_INACTIVEUSER)))
            {
                ddlist.SelectedIndex = 3;
            }
            else if (Request.PhysicalPath.Equals(Server.MapPath(Constants.URL_Monitor_LIST_LOGINUSER)))
            {
                ddlist.SelectedIndex = 4;
            }
            else if (Request.PhysicalPath.Equals(Server.MapPath(Constants.URL_Monitor_LIST_REGISTEREDUSER)))
            {
                ddlist.SelectedIndex = 5;
            }
            else if (Request.PhysicalPath.Equals(Server.MapPath(Constants.URL_Monitor_LIST_MISSEDCLASSUSER)))
            {
                ddlist.SelectedIndex = 6;
            }
            else if (Request.PhysicalPath.Equals(Server.MapPath(Constants.URL_Monitor_LIST_CTDLOG)))
            {
                ddlist.SelectedIndex = 7;
            }
            else if (Request.PhysicalPath.Equals(Server.MapPath(Constants.URL_Monitor_LIST_ERRORLOG)))
            {
                ddlist.SelectedIndex = 8;
            }
            else if (Request.PhysicalPath.Equals(Server.MapPath(Constants.URL_Monitor_LIST_SYSTEMLOG)))
            {
                ddlist.SelectedIndex = 9;
            }
            else if (Request.PhysicalPath.Equals(Server.MapPath(Constants.URL_Monitor_LIST_SQLAZURE)))
            {
                ddlist.SelectedIndex = 10;
            }
            else if (Request.PhysicalPath.Equals(Server.MapPath(Constants.URL_Monitor_LIST_LOGTYPE)))
            {
                ddlist.SelectedIndex = 11;
            }
            #endregion
        }

        protected void OperateDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList operateDDL = ((DropDownList)sender);
            if (operateDDL == null) operateDDL = ((DropDownList)menuLoginView.FindControl("OperateDDL"));
            if (operateDDL != null)
            {
                lastSelectOperate = operateDDL.SelectedIndex;
                Response.Redirect(operateDDL.SelectedValue);
            }
        }

        protected void logoutLnkBtn_Click(object sender, EventArgs e)
        {
            FormsAuthentication.RedirectToLoginPage();
            FormsAuthentication.SignOut();
            ContextService.ClearAccount();
            Response.End();
        }

        protected void GoOperateBtn_Click(object sender, EventArgs e)
        {
            DropDownList operateDDL = ((DropDownList)menuLoginView.FindControl("OperateDDL"));
            Response.Redirect(operateDDL.SelectedValue);
        }
    }
}