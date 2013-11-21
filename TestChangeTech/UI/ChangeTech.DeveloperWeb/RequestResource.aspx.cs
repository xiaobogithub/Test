using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Ethos.DependencyInjection;
using ChangeTech.Models;
using ChangeTech.Contracts;
using System.Net;
using Microsoft.WindowsAzure.StorageClient;
using Microsoft.WindowsAzure;
using ChangeTech.IDataRepository;
using Ethos.Utility;

namespace ChangeTech.DeveloperWeb
{
    public partial class RequestResource : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(Request.QueryString["media"]) && !string.IsNullOrEmpty(Request.QueryString["target"]))
            {
                string webRoot = "http://changetechstorage.blob.core.windows.net/";//System.Configuration.ConfigurationManager.AppSettings["BlobPath"];
                string ResourceAddress = string.Empty;
                switch(Request.QueryString["target"])
                {
                    //case "logo":

                    //    ResourceAddress = webRoot + "logocontainer/" + Request.QueryString["media"];

                    //    //FileStream fs = new FileStream(ResourceAddress,op
                    //    break;
                    case "Image":
                        ResourceAddress = webRoot + "originalimagecontainer/" + Request.QueryString["media"];
                        break;
                    case "Video":
                        ResourceAddress = webRoot + "videocontainer/" + Request.QueryString["media"];
                        break;
                    case "Audio":
                        ResourceAddress = webRoot + "audiocontainer/" + Request.QueryString["media"];
                        break;
                    case "Document":
                        ResourceAddress = webRoot + "documentcontainer/" + Request.QueryString["media"];
                        break;
                }
                FileDownload(ResourceAddress);
            }
        }

        private void FileDownload(string fullFileName)
        {
            IContainerContext containerContext = ContainerManager.GetContainer("container");
            CloudBlob blob = null;
            if (Request.QueryString["target"].Equals("Image"))
            {
                blob = GetContainer(ChangeTech.Models.BlobContainerTypeEnum.OriginalImageContainer).GetBlobReference(fullFileName);
            }
            else
            {
                blob = GetContainer(ChangeTech.Models.BlobContainerTypeEnum.AudioContainer).GetBlobReference(fullFileName);
            }
            string[] strType = fullFileName.Split('.');
            string fileExtentsion = "." + strType.Last();
            Response.ContentType = FileUtility.GetMIMEType(fileExtentsion);
            if (!Request.QueryString["target"].Equals("Image"))
            {
                if (Request.QueryString["name"] == null)
                    Request.QueryString["name"] = string.Empty;
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + Request.QueryString["name"].Replace(" ", ""));
            }
            blob.DownloadToStream(Response.OutputStream);
            Response.End();
        }

        private CloudBlobContainer GetContainer(BlobContainerTypeEnum containerType)
        {

            IContainerContext containerContext = ContainerManager.GetContainer("container");
            string accountName = containerContext.Resolve<ISystemSettingRepository>().GetSettingValue("AzureStorageAccountName");
            string accountKey = containerContext.Resolve<ISystemSettingRepository>().GetSettingValue("AzureStorageAccountKey");
            StorageCredentialsAccountAndKey securityKey = new StorageCredentialsAccountAndKey(accountName,
                accountKey);
            CloudStorageAccount account = new CloudStorageAccount(securityKey, false);
            var client = account.CreateCloudBlobClient();
            return client.GetContainerReference(containerType.ToString().ToLower());
        }
    }
}
