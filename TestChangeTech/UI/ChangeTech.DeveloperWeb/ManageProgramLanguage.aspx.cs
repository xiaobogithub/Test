using System;
using System.Web.UI.WebControls;
using ChangeTech.Contracts;
using ChangeTech.Models;
using Ethos.DependencyInjection;

namespace ChangeTech.DeveloperWeb
{
    public partial class ManageProgramLanguage : PageBase<ModelBase>
    {
        public string VersionNumberWithoutDot
        {
            get
            {
                string version = System.Configuration.ConfigurationManager.AppSettings["ProjectVersionWithoutDot"];
                return version;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]))
            {
                if (!IsPostBack)
                {
                    ProgramModel programModel = Resolve<IProgramService>().GetProgramByGUID(
                        new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]));
                    programNameLbl.Text = programModel.ProgramName;
                    string azureAccountName = Resolve<ISystemSettingService>().GetSettingValue("AzureStorageAccountName");
                    ManageLanguageSliverLight.InitParameters = string.Format("Mode=Language, Azure={0}, Program={1}", azureAccountName, programModel.ProgramName);
                    ManageLanguageSliverLight.Source = string.Format("~/ClientBin/ChangeTech.Silverlight.DesignPage.xap?Version={0}", VersionNumberWithoutDot);
                    ((HyperLink)Master.FindControl("menuLoginView").FindControl("hpBackLink")).NavigateUrl = string.Format("~/EditProgram.aspx?{0}={1}", Constants.QUERYSTR_PROGRAM_GUID, Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]);
                }
            }
            else
            {
                Response.Redirect("InvalidUrl.aspx");
            }
        }
    }
}
