using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ethos.Utility;
using Ethos.DependencyInjection;
using ChangeTech.Models;
using ChangeTech.Contracts;
using ChangeTech.Services;
using System.Web.Security;

namespace ChangeTech.DeveloperWeb
{
    public partial class CTPPSmartPhone : PageBase<CTPPEndUserPageModel>
    {
        #region get ViewState and QueryString
        private string InputBrand
        {
            get
            {
                if (ViewState["BrandURL"] != null)
                {
                    return ViewState["BrandURL"].ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                ViewState["BrandURL"] = value;
            }
        }
        private string InputProgram
        {
            get
            {
                if (ViewState["ProgramURL"] != null)
                {
                    return ViewState["ProgramURL"].ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                ViewState["ProgramURL"] = value;
            }
        }
        private string orderURL
        {
            get
            {
                if (ViewState["OrderURL"] != null)
                {
                    return ViewState["OrderURL"].ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                ViewState["OrderURL"] = value;
            }
        }
        private string ViewStateusername
        {
            get
            {
                if (ViewState["ViewStateusername"] != null)
                {
                    return ViewState["ViewStateusername"].ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                ViewState["ViewStateusername"] = value;
            }
        }
        private string ViewStatepassword
        {
            get
            {
                if (ViewState["ViewStatepassword"] != null)
                {
                    return ViewState["ViewStatepassword"].ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                ViewState["ViewStatepassword"] = value;
            }
        }
        private string CTPPEncriptString
        {
            get
            {
                return Context.Request.QueryString["CTPP"];
            }
        }
        private string PaymentEncriptString
        {
            get
            {
                return Context.Request.QueryString[Constants.QUERYSTR_PAYMENT_TRANSATION_ID];
            }
        }
        private int CTPPInactive
        {
            get
            {
                if (ViewState["ViewStateInactive"] != null)
                {
                    return int.Parse(ViewState["ViewStateInactive"].ToString());
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                ViewState["ViewStateInactive"] = value;
            }
        }
        private Guid BindProgramGuid
        {
            get
            {
                if (ViewState["ViewStateBindProgramGuid"] != null)
                {
                    return new Guid(ViewState["ViewStateBindProgramGuid"].ToString());
                }
                else
                {
                    return Guid.Empty;
                }
            }
            set
            {
                ViewState["ViewStateBindProgramGuid"] = value;
            }
        }
        private Guid BindUserGuid
        {
            get
            {
                if (ViewState["ViewStateBindUserGuid"] != null)
                {
                    return new Guid(ViewState["ViewStateBindUserGuid"].ToString());
                }
                else
                {
                    return Guid.Empty;
                }
            }
            set
            {
                ViewState["ViewStateBindUserGuid"] = value;
            }
        }
        #endregion

        #region public variables
        public string ProgramSubColor
        {
            //ProgramSubheadColor
            get
            {
                if (ViewState["ProgramSubColor"] != null)
                {
                    return ViewState["ProgramSubColor"].ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                ViewState["ProgramSubColor"] = value;
            }
        }
        public string ProgramNameForTitle
        {
            //Program Name
            get
            {
                if (ViewState["ProgramNameForTitle"] != null)
                {
                    return ViewState["ProgramNameForTitle"].ToString();
                }
                else
                {
                    return "Changetech Program Page";
                }
            }
            set
            {
                ViewState["ProgramNameForTitle"] = value;
            }
        }
        public string HelpRelapsePageSequenceGuid
        {
            get
            {
                if (ViewState["HelpRelapsePageSequenceGuid"] != null)
                {
                    return ViewState["HelpRelapsePageSequenceGuid"].ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                ViewState["HelpRelapsePageSequenceGuid"] = value;
            }
        }
        public string ReportRelapsePageSequenceGuid
        {
            get
            {
                if (ViewState["ReportRelapsePageSequenceGuid"] != null)
                {
                    return ViewState["ReportRelapsePageSequenceGuid"].ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                ViewState["ReportRelapsePageSequenceGuid"] = value;
            }
        }
        public int ReportRelapseAvailableTime
        {
            get
            {
                int _availableTime = 24;
                if (ViewState["ReportRelapseAvailableTime"] != null)
                {
                    int.TryParse(ViewState["ReportRelapseAvailableTime"].ToString(), out _availableTime);
                }
                return _availableTime;
            }
            set
            {
                ViewState["ReportRelapseAvailableTime"] = value;
            }
        }
        public DateTime? ReportRelapseLastFinishTime
        {
            get
            {
                DateTime? _lastReportTime = null;
                if (ViewState["ReportRelapseLastFinishTime"] != null)
                {
                    DateTime _tempTime;
                    if (DateTime.TryParse(ViewState["ReportRelapseLastFinishTime"].ToString(), out _tempTime))
                    {
                        _lastReportTime = _tempTime;
                    }
                }
                return _lastReportTime;
            }
            set
            {
                ViewState["ReportRelapseLastFinishTime"] = value;
            }
        }
        public string MobileBookmarkLink
        {
            get
            {
                if (ViewState["MobileBookmarkLink"] != null)
                {
                    return ViewState["MobileBookmarkLink"].ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                ViewState["MobileBookmarkLink"] = value;
            }
        }
        #endregion

        #region const variable
        const string TEMPUSERNAME = "TempUser";
        const int SESSIONNOTDONE = 0;//Not done
        const int SESSIONDONE = 1;//Done
        const int SESSIONSTART = 2;//Start.//Assert: only 1 row can be into this.
        const string LoginTitle = "Log in";
        const string UserName = "UserName";
        const string Password = "Password";
        const string LoginOutTitle = "LOGOUT";

        #endregion

        #region Page Load

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!ServiceUtility.IsMobileDevice)
            {
                Response.Redirect(Request.Url.AbsoluteUri.Replace(Constants.URL_DEVELOPER_CTPPS, Constants.URL_DEVELOPER_CTPP), true);
            }

            try
            {
                LogUtility.LogUtilityIntance.LogMessage(Request.Url.ToString());
                if (!IsPostBack)
                {
                    CurrentStatus();
                    if (IsClearInvalidUser())
                    {
                        return;//If true, it will refresh the page by redirect(currentURL,false), so here it is return
                    }
                    CTPPModel thisCTPPModel = null;
                    thisCTPPModel = InitialPage();
                    if (thisCTPPModel != null)
                    {
                        BindCTPPModel(thisCTPPModel);
                    }
                    else
                    {
                        Response.Redirect("/InvalidUrl.aspx", false);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, "CTPP");
                Response.Redirect("/InvalidUrl.aspx", false);
                return;
            }
        }
        #endregion

        #region private functions PageLoad Related.
        private CTPPModel InitialPage()
        {
            CTPPModel thisCTPPModel = null;
            Guid requestProgramGuid = Guid.Empty, requestUserGuid = Guid.Empty, requestPageGuid = Guid.Empty;
            string requestErrorInfo = string.Empty;
            // From flash side
            if (!string.IsNullOrEmpty(CTPPEncriptString))//?CTPP=......
            {
                //01; ProgramGUID；UserGUID；PageGUID
                string[] CTPPInfo = StringUtility.MD5Decrypt(CTPPEncriptString, Constants.MD5_KEY).Split(';');
                requestProgramGuid = new Guid(CTPPInfo[1]);//used for get ctpp brand and program
                thisCTPPModel = Resolve<ICTPPService>().GetCTPP(requestProgramGuid);
                if (thisCTPPModel != null)
                {
                    BrandModel requestBrandModel = Resolve<IBrandService>().GetBrandByGUID(thisCTPPModel.BrandGUID);
                    InputBrand = requestBrandModel.BrandURL;
                    InputProgram = thisCTPPModel.ProgramURL;
                    requestUserGuid = new Guid(CTPPInfo[2]);//used for user login
                    NoteUserFromFlash(requestUserGuid);

                    int isValid = int.Parse(CTPPInfo[0]);
                    // 0: Error code 
                    // 1: Last page or session ending
                    if (isValid == 1)
                    {
                        requestPageGuid = new Guid(CTPPInfo[3]);
                        if (requestPageGuid == Guid.Empty)
                        {
                            CTPPFromSessionEnding(requestProgramGuid, thisCTPPModel);
                        }
                        else
                        {
                            CTPPFromLastPage(requestPageGuid, thisCTPPModel);
                        }
                    }
                    else
                    {
                        CTPPFromErrorCode(requestProgramGuid, CTPPInfo[3], thisCTPPModel);
                    }
                }
            }
            else if (!string.IsNullOrEmpty(PaymentEncriptString))//From payment
            {
                string paymentTransationID = StringUtility.MD5Decrypt(PaymentEncriptString, Constants.MD5_KEY);
                PaymentTemplateModel paymodel = Resolve<IPaymentService>().GetCompletePaymentTip(paymentTransationID);
                thisCTPPModel = Resolve<ICTPPService>().GetCTPP(paymodel.ProgramGUID);
                if (thisCTPPModel != null)
                {
                    BrandModel requestBrandModel = Resolve<IBrandService>().GetBrandByGUID(thisCTPPModel.BrandGUID);
                    InputBrand = requestBrandModel.BrandURL;
                    InputProgram = thisCTPPModel.ProgramURL;
                    requestUserGuid = paymodel.UserGUID;//used for user login
                    NoteUserFromFlash(requestUserGuid);

                    //Bubble_titile_From_Payment_in_CTPP
                    LanguageModel thisLanguage = Resolve<IProgramService>().GetProgramLanguage(paymodel.ProgramGUID);
                    SpecialStringModel titleSpecialString = new SpecialStringModel();
                    if (thisLanguage != null && thisLanguage.LanguageGUID != Guid.Empty)
                    {
                        titleSpecialString = Resolve<ISpecialStringService>().GetSpecialStringBy("Bubble_titile_From_Payment_in_CTPP", thisLanguage.LanguageGUID);
                    }
                    else//Default language  Norwegian 
                    {
                        titleSpecialString = Resolve<ISpecialStringService>().GetSpecialStringBy("Bubble_titile_From_Payment_in_CTPP", new Guid("FB5AE1DC-4CAF-4613-9739-7397429DDF25"));
                    }

                    //Resolve<ISpecialStringService>().GetSpecialStringBy("Bubble_titile_From_Payment_in_CTPP",
                    if (paymodel.IsPaid)//pay success
                    {
                        this.h1Header.InnerHtml = (titleSpecialString == null || titleSpecialString.Value == null) ? string.Empty : titleSpecialString.Value.Replace("\r\n", "<br/>").Replace("\r", "<br/>").Replace("\n", "<br/>");
                        this.pDescription.InnerHtml = paymodel.SuccessfulTip == null ? string.Empty : paymodel.SuccessfulTip.Replace("\r\n", "<br/>").Replace("\r", "<br/>").Replace("\n", "<br/>").Replace("<br/><br/>", "<span class=\"emptyline\" style=\"display:block;\">&nbsp;</span>");
                        string accountName = Resolve<ISystemSettingService>().GetSettingValue("AzureStorageAccountName");
                        string bolbPath = ServiceUtility.GetBlobPath(accountName);
                        string ImageUrl = "";
                        if (thisCTPPModel.ProgramPresenter != null && thisCTPPModel.ProgramPresenter.NameOnServer.Any())
                        {
                            //ImageUrl = bolbPath + BlobContainerType.OriginalImageContainer.ToString().ToLower() + "/" + lastpagecontentmodel.PresenterImage.NameOnServer;
                            ImageUrl = "../RequestResource.aspx?target=Image&media=" + thisCTPPModel.ProgramPresenter.NameOnServer;
                        }
                        this.imgCont.InnerHtml = string.Format("<img id=\"over\" src=\"{0}\" style=\"display:none;\"/><img id=\"under\" src=\"../Images/bg-programwindow.png\" style=\"display:none;\"/><canvas id=\"out\"></canvas>", ImageUrl);
                        //this.imgCont.Style.Add("background", string.Format("url({0}) 10px 0px no-repeat;", ImageUrl));

                    }
                    else//payment fail
                    {
                        this.h1Header.InnerHtml = (titleSpecialString == null || titleSpecialString.Value == null) ? string.Empty : titleSpecialString.Value.Replace("\r\n", "<br/>").Replace("\r", "<br/>").Replace("\n", "<br/>");
                        this.pDescription.InnerHtml = paymodel.ExceptionTip == null ? string.Empty : paymodel.ExceptionTip.Replace("\r\n", "<br/>").Replace("\r", "<br/>").Replace("\n", "<br/>").Replace("<br/><br/>", "<span class=\"emptyline\" style=\"display:block;\">&nbsp;</span>");
                        string accountName = Resolve<ISystemSettingService>().GetSettingValue("AzureStorageAccountName");
                        string bolbPath = ServiceUtility.GetBlobPath(accountName);
                        string ImageUrl = "";
                        if (thisCTPPModel.ProgramPresenter != null && thisCTPPModel.ProgramPresenter.NameOnServer.Any())
                        {
                            //ImageUrl = bolbPath + BlobContainerType.OriginalImageContainer.ToString().ToLower() + "/" + lastpagecontentmodel.PresenterImage.NameOnServer;
                            ImageUrl = "../RequestResource.aspx?target=Image&media=" + thisCTPPModel.ProgramPresenter.NameOnServer;
                        }
                        this.imgCont.InnerHtml = string.Format("<img id=\"over\" src=\"{0}\" style=\"display:none;padding-left:10px;\"/><img id=\"under\" src=\"../Images/bg-programwindow.png\" style=\"display:none;padding-left:10px;\"/><canvas id=\"out\"></canvas>", ImageUrl);
                        //this.imgCont.Style.Add("background", string.Format("url({0}) 10px 0px no-repeat;", ImageUrl));
                    }
                }
            }
            else//clear url
            {
                InputBrand = Request.QueryString["brand"];
                InputProgram = Request.QueryString["program"];
                thisCTPPModel = Resolve<ICTPPService>().GetCTPPByBrandAndProgram(InputBrand, InputProgram);
                if (thisCTPPModel != null)
                {
                    UserModel user = Resolve<IUserService>().GetUserByUserName(Context.User.Identity.Name);
                    if (user == null)
                    {
                        DisplayTheLoginMenu();
                        //CTPPNotFromFlashOrPayment(thisCTPPModel);
                    }
                    else
                    {
                        string returnMessage = Resolve<IUserService>().EndUserLoginForCTPP(user.UserName, user.PassWord, thisCTPPModel.ProgramGUID.ToString().Trim());
                        string[] logedStr = returnMessage.Split(';');
                        if (logedStr[0] == "1")
                        {
                            HideTheLoginMenu();
                            //CTPPNotFromFlashOrPayment(thisCTPPModel);
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(Context.User.Identity.Name))
                            {
                                ClearCurrentUser();//Clear Current UserInfo.
                            }
                            else
                            {
                                DisplayTheLoginMenu();
                                //CTPPNotFromFlashOrPayment(thisCTPPModel);
                            }
                        }
                    }
                    CTPPNotFromFlashOrPayment(thisCTPPModel);
                }
            }

            if (thisCTPPModel != null)
            {
                ProgramNameForTitle = thisCTPPModel.ProgramName;
                HelpRelapsePageSequenceGuid = thisCTPPModel.HelpButtonRelapsePageSequenceGuid.ToString();
                ReportRelapsePageSequenceGuid = thisCTPPModel.ReportButtonRelapsePageSequenceGuid.ToString();

                if (thisCTPPModel.ReportButtonAvailableTime != null)
                    ReportRelapseAvailableTime = (int)thisCTPPModel.ReportButtonAvailableTime;
            }
            return thisCTPPModel;

        }
        private void CurrentStatus()
        {
            if (HttpContext.Current == null)
            {
                LogUtility.LogUtilityIntance.LogMessage("HttpContext.Current is null");
            }
            else
            {
                if (HttpContext.Current.User == null)
                {
                    LogUtility.LogUtilityIntance.LogMessage("HttpContext.Current.User is null");
                }
                if (HttpContext.Current.Session == null)
                {
                    LogUtility.LogUtilityIntance.LogMessage("HttpContext.Current.Session is null");
                }
            }
        }
        private bool IsClearInvalidUser()
        {
            bool isClearUser = false;
            if (HttpContext.Current.Session["CurrentAccount"] != null && ContextService.CurrentAccount != null)//IsAuthenticated
            {
                if (ContextService.CurrentAccount.UserType != UserTypeEnum.User)
                {
                    ClearCurrentUser();
                    isClearUser = true;
                }
            }
            return isClearUser;
        }
        private void ClearCurrentUser()
        {
            if (Request.QueryString["brand"] != null && Request.QueryString["program"] != null)
            {
                InputBrand = Request.QueryString["brand"];
                InputProgram = Request.QueryString["program"];
            }
            FormsAuthentication.SignOut();
            ContextService.ClearAccount();
            string url = Request.Url.PathAndQuery;
            url = url.Replace("#", "");
            Response.Redirect(url, false);
        }

        private void NoteUserFromFlash(Guid userGuid)
        {
            UserModel requestUserModel = Resolve<IUserService>().GetUserModelByUserGUID(userGuid);
            //if the user is temp user, the login name should no display CHANGETECHTEMP+UserGuid, but display "TempUser"
            //So don't use the userName to get this user in this page.
            if (requestUserModel != null && requestUserModel.UserName != null && requestUserModel.UserName.ToUpper().Contains("CHANGETECHTEMP"))
            {
                requestUserModel.UserName = TEMPUSERNAME;
            }
            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
            requestUserModel.UserName,
            false,
            1440 * 30);
            string encyptedTicket = FormsAuthentication.Encrypt(authTicket);
            HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encyptedTicket);
            authCookie.Expires = DateTime.UtcNow.AddMonths(1); 
            Response.Cookies.Add(authCookie);
            AuthenticateEventArgs ex = new AuthenticateEventArgs();
            ex.Authenticated = true;
            ContextService.CurrentAccount = requestUserModel;
            string role = authTicket.UserData;
            FormsIdentity id = new FormsIdentity(authTicket);
            System.Security.Principal.GenericPrincipal principal = new System.Security.Principal.GenericPrincipal(id, new string[] { role });
            Context.User = principal;
        }
        private void CTPPFromSessionEnding(Guid requestProgramGuid, CTPPModel model)
        {
            AccessoryPageModel sessionEndingPage = Resolve<IProgramAccessoryService>().GetProgramAccessoryByProgarm(requestProgramGuid, "Session ending");
            this.h1Header.InnerHtml = (sessionEndingPage == null || sessionEndingPage.Heading == null) ? string.Empty : sessionEndingPage.Heading.Replace("\r\n", "<br/>").Replace("\r", "<br/>").Replace("\n", "<br/>");
            this.pDescription.InnerHtml = (sessionEndingPage == null || sessionEndingPage.Text == null) ? string.Empty : sessionEndingPage.Text.Replace("\r\n", "<br/>").Replace("\r", "<br/>").Replace("\n", "<br/>").Replace("<br/><br/>", "<span class=\"emptyline\" style=\"display:block;\">&nbsp;</span>");
            string accountName = Resolve<ISystemSettingService>().GetSettingValue("AzureStorageAccountName");
            string bolbPath = ServiceUtility.GetBlobPath(accountName);
            string ImageUrl = "";
            if (model.ProgramPresenter != null && model.ProgramPresenter.NameOnServer.Any())
            {
                ImageUrl = "../RequestResource.aspx?target=Image&media=" + model.ProgramPresenter.NameOnServer;
            }
            this.imgCont.InnerHtml = string.Format("<img id=\"over\" src=\"{0}\" style=\"display:none;padding-left:10px;\"/><img id=\"under\" src=\"../Images/bg-programwindow.png\" style=\"display:none;padding-left:10px;\"/><canvas id=\"out\"></canvas>", ImageUrl);
            //this.imgCont.Style.Add("background", string.Format("url({0}) 10px 0px no-repeat;", ImageUrl));
        }
        private void CTPPFromLastPage(Guid requestPageGuid, CTPPModel model)
        {
            PageContentForCTPPLastPageModel lastpagecontentmodel = Resolve<IPageService>().getPageContentForCTPP(requestPageGuid);
            this.h1Header.InnerHtml = (lastpagecontentmodel == null || lastpagecontentmodel.Heading == null) ? string.Empty : lastpagecontentmodel.Heading.Replace("\r\n", "<br/>").Replace("\r", "<br/>").Replace("\n", "<br/>");
            this.pDescription.InnerHtml = (lastpagecontentmodel == null || lastpagecontentmodel.Body == null) ? string.Empty : lastpagecontentmodel.Body.Replace("\r\n", "<br/>").Replace("\r", "<br/>").Replace("\n", "<br/>").Replace("<br/><br/>", "<span class=\"emptyline\" style=\"display:block;\">&nbsp;</span>");
            string accountName = Resolve<ISystemSettingService>().GetSettingValue("AzureStorageAccountName");
            string bolbPath = ServiceUtility.GetBlobPath(accountName);
            string ImageUrl = "";
            if (lastpagecontentmodel.PresenterImage != null && !string.IsNullOrEmpty(lastpagecontentmodel.PresenterImage.NameOnServer))
            {
                ImageUrl = "../RequestResource.aspx?target=Image&media=" + lastpagecontentmodel.PresenterImage.NameOnServer;
            }
            else if (model.ProgramPresenter != null && !string.IsNullOrEmpty(model.ProgramPresenter.NameOnServer))
            {
                //ImageUrl = bolbPath + BlobContainerType.OriginalImageContainer.ToString().ToLower() + "/" + lastpagecontentmodel.PresenterImage.NameOnServer;
                ImageUrl = "../RequestResource.aspx?target=Image&media=" + model.ProgramPresenter.NameOnServer;
            }
            this.imgCont.InnerHtml = string.Format("<img id=\"over\" src=\"{0}\" style=\"display:none;padding-left:10px;\"/><img id=\"under\" src=\"../Images/bg-programwindow.png\" style=\"display:none;padding-left:10px;\"/><canvas id=\"out\"></canvas>", ImageUrl);
            //this.imgCont.Style.Add("background", string.Format("url({0}) 10px 0px no-repeat;", ImageUrl));

        }
        private void CTPPFromErrorCode(Guid requestProgramGuid, string message, CTPPModel model)
        {
            string endingFailedMsg = Resolve<ITipMessageService>().GetTipMessageText(requestProgramGuid, message);
            this.h1Header.InnerHtml = "";
            this.pDescription.InnerHtml = endingFailedMsg == null ? string.Empty : endingFailedMsg.Replace("\r\n", "<br/>").Replace("\r", "<br/>").Replace("\n", "<br/>").Replace("<br/><br/>", "<span class=\"emptyline\" style=\"display:block;\">&nbsp;</span>");
            string accountName = Resolve<ISystemSettingService>().GetSettingValue("AzureStorageAccountName");
            string bolbPath = ServiceUtility.GetBlobPath(accountName);
            string ImageUrl = "";
            if (model.ProgramPresenter != null && model.ProgramPresenter.NameOnServer.Any())
            {
                //ImageUrl = bolbPath + BlobContainerType.OriginalImageContainer.ToString().ToLower() + "/" + thisCTPPModel.ProgramPresenter.NameOnServer;
                ImageUrl = "../RequestResource.aspx?target=Image&media=" + model.ProgramPresenter.NameOnServer;
            }
            this.imgCont.InnerHtml = string.Format("<img id=\"over\" src=\"{0}\" style=\"display:none;padding-left:10px;\"/><img id=\"under\" src=\"../Images/bg-programwindow.png\" style=\"display:none;padding-left:10px;\"/><canvas id=\"out\"></canvas>", ImageUrl);
            //this.imgCont.Style.Add("background", string.Format("url({0}) 10px 0px no-repeat;", ImageUrl));

        }
        private void CTPPNotFromFlashOrPayment(CTPPModel model)
        {
            this.h1Header.InnerHtml = string.IsNullOrEmpty(model.ProgramDescriptionTitleForMobile) ? string.Empty : model.ProgramDescriptionTitleForMobile.Replace("\r\n", "<br/>").Replace("\r", "<br/>").Replace("\n", "<br/>");
            this.pDescription.InnerHtml = string.IsNullOrEmpty(model.ProgramDescriptionForMobile) ? string.Empty : model.ProgramDescriptionForMobile.Replace("\r\n", "<br/>").Replace("\r", "<br/>").Replace("\n", "<br/>").Replace("<br/><br/>", "<span class=\"emptyline\" style=\"display:block;\">&nbsp;</span>");
            string accountName = Resolve<ISystemSettingService>().GetSettingValue("AzureStorageAccountName");
            string bolbPath = ServiceUtility.GetBlobPath(accountName);
            string ImageUrl = "";
            if (model.ProgramPresenter != null && model.ProgramPresenter.NameOnServer.Any())
            {
                //ImageUrl = bolbPath + BlobContainerType.OriginalImageContainer.ToString().ToLower() + "/" + thisCTPPModel.ProgramPresenter.NameOnServer;
                ImageUrl = "../RequestResource.aspx?target=Image&media=" + model.ProgramPresenter.NameOnServer;
            }
            this.imgCont.InnerHtml = string.Format("<img id=\"over\" src=\"{0}\" style=\"display:none;padding-left:10px;\"/><img id=\"under\" src=\"../Images/bg-programwindow.png\" style=\"display:none;padding-left:10px;\"/><canvas id=\"out\"></canvas>", ImageUrl);
            //this.imgCont.Style.Add("background", string.Format("url({0}) 10px 0px no-repeat;", ImageUrl));
        }

        private void BindCTPPColor(CTPPModel thisCTPPModel)
        {
            HttpBrowserCapabilities bc = Request.Browser;

            System.Web.UI.HtmlControls.HtmlGenericControl thisDivControl = new System.Web.UI.HtmlControls.HtmlGenericControl();
            thisDivControl = (System.Web.UI.HtmlControls.HtmlGenericControl)this.FindControl("wrapper_topline");
            switch (bc.Browser.ToUpper())
            {
                case "IE":
                    thisDivControl.Style.Add("background", string.Format("{0}", thisCTPPModel.BackDarkColor));
                    break;
                case "SAFARI":
                    thisDivControl.Style.Add("background", string.Format("-webkit-linear-gradient(top, {0}, {1})", thisCTPPModel.BackLightColor, thisCTPPModel.BackDarkColor));
                    //thisDivControl.Style.Add("background", string.Format("-webkit-gradient(linear, left top, left bottom, from({0}), to({1})", thisCTPPModel.BackLightColor, thisCTPPModel.BackDarkColor));//not used.
                    break;
                case "CHROME":
                    thisDivControl.Style.Add("background", string.Format("-webkit-linear-gradient(top, {0}, {1})", thisCTPPModel.BackLightColor, thisCTPPModel.BackDarkColor));
                    //thisDivControl.Style.Add("background", string.Format("-webkit-gradient(linear, left top, left bottom, from({0}), to({1})", thisCTPPModel.BackLightColor, thisCTPPModel.BackDarkColor));//not used.
                    break;
                case "FIREFOX":
                    thisDivControl.Style.Add("background", string.Format("-moz-linear-gradient(top, {0}, {1})", thisCTPPModel.BackLightColor, thisCTPPModel.BackDarkColor));
                    break;
                case "OPERA":
                    thisDivControl.Style.Add("background", string.Format("-o-linear-gradient(top, {0}, {1})", thisCTPPModel.BackLightColor, thisCTPPModel.BackDarkColor));
                    break;
                default:
                    thisDivControl.Style.Add("background", string.Format("{0}", thisCTPPModel.BackDarkColor));
                    break;
            }
        }

        private void BindIsInactive(CTPPModel thisCTPPModel)
        {
            if (thisCTPPModel.InActive == 1)//inactive  no login ,no order
            {
                CTPPInactive = 1;
                this.LoginView.Visible = false;
                bindSessionListAndSpeStr(thisCTPPModel.ProgramGUID, Guid.Empty, thisCTPPModel);
            }
            else//active   has login,order function
            {
                CTPPInactive = 0;
                if (HttpContext.Current.User != null &&
                    HttpContext.Current.User.Identity.IsAuthenticated && ContextService.CurrentAccount != null)
                {
                    UserModel thisuser = Resolve<IUserService>().GetUserModelByUserGUID(ContextService.CurrentAccount.UserGuid);
                    ViewStateusername = thisuser.UserName;
                    ViewStatepassword = thisuser.PassWord;
                    bindSessionListAndSpeStr(thisCTPPModel.ProgramGUID, thisuser.UserGuid, thisCTPPModel);
                }
                else
                {
                    bindSessionListAndSpeStr(thisCTPPModel.ProgramGUID, Guid.Empty, thisCTPPModel);
                }
            }
        }

        private void BindLoginPanelSpecialString(CTPPModel thisCTPPModel)
        {
            SpecialStringModel specialStringModelByLogin = Resolve<ISpecialStringService>().GetSpecialStringBy("Login", thisCTPPModel.LanguageGUID);
            SpecialStringModel specialStringModelByUserName = Resolve<ISpecialStringService>().GetSpecialStringBy("UserName", thisCTPPModel.LanguageGUID);
            SpecialStringModel specialStringModelByPassword = Resolve<ISpecialStringService>().GetSpecialStringBy("Password", thisCTPPModel.LanguageGUID);

            if (specialStringModelByLogin != null)
            {
                this.lblLoginTitle.Text = !string.IsNullOrEmpty(specialStringModelByLogin.Value) ? specialStringModelByLogin.Value : LoginTitle;
            }
            if (specialStringModelByUserName != null)
            {
                this.lblUserName.Text = !string.IsNullOrEmpty(specialStringModelByUserName.Value) ? specialStringModelByUserName.Value : UserName;
            }
            if (specialStringModelByPassword != null)
            {
                this.lblPassword.Text = !string.IsNullOrEmpty(specialStringModelByPassword.Value) ? specialStringModelByPassword.Value : Password;
            }
        }

        private void BindCTPPModel(CTPPModel thisCTPPModel)
        {
            BindLoginPanelSpecialString(thisCTPPModel);
            //bind MobileBookmarkLink
            if (thisCTPPModel.MobileBookmarkLink != null)
            {
                // value=changetechstorage
                string accountNameForMobileBookmark = Resolve<ISystemSettingService>().GetSettingValue("AzureStorageAccountName");
                //"http://changetechstorage.blob.core.windows.net/logocontainer/"
                string bolbPathForMobileBookmark = ServiceUtility.GetBlobPath(accountNameForMobileBookmark);
                MobileBookmarkLink = bolbPathForMobileBookmark + BlobContainerTypeEnum.LogoContainer.ToString().ToLower() + "/" + thisCTPPModel.MobileBookmarkLink.NameOnServer;
            }
            else
            {
                MobileBookmarkLink = "/Images/icon-sosinternational.png";
            }

            var newBrandModel = Resolve<IBrandService>().GetBrandByGUID(thisCTPPModel.BrandGUID);
            if (newBrandModel.BrandLogo != null)
            {
                string accountName = Resolve<ISystemSettingService>().GetSettingValue("AzureStorageAccountName");
                string bolbPath = ServiceUtility.GetBlobPath(accountName);
                string logoSrc = bolbPath + BlobContainerTypeEnum.LogoContainer.ToString().ToLower() + "/" + newBrandModel.BrandLogo.NameOnServer;
                System.Web.UI.HtmlControls.HtmlGenericControl thisDivControl = new System.Web.UI.HtmlControls.HtmlGenericControl();
                thisDivControl = (System.Web.UI.HtmlControls.HtmlGenericControl)this.FindControl("programlogo");
                thisDivControl.Style.Add("background", string.Format("url({0}) 0px 20px no-repeat;", logoSrc));

            }
            this.programtitle.InnerHtml = thisCTPPModel.ProgramName;

            if (!string.IsNullOrEmpty(thisCTPPModel.BackDarkColor) && !string.IsNullOrEmpty(thisCTPPModel.BackLightColor))
            {
                BindCTPPColor(thisCTPPModel);
            }
            if (!string.IsNullOrEmpty(thisCTPPModel.ProgramSubheadColor))
            {
                ProgramSubColor = thisCTPPModel.ProgramSubheadColor;
                ((System.Web.UI.HtmlControls.HtmlGenericControl)this.FindControl("programlogo")).Style.Add("border-right", string.Format("1px dotted {0}", thisCTPPModel.ProgramSubheadColor));
            }

            BindIsInactive(thisCTPPModel);
            SetHelpAndReportButtonArea(thisCTPPModel);
        }
        #endregion

        #region Bind Session List
        void bindSessionListAndSpeStr(Guid programGuid, Guid userGuid, CTPPModel thisCTPPModel)
        {
            BindProgramGuid = programGuid;//used for the session row databind for the "ishasdone==2,start day"
            BindUserGuid = userGuid;

            if (programGuid != Guid.Empty && userGuid != Guid.Empty)
                ReportRelapseLastFinishTime = Resolve<IProgramUserService>().GetLastReportRelapseTime(programGuid, userGuid);
            else
                ReportRelapseLastFinishTime = null;

            int numberOfSession = Resolve<ISessionService>().GetNumberOfSession(programGuid);
            InitialPageInfo((int)Math.Ceiling(Convert.ToDouble(numberOfSession) / 1000), "Name", "asc");//1000=PageSize

            PagingString = Ethos.Utility.PagingSortingService.InitialPagingString(Request.Url.PathAndQuery, PageNumber, CurrentPageNumber, Resources.Share.Previous, Resources.Share.Next);


            Model = Resolve<ISessionService>().GetCTPPEndUserPageModel(programGuid, userGuid, CurrentPageNumber, 1000, CTPPVersionEnum.SmartphoneVersion);//1000=PageSize
            //this.h1sessionoverview.InnerHtml = Model.SpeSDays_in_program;
            orderURL = Model.orderUrl;

            this.Container_default_homescreen_Heading.InnerHtml = Model.SpeSContainerHomescreenHeading;
            this.Container_default_homescreen_Text.InnerHtml = Model.SpeSContainerHomescreenText;
            this.Container_default_below_help_Text.InnerHtml = Model.SpeSContainerBelowHelpbuttonText;

            //this.rpSessions.DataSource = Model.endUserSessionModel;
            //this.rpSessions.DataBind();
        }
        #endregion

        #region logout/login
        protected void btnLogout_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            ContextService.ClearAccount();
            string url = Request.Url.PathAndQuery;

            //has ctpp querystring
            if (url.ToLower().IndexOf((Constants.URL_DEVELOPER_CTPPS.ToLower() + "?ctpp")) > -1 || (Request.QueryString["brand"] != null && Request.QueryString["program"] != null))
            {
                url = "/" + Constants.URL_DEVELOPER_CTPPS + "?brand=" + InputBrand + "&program=" + InputProgram;
            }
            //has PaymentTransationID querystring
            else if (url.ToLower().IndexOf((Constants.URL_DEVELOPER_CTPPS.ToLower() + "?" + Constants.QUERYSTR_PAYMENT_TRANSATION_ID.ToLower())) > -1 || (Request.QueryString["brand"] != null && Request.QueryString["program"] != null))
            {
                url = "/" + Constants.URL_DEVELOPER_CTPPS + "?brand=" + InputBrand + "&program=" + InputProgram;
            }
            else
            {
                //don't know what this mean, need to ask JGX.
                int removeAtIndex = url.IndexOf("?", 13);
                if (removeAtIndex > 0)
                {
                    if (url.Substring(removeAtIndex + 1, 5).ToLower() == "brand")
                    {
                        url = url.Remove(removeAtIndex);
                    }
                }
            }
            url = url.Replace("#", "");
            Response.Redirect(url, false);
            return;
        }
        #endregion

        #region login
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                lblWrongInfo.Text = "";
                lblWrongInfo.Visible = false;
                IContainerContext containerContext = ContainerManager.GetContainer("container");
                CTPPModel thisCTPPModel = Resolve<ICTPPService>().GetCTPPByBrandAndProgram(InputBrand, InputProgram);
                string returnMessage = "";
                if (thisCTPPModel != null)
                {
                    returnMessage = Resolve<IUserService>().EndUserLoginForCTPP(this.txtUserName.Text.Trim(), this.txtPassword.Text.Trim(), thisCTPPModel.ProgramGUID.ToString().Trim());
                }
                else
                {
                    returnMessage = returnMessage = Convert.ToInt32(ReturnMessageCodeEnum.ErrorCode).ToString() + ";Can't find this program.";//thisCTPPModel is null.
                }
                string[] logedStr = returnMessage.Split(';');
                if (logedStr[0] == "1")
                {
                    //ClientScript.RegisterStartupScript(this.GetType(), "", "HideLoginMenu();");
                    HideTheLoginMenu();


                    //result = Convert.ToInt32(ReturnMessageCodeEnum.SuccessCode).ToString() + ";" + user.UserGUID + ";" + programGuid + ";" + ((int)programUser.Day + 1);
                    Guid userGuid = new Guid(logedStr[1]);
                    Guid programGuid = new Guid(logedStr[2]);
                    int programUserDay = int.Parse(logedStr[3].Trim());

                    UserModel requestUserModel = Resolve<IUserService>().GetUserModelByUserGUID(userGuid);
                    FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                    requestUserModel.UserName,
                    false,
                    1440 * 30);
                    string encyptedTicket = FormsAuthentication.Encrypt(authTicket);
                    // save user's info to cookie
                    HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encyptedTicket);
                    authCookie.Expires = DateTime.UtcNow.AddMonths(1);
                    Response.Cookies.Add(authCookie);
                    AuthenticateEventArgs ex = new AuthenticateEventArgs();
                    ex.Authenticated = true;
                    ContextService.CurrentAccount = requestUserModel;

                    string role = authTicket.UserData;
                    FormsIdentity id = new FormsIdentity(authTicket);
                    System.Security.Principal.GenericPrincipal principal = new System.Security.Principal.GenericPrincipal(id, new string[] { role });
                    Context.User = principal;

                    ViewStateusername = requestUserModel.UserName;
                    ViewStatepassword = requestUserModel.PassWord;

                    bindSessionListAndSpeStr(thisCTPPModel.ProgramGUID, requestUserModel.UserGuid, thisCTPPModel);

                    HelpRelapsePageSequenceGuid = thisCTPPModel.HelpButtonRelapsePageSequenceGuid.ToString();
                    ReportRelapsePageSequenceGuid = thisCTPPModel.ReportButtonRelapsePageSequenceGuid.ToString();

                    if (thisCTPPModel.ReportButtonAvailableTime != null)
                        ReportRelapseAvailableTime = (int)thisCTPPModel.ReportButtonAvailableTime;

                    SetHelpAndReportButtonArea(thisCTPPModel);
                }
                else
                {
                    SpecialStringModel specialStringModelByLogout = Resolve<ISpecialStringService>().GetSpecialStringBy(logedStr[1], thisCTPPModel.LanguageGUID);
                    //ClientScript.RegisterStartupScript(this.GetType(), "", "DisplayLoginMenu();");
                    DisplayTheLoginMenu();
                    lblWrongInfo.Visible = true;
                    if (specialStringModelByLogout != null)
                    {
                        lblWrongInfo.Text = !string.IsNullOrEmpty(specialStringModelByLogout.Value) ? specialStringModelByLogout.Value : logedStr[1];
                    }
                    else
                    {
                        lblWrongInfo.Text = logedStr[1];
                    }
                    return;
                }
            }
            catch (Exception ex)
            {
                //ClientScript.RegisterStartupScript(this.GetType(), "", "DisplayLoginMenu();");  
                DisplayTheLoginMenu();
                lblWrongInfo.Visible = true;
                lblWrongInfo.Text = ex.ToString();
                return;
            }

        }
        private void DisplayTheLoginMenu()
        {
            this.regularContent.Style.Value = "display:none;";
            this.dialoguebox.Style.Value = "display:block;";
        }
        private void HideTheLoginMenu()
        {
            this.regularContent.Style.Value = "display:block;";
            this.dialoguebox.Style.Value = "display:none;";
        }
        #endregion


        #region item command
        protected void rpSessions_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            //ItemCommand
        }
        #endregion


        private string GetHelpButtonLinkAddress()
        {
            string url = string.Empty;
            ProgramPropertyModel proPovertyModel = Resolve<IProgramService>().GetProgramProperty(BindProgramGuid);
            string serverPath = string.Empty;
            if (proPovertyModel.IsSupportHttps)
            {
                serverPath = Resolve<ISystemSettingService>().GetSettingValue("WebServerPathHttps");
            }
            else
            {
                serverPath = Resolve<ISystemSettingService>().GetSettingValue("WebServerPath");
            }
            string programCode = proPovertyModel.ProgramCode;
            string securityStr = StringUtility.MD5Encrypt(string.Format("{0};{1};{2};{3}", ViewStateusername, ViewStatepassword, UserTaskTypeEnum.HelpInCTPP.ToString(), HelpRelapsePageSequenceGuid), Constants.MD5_KEY);
            url = string.Format("{0}ChangeTech.html?P={1}&Mode=Live&Security={2}", serverPath, programCode, securityStr);
            url = url.Replace("#", "");
            return url;
        }

        private string GetReportButtonLinkAddress()
        {
            string url = string.Empty;
            ProgramPropertyModel proPovertyModel = Resolve<IProgramService>().GetProgramProperty(BindProgramGuid);
            string serverPath = string.Empty;
            if (proPovertyModel.IsSupportHttps)
            {
                serverPath = Resolve<ISystemSettingService>().GetSettingValue("WebServerPathHttps");
            }
            else
            {
                serverPath = Resolve<ISystemSettingService>().GetSettingValue("WebServerPath");
            }
            string programCode = proPovertyModel.ProgramCode;
            string securityStr = StringUtility.MD5Encrypt(string.Format("{0};{1};{2};{3}", ViewStateusername, ViewStatepassword, UserTaskTypeEnum.ReportInCTPP.ToString(), ReportRelapsePageSequenceGuid), Constants.MD5_KEY);
            url = string.Format("{0}ChangeTech.html?P={1}&Mode=Live&Security={2}", serverPath, programCode, securityStr);
            url = url.Replace("#", "");
            return url;
        }

        private void SetDisplayStatusForHelpRelapse()
        {
            HelpAndReportButtonDisplaySettingEnum displayStatus = HelpAndReportButtonDisplaySettingEnum.InVisible;// Help button default invisible.  //Not the same as Report button
            if (HttpContext.Current.Session["CurrentAccount"] != null && ContextService.CurrentAccount != null)//IsAuthenticated
            {
                if (ContextService.CurrentAccount.UserType == UserTypeEnum.User)
                {
                    if (!string.IsNullOrEmpty(HelpRelapsePageSequenceGuid) && HelpRelapsePageSequenceGuid != Guid.Empty.ToString())
                    {
                        string status = string.Empty;
                        if (BindProgramGuid != Guid.Empty && BindUserGuid != Guid.Empty)
                        {
                            status = Resolve<IProgramUserService>().GetProgramUserStatus(BindProgramGuid, BindUserGuid);
                        }
                        if (status == ProgramUserStatusEnum.Active.ToString() || status == ProgramUserStatusEnum.Completed.ToString())
                        {
                            // Set current time according TimeZone
                            DateTime setCurrentTimeByTimeZone = DateTime.UtcNow;
                            setCurrentTimeByTimeZone = Resolve<IProgramUserService>().GetCurrentTimeByTimeZone(BindProgramGuid, BindUserGuid, setCurrentTimeByTimeZone);
                            //If IsNeedHelp now is true ,it is available
                            if (Resolve<ISessionService>().GetIsNeedHelp(BindProgramGuid, BindUserGuid, setCurrentTimeByTimeZone))
                            {
                                displayStatus = HelpAndReportButtonDisplaySettingEnum.Actual;
                            }
                            else
                            {
                                displayStatus = HelpAndReportButtonDisplaySettingEnum.UnAvailable;
                            }
                        }
                    }
                }
                else
                {
                    displayStatus = HelpAndReportButtonDisplaySettingEnum.InVisible;
                }
            }
            else
            {
                displayStatus = HelpAndReportButtonDisplaySettingEnum.InVisible;
            }

            if (BindProgramGuid != Guid.Empty)
            {
                CTPPModel ctppModel = Resolve<ICTPPService>().GetCTPP(BindProgramGuid);
                if (ctppModel != null && ctppModel.IsEnableSpecificReportAndHelpButtons == true)
                {
                    switch (displayStatus)
                    {
                        case HelpAndReportButtonDisplaySettingEnum.Actual:
                            this.DivHelpButtonArea.Visible = true;
                            this.DivHelpButtonArea.Attributes["class"] = "container-emergency-actual";
                            this.HelpButtonHeading.InnerHtml = ctppModel.HelpButtonHeading;
                            //<a href="#" class="b-emergency">Get help!</a>
                            this.HelpButtonLinkArea.InnerHtml = string.Format("<a href=\"{0}\" class=\"b-emergency\">{1}</a>", GetHelpButtonLinkAddress(), ctppModel.HelpButtonActual);
                            break;
                        case HelpAndReportButtonDisplaySettingEnum.InVisible:
                            this.DivHelpButtonArea.Visible = false;
                            break;
                        case HelpAndReportButtonDisplaySettingEnum.UnAvailable:
                            this.DivHelpButtonArea.Visible = true;
                            this.DivHelpButtonArea.Attributes["class"] = "container-checkin-untaken";
                            this.HelpButtonHeading.InnerHtml = ctppModel.HelpButtonHeading;
                            //<a class="b-checkin">Unavailable</a>
                            this.HelpButtonLinkArea.InnerHtml = string.Format("<a class=\"b-checkin\">{0}</a>", ctppModel.HelpButtonActual);
                            break;
                        default://  InVisible for default
                            this.DivHelpButtonArea.Visible = false;
                            break;
                    }
                }
                else
                {
                    switch (displayStatus)
                    {
                        case HelpAndReportButtonDisplaySettingEnum.Actual:
                            this.DivHelpButtonArea.Visible = true;
                            this.DivHelpButtonArea.Attributes["class"] = "container-emergency-actual";
                            this.HelpButtonHeading.InnerHtml = Model.SpeSHelpButtonHeading;
                            //<a href="#" class="b-emergency">Get help!</a>
                            this.HelpButtonLinkArea.InnerHtml = string.Format("<a href=\"{0}\" class=\"b-emergency\">{1}</a>", GetHelpButtonLinkAddress(), Model.SpeSHelpButtonActual);
                            break;
                        case HelpAndReportButtonDisplaySettingEnum.InVisible:
                            this.DivHelpButtonArea.Visible = false;
                            break;
                        case HelpAndReportButtonDisplaySettingEnum.UnAvailable:
                            this.DivHelpButtonArea.Visible = true;
                            this.DivHelpButtonArea.Attributes["class"] = "container-checkin-untaken";
                            this.HelpButtonHeading.InnerHtml = Model.SpeSHelpButtonHeading;
                            //<a class="b-checkin">Unavailable</a>
                            this.HelpButtonLinkArea.InnerHtml = string.Format("<a class=\"b-checkin\">{0}</a>", Model.SpeSHelpButtonActual);
                            break;
                        default://  InVisible for default
                            this.DivHelpButtonArea.Visible = false;
                            break;
                    }
                }
            }
            else
            {
                switch (displayStatus)
                {
                    case HelpAndReportButtonDisplaySettingEnum.Actual:
                        this.DivHelpButtonArea.Visible = true;
                        this.DivHelpButtonArea.Attributes["class"] = "container-emergency-actual";
                        this.HelpButtonHeading.InnerHtml = Model.SpeSHelpButtonHeading;
                        //<a href="#" class="b-emergency">Get help!</a>
                        this.HelpButtonLinkArea.InnerHtml = string.Format("<a href=\"{0}\" class=\"b-emergency\">{1}</a>", GetHelpButtonLinkAddress(), Model.SpeSHelpButtonActual);
                        break;
                    case HelpAndReportButtonDisplaySettingEnum.InVisible:
                        this.DivHelpButtonArea.Visible = false;
                        break;
                    case HelpAndReportButtonDisplaySettingEnum.UnAvailable:
                        this.DivHelpButtonArea.Visible = true;
                        this.DivHelpButtonArea.Attributes["class"] = "container-checkin-untaken";
                        this.HelpButtonHeading.InnerHtml = Model.SpeSHelpButtonHeading;
                        //<a class="b-checkin">Unavailable</a>
                        this.HelpButtonLinkArea.InnerHtml = string.Format("<a class=\"b-checkin\">{0}</a>", Model.SpeSHelpButtonActual);
                        break;
                    default://  InVisible for default
                        this.DivHelpButtonArea.Visible = false;
                        break;
                }
            }
        }

        private void SetDisplayStatusForReportRelapse()
        {
            HelpAndReportButtonDisplaySettingEnum displayStatus = HelpAndReportButtonDisplaySettingEnum.UnAvailable;// Report button default UnAvailable class.    //Not the same as help button
            if (HttpContext.Current.Session["CurrentAccount"] != null && ContextService.CurrentAccount != null)//IsAuthenticated
            {
                if (ContextService.CurrentAccount.UserType == UserTypeEnum.User)
                {
                    if (!string.IsNullOrEmpty(ReportRelapsePageSequenceGuid) && ReportRelapsePageSequenceGuid != Guid.Empty.ToString())
                    {
                        string status = string.Empty;
                        if (BindProgramGuid != Guid.Empty && BindUserGuid != Guid.Empty)
                        {
                            status = Resolve<IProgramUserService>().GetProgramUserStatus(BindProgramGuid, BindUserGuid);
                        }
                        if (status == ProgramUserStatusEnum.Active.ToString() || status == ProgramUserStatusEnum.Completed.ToString())
                        {
                            DateTime setCurrentTimeByTimeZone = DateTime.UtcNow;
                            setCurrentTimeByTimeZone = Resolve<IProgramUserService>().GetCurrentTimeByTimeZone(BindProgramGuid, BindUserGuid, setCurrentTimeByTimeZone);
                            //if (ReportRelapseAvailableTime <= DateTime.UtcNow.Hour)
                            if (ReportRelapseAvailableTime <= setCurrentTimeByTimeZone.Hour)
                            {
                                //If IsNeedReport now is true ,it is available
                                if (Resolve<ISessionService>().GetIsNeedReport(BindProgramGuid, BindUserGuid, setCurrentTimeByTimeZone))
                                {
                                    if (ReportRelapseLastFinishTime == null || (ReportRelapseLastFinishTime != null && (((DateTime)ReportRelapseLastFinishTime).Date < setCurrentTimeByTimeZone.Date)))
                                    {
                                        displayStatus = HelpAndReportButtonDisplaySettingEnum.Actual;
                                    }
                                    else
                                    {
                                        displayStatus = HelpAndReportButtonDisplaySettingEnum.Complete;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        displayStatus = HelpAndReportButtonDisplaySettingEnum.InVisible;
                    }
                }
            }
            else
            {
                displayStatus = HelpAndReportButtonDisplaySettingEnum.InVisible;
            }

            if (BindProgramGuid != Guid.Empty)
            {
                CTPPModel ctppModel = Resolve<ICTPPService>().GetCTPP(BindProgramGuid);
                if (ctppModel != null && ctppModel.IsEnableSpecificReportAndHelpButtons == true)
                {
                    switch (displayStatus)
                    {
                        case HelpAndReportButtonDisplaySettingEnum.InVisible:
                            this.DivReportButtonArea.Visible = false;
                            break;
                        case HelpAndReportButtonDisplaySettingEnum.Actual:
                            this.DivReportButtonArea.Visible = true;
                            this.DivReportButtonArea.Attributes["class"] = "container-checkin-actual";
                            this.ReportButtonHeading.InnerHtml = ctppModel.ReportButtonHeading;
                            //<a href="#" class="b-checkin" >Report here!</a>
                            this.ReportButtonLinkArea.InnerHtml = string.Format("<a href=\"{0}\" class=\"b-checkin\" >{1}</a>", GetReportButtonLinkAddress(), ctppModel.ReportButtonActual);
                            break;
                        case HelpAndReportButtonDisplaySettingEnum.Complete:
                            this.DivReportButtonArea.Visible = true;
                            this.DivReportButtonArea.Attributes["class"] = "container-checkin-complete";
                            this.ReportButtonHeading.InnerHtml = ctppModel.ReportButtonHeading;
                            //<div class="day-label-completed">Completed</div>
                            this.ReportButtonLinkArea.InnerHtml = string.Format("<div class=\"day-label-completed\">{0}</div>", ctppModel.ReportButtonComplete);
                            break;
                        case HelpAndReportButtonDisplaySettingEnum.UnAvailable:
                            this.DivReportButtonArea.Visible = true;
                            this.DivReportButtonArea.Attributes["class"] = "container-checkin-untaken";
                            this.ReportButtonHeading.InnerHtml = ctppModel.ReportButtonHeading;
                            //<a class="b-checkin">Unavailable</a>
                            this.ReportButtonLinkArea.InnerHtml = string.Format("<a class=\"b-checkin\">{0}</a>", ctppModel.ReportButtonUntaken);
                            break;
                        default:
                            this.DivReportButtonArea.Visible = false;
                            break;
                    }
                }
                else
                {
                    switch (displayStatus)
                    {
                        case HelpAndReportButtonDisplaySettingEnum.InVisible:
                            this.DivReportButtonArea.Visible = false;
                            break;
                        case HelpAndReportButtonDisplaySettingEnum.Actual:
                            this.DivReportButtonArea.Visible = true;
                            this.DivReportButtonArea.Attributes["class"] = "container-checkin-actual";
                            this.ReportButtonHeading.InnerHtml = Model.SpeSReportButtonHeading;
                            //<a href="#" class="b-checkin" >Report here!</a>
                            this.ReportButtonLinkArea.InnerHtml = string.Format("<a href=\"{0}\" class=\"b-checkin\" >{1}</a>", GetReportButtonLinkAddress(), Model.SpeSReportButtonActual);
                            break;
                        case HelpAndReportButtonDisplaySettingEnum.Complete:
                            this.DivReportButtonArea.Visible = true;
                            this.DivReportButtonArea.Attributes["class"] = "container-checkin-complete";
                            this.ReportButtonHeading.InnerHtml = Model.SpeSReportButtonHeading;
                            //<div class="day-label-completed">Completed</div>
                            this.ReportButtonLinkArea.InnerHtml = string.Format("<div class=\"day-label-completed\">{0}</div>", Model.SpeSReportButtonComplete);
                            break;
                        case HelpAndReportButtonDisplaySettingEnum.UnAvailable:
                            this.DivReportButtonArea.Visible = true;
                            this.DivReportButtonArea.Attributes["class"] = "container-checkin-untaken";
                            this.ReportButtonHeading.InnerHtml = Model.SpeSReportButtonHeading;
                            //<a class="b-checkin">Unavailable</a>
                            this.ReportButtonLinkArea.InnerHtml = string.Format("<a class=\"b-checkin\">{0}</a>", Model.SpeSReportButtonUntaken);
                            break;
                        default:
                            this.DivReportButtonArea.Visible = false;
                            break;
                    }
                }
            }
            else
            {
                switch (displayStatus)
                {
                    case HelpAndReportButtonDisplaySettingEnum.InVisible:
                        this.DivReportButtonArea.Visible = false;
                        break;
                    case HelpAndReportButtonDisplaySettingEnum.Actual:
                        this.DivReportButtonArea.Visible = true;
                        this.DivReportButtonArea.Attributes["class"] = "container-checkin-actual";
                        this.ReportButtonHeading.InnerHtml = Model.SpeSReportButtonHeading;
                        //<a href="#" class="b-checkin" >Report here!</a>
                        this.ReportButtonLinkArea.InnerHtml = string.Format("<a href=\"{0}\" class=\"b-checkin\" >{1}</a>", GetReportButtonLinkAddress(), Model.SpeSReportButtonActual);
                        break;
                    case HelpAndReportButtonDisplaySettingEnum.Complete:
                        this.DivReportButtonArea.Visible = true;
                        this.DivReportButtonArea.Attributes["class"] = "container-checkin-complete";
                        this.ReportButtonHeading.InnerHtml = Model.SpeSReportButtonHeading;
                        //<div class="day-label-completed">Completed</div>
                        this.ReportButtonLinkArea.InnerHtml = string.Format("<div class=\"day-label-completed\">{0}</div>", Model.SpeSReportButtonComplete);
                        break;
                    case HelpAndReportButtonDisplaySettingEnum.UnAvailable:
                        this.DivReportButtonArea.Visible = true;
                        this.DivReportButtonArea.Attributes["class"] = "container-checkin-untaken";
                        this.ReportButtonHeading.InnerHtml = Model.SpeSReportButtonHeading;
                        //<a class="b-checkin">Unavailable</a>
                        this.ReportButtonLinkArea.InnerHtml = string.Format("<a class=\"b-checkin\">{0}</a>", Model.SpeSReportButtonUntaken);
                        break;
                    default:
                        this.DivReportButtonArea.Visible = false;
                        break;
                }
            }
        }

        /// <summary>
        /// set the programwindow-right div area. Include the Facts, Help button and Report button
        /// </summary>
        /// <param name="thisCTPPModel"></param>
        private void SetHelpAndReportButtonArea(CTPPModel thisCTPPModel)
        {
            SetDisplayStatusForReportRelapse();
            SetDisplayStatusForHelpRelapse();
        }
    }
}