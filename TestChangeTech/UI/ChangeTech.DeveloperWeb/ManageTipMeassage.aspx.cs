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
    public partial class ManageTipMeassage : PageBase<TipMessageModel>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]))
            {
                if (!IsPostBack)
                {
                    try
                    {
                        InitialPage();
                        ((HyperLink)Master.FindControl("menuLoginView").FindControl("hpBackLink")).NavigateUrl = string.Format("EditProgram.aspx?{0}={1}", Constants.QUERYSTR_PROGRAM_GUID, Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]);
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

        private void InitialPage()
        {
            //BindLanguageDropDown();
            BindTipMessageDropDown();
            if (!string.IsNullOrEmpty(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]) && !string.IsNullOrEmpty(ddlTipMessage.SelectedValue))
            {
                BindTipMessage(new Guid(ddlTipMessage.SelectedValue), new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]));
            }
        }

        private void BindTipMessageDropDown()
        {
            ddlTipMessage.DataSource = Resolve<ITipMessageService>().GetAllTipMessageType();
            ddlTipMessage.DataTextField = "TipMessageTypeName";
            ddlTipMessage.DataValueField = "TipMessageTypeGUID";
            ddlTipMessage.DataBind();
        }

        //private void BindLanguageDropDown()
        //{
        //    ddlLanguage.DataSource = Resolve<ILanguageService>().GetLanguagesModel();
        //    ddlLanguage.DataTextField = "Name";
        //    ddlLanguage.DataValueField = "LanguageGUID";
        //    ddlLanguage.DataBind();
        //}

        private void BindTipMessage(Guid tipMessageTypeGuid, Guid languageGuid)
        {
            Model = Resolve<ITipMessageService>().GetTipMessageModel(tipMessageTypeGuid, languageGuid);
            if (Model.TipMessageGUID != Guid.Empty)
            {
                txtMessage.Text = Model.Message;
                txtTitle.Text = Model.Title;
                txtBackButton.Text = Model.BackButtonName;
                txtExplanation.Text = Model.Explanation;
                btnUpdate.Text = "Update";
            }
            else
            {
                btnUpdate.Text = "Insert";
            }
        }

        private void ResetPage()
        {
            txtBackButton.Text = "";
            txtMessage.Text = "";
            txtTitle.Text = "";
            txtExplanation.Text = "";
            if (!string.IsNullOrEmpty(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]) && !string.IsNullOrEmpty(ddlTipMessage.SelectedValue))
            {
                BindTipMessage(new Guid(ddlTipMessage.SelectedValue), new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]));
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                Button updateButton = sender as Button;
                TipMessageModel messageModel = new TipMessageModel
                {
                    Title = txtTitle.Text,
                    Message = txtMessage.Text,
                    BackButtonName = txtBackButton.Text,
                    ProgramGUID = new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]),
                    TipMessageTypeGUID = new Guid(ddlTipMessage.SelectedValue)
                };
                if (updateButton.Text.Equals("Insert"))
                {
                    Resolve<ITipMessageService>().InsertTipMessage(messageModel);
                    Response.Redirect(Request.Url.ToStringWithoutPort());
                }
                else
                {
                    Resolve<ITipMessageService>().UpdateTipMessageModel(messageModel);
                    Response.Redirect(Request.Url.ToStringWithoutPort());
                }
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            ResetPage();
        }

        protected void ddlTipMessage_SelectedIndexChanged(object sender, EventArgs e)
        {
            ResetPage();
        }
    }
}
