using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Ethos.Utility;
using Ethos.DependencyInjection;
using System.Web.SessionState;
using ChangeTech.IDataRepository;
using ChangeTech.Models;
using ChangeTech.Contracts;

namespace ChangeTech.DeveloperWeb
{
    /// <summary>
    /// Summary description for AuthenticateLFAccount
    /// </summary>
    /// 
    [WebService(Namespace = "http://changetech.no/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class AuthenticateLFAccount : IHttpHandler, IRequiresSessionState
    {
        private const string VALIDATE_FAIL = "fail";
        private const string VALIDATE_SUCCESS = "success";
        private const string MARK_FORMAT = "ÅÅÅÅMMDD-9999";
        private const int AUTHENTICATE_SUCCESS = 1;
        private const int CODE_LENGTH = 4;
        public void ProcessRequest(HttpContext context)
        {
            string returnMessage = string.Empty;
            IContainerContext containerContext = ContainerManager.GetContainer("container");
            string validateCode = System.Web.HttpUtility.UrlDecode(context.Request.Params["validateCode"]);
            string userHostAddress = System.Web.HttpUtility.UrlDecode(context.Request.Params["userHostAddress"]);
            string userAgent = System.Web.HttpUtility.UrlDecode(context.Request.Params["userAgent"]);
            try
            {
                if (!string.IsNullOrEmpty(validateCode) && validateCode != MARK_FORMAT)
                {
                    if (context.Session[validateCode] == null)
                    {
                        if (AuthenticateAccount(validateCode))
                        {
                            //TODO: insert data(userHostAddress,userAgent,DateTime.Now)
                            context.Session[validateCode] = validateCode;
                            returnMessage = VALIDATE_SUCCESS;
                        }
                        else
                        {
                            returnMessage = VALIDATE_FAIL;
                        }
                    }
                    else
                    {

                        if (AuthenticateAccount(context.Session[validateCode].ToString()))
                        {
                            //TODO: Update Date.
                            returnMessage = VALIDATE_SUCCESS;
                        }
                        else
                        {
                            returnMessage = VALIDATE_FAIL;
                        }
                    }
                }
                else
                {
                    returnMessage = VALIDATE_FAIL;
                }
                InsertLogModel insertLogModel = new InsertLogModel
                {
                    ActivityLogType = (int)LogTypeEnum.LFAuthenticateSuccess,
                    Browser = HttpContext.Current.Request.UserAgent,
                    IP = HttpContext.Current.Request.UserHostAddress,
                    From = string.Empty,
                    Message = string.Format("ReturnMessage : {0} , PersonNumber: {1}", returnMessage, validateCode),
                    PageGuid = Guid.Empty,
                    PageSequenceGuid = Guid.Empty,
                    SessionGuid = Guid.Empty,
                    ProgramGuid = Guid.Empty,
                    UserGuid = Guid.Empty
                };
                containerContext.Resolve<IActivityLogService>().Insert(insertLogModel);
                context.Response.ContentType = "text/plain";
                context.Response.Write(returnMessage);
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                InsertLogModel insertLogModel = new InsertLogModel
                {
                    ActivityLogType = (int)LogTypeEnum.LFAuthenticateSuccess,
                    Browser = HttpContext.Current.Request.UserAgent,
                    IP = HttpContext.Current.Request.UserHostAddress,
                    From = string.Empty,
                    Message = string.Format("LFAuthenticateException(ProcessRequest method) : {0} , PersonNumber: {1}", ex, validateCode),
                    PageGuid = Guid.Empty,
                    PageSequenceGuid = Guid.Empty,
                    SessionGuid = Guid.Empty,
                    ProgramGuid = Guid.Empty,
                    UserGuid = Guid.Empty
                };
                containerContext.Resolve<IActivityLogService>().Insert(insertLogModel);
                context.Response.ContentType = "text/xml";
                context.Response.Write(VALIDATE_FAIL);
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        private bool AuthenticateAccount(string validateCode)
        {
            bool flag = false;
            IContainerContext containerContext = ContainerManager.GetContainer("container");
            try
            {
                if (validateCode.Length == CODE_LENGTH)
                {
                    HPOrderModel hpOrderModel = containerContext.Resolve<IHPOrderService>().GetHPOrderModelByCode(validateCode);
                    if (hpOrderModel != null)
                    {
                        //Every code should be valid to enter the LF-portal during the period of the HP Order and 4 weeks thereafter.
                        if (hpOrderModel.StartDate <= DateTime.UtcNow.Date && hpOrderModel.StopDate.AddDays(28) >= DateTime.UtcNow.Date)
                        {
                            flag = true;
                        }
                    }
                }
                else
                {
                    string requesterId = containerContext.Resolve<ISystemSettingRepository>().GetSettingValue("RequesterId");//CHANGETECH
                    string requesterPassword = containerContext.Resolve<ISystemSettingRepository>().GetSettingValue("RequesterPasswordByLF");//"kjxPwq7821"
                    string nationality = containerContext.Resolve<ISystemSettingRepository>().GetSettingValue("Nationality");//SE
                    string insuranceCompany = containerContext.Resolve<ISystemSettingRepository>().GetSettingValue("InsuranceCompany");//LF
                    Util.SetCertificatePolicy();
                    LFPortalService.PortalAutenticationServiceClient proxy = new LFPortalService.PortalAutenticationServiceClient();
                    LFPortalService.PortalResponse responseResult = proxy.autenticate(requesterId, requesterPassword, validateCode, nationality, "", insuranceCompany, "");
                    if (responseResult.autenticated == AUTHENTICATE_SUCCESS)
                    {
                        flag = true;
                        InsertLogModel insertLogModel = new InsertLogModel
                        {
                            ActivityLogType = (int)LogTypeEnum.LFAuthenticateSuccess,
                            Browser = HttpContext.Current.Request.UserAgent,
                            IP = HttpContext.Current.Request.UserHostAddress,
                            From = string.Empty,
                            Message = string.Format("LFAuthenticateMessage : {0} , PersonNumber : {1}", "LF-PersonNumber authenticate successful.", validateCode),
                            PageGuid = Guid.Empty,
                            PageSequenceGuid = Guid.Empty,
                            SessionGuid = Guid.Empty,
                            ProgramGuid = Guid.Empty,
                            UserGuid = Guid.Empty
                        };
                        containerContext.Resolve<IActivityLogService>().Insert(insertLogModel);
                    }
                    else
                    {
                        InsertLogModel insertLogModel = new InsertLogModel
                        {
                            ActivityLogType = (int)LogTypeEnum.LFAuthenticateFail,
                            Browser = HttpContext.Current.Request.UserAgent,
                            IP = HttpContext.Current.Request.UserHostAddress,
                            From = string.Empty,
                            Message = string.Format("LFAuthenticateMessage : {0} , PersonNumber : {1}", "LF-PersonNumber authenticate failed.", validateCode),
                            PageGuid = Guid.Empty,
                            PageSequenceGuid = Guid.Empty,
                            SessionGuid = Guid.Empty,
                            ProgramGuid = Guid.Empty,
                            UserGuid = Guid.Empty
                        };
                        containerContext.Resolve<IActivityLogService>().Insert(insertLogModel);
                    }
                }
            }
            catch (TimeoutException ex)
            {
                flag = true;
                InsertLogModel insertLogModel = new InsertLogModel
                {
                    ActivityLogType = (int)LogTypeEnum.LFAuthenticateTimeOut,
                    Browser = HttpContext.Current.Request.UserAgent,
                    IP = HttpContext.Current.Request.UserHostAddress,
                    From = string.Empty,
                    Message = string.Format("LFAuthenticateTimeoutException : {0} , PersonNumber : {1}", ex.Message,validateCode),
                    PageGuid = Guid.Empty,
                    PageSequenceGuid = Guid.Empty,
                    SessionGuid = Guid.Empty,
                    ProgramGuid = Guid.Empty,
                    UserGuid = Guid.Empty
                };
                containerContext.Resolve<IActivityLogService>().Insert(insertLogModel);
            }
            catch (Exception ex)
            {
                flag = true;
                InsertLogModel insertLogModel = new InsertLogModel
                {
                    ActivityLogType = (int)LogTypeEnum.LFAuthenticateException,
                    Browser = HttpContext.Current.Request.UserAgent,
                    IP = HttpContext.Current.Request.UserHostAddress,
                    From = string.Empty,
                    Message = string.Format("LFAuthenticateException : {0} , PersonNumber : {1}", ex.Message,validateCode),
                    PageGuid = Guid.Empty,
                    PageSequenceGuid = Guid.Empty,
                    SessionGuid = Guid.Empty,
                    ProgramGuid = Guid.Empty,
                    UserGuid = Guid.Empty
                };
                containerContext.Resolve<IActivityLogService>().Insert(insertLogModel);
            }

            return flag;
        }
    }


    public static class Util
    {
        ///　<summary>
        ///　Sets　the　cert　policy.
        ///　</summary>
        public static void SetCertificatePolicy()
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback
                       += RemoteCertificateValidate;
        }

        ///　<summary>
        ///　Remotes　the　certificate　validate.
        ///　</summary>
        private static bool RemoteCertificateValidate(
           object sender, System.Security.Cryptography.X509Certificates.X509Certificate cert,
            System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors error)
        {
            //　trust　any　certificate!!!
            System.Console.WriteLine("Warning,　trust　any　certificate");
            return true;
        }
    }
}