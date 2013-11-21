using System;
using Ethos.DependencyInjection;
using ChangeTech.Models;
using ChangeTech.Contracts;
using System.Web.UI.WebControls;

using System.IO;
using System.Collections.Generic;
using Ethos.Utility;
using System.Configuration;
using ChangeTech.Services;

namespace ChangeTech.DeveloperWeb
{
    public partial class EditProgram : PageBase<EditProgramModel>
    {
        protected static string lastSelectedLanguage = string.Empty;
        const string DEFALUT_LANGUAGE_NORWEGIAN = "Norwegian";
        const string CTPPPRESENTERMODE = "CTPPPresenterImage";
        const string PAGEREVIEWPRESENTERMODE = "PagePresenterImage";
        static string PROGRAMOPERATION = string.Empty;
        ProgramPropertyModel programPropertyModel;
        private string ProgramGUID
        {
            get
            {
                return Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID];
            }
        }

        private string ProgramPage
        {
            get
            {
                return Request.QueryString[Constants.QUERYSTR_PROGRAM_PAGE];
            }
        }

        private bool IsContainsNorwegianLanguage()
        {
            bool flag = false;
            LanguagesModel languages = Resolve<IProgramLanguageService>().GetLanguagesSupportByProgram(new Guid(ProgramGUID));
            foreach (var language in languages)
            {
                if (language.Name == DEFALUT_LANGUAGE_NORWEGIAN)
                {
                    flag = true;
                }
            }
            return flag;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]))
            {
                if (!IsPostBack)
                {
                    try
                    {
                        if (IsContainsNorwegianLanguage()) lastSelectedLanguage = DEFALUT_LANGUAGE_NORWEGIAN;
                        if (Request.UrlReferrer.AbsoluteUri.Contains(Constants.URL_DEVELOPER_LIST_PROGRAM))
                        {
                            if (IsContainsNorwegianLanguage()) lastSelectedLanguage = DEFALUT_LANGUAGE_NORWEGIAN;
                        }
                        BindProgramModel();
                    }
                    catch (Exception ex)
                    {
                        LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                        throw ex;
                    }
                    ((HyperLink)Master.FindControl("menuLoginView").FindControl("hpBackLink")).NavigateUrl = string.Format("ListProgram.aspx?Page={0}", ProgramPage);
                    ((Literal)Master.FindControl("menuLoginView").FindControl("SiteMapPath").FindControl("ltlEditProgram")).Text = string.Format("Edit program \"{0}\"", Model.ProgramName);
                    ((HyperLink)Master.FindControl("menuLoginView").FindControl("SiteMapPath").FindControl("hpProgramLink")).NavigateUrl = string.Format("ListProgram.aspx?Page={0}", ProgramPage);
                }
            }
            else
            {
                Response.Redirect("InvalidUrl.aspx");
            }
        }

        #region Button Events

        protected void btnUpdateProgram_Click(object sender, EventArgs e)
        {
            Guid fileGuid = Guid.NewGuid();
            string fileType = "";
            string fileName = "";
            try
            {
                ProgramModel programModel = new ProgramModel();
                string programGuid = Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID];
                if (!string.IsNullOrEmpty(programGuid))
                {
                    programModel = Resolve<IProgramService>().GetProgramByGUID(new Guid(programGuid));
                    //(Guid guid, Guid statusGuid, string programName, string description, Guid defauleLanguage, Guid ProgramLogoGuid, string LogoName, string LogoType, string LogoFileExtension, string shortName)
                    ProgramModel program = new ProgramModel()
                    {
                        Guid = new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]),
                        StatusGuid = new Guid(dropListStatus.SelectedValue),
                        ProgramName = txtProgramNameByCtpp.Text,
                        NameInDeveloper = txtProgramNameInDeveloper.Text,
                        Description = txtProgramDescription.Text,
                        DefaultLanguage = programModel.DefaultLanguage,
                        ProgramLogo = new ResourceModel()
                        {
                            ID = fileGuid,
                            Name = fileName,
                            Type = "Logo",
                            Extension = fileType
                        },
                    };
                    Resolve<IProgramService>().EditProgram(program);
                }
                //Resolve<IProgramService>().EditProgram(new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]), new Guid(dropListStatus.SelectedValue)
                //    , txtProgramNameInDeveloper.Text, txtProgramDescription.Text, new Guid(defaultLanguageDropdownlist.SelectedValue), fileGuid, fileName
                //    , "Logo", fileType, shortNameTextBox.Text);
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
            Response.Redirect(Request.Url.ToStringWithoutPort());
        }

        private bool IsValidateShortName()
        {
            return Resolve<IProgramService>().IsValidShortName(new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]), shortNameTextBox.Text);
        }

        protected void btnAddNewDay_Click(object sender, EventArgs e)
        {
            Guid programGuid = new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]);
            Response.Redirect(string.Format("AddSession.aspx?{0}={1}&{2}={3}&{4}={5}", Constants.QUERYSTR_PROGRAM_GUID, programGuid, Constants.QUERYSTR_PROGRAM_PAGE, ProgramPage, Constants.QUERYSTR_SESSION_PAGE, CurrentPageNumber));
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            string sessionID = ((Button)sender).CommandArgument;
            Response.Redirect(string.Format("EditSession.aspx?{0}={1}&{2}={3}&{4}={5}", Constants.QUERYSTR_PROGRAM_PAGE, ProgramPage, Constants.QUERYSTR_SESSION_PAGE, CurrentPageNumber, Constants.QUERYSTR_SESSION_GUID, sessionID));
        }

        protected void btnPageReview_Click(object sender, EventArgs e)
        {
            try
            {
                string sessionID = ((Button)sender).CommandArgument;
                Response.Redirect(string.Format("PageReview.aspx?{0}={1}&{2}={3}&{4}={5}&{6}={7}&{8}={9}", Constants.QUERYSTR_PROGRAM_PAGE, ProgramPage, Constants.QUERYSTR_SESSION_PAGE, CurrentPageNumber, Constants.QUERYSTR_SESSION_GUID, sessionID, Constants.QUERYSTR_USER_GUID, ContextService.CurrentAccount.UserGuid, Constants.QUERYSTR_PRESENTERIMAGE_MODE, PAGEREVIEWPRESENTERMODE));
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        //protected void btnDelete_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        string sessionID = ((Button)sender).CommandArgument;
        //        Resolve<ISessionService>().DeleteSession(new Guid(sessionID));
        //    }
        //    catch (Exception ex)
        //    {
        //        LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
        //        throw ex;
        //    }
        //    Response.Redirect(Request.Url.ToStringWithoutPort());
        //}


        //protected void btnPageReview_Click(object sender, EventArgs e)
        //{
        //    string sessionID = ((Button)sender).CommandArgument;
        ////    Response.Redirect(string.Format("PageReview.aspx?{0}={1}&{2}={3}&{4}={5}&{6}={7}", Constants.QUERYSTR_PROGRAM_PAGE, ProgramPage, Constants.QUERYSTR_SESSION_PAGE, CurrentPageNumber, Constants.QUERYSTR_SESSION_GUID, sessionID, Constants.QUERYSTR_PRESENTERIMAGE_MODE, PAGEREVIEWPRESENTERMODE));
        ////}

        //protected void btnMakeCopy_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        string sessionID = ((Button)sender).CommandArgument;
        //        Resolve<ISessionService>().MakeCopySession(new Guid(sessionID));
        //    }
        //    catch (Exception ex)
        //    {
        //        LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
        //        throw ex;
        //    }
        //    Response.Redirect(Request.Url.ToStringWithoutPort());
        //}

        protected void btnUp_Click(object sender, EventArgs e)
        {
            try
            {
                string sessionID = ((Button)sender).CommandArgument;
                Resolve<ISessionService>().AdjustSessionUp(new Guid(sessionID));
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
            Response.Redirect(Request.Url.ToStringWithoutPort());
        }

        protected void btnDown_Click(object sender, EventArgs e)
        {
            try
            {
                string sessionID = ((Button)sender).CommandArgument;
                Resolve<ISessionService>().AdjustSessionDown(new Guid(sessionID));
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
            Response.Redirect(Request.Url.ToStringWithoutPort());
        }

        protected void rpProgram_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            int programSecuirty = 0;
            PermissionEnum applilcationPermission = (PermissionEnum)ContextService.CurrentAccount.Security;
            if (ContextService.CurrentAccount.ProgramSecuirty.ContainsKey(new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID])))
            {
                programSecuirty = ContextService.CurrentAccount.ProgramSecuirty[new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID])];
            }
            if (e.Item.ItemType == ListItemType.Header)
            {
                //// program security
                //if (Resolve<IUserService>().HasPermission((PermissionEnum)programSecuirty, PermissionEnum.ProgramCreate, applilcationPermission) &&
                //    !Model.IsLiveProgram)
                //{
                //    ((Button)e.Item.FindControl("btnAddNewDay")).Enabled = true;
                //}
            }
            if (e.Item.DataItem != null)
            {
                SessionModel sessionModel = (SessionModel)e.Item.DataItem;

                Button upButton = (Button)e.Item.FindControl("ButtonUp");
                Button downButton = (Button)e.Item.FindControl("ButtonDown");

                // Chen Pu: 2010-11-15, DTD-1071, fix the order problem
                //if(e.Item.ItemIndex == 0 && this.CurrentPageNumber == 1)
                //{
                //    upButton.Visible = false;
                //}

                //if(PageNumber == CurrentPageNumber && e.Item.ItemIndex == Model.Sessions.Count - ((PageNumber - 1) * PageSize) - 1)
                //{
                //    downButton.Visible = false;
                //}

                int numberOfNormalSession = Resolve<ISessionService>().GetNumberOfNormalSessions(new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]));
                //int minCountdownSessionNO = Resolve<ISessionService>().GetMinCountdownSessionDayNO(new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]));
                //int maxCountdownSessionNO = Resolve<ISessionService>().GetMaxCountdownSessionDayNO(new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]));
                if (sessionModel.Day == 0 || sessionModel.Day == -5)
                {
                    upButton.Visible = false;
                }

                if (sessionModel.Day == numberOfNormalSession - 1 || sessionModel.Day == -1)
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

                #region MyRegion
                //// for popup new preview page
                //Button preview = (Button)e.Item.FindControl("btnPreview");
                //string url = string.Format("ChangeTechF.html?Mode=Preview&Object=Session&{0}={1}&{2}={3}",
                //    Constants.QUERYSTR_SESSION_GUID, preview.CommandArgument,
                //    Constants.QUERYSTR_USER_GUID, ContextService.CurrentAccount.UserGuid);

                //string html5URL = string.Format("ChangeTech5.html?Mode=Preview&Object=Session&{0}={1}&{2}={3}",
                //   Constants.QUERYSTR_SESSION_GUID, preview.CommandArgument,
                //   Constants.QUERYSTR_USER_GUID, ContextService.CurrentAccount.UserGuid);

                //if (programPropertyModel.IsHTML5PreviewEnable)
                //{
                //    preview.Attributes.Add("OnClick", "return openPageD('" + url + "','" + html5URL + "');");
                //}
                //else
                //{
                //    preview.Attributes.Add("OnClick", "return openPage('" + url + "');");
                //}
                #endregion

                // program security
                if (Resolve<IUserService>().HasPermission((PermissionEnum)programSecuirty, PermissionEnum.ProgramDelete, applilcationPermission) &&
                    !Model.IsLiveProgram)
                {
                    //DropDownList moreOptionsDDL = ((DropDownList)e.Item.FindControl("moreOptionsDDL"));
                    //moreOptionsDDL.Enabled = true;
                }
                if (Resolve<IUserService>().HasPermission((PermissionEnum)programSecuirty, PermissionEnum.ProgramEdit, applilcationPermission))
                {
                    DropDownList moreOptionsDDL = ((DropDownList)e.Item.FindControl("moreOptionsDDL"));
                    moreOptionsDDL.Enabled = true;
                    ((Button)e.Item.FindControl("btnEdit")).Enabled = true;
                    ((Button)e.Item.FindControl("btnPageReview")).Enabled = true;
                }
                if (Resolve<IUserService>().HasPermission((PermissionEnum)programSecuirty, PermissionEnum.ProgramCreate, applilcationPermission) &&
                    !Model.IsLiveProgram)
                {
                    //((Button)e.Item.FindControl("btnMakeCopy")).Enabled = true;
                }
            }
        }
        #endregion

        #region Old follows
        //protected void programRoomLinkButton_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect(string.Format("ListProgramRoom.aspx?{0}={1}", Constants.QUERYSTR_PROGRAM_GUID, Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]));
        //}

        //protected void manageProgramLanguageLinkButton_Click(object sender, EventArgs e)
        //{
        //    Guid defaultProgramGUID = Resolve<IProgramLanguageService>().GetDefaultProgramGUID(new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]));
        //    Response.Redirect(string.Format("ManageProgramLanguage.aspx?{0}={1}", Constants.QUERYSTR_PROGRAM_GUID, defaultProgramGUID));
        //}

        //protected void relapseLinkButton_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect(string.Format("ManageRelapsePageSequence.aspx?{0}={1}&{2}={3}&{4}={5}",
        //        Constants.QUERYSTR_PROGRAM_GUID, Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID],
        //        Constants.QUERYSTR_PROGRAM_PAGE, ProgramPage,
        //        Constants.QUERYSTR_SESSION_PAGE, CurrentPageNumber));
        //}

        //protected void tipMessageLinkButton_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect(string.Format("ManageTipMeassage.aspx?{0}={1}", Constants.QUERYSTR_PROGRAM_GUID, Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]));
        //}

        //protected void manageProgramColorLinkButton_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect(string.Format("ManageProgramColor.aspx?{0}={1}", Constants.QUERYSTR_PROGRAM_GUID, Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]));
        //}

        //protected void checkExpressionLinkButton_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect(string.Format("CheckProgram.aspx?{0}={1}", Constants.QUERYSTR_PROGRAM_GUID, Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]));
        //    //string result = Resolve<IProgramService>().CheckExpressionForProgram(new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]));
        //    //if (string.IsNullOrEmpty(result))
        //    //{
        //    //    result = "All right.";
        //    //}

        //    //ClientScript.RegisterStartupScript(this.GetType(), "tip", "alert('" + result + "');", true);
        //}

        //protected void userGroupLinkButton_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect(string.Format("ManageUserCompany.aspx?{0}={1}", Constants.QUERYSTR_PROGRAM_GUID, Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]));
        //}

        //protected void StatisticsLinkButton_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect(string.Format("Report.aspx?{0}={1}", Constants.QUERYSTR_PROGRAM_GUID, Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]));
        //}

        //protected void PinCodeLinkButton_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect(string.Format("AddPinCode.aspx?{0}={1}", Constants.QUERYSTR_PROGRAM_GUID, Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]));
        //}

        //protected void SMLinkButton_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect(string.Format("ManageSM.aspx?{0}={1}", Constants.QUERYSTR_PROGRAM_GUID, Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]));
        //}

        //protected void programLinkButton_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect(string.Format("ManageProgram.aspx?{0}={1}", Constants.QUERYSTR_PROGRAM_GUID, Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]));
        //}

        //protected void smsLinkButton_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect(string.Format("ListSMS.aspx?{0}={1}", Constants.QUERYSTR_PROGRAM_GUID, Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]));
        //}

        //protected void programScheduleLinkButton_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect(string.Format("ProgramSchedule.aspx?{0}={1}", Constants.QUERYSTR_PROGRAM_GUID, ProgramGUID));
        //}

        //protected void emailLinkButton_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect(string.Format("EditEmailTemplate.aspx?{0}={1}", Constants.QUERYSTR_PROGRAM_GUID, ProgramGUID));
        //}

        //protected void loginLinkButton_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect(string.Format("ManageLoginTemplate.aspx?{0}={1}", Constants.QUERYSTR_PROGRAM_GUID, ProgramGUID));
        //}

        //protected void passwordReminderLinkButton_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect(string.Format("ManagePasswordReminderTemplate.aspx?{0}={1}", Constants.QUERYSTR_PROGRAM_GUID, ProgramGUID));
        //}

        //protected void sessionEndingLinkButton_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect(string.Format("ManageSessionEndingTemplate.aspx?{0}={1}", Constants.QUERYSTR_PROGRAM_GUID, ProgramGUID));
        //}

        //protected void paymentLinkButton_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect(string.Format("ManatePayment.aspx?{0}={1}", Constants.QUERYSTR_PROGRAM_GUID, ProgramGUID));
        //}

        //protected void userMenuLinkButton_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect(string.Format("ManageUserMenu.aspx?{0}={1}", Constants.QUERYSTR_PROGRAM_GUID, ProgramGUID));
        //}

        //protected void helpItemLinkButton_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect(string.Format("ManageHelpItem.aspx?{0}={1}", Constants.QUERYSTR_PROGRAM_GUID, ProgramGUID));
        //}

        //protected void CTPPLinkButton_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect(string.Format("ManageCTPP.aspx?{0}={1}&{2}={3}&{4}={5}",
        //        Constants.QUERYSTR_PROGRAM_GUID, ProgramGUID,
        //        Constants.QUERYSTR_PRESENTERIMAGE_MODE, CTPPPRESENTERMODE,
        //        Constants.QUERYSTR_PROGRAM_PAGE, ProgramPage));
        //}

        //protected void dailySMSContentLinkButton_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect(string.Format("ManageDailyReportSMS.aspx?{0}={1}",
        //        Constants.QUERYSTR_PROGRAM_GUID, ProgramGUID));
        //}

        //protected void copyTipMessageLinkButton_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect(string.Format("ManageCopyTipMessage.aspx?{0}={1}", Constants.QUERYSTR_PROGRAM_GUID, ProgramGUID));
        //}

        ////protected void exportUserVariableLinkButton_Click(object sender, EventArgs e)
        ////{
        ////    Response.Redirect(string.Format("ExportUserVariable.aspx?{0}={1}", Constants.QUERYSTR_PROGRAM_GUID, Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]));
        ////}

        ////protected void exportUserVariableLinkButton_Click(object sender, EventArgs e)
        ////{
        ////    exportUservariableGridView.DataSource = Resolve<IPageVariableService>().GetProgramUserPageVariable(new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]));
        ////    exportUservariableGridView.DataBind();

        ////    Ethos.Utility.ExportExcel.ExportExcelFromGridView(exportUservariableGridView, "PageVariableStatistic");
        ////}
        
        #endregion

        public override void VerifyRenderingInServerForm(System.Web.UI.Control control)
        {
        }

        #region Private Methods

        private void BindProgramModel()
        {  
            Guid programGuid = new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]);
            programPropertyModel = Resolve<IProgramService>().GetProgramProperty(programGuid);
            BindProgramSupportLanguages(programGuid);
            // initial variable for paging
            int numberOfSession = Resolve<ISessionService>().GetNumberOfSession(programGuid);
            InitialPageInfo((int)Math.Ceiling(Convert.ToDouble(numberOfSession) / Constants.PAGE_SIZE), "Day", "asc");
            Model = Resolve<IProgramService>().GetEditProgramModelByGuid(programGuid, CurrentPageNumber, Constants.PAGE_SIZE);
            
            // set program security
            int programPermission = 0;
            if (ContextService.CurrentAccount.ProgramSecuirty.ContainsKey(programGuid))
            {
                programPermission = ContextService.CurrentAccount.ProgramSecuirty[programGuid];
            }
            if (Resolve<IUserService>().HasPermission((PermissionEnum)programPermission, PermissionEnum.ProgramEdit, (PermissionEnum)ContextService.CurrentAccount.Security))
            {
                btnUpdateProgram.Enabled = true;
            }

            txtProgramNameByCtpp.Text = Model.ProgramName;
            txtProgramNameInDeveloper.Text = Model.NameInDeveloper;
            txtProgramDescription.Text = Model.Description;
            
            dropListStatus.DataSource = Model.ProgramStatusList;
            dropListStatus.DataBind();

            if (!string.IsNullOrEmpty(Model.ProgramStatus))
            {
                dropListStatus.SelectedValue = Model.ProgramStatus;
                //UpdateControlStatusBasedOnProgramStatus(Model.IsLiveProgram);
            }

            // for paging            
            string url = Request.Url.ToStringWithoutPort();
            PagingString = PagingSortingService.InitialPagingString(Request.Url.ToStringWithoutPort(), PageNumber, CurrentPageNumber, Resources.Share.Previous, Resources.Share.Next);

            rpProgram.DataSource = Model.Sessions;
            rpProgram.DataBind();

            //LanguagesModel languages = Resolve<IProgramLanguageService>().GetLanguagesSupportByProgram(programGuid);
            //defaultLanguageDropdownlist.DataSource = languages;
            //defaultLanguageDropdownlist.DataBind();
            //defaultLanguageDropdownlist.SelectedValue = Model.DefaultLanguage.ToString();

            int programSecuirty = 0;
            PermissionEnum applilcationPermission = (PermissionEnum)ContextService.CurrentAccount.Security;
            if (ContextService.CurrentAccount.ProgramSecuirty.ContainsKey(new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID])))
            {
                programSecuirty = ContextService.CurrentAccount.ProgramSecuirty[new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID])];
            }
            // program security
            if (Resolve<IUserService>().HasPermission((PermissionEnum)programSecuirty, PermissionEnum.ProgramCreate, applilcationPermission) && !Model.IsLiveProgram)
            {
                ((Button)Master.FindControl("ContentPlaceHolder1").FindControl("btnAddNewDay")).Enabled = true;
            }
        }

        private void BindProgramSupportLanguages(Guid programGuid)
        {
            ProgramModel programModel = Resolve<IProgramService>().GetProgramByGUID(programGuid);
            LanguagesModel languages = Resolve<IProgramLanguageService>().GetLanguagesSupportByProgram(programGuid);
            
            foreach (LanguageModel language in languages)
            {
                switch (language.Name)
                {
                    case "Danish":
                        if (language.LanguageGUID == programModel.DefaultLanguage)
                        {
                            DanishLanguageLinkBtn.Visible = false;
                            DanishLanguageLabel.Visible = true;
                            lastSelectedLanguage = language.Name;
                        }
                        else
                        {
                            DanishLanguageLinkBtn.Visible = true;
                            DanishLanguageLabel.Visible = false;
                        }
                        break;
                    case "English":
                        if (language.LanguageGUID == programModel.DefaultLanguage)
                        {
                            EnglishLanguageLinkBtn.Visible = false;
                            EnglishLanguageLabel.Visible = true;
                            lastSelectedLanguage = language.Name;
                        }
                        else
                        {
                            EnglishLanguageLinkBtn.Visible = true;
                            EnglishLanguageLabel.Visible = false;
                        }
                        break;
                    case "Finnish":
                        if (language.LanguageGUID == programModel.DefaultLanguage)
                        {
                            FinnishLanguageLinkBtn.Visible = false;
                            FinnishLanguageLabel.Visible = true;
                            lastSelectedLanguage = language.Name;
                        }
                        else
                        {
                            FinnishLanguageLinkBtn.Visible = true;
                            FinnishLanguageLabel.Visible = false;
                        }
                        break;
                    case "Norwegian":
                        if (language.LanguageGUID == programModel.DefaultLanguage)
                        {
                            NorwegianLanguageLinkBtn.Visible = false;
                            NorwegianLanguageLabel.Visible = true;
                            lastSelectedLanguage = language.Name;
                        }
                        else
                        {
                            NorwegianLanguageLinkBtn.Visible = true;
                            NorwegianLanguageLabel.Visible = false;
                        }
                        break;
                    case "Norwegian Test":
                        if (language.LanguageGUID == programModel.DefaultLanguage)
                        {
                            NorwegianTestLanguageLinkBtn.Visible = false;
                            NorwegianTestLanguageLabel.Visible = true;
                            lastSelectedLanguage = language.Name;
                        }
                        else
                        {
                            NorwegianTestLanguageLinkBtn.Visible = true;
                            NorwegianTestLanguageLabel.Visible = false;
                        }
                        break;
                    case "Icelandic":
                        if (language.LanguageGUID == programModel.DefaultLanguage)
                        {
                            IcelandicLanguageLinkBtn.Visible = false;
                            IcelandicLanguageLabel.Visible = true;
                            lastSelectedLanguage = language.Name;
                        }
                        else
                        {
                            IcelandicLanguageLinkBtn.Visible = true;
                            IcelandicLanguageLabel.Visible = false;
                        }
                        break;
                    case "Spanish":
                        if (language.LanguageGUID == programModel.DefaultLanguage)
                        {
                            SpanishLanguageLinkBtn.Visible = false;
                            SpanishLanguageLabel.Visible = true;
                            lastSelectedLanguage = language.Name;
                        }
                        else
                        {
                            SpanishLanguageLinkBtn.Visible = true;
                            SpanishLanguageLabel.Visible = false;
                        }
                        break;
                    case "Swedish":
                        if (language.LanguageGUID == programModel.DefaultLanguage)
                        {
                            SwedishLanguageLinkBtn.Visible = false;
                            SwedishLanguageLabel.Visible = true;
                            lastSelectedLanguage = language.Name;
                        }
                        else
                        {
                            SwedishLanguageLinkBtn.Visible = true;
                            SwedishLanguageLabel.Visible = false;
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        private void UpdateControlStatusBasedOnProgramStatus(bool isLiveProgram)
        {
            if (isLiveProgram)
            {
                txtProgramNameByCtpp.Enabled = false;
                txtProgramNameInDeveloper.Enabled = false;
                txtProgramDescription.Enabled = false;              
                //defaultLanguageDropdownlist.Enabled = false;
                //programRoomLinkButton.Enabled = false;
                //manageProgramColorLinkButton.Enabled = false;
                //manageProgramLanguageLinkButton.Enabled = false;
                btnUpdateProgram.Enabled = false;
                warnLbl.Visible = true;
            }
            else
            {
                txtProgramNameInDeveloper.Enabled = true;
                txtProgramDescription.Enabled = true;               
                //defaultLanguageDropdownlist.Enabled = true;
                //programRoomLinkButton.Enabled = true;
                //manageProgramColorLinkButton.Enabled = true;
                //manageProgramLanguageLinkButton.Enabled = true;
                btnUpdateProgram.Enabled = true;
                warnLbl.Visible = false;
            }
        }
        #endregion

        #region DropDownList SelectChanges Event
        protected void ProgramManageDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList programManageDDL = (DropDownList)sender;
            string programManage = (programManageDDL.Text).Substring(2);
            
            //if (PROGRAMOPERATION == programManage)
            //{
            //    return;
            //}
            //PROGRAMOPERATION = programManage;
            switch (programManage)
            {
                case "Program room settings":
                    Response.Redirect(string.Format("ListProgramRoom.aspx?{0}={1}&{2}={3}", Constants.QUERYSTR_PROGRAM_GUID, Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID], Constants.QUERYSTR_PROGRAM_PAGE, Request.QueryString[Constants.QUERYSTR_PROGRAM_PAGE]));
                    break;
                case "Language settings":
                    Guid defaultProgramGUID = Resolve<IProgramLanguageService>().GetDefaultProgramGUID(new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]));
                    Response.Redirect(string.Format("ManageProgramLanguage.aspx?{0}={1}&{2}={3}", Constants.QUERYSTR_PROGRAM_GUID, defaultProgramGUID, Constants.QUERYSTR_PROGRAM_PAGE, Request.QueryString[Constants.QUERYSTR_PROGRAM_PAGE]));
                    break;
                case "Program color settings(flash)":
                    Response.Redirect(string.Format("ManageProgramColor.aspx?{0}={1}&{2}={3}", Constants.QUERYSTR_PROGRAM_GUID, Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID], Constants.QUERYSTR_PROGRAM_PAGE, Request.QueryString[Constants.QUERYSTR_PROGRAM_PAGE]));
                    break;
                case "Check program":
                    Response.Redirect(string.Format("CheckProgram.aspx?{0}={1}&{2}={3}", Constants.QUERYSTR_PROGRAM_GUID, Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID], Constants.QUERYSTR_PROGRAM_PAGE, Request.QueryString[Constants.QUERYSTR_PROGRAM_PAGE]));
                    break;
                case "Export user data":
                    Page.ClientScript.RegisterStartupScript(ClientScript.GetType(), "openExportUserVariablePage", "<script>var url = document.URL.replace('EditProgram', 'ExportUserVariable');window.open(url, 'ExportUserVariable', 'width=400,height=160,toolbar=no,scrollbars=no,menubar=no,top=300,left=400');</script>");
                    //Page.RegisterClientScriptBlock("OpenExportUserVariablePage", "<script> var url = document.URL.replace('EditProgram', 'ExportUserVariable');window.open(url, 'ExportUserVariable', 'width=400,height=160,toolbar=no,scrollbars=no,menubar=no,top=300,left=400');</script>");
                    break;
                case "User groups":
                    Response.Redirect(string.Format("ManageUserCompany.aspx?{0}={1}&{2}={3}", Constants.QUERYSTR_PROGRAM_GUID, Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID], Constants.QUERYSTR_PROGRAM_PAGE, Request.QueryString[Constants.QUERYSTR_PROGRAM_PAGE]));
                    break;
                case "Tip messages settings":
                    Response.Redirect(string.Format("ManageTipMeassage.aspx?{0}={1}&{2}={3}", Constants.QUERYSTR_PROGRAM_GUID, Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID], Constants.QUERYSTR_PROGRAM_PAGE, Request.QueryString[Constants.QUERYSTR_PROGRAM_PAGE]));
                    break;
                case "Statistics":
                    Response.Redirect(string.Format("Report.aspx?{0}={1}&{2}={3}", Constants.QUERYSTR_PROGRAM_GUID, Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID], Constants.QUERYSTR_PROGRAM_PAGE, Request.QueryString[Constants.QUERYSTR_PROGRAM_PAGE]));
                    break;
                case "Pincode page settings":
                    Response.Redirect(string.Format("AddPinCode.aspx?{0}={1}&{2}={3}", Constants.QUERYSTR_PROGRAM_GUID, Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID], Constants.QUERYSTR_PROGRAM_PAGE, Request.QueryString[Constants.QUERYSTR_PROGRAM_PAGE]));
                    break;
                case "Pincode SMS settings":
                    Response.Redirect(string.Format("ManageSM.aspx?{0}={1}&{2}={3}", Constants.QUERYSTR_PROGRAM_GUID, Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID], Constants.QUERYSTR_PROGRAM_PAGE, Request.QueryString[Constants.QUERYSTR_PROGRAM_PAGE]));
                    break;
                case "SMSes sent from program":
                    Response.Redirect(string.Format("ListSMS.aspx?{0}={1}&{2}={3}", Constants.QUERYSTR_PROGRAM_GUID, Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID], Constants.QUERYSTR_PROGRAM_PAGE, Request.QueryString[Constants.QUERYSTR_PROGRAM_PAGE]));
                    break;
                case "Export user data - extended":
                    Page.ClientScript.RegisterStartupScript(ClientScript.GetType(), "openExportUserVariablePage", "<script>var url = document.URL.replace('EditProgram', 'ExportUserVariableExtension');window.open(url, 'ExportUserVariable', 'width=400,height=160,toolbar=no,scrollbars=no,menubar=no,top=300,left=400');</script>");
                    //Page.RegisterClientScriptBlock("OpenExportUserVariablePage", "<script> var url = document.URL.replace('EditProgram', 'ExportUserVariableExtension');window.open(url, 'ExportUserVariable', 'width=400,height=160,toolbar=no,scrollbars=no,menubar=no,top=300,left=400');</script>");
                    break;
                case "Daily SMS settings":
                    Response.Redirect(string.Format("ManageDailyReportSMS.aspx?{0}={1}&{2}={3}",
               Constants.QUERYSTR_PROGRAM_GUID, ProgramGUID, Constants.QUERYSTR_PROGRAM_PAGE, Request.QueryString[Constants.QUERYSTR_PROGRAM_PAGE]));
                    break;
                case "Copy tip messages from another program":
                    Response.Redirect(string.Format("ManageCopyTipMessage.aspx?{0}={1}&{2}={3}", Constants.QUERYSTR_PROGRAM_GUID, ProgramGUID, Constants.QUERYSTR_PROGRAM_PAGE, Request.QueryString[Constants.QUERYSTR_PROGRAM_PAGE]));
                    break;
                case "Program schedule settings":
                    Response.Redirect(string.Format("ProgramSchedule.aspx?{0}={1}&{2}={3}", Constants.QUERYSTR_PROGRAM_GUID, ProgramGUID, Constants.QUERYSTR_PROGRAM_PAGE, Request.QueryString[Constants.QUERYSTR_PROGRAM_PAGE]));
                    break;
                case "Email template settings":
                    Response.Redirect(string.Format("EditEmailTemplate.aspx?{0}={1}&{2}={3}", Constants.QUERYSTR_PROGRAM_GUID, ProgramGUID, Constants.QUERYSTR_PROGRAM_PAGE, Request.QueryString[Constants.QUERYSTR_PROGRAM_PAGE]));
                    break;
                case "Login page settings":
                    Response.Redirect(string.Format("ManageLoginTemplate.aspx?{0}={1}&{2}={3}", Constants.QUERYSTR_PROGRAM_GUID, ProgramGUID, Constants.QUERYSTR_PROGRAM_PAGE, Request.QueryString[Constants.QUERYSTR_PROGRAM_PAGE]));
                    break;
                case "Password reminder page settings":
                    Response.Redirect(string.Format("ManagePasswordReminderTemplate.aspx?{0}={1}&{2}={3}", Constants.QUERYSTR_PROGRAM_GUID, ProgramGUID, Constants.QUERYSTR_PROGRAM_PAGE, Request.QueryString[Constants.QUERYSTR_PROGRAM_PAGE]));
                    break;
                case "Session end page settings (not in use anymore)":
                    Response.Redirect(string.Format("ManageSessionEndingTemplate.aspx?{0}={1}&{2}={3}", Constants.QUERYSTR_PROGRAM_GUID, ProgramGUID, Constants.QUERYSTR_PROGRAM_PAGE, Request.QueryString[Constants.QUERYSTR_PROGRAM_PAGE]));
                    break;
                case "Payment page settings":
                    Response.Redirect(string.Format("ManatePayment.aspx?{0}={1}&{2}={3}", Constants.QUERYSTR_PROGRAM_GUID, ProgramGUID, Constants.QUERYSTR_PROGRAM_PAGE, Request.QueryString[Constants.QUERYSTR_PROGRAM_PAGE]));
                    break;
                case "User menu settings":
                    Response.Redirect(string.Format("ManageUserMenu.aspx?{0}={1}&{2}={3}", Constants.QUERYSTR_PROGRAM_GUID, ProgramGUID, Constants.QUERYSTR_PROGRAM_PAGE, Request.QueryString[Constants.QUERYSTR_PROGRAM_PAGE]));
                    break;
                case "FAQ settings":
                    Response.Redirect(string.Format("ManageHelpItem.aspx?{0}={1}&{2}={3}", Constants.QUERYSTR_PROGRAM_GUID, ProgramGUID, Constants.QUERYSTR_PROGRAM_PAGE, Request.QueryString[Constants.QUERYSTR_PROGRAM_PAGE]));
                    break;
                //case "Manage program":
                //    Response.Redirect(string.Format("ManageProgram.aspx?{0}={1}&{2}={3}", Constants.QUERYSTR_PROGRAM_GUID, Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID], Constants.QUERYSTR_PROGRAM_PAGE, Request.QueryString[Constants.QUERYSTR_PROGRAM_PAGE]));
                //    break;
                //case "Relapse":
                //    Response.Redirect(string.Format("ManageRelapsePageSequence.aspx?{0}={1}&{2}={3}&{4}={5}",
                //        Constants.QUERYSTR_PROGRAM_GUID, Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID],
                //        Constants.QUERYSTR_PROGRAM_PAGE, ProgramPage,
                //        Constants.QUERYSTR_SESSION_PAGE, CurrentPageNumber));
                //    break;
                //case "Program Page(CTPP)":
                //    Response.Redirect(string.Format("ManageCTPP.aspx?{0}={1}&{2}={3}&{4}={5}",
                //    Constants.QUERYSTR_PROGRAM_GUID, ProgramGUID,
                //    Constants.QUERYSTR_PRESENTERIMAGE_MODE, CTPPPRESENTERMODE,
                //    Constants.QUERYSTR_PROGRAM_PAGE, ProgramPage));
                //    break;
                default:
                    break;
            }
        }

        protected void moreOptionsDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList moreOptionsDDL = (DropDownList)sender;
            string selectOption = moreOptionsDDL.SelectedValue;
            string sessionID = moreOptionsDDL.DataValueField;
            switch (selectOption)
            {
                case Constants.PAGE_REVIEW://page review
                    Response.Redirect(string.Format("PageReview.aspx?{0}={1}&{2}={3}&{4}={5}&{6}={7}", Constants.QUERYSTR_PROGRAM_PAGE, ProgramPage, Constants.QUERYSTR_SESSION_PAGE, CurrentPageNumber, Constants.QUERYSTR_SESSION_GUID, sessionID, Constants.QUERYSTR_PRESENTERIMAGE_MODE, PAGEREVIEWPRESENTERMODE));
                    break;
                case Constants.PREVIEW://preview
                    // for popup new preview page
                    string url = string.Format("/ChangeTechF.html?Mode=Preview&Object=Session&{0}={1}&{2}={3}",
                        Constants.QUERYSTR_SESSION_GUID, sessionID,
                        Constants.QUERYSTR_USER_GUID, ContextService.CurrentAccount.UserGuid);

                    string html5URL = string.Format("/ChangeTech5.html?Mode=Preview&Object=Session&{0}={1}&{2}={3}",
                       Constants.QUERYSTR_SESSION_GUID, sessionID,
                       Constants.QUERYSTR_USER_GUID, ContextService.CurrentAccount.UserGuid);

                    string html5rURL = string.Format("/ChangeTech5r.html?Mode=Preview&Object=Session&{0}={1}&{2}={3}",
                      Constants.QUERYSTR_SESSION_GUID, sessionID,
                      Constants.QUERYSTR_USER_GUID, ContextService.CurrentAccount.UserGuid);
                    
                    
                       Guid programGuid = new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]);
                       if (programGuid != Guid.Empty)
                       {
                           programPropertyModel = Resolve<IProgramService>().GetProgramProperty(programGuid);
                           if (programPropertyModel.EnableHTML5NewUI)
                           {
                               Page.ClientScript.RegisterStartupScript(ClientScript.GetType(), "openUrl", "<script>window.open('" + url + "','_blank');window.open('" + html5rURL + "','_blank');</script>");
                           }
                           else if (programPropertyModel.IsHTML5PreviewEnable)
                           {
                               ClientScript.RegisterStartupScript(ClientScript.GetType(), "openUrl", "<script>window.open('" + url + "',target='_blank');window.focus();</script>");
                               //ClientScript.RegisterStartupScript(ClientScript.GetType(), "openUrl", "<script>openPageD('" + url + "','" + html5URL + "');</script>");
                               //Page.ClientScript.RegisterStartupScript(ClientScript.GetType(), "openUrl", "<script>window.open('" + url + "','_blank');window.open('" + html5URL + "','_blank');</script>");
                           }
                           else
                           {
                               Page.ClientScript.RegisterStartupScript(ClientScript.GetType(), "openUrl", "<script>window.open('" + url + "',target='_blank');</script>");
                           }
                       }
                    break;
                case Constants.MAKE_COPY://make copy
                    try
                    {
                        Resolve<ISessionService>().MakeCopySession(new Guid(sessionID));
                    }
                    catch (Exception ex)
                    {
                        LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                        throw ex;
                    }
                    Response.Redirect(Request.Url.ToStringWithoutPort());
                    break;
                case Constants.DELETE://delete day
                    try
                    {
                        Resolve<ISessionService>().DeleteSession(new Guid(sessionID));
                    }
                    catch (Exception ex)
                    {
                        LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                        throw ex;
                    }
                    Response.Redirect(Request.Url.ToStringWithoutPort());
                    break;
            }
        }

        protected void dropListStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            ProgramModel programModel = new ProgramModel();
            string programGuid = Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID];
            if (!string.IsNullOrEmpty(programGuid))
            {
                programModel = Resolve<IProgramService>().GetProgramByGUID(new Guid(programGuid));
                ProgramModel program = new ProgramModel()
                {
                    Guid = new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]),
                    StatusGuid = new Guid(dropListStatus.SelectedValue),
                    ProgramName = txtProgramNameByCtpp.Text,
                    NameInDeveloper = txtProgramNameInDeveloper.Text,
                    Description = txtProgramDescription.Text,
                    DefaultLanguage = programModel.DefaultLanguage,
                    ShortName = shortNameTextBox.Text,
                    ProgramLogo = new ResourceModel()
                    {
                        ID = Guid.Empty,
                        Name = "",
                        Type = "Logo",
                        Extension = ""
                    },
                };
                // add log about update program's status.
                ProgramStatusModel programStatusModel = Resolve<IProgramStatusService>().GetProgramStatusByStatusGuid(program.StatusGuid);
                string programStatus = string.Empty;
                if (programStatusModel != null)
                {
                    programStatus = programStatusModel.Name;
                }
                InsertLogModel insertLogModel = new InsertLogModel
                {
                    ActivityLogType = (int)LogTypeEnum.ModifyProgram,
                    Browser = Request.UserAgent,
                    IP = Request.UserHostAddress,
                    Message = "Update Program's Status :" + programStatus,
                    ProgramGuid = program.Guid,
                    PageGuid = Guid.Empty,
                    PageSequenceGuid = Guid.Empty,
                    SessionGuid = Guid.Empty,
                    UserGuid = ContextService.CurrentAccount.UserGuid,
                    From = string.Empty
                };
                Resolve<IActivityLogService>().Insert(insertLogModel);

                Resolve<IProgramService>().EditProgram(program);
                //Resolve<IProgramService>().EditProgram(new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]), new Guid(dropListStatus.SelectedValue)
                //        , txtProgramNameInDeveloper.Text, txtProgramDescription.Text, new Guid(defaultLanguageDropdownlist.SelectedValue), Guid.Empty, ""
                //        , "Logo", "", shortNameTextBox.Text);
                Response.Redirect(Request.Url.ToStringWithoutPort());
            }
        }
        #endregion


        protected void btnManageProgram_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("ManageProgram.aspx?{0}={1}&{2}={3}", Constants.QUERYSTR_PROGRAM_GUID, Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID], Constants.QUERYSTR_PROGRAM_PAGE, Request.QueryString[Constants.QUERYSTR_PROGRAM_PAGE]));
        }

        protected void btnProgramPage_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("ManageCTPP.aspx?{0}={1}&{2}={3}&{4}={5}",
                    Constants.QUERYSTR_PROGRAM_GUID, ProgramGUID,
                    Constants.QUERYSTR_PRESENTERIMAGE_MODE, CTPPPRESENTERMODE,
                    Constants.QUERYSTR_PROGRAM_PAGE, ProgramPage));
        }

        protected void btnSubroutines_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("ManageRelapsePageSequence.aspx?{0}={1}&{2}={3}&{4}={5}",
                       Constants.QUERYSTR_PROGRAM_GUID, Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID],
                       Constants.QUERYSTR_PROGRAM_PAGE, ProgramPage,
                       Constants.QUERYSTR_SESSION_PAGE, CurrentPageNumber));
        }

        protected void DanishLanguageLinkBtn_Click(object sender, EventArgs e)
        {
            lastSelectedLanguage = DanishLanguageLinkBtn.Text;
            Guid languageGuid = new Guid("1D181168-59F7-4F69-84A7-7A8391760760");
            ProgramModel programModel = Resolve<IProgramService>().GetProgramByProgramGUIDAndLanguageGUID(new Guid(Request.Params[Constants.QUERYSTR_PROGRAM_GUID]),languageGuid);
            Response.Redirect(string.Format("EditProgram.aspx?{0}={1}&{2}={3}", Constants.QUERYSTR_PROGRAM_PAGE, ProgramPage, Constants.QUERYSTR_PROGRAM_GUID, programModel.Guid));
        }

        protected void EnglishLanguageLinkBtn_Click(object sender, EventArgs e)
        {
            lastSelectedLanguage = EnglishLanguageLinkBtn.Text;
            Guid languageGuid = new Guid("4C8140B5-25E0-46AC-A99E-6438B445C5B4");
            ProgramModel programModel = Resolve<IProgramService>().GetProgramByProgramGUIDAndLanguageGUID(new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]), languageGuid);
            Response.Redirect(string.Format("EditProgram.aspx?{0}={1}&{2}={3}", Constants.QUERYSTR_PROGRAM_PAGE, ProgramPage, Constants.QUERYSTR_PROGRAM_GUID, programModel.Guid));
        }

        protected void FinnishLanguageLinkBtn_Click(object sender, EventArgs e)
        {
            lastSelectedLanguage = FinnishLanguageLinkBtn.Text;
            Guid languageGuid = new Guid("D8C900AD-9A11-499D-88E6-ECE687127B9E");
            ProgramModel programModel = Resolve<IProgramService>().GetProgramByProgramGUIDAndLanguageGUID(new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]), languageGuid);
            Response.Redirect(string.Format("EditProgram.aspx?{0}={1}&{2}={3}", Constants.QUERYSTR_PROGRAM_PAGE, ProgramPage, Constants.QUERYSTR_PROGRAM_GUID, programModel.Guid));
        }

        protected void NorwegianLanguageLinkBtn_Click(object sender, EventArgs e)
        {
            lastSelectedLanguage = NorwegianLanguageLinkBtn.Text;
            Guid languageGuid = new Guid("FB5AE1DC-4CAF-4613-9739-7397429DDF25");
            ProgramModel programModel = Resolve<IProgramService>().GetProgramByProgramGUIDAndLanguageGUID(new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]), languageGuid);
            Response.Redirect(string.Format("EditProgram.aspx?{0}={1}&{2}={3}", Constants.QUERYSTR_PROGRAM_PAGE, ProgramPage, Constants.QUERYSTR_PROGRAM_GUID, programModel.Guid));
        }

        protected void NorwegianTestLanguageLinkBtn_Click(object sender, EventArgs e)
        {
            lastSelectedLanguage = NorwegianTestLanguageLinkBtn.Text;
            Guid languageGuid = new Guid("D6274B2D-FD5E-4D6F-BF88-9592F6E393C2");
            ProgramModel programModel = Resolve<IProgramService>().GetProgramByProgramGUIDAndLanguageGUID(new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]), languageGuid);
            Response.Redirect(string.Format("EditProgram.aspx?{0}={1}&{2}={3}", Constants.QUERYSTR_PROGRAM_PAGE, ProgramPage, Constants.QUERYSTR_PROGRAM_GUID, programModel.Guid));
        }

        protected void IcelandicLanguageLinkBtn_Click(object sender, EventArgs e)
        {
            lastSelectedLanguage = IcelandicLanguageLinkBtn.Text;
            Guid languageGuid = new Guid("2E20F0D1-1B72-41B9-B92B-6EDFB5A259E0");
            ProgramModel programModel = Resolve<IProgramService>().GetProgramByProgramGUIDAndLanguageGUID(new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]), languageGuid);
            Response.Redirect(string.Format("EditProgram.aspx?{0}={1}&{2}={3}", Constants.QUERYSTR_PROGRAM_PAGE, ProgramPage, Constants.QUERYSTR_PROGRAM_GUID, programModel.Guid));
        }

        protected void SpanishLanguageLinkBtn_Click(object sender, EventArgs e)
        {
            lastSelectedLanguage = SpanishLanguageLinkBtn.Text;
            Guid languageGuid = new Guid("57F6794F-0769-42BE-A9DE-9E731F9B1DCD");
            ProgramModel programModel = Resolve<IProgramService>().GetProgramByProgramGUIDAndLanguageGUID(new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]), languageGuid);
            Response.Redirect(string.Format("EditProgram.aspx?{0}={1}&{2}={3}", Constants.QUERYSTR_PROGRAM_PAGE, ProgramPage, Constants.QUERYSTR_PROGRAM_GUID, programModel.Guid));
        }

        protected void SwedishLanguageLinkBtn_Click(object sender, EventArgs e)
        {
            lastSelectedLanguage = SwedishLanguageLinkBtn.Text;
            Guid languageGuid = new Guid("19058246-3A39-4BEC-ACEA-A1E1B4466E62");
            ProgramModel programModel = Resolve<IProgramService>().GetProgramByProgramGUIDAndLanguageGUID(new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]), languageGuid);
            //ClientScript.RegisterStartupScript(GetType(), "selected", "$('li[name='language']').click(function () { $('li[class='active']').removeAttr('class');$(this).addClass('active');});", true);
            //Page.RegisterClientScriptBlock("OpenExportUserVariablePage", "<script> var url = document.URL.replace('EditProgram', 'ExportUserVariable');window.open(url, 'ExportUserVariable', 'width=400,height=160,toolbar=no,scrollbars=no,menubar=no,top=300,left=400');</script>");
            Response.Redirect(string.Format("EditProgram.aspx?{0}={1}&{2}={3}", Constants.QUERYSTR_PROGRAM_PAGE, ProgramPage, Constants.QUERYSTR_PROGRAM_GUID, programModel.Guid));
        }


        //protected void defaultLanguageDropdownlist_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    ProgramModel programModel = Resolve<IProgramService>().GetProgramByProgramGUIDAndLanguageGUID(new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]),
        //        new Guid(defaultLanguageDropdownlist.SelectedValue));

        //    Response.Redirect(string.Format("EditProgram.aspx?{0}={1}&{2}={3}", Constants.QUERYSTR_PROGRAM_PAGE, ProgramPage, Constants.QUERYSTR_PROGRAM_GUID, programModel.Guid));
        //}
    }
}
