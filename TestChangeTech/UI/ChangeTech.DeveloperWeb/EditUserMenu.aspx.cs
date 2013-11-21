using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ChangeTech.Models;
using Ethos.DependencyInjection;
using ChangeTech.Contracts;
using Ethos.Utility;

namespace ChangeTech.DeveloperWeb
{
    public partial class EditUserMenu : PageBase<UserMenuModel>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString[Constants.QUERYSTR_MENUITEM_GUID]))
                {
                    GetMenuItem();

                    ((HyperLink)Master.FindControl("menuLoginView").FindControl("hpBackLink")).NavigateUrl = string.Format("~/ManageUserMenu.aspx?{0}={1}", Constants.QUERYSTR_PROGRAM_GUID, Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]);
                }
                else
                {
                    Response.Redirect("InvalidUrl.aspx");
                }
            }
        }

        private void GetMenuItem()
        {
            try
            {
                UserMenuModel userMenuModel = Resolve<IUserMenuService>().GetUserMenu(new Guid(Request.QueryString[Constants.QUERYSTR_MENUITEM_GUID]));
                nameTxtBox.Text = userMenuModel.Name;
                textTxtBox.Text = userMenuModel.Text;
                formTextTxtBox.Text = userMenuModel.FormText;
                formTitleTxtBox.Text = userMenuModel.FormTitle;
                backButtonTextTxtBox.Text = userMenuModel.FormBackButtonText;
                submitButtonTextTxtBox.Text = userMenuModel.FormSubmitButtonText;
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                msgLbl.Text = "Fail to get menu item content, internal error occurs, please report to Change Tech develop team.";
            }
        }

        protected void updateButton_Click(object sender, EventArgs args)
        {
            try
            {
                Resolve<IUserMenuService>().UpdateUserMenu(new Guid(Request.QueryString[Constants.QUERYSTR_MENUITEM_GUID]), textTxtBox.Text.Trim(),
                    formTitleTxtBox.Text.Trim(), formTextTxtBox.Text.Trim(), backButtonTextTxtBox.Text.Trim(), submitButtonTextTxtBox.Text.Trim());
                msgLbl.Text = "Update successfuly.";
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                msgLbl.Text = "Fail to update, internal error occurs, please report to Change Tech develop team.";
            }
        }

        protected void cancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("ManageUserMenu.aspx?{0}={1}", Constants.QUERYSTR_PROGRAM_GUID, Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]));
        }
    }
}