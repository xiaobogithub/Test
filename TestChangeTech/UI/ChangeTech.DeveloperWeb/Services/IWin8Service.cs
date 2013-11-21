using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using ChangeTech.Models;

namespace ChangeTech.DeveloperWeb.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IWin8Service
    {
        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<ProgramCategoryModel> GetPrograms(string windowsLiveId, string applicationId);
        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        ProgramContentModel GetProgram(string windowsLiveId, string applicationId, string programGuidStr);
        //[OperationContract]
        //[WebInvoke(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        //List<SessionInfoModel> GetSessions(string windowsLiveId, string applicationId, string programGuidStr);
        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        RegisterMessageModel RegisterProgramUsers(RegisterWin8ProgramUsersModel registerProgramUsersModel);
    }
}