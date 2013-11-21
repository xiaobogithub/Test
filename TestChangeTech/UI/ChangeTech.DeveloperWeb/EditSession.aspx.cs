using System;
using System.Web.UI.WebControls;
using ChangeTech.Contracts;
using Ethos.DependencyInjection;
using ChangeTech.Models;
using Ethos.Utility;

namespace ChangeTech.DeveloperWeb
{
    public partial class EditSession : PageBase<EditSessionModel>
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString[Constants.QUERYSTR_SESSION_GUID]))
            {
                if (!IsPostBack)
                {
                    try
                    {
                        BindSessionModel();
                        hfProgramGuid.Value = Model.ProgramGuid.ToString();
                    }
                    catch (Exception ex)
                    {
                        LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                        throw ex;
                    }
                    ((HyperLink)Master.FindControl("menuLoginView").FindControl("hpBackLink")).NavigateUrl = string.Format("EditProgram.aspx?{0}={1}&{2}={3}&Page={4}", Constants.QUERYSTR_PROGRAM_PAGE, ProgramPage, Constants.QUERYSTR_PROGRAM_GUID, Model.ProgramGuid.ToString(), SessionPage);
                    ((Literal)Master.FindControl("menuLoginView").FindControl("SiteMapPath").FindControl("ltlEditSession")).Text = "Day " + Model.Day + ": " + Model.Name;
                    ((HyperLink)Master.FindControl("menuLoginView").FindControl("SiteMapPath").FindControl("hpEditProgramLink")).Text = Model.ProgramName;
                    ((HyperLink)Master.FindControl("menuLoginView").FindControl("SiteMapPath").FindControl("hpEditProgramLink")).NavigateUrl = string.Format("EditProgram.aspx?{0}={1}&{2}={3}&Page={4}", Constants.QUERYSTR_PROGRAM_PAGE, ProgramPage, Constants.QUERYSTR_PROGRAM_GUID, Model.ProgramGuid.ToString(), SessionPage);
                    ((HyperLink)Master.FindControl("menuLoginView").FindControl("SiteMapPath").FindControl("hpProgramLink")).NavigateUrl = string.Format("ListProgram.aspx?Page={0}", ProgramPage);

                }
            }
            else
            {
                Response.Redirect("InvalidUrl.aspx");
            }
        }

        #region Private methods

        private void FormatDDL()
        {
            int days = Resolve<ISessionService>().GetNumberOfSession(Model.ProgramGuid);
            this.ddlDay.Items.Clear();
            if (Model.Day >= 0)
            {
                for (int i = 0; i < days; i++)
                {
                    ddlDay.Items.Add(i.ToString());
                }
            }
            else
            {
                for (int i = -5; i < 0; i++)
                {
                    ddlDay.Items.Add(i.ToString());
                }
            }

            ddlDay.SelectedValue = days.ToString();
        }

        private void BindSessionModel()
        {
            Guid sessionGuid = new Guid(Request.QueryString[Constants.QUERYSTR_SESSION_GUID]);
            Model = Resolve<ISessionService>().GetSessionBySessonGuid(sessionGuid);
            programPropertyModel = Resolve<IProgramService>().GetProgramProperty(Model.ProgramGuid);
            FormatDDL();
            //lblTitle.Text = Model.ProgramName + ", " + Model.Name;
            // set program security
            int programSecuirty = 0;
            if (ContextService.CurrentAccount.ProgramSecuirty.ContainsKey(Model.ProgramGuid))
            {
                programSecuirty = ContextService.CurrentAccount.ProgramSecuirty[Model.ProgramGuid];
            }

            if (Resolve<IUserService>().HasPermission((PermissionEnum)programSecuirty, PermissionEnum.ProgramEdit, (PermissionEnum)ContextService.CurrentAccount.Security))
            {
                btnUpdateSession.Enabled = true;
            }

            PermissionEnum applilcationPermission = (PermissionEnum)ContextService.CurrentAccount.Security;
            // program security
            if (Resolve<IUserService>().HasPermission((PermissionEnum)programSecuirty, PermissionEnum.ProgramCreate, applilcationPermission) && !Model.IsLiveProgram)
            {
                ((Button)Master.FindControl("ContentPlaceHolder1").FindControl("btnAddSeq")).Enabled = true;
            }

            txtSessionName.Text = Model.Name;
            txtSessionDescription.Text = Model.Description;
            ddlDay.SelectedValue = Model.Day.ToString();
            if (Model.IsNeedReportButton)
                this.chkIsNeedReport.Checked = true;
            else
                this.chkIsNeedReport.Checked = false;
            this.chkIsNeedHelp.Checked = Model.IsNeedHelpButton;

            // for paging
            InitialPageInfo((int)Math.Ceiling(Convert.ToDouble(Model.PageSequences.Count) / Constants.PAGE_SIZE), "Order", "asc");
            string url = Request.Url.ToStringWithoutPort();
            PagingString = Ethos.Utility.PagingSortingService.InitialPagingString(Request.Url.ToStringWithoutPort(), PageNumber, CurrentPageNumber, Resources.Share.Previous, Resources.Share.Next);

            rpSession.DataSource = PagingSortingService.GetCurrentPage<PageSequenceModel>(Model.PageSequences, CurrentPageNumber, PageNumber, Constants.PAGE_SIZE, SortExpression + " " + SortOrder);
            rpSession.DataBind();

            UpdateControlStatusBasedOnProgramStatus(Model.IsLiveProgram);
        }

        private void UpdateControlStatusBasedOnProgramStatus(bool isLiveProgram)
        {
            if (isLiveProgram)
            {
                txtSessionName.Enabled = false;
                txtSessionDescription.Enabled = false;
                ddlDay.Enabled = false;
                btnUpdateSession.Enabled = false;
                warnLbl.Visible = true;
            }
            else
            {
                txtSessionName.Enabled = true;
                txtSessionDescription.Enabled = true;
                ddlDay.Enabled = true;
                btnUpdateSession.Enabled = true;
                warnLbl.Visible = false;
            }
        }
        #endregion

        #region Button Events

        protected void btnUp_click(object sender, EventArgs e)
        {
            try
            {
                Button upButton = sender as Button;
                string sessionContentGuid = upButton.CommandArgument;
                Resolve<ISessionContentService>().UpOrderNO(new Guid(sessionContentGuid));
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
            Response.Redirect(Request.Url.ToStringWithoutPort());
        }

        protected void btnDown_click(object sender, EventArgs e)
        {
            try
            {
                Button downButton = sender as Button;
                string sessionContentGuid = downButton.CommandArgument;
                Resolve<ISessionContentService>().DownOrderNO(new Guid(sessionContentGuid));
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
            Response.Redirect(Request.Url.ToStringWithoutPort());
        }

        protected void btnAddSeq_Click(object sender, EventArgs e)
        {
            string sessionGuid = Request.QueryString[Constants.QUERYSTR_SESSION_GUID];
            Response.Redirect(string.Format("AddPageSequence.aspx?{0}={1}&{2}={3}&{4}={5}&{6}={7}&{8}=EditSession", Constants.QUERYSTR_PROGRAM_PAGE, ProgramPage, Constants.QUERYSTR_SESSION_PAGE, SessionPage, Constants.QUERYSTR_SESSION_GUID, sessionGuid, Constants.QUERYSTR_PAGE_SEQUENCE_PAGE, CurrentPageNumber, Constants.QUERYSTR_PREVIOUSPAGE));
        }

        protected void rpSession_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            int programSecuirty = 0;
            PermissionEnum applicationPermission = (PermissionEnum)ContextService.CurrentAccount.Security;
            if (ContextService.CurrentAccount.ProgramSecuirty.ContainsKey(Model.ProgramGuid))
            {
                programSecuirty = ContextService.CurrentAccount.ProgramSecuirty[Model.ProgramGuid];
            }

            if (e.Item.ItemType == ListItemType.Header)
            {
                //// program security
                //if (Resolve<IUserService>().HasPermission((PermissionEnum)programSecuirty, PermissionEnum.ProgramCreate, applicationPermission) &&
                //    !Model.IsLiveProgram)
                //{
                //    ((Button)e.Item.FindControl("btnAddSeq")).Enabled = true;
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
                if (PageNumber == CurrentPageNumber && e.Item.ItemIndex == Model.PageSequences.Count - ((PageNumber - 1) * Constants.PAGE_SIZE) - 1)
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
                //    string url = string.Format("ChangeTechF.html?Mode=Preview&Object=PageSequence&{0}={1}&{2}={3}&{4}={5}",
                //        Constants.QUERYSTR_SESSION_GUID, Request.QueryString[Constants.QUERYSTR_SESSION_GUID],
                //        Constants.QUERYSTR_PAGE_SEQUENCE_GUID, pageSequenceGuid,
                //        Constants.QUERYSTR_USER_GUID, ContextService.CurrentAccount.UserGuid);

                //    string html5URL = string.Format("ChangeTech5.html?Mode=Preview&Object=PageSequence&{0}={1}&{2}={3}&{4}={5}",
                //        Constants.QUERYSTR_SESSION_GUID, Request.QueryString[Constants.QUERYSTR_SESSION_GUID],
                //        Constants.QUERYSTR_PAGE_SEQUENCE_GUID, pageSequenceGuid,
                //        Constants.QUERYSTR_USER_GUID, ContextService.CurrentAccount.UserGuid);
                //    Guid sessionGuid = new Guid(Request.QueryString[Constants.QUERYSTR_SESSION_GUID]);
                // for popup page
                Button preview = (Button)e.Item.FindControl("btnPreview");
                string url = string.Format("ChangeTechF.html?Mode=Preview&Object=PageSequence&{0}={1}&{2}={3}&{4}={5}",
                    Constants.QUERYSTR_SESSION_GUID, Request.QueryString[Constants.QUERYSTR_SESSION_GUID],
                    Constants.QUERYSTR_PAGE_SEQUENCE_GUID, preview.CommandArgument,
                    Constants.QUERYSTR_USER_GUID, ContextService.CurrentAccount.UserGuid);

                string html5URL = string.Format("ChangeTech5.html?Mode=Preview&Object=PageSequence&{0}={1}&{2}={3}&{4}={5}",
                    Constants.QUERYSTR_SESSION_GUID, Request.QueryString[Constants.QUERYSTR_SESSION_GUID],
                    Constants.QUERYSTR_PAGE_SEQUENCE_GUID, preview.CommandArgument,
                    Constants.QUERYSTR_USER_GUID, ContextService.CurrentAccount.UserGuid);

                string html5rURL = string.Format("ChangeTech5r.html?Mode=Preview&Object=PageSequence&{0}={1}&{2}={3}&{4}={5}",
                    Constants.QUERYSTR_SESSION_GUID, Request.QueryString[Constants.QUERYSTR_SESSION_GUID],
                    Constants.QUERYSTR_PAGE_SEQUENCE_GUID, preview.CommandArgument,
                    Constants.QUERYSTR_USER_GUID, ContextService.CurrentAccount.UserGuid);

                if (programPropertyModel.EnableHTML5NewUI)
                {
                    preview.Attributes.Add("OnClick", "return openPageD('" + url + "','" + html5rURL + "');");
                }
                else if (programPropertyModel.IsHTML5PreviewEnable)
                {

                    preview.Attributes.Add("OnClick", "return openPageD('" + url + "','" + html5URL + "');");
                }
                else
                {
                    preview.Attributes.Add("OnClick", "return openPage('" + url + "');");
                }

                // check program security
                if (Resolve<IUserService>().HasPermission((PermissionEnum)programSecuirty, PermissionEnum.ProgramDelete, applicationPermission) &&
                    !Model.IsLiveProgram)
                {
                    DropDownList moreOptionsDDL = ((DropDownList)e.Item.FindControl("moreOptionsDDL"));
                    moreOptionsDDL.Enabled = true;
                }
                if (Resolve<IUserService>().HasPermission((PermissionEnum)programSecuirty, PermissionEnum.ProgramEdit, applicationPermission))
                {
                    ((Button)e.Item.FindControl("btnEdit")).Enabled = true;
                    ((Button)e.Item.FindControl("btnPreview")).Enabled = true;
                }
                if (Resolve<IUserService>().HasPermission((PermissionEnum)programSecuirty, PermissionEnum.ProgramCreate, applicationPermission) &&
                    !Model.IsLiveProgram)
                {
                    //((Button)e.Item.FindControl("btnMakeCopy")).Enabled = true;
                }
            }
        }


        protected void btnUpdateSession_Click(object sender, EventArgs e)
        {
            string sessionGuid = Request.QueryString[Constants.QUERYSTR_SESSION_GUID];

            Resolve<ISessionService>().EditSession(new Guid(sessionGuid), txtSessionName.Text, txtSessionDescription.Text
                , Convert.ToInt32(ddlDay.SelectedValue), this.chkIsNeedReport.Checked, this.chkIsNeedHelp.Checked);

            Response.Redirect(Request.Url.ToStringWithoutPort());
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            Button editButton = sender as Button;
            string pageSequenceGUID = editButton.CommandArgument;
            Response.Redirect(string.Format("EditPageSequence.aspx?{0}={1}&{2}={3}&{4}={5}&{6}={7}&{8}={9}", 
                Constants.QUERYSTR_PROGRAM_PAGE, ProgramPage, 
                Constants.QUERYSTR_SESSION_PAGE, SessionPage, 
                Constants.QUERYSTR_PAGE_SEQUENCE_PAGE, CurrentPageNumber, 
                Constants.QUERYSTR_SESSION_GUID, Request.QueryString[Constants.QUERYSTR_SESSION_GUID], 
                Constants.QUERYSTR_PAGE_SEQUENCE_GUID, pageSequenceGUID));
        }

        protected void addFromDayButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("CopyFromAnotherDay.aspx?{0}={1}&{2}={3}&{4}={5}&{6}={7}", Constants.QUERYSTR_PROGRAM_PAGE, ProgramPage, Constants.QUERYSTR_SESSION_PAGE, SessionPage, Constants.QUERYSTR_SESSION_GUID, Request.QueryString[Constants.QUERYSTR_SESSION_GUID], Constants.QUERYSTR_PAGE_SEQUENCE_PAGE, CurrentPageNumber));
        }

        protected void moreOptionsDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList moreOptionsDDL = (DropDownList)sender;
            string selectOption = moreOptionsDDL.SelectedValue;
            string pageSequenceGuid = moreOptionsDDL.DataValueField;
            Guid sessionContentGuid = Resolve<ISessionContentService>().GetSessionContentByPageSeqGuidAndSessionGuid(new Guid(pageSequenceGuid), new Guid(SessionGUID)).SessionContentGUID;
            switch (selectOption)
            {
                case Constants.MAKE_COPY://make copy
                    try
                    {
                        Resolve<ISessionContentService>().MakeCopySessionContent(sessionContentGuid);
                    }
                    catch (Exception ex)
                    {
                        LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                        throw ex;
                    }
                    Response.Redirect(Request.Url.ToStringWithoutPort());
                    break;
                case Constants.DELETE://make copy
                    try
                    {
                        Resolve<ISessionContentService>().DeleteSessionContent(sessionContentGuid);
                    }
                    catch (Exception ex)
                    {
                        LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                        throw ex;
                    }
                    Response.Redirect(Request.Url.ToStringWithoutPort());
                    break;
                #region PREVIEW
                //case PREVIEW://preview
                //    // for popup page
                //    string url = string.Format("ChangeTechF.html?Mode=Preview&Object=PageSequence&{0}={1}&{2}={3}&{4}={5}",
                //        Constants.QUERYSTR_SESSION_GUID, Request.QueryString[Constants.QUERYSTR_SESSION_GUID],
                //        Constants.QUERYSTR_PAGE_SEQUENCE_GUID, pageSequenceGuid,
                //        Constants.QUERYSTR_USER_GUID, ContextService.CurrentAccount.UserGuid);

                //    string html5URL = string.Format("ChangeTech5.html?Mode=Preview&Object=PageSequence&{0}={1}&{2}={3}&{4}={5}",
                //        Constants.QUERYSTR_SESSION_GUID, Request.QueryString[Constants.QUERYSTR_SESSION_GUID],
                //        Constants.QUERYSTR_PAGE_SEQUENCE_GUID, pageSequenceGuid,
                //        Constants.QUERYSTR_USER_GUID, ContextService.CurrentAccount.UserGuid);
                //    Guid sessionGuid = new Guid(Request.QueryString[Constants.QUERYSTR_SESSION_GUID]);
                //    Model = Resolve<ISessionService>().GetSessionBySessonGuid(sessionGuid);
                //    programPropertyModel = Resolve<IProgramService>().GetProgramProperty(Model.ProgramGuid);
                //    if (programPropertyModel.IsHTML5PreviewEnable)
                //    {
                //        Page.ClientScript.RegisterStartupScript(ClientScript.GetType(), "openUrl", "<script>window.open('" + url + "');window.open('" + html5URL + "');</script>");
                //    }
                //    else
                //    {
                //        Page.ClientScript.RegisterStartupScript(ClientScript.GetType(), "openUrl", "<script>window.open('" + url + "');</script>");
                //    }
                //    break; 
                #endregion
            }
        }
        #endregion

        #region Old follows
        //protected void btnDelete_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        Button upButton = sender as Button;
        //        string sessionContentGuid = upButton.CommandArgument;
        //        Resolve<ISessionContentService>().DeleteSessionContent(new Guid(sessionContentGuid));
        //    }
        //    catch (Exception ex)
        //    {
        //        LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
        //        throw ex;
        //    }
        //    Response.Redirect(Request.Url.ToStringWithoutPort());
        //}
        //protected void btnMakeCopy_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        Button upButton = sender as Button;
        //        string sessionContentGuid = upButton.CommandArgument;
        //        Resolve<ISessionContentService>().MakeCopySessionContent(new Guid(sessionContentGuid));
        //    }
        //    catch (Exception ex)
        //    {
        //        LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
        //        throw ex;
        //    }
        //    Response.Redirect(Request.Url.ToStringWithoutPort());
        //}

        //protected void btnPreview_Click(object sender, EventArgs e)
        //{
        //    string sessionContentGuid = ((Button)sender).CommandArgument;
        //    Response.Redirect(string.Format("PreviewPage.aspx?Object=PageSequence&SessionContentGUID={0}", sessionContentGuid));
        //} 
        #endregion
    }
}
