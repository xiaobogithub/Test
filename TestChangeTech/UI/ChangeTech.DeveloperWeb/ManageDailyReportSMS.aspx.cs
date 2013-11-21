using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ChangeTech.DeveloperWeb
{
    public partial class ManageDailyReportSMS : System.Web.UI.Page
    {
        private string ProgramGUID
        {
            get
            {
                return Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID];
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //string azureAccountName = Resolve<ISystemSettingService>().GetSettingValue("AzureStorageAccountName");
            //Silverlight1.InitParameters = "Mode=CTPPPresenterImage,Azure=" + azureAccountName;

            if (!string.IsNullOrEmpty(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]))
            {
                if (!IsPostBack)
                {
                    ((HyperLink)Master.FindControl("menuLoginView").FindControl("hpBackLink")).NavigateUrl = string.Format("EditProgram.aspx?{0}={1}", Constants.QUERYSTR_PROGRAM_GUID, ProgramGUID);
                    ((HyperLink)Master.FindControl("menuLoginView").FindControl("SiteMapPath").FindControl("hpEditProgramLink")).NavigateUrl = string.Format("EditProgram.aspx?{0}={1}", Constants.QUERYSTR_PROGRAM_GUID, ProgramGUID);
                    ((HyperLink)Master.FindControl("menuLoginView").FindControl("SiteMapPath").FindControl("hpProgramLink")).NavigateUrl = string.Format("ListProgram.aspx");
                }
            }
            else
            {
                Response.Redirect("InvalidUrl.aspx");
            }
        }
    }
}