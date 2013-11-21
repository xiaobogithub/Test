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

namespace ChangeTech.DeveloperWeb
{
    public partial class ManageCTPP : PageBase<CTPPModel>
    {
        public string VersionNumberWithoutDot
        {
            get
            {
                string version = System.Configuration.ConfigurationManager.AppSettings["ProjectVersionWithoutDot"];
                return version;
            }
        }

        private string ProgramPage
        {
            get
            {
                return Request.QueryString[Constants.QUERYSTR_PROGRAM_PAGE];
            }
        }

        private string CTPPGuid
        {
            get
            {
                return ViewState["CTPPGUID"] == null ? null : ViewState["CTPPGUID"].ToString();
            }
            set
            {
                ViewState["CTPPGUID"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string azureAccountName = Resolve<ISystemSettingService>().GetSettingValue("AzureStorageAccountName");
            if (!string.IsNullOrEmpty(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]))
            {
                if (!IsPostBack)
                {
                    try
                    {
                        BindCTPPTemplate();
                        ((HyperLink)Master.FindControl("menuLoginView").FindControl("hpBackLink")).NavigateUrl = string.Format(string.Format("EditProgram.aspx?{0}={1}", Constants.QUERYSTR_PROGRAM_GUID, Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]));
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

        #region BindCTPPTemplate
        private void BindCTPPTemplate()
        {
            lblErrorProgramUrl.Visible = false;

            ProgramModel programModel = Resolve<IProgramService>().GetProgramByGUID(new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]));
            programLabel.Text = programModel.ProgramName;
            //this.hlSetPresenter.NavigateUrl = string.Format("CTPPPresenterImage.aspx?{0}={1}", Constants.QUERYSTR_PROGRAM_GUID, Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]);


            List<BrandModel> brandsModel = Resolve<IBrandService>().GetBrandList();
            if (brandsModel.Any())
            {
                ddlBrand.DataSource = brandsModel;
            }
            else
            {
                ddlBrand.DataSource = null;
            }
            ddlBrand.DataBind();

            Model = new CTPPModel();
            Model = Resolve<ICTPPService>().GetCTPP(programModel.Guid);
            if (Model != null && Model.CTPPGUID != Guid.Empty)//get the ctpp items
            {
                CTPPGuid = Model.CTPPGUID.ToString();

                ddlBrand.SelectedValue = Model.BrandGUID.ToString();
                txtDescription.Text = Model.ProgramDescription;
                txtDescriptionTitle.Text = Model.ProgramDescriptionTitle;
                txtDescriptionForMobile.Text = Model.ProgramDescriptionForMobile;
                txtDescriptionTitleForMobile.Text = Model.ProgramDescriptionTitleForMobile;
                txtLinkTechnology.Text = Model.LinkTechnology;
                //txtProgramName.Text = Model.ProgramName;
                txtProgramSubheading.Text = Model.ProgramSubheading;
                txtProgramURL.Text = Model.ProgramURL;
                txtPromotionLink1.Text = Model.PromotionLink1;
                txtPromotionLink2.Text = Model.PromotionLink2;
                txtVideoLink.Text = Model.VideoLink;
                txtFacebookLink.Text = Model.FacebookLink;
                txtForSideLink.Text = Model.ForSideLink;
                txtSubPriceLink.Text = Model.SubPriceLink;

                txtFact1Header.Text = Model.FactHeader1;
                txtFact1Content.Text = Model.FactContent1;
                txtFact2Header.Text = Model.FactHeader2;
                txtFact2Content.Text = Model.FactContent2;
                txtFact3Header.Text = Model.FactHeader3;
                txtFact3Content.Text = Model.FactContent3;
                txtFact4Header.Text = Model.FactHeader4;
                txtFact4Content.Text = Model.FactContent4;

                //IsNotShowOtherPrograms
                if (Model.IsNotShowOtherPrograms.Value == true)
                {
                    chbOtherProgramsInvisible.Checked = true;
                }
                else
                {
                    chbOtherProgramsInvisible.Checked = false;
                }

                ////IsNotShowStartButton
                //if (Model.IsNotShowStartButton.Value == true)
                //{
                //    chbStartButtonInvisible.Checked = true;
                //}
                //else
                //{
                //    chbStartButtonInvisible.Checked = false;
                //}

                //IsEnableSpecificReportAndHelpButtons
                if (Model.IsEnableSpecificReportAndHelpButtons.Value == true)
                {
                    chbSpecificReportAndHelp.Checked = true;
                    txtHelpButtonHeading.Enabled = true;
                    txtHelpButtonActual.Enabled = true;
                    txtReportButtonHeading.Enabled = true;
                    txtReportButtonActual.Enabled = true;
                    txtReportButtonComplete.Enabled = true;
                    txtReportButtonUntaken.Enabled = true;
                }
                else
                {
                    chbSpecificReportAndHelp.Checked = false;
                    txtHelpButtonHeading.Enabled = false;
                    txtHelpButtonActual.Enabled = false;
                    txtReportButtonHeading.Enabled = false;
                    txtReportButtonActual.Enabled = false;
                    txtReportButtonComplete.Enabled = false;
                    txtReportButtonUntaken.Enabled = false;
                }
                txtReportButtonHeading.Text = Model.ReportButtonHeading;
                txtReportButtonActual.Text = Model.ReportButtonActual;
                txtReportButtonComplete.Text = Model.ReportButtonComplete;
                txtReportButtonUntaken.Text = Model.ReportButtonUntaken;
                txtHelpButtonHeading.Text = Model.HelpButtonHeading;
                txtHelpButtonActual.Text = Model.HelpButtonActual;


                if (Model.IsFacebook == 1)
                {
                    chbFacebook.Checked = true;
                }
                else
                {
                    chbFacebook.Checked = false;
                }

                if (Model.IsTwitter == 1)
                {
                    chbTwitter.Checked = true;
                }
                else
                {
                    chbTwitter.Checked = false;
                }

                if (Model.PromotionField1 != null)
                {
                    string accountName = Resolve<ISystemSettingService>().GetSettingValue("AzureStorageAccountName");
                    string bolbPath = ServiceUtility.GetBlobPath(accountName);
                    promotionField1.ImageUrl = bolbPath + BlobContainerTypeEnum.LogoContainer.ToString().ToLower() + "/" + Model.PromotionField1.NameOnServer;
                }

                if (Model.PromotionField2 != null)
                {
                    string accountName = Resolve<ISystemSettingService>().GetSettingValue("AzureStorageAccountName");
                    string bolbPath = ServiceUtility.GetBlobPath(accountName);
                    promotionField2.ImageUrl = bolbPath + BlobContainerTypeEnum.LogoContainer.ToString().ToLower() + "/" + Model.PromotionField2.NameOnServer;
                }

                this.txtDarkColor.Text = Model.BackDarkColor;
                txtLightColor.Text = Model.BackLightColor;
                txtSubheadColor.Text = Model.ProgramSubheadColor;
                if (Model.IsGooglePlus == 1)
                {
                    chbGooglePlus.Checked = true;
                }
                else
                {
                    chbGooglePlus.Checked = false;
                }
                if (Model.InActive == 1)
                {
                    chbInactive.Checked = true;
                }
                else
                {
                    chbInactive.Checked = false;
                }
                if (Model.RetakeEnable == 1)
                {
                    chbRetake.Checked = true;
                }
                else
                {
                    chbRetake.Checked = false;
                }
                if (Model.CTPPLogo != null)
                {
                    string accountName = Resolve<ISystemSettingService>().GetSettingValue("AzureStorageAccountName");
                    string bolbPath = ServiceUtility.GetBlobPath(accountName);
                    CTPPLogoField.ImageUrl = bolbPath + BlobContainerTypeEnum.LogoContainer.ToString().ToLower() + "/" + Model.CTPPLogo.NameOnServer;
                }

                if (Model.ImageForVideoLink != null)
                {
                    string accountName = Resolve<ISystemSettingService>().GetSettingValue("AzureStorageAccountName");
                    string bolbPath = ServiceUtility.GetBlobPath(accountName);
                    imgForVideo.ImageUrl = bolbPath + BlobContainerTypeEnum.LogoContainer.ToString().ToLower() + "/" + Model.ImageForVideoLink.NameOnServer;
                }

                if (Model.ProgramPresenter != null)
                {
                    string accountName = Resolve<ISystemSettingService>().GetSettingValue("AzureStorageAccountName");
                    string bolbPath = ServiceUtility.GetBlobPath(accountName);
                    CTPPPresenterField.ImageUrl = bolbPath + BlobContainerTypeEnum.OriginalImageContainer.ToString().ToLower() + "/" + Model.ProgramPresenter.NameOnServer;
                }

                if (Model.MobileBookmarkLink != null)
                {
                    string accountName = Resolve<ISystemSettingService>().GetSettingValue("AzureStorageAccountName");
                    string bolbPath = ServiceUtility.GetBlobPath(accountName);
                    imgMobileBookmark.ImageUrl = bolbPath + BlobContainerTypeEnum.LogoContainer.ToString().ToLower() + "/" + Model.MobileBookmarkLink.NameOnServer;
                }

                //For help button relapse initialize
                if (!string.IsNullOrEmpty(Model.HelpButtonRelapsePageSequenceName))
                    lblHelpRelapse.Text = Model.HelpButtonRelapsePageSequenceName;
                else
                    lblHelpRelapse.Text = string.Empty;

                //For report button relapse initialize
                if (!string.IsNullOrEmpty(Model.ReportButtonRelapsePageSequenceName))
                    lblReportRelapse.Text = Model.ReportButtonRelapsePageSequenceName;
                else
                    lblReportRelapse.Text = string.Empty;

                if (Model.ReportButtonAvailableTime != null)
                    txtReportButtonAvailableTime.Text = Model.ReportButtonAvailableTime.ToString();

                txtRemindSMS1Text.Text = Model.RemindSMS1Text;
                if (Model.RemindSMS1TimeMinute != null && Model.RemindSMS1TimeMinute != 0)
                {
                    int minutes = Convert.ToInt32(Model.RemindSMS1TimeMinute);
                    txtRemindSMS1TimeHour.Text = (minutes / 60).ToString();
                    txtRemindSMS1TimeMinute.Text = (minutes % 60).ToString();
                }
                else
                {
                    txtRemindSMS1TimeHour.Text = string.Empty;
                    txtRemindSMS1TimeMinute.Text = string.Empty;
                }
                txtRemindSMS2Text.Text = Model.RemindSMS2Text;
                if (Model.RemindSMS2TimeMinute != null && Model.RemindSMS2TimeMinute != 0)
                {
                    int minutes = Convert.ToInt32(Model.RemindSMS2TimeMinute);
                    txtRemindSMS2TimeHour.Text = (minutes / 60).ToString();
                    txtRemindSMS2TimeMinute.Text = (minutes % 60).ToString();
                }
                else
                {
                    txtRemindSMS2TimeHour.Text = string.Empty;
                    txtRemindSMS2TimeMinute.Text = string.Empty;
                }
            }
            else//create a new ctpp table into db so that the okButton only used to update(no insert) the db
            {
                CTPPModel CTPPTemplateModel = new CTPPModel();
                if (ddlBrand.DataSource != null)
                {
                    CTPPTemplateModel.BrandGUID = new Guid(ddlBrand.SelectedValue);
                }
                else
                {
                    CTPPTemplateModel.BrandGUID = Guid.Empty;
                }
                CTPPTemplateModel.IsNotShowOtherPrograms = false;
                CTPPTemplateModel.IsNotShowStartButton = false;
                CTPPTemplateModel.IsFacebook = 0;
                CTPPTemplateModel.IsTwitter = 0;
                CTPPTemplateModel.IsGooglePlus = 0;
                CTPPTemplateModel.InActive = 0;
                CTPPTemplateModel.RetakeEnable = 0;
                CTPPTemplateModel.ProgramGUID = new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]);
                CTPPGuid = Resolve<ICTPPService>().AddCTPPTemplate(CTPPTemplateModel).ToString();
            }
        }
        #endregion

        protected void okButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (!UpdateWholePageWithoutSpecailItems()) return;
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
            Response.Redirect(Request.Url.ToStringWithoutPort());
        }

        protected void cancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format(string.Format("EditProgram.aspx?{0}={1}", Constants.QUERYSTR_PROGRAM_GUID, Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID])));
        }

        /// <remarks>The special items means that the items need be changed by click button. eg. Help button, Report button etc.</remarks>
        private bool UpdateWholePageWithoutSpecailItems()
        {
            bool flag = false;

            Model = Resolve<ICTPPService>().GetCTPP(new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]));
            if (Model != null)
            {
                CTPPModel CTPPModel = new CTPPModel();
                if (!string.IsNullOrEmpty(ddlBrand.SelectedValue))
                {
                    CTPPModel.BrandGUID = new Guid(ddlBrand.SelectedValue);
                    //Judge whether the programUrl && branUrl has existed.

                    if (!(Model.BrandGUID == CTPPModel.BrandGUID && (Model.ProgramURL == null || Model.ProgramURL == txtProgramURL.Text.Trim())))
                    {
                        BrandModel brandModel = Resolve<IBrandService>().GetBrandByGUID(CTPPModel.BrandGUID);
                        if (Resolve<ICTPPService>().IsExistCTPPWithBrandUrlAndProgramUrl(brandModel.BrandURL, txtProgramURL.Text.Trim()))
                        {
                            //Page.ClientScript.RegisterClientScriptBlock(GetType(), "", "<script>alert('The program URL in this brand has existed. Please rename it to a new one.')</scripy>");
                            lblErrorProgramUrl.Visible = true;
                            lblErrorProgramUrl.Text = "The program URL in this brand has existed. Please rename it to a new one.";
                            return false;
                        }
                        else
                        {
                            lblErrorProgramUrl.Visible = false;
                        }
                    }
                }
                else
                {
                    CTPPModel.BrandGUID = Guid.Empty;
                }
                CTPPModel.CTPPGUID = Model.CTPPGUID;
                if (chbFacebook.Checked)
                {
                    CTPPModel.IsFacebook = 1;
                }
                else
                {
                    CTPPModel.IsFacebook = 0;
                }
                if (chbTwitter.Checked)
                {
                    CTPPModel.IsTwitter = 1;
                }
                else
                {
                    CTPPModel.IsTwitter = 0;
                }
                CTPPModel.LinkTechnology = txtLinkTechnology.Text.Trim();
                CTPPModel.ProgramDescription = txtDescription.Text.Trim();
                CTPPModel.ProgramDescriptionForMobile = txtDescriptionForMobile.Text.Trim();
                CTPPModel.ProgramGUID = new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]);
                //CTPPModel.ProgramName = txtProgramName.Text;
                CTPPModel.ProgramSubheading = txtProgramSubheading.Text.Trim();
                CTPPModel.ProgramURL = txtProgramURL.Text.Trim();
                CTPPModel.PromotionLink1 = txtPromotionLink1.Text.Trim();
                CTPPModel.PromotionLink2 = txtPromotionLink2.Text.Trim();
                CTPPModel.VideoLink = txtVideoLink.Text.Trim();
                CTPPModel.SubPriceLink = txtSubPriceLink.Text.Trim();
                CTPPModel.ForSideLink = txtForSideLink.Text.Trim();
                CTPPModel.FacebookLink = txtFacebookLink.Text.Trim();

                CTPPModel.FactHeader1 = txtFact1Header.Text;
                CTPPModel.FactContent1 = txtFact1Content.Text;
                CTPPModel.FactHeader2 = txtFact2Header.Text;
                CTPPModel.FactContent2 = txtFact2Content.Text;
                CTPPModel.FactHeader3 = txtFact3Header.Text;
                CTPPModel.FactContent3 = txtFact3Content.Text;
                CTPPModel.FactHeader4 = txtFact4Header.Text;
                CTPPModel.FactContent4 = txtFact4Content.Text;


                #region update promotion 1 pic
                CTPPModel.PromotionField1 = GenerateResourceModel(fileUpload1, ResourceTypeEnum.Logo, CTPPModel.PromotionField1);
                #endregion

                #region update promotion 2 pic
                CTPPModel.PromotionField2 = GenerateResourceModel(fileUpload2, ResourceTypeEnum.Logo, CTPPModel.PromotionField2);
                #endregion

                CTPPModel.ProgramPresenter = new ResourceModel();
                CTPPModel.ProgramPresenter.Extension = string.Empty;

                CTPPModel.BackDarkColor = txtDarkColor.Text.Trim();
                CTPPModel.BackLightColor = txtLightColor.Text.Trim();
                CTPPModel.ProgramSubheadColor = txtSubheadColor.Text.Trim();
                CTPPModel.ProgramDescriptionTitle = txtDescriptionTitle.Text.Trim();
                CTPPModel.ProgramDescriptionTitleForMobile = txtDescriptionTitleForMobile.Text.Trim();
                if (chbGooglePlus.Checked)
                {
                    CTPPModel.IsGooglePlus = 1;
                }
                else
                {
                    CTPPModel.IsGooglePlus = 0;
                }
                if (chbInactive.Checked)
                {
                    CTPPModel.InActive = 1;
                }
                else
                {
                    CTPPModel.InActive = 0;
                }
                if (chbRetake.Checked)
                {
                    CTPPModel.RetakeEnable = 1;
                }
                else
                {
                    CTPPModel.RetakeEnable = 0;
                }
                //IsNotShowOtherPrograms
                if (chbOtherProgramsInvisible.Checked)
                {
                    CTPPModel.IsNotShowOtherPrograms = true;
                }
                else
                {
                    CTPPModel.IsNotShowOtherPrograms = false;
                }

                #region IsNotShowStartButton
                ////IsNotShowStartButton
                //if (chbStartButtonInvisible.Checked)
                //{
                //    CTPPModel.IsNotShowStartButton = true;
                //}
                //else
                //{
                //    CTPPModel.IsNotShowStartButton = false;
                //} 
                #endregion

                //IsEnableSpecificReportAndHelpButtons
                if (chbSpecificReportAndHelp.Checked)
                {
                    CTPPModel.IsEnableSpecificReportAndHelpButtons = true;
                    CTPPModel.ReportButtonHeading = txtReportButtonHeading.Text;
                    CTPPModel.ReportButtonActual = txtReportButtonActual.Text;
                    CTPPModel.ReportButtonComplete = txtReportButtonComplete.Text;
                    CTPPModel.ReportButtonUntaken = txtReportButtonUntaken.Text;
                    CTPPModel.HelpButtonHeading = txtHelpButtonHeading.Text;
                    CTPPModel.HelpButtonActual = txtHelpButtonActual.Text;
                }
                else
                {
                    CTPPModel.IsEnableSpecificReportAndHelpButtons = false;
                    CTPPModel.ReportButtonHeading = Model.ReportButtonHeading;
                    CTPPModel.ReportButtonActual = Model.ReportButtonActual;
                    CTPPModel.ReportButtonComplete = Model.ReportButtonComplete;
                    CTPPModel.ReportButtonUntaken = Model.ReportButtonUntaken;
                    CTPPModel.HelpButtonHeading = Model.HelpButtonHeading;
                    CTPPModel.HelpButtonActual = Model.HelpButtonActual;
                }

                #region update CTPP LOGO pic
                CTPPModel.CTPPLogo = GenerateResourceModel(fileUpload3, ResourceTypeEnum.Logo, CTPPModel.CTPPLogo);
                #endregion
                #region update image for video
                CTPPModel.ImageForVideoLink = GenerateResourceModel(fileUpload4, ResourceTypeEnum.Logo, CTPPModel.ImageForVideoLink);
                #endregion

                #region update mobile bookmark
                CTPPModel.MobileBookmarkLink = GenerateResourceModel(fulMobileBookmark, ResourceTypeEnum.Logo, CTPPModel.MobileBookmarkLink);
                #endregion

                if (!string.IsNullOrEmpty(txtReportButtonAvailableTime.Text.Trim()))
                {
                    int availableTime = 24;
                    if (int.TryParse(txtReportButtonAvailableTime.Text.Trim(), out availableTime))
                    {
                        CTPPModel.ReportButtonAvailableTime = availableTime;
                    }
                    else
                    {
                        CTPPModel.ReportButtonAvailableTime = Model.ReportButtonAvailableTime;
                    }
                }

                CTPPModel.RemindSMS1Text = txtRemindSMS1Text.Text.Trim();
                CTPPModel.RemindSMS2Text = txtRemindSMS2Text.Text.Trim();

                int testHH = 0, testMM = 0;//just for try parse ,not for other use.
                if (int.TryParse(txtRemindSMS1TimeHour.Text, out testHH) && int.TryParse(txtRemindSMS1TimeMinute.Text, out testMM))
                {
                    CTPPModel.RemindSMS1TimeMinute = Convert.ToInt32(txtRemindSMS1TimeHour.Text) * 60 + Convert.ToInt32(txtRemindSMS1TimeMinute.Text);
                }
                else
                {
                    CTPPModel.RemindSMS1TimeMinute = Model.RemindSMS1TimeMinute;
                }
                if (int.TryParse(txtRemindSMS2TimeHour.Text, out testHH) && int.TryParse(txtRemindSMS2TimeMinute.Text, out testMM))
                {
                    CTPPModel.RemindSMS2TimeMinute = Convert.ToInt32(txtRemindSMS2TimeHour.Text) * 60 + Convert.ToInt32(txtRemindSMS2TimeMinute.Text);
                }
                else
                {
                    CTPPModel.RemindSMS2TimeMinute = Model.RemindSMS2TimeMinute;
                }
                //if 0, set null. Because if the user doesnot insert any value in the hh and mm blank, it will be convert to 0, 
                //but if the time is stored as 0,it means the reportSmsTime is 00:00(HH:MM) every day.
                //But its meaning is no report sms time, no report sms.
                //So Tell Bent ,the hh and mm can not be set 00:00. if set like this, will be regarded as no sms.
                if (CTPPModel.RemindSMS1TimeMinute == 0) CTPPModel.RemindSMS1TimeMinute = null;
                if (CTPPModel.RemindSMS2TimeMinute == 0) CTPPModel.RemindSMS2TimeMinute = null;

                Resolve<ICTPPService>().UpdateCTPP(CTPPModel, false);
                flag = true;
            }
            else
            {
                throw new InvalidOperationException("Can't find the CTPP Model.");
            }
            return flag;
        }

        private ResourceModel GenerateResourceModel(FileUpload fileUpload, ResourceTypeEnum resourceType, ResourceModel resourceModel)
        {
            Guid resourceGUID = Guid.NewGuid();
            string resourceFileType = "";
            string resourceName = "";
            if (fileUpload.HasFile)
            {
                resourceFileType = fileUpload.FileName.Substring(fileUpload.FileName.LastIndexOf("."));
                resourceName = resourceGUID.ToString() + resourceFileType;

                Resolve<IResourceService>().SaveResourceToAzureBlobStorage(fileUpload.FileContent, resourceGUID.ToString(), resourceName, resourceType.ToString(), Guid.Empty);
            }
            else
            {
                if (resourceModel != null && resourceModel.ID != Guid.Empty)
                {
                    resourceGUID = resourceModel.ID;
                }
                else
                {
                    resourceGUID = Guid.Empty;
                }
                resourceFileType = string.Empty;
                resourceName = resourceGUID.ToString() + resourceFileType;

            }
            return new ResourceModel
            {
                Extension = resourceFileType,
                ID = resourceGUID,
                Name = resourceName,
                NameOnServer = resourceName,
                Type = resourceType.ToString(),
            };
        }

        protected void btnSetHelpRelapse_Click(object sender, EventArgs e)
        {
            try
            {
                if (!UpdateWholePageWithoutSpecailItems()) return;
                else
                    Response.Redirect(string.Format("ManageRelapsePageSequence.aspx?{0}={1}&{2}={3}&{4}={5}&{6}={7}",
                        Constants.QUERYSTR_PROGRAM_GUID, Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID],
                        Constants.QUERYSTR_PROGRAM_PAGE, ProgramPage,
                        Constants.QUERYSTR_CTPP_GUID, CTPPGuid,
                        Constants.QUERYSTR_CTPP_TYPE, (int)CTPPRelapseEnum.HelpButtonRelapse));
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        protected void btnUnBindHelpRelapse_Click(object sender, EventArgs e)
        {
            if (Resolve<ICTPPService>().UnBindCTPPRelapse(new Guid(CTPPGuid), CTPPRelapseEnum.HelpButtonRelapse))
            {
                this.lblHelpRelapse.Text = string.Empty;
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "tip", "alert('Can not finish the delete operation successful.Please contact the developer if you retry again and failed.');", true);
            }
        }

        protected void btnSetReportRelapse_Click(object sender, EventArgs e)
        {
            try
            {
                if (!UpdateWholePageWithoutSpecailItems()) return;
                else
                    Response.Redirect(string.Format("ManageRelapsePageSequence.aspx?{0}={1}&{2}={3}&{4}={5}&{6}={7}",
                        Constants.QUERYSTR_PROGRAM_GUID, Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID],
                        Constants.QUERYSTR_PROGRAM_PAGE, ProgramPage,
                        Constants.QUERYSTR_CTPP_GUID, CTPPGuid,
                        Constants.QUERYSTR_CTPP_TYPE, (int)CTPPRelapseEnum.ReportButtonRelapse));
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        protected void btnUnBindReportRelapse_Click(object sender, EventArgs e)
        {
            if (Resolve<ICTPPService>().UnBindCTPPRelapse(new Guid(CTPPGuid), CTPPRelapseEnum.ReportButtonRelapse))
            {
                this.lblReportRelapse.Text = string.Empty;
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "tip", "alert('Can not finish the delete operation successful.Please contact the developer if you retry again and failed.');", true);
            }
        }
    }
}