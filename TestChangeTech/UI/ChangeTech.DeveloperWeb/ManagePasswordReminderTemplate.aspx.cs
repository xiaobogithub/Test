﻿using System;
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
    public partial class ManagePasswordReminderTemplate : PageBase<AccessoryPageModel>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]))
            {
                if (!IsPostBack)
                {
                    try
                    {
                        BindLoginTemplate();
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

        private void BindLoginTemplate()
        {
            ProgramModel programmodel = Resolve<IProgramService>().GetProgramByGUID(new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]));
            Model = Resolve<IProgramAccessoryService>().GetProgramAccessoryByProgarm(programmodel.Guid, "Password reminder");
            if (Model.AccessoryTemplateGUID != Guid.Empty)
            {
                txtHeading.Text = Model.Heading;
                txtPrimaryButtonText.Text = Model.PrimaryButtonText;
                txtSecondaryButtonText.Text = Model.SecondaryButtonText;
                txtText.Text = Model.Text;
                txtUserNameText.Text = Model.UserNameText;
                btnOK.Text = "Update";
                btnOK.CommandArgument = Model.AccessoryTemplateGUID.ToString();
            }
            else
            {
                txtHeading.Text = "";
                txtPrimaryButtonText.Text = "";
                txtSecondaryButtonText.Text = "";
                txtText.Text = "";
                txtUserNameText.Text = "";
                btnOK.Text = "Add new";
            }

        }

        //private void BindLanguage()
        //{
        //    ddlLanguage.DataSource = Resolve<IProgramLanguageService>().GetLanguagesSupportByProgram(new Guid(ddlProgram.SelectedValue));
        //    ddlLanguage.DataTextField = "Name";
        //    ddlLanguage.DataValueField = "LanguageGUID";
        //    ddlLanguage.DataBind();

        //    BindLoginTemplate();
        //}

        //private void BindProgram()
        //{
        //    ddlProgram.DataSource = Resolve<IProgramService>().GetSimpleProgramsModel();
        //    ddlProgram.DataTextField = "ProgramName";
        //    ddlProgram.DataValueField = "ProgramGuid";
        //    ddlProgram.DataBind();

        //    BindLanguage();
        //}

        protected void btnOK_Click(object sender, EventArgs e)
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
                            Heading = txtHeading.Text,
                            //LanguageGUID = new Guid(ddlLanguage.SelectedValue),
                            PrimaryButtonText = txtPrimaryButtonText.Text,
                            ProgramGUID = new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]),
                            SecondaryButtonText = txtSecondaryButtonText.Text,
                            Text = txtText.Text,
                            UserNameText = txtUserNameText.Text,
                            Type = "Password reminder"
                        };
                        Resolve<IProgramAccessoryService>().UpdateAccessory(accessoryModel);
                    }
                }
                else
                {
                    AccessoryPageModel accessoryModel = new AccessoryPageModel
                    {
                        Heading = txtHeading.Text,
                        //LanguageGUID = new Guid(ddlLanguage.SelectedValue),
                        PrimaryButtonText = txtPrimaryButtonText.Text,
                        ProgramGUID = new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]),
                        SecondaryButtonText = txtSecondaryButtonText.Text,
                        Text = txtText.Text,
                        UserNameText = txtUserNameText.Text,
                        Type = "Password reminder"
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
        //    BindLoginTemplate();
        //}
    }
}
