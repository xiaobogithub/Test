using System;
using System.Web.UI.WebControls;
using ChangeTech.Contracts;
using Ethos.DependencyInjection;
using ChangeTech.Models;
using Ethos.Utility;

namespace ChangeTech.DeveloperWeb
{
    public partial class EditPageSequence : PageBase<EditPageSequenceModel>
    {
        ProgramPropertyModel programPropertyModel;
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

        private string ProgramGUID
        {
            get
            {
                return Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID];
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString[Constants.QUERYSTR_PAGE_SEQUENCE_GUID]))
            {
                if (!IsPostBack)
                {
                    try
                    {
                        BindProgramRoom();
                        BindEditPageSequence();
                        InitialBackURL();
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

        private void InitialBackURL()
        {
            if (IsEditPageSequenceSelf())
            {
                ((HyperLink)Master.FindControl("menuLoginView").FindControl("hpBackLink")).NavigateUrl = string.Format("~/ManageRelapsePageSequence.aspx?{0}={1}&{2}={3}&{4}={5}",
                    Constants.QUERYSTR_PROGRAM_GUID, Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID],
                    Constants.QUERYSTR_PROGRAM_PAGE, ProgramPage,
                    Constants.QUERYSTR_CTPP_TYPE, Request.QueryString[Constants.QUERYSTR_CTPP_TYPE]);
                ((Literal)Master.FindControl("menuLoginView").FindControl("SiteMapPath").FindControl("ltlEditPageSequence")).Text += "(" + Model.Name + ")";
                ((HyperLink)Master.FindControl("menuLoginView").FindControl("SiteMapPath").FindControl("hpEditSessionLink")).Text = "Subroutine";
                ((HyperLink)Master.FindControl("menuLoginView").FindControl("SiteMapPath").FindControl("hpEditSessionLink")).NavigateUrl = string.Format("ManageRelapsePageSequence.aspx?{0}={1}&{2}={3}", Constants.QUERYSTR_PROGRAM_GUID, Model.ProgramGuid.ToString(), Constants.QUERYSTR_PROGRAM_PAGE, ProgramPage);
                ((HyperLink)Master.FindControl("menuLoginView").FindControl("SiteMapPath").FindControl("hpEditProgramLink")).Text = Model.ProgramName;
                ((HyperLink)Master.FindControl("menuLoginView").FindControl("SiteMapPath").FindControl("hpEditProgramLink")).NavigateUrl = string.Format("EditProgram.aspx?{0}={1}&{2}={3}&Page={4}",
                    Constants.QUERYSTR_PROGRAM_PAGE, ProgramPage,
                    Constants.QUERYSTR_PROGRAM_GUID, Model.ProgramGuid.ToString(),
                    SessionPage);
                ((HyperLink)Master.FindControl("menuLoginView").FindControl("SiteMapPath").FindControl("hpProgramLink")).NavigateUrl = string.Format("ListProgram.aspx?Page={0}", ProgramPage);
            }
            else
            {
                EditSessionModel editSessionModel = new EditSessionModel();
                PageSequenceModel pageSeqModel = new PageSequenceModel();
                if (!string.IsNullOrEmpty(SessionGUID))
                {
                    editSessionModel = Resolve<ISessionService>().GetSessionBySessonGuid(new Guid(SessionGUID));
                    pageSeqModel = Resolve<IPageSequenceService>().GetPageSequenceBySessionGuidAndPageSeqGuid(new Guid(SessionGUID), new Guid(Request.QueryString[Constants.QUERYSTR_PAGE_SEQUENCE_GUID]));
                }
                ((HyperLink)Master.FindControl("menuLoginView").FindControl("hpBackLink")).NavigateUrl = string.Format("~/EditSession.aspx?{0}={1}&{2}={3}&{4}={5}&Page={6}", Constants.QUERYSTR_PROGRAM_PAGE, ProgramPage, Constants.QUERYSTR_SESSION_PAGE, SessionPage, Constants.QUERYSTR_SESSION_GUID, SessionGUID, PageSequencePage);
                ((Literal)Master.FindControl("menuLoginView").FindControl("SiteMapPath").FindControl("ltlEditPageSequence")).Text = "Seq " + pageSeqModel.Order + ": " + pageSeqModel.Name;
                ((HyperLink)Master.FindControl("menuLoginView").FindControl("SiteMapPath").FindControl("hpEditSessionLink")).Text = "Day " + editSessionModel.Day + ": " + Model.SessionName;
                ((HyperLink)Master.FindControl("menuLoginView").FindControl("SiteMapPath").FindControl("hpEditSessionLink")).NavigateUrl = string.Format("EditSession.aspx?{0}={1}&{2}={3}&{4}={5}&Page={6}", Constants.QUERYSTR_PROGRAM_PAGE, ProgramPage, Constants.QUERYSTR_SESSION_PAGE, SessionPage, Constants.QUERYSTR_SESSION_GUID, SessionGUID, PageSequencePage);
                ((HyperLink)Master.FindControl("menuLoginView").FindControl("SiteMapPath").FindControl("hpEditProgramLink")).Text = Model.ProgramName;
                ((HyperLink)Master.FindControl("menuLoginView").FindControl("SiteMapPath").FindControl("hpEditProgramLink")).NavigateUrl = string.Format("EditProgram.aspx?{0}={1}&{2}={3}&Page={4}", Constants.QUERYSTR_PROGRAM_PAGE, ProgramPage, Constants.QUERYSTR_PROGRAM_GUID, Model.ProgramGuid.ToString(), SessionPage);
                ((HyperLink)Master.FindControl("menuLoginView").FindControl("SiteMapPath").FindControl("hpProgramLink")).NavigateUrl = string.Format("ListProgram.aspx?Page={0}", ProgramPage);
            }
        }

        // not find the place to set QUERYSTR_EDITMODE in this page, so I will always return false now.
        protected bool IsEditPageSequenceSelf()
        {
            bool flug = false;
            if (!string.IsNullOrEmpty(Request.QueryString[Constants.QUERYSTR_EDITMODE]) && Request.QueryString[Constants.QUERYSTR_EDITMODE].Equals(Constants.SELF))
            {
                flug = true;
            }

            return flug;
        }

        private void BindProgramRoom()
        {
            Guid programguid = Guid.Empty;
            if (IsEditPageSequenceSelf())
            {
                programguid = new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]);
            }
            else
            {
                programguid = Resolve<ISessionService>().GetSessionBySessonGuid(new Guid(SessionGUID)).ProgramGuid;
            }
            programPropertyModel = Resolve<IProgramService>().GetProgramProperty(programguid);
            ddlProgramRoom.DataSource =
               Resolve<IProgramRoomService>().GetRoomByProgram(programguid);
            ddlProgramRoom.DataBind();
            ddlProgramRoom.Items.Insert(0, new ListItem("", ""));
        }

        private void BindEditPageSequence()
        {
            if (IsEditPageSequenceSelf())
            {
                Model = Resolve<IPageSequenceService>().GetPageSequenceByProgramGuidPageSequenceGuid(new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]), new Guid(Request.QueryString[Constants.QUERYSTR_PAGE_SEQUENCE_GUID]));
                
            }
            else
            {
                Model = Resolve<IPageSequenceService>().GetPageSequenceBySessionGuidPageSequenceGuid(new Guid(SessionGUID)
                        , new Guid(Request.QueryString[Constants.QUERYSTR_PAGE_SEQUENCE_GUID]));
                if (string.IsNullOrEmpty(Request.QueryString["Option"]))//There is nowhere to add Option args now.
                {
                    hfFlagMoreReference.Value = Resolve<IPageSequenceService>().PageSequenceInMoreSession(new Guid(SessionGUID), new Guid(Request.QueryString[Constants.QUERYSTR_PAGE_SEQUENCE_GUID])).ToString();
                }
                else
                {
                    hfFlagMoreReference.Value = Resolve<IPageSequenceService>().PageSequenceReferenced(new Guid(Request.QueryString[Constants.QUERYSTR_PAGE_SEQUENCE_GUID])).ToString();
                }
            }
            txtPageSeqName.Text = Model.Name;
            txtPageSeqDescription.Text = Model.Description;
            ddlProgramRoom.SelectedValue = Model.ProgramRoomGuid.ToString();
            hfPageSeqID.Value = Model.ID.ToString();

            // for paging
            InitialPageInfo((int)Math.Ceiling(Convert.ToDouble(Model.Pages.Count) / Constants.PAGE_SIZE), "Order", "asc");
            string url = Request.Url.ToStringWithoutPort();
            PagingString = PagingSortingService.InitialPagingString(Request.Url.ToStringWithoutPort(), PageNumber, CurrentPageNumber, Resources.Share.Previous, Resources.Share.Next);

            rpPages.DataSource = PagingSortingService.GetCurrentPage<SimplePageContentModel>(Model.Pages, CurrentPageNumber, PageNumber, Constants.PAGE_SIZE, SortExpression + " " + SortOrder);
            rpPages.DataBind();

            int programSecuirty = 0;
            PermissionEnum applicationSecurity = (PermissionEnum)ContextService.CurrentAccount.Security;
            if (ContextService.CurrentAccount.ProgramSecuirty.ContainsKey(Model.ProgramGuid))
            {
                programSecuirty = ContextService.CurrentAccount.ProgramSecuirty[Model.ProgramGuid];
            }
            // program security
            if (Resolve<IUserService>().HasPermission((PermissionEnum)programSecuirty, PermissionEnum.ProgramCreate, applicationSecurity) &&
                !Model.IsLiveProgram)
            {
                ((Button)Master.FindControl("ContentPlaceHolder1").FindControl("btnAddPage")).Enabled = true;
            }

            UpdateControlStatusBasedOnProgramStatus(Model.IsLiveProgram);
        }

        private Guid BeforeEditPageSequence()
        {
            // updateFlag means if there are two or more reference, update or not
            Guid pagesequenceguid = Guid.Empty;
            if (IsEditPageSequenceSelf())
            {
                pagesequenceguid = new Guid(Request.QueryString[Constants.QUERYSTR_PAGE_SEQUENCE_GUID]);
            }
            else
            {
                bool affectFlag = Convert.ToBoolean(hfUpdatePageSeq.Value == "" ? "true" : hfUpdatePageSeq.Value);
                pagesequenceguid = Resolve<IPageSequenceService>().BeforeEditPageSequenceAfterRefactoring(new Guid(SessionGUID), new Guid(Request.QueryString[Constants.QUERYSTR_PAGE_SEQUENCE_GUID]), affectFlag);
            }

            return pagesequenceguid;
        }

        private string GetUrlAfterEditPageSequence(string url, string oldPageSequenceGuid, string newPageSequenceGuid)
        {
            return url.Replace(oldPageSequenceGuid, newPageSequenceGuid);
        }

        protected void btnUpdatePageSeq_Click(object sender, EventArgs e)
        {
            try
            {
                //Guid sequenceGuid = new Guid(Request.QueryString[Constants.QUERYSTR_PAGE_SEQUENCE_GUID]);
                if (IsEditPageSequenceSelf())
                {
                    Guid roomGuid = Guid.Empty;
                    if (ddlProgramRoom.SelectedIndex > 0)
                    {
                        roomGuid = new Guid(ddlProgramRoom.SelectedValue);
                    }
                    Resolve<IPageSequenceService>().UpdateRelapsePageSequence(new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]), new Guid(Request.QueryString[Constants.QUERYSTR_PAGE_SEQUENCE_GUID]), txtPageSeqName.Text, txtPageSeqDescription.Text, roomGuid);
                    Response.Redirect(Request.Url.ToString());
                }
                else
                {
                    Guid sequenceGuid = BeforeEditPageSequence();
                    Guid sessionGuid = new Guid(SessionGUID);
                    Guid RoomGuid;
                    if (ddlProgramRoom.SelectedValue != "")
                    {
                        RoomGuid = new Guid(ddlProgramRoom.SelectedValue);
                    }
                    else
                    {
                        RoomGuid = new Guid();
                    }
                    Resolve<IPageSequenceService>().UpdatePageSequence(sequenceGuid, sessionGuid, RoomGuid, txtPageSeqName.Text, txtPageSeqDescription.Text);
                    Response.Redirect(GetUrlAfterEditPageSequence(Request.Url.ToStringWithoutPort(), Request.QueryString[Constants.QUERYSTR_PAGE_SEQUENCE_GUID], sequenceGuid.ToString()));
                }
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        protected void btnUp_Click(object sender, EventArgs e)
        {
            try
            {
                Guid pageSeqGuid = BeforeEditPageSequence();
                int pageOrder = Convert.ToInt32(((Button)sender).CommandArgument);
                Guid pageGuid = GetPageGuid(pageSeqGuid, pageOrder);
                Resolve<IPageService>().AdjustPageOrderUp(pageSeqGuid, pageGuid);
                Response.Redirect(GetUrlAfterEditPageSequence(Request.Url.ToStringWithoutPort(), Request.QueryString[Constants.QUERYSTR_PAGE_SEQUENCE_GUID], pageSeqGuid.ToString()));
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        private Guid GetPageGuid(Guid pageSeqGuid, int pageOrder)
        {
            return Resolve<IPageService>().GetPageGuidByPageSequenceAndOrder(pageSeqGuid, pageOrder);
        }

        protected void btnDown_Click(object sender, EventArgs e)
        {
            try
            {
                int pageOrder = Convert.ToInt32(((Button)sender).CommandArgument);
                Guid pageSeqGuid = BeforeEditPageSequence();
                Guid pageGuid = GetPageGuid(pageSeqGuid, pageOrder);
                Resolve<IPageService>().AdjustPageOrderDown(pageSeqGuid, pageGuid);
                Response.Redirect(GetUrlAfterEditPageSequence(Request.Url.ToStringWithoutPort(), Request.QueryString[Constants.QUERYSTR_PAGE_SEQUENCE_GUID], pageSeqGuid.ToString()));
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

       
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            string pageGuid = ((Button)sender).CommandArgument;
            if (IsEditPageSequenceSelf())
            {
                Response.Redirect(string.Format("EditPage.aspx?{0}={1}&{2}={3}&{4}={5}&{6}={7}&{8}={9}&{10}={11}&{12}={13}&{14}={15}&{16}={17}&{18}={19}&{20}={21}",
                    Constants.QUERYSTR_EDITMODE, Constants.SELF,
                    Constants.QUERYSTR_PROGRAM_GUID, Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID],
                    Constants.QUERYSTR_PAGE_SEQUENCE_GUID, Request.QueryString[Constants.QUERYSTR_PAGE_SEQUENCE_GUID],
                    Constants.QUERYSTR_PAGE_GUID, pageGuid,
                    Constants.QUERYSTR_USER_GUID, ContextService.CurrentAccount.UserGuid,
                    //can't use Model as Model is null
                    //Constants.QUERYSTR_READONLY, Model.IsLiveProgram ? "True" : "False",
                    Constants.QUERYSTR_READONLY, txtPageSeqName.Enabled ? "False" : "True",
                    Constants.QUERYSTR_PROGRAM_PAGE, ProgramPage,
                    Constants.QUERYSTR_SESSION_PAGE, SessionPage,
                    Constants.QUERYSTR_PAGE_SEQUENCE_PAGE, PageSequencePage,
                    Constants.QUERYSTR_PAGE_PAGE, CurrentPageNumber,
                    Constants.QUERYSTR_CTPP_TYPE, Request.QueryString[Constants.QUERYSTR_CTPP_TYPE]));
            }
            else
            {
                Response.Redirect(string.Format("EditPage.aspx?{0}={1}&{2}={3}&{4}={5}&{6}={7}&{8}={9}&{10}={11}&{12}={13}&{14}={15}&{16}={17}",
                    Constants.QUERYSTR_SESSION_GUID, SessionGUID,
                    Constants.QUERYSTR_PAGE_SEQUENCE_GUID, Request.QueryString[Constants.QUERYSTR_PAGE_SEQUENCE_GUID],
                    Constants.QUERYSTR_PAGE_GUID, pageGuid,
                    Constants.QUERYSTR_USER_GUID, ContextService.CurrentAccount.UserGuid,
                    Constants.QUERYSTR_READONLY, txtPageSeqName.Enabled ? "False" : "True",
                    Constants.QUERYSTR_PROGRAM_PAGE, ProgramPage,
                    Constants.QUERYSTR_SESSION_PAGE, SessionPage,
                    Constants.QUERYSTR_PAGE_SEQUENCE_PAGE, PageSequencePage,
                    Constants.QUERYSTR_PAGE_PAGE, CurrentPageNumber));
            }
        }

        protected void btnAddPage_Click(object sender, EventArgs e)
        {
            if (IsEditPageSequenceSelf())
            {
                Response.Redirect(string.Format("AddPage.aspx?{0}={1}&{2}={3}&{4}={5}&{6}={7}&{8}={9}&{10}={11}&{12}={13}&{14}={15}&{16}={17}&{18}={19}",
                    Constants.QUERYSTR_EDITMODE, Constants.SELF,
                    Constants.QUERYSTR_PROGRAM_GUID, Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID],
                    Constants.QUERYSTR_PAGE_SEQUENCE_GUID, Request.QueryString[Constants.QUERYSTR_PAGE_SEQUENCE_GUID],
                    Constants.QUERYSTR_USER_GUID, ContextService.CurrentAccount.UserGuid,
                    Constants.QUERYSTR_PROGRAM_PAGE, ProgramPage,
                    Constants.QUERYSTR_SESSION_PAGE, SessionPage,
                    Constants.QUERYSTR_PAGE_SEQUENCE_PAGE, PageSequencePage,
                    Constants.QUERYSTR_PAGE_PAGE,CurrentPageNumber,
                    Constants.QUERYSTR_SESSION_GUID, Request.QueryString[Constants.QUERYSTR_SESSION_GUID],
                    Constants.QUERYSTR_CTPP_TYPE, Request.QueryString[Constants.QUERYSTR_CTPP_TYPE]));
            }
            else
            {
                Response.Redirect(string.Format("AddPage.aspx?{0}={1}&{2}={3}&{4}={5}&{6}={7}&{8}={9}&{10}={11}&{12}={13}",
                   Constants.QUERYSTR_SESSION_GUID, SessionGUID,
                   Constants.QUERYSTR_PAGE_SEQUENCE_GUID, Request.QueryString[Constants.QUERYSTR_PAGE_SEQUENCE_GUID],
                   Constants.QUERYSTR_USER_GUID, ContextService.CurrentAccount.UserGuid,
                   Constants.QUERYSTR_PROGRAM_PAGE, ProgramPage,
                   Constants.QUERYSTR_SESSION_PAGE, SessionPage,
                   Constants.QUERYSTR_PAGE_SEQUENCE_PAGE, PageSequencePage,
                   Constants.QUERYSTR_PAGE_PAGE, CurrentPageNumber));
            }
        }

        protected void rpPages_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            int programSecuirty = 0;
            PermissionEnum applicationSecurity = (PermissionEnum)ContextService.CurrentAccount.Security;
            if (ContextService.CurrentAccount.ProgramSecuirty.ContainsKey(Model.ProgramGuid))
            {
                programSecuirty = ContextService.CurrentAccount.ProgramSecuirty[Model.ProgramGuid];
            }

            if (e.Item.ItemType == ListItemType.Header)
            {
                //// program security
                //if (Resolve<IUserService>().HasPermission((PermissionEnum)programSecuirty, PermissionEnum.ProgramCreate, applicationSecurity) &&
                //    !Model.IsLiveProgram)
                //{
                //    ((Button)e.Item.FindControl("btnAddPage")).Enabled = true;
                //}
            }
            if (e.Item.DataItem != null)
            {
                Button upButton = (Button)e.Item.FindControl("ButtonUp");
                Button downButton = (Button)e.Item.FindControl("ButtonDown");
                if (e.Item.ItemIndex == 0 && this.CurrentPageNumber == 1)
                {
                    upButton.Visible = false;
                }
                if (PageNumber == CurrentPageNumber && e.Item.ItemIndex == Model.Pages.Count - ((PageNumber - 1) * Constants.PAGE_SIZE) - 1)
                {
                    downButton.Visible = false;
                }

                if (Model.IsLiveProgram)
                {
                    upButton.Enabled = false;
                    downButton.Enabled = false;
                }
                else
                {
                    upButton.Enabled = true;
                    downButton.Enabled = true;
                }

                // for popup page
                Button preview = (Button)e.Item.FindControl("btnPreview");
                string flashURL = string.Format("ChangeTechF.html?Mode=Preview&Object=Page&{0}={1}&{2}={3}&{4}={5}&{6}={7}&{8}={9}",
                    Constants.QUERYSTR_SESSION_GUID, GetSessionGUID(),
                    Constants.QUERYSTR_PAGE_SEQUENCE_GUID, Request.QueryString[Constants.QUERYSTR_PAGE_SEQUENCE_GUID],
                    Constants.QUERYSTR_PAGE_GUID, preview.CommandArgument,
                    Constants.QUERYSTR_LANGUAGE_GUID,
                    GetLanguageGUID(),
                    Constants.QUERYSTR_USER_GUID, ContextService.CurrentAccount.UserGuid);
               
                string html5URL = string.Format("ChangeTech5.html?Mode=Preview&Object=Page&{0}={1}&{2}={3}&{4}={5}&{6}={7}&{8}={9}",
                    Constants.QUERYSTR_SESSION_GUID, GetSessionGUID(),
                    Constants.QUERYSTR_PAGE_SEQUENCE_GUID, Request.QueryString[Constants.QUERYSTR_PAGE_SEQUENCE_GUID],
                    Constants.QUERYSTR_PAGE_GUID, preview.CommandArgument,
                    Constants.QUERYSTR_LANGUAGE_GUID,
                    GetLanguageGUID(),
                    Constants.QUERYSTR_USER_GUID, ContextService.CurrentAccount.UserGuid);

                string html5rURL = string.Format("ChangeTech5r.html?Mode=Preview&Object=Page&{0}={1}&{2}={3}&{4}={5}&{6}={7}&{8}={9}",
                    Constants.QUERYSTR_SESSION_GUID, GetSessionGUID(),
                    Constants.QUERYSTR_PAGE_SEQUENCE_GUID, Request.QueryString[Constants.QUERYSTR_PAGE_SEQUENCE_GUID],
                    Constants.QUERYSTR_PAGE_GUID, preview.CommandArgument,
                    Constants.QUERYSTR_LANGUAGE_GUID,
                    GetLanguageGUID(),
                    Constants.QUERYSTR_USER_GUID, ContextService.CurrentAccount.UserGuid);

                if (programPropertyModel.EnableHTML5NewUI)
                {
                    preview.Attributes.Add("OnClick", "return openPageD('" + flashURL + "','" + html5rURL + "');");
                }
                else if (programPropertyModel.IsHTML5PreviewEnable)
                {

                    preview.Attributes.Add("OnClick", "return openPageD('" + flashURL + "','" + html5URL + "');");
                }
                else
                {
                    preview.Attributes.Add("OnClick", "return openPage('" + flashURL + "');");
                }

                // program security
                if (Resolve<IUserService>().HasPermission((PermissionEnum)programSecuirty, PermissionEnum.ProgramDelete, applicationSecurity) &&
                    !Model.IsLiveProgram)
                {
                    DropDownList moreOptionsDDL = ((DropDownList)e.Item.FindControl("moreOptionsDDL"));
                    moreOptionsDDL.Enabled = true;
                }
                if (Resolve<IUserService>().HasPermission((PermissionEnum)programSecuirty, PermissionEnum.ProgramEdit, applicationSecurity))
                {
                    ((Button)e.Item.FindControl("btnEdit")).Enabled = true;
                    ((Button)e.Item.FindControl("btnPreview")).Enabled = true;
                }
                if (Resolve<IUserService>().HasPermission((PermissionEnum)programSecuirty, PermissionEnum.ProgramCreate, applicationSecurity) &&
                    !Model.IsLiveProgram)
                {
                    //((Button)e.Item.FindControl("btnMakeCopy")).Enabled = true;
                }
            }
        }

        protected void moreOptionsDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            string url = string.Empty;
            DropDownList moreOptionsDDL = (DropDownList)sender;
            string selectOption = moreOptionsDDL.SelectedValue;
            string pageGuid = moreOptionsDDL.DataValueField;
            Guid pageSeqGuid = BeforeEditPageSequence();
            switch (selectOption)
            {
                case Constants.MAKE_COPY://make copy
                    try
                    {
                        Resolve<IPageService>().MakeCopyPage(new Guid(pageGuid));
                        url = GetUrlAfterEditPageSequence(Request.Url.ToStringWithoutPort(), Request.QueryString[Constants.QUERYSTR_PAGE_SEQUENCE_GUID], pageSeqGuid.ToString());
                        Response.Redirect(url, false);
                    }
                    catch (Exception ex)
                    {
                        LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                        throw ex;
                    }
                    break;
                case Constants.DELETE:
                    Resolve<IPageService>().DeletePage(new Guid(pageGuid));
                    url = GetUrlAfterEditPageSequence(Request.Url.ToStringWithoutPort(), Request.QueryString[Constants.QUERYSTR_PAGE_SEQUENCE_GUID], pageSeqGuid.ToString());
                    Response.Redirect(url, false);
                    break;

                #region preview
                //case PREVIEW://preview
                //    string flashURL = string.Format("ChangeTechF.html?Mode=Preview&Object=Page&{0}={1}&{2}={3}&{4}={5}&{6}={7}&{8}={9}",
                //        Constants.QUERYSTR_SESSION_GUID, GetSessionGUID(),
                //        Constants.QUERYSTR_PAGE_SEQUENCE_GUID, Request.QueryString[Constants.QUERYSTR_PAGE_SEQUENCE_GUID],
                //        Constants.QUERYSTR_PAGE_GUID, pageGuid,
                //        Constants.QUERYSTR_LANGUAGE_GUID,
                //        GetLanguageGUID(),
                //        Constants.QUERYSTR_USER_GUID, ContextService.CurrentAccount.UserGuid);
                //    string html5URL = string.Format("ChangeTech5.html?Mode=Preview&Object=Page&{0}={1}&{2}={3}&{4}={5}&{6}={7}&{8}={9}",
                //        Constants.QUERYSTR_SESSION_GUID, GetSessionGUID(),
                //        Constants.QUERYSTR_PAGE_SEQUENCE_GUID, Request.QueryString[Constants.QUERYSTR_PAGE_SEQUENCE_GUID],
                //        Constants.QUERYSTR_PAGE_GUID, pageGuid,
                //        Constants.QUERYSTR_LANGUAGE_GUID,
                //        GetLanguageGUID(),
                //        Constants.QUERYSTR_USER_GUID, ContextService.CurrentAccount.UserGuid);
                //    Guid programGuid = Guid.Empty;
                //    if (IsEditPageSequenceSelf())
                //    {
                //        programGuid = new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]);
                //    }
                //    else
                //    {
                //        programGuid = Resolve<ISessionService>().GetSessionBySessonGuid(new Guid(SessionGUID)).ProgramGuid;
                //    }
                //    programPropertyModel = Resolve<IProgramService>().GetProgramProperty(programGuid);
                //    if (programPropertyModel.IsHTML5PreviewEnable)
                //    {
                //        Page.ClientScript.RegisterStartupScript(ClientScript.GetType(), "openUrl", "<script>window.open('" + flashURL + "');window.open('" + html5URL + "');</script>");
                //    }
                //    else
                //    {
                //        Page.ClientScript.RegisterStartupScript(ClientScript.GetType(), "openUrl", "<script>window.open('" + flashURL + "');</script>");
                //    }
                //    break; 
                #endregion
            }
        }

        private Guid GetLanguageGUID()
        {
            Guid languageGuid = Guid.Empty;
            if (ViewState["Language"] != null)
            {
                languageGuid = new Guid(ViewState["Language"].ToString());
            }
            else
            {
                if (IsEditPageSequenceSelf())
                {
                    languageGuid = Resolve<IProgramLanguageService>().GetDefaultProgramGUID(new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]));
                }
                else
                {
                    languageGuid = Resolve<IProgramLanguageService>().GetLanguageOfProgramBySessionGUID(new Guid(SessionGUID));
                }

                ViewState["Language"] = languageGuid;
            }

            return languageGuid;
        }

        private Guid GetSessionGUID()
        {
            Guid sessionguid = Guid.Empty;
            if (IsEditPageSequenceSelf())
            {
                if (ViewState["Session"] != null)
                {
                    sessionguid = new Guid(ViewState["Session"].ToString());
                }
                else
                {
                    sessionguid = Resolve<ISessionService>().GetFirstSessionGUID(new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]));
                    ViewState["Session"] = sessionguid;
                }
            }
            else
            {
                sessionguid = new Guid(SessionGUID);
            }

            return sessionguid;
        }

        private void UpdateControlStatusBasedOnProgramStatus(bool isLiveProgram)
        {
            if (isLiveProgram)
            {
                txtPageSeqName.Enabled = false;
                txtPageSeqDescription.Enabled = false;
                ddlProgramRoom.Enabled = false;
                btnUpdatePageSeq.Enabled = false;
                warnLbl.Visible = true;
            }
            else
            {
                txtPageSeqName.Enabled = true;
                txtPageSeqDescription.Enabled = true;
                ddlProgramRoom.Enabled = true;
                btnUpdatePageSeq.Enabled = true;
                warnLbl.Visible = false;
            }
        }

        #region Old follow

        //protected void btnDelete_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        int pageOrder = Convert.ToInt32(((Button)sender).CommandArgument);
        //        Guid pageSeqGuid = BeforeEditPageSequence();
        //        Guid pageGuid = GetPageGuid(pageSeqGuid, pageOrder);
        //        Resolve<IPageService>().DeletePage(pageGuid);
        //        Response.Redirect(GetUrlAfterEditPageSequence(Request.Url.ToStringWithoutPort(), Request.QueryString[Constants.QUERYSTR_PAGE_SEQUENCE_GUID], pageSeqGuid.ToString()));
        //    }
        //    catch (Exception ex)
        //    {
        //        LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
        //        throw ex;
        //    }
        //}

        //protected void btnMakeCopy_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        Guid pageSeqGuid = BeforeEditPageSequence();
        //        int pageOrder = Convert.ToInt32(((Button)sender).CommandArgument);
        //        Guid pageGuid = GetPageGuid(pageSeqGuid, pageOrder);
        //        Resolve<IPageService>().MakeCopyPage(pageGuid);
        //        Response.Redirect(GetUrlAfterEditPageSequence(Request.Url.ToStringWithoutPort(), Request.QueryString[Constants.QUERYSTR_PAGE_SEQUENCE_GUID], pageSeqGuid.ToString()));
        //    }
        //    catch (Exception ex)
        //    {
        //        LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
        //        throw ex;
        //    }
        //}

        #endregion
    }
}
