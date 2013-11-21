using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Models;
using ChangeTech.Entities;

namespace ChangeTech.Contracts
{
    public interface IPageService
    {
        void AdjustPageOrderUp(Guid pageSequenceGuid, Guid pageGuid);
        void AdjustPageOrderDown(Guid pageSequenceGuid, Guid pageGuid);
        void DeletePage(Guid pageGuid);
        void MakeCopyPage(Guid pageGuid);
        Guid GetPageGuidByPageSequenceAndOrder(Guid pageSequenceGuid, int order);
        Guid GetPageGuidByPageSequenceAndOldPageGuid(Guid pageSequence, Guid oldPageGuid);
        void UpdatePageVariableLastAccessTime(Guid pageVariableGUID);

        Guid GetLastPageGuidOfSession(Guid sessionGuid);
        EditPageModel GetEditPageModel(Guid page, Guid sessionGuid, Guid pageSequenceGuid);
        EditPageModel GetEditPageModelOfRelapsePageSeqence(Guid pageGuid, Guid relapsePageSequenceGuid, Guid programGuid);
        EditPageModel GetEditPageModel(Guid pageGuid, Guid programGuid);
        SimplePageModel GetSimplePageModel(Guid pageGuid);
        string GetPagePreviewModel(Guid pageGuid, Guid sessionGuid, Guid pageSequenceGuid, Guid userGuid);
        PageMaterials GetPageMaterials();
        List<PageTemplateModel> GetPageTemplates();
        List<SimplePageContentModel> GetPagesOfPageSequence(Guid pageSequenceGUID);
        List<SimplePageContentModel> GetPagesOfSession(Guid sessionGuid);
        List<StandardTemplatePageContentModel> GetPagesDownResOfPageSequence(Guid pageSequenceGUID);//used in ctpp page for download
        List<PageQuestionModel> ParasePageQuestionModel(IQueryable<PageQuestion> pageQuestionEntities);
        Guid GetNextPage(Guid currentPageGUID);
        Guid GetPrevioursPage(Guid currentPageGUID);

        Guid SavePage(SimplePageContentModel simplePageContentModel);
        Guid SaveStandardTemplatePage(StandardTemplatePageContentModel newStandardPage);
        Guid SaveGetInfoTemplatePage(GetInfoTemplatePageContentModel newGetInfoPage);
        Guid SavePushPictureTemplatePage(PushPictureTemplatePageContentModel newPushPicPage);
        Guid SaveTimerTemplatePage(TimerTemplatePageContentModel newTimerPage);
        Guid SaveChoosePreferenceTemplatePage(ChoosePreferencesTemplatePageContentModel choosePreferencePage);
        Guid SaveAccountCreationTemplatePage(AccountCreationTemplatePageContentModel newPage);
        Guid SaveGraphTemplatePage(GraphTemplatePageContentModel graphPage);
        Guid SaveSMSTemplatePage(SMSTemplatePageContentModel smsPage);

        Guid SaveChangedToStandardTemplatePage(Guid oldPageGuid, StandardTemplatePageContentModel newPage);
        Guid SaveChangedToGetInfoTemplatePage(Guid oldPageGuid, GetInfoTemplatePageContentModel newpage);        
        Guid SaveChangedToChoosePreferenceTemplatePage(Guid oldPageGuid, ChoosePreferencesTemplatePageContentModel newpage);     
        Guid SaveChangedToGraphTemplatePageContentModel(Guid oldPageGuid, GraphTemplatePageContentModel graphPage);
        Guid SaveChangedToAccountCreationTemplatePage(Guid oldPageGuid, AccountCreationTemplatePageContentModel newPage);        
        Guid SaveChangedToPushPicturesTemplatePage(Guid oldPageGuid, PushPictureTemplatePageContentModel newPage);    
        Guid SaveChangedToTimerTemplatePage(Guid oldPageGuid, TimerTemplatePageContentModel newPage);
        Guid SaveChangedToSMSTemplatePage(Guid oldPageGuid, SMSTemplatePageContentModel smsPage);
        Guid SaveChangedToScreenResultsTemplatePage(Guid oldPageGuid, ScreenResultTemplatePageContentModel newpage);

        void UpdateStandardTemplatePage(StandardTemplatePageContentModel standardPage, Guid pageGuid);
        void UpdateGetInfoTemplatePage(GetInfoTemplatePageContentModel getInfoPage, Guid pageGuid);
        void UpdateGraphTemplatePage(GraphTemplatePageContentModel graphPage, Guid pageGuid);
        void UpdatePushPicTemplatePage(PushPictureTemplatePageContentModel pushPicPage, Guid pageGuid);
        void UpdateTimerTemplatePage(TimerTemplatePageContentModel timerPage, Guid pageGuid);
        void UpdateChoosePreferenceTemplatePage(ChoosePreferencesTemplatePageContentModel choosePreferencePage, Guid pageGuid);
        void UpdateAccoutCrationTemplatePage(AccountCreationTemplatePageContentModel accountPage, Guid pageGuid);
        void UpdateSMSTemplatePage(SMSTemplatePageContentModel smsPage, Guid pageGuid);
        void UpdatePageContentForPageReview(PageUpdateForPageReviewModel pageUpdateForPageReview);
        void UpdatePageContentForCloneSessionContent(Session newSession, Dictionary<Guid, Guid> pageDictionary);
        void UpdatePageContentForCloneRelapse(Relapse newRelapse, List<KeyValuePair<string, string>> pageDictionary);
        void UpdateScreenResultsTemplatePage(ScreenResultTemplatePageContentModel screenResultsPage, Guid pageGuid);

        PageContentForCTPPLastPageModel getPageContentForCTPP(Guid pageGuid);

        void UpdateStandardTemplatePageOfDownloadResource(StandardTemplatePageContentModel standardPage, Guid pageGuid);

        bool IsPageHasMoreReference(Guid SessionGuid, Guid PageSequenceGuid);
        void DeletePageForPageReview(PageUpdateForPageReviewModel deleteModel);
        void AdjustPageOrderForPageReview(PageUpdateForPageReviewModel originalModel, PageUpdateForPageReviewModel swapToModel);
        Page ClonePage(Page page, List<KeyValuePair<string, string>> cloneRelapseGUIDList, CloneProgramParameterModel cloneParameterModel);
        Page SetDefaultGuidForPage(Page needSetDefaultEntity, CloneProgramParameterModel cloneParameterModel);

        Dictionary<string, string> GetResourcesBySessionGuid(Guid sessionGuid);
        #region the services provider for ctpp
        Dictionary<string, string> GetResourcesBySessionGuid(Guid sessionGuid, string serverPath, List<CTPPSessionPageBodyModel> sPageBodyList, List<CTPPSessionPageMediaResourceModel> sPageMediaResourceList);
        #endregion
        Guid SaveScreenResultsTemplatePage(ScreenResultTemplatePageContentModel newScreenResultsPage);

        string GetPageGraphData(Guid pageGuid);
    }
}
