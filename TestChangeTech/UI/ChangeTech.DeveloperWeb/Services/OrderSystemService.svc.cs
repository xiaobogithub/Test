using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using Ethos.DependencyInjection;
using Ethos.Utility;
using ChangeTech.Contracts;
using ChangeTech.Models;

namespace ChangeTech.DeveloperWeb.Services
{
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class OrderSystemService : ServiceBase
    {
        // To use HTTP GET, add [WebGet] attribute. (Default ResponseFormat is WebMessageFormat.Json)
        // To create an operation that returns XML,
        //     add [WebGet(ResponseFormat=WebMessageFormat.Xml)],
        //     and include the following line in the operation body:
        //         WebOperationContext.Current.OutgoingResponse.ContentType = "text/xml";

        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        public List<SimpleProgramModel> GetOrderPrograms(string LanguageGuid)
        {
            List<SimpleProgramModel> programModels = null;
            Guid languageGuid = Guid.Empty;
            try
            {
                if (Guid.TryParse(LanguageGuid, out languageGuid))
                {
                    programModels = Resolve<IProgramService>().GetOrderProgramsByLanguageGuidAndProgramPublishStatusGuid(languageGuid);
                    //programModels = Resolve<IProgramService>().GetProgramsByLanguageGuid(languageGuid);
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
