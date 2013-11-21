using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Ethos.Utility;
using System.Web.SessionState;

namespace ChangeTech.LF.Web.Handler
{
    /// <summary>
    /// Summary description for GoWebSessionHandler
    /// </summary>
    [WebService(Namespace = "http://changetech.no/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class GetGoWebSessionHandler : IHttpHandler, IRequiresSessionState
    {
        private const string GOWEBSESSION_SAVE_SUCCESS = "success";
        private const string GOWEBSESSION_SAVE_FAIL = "fail";
        private string goWebSession = string.Empty;
        public void ProcessRequest(HttpContext context)
        {
            try
            {
                if (!string.IsNullOrEmpty(LFContextService.GoWebEncryptUrl))
                {
                    goWebSession = LFContextService.GoWebEncryptUrl;
                }
                context.Response.ContentType = "text/plain";
                context.Response.Write(goWebSession);
            }
            catch (Exception ex)
            {
                context.Response.ContentType = "text/plain";
                context.Response.Write(GOWEBSESSION_SAVE_FAIL);
                LogUtility.LogUtilityIntance.LogMessage(string.Format("Method: {0},  Exception Message:{1}, Request URL: {2}", "GoWebSessionHandler", ex, context.Request.Url.ToString()));
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