using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ethos.DependencyInjection;
using ChangeTech.Models;
using ChangeTech.Contracts;
using Ethos.Utility;

namespace ChangeTech.DeveloperWeb
{
    public partial class UserPopUp : PageBase<UserModel>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID] != null)
            {               
               BindUser();
            }
            else
            {
                Response.Redirect("InvalidUrl.aspx");
            }
        }

        private void BindUser()
        {
            ProgramModel programmodel = Resolve<IProgramService>().GetProgramByProgramGUIDAndLanguageGUID(new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]),new Guid(Request.QueryString[Constants.QUERYSTR_LANGUAGE_GUID]));
            int usersCount = Resolve<IUserService>().GetUsrsCout(programmodel.Guid, (UserTypeEnum)Enum.Parse(typeof(UserTypeEnum), ddlUserType.SelectedValue), ddlUserStatus.SelectedValue, EmailTextBox.Text.Trim());
            InitialPageInfo((int)Math.Ceiling(Convert.ToDouble(usersCount) / PageSize), "UserName", "asc");
            string url = Request.Url.ToStringWithoutPort();
            PagingString = Ethos.Utility.PagingSortingService.InitialPagingString(Request.Url.ToStringWithoutPort(), PageNumber, CurrentPageNumber, Resources.Share.Previous, Resources.Share.Next);
            UserRepeater.DataSource = Resolve<IUserService>().GetUsersInProgram(programmodel.Guid, (UserTypeEnum)Enum.Parse(typeof(UserTypeEnum), ddlUserType.SelectedValue), ddlUserStatus.SelectedValue, EmailTextBox.Text.Trim(), CurrentPageNumber, PageSize);
            UserRepeater.DataBind();
            //UserGridView.DataSource = Resolve<IUserService>().GetUsersInProgram(new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]), Convert.ToInt32(ddlUserType.SelectedValue), ddlUserStatus.SelectedValue, EmailTextBox.Text.Trim());
            //UserGridView.DataBind();
        }

        protected void selectLinkButton_Click(object sender, EventArgs e)
        {
            string email = (sender as LinkButton).CommandArgument;
            string Script = @"<script language=""JavaScript"">	
		       window.returnValue = '";
            Script += email;
            Script += @"';
		       window.close();
			</script>";

            Response.Write(Script);
        }

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            BindUser();
        }

        //protected void UserGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    UserGridView.PageIndex = e.NewPageIndex;
        //    BindUser();
        //}
    }
}
