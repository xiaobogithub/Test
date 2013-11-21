using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ethos.DependencyInjection;
using ChangeTech.Models;
using ChangeTech.Contracts;
using System.Xml;
using System.IO;
using Ethos.Utility;
using ChangeTech.Services;

namespace ChangeTech.DeveloperWeb
{
    public partial class ManageProgram : PageBase<ProgramPropertyModel>
    {
        private string ProgramGUID { get { return Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]; } }
        private string DefaultTimeZoneValue { get { return Request.Form["DropDownTimezone"]; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                InitialPage();
                ((HyperLink)Master.FindControl("menuLoginView").FindControl("hpBackLink")).NavigateUrl = string.Format("EditProgram.aspx?{0}={1}", Constants.QUERYSTR_PROGRAM_GUID, ProgramGUID);
            }
        }

        private void InitialPage()
        {
            Model = Resolve<IProgramService>().GetProgramProperty(new Guid(ProgramGUID));
           
            if (Model.ProgramLogo!= null)
            {
                string accountName = Resolve<ISystemSettingService>().GetSettingValue("AzureStorageAccountName");
                string bolbPath = ServiceUtility.GetBlobPath(accountName);
                programLogo.ImageUrl = bolbPath + BlobContainerTypeEnum.LogoContainer.ToString().ToLower() + "/" + Model.ProgramLogo.Name;
            }
            paymentCheckBox.Checked = Model.IsNeedPayment;
            pinCodeCheckBox.Checked = Model.IsNeedPinCode;
            priceTextBox.Text = Model.Price;
            cutConnectCheckBox.Checked = Model.IsNeedCutConnect;
            weeksTextBox.Text = Model.Weeks;
            isContainTwoPartsCheckBox.Checked = Model.IsContainTwoParts;
            InitialDropDown();
            switchDayDropDownList.SelectedValue = Model.SwitchDay.ToString();
            supportHttpsCheckBox.Checked = Model.IsSupportHttps;
            genderCheckBox.Checked = Model.SeparateGender;
            needSerialNumberCheckBox.Checked = Model.IsNeedSerialNumber;
            cttpEnableCheckBox.Checked = Model.IsCTPPEnable;
            html5EnableCheckBox.Checked = Model.IsHTML5PreviewEnable;
            supportEmailTextBox.Text = Model.SupportEmail;
            supportNameTextBox.Text = Model.SupportName;
            noCatchingUpCheckBox.Checked = Model.IsNoCatchUp;
            supportTimeZoneCheckBox.Checked = Model.IsSupportTimeZone;
            orderProgramCheckBox.Checked = Model.IsOrderProgram;
            hpOrderProgramCheckBox.Checked = Model.IsHPOrderProgram;
            startButtonInvisibleCheckBox.Checked = Model.IsInvisibleStartButton;
            dayAndSetMenuInvisibleCheckBox.Checked = Model.IsInvisibleDayAndSetMenu;
            html5NewUIEnableCheckBox.Checked = Model.EnableHTML5NewUI;
            txtOrderProgramText.Text = Model.OrderProgramText;
            shortNameTextBox.Text = Model.ShortName;
            switch (Model.FlashOrHTML5)
            {
                case (int)FlashOrHtml5Enum.FlashOnly: ChooseFlash.Checked = true; break;
                case (int)FlashOrHtml5Enum.AutoDetection: ChooseDetect.Checked = true; break;
                case (int)FlashOrHtml5Enum.HTML5Only: ChooseHTML5.Checked = true; break;
                default: ChooseFlash.Checked = true; break;
            }

            string serverPath = string.Empty;
            if(Model.IsSupportHttps)
            {
                serverPath = Resolve<ISystemSettingService>().GetSettingValue("WebServerPathHttps");
            }
            else
            {
                serverPath = Resolve<ISystemSettingService>().GetSettingValue("WebServerPath");
            }

            string screenurl = string.Format(serverPath + "ChangeTech.html?Mode=Trial&{0}={1}", Constants.QUERYSTR_PROGRAM_CODE, Model.ProgramCode);
            string liveurl = string.Format(serverPath + "ChangeTech.html?Mode=Live&{0}={1}", Constants.QUERYSTR_PROGRAM_CODE, Model.ProgramCode);
            string testurl = string.Format(serverPath + "ChangeTech.html?Mode=Trial&{0}={1}&{2}={3}", Constants.QUERYSTR_PROGRAM_CODE, Model.ProgramCode, Constants.QUERYSTR_USERTYPE, "TESTER");

            string screenFlashURL = screenurl.Replace("ChangeTech.html", "ChangeTechF.html");
            string liveFlashURL = liveurl.Replace("ChangeTech.html", "ChangeTechF.html");
            string testFlashURL = testurl.Replace("ChangeTech.html", "ChangeTechF.html");

            string screenHTML5URL = screenurl.Replace("ChangeTech.html", "ChangeTech5.html");
            string liveHTML5URL = liveurl.Replace("ChangeTech.html", "ChangeTech5.html");
            string testHTML5URL = testurl.Replace("ChangeTech.html", "ChangeTech5.html");
            string CTPPurl = string.Empty;
            if (Model.IsCTPPEnable)
            {
                CTPPModel thisCTPPModel = Resolve<ICTPPService>().GetCTPP(new Guid(ProgramGUID));
                if (thisCTPPModel != null && thisCTPPModel.BrandGUID != Guid.Empty)
                {
                    BrandModel thisBrandModel = Resolve<IBrandService>().GetBrandByGUID(thisCTPPModel.BrandGUID);
                    if (thisBrandModel != null && (!string.IsNullOrEmpty(thisBrandModel.BrandURL))&&(!string.IsNullOrEmpty(thisCTPPModel.ProgramURL)))
                    {
                        CTPPurl = string.Format(serverPath + "{0}/{1}", thisBrandModel.BrandURL, thisCTPPModel.ProgramURL);
                    }
                }
            }

            //Default link
            screenDefaultHyperLink.Text = screenurl;
            screenDefaultHyperLink.NavigateUrl = screenurl;
            loginDefaultHyperLink.Text = liveurl;
            loginDefaultHyperLink.NavigateUrl = liveurl;
            testDefaultHyperLink.Text = testurl;
            testDefaultHyperLink.NavigateUrl = testurl;


            //Flash link
            screenFlashHyperLink.Text = screenFlashURL;
            screenFlashHyperLink.NavigateUrl = screenFlashURL;
            loginFlashHyperLink.Text = liveFlashURL;
            loginFlashHyperLink.NavigateUrl = liveFlashURL;
            testFlashHyperLink.Text = testFlashURL;
            testFlashHyperLink.NavigateUrl = testFlashURL;


            //HTML5 link
            screenHTML5HyperLink.Text = screenHTML5URL;
            screenHTML5HyperLink.NavigateUrl = screenHTML5URL;
            loginHTML5HyperLink.Text = liveHTML5URL;
            loginHTML5HyperLink.NavigateUrl = liveHTML5URL;
            testHTML5HyperLink.Text = testHTML5URL;
            testHTML5HyperLink.NavigateUrl = testHTML5URL;

            //CTPP link
            CTPPHyperLink.Text = CTPPurl;
            CTPPHyperLink.NavigateUrl = CTPPurl;
        }

        private void InitialDropDown()
        {
            switchDayDropDownList.Items.Clear();
            int days = Resolve<ISessionService>().GetNumberOfNormalSessions(new Guid(ProgramGUID));
            for(int i = 0; i < days; i++)
            {
                switchDayDropDownList.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
        }


        protected void saveButton_Click(object sender, EventArgs e)
        {
            Guid fileGuid = Guid.NewGuid();
            string fileType = "";
            string fileName = "";
            if (!IsValidateShortName())
            {
                ClientScript.RegisterStartupScript(this.GetType(), "tip", "alert('Short name is used by other program.');", true);
                return;
            }
            if(!ValidatePrice())                 
            {
                ClientScript.RegisterStartupScript(this.GetType(), "tip", "alert('Price is wrong fomat.');", true);
                return;
            }           
            if (fileUpload.HasFile)
            {
                fileType = fileUpload.FileName.Substring(fileUpload.FileName.LastIndexOf("."));
                fileName = fileGuid.ToString() + fileType;

                Resolve<IResourceService>().SaveResourceToAzureBlobStorage(fileUpload.FileContent, fileGuid.ToString(), fileName, ResourceTypeEnum.Logo.ToString(), Guid.Empty);
            }

            ProgramPropertyModel ppModel = new ProgramPropertyModel();
            ppModel.ProgramGUID = new Guid(ProgramGUID);
            ppModel.IsNeedPayment = paymentCheckBox.Checked;
            ppModel.IsNeedPinCode = pinCodeCheckBox.Checked;
            ppModel.IsNeedCutConnect = cutConnectCheckBox.Checked;
            ppModel.IsContainTwoParts = isContainTwoPartsCheckBox.Checked;
            ppModel.IsSupportHttps = supportHttpsCheckBox.Checked;
            ppModel.IsNeedSerialNumber = needSerialNumberCheckBox.Checked;
            ppModel.SeparateGender = genderCheckBox.Checked;
            ppModel.SupportEmail = supportEmailTextBox.Text;
            ppModel.SupportName = supportNameTextBox.Text;
            ppModel.IsCTPPEnable = cttpEnableCheckBox.Checked;
            ppModel.IsHTML5PreviewEnable = html5EnableCheckBox.Checked;
            ppModel.IsNoCatchUp = noCatchingUpCheckBox.Checked;
            ppModel.IsSupportTimeZone = supportTimeZoneCheckBox.Checked;
            ppModel.IsOrderProgram = orderProgramCheckBox.Checked;
            ppModel.IsHPOrderProgram = hpOrderProgramCheckBox.Checked;
            ppModel.IsInvisibleStartButton = startButtonInvisibleCheckBox.Checked;
            ppModel.IsInvisibleDayAndSetMenu = dayAndSetMenuInvisibleCheckBox.Checked;
            ppModel.EnableHTML5NewUI = html5NewUIEnableCheckBox.Checked;
            ppModel.OrderProgramText = txtOrderProgramText.Text;
            ppModel.ShortName = shortNameTextBox.Text;
            ppModel.ProgramLogo = new ResourceModel()
            {
                ID = fileGuid,
                Name = fileName,
                Type = "Logo",
                Extension = fileType
            };
            //judge whether  support TimeZone function.if is true , set default TimeZone.
            if (supportTimeZoneCheckBox.Checked)
            {
                ProgramModel programModel = Resolve<IProgramService>().GetProgramByGUID(new Guid(ProgramGUID));
                string logMessage = " TimeZone : " + programModel.TimeZone + " ---> TimeZone : " + DefaultTimeZoneValue;
                //update program's TimeZone.
                ppModel.TimeZone = Convert.ToDecimal(DefaultTimeZoneValue);

                // add log
                InsertLogModel model = new InsertLogModel
                {
                    ActivityLogType = (int)LogTypeEnum.ModifyProgram,
                    Browser = HttpContext.Current.Request.UserAgent,
                    IP = HttpContext.Current.Request.UserHostAddress,
                    Message = logMessage,
                    ProgramGuid = new Guid(ProgramGUID),
                    PageGuid = Guid.Empty,
                    PageSequenceGuid = Guid.Empty,
                    SessionGuid = Guid.Empty,
                    UserGuid = ContextService.CurrentAccount.UserGuid,
                    From = string.Empty
                };
                Resolve<IActivityLogService>().Insert(model);
            }

            if (ChooseFlash.Checked)
            {
                ppModel.FlashOrHTML5 = (int)FlashOrHtml5Enum.FlashOnly;
            }
            else if (ChooseDetect.Checked)
            {
                ppModel.FlashOrHTML5 = (int)FlashOrHtml5Enum.AutoDetection;
            }
            else if (ChooseHTML5.Checked)
            {
                ppModel.FlashOrHTML5 = (int)FlashOrHtml5Enum.HTML5Only;
            }
            else//default
            {
                ppModel.FlashOrHTML5 = (int)FlashOrHtml5Enum.FlashOnly;
            }


            if(cutConnectCheckBox.Checked)
            {
                ppModel.Weeks = weeksTextBox.Text;
            }

            if(paymentCheckBox.Checked)
            {
                ppModel.Price = priceTextBox.Text;
            }

            if(isContainTwoPartsCheckBox.Checked)
            {
                ppModel.SwitchDay = Convert.ToInt32(switchDayDropDownList.SelectedValue);
            }
            else
            {
                ppModel.SwitchDay = 0;
            }
            // add log about update Program.
            InsertLogModel logModel = new InsertLogModel
            {
                ActivityLogType = (int)LogTypeEnum.ModifyProgram,
                Browser = HttpContext.Current.Request.UserAgent,
                IP = HttpContext.Current.Request.UserHostAddress,
                Message = "Update Program IsOrderProgram : " + ppModel.IsOrderProgram
                                + " ; Update Program IsSupportTimeZone : " + ppModel.IsSupportTimeZone
                                + " ; Update Program IsNoCatchUp : " + ppModel.IsNoCatchUp
                                + " ; Update Program IsHTML5PreviewEnable : " + ppModel.IsHTML5PreviewEnable
                                + " ; Update Program IsCTPPEnable : " + ppModel.IsCTPPEnable
                                + " ; Update Program IsSupportHttps : " + ppModel.IsSupportHttps
                                + " ; Update Program IsContainTwoParts : " + ppModel.IsContainTwoParts
                                + " ; Update Program IsNeedPayment : " + ppModel.IsNeedPayment
                                + " ; Update Program IsNeedPinCode : " + ppModel.IsNeedPinCode
                                + " ; Update Program FlashOrHTML5 : " + ppModel.FlashOrHTML5
                                + " ; Update Program TimeZone : " + ppModel.TimeZone
                                + " ; Update Program IsNoCatchUp : " + ppModel.IsNoCatchUp
                                + " ; Update Program IsInvisibleStartButton : " + ppModel.IsInvisibleStartButton
                                + " ; Update Program IsInvisibleDayAndSetMenu : " + ppModel.IsInvisibleDayAndSetMenu,
                ProgramGuid = ppModel.ProgramGUID,
                PageGuid = Guid.Empty,
                PageSequenceGuid = Guid.Empty,
                SessionGuid = Guid.Empty,
                UserGuid = ContextService.CurrentAccount.UserGuid,
                From = string.Empty
            };
            Resolve<IActivityLogService>().Insert(logModel);
            Resolve<IProgramService>().UpdateProgramProperty(ppModel);
            Response.Redirect(string.Format("ManageProgram.aspx?{0}={1}", Constants.QUERYSTR_PROGRAM_GUID, ProgramGUID)); 

        }

        protected void cancelButton_Click(object sender, EventArgs e)
        {
            string goBackUrl = string.Format("EditProgram.aspx?{0}={1}", Constants.QUERYSTR_PROGRAM_GUID, ProgramGUID);
            Response.Redirect(goBackUrl);
        }

        private bool ValidatePrice()
        {
            bool flag = true;
            if(paymentCheckBox.Checked)
            {
                int price;
                flag = int.TryParse(priceTextBox.Text, out price);
            }
            if(cutConnectCheckBox.Checked)
            {
                int weeks;
                flag = int.TryParse(weeksTextBox.Text, out weeks);
            }

            return flag;
        }

        private bool IsValidateShortName()
        {
            return Resolve<IProgramService>().IsValidShortName(new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]), shortNameTextBox.Text);
        }
    }
}
