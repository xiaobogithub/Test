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
    public partial class AddUserGroup : PageBase<UserGroupModel>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]))
            {
                if (!IsPostBack)
                {
                    ((HyperLink)Master.FindControl("hpBackLink")).NavigateUrl = string.Format("ManageGroup.aspx?{0}={1}", Constants.QUERYSTR_PROGRAM_GUID, Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]);
                    cancelHyperLink.NavigateUrl = string.Format("ManageGroup.aspx?{0}={1}", Constants.QUERYSTR_PROGRAM_GUID, Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]);
                }
            }
            else
            {
                Response.Redirect("InvalidUrl.aspx");
            }
        }

        protected void addButton_Click(object sender, EventArgs e)
        {
            UserGroupModel model = new UserGroupModel();
            model.Name = groupNameTextBox.Text;
            model.Description = descriptionTextBox.Text;
            model.ProgramGUID = new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]);
            Resolve<IUserGroupService>().AddUserGroup(model);

            Response.Redirect(string.Format("ManageGroup.aspx?{0}={1}", Constants.QUERYSTR_PROGRAM_GUID, Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]));
        }
    }
}
