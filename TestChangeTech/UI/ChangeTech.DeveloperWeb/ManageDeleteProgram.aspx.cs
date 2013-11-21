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
    public partial class ManageDeleteProgram : PageBase<DeleteApplicationModel>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                InitialPage();
                ((HyperLink)Master.FindControl("menuLoginView").FindControl("hpBackLink")).NavigateUrl = "~/Default.aspx";
            }
        }

        private void InitialPage()
        {
            BindProgramDropDownList();
            BindUserDropDownList();
            BindApplicantApplicationList();
            BindAssigneeApplicationList();
        }

        private void BindAssigneeApplicationList()
        {
            assignToMeGridView.DataSource = Resolve<IDeleteApplicationService>().GetApplicationListByAssigneeAndStatus(ContextService.CurrentAccount.UserGuid, (int)ApplicationStatusEnum.OnProcess);
            assignToMeGridView.DataBind();
        }

        private void BindApplicantApplicationList()
        {
            myApplicationGridView.DataSource = Resolve<IDeleteApplicationService>().GetApplicationListByApplicant(ContextService.CurrentAccount.UserGuid);
            myApplicationGridView.DataBind();
        }

        private void BindUserDropDownList()
        {
            AssigneeDropDownList.DataSource = Resolve<IUserService>().GetSuperAdminList();
            AssigneeDropDownList.DataTextField = "UserName";
            AssigneeDropDownList.DataValueField = "UserGuid";
            AssigneeDropDownList.DataBind();
            AssigneeDropDownList.Items.Remove(new ListItem(ContextService.CurrentAccount.UserName, ContextService.CurrentAccount.UserGuid.ToString()));
        }

        private void BindProgramDropDownList()
        {
            programDropDownList.DataSource = Resolve<IProgramService>().GetSimpleProgramsModel();
            programDropDownList.DataTextField = "ProgramName";
            programDropDownList.DataValueField = "ProgramGuid";
            programDropDownList.DataBind();
        }

        private string ValidateApplication()
        {
            string validateStr = string.Empty;
            if(string.IsNullOrEmpty(programDropDownList.SelectedValue))
            {
                validateStr += "Please select a proram.";
            }
            if(string.IsNullOrEmpty(AssigneeDropDownList.SelectedValue))
            {
                validateStr += "Please select an assignee.";
            }
            return validateStr;
        }

        protected void submitButton_Click(object sender, EventArgs e)
        {
            string validateStr = ValidateApplication();
            if(string.IsNullOrEmpty(validateStr))
            {
                if(Resolve<IDeleteApplicationService>().IsProgramDeleteApplicationExist(new Guid(programDropDownList.SelectedValue)))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Tip", "alert('There is already an application to delete the program.')", true);
                }
                else
                {
                    DeleteApplicationModel applicatinModel = new DeleteApplicationModel
                    {
                        ApplicantGUID = ContextService.CurrentAccount.UserGuid,
                        AssigneeGUID = new Guid(AssigneeDropDownList.SelectedValue),
                        ProgramGUID = new Guid(programDropDownList.SelectedValue)
                    };

                    Resolve<IDeleteApplicationService>().CreateApplication(applicatinModel);
                    Response.Redirect(Request.Url.ToStringWithoutPort());
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Tip", "alert('" + validateStr + "')", true);
            }
        }

        protected void myApplicationGridView_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            myApplicationGridView.PageIndex = e.NewSelectedIndex;
            BindApplicantApplicationList();
        }

        protected void assignToMeGridView_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            assignToMeGridView.PageIndex = e.NewSelectedIndex;
            BindAssigneeApplicationList();
        }

        protected void approveButton_Click(object sender, EventArgs e)
        {
            Button approveButton = sender as Button;
            Resolve<IDeleteApplicationService>().Approve(new Guid(approveButton.CommandArgument));
            Response.Redirect(Request.Url.ToStringWithoutPort());
        }

        protected void declineButton_Click(object sender, EventArgs e)
        {
            Button declineButton = sender as Button;
            Resolve<IDeleteApplicationService>().Decline(new Guid(declineButton.CommandArgument));
            Response.Redirect(Request.Url.ToStringWithoutPort());
        }
    }
}
