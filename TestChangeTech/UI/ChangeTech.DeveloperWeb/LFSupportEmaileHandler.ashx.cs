using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ethos.DependencyInjection;
using ChangeTech.Services;
using Ethos.EmailModule.EmailService;
using System.Net.Mail;
using ChangeTech.Models;
using ChangeTech.Contracts;
using Ethos.Utility;

namespace ChangeTech.DeveloperWeb
{
    /// <summary>
    /// Summary description for LFSupportEmaileHandler
    /// </summary>
    public class LFSupportEmaileHandler : IHttpHandler
    {
        private const string SENDEMAIL_SUCCESS = "success";
        private const string SENDEMAIL_FAIL = "error";
        public void ProcessRequest(HttpContext context)
        {
            try
            {
                IContainerContext containerContext = ContainerManager.GetContainer("container");
                string subject = System.Web.HttpUtility.UrlDecode(context.Request.Params["subject"]);
                string body = System.Web.HttpUtility.UrlDecode(context.Request.Params["body"]);
                string emailFromAddress = System.Web.HttpUtility.UrlDecode(context.Request.Params["emailFromAddress"]);
                string emailFromName = System.Web.HttpUtility.UrlDecode(context.Request.Params["emailFromName"]);
                string emailTo = containerContext.Resolve<ISystemSettingService>().GetSettingValue("LFEmailToAddress");

                LFSupportEmailModel supportEmailModel = new LFSupportEmailModel
                {
                    Subject = subject,
                    Body = body,
                    EmailTo = emailTo,
                    EmailFromAddress = emailFromAddress,
                    EmailFromName = emailFromName
                };
                containerContext.Resolve<IEmailService>().SendLFSupportEmail(supportEmailModel);
                context.Response.ContentType = "text/plain";
                context.Response.Write(SENDEMAIL_SUCCESS);
            }
            catch (Exception ex)
            {
                context.Response.ContentType = "text/plain";
                context.Response.Write(SENDEMAIL_FAIL);
                LogUtility.LogUtilityIntance.LogMessage(string.Format("Method: {0},  Exception Message:{1}, Request URL: {2}", "SupportEmailHandler",  ex, context.Request.Url.ToString()));
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