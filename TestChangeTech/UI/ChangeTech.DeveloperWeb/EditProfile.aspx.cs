using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ChangeTech.Models;
using Ethos.DependencyInjection;
using ChangeTech.Contracts;
using System.Web.Security;
using Ethos.Utility;

namespace ChangeTech.DeveloperWeb
{
    public partial class EditProfile : PageBase<UserModel>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    InitialProfile();
                }
                catch (Exception ex)
                {
                    LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                    throw ex;
                }  
            }
        }

        private void InitialProfile()
        {
            Model = ContextService.CurrentAccount;
            txtMobilePhone.Text = Model.PhoneNumber.Trim();
            txtUserName.Text = Model.UserName;

            ddlGender.SelectedValue = Model.Gender.ToString().Trim();
            ((HyperLink)Master.FindControl("menuLoginView").FindControl("hpBackLink")).NavigateUrl = "~/Profile.aspx";
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtUserName.Text.Equals(ContextService.CurrentAccount.UserName))
                {
                    Resolve<IUserService>().UpdateUserWithoutPassword(ContextService.CurrentAccount.UserGuid, txtUserName.Text, txtMobilePhone.Text, ddlGender.SelectedValue);
                    ContextService.CurrentAccount = Resolve<IUserService>().GetUserModelByUserGUID(ContextService.CurrentAccount.UserGuid);
                    Response.Redirect("Profile.aspx");
                }
                else
                {
                    Resolve<IUserService>().UpdateUserWithoutPassword(ContextService.CurrentAccount.UserGuid, txtUserName.Text, txtMobilePhone.Text, ddlGender.SelectedValue);
                    ContextService.ClearAccount();
                    FormsAuthentication.SignOut();
                    Response.Redirect("Information.aspx");
                }
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }  
        }

        protected void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("Profile.aspx");
        }
    }
}
