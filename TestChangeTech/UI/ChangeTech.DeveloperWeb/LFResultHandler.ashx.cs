using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ethos.DependencyInjection;
using ChangeTech.Contracts;
using Ethos.Utility;

namespace ChangeTech.DeveloperWeb
{
    /// <summary>
    /// Summary description for LFResultHandler
    /// </summary>
    public class LFResultHandler : IHttpHandler
    {

         private const string SUCCESS = "success";
        private const string FAIL = "error";
        public void ProcessRequest(HttpContext context)
        {
            try
            {
                IContainerContext containerContext = ContainerManager.GetContainer("container");
                string parameterInfo = System.Web.HttpUtility.UrlDecode(context.Request.Params["parameterInfo"]);
                //ProgramUserGuid;PageGuid
                string[] userInfo = StringUtility.MD5Decrypt(parameterInfo, Constants.MD5_KEY).Split(';');
                Guid programUserGuid = new Guid(userInfo[0]);
                Guid pageGuid = new Guid(userInfo[1]);

                context.Response.ContentType = "text/plain";
                context.Response.Write(SUCCESS);
            }
            catch (Exception ex)
            {
                context.Response.ContentType = "text/plain";
                context.Response.Write(FAIL);
                LogUtility.LogUtilityIntance.LogMessage(string.Format("Method: {0},  Exception Message:{1}, Request URL: {2}", "LFResultHandler", ex, context.Request.Url.ToString()));
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