using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Ethos.DependencyInjection;
using ChangeTech.Contracts;
using ChangeTech.Models;
using System.IO;
using Ethos.Utility;
using System.Xml;

namespace ChangeTech.DeveloperWeb
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://changetech.ethostech.no/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class AuthenticateUser : IHttpHandler
    {
        public const int ENDUSER_LOGIN_STATUS = 0;
        public const int USER_GUID = 1;
        public const int DAY = 3;
        public const int USER_NAME = 0;
        public const int PASSWORD = 1;
        public const int TASK_TYPE = 2;
        public const int TASK_CONTENT = 3;
        public const int MIN_PARAMETER_COUNT_IF_HAVE_TASK = 3; 
        public void ProcessRequest(HttpContext context)
        {
            try
            {
                IContainerContext containerContext = ContainerManager.GetContainer("container");
                string programMode = context.Request.QueryString["Mode"];
                string ctpp = context.Request.QueryString["CTPP"];
                string programGuid = context.Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID];

                string returnMessage = string.Empty;
                if(string.IsNullOrEmpty(programGuid) && !string.IsNullOrEmpty(context.Request.QueryString[Constants.QUERYSTR_PROGRAM_CODE]))
                {
                    programGuid = containerContext.Resolve<IProgramService>().GetProgramGUIDByProgramCode(context.Request.QueryString[Constants.QUERYSTR_PROGRAM_CODE]).ToString();
                }

                string security = context.Request.QueryString[Constants.QUERYSTR_SECURITY];
                string hpSecurity = context.Request.QueryString[Constants.QUERYSTR_HP_SECURITY];
                if (security != null)
                    security = security.Replace("#", "");

                Guid languageGuid = containerContext.Resolve<IProgramLanguageService>().GetLanguageOfProgramByProgramGUID(new Guid(programGuid));
                Guid sessionGuid = Guid.Empty;
                if (ctpp != null) {
                    //Get XML 
                    returnMessage = Convert.ToInt32(ReturnMessageCodeEnum.SuccessCode).ToString() + ";" + containerContext.Resolve<ICTPPService>().GetCTPPModelAsXML(new Guid(programGuid));
                }
                else
                {
                    if (programMode == "Live")
                    {
                        if (!string.IsNullOrEmpty(security))
                        {
                            UserTaskModel userTaskModel = GenerateUserTaskModel(security);
                            returnMessage = ValidateUser(userTaskModel, programGuid, languageGuid, context);
                        }
                        else
                        {
                            //Get XML of login page
                            returnMessage = Convert.ToInt32(ReturnMessageCodeEnum.SuccessCode).ToString() + ";" + containerContext.Resolve<IProgramAccessoryService>().GetProgramAccessoryModelAsXML(new Guid(programGuid), languageGuid);
                        }
                    }
                    // Start screening (Day0)
                    else if (programMode == "Trial")
                    {
                        if (!string.IsNullOrEmpty(hpSecurity))
                        {
                            //UserName="";Password="";
                            UserTaskModel userTaskModel = GenerateUserTaskModel(hpSecurity);
                            returnMessage = ValidateUser(userTaskModel, programGuid, languageGuid, context);
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(security))
                            {
                                returnMessage = Convert.ToInt32(ReturnMessageCodeEnum.SuccessCode).ToString() + ";" + StartScreening(programGuid, languageGuid, context);
                            }
                            else
                            {
                                //UserName="";Password="";
                                UserTaskModel userTaskModel = GenerateUserTaskModel(security);
                                returnMessage = ValidateUser(userTaskModel, programGuid, languageGuid, context);
                            }
                        }
                    }
                    //}
                    //else
                    //{
                    //    // for study...random function
                    //    returnMessage = RandomStudy(context);
                    //}
                }
                context.Response.ContentType = "text/plain";
                if (!context.Request.UserAgent.Contains("MSIE"))
                {
                    context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    context.Response.Cache.SetNoStore();
                    context.Response.Expires = 0;
                    context.Response.Buffer = true;
                    context.Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
                    context.Response.AddHeader("pragma", "no-cache");
                    context.Response.CacheControl = "no-cache";
                }
                context.Response.Write(returnMessage);
            }
            catch(Exception ex)
            {
                LogUtility.LogUtilityIntance.LogMessage(string.Format("Method: {0}, RequestURL:{1}, Exception: {2}", System.Reflection.MethodBase.GetCurrentMethod().Name, context.Request.Url.ToStringWithoutPort(), ex));
                throw ex;
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        #region private methods

        private string ReturnMessage_HasOrderLicence(UserTaskModel userTaskModel , string programGuid, Guid languageGuid, HttpContext context)
        {
            string returnMessage = string.Empty;
            IContainerContext containerContext = ContainerManager.GetContainer("container");
            ValidateOrderLicenceResponseModel responseModel = containerContext.Resolve<IOrderLicenceService>().ValidateOrderLicence(new Guid(userTaskModel.TaskContent), new Guid(programGuid));
            switch (responseModel.Result)
            {
                case ResultEnum.Success:
                    returnMessage = Convert.ToInt32(ReturnMessageCodeEnum.SuccessCode).ToString() + ";" + StartScreening(programGuid, languageGuid, context);
                    break;
                case ResultEnum.Error:
                    returnMessage = Convert.ToInt32(ReturnMessageCodeEnum.ErrorCode).ToString()+";" +containerContext.Resolve<ITipMessageService>().GetTipMessageText(new Guid(programGuid), responseModel.LoginFailedType.ToString());
                    #region Judge Program IsCTPPEnable
                    //ProgramModel programModel = containerContext.Resolve<IProgramService>().GetProgramByGUID(new Guid(programGuid));
                    //if (programModel.IsCTPPEnable)
                    //{
                    //    returnMessage = Convert.ToInt32(ReturnMessageCodeEnum.ErrorCode).ToString()+";" + responseModel.LoginFailedType;
                    //}
                    //else
                    //{
                    //    returnMessage = "0" + "ErrorMessage";//containerContext.Resolve<ITipMessageService>().GetTipMessageText(new Guid(programGuid), responseModel.LoginFailedType.ToString());
                    //} 
                    #endregion
                    break;
            }

            return returnMessage;
        }

        private string ReturnMessage_HasHPOrderLicence(UserTaskModel userTaskModel, string programGuid, Guid languageGuid, HttpContext context)
        {
            string returnMessage = string.Empty;
            IContainerContext containerContext = ContainerManager.GetContainer("container");
            ValidateOrderLicenceResponseModel responseModel = containerContext.Resolve<IHPOrderLicenceService>().ValidateHPOrderLicence(new Guid(userTaskModel.TaskContent), new Guid(programGuid));
            switch (responseModel.Result)
            {
                case ResultEnum.Success:
                    returnMessage = Convert.ToInt32(ReturnMessageCodeEnum.SuccessCode).ToString() + ";" + StartScreening(programGuid, languageGuid, context);
                    break;
                case ResultEnum.Error:
                    returnMessage = Convert.ToInt32(ReturnMessageCodeEnum.ErrorCode).ToString() + ";" + containerContext.Resolve<ITipMessageService>().GetTipMessageText(new Guid(programGuid), responseModel.LoginFailedType.ToString());
                    #region Judge Program IsCTPPEnable
                    //ProgramModel programModel = containerContext.Resolve<IProgramService>().GetProgramByGUID(new Guid(programGuid));
                    //if (programModel.IsCTPPEnable)
                    //{
                    //    returnMessage = Convert.ToInt32(ReturnMessageCodeEnum.ErrorCode).ToString()+";" + responseModel.LoginFailedType;
                    //}
                    //else
                    //{
                    //    returnMessage = "0" + "ErrorMessage";//containerContext.Resolve<ITipMessageService>().GetTipMessageText(new Guid(programGuid), responseModel.LoginFailedType.ToString());
                    //} 
                    #endregion
                    break;
            }

            return returnMessage;
        }

        private string RandomStudy(HttpContext context)
        {
            IContainerContext containerContext = ContainerManager.GetContainer("container");

            string returnMessage = string.Empty;
            Guid userGuid = new Guid(context.Request.QueryString[Constants.QUERYSTR_USER_GUID]);
            Guid programGuid = new Guid(context.Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]);
            Guid sessionGuid = containerContext.Resolve<ISessionService>().GetSessionGuidByProgarmAndDay(programGuid, 0);
            Guid languageGuid = containerContext.Resolve<IProgramLanguageService>().GetLanguageOfProgramByProgramGUID(programGuid);

            returnMessage = containerContext.Resolve<ISessionService>().GetLiveSessionModelAsXML(userGuid, programGuid, languageGuid, 0);
            returnMessage = containerContext.Resolve<IXMLService>().PraseGraphPage(returnMessage, userGuid, sessionGuid, languageGuid, false);
            returnMessage = containerContext.Resolve<IXMLService>().ParseBeforeAfterShowExpression(returnMessage, sessionGuid, "Session", programGuid.ToString(), userGuid.ToString());
            returnMessage = containerContext.Resolve<IXMLService>().ParseTimePickerQuestion(returnMessage, languageGuid);

            return returnMessage;

        }

        private string ValidateUser(UserTaskModel userTaskModel, string programGuid, Guid languageGuid, HttpContext context)
        {
            string returnMessage = string.Empty;
            IContainerContext containerContext = ContainerManager.GetContainer("container");
            Guid sessionGuid = Guid.Empty;

            switch (userTaskModel.TaskType)
            {
                case UserTaskTypeEnum.Login:
                    returnMessage = ReturnMessage_Login(userTaskModel, programGuid, languageGuid, context, returnMessage, containerContext);
                    break;
                case UserTaskTypeEnum.TakeSession:
                    returnMessage = ReturnMessage_TakeSession(userTaskModel, programGuid, languageGuid, context, returnMessage, containerContext);
                    break;
                case UserTaskTypeEnum.RetakeSession:
                    returnMessage = ReturnMessage_RetakeSession(userTaskModel, programGuid, languageGuid, context, returnMessage, containerContext);
                    break;
                case UserTaskTypeEnum.HelpInCTPP:
                    returnMessage = ReturnMessage_HelpInCTPP(userTaskModel, programGuid, languageGuid, returnMessage, containerContext);
                    break;
                case UserTaskTypeEnum.ReportInCTPP:
                    returnMessage = ReturnMessage_ReportInCTPP(userTaskModel, programGuid, languageGuid, returnMessage, containerContext);
                    break;
                case UserTaskTypeEnum.RegisteOfOrderSystem:
                    if (!string.IsNullOrEmpty(userTaskModel.TaskContent))
                    {
                        returnMessage = ReturnMessage_HasOrderLicence(userTaskModel, programGuid, languageGuid, context);
                    }   
                    break;
                case UserTaskTypeEnum.RegisteOfHPOrderSystem:
                    if (!string.IsNullOrEmpty(userTaskModel.TaskContent))
                    {
                        returnMessage = ReturnMessage_HasHPOrderLicence(userTaskModel, programGuid, languageGuid, context);
                    }
                    break;
            }

            return returnMessage;
        }

        private static string ReturnMessage_ReportInCTPP(UserTaskModel userTaskModel, string programGuid, Guid languageGuid, string returnMessage, IContainerContext containerContext)
        {
            //TaskContent: PageSequenceGuid
            if (containerContext.Resolve<IUserService>().IsValidUser(userTaskModel.UserName, userTaskModel.PassWord, new Guid(programGuid)))
            {
                UserModel userModel = containerContext.Resolve<IUserService>().GetUserByUserName(userTaskModel.UserName, new Guid(programGuid));
                string relapseModelXML = containerContext.Resolve<IPageSequenceService>().GetRelapseModelAsXML(languageGuid, new Guid(programGuid), new Guid(userTaskModel.TaskContent), userModel.UserGuid);
                Guid sessionGuid = containerContext.Resolve<ISessionService>().GetFirstSessionGUID(new Guid(programGuid));
                relapseModelXML = containerContext.Resolve<IXMLService>().PraseGraphPage(relapseModelXML, userModel.UserGuid, sessionGuid, languageGuid, false);
                relapseModelXML = containerContext.Resolve<IXMLService>().ParseBeforeAfterShowExpression(relapseModelXML, sessionGuid, "Session", programGuid.ToString(), userModel.UserGuid.ToString());
                relapseModelXML = containerContext.Resolve<IXMLService>().ParseTimePickerQuestion(relapseModelXML, languageGuid);
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(relapseModelXML);
                //Create an attribute.
                XmlAttribute attr = xmlDoc.CreateAttribute(Constants.FLASHXMLATTR_IS_REPORT_IN_CTPP);
                attr.Value = "true";

                //Add the new node to the document. 
                xmlDoc.DocumentElement.SetAttributeNode(attr);

                returnMessage = Convert.ToInt32(ReturnMessageCodeEnum.SuccessCode).ToString() + ";" + xmlDoc.InnerXml;
            }
            else
            {
                // Login fail, show login page to let user login
                returnMessage = Convert.ToInt32(ReturnMessageCodeEnum.SuccessCode).ToString() + ";" + containerContext.Resolve<IProgramAccessoryService>().GetProgramAccessoryModelAsXML(new Guid(programGuid), languageGuid);
            }
            return returnMessage;
        }

        private static string ReturnMessage_HelpInCTPP(UserTaskModel userTaskModel, string programGuid, Guid languageGuid, string returnMessage, IContainerContext containerContext)
        {
            //TaskContent: PageSequenceGuid
            if (containerContext.Resolve<IUserService>().IsValidUser(userTaskModel.UserName, userTaskModel.PassWord, new Guid(programGuid)))
            {
                UserModel userModel = containerContext.Resolve<IUserService>().GetUserByUserName(userTaskModel.UserName, new Guid(programGuid));
                string relapseModelXML = containerContext.Resolve<IPageSequenceService>().GetRelapseModelAsXML(languageGuid, new Guid(programGuid), new Guid(userTaskModel.TaskContent), userModel.UserGuid);
                Guid sessionGuid = containerContext.Resolve<ISessionService>().GetFirstSessionGUID(new Guid(programGuid));
                relapseModelXML = containerContext.Resolve<IXMLService>().PraseGraphPage(relapseModelXML, userModel.UserGuid, sessionGuid, languageGuid, false);
                relapseModelXML = containerContext.Resolve<IXMLService>().ParseBeforeAfterShowExpression(relapseModelXML, sessionGuid, "Session", programGuid.ToString(), userModel.UserGuid.ToString());
                relapseModelXML = containerContext.Resolve<IXMLService>().ParseTimePickerQuestion(relapseModelXML, languageGuid);
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(relapseModelXML);
                //Create an attribute.
                XmlAttribute attr = xmlDoc.CreateAttribute(Constants.FLASHXMLATTR_IS_HELP_IN_CTPP);
                attr.Value = "true";

                //Add the new node to the document. 
                xmlDoc.DocumentElement.SetAttributeNode(attr);

                returnMessage = Convert.ToInt32(ReturnMessageCodeEnum.SuccessCode).ToString() + ";" + xmlDoc.InnerXml;
            }
            else
            {
                // Login fail, show login page to let user login
                returnMessage = Convert.ToInt32(ReturnMessageCodeEnum.SuccessCode).ToString() + ";" + containerContext.Resolve<IProgramAccessoryService>().GetProgramAccessoryModelAsXML(new Guid(programGuid), languageGuid);
            }
            return returnMessage;
        }

        private string ReturnMessage_TakeSession(UserTaskModel userTaskModel, string programGuid, Guid languageGuid, HttpContext context, string returnMessage, IContainerContext containerContext)
        {
            //TaskContent: Day
            if (containerContext.Resolve<IUserService>().IsValidUser(userTaskModel.UserName, userTaskModel.PassWord, new Guid(programGuid)))
            {
                UserModel userModel = containerContext.Resolve<IUserService>().GetUserByUserName(userTaskModel.UserName, new Guid(programGuid));
                // if need pin code, go to pincode validate page else go through session
                if (containerContext.Resolve<IProgramService>().IsProgramNeedPinCode(new Guid(programGuid)))
                {
                    returnMessage = Convert.ToInt32(ReturnMessageCodeEnum.SuccessCode).ToString() + ";" + containerContext.Resolve<IProgramAccessoryService>().GetPinCodePageModelAsXML(new Guid(programGuid), languageGuid, userModel.UserGuid);
                }
                else
                {
                    returnMessage = Convert.ToInt32(ReturnMessageCodeEnum.SuccessCode).ToString() + ";" + GetCurrentSessionXML(userModel.UserGuid, new Guid(programGuid), languageGuid, Convert.ToInt32(userTaskModel.TaskContent), context, false);
                }
            }
            else
            {
                // Login fail, show login page to let user login
                returnMessage = Convert.ToInt32(ReturnMessageCodeEnum.SuccessCode).ToString() + ";" + containerContext.Resolve<IProgramAccessoryService>().GetProgramAccessoryModelAsXML(new Guid(programGuid), languageGuid);
            }
            return returnMessage;
        }

        private string ReturnMessage_RetakeSession(UserTaskModel userTaskModel, string programGuid, Guid languageGuid, HttpContext context, string returnMessage, IContainerContext containerContext)
        {
            //TaskContent: Day
            if (containerContext.Resolve<IUserService>().IsValidUser(userTaskModel.UserName, userTaskModel.PassWord, new Guid(programGuid)))
            {
                UserModel userModel = containerContext.Resolve<IUserService>().GetUserByUserName(userTaskModel.UserName, new Guid(programGuid));
                // if need pin code, go to pincode validate page else go through session
                if (containerContext.Resolve<IProgramService>().IsProgramNeedPinCode(new Guid(programGuid)))
                {
                    returnMessage = Convert.ToInt32(ReturnMessageCodeEnum.SuccessCode).ToString() + ";" + containerContext.Resolve<IProgramAccessoryService>().GetPinCodePageModelAsXML(new Guid(programGuid), languageGuid, userModel.UserGuid);
                }
                else
                {
                    returnMessage = Convert.ToInt32(ReturnMessageCodeEnum.SuccessCode).ToString() + ";" + GetCurrentSessionXML(userModel.UserGuid, new Guid(programGuid), languageGuid, Convert.ToInt32(userTaskModel.TaskContent), context, true);
                }
            }
            else
            {
                // Login fail, show login page to let user login
                returnMessage = Convert.ToInt32(ReturnMessageCodeEnum.SuccessCode).ToString() + ";" + containerContext.Resolve<IProgramAccessoryService>().GetProgramAccessoryModelAsXML(new Guid(programGuid), languageGuid);
            }
            return returnMessage;
        }

        private static string ReturnMessage_Login(UserTaskModel userTaskModel, string programGuid, Guid languageGuid, HttpContext context, string returnMessage, IContainerContext containerContext)
        {
            returnMessage = containerContext.Resolve<IUserService>().EndUserLogin(userTaskModel.UserName, userTaskModel.PassWord, programGuid);
            string[] endUserLoginStr = returnMessage.Split(';');
            if (endUserLoginStr[ENDUSER_LOGIN_STATUS] == "1")
                returnMessage = ChangeTech.Services.ServiceUtility.GetValidateUserMessageForNotRetakeAfterLogin(userTaskModel.UserName, new Guid(endUserLoginStr[USER_GUID]), new Guid(programGuid), Convert.ToInt32(endUserLoginStr[DAY]), context, languageGuid, false);
            else
                // Login fail, show login page to let user login
                returnMessage = Convert.ToInt32(ReturnMessageCodeEnum.SuccessCode).ToString() + ";" + containerContext.Resolve<IProgramAccessoryService>().GetProgramAccessoryModelAsXML(new Guid(programGuid), languageGuid);
            return returnMessage;
        }
        
        private string GetCurrentSessionXML(Guid userguid, Guid programguid, Guid languageguid, int day, HttpContext context, bool isRetake)
        {
            IContainerContext containerContext = ContainerManager.GetContainer("container");
            string returnMessage = string.Empty;
            returnMessage = containerContext.Resolve<ISessionService>().GetLiveSessionModelAsXML(userguid, programguid, languageguid, day);
            Guid sessionGuid = containerContext.Resolve<ISessionService>().GetSessionGuidByProgarmAndDay(programguid, day);
            returnMessage = containerContext.Resolve<IXMLService>().PraseGraphPage(returnMessage, userguid, sessionGuid, languageguid, isRetake);
            returnMessage = containerContext.Resolve<IXMLService>().ParseBeforeAfterShowExpression(returnMessage, sessionGuid, "Session", programguid.ToString(), userguid.ToString());
            returnMessage = containerContext.Resolve<IXMLService>().ParseTimePickerQuestion(returnMessage, languageguid);
            // add log
            InsertLogModel model = new InsertLogModel
            {
                ActivityLogType = (int)LogTypeEnum.Login,
                Browser = context.Request.UserAgent,
                IP = context.Request.UserHostAddress,
                Message = "Login Sucessful",
                ProgramGuid = programguid,
                SessionGuid = sessionGuid,
                UserGuid = userguid
            };
            containerContext.Resolve<IActivityLogService>().Insert(model);
            model.Message = "Session Begin";
            model.ActivityLogType = (int)LogTypeEnum.StartDay;
            containerContext.Resolve<IActivityLogService>().Insert(model);

            return returnMessage;
        }

        private string StartScreening(string programGuid, Guid languageGuid, HttpContext context)
        {
            string returnMessage = string.Empty;

            Guid companyGuid = GetCompanyGuidInContext(context);

            // normal register without company
            if(companyGuid == Guid.Empty)
            {
                returnMessage = RegisterNewUserAndReturnScreenXML(programGuid, languageGuid, context, companyGuid);
            }
            else
            {
                // register with validate company
                if(ValidateCompanyRight(new Guid(programGuid), companyGuid))
                {
                    returnMessage = RegisterNewUserAndReturnScreenXML(programGuid, languageGuid, context, companyGuid);
                }
                else // invalidate company
                {
                    returnMessage = getMessageXMLForInvalidateCompany(new Guid(programGuid), LoginFailedTypeEnum.InvalidateCompany.ToString());
                    returnMessage = string.Format(GetEmptySessionXML(new Guid(programGuid), Guid.Empty, languageGuid), returnMessage);
                }
            }

            return returnMessage;
        }

        private Guid GetCompanyGuidInContext(HttpContext context)
        {
            IContainerContext containerContext = ContainerManager.GetContainer("container");
            Guid companyGuid = Guid.Empty;
            if(!string.IsNullOrEmpty(context.Request.QueryString[Constants.QUERYSTR_COMPANY_GUID]))
            {
                companyGuid = new Guid(context.Request.QueryString[Constants.QUERYSTR_COMPANY_GUID]);
            }
            else
            {
                if(!string.IsNullOrEmpty(context.Request.QueryString[Constants.QUERYSTR_COMPANY_CODE]))
                {
                    companyGuid = containerContext.Resolve<ICompanyService>().GetCompanyGuidByCode(context.Request.QueryString[Constants.QUERYSTR_COMPANY_CODE]);
                }
            }

            return companyGuid;
        }

        private string GetEmptySessionXML(Guid programguid, Guid userguid, Guid languageguid)
        {
            IContainerContext containerContext = ContainerManager.GetContainer("container");
            return containerContext.Resolve<ISessionService>().GetEmptySessionXML(programguid, userguid, languageguid);
        }

        private string getMessageXMLForInvalidateCompany(Guid programguid, string messagetype)
        {
            IContainerContext containerContext = ContainerManager.GetContainer("container");
            return containerContext.Resolve<ITipMessageService>().GetTipMessageText(programguid, messagetype);
        }

        private bool ValidateCompanyRight(Guid programguid, Guid companyguid)
        {
            IContainerContext containerContext = ContainerManager.GetContainer("container");
            return containerContext.Resolve<ICompanyService>().ValidateCompanyRight(programguid, companyguid);
        }

        private string RegisterNewUserAndReturnScreenXML(string programGuid, Guid languageGuid, HttpContext context, Guid companyGuid)
        {
            string returnMessage = string.Empty;
            Guid sessionGuid = Guid.Empty;
            string studyContentGuid = context.Request.QueryString[Constants.QUERYSTR_STUDY_CONTENT_GUID];

            IContainerContext containerContext = ContainerManager.GetContainer("container");
            UserModel tempUser = new UserModel();
            tempUser.UserGuid = Guid.NewGuid();
            tempUser.UserName = "ChangeTechTemp" + tempUser.UserGuid;
            tempUser.PassWord = "temp";
            tempUser.Gender = GenderEnum.Male;
            tempUser.PhoneNumber = "temp";
            tempUser.ProgramGuid = new Guid(programGuid);
            if(companyGuid!=Guid.Empty)
            {
                tempUser.UserGroupGuid = companyGuid;
            }
            if(!string.IsNullOrEmpty(context.Request.QueryString[Constants.QUERYSTR_USERTYPE]))
            {
                tempUser.UserType = UserTypeEnum.Tester;
            }
            else
            {
                tempUser.UserType = UserTypeEnum.User;
            }
            containerContext.Resolve<IUserService>().RegisterUser(tempUser, languageGuid, new Guid(programGuid), context.Request.UserHostAddress);
            containerContext.Resolve<IProgramService>().JoinProgram(new Guid(programGuid), tempUser.UserGuid, tempUser.UserGroupGuid, 3, context.Request.UserHostAddress, ProgramUserStatusEnum.Screening, studyContentGuid);
            sessionGuid = containerContext.Resolve<ISessionService>().GetSessionGuidByProgarmAndDay(new Guid(programGuid), 0);

            // add log
            InsertLogModel model = new InsertLogModel
            {
                ActivityLogType = (int)LogTypeEnum.Login,
                Browser = context.Request.UserAgent,
                IP = context.Request.UserHostAddress,
                Message = "Start screening",
                ProgramGuid = new Guid(programGuid),
                SessionGuid = sessionGuid,
                UserGuid = tempUser.UserGuid,
                From = context.Request.QueryString[Constants.QUERYSTR_FROMWEBSITE]
            };
            containerContext.Resolve<IActivityLogService>().Insert(model);

            returnMessage = containerContext.Resolve<ISessionService>().GetLiveSessionModelAsXML(tempUser.UserGuid, new Guid(programGuid), languageGuid, 0);
            returnMessage = containerContext.Resolve<IXMLService>().PraseGraphPage(returnMessage, tempUser.UserGuid, sessionGuid, languageGuid, false);
            returnMessage = containerContext.Resolve<IXMLService>().ParseBeforeAfterShowExpression(returnMessage, sessionGuid, "Session", programGuid, tempUser.UserGuid.ToString());
            returnMessage = containerContext.Resolve<IXMLService>().ParseTimePickerQuestion(returnMessage, languageGuid);

            return returnMessage;
        }

        private static UserTaskModel GenerateUserTaskModel(string security)
        {
            string[] userInfo = StringUtility.MD5Decrypt(security, Constants.MD5_KEY).Split(';');
            UserTaskModel userTaskModel = new UserTaskModel();
            userTaskModel.UserName = userInfo[USER_NAME];
            userTaskModel.PassWord = userInfo[PASSWORD];
            UserTaskTypeEnum userTaskType = UserTaskTypeEnum.Login;
            if (userInfo.Length >= MIN_PARAMETER_COUNT_IF_HAVE_TASK)
            {
                if (Enum.TryParse(userInfo[TASK_TYPE], true, out userTaskType))
                {
                    userTaskModel.TaskType = userTaskType;
                    if (userInfo.Length > MIN_PARAMETER_COUNT_IF_HAVE_TASK)
                        userTaskModel.TaskContent = userInfo[TASK_CONTENT];
                }
                else
                {
                    //TODO:parameter error, need to handle
                }
            }
            return userTaskModel;
        }
        #endregion
    }
}
