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
    public partial class ManageGroup : PageBase<UserGroupModel>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]))
            {
                if (!IsPostBack)
                {
                    BindUserGroup();
                }
                ((HyperLink)Master.FindControl("hpBackLink")).NavigateUrl = string.Format("EditProgram.aspx?{0}={1}", Constants.QUERYSTR_PROGRAM_GUID, Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]);
            }
        }

        private void BindUserGroup()
        {
            groupRepeater.DataSource = Resolve<IUserGroupService>().GetUserGroupList(new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]));
            groupRepeater.DataBind();
        }

        protected void editButton_Click(object sender, EventArgs e)
        {
            string groupguid = ((Button)sender).CommandArgument;
            Response.Redirect(string.Format("EditUserGroup.aspx?{0}={1}&{2}={3}", Constants.QUERYSTR_PROGRAM_GUID, Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID], Constants.QUERYSTR_USERGROUP_GUID, groupguid));
        }

        protected void deleteButton_Click(object sender, EventArgs e)
        {
            string groupguid = ((Button)sender).CommandArgument;
            Resolve<IUserGroupService>().DeleteUserGroup(new Guid(groupguid));
            Response.Redirect(string.Format("ManageGroup.aspx?{0}={1}", Constants.QUERYSTR_PROGRAM_GUID, Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]));
        }

        protected void addButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("AddUserGroup.aspx?{0}={1}", Constants.QUERYSTR_PROGRAM_GUID, Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]));
        }

        protected void groupRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.DataItem != null)
            {
                Label screenUrlLabel = e.Item.FindControl("surlLabel") as Label;
                Label liveUrlLabel = e.Item.FindControl("lurlLabel") as Label;
                string groupguid = (e.Item.FindControl("editButton") as Button).CommandArgument;
                screenUrlLabel.Text = string.Format(Resolve<ISystemSettingService>().GetSettingValue("WebServerPath") + "ChangeTech.html?Mode=Trial&{0}={1}&{2}={3}", Constants.QUERYSTR_PROGRAM_GUID, Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID], Constants.QUERYSTR_USERGROUP_GUID, groupguid);
                liveUrlLabel.Text = string.Format(Resolve<ISystemSettingService>().GetSettingValue("WebServerPath") + "ChangeTech.html?Mode=Live&{0}={1}&{2}={3}", Constants.QUERYSTR_PROGRAM_GUID, Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID], Constants.QUERYSTR_USERGROUP_GUID, groupguid);
            }
        }
    }
}
