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
    public partial class EditCompanyRight : PageBase<CompanyRightModel>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                InitialPageInfo();
               // ((HyperLink)Master.FindControl("hpBackLink")).NavigateUrl = string.Format("ManageUserCompany.aspx?{0}={1}", Constants.QUERYSTR_PROGRAM_GUID, Model.ProgramGUID);
            }
        }

        private void InitialPageInfo()
        {
            Model = Resolve<ICompanyService>().GetCompanyRightModelByGuid(new Guid(Request.QueryString[Constants.QUERYSTR_COMPANY_RIGHT_GUID]));

            string serverPath = string.Empty;
            if(Model.IsSupportHttps)
            {
                serverPath = Resolve<ISystemSettingService>().GetSettingValue("WebServerPathHttps");
            }
            else
            {
                serverPath = Resolve<ISystemSettingService>().GetSettingValue("WebServerPath");
            }
            programNameLabel.Text = Model.ProgramName;
            companyLabel.Text = Model.CompanyName;
            overdueTextBox.Text = Model.OverDueTime.ToString("yyyy-MM-dd");
            versionLabel.Text = Model.Language;
            screenurlHyperLink.NavigateUrl = string.Format(serverPath + "ChangeTech.html?Mode=Trial&{0}={1}&{2}={3}", Constants.QUERYSTR_PROGRAM_CODE, Model.ProgramCode, Constants.QUERYSTR_COMPANY_CODE, Model.CompanyCode);
            screenurlHyperLink.Text = screenurlHyperLink.NavigateUrl;
            loginHyperLink.NavigateUrl = string.Format(serverPath + "ChangeTech.html?Mode=Live&{0}={1}&{2}={3}", Constants.QUERYSTR_PROGRAM_CODE, Model.ProgramCode, Constants.QUERYSTR_COMPANY_CODE, Model.CompanyCode);
            loginHyperLink.Text = loginHyperLink.NavigateUrl;
        }

        protected void updateButton_Click(object sender, EventArgs e)
        {
            Resolve<ICompanyService>().UpdateCompanyRightOverdueTime(Convert.ToDateTime(overdueTextBox.Text), new Guid(Request.QueryString[Constants.QUERYSTR_COMPANY_RIGHT_GUID]));
        }
    }
}
