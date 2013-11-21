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
    public partial class ForgetPassword : PageBase<ProgramStatusModel>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ((HyperLink)Master.FindControl("menuLoginView").FindControl("hpBackLink")).NavigateUrl = string.Format("~/Default.aspx");
        }

        protected void btnRetrievePassword_Click(object sender, EventArgs e)
        {
            try
            {
                if (Resolve<IUserService>().IsValidUserRetrievePassword(txtUserEmail.Text, txtMobilePhone.Text, Guid.Empty))
                {
                    Resolve<IEmailService>().SendForgetPasswordEmail(txtUserEmail.Text, Guid.Empty);
                    MsgLbl.Text = "Password has been send your email box.";
                }
                else
                {
                    //Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "alert('The information your typed are wrong!');", true);
                    MsgLbl.Text = "We cannot find the registered user according what you input, please check whether you have provider correct information.";
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
