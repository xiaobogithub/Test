using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using ChangeTech.Contracts;
using ChangeTech.Models;
using Ethos.DependencyInjection;
using Ethos.Utility;
using System.Web;
using System.IO;

namespace ChangeTech.DeveloperWeb
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ChangeTechService" in code, svc and config file together.
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    public class ChangeTechService : ServiceBase
    {
        public const string SpeSNameOfNotSupportHTML5 = "Not_Support_HTML5";

        // To use HTTP GET, add [WebGet] attribute. (Default ResponseFormat is WebMessageFormat.Json)
        // To create an operation that returns XML,
        //     add [WebGet(ResponseFormat=WebMessageFormat.Xml)],
        //     and include the following line in the operation body:
        //         WebOperationContext.Current.OutgoingResponse.ContentType = "text/xml";
        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        public int GetChoiceOfFlashOrHtml5(string ProgramCode)
        {
            int Choice = (int)FlashOrHtml5Enum.FlashOnly;
            try
            {
                Guid programGuid = Guid.Empty;
                //In the current version, the qureystring in the screen url is program code, but in old versions this is programGuid.
                //To insure the old screen url can work, wo must judge the parameter here. //2012-02-10
                if (!Guid.TryParse(ProgramCode, out programGuid))//ProgramCode parameter is 6-byte program code string, not programguid.
                {
                    programGuid = Resolve<IProgramService>().GetProgramGUIDByProgramCode(ProgramCode);
                }
                ProgramPropertyModel programModel = Resolve<IProgramService>().GetProgramProperty(programGuid);
                if (programModel != null)
                {
                    Choice = programModel.FlashOrHTML5;
                }
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
            return Choice;
        }


        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        public RedirectInfoModel GetRedirectInfo(ParametersForRedirectInfoModel RedirectParameterModel)
        {
            RedirectInfoModel redirectInfoModel = new RedirectInfoModel();
            int Choice = (int)RedirectChoiceEnum.FlashOnly;
            bool isHtml5UIEnable = false;
            string RedirectToCTPPURL = "";
            try
            {
                Guid programGuid = Guid.Empty;
                //In the current version, the qureystring in the screen url is program code, but in old versions this is programGuid.
                //To insure the old screen url can work, wo must judge the parameter here. //2012-02-10
                if (!Guid.TryParse(RedirectParameterModel.ProgramCodeOrGuid, out programGuid))//ProgramCode parameter is 6-byte program code string, not programguid.
                {
                    programGuid = Resolve<IProgramService>().GetProgramGUIDByProgramCode(RedirectParameterModel.ProgramCodeOrGuid);
                }

                #region to judge whether user have done all classes and need go to CTPP
                if (!string.IsNullOrEmpty(RedirectParameterModel.Mode) && RedirectParameterModel.Mode.ToLower() == "live" && !string.IsNullOrEmpty(RedirectParameterModel.Security))
                {
                    string[] userInfo = StringUtility.MD5Decrypt(RedirectParameterModel.Security, Constants.MD5_KEY).Split(';');
                    if (userInfo.Length == 2)//else , userInfo.Length==3, the third is day number, and this is "retake"
                    {
                        string userName = userInfo[0];
                        string password = userInfo[1];

                        string returnMessage = Resolve<IUserService>().EndUserLogin(userName, password, programGuid.ToString());
                        string[] logedStr = returnMessage.Split(';');
                        if (logedStr.Length > 1 && logedStr[0] == "0" && logedStr[1].ToLower() == LoginFailedTypeEnum.HaveDoneAllClassed.ToString().ToLower())
                        {
                            if (Resolve<IProgramService>().IsProgramCTPPEnable(programGuid))
                            {
                                UserModel userModel = Resolve<IUserService>().GetUserByUserName(userName, programGuid);
                                if (userModel != null)
                                {
                                    RedirectToCTPPURL = "CTPP.aspx?CTPP=" + StringUtility.MD5Encrypt(string.Format("{0};{1};{2};{3}", Convert.ToInt32(ReturnMessageCodeEnum.SuccessCode).ToString(), programGuid, userModel.UserGuid, Guid.Empty), Constants.MD5_KEY);
                                    Choice = (int)RedirectChoiceEnum.ToCTPPWithQueryStringCTPP;
                                }
                            }
                        }
                    }
                }
                #endregion

                //IF !=, it means the user have not done all classes, so need redirect to Flash or Html5
                if (Choice != (int)RedirectChoiceEnum.ToCTPPWithQueryStringCTPP)
                {
                    ProgramPropertyModel programModel = Resolve<IProgramService>().GetProgramProperty(programGuid);
                    if (programModel != null)
                    {
                        Choice = programModel.FlashOrHTML5;
                        isHtml5UIEnable = programModel.EnableHTML5NewUI;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }

            redirectInfoModel.RedirectChoice = (RedirectChoiceEnum)Choice;
            redirectInfoModel.RedirectToCTPPURL = RedirectToCTPPURL;
            redirectInfoModel.IsNewHtml5UIEnable = isHtml5UIEnable;
            return redirectInfoModel;
        }


        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        public string GetSpecialStringValueForHtml5Incompatible(string ProgramCode)
        {
            string SSValue = "";
            try
            {
                Guid programGuid = Resolve<IProgramService>().GetProgramGUIDByProgramCode(ProgramCode);
                ProgramModel programModel = Resolve<IProgramService>().GetProgramByGUID(programGuid);
                SpecialStringModel SpeSModel = Resolve<ISpecialStringService>().GetSpecialStringBy(SpeSNameOfNotSupportHTML5, programModel.DefaultLanguage);
                SSValue = SpeSModel.Value;
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
            return SSValue;
        }

        //Get TimeZone's DropDownList options.
        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        public string GetTimeZoneOpts(string ProgramGuid)
        {
            string timeZoneOpts = string.Empty;
            Guid thisProgramGuid = Guid.Empty;
            try
            {
                if (Guid.TryParse(ProgramGuid, out thisProgramGuid))
                {
                    timeZoneOpts = Resolve<IXMLService>().GetTimeZoneOpts(thisProgramGuid);
                }
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
            return timeZoneOpts;
        }


        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        public List<SimpleProgramModel> GetOrderContents(string LanguageGuid)
        {
            List<SimpleProgramModel> programModels = null;
            Guid languageGuid = Guid.Empty;
            try
            {
                if (Guid.TryParse(LanguageGuid, out languageGuid))
                {
                    programModels = Resolve<IProgramService>().GetProgramsByLanguageGuid(languageGuid);
                }
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
            return programModels;
        }
        // Add more operations here and mark them with [OperationContract]
    }
}
