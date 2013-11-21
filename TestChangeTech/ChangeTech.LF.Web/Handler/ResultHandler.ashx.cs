using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using Ethos.Utility;
using System.Web.Script.Serialization;
using System.Web.SessionState;

namespace ChangeTech.LF.Web.Handler
{
    /// <summary>
    /// Summary description for ResultHandler
    /// </summary>
    public class ResultHandler : IHttpHandler, IRequiresSessionState
    {
        private const string REQUESTRESULT_FAIL = "fail";
        private const string GOWEB = "GoWeb";
        public void ProcessRequest(HttpContext context)
        {
            try
            {
                string encryptStr = System.Web.HttpUtility.UrlEncode(context.Request.Params["parameterInfo"]);
                string encryptFullUrl = context.Request.Params["encryptUrl"];
                //Save GoWebUrl to Session
                if (string.IsNullOrEmpty(LFContextService.GoWebEncryptUrl))
                {
                    //http://localhost:64990/minhalsoprofil.html?GoWeb=...
                    if (!string.IsNullOrEmpty(encryptFullUrl) && encryptFullUrl.Contains(GOWEB))
                    {
                        LFContextService.GoWebEncryptUrl = encryptFullUrl;
                    }
                }

                if (!string.IsNullOrEmpty(encryptFullUrl) && encryptFullUrl.Contains(GOWEB))
                {
                    //string requestUrl = "http://localhost:41265/DecryptUrlHandler.ashx";
                    //string requestUrl = "http://0615a57f2f8a4ea29089014390a952f7.cloudapp.net/DecryptUrlHandler.ashx";
                    //string requestUrl = "http://program.changetech.no/DecryptUrlHandler.ashx";
                    string requestUrl = System.Configuration.ConfigurationManager.AppSettings["ResultLineRequestUrlToLive"];

                    HttpRequest original = context.Request;
                    HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(requestUrl);
                    request.UserAgent = original.UserAgent;
                    request.AllowWriteStreamBuffering = true;
                    request.Method = "POST";

                    string post = "encryptStr=" + encryptStr;
                    byte[] data = System.Text.Encoding.ASCII.GetBytes(post);
                    request.ContentLength = data.Length;
                    request.ContentType = "application/x-www-form-urlencoded";

                    Stream dataStream = request.GetRequestStream(); //Here is the WebException thrown
                    dataStream.Write(data, 0, data.Length);
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    dataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(dataStream);
                    object responseFromServer = reader.ReadToEnd();

                    context.Response.ContentType = "text/javascript";
                    context.Response.Cache.SetNoStore();
                    context.Response.Write(new JavaScriptSerializer().Serialize(responseFromServer));
                }
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogMessage(string.Format("Method: {0},  Exception Message:{1}, Request URL: {2}", "ResultHandler", ex, context.Request.Url.ToString()));
                context.Response.ContentType = "text/plain";
                context.Response.Write(REQUESTRESULT_FAIL);
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