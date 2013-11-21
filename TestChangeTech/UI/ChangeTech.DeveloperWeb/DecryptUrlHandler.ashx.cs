using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ethos.Utility;
using ChangeTech.Models;
using System.Web.Script.Serialization;
using ChangeTech.Contracts;
using Ethos.DependencyInjection;

namespace ChangeTech.DeveloperWeb.Handler
{
    /// <summary>
    /// Summary description for DecryptUrlHandler
    /// </summary>
    public class DecryptUrlHandler : IHttpHandler
    {
        public static readonly string FAIL = "fail";
        public static readonly string MD5_KEY = "psycholo";
        public void ProcessRequest(HttpContext context)
        {
            IContainerContext containerContext = ContainerManager.GetContainer("container");
            try
            {
                //Get encrypt string from LF-Project's handler.
                string decryptStrs = string.Empty;
                string encryptStr = System.Web.HttpUtility.UrlDecode(context.Request.Params["encryptStr"]);
                if (!string.IsNullOrEmpty(encryptStr))
                {
                    decryptStrs = StringUtility.MD5Decrypt(encryptStr, Constants.MD5_KEY);
                }
                context.Response.ContentType = "text/javascript";
                context.Response.Cache.SetNoStore();
                context.Response.Write(decryptStrs);
            }
            catch (Exception ex)
            {
                InsertLogModel insertLogModel = new InsertLogModel
                {
                    ActivityLogType = (int)LogTypeEnum.DecryptGoWebURL,
                    Browser = string.Empty,
                    From = string.Empty,
                    IP = string.Empty,
                    Message = string.Format("DecryptGoWebURLException : {0}", ex),
                    PageGuid = Guid.Empty,
                    PageSequenceGuid = Guid.Empty,
                    SessionGuid = Guid.Empty,
                    ProgramGuid = Guid.Empty,
                    UserGuid = Guid.Empty
                };
                containerContext.Resolve<IActivityLogService>().Insert(insertLogModel);
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                context.Response.ContentType = "text/xml";
                context.Response.Write(FAIL);
            }
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
                    string variableValue = variableInfo.Substring(index+1);
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

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}