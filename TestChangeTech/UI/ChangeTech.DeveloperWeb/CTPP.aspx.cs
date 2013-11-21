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


//Error: AjaxControlToolkit requires ASP.NET Ajax 4.0 scripts. 
//Ensure the correct version of the scripts are referenced. 
//If you are using an ASP.NET ScriptManager, switch to the AjaxScriptManager in System.Web.Ajax.dll,
//or use the ToolkitScriptManager in AjaxControlToolkit.dll.

namespace ChangeTech.DeveloperWeb
{
    public partial class CTPP : PageBase<CTPPEndUserPageModel>
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
        private int RetakeEnable
        {
            get
            {
                if (ViewState["ViewStateRetakeEnable"] != null)
                {
                    return int.Parse(ViewState["ViewStateRetakeEnable"].ToString());
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                ViewState["ViewStateRetakeEnable"] = value;
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
        #region the list for CTPP
        private List<CTPPSessionPageBodyModel> CtppSessionPageBodyList = new List<CTPPSessionPageBodyModel>();
        private List<CTPPSessionPageMediaResourceModel> CtppSessionPageMediaResourceList = new List<CTPPSessionPageMediaResourceModel>();
        private string serverResourcePath;
        #endregion
        #endregion

        #region public variables
        public string Logout = string.Empty;
        public string URLofVideoBox
        {
            get
            {
                if (ViewState["URLofVideoBox"] != null)
                {
                    return ViewState["URLofVideoBox"].ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                ViewState["URLofVideoBox"] = value;
            }
        }
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

        #endregion

        #region const variable
        const string TEMPUSERNAME = "TempUser";
        const int SESSIONNOTDONE = 0;//Not done
        const int SESSIONDONE = 1;//Done
        const int SESSIONSTART = 2;//Start.//Assert: only 1 row can be into this.
        const string LoginTitle = "Log in";
        const string LoginOutTitle = "LOGOUT";
        const string UserName = "UserName";
        const string Password = "Password";
        const string Mobile = "Mobile";
        const string ForgottenPassword = "Forgotten password";
        const string RetrievePassword = "Retrieve password";
        const string PasswordHasSent = "PasswordHasSent";
        const string specialStringNameByLogin = "Login";
        const string specialStringNameByRetrievePassword = "Retrieve_password";
        const string specialStringNameByForgottenPassword = "Forgotten_password?";

        #endregion

        #region Page Load

        protected void Page_Load(object sender, EventArgs e)
        {
            if (ServiceUtility.IsMobileDevice)
            {
                Response.Redirect(Request.Url.AbsoluteUri.Replace(Constants.URL_DEVELOPER_CTPP, Constants.URL_DEVELOPER_CTPPS), true);
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
                else
                {
                    BindOtherCTPP();
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
                        this.imgCont.InnerHtml = string.Format("<img id=\"over\" src=\"{0}\" style=\"display:none;padding-left:10px;\"/><img id=\"under\" src=\"../Images/bg-programwindow.png\" style=\"display:none;padding-left:10px;\"/><canvas id=\"out\"></canvas>", ImageUrl);
                    }
                    else//payment fail
                    {
                        this.h1Header.InnerHtml = (titleSpecialString == null || titleSpecialString.Value == null) ? string.Empty : titleSpecialString.Value.Replace("\r\n", "<br/>").Replace("\r", "<br/>").Replace("\n", "<br/>");
                        this.pDescription.InnerHtml = paymodel.ExceptionTip == null ? string.Empty : paymodel.ExceptionTip.ToString().Replace("\r\n", "<br/>").Replace("\r", "<br/>").Replace("\n", "<br/>").Replace("<br/><br/>", "<span class=\"emptyline\" style=\"display:block;\">&nbsp;</span>");
                        string accountName = Resolve<ISystemSettingService>().GetSettingValue("AzureStorageAccountName");
                        string bolbPath = ServiceUtility.GetBlobPath(accountName);
                        string ImageUrl = "";
                        if (thisCTPPModel.ProgramPresenter != null && thisCTPPModel.ProgramPresenter.NameOnServer.Any())
                        {
                            //ImageUrl = bolbPath + BlobContainerType.OriginalImageContainer.ToString().ToLower() + "/" + lastpagecontentmodel.PresenterImage.NameOnServer;
                            ImageUrl = "../RequestResource.aspx?target=Image&media=" + thisCTPPModel.ProgramPresenter.NameOnServer;
                        }
                        this.imgCont.InnerHtml = string.Format("<img id=\"over\" src=\"{0}\" style=\"display:none;padding-left:10px;\"/><img id=\"under\" src=\"../Images/bg-programwindow.png\" style=\"display:none;padding-left:10px;\"/><canvas id=\"out\"></canvas>", ImageUrl);
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
                    //UserModel user = Resolve<IUserService>().GetUserByUserName(Context.User.Identity.Name);
                    //if(user!=null)
                    //{
                    //    string returnMessage = Resolve<IUserService>().EndUserLoginForCTPP(user.UserName, user.PassWord, thisCTPPModel.ProgramGUID.ToString().Trim());
                    //    string[] logedStr = returnMessage.Split(';');
                    //    if (logedStr[0] == "0")
                    //    {
                    //        if (!string.IsNullOrEmpty(Context.User.Identity.Name))
                    //        {
                    //            ClearCurrentUser();//Clear Current UserInfo.
                    //        }
                    //    }
                    //}
                    CTPPNotFromFlashOrPayment(thisCTPPModel);
                }
            }

            if (thisCTPPModel != null)
            {
                RetakeEnable = thisCTPPModel.RetakeEnable;
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
            int removeAtIndex = url.IndexOf("?", 13);
            if (removeAtIndex > 0)
            {
                if (url.Substring(removeAtIndex + 1, 5).ToLower() == "brand")
                {
                    url = url.Remove(removeAtIndex);
                }
            }
            if (!string.IsNullOrEmpty(InputBrand) && !string.IsNullOrEmpty(InputProgram))
            {
                Response.Redirect("/" + InputBrand + "/" + InputProgram, false);
            }
            else
            {
                Response.Redirect(url, false);
            }
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
            this.h1Header.InnerHtml = (sessionEndingPage == null || sessionEndingPage.Heading == null) ? string.Empty : sessionEndingPage.Heading.ToString().Replace("\r\n", "<br/>").Replace("\r", "<br/>").Replace("\n", "<br/>");
            this.pDescription.InnerHtml = (sessionEndingPage == null || sessionEndingPage.Text == null) ? string.Empty : sessionEndingPage.Text.Replace("\r\n", "<br/>").Replace("\r", "<br/>").Replace("\n", "<br/>").Replace("<br/><br/>", "<span class=\"emptyline\" style=\"display:block;\">&nbsp;</span>");
            string accountName = Resolve<ISystemSettingService>().GetSettingValue("AzureStorageAccountName");
            string bolbPath = ServiceUtility.GetBlobPath(accountName);
            string ImageUrl = "";
            if (model.ProgramPresenter != null && model.ProgramPresenter.NameOnServer.Any())
            {
                ImageUrl = "../RequestResource.aspx?target=Image&media=" + model.ProgramPresenter.NameOnServer;
            }
            this.imgCont.InnerHtml = string.Format("<img id=\"over\" src=\"{0}\" style=\"display:none;padding-left:10px;\"/><img id=\"under\" src=\"../Images/bg-programwindow.png\" style=\"display:none;padding-left:10px;\"/><canvas id=\"out\"></canvas>", ImageUrl);
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

        }
        private void CTPPNotFromFlashOrPayment(CTPPModel model)
        {
            this.h1Header.InnerHtml = string.IsNullOrEmpty(model.ProgramDescriptionTitle) ? string.Empty : model.ProgramDescriptionTitle.ToString().Replace("\r\n", "<br/>").Replace("\r", "<br/>").Replace("\n", "<br/>");
            this.pDescription.InnerHtml = string.IsNullOrEmpty(model.ProgramDescription) ? string.Empty : model.ProgramDescription.ToString().Replace("\r\n", "<br/>").Replace("\r", "<br/>").Replace("\n", "<br/>").Replace("<br/><br/>", "<span class=\"emptyline\" style=\"display:block;\">&nbsp;</span>");
            string accountName = Resolve<ISystemSettingService>().GetSettingValue("AzureStorageAccountName");
            string bolbPath = ServiceUtility.GetBlobPath(accountName);
            string ImageUrl = "";
            if (model.ProgramPresenter != null && model.ProgramPresenter.NameOnServer.Any())
            {
                //ImageUrl = bolbPath + BlobContainerType.OriginalImageContainer.ToString().ToLower() + "/" + thisCTPPModel.ProgramPresenter.NameOnServer;
                ImageUrl = "../RequestResource.aspx?target=Image&media=" + model.ProgramPresenter.NameOnServer;
            }
            this.imgCont.InnerHtml = string.Format("<img id=\"over\" src=\"{0}\" style=\"display:none;padding-left:10px;\"/><img id=\"under\" src=\"../Images/bg-programwindow.png\" style=\"display:none;padding-left:10px;\"/><canvas id=\"out\"></canvas>", ImageUrl);
        }
        private void BindShareIcon(CTPPModel thisCTPPModel)
        {
            if (thisCTPPModel.IsFacebook == 0)
            {
                //this.fackbookLnk.Visible = false;
                this.fblikeDiv.Visible = false;
            }
            else
            {
                //get clean url to avoid that the url containing user info is sent to facebook
                string absoluteUri = Request.Url.AbsoluteUri.ToLower();//http://localhost:41265/CTPP.aspx?brand=tb&program=puu
                string host = absoluteUri.Substring(0, absoluteUri.ToLower().IndexOf("/" + Constants.URL_DEVELOPER_CTPP.ToLower()));
                string navigateUrl = host + "/" + InputBrand + "/" + InputProgram;

                this.fblikeDiv.Visible = true;
                this.fblikeDiv.Attributes.Add("data-href", navigateUrl);
            }

        }
        private void BindPromotionField(CTPPModel thisCTPPModel)
        {
            if (thisCTPPModel.PromotionField1 != null)
            {
                if (!string.IsNullOrEmpty(thisCTPPModel.PromotionLink1))
                {
                    //this.imagebtnAD1.Enabled = true;
                    this.hlPromotion1.Enabled = true;
                    this.hlPromotion1.NavigateUrl = thisCTPPModel.PromotionLink1;
                }
                else
                {
                    //this.imagebtnAD1.Enabled = false;
                    this.hlPromotion1.Enabled = false;
                }
                string accountName = Resolve<ISystemSettingService>().GetSettingValue("AzureStorageAccountName");
                string bolbPath = ServiceUtility.GetBlobPath(accountName);
                string imageUrl = bolbPath + BlobContainerTypeEnum.LogoContainer.ToString().ToLower() + "/" + thisCTPPModel.PromotionField1.NameOnServer;
                this.hlPromotion1.Style.Add("background", string.Format("url({0}) top left no-repeat #f0f6fc;", imageUrl));
            }
            else
            {
                this.hlPromotion1.Enabled = false;
                //this.imagebtnAD1.Enabled = false;
            }

            if (thisCTPPModel.PromotionField2 != null)
            {
                if (!string.IsNullOrEmpty(thisCTPPModel.PromotionLink2))
                {
                    this.hlPromotion2.Enabled = true;
                    this.hlPromotion2.NavigateUrl = thisCTPPModel.PromotionLink2;
                    //this.imagebtnAD2.Enabled = true;
                }
                else
                {
                    this.hlPromotion2.Enabled = false;
                    //this.imagebtnAD2.Enabled = false;
                }
                string accountName = Resolve<ISystemSettingService>().GetSettingValue("AzureStorageAccountName");
                string bolbPath = ServiceUtility.GetBlobPath(accountName);
                string imageUrl = bolbPath + BlobContainerTypeEnum.LogoContainer.ToString().ToLower() + "/" + thisCTPPModel.PromotionField2.NameOnServer;
                this.hlPromotion2.Style.Add("background", string.Format("url({0}) top left no-repeat #f0f6fc;", imageUrl));
            }
            else
            {
                this.hlPromotion2.Enabled = false;
                //this.imagebtnAD2.Enabled = false;
            }

        }
        private void BindCTPPColor(CTPPModel thisCTPPModel)
        {
            HttpBrowserCapabilities bc = Request.Browser;

            //((System.Web.UI.HtmlControls.HtmlGenericControl)e.Item.FindControl("dayTitle")).Attributes["class"] = "acc_trigger_grey day-title";
            System.Web.UI.HtmlControls.HtmlGenericControl thisDivControl = new System.Web.UI.HtmlControls.HtmlGenericControl();
            thisDivControl = (System.Web.UI.HtmlControls.HtmlGenericControl)this.FindControl("programribbon_program01");
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

        private void EnablePriceButton()
        {
            this.priceInfo.Visible = true;
            //this.lblBuySub.Visible = true;
            this.BuySubLink.Visible = true;
            this.BuySubLink2.Visible = true;
            this.priceInfo2.Visible = true;

        }

        private void UnenablePriceButton()
        {
            this.priceInfo.Visible = false;
            //this.lblBuySub.Visible = false;
            this.BuySubLink.Visible = false;
            this.BuySubLink2.Visible = false;
            this.priceInfo2.Visible = false;
        }

        private void BindIsInactive(CTPPModel thisCTPPModel)
        {
            if (thisCTPPModel.InActive == 1)//inactive  no login ,no order
            {
                CTPPInactive = 1;
                this.LoginView.Visible = false;
                this.priceInfo.Visible = false;
                //this.lblBuySub.Visible = false;
                this.BuySubLink.Visible = false;
                this.BuySubLink2.Visible = false;
                this.priceInfo2.Visible = false;
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
                    if (!thisuser.IsPaid && thisCTPPModel.NeedPay)
                    {
                        EnablePriceButton();
                        int priceNum = string.IsNullOrEmpty(thisCTPPModel.Price) ? 0 : int.Parse(thisCTPPModel.Price);
                        string price = Math.Round(priceNum / 100.0, 2).ToString() + ",-";
                        if (!string.IsNullOrEmpty(thisCTPPModel.Price))
                        {
                            this.lblPrice.Text = price;
                            this.lblPrice2.Text = price;
                        }
                    }
                    else
                    {
                        UnenablePriceButton();
                    }
                    bindSessionListAndSpeStr(thisCTPPModel.ProgramGUID, thisuser.UserGuid, thisCTPPModel);
                }
                else
                {
                    EnablePriceButton();
                    if (thisCTPPModel.NeedPay)
                    {
                        int priceNum = string.IsNullOrEmpty(thisCTPPModel.Price) ? 0 : int.Parse(thisCTPPModel.Price);
                        string price = Math.Round(priceNum / 100.0, 2).ToString() + ",-";
                        if (!string.IsNullOrEmpty(thisCTPPModel.Price))
                        {
                            this.lblPrice.Text = price;
                            this.lblPrice2.Text = price;
                        }
                    }
                    bindSessionListAndSpeStr(thisCTPPModel.ProgramGUID, Guid.Empty, thisCTPPModel);
                }
            }
        }

        private void BindLoginPanelSpecialString(CTPPModel thisCTPPModel)
        {
            SpecialStringModel specialStringModelByLogin = Resolve<ISpecialStringService>().GetSpecialStringBy(specialStringNameByLogin, thisCTPPModel.LanguageGUID);
            SpecialStringModel specialStringModelByUserName = Resolve<ISpecialStringService>().GetSpecialStringBy(UserName, thisCTPPModel.LanguageGUID);
            SpecialStringModel specialStringModelByPassword = Resolve<ISpecialStringService>().GetSpecialStringBy(Password, thisCTPPModel.LanguageGUID);
            SpecialStringModel specialStringModelByMobile = Resolve<ISpecialStringService>().GetSpecialStringBy(Mobile, thisCTPPModel.LanguageGUID);
            SpecialStringModel specialStringModelByRetrievePwd = Resolve<ISpecialStringService>().GetSpecialStringBy(specialStringNameByRetrievePassword, thisCTPPModel.LanguageGUID);
            SpecialStringModel specialStringModelByForgotPwd = Resolve<ISpecialStringService>().GetSpecialStringBy(specialStringNameByForgottenPassword, thisCTPPModel.LanguageGUID);

            if (specialStringModelByLogin != null)
            {
                string specialStringByLoginValue = specialStringModelByLogin.Value;
                this.lblLoginTitle.Text = !string.IsNullOrEmpty(specialStringByLoginValue) ? specialStringByLoginValue : LoginTitle;
                this.btnLogin.Text = !string.IsNullOrEmpty(specialStringByLoginValue) ? specialStringByLoginValue : LoginTitle;
            }
            if (specialStringModelByUserName != null)
            {
                string specialStringByUserNameValue = specialStringModelByUserName.Value;
                this.lblUserName.Text = !string.IsNullOrEmpty(specialStringByUserNameValue) ? specialStringByUserNameValue : UserName;
                this.lblForgotPwdUserName.Text = !string.IsNullOrEmpty(specialStringByUserNameValue) ? specialStringByUserNameValue : UserName;
            }
            if (specialStringModelByPassword != null)
            {
                string specialStringByPasswordValue = specialStringModelByPassword.Value;
                this.lblPassword.Text = !string.IsNullOrEmpty(specialStringByPasswordValue) ? specialStringByPasswordValue : Password;
                this.lblForgotPwd.Text = !string.IsNullOrEmpty(specialStringByPasswordValue) ? specialStringByPasswordValue : Password;
            }
            if (specialStringModelByMobile != null)
            {
               //this.lblMobilePhone.Text = !string.IsNullOrEmpty(specialStringModelByMobile.Value) ? specialStringModelByMobile.Value : Mobile;
            }
            if (specialStringModelByRetrievePwd != null)
            {
                this.btnForgotPassword.Text = !string.IsNullOrEmpty(specialStringModelByRetrievePwd.Value) ? specialStringModelByRetrievePwd.Value : RetrievePassword;
            }
            if (specialStringModelByForgotPwd != null)
            {
                string specialStringByForgotPwdValue = specialStringModelByForgotPwd.Value;
                this.forgotPwdLinkA.InnerText = !string.IsNullOrEmpty(specialStringByForgotPwdValue) ? specialStringByForgotPwdValue : ForgottenPassword;
                this.lblForgotPwd.Text = !string.IsNullOrEmpty(specialStringByForgotPwdValue) ? specialStringByForgotPwdValue : ForgottenPassword;
            }
        }

        private void BindCTPPModel(CTPPModel thisCTPPModel)
        {
            BindLoginPanelSpecialString(thisCTPPModel);
            this.lblProgramName.Text = thisCTPPModel.ProgramName;
            var newBrandModel = Resolve<IBrandService>().GetBrandByGUID(thisCTPPModel.BrandGUID);
            if (newBrandModel.BrandLogo != null)
            {
                string accountName = Resolve<ISystemSettingService>().GetSettingValue("AzureStorageAccountName");
                string bolbPath = ServiceUtility.GetBlobPath(accountName);
                this.imgProgramLogo.Src = bolbPath + BlobContainerTypeEnum.LogoContainer.ToString().ToLower() + "/" + newBrandModel.BrandLogo.NameOnServer;
                this.imgBrandLogo.Src = bolbPath + BlobContainerTypeEnum.LogoContainer.ToString().ToLower() + "/" + newBrandModel.BrandLogo.NameOnServer;
            }
            this.h1ProgramName.InnerText = thisCTPPModel.ProgramName;

            BindShareIcon(thisCTPPModel);

            this.lblBrandName.Text = newBrandModel.BrandName;
            this.lblBrandNameBottom.Text = newBrandModel.BrandName;
            this.lblBrandNameBottom2.Text = newBrandModel.BrandName;
            this.programSubheadingInCTPPlbl.Text = thisCTPPModel.ProgramSubheading;
            if (!string.IsNullOrEmpty(thisCTPPModel.ForSideLink))
            {
                this.ForSideLink.Visible = true;
                this.ForSideLink.NavigateUrl = thisCTPPModel.ForSideLink;
            }
            else
            {
                this.ForSideLink.Visible = false;
            }

            if (!string.IsNullOrEmpty(thisCTPPModel.FacebookLink))
            {
                this.facebookBottomLink.Visible = true;
                this.facebookBottomLink.NavigateUrl = thisCTPPModel.FacebookLink;

            }
            else
            {
                this.facebookBottomLink.Visible = false;
            }

            BindPromotionField(thisCTPPModel);

            if (!string.IsNullOrEmpty(thisCTPPModel.BackDarkColor) && !string.IsNullOrEmpty(thisCTPPModel.BackLightColor))
            {
                BindCTPPColor(thisCTPPModel);
            }
            if (!string.IsNullOrEmpty(thisCTPPModel.ProgramSubheadColor))
            {
                ProgramSubColor = thisCTPPModel.ProgramSubheadColor;
                this.programSubheadingInCTPPlbl.ForeColor = System.Drawing.Color.FromName(thisCTPPModel.ProgramSubheadColor);
                ((System.Web.UI.HtmlControls.HtmlGenericControl)this.FindControl("programtitle")).Style.Add("border-left", string.Format("2px dotted {0}", thisCTPPModel.ProgramSubheadColor));
                this.ForSideLink.ForeColor = System.Drawing.Color.FromName(thisCTPPModel.ProgramSubheadColor);
                //this.usermenu.Style.Add("color", thisCTPPModel.ProgramSubheadColor);
            }
            BindOtherCTPP();
            BindIsInactive(thisCTPPModel);
            SetProgramwindowRightArea(thisCTPPModel);
        }

        private void BindOtherCTPP()
        {
            CTPPModel thisCTPPModel = null;
            Guid requestProgramGuid = Guid.Empty;
            if (!string.IsNullOrEmpty(CTPPEncriptString))//?CTPP=......
            {
                //01; ProgramGUID；UserGUID；PageGUID
                string[] CTPPInfo = StringUtility.MD5Decrypt(CTPPEncriptString, Constants.MD5_KEY).Split(';');
                requestProgramGuid = new Guid(CTPPInfo[1]);//used for get ctpp brand and program
                thisCTPPModel = Resolve<ICTPPService>().GetCTPP(requestProgramGuid);
            }
            else
            {
                thisCTPPModel = Resolve<ICTPPService>().GetCTPPByBrandAndProgram(InputBrand, InputProgram);
            }

            SpecialStringModel specialStringModelByLogout = Resolve<ISpecialStringService>().GetSpecialStringBy("Logout", thisCTPPModel.LanguageGUID);
            LinkButton LogoutLink = ((LinkButton)LoginView.FindControl("logoutLnkBtn"));
            if (specialStringModelByLogout != null && LogoutLink != null)
            {
                LogoutLink.Text = !string.IsNullOrEmpty(specialStringModelByLogout.Value) ? specialStringModelByLogout.Value : LoginOutTitle;
            }

            if (thisCTPPModel.IsNotShowOtherPrograms == true)
            {
                this.listOtherCTPP.Visible = false;
                this.lblBrandName.Visible = false;
                this.listOtherCTPP.DataSource = null;
                this.listOtherCTPP.DataBind();
            }
            else
            {
                var otherCTPPMs = Resolve<ICTPPService>().GetCTPPInBrandNotThisProgram(InputBrand, InputProgram);
                if (otherCTPPMs.Any())
                {
                    this.listOtherCTPP.Visible = true;
                    this.listOtherCTPP.DataSource = otherCTPPMs;

                    this.listOtherCTPP.DataBind();
                }
                else
                {
                    this.listOtherCTPP.Visible = false;
                    this.lblBrandName.Visible = false;
                }
            }
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


            Model = Resolve<ISessionService>().GetCTPPEndUserPageModel(programGuid, userGuid, CurrentPageNumber, 1000, CTPPVersionEnum.PCVersion);//1000=PageSize
            lblDaysinprogram.Text = Model.SpeSDays_in_program;
            lblClickaday.Text = Model.SpeSClick_a_day;
            orderURL = Model.orderUrl;
            //Is StartButton Invisible
            ProgramPropertyModel ppModel = Resolve<IProgramService>().GetProgramProperty(programGuid);
            if (ppModel.IsInvisibleStartButton == true || thisCTPPModel.IsNotShowStartButton == true)
            {
                BuyLink.Visible = false;
                BuyLink2.Visible = false;
                this.BuyLink.NavigateUrl = string.Empty;
                this.BuyLink2.NavigateUrl = string.Empty;
            }
            else
            {
                BuyLink.Visible = true;
                BuyLink2.Visible = true;
                this.BuyLink.NavigateUrl = orderURL;
                this.BuyLink2.NavigateUrl = orderURL;

            }
            this.lblBuy.Text = Model.SpeSBuy;
            this.lblBuy2.Text = Model.SpeSBuy;
            if (this.lblPrice.Text == string.Empty && this.priceInfo.Visible == true)
            {
                this.lblPrice.Text = Model.SpeSFree;
                this.lblPrice2.Text = Model.SpeSFree;
            }

            if (!string.IsNullOrEmpty(Model.SpeSBuy_subtext) && !string.IsNullOrEmpty(thisCTPPModel.SubPriceLink))
            {
                this.BuySubLink.Text = Model.SpeSBuy_subtext;
                this.BuySubLink.NavigateUrl = thisCTPPModel.SubPriceLink;
                this.BuySubLink2.Text = Model.SpeSBuy_subtext;
                this.BuySubLink2.NavigateUrl = thisCTPPModel.SubPriceLink;
            }
            if (this.lblBrandName.Text.IndexOf(Model.SpeSOther_programs_from) < 0)
            {
                this.lblBrandName.Text = Model.SpeSOther_programs_from + " " + this.lblBrandName.Text;
            }
            if (this.videoArea.Visible)
            {
                this.VideoSubH.InnerText = Model.SpeSVideoSubtext1;
                this.VideoSubP.InnerText = Model.SpeSVideoSubtext2;
            }
            this.lblProvidedBy.Text = string.IsNullOrEmpty(Model.SpeSProvided_by) ? "is a service provided by" : Model.SpeSProvided_by;
            this.lblUseFrom.Text = string.IsNullOrEmpty(Model.SpeSUseFrom) ? "using EasyChange Technology from" : Model.SpeSUseFrom;
            this.lblAllRightsReserved.Text = string.IsNullOrEmpty(Model.SpeSAllRightsReserved) ? "All rights reserved." : Model.SpeSAllRightsReserved;

            //the services provider for ctpp
            if (CtppSessionPageBodyList.Count > 0)
            {
                CtppSessionPageBodyList.Clear();
            }
            if (CtppSessionPageMediaResourceList.Count > 0)
            {
                CtppSessionPageMediaResourceList.Clear();
            }
            IProgramService programService = Resolve<IProgramService>();
            CtppSessionPageBodyList = programService.GetAllPageBodyList(programGuid);
            CtppSessionPageMediaResourceList = programService.GetAllPageMediaResourceList(programGuid);
            serverResourcePath = Resolve<ISystemSettingService>().GetSettingValue("WebServerPath");
            this.rpSessions.DataSource = Model.endUserSessionModel;
            this.rpSessions.DataBind();
        }
        #endregion

        #region logout/login
        protected void btnLogout_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            ContextService.ClearAccount();
            string url = Request.Url.PathAndQuery;
            //has ctpp querystring
            if (url.ToLower().IndexOf((Constants.URL_DEVELOPER_CTPP.ToLower() + "?ctpp")) > -1 || (Request.QueryString["brand"] != null && Request.QueryString["program"] != null))
            {
                url = "/" + InputBrand + "/" + InputProgram;
            }
            //has PaymentTransationID querystring
            else if (url.ToLower().IndexOf((Constants.URL_DEVELOPER_CTPP.ToLower() + "?" + Constants.QUERYSTR_PAYMENT_TRANSATION_ID.ToLower())) > -1 || (Request.QueryString["brand"] != null && Request.QueryString["program"] != null))
            {
                url = "/" + InputBrand + "/" + InputProgram;
            }
            else
            {
                int removeAtIndex = url.IndexOf("?", 13);
                if (removeAtIndex > 0)
                {
                    if (url.Substring(removeAtIndex + 1, 5).ToLower() == "brand")
                    {
                        url = url.Remove(removeAtIndex);
                    }
                }
            }
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
                    returnMessage = Convert.ToInt32(ReturnMessageCodeEnum.ErrorCode).ToString() + ";Can't find this program.";//thisCTPPModel is null.
                }
                string[] logedStr = returnMessage.Split(';');
                if (logedStr[0] == "1")
                {
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

                    //HttpCookie cook = Request.Cookies[FormsAuthentication.FormsCookieName]; //FormsAuthentication.GetAuthCookie(FormsAuthentication.FormsCookieName, false);
                    //string username = authTicket.Name;

                    //UserModel thisuser = Resolve<IUserService>().GetUserByUserName(username, thisCTPPModel.ProgramGUID);
                    ViewStateusername = requestUserModel.UserName;
                    ViewStatepassword = requestUserModel.PassWord;
                    if (!requestUserModel.IsPaid && thisCTPPModel.NeedPay)
                    {
                        this.priceInfo.Visible = true;
                        this.priceInfo2.Visible = true;

                        //this.lblBuySub.Visible = true;
                        this.BuySubLink.Visible = true;
                        this.BuySubLink2.Visible = true;
                    }
                    else
                    {
                        this.priceInfo.Visible = false;
                        this.priceInfo2.Visible = false;
                        //this.lblBuySub.Visible = false;
                        this.BuySubLink.Visible = false;
                        this.BuySubLink2.Visible = false;
                    }
                    bindSessionListAndSpeStr(thisCTPPModel.ProgramGUID, requestUserModel.UserGuid, thisCTPPModel);

                    HelpRelapsePageSequenceGuid = thisCTPPModel.HelpButtonRelapsePageSequenceGuid.ToString();
                    ReportRelapsePageSequenceGuid = thisCTPPModel.ReportButtonRelapsePageSequenceGuid.ToString();

                    if (thisCTPPModel.ReportButtonAvailableTime != null)
                        ReportRelapseAvailableTime = (int)thisCTPPModel.ReportButtonAvailableTime;

                    SetProgramwindowRightArea(thisCTPPModel);
                }
                else
                {
                    SpecialStringModel specialStringModelByLogout = Resolve<ISpecialStringService>().GetSpecialStringBy(logedStr[1], thisCTPPModel.LanguageGUID);
                    this.LoginModelDia.Show();
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
                this.LoginModelDia.Show();
                lblWrongInfo.Visible = true;
                lblWrongInfo.Text = ex.ToString();
                return;
            }

        }
        #endregion

        #region ItemDataBound
        protected void rpSessions_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.DataItem != null)
            {
                List<string> downloadResourceS = new List<string>();
                if (CTPPInactive == 0)
                {
                    //downloadResourceS = Resolve<ICTPPService>().GetSessionResource(new Guid(((Button)e.Item.FindControl("hiddenSession")).Text));
                    downloadResourceS = Resolve<ICTPPService>().GetSessionResource(new Guid(((Button)e.Item.FindControl("hiddenSession")).Text), serverResourcePath, CtppSessionPageBodyList, CtppSessionPageMediaResourceList);

                }
                int isHasDoneStr = int.Parse(((Button)e.Item.FindControl("hiddenSessionIsHasDone")).Text.Trim());
                switch (isHasDoneStr)
                {
                    case SESSIONNOTDONE://not done
                        ((Label)e.Item.FindControl("lblCompleted")).Text = Model.SpeSUntaken;//"NOT YET TAKEN";  
                        ((Label)e.Item.FindControl("lblCompleted")).CssClass = "day-untaken-label-actual";
                        ((Button)e.Item.FindControl("btnOpenDay")).Text = Model.SpeSUnavailable;//"Not yet available";
                        ((Button)e.Item.FindControl("btnOpenDay")).CssClass = "openday-untaken";
                        ((Button)e.Item.FindControl("btnOpenDay")).Enabled = false;

                        ((System.Web.UI.HtmlControls.HtmlGenericControl)e.Item.FindControl("dayTitle")).Attributes["class"] = "acc_trigger_grey day-title";
                        ((System.Web.UI.HtmlControls.HtmlGenericControl)e.Item.FindControl("accContainer")).Attributes["class"] = "acc_container_grey";
                        for (int i = 0; i < downloadResourceS.Count; i++)
                        {
                            if (downloadResourceS[i] == null)
                                downloadResourceS[i] = string.Empty;
                            int hrefIndex = downloadResourceS[i].IndexOf("href=");//href second \'
                            int hrefStartIndex = hrefIndex + 6;
                            int hrefEndIndex = downloadResourceS[i].IndexOf('\'', hrefStartIndex);
                            string replaceStr = downloadResourceS[i].Substring(hrefStartIndex, hrefEndIndex - hrefStartIndex);
                            downloadResourceS[i] = downloadResourceS[i].Remove(hrefEndIndex + 1, 16);
                            downloadResourceS[i] = downloadResourceS[i].Replace(replaceStr, "###");
                            downloadResourceS[i] = downloadResourceS[i]
                                .Replace("class=\"audio\"", "class=\"media sound-unavailable\"")
                                .Replace("class=\"video\"", "class=\"media video-unavailable\"")
                                .Replace("class=\"document\"", "class=\"media pdf-unavailable\"");
                        }
                        break;
                    case SESSIONDONE://done
                        ((Label)e.Item.FindControl("lblCompleted")).Text = Model.SpeSCompleted;
                        ((Label)e.Item.FindControl("lblCompleted")).CssClass = "day-label-completed";
                        if (RetakeEnable == 0)//Retake is not enable
                        {
                            ((Button)e.Item.FindControl("btnOpenDay")).Visible = false;
                        }
                        else
                        {
                            ((Button)e.Item.FindControl("btnOpenDay")).Visible = true;
                            ((Button)e.Item.FindControl("btnOpenDay")).Text = Model.SpeSRetake;//"Retake this day";
                            ((Button)e.Item.FindControl("btnOpenDay")).CssClass = "openday";
                            ((Button)e.Item.FindControl("btnOpenDay")).Enabled = true;
                        }
                        for (int i = 0; i < downloadResourceS.Count; i++)
                        {
                            if (downloadResourceS[i] == null)
                                downloadResourceS[i] = string.Empty;
                            if (downloadResourceS[i].IndexOf("class=\"audio\"") > -1 || downloadResourceS[i].IndexOf("class=\"video\"") > -1)
                            {
                                int hrefIndex = downloadResourceS[i].IndexOf("href=");//href second \'
                                int hrefStartIndex = hrefIndex + 6;
                                int hrefEndIndex = downloadResourceS[i].IndexOf('\'', hrefStartIndex);
                                string replaceStr = downloadResourceS[i].Substring(hrefStartIndex, hrefEndIndex - hrefStartIndex);
                                string mediaPath = "https://changetechstorage.blob.core.windows.net/";
                                if (downloadResourceS[i].IndexOf("class=\"audio\"") > -1)
                                {
                                    mediaPath += "audiocontainer/";
                                    mediaPath += replaceStr.Substring(replaceStr.IndexOf("media=") + 6, replaceStr.IndexOf("name=") - 1 - replaceStr.IndexOf("media=") - 6);
                                    string mediaName = replaceStr.Substring(replaceStr.IndexOf("name=") + 5);
                                    downloadResourceS[i] = downloadResourceS[i].Remove(hrefEndIndex + 1, 16);
                                    downloadResourceS[i] = downloadResourceS[i].Insert(hrefEndIndex + 1, "  onclick=\"playAudio('" + mediaPath + "','" + mediaName + "')\"  ");
                                    downloadResourceS[i] = downloadResourceS[i].Replace(replaceStr, "###");
                                }
                                else
                                {
                                    mediaPath += "videocontainer/";
                                    mediaPath += replaceStr.Substring(replaceStr.IndexOf("media=") + 6, replaceStr.IndexOf("name=") - 1 - replaceStr.IndexOf("media=") - 6);

                                    string mediaName = replaceStr.Substring(replaceStr.IndexOf("name=") + 5);
                                    downloadResourceS[i] = downloadResourceS[i].Remove(hrefEndIndex + 1, 16);
                                    downloadResourceS[i] = downloadResourceS[i].Insert(hrefEndIndex + 1, "   onclick=\"playVideo('" + mediaPath + "','" + mediaName + "')\" ");
                                    downloadResourceS[i] = downloadResourceS[i].Replace(replaceStr, "###");
                                }

                                downloadResourceS[i] = downloadResourceS[i]
                                    .Replace("class=\"audio\"", "class=\"media sound\"")
                                    .Replace("class=\"video\"", "class=\"media video\"")
                                    .Replace("class=\"document\"", "class=\"media pdf\"");
                            }
                            else
                            {
                                downloadResourceS[i] = downloadResourceS[i]
                                   .Replace("class=\"audio\"", "class=\"media sound\"")
                                   .Replace("class=\"video\"", "class=\"media sound\"")
                                   .Replace("class=\"document\"", "class=\"media pdf\"");
                            }
                        }
                        break;
                    case SESSIONSTART://if (isHasDoneStr == ISHAVEDONESTART)//judge whether  ready to do
                        //Assert: only 1 row can be into this.
                        ((Label)e.Item.FindControl("lblCompleted")).Text = Model.SpeSReadyToGo;//"Ready to go";
                        ((Label)e.Item.FindControl("lblCompleted")).CssClass = "day-label-actual";
                        ((Button)e.Item.FindControl("btnOpenDay")).Text = Model.SpeSStart_day; //"Open this day";
                        ((Button)e.Item.FindControl("btnOpenDay")).CssClass = "openday";
                        ((Button)e.Item.FindControl("btnOpenDay")).Enabled = true;

                        for (int i = 0; i < downloadResourceS.Count; i++)
                        {
                            if (downloadResourceS[i] == null)
                                downloadResourceS[i] = string.Empty;
                            int hrefIndex = downloadResourceS[i].IndexOf("href=");//href second \'
                            int hrefStartIndex = hrefIndex + 6;
                            int hrefEndIndex = downloadResourceS[i].IndexOf('\'', hrefStartIndex);
                            string replaceStr = downloadResourceS[i].Substring(hrefStartIndex, hrefEndIndex - hrefStartIndex);
                            downloadResourceS[i] = downloadResourceS[i].Remove(hrefEndIndex + 1, 16);
                            downloadResourceS[i] = downloadResourceS[i].Replace(replaceStr, "###");
                            downloadResourceS[i] = downloadResourceS[i]
                                .Replace("class=\"audio\"", "class=\"media sound-unavailable\"")
                                .Replace("class=\"video\"", "class=\"media video-unavailable\"")
                                .Replace("class=\"document\"", "class=\"media pdf-unavailable\"");
                        }
                        break;
                }

                ((Repeater)e.Item.FindControl("rpDownloadList")).DataSource = downloadResourceS;
                ((Repeater)e.Item.FindControl("rpDownloadList")).DataBind();
            }
        }
        #endregion

        #region item command
        protected void rpSessions_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "OpenDay")
            {
                int isHasDoneStr = int.Parse(((Button)e.Item.FindControl("hiddenSessionIsHasDone")).Text.Trim());//0:Not yet available; 1:Retake this day;  2:ready to do;
                string dayNum = e.CommandArgument.ToString();
                Button btnOpenDay = (Button)e.Item.FindControl("btnOpenDay");

                string url = string.Empty;
                CTPPModel thisCTPPModel = Resolve<ICTPPService>().GetCTPPByBrandAndProgram(InputBrand, InputProgram);
                switch (isHasDoneStr)
                {
                    case SESSIONNOTDONE://btnOpenDay.Text == "Not yet available
                        //enabled=false
                        break;
                    case SESSIONDONE://btnOpenDay.Text == "Retake this day"

                        if (thisCTPPModel != null)
                        {
                            ProgramPropertyModel proPovertyModel = Resolve<IProgramService>().GetProgramProperty(thisCTPPModel.ProgramGUID);
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
                            string securityStr = StringUtility.MD5Encrypt(string.Format("{0};{1};{2};{3}", ViewStateusername, ViewStatepassword, UserTaskTypeEnum.RetakeSession.ToString(), dayNum), Constants.MD5_KEY);
                            url = serverPath + "ChangeTech.html?P=" + programCode + "&Mode=Live&Security=" + securityStr;
                            Response.Redirect(url, false);
                            return;
                        }
                        break;
                    case SESSIONSTART://start day
                        if (dayNum.Trim() == "0")//day 0, regist , screen url ,not login url as follows
                        {
                            if (thisCTPPModel != null)
                            {
                                ProgramPropertyModel proPovertyModel = Resolve<IProgramService>().GetProgramProperty(thisCTPPModel.ProgramGUID);
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
                                url = string.Format(serverPath + "ChangeTech.html?Mode=Trial&{0}={1}", Constants.QUERYSTR_PROGRAM_CODE, proPovertyModel.ProgramCode);
                                Response.Redirect(url, false);
                                return;
                            }
                        }
                        else// not day 0.
                        {
                            if (thisCTPPModel != null)
                            {
                                ProgramPropertyModel proPovertyModel = Resolve<IProgramService>().GetProgramProperty(thisCTPPModel.ProgramGUID);
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
                                string securityStr = StringUtility.MD5Encrypt(string.Format("{0};{1};{2};{3}", ViewStateusername, ViewStatepassword, UserTaskTypeEnum.TakeSession.ToString(), dayNum), Constants.MD5_KEY);
                                url = serverPath + "ChangeTech.html?P=" + programCode + "&Mode=Live&Security=" + securityStr;
                                Response.Redirect(url, false);
                                return;
                            }
                        }
                        break;
                }
            }
        }
        #endregion

        #region OtherCTPPlist
        protected void listOtherCTPP_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.DataItem != null)
            {
                string programGuid = ((Label)e.Item.FindControl("lblCTPPGuid")).Text;
                CTPPModel thisModel = Resolve<ICTPPService>().GetCTPP(new Guid(programGuid));
                if (thisModel != null)
                {
                    //if has entire logo
                    if (thisModel.CTPPLogo != null && !string.IsNullOrEmpty(thisModel.CTPPLogo.NameOnServer))
                    {
                        string accountName = Resolve<ISystemSettingService>().GetSettingValue("AzureStorageAccountName");
                        string bolbPath = ServiceUtility.GetBlobPath(accountName);
                        string ImageUrl = bolbPath + BlobContainerTypeEnum.LogoContainer.ToString().ToLower() + "/" + thisModel.CTPPLogo.NameOnServer;
                        ((System.Web.UI.HtmlControls.HtmlGenericControl)e.Item.FindControl("liOtherNewRow")).Visible = true;
                        ((System.Web.UI.HtmlControls.HtmlGenericControl)e.Item.FindControl("liOtherOldRow")).Visible = false;
                        ((LinkButton)e.Item.FindControl("lbOtherList2")).Style.Add("background", string.Format("url({0}) top left no-repeat #f0f6fc", ImageUrl));
                    }
                    else
                    {
                        if (thisModel.ProgramPresenter != null && !string.IsNullOrEmpty(thisModel.ProgramPresenter.NameOnServer))
                        {
                            string accountName = Resolve<ISystemSettingService>().GetSettingValue("AzureStorageAccountName");
                            string bolbPath = ServiceUtility.GetBlobPath(accountName);
                            string ImageUrl = bolbPath + BlobContainerTypeEnum.OriginalImageContainer.ToString().ToLower() + "/" + thisModel.ProgramPresenter.NameOnServer;
                            Image otherCTPPpreserter = (Image)e.Item.FindControl("otherCTPPLogo");
                            otherCTPPpreserter.ImageUrl = ImageUrl;
                        }
                        ((System.Web.UI.HtmlControls.HtmlGenericControl)e.Item.FindControl("liOtherNewRow")).Visible = false;
                        ((System.Web.UI.HtmlControls.HtmlGenericControl)e.Item.FindControl("liOtherOldRow")).Visible = true;
                    }
                }
                else//thisModel is null, so all can't be visible 
                {
                    ((System.Web.UI.HtmlControls.HtmlGenericControl)e.Item.FindControl("liOtherNewRow")).Visible = false;
                    ((System.Web.UI.HtmlControls.HtmlGenericControl)e.Item.FindControl("liOtherOldRow")).Visible = false;
                }
            }
        }
        #endregion

        protected void listOtherCTPP_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "otherCTPPClick")
            {
                string programGuid = e.CommandArgument.ToString();
                CTPPModel thisModel = Resolve<ICTPPService>().GetCTPP(new Guid(programGuid));
                if (thisModel != null)
                {
                    string url = "/" + InputBrand + "/" + thisModel.ProgramURL;
                    Response.Redirect(url, false);
                    return;
                }
            }
            else if (e.CommandName == "otherCTPPClick2")
            {
                string programGuid = e.CommandArgument.ToString();
                CTPPModel thisModel = Resolve<ICTPPService>().GetCTPP(new Guid(programGuid));
                if (thisModel != null)
                {
                    string url = "/" + InputBrand + "/" + thisModel.ProgramURL;
                    Response.Redirect(url, false);
                    return;
                }
            }
        }

        protected void btnBuy_Click(object sender, EventArgs e)
        {
            Response.Redirect(orderURL, false);
            return;
        }

        protected void btnBuy2_Click(object sender, EventArgs e)
        {
            Response.Redirect(orderURL, false);
            return;
        }


        //else
        //       {
        //           SpecialStringModel specialStringModelByLogout = Resolve<ISpecialStringService>().GetSpecialStringBy(logedStr[1], thisCTPPModel.LanguageGUID);
        //           this.LoginModelDia.Show();
        //           lblWrongInfo.Visible = true;
        //           if (specialStringModelByLogout != null)
        //           {
        //               lblWrongInfo.Text = !string.IsNullOrEmpty(specialStringModelByLogout.Value) ? specialStringModelByLogout.Value : logedStr[1];
        //           }
        //           else
        //           {
        //               lblWrongInfo.Text = logedStr[1];
        //           }
        //           return;
        //       }

        #region ForgotPassword
        protected void btnForgotPassword_Click(object sender, EventArgs e)
        {
            try
            {
                lblForgotPwdWrongInfo.Text = "";
                lblForgotPwdWrongInfo.Visible = false;
                IContainerContext containerContext = ContainerManager.GetContainer("container");
                CTPPModel thisCTPPModel = Resolve<ICTPPService>().GetCTPPByBrandAndProgram(InputBrand, InputProgram);
                if (Resolve<IUserService>().IsValidUserRetrievePassword(txtForgotPwdUserName.Text, thisCTPPModel.ProgramGUID))
                {
                    this.ForgotPwdDia.Show();
                    this.LoginModelDia.Hide();
                    lblForgotPwdWrongInfo.Visible = true;
                    Resolve<IEmailService>().SendForgetPasswordEmail(txtForgotPwdUserName.Text, thisCTPPModel.ProgramGUID);
                    txtForgotPwdUserName.Text = string.Empty;
                    //txtMobilePhone.Text = string.Empty;
                    SpecialStringModel specialStringModelByPwdHasSent = Resolve<ISpecialStringService>().GetSpecialStringBy(PasswordHasSent, thisCTPPModel.LanguageGUID);
                    if (specialStringModelByPwdHasSent != null)
                    {
                        lblForgotPwdWrongInfo.Text = !string.IsNullOrEmpty(specialStringModelByPwdHasSent.Value) ? specialStringModelByPwdHasSent.Value : PasswordHasSent;
                    }
                }
                else
                {
                    this.ForgotPwdDia.Show();
                    this.LoginModelDia.Hide();
                    lblForgotPwdWrongInfo.Visible = true;
                    txtForgotPwdUserName.Text = string.Empty;
                    //txtMobilePhone.Text = string.Empty;
                    SpecialStringModel specialStringModelByUserNotExisted = Resolve<ISpecialStringService>().GetSpecialStringBy(LoginFailedTypeEnum.UserNotExisted.ToString(), thisCTPPModel.LanguageGUID);
                    if (specialStringModelByUserNotExisted != null)
                    {
                        lblForgotPwdWrongInfo.Text = !string.IsNullOrEmpty(specialStringModelByUserNotExisted.Value) ? specialStringModelByUserNotExisted.Value : LoginFailedTypeEnum.UserNotExisted.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                this.LoginModelDia.Show();
                lblForgotPwdWrongInfo.Visible = true;
                lblForgotPwdWrongInfo.Text = ex.ToString();
                return;
            }
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
            HelpAndReportButtonDisplaySettingEnum displayStatus = HelpAndReportButtonDisplaySettingEnum.UnAvailable;// Help button default invisible.  //Not the same as Report button
            if (HttpContext.Current.Session["CurrentAccount"] != null && ContextService.CurrentAccount != null)//IsAuthenticated
            {
                if (ContextService.CurrentAccount.UserType == UserTypeEnum.User || ContextService.CurrentAccount.UserType == UserTypeEnum.Tester)
                {
                    if (!string.IsNullOrEmpty(HelpRelapsePageSequenceGuid) && HelpRelapsePageSequenceGuid != Guid.Empty.ToString())
                    {
                        string status = string.Empty;
                        if (BindProgramGuid != Guid.Empty && BindUserGuid != Guid.Empty)
                        {
                            status = Resolve<IProgramUserService>().GetProgramUserStatus(BindProgramGuid, BindUserGuid);
                        }
                        //If IsNeedHelp now is true ,it is available
                        if (status == ProgramUserStatusEnum.Active.ToString() || status == ProgramUserStatusEnum.Completed.ToString())
                        {
                            DateTime setCurrentTimeByTimeZone = DateTime.UtcNow;
                            setCurrentTimeByTimeZone = Resolve<IProgramUserService>().GetCurrentTimeByTimeZone(BindProgramGuid, BindUserGuid, setCurrentTimeByTimeZone);
                            if (Resolve<ISessionService>().GetIsNeedHelp(BindProgramGuid, BindUserGuid, setCurrentTimeByTimeZone))
                            {
                                displayStatus = HelpAndReportButtonDisplaySettingEnum.Actual;
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
                if (ContextService.CurrentAccount.UserType == UserTypeEnum.User || ContextService.CurrentAccount.UserType == UserTypeEnum.Tester)
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

            //switch (displayStatus)
            //{
            //    case HelpAndReportButtonDisplaySettingEnum.InVisible:
            //        this.DivReportButtonArea.Visible = false;
            //        break;
            //    case HelpAndReportButtonDisplaySettingEnum.Actual:
            //        this.DivReportButtonArea.Visible = true;
            //        this.DivReportButtonArea.Attributes["class"] = "container-checkin-actual";
            //        this.ReportButtonHeading.InnerHtml = Model.SpeSReportButtonHeading;
            //        //<a href="#" class="b-checkin" >Report here!</a>
            //        this.ReportButtonLinkArea.InnerHtml = string.Format("<a href=\"{0}\" class=\"b-checkin\" >{1}</a>", GetReportButtonLinkAddress(), Model.SpeSReportButtonActual);
            //        break;
            //    case HelpAndReportButtonDisplaySettingEnum.Complete:
            //        this.DivReportButtonArea.Visible = true;
            //        this.DivReportButtonArea.Attributes["class"] = "container-checkin-complete";
            //        this.ReportButtonHeading.InnerHtml = Model.SpeSReportButtonHeading;
            //        //<div class="day-label-completed">Completed</div>
            //        this.ReportButtonLinkArea.InnerHtml = string.Format("<div class=\"day-label-completed\">{0}</div>", Model.SpeSReportButtonComplete);
            //        break;
            //    case HelpAndReportButtonDisplaySettingEnum.UnAvailable:
            //        this.DivReportButtonArea.Visible = true;
            //        this.DivReportButtonArea.Attributes["class"] = "container-checkin-untaken";
            //        this.ReportButtonHeading.InnerHtml = Model.SpeSReportButtonHeading;
            //        //<a class="b-checkin">Unavailable</a>
            //        this.ReportButtonLinkArea.InnerHtml = string.Format("<a class=\"b-checkin\">{0}</a>", Model.SpeSReportButtonUntaken);
            //        break;
            //    default:
            //        this.DivReportButtonArea.Visible = false;
            //        break;
            //}
        }

        private void BindFactsContainerArea(CTPPModel thisCTPPModel)
        {
            if (HttpContext.Current.Session["CurrentAccount"] != null && ContextService.CurrentAccount != null)//IsAuthenticated
            {
                this.container_facts.Visible = false;
            }
            else
            {
                this.container_facts.Visible = true;

                //The 4 facts header ,content and video
                #region 4Facts
                if (!string.IsNullOrEmpty(thisCTPPModel.FactHeader1))
                {
                    this.h5Fact1Header.Visible = true;
                    this.h5Fact1Header.InnerHtml = thisCTPPModel.FactHeader1;
                }
                else
                {
                    this.h5Fact1Header.Visible = false;
                }
                if (!string.IsNullOrEmpty(thisCTPPModel.FactHeader2))
                {
                    this.h5Fact2Header.Visible = true;
                    this.h5Fact2Header.InnerHtml = thisCTPPModel.FactHeader2;
                }
                else
                {
                    this.h5Fact2Header.Visible = false;
                }
                if (!string.IsNullOrEmpty(thisCTPPModel.FactHeader3))
                {
                    this.h5Fact3Header.Visible = true;
                    this.h5Fact3Header.InnerHtml = thisCTPPModel.FactHeader3;
                }
                else
                {
                    this.h5Fact3Header.Visible = false;
                }
                if (!string.IsNullOrEmpty(thisCTPPModel.FactHeader4))
                {
                    this.h5Fact4Header.Visible = true;
                    this.h5Fact4Header.InnerHtml = thisCTPPModel.FactHeader4;
                }
                else
                {
                    this.h5Fact4Header.Visible = false;
                }
                if (!string.IsNullOrEmpty(thisCTPPModel.FactContent1))
                {
                    this.divFact1Content.Visible = true;
                    this.divFact1Content.InnerHtml = thisCTPPModel.FactContent1;
                }
                else
                {
                    this.divFact1Content.Visible = false;
                }
                if (!string.IsNullOrEmpty(thisCTPPModel.FactContent2))
                {
                    this.divFact2Content.Visible = true;
                    this.divFact2Content.InnerHtml = thisCTPPModel.FactContent2;
                }
                else
                {
                    this.divFact2Content.Visible = false;
                }
                if (!string.IsNullOrEmpty(thisCTPPModel.FactContent3))
                {
                    this.divFact3Content.Visible = true;
                    this.divFact3Content.InnerHtml = thisCTPPModel.FactContent3;
                }
                else
                {
                    this.divFact3Content.Visible = false;
                }
                if (!string.IsNullOrEmpty(thisCTPPModel.FactContent4))
                {
                    this.divFact4Content.Visible = true;
                    this.divFact4Content.InnerHtml = thisCTPPModel.FactContent4;
                }
                else
                {
                    this.divFact4Content.Visible = false;
                }

                if (!string.IsNullOrEmpty(thisCTPPModel.VideoLink) && thisCTPPModel.ImageForVideoLink != null)//Video area on the right of box.
                {
                    //this.videoplayerInBox.Visible = true;
                    this.videoArea.Visible = true;

                    //image and video
                    string accountName = Resolve<ISystemSettingService>().GetSettingValue("AzureStorageAccountName");
                    string bolbPath = ServiceUtility.GetBlobPath(accountName);
                    string imageUrl = bolbPath + BlobContainerTypeEnum.LogoContainer.ToString().ToLower() + "/" + thisCTPPModel.ImageForVideoLink.NameOnServer;
                    this.hplinkImageForVideo.Style.Add("background", string.Format("url({0}) no-repeat", imageUrl));
                    this.hplinkImageForVideo.Style.Add("background-size", "100% 100%");

                    this.hplinkImageForVideo.Attributes.Add("onclick", "playVideo('" + thisCTPPModel.VideoLink + "','Video')");

                    URLofVideoBox = thisCTPPModel.VideoLink.Trim();
                }
                else
                {
                    this.videoArea.Visible = false;
                }
                #endregion

            }
        }

        /// <summary>
        /// set the programwindow-right div area. Include the Facts, Help button and Report button
        /// </summary>
        /// <param name="thisCTPPModel"></param>
        private void SetProgramwindowRightArea(CTPPModel thisCTPPModel)
        {
            BindFactsContainerArea(thisCTPPModel);
            SetDisplayStatusForReportRelapse();
            SetDisplayStatusForHelpRelapse();
        }

    }
}