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
    public partial class Profile : PageBase<UserModel>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    InitialUserInfo();
                }
                catch (Exception ex)
                {
                    LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                    throw ex;
                }
                ((HyperLink)Master.FindControl("menuLoginView").FindControl("hpBackLink")).NavigateUrl = "~/Home.aspx";
            }
        }

        private void InitialUserInfo()
        {
            Model = ContextService.CurrentAccount;
            lblGender.Text = Model.Gender.ToString().Trim();
            lblMobilePhone.Text = Model.PhoneNumber;
            lblUserName.Text = Model.UserName;
            chkSuperAdmin.Checked = (((PermissionEnum)Model.Security) & PermissionEnum.ApplicationSuperAdmin) == PermissionEnum.ApplicationSuperAdmin;
            chkCreate.Checked = (((PermissionEnum)Model.Security) & PermissionEnum.ApplicationCreate) == PermissionEnum.ApplicationCreate;
            chkEdit.Checked = (((PermissionEnum)Model.Security) & PermissionEnum.ApplicationEdit) == PermissionEnum.ApplicationEdit;
            chkDelete.Checked = (((PermissionEnum)Model.Security) & PermissionEnum.ApplicationDelete) == PermissionEnum.ApplicationDelete;
            chkAdmin.Checked = (((PermissionEnum)Model.Security) & PermissionEnum.ApplicationAdmin) == PermissionEnum.ApplicationAdmin;
        }

        protected void btnChangePwd_Click(object sender, EventArgs e)
        {
            Response.Redirect("UpdatePassword.aspx");
        }

        protected void btnEditProfile_Click(object sender, EventArgs e)
        {
            Response.Redirect("EditProfile.aspx");
        }
    }
}
