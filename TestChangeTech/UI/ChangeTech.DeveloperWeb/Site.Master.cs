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


namespace ChangeTech.DeveloperWeb
{
    public partial class Site : System.Web.UI.MasterPage
    {
        #region Public Variable Property
        private static int lastSelectOperate;

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
        #endregion

        protected void managePageVarible_Load(object sender, EventArgs e)
        {
            if (ContextService.CurrentAccount != null)
            {
                ((HyperLink)sender).NavigateUrl = "~/PageVariableList.aspx?" + Constants.QUERYSTR_USER_GUID + "=" + ContextService.CurrentAccount.UserGuid;
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
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
                    //DropDownList operateDDL = ((DropDownList)menuLoginView.FindControl("OperateDDL"));
                    //operateDDL.SelectedIndex = lastSelectOperate;
                    footerLocalize.Text = string.Format(Convert.ToString(GetGlobalResourceObject("Share", "Footer")), DateTime.UtcNow.Year);
                    
                }
                
            }
            
        }

        #region Private Function
        private void InitOperateDDL()
        {
            List<OperateModel> operateList = new List<OperateModel>();
            DropDownList ddlist = ((DropDownList)menuLoginView.FindControl("OperateDDL"));
            if (SuperAdminPermission)
            {
                operateList.Add(new OperateModel { Name = "--Please select--", Value = "" });
                operateList.Add(new OperateModel { Name = "· Programs", Value = Constants.URL_DEVELOPER_LIST_PROGRAM });
                operateList.Add(new OperateModel { Name = "· Studies", Value = Constants.URL_DEVELOPER_LIST_STUDY });
                operateList.Add(new OperateModel { Name = "· PsyBase", Value = "" });
                operateList.Add(new OperateModel { Name = "--Predictor category", Value = Constants.URL_DEVELOPER_LIST_PREDICTOR_CATEGORY });
                operateList.Add(new OperateModel { Name = "--Predictor", Value = Constants.URL_DEVELOPER_LIST_PREDICTOR });
                operateList.Add(new OperateModel { Name = "--Intervent category", Value = Constants.URL_DEVELOPER_LIST_INTERVENT_CATEGORY });
                operateList.Add(new OperateModel { Name = "--Intervent", Value = Constants.URL_DEVELOPER_LIST_INTERVENT });
                operateList.Add(new OperateModel { Name = "--Page sequence", Value = Constants.URL_DEVELOPER_MOVE_PAGE_SEQUENCE });
                operateList.Add(new OperateModel { Name = "· Back-office", Value = "" });
                operateList.Add(new OperateModel { Name = "--Brand", Value = Constants.URL_DEVELOPER_MANAGE_BRAND });
                operateList.Add(new OperateModel { Name = "--Order email template", Value = Constants.URL_DEVELOPER_MANAGE_ORDER_EMAIL_TEMPLATE });
                operateList.Add(new OperateModel { Name = "--Special string", Value = Constants.URL_DEVELOPER_MANAGE_SPECIAL_STRING });
                operateList.Add(new OperateModel { Name = "--Payment order", Value = Constants.URL_DEVELOPER_MANAGE_PAYMENT_ORDER });
                operateList.Add(new OperateModel { Name = "--Manage language", Value = Constants.URL_DEVELOPER_MANAGE_LANGUAGE });
                operateList.Add(new OperateModel { Name = "--Delete program", Value = Constants.URL_DEVELOPER_MANAGE_DELETE_PROGRAM });
                operateList.Add(new OperateModel { Name = "--Translation job", Value = Constants.URL_DEVELOPER_MANAGE_TRANSLATIONJOB });
                operateList.Add(new OperateModel { Name = "--Application user security", Value = Constants.URL_DEVELOPER_MANAGE_APPLICATION_SECURITY });
                operateList.Add(new OperateModel { Name = "--Program user security", Value = Constants.URL_DEVELOPER_MANAGE_PROGRAM_SECURITY });
                operateList.Add(new OperateModel { Name = "--Activity log", Value = Constants.URL_DEVELOPER_LIST_ACTIVITY_LOG });
                operateList.Add(new OperateModel { Name = "--Profile", Value = Constants.URL_DEVELOPER_PROFILE });
                if (ddlist != null)
                {
                    ddlist.DataSource = operateList;
                    ddlist.DataTextField = "Name";
                    ddlist.DataValueField = "Value";
                    ddlist.DataBind();
                }

                #region Selected oprate
                if (Request.PhysicalPath.Equals(Server.MapPath(Constants.URL_DEVELOPER_LIST_PROGRAM)) || Request.PhysicalPath.Equals(Server.MapPath(Constants.URL_DEVELOPER_HOME)))
                {
                    ddlist.SelectedIndex = 1;
                }
                else if (Request.PhysicalPath.Equals(Server.MapPath(Constants.URL_DEVELOPER_LIST_STUDY)))
                {
                    ddlist.SelectedIndex = 2;
                }
                else if (Request.PhysicalPath.Equals(Server.MapPath(Constants.URL_DEVELOPER_LIST_PREDICTOR_CATEGORY)))
                {
                    ddlist.SelectedIndex = 4;
                }
                else if (Request.PhysicalPath.Equals(Server.MapPath(Constants.URL_DEVELOPER_LIST_PREDICTOR)))
                {
                    ddlist.SelectedIndex = 5;
                }
                else if (Request.PhysicalPath.Equals(Server.MapPath(Constants.URL_DEVELOPER_LIST_INTERVENT_CATEGORY)))
                {
                    ddlist.SelectedIndex = 6;
                }
                else if (Request.PhysicalPath.Equals(Server.MapPath(Constants.URL_DEVELOPER_LIST_INTERVENT)))
                {
                    ddlist.SelectedIndex = 7;
                }
                else if (Request.PhysicalPath.Equals(Server.MapPath(Constants.URL_DEVELOPER_MOVE_PAGE_SEQUENCE)))
                {
                    ddlist.SelectedIndex = 8;
                }
                else if (Request.PhysicalPath.Equals(Server.MapPath(Constants.URL_DEVELOPER_MANAGE_BRAND)))
                {
                    ddlist.SelectedIndex = 10;
                }
                else if (Request.PhysicalPath.Equals(Server.MapPath(Constants.URL_DEVELOPER_MANAGE_ORDER_EMAIL_TEMPLATE)))
                {
                    ddlist.SelectedIndex = 11;
                }
                else if (Request.PhysicalPath.Equals(Server.MapPath(Constants.URL_DEVELOPER_MANAGE_SPECIAL_STRING)))
                {
                    ddlist.SelectedIndex = 12;
                }
                else if (Request.PhysicalPath.Equals(Server.MapPath(Constants.URL_DEVELOPER_MANAGE_PAYMENT_ORDER)))
                {
                    ddlist.SelectedIndex = 13;
                }
                else if (Request.PhysicalPath.Equals(Server.MapPath(Constants.URL_DEVELOPER_MANAGE_LANGUAGE)))
                {
                    ddlist.SelectedIndex = 14;
                }
                else if (Request.PhysicalPath.Equals(Server.MapPath(Constants.URL_DEVELOPER_MANAGE_DELETE_PROGRAM)))
                {
                    ddlist.SelectedIndex = 15;
                }
                else if (Request.PhysicalPath.Equals(Server.MapPath(Constants.URL_DEVELOPER_MANAGE_TRANSLATIONJOB)))
                {
                    ddlist.SelectedIndex = 16;
                }
                else if (Request.PhysicalPath.Equals(Server.MapPath(Constants.URL_DEVELOPER_MANAGE_BRAND)))
                {
                    ddlist.SelectedIndex = 14;
                }
                else if (Request.PhysicalPath.Equals(Server.MapPath(Constants.URL_DEVELOPER_MANAGE_PAYMENT_ORDER)))
                {
                    ddlist.SelectedIndex = 15;
                }
                else if (Request.PhysicalPath.Equals(Server.MapPath(Constants.URL_DEVELOPER_MANAGE_ORDER_EMAIL_TEMPLATE)))
                {
                    ddlist.SelectedIndex = 16;
                }
                else if (Request.PhysicalPath.Equals(Server.MapPath(Constants.URL_DEVELOPER_MANAGE_APPLICATION_SECURITY)))
                {
                    ddlist.SelectedIndex = 17;
                }
                else if (Request.PhysicalPath.Equals(Server.MapPath(Constants.URL_DEVELOPER_MANAGE_PROGRAM_SECURITY)))
                {
                    ddlist.SelectedIndex = 18;
                }
                else if (Request.PhysicalPath.Equals(Server.MapPath(Constants.URL_DEVELOPER_LIST_ACTIVITY_LOG)))
                {
                    ddlist.SelectedIndex = 19;
                }
                else if (Request.PhysicalPath.Equals(Server.MapPath(Constants.URL_DEVELOPER_PROFILE)))
                {
                    ddlist.SelectedIndex = 20;
                }
                #endregion
            }
            else if (ManagePermission)
            {
                operateList.Add(new OperateModel { Name = "--Please select--", Value = "" });
                operateList.Add(new OperateModel { Name = "· Programs", Value = Constants.URL_DEVELOPER_LIST_PROGRAM });
                operateList.Add(new OperateModel { Name = "· Studies", Value = Constants.URL_DEVELOPER_LIST_STUDY });
                operateList.Add(new OperateModel { Name = "· Back-office", Value = "" });
                operateList.Add(new OperateModel { Name = "--Brand", Value = Constants.URL_DEVELOPER_MANAGE_BRAND });
                operateList.Add(new OperateModel { Name = "--Order email template", Value = Constants.URL_DEVELOPER_MANAGE_ORDER_EMAIL_TEMPLATE });
                operateList.Add(new OperateModel { Name = "--Special string", Value = Constants.URL_DEVELOPER_MANAGE_SPECIAL_STRING });
                operateList.Add(new OperateModel { Name = "--Payment order", Value = Constants.URL_DEVELOPER_MANAGE_PAYMENT_ORDER });
                operateList.Add(new OperateModel { Name = "--Manage language", Value = Constants.URL_DEVELOPER_MANAGE_LANGUAGE });
                operateList.Add(new OperateModel { Name = "--Delete program", Value = Constants.URL_DEVELOPER_MANAGE_DELETE_PROGRAM });
                operateList.Add(new OperateModel { Name = "--Translation job", Value = Constants.URL_DEVELOPER_MANAGE_TRANSLATIONJOB });
                operateList.Add(new OperateModel { Name = "--Application user security", Value = Constants.URL_DEVELOPER_MANAGE_APPLICATION_SECURITY });
                operateList.Add(new OperateModel { Name = "--Program user security", Value = Constants.URL_DEVELOPER_MANAGE_PROGRAM_SECURITY });
                operateList.Add(new OperateModel { Name = "--Activity log", Value = Constants.URL_DEVELOPER_LIST_ACTIVITY_LOG });
                operateList.Add(new OperateModel { Name = "--Profile", Value = Constants.URL_DEVELOPER_PROFILE });
                if (ddlist != null)
                {
                    ddlist.DataSource = operateList;
                    ddlist.DataTextField = "Name";
                    ddlist.DataValueField = "Value";
                    ddlist.DataBind();
                }
                #region Selected oprate
                if (Request.PhysicalPath.Equals(Server.MapPath(Constants.URL_DEVELOPER_LIST_PROGRAM)))
                {
                    ddlist.SelectedIndex = 1;
                }
                else if (Request.PhysicalPath.Equals(Server.MapPath(Constants.URL_DEVELOPER_LIST_STUDY)))
                {
                    ddlist.SelectedIndex = 2;
                }
                else if (Request.PhysicalPath.Equals(Server.MapPath(Constants.URL_DEVELOPER_MANAGE_BRAND)))
                {
                    ddlist.SelectedIndex = 4;
                }
                else if (Request.PhysicalPath.Equals(Server.MapPath(Constants.URL_DEVELOPER_MANAGE_ORDER_EMAIL_TEMPLATE)))
                {
                    ddlist.SelectedIndex = 5;
                }
                else if (Request.PhysicalPath.Equals(Server.MapPath(Constants.URL_DEVELOPER_MANAGE_SPECIAL_STRING)))
                {
                    ddlist.SelectedIndex = 6;
                }
                else if (Request.PhysicalPath.Equals(Server.MapPath(Constants.URL_DEVELOPER_MANAGE_PAYMENT_ORDER)))
                {
                    ddlist.SelectedIndex = 7;
                }
                else if (Request.PhysicalPath.Equals(Server.MapPath(Constants.URL_DEVELOPER_MANAGE_LANGUAGE)))
                {
                    ddlist.SelectedIndex = 8;
                }
                else if (Request.PhysicalPath.Equals(Server.MapPath(Constants.URL_DEVELOPER_MANAGE_DELETE_PROGRAM)))
                {
                    ddlist.SelectedIndex = 9;
                }
                else if (Request.PhysicalPath.Equals(Server.MapPath(Constants.URL_DEVELOPER_MANAGE_TRANSLATIONJOB)))
                {
                    ddlist.SelectedIndex = 10;
                }
                else if (Request.PhysicalPath.Equals(Server.MapPath(Constants.URL_DEVELOPER_MANAGE_BRAND)))
                {
                    ddlist.SelectedIndex = 11;
                }
                else if (Request.PhysicalPath.Equals(Server.MapPath(Constants.URL_DEVELOPER_MANAGE_PAYMENT_ORDER)))
                {
                    ddlist.SelectedIndex = 12;
                }
                else if (Request.PhysicalPath.Equals(Server.MapPath(Constants.URL_DEVELOPER_MANAGE_ORDER_EMAIL_TEMPLATE)))
                {
                    ddlist.SelectedIndex = 13;
                }
                else if (Request.PhysicalPath.Equals(Server.MapPath(Constants.URL_DEVELOPER_MANAGE_APPLICATION_SECURITY)))
                {
                    ddlist.SelectedIndex = 14;
                }
                else if (Request.PhysicalPath.Equals(Server.MapPath(Constants.URL_DEVELOPER_MANAGE_PROGRAM_SECURITY)))
                {
                    ddlist.SelectedIndex = 15;
                }
                else if (Request.PhysicalPath.Equals(Server.MapPath(Constants.URL_DEVELOPER_LIST_ACTIVITY_LOG)))
                {
                    ddlist.SelectedIndex = 16;
                }
                else if (Request.PhysicalPath.Equals(Server.MapPath(Constants.URL_DEVELOPER_PROFILE)))
                {
                    ddlist.SelectedIndex = 17;
                }
                #endregion
            }
        }

        private void ChooseHyperLink(string id)
        {
            HyperLink hylink = ((HyperLink)menuLoginView.FindControl(id));
            if (hylink != null)
            {
                hylink.Enabled = false;
                hylink.Attributes.Add("class", "selected");
            }
        }
       
        private void SetEditProgramPath()
        {
            if (this.FindControl("menuLoginView") != null
                && this.FindControl("menuLoginView").FindControl("SiteMapPath")!=null
                && this.FindControl("menuLoginView").FindControl("SiteMapPath").Controls.Count > 6)
            {
                
                string programGUID = Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID];       
       
                string programPage = Request.QueryString[Constants.QUERYSTR_PROGRAM_PAGE];
            
                IContainerContext context = ContainerManager.GetContainer("container");
                ProgramModel programModel = context.Resolve<IProgramService>().GetProgramByGUID(new Guid(programGUID));
                HyperLink editProgram = (HyperLink)this.FindControl("menuLoginView").FindControl("SiteMapPath").Controls[4].Controls[0];
                if (editProgram != null)
                {
                    editProgram.Text = string.Format("Edit Program \"{0}\"", programModel.ProgramName);
                    editProgram.NavigateUrl = string.Format("EditProgram.aspx?{0}={1}&{2}={3}", Constants.QUERYSTR_PROGRAM_PAGE, programPage, Constants.QUERYSTR_PROGRAM_GUID, programGUID);
                }
            }
        }
        #endregion

        #region Event Handler
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

        protected void GoOperateBtn_Click(object sender, EventArgs e)
        {
            DropDownList operateDDL = ((DropDownList)menuLoginView.FindControl("OperateDDL"));
            Response.Redirect(operateDDL.SelectedValue);
        }

        protected void logoutLnkBtn_Click(object sender, EventArgs e)
        {
            FormsAuthentication.RedirectToLoginPage();
            FormsAuthentication.SignOut();
            ContextService.ClearAccount();
            Response.End();
        }
        protected void NodeItemHandle(object sender, EventArgs e)
        {
            SetEditProgramPath();
        }
        #endregion
    }
}
