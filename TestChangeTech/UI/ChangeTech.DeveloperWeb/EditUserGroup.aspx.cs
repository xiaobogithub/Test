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
    public partial class EditUserGroup : PageBase<UserGroupModel>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString[Constants.QUERYSTR_USERGROUP_GUID]))
            {
                if (!IsPostBack)
                {
                    BindUserGroup();
                }
                ((HyperLink)Master.FindControl("hpBackLink")).NavigateUrl = string.Format("ManageGroup.aspx?{0}={1}", Constants.QUERYSTR_PROGRAM_GUID, Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]);
            }
            else
            {
                Response.Redirect("InvalidUrl.aspx");
            }
        }

        private void BindUserGroup()
        {
            UserGroupModel model = Resolve<IUserGroupService>().GetUserGroupModel(new Guid(Request.QueryString[Constants.QUERYSTR_USERGROUP_GUID]));
            groupNameTextBox.Text = model.Name;
            descriptionTextBox.Text = model.Description;
            cancelHyperLink.NavigateUrl = string.Format("ManageGroup.aspx?{0}={1}", Constants.QUERYSTR_PROGRAM_GUID, Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]);
        }

        protected void updateButton_Click(object sender, EventArgs e)
        {
            UserGroupModel model = new UserGroupModel
            {
                Description = descriptionTextBox.Text,
                Name = groupNameTextBox.Text,
                GroupGUID = new Guid(Request.QueryString[Constants.QUERYSTR_USERGROUP_GUID])
            };

            Resolve<IUserGroupService>().UpdateUserGroup(model);
            Response.Redirect(string.Format("ManageGroup.aspx?{0}={1}", Constants.QUERYSTR_PROGRAM_GUID, Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]));
        }
    }
}
