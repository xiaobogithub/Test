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
    public partial class AddCompay3rdParty : PageBase<CompanyRightModel>
    {
        private string ProgramGUID { get { return Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]; } }
        private string LanguageGUID { get { return Request.QueryString[Constants.QUERYSTR_LANGUAGE_GUID]; } }

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
               //((HyperLink)Master.FindControl("menuLoginView").FindControl("hpBackLink")).NavigateUrl = string.Format("MaintenanceHome.aspx?{0}={1}&{2}={3}", Constants.QUERYSTR_PROGRAM_GUID, ProgramGUID, Constants.QUERYSTR_LANGUAGE_GUID, LanguageGUID);
            }
        }

        protected void addButton_Click(object sender, EventArgs e)
        {
            ProgramModel program = Resolve<IProgramService>().GetProgramByProgramGUIDAndLanguageGUID(new Guid(ProgramGUID), new Guid(LanguageGUID));
            CompanyRightModel rightModel = new CompanyRightModel();
            rightModel.CompanyGUID = Guid.NewGuid();
            rightModel.CompanyRightGUID = Guid.NewGuid();
            rightModel.CompanyName = companyTextBox.Text;
            rightModel.OverDueTime = Convert.ToDateTime(overdueTextBox.Text);
            rightModel.StartTime = Convert.ToDateTime(startTextBox.Text);
            rightModel.ProgramGUID = program.Guid;
            rightModel.ContactPerson = contactPersonTextBox.Text;
            rightModel.InternalContact = internalContactTextBox.Text;
            rightModel.Mobile = mobileTextBox.Text;
            rightModel.Email = emailTextBox.Text;
            rightModel.StreetAddress = streetAddressTextBox.Text;
            rightModel.PostalAddress = postalAddressTextBox.Text;
            rightModel.ComayDescription = notesTextBox.Text;

            Resolve<ICompanyService>().AddCompany(rightModel);
            Response.Redirect(string.Format("MaintenanceHome.aspx?{0}={1}&{2}={3}", Constants.QUERYSTR_PROGRAM_GUID, ProgramGUID, Constants.QUERYSTR_LANGUAGE_GUID, LanguageGUID));
        }

        protected void cancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("MaintenanceHome.aspx?{0}={1}&{2}={3}", Constants.QUERYSTR_PROGRAM_GUID, ProgramGUID, Constants.QUERYSTR_LANGUAGE_GUID, LanguageGUID));
        }
    }
}
