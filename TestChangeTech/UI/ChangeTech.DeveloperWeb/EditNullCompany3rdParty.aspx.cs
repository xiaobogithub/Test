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
    public partial class EditNullCompany3rdParty : PageBase<UserModel>
    {
        private string ProgramGuid { get { return Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]; } }
        private string LanguageGuid { get { return Request.QueryString[Constants.QUERYSTR_LANGUAGE_GUID]; } }

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                InitialPage();
              //  ((HyperLink)Master.FindControl("hpBackLink")).NavigateUrl = string.Format("MaintenanceHome.aspx?{0}={1}&{2}={3}", Constants.QUERYSTR_PROGRAM_GUID, ProgramGuid, Constants.QUERYSTR_LANGUAGE_GUID, LanguageGuid);
            }
        }

        private void InitialPage()
        {
            string serverPath = string.Empty;
            if (!string.IsNullOrEmpty(ProgramGuid) && !string.IsNullOrEmpty(LanguageGuid))
            {
                ProgramModel currentProgramModel = Resolve<IProgramService>().GetProgramByProgramGUIDAndLanguageGUID(new Guid(ProgramGuid), new Guid(LanguageGuid));
                programNameLabel.Text = currentProgramModel.ProgramName;
                versionLabel.Text = currentProgramModel.DefaultLanguageName;

                if (Resolve<IProgramService>().IsSupportHttps(currentProgramModel.Guid))
                {
                    serverPath = Resolve<ISystemSettingService>().GetSettingValue("WebServerPathHttps");
                }
                else
                {
                    serverPath = Resolve<ISystemSettingService>().GetSettingValue("WebServerPath");
                }
                screenurlHyperLink.NavigateUrl = string.Format(serverPath + "ChangeTech.html?Mode=Trial&{0}={1}", Constants.QUERYSTR_PROGRAM_CODE, currentProgramModel.Code);
                screenurlHyperLink.Text = screenurlHyperLink.NavigateUrl;
                loginHyperLink.NavigateUrl = string.Format(serverPath + "ChangeTech.html?Mode=Live&{0}={1}", Constants.QUERYSTR_PROGRAM_CODE, currentProgramModel.Code);
                loginHyperLink.Text = loginHyperLink.NavigateUrl;

                BindReportGridView(currentProgramModel.Guid);
            }
        }

        private void BindReportGridView(Guid programGuid)
        {
            ReportGridView.DataSource = Resolve<IReportService>().GetReportItems(programGuid, Guid.Empty, "", false);
            ReportGridView.DataBind();

            if(Resolve<IProgramService>().IsProgramSeparateGender(programGuid))
            {
                separatePanel.Visible = true;
                maleGridView.DataSource = Resolve<IReportService>().GetReportItems(programGuid, Guid.Empty, GenderEnum.Male.ToString(), false);
                maleGridView.DataBind();
                femaleGridView.DataSource = Resolve<IReportService>().GetReportItems(programGuid, Guid.Empty, GenderEnum.Female.ToString(), false);
                femaleGridView.DataBind();
            }
        }

        protected void ShowUserButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("UsersNotInCompany.aspx?{0}={1}&{2}={3}", Constants.QUERYSTR_PROGRAM_GUID, ProgramGuid, Constants.QUERYSTR_LANGUAGE_GUID, LanguageGuid));
        }
    }
}
