using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Ethos.DependencyInjection;
using ChangeTech.Contracts;
using ChangeTech.Models;
using Ethos.Utility;

namespace ChangeTech.DeveloperWeb
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class SMSKickStart : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            IContainerContext containerContext = ContainerManager.GetContainer("container");
            // For example: CHANGETECH KICK, the first part is ChangeTech, the second part is program short name
            string prefixAndProgram = context.Request.QueryString[Constants.QUERYSTR_PROGRAM_SHORT_NAME];
            string userMobile = context.Request.QueryString[Constants.QUERYSTR_USER_MOBILE];
            string[] temp = prefixAndProgram.Split(' ');
            string programShortName = prefixAndProgram;
            if (temp.Length == 2)
            {
                programShortName = temp[1];
            }

            try
            {
                if (!string.IsNullOrEmpty(programShortName) && !string.IsNullOrEmpty(userMobile))
                {
                    SMSRecordModel smsRecordModel = new SMSRecordModel
                    {
                        UserMobile = userMobile,
                        ProgramShortName = prefixAndProgram
                    };
                    containerContext.Resolve<IShortMessageService>().AddSMSRecord(smsRecordModel);
                    containerContext.Resolve<IProgramUserService>().ActiveSMSKickStart(programShortName, userMobile);

                    // add log
                    InsertLogModel model = new InsertLogModel
                    {
                        ActivityLogType = (int)LogTypeEnum.UpdateProgramUser,
                        Browser = context.Request.UserAgent,
                        IP = context.Request.UserHostAddress,
                        Message = "ActiveSMSKickStart Successful .",
                        PageGuid = Guid.Empty,
                        ProgramGuid = Guid.Empty,
                        SessionGuid = Guid.Empty,
                        UserGuid = Guid.Empty,
                        PageSequenceGuid = Guid.Empty
                    };
                    containerContext.Resolve<IActivityLogService>().Insert(model);
                    context.Response.ContentType = "text/plain";
                    context.Response.Write("Hello World");
                }
                else
                {
                    // add log
                    InsertLogModel model = new InsertLogModel
                    {
                        ActivityLogType = (int)LogTypeEnum.UpdateProgramUser,
                        Browser = context.Request.UserAgent,
                        IP = context.Request.UserHostAddress,
                        Message = string.Format("ActiveSMSKickStart's parameters incorrect. Program : {0} , Mobile : {1}.", prefixAndProgram, userMobile),
                        PageGuid = Guid.Empty,
                        ProgramGuid = Guid.Empty,
                        SessionGuid = Guid.Empty,
                        UserGuid = Guid.Empty,
                        PageSequenceGuid = Guid.Empty
                    };
                    containerContext.Resolve<IActivityLogService>().Insert(model);
                    context.Response.ContentType = "text/plain";
                    context.Response.Write("Fail");
                }
            }
            catch (NullReferenceException nullReferenceException)
            {
                // add log
                InsertLogModel logModel = new InsertLogModel
                {
                    ActivityLogType = (int)LogTypeEnum.UpdateProgramUser,
                    Browser = context.Request.UserAgent,
                    IP = context.Request.UserHostAddress,
                    Message = string.Format("ActiveSMSKickStart Failed .nullReferenceException : {0}", nullReferenceException.Message),
                    ProgramGuid = Guid.Empty,
                    SessionGuid = Guid.Empty,
                    UserGuid = Guid.Empty
                };
                containerContext.Resolve<IActivityLogService>().Insert(logModel);
                context.Response.ContentType = "text/plain";
                context.Response.Write("Fail");
                LogUtility.LogUtilityIntance.LogMessage(string.Format("Method: {0}, Request URL: {1}, Exception Message:{2},", "SMSKickStart", context.Request.Url.ToString(), nullReferenceException.Message));
            }
            catch (Exception ex)
            {
                // add log
                InsertLogModel logModel = new InsertLogModel
                {
                    ActivityLogType = (int)LogTypeEnum.UpdateProgramUser,
                    Browser = context.Request.UserAgent,
                    IP = context.Request.UserHostAddress,
                    Message = string.Format("ActiveSMSKickStart Failed. Exception : {1}.", ex.Message),
                    PageGuid = Guid.Empty,
                    ProgramGuid = Guid.Empty,
                    SessionGuid = Guid.Empty,
                    UserGuid = Guid.Empty,
                    PageSequenceGuid = Guid.Empty
                };
                containerContext.Resolve<IActivityLogService>().Insert(logModel);
                context.Response.ContentType = "text/plain";
                context.Response.Write("Fail");
                LogUtility.LogUtilityIntance.LogMessage(string.Format("Method: {0}, Request URL: {1}, Exception Message:{2},", "SMSKickStart", context.Request.Url.ToString(), ex.Message));
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
