using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using ChangeTech.Contracts;
using ChangeTech.Models;
using Ethos.DependencyInjection;
using Ethos.Utility;

namespace ChangeTech.DeveloperWeb
{
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    public class ManageDailyReportSMSService : ServiceBase
    {
        // To use HTTP GET, add [WebGet] attribute. (Default ResponseFormat is WebMessageFormat.Json)
        // To create an operation that returns XML,
        //     add [WebGet(ResponseFormat=WebMessageFormat.Xml)],
        //     and include the following line in the operation body:
        //         WebOperationContext.Current.OutgoingResponse.ContentType = "text/xml";


        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        public string GetProgramDailySMSTime(string ProgramGuid)
        {
            Guid thisProgramGuid = Guid.Empty;
            string time = string.Empty;
            if (Guid.TryParse(ProgramGuid, out thisProgramGuid))
            {
                int? minutes = Resolve<IProgramService>().GetProgramDailySMSTime(thisProgramGuid);
                if (minutes != null)
                {
                    time = (minutes / 60).ToString() + ":" + ((minutes % 60) < 10 ? string.Format("0{0}", (minutes % 60)) : (minutes % 60).ToString());// HH:mm
                }
            }
            return time;
        }

        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        public bool UpdateProgramDailySMSTime(string ProgramGuid, string DailySMSTime)
        {
            bool flag = false;//The validation result.
            //DailySMSTime must be null||""  , or must be HH:mm||H:m||H:mm||HH:m 
            Guid thisProgramGuid = Guid.Empty;
            int? time = null;
            bool validation = false;
            if (Guid.TryParse(ProgramGuid, out thisProgramGuid))
            {
                DailySMSTime = DailySMSTime.Trim();
                if (!string.IsNullOrEmpty(DailySMSTime))
                {
                    int HH = 0, mm = 0;
                    string[] strSplitTime = DailySMSTime.Split(':');
                    if (strSplitTime.Length == 2)
                    {
                        if (int.TryParse(strSplitTime[0], out HH) && int.TryParse(strSplitTime[1], out mm))
                        {
                            time = HH * 60 + mm;
                            validation = true;
                        }
                    }
                }
                else
                {
                    time = null;
                    validation = true;
                }
                if (validation)
                    Resolve<IProgramService>().UpdateProgramDailySMSTime(thisProgramGuid, time);
            }
            flag = validation;
            return flag;
        }

        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        public List<DailySMSContentModel> GetDailySMSContentList(string ProgramGuid)
        {
            List<DailySMSContentModel> dailySMSModelList = new List<DailySMSContentModel>();
            Guid thisProgramGuid = Guid.Empty;
            if (Guid.TryParse(ProgramGuid, out thisProgramGuid))
            {
                dailySMSModelList = Resolve<IShortMessageService>().GetDailySMSContentList(thisProgramGuid);
            }
            return dailySMSModelList;
        }

        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        public void UpdateProgramDailySMSContentBySessionGuid(string SessionGuid, string NewContent)
        {
            Guid thisSessionGuid = Guid.Empty;
            if (Guid.TryParse(SessionGuid, out thisSessionGuid))
            {
                Resolve<IShortMessageService>().UpdateProgramDailySMSContentBySessionGuid(thisSessionGuid, NewContent);
            }
        }

        // Add more operations here and mark them with [OperationContract]
    }
}
