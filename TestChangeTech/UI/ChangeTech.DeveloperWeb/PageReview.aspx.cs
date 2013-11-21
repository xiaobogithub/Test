using System;
using System.Web.UI.WebControls;
using ChangeTech.Contracts;
using Ethos.DependencyInjection;
using ChangeTech.Models;
using Ethos.Utility;

namespace ChangeTech.DeveloperWeb
{
    public partial class PageReview : PageBase<EditSessionModel>
    {
        private string SessionGUID
        {
            get
            {
                return Request.QueryString[Constants.QUERYSTR_SESSION_GUID];
            }
        }

        private string ProgramPage
        {
            get
            {
                return Request.QueryString[Constants.QUERYSTR_PROGRAM_PAGE];
            }
        }

        private string SessionPage
        {
            get
            {
                return Request.QueryString[Constants.QUERYSTR_SESSION_PAGE];
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //string azureAccountName = Resolve<ISystemSettingService>().GetSettingValue("AzureStorageAccountName");
            //Silverlight1.InitParameters = "Mode=CTPPPresenterImage,Azure=" + azureAccountName;

            if (!string.IsNullOrEmpty(Request.QueryString[Constants.QUERYSTR_SESSION_GUID]))
            {
                if (!IsPostBack)
                {
                    try
                    {
                        Guid sessionGuid = new Guid(Request.QueryString[Constants.QUERYSTR_SESSION_GUID]);
                        Model = Resolve<ISessionService>().GetSessionBySessonGuid(sessionGuid);
                        //BindSessionModel();
                        //hfProgramGuid.Value = Model.ProgramGuid.ToString();
                    }
                    catch (Exception ex)
                    {
                        LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                        throw ex;
                    }
                    ((HyperLink)Master.FindControl("menuLoginView").FindControl("hpBackLink")).NavigateUrl = string.Format("EditProgram.aspx?{0}={1}&{2}={3}&Page={4}", Constants.QUERYSTR_PROGRAM_PAGE, ProgramPage, Constants.QUERYSTR_PROGRAM_GUID, Model.ProgramGuid.ToString(), SessionPage);
                    ((Literal)Master.FindControl("menuLoginView").FindControl("SiteMapPath").FindControl("ltlPageReview")).Text += "(" + Model.Name + ")";
                    ((HyperLink)Master.FindControl("menuLoginView").FindControl("SiteMapPath").FindControl("hpEditProgramLink")).Text += "(" + Model.ProgramName + ")";
                    ((HyperLink)Master.FindControl("menuLoginView").FindControl("SiteMapPath").FindControl("hpEditProgramLink")).NavigateUrl = string.Format("EditProgram.aspx?{0}={1}&{2}={3}&Page={4}", Constants.QUERYSTR_PROGRAM_PAGE, ProgramPage, Constants.QUERYSTR_PROGRAM_GUID, Model.ProgramGuid.ToString(), SessionPage);
                    ((HyperLink)Master.FindControl("menuLoginView").FindControl("SiteMapPath").FindControl("hpProgramLink")).NavigateUrl = string.Format("ListProgram.aspx?Page={0}", ProgramPage);

                }
            }
            else
            {
                Response.Redirect("InvalidUrl.aspx");
            }
        }

        protected void btnAddSeq_Click(object sender, EventArgs e)
        {
            string sessionGuid = Request.QueryString[Constants.QUERYSTR_SESSION_GUID];
            Response.Redirect(string.Format("AddPageSequence.aspx?{0}={1}&{2}={3}&{4}={5}&{6}=PageReview", Constants.QUERYSTR_PROGRAM_PAGE, ProgramPage, Constants.QUERYSTR_SESSION_PAGE, SessionPage, Constants.QUERYSTR_SESSION_GUID, sessionGuid, Constants.QUERYSTR_PREVIOUSPAGE));
        }
    }
}