using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Xml;
using ChangeTech.Contracts;
using ChangeTech.Models;
using Ethos.DependencyInjection;
using Ethos.Utility;
using ChangeTech.Services;
using ChangeTech.Entities;
using ChangeTech.IDataRepository;
using System.Web.Script.Serialization;

namespace ChangeTech.DeveloperWeb
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.Web.Script.Services.ScriptService]
    public class AccepteAnswer : IHttpHandler
    {
        [WebMethod]
        public void ProcessRequest(HttpContext context)
        {
            IContainerContext containerContext = ContainerManager.GetContainer("container");

            // Analyize request stream
            Stream instream = context.Request.InputStream;
            BinaryReader br = new BinaryReader(instream, System.Text.Encoding.UTF8);
            byte[] bytes = br.ReadBytes((int)instream.Length);
            string requestString = Encoding.UTF8.GetString(bytes);

            try
            {
                // The result will return to flash
                string responseStr = string.Empty;
                // Get mode and day from query string
                string programMode = context.Request.QueryString[Constants.QUERYSTR_MODE];

                //Judge program whether belong to OrderSystem's program
                string security = context.Request.QueryString[Constants.QUERYSTR_SECURITY];
                string hpSecurity = context.Request.QueryString[Constants.QUERYSTR_HP_SECURITY];

                // Parse request string
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(requestString);
                XmlNode root = doc.FirstChild;

                string userGuid = root.Attributes[Constants.QUERYSTR_USER_GUID].Value;
                string programGuid = root.Attributes[Constants.QUERYSTR_PROGRAM_GUID].Value;

                if (root.Name.Equals("XMLModel"))
                {
                    string sessionGuid = "";
                    if (root.Attributes[Constants.QUERYSTR_SESSION_GUID] != null && !String.IsNullOrEmpty(root.Attributes[Constants.QUERYSTR_SESSION_GUID].Value))
                        sessionGuid = root.Attributes[Constants.QUERYSTR_SESSION_GUID].Value;

                    Guid pageSequenceGuid = Guid.Empty;
                    if (root.Attributes[Constants.FLASHXMLATTR_PAGE_SEQUENCE_GUID] != null && !String.IsNullOrEmpty(root.Attributes[Constants.FLASHXMLATTR_PAGE_SEQUENCE_GUID].Value))
                    {
                        pageSequenceGuid = new Guid(root.Attributes[Constants.FLASHXMLATTR_PAGE_SEQUENCE_GUID].Value);
                    }

                    Guid pageGuid = Guid.Empty;
                    if (root.Attributes[Constants.FLASHXMLATTR_PAGE_GUID] != null && !String.IsNullOrEmpty(root.Attributes[Constants.FLASHXMLATTR_PAGE_GUID].Value))
                    {
                        pageGuid = new Guid(root.Attributes[Constants.FLASHXMLATTR_PAGE_GUID].Value);
                    }

                    XmlNode submitFunctionNode = root.FirstChild;

                    Guid languageGuid = containerContext.Resolve<IProgramService>().GetProgramLanguage(new Guid(programGuid)).LanguageGUID;

                    switch (submitFunctionNode.Name)
                    {
                        //Register User --> Update User's TimeZone
                        case "Register":
                            decimal timeZone = 0;
                            string registerEmail = submitFunctionNode.Attributes["Email"].Value;
                            string registerPassward = submitFunctionNode.Attributes["Password"].Value;
                            string mobile = submitFunctionNode.Attributes["Mobile"].Value;
                            string serialNumber = submitFunctionNode.Attributes["SerialNumber"].Value;
                            if (containerContext.Resolve<IProgramService>().GetProgramByGUID(new Guid(programGuid)).IsSupportTimeZone == true)
                            {
                                string timeZoneStr = !string.IsNullOrEmpty(submitFunctionNode.Attributes["TimeZone"].Value) ? submitFunctionNode.Attributes["TimeZone"].Value : "0";
                                decimal.TryParse(timeZoneStr, out timeZone);
                            }

                            RegisterProgramUserModel registerProgramUser = new RegisterProgramUserModel()
                            {
                                UserName = registerEmail,
                                Password = registerPassward,
                                ProgramGuid = new Guid(programGuid),
                                PhoneNumber = mobile,
                                SerialNumber = serialNumber,
                                UserTimeZone = timeZone,
                                UserGuid = new Guid(userGuid)
                            };

                            if (!programMode.Equals("Preview"))
                            {
                                if (!string.IsNullOrEmpty(hpSecurity))
                                {
                                    responseStr = HPOrderLicenceRegistHandler(registerProgramUser, hpSecurity, programGuid, userGuid);
                                }
                                else
                                {
                                    if (string.IsNullOrEmpty(security))
                                    {
                                        responseStr = Register(registerProgramUser);
                                    }
                                    else
                                    {
                                        responseStr = OrderLicenceRegistHandler(registerProgramUser, security, programGuid, userGuid);
                                    }
                                }
                            }
                            break;
                        case "Login":
                            responseStr = Login(submitFunctionNode, /*day,*/ programGuid, languageGuid, context);
                            break;
                        case "PinCode":
                            responseStr = ValidatePinCode(submitFunctionNode, programGuid, userGuid, languageGuid, context);
                            break;
                        case "PinCodeReminder":
                            responseStr = ReSendPinCode(programGuid, userGuid);
                            break;
                        case "PasswordReminder":
                            string accountEMail = submitFunctionNode.Attributes["Email"].Value;
                            responseStr = SendPassword(accountEMail, programGuid);
                            break;
                        case "Feedbacks":
                            // so far only care about Question, set Value(UserPageVariable table) to null. 
                            responseStr = StoreUserFeedback(root, submitFunctionNode, userGuid, programGuid, sessionGuid, programMode, context);
                            break;
                        case "Assignments":
                            // so far only care about UserPageVariable, set QuestionAnswerGUID to null, need to look through later.
                            responseStr = SetPageVariableValue(submitFunctionNode, programGuid, sessionGuid, userGuid, context);
                            break;
                        case "SessionEnding":
                            bool isRetake = false;
                            if (root.Attributes["IsRetake"] != null)
                            {
                                if (root.Attributes["IsRetake"].Value.ToLower().Equals("true"))
                                {
                                    isRetake = true;
                                }
                            }
                            ProgramUser pu = containerContext.Resolve<IProgramUserService>().GetProgramUserByProgramGuidAndUserGuid(new Guid(programGuid), new Guid(userGuid));
                            if (pu != null)
                            {
                                Win8ProgramUserModel win8ProgramUserModel = containerContext.Resolve<IWin8ProgramUserService>().GetWin8ProgramUserModelByProgramUserGuid(pu.ProgramUserGUID);
                                if (win8ProgramUserModel == null)
                                {
                                    responseStr = SessionEndingHandler(hpSecurity, userGuid, programGuid, sessionGuid, pageGuid, languageGuid, context, isRetake);
                                }
                                else
                                {
                                    responseStr = SessionEndingByWin8Handler(hpSecurity, userGuid, programGuid, sessionGuid, pageGuid, languageGuid, context, isRetake);
                                }
                            }
                            break;
                        case "SMS":
                            responseStr = SetSMS(submitFunctionNode, userGuid, programGuid, sessionGuid);
                            break;
                        case "PageStart":
                            if (submitFunctionNode.Attributes["IsGraph"] != null)
                            {
                                string isGraph = submitFunctionNode.Attributes["IsGraph"].Value;
                                if (isGraph == "0")
                                {
                                    responseStr = RecordPageActiviryLog(userGuid, programGuid, sessionGuid, pageSequenceGuid.ToString(),
                                        pageGuid.ToString(), context, LogTypeEnum.PageStart);
                                }
                                else //need to calculate
                                { 
                                    responseStr = GetGraphData(userGuid, programGuid, languageGuid, sessionGuid, pageSequenceGuid.ToString(),
                                        pageGuid.ToString());                                   
                                    if (responseStr.Substring(0,1).CompareTo("1")==0)//shoud be "1;XML" or "1;OK"
                                    {
                                        string insertStr = RecordPageActiviryLog(userGuid, programGuid, sessionGuid, pageSequenceGuid.ToString(),
                                            pageGuid.ToString(), context, LogTypeEnum.PageStart);
                                        if (insertStr.ToUpper().CompareTo("1;OK") != 0)
                                        {
                                            responseStr = "0;ERROR";
                                        }
                                    }
                                }
                            }
                            else {
                                responseStr = RecordPageActiviryLog(userGuid, programGuid, sessionGuid, pageSequenceGuid.ToString(),
                                        pageGuid.ToString(), context, LogTypeEnum.PageStart);
                            }
                            break;
                        case "PageEnd":
                            responseStr = RecordPageActiviryLog(userGuid, programGuid, sessionGuid, pageSequenceGuid.ToString(),
                                pageGuid.ToString(), context, LogTypeEnum.PageEnd);
                            break;
                        case "PageSequenceStart":
                            responseStr = RecordPageActiviryLog(userGuid, programGuid, sessionGuid, pageSequenceGuid.ToString(),
                                pageGuid.ToString(), context, LogTypeEnum.PageSequenceStart);
                            break;
                        case "PageSequenceEnd":
                            responseStr = RecordPageActiviryLog(userGuid, programGuid, sessionGuid, pageSequenceGuid.ToString(),
                                pageGuid.ToString(), context, LogTypeEnum.PageSequenceEnd);
                            break;
                        case "RelapseStart":
                            responseStr = RecordPageActiviryLog(userGuid, programGuid, sessionGuid, pageSequenceGuid.ToString(),
                                pageGuid.ToString(), context, LogTypeEnum.RelapseStart);
                            break;
                        case "RelapseEnd":
                            responseStr = RecordPageActiviryLog(userGuid, programGuid, sessionGuid, pageSequenceGuid.ToString(),
                                pageGuid.ToString(), context, LogTypeEnum.RelapseEnd);
                            // Update report status if needed.
                            if (root.Attributes[Constants.FLASHXMLATTR_IS_REPORT_IN_CTPP] != null
                                && root.Attributes[Constants.FLASHXMLATTR_IS_REPORT_IN_CTPP].Value.ToLower().Equals("true"))
                            {
                                containerContext.Resolve<IProgramUserService>().SetLastReportRelapseTime(new Guid(programGuid), new Guid(userGuid));
                            }

                            //Only if the relapseend is from HelpButtonRelapse or ReportButtonRelapse, it need call RelapseEndingHandler
                            if ((root.Attributes[Constants.FLASHXMLATTR_IS_REPORT_IN_CTPP] != null
                                && root.Attributes[Constants.FLASHXMLATTR_IS_REPORT_IN_CTPP].Value.ToLower().Equals("true"))
                                || (root.Attributes[Constants.FLASHXMLATTR_IS_HELP_IN_CTPP] != null
                                && root.Attributes[Constants.FLASHXMLATTR_IS_HELP_IN_CTPP].Value.ToLower().Equals("true")))
                            {
                                responseStr = RelapseEndingHandler(userGuid, programGuid, pageGuid, languageGuid, context);
                            }
                            else
                            {
                                responseStr = Convert.ToInt32(ReturnMessageCodeEnum.SuccessCode).ToString() + ";OK";
                            }
                            break;
                        case "GoWeb":
                            string resultUrl = root.Attributes["URL"].Value;
                            responseStr = LFResultParametersEncrypt(resultUrl);
                            break;
                    }
                }
                else if (root.Name.Equals("ProgrameStatus"))
                {
                    // Pause program
                    if (root.Attributes["Status"].Value.Equals("pause"))
                    {
                        int week = Convert.ToInt32(root.Attributes["Week"].Value);
                        responseStr = PauseProgram(userGuid, programGuid, week);
                    }
                    // Exit program
                    else if (root.Attributes["Status"].Value.Equals("exit"))
                    {
                        responseStr = ExitProgram(userGuid, programGuid);
                    }
                }
                // Update profile
                else if (root.Name.Equals("ChangeProfile"))
                {
                    responseStr = UpdateProfile(root, userGuid);
                }
                // Tip a friend
                else if (root.Name.Equals("TipFriend"))
                {
                    string invitee = root.Attributes["Invitee"].Value;
                    responseStr = TipFriend(invitee, userGuid, programGuid);
                }
                // Update user set TimeZone
                else if (root.Name.Equals("TimeZone"))
                {
                    decimal timeZone = decimal.MinValue;
                    string timeZoneStr = root.Attributes["Value"].Value;
                    if (decimal.TryParse(timeZoneStr, out timeZone))
                    {
                        responseStr = UpdateProgramUserTimeZone(timeZone, userGuid, programGuid);
                    }
                }
                // Set SMS to Email
                else if (root.Name.Equals("SMSToEmail"))
                {
                    string smsToEmailStr = !string.IsNullOrEmpty(root.Attributes["Value"].Value) ? root.Attributes["Value"].Value : bool.FalseString;
                    bool isSmsToEmail = false;
                    if (bool.TryParse(smsToEmailStr, out isSmsToEmail))
                    {
                        responseStr = SetSMSToEmailForProgramUser(isSmsToEmail, new Guid(programGuid), new Guid(userGuid), context);
                    }
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
                context.Response.Write(responseStr);
            }
            catch (Exception ex)
            {
                context.Response.ContentType = "text/plain";
                context.Response.Write(ex.Message);
                LogUtility.LogUtilityIntance.LogMessage(string.Format("Method: {0}, Request: {1}, Exception Message:{2}, Request URL: {3}", "AcceptAnswer", requestString, ex, context.Request.Url.ToString()));
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        #region Function handler

        private string LFResultParametersEncrypt(string resultUrl)
        {
            string returnMessage = string.Empty;
            ResultResponseModel resultResponseModel = null;
            try
            {
                if (resultUrl.Contains("?"))
                {
                    int index = resultUrl.IndexOf("?");
                    string urlWithoutParameters = resultUrl.Substring(0, index);
                    string parameters = resultUrl.Substring(index + 1);
                    //TOB=1&ALK=2&DRO=3
                    string[] variablesInfo = parameters.Split('&');
                    resultResponseModel = GetResultResponseModel(variablesInfo);
                    returnMessage = Convert.ToInt32(ReturnMessageCodeEnum.SuccessCode).ToString() + ";" + urlWithoutParameters + "?GoWeb=" + StringUtility.MD5Encrypt(new JavaScriptSerializer().Serialize(resultResponseModel), "psycholo");
                }
                else
                {
                    returnMessage = Convert.ToInt32(ReturnMessageCodeEnum.SuccessCode).ToString() + ";" + resultUrl;
                }
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, string.Format("Method Name:{0}, Output {1}", "LFResultParametersEncrypt", returnMessage));
                returnMessage = Convert.ToInt32(ReturnMessageCodeEnum.ErrorCode).ToString() + ";Internal error.";
            }
            return returnMessage;
        }

        private ResultResponseModel GetResultResponseModel(string[] variablesInfo)
        {
            ResultResponseModel resultResponseModel = new ResultResponseModel();
            ResultLineModel resultLineModel = new ResultLineModel();
            List<ResultVariableModel> resultVariables = new List<ResultVariableModel>();
            foreach (string variableInfo in variablesInfo)
            {
                //[TOB=1,ALK=2,DRO=3]
                if (variableInfo.Contains("="))
                {
                    int index = variableInfo.IndexOf("=");
                    string variableName = variableInfo.Substring(0, index);
                    string variableValue = variableInfo.Substring(index + 1);
                    // Get Variable value according variableName and programUserGuid
                    ResultVariableModel resultVariableModel = new ResultVariableModel
                    {
                        VariableName = variableName,
                        VariableValue = variableValue
                    };

                    if (!resultVariables.Contains(resultVariableModel))
                    {
                        resultVariables.Add(resultVariableModel);
                    }
                }
            }
            resultLineModel.ResultDateTime = DateTime.UtcNow;
            resultLineModel.ResultVaribles = resultVariables;
            resultResponseModel.ResultType = ResultTypeEnum.ResultLine;
            resultResponseModel.Content = resultLineModel;

            return resultResponseModel;
        }

        private string ReSendPinCode(string programGuid, string userGuid)
        {
            // "3" means re-send PinCode successfully, "4" means failed
            string result = string.Empty;
            if (SendPinCode(programGuid, userGuid))
            {
                result = "3";
            }
            else
            {
                result = "4";
            }

            return result;
        }

        private string ValidatePinCode(XmlNode submitFunctionNode, string programGuid, string userGuid, Guid languageGuid, HttpContext context)
        {
            string returnStr = string.Empty;
            IContainerContext containerContext = ContainerManager.GetContainer("container");
            string pinCode = submitFunctionNode.Attributes["PinCode"].Value;

            if (containerContext.Resolve<IUserService>().ValidatePinCode(new Guid(userGuid), pinCode))
            {
                // should user pay for program
                if (containerContext.Resolve<IPaymentService>().ShoulPayForProgram(new Guid(userGuid), new Guid(programGuid)))
                {
                    returnStr = containerContext.Resolve<IProgramAccessoryService>().GetPaymentModelAsXML(new Guid(programGuid), languageGuid, new Guid(userGuid));
                    //returnStr = containerContext.Resolve<IXMLService>().ParsePaylinkOnly(returnStr, programGuid, userGuid);
                    returnStr = Convert.ToInt32(ReturnMessageCodeEnum.SuccessCode).ToString() + ";" + returnStr;
                }
                else
                {
                    DateTime setCurrentTimeByTimeZone = containerContext.Resolve<IProgramUserService>().GetCurrentTimeByTimeZone(new Guid(programGuid), new Guid(userGuid), DateTime.UtcNow);
                    string security = context.Request.QueryString[Constants.QUERYSTR_SECURITY];
                    if (security != null)
                        security = security.Replace("#", "");
                    if (!string.IsNullOrEmpty(security))
                    {
                        string[] userInfo = StringUtility.MD5Decrypt(security, Constants.MD5_KEY).Split(';');
                        string userName = userInfo[0];
                        string password = userInfo[1];
                        string day = string.Empty;
                        string userTaskTypeStr = string.Empty;
                        UserTaskTypeEnum userTaskType = UserTaskTypeEnum.Login;
                        if (userInfo.Length > 2)
                        {
                            day = userInfo[3];
                            userTaskTypeStr = userInfo[2];
                            Enum.TryParse(userTaskTypeStr, true, out userTaskType);
                        }
                        if (!string.IsNullOrEmpty(day))
                        {
                            if (userTaskType == UserTaskTypeEnum.TakeSession)
                            {
                                returnStr = GetCurrentSessionXML(new Guid(userGuid), new Guid(programGuid), languageGuid, Convert.ToInt32(day), context, false);
                            }
                            else
                            {
                                returnStr = GetCurrentSessionXML(new Guid(userGuid), new Guid(programGuid), languageGuid, Convert.ToInt32(day), context, true);
                            }
                        }
                        else
                        {
                            //returnStr = GetCurrentSessionXML(new Guid(userGuid), new Guid(programGuid), languageGuid, shouldDoDay, context, false);
                            int shouldDoDay = containerContext.Resolve<IProgramUserService>().GetShouldDoDay(new Guid(programGuid), new Guid(userGuid), setCurrentTimeByTimeZone);
                            returnStr = ServiceUtility.GetValidateUserMessageForNotRetakeAfterLogin(userName, new Guid(userGuid), new Guid(programGuid), shouldDoDay, context, languageGuid, true);
                        }
                    }
                    else
                    {
                        //returnStr = GetCurrentSessionXML(new Guid(userGuid), new Guid(programGuid), languageGuid, shouldDoDay, context, false);
                        int shouldDoDay = containerContext.Resolve<IProgramUserService>().GetShouldDoDay(new Guid(programGuid), new Guid(userGuid), setCurrentTimeByTimeZone);
                        UserModel userModel = containerContext.Resolve<IUserService>().GetUserByUserGuid(new Guid(userGuid));
                        returnStr = ServiceUtility.GetValidateUserMessageForNotRetakeAfterLogin(userModel.UserName, new Guid(userGuid), new Guid(programGuid), shouldDoDay, context, languageGuid, true);
                    }
                }
            }
            else
            {
                returnStr = Convert.ToInt32(ReturnMessageCodeEnum.ErrorCode).ToString() + ";" + containerContext.Resolve<ITipMessageService>().GetTipMessageText(new Guid(programGuid), LoginFailedTypeEnum.PinCodeWrong.ToString());
            }
            return returnStr;
        }

        private bool IsRetakeSession(IContainerContext containerContext, Guid programGuid, Guid userGuid, Guid sessionGuid)
        {
            bool isRetake = false;
            ProgramUser pu = containerContext.Resolve<IProgramUserService>().GetProgramUserByProgramGuidAndUserGuid(programGuid, userGuid);
            if (pu != null)
            {
                EditSessionModel editSessionModel = containerContext.Resolve<ISessionService>().GetSessionBySessonGuid(sessionGuid);
                ProgramUserSession puSession = containerContext.Resolve<IProgramUserSessionRepository>().GetProgramUserSessionByProgramUserGuidAndSessionGuid(pu.ProgramUserGUID, sessionGuid);
                if (puSession != null)
                {
                    if (!puSession.SessionReference.IsLoaded) puSession.SessionReference.Load();
                    if (editSessionModel != null && editSessionModel.Day == puSession.Session.Day)
                    {
                        isRetake = true;
                    }
                }
            }

            return isRetake;
        }

        private string SetSMS(XmlNode submitFunctionNode, string userGuid, string programGuid, string sessionGuid)
        {
            try
            {
                IContainerContext containerContext = ContainerManager.GetContainer("container");
                bool isRetake = IsRetakeSession(containerContext, new Guid(programGuid), new Guid(userGuid), new Guid(sessionGuid));
                if (!isRetake)
                {
                    int sendSMSAfterDays = int.MinValue;
                    int sendTime = int.MinValue;
                    ShortMessageModel smsModel = new ShortMessageModel();
                    smsModel.Text = submitFunctionNode.Attributes["Text"].Value;
                    smsModel.MobileNo = submitFunctionNode.Attributes["MobilePhone"].Value.Trim();
                    if (!string.IsNullOrEmpty(submitFunctionNode.Attributes["Days"].Value))
                    {
                        //smsModel.SendDateTime = Convert.ToInt32(submitFunctionNode.Attributes["Days"].Value);
                        sendSMSAfterDays = Convert.ToInt32(submitFunctionNode.Attributes["Days"].Value);
                    }
                    else
                    {
                        //smsModel.SendDateTime = 0;
                        sendSMSAfterDays = 0;
                    }
                    if (!string.IsNullOrEmpty(submitFunctionNode.Attributes["Time"].Value))
                    {
                        //smsModel.SendTime = Convert.ToInt32(submitFunctionNode.Attributes["Time"].Value);
                        sendTime = Convert.ToInt32(submitFunctionNode.Attributes["Time"].Value);
                    }
                    else
                    {
                        //smsModel.SendTime = 0;
                        sendTime = 0;
                    }

                    //Set sendDateTime.
                    DateTime setCurrentTimeByTimeZone = containerContext.Resolve<IProgramUserService>().GetCurrentTimeByTimeZone(new Guid(programGuid), new Guid(userGuid), DateTime.UtcNow);
                    //Set send SMS Date by TimeZone.
                    DateTime sendDateByTimeZone = setCurrentTimeByTimeZone.AddDays(sendSMSAfterDays).Date.AddMinutes(sendTime);
                    //Set send SMS UTCTime according sendDateByTimeZone.
                    DateTime sendDateByUtc = containerContext.Resolve<IProgramUserService>().GetCurrentTimeFromTimeZoneToUtc(new Guid(programGuid), new Guid(userGuid), sendDateByTimeZone);
                    smsModel.SendDateTime = sendDateByUtc;

                    smsModel.UserGUID = new Guid(userGuid);
                    smsModel.ProgramGUID = new Guid(programGuid);
                    if (!string.IsNullOrEmpty(sessionGuid))
                        smsModel.SessionGUID = new Guid(sessionGuid);
                    else
                        smsModel.SessionGUID = Guid.Empty;

                    containerContext.Resolve<IShortMessageService>().AddShortMessage(smsModel);

                    // add log
                    InsertLogModel model = new InsertLogModel
                    {
                        ActivityLogType = (int)LogTypeEnum.SetShortMessage,
                        Browser = HttpContext.Current.Request.UserAgent,
                        IP = HttpContext.Current.Request.UserHostAddress,
                        Message = "Add ShortMessage to Queue",
                        ProgramGuid = new Guid(programGuid),
                        PageGuid = Guid.Empty,
                        PageSequenceGuid = Guid.Empty,
                        SessionGuid = new Guid(sessionGuid),
                        UserGuid = new Guid(userGuid),
                        From = string.Empty
                    };
                }
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }

            return "Successful";
        }

        [WebMethod]
        private string Register(RegisterProgramUserModel registerprogramUser)//(string registerEmail, string registerPassward, string userGuid, string programGuid, Guid languageGUID, string mobile, string serialNumber)
        {
            IContainerContext containerContext = ContainerManager.GetContainer("container");

            string returnMessage = string.Empty;
            try
            {
                JoinProgramResultEnum joinProgramResult = containerContext.Resolve<IProgramUserService>().EndUserJoinProgram(registerprogramUser);

                if (joinProgramResult != JoinProgramResultEnum.Success)
                {
                    returnMessage = Convert.ToInt32(ReturnMessageCodeEnum.ErrorCode).ToString() + ";" + containerContext.Resolve<ITipMessageService>().GetTipMessageText(registerprogramUser.ProgramGuid, joinProgramResult.ToString());
                }
                else
                {
                    UserModel um = containerContext.Resolve<IUserService>().GetUserByUserName(registerprogramUser.UserName, registerprogramUser.ProgramGuid);
                    returnMessage = Convert.ToInt32(ReturnMessageCodeEnum.SuccessCode).ToString() + ";" + um.UserGuid;

                    if (!string.IsNullOrEmpty(registerprogramUser.PhoneNumber))
                    {
                        try
                        {
                            SendPinCode(registerprogramUser.ProgramGuid.ToString(), registerprogramUser.UserGuid.ToString());
                        }
                        catch
                        { }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, string.Format("Method Name:{0}, Email: {1}, Password: {2}", "RegisterUser", registerprogramUser.UserName, registerprogramUser.Password));
                returnMessage = Convert.ToInt32(ReturnMessageCodeEnum.ErrorCode).ToString() + ";Internal error.";
            }
            return returnMessage;
        }

        private bool SendPinCode(string programGuid, string userGuid)
        {
            IContainerContext containerContext = ContainerManager.GetContainer("container");

            bool flag = containerContext.Resolve<IShortMessageService>().SendSM(GetMessageModel(programGuid, userGuid));
            if (flag)
            {
                // add log
                InsertLogModel model = new InsertLogModel
                {
                    ActivityLogType = (int)LogTypeEnum.SendPinCodeSM,
                    Message = "Send PinCode Sucessful",
                    ProgramGuid = new Guid(programGuid),
                    UserGuid = new Guid(userGuid)
                };
                containerContext.Resolve<IActivityLogService>().Insert(model);
            }

            return flag;
        }

        private GetMessageModel GetMessageModel(string programGuid, string userGuid)
        {
            IContainerContext containerContext = ContainerManager.GetContainer("container");

            UserModel userInfo = containerContext.Resolve<IUserService>().GetUserByUserGuid(new Guid(userGuid));
            GetMessageModel messageModel = InitialMessageModel(programGuid, userGuid);
            messageModel.sMobileNumber = userInfo.PhoneNumber;
            messageModel.sMessage = string.Format(messageModel.sMessage, userInfo.PinCode);

            return messageModel;
        }

        private GetMessageModel InitialMessageModel(string programGuid, string userGuid)
        {
            IContainerContext containerContext = ContainerManager.GetContainer("container");
            GetMessageModel messageModel = new GetMessageModel();
            messageModel.sUser = containerContext.Resolve<ISystemSettingService>().GetSettingValue("sUser");
            messageModel.sPass = containerContext.Resolve<ISystemSettingService>().GetSettingValue("sPass");
            messageModel.sOrigrinator = containerContext.Resolve<ISystemSettingService>().GetSettingValue("sOriginator");
            messageModel.sForeignID = "0";
            messageModel.sMessage = containerContext.Resolve<IShortMessageService>().GetMessageTextByProgramAndType(new Guid(programGuid), SMTypeEnum.PinCode.ToString());
            return messageModel;
        }

        private string Login(XmlNode submitFunctionNode, /*string day,*/ string programGuid, Guid languageGuid, HttpContext context)
        {
            IContainerContext containerContext = ContainerManager.GetContainer("container");

            string returnMessage = string.Empty;

            //TODO: These code is same as code in AuthenticateUser.ashx.cs, should refactor

            // get username and password
            string userName = submitFunctionNode.Attributes["Username"].Value;
            string password = submitFunctionNode.Attributes["Password"].Value;

            returnMessage = containerContext.Resolve<IUserService>().EndUserLogin(userName, password, programGuid);

            string[] logedStr = returnMessage.Split(';');
            if (logedStr[0] == "1")
            {
                string userStatus = containerContext.Resolve<IUserService>().GetUserStatus(userName, new Guid(programGuid));
                if (userStatus.Equals(ProgramUserStatusEnum.Paused.ToString()))
                {
                    returnMessage = Convert.ToInt32(ReturnMessageCodeEnum.ErrorCode).ToString() + ";" + LoginFailedTypeEnum.ProgramIsPaused;
                }
                else
                {
                    //set current time by TimeZone.
                    DateTime setCurrentTimeByTimeZone = containerContext.Resolve<IProgramUserService>().GetCurrentTimeByTimeZone(new Guid(programGuid), new Guid(logedStr[1]), DateTime.UtcNow);
                    int isThereClassReturnCode = containerContext.Resolve<IProgramUserService>().IsThereClassToday(new Guid(logedStr[1]), new Guid(logedStr[2]), setCurrentTimeByTimeZone);
                    if (isThereClassReturnCode == 0)
                    {
                        // if need pin code, go to pincode validate page else go through session
                        if (containerContext.Resolve<IProgramService>().IsProgramNeedPinCode(new Guid(logedStr[2])))
                        {
                            returnMessage = Convert.ToInt32(ReturnMessageCodeEnum.SuccessCode).ToString() + ";" + containerContext.Resolve<IProgramAccessoryService>().GetPinCodePageModelAsXML(new Guid(logedStr[2]), languageGuid, new Guid(logedStr[1]));
                        }
                        else
                        {
                            returnMessage = GetCurrentSessionXML(new Guid(logedStr[1]), new Guid(logedStr[2]), languageGuid, Convert.ToInt32(logedStr[3]), context, false);
                        }
                    }
                    else if (isThereClassReturnCode == 1)
                    {
                        returnMessage = Convert.ToInt32(ReturnMessageCodeEnum.ErrorCode).ToString() + ";" + LoginFailedTypeEnum.WaitUntilNextModay;

                        InsertLogModel model = new InsertLogModel
                        {
                            ActivityLogType = (int)LogTypeEnum.Login,
                            Browser = context.Request.UserAgent,
                            IP = context.Request.UserHostAddress,
                            Message = "Wait for next monday",
                            ProgramGuid = new Guid(logedStr[2]),
                            SessionGuid = Guid.Empty,
                            UserGuid = new Guid(logedStr[1])
                        };
                        containerContext.Resolve<IActivityLogService>().Insert(model);
                    }
                    else if (isThereClassReturnCode == 5)
                    {
                        if (containerContext.Resolve<IProgramService>().IsProgramNeedPinCode(new Guid(logedStr[2])))
                        {
                            returnMessage = Convert.ToInt32(ReturnMessageCodeEnum.SuccessCode).ToString() + ";" + containerContext.Resolve<IProgramAccessoryService>().GetPinCodePageModelAsXML(new Guid(logedStr[2]), languageGuid, new Guid(logedStr[1]));
                        }
                        else
                        {
                            // should do outline work DTD-1001
                            //int outlineday = containerContext.Resolve<IProgramUserService>().GetOutlineDay(DateTime.UtcNow);
                            int outlineday = containerContext.Resolve<IProgramUserService>().GetOutlineDay(setCurrentTimeByTimeZone);
                            returnMessage = GetCurrentSessionXML(new Guid(logedStr[1]), new Guid(logedStr[2]), languageGuid, outlineday, context, false);
                        }
                    }
                    else if (isThereClassReturnCode == 6)
                    {
                        // should pay for program
                        returnMessage = containerContext.Resolve<IProgramAccessoryService>().GetPaymentModelAsXML(new Guid(logedStr[2]), languageGuid, new Guid(logedStr[1]));
                        //returnMessage = containerContext.Resolve<IXMLService>().ParsePaylinkOnly(returnMessage, logedStr[2], logedStr[1]);
                        returnMessage = Convert.ToInt32(ReturnMessageCodeEnum.SuccessCode).ToString() + ";" + returnMessage;
                    }
                    else if (isThereClassReturnCode == 7)
                    {
                        returnMessage = Convert.ToInt32(ReturnMessageCodeEnum.ErrorCode).ToString() + ";" + LoginFailedTypeEnum.NoSchedule;
                    }
                    else if (isThereClassReturnCode == 8)
                    {
                        returnMessage = Convert.ToInt32(ReturnMessageCodeEnum.ErrorCode).ToString() + ";" + LoginFailedTypeEnum.NoScheduleForCurrentSession;
                    }
                    else if (isThereClassReturnCode == -1)// have error
                    {
                        returnMessage = Convert.ToInt32(ReturnMessageCodeEnum.ErrorCode).ToString() + ";" + LoginFailedTypeEnum.ErrorExist;
                    }
                    else
                    {
                        // todo , 2:4
                        returnMessage = Convert.ToInt32(ReturnMessageCodeEnum.ErrorCode).ToString() + ";" + LoginFailedTypeEnum.HaveFinishedTodaysClass;

                        InsertLogModel model = new InsertLogModel
                        {
                            ActivityLogType = (int)LogTypeEnum.Login,
                            Browser = context.Request.UserAgent,
                            IP = context.Request.UserHostAddress,
                            Message = "Have completed all class",
                            ProgramGuid = new Guid(logedStr[2]),
                            SessionGuid = Guid.Empty,
                            UserGuid = new Guid(logedStr[1])
                        };
                        containerContext.Resolve<IActivityLogService>().Insert(model);
                    }
                }
            }

            // get tip message
            string[] loginFailedString = returnMessage.Split(';');
            if (loginFailedString[0].Equals("0"))
            {
                returnMessage = Convert.ToInt32(ReturnMessageCodeEnum.ErrorCode).ToString() + ";" + containerContext.Resolve<ITipMessageService>().GetTipMessageText(new Guid(programGuid), loginFailedString[1]);
            }

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

            return Convert.ToInt32(ReturnMessageCodeEnum.SuccessCode).ToString() + ";" + returnMessage;
        }

        private string SendPassword(string accountEMail, string programGuid)
        {
            IContainerContext containerContext = ContainerManager.GetContainer("container");
            string returnMessage = string.Empty;
            try
            {
                containerContext.Resolve<IEmailService>().SendForgetPasswordEmail(accountEMail, new Guid(programGuid));
                returnMessage = Convert.ToInt32(ReturnMessageCodeEnum.SuccessCode).ToString() + ";OK";
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, string.Format("Method Name:{0}, Email: {1}, Program GUID: {2}", "PasswordReminder", accountEMail, programGuid));
                returnMessage = Convert.ToInt32(ReturnMessageCodeEnum.ErrorCode).ToString() + ";Internal error.";
            }
            return returnMessage;
        }

        private string StoreUserFeedback(XmlNode root, XmlNode submitFunctionNode, string userGuid, string programGuid,
            string sessionGuid, string programMode, HttpContext context)
        {
            IContainerContext containerContext = ContainerManager.GetContainer("container");
            string returnMessage = string.Empty;

            try
            {
                List<UserAnswerModel> answerList = new List<UserAnswerModel>();
                for (int i = 0; i < submitFunctionNode.ChildNodes.Count; i++)
                {
                    UserAnswerModel userAnswer = new UserAnswerModel();
                    XmlNode feedbackNode = submitFunctionNode.ChildNodes[i];

                    userAnswer.UserGuid = userGuid;
                    userAnswer.PageGuid = root.Attributes["PageGUID"].Value;
                    userAnswer.PageQuestionGUID = feedbackNode.Attributes["GUID"].Value;
                    userAnswer.QuestionValue = feedbackNode.Attributes["Value"].Value;
                    userAnswer.ProgramGuid = programGuid;
                    userAnswer.SessionGuid = sessionGuid;

                    //Temp solution
                    //TODO: The xmlNode of pageSequenceOrder for Relapse should be a int,but now is a guid.
                    userAnswer.LanguageGuid = containerContext.Resolve<IProgramLanguageService>().GetLanguageOfProgramByProgramGUID(new Guid(programGuid)).ToString();
                    int psOrder = -1;
                    if (int.TryParse(root.Attributes["PageSequenceOrder"].Value, out psOrder))
                    {
                        userAnswer.PageSequenceOrder = psOrder; //<XMLModel UserGUID="C42A98E0-E23B-4BF6-A391-77ECC785B70F" ProgramGUID="35E19FBB-5078-497D-B608-2F6650DAE7A6" SessionGUID="AD334212-EC2C-4BB4-962F-E472A19F74CF" PageSequenceOrder="35E794B8-780E-41F9-929C-1158FE6BB8BF" PageGUID="3F9A9E0D-B9FB-4153-AF32-8FBA65E57EC8"><Feedbacks><Feedback GUID="536A5C9D-F072-4037-A19A-3193F83E83EA" Value="1350" /></Feedbacks></XMLModel>,
                    }

                    //For relapse
                    if (root.Attributes["RelapsePageSequenceGUID"] != null)
                    {
                        userAnswer.RelapsePageSequenceGuid = root.Attributes["RelapsePageSequenceGUID"].Value;
                    }
                    if (root.Attributes["RelapsePageGUID"] != null)
                    {
                        userAnswer.RelapsePageGuid = root.Attributes["RelapsePageGUID"].Value;
                    }

                    answerList.Add(userAnswer);
                }
                containerContext.Resolve<IUserAnswerService>().SaveUserAnswer(answerList, programMode);

                // add log
                InsertLogModel model = new InsertLogModel
                {
                    ActivityLogType = (int)LogTypeEnum.SubmitPage,
                    Browser = context.Request.UserAgent,
                    IP = context.Request.UserHostAddress,
                    Message = "Feedbacks: " + submitFunctionNode.InnerXml,
                    PageGuid = new Guid(root.Attributes["PageGUID"].Value),
                    ProgramGuid = new Guid(programGuid),
                    SessionGuid = string.IsNullOrEmpty(sessionGuid) ? Guid.Empty : new Guid(sessionGuid),
                    UserGuid = new Guid(userGuid)
                };

                containerContext.Resolve<IActivityLogService>().Insert(model);
                returnMessage = Convert.ToInt32(ReturnMessageCodeEnum.SuccessCode).ToString() + ";OK";
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, string.Format("Method Name:{0}, Feedback: {1}, Request URL: {2}",
                    "Feedback", root.OuterXml, context.Request.Url.ToStringWithoutPort()));
                returnMessage = Convert.ToInt32(ReturnMessageCodeEnum.ErrorCode).ToString() + ";Internal error.";
            }

            return returnMessage;
        }

        private string SetPageVariableValue(XmlNode submitFunctionNode, string programGuid, string sessionGuid, string userGuid, HttpContext context)
        {
            IContainerContext containerContext = ContainerManager.GetContainer("container");
            string returnMessage = string.Empty;

            try
            {
                List<SetPageVariableModel> setVariableList = new List<SetPageVariableModel>();
                for (int i = 0; i < submitFunctionNode.ChildNodes.Count; i++)
                {
                    SetPageVariableModel setVariable = new SetPageVariableModel();
                    XmlNode assignmentNode = submitFunctionNode.ChildNodes[i];
                    setVariable.ProgramGUID = new Guid(programGuid);
                    if (!string.IsNullOrEmpty(sessionGuid))
                        setVariable.SessionGUID = new Guid(sessionGuid);
                    else
                        // for the variables not in session
                        setVariable.SessionGUID = Guid.Empty;
                    setVariable.VariableName = assignmentNode.Attributes["Variable"].Value;
                    setVariable.VariableValue = assignmentNode.Attributes["Value"].Value;
                    setVariable.UserGUID = new Guid(userGuid);
                    setVariableList.Add(setVariable);
                }
                containerContext.Resolve<IPageVariableService>().SaveSetPageVariable(setVariableList);
                // add log
                InsertLogModel model = new InsertLogModel
                {
                    ActivityLogType = (int)LogTypeEnum.PageAssignment,
                    Browser = context.Request.UserAgent,
                    IP = context.Request.UserHostAddress,
                    Message = "Assignments" + submitFunctionNode.InnerXml,
                    ProgramGuid = new Guid(programGuid),
                    SessionGuid = string.IsNullOrEmpty(sessionGuid) ? Guid.Empty : new Guid(sessionGuid),
                    UserGuid = new Guid(userGuid)
                };
                containerContext.Resolve<IActivityLogService>().Insert(model);
                returnMessage = Convert.ToInt32(ReturnMessageCodeEnum.SuccessCode).ToString() + ";OK";
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, string.Format("Method Name:{0}, Assignments: {1}, Request URL: {2}", "Assignments", submitFunctionNode.OuterXml, context.Request.Url.ToStringWithoutPort()));
                returnMessage = Convert.ToInt32(ReturnMessageCodeEnum.ErrorCode).ToString() + ";Internal error.";
            }

            return returnMessage;
        }

        private string PauseProgram(string userGuid, string programGuid, int week)
        {
            IContainerContext containerContext = ContainerManager.GetContainer("container");
            string returnMessage = string.Empty;

            try
            {
                // true: update the pauseday, false: already paused
                containerContext.Resolve<IProgramUserService>().PauseProgram(new Guid(userGuid), new Guid(programGuid), week * 7);
                returnMessage = Convert.ToInt32(ReturnMessageCodeEnum.SuccessCode).ToString() + ";OK";
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, string.Format("Method Name:{0}, Output {1}", "PauseProgram", returnMessage));
                returnMessage = Convert.ToInt32(ReturnMessageCodeEnum.ErrorCode).ToString() + ";Internal error.";
            }

            return returnMessage;
        }

        private string ExitProgram(string userGuid, string programGuid)
        {
            IContainerContext containerContext = ContainerManager.GetContainer("container");
            string returnMessage = string.Empty;

            try
            {
                containerContext.Resolve<IProgramUserService>().ExitProgram(new Guid(userGuid), new Guid(programGuid));
                returnMessage = Convert.ToInt32(ReturnMessageCodeEnum.SuccessCode).ToString() + ";OK";
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, string.Format("Method Name:{0}, Output {1}", "Exit program", returnMessage));
                returnMessage = Convert.ToInt32(ReturnMessageCodeEnum.ErrorCode).ToString() + ";Internal error.";
            }

            return returnMessage;
        }

        private string UpdateProfile(XmlNode root, string userGuid)
        {
            IContainerContext containerContext = ContainerManager.GetContainer("container");
            string returnMessage = string.Empty;

            try
            {
                XmlNode profileItemsNode = root.FirstChild;
                if (profileItemsNode.HasChildNodes)
                {
                    UserModel userInfo = new UserModel();
                    userInfo.UserGuid = new Guid(userGuid);
                    foreach (XmlNode node in profileItemsNode.ChildNodes)
                    {
                        switch (node.Attributes["Name"].Value)
                        {
                            case "Email":
                                userInfo.UserName = node.Attributes["NewValue"].Value;
                                break;
                            case "FirstName":
                                userInfo.FirstName = node.Attributes["NewValue"].Value;
                                break;
                            case "LastName":
                                userInfo.LastName = node.Attributes["NewValue"].Value;
                                break;
                            case "MobilePhone":
                                userInfo.PhoneNumber = node.Attributes["NewValue"].Value;
                                break;
                        }
                    }
                    returnMessage = containerContext.Resolve<IUserService>().UpdateUserProfile(userInfo).ToString();
                }
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, string.Format("Method Name:{0}, Output {1}, Request Data: {2}", "UpdateProfile", returnMessage, root.OuterXml));
            }
            return returnMessage;
        }

        private string TipFriend(string invitee, string userGuid, string programGuid)
        {
            IContainerContext containerContext = ContainerManager.GetContainer("container");
            string returnMessage = string.Empty;

            try
            {
                containerContext.Resolve<IEmailService>().SendInvitiationEmail(new Guid(userGuid), new Guid(programGuid), invitee);
                returnMessage = Convert.ToInt32(ReturnMessageCodeEnum.SuccessCode).ToString() + ";OK";
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, string.Format("Method Name:{0}, Output {1}", "UpdateProfile", returnMessage));
                returnMessage = Convert.ToInt32(ReturnMessageCodeEnum.ErrorCode).ToString() + ";Internal error.";
            }

            return returnMessage;
        }

        //update programuser's timezone 
        private string UpdateProgramUserTimeZone(decimal timeZone, string userGuid, string programGuid)
        {
            IContainerContext containerContext = ContainerManager.GetContainer("container");
            string returnMessage = string.Empty;
            try
            {
                ProgramUser pu = containerContext.Resolve<IProgramUserService>().GetProgramUserByProgramGuidAndUserGuid(new Guid(programGuid), new Guid(userGuid));
                string logMessage = string.Empty;
                if (pu.TimeZone.HasValue)
                {
                    logMessage = " TimeZone : " + pu.TimeZone.Value + " ---> TimeZone : " + timeZone;
                }
                else
                {
                    logMessage = " TimeZone : " + timeZone;
                }

                ProgramUserModel programUserModel = new ProgramUserModel()
                {
                    ProgramGUID = new Guid(programGuid),
                    UserGUID = new Guid(userGuid),
                    UserTimeZone = timeZone
                };
                //containerContext.Resolve<IProgramUserService>().UpdateProgramUserTimeZone(programUserModel);
                containerContext.Resolve<IProgramUserService>().UpdateProgramUserProperty(programUserModel);

                // add log
                InsertLogModel model = new InsertLogModel
                {
                    ActivityLogType = (int)LogTypeEnum.ModifyProgramUser,
                    Browser = HttpContext.Current.Request.UserAgent,
                    IP = HttpContext.Current.Request.UserHostAddress,
                    Message = logMessage,
                    ProgramGuid = new Guid(programGuid),
                    PageGuid = Guid.Empty,
                    PageSequenceGuid = Guid.Empty,
                    SessionGuid = Guid.Empty,
                    UserGuid = new Guid(userGuid),
                    From = string.Empty
                };
                containerContext.Resolve<IActivityLogService>().Insert(model);
                returnMessage = Convert.ToInt32(ReturnMessageCodeEnum.SuccessCode).ToString() + ";OK";
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, string.Format("Method Name:{0}, Output {1}", "UpdateProgramUserTimeZone", returnMessage));
                returnMessage = Convert.ToInt32(ReturnMessageCodeEnum.ErrorCode).ToString() + ";Internal error.";
            }

            return returnMessage;
        }

        private string RelapseEndingHandler(string userGuid, string programGuid, Guid pageGuid, Guid languageGuid, HttpContext context)
        {
            IContainerContext containerContext = ContainerManager.GetContainer("container");
            string returnMessage = string.Empty;

            try
            {
                ProgramModel programModel = containerContext.Resolve<IProgramService>().GetProgramByGUID(new Guid(programGuid));
                if (programModel.IsCTPPEnable)
                {
                    returnMessage = Convert.ToInt32(ReturnMessageCodeEnum.CtppCode).ToString() + ";" + StringUtility.MD5Encrypt(string.Format("{0};{1};{2};{3}", Convert.ToInt32(ReturnMessageCodeEnum.SuccessCode).ToString(), programGuid, userGuid, pageGuid), Constants.MD5_KEY);
                }
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex,
                    string.Format("Method Name:{0}, Request URL: {1}, User GUID: {2}, Program GUID: {3}, Session GUID: {4}; Page GUID: {5}",
                    "RelapseEnding",
                    context.Request.Url.ToStringWithoutPort(),
                    userGuid,
                    programGuid,
                    Guid.Empty.ToString(),
                    pageGuid));

                string endingFailedMsg = containerContext.Resolve<ITipMessageService>().GetTipMessageText(new Guid(programGuid), "EndingFailed");
                returnMessage = Convert.ToInt32(ReturnMessageCodeEnum.ErrorCode).ToString() + ";" + endingFailedMsg;
            }

            return returnMessage;
        }

        private string SessionEndingHandler(string hpSecurity, string userGuid, string programGuid, string sessionGuid, Guid pageGuid, Guid languageGuid, HttpContext context, bool isRetake)
        {
            IContainerContext containerContext = ContainerManager.GetContainer("container");
            string returnMessage = string.Empty;

            try
            {
                ProgramModel programModel = containerContext.Resolve<IProgramService>().GetProgramByGUID(new Guid(programGuid));
                if (programModel != null)
                {
                    if (programModel.IsCTPPEnable && isRetake)
                    {
                        //if (pageGuid.ToString().Equals(Guid.Empty.ToString()))
                        //{
                        //    returnMessage = containerContext.Resolve<IProgramAccessoryService>().GetSessionEndingPageModelAsXML(new Guid(programGuid), languageGuid, new Guid(userGuid));
                        //    returnMessage = Convert.ToInt32(ReturnMessageCodeEnum.SuccessCode).ToString() + ";" + returnMessage;
                        //}
                        //else
                        //{
                        returnMessage = Convert.ToInt32(ReturnMessageCodeEnum.CtppCode).ToString() + ";" + StringUtility.MD5Encrypt(string.Format("{0};{1};{2};{3}", Convert.ToInt32(ReturnMessageCodeEnum.SuccessCode).ToString(), programGuid, userGuid, pageGuid), Constants.MD5_KEY);
                        //}
                    }
                    else
                    {
                        // if sessionGuid is null or empty, it's payment page won't update programuser and class status
                        if (!string.IsNullOrEmpty(sessionGuid))
                        {
                            // Set class stauts
                            containerContext.Resolve<IProgramUserService>().SetUserClassStatus(hpSecurity, new Guid(userGuid), new Guid(programGuid), new Guid(sessionGuid));

                            // Add log
                            InsertLogModel model = new InsertLogModel
                            {
                                ActivityLogType = (int)LogTypeEnum.EndDay,
                                Browser = context.Request.UserAgent,
                                IP = context.Request.UserHostAddress,
                                Message = "Session End",
                                ProgramGuid = new Guid(programGuid),
                                SessionGuid = new Guid(sessionGuid),
                                UserGuid = new Guid(userGuid)
                            };
                            containerContext.Resolve<IActivityLogService>().Insert(model);
                            // If it tester, send daily email immediately
                            containerContext.Resolve<IProgramUserService>().SendEmailToTester(new Guid(userGuid), new Guid(programGuid));
                        }

                        //set current time accoding TimeZone.
                        DateTime setCurrentTimeByTimeZone = containerContext.Resolve<IProgramUserService>().GetCurrentTimeByTimeZone(new Guid(programGuid), new Guid(userGuid), DateTime.UtcNow);
                        int isThereClassReturnCode = containerContext.Resolve<IProgramUserService>().IsThereClassToday(new Guid(userGuid), new Guid(programGuid), setCurrentTimeByTimeZone);
                        if (programModel.IsCTPPEnable && programModel.IsNoCatchUp == true)
                        {
                            returnMessage = Convert.ToInt32(ReturnMessageCodeEnum.CtppCode).ToString() + ";" + StringUtility.MD5Encrypt(string.Format("{0};{1};{2};{3}", Convert.ToInt32(ReturnMessageCodeEnum.SuccessCode).ToString(), programGuid, userGuid, pageGuid), Constants.MD5_KEY);
                        }
                        else
                        {
                            if (isThereClassReturnCode == 0)
                            {
                                returnMessage = containerContext.Resolve<IProgramUserService>().GetUserClass(new Guid(userGuid), new Guid(programGuid), languageGuid);
                                // If returnMessage is empty, will throw "Root element is missing" exception
                                if (!string.IsNullOrEmpty(returnMessage))
                                {
                                    returnMessage = containerContext.Resolve<IXMLService>().PraseGraphPage(returnMessage, new Guid(userGuid), new Guid(sessionGuid), languageGuid, false);
                                    returnMessage = containerContext.Resolve<IXMLService>().ParseBeforeAfterShowExpression(returnMessage, new Guid(sessionGuid), "Session", programGuid, userGuid);
                                    returnMessage = containerContext.Resolve<IXMLService>().ParseTimePickerQuestion(returnMessage, languageGuid);
                                    returnMessage = Convert.ToInt32(ReturnMessageCodeEnum.SuccessCode).ToString() + ";" + returnMessage;
                                }

                                // add log
                                InsertLogModel logmodel = new InsertLogModel
                                {
                                    ActivityLogType = (int)LogTypeEnum.Login,
                                    Browser = context.Request.UserAgent,
                                    IP = context.Request.UserHostAddress,
                                    Message = "Login Sucessful",
                                    ProgramGuid = new Guid(programGuid),
                                    SessionGuid = new Guid(sessionGuid),
                                    UserGuid = new Guid(userGuid)
                                };
                                containerContext.Resolve<IActivityLogService>().Insert(logmodel);
                            }
                            // Need to pay
                            else if (isThereClassReturnCode == 6)
                            {
                                returnMessage = containerContext.Resolve<IProgramAccessoryService>().GetPaymentModelAsXML(new Guid(programGuid), languageGuid, new Guid(userGuid));
                                returnMessage = Convert.ToInt32(ReturnMessageCodeEnum.SuccessCode).ToString() + ";" + returnMessage;
                            }
                            else if (isThereClassReturnCode == 7)
                            {
                                returnMessage = Convert.ToInt32(ReturnMessageCodeEnum.ErrorCode).ToString() + ";" + LoginFailedTypeEnum.NoSchedule;
                            }
                            else if (isThereClassReturnCode == 8)
                            {
                                returnMessage = Convert.ToInt32(ReturnMessageCodeEnum.ErrorCode).ToString() + ";" + LoginFailedTypeEnum.NoScheduleForCurrentSession;
                            }
                            else if (isThereClassReturnCode == -1)// have error
                            {
                                returnMessage = Convert.ToInt32(ReturnMessageCodeEnum.ErrorCode).ToString() + ";" + LoginFailedTypeEnum.ErrorExist;
                            }
                            // 2: Have finished today's class
                            // 4: Complete
                            // 9: Complete but the program user status should be changed to complete 
                            else
                            {
                                if (isThereClassReturnCode == 9)
                                {
                                    // Set class stauts
                                    containerContext.Resolve<IProgramUserService>().SetUserClassStatus(hpSecurity, new Guid(userGuid), new Guid(programGuid), new Guid(sessionGuid));
                                }
                                if (programModel.IsCTPPEnable)
                                {
                                    returnMessage = Convert.ToInt32(ReturnMessageCodeEnum.CtppCode).ToString() + ";" + StringUtility.MD5Encrypt(string.Format("{0};{1};{2};{3}", Convert.ToInt32(ReturnMessageCodeEnum.SuccessCode).ToString(), programGuid, userGuid, pageGuid), Constants.MD5_KEY);
                                }
                                else
                                {
                                    returnMessage = containerContext.Resolve<IProgramAccessoryService>().GetSessionEndingPageModelAsXML(new Guid(programGuid), languageGuid, new Guid(userGuid));
                                    returnMessage = Convert.ToInt32(ReturnMessageCodeEnum.SessionEnding).ToString() + ";" + returnMessage;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex,
                    string.Format("Method Name:{0}, Request URL: {1}, User GUID: {2}, Program GUID: {3}, Session GUID: {4}; Page GUID: {5}",
                    "SessionEnding",
                    context.Request.Url.ToStringWithoutPort(),
                    userGuid,
                    programGuid,
                    sessionGuid,
                    pageGuid));

                string endingFailedMsg = containerContext.Resolve<ITipMessageService>().GetTipMessageText(new Guid(programGuid), "EndingFailed");
                returnMessage = Convert.ToInt32(ReturnMessageCodeEnum.ErrorCode).ToString() + ";" + endingFailedMsg;
            }

            return returnMessage;
        }

        private string SessionEndingByWin8Handler(string hpSecurity, string userGuid, string programGuid, string sessionGuid, Guid pageGuid, Guid languageGuid, HttpContext context, bool isRetake)
        {
            IContainerContext containerContext = ContainerManager.GetContainer("container");
            string returnMessage = string.Empty;

            try
            {
                ProgramModel programModel = containerContext.Resolve<IProgramService>().GetProgramByGUID(new Guid(programGuid));
                if (programModel != null)
                {
                    if (programModel.IsCTPPEnable && isRetake)
                    {
                        //if (pageGuid.ToString().Equals(Guid.Empty.ToString()))
                        //{
                        //    returnMessage = containerContext.Resolve<IProgramAccessoryService>().GetSessionEndingPageModelAsXML(new Guid(programGuid), languageGuid, new Guid(userGuid));
                        //    returnMessage = Convert.ToInt32(ReturnMessageCodeEnum.SuccessCode).ToString() + ";" + returnMessage;
                        //}
                        //else
                        //{
                        returnMessage = Convert.ToInt32(ReturnMessageCodeEnum.CtppCode).ToString() + ";" + StringUtility.MD5Encrypt(string.Format("{0};{1};{2};{3}", Convert.ToInt32(ReturnMessageCodeEnum.SuccessCode).ToString(), programGuid, userGuid, pageGuid), Constants.MD5_KEY);
                        //}
                    }
                    else
                    {
                        // if sessionGuid is null or empty, it's payment page won't update programuser and class status
                        if (!string.IsNullOrEmpty(sessionGuid))
                        {
                            // Set class stauts
                            containerContext.Resolve<IProgramUserService>().SetUserClassStatus(hpSecurity, new Guid(userGuid), new Guid(programGuid), new Guid(sessionGuid));

                            // Add log
                            InsertLogModel model = new InsertLogModel
                            {
                                ActivityLogType = (int)LogTypeEnum.EndDay,
                                Browser = context.Request.UserAgent,
                                IP = context.Request.UserHostAddress,
                                Message = "Session End By Win8 Application",
                                ProgramGuid = new Guid(programGuid),
                                SessionGuid = new Guid(sessionGuid),
                                UserGuid = new Guid(userGuid)
                            };
                            containerContext.Resolve<IActivityLogService>().Insert(model);
                            // If it tester, send daily email immediately
                            containerContext.Resolve<IProgramUserService>().SendEmailToTester(new Guid(userGuid), new Guid(programGuid));
                        }

                        //set current time accoding TimeZone.
                        DateTime? currentSessionDate = null;
                        DateTime setCurrentTimeByTimeZone = containerContext.Resolve<IProgramUserService>().GetCurrentTimeByTimeZone(new Guid(programGuid), new Guid(userGuid), DateTime.UtcNow);
                        ProgramUser pu = containerContext.Resolve<IProgramUserService>().GetProgramUserByProgramGuidAndUserGuid(new Guid(programGuid), new Guid(userGuid));
                        Session sessionEntity = containerContext.Resolve<ISessionRepository>().GetSessionByProgramGuidAndDay(new Guid(programGuid), 0);
                        if (sessionEntity != null)
                        {
                            ProgramUserSession puSession = containerContext.Resolve<IProgramUserSessionRepository>().GetProgramUserSessionByProgramUserGuidAndSessionGuid(pu.ProgramUserGUID, sessionEntity.SessionGUID);
                            if (puSession == null)
                            {
                                currentSessionDate = containerContext.Resolve<IProgramUserService>().ExpectSessionDate(new Guid(programGuid), new Guid(userGuid), 0).Value;
                            }
                            else
                            {
                                currentSessionDate = containerContext.Resolve<IProgramUserService>().ExpectSessionDate(new Guid(programGuid), new Guid(userGuid), pu.Day.Value + 1).Value;
                            }
                        }

                        if (programModel.IsCTPPEnable && programModel.IsNoCatchUp == true)
                        {
                            returnMessage = Convert.ToInt32(ReturnMessageCodeEnum.CtppCode).ToString() + ";" + StringUtility.MD5Encrypt(string.Format("{0};{1};{2};{3}", Convert.ToInt32(ReturnMessageCodeEnum.SuccessCode).ToString(), programGuid, userGuid, pageGuid), Constants.MD5_KEY);
                        }
                        else
                        {
                            if (currentSessionDate.Value.Date <= setCurrentTimeByTimeZone.Date)
                            {
                                returnMessage = containerContext.Resolve<IProgramUserService>().GetUserClass(new Guid(userGuid), new Guid(programGuid), languageGuid);
                                // If returnMessage is empty, will throw "Root element is missing" exception
                                if (!string.IsNullOrEmpty(returnMessage))
                                {
                                    returnMessage = containerContext.Resolve<IXMLService>().PraseGraphPage(returnMessage, new Guid(userGuid), new Guid(sessionGuid), languageGuid, false);
                                    returnMessage = containerContext.Resolve<IXMLService>().ParseBeforeAfterShowExpression(returnMessage, new Guid(sessionGuid), "Session", programGuid, userGuid);
                                    returnMessage = containerContext.Resolve<IXMLService>().ParseTimePickerQuestion(returnMessage, languageGuid);
                                    returnMessage = Convert.ToInt32(ReturnMessageCodeEnum.SuccessCode).ToString() + ";" + returnMessage;
                                }

                                // add log
                                InsertLogModel logmodel = new InsertLogModel
                                {
                                    ActivityLogType = (int)LogTypeEnum.Login,
                                    Browser = context.Request.UserAgent,
                                    IP = context.Request.UserHostAddress,
                                    Message = "Login Sucessful",
                                    ProgramGuid = new Guid(programGuid),
                                    SessionGuid = new Guid(sessionGuid),
                                    UserGuid = new Guid(userGuid)
                                };
                                containerContext.Resolve<IActivityLogService>().Insert(logmodel);
                            }
                            // 2: Have finished today's class
                            // 4: Complete
                            else
                            {
                                if (programModel.IsCTPPEnable)
                                {
                                    returnMessage = Convert.ToInt32(ReturnMessageCodeEnum.CtppCode).ToString() + ";" + StringUtility.MD5Encrypt(string.Format("{0};{1};{2};{3}", Convert.ToInt32(ReturnMessageCodeEnum.SuccessCode).ToString(), programGuid, userGuid, pageGuid), Constants.MD5_KEY);
                                }
                                else
                                {
                                    returnMessage = containerContext.Resolve<IProgramAccessoryService>().GetSessionEndingPageModelAsXML(new Guid(programGuid), languageGuid, new Guid(userGuid));
                                    returnMessage = Convert.ToInt32(ReturnMessageCodeEnum.SessionEnding).ToString() + ";" + returnMessage;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex,
                    string.Format("Method Name:{0}, Request URL: {1}, User GUID: {2}, Program GUID: {3}, Session GUID: {4}; Page GUID: {5}",
                    "SessionEnding",
                    context.Request.Url.ToStringWithoutPort(),
                    userGuid,
                    programGuid,
                    sessionGuid,
                    pageGuid));

                string endingFailedMsg = containerContext.Resolve<ITipMessageService>().GetTipMessageText(new Guid(programGuid), "EndingFailed");
                returnMessage = Convert.ToInt32(ReturnMessageCodeEnum.ErrorCode).ToString() + ";" + endingFailedMsg;
            }

            return returnMessage;
        }

        private string RecordPageActiviryLog(string userGuid, string programGuid, string sessionGuid, string pageSequenceGuid, string pageGuid, HttpContext context, LogTypeEnum activityLogType)
        {
            IContainerContext containerContext = ContainerManager.GetContainer("container");
            string returnMessage = string.Empty;
            try
            {
                // add log
                InsertLogModel model = new InsertLogModel
                {
                    ActivityLogType = (int)activityLogType,
                    Browser = context.Request.UserAgent,
                    IP = context.Request.UserHostAddress,
                    Message = activityLogType.ToString(),
                    PageGuid = string.IsNullOrEmpty(pageGuid) ? Guid.Empty : new Guid(pageGuid),
                    ProgramGuid = new Guid(programGuid),
                    SessionGuid = string.IsNullOrEmpty(sessionGuid) ? Guid.Empty : new Guid(sessionGuid),
                    UserGuid = string.IsNullOrEmpty(userGuid) ? Guid.Empty : new Guid(userGuid),
                    PageSequenceGuid = string.IsNullOrEmpty(pageSequenceGuid) ? Guid.Empty : new Guid(pageSequenceGuid)
                };

                containerContext.Resolve<IActivityLogService>().Insert(model);
                returnMessage = Convert.ToInt32(ReturnMessageCodeEnum.SuccessCode).ToString() + ";OK";
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, string.Format("Method Name:{0}, Page Guid",
                        "RecordPageActiviryLog", pageGuid));
                returnMessage = Convert.ToInt32(ReturnMessageCodeEnum.ErrorCode).ToString() + ";Internal error.";
            }
            return returnMessage;
        }

        //root node for IsSMSToEmail
        private string SetSMSToEmailForProgramUser(bool isSmsToEmail, Guid programGuid, Guid userGuid, HttpContext context)
        {
            string returnMessage = string.Empty;
            IContainerContext containerContext = ContainerManager.GetContainer("container");
            try
            {
                //Get IsSMSToEmail value and update ProgramUser.
                ProgramUserModel programUserModel = new ProgramUserModel()
                {
                    ProgramGUID = programGuid,
                    UserGUID = userGuid,
                    IsSMSToEmail = isSmsToEmail
                };
                //update programUser's IsSMSToEmail Attribute.
                //containerContext.Resolve<IProgramUserService>().UpdateProgramUserSMSToEmail(programUserModel);
                containerContext.Resolve<IProgramUserService>().UpdateProgramUserProperty(programUserModel);

                // add log
                InsertLogModel model = new InsertLogModel
                {
                    ActivityLogType = (int)LogTypeEnum.ModifyProgramUser,
                    IP = context.Request.UserHostAddress,
                    Browser = context.Request.UserAgent,
                    Message = "Set SMS To Email",
                    ProgramGuid = programGuid,
                    UserGuid = userGuid,
                    PageGuid = Guid.Empty,
                    PageSequenceGuid = Guid.Empty,
                    SessionGuid = Guid.Empty,
                    From = string.Empty
                };
                containerContext.Resolve<IActivityLogService>().Insert(model);
                returnMessage = Convert.ToInt32(ReturnMessageCodeEnum.SuccessCode).ToString() + ";OK";

            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, string.Format("Method Name:{0}, Output {1}", "SetSMSToEmailForProgramUser", returnMessage));
                returnMessage = Convert.ToInt32(ReturnMessageCodeEnum.ErrorCode).ToString() + ";Internal error.";
            }

            return returnMessage;
        }

        private string HPOrderLicenceRegistHandler(RegisterProgramUserModel registerProgramUser, string hpSecurity, string programGuid, string userGuid)
        {
            string returnMessage = string.Empty;
            IContainerContext containerContext = ContainerManager.GetContainer("container");
            try
            {
                string[] logstr = StringUtility.MD5Decrypt(hpSecurity, Constants.MD5_KEY).Split(';');
                if (logstr.Length >= 4)
                {
                    string hpOrderGuidStr = logstr[3];
                    if (!string.IsNullOrEmpty(hpOrderGuidStr))
                    {
                        Guid hpOrderGuid = new Guid(hpOrderGuidStr);

                        ValidateOrderLicenceResponseModel responseModel = containerContext.Resolve<IHPOrderLicenceService>().ValidateHPOrderLicence(hpOrderGuid, new Guid(programGuid));
                        switch (responseModel.Result)
                        {
                            case ResultEnum.Success:
                                HPOrderModel orderModel = containerContext.Resolve<IHPOrderService>().GetHPOrderModelByHPOrderGuid(hpOrderGuid);
                                ProgramUser pu = containerContext.Resolve<IProgramUserService>().GetProgramUserByProgramGuidAndUserGuid(orderModel.ProgramGUID, registerProgramUser.UserGuid);
                                returnMessage = Register(registerProgramUser);

                                if (orderModel != null && pu != null)
                                {
                                    //insert data to HPOrderLicence table
                                    HPOrderLicenceModel orderLicenceModel = new HPOrderLicenceModel
                                    {
                                        HPOrderLicenceGUID = Guid.NewGuid(),
                                        HPOrderGUID = orderModel.HPOrderGUID,
                                        ProgramUserGUID = pu.ProgramUserGUID
                                    };
                                    containerContext.Resolve<IHPOrderLicenceService>().Insert(orderLicenceModel);
                                }
                                break;
                            case ResultEnum.Error:
                                ProgramModel programModel = containerContext.Resolve<IProgramService>().GetProgramByGUID(new Guid(programGuid));
                                if (programModel.IsCTPPEnable)
                                {
                                    returnMessage = string.Format("{0};{1};{2};{3}", Convert.ToInt32(ReturnMessageCodeEnum.ErrorCode).ToString(), programGuid, userGuid, responseModel.LoginFailedType);
                                    returnMessage = Convert.ToInt32(ReturnMessageCodeEnum.CtppCode).ToString() + ";" + StringUtility.MD5Encrypt(returnMessage, Constants.MD5_KEY);
                                }
                                else
                                {
                                    returnMessage = Convert.ToInt32(ReturnMessageCodeEnum.ErrorCode).ToString() + ";" + containerContext.Resolve<ITipMessageService>().GetTipMessageText(new Guid(programGuid), responseModel.LoginFailedType.ToString());
                                }
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, string.Format("Method Name:{0}, Output {1}", "OrderLicenceRegistHandler", returnMessage));
                returnMessage = Convert.ToInt32(ReturnMessageCodeEnum.ErrorCode).ToString() + ";Internal error.";
            }

            return returnMessage;
        }

        private string OrderLicenceRegistHandler(RegisterProgramUserModel registerProgramUser, string security, string programGuid, string userGuid)
        {
            string returnMessage = string.Empty;
            IContainerContext containerContext = ContainerManager.GetContainer("container");
            try
            {
                string[] logstr = StringUtility.MD5Decrypt(security, Constants.MD5_KEY).Split(';');
                if (logstr.Length >= 4)
                {
                    string orderContentGuidStr = logstr[3];
                    if (!string.IsNullOrEmpty(orderContentGuidStr))
                    {
                        Guid orderContentGuid = new Guid(orderContentGuidStr);
                        ValidateOrderLicenceResponseModel responseModel = containerContext.Resolve<IOrderLicenceService>().ValidateOrderLicence(orderContentGuid, new Guid(programGuid));
                        switch (responseModel.Result)
                        {
                            case ResultEnum.Success:
                                OrderContentModel ocModel = containerContext.Resolve<IOrderContentService>().GetOrderContentByOrderContentGuid(orderContentGuid);
                                ProgramUser pu = containerContext.Resolve<IProgramUserService>().GetProgramUserByProgramGuidAndUserGuid(ocModel.ProgramGUID, registerProgramUser.UserGuid);
                                returnMessage = Register(registerProgramUser);

                                if (ocModel != null && pu != null)
                                {
                                    OrderLicenceModel olModel = containerContext.Resolve<IOrderLicenceService>().GetOrderLicenceByOrderContentGuidAndProgramUserGuid(ocModel.OrderContentGUID, pu.ProgramUserGUID);
                                    if (olModel == null)
                                    {
                                        //insert data to OrderLicence table
                                        OrderLicenceModel orderLicenceModel = new OrderLicenceModel
                                        {
                                            OrderLicenceGUID = Guid.NewGuid(),
                                            OrderContentGUID = ocModel.OrderContentGUID,
                                            ProgramUserGUID = pu.ProgramUserGUID,
                                            UpdatedBy = ocModel.UpdatedBy,
                                            LastRegisted = containerContext.Resolve<IOrderLicenceService>().GetLastRegistedUserByOrderContentGuidAndProgramGuid(orderContentGuid, ocModel.ProgramGUID).LastRegisted
                                        };
                                        containerContext.Resolve<IOrderLicenceService>().InsertOrderLicence(orderLicenceModel);
                                    }
                                }
                                #region promotion handler
                                //if (ocModel != null && pu != null)
                                //{
                                //    // isPromotion is true : update OrderLicence table's licence.
                                //    if (isPromotion)
                                //    {
                                //        //update OrderLicence table's data
                                //        OrderLicenceModel orderLicenceModel = containerContext.Resolve<IOrderLicenceService>().GetOrderLicenceByOrderContentGuid(ocModel.OrderContentGUID);
                                //        if (orderLicenceModel != null)
                                //        {
                                //            orderLicenceModel.ProgramUserGUID = pu.ProgramUserGUID;
                                //            containerContext.Resolve<IOrderLicenceService>().UpdateOrderLicence(orderLicenceModel);
                                //        }
                                //    }
                                //    // isPromotion is false : insert a licence log to OrderLicence table.
                                //    else
                                //    {
                                //        //insert data to OrderLicence table
                                //        OrderLicenceModel orderLicenceModel = new OrderLicenceModel
                                //        {
                                //            OrderLicenceGUID = Guid.NewGuid(),
                                //            OrderContentGUID = ocModel.OrderContentGUID,
                                //            ProgramUserGUID = pu.ProgramUserGUID,
                                //            PromotionCode = null,
                                //            UpdatedBy = ocModel.UpdatedBy
                                //        };
                                //        containerContext.Resolve<IOrderLicenceService>().InsertOrderLicence(orderLicenceModel);
                                //    }
                                //} 
                                #endregion
                                break;
                            case ResultEnum.Error:
                                ProgramModel programModel = containerContext.Resolve<IProgramService>().GetProgramByGUID(new Guid(programGuid));
                                if (programModel.IsCTPPEnable)
                                {
                                    returnMessage = string.Format("{0};{1};{2};{3}", Convert.ToInt32(ReturnMessageCodeEnum.ErrorCode).ToString(), programGuid, userGuid, responseModel.LoginFailedType);
                                    returnMessage = Convert.ToInt32(ReturnMessageCodeEnum.CtppCode).ToString() + ";" + StringUtility.MD5Encrypt(returnMessage, Constants.MD5_KEY);
                                }
                                else
                                {
                                    returnMessage = Convert.ToInt32(ReturnMessageCodeEnum.ErrorCode).ToString() + ";" + containerContext.Resolve<ITipMessageService>().GetTipMessageText(new Guid(programGuid), responseModel.LoginFailedType.ToString());
                                }
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, string.Format("Method Name:{0}, Output {1}", "OrderLicenceRegistHandler", returnMessage));
                returnMessage = Convert.ToInt32(ReturnMessageCodeEnum.ErrorCode).ToString() + ";Internal error.";
            }

            return returnMessage;
        }


        private string GetGraphData(string userGuid, string programGuid, Guid languageGuid, string sessionGuid, string pageSequenceGuid, string pageGuid)
        {
            string returnMessage = string.Empty;
            IContainerContext containerContext = ContainerManager.GetContainer("container");
            returnMessage = containerContext.Resolve<IPageService>().GetPageGraphData(new Guid(pageGuid));
            if (!string.IsNullOrEmpty(returnMessage))
            {
                returnMessage = containerContext.Resolve<IXMLService>().PraseGraphForOnePage(returnMessage, new Guid(userGuid), new Guid(sessionGuid), languageGuid);
                if (!string.IsNullOrEmpty(returnMessage))
                {
                    returnMessage = "1;" + returnMessage;
                }
                else
                {
                    returnMessage = "0;ERROR";
                }
            }
            else
            {
                returnMessage = "0;ERROR";
            }

            return returnMessage;
        }
        #endregion
    }
}
