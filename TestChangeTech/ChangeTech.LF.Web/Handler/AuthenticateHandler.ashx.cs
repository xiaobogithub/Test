﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Net;
using System.IO;
using System.Text;
using System.Web.SessionState;
using Ethos.Utility;

namespace ChangeTech.LF.Web
{
    /// <summary>
    /// Summary description for AuthenticateHandler
    /// </summary>
    [WebService(Namespace = "http://changetech.no/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class AuthenticateHandler : IHttpHandler, IRequiresSessionState
    {
        private static CookieContainer cookieContainer = new CookieContainer();
        private const string AUTHENTICATE_FAIL = "fail";
        private const string MARK_FORMAT = "ÅÅÅÅMMDD-9999";
        public void ProcessRequest(HttpContext context)
        {
            try
            {
                //string requestUrl = "http://localhost:41265/AuthenticateLFAccount.ashx";
                //string requestUrl = "http://e40499ee6dda48679e86d8e8d89ed0d2.cloudapp.net/AuthenticateLFAccount.ashx";
                //string requestUrl = "http://program.changetech.no/AuthenticateLFAccount.ashx";
                string requestUrl = System.Configuration.ConfigurationManager.AppSettings["RequestUrlToLive"];
                string validateCode = context.Request.Params["validateCode"];
                if (validateCode.Contains("-") && validateCode != MARK_FORMAT)
                {
                    validateCode = validateCode.Replace("-", "");
                }
                if (string.IsNullOrEmpty(LFContextService.LFAccount))
                {
                    if (!string.IsNullOrEmpty(validateCode) && validateCode != MARK_FORMAT)
                    {
                        LFContextService.LFAccount = validateCode;
                    }
                }

                //Get CurrentSession timeout.
                //TimeSpan SessTimeOut = new TimeSpan(0, 0, System.Web.HttpContext.Current.Session.Timeout, 0, 0); 
                HttpRequest original = context.Request;
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(requestUrl);
                request.UserAgent = original.UserAgent;
                request.AllowWriteStreamBuffering = true;
                request.Method = "POST";
                request.CookieContainer = cookieContainer;

                //request.AllowAutoRedirect = false;
                //request.ServicePoint.Expect100Continue = false;
                //request.KeepAlive = true;
                string validateCodeByEncode = System.Web.HttpUtility.UrlEncode(LFContextService.LFAccount);
                string userHostAddressByEncode = System.Web.HttpUtility.UrlEncode(context.Request.UserHostAddress);
                string userAgentByEncode = System.Web.HttpUtility.UrlEncode(context.Request.UserAgent);
                string post = "validateCode=" + validateCodeByEncode + "&userAgent=" + userAgentByEncode + "&userHostAddress=" + userHostAddressByEncode;
                byte[] data = Encoding.ASCII.GetBytes(post);
                request.ContentLength = data.Length;
                request.ContentType = "application/x-www-form-urlencoded";

                Stream dataStream = request.GetRequestStream(); //Here is the WebException thrown
                dataStream.Write(data, 0, data.Length);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                string cookieHeader = request.CookieContainer.GetCookieHeader(new Uri(requestUrl));
                cookieContainer.SetCookies(new Uri(requestUrl), cookieHeader);

                dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();
                if (responseFromServer == AUTHENTICATE_FAIL)
                {
                    LFContextService.ClearLFAccount();
                }
                context.Response.ContentType = "text/plain";
                context.Response.Write(responseFromServer);
            }
            catch (Exception ex)
            {
                context.Response.ContentType = "text/plain";
                context.Response.Write(AUTHENTICATE_FAIL);
                LogUtility.LogUtilityIntance.LogMessage(string.Format("Method: {0},  Exception Message:{1}, Request URL: {2}", "AuthenticateHandler", ex, context.Request.Url.ToString()));
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