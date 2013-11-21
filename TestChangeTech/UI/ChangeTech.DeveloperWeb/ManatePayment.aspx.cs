using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ethos.Utility;
using Ethos.DependencyInjection;
using ChangeTech.Models;
using ChangeTech.Contracts;

namespace ChangeTech.DeveloperWeb
{
    public partial class ManatePayment : PageBase<PaymentTemplateModel>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]))
            {
                if (!IsPostBack)
                {
                    try
                    {
                        BindPaymentTemplate();
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

        //private void BindProgram()
        //{
        //    programDropDownList.DataSource = Resolve<IProgramService>().GetSimpleProgramsModel();
        //    programDropDownList.DataTextField = "ProgramName";
        //    programDropDownList.DataValueField = "ProgramGuid";
        //    programDropDownList.DataBind();

        //    BindLanguage();
        //}

        //private void BindLanguage()
        //{
        //    languageDropDownList.DataSource = Resolve<IProgramLanguageService>().GetLanguagesSupportByProgram(new Guid(programDropDownList.SelectedValue));
        //    languageDropDownList.DataTextField = "Name";
        //    languageDropDownList.DataValueField = "LanguageGUID";
        //    languageDropDownList.DataBind();

        //    BindPaymentTemplate();
        //}

        private void BindPaymentTemplate()
        {
            ProgramModel programModel = Resolve<IProgramService>().GetProgramByGUID(new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]));
            programLabel.Text = programModel.ProgramName;

            Model = Resolve<IPaymentService>().GetPaymentTemplate(programModel.Guid);
            if (Model.PaymentTemplateGUID != Guid.Empty)
            {
                titleTextBox.Text = Model.Heading;
                primarybuttonTextBox.Text = Model.PrimaryButtonCaption;
                textTextBox.Text = Model.Text;
                descriptionTextBox.Text = Model.OrderDescription;
                exceptionTipTextBox.Text = Model.ExceptionTip;
                successfulTipTextBox.Text = Model.SuccessfulTip;
                okButton.Text = "Update";
                okButton.CommandArgument = Model.ProgramGUID.ToString();
                loginlinkTextBox.Text = Model.LoginText;
            }
            else
            {
                titleTextBox.Text = "";
                primarybuttonTextBox.Text = "";
                textTextBox.Text = "";
                descriptionTextBox.Text = "";
                exceptionTipTextBox.Text = "";
                successfulTipTextBox.Text = "";
                loginlinkTextBox.Text = "";
                okButton.Text = "Add new";
            }
        }

        protected void okButton_Click(object sender, EventArgs e)
        {
            try
            {
                Button okButton = sender as Button;
                if (okButton.Text == "Update")
                {
                    if (!string.IsNullOrEmpty(okButton.CommandArgument))
                    {
                        PaymentTemplateModel paymentTemplateModel = new PaymentTemplateModel
                        {
                            ProgramGUID = new Guid(okButton.CommandArgument),
                            Heading = titleTextBox.Text,
                            //LanguageGUID = new Guid(ddlLanguage.SelectedValue),
                            PrimaryButtonCaption = primarybuttonTextBox.Text,
                            //ProgramGUID = new Guid(ddlProgram.SelectedValue),
                            Text = textTextBox.Text,
                            OrderDescription = descriptionTextBox.Text,
                            ExceptionTip = exceptionTipTextBox.Text,
                            SuccessfulTip = successfulTipTextBox.Text,
                            LoginText = loginlinkTextBox.Text
                            //Type = "Standard"
                        };
                        Resolve<IPaymentService>().UpdatePaymentTemplate(paymentTemplateModel);
                    }
                }
                else
                {
                    PaymentTemplateModel paymentTemplateModel = new PaymentTemplateModel
                    {
                        PaymentTemplateGUID = Guid.NewGuid(),
                        Heading = titleTextBox.Text,
                        //LanguageGUID = new Guid(ddlLanguage.SelectedValue),
                        PrimaryButtonCaption = primarybuttonTextBox.Text,
                        ProgramGUID = new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]),
                        Text = textTextBox.Text,
                        OrderDescription = descriptionTextBox.Text,
                        ExceptionTip = exceptionTipTextBox.Text,
                        SuccessfulTip = successfulTipTextBox.Text,
                        LoginText = loginlinkTextBox.Text
                    };
                    Resolve<IPaymentService>().AddPaymentTemplate(paymentTemplateModel);
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
            Response.Redirect(string.Format(string.Format("EditProgram.aspx?{0}={1}", Constants.QUERYSTR_PROGRAM_GUID, Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID])));
        }

        //protected void programDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    BindLanguage();
        //}

        //protected void languageDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    BindPaymentTemplate();
        //}
    }
}
