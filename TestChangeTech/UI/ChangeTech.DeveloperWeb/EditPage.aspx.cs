using System;
using System.Web.UI.WebControls;
using ChangeTech.Contracts;
using Ethos.DependencyInjection;
using ChangeTech.Models;
using Ethos.Utility;
using System.Web.Configuration;

namespace ChangeTech.DeveloperWeb
{
    public partial class EditPage : PageBase<EditPageModel>
    {
        public const string PAGEREVIEWPRESENTERMODE = "PagePresenterImage";
        public string VersionNumberWithoutDot
        {
            get
            {
                string version = System.Configuration.ConfigurationManager.AppSettings["ProjectVersionWithoutDot"];
                return version;
            }
        }
        #region private variable
        private string PageGUID
        {
            get
            {
                return Request.QueryString[Constants.QUERYSTR_PAGE_GUID];
            }
        }

        private string PageSequenceGUID
        {
            get
            {
                return Request.QueryString[Constants.QUERYSTR_PAGE_SEQUENCE_GUID];
            }
        }

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

        private string PageSequencePage
        {
            get
            {
                return Request.QueryString[Constants.QUERYSTR_PAGE_SEQUENCE_PAGE];
            }
        }

        private string PagePage
        {
            get
            {
                return Request.QueryString[Constants.QUERYSTR_PAGE_PAGE];
            }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString[Constants.QUERYSTR_PAGE_GUID]) &&
                !string.IsNullOrEmpty(Request.QueryString[Constants.QUERYSTR_PAGE_SEQUENCE_GUID]))
            {
                if (!IsPostBack)
                {
                    if (Request.QueryString[Constants.QUERYSTR_READONLY].Equals("True"))
                    {
                        warnLbl.Visible = true;
                    }
                    EditSessionModel editSessionModel = new EditSessionModel();
                    PageSequenceModel pageSeqModel = new PageSequenceModel();
                    if (!string.IsNullOrEmpty(SessionGUID))
                    {
                        editSessionModel = Resolve<ISessionService>().GetSessionBySessonGuid(new Guid(SessionGUID));
                        pageSeqModel = Resolve<IPageSequenceService>().GetPageSequenceBySessionGuidAndPageSeqGuid(new Guid(SessionGUID), new Guid(PageSequenceGUID));
                    }
                    SimplePageModel simplePageModel = Resolve<IPageService>().GetSimplePageModel(new Guid(PageGUID));
                    if (!string.IsNullOrEmpty(Request.QueryString[Constants.QUERYSTR_EDITMODE]) &&
                        Request.QueryString[Constants.QUERYSTR_EDITMODE].Equals("Self"))
                    {
                        Model = Resolve<IPageService>().GetEditPageModelOfRelapsePageSeqence(new Guid(PageGUID), new Guid(PageSequenceGUID), new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]));

                        //((HyperLink)Master.FindControl("menuLoginView").FindControl("hpBackLink")).NavigateUrl = string.Format("EditPageSequence.aspx?{0}={1}&{2}={3}&{4}={5}&Page={6}&{7}={8}&{9}={10}",
                        ////   Constants.QUERYSTR_PROGRAM_PAGE, ProgramPage,
                        ////   Constants.QUERYSTR_SESSION_PAGE, SessionPage,
                        ////   Constants.QUERYSTR_PAGE_SEQUENCE_GUID, PageSequenceGUID,
                        ////   PagePage,
                        ////   Constants.QUERYSTR_EDITMODE, "Self",
                        ////   Constants.QUERYSTR_CTPP_TYPE, Request.QueryString[Constants.QUERYSTR_CTPP_TYPE]);

                        ((HyperLink)Master.FindControl("menuLoginView").FindControl("hpBackLink")).NavigateUrl = Request.Url.AbsoluteUri.Replace("EditPage.aspx", "EditPageSequence.aspx");
                        ((Literal)Master.FindControl("menuLoginView").FindControl("SiteMapPath").FindControl("ltlEditPage")).Text = "Page " + simplePageModel.PageOrderNo + ": " + simplePageModel.PageHeading;
                        ((HyperLink)Master.FindControl("menuLoginView").FindControl("SiteMapPath").FindControl("hpEditPageSequence")).Text = "Seq " + pageSeqModel.Order + ": " + (string.IsNullOrEmpty(pageSeqModel.Name) ? Model.PageSequenceName : pageSeqModel.Name);
                        ((HyperLink)Master.FindControl("menuLoginView").FindControl("SiteMapPath").FindControl("hpEditPageSequence")).NavigateUrl = string.Format("EditPageSequence.aspx?{0}={1}&{2}={3}&{4}={5}&Page={6}&{7}={8}&{9}={10}",
                            Constants.QUERYSTR_PROGRAM_PAGE, ProgramPage,
                            Constants.QUERYSTR_SESSION_PAGE, SessionPage,
                            Constants.QUERYSTR_PAGE_SEQUENCE_GUID, PageSequenceGUID,
                            PagePage,
                            Constants.QUERYSTR_EDITMODE, "Self",
                            Constants.QUERYSTR_PROGRAM_GUID, Model.ProgramGUID);
                        ((HyperLink)Master.FindControl("menuLoginView").FindControl("SiteMapPath").FindControl("hpEditSessionLink")).Text = "Subroutine";
                        ((HyperLink)Master.FindControl("menuLoginView").FindControl("SiteMapPath").FindControl("hpEditSessionLink")).NavigateUrl = string.Format("ManageRelapsePageSequence.aspx?{0}={1}&{2}={3}&{4}={5}", Constants.QUERYSTR_PROGRAM_GUID, Model.ProgramGUID.ToString(), Constants.QUERYSTR_PROGRAM_PAGE, ProgramPage, Constants.QUERYSTR_SESSION_PAGE, SessionPage);
                        ((HyperLink)Master.FindControl("menuLoginView").FindControl("SiteMapPath").FindControl("hpEditProgramLink")).Text = Model.ProgramName;
                        ((HyperLink)Master.FindControl("menuLoginView").FindControl("SiteMapPath").FindControl("hpEditProgramLink")).NavigateUrl = string.Format("EditProgram.aspx?{0}={1}&{2}={3}&Page={4}", Constants.QUERYSTR_PROGRAM_PAGE, ProgramPage, Constants.QUERYSTR_PROGRAM_GUID, Model.ProgramGUID.ToString(), SessionPage);
                        ((HyperLink)Master.FindControl("menuLoginView").FindControl("SiteMapPath").FindControl("hpProgramLink")).NavigateUrl = string.Format("ListProgram.aspx?Page={0}", ProgramPage);
                    }
                    else
                    {
                        if ((!string.IsNullOrEmpty(Request.QueryString[Constants.QUERYSTR_PREVIOUSPAGE])))
                        {
                            switch (Request.QueryString[Constants.QUERYSTR_PREVIOUSPAGE])
                            {
                                case "PageReview":
                                    Model = Resolve<IPageService>().GetEditPageModel(new Guid(PageGUID), new Guid(SessionGUID), new Guid(PageSequenceGUID));

                                    //((HyperLink)Master.FindControl("menuLoginView").FindControl("hpBackLink")).NavigateUrl = string.Format("PageReview.aspx?{0}={1}&{2}={3}&{4}={5}&{6}={7}",
                                    ////    Constants.QUERYSTR_PROGRAM_PAGE, ProgramPage,
                                    ////    Constants.QUERYSTR_SESSION_PAGE, SessionPage,
                                    ////    Constants.QUERYSTR_SESSION_GUID, SessionGUID,
                                    ////    Constants.QUERYSTR_PRESENTERIMAGE_MODE, PAGEREVIEWPRESENTERMODE);
                                    ((HyperLink)Master.FindControl("menuLoginView").FindControl("hpBackLink")).NavigateUrl = Request.Url.AbsoluteUri.Replace("EditPage.aspx", "EditPageSequence.aspx");
                                    ((Literal)Master.FindControl("menuLoginView").FindControl("SiteMapPath").FindControl("ltlEditPage")).Text = "Page " + simplePageModel.PageOrderNo + ": " + simplePageModel.PageHeading;
                                    ((HyperLink)Master.FindControl("menuLoginView").FindControl("SiteMapPath").FindControl("hpEditPageSequence")).Visible = false;
                                    ((HyperLink)Master.FindControl("menuLoginView").FindControl("SiteMapPath").FindControl("hpEditSessionLink")).Visible = false;
                                    //((HyperLink)Master.FindControl("menuLoginView").FindControl("SiteMapPath").FindControl("hpEditPageSequence")).Text = "Seq " + pageSeqModel.Order + ": " + pageSeqModel.Name;
                                    //((HyperLink)Master.FindControl("menuLoginView").FindControl("SiteMapPath").FindControl("hpEditSessionLink")).Text = "Day " + editSessionModel.Day + ": " + Model.SessionName;
                                    ((HyperLink)Master.FindControl("menuLoginView").FindControl("SiteMapPath").FindControl("hpEditProgramLink")).Text = Model.ProgramName;
                                    ((HyperLink)Master.FindControl("menuLoginView").FindControl("SiteMapPath").FindControl("hpEditProgramLink")).NavigateUrl = string.Format("EditProgram.aspx?{0}={1}&{2}={3}&Page={4}", Constants.QUERYSTR_PROGRAM_PAGE, ProgramPage, Constants.QUERYSTR_PROGRAM_GUID, Model.ProgramGUID.ToString(), SessionPage);
                                    ((HyperLink)Master.FindControl("menuLoginView").FindControl("SiteMapPath").FindControl("hpProgramLink")).NavigateUrl = string.Format("ListProgram.aspx?Page={0}", ProgramPage);
                                    break;
                            }
                        }
                        else
                        {
                            Model = Resolve<IPageService>().GetEditPageModel(new Guid(PageGUID), new Guid(SessionGUID), new Guid(PageSequenceGUID));

                            //((HyperLink)Master.FindControl("menuLoginView").FindControl("hpBackLink")).NavigateUrl = string.Format("EditPageSequence.aspx?{0}={1}&{2}={3}&{4}={5}&{6}={7}&{8}={9}&Page={10}",
                            ////    Constants.QUERYSTR_PROGRAM_PAGE, ProgramPage,
                            ////    Constants.QUERYSTR_SESSION_PAGE, SessionPage,
                            ////    Constants.QUERYSTR_PAGE_SEQUENCE_PAGE, PageSequencePage,
                            ////    Constants.QUERYSTR_SESSION_GUID, SessionGUID,
                            ////    Constants.QUERYSTR_PAGE_SEQUENCE_GUID, PageSequenceGUID,
                            ////    PagePage);
                            ((HyperLink)Master.FindControl("menuLoginView").FindControl("hpBackLink")).NavigateUrl = Request.Url.AbsoluteUri.Replace("EditPage.aspx", "EditPageSequence.aspx");
                            ((Literal)Master.FindControl("menuLoginView").FindControl("SiteMapPath").FindControl("ltlEditPage")).Text = "Page " + simplePageModel.PageOrderNo + ": " + simplePageModel.PageHeading;
                            ((HyperLink)Master.FindControl("menuLoginView").FindControl("SiteMapPath").FindControl("hpEditPageSequence")).Text = "Seq " + pageSeqModel.Order + ": " + pageSeqModel.Name;
                            ((HyperLink)Master.FindControl("menuLoginView").FindControl("SiteMapPath").FindControl("hpEditPageSequence")).NavigateUrl = string.Format("EditPageSequence.aspx?{0}={1}&{2}={3}&{4}={5}&{6}={7}&{8}={9}&Page={10}",
                                Constants.QUERYSTR_PROGRAM_PAGE, ProgramPage,
                                Constants.QUERYSTR_SESSION_PAGE, SessionPage,
                                Constants.QUERYSTR_PAGE_SEQUENCE_PAGE, PageSequencePage,
                                Constants.QUERYSTR_SESSION_GUID, SessionGUID,
                                Constants.QUERYSTR_PAGE_SEQUENCE_GUID, PageSequenceGUID,
                                PagePage);
                            ((HyperLink)Master.FindControl("menuLoginView").FindControl("SiteMapPath").FindControl("hpEditSessionLink")).Text = "Day " + editSessionModel.Day + ": " + Model.SessionName;
                            ((HyperLink)Master.FindControl("menuLoginView").FindControl("SiteMapPath").FindControl("hpEditSessionLink")).NavigateUrl = string.Format("EditSession.aspx?{0}={1}&{2}={3}&{4}={5}&Page={6}", Constants.QUERYSTR_PROGRAM_PAGE, ProgramPage, Constants.QUERYSTR_SESSION_PAGE, SessionPage, Constants.QUERYSTR_SESSION_GUID, SessionGUID, PageSequencePage);
                            ((HyperLink)Master.FindControl("menuLoginView").FindControl("SiteMapPath").FindControl("hpEditProgramLink")).Text = Model.ProgramName;
                            ((HyperLink)Master.FindControl("menuLoginView").FindControl("SiteMapPath").FindControl("hpEditProgramLink")).NavigateUrl = string.Format("EditProgram.aspx?{0}={1}&{2}={3}&Page={4}", Constants.QUERYSTR_PROGRAM_PAGE, ProgramPage, Constants.QUERYSTR_PROGRAM_GUID, Model.ProgramGUID.ToString(), SessionPage);
                            ((HyperLink)Master.FindControl("menuLoginView").FindControl("SiteMapPath").FindControl("hpProgramLink")).NavigateUrl = string.Format("ListProgram.aspx?Page={0}", ProgramPage);
                        }
                    }
                    string azureAccountName = Resolve<ISystemSettingService>().GetSettingValue("AzureStorageAccountName");
                    EditPageSliverLight.InitParameters = "Mode=Edit,Azure=" + azureAccountName;
                    //System.Configuration.ConfigurationManager.AppSettings[""]
                    EditPageSliverLight.Source = string.Format("~/ClientBin/ChangeTech.Silverlight.DesignPage.xap?Version={0}", VersionNumberWithoutDot);
                }
            }
            else
            {
                Response.Redirect("InvalidUrl.aspx");
            }
        }

        protected void lnkBtnNextPage_Click(object sender, EventArgs e)
        {
            Guid currentPageGuid = new Guid(Request.QueryString[Constants.QUERYSTR_PAGE_GUID]);
            Guid nextPageGuid = Guid.Empty;
            try
            {
                nextPageGuid = Resolve<IPageService>().GetNextPage(currentPageGuid);
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }

            if (nextPageGuid != Guid.Empty)
            {
                Response.Redirect(Request.Url.ToStringWithoutPort().Replace(currentPageGuid.ToString(), nextPageGuid.ToString()));
            }
            else
            {
                Response.Write(string.Format("<script>alert('{0}')</script>", "This is already the last page."));
            }
        }

        protected void lnkBtnPreviousPage_Click(object sender, EventArgs e)
        {
            Guid currentPageGuid = new Guid(Request.QueryString[Constants.QUERYSTR_PAGE_GUID]);
            Guid previousPageGuid = Guid.Empty;
            try
            {
                previousPageGuid = Resolve<IPageService>().GetPrevioursPage(currentPageGuid);
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }

            if (previousPageGuid != Guid.Empty)
            {
                Response.Redirect(Request.Url.ToStringWithoutPort().Replace(currentPageGuid.ToString(), previousPageGuid.ToString()));
            }
            else
            {
                Response.Write(string.Format("<script>alert('{0}')</script>", "This is already the first page."));
            }
        }
    }
}
