using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Models;
using Google.API.Translate;

namespace ChangeTech.Contracts
{
    public interface ITranslationJobService
    {
        #region TranslationJob
        List<TranslationJobModel> GetTranslationJobList();
        List<TranslationJobModel> GetTranslationJobList(Guid translatorGuid);
        Guid AddTranslationJob(TranslationJobModel model);
        void UpdateTranslationJob(TranslationJobModel model);
        void DeleteTranslationJob(Guid translationJobGuid);
        TranslationJobModel GetTranslationJobByGUID(Guid translationJobGuid);
        #endregion

        #region TranslationJobContent
        List<TranslationJobContentModel> GetTranslationJobContentList(Guid translationJobGuid);
        void UpdateTranslationJobContent(TranslationJobContentModel model);
        #endregion

        #region TranslationJobElement
        List<TranslationJobElementModel> GetTranslationJobElementList(Guid translationJobContentId);
        void UpdateTranslationJobElement(TranslationJobElementModel model);
        void UpdateTranslationJobElementsToDefaultContent(Guid translationJobGuid);
        #endregion

        #region Translator
        List<TranslationJobTranslatorModel> GetTranslators(Guid translationJobGuid, bool IsHasPermission);
        Guid AddTranslationJobTranslator(TranslationJobTranslatorModel translatorModel);
        void DeleteTranslationJobTranslator(Guid translationJobTranslatorGuid);
        #endregion

        string GoogleTranslationJob(string fromString, string fromLanguage, string toLanguage);

        List<TranslationJobElementModel> GoogleTranslateForElements(List<TranslationJobElementModel> elementList);

        Guid GetToLanguageGuidFromTransContentGuid(Guid TranslationJobContentGuid);

        string GoogleTranslateForElement(TranslationJobElementModel elementModel);

        void UpdateElementStatusWhenFromObjectUpdated(string position, string objectGuid, string updateValue);

        void UpdateElementWhenFromUpdated(string table, string objectGuid, Guid languageGuid);

        TranslationJobElementPagePreviewModel getTranslationJobPagePreviewModel(Guid pageGuid, Guid translationJobContentGuid);
    }
}
