using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using ChangeTech.Models;

namespace ChangeTech.Contracts
{
    [ServiceContract]
    public interface IService
    {
        [OperationContract]
        PageMaterials GetPageMaterials();
        [OperationContract]
        ResourceListModel GetResourceName(Guid category, ResourceTypeEnum type);
        [OperationContract]
        EditPageModel GetEditPageModel(Guid page, Guid sessionGuid, Guid pageSequenceGuid);
        [OperationContract]
        EditPageModel GetEditPageModelByProgram(Guid pageGuid, Guid programGuid);
        [OperationContract]
        ResourceCategoriesModel GetResourceCategory();
        [OperationContract]
        EditSessionModel GetSession(Guid SessionGuid);
        [OperationContract]
        LanguagesModel GetProgramLanguages(Guid ProgramGuid);
        [OperationContract]
        int GetCountOfPagesInPageSequence(Guid sequenceGuid);
        [OperationContract]
        List<SimplePageContentModel> GetPagesOfPageSequence(Guid pageSequenceGUID, Guid languageGUID);
        [OperationContract]
        int GetCountOfVariableByProgram(Guid programGUID, VariableTypeEnum pageVariableType,  Guid groupGuid);
        [OperationContract]
        List<EditPageVariableModel> GetPageVariableByProgram(Guid programGUID, VariableTypeEnum pageVariableType, Guid userGUID, Guid groupGuid, int pageSize, int pageIndex);
        [OperationContract]
        List<ExpressionGroupModel> GetExpressionGroupsOfProgram(Guid sessionGuid);
        [OperationContract]
        EditPageVariableGroupModel GetPageVariableGroupForProgram(Guid programGuid);
        [OperationContract]
        List<ExpressionModel> GetExpressionsOfGroup(Guid expressionGroupGuid);
        [OperationContract]
        List<ExpressionModel> GetExpressionsOfProgram(Guid sessionGuid);
        [OperationContract]
        List<ProgramImageReference> GetReferencesInfoOfImage(Guid resourceGuid);
        [OperationContract]
        List<TranslationModel> GetTranslateByProgram(Guid programGuid, Guid languageGuid);
        [OperationContract]
        ProgramReportModel GetProgramReport(Guid programguid, Guid languageguid);
        [OperationContract]
        void ReportProgram(string fileName, Guid programGuid, Guid languageGuid);

        [OperationContract]
        Guid GetFistSessionOfProgram(Guid programGuid);
        [OperationContract]
        void SaveResource(Guid resourceId, Guid categoryGuid, string resourceName, string type);
        [OperationContract]
        Guid SaveStandardTemplatePage(StandardTemplatePageContentModel newpage);
        [OperationContract]
        void SaveResourceCategory(Guid categoryGuid, string categoryName, string categoryDes);
        [OperationContract]
        Guid SaveGetInfoTemplatePage(GetInfoTemplatePageContentModel newPage);
        [OperationContract]
        void SavePageVariable(EditPageVariableModel pageVariable);
        [OperationContract]
        Guid SaveChoosePreferenceTemplatePageContentModel(ChoosePreferencesTemplatePageContentModel choosePreferencePage);
        [OperationContract]
        Guid SaveGraphTemplatePageContentModel(GraphTemplatePageContentModel graphPage);
        [OperationContract]
        Guid SaveAccountCreationTemplatePage(AccountCreationTemplatePageContentModel newPage);
        [OperationContract]
        Guid SavePushPicturesTemplatePage(PushPictureTemplatePageContentModel newPage);
        [OperationContract]
        Guid SaveScreenResultsTemplatePage(ScreenResultTemplatePageContentModel newPage);
        [OperationContract]
        Guid SaveTimerTemplatePage(TimerTemplatePageContentModel newPage);
        [OperationContract]
        Guid SaveSMSTemplatePage(SMSTemplatePageContentModel newPage);
        [OperationContract]
        void SavePageVariableGroup(EditPageVariableGroupModel groupModel);
        [OperationContract]
        void SaveExpressionGroup(EditExpressionGroupModel groups);
        [OperationContract]
        void SaveExpression(EditExpressionModel expresions);
        [OperationContract]
        Guid SaveChangedToStandardTemplatePage(Guid oldPageGuid, StandardTemplatePageContentModel newpage);
        [OperationContract]
        Guid SaveChangedToGetInfoTemplatePage(Guid oldPageGuid, GetInfoTemplatePageContentModel newpage);
        [OperationContract]
        Guid SaveChangedToChoosePreferenceTemplatePage(Guid oldPageGuid, ChoosePreferencesTemplatePageContentModel newpage);
        [OperationContract]
        Guid SaveChangedToGraphTemplatePageContentModel(Guid oldPageGuid, GraphTemplatePageContentModel graphPage);
        [OperationContract]
        Guid SaveChangedToAccountCreationTemplatePage(Guid oldPageGuid, AccountCreationTemplatePageContentModel newPage);
        [OperationContract]
        Guid SaveChangedToPushPicturesTemplatePage(Guid oldPageGuid, PushPictureTemplatePageContentModel newPage);
        [OperationContract]
        Guid SaveChangedToTimerTemplatePage(Guid oldPageGuid, TimerTemplatePageContentModel newPage);
        [OperationContract]
        Guid SaveChangedToSMSTemplatePage(Guid oldPageGuid, SMSTemplatePageContentModel newPage);

        [OperationContract]
        void AddExpression(ExpressionModel expressionModel);
        [OperationContract]
        void InsertResourceCategory(Guid categoryGuid, string categoryName, string Des);

        [OperationContract]
        void UpdateStandardTemplatePage(StandardTemplatePageContentModel standardPage, Guid pageGuid);
        [OperationContract]
        void UpdatePushPictureTemplatePage(PushPictureTemplatePageContentModel pushPicPage, Guid pageGuid);
        [OperationContract]
        void UpdateTimerTemplatePage(TimerTemplatePageContentModel timerPage, Guid pageGuid);
        [OperationContract]
        void UpdateGetInfoTemplatePage(GetInfoTemplatePageContentModel getInfoPage, Guid pageGuid);
        [OperationContract]
        void UpdateGraphTemplatePage(GraphTemplatePageContentModel graphPage, Guid pageGuid);
        [OperationContract]
        void UpdateChoosePreferencesTemplatePageContentModel(ChoosePreferencesTemplatePageContentModel choosePreferencePage, Guid pageGuid);
        [OperationContract]
        void UpdateAccoutCrationTemplatePage(AccountCreationTemplatePageContentModel accountPage, Guid pageGuid);
        [OperationContract]
        void UpdateSMSTemplatePage(SMSTemplatePageContentModel smsPage, Guid pageGuid);
        [OperationContract]
        void UpdateResourceCategory(Guid resourceGuid, Guid resourceCategoryGuid);
        [OperationContract]
        void UpdateResourceEntity(ResourceModel resourceModel);
        [OperationContract]
        void UpdatePageVariableLastAccessTime(Guid pageVariableGUID);

        [OperationContract]
        bool IsPageSequenceUsedInSameProgramButNotInSameSession(Guid sequenceGuid, Guid sessionGuid);
        [OperationContract]
        Guid BeforeEditPageSequence(Guid sessionGuid, Guid pageSeqGuid, bool affectFlag);
        [OperationContract]
        Guid GetNewPageGuidByPageSequenceAndOldPageGuid(Guid newPageSequenceGuid, Guid oldPageGuid);
        [OperationContract]
        PageSequenceModel GetPageSequence(Guid pagesequenceGuid);

        [OperationContract]
        void DeleteResource(Guid resourceGuid);
        [OperationContract]
        bool DeleteNotUsedResource(Guid resourceGuid);
        [OperationContract]
        void DeleteResourceCategory(Guid categoryGuid);
        [OperationContract]
        int BeforeDeletePageVariable(Guid pageVariableGuid);
        [OperationContract]
        void DeletePageVariable(Guid PageVariableGUID);

        [OperationContract]
        List<RelapseModel> GetRelapsePageSequenceModelList(Guid programGuid);
        [OperationContract]
        List<RelapseModel> GetRelapsePageSequenceModelListBySessionGuid(Guid sessionGuid);

        //The methods below only is used to create model proxy, don't have any use
        [OperationContract]
        EditStandardTemplatePageContentModel GetEditStandTemplatePageContentModel();
        [OperationContract]
        EditGetInfoTemplatePageContentModel GetEditGetInfoTemplatePageContentModel();
        [OperationContract]
        EditPushPictureTemplatePageContentModel GetEditPushPictureTemplatePageContentModel();
        [OperationContract]
        EditTimerTemplatePageContentModel GetTimerTemplatePageContentModel();
        //[OperationContract]
        //EditAccountCreationTemplatePageContentModel GetAccountCreationTemplatePageContentModel();
        [OperationContract]
        EditGraphTemplatePageContentModel GetGraphTemplatePageContentModel();
        [OperationContract]
        EditChoosePreferencesTemplatePageContentModel GetEditChoosePreferenceTemplatePageContentModel();
        [OperationContract]
        EditSMSTemplatePageContentModel GetSMSTemplatePageContentModel();
        [OperationContract]
        EditExpressionGroupModel GetEditExpressionGroupModel();
        [OperationContract]
        EditExpressionModel GetEditExpressionModel();

        [OperationContract]
        ProgramsModel GetPrograms();
        //[OperationContract]
        //bool HasPermission(Permission programsecqurity, Permission permission, Permission applicationsecurity);

        [OperationContract]
        string GetBlobURLWithSharedWriteAccess(string resourceType, string fileGUID);

        [OperationContract]
        bool CheckThumnailImageWhetherExist(string imageName);

        [OperationContract]
        LanguagesModel GetLanguagesSupportByProgram(Guid programGUID);

        [OperationContract]
        LanguagesModel GetLanguagesNotSupportByProgram(Guid programGUID);

        [OperationContract]
        string GetAddRemoveStauts(Guid programGUID, string fileName);

        [OperationContract]
        string AddProgramLanguage(Guid programGUID, Guid languageGUID);

        [OperationContract]
        string RemoveProgramLanguage(Guid programGUID, Guid lanaguegGUID);

        [OperationContract]
        void ExportProgram(string fileName, Guid programGUID, Guid languageGUID, string startDay, string endDay, bool includeRelapse, bool includeProgramRoom,
        bool includeAccessoryTemplate, bool includeEmailTemplate, bool includeHelpItem, bool includeUserMenu, bool includeTipMessage, bool includeSpecialString);

        [OperationContract]
        string ImportProgramData(string tableName, string id, string type, string newText, Guid languageGUID);

        [OperationContract]
        ResourceModel CropImage(CropImageModel cropImageModel);

        [OperationContract]
        void SetCTPPPresenterImage(Guid presenterImageGUID, Guid CTPPGUID);

        [OperationContract]
        void SetPagePresenterImage(PageUpdateForPageReviewModel pageUpdateForPageReview);

        [OperationContract]
        bool IsPageHasMoreReference(Guid SessionGuid, Guid PageSequenceGuid);

        [OperationContract]
        Guid SaveChangedToScreenResultsTemplatePage(Guid oldPageGuid, ScreenResultTemplatePageContentModel newpage);
        [OperationContract]
        void UpdateScreenResultsTemplatePage(ScreenResultTemplatePageContentModel screenResultPage, Guid pageGuid);
        [OperationContract]
        EditScreenResultsTemplatePageContentModel GetEditScreenResultTemplatePageContentModel();
       
        [OperationContract]
        bool IsEnableHTML5NewUI(Guid programguid);
        
    }
}
