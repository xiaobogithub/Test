using System;
using System.Collections.Generic;
using System.ServiceModel.Activation;

using ChangeTech.Models;
using Ethos.DependencyInjection;
using Ethos.Utility;
using ChangeTech.DeveloperWeb;
using Microsoft.WindowsAzure.StorageClient;

namespace ChangeTech.Contracts
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    public class Service : ServiceBase, IService
    {
        private string versionNumber
        {
            get
            {
                string version = System.Configuration.ConfigurationManager.AppSettings["ProjectVersionWithoutDot"];
                return string.IsNullOrEmpty(version) ? "" : version.ToLower();
            }
        }

        public const string STATUSQUEUEMAKT = "s";//"status" need be changed to only 1 char, to avoid the queuename beyond 63

        #region IService Members
        public PageMaterials GetPageMaterials()
        {
            PageMaterials pm = null;
            try
            {
                pm = Resolve<IPageService>().GetPageMaterials();
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
            return pm;
        }

        public ResourceListModel GetResourceName(Guid categoryGuid, ResourceTypeEnum type)
        {
            ResourceListModel images = null;
            try
            {
                images = Resolve<IResourceService>().GetResourceNameByCategoryGuid(categoryGuid, type);
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
            return images;
        }

        public ResourceCategoriesModel GetResourceCategory()
        {
            ResourceCategoriesModel rc = null;
            try
            {
                rc = Resolve<IResourceCategoryService>().GetAllCategory();
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
            return rc;
        }

        public EditPageModel GetEditPageModelByProgram(Guid pageGuid, Guid programGuid)
        {
            EditPageModel editPageModel = null;
            try
            {
                editPageModel = Resolve<IPageService>().GetEditPageModel(pageGuid, programGuid);
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
            return editPageModel;
        }

        public EditPageModel GetEditPageModel(Guid page, Guid sessionGuid, Guid pageSequenceGuid)
        {
            EditPageModel editPageModel = null;
            try
            {
                editPageModel = Resolve<IPageService>().GetEditPageModel(page, sessionGuid, pageSequenceGuid);
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
            return editPageModel;
        }

        public List<ProgramImageReference> GetReferencesInfoOfImage(Guid resourceGuid)
        {
            List<ProgramImageReference> resourceReferenceInfo = null;
            try
            {
                resourceReferenceInfo = Resolve<IResourceService>().GetReferencesInfoOfImage(resourceGuid);
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, string.Format("Method: {0}; Resource: {1}", System.Reflection.MethodBase.GetCurrentMethod().Name, resourceGuid));
                throw ex;
            }
            return resourceReferenceInfo;
        }

        public List<TranslationModel> GetTranslateByProgram(Guid programGuid, Guid languageGuid)
        {
            List<TranslationModel> translationModels = null;
            //try
            //{
            //    translationModels = Resolve<IProgramService>().GetTranslationData(Resolve<IProgramService>().GetProgramByProgramGUIDAndLanguageGUID(programGuid, languageGuid).Guid);
            //}
            //catch (Exception ex)
            //{
            //    LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            //    throw ex;
            //}
            return translationModels;
        }

        public ProgramReportModel GetProgramReport(Guid programguid, Guid language)
        {
            ProgramReportModel reportModel = null;
            try
            {
                reportModel = Resolve<IProgramService>().GetProgramReportModel(Resolve<IProgramService>().GetProgramByProgramGUIDAndLanguageGUID(programguid, language).Guid);
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
            return reportModel;
        }

        public void SaveResourceCategory(Guid categoryGuid, string categoryName, string categoryDes)
        {
            try
            {
                Resolve<IResourceCategoryService>().UpdateResourceCategory(categoryGuid, categoryName, categoryDes);
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        public void SaveResource(Guid resourceId, Guid categoryGuid, string resourceName, string type)
        {
            try
            {
                Resolve<IResourceService>().SaveResource(resourceId, categoryGuid, resourceName, type);
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        public void DeleteResource(Guid resourceGuid)
        {
            try
            {
                Resolve<IResourceService>().DeleteResource(resourceGuid);
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        public bool DeleteNotUsedResource(Guid resourceGuid)
        {
            try
            {
                if (GetReferencesInfoOfImage(resourceGuid).Count <= 0)
                {
                    Resolve<IResourceService>().DeleteResource(resourceGuid);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        public void InsertResourceCategory(Guid categotyGuid, string categoryName, string Des)
        {
            try
            {
                Resolve<IResourceCategoryService>().InsertResourceCategory(categotyGuid, categoryName, Des);
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        public void DeleteResourceCategory(Guid categoryGuid)
        {
            try
            {
                Resolve<IResourceCategoryService>().DeleteResourceCategory(categoryGuid);
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        public Guid SaveStandardTemplatePage(StandardTemplatePageContentModel newpage)
        {
            Guid newPageGuid = Guid.Empty;
            try
            {
                newPageGuid = Resolve<IPageService>().SaveStandardTemplatePage(newpage);
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
            return newPageGuid;
        }

        public Guid SaveChangedToStandardTemplatePage(Guid oldPageGuid, StandardTemplatePageContentModel newpage)
        {
            Guid newPageGuid = Guid.Empty;
            try
            {
                newPageGuid = Resolve<IPageService>().SaveChangedToStandardTemplatePage(oldPageGuid, newpage);
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
            return newPageGuid;
        }

        public Guid SaveChangedToGetInfoTemplatePage(Guid oldPageGuid, GetInfoTemplatePageContentModel newpage)
        {
            Guid newPageGuid = Guid.Empty;
            try
            {
                newPageGuid = Resolve<IPageService>().SaveChangedToGetInfoTemplatePage(oldPageGuid, newpage);
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
            return newPageGuid;
        }

        public Guid SaveChangedToScreenResultsTemplatePage(Guid oldPageGuid, ScreenResultTemplatePageContentModel newpage)
        {
            Guid newPageGuid = Guid.Empty;
            try
            {
                newPageGuid = Resolve<IPageService>().SaveChangedToScreenResultsTemplatePage(oldPageGuid, newpage);
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
            return newPageGuid;
        }

        public Guid SaveChangedToChoosePreferenceTemplatePage(Guid oldPageGuid, ChoosePreferencesTemplatePageContentModel newpage)
        {
            Guid newPageGuid = Guid.Empty;
            try
            {
                newPageGuid = Resolve<IPageService>().SaveChangedToChoosePreferenceTemplatePage(oldPageGuid, newpage);
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
            return newPageGuid;
        }

        public Guid SaveChangedToPushPicturesTemplatePage(Guid oldPageGuid, PushPictureTemplatePageContentModel newpage)
        {
            Guid newPageGuid = Guid.Empty;
            try
            {
                newPageGuid = Resolve<IPageService>().SaveChangedToPushPicturesTemplatePage(oldPageGuid, newpage);
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
            return newPageGuid;
        }

        public Guid SaveChangedToAccountCreationTemplatePage(Guid oldPageGuid, AccountCreationTemplatePageContentModel newPage)
        {
            Guid newPageGuid = Guid.Empty;
            try
            {
                newPageGuid = Resolve<IPageService>().SaveChangedToAccountCreationTemplatePage(oldPageGuid, newPage);
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
            return newPageGuid;
        }


        public Guid SaveChangedToTimerTemplatePage(Guid oldPageGuid, TimerTemplatePageContentModel newPage)
        {
            Guid newPageGuid = Guid.Empty;
            try
            {
                newPageGuid = Resolve<IPageService>().SaveChangedToTimerTemplatePage(oldPageGuid, newPage);
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
            return newPageGuid;
        }

        public Guid SaveChangedToGraphTemplatePageContentModel(Guid oldPageGuid, GraphTemplatePageContentModel graphPage)
        {
            Guid newPageGuid = Guid.Empty;
            try
            {
                newPageGuid = Resolve<IPageService>().SaveChangedToGraphTemplatePageContentModel(oldPageGuid, graphPage);
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
            return newPageGuid;
        }

        public Guid SaveChangedToSMSTemplatePage(Guid oldPageGuid, SMSTemplatePageContentModel newPage)
        {
            try
            {
                return Resolve<IPageService>().SaveChangedToSMSTemplatePage(oldPageGuid, newPage);
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        public Guid SaveGraphTemplatePageContentModel(GraphTemplatePageContentModel graphPage)
        {
            Guid newPageGuid = Guid.Empty;
            try
            {
                newPageGuid = Resolve<IPageService>().SaveGraphTemplatePage(graphPage);
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
            return newPageGuid;
        }

        public List<SimplePageContentModel> GetPagesOfPageSequence(Guid pageSequenceGUID, Guid languageGUID)
        {
            List<SimplePageContentModel> pages = null;
            try
            {
                pages = Resolve<IPageService>().GetPagesOfPageSequence(pageSequenceGUID);
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, string.Format("Method:{0};pageSequenceGUID:{1};languageGUID:{2}", System.Reflection.MethodBase.GetCurrentMethod().Name, pageSequenceGUID, languageGUID));
                throw ex;
            }
            return pages;
        }

        public void UpdateStandardTemplatePage(StandardTemplatePageContentModel standardPage, Guid pageGuid)
        {
            try
            {
                Resolve<IPageService>().UpdateStandardTemplatePage(standardPage, pageGuid);
                //Resolve<IPageService>().UpdateStandardTemplatePageOfDownloadResource(standardPage, pageGuid);
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        public Guid SaveGetInfoTemplatePage(GetInfoTemplatePageContentModel newPage)
        {
            Guid newPageGuid = Guid.Empty;
            try
            {

                newPageGuid = Resolve<IPageService>().SaveGetInfoTemplatePage(newPage);
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
            return newPageGuid;
        }

        public Guid SaveScreenResultsTemplatePage(ScreenResultTemplatePageContentModel newPage)
        {
            Guid newPageGuid = Guid.Empty;
            try
            {
                newPageGuid = Resolve<IPageService>().SaveScreenResultsTemplatePage(newPage);
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
            return newPageGuid;
        }

        public void UpdateGraphTemplatePage(GraphTemplatePageContentModel graphPage, Guid pageGuid)
        {
            try
            {
                Resolve<IPageService>().UpdateGraphTemplatePage(graphPage, pageGuid);
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        public void UpdateGetInfoTemplatePage(GetInfoTemplatePageContentModel getInfoPage, Guid pageGuid)
        {
            try
            {
                Resolve<IPageService>().UpdateGetInfoTemplatePage(getInfoPage, pageGuid);
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, string.Format("Method:{0};pageGuid:{1}", System.Reflection.MethodBase.GetCurrentMethod().Name, pageGuid));
                throw ex;
            }
        }

        public void UpdateScreenResultsTemplatePage(ScreenResultTemplatePageContentModel screenResultPage, Guid pageGuid)
        {
            try
            {
                Resolve<IPageService>().UpdateScreenResultsTemplatePage(screenResultPage, pageGuid);
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, string.Format("Method:{0};pageGuid:{1}", System.Reflection.MethodBase.GetCurrentMethod().Name, pageGuid));
                throw ex;
            }
        }

        public void UpdateAccoutCrationTemplatePage(AccountCreationTemplatePageContentModel accountPage, Guid pageGuid)
        {
            try
            {
                Resolve<IPageService>().UpdateAccoutCrationTemplatePage(accountPage, pageGuid);
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, string.Format("Method:{0};pageGuid:{1}", System.Reflection.MethodBase.GetCurrentMethod().Name, pageGuid));
                throw ex;
            }
        }

        public void UpdateSMSTemplatePage(SMSTemplatePageContentModel smsPage, Guid pageGuid)
        {
            try
            {
                Resolve<IPageService>().UpdateSMSTemplatePage(smsPage, pageGuid);
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, string.Format("Method:{0};pageGuid:{1}", System.Reflection.MethodBase.GetCurrentMethod().Name, pageGuid));
                throw ex;
            }
        }

        public Guid SaveAccountCreationTemplatePage(AccountCreationTemplatePageContentModel newPage)
        {
            try
            {
                return Resolve<IPageService>().SaveAccountCreationTemplatePage(newPage);
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        public Guid SavePushPicturesTemplatePage(PushPictureTemplatePageContentModel newPage)
        {
            Guid newPageGuid = Guid.Empty;
            try
            {
                newPageGuid = Resolve<IPageService>().SavePushPictureTemplatePage(newPage);
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
            return newPageGuid;
        }

        public void UpdatePushPictureTemplatePage(PushPictureTemplatePageContentModel pushPicPage, Guid pageGuid)
        {
            try
            {
                Resolve<IPageService>().UpdatePushPicTemplatePage(pushPicPage, pageGuid);
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        public Guid SaveTimerTemplatePage(TimerTemplatePageContentModel newPage)
        {
            Guid newPageGuid = Guid.Empty;
            try
            {
                newPageGuid = Resolve<IPageService>().SaveTimerTemplatePage(newPage);
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
            return newPageGuid;
        }

        public Guid SaveSMSTemplatePage(SMSTemplatePageContentModel newPage)
        {
            try
            {
                return Resolve<IPageService>().SaveSMSTemplatePage(newPage);
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        public void UpdateTimerTemplatePage(TimerTemplatePageContentModel timerPage, Guid pageGuid)
        {
            try
            {
                Resolve<IPageService>().UpdateTimerTemplatePage(timerPage, pageGuid);
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        public void UpdateResourceCategory(Guid resourceGuid, Guid resourceCategoryGuid)
        {
            Resolve<IResourceService>().UpdateResourceCategory(resourceGuid, resourceCategoryGuid);
        }

        public void UpdateResourceEntity(ResourceModel resourceModel)
        {
            try
            {
                Resolve<IResourceService>().UpdateResource(resourceModel);
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        public void UpdatePageVariableLastAccessTime(Guid pageVariableGUID)
        {
            Resolve<IPageService>().UpdatePageVariableLastAccessTime(pageVariableGUID);
        }

        public int GetCountOfVariableByProgram(Guid programGUID, VariableTypeEnum pageVariableType,  Guid groupGuid)
        {
            int countOfVariables = 0;
            try
            {
                countOfVariables = Resolve<IPageVariableService>().GetPageVariableCount(programGUID, pageVariableType, groupGuid);
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
            return countOfVariables;
        }

        public List<EditPageVariableModel> GetPageVariableByProgram(Guid programGUID, VariableTypeEnum pageVariableType, Guid userGUID, Guid groupGuid, int pageSize, int pageIndex)
        {
            List<EditPageVariableModel> pageVariableList = null;
            try
            {
                pageVariableList = Resolve<IPageVariableService>().GetPageVariableByProgram(programGUID, pageVariableType, userGUID, groupGuid, pageSize, pageIndex);
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
            return pageVariableList;
        }

        public void DeletePageVariable(Guid PageVariableGUID)
        {
            try
            {
                Resolve<IPageVariableService>().Delete(PageVariableGUID);
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        public void SavePageVariable(EditPageVariableModel pageVariable)
        {
            try
            {
                if (pageVariable.modelStatus == ModelStatus.ModelAdd)
                {
                    Resolve<IPageVariableService>().Add(pageVariable);
                }
                else if (pageVariable.modelStatus == ModelStatus.ModelEdit)
                {
                    Resolve<IPageVariableService>().Edit(pageVariable);
                }
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, String.Format("Method Name: {0}, Page Variable Name: {1}", System.Reflection.MethodBase.GetCurrentMethod().Name, pageVariable.Name));
                throw ex;
            }
        }

        public Guid SaveChoosePreferenceTemplatePageContentModel(ChoosePreferencesTemplatePageContentModel choosePreferencePage)
        {
            Guid newPageGuid = Guid.Empty;
            try
            {
                newPageGuid = Resolve<IPageService>().SaveChoosePreferenceTemplatePage(choosePreferencePage);
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
            return newPageGuid;
        }

        public void UpdateChoosePreferencesTemplatePageContentModel(ChoosePreferencesTemplatePageContentModel choosePreferencePage, Guid pageGuid)
        {
            try
            {
                Resolve<IPageService>().UpdateChoosePreferenceTemplatePage(choosePreferencePage, pageGuid);
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        public Guid GetNewPageGuidByPageSequenceAndOldPageGuid(Guid newPageSequenceGuid, Guid oldPageGuid)
        {
            try
            {
                return Resolve<IPageService>().GetPageGuidByPageSequenceAndOldPageGuid(newPageSequenceGuid, oldPageGuid);
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        public int GetCountOfPagesInPageSequence(Guid sequenceGuid)
        {
            int count = 0;
            try
            {
                count = Resolve<IPageSequenceService>().GetCountOfPagesInPageSequence(sequenceGuid);
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
            return count;
        }

        public EditPageVariableGroupModel GetPageVariableGroupForProgram(Guid programGuid)
        {
            try
            {
                return Resolve<IPageVariableGroupService>().GetPageVariableGroupByProgram(programGuid);
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        public void SavePageVariableGroup(EditPageVariableGroupModel groupModel)
        {
            try
            {
                Resolve<IPageVariableGroupService>().SavePageVariabeGroup(groupModel);
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        public int BeforeDeletePageVariable(Guid pageVariableGuid)
        {
            try
            {
                return Resolve<IPageVariableService>().BeforeDeletePageVariable(pageVariableGuid);
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        public LanguagesModel GetProgramLanguages(Guid ProgramGuid)
        {
            LanguagesModel lm = null;
            try
            {
                lm = Resolve<IProgramLanguageService>().GetLanguagesSupportByProgram(ProgramGuid);
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
            return lm;
        }

        public bool IsPageSequenceUsedInSameProgramButNotInSameSession(Guid sessionGuid, Guid sequenceGuid)
        {
            bool yes = false;
            try
            {
                yes = Resolve<IPageSequenceService>().PageSequenceInMoreSession(sessionGuid, sequenceGuid);
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
            return yes;
        }

        public Guid BeforeEditPageSequence(Guid sessionGuid, Guid pageSeqGuid, bool affectFlag)
        {
            try
            {
                return Resolve<IPageSequenceService>().BeforeEditPageSequenceAfterRefactoring(sessionGuid, pageSeqGuid, affectFlag);
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        public List<ExpressionGroupModel> GetExpressionGroupsOfProgram(Guid sessionGuid)
        {
            List<ExpressionGroupModel> expressionGroupsModel = null;
            try
            {
                expressionGroupsModel = Resolve<IExpressionGroupService>().GetExpressionGroupOfProgram(sessionGuid);
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
            return expressionGroupsModel;
        }

        public void SaveExpressionGroup(EditExpressionGroupModel groups)
        {
            Resolve<IExpressionGroupService>().SaveEditExpressionGroup(groups);
        }

        public void AddExpression(ExpressionModel expressionModel)
        {
            Resolve<IExpressionService>().AddExpression(expressionModel);
        }

        public List<ExpressionModel> GetExpressionsOfGroup(Guid expressionGroupGuid)
        {
            return Resolve<IExpressionService>().GetExpressionsOfGroup(expressionGroupGuid);
        }

        public List<ExpressionModel> GetExpressionsOfProgram(Guid sessionGuid)
        {
            return Resolve<IExpressionService>().GetExpressionOfProgram(sessionGuid);
        }

        public void SaveExpression(EditExpressionModel expresions)
        {
            Resolve<IExpressionService>().SaveExpressions(expresions);
        }

        public ProgramsModel GetPrograms()
        {
            return Resolve<IProgramService>().GetProgramsModel();
        }

        //public bool HasPermission(Permission programsecqurity, Permission permission, Permission applicationsecurity)
        //{
        //    try
        //    {
        //        return Resolve<IUserService>().HasPermission(programsecqurity, permission, applicationsecurity);
        //    }
        //    catch (Exception ex)
        //    {
        //        LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
        //        throw ex;
        //    }
        //}

        public string GetBlobURLWithSharedWriteAccess(string resourceType, string fileGUID)
        {
            try
            {
                return Resolve<IResourceService>().GetBlobURLWithSharedWriteAccess(resourceType, fileGUID);
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        public bool CheckThumnailImageWhetherExist(string imageName)
        {
            try
            {
                return Resolve<IResourceService>().CheckThumnailImageWhetherExist(imageName);
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }


        public LanguagesModel GetLanguagesSupportByProgram(Guid programGUID)
        {
            try
            {
                return Resolve<IProgramLanguageService>().GetLanguagesSupportByProgram(
                    programGUID);
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        public LanguagesModel GetLanguagesNotSupportByProgram(Guid programGUID)
        {
            try
            {
                return Resolve<IProgramLanguageService>().GetLanguagesNotSupportByProgram(
                    programGUID);
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        public string GetAddRemoveStauts(Guid programGUID, string fileName)
        {
            string statusMessageStr = "";
            try
            {
                string statusQueueName = string.Format("{0}{1}{2}", STATUSQUEUEMAKT, fileName.ToLower().Replace('.', '0'), versionNumber);
                CloudQueue statusQueue = Resolve<IAzureQueueService>().GetCloudQueue(statusQueueName);
                if (statusQueue != null && statusQueue.Exists())
                {
                    CloudQueueMessage statusMessageInQueue = statusQueue.PeekMessage();

                    if (statusMessageInQueue != null)
                    {
                        string statusMessage = statusMessageInQueue.AsString;
                        string[] statusMessageArrary = statusMessage.Split(new char[] { ';' });

                        //timeLbl.Text = Math.Round((DateTime.UtcNow - Convert.ToDateTime(startTimeLbl.Text)).TotalSeconds, 0).ToString() + " seconds";
                        if (statusMessageArrary[0].Equals("Complete"))
                        {
                            statusQueue.Clear();
                        }
                        statusMessageStr = statusMessageArrary[0];
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                statusMessageStr = string.Format("Error occurs: {0}", ex.Message);
                //throw ex;
            }
            return statusMessageStr;
        }

        public string AddProgramLanguage(Guid programGUID, Guid languageGUID)
        {
            string returnMsg = string.Empty;
            try
            {
                string statusQueueName = string.Format("{0}{1}{2}", STATUSQUEUEMAKT, programGUID, versionNumber);
                CloudQueue operationQueue = Resolve<IAzureQueueService>().GetCloudQueue(string.Format("operationqueue{0}", versionNumber));

                operationQueue.CreateIfNotExist();

                IEnumerable<CloudQueueMessage> operationMessagesInQueue = operationQueue.PeekMessages(32);
                string operationMsgStr = string.Format("{0};{1}", "AddProgramLanguage", programGUID);
                bool hasOperationOfThisProgram = false;
                foreach (CloudQueueMessage operationMessage in operationMessagesInQueue)
                {
                    if (operationMessage.AsString.StartsWith(operationMsgStr))
                    {
                        hasOperationOfThisProgram = true;
                        break;
                    }
                }

                if (!hasOperationOfThisProgram)
                {
                    string operationMsg = string.Format("{0};{1};{2};{3}", "AddProgramLanguage", programGUID, languageGUID, ContextService.CurrentAccount.UserGuid);
                    Resolve<IAzureQueueService>().AddQueueMessage(operationQueue, operationMsg, false);

                    CloudQueue statusQueue = Resolve<IAzureQueueService>().GetCloudQueue(statusQueueName);
                    string statusMsg = string.Format("{0}", "Initializing");
                    Resolve<IAzureQueueService>().AddQueueMessage(statusQueue, statusMsg, true);
                }
                else
                {
                    returnMsg = "There is another operation of this program is waiting for processing, please wait for a moment and retry!";
                }
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
            return returnMsg;
        }

        public void ExportProgram(string fileName, Guid programGUID, Guid languageGUID, string startDay, string endDay, bool includeRelapse, bool includeProgramRoom,
        bool includeAccessoryTemplate, bool includeEmailTemplate, bool includeHelpItem, bool includeUserMenu, bool includeTipMessage, bool includeSpecialString)
        {
            try
            {
                Resolve<IExportService>().AddExportProgramCommand(fileName, programGUID, languageGUID, startDay, endDay, includeRelapse, includeProgramRoom,
                includeAccessoryTemplate, includeEmailTemplate, includeHelpItem, includeUserMenu, includeTipMessage, includeSpecialString);
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        public void ReportProgram(string fileName, Guid programGuid, Guid languageGuid)
        {
            try
            {
                Resolve<IExportService>().AddReportProgramCommand(fileName, programGuid, languageGuid);
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        public string RemoveProgramLanguage(Guid programGUID, Guid lanaguegGUID)
        {
            string returnMsg = string.Empty;
            try
            {
                string statusQueueName = string.Format("{0}{1}{2}", STATUSQUEUEMAKT, programGUID, versionNumber);
                CloudQueue operationQueue = Resolve<IAzureQueueService>().GetCloudQueue(string.Format("operationqueue{0}", versionNumber));

                operationQueue.CreateIfNotExist();

                IEnumerable<CloudQueueMessage> operationMessagesInQueue = operationQueue.PeekMessages(32);
                string operationMsgStr = string.Format("{0};{1}", "RemoveProgramLanguage", programGUID);
                bool hasOperationOfThisProgram = false;
                foreach (CloudQueueMessage operationMessage in operationMessagesInQueue)
                {
                    if (operationMessage.AsString.StartsWith(operationMsgStr))
                    {
                        hasOperationOfThisProgram = true;
                        break;
                    }
                }

                if (!hasOperationOfThisProgram)
                {
                    string operationMsg = string.Format("{0};{1};{2};{3}", "RemoveProgramLanguage", programGUID, lanaguegGUID, ContextService.CurrentAccount.UserGuid);
                    Resolve<IAzureQueueService>().AddQueueMessage(operationQueue, operationMsg, false);

                    CloudQueue statusQueue = Resolve<IAzureQueueService>().GetCloudQueue(statusQueueName);
                    string statusMsg = string.Format("{0}", "Initializing");
                    Resolve<IAzureQueueService>().AddQueueMessage(statusQueue, statusMsg, true);
                }
                else
                {
                    returnMsg = "There is another operation of this program is waiting for processing, please wait for a moment and retry!";
                }
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
            return returnMsg;
        }

        public string ImportProgramData(string tableName, string id, string type, string newText, Guid languageGUID)
        {
            try
            {
                LogUtility.LogUtilityIntance.LogMessage(string.Format("{0};{1};{2};{3};{4}", tableName, id, type, newText, DateTime.UtcNow.ToString()));
                switch (tableName)
                {
                    case "PageContent":
                        Resolve<ITranslationService>().TranslatePageContent(new Guid(id), type, newText);
                        break;
                    case "PageQuestionContent":
                        Resolve<ITranslationService>().TranslatePageQuestionContent(new Guid(id), type, newText);
                        break;
                    case "PageQuestionItemContent":
                        Resolve<ITranslationService>().TranslatePageQuestionItemContent(new Guid(id), type, newText);
                        break;
                    case "ProgramRoom":
                        Resolve<ITranslationService>().TranslateProgramRoom(new Guid(id), type, newText);
                        break;
                    case "GraphContent":
                        Resolve<ITranslationService>().TranslateGraphContent(new Guid(id), type, newText);
                        break;
                    case "EmailTemplate":
                        Resolve<ITranslationService>().TranslateEmailTemplate(new Guid(id), type, newText);
                        break;
                    case "GraphItemContent":
                        Resolve<ITranslationService>().TranslateGraphItemContent(new Guid(id), type, newText);
                        break;
                    case "HelpItem":
                        Resolve<ITranslationService>().TranslateHelpItem(new Guid(id), type, newText);
                        break;
                    case "Preferences":
                        Resolve<ITranslationService>().TranslatePreference(new Guid(id), type, newText);
                        break;
                    case "Session":
                        Resolve<ITranslationService>().TranslateSession(new Guid(id), type, newText);
                        break;
                    case "SpecialString":
                        Resolve<ITranslationService>().TranslateSepcialString(id, languageGUID, type, newText);
                        break;
                    case "TipMessage":
                        Resolve<ITranslationService>().TranslateTipMessage(new Guid(id), type, newText);
                        break;
                    case "UserMenu":
                        Resolve<ITranslationService>().TranslateUserMenu(new Guid(id), type, newText);
                        break;
                    case "AccessoryTemplate":
                        Resolve<ITranslationService>().TranslateAccessoryTemplate(new Guid(id), type, newText);
                        break;
                    case "Relapse":
                    case "PageSequence":
                        Resolve<ITranslationService>().TranslatePageSequence(new Guid(id), type, newText);
                        break;
                    case "ScreenResultTemplatePageLine":
                        Resolve<ITranslationService>().TranslateScreenResultTemplatePageLine(new Guid(id), type, newText);
                        break;
                }
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, string.Format("ImportProgramData: {0}, {1}, {2}, {3}", tableName, id, type, newText));
                throw ex;
            }
            return id;
        }

        public ResourceModel CropImage(CropImageModel cropImageModel)
        {
            try
            {
                return Resolve<IResourceService>().CropAndSaveImage(cropImageModel);
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        public Guid GetFistSessionOfProgram(Guid programGuid)
        {
            try
            {
                return Resolve<ISessionService>().GetFirstSessionGUID(programGuid);
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        public List<RelapseModel> GetRelapsePageSequenceModelList(Guid programGuid)
        {
            try
            {
                return Resolve<IRelapseService>().GetRelapseModelList(programGuid);
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        public List<RelapseModel> GetRelapsePageSequenceModelListBySessionGuid(Guid sessionGuid)
        {
            try
            {
                return Resolve<IRelapseService>().GetRelapseModelListBySessionGUID(sessionGuid);
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        public PageSequenceModel GetPageSequence(Guid pagesequenceGuid)
        {
            try
            {
                return Resolve<IPageSequenceService>().GetRelapsePageSequenceModelBySequenceGuid(pagesequenceGuid);
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        public void SetCTPPPresenterImage(Guid presenterImageGUID, Guid ProgramGUID)
        {
            CTPPModel model = Resolve<ICTPPService>().GetCTPP(ProgramGUID);
            if (model != null)
            {
                ResourceModel resourceModel = Resolve<IResourceService>().GetResourceModelByGuid(presenterImageGUID);
                if (resourceModel != null && resourceModel.Extension != string.Empty)
                {
                    model.ProgramPresenter = new ResourceModel
                    {
                        Extension = resourceModel.Extension,
                        ID = resourceModel.ID,
                        Name = resourceModel.Name,
                        NameOnServer = resourceModel.NameOnServer,
                        Type = resourceModel.Type,
                    };
                }
                else
                {
                    model.ProgramPresenter = new ResourceModel();
                    model.ProgramPresenter.Extension = string.Empty;
                }
                Resolve<ICTPPService>().UpdateCTPP(model, true);
            }
        }

        public void SetPagePresenterImage(PageUpdateForPageReviewModel pageUpdateForPageReview)
        {
            //PageUpdateForPageReviewModel pageUpdateForPageReview = new PageUpdateForPageReviewModel
            //{
            //    IsUpdatePageSequence=true,
            //    SessionGUID=
            //    PageGUID = pageGUID,
            //    PresenterImageGUID = presenterImageGUID
            //};
            Resolve<IPageService>().UpdatePageContentForPageReview(pageUpdateForPageReview);
        }

        public bool IsPageHasMoreReference(Guid SessionGuid, Guid PageSequenceGuid)
        {
            return Resolve<IPageService>().IsPageHasMoreReference(SessionGuid, PageSequenceGuid);
        }

        public bool IsEnableHTML5NewUI(Guid programguid)
        {
           ProgramPropertyModel pm = Resolve<IProgramService>().GetProgramProperty(programguid);
           bool flag = false;
           flag = pm.EnableHTML5NewUI;
           return flag;  
        }

        #region  NotImplementedMethod
        public EditChoosePreferencesTemplatePageContentModel GetEditChoosePreferenceTemplatePageContentModel()
        {
            throw new NotImplementedException();
        }

        public EditSessionModel GetSession(Guid SessionGuid)
        {
            return Resolve<ISessionService>().GetSessionBySessonGuid(SessionGuid);
        }

        public EditStandardTemplatePageContentModel GetEditStandTemplatePageContentModel()
        {
            throw new NotImplementedException();
        }

        public EditGetInfoTemplatePageContentModel GetEditGetInfoTemplatePageContentModel()
        {
            throw new NotImplementedException();
        }

        public EditScreenResultsTemplatePageContentModel GetEditScreenResultTemplatePageContentModel()
        {
            throw new NotImplementedException();
        }

        public EditGraphTemplatePageContentModel GetGraphTemplatePageContentModel()
        {
            throw new NotImplementedException();
        }

        public EditPushPictureTemplatePageContentModel GetEditPushPictureTemplatePageContentModel()
        {
            throw new NotImplementedException();
        }

        public EditTimerTemplatePageContentModel GetTimerTemplatePageContentModel()
        {
            throw new NotImplementedException();
        }

        //public EditAccountCreationTemplatePageContentModel GetAccountCreationTemplatePageContentModel()
        //{
        //    throw new NotImplementedException();
        //}

        public EditExpressionGroupModel GetEditExpressionGroupModel()
        {
            throw new NotImplementedException();
        }

        public EditExpressionModel GetEditExpressionModel()
        {
            throw new NotImplementedException();
        }

        public EditSMSTemplatePageContentModel GetSMSTemplatePageContentModel()
        {
            throw new NotImplementedException();
        }
        #endregion

        #endregion

        private bool IsValidAccess(string securityCode)
        {
            //TODO: Implement check logic
            return true;
        }
    }
}
