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

namespace ChangeTech.DeveloperWeb
{
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    public class PageReviewService : ServiceBase
    {
        // To use HTTP GET, add [WebGet] attribute. (Default ResponseFormat is WebMessageFormat.Json)
        // To create an operation that returns XML,
        //     add [WebGet(ResponseFormat=WebMessageFormat.Xml)],
        //     and include the following line in the operation body:
        //         WebOperationContext.Current.OutgoingResponse.ContentType = "text/xml";
        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        public List<SimplePageContentModel> GetPagesOfSession(string sessionId)
        {
            Guid sessionGuid = new Guid(sessionId);
            List<SimplePageContentModel> simplePageContentsModel = null;
            try
            {
                simplePageContentsModel = Resolve<IPageService>().GetPagesOfSession(sessionGuid);
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
            return simplePageContentsModel;
        }

        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        public List<PageTemplateModel> GetPageTemplates()
        {
            List<PageTemplateModel> pageTemplatesModel = null;
            try
            {
                pageTemplatesModel = Resolve<IPageService>().GetPageTemplates();
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
            return pageTemplatesModel;
        }

        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        public ChangeTechResponse SavePage(SimplePageContentModel simplePageContentModel)
        {
            ChangeTechResponse response = new ChangeTechResponse();
            try
            {
                Resolve<IPageService>().SavePage(simplePageContentModel);
                return response;

            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        public ChangeTechResponse UpdatePageContentForPageReview(PageUpdateForPageReviewModel pageContent)
        {
            ChangeTechResponse response = new ChangeTechResponse();
            try
            {
                Resolve<IPageService>().UpdatePageContentForPageReview(pageContent);
                return response;

            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        public bool IsPageHasMoreReference(string sessionId, string pageSequenceId)
        {
            Guid sessionGuid = new Guid(sessionId);
            Guid pageSequenceGuid = new Guid(pageSequenceId);
            bool isPageHasMoreReference = false;
            try
            {
                isPageHasMoreReference = Resolve<IPageService>().IsPageHasMoreReference(sessionGuid, pageSequenceGuid);
                return isPageHasMoreReference;
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        public ChangeTechResponse DeletePageForPageReview(PageUpdateForPageReviewModel deleteModel)
        {
            ChangeTechResponse response = new ChangeTechResponse();
            try
            {
                Resolve<IPageService>().DeletePageForPageReview(deleteModel);
                return response;
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        public ChangeTechResponse AdjustPageOrderForPageReview(PageUpdateForPageReviewModel originalModel, PageUpdateForPageReviewModel swapToModel)
        {
            ChangeTechResponse response = new ChangeTechResponse();
            try
            {
                Resolve<IPageService>().AdjustPageOrderForPageReview(originalModel, swapToModel);
                return response;
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }
        // Add more operations here and mark them with [OperationContract]
    }

    [DataContract]
    public class ChangeTechResponse
    {
        ChangeTechResultEnum result = ChangeTechResultEnum.Success;
        string errorMessage = string.Empty;

        public ChangeTechResponse()
        {
            this.result = 0;
            this.errorMessage = string.Empty;
        }

        public ChangeTechResponse(ChangeTechResultEnum result, string errorMessage)
        {
            this.result = result;
            this.errorMessage = errorMessage;
        }

        [DataMember]
        public ChangeTechResultEnum Result
        {
            get { return result; }
            internal set { result = value; }
        }

        [DataMember]
        public string ErrorMessage
        {
            get { return errorMessage; }
            internal set { errorMessage = value; }
        }

    }

    public enum ChangeTechResultEnum
    {
        Success = 0,            // no error
        Error = 1
    }
}
