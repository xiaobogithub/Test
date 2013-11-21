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
    public partial class UpdatePassword : PageBase<UserModel>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ((HyperLink)Master.FindControl("menuLoginView").FindControl("hpBackLink")).NavigateUrl = "~/Profile.aspx";
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (Resolve<IUserService>().IsValidUser(ContextService.CurrentAccount.UserGuid, txtOldPassword.Text))
                {
                    Resolve<IUserService>().UpdatePassword(ContextService.CurrentAccount.UserGuid, txtNewPassword.Text);
                    Response.Redirect("Profile.aspx");
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "alert('The old password is wrong!');", true);
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
