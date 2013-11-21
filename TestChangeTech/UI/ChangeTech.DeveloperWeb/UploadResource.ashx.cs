using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using ChangeTech.Models;
using Microsoft.WindowsAzure.StorageClient;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.ServiceRuntime;
using System.Collections.Specialized;
using System.Drawing.Imaging;
using Ethos.DependencyInjection;
using ChangeTech.Contracts;

namespace ChangeTech.DeveloperWeb
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://www.changetech.no/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class UploadResource : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.QueryString.Count > 0)
            {
                string fileGuid = context.Request.QueryString["FileGuid"];
                string fileName = HttpUtility.UrlDecode(context.Request.QueryString["FileName"]);
                string categoryGUID = context.Request.QueryString["CategoryGuid"];
                if (context.Request.QueryString["Type"].Equals(ResourceTypeEnum.Video.ToString()) ||
                    context.Request.QueryString["Type"].Equals(ResourceTypeEnum.Audio.ToString()) ||
                    context.Request.QueryString["Type"].Equals(ResourceTypeEnum.Document.ToString()) ||
                    context.Request.QueryString["Type"].Equals(ResourceTypeEnum.Image.ToString()))
                {
                    IContainerContext containerContext = ContainerManager.GetContainer("container");
                    containerContext.Resolve<IResourceService>().SaveResourceToAzureBlobStorage(context.Request.InputStream, fileGuid, fileName, context.Request.QueryString["Type"], new Guid(categoryGUID));
                }
                else
                {
                    throw new ArgumentException("Invalid resource type!");
                }
            }
            else
            {
                context.Response.ContentType = "text/plain";
                context.Response.Write("Welcome you come to ChangeTech, but you don't have access to this page.");
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
