using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ChangeTech.Models;
using ChangeTech.Contracts;
using Ethos.DependencyInjection;
using System.IO;
using Ethos.Utility;

namespace ChangeTech.DeveloperWeb
{
    public partial class AddBrand : PageBase<BrandModel>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    ((HyperLink)Master.FindControl("menuLoginView").FindControl("hpBackLink")).NavigateUrl = "~/ManageBrand.aspx";
                }
                catch (Exception ex)
                {
                    LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                    throw ex;
                }
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            BrandModel brandModel = new BrandModel();

            try
            {
                if (fileUpload.HasFile)
                {
                    Guid fileGuid = Guid.NewGuid();
                    string fileType = fileUpload.FileName.Substring(fileUpload.FileName.LastIndexOf("."));
                    string fileName = fileGuid.ToString() + "." + fileType;

                    //fileUpload.SaveAs(Server.MapPath("ClientBin/ProgramImages/Logo/" + fileGuid + fileType));
                    // 2010-03-19: Chen Pu Replace with save to azure method instead of save to file system
                    //Resolve<IResourceService>().SaveResourceToAzureBlobStorage(fileUpload.FileContent, fileGuid.ToString(), fileUpload.FileName, "Logo", Guid.Empty);
                    Resolve<IResourceService>().SaveResourceToAzureBlobStorage(fileUpload.FileContent, fileGuid.ToString(), fileName, ResourceTypeEnum.Logo.ToString(), Guid.Empty);

                    brandModel.BrandLogo = new ResourceModel
                    {
                        Extension = fileType,
                        ID = fileGuid,
                        Name = fileGuid + fileType,
                        NameOnServer = fileGuid + fileType,
                        Type = "Logo",
                    };
                }



                if (!string.IsNullOrEmpty(txtBrandName.Text))
                {
                    brandModel.BrandName = txtBrandName.Text;
                }

                if (!string.IsNullOrEmpty(txtBrandURL.Text))
                {
                    brandModel.BrandURL = txtBrandURL.Text;
                }

                Resolve<IBrandService>().AddNewBrand(brandModel);
                Response.Redirect("ManageBrand.aspx");
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        protected void cancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("ManageBrand.aspx");
        }
    }
}