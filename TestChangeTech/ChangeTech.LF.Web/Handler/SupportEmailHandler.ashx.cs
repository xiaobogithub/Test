using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Text;
using System.IO;
using Ethos.Utility;

namespace ChangeTech.LF.Web
{
    /// <summary>
    /// Summary description for SupportEmailHandler
    /// </summary>
    public class SupportEmailHandler : IHttpHandler
    {
        private const string SENDEMAIL_FAIL = "error";
        public void ProcessRequest(HttpContext context)
        {
            try
            {
                string subject = System.Web.HttpUtility.UrlEncode(context.Request.Params["subject"]);
                string body = System.Web.HttpUtility.UrlEncode(context.Request.Params["body"]);
                string emailFromAddress = System.Web.HttpUtility.UrlEncode(context.Request.Params["emailFromAddress"]);
                string emailFromName = System.Web.HttpUtility.UrlEncode(context.Request.Params["emailFromName"]);
                //string requestUrl = "http://localhost:41265/LFSupportEmaileHandler.ashx";
                //string requestUrl = "http://0615a57f2f8a4ea29089014390a952f7.cloudapp.net/LFSupportEmaileHandler.ashx";
                //string requestUrl = "http://program.changetech.no/LFSupportEmaileHandler.ashx";
                string requestUrl = System.Configuration.ConfigurationManager.AppSettings["SupportEmailRequestUrlToLive"];
            
                HttpRequest original = context.Request;
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(requestUrl);
                request.UserAgent = original.UserAgent;
                request.AllowWriteStreamBuffering = true;
                request.Method = "POST";
                //request.AllowAutoRedirect = false;
                //request.ServicePoint.Expect100Continue = false;
                //request.KeepAlive = true;

                string post = "subject=" + subject + "&body=" + body + "&emailFromAddress=" + emailFromAddress + "&emailFromName=" + emailFromName;
                byte[] data = Encoding.ASCII.GetBytes(post);
                request.ContentLength = data.Length;
                request.ContentType = "application/x-www-form-urlencoded";

                Stream dataStream = request.GetRequestStream(); //Here is the WebException thrown
                dataStream.Write(data, 0, data.Length);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();

                context.Response.ContentType = "text/plain";
                context.Response.Write(responseFromServer);
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