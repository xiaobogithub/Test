using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ethos.Utility;
using ChangeTech.Contracts;
using Ethos.DependencyInjection;
using ChangeTech.Models;

namespace ChangeTech.DeveloperWeb
{
    public partial class ManageSessionEndingTemplate : PageBase<AccessoryPageModel>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]))
            {
                if (!IsPostBack)
                {
                    try
                    {
                        BindTemplate();
                       ((HyperLink)Master.FindControl("menuLoginView").FindControl("hpBackLink")).NavigateUrl = string.Format(string.Format("EditProgram.aspx?{0}={1}", Constants.QUERYSTR_PROGRAM_GUID, Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]));
                    }
                    catch (Exception ex)
                    {
                        LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                        throw ex;
                    }
                }
            }
            else
            {
                Response.Redirect("InvalidUrl.aspx");
            }
        }

        //private void InitialPage()
        //{
        //    BindProgram();
        //}

        private void BindTemplate()
        {
            ProgramModel programModel = Resolve<IProgramService>().GetProgramByGUID(new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]));
            programLabel.Text = programModel.ProgramName;

            Model = Resolve<IProgramAccessoryService>().GetProgramAccessoryByProgarm(programModel.Guid, "Session ending");
            if (Model.AccessoryTemplateGUID != Guid.Empty)
            {
                titleTextBox.Text = Model.Heading;
                textTextBox.Text = Model.Text;
                primaryButtonTextBox.Text = Model.PrimaryButtonText;
                SaveButton.Text = "Update";
                SaveButton.CommandArgument = Model.AccessoryTemplateGUID.ToString();
            }
            else
            {
                titleTextBox.Text = "";
                textTextBox.Text = "";
                primaryButtonTextBox.Text = "";
                SaveButton.Text = "Add new";
            }
        }

        //private void BindLanguage()
        //{
        //    ddlLanguage.DataSource = Resolve<IProgramLanguageService>().GetLanguagesSupportByProgram(new Guid(ddlProgram.SelectedValue));
        //    ddlLanguage.DataTextField = "Name";
        //    ddlLanguage.DataValueField = "LanguageGUID";
        //    ddlLanguage.DataBind();

        //    BindTemplate();
        //}

        //private void BindProgram()
        //{
        //    ddlProgram.DataSource = Resolve<IProgramService>().GetSimpleProgramsModel();
        //    ddlProgram.DataTextField = "ProgramName";
        //    ddlProgram.DataValueField = "ProgramGuid";
        //    ddlProgram.DataBind();

        //    BindLanguage();
        //}

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                Button okButton = sender as Button;
                if (okButton.Text == "Update")
                {
                    if (!string.IsNullOrEmpty(okButton.CommandArgument))
                    {
                        AccessoryPageModel accessoryModel = new AccessoryPageModel
                        {
                            AccessoryTemplateGUID = new Guid(okButton.CommandArgument),
                            Heading = titleTextBox.Text,
                            //LanguageGUID = new Guid(ddlLanguage.SelectedValue),
                            PrimaryButtonText = primaryButtonTextBox.Text,
                            //ProgramGUID = new Guid(ddlProgram.SelectedValue),
                            Text = textTextBox.Text,
                            //Type = "Session ending"
                        };
                        Resolve<IProgramAccessoryService>().UpdateAccessory(accessoryModel);
                    }
                }
                else
                {
                    //ProgramModel programModel = Resolve<IProgramService>().GetProgramByProgramGUIDAndLanguageGUID(new Guid(ddlProgram.SelectedValue), new Guid(ddlLanguage.SelectedValue));
                    AccessoryPageModel accessoryModel = new AccessoryPageModel
                    {
                        Heading = titleTextBox.Text,
                        //LanguageGUID = new Guid(ddlLanguage.SelectedValue),
                        PrimaryButtonText = primaryButtonTextBox.Text,
                        ProgramGUID = new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]),
                        Text = textTextBox.Text,
                        Type = "Session ending"
                    };
                    Resolve<IProgramAccessoryService>().AddAccessroy(accessoryModel);
                }
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
            Response.Redirect(Request.Url.ToStringWithoutPort());
        }

        protected void cancelButton_Click(object sender, EventArgs e)
        {
            string goBackUrl = string.Format(string.Format("EditProgram.aspx?{0}={1}", Constants.QUERYSTR_PROGRAM_GUID, Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]));
            Response.Redirect(goBackUrl);
        }

        //protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    BindLanguage();
        //}

        //protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    BindTemplate();
        //}
    }
}
