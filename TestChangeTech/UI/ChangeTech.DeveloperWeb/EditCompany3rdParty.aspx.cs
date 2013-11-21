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
    public partial class EditCompany3rdParty : PageBase<CompanyRightModel>
    {
        private string ProgramGUID { get { return Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]; } }
        private string LanguageGUID { get { return Request.QueryString[Constants.QUERYSTR_LANGUAGE_GUID]; } }

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(Request.QueryString[Constants.QUERYSTR_COMPANY_RIGHT_GUID]))
            {
                if(!IsPostBack)
                {
                    InitialPag();
                   // ((HyperLink)Master.FindControl("hpBackLink")).NavigateUrl = string.Format("MaintenanceHome.aspx?{0}={1}&{2}={3}", Constants.QUERYSTR_PROGRAM_GUID, ProgramGUID, Constants.QUERYSTR_LANGUAGE_GUID, LanguageGUID);
                }
            }
            else
            {
                Response.Redirect("InvalidUrl.aspx");
            }
        }

        private void InitialPag()
        {
            Model = Resolve<ICompanyService>().GetCompanyRightModelByGuid(new Guid(Request.QueryString[Constants.QUERYSTR_COMPANY_RIGHT_GUID]));
            programNameLabel.Text = Model.ProgramName;
            companyTextBox.Text = Model.CompanyName;
            overdueTextBox.Text = Model.OverDueTime.ToString("yyyy-MM-dd");
            startTextBox.Text = Model.StartTime.ToString("yyyy-MM-dd");
            versionLabel.Text = Model.Language;
            contactPersonTextBox.Text = Model.ContactPerson;
            internalContactTextBox.Text = Model.InternalContact;
            mobileTextBox.Text = Model.Mobile;
            emailTextBox.Text = Model.Email;
            streetAddressTextBox.Text = Model.StreetAddress;
            postalAddressTextBox.Text = Model.PostalAddress;
            notesTextBox.Text = Model.ComayDescription;

            string serverPath = string.Empty;
            if(Resolve<IProgramService>().IsSupportHttps(new Guid(ProgramGUID)))
            {
                serverPath = Resolve<ISystemSettingService>().GetSettingValue("WebServerPathHttps");
            }
            else
            {
                serverPath = Resolve<ISystemSettingService>().GetSettingValue("WebServerPath");
            }
            screenurlHyperLink.NavigateUrl = string.Format(serverPath + "ChangeTech.html?Mode=Trial&{0}={1}&{2}={3}", Constants.QUERYSTR_PROGRAM_CODE, Model.ProgramCode, Constants.QUERYSTR_COMPANY_CODE, Model.CompanyCode);
            screenurlHyperLink.Text = screenurlHyperLink.NavigateUrl;
            loginHyperLink.NavigateUrl = string.Format(serverPath + "ChangeTech.html?Mode=Live&{0}={1}&{2}={3}", Constants.QUERYSTR_PROGRAM_CODE, Model.ProgramCode, Constants.QUERYSTR_COMPANY_CODE, Model.CompanyCode);
            loginHyperLink.Text = loginHyperLink.NavigateUrl;

            BindReportGridView(Model.ProgramGUID, Model.CompanyGUID);
        }

        private void BindReportGridView(Guid programGuid, Guid companyGuid)
        {
            ReportGridView.DataSource = Resolve<IReportService>().GetReportItems(programGuid, companyGuid, "", true);
            ReportGridView.DataBind();

            if(Resolve<IProgramService>().IsProgramSeparateGender(programGuid))
            {
                separatePanel.Visible = true;
                maleGridView.DataSource = Resolve<IReportService>().GetReportItems(programGuid, companyGuid, GenderEnum.Male.ToString(), true);
                maleGridView.DataBind();
                femaleGridView.DataSource = Resolve<IReportService>().GetReportItems(programGuid, companyGuid, GenderEnum.Female.ToString(), true);
                femaleGridView.DataBind();
            }
        }

        protected void editButton_Click(object sender, EventArgs e)
        {
            CompanyRightModel rightModel = new CompanyRightModel();
            rightModel.CompanyRightGUID = new Guid(Request.QueryString[Constants.QUERYSTR_COMPANY_RIGHT_GUID]);
            rightModel.CompanyName = companyTextBox.Text;
            rightModel.OverDueTime = Convert.ToDateTime(overdueTextBox.Text);
            rightModel.StartTime = Convert.ToDateTime(startTextBox.Text);
            rightModel.ContactPerson = contactPersonTextBox.Text;
            rightModel.InternalContact = internalContactTextBox.Text;
            rightModel.Mobile = mobileTextBox.Text;
            rightModel.Email = emailTextBox.Text;
            rightModel.StreetAddress = streetAddressTextBox.Text;
            rightModel.PostalAddress = postalAddressTextBox.Text;
            rightModel.ComayDescription = notesTextBox.Text;

            Resolve<ICompanyService>().UpdateCompanyRight(rightModel);
            Response.Redirect(Request.Url.PathAndQuery);
            //Response.Redirect(string.Format("EditCompany3rdParty.aspx?{0}={1}", Constants.QUERYSTR_COMPANY_RIGHT_GUID, Request.QueryString[Constants.QUERYSTR_COMPANY_RIGHT_GUID]));
        }

        protected void cancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("MaintenanceHome.aspx?{0}={1}&{2}={3}", Constants.QUERYSTR_PROGRAM_GUID, ProgramGUID, Constants.QUERYSTR_LANGUAGE_GUID, LanguageGUID));
        }

        protected void ShowUserButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("CompanyUsers.aspx?{0}={1}&{2}={3}&{4}={5}",
                Constants.QUERYSTR_COMPANY_RIGHT_GUID, Request.QueryString[Constants.QUERYSTR_COMPANY_RIGHT_GUID],
                Constants.QUERYSTR_PROGRAM_GUID, ProgramGUID,
                Constants.QUERYSTR_LANGUAGE_GUID, LanguageGUID));
        }
    }
}
