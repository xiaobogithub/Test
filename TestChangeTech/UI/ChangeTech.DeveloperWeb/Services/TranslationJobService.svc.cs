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
    public class TranslationJobService : ServiceBase
    {
        // To use HTTP GET, add [WebGet] attribute. (Default ResponseFormat is WebMessageFormat.Json)
        // To create an operation that returns XML,
        //     add [WebGet(ResponseFormat=WebMessageFormat.Xml)],
        //     and include the following line in the operation body:
        //         WebOperationContext.Current.OutgoingResponse.ContentType = "text/xml";
        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        public List<TranslationJobContentModel> GetTranslationJobContent(string translationJobId)
        {
            Guid translationJobGuid = new Guid(translationJobId);
            List<TranslationJobContentModel> TranslationJobContentsModel = null;
            try
            {
                TranslationJobContentsModel = Resolve<ITranslationJobService>().GetTranslationJobContentList(translationJobGuid);
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
            return TranslationJobContentsModel;
        }

        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        public ChangeTechResponse UpdateTranslationJobContent(TranslationJobContentModel jobContent)
        {
            ChangeTechResponse response = new ChangeTechResponse();
            try
            {
                Resolve<ITranslationJobService>().UpdateTranslationJobContent(jobContent);
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
        public List<TranslationJobElementModel> GetTranslationJobElement(string translationJobContentId)
        {
            Guid translationJobContentGuid = new Guid(translationJobContentId);
            List<TranslationJobElementModel> TranslationJobElementsModel = null;
            try
            {
                TranslationJobElementsModel = Resolve<ITranslationJobService>().GetTranslationJobElementList(translationJobContentGuid);
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
            return TranslationJobElementsModel;
        }

        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        public ChangeTechResponse UpdateTranslationJobTranslated(TranslationJobElementModel jobTranslated)
        {
            ChangeTechResponse response = new ChangeTechResponse();
            try
            {
                Resolve<ITranslationJobService>().UpdateTranslationJobElement(jobTranslated);
                LogUtility.LogUtilityIntance.LogMessage(string.Format("{0};{1};{2};{3};{4}", jobTranslated.Object, jobTranslated.ToObjectGUID, jobTranslated.Position, jobTranslated.Translated, DateTime.UtcNow.ToString()));
                switch (jobTranslated.Object)
                {
                    case "Program"://DTD-244 sub-task1
                        Resolve<ITranslationService>().TranslateProgram(new Guid(jobTranslated.ToObjectGUID), jobTranslated.Position, jobTranslated.Translated);
                        break;
                    case "CTPP"://DTD-244 sub-task5
                        Resolve<ITranslationService>().TranslateCTPP(new Guid(jobTranslated.ToObjectGUID), jobTranslated.Position, jobTranslated.Translated);
                        break;
                    case "PageContent":
                        Resolve<ITranslationService>().TranslatePageContent(new Guid(jobTranslated.ToObjectGUID), jobTranslated.Position, jobTranslated.Translated);
                        break;
                    case "PageQuestionContent":
                        Resolve<ITranslationService>().TranslatePageQuestionContent(new Guid(jobTranslated.ToObjectGUID), jobTranslated.Position, jobTranslated.Translated);
                        break;
                    case "PageQuestionItemContent":
                        Resolve<ITranslationService>().TranslatePageQuestionItemContent(new Guid(jobTranslated.ToObjectGUID), jobTranslated.Position, jobTranslated.Translated);
                        break;
                    case "ProgramRoom":
                        Resolve<ITranslationService>().TranslateProgramRoom(new Guid(jobTranslated.ToObjectGUID), jobTranslated.Position, jobTranslated.Translated);
                        break;
                    case "GraphContent":
                        Resolve<ITranslationService>().TranslateGraphContent(new Guid(jobTranslated.ToObjectGUID), jobTranslated.Position, jobTranslated.Translated);
                        break;
                    case "EmailTemplate":
                        Resolve<ITranslationService>().TranslateEmailTemplate(new Guid(jobTranslated.ToObjectGUID), jobTranslated.Position, jobTranslated.Translated);
                        break;
                    case "GraphItemContent":
                        Resolve<ITranslationService>().TranslateGraphItemContent(new Guid(jobTranslated.ToObjectGUID), jobTranslated.Position, jobTranslated.Translated);
                        break;
                    case "HelpItem":
                        Resolve<ITranslationService>().TranslateHelpItem(new Guid(jobTranslated.ToObjectGUID), jobTranslated.Position, jobTranslated.Translated);
                        break;
                    case "Preferences":
                        Resolve<ITranslationService>().TranslatePreference(new Guid(jobTranslated.ToObjectGUID), jobTranslated.Position, jobTranslated.Translated);
                        break;
                    case "Session":
                        Resolve<ITranslationService>().TranslateSession(new Guid(jobTranslated.ToObjectGUID), jobTranslated.Position, jobTranslated.Translated);
                        break;
                    case "SpecialString":
                        // TODO get to languageguid
                        Guid toLanguageGuid = Resolve<ITranslationJobService>().GetToLanguageGuidFromTransContentGuid(jobTranslated.TranslationJobContentGUID);
                        Resolve<ITranslationService>().TranslateSepcialString(jobTranslated.ToObjectGUID, toLanguageGuid, jobTranslated.Position, jobTranslated.Translated);
                        break;
                    case "TipMessage":
                        Resolve<ITranslationService>().TranslateTipMessage(new Guid(jobTranslated.ToObjectGUID), jobTranslated.Position, jobTranslated.Translated);
                        break;
                    case "UserMenu":
                        Resolve<ITranslationService>().TranslateUserMenu(new Guid(jobTranslated.ToObjectGUID), jobTranslated.Position, jobTranslated.Translated);
                        break;
                    case "AccessoryTemplate":
                        Resolve<ITranslationService>().TranslateAccessoryTemplate(new Guid(jobTranslated.ToObjectGUID), jobTranslated.Position, jobTranslated.Translated);
                        break;
                    case "Relapse":
                        break;
                    case "PageSequence":
                        Resolve<ITranslationService>().TranslatePageSequence(new Guid(jobTranslated.ToObjectGUID), jobTranslated.Position, jobTranslated.Translated);
                        break;
                    case "ScreenResultTemplatePageLine":
                        Resolve<ITranslationService>().TranslateScreenResultTemplatePageLine(new Guid(jobTranslated.ToObjectGUID), jobTranslated.Position, jobTranslated.Translated);
                        break;
                }
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
        public string GoogleTranslateForElement(TranslationJobElementModel jobElement)
        {
            string translated = "";
            try
            {
                translated = Resolve<ITranslationJobService>().GoogleTranslateForElement(jobElement);
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
            return translated;
        }


        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        public TranslationJobElementPagePreviewModel GetTranslationJobPagePreview(string pageId, string translationJobContentId)
        {
            Guid pageGuid = new Guid(pageId);
            Guid translationJobContentGuid = new Guid(translationJobContentId);
            TranslationJobElementPagePreviewModel previewModel = new TranslationJobElementPagePreviewModel();
            try
            {
                previewModel = Resolve<ITranslationJobService>().getTranslationJobPagePreviewModel(pageGuid, translationJobContentGuid);
            }
            catch(Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
            return previewModel;
        }
    }
}
