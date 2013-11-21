using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Ethos.Utility;
using System.Configuration;
using ChangeTech.Services;
using Ethos.DependencyInjection;
using ChangeTech.Models;
using ChangeTech.Contracts;

namespace ChangeTech.DeveloperWeb
{
    public partial class EditBrand :PageBase<BrandModel>
    {

        private string BrandGUID
        {
            get
            {
                return Request.QueryString[Constants.QUERYSTR_BRAND_GUID];
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString[Constants.QUERYSTR_BRAND_GUID]))
            {
                if (!IsPostBack)
                {
                    try
                    {
                        BindBrandModel();
                    }
                    catch (Exception ex)
                    {
                        LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                        throw ex;
                    }
                    ((HyperLink)Master.FindControl("menuLoginView").FindControl("hpBackLink")).NavigateUrl = "ManageBrand.aspx";
                }
            }
            else
            {
                Response.Redirect("InvalidUrl.aspx");
            }
        }

        #region Button Events

        protected void btnUpdateBrand_Click(object sender, EventArgs e)
        {
            Guid fileGuid = Guid.NewGuid();
            string fileType = "";
            string fileName = "";
            try
            {
                Guid brandGuid = new Guid(Request.QueryString[Constants.QUERYSTR_BRAND_GUID]);
                Model = Resolve<IBrandService>().GetBrandByGUID(brandGuid);

                if (fileUpload.HasFile)
                {
                    fileType = fileUpload.FileName.Substring(fileUpload.FileName.LastIndexOf("."));
                    fileName = fileGuid.ToString() + fileType;

                    Resolve<IResourceService>().SaveResourceToAzureBlobStorage(fileUpload.FileContent, fileGuid.ToString(), fileName, ResourceTypeEnum.Logo.ToString(), Guid.Empty);
                }
                else
                {
                    fileGuid = Model.BrandLogo.ID;
                    fileType = string.Empty;

                }
                BrandModel newModel = new BrandModel();
                newModel.BrandGUID = Model.BrandGUID;
                newModel.BrandName = txtBrandName.Text.Trim();
                newModel.BrandURL = txtBrandURL.Text.Trim();



                newModel.BrandLogo = new ResourceModel
                {
                    Extension = fileType,
                    ID = fileGuid,
                    Name = fileGuid + fileType,
                    NameOnServer = fileGuid + fileType,
                    Type = "Logo",
                };
                Resolve<IBrandService>().UpdateBrand(newModel);
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
            Response.Redirect(Request.Url.PathAndQuery);

        }

        protected void cancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("ManageBrand.aspx");
        }
        #endregion

        #region BindBrandModel

        private void BindBrandModel()
        {
            Guid brandGuid = new Guid(Request.QueryString[Constants.QUERYSTR_BRAND_GUID]);

            Model = Resolve<IBrandService>().GetBrandByGUID(brandGuid);

            txtBrandName.Text = Model.BrandName;
            txtBrandURL.Text = Model.BrandURL;

            if (Model.BrandLogo != null)
            {
                string accountName = Resolve<ISystemSettingService>().GetSettingValue("AzureStorageAccountName");
                string bolbPath = ServiceUtility.GetBlobPath(accountName);
                brandLogo.ImageUrl = bolbPath + BlobContainerTypeEnum.LogoContainer.ToString().ToLower() + "/" + Model.BrandLogo.Name;
            }
        }

        #endregion



    }
}