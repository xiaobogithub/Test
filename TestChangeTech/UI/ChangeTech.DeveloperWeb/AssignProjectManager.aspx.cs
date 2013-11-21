using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ethos.DependencyInjection;
using ChangeTech.Models;
using ChangeTech.Contracts;

namespace ChangeTech.DeveloperWeb
{
    public partial class AssignProjectManager : PageBase<UserModel>
    {
        private string ProgramGUID
        {
            get
            {
                return Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID];
            }
        }

        private string ProgramPage
        {
            get
            {
                return Request.QueryString[Constants.QUERYSTR_PROGRAM_PAGE];
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]))
            {
                if (!IsPostBack)
                {
                    ((HyperLink)Master.FindControl("menuLoginView").FindControl("hpBackLink")).NavigateUrl = string.Format("ListProgram.aspx?Page={0}", ProgramPage);
                    BindUser();
                }
            }
            else
            {
                Response.Redirect("InvalidUrl.aspx");
            }
        }

        private void BindUser()
        {
            programHiddenField.Value = Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID];
            adminGridView.DataSource = Resolve<IUserService>().GetUsersInProgram(new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]), UserTypeEnum.ProjectManager, string.Empty, emailTextBox.Text);
            adminGridView.DataBind();
        }

        protected void selectLinkButton_Click(object sender, EventArgs e)
        {
            Guid userguid = new Guid(((LinkButton)sender).CommandArgument);
            Resolve<IProgramService>().AssignManagerToProgram(new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]), userguid);
            // don't know why there pass programGuid, listprogram never use it
            Response.Redirect(string.Format("ListProgram.aspx?{0}={1}&Page={2}", Constants.QUERYSTR_PROGRAM_GUID, Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID], ProgramPage));
        }

        protected void searchButton_Click(object sender, EventArgs e)
        {
            BindUser();
        }

        protected void emailTextBox_TextChanged(object sender, EventArgs e)
        {
            BindUser();
        }
    }
}
