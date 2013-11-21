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
using System.Data;

namespace ChangeTech.DeveloperWeb.Services
{
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class MonitorService : ServiceBase
    {
        // To use HTTP GET, add [WebGet] attribute. (Default ResponseFormat is WebMessageFormat.Json)
        // To create an operation that returns XML,
        //     add [WebGet(ResponseFormat=WebMessageFormat.Xml)],
        //     and include the following line in the operation body:
        //         WebOperationContext.Current.OutgoingResponse.ContentType = "text/xml";
        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        public List<ProgramUserReportModel> GetProgramUserReport()
        {

            // Add your operation implementation here
            return Resolve<IReportService>().GetProgramUserReport();
        }

        // Add more operations here and mark them with [OperationContract]
    }
}
