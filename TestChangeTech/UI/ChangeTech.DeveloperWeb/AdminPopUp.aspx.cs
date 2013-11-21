using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ChangeTech.Models;
using Ethos.DependencyInjection;
using ChangeTech.Contracts;

namespace ChangeTech.DeveloperWeb
{
    public partial class AdminPopUp : PageBase<UserModel>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID] != null)
            {
                if (!IsPostBack)
                {
                    BindAdminUser();
                }
            }
            else
            {
                Response.Redirect("InvalidUrl.aspx");
            }
        }

        private void BindAdminUser()
        {
            adminGridView.DataSource = Resolve<IUserService>().GetUsersInProgram(new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]), UserTypeEnum.Administrator, "", emailTextBox.Text);
            adminGridView.DataBind();
        }

        protected void selectLinkButton_Click(object sender, EventArgs e)
        {
            string UserName = ((LinkButton)sender).CommandArgument;
            Resolve<IUserService>().UpdateUserType(UserName, UserTypeEnum.ProjectManager, new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]));
            //ClientScript.RegisterStartupScript(this.GetType(), "return", "returnValue();", true);
            string Script = @"<script language=""JavaScript"">	
		       window.returnValue = '";
            Script += UserName;
            Script += @"';
		       window.close();
			</script>";

            Response.Write(Script);
        }

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            BindAdminUser();
        }

    }
}
