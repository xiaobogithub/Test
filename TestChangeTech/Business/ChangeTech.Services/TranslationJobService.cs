using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ethos.DependencyInjection;
using ChangeTech.Contracts;
using ChangeTech.Models;
using ChangeTech.Entities;
using ChangeTech.IDataRepository;
using System.Xml;
using System.Data;
using Google.API.Translate;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Security;
using System.Net;
using System.Net.Security;
using System.IO;
using Ethos.Utility;

namespace ChangeTech.Services
{
    public class TranslationJobService : ServiceBase, ITranslationJobService
    {
        public const int GoogleTranslationAPIMaxLength = 500;
        #region private method
        private string CalculateCompleted(int finishedElements, int elements)
        {
            string completed = "";
            if (elements == 0)
                completed = "0%";
            else
                completed = Math.Round(finishedElements * 100.0 / elements, 2).ToString() + "%";
            return completed;
        }
        #endregion

        #region TranslationJob

        public List<TranslationJobModel> GetTranslationJobList()
        {
            return GetTranslationJobList(Guid.Empty);
            //List<TranslationJob> translationJobList = Resolve<ITranslationJobRepository>().GetAllTranslationJob().ToList();
            //List<TranslationJobModel> modelList = MapTranslationJobListToModel(translationJobList);

            //return modelList;
        }

        public List<TranslationJobModel> GetTranslationJobList(Guid translatorGuid)
        {
            DataTable translationJobTable = Resolve<IStoreProcedure>().GetTranslationJobs(translatorGuid);
            List<TranslationJobModel> modelList = new List<TranslationJobModel>();
            for (int i = 0; i < translationJobTable.Rows.Count; i++)
            {
                int finishedElements = 0;
                int elements = 0;
                int words = 0;
                int defaultTranslatedField = 1;
                int.TryParse(translationJobTable.Rows[i]["FinishedElements"].ToString(), out finishedElements);
                int.TryParse(translationJobTable.Rows[i]["Elements"].ToString(), out elements);
                int.TryParse(translationJobTable.Rows[i]["Words"].ToString(), out words);
                int.TryParse(translationJobTable.Rows[i]["ContentInTranslated"].ToString(), out defaultTranslatedField);
                TranslationJobModel model = new TranslationJobModel
                {
                    TranslationJobGUID = new Guid(translationJobTable.Rows[i]["TranslationJobGUID"].ToString()),
                    Program = new ProgramBaseModel
                    {
                        ProgramGuid = new Guid(translationJobTable.Rows[i]["ProgramGUID"].ToString()),
                        Name = translationJobTable.Rows[i]["ProgramName"].ToString()
                    },
                    FromLanguage = new LanguageBaseModel
                    {
                        LanguageGUID = new Guid(translationJobTable.Rows[i]["FromLanguageGUID"].ToString()),
                        Name = translationJobTable.Rows[i]["FromLanguageName"].ToString()
                    },
                    ToLanguage = new LanguageBaseModel
                    {
                        LanguageGUID = new Guid(translationJobTable.Rows[i]["ToLanguageGUID"].ToString()),
                        Name = translationJobTable.Rows[i]["ToLanguageName"].ToString()
                    },
                    Translators = translationJobTable.Rows[i]["Translators"].ToString(),
                    FinishedElements = finishedElements,
                    Elements = elements,
                    Words = words,
                    Completed = CalculateCompleted(finishedElements, elements),
                    DefaultTranslatedContent = defaultTranslatedField
                };
                modelList.Add(model);
            }
            //List<TranslationJob> translationJobList = Resolve<ITranslationJobRepository>().GetAllTranslationJob().Where(tj => tj.TranslationJobTranslator.Where(tjt => tjt.TranslatorGUID == translatorGuid).Count() > 0).ToList();
            //List<TranslationJobModel> modelList = MapTranslationJobListToModel(translationJobList);

            return modelList;
        }

        public TranslationJobModel GetTranslationJobByGUID(Guid translationJobGuid)
        {
            TranslationJob translationJobEntity = Resolve<ITranslationJobRepository>().GetTranslationJobByGuid(translationJobGuid);
            TranslationJobModel model = MapTranslationJobToModel(translationJobEntity);
            return model;

        }

        public Guid AddTranslationJob(TranslationJobModel model)
        {
            model.TranslationJobGUID = Guid.NewGuid();
            TranslationJob translationJobEntity = new TranslationJob();
            translationJobEntity.TranslationJobGUID = model.TranslationJobGUID;
            translationJobEntity.ProgramGUID = model.Program.ProgramGuid;
            translationJobEntity.FromLanguageGUID = model.FromLanguage.LanguageGUID;
            translationJobEntity.ToLanguageGUID = model.ToLanguage.LanguageGUID;
            translationJobEntity.TranslationJobStatusID = (int)ChangeTech.Models.TranslationJobStatusEnum.NotStart;
            translationJobEntity.ContentInTranslated = model.DefaultTranslatedContent;
            Resolve<ITranslationJobRepository>().Insert(translationJobEntity);


            //Add Contents if not exist.
            List<TranslationJobContent> translationJobContentList = Resolve<ITranslationJobContentRepository>().GetTranslationJobContentByJobGuid(translationJobEntity.TranslationJobGUID).ToList();
            if (translationJobContentList.Count == 0)
                AddTranslationJobContentAndElement(model);

            return translationJobEntity.TranslationJobGUID;
        }

        public void DeleteTranslationJob(Guid translationJobGuid)
        {
            Resolve<ITranslationJobRepository>().Delete(translationJobGuid);
        }

        public void UpdateTranslationJob(TranslationJobModel model)
        {
            TranslationJob entity = Resolve<ITranslationJobRepository>().GetTranslationJobByGuid(model.TranslationJobGUID);
            entity.ProgramGUID = model.Program.ProgramGuid;
            entity.FromLanguageGUID = model.FromLanguage.LanguageGUID;
            entity.ToLanguageGUID = model.ToLanguage.LanguageGUID;
            Resolve<ITranslationJobRepository>().Update(entity);
        }

        #region private method
        private List<TranslationJobModel> MapTranslationJobListToModel(List<TranslationJob> translationJobList)
        {
            List<TranslationJobModel> modelList = new List<TranslationJobModel>();
            foreach (TranslationJob translationJobEntity in translationJobList)
            {
                TranslationJobModel model = MapTranslationJobToModel(translationJobEntity);
                modelList.Add(model);
            }
            return modelList;
        }

        private TranslationJobModel MapTranslationJobToModel(TranslationJob translationJobEntity)
        {
            if (!translationJobEntity.ProgramReference.IsLoaded)
            {
                translationJobEntity.ProgramReference.Load();
            }
            if (!translationJobEntity.LanguageReference.IsLoaded)
            {
                translationJobEntity.LanguageReference.Load();
            }
            if (!translationJobEntity.Language1Reference.IsLoaded)
            {
                translationJobEntity.Language1Reference.Load();
            }
            string translatorNames = GetTranslatorNames(translationJobEntity.TranslationJobGUID);

            int finishedElements = 0;
            int elements = 0;
            int words = 0;
            string completed = "";
            List<TranslationJobContentModel> contentModelList = GetTranslationJobContentList(translationJobEntity.TranslationJobGUID);
            foreach (TranslationJobContentModel contentModel in contentModelList)
            {
                finishedElements += contentModel.FinishedElements;
                elements += contentModel.Elements;
                words += contentModel.Words;
            }
            if (elements == 0)
                completed = "0%";
            else
                completed = Math.Round(finishedElements * 100.0 / elements, 2).ToString() + "%";

            TranslationJobModel model = new TranslationJobModel
            {
                TranslationJobGUID = translationJobEntity.TranslationJobGUID,
                Program = new ProgramBaseModel
                {
                    ProgramGuid = translationJobEntity.Program.ProgramGUID,
                    Name = translationJobEntity.Program.Name
                },
                FromLanguage = new LanguageBaseModel
                {
                    LanguageGUID = translationJobEntity.Language.LanguageGUID,
                    Name = translationJobEntity.Language.Name
                },
                ToLanguage = new LanguageBaseModel
                {
                    LanguageGUID = translationJobEntity.Language1.LanguageGUID,
                    Name = translationJobEntity.Language1.Name
                },
                Translators = translatorNames,
                FinishedElements = finishedElements,
                Elements = elements,
                Words = words,
                Completed = completed,
                DefaultTranslatedContent = translationJobEntity.ContentInTranslated == null ? 1 : (int)translationJobEntity.ContentInTranslated
            };
            return model;
        }

        private string GetTranslatorNames(Guid translationJobGUID)
        {
            List<TranslationJobTranslatorModel> translators = GetTranslators(translationJobGUID, true);
            string translatorNames = string.Empty;
            if (translators != null && translators.Count > 0)
            {
                foreach (TranslationJobTranslatorModel translator in translators)
                {
                    translatorNames += translator.TranslatorName;
                    translatorNames += ",";
                }
                translatorNames = translatorNames.Remove(translatorNames.LastIndexOf(','));
            }
            return translatorNames;
        }
        #endregion

        #endregion

        #region TranslationJobContent
        public List<TranslationJobContentModel> GetTranslationJobContentList(Guid translationJobGuid)
        {
            DataTable translationJobContentTable = Resolve<IStoreProcedure>().GetTranslationJobContents(translationJobGuid);
            List<TranslationJobContentModel> modelList = new List<TranslationJobContentModel>();
            for (int i = 0; i < translationJobContentTable.Rows.Count; i++)
            {
                int finishedElements = 0;
                int elements = 0;
                int words = 0;
                int.TryParse(translationJobContentTable.Rows[i]["FinishedElements"].ToString(), out finishedElements);
                int.TryParse(translationJobContentTable.Rows[i]["Elements"].ToString(), out elements);
                int.TryParse(translationJobContentTable.Rows[i]["Words"].ToString(), out words);
                TranslationJobContentModel model = new TranslationJobContentModel
                {
                    TranslationJobGUID = new Guid(translationJobContentTable.Rows[i]["TranslationJobGUID"].ToString()),
                    TranslationJobContentGUID = new Guid(translationJobContentTable.Rows[i]["TranslationJobContentGUID"].ToString()),
                    ContentName = translationJobContentTable.Rows[i]["TranslationJobContentName"].ToString(),
                    Note = translationJobContentTable.Rows[i]["Note"].ToString(),
                    FinishedElements = finishedElements,
                    Elements = elements,
                    Words = words,
                    Completed = CalculateCompleted(finishedElements, elements)
                };
                modelList.Add(model);
            }
            //List<TranslationJobContent> translationJobContentList = Resolve<ITranslationJobContentRepository>().GetTranslationJobContentByJobGuid(translationJobGuid).OrderBy(t => t.Day == null ? int.MaxValue : t.Day).ToList();
            //List<TranslationJobContentModel> modelList = MapTranslationJobContentListToModel(translationJobContentList);
            return modelList;
        }

        public void UpdateTranslationJobContent(TranslationJobContentModel model)
        {
            TranslationJobContent entity = Resolve<ITranslationJobContentRepository>().GetTranslationJobContentByGuid(model.TranslationJobContentGUID);
            // so far, only update note
            if (model.Note != null)
                entity.Note = model.Note;
            Resolve<ITranslationJobContentRepository>().Update(entity);
        }

        #region private method
        private List<TranslationJobContentModel> MapTranslationJobContentListToModel(List<TranslationJobContent> translationJobContentList)
        {
            List<TranslationJobContentModel> modelList = new List<TranslationJobContentModel>();
            foreach (TranslationJobContent translationJobContentEntity in translationJobContentList)
            {
                TranslationJobContentModel model = MapTranslationJobContentToModel(translationJobContentEntity);
                modelList.Add(model);
            }
            return modelList;
        }

        private TranslationJobContentModel MapTranslationJobContentToModel(TranslationJobContent translationJobContentEntity)
        {
            int finishedElements = 0;
            int elements = 0;
            int words = 0;
            string completed = "";
            List<TranslationJobElementModel> elementModelList = GetTranslationJobElementList(translationJobContentEntity.TranslationJobContentGUID);
            foreach (TranslationJobElementModel elementModel in elementModelList)
            {
                if (elementModel.StatusID == (int)ChangeTech.Models.TranslationJobStatusEnum.Finished)
                    finishedElements++;
                elements++;
                // get words elementModel.Original
                //words += method(elementModel.Original);
                words += elementModel.Words; //GetTranslationJobWordsCount(elementModel.Original);
            }
            if (elements == 0)
                completed = "0%";
            else
                completed = Math.Round(finishedElements * 100.0 / elements, 2).ToString() + "%";
            TranslationJobContentModel model = new TranslationJobContentModel
            {
                TranslationJobContentGUID = translationJobContentEntity.TranslationJobContentGUID,
                TranslationJobGUID = translationJobContentEntity.TranslationJobGUID,
                ContentName = translationJobContentEntity.TranslationJobContentName,
                Note = translationJobContentEntity.Note,
                FinishedElements = finishedElements,
                Elements = elements,
                Words = words,
                Completed = completed
            };
            return model;
        }

        private int GetTranslationJobWordsCount(string WordText)
        {
            int WordsCount = 0;
            if (string.IsNullOrEmpty(WordText.Trim()))
            {
                WordsCount = 0;
            }
            else
            {
                string[] wordsSplit = WordText.Trim().Split(' ');
                WordsCount = wordsSplit.Count();
            }
            return WordsCount;
        }
        #endregion

        #endregion

        #region TranslationJobElement
        public List<TranslationJobElementModel> GetTranslationJobElementList(Guid translationJobContentId)
        {
            //List<TranslationJobElement> translationJobElementList = Resolve<ITranslationJobElementRepository>().GetTranslationJobElementByContentGuid(translationJobContentId).ToList();
            TranslationJobContent content = Resolve<ITranslationJobContentRepository>().GetTranslationJobContentByGuid(translationJobContentId);
            if (!content.TranslationJobReference.IsLoaded) content.TranslationJobReference.Load();
            int defaultTranslated = content.TranslationJob.ContentInTranslated == null ? (int)DefaultContentInTranslatedFieldEnum.OriginalText : (int)content.TranslationJob.ContentInTranslated;
            DataTable elementTable = Resolve<IStoreProcedure>().GetTranslationJobElements(translationJobContentId);
            List<TranslationJobElementModel> modelList = new List<TranslationJobElementModel>(); //= MapTranslationJobElementListToModel(translationJobElementList);
            for (int i = 0; i < elementTable.Rows.Count; i++)
            {
                int words = 0;
                int.TryParse(elementTable.Rows[i]["Words"].ToString().Trim(), out words);
                TranslationJobElementModel elementModel = new TranslationJobElementModel
                {
                    Original = elementTable.Rows[i]["FromContent"].ToString(),
                    FromObjectGUID = elementTable.Rows[i]["FromObjectGUID"].ToString(),
                    MaxLength = elementTable.Rows[i]["MaxLength"].ToString().Trim() == "" ? null : (int?)int.Parse(elementTable.Rows[i]["MaxLength"].ToString().Trim()),
                    Object = elementTable.Rows[i]["Object"].ToString(),
                    Position = elementTable.Rows[i]["Position"].ToString(),
                    StatusID = int.Parse(elementTable.Rows[i]["StatusGUID"].ToString()),
                    Translated = elementTable.Rows[i]["ToContent"].ToString(),
                    TranslationJobContentGUID = new Guid(elementTable.Rows[i]["TranslationJobContentGUID"].ToString()),
                    ToObjectGUID = elementTable.Rows[i]["ToObjectGUID"].ToString(),
                    TranslationJobElementGUID = new Guid(elementTable.Rows[i]["TranslationJobElementGUID"].ToString()),
                    Order = elementTable.Rows[i]["ElementOrder"].ToString() == "" ? "" : (elementTable.Rows[i]["ElementOrder"].ToString().IndexOf('-') == 0 ? elementTable.Rows[i]["ElementOrder"].ToString().Substring(0, 15) : elementTable.Rows[i]["ElementOrder"].ToString().Substring(0, 14)),//for distinguish between pages. // 15 means the day -1,-2... toSTRING('d4') ="-0001","-0002", the count is 5.
                    Words = words,
                    DefaultTranslatedContent = defaultTranslated
                };
                modelList.Add(elementModel);
            }
            //modelList = GoogleTranslateForElements(modelList);
            return modelList;
        }

        public void UpdateTranslationJobElement(TranslationJobElementModel model)
        {
            TranslationJobElement entity = Resolve<ITranslationJobElementRepository>().GetTranslationJobElementByGuid(model.TranslationJobElementGUID);
            // so far, only update note
            if (model.Translated != null)
            {
                entity.ToContent = model.Translated;
                entity.StatusGUID = (int)ChangeTech.Models.TranslationJobStatusEnum.Finished; //finished.
                Resolve<ITranslationJobElementRepository>().Update(entity);
            }

        }

        public void UpdateTranslationJobElementsToDefaultContent(Guid translationJobGuid)
        {
            TranslationJob translationJobEntity = Resolve<ITranslationJobRepository>().GetTranslationJobByGuid(translationJobGuid);
            List<TranslationJobElement> translationJobElementList = Resolve<ITranslationJobElementRepository>().GetTranslationJobElementsByJobGuid(translationJobGuid).Where(x => x.StatusGUID == (int)ChangeTech.Models.TranslationJobStatusEnum.NotStart).ToList();

            switch (translationJobEntity.ContentInTranslated)
            {
                case (int)ChangeTech.Models.DefaultContentInTranslatedFieldEnum.OriginalText:
                    // set status to finished
                    foreach (TranslationJobElement translationJobElement in translationJobElementList)
                    {
                        translationJobElement.StatusGUID = (int)ChangeTech.Models.TranslationJobStatusEnum.Finished;
                        Resolve<ITranslationJobElementRepository>().Update(translationJobElement);
                    }
                    break;
                case (int)ChangeTech.Models.DefaultContentInTranslatedFieldEnum.GoogleTranslation:
                    // Call google translate api, update content, then set status to finished
                    string translated = "";
                    foreach (TranslationJobElement translationJobElement in translationJobElementList)
                    {
                        // So far only need Original and TranslationJobContentGUID
                        TranslationJobElementModel jobElement = new TranslationJobElementModel
                        {
                            Original = translationJobElement.FromContent,
                            FromObjectGUID = translationJobElement.FromObjectGUID,
                            MaxLength = translationJobElement.MaxLength,
                            Object = translationJobElement.Object,
                            Position = translationJobElement.Position,
                            StatusID = translationJobElement.StatusGUID,
                            Translated = translationJobElement.ToContent,
                            TranslationJobContentGUID = translationJobElement.TranslationJobContentGUID,
                            ToObjectGUID = translationJobElement.ToObjectGUID,
                            TranslationJobElementGUID = translationJobElement.TranslationJobElementGUID,
                            Order = translationJobElement.ElementOrder == "" ? "" : (translationJobElement.ElementOrder.IndexOf('-') == 0 ? translationJobElement.ElementOrder.Substring(0, 15) : translationJobElement.ElementOrder.Substring(0, 14)) //for distinguish between pages. // 15 means the day -1,-2... toSTRING('d4') ="-0001","-0002", the count is 5.
                        };
                        translated = Resolve<ITranslationJobService>().GoogleTranslateForElement(jobElement);
                        if (!string.IsNullOrEmpty(translated))
                            translationJobElement.ToContent = translated;
                        else
                            translationJobElement.ToContent = "";
                        translationJobElement.StatusGUID = (int)ChangeTech.Models.TranslationJobStatusEnum.Finished;
                        Resolve<ITranslationJobElementRepository>().Update(translationJobElement);
                        LogUtility.LogUtilityIntance.LogMessage(string.Format("{0};{1};{2};{3};{4}", translationJobElement.Object, translationJobElement.ToObjectGUID, translationJobElement.Position, translationJobElement.ToContent, DateTime.UtcNow.ToString()));
                        SetElementValue(translationJobElement);
                    }
                    break;
                case (int)ChangeTech.Models.DefaultContentInTranslatedFieldEnum.Nothing:
                    // Update content to nothing, set status to finished
                    foreach (TranslationJobElement translationJobElement in translationJobElementList)
                    {
                        translationJobElement.ToContent = "";
                        translationJobElement.StatusGUID = (int)ChangeTech.Models.TranslationJobStatusEnum.Finished;
                        Resolve<ITranslationJobElementRepository>().Update(translationJobElement);
                        LogUtility.LogUtilityIntance.LogMessage(string.Format("{0};{1};{2};{3};{4}", translationJobElement.Object, translationJobElement.ToObjectGUID, translationJobElement.Position, translationJobElement.ToContent, DateTime.UtcNow.ToString()));
                        SetElementValue(translationJobElement);
                    }
                    break;
            }
        }

        #region private method
        //private  List<TranslationJobElementModel> MapTranslationJobElementListToModel(List<TranslationJobElement> translationJobElementList)
        //{
        //    List<TranslationJobElementModel> modelList = new List<TranslationJobElementModel>();
        //    foreach (TranslationJobElement translationJobElementEntity in translationJobElementList)
        //    {
        //        TranslationJobElementModel model = MapTranslationJobElementToModel(translationJobElementEntity);
        //        modelList.Add(model);
        //    }
        //    return modelList;
        //}

        //private TranslationJobElementModel MapTranslationJobElementToModel(TranslationJobElement translationJobElementEntity)
        //{
        //    string original = "";
        //    string googleTranslate = "";
        //    string translated = "";
        //    if (!translationJobElementEntity.TranslationJobContentReference.IsLoaded)
        //    {
        //        translationJobElementEntity.TranslationJobContentReference.Load();
        //    }
        //    if (!translationJobElementEntity.TranslationJobContent.TranslationJobReference.IsLoaded)
        //    {
        //        translationJobElementEntity.TranslationJobContent.TranslationJobReference.Load();
        //    }
        //    if (!translationJobElementEntity.TranslationJobContent.TranslationJob.LanguageReference.IsLoaded)
        //    {
        //        translationJobElementEntity.TranslationJobContent.TranslationJob.LanguageReference.Load();
        //    }
        //    if (!translationJobElementEntity.TranslationJobContent.TranslationJob.Language1Reference.IsLoaded)
        //    {
        //        translationJobElementEntity.TranslationJobContent.TranslationJob.Language1Reference.Load();
        //    }
        //    TranslationJobElementValueModel elementFromValueModel = new TranslationJobElementValueModel
        //    {
        //        languageGuid = translationJobElementEntity.TranslationJobContent.TranslationJob.Language.LanguageGUID,
        //        Object = translationJobElementEntity.Object,
        //        ObjectGUID = translationJobElementEntity.FromObjectGUID,
        //        Position = translationJobElementEntity.Position
        //    };
        //    TranslationJobElementValueModel elementToValueModel = new TranslationJobElementValueModel
        //    {
        //        languageGuid = translationJobElementEntity.TranslationJobContent.TranslationJob.Language1.LanguageGUID,
        //        Object = translationJobElementEntity.Object,
        //        ObjectGUID = translationJobElementEntity.ToObjectGUID,
        //        Position = translationJobElementEntity.Position
        //    };
        //    original = GetElementValue(elementFromValueModel);
        //    translated = GetElementValue(elementToValueModel);

        //    string fromLanguageCode = GetLanguageCodeForAPI(translationJobElementEntity.TranslationJobContent.TranslationJob.Language.Name);
        //    string toLanguageCode = GetLanguageCodeForAPI(translationJobElementEntity.TranslationJobContent.TranslationJob.Language1.Name);
        //    googleTranslate = GoogleTranslationJob(original, fromLanguageCode, toLanguageCode);

        //    TranslationJobElementModel model = new TranslationJobElementModel
        //    {
        //        TranslationJobElementGUID = translationJobElementEntity.TranslationJobElementGUID,
        //        FromObjectGUID = translationJobElementEntity.FromObjectGUID,
        //        ToObjectGUID = translationJobElementEntity.ToObjectGUID,
        //        Object = translationJobElementEntity.Object,
        //        Position = translationJobElementEntity.Position,
        //        MaxLength = translationJobElementEntity.MaxLength,
        //        StatusID = translationJobElementEntity.StatusGUID,
        //        Original = original,
        //        GoogleTranslate = googleTranslate,
        //        Translated = translated
        //    };
        //    return model;
        //}

        private void SetElementValue(TranslationJobElement translationJobElement)
        {
            switch (translationJobElement.Object)
            {
                case "PageContent":
                    Resolve<ITranslationService>().TranslatePageContent(new Guid(translationJobElement.ToObjectGUID), translationJobElement.Position, translationJobElement.ToContent);
                    break;
                case "PageQuestionContent":
                    Resolve<ITranslationService>().TranslatePageQuestionContent(new Guid(translationJobElement.ToObjectGUID), translationJobElement.Position, translationJobElement.ToContent);
                    break;
                case "PageQuestionItemContent":
                    Resolve<ITranslationService>().TranslatePageQuestionItemContent(new Guid(translationJobElement.ToObjectGUID), translationJobElement.Position, translationJobElement.ToContent);
                    break;
                case "ProgramRoom":
                    Resolve<ITranslationService>().TranslateProgramRoom(new Guid(translationJobElement.ToObjectGUID), translationJobElement.Position, translationJobElement.ToContent);
                    break;
                case "GraphContent":
                    Resolve<ITranslationService>().TranslateGraphContent(new Guid(translationJobElement.ToObjectGUID), translationJobElement.Position, translationJobElement.ToContent);
                    break;
                case "EmailTemplate":
                    Resolve<ITranslationService>().TranslateEmailTemplate(new Guid(translationJobElement.ToObjectGUID), translationJobElement.Position, translationJobElement.ToContent);
                    break;
                case "GraphItemContent":
                    Resolve<ITranslationService>().TranslateGraphItemContent(new Guid(translationJobElement.ToObjectGUID), translationJobElement.Position, translationJobElement.ToContent);
                    break;
                case "HelpItem":
                    Resolve<ITranslationService>().TranslateHelpItem(new Guid(translationJobElement.ToObjectGUID), translationJobElement.Position, translationJobElement.ToContent);
                    break;
                case "Preferences":
                    Resolve<ITranslationService>().TranslatePreference(new Guid(translationJobElement.ToObjectGUID), translationJobElement.Position, translationJobElement.ToContent);
                    break;
                case "Session":
                    Resolve<ITranslationService>().TranslateSession(new Guid(translationJobElement.ToObjectGUID), translationJobElement.Position, translationJobElement.ToContent);
                    break;
                case "SpecialString":
                    Guid toLanguageGuid = Resolve<ITranslationJobService>().GetToLanguageGuidFromTransContentGuid(translationJobElement.TranslationJobContentGUID);
                    Resolve<ITranslationService>().TranslateSepcialString(translationJobElement.ToObjectGUID, toLanguageGuid, translationJobElement.Position, translationJobElement.ToContent);
                    break;
                case "TipMessage":
                    Resolve<ITranslationService>().TranslateTipMessage(new Guid(translationJobElement.ToObjectGUID), translationJobElement.Position, translationJobElement.ToContent);
                    break;
                case "UserMenu":
                    Resolve<ITranslationService>().TranslateUserMenu(new Guid(translationJobElement.ToObjectGUID), translationJobElement.Position, translationJobElement.ToContent);
                    break;
                case "AccessoryTemplate":
                    Resolve<ITranslationService>().TranslateAccessoryTemplate(new Guid(translationJobElement.ToObjectGUID), translationJobElement.Position, translationJobElement.ToContent);
                    break;
                case "Relapse":
                case "PageSequence":
                    Resolve<ITranslationService>().TranslatePageSequence(new Guid(translationJobElement.ToObjectGUID), translationJobElement.Position, translationJobElement.ToContent);
                    break;
                case "ScreenResultTemplatePageLine":
                    Resolve<ITranslationService>().TranslateScreenResultTemplatePageLine(new Guid(translationJobElement.ToObjectGUID), translationJobElement.Position, translationJobElement.ToContent);
                    break;
            }
        }

        private string GetElementValue(TranslationJobElementValueModel elementModel)
        {
            string elementValue = string.Empty;
            switch (elementModel.Object)
            {
                case "PageContent":
                    elementValue = Resolve<ITranslationService>().GetPageContent(new Guid(elementModel.ObjectGUID), elementModel.Position);
                    break;
                case "PageQuestionContent":
                    elementValue = Resolve<ITranslationService>().GetPageQuestionContent(new Guid(elementModel.ObjectGUID), elementModel.Position);
                    break;
                case "PageQuestionItemContent":
                    elementValue = Resolve<ITranslationService>().GetPageQuestionItemContent(new Guid(elementModel.ObjectGUID), elementModel.Position);
                    break;
                case "ProgramRoom":
                    elementValue = Resolve<ITranslationService>().GetProgramRoom(new Guid(elementModel.ObjectGUID), elementModel.Position);
                    break;
                case "GraphContent":
                    elementValue = Resolve<ITranslationService>().GetGraphContent(new Guid(elementModel.ObjectGUID), elementModel.Position);
                    break;
                case "EmailTemplate":
                    elementValue = Resolve<ITranslationService>().GetEmailTemplate(new Guid(elementModel.ObjectGUID), elementModel.Position);
                    break;
                case "GraphItemContent":
                    elementValue = Resolve<ITranslationService>().GetGraphItemContent(new Guid(elementModel.ObjectGUID), elementModel.Position);
                    break;
                case "HelpItem":
                    elementValue = Resolve<ITranslationService>().GetHelpItem(new Guid(elementModel.ObjectGUID), elementModel.Position);
                    break;
                case "Preferences":
                    elementValue = Resolve<ITranslationService>().GetPreference(new Guid(elementModel.ObjectGUID), elementModel.Position);
                    break;
                case "Session":
                    elementValue = Resolve<ITranslationService>().GetSession(new Guid(elementModel.ObjectGUID), elementModel.Position);
                    break;
                case "SpecialString":
                    elementValue = Resolve<ITranslationService>().GetSepcialString(elementModel.ObjectGUID, elementModel.languageGuid, elementModel.Position);
                    break;
                case "TipMessage":
                    elementValue = Resolve<ITranslationService>().GetTipMessage(new Guid(elementModel.ObjectGUID), elementModel.Position);
                    break;
                case "UserMenu":
                    elementValue = Resolve<ITranslationService>().GetUserMenu(new Guid(elementModel.ObjectGUID), elementModel.Position);
                    break;
                case "AccessoryTemplate":
                    elementValue = Resolve<ITranslationService>().GetAccessoryTemplate(new Guid(elementModel.ObjectGUID), elementModel.Position);
                    break;
                case "Relapse":
                case "PageSequence":
                    elementValue = Resolve<ITranslationService>().GetPageSequence(new Guid(elementModel.ObjectGUID), elementModel.Position);
                    break;
                case "ScreenResultTemplatePageLine":
                    elementValue = Resolve<ITranslationService>().GetPageContent(new Guid(elementModel.ObjectGUID), elementModel.Position);
                    break;
            }

            return elementValue;
        }

        #endregion

        #endregion

        // this function is private, used for getTranslators.
        private List<TranslationJobTranslatorModel> GetTranslationJobTranslatorsByTranslationJobGUID(Guid TranslationJobGuid)
        {
            List<TranslationJobTranslatorModel> translators = new List<TranslationJobTranslatorModel>();

            List<TranslationJobTranslator> translatorEntities = new List<TranslationJobTranslator>();
            translatorEntities = Resolve<ITranslationJobTranslatorRepository>().GetTranslatorsByTranslationJobGUID(TranslationJobGuid).ToList();
            foreach (TranslationJobTranslator translatorEntity in translatorEntities)
            {
                TranslationJobTranslatorModel translatorModel = new TranslationJobTranslatorModel();
                translatorModel.TranslationJobGUID = translatorEntity.TranslationJobGUID;
                translatorModel.TranslationJobTranslatorGUID = translatorEntity.TranslationJobTranslatorGUID;
                translatorModel.TranslatorGUID = translatorEntity.TranslatorGUID;
                if (!translatorEntity.UserReference.IsLoaded)
                {
                    translatorEntity.UserReference.Load();
                }
                //translatorModel.TranslatorName = translatorEntity.User.Email;
                translatorModel.TranslatorName = translatorEntity.User.FirstName + " " + translatorEntity.User.LastName;
                translators.Add(translatorModel);
            }

            return translators;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="translationJobGuid"></param>
        /// <param name="IsHasPermission">True: get the translator who has the permission to this job. False: the opposite.</param>
        /// <returns></returns>
        public List<TranslationJobTranslatorModel> GetTranslators(Guid translationJobGuid, bool IsHasPermission)
        {
            List<TranslationJobTranslatorModel> transtors = new List<TranslationJobTranslatorModel>();
            List<TranslationJobTranslatorModel> transtorsInJob = GetTranslationJobTranslatorsByTranslationJobGUID(translationJobGuid);
            if (IsHasPermission)
            {
                transtors = transtorsInJob;
                //                transtors.Where(s=>s.TranslatorGUID in allTranstorUsers);
            }
            else
            {
                //UsersModel allTranstorUsers = Resolve<IUserService>().GetUserByUserType(ChangeTech.Models.UserType.Translator);
                IQueryable<User> userEntities = Resolve<IUserRepository>().GetUserByUserType((int)ChangeTech.Models.UserTypeEnum.Translator);
                userEntities = userEntities.Where(u => u.TranslationJobTranslator.Where(t => t.TranslationJobGUID == translationJobGuid).Count() == 0);
                foreach (User userEntity in userEntities)
                {
                    //List<TranslationJobTranslatorModel> listOfInJOb = new List<TranslationJobTranslatorModel>();
                    //if (transtorsInJob != null && transtorsInJob.Count > 0)
                    //{
                    //    listOfInJOb = transtorsInJob.Where(s => s.TranslatorGUID == userEntity.UserGUID).ToList();
                    //    if (listOfInJOb != null && listOfInJOb.Count > 0)
                    //    {
                    //        continue;
                    //    }
                    //    else
                    //    {
                    //        TranslationJobTranslatorModel translator = new TranslationJobTranslatorModel();
                    //        translator.TranslatorGUID = userEntity.UserGUID;
                    //        translator.TranslatorName = userEntity.Email;
                    //        transtors.Add(translator);
                    //    }
                    //}
                    //else
                    //{
                    //    TranslationJobTranslatorModel translator = new TranslationJobTranslatorModel();
                    //    translator.TranslatorGUID = userEntity.UserGUID;
                    //    translator.TranslatorName = userEntity.Email;
                    //    transtors.Add(translator);
                    //}

                    TranslationJobTranslatorModel translator = new TranslationJobTranslatorModel();
                    translator.TranslatorGUID = userEntity.UserGUID;
                    //translator.TranslatorName = userEntity.Email;
                    translator.TranslatorName = userEntity.FirstName + " " + userEntity.LastName;
                    transtors.Add(translator);

                }
            }
            return transtors;
        }

        public Guid AddTranslationJobTranslator(TranslationJobTranslatorModel translatorModel)
        {
            TranslationJobTranslator TranslationJobTranslatorEntity = new TranslationJobTranslator();
            TranslationJobTranslatorEntity.TranslationJobGUID = translatorModel.TranslationJobGUID;
            TranslationJobTranslatorEntity.TranslationJobTranslatorGUID = Guid.NewGuid(); //translatorModel.TranslationJobTranslatorGUID;
            TranslationJobTranslatorEntity.TranslatorGUID = translatorModel.TranslatorGUID;

            Resolve<ITranslationJobTranslatorRepository>().Insert(TranslationJobTranslatorEntity);
            return TranslationJobTranslatorEntity.TranslationJobTranslatorGUID;

        }

        public void DeleteTranslationJobTranslator(Guid translationJobTranslatorGuid)
        {
            Resolve<ITranslationJobTranslatorRepository>().Delete(translationJobTranslatorGuid);
        }


        #region Add translationJob Content and Element
        //TODO:need to refactor this. to return a model and then use this model for anything.
        private Guid AddTranslationJobContent(TranslationJobContentModel ContentModel)
        {
            TranslationJobContent contentEntity = new TranslationJobContent();
            contentEntity.Note = ContentModel.Note;
            contentEntity.TranslationJobContentGUID = Guid.NewGuid();
            contentEntity.TranslationJobContentName = ContentModel.ContentName;
            contentEntity.TranslationJobGUID = ContentModel.TranslationJobGUID;
            Resolve<ITranslationJobContentRepository>().Insert(contentEntity);

            return contentEntity.TranslationJobContentGUID;
        }
        private Guid AddTranslationJobElement(TranslationJobElementModel ElementModel)
        {
            TranslationJobElement elementEntity = new TranslationJobElement();
            //elementEntity.
            elementEntity.MaxLength = ElementModel.MaxLength;
            elementEntity.Object = ElementModel.Object;
            elementEntity.FromObjectGUID = ElementModel.FromObjectGUID;
            elementEntity.ToObjectGUID = ElementModel.ToObjectGUID;
            elementEntity.Position = ElementModel.Position;
            elementEntity.StatusGUID = ElementModel.StatusID;
            elementEntity.TranslationJobContentGUID = ElementModel.TranslationJobContentGUID;
            elementEntity.TranslationJobElementGUID = Guid.NewGuid();
            elementEntity.FromContent = ElementModel.Original;
            elementEntity.ToContent = ElementModel.Translated;
            elementEntity.Words = ElementModel.Words;
            elementEntity.ElementOrder = ElementModel.Order;

            Resolve<ITranslationJobElementRepository>().Insert(elementEntity);
            return elementEntity.TranslationJobElementGUID;

        }

        private void AddTranslationJobContentAndElement(TranslationJobModel model)
        {
            #region getTranslation Data
            InsertLogModel insertLogModel = new InsertLogModel
            {
                ActivityLogType = (int)LogTypeEnum.AddTranslationJob,
                Browser = string.Empty,
                From = string.Empty,
                IP = string.Empty,
                Message = string.Format("AddTranslationJobContentAndElement:Begin to Get the translationData.The time is {0}", DateTime.UtcNow),
                PageGuid = Guid.Empty,
                PageSequenceGuid = Guid.Empty,
                SessionGuid = Guid.Empty,
                ProgramGuid = model.Program.ProgramGuid
            };
            Resolve<IActivityLogService>().Insert(insertLogModel);

            ProgramModel programModel = Resolve<IProgramService>().GetProgramByProgramGUIDAndLanguageGUID(model.Program.ProgramGuid, model.ToLanguage.LanguageGUID);
            //string translationDataStr = Resolve<IStoreProcedure>().GetTranslationData(programModel.Guid, programModel.StartDay, programModel.StartDay + programModel.DaysCount - 1,
            //    true, true, true, true, true, true, true, true);
            string translationDataStr = Resolve<IStoreProcedure>().GetTranslationDataForTranslate(programModel.Guid, programModel.StartDay, programModel.StartDay + programModel.DaysCount - 1,
                true, true, true, true, true, true, true, true, model.FromLanguage.LanguageGUID);

            insertLogModel = new InsertLogModel
            {
                ActivityLogType = (int)LogTypeEnum.AddTranslationJob,
                Browser = string.Empty,
                From = string.Empty,
                IP = string.Empty,
                Message = string.Format("AddTranslationJobContentAndElement:Get the translationData successfully.The time is {0}", DateTime.UtcNow),
                PageGuid = Guid.Empty,
                PageSequenceGuid = Guid.Empty,
                SessionGuid = Guid.Empty,
                ProgramGuid = model.Program.ProgramGuid
            };
            Resolve<IActivityLogService>().Insert(insertLogModel);
            #endregion

            #region get programData
            ParseProgramData(translationDataStr, programModel, model);
            insertLogModel = new InsertLogModel
            {
                ActivityLogType = (int)LogTypeEnum.AddTranslationJob,
                Browser = string.Empty,
                From = string.Empty,
                IP = string.Empty,
                Message = string.Format("AddTranslationJobContentAndElement:Get the ParaseProgramData successfully.The time is {0}", DateTime.UtcNow),
                PageGuid = Guid.Empty,
                PageSequenceGuid = Guid.Empty,
                SessionGuid = Guid.Empty,
                ProgramGuid = model.Program.ProgramGuid
            };
            Resolve<IActivityLogService>().Insert(insertLogModel);
            #endregion

            #region get sessionData
            ParaseSessionData(translationDataStr, programModel.StartDay, model);
            insertLogModel = new InsertLogModel
            {
                ActivityLogType = (int)LogTypeEnum.AddTranslationJob,
                Browser = string.Empty,
                From = string.Empty,
                IP = string.Empty,
                Message = string.Format("AddTranslationJobContentAndElement:Get the ParaseSessionData successfully.The time is {0}", DateTime.UtcNow),
                PageGuid = Guid.Empty,
                PageSequenceGuid = Guid.Empty,
                SessionGuid = Guid.Empty,
                ProgramGuid = model.Program.ProgramGuid
            };
            Resolve<IActivityLogService>().Insert(insertLogModel);
            #endregion

            #region Add Content
            Guid contentGuid = Guid.NewGuid();
            TranslationJobContent contentEntity = new TranslationJobContent();
            contentEntity.Note = string.Empty;
            contentEntity.TranslationJobContentGUID = contentGuid;
            contentEntity.TranslationJobContentName = "Misc";
            contentEntity.TranslationJobGUID = model.TranslationJobGUID;

            Resolve<ITranslationJobContentRepository>().Insert(contentEntity);
            #endregion

            #region Get misc
            //the follows is 8 true related insert
            model.Order = "1110";
            //model.Order = "9999";
            ParasePageSequenceData(translationDataStr, "0", contentGuid, true, model);//Relapse
            insertLogModel = new InsertLogModel
            {
                ActivityLogType = (int)LogTypeEnum.AddTranslationJob,
                Browser = string.Empty,
                From = string.Empty,
                IP = string.Empty,
                Message = string.Format("AddTranslationJobContentAndElement:Insert Relapse successfully.The time is {0}", DateTime.UtcNow),
                PageGuid = Guid.Empty,
                PageSequenceGuid = Guid.Empty,
                SessionGuid = Guid.Empty,
                ProgramGuid = model.Program.ProgramGuid
            };
            Resolve<IActivityLogService>().Insert(insertLogModel);

            model.Order = "9999-9999-0001";
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(translationDataStr);
            XmlNodeList programRoomList = xmlDoc.SelectNodes(string.Format("//ProgramRooms/ProgramRoom"));
            ConstructWorkSheet(programRoomList, "ProgramRoom", contentGuid, model);
            //worksheetBuilder.Append(ConstructWorkSheet(exportWorkSheetTemplate, "Program Room", programRoomList, programModel.Name, programModel.DefaultLanguageName, "ProgramRoom"));
            insertLogModel = new InsertLogModel
            {
                ActivityLogType = (int)LogTypeEnum.AddTranslationJob,
                Browser = string.Empty,
                From = string.Empty,
                IP = string.Empty,
                Message = string.Format("AddTranslationJobContentAndElement:Insert Program Room successfully.The time is {0}", DateTime.UtcNow),
                PageGuid = Guid.Empty,
                PageSequenceGuid = Guid.Empty,
                SessionGuid = Guid.Empty,
                ProgramGuid = model.Program.ProgramGuid
            };
            Resolve<IActivityLogService>().Insert(insertLogModel);

            model.Order = "9999-9999-0002";
            XmlNodeList accessoryTemplateList = xmlDoc.SelectNodes(string.Format("//AccessoryTemplates/AccessoryTemplate"));
            ConstructWorkSheet(accessoryTemplateList, "AccessoryTemplate", contentGuid, model);
            //worksheetBuilder.Append(ConstructWorkSheet(exportWorkSheetTemplate, "Program Template", accessoryTemplateList, programModel.Name, programModel.DefaultLanguageName, "AccessoryTemplate"));
            insertLogModel = new InsertLogModel
            {
                ActivityLogType = (int)LogTypeEnum.AddTranslationJob,
                Browser = string.Empty,
                From = string.Empty,
                IP = string.Empty,
                Message = string.Format("AddTranslationJobContentAndElement:Insert Program Template successfully.The time is {0}", DateTime.UtcNow),
                PageGuid = Guid.Empty,
                PageSequenceGuid = Guid.Empty,
                SessionGuid = Guid.Empty,
                ProgramGuid = model.Program.ProgramGuid
            };
            Resolve<IActivityLogService>().Insert(insertLogModel);

            model.Order = "9999-9999-0003";
            XmlNodeList emailTemplateList = xmlDoc.SelectNodes(string.Format("//EmailTemplates/EmailTemplate"));
            ConstructWorkSheet(emailTemplateList, "EmailTemplate", contentGuid, model);
            //worksheetBuilder.Append(ConstructWorkSheet(exportWorkSheetTemplate, "Email Template", emailTemplateList, programModel.Name, programModel.DefaultLanguageName, "EmailTemplate"));
            insertLogModel = new InsertLogModel
            {
                ActivityLogType = (int)LogTypeEnum.AddTranslationJob,
                Browser = string.Empty,
                From = string.Empty,
                IP = string.Empty,
                Message = string.Format("AddTranslationJobContentAndElement:Insert Email Template successfully.The time is {0}", DateTime.UtcNow),
                PageGuid = Guid.Empty,
                PageSequenceGuid = Guid.Empty,
                SessionGuid = Guid.Empty,
                ProgramGuid = model.Program.ProgramGuid
            };
            Resolve<IActivityLogService>().Insert(insertLogModel);

            model.Order = "9999-9999-0004";
            XmlNodeList helpItemList = xmlDoc.SelectNodes(string.Format("//HelpItems/HelpItem"));
            ConstructWorkSheet(helpItemList, "HelpItem", contentGuid, model);
            //worksheetBuilder.Append(ConstructWorkSheet(exportWorkSheetTemplate, "Help Items", helpItemList, programModel.Name, programModel.DefaultLanguageName, "HelpItem"));
            insertLogModel = new InsertLogModel
            {
                ActivityLogType = (int)LogTypeEnum.AddTranslationJob,
                Browser = string.Empty,
                From = string.Empty,
                IP = string.Empty,
                Message = string.Format("AddTranslationJobContentAndElement:Insert Help Items successfully.The time is {0}", DateTime.UtcNow),
                PageGuid = Guid.Empty,
                PageSequenceGuid = Guid.Empty,
                SessionGuid = Guid.Empty,
                ProgramGuid = model.Program.ProgramGuid
            };
            Resolve<IActivityLogService>().Insert(insertLogModel);

            model.Order = "9999-9999-0005";
            XmlNodeList userMenuList = xmlDoc.SelectNodes(string.Format("//UserMenus/UserMenu"));
            ConstructWorkSheet(userMenuList, "UserMenu", contentGuid, model);
            // worksheetBuilder.Append(ConstructWorkSheet(exportWorkSheetTemplate, "User Menus", userMenuList, programModel.Name, programModel.DefaultLanguageName, "UserMenu"));
            insertLogModel = new InsertLogModel
            {
                ActivityLogType = (int)LogTypeEnum.AddTranslationJob,
                Browser = string.Empty,
                From = string.Empty,
                IP = string.Empty,
                Message = string.Format("AddTranslationJobContentAndElement:Insert User Menus successfully.The time is {0}", DateTime.UtcNow),
                PageGuid = Guid.Empty,
                PageSequenceGuid = Guid.Empty,
                SessionGuid = Guid.Empty,
                ProgramGuid = model.Program.ProgramGuid
            };
            Resolve<IActivityLogService>().Insert(insertLogModel);

            model.Order = "9999-9999-0006";
            XmlNodeList tipMessageList = xmlDoc.SelectNodes(string.Format("//TipMessages/TipMessage"));
            ConstructWorkSheet(tipMessageList, "TipMessage", contentGuid, model);
            //worksheetBuilder.Append(ConstructWorkSheet(exportWorkSheetTemplate, "Tip Messages", tipMessageList, programModel.Name, programModel.DefaultLanguageName, "TipMessage"));
            insertLogModel = new InsertLogModel
            {
                ActivityLogType = (int)LogTypeEnum.AddTranslationJob,
                Browser = string.Empty,
                From = string.Empty,
                IP = string.Empty,
                Message = string.Format("AddTranslationJobContentAndElement:Insert TipMessage successfully.The time is {0}", DateTime.UtcNow),
                PageGuid = Guid.Empty,
                PageSequenceGuid = Guid.Empty,
                SessionGuid = Guid.Empty,
                ProgramGuid = model.Program.ProgramGuid
            };
            Resolve<IActivityLogService>().Insert(insertLogModel);

            //ProgramDailySMSs  content
            model.Order = "9999-9999-0008";
            XmlNodeList programDailySMSNodes = xmlDoc.SelectNodes("//ProgramDailySMSs/ProgramDailySMS");
            CheckprogramDailySMS(programDailySMSNodes, "ProgramDailySMS", contentGuid, model);
            //ConstructWorkSheet(programDailySMSNodes, "ProgramDailySMS", contentGuid, model);

            //worksheetBuilder.Append(ConstructWorkSheet(exportWorkSheetTemplate, "Program Daily SMS", programDailySMSNodes, programModel.Name, programModel.DefaultLanguageName, "ProgramDailySMS"));
            //foreach (XmlNode programDailySMSModel in programDailySMSNodes)
            //{
            //    string sessionID = programDailySMSModel.Attributes["ID"].Value;
            //    Guid sessionGuid = new Guid(sessionID);
            //}

            insertLogModel = new InsertLogModel
            {
                ActivityLogType = (int)LogTypeEnum.AddTranslationJob,
                Browser = string.Empty,
                From = string.Empty,
                IP = string.Empty,
                Message = string.Format("AddTranslationJobContentAndElement:Insert ProgramDailySMS successfully.The time is {0}", DateTime.UtcNow),
                PageGuid = Guid.Empty,
                PageSequenceGuid = Guid.Empty,
                SessionGuid = Guid.Empty,
                ProgramGuid = model.Program.ProgramGuid
            };
            Resolve<IActivityLogService>().Insert(insertLogModel);

            #region MyRegion
            //model.Order = "9999-9999-0007";
            //XmlNodeList specialStringList = xmlDoc.SelectNodes(string.Format("//SpecialStrings/SpecialString"));
            //ConstructWorkSheet(specialStringList, "SpecialString", contentGuid, model);
            ////worksheetBuilder.Append(ConstructWorkSheet(exportWorkSheetTemplate, "Special Strings", specialStringList, programModel.Name, programModel.DefaultLanguageName, "SpecialString"));
            //insertLogModel = new InsertLogModel
            //{
            //    ActivityLogType = (int)LogTypeEnum.AddTranslationJob,
            //    Browser = string.Empty,
            //    From = string.Empty,
            //    IP = string.Empty,
            //    Message = string.Format("AddTranslationJobContentAndElement:Insert SpecialString successfully.The time is {0}", DateTime.UtcNow),
            //    PageGuid = Guid.Empty,
            //    PageSequenceGuid = Guid.Empty,
            //    SessionGuid = Guid.Empty,
            //    ProgramGuid = model.Program.ProgramGuid
            //};
            //Resolve<IActivityLogService>().Insert(insertLogModel); 
            #endregion
            #endregion
        }

        private void ParseProgramData(string translationDataStr, ProgramModel programModel, TranslationJobModel translationJobModelModel)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(translationDataStr);
            XmlNodeList programNodes = xmlDoc.SelectNodes("//Program");
            foreach (XmlNode programNode in programNodes)
            {
                string programID = programNode.Attributes["ID"].Value;
                Guid programGuid = new Guid(programID);
                int programOrder = 0;
                ProgramModel editProgramModel = Resolve<IProgramService>().GetProgramByGUID(programGuid);
                translationJobModelModel.Order = programOrder.ToString("D4");

                #region Add TranslationJobContent
                Guid contentGuid = Guid.NewGuid();
                TranslationJobContent contentEntity = new TranslationJobContent();
                contentEntity.Note = string.Empty;
                contentEntity.TranslationJobContentGUID = contentGuid;
                contentEntity.TranslationJobContentName = editProgramModel.ProgramName;
                contentEntity.TranslationJobGUID = translationJobModelModel.TranslationJobGUID;

                Resolve<ITranslationJobContentRepository>().Insert(contentEntity);
                #endregion

                string fromObjectID = programNode.Attributes["FromID"] == null ? "" : programNode.Attributes["FromID"].Value;
                Program fromProgram = new Program();
                if (string.IsNullOrEmpty(fromObjectID))
                {
                    fromObjectID = GetFromObjectID("Program", programID, translationJobModelModel);
                    fromProgram = Resolve<IProgramRepository>().GetProgramByGuid(new Guid(fromObjectID));
                }

                string FromContentDes = (programNode.Attributes["FromDescription"] == null || programNode.Attributes["FromDescription"].Value == "") ? (fromProgram == null ? programNode.Attributes["Description"].Value : (fromProgram.Description == null ? programNode.Attributes["Description"].Value : fromProgram.Description)) : programNode.Attributes["FromDescription"].Value;
                //Add program description to TranslationJobElement table.
                TranslationJobElementModel elementDescriptionModel = new TranslationJobElementModel();
                elementDescriptionModel.MaxLength = 1000;
                elementDescriptionModel.Object = "Program";
                elementDescriptionModel.FromObjectGUID = fromObjectID;
                elementDescriptionModel.ToObjectGUID = programID;
                elementDescriptionModel.Position = "Description";
                elementDescriptionModel.StatusID = (int)ChangeTech.Models.TranslationJobStatusEnum.NotStart;
                elementDescriptionModel.TranslationJobContentGUID = contentGuid;
                elementDescriptionModel.Original = FromContentDes;
                elementDescriptionModel.Translated = programNode.Attributes["Description"].Value;
                elementDescriptionModel.Words = GetTranslationJobWordsCount(FromContentDes);
                elementDescriptionModel.Order = translationJobModelModel.Order + "-" + "0000" + "-" + "0000" + "-" + "0001";
                Guid elementDescriptionGuid = AddTranslationJobElement(elementDescriptionModel);

                string FromOrderProgramText = (programNode.Attributes["FromOrderProgramText"] == null || programNode.Attributes["FromOrderProgramText"].Value == "") ? (fromProgram == null ? programNode.Attributes["OrderProgramText"].Value : (fromProgram.OrderProgramText == null ? programNode.Attributes["OrderProgramText"].Value : fromProgram.OrderProgramText)) : programNode.Attributes["FromOrderProgramText"].Value;
                //Add order program text to TranslationJobElement table.
                TranslationJobElementModel elementOrderProgramTextModel = new TranslationJobElementModel();
                elementOrderProgramTextModel.MaxLength = 1000;
                elementOrderProgramTextModel.Object = "Program";
                elementOrderProgramTextModel.FromObjectGUID = fromObjectID;
                elementOrderProgramTextModel.ToObjectGUID = programID;
                elementOrderProgramTextModel.Position = "OrderProgramText";
                elementOrderProgramTextModel.StatusID = (int)ChangeTech.Models.TranslationJobStatusEnum.NotStart;
                elementOrderProgramTextModel.TranslationJobContentGUID = contentGuid;
                elementOrderProgramTextModel.Original = FromOrderProgramText;
                elementOrderProgramTextModel.Translated = programNode.Attributes["OrderProgramText"].Value;
                elementOrderProgramTextModel.Words = GetTranslationJobWordsCount(FromOrderProgramText);
                elementOrderProgramTextModel.Order = translationJobModelModel.Order + "-" + "0000" + "-" + "0000" + "-" + "0002";
                Guid elementOrderProgramTextGuid = AddTranslationJobElement(elementOrderProgramTextModel);

                XmlNodeList ctppNodes = xmlDoc.SelectNodes("//CTPP");
                //Add CTPP to TranslationJobElement table.
                CTPP ctppEntity = Resolve<ICTPPRepository>().GetCTPPByProgramGuid(fromProgram.ProgramGUID);
                if (ctppEntity != null)
                {
                    foreach (XmlNode ctppNode in ctppNodes)
                    {
                        //Ctpp ProgramDescription
                        string FromProgramDes = (ctppNode.Attributes["FromProgramDescription"] == null || ctppNode.Attributes["FromProgramDescription"].Value == "") ? (ctppEntity == null ? ctppNode.Attributes["ProgramDescription"].Value : (ctppEntity.ProgramDescription == null ? ctppNode.Attributes["ProgramDescription"].Value : ctppEntity.ProgramDescription)) : ctppNode.Attributes["FromProgramDescription"].Value;
                        TranslationJobElementModel elementProgramDesModel = new TranslationJobElementModel();
                        elementProgramDesModel.MaxLength = 1000;
                        elementProgramDesModel.Object = "CTPP";
                        elementProgramDesModel.FromObjectGUID = fromObjectID;
                        elementProgramDesModel.ToObjectGUID = programID;
                        elementProgramDesModel.Position = "ProgramDescription";
                        elementProgramDesModel.StatusID = (int)ChangeTech.Models.TranslationJobStatusEnum.NotStart;
                        elementProgramDesModel.TranslationJobContentGUID = contentGuid;
                        elementProgramDesModel.Original = FromProgramDes;
                        elementProgramDesModel.Translated = ctppNode.Attributes["ProgramDescription"].Value;
                        elementProgramDesModel.Words = GetTranslationJobWordsCount(FromProgramDes);
                        elementProgramDesModel.Order = translationJobModelModel.Order + "-" + "0000" + "-" + "0000" + "-" + "0003";
                        elementDescriptionGuid = AddTranslationJobElement(elementProgramDesModel);

                        //Ctpp ProgramDescriptionTitle
                        string FromProgramDesTitle = (ctppNode.Attributes["FromProgramDescriptionTitle"] == null || ctppNode.Attributes["FromProgramDescriptionTitle"].Value == "") ? (ctppEntity == null ? ctppNode.Attributes["ProgramDescriptionTitle"].Value : (ctppEntity.ProgramDescriptionTitle == null ? ctppNode.Attributes["ProgramDescriptionTitle"].Value : ctppEntity.ProgramDescriptionTitle)) : ctppNode.Attributes["FromProgramDescriptionTitle"].Value;
                        TranslationJobElementModel elementProgramDesTitleModel = new TranslationJobElementModel();
                        elementProgramDesTitleModel.MaxLength = 200;
                        elementProgramDesTitleModel.Object = "CTPP";
                        elementProgramDesTitleModel.FromObjectGUID = fromObjectID;
                        elementProgramDesTitleModel.ToObjectGUID = programID;
                        elementProgramDesTitleModel.Position = "ProgramDescriptionTitle";
                        elementProgramDesTitleModel.StatusID = (int)ChangeTech.Models.TranslationJobStatusEnum.NotStart;
                        elementProgramDesTitleModel.TranslationJobContentGUID = contentGuid;
                        elementProgramDesTitleModel.Original = FromProgramDesTitle;
                        elementProgramDesTitleModel.Translated = ctppNode.Attributes["ProgramDescriptionTitle"].Value;
                        elementProgramDesTitleModel.Words = GetTranslationJobWordsCount(FromProgramDesTitle);
                        elementProgramDesTitleModel.Order = translationJobModelModel.Order + "-" + "0000" + "-" + "0000" + "-" + "0004";
                        elementDescriptionGuid = AddTranslationJobElement(elementProgramDesTitleModel);

                        //Ctpp ProgramDescriptionForMobile
                        string FromProgramDesForMobile = (ctppNode.Attributes["FromProgramDescriptionForMobile"] == null || ctppNode.Attributes["FromProgramDescriptionForMobile"].Value == "") ? (ctppEntity == null ? ctppNode.Attributes["ProgramDescriptionForMobile"].Value : (ctppEntity.ProgramDescriptionForMobile == null ? ctppNode.Attributes["ProgramDescriptionForMobile"].Value : ctppEntity.ProgramDescriptionForMobile)) : ctppNode.Attributes["FromProgramDescriptionForMobile"].Value;
                        TranslationJobElementModel elementProgramDesForMobileModel = new TranslationJobElementModel();
                        elementProgramDesForMobileModel.MaxLength = 1000;
                        elementProgramDesForMobileModel.Object = "CTPP";
                        elementProgramDesForMobileModel.FromObjectGUID = fromObjectID;
                        elementProgramDesForMobileModel.ToObjectGUID = programID;
                        elementProgramDesForMobileModel.Position = "ProgramDescriptionForMobile";
                        elementProgramDesForMobileModel.StatusID = (int)ChangeTech.Models.TranslationJobStatusEnum.NotStart;
                        elementProgramDesForMobileModel.TranslationJobContentGUID = contentGuid;
                        elementProgramDesForMobileModel.Original = FromProgramDesForMobile;
                        elementProgramDesForMobileModel.Translated = ctppNode.Attributes["ProgramDescriptionForMobile"].Value;
                        elementProgramDesForMobileModel.Words = GetTranslationJobWordsCount(FromProgramDesForMobile);
                        elementProgramDesForMobileModel.Order = translationJobModelModel.Order + "-" + "0000" + "-" + "0000" + "-" + "0005";
                        elementDescriptionGuid = AddTranslationJobElement(elementProgramDesForMobileModel);

                        //Ctpp ProgramDescriptionTitleForMobile
                        string FromProgramDesTitleForMobile = (ctppNode.Attributes["FromProgramDescriptionTitleForMobile"] == null || ctppNode.Attributes["FromProgramDescriptionTitleForMobile"].Value == "") ? (ctppEntity == null ? ctppNode.Attributes["ProgramDescriptionTitleForMobile"].Value : (ctppEntity.ProgramDescriptionTitleForMobile == null ? ctppNode.Attributes["ProgramDescriptionTitleForMobile"].Value : ctppEntity.ProgramDescriptionTitleForMobile)) : ctppNode.Attributes["FromProgramDescriptionTitleForMobile"].Value;
                        TranslationJobElementModel elementProgramDesTitleForMobileModel = new TranslationJobElementModel();
                        elementProgramDesTitleForMobileModel.MaxLength = 200;
                        elementProgramDesTitleForMobileModel.Object = "CTPP";
                        elementProgramDesTitleForMobileModel.FromObjectGUID = fromObjectID;
                        elementProgramDesTitleForMobileModel.ToObjectGUID = programID;
                        elementProgramDesTitleForMobileModel.Position = "ProgramDescriptionTitleForMobile";
                        elementProgramDesTitleForMobileModel.StatusID = (int)ChangeTech.Models.TranslationJobStatusEnum.NotStart;
                        elementProgramDesTitleForMobileModel.TranslationJobContentGUID = contentGuid;
                        elementProgramDesTitleForMobileModel.Original = FromProgramDesTitleForMobile;
                        elementProgramDesTitleForMobileModel.Translated = ctppNode.Attributes["ProgramDescriptionTitleForMobile"].Value;
                        elementProgramDesTitleForMobileModel.Words = GetTranslationJobWordsCount(FromProgramDesTitleForMobile);
                        elementProgramDesTitleForMobileModel.Order = translationJobModelModel.Order + "-" + "0000" + "-" + "0000" + "-" + "0006";
                        elementDescriptionGuid = AddTranslationJobElement(elementProgramDesTitleForMobileModel);
                    }
                }
            }
        }

        private void ParaseSessionData(string translationDataStr, int startDay, TranslationJobModel translationJobModelModel)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(translationDataStr);
            XmlNodeList sessionNodes = xmlDoc.SelectNodes("//Sessions/Session");

            //The from day is not always 0
            int dayCount = startDay;
            foreach (XmlNode sessionModel in sessionNodes)
            {
                string sessionID = sessionModel.Attributes["ID"].Value;
                Guid sessionGuid = new Guid(sessionID);

                //Add to translationJobContent
                EditSessionModel editSessionModel = Resolve<ISessionService>().GetSessionBySessonGuid(sessionGuid);
                translationJobModelModel.Order = editSessionModel.Day.ToString("D4");

                #region Add TranslationJobContent
                Guid contentGuid = Guid.NewGuid();
                TranslationJobContent contentEntity = new TranslationJobContent();
                contentEntity.Note = string.Empty;
                contentEntity.TranslationJobContentGUID = contentGuid;
                contentEntity.TranslationJobContentName = editSessionModel.Name;
                contentEntity.TranslationJobGUID = translationJobModelModel.TranslationJobGUID;
                contentEntity.Day = editSessionModel.Day;

                Resolve<ITranslationJobContentRepository>().Insert(contentEntity);
                #endregion

                //string fromObjectID = sessionModel.Attributes["ObjectDefaultGUID"] == null ? "" : sessionModel.Attributes["ObjectDefaultGUID"].Value;
                string fromObjectID = sessionModel.Attributes["FromID"] == null ? "" : sessionModel.Attributes["FromID"].Value;
                Session fromSession = new Session();
                if (string.IsNullOrEmpty(fromObjectID))
                {
                    fromObjectID = GetFromObjectID("Session", sessionID, translationJobModelModel);
                    fromSession = Resolve<ISessionRepository>().GetSessionBySessionGuid(new Guid(fromObjectID));
                }
                //FromContentName: if has DefaultGUID, use the ["From..."] else ,use GetFromObjectID and from Entity. If all are null or empty,use the same with to content value.
                string FromContentName = (sessionModel.Attributes["FromName"] == null || sessionModel.Attributes["FromName"].Value == "") ? (fromSession == null ? sessionModel.Attributes["Name"].Value : (fromSession.Name == null ? sessionModel.Attributes["Name"].Value : fromSession.Name)) : sessionModel.Attributes["FromName"].Value;

                //Add session name to TranslationJobElement table.
                TranslationJobElementModel elementModel = new TranslationJobElementModel();
                elementModel.MaxLength = 500;
                elementModel.Object = "Session";
                elementModel.FromObjectGUID = fromObjectID;
                elementModel.ToObjectGUID = sessionID;
                elementModel.Position = "Name";
                elementModel.StatusID = (int)ChangeTech.Models.TranslationJobStatusEnum.NotStart;
                elementModel.TranslationJobContentGUID = contentGuid;
                elementModel.Original = FromContentName;
                elementModel.Translated = sessionModel.Attributes["Name"].Value;
                elementModel.Words = GetTranslationJobWordsCount(FromContentName);
                elementModel.Order = translationJobModelModel.Order + "-" + "0000" + "-" + "0000" + "-" + "0001";
                Guid elementGuid = AddTranslationJobElement(elementModel);


                string FromContentDes = (sessionModel.Attributes["FromDescription"] == null || sessionModel.Attributes["FromDescription"].Value == "") ? (fromSession == null ? sessionModel.Attributes["Description"].Value : (fromSession.Description == null ? sessionModel.Attributes["Description"].Value : fromSession.Description)) : sessionModel.Attributes["FromDescription"].Value;
                //Add session description to TranslationJobElement table.
                TranslationJobElementModel elementDescriptionModel = new TranslationJobElementModel();
                elementDescriptionModel.MaxLength = 1000;
                elementDescriptionModel.Object = "Session";
                elementDescriptionModel.FromObjectGUID = fromObjectID;
                elementDescriptionModel.ToObjectGUID = sessionID;
                elementDescriptionModel.Position = "Description";
                elementDescriptionModel.StatusID = (int)ChangeTech.Models.TranslationJobStatusEnum.NotStart;
                elementDescriptionModel.TranslationJobContentGUID = contentGuid;
                elementDescriptionModel.Original = FromContentDes;
                elementDescriptionModel.Translated = sessionModel.Attributes["Description"].Value;
                elementDescriptionModel.Words = GetTranslationJobWordsCount(FromContentDes);
                elementDescriptionModel.Order = translationJobModelModel.Order + "-" + "0000" + "-" + "0000" + "-" + "0002";
                Guid elementDescriptionGuid = AddTranslationJobElement(elementDescriptionModel);

                ParasePageSequenceData(translationDataStr, sessionID, contentGuid, false, translationJobModelModel);
            }
        }

        private void ParasePageSequenceData(string translationDataStr, string sessionID, Guid translationJobContentGuid, bool isRelapse, TranslationJobModel translationJobModelModel)//,  bool isRelapse)//=false
        {
            string order = translationJobModelModel.Order;//9999
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(translationDataStr);
            XmlNodeList pageSequenceNodes = null;
            if (isRelapse)
            {
                pageSequenceNodes = xmlDoc.SelectNodes(string.Format("//Relapses/Relapse[@DefaultGUID=\"{0}\"]", sessionID));
            }
            else
            {
                pageSequenceNodes = xmlDoc.SelectNodes(string.Format("//PageSequences/PageSequence[@DefaultGUID=\"{0}\"]", sessionID));
            }

            foreach (XmlNode pageSequenceModel in pageSequenceNodes)
            {
                if (isRelapse)
                {
                    order = (int.Parse(order) + 1).ToString("D4");
                    translationJobModelModel.Order = order;
                }
                else
                {
                    translationJobModelModel.Order = order;
                }
                
                string pageSeqOrder = pageSequenceModel.Attributes["Order"] == null ? "9999" : pageSequenceModel.Attributes["Order"].Value;
                translationJobModelModel.Order += "-" + (int.Parse(pageSeqOrder)).ToString("D4");

                string pageSequenceID = pageSequenceModel.Attributes["ID"].Value;
                Guid pageSequenceGuid = new Guid(pageSequenceModel.Attributes["ID"].Value);

                //string fromObjectID = pageSequenceModel.Attributes["ObjectDefaultGUID"] == null ? "" : pageSequenceModel.Attributes["ObjectDefaultGUID"].Value;
                string fromObjectID = pageSequenceModel.Attributes["FromID"] == null ? "" : pageSequenceModel.Attributes["FromID"].Value;
                PageSequence fromPageSequence = new PageSequence();
                if (string.IsNullOrEmpty(fromObjectID))
                {
                    if (isRelapse)//Relapse
                    {
                        fromObjectID = GetFromObjectID("Relapse", pageSequenceID, translationJobModelModel);//pageSequenceID = relapse id
                    }
                    else
                    {
                        fromObjectID = GetFromObjectID("PageSequence", pageSequenceID, translationJobModelModel);
                    }
                    fromPageSequence = Resolve<IPageSequenceRepository>().GetPageSequenceByGuid(new Guid(fromObjectID));
                }

                //add TranslationJobElement (The element's position is 'PageSequence' or 'Relapse' )
                if (isRelapse)//Relapse
                {
                    string fromContentName = (pageSequenceModel.Attributes["FromName"] == null || pageSequenceModel.Attributes["FromName"].Value == "") ? (fromPageSequence == null ? pageSequenceModel.Attributes["Name"].Value : (fromPageSequence.Name == null ? pageSequenceModel.Attributes["Name"].Value : fromPageSequence.Name)) : pageSequenceModel.Attributes["FromName"].Value;
                    TranslationJobElementModel elementModel = new TranslationJobElementModel();
                    elementModel.MaxLength = 500;
                    elementModel.Object = "Relapse";
                    elementModel.FromObjectGUID = fromObjectID;
                    elementModel.ToObjectGUID = pageSequenceID;
                    elementModel.Position = "Name";
                    elementModel.StatusID = (int)ChangeTech.Models.TranslationJobStatusEnum.NotStart;
                    elementModel.TranslationJobContentGUID = translationJobContentGuid;
                    elementModel.Original = fromContentName;
                    elementModel.Translated = pageSequenceModel.Attributes["Name"].Value;
                    elementModel.Words = GetTranslationJobWordsCount(fromContentName);
                    elementModel.Order = translationJobModelModel.Order + "-" + "0000" + "-" + "0001";
                    Guid elementGuid = AddTranslationJobElement(elementModel);
                    
                    string fromContentDes = (pageSequenceModel.Attributes["FromDescription"] == null || pageSequenceModel.Attributes["FromDescription"].Value == "") ? (fromPageSequence == null ? pageSequenceModel.Attributes["Description"].Value : (fromPageSequence.Description == null ? pageSequenceModel.Attributes["Description"].Value : fromPageSequence.Description)) : pageSequenceModel.Attributes["FromDescription"].Value;
                    TranslationJobElementModel elementDescModel = new TranslationJobElementModel();
                    elementDescModel.MaxLength = 250;
                    elementDescModel.Object = "Relapse";
                    elementDescModel.FromObjectGUID = fromObjectID;
                    elementDescModel.ToObjectGUID = pageSequenceID;
                    elementDescModel.Position = "Description";
                    elementDescModel.StatusID = (int)ChangeTech.Models.TranslationJobStatusEnum.NotStart;
                    elementDescModel.TranslationJobContentGUID = translationJobContentGuid;
                    elementDescModel.Original = fromContentDes;
                    elementDescModel.Translated = pageSequenceModel.Attributes["Description"].Value;
                    elementDescModel.Words = GetTranslationJobWordsCount(fromContentDes);
                    elementDescModel.Order = translationJobModelModel.Order + "-" + "0000" + "-" + "0002";
                    Guid elementDescGuid = AddTranslationJobElement(elementDescModel);
                }
                else
                {
                    string fromContentName = (pageSequenceModel.Attributes["FromName"] == null || pageSequenceModel.Attributes["FromName"].Value == "") ? (fromPageSequence == null ? pageSequenceModel.Attributes["Name"].Value : (fromPageSequence.Name == null ? pageSequenceModel.Attributes["Name"].Value : fromPageSequence.Name)) : pageSequenceModel.Attributes["FromName"].Value;
                    TranslationJobElementModel elementModel = new TranslationJobElementModel();
                    elementModel.MaxLength = 500;
                    elementModel.Object = "PageSequence";
                    elementModel.FromObjectGUID = fromObjectID;
                    elementModel.ToObjectGUID = pageSequenceID;
                    elementModel.Position = "Name";
                    elementModel.StatusID = (int)ChangeTech.Models.TranslationJobStatusEnum.NotStart;
                    elementModel.TranslationJobContentGUID = translationJobContentGuid;
                    elementModel.Original = fromContentName;
                    elementModel.Translated = pageSequenceModel.Attributes["Name"].Value;
                    elementModel.Words = GetTranslationJobWordsCount(fromContentName);
                    elementModel.Order = translationJobModelModel.Order + "-" + "0000" + "-" + "0001";
                    Guid elementGuid = AddTranslationJobElement(elementModel);
                    
                    string fromContentDes = (pageSequenceModel.Attributes["FromDescription"] == null || pageSequenceModel.Attributes["FromDescription"].Value == "") ? (fromPageSequence == null ? pageSequenceModel.Attributes["Description"].Value : (fromPageSequence.Description == null ? pageSequenceModel.Attributes["Description"].Value : fromPageSequence.Description)) : pageSequenceModel.Attributes["FromDescription"].Value;
                    TranslationJobElementModel elementDescModel = new TranslationJobElementModel();
                    elementDescModel.MaxLength = 250;
                    elementDescModel.Object = "PageSequence";
                    elementDescModel.FromObjectGUID = fromObjectID;
                    elementDescModel.ToObjectGUID = pageSequenceID;
                    elementDescModel.Position = "Description";
                    elementDescModel.StatusID = (int)ChangeTech.Models.TranslationJobStatusEnum.NotStart;
                    elementDescModel.TranslationJobContentGUID = translationJobContentGuid;
                    elementDescModel.Original = fromContentDes;
                    elementDescModel.Translated = pageSequenceModel.Attributes["Description"].Value;
                    elementDescModel.Words = GetTranslationJobWordsCount(fromContentDes);
                    elementDescModel.Order = translationJobModelModel.Order + "-" + "0000" + "-" + "0002";
                    Guid elementDescGuid = AddTranslationJobElement(elementDescModel);
                }
               
                string psOrder = translationJobModelModel.Order;
                XmlNodeList pageNodes = xmlDoc.SelectNodes(string.Format("//PageContents/PageContent[@DefaultGUID=\"{0}\"]", pageSequenceID));
                //List<TranslationModel> pageList = translationModelList.Where(s => s.Object.Equals("PageContent") && s.DefaultGUID.ToString().Equals(pageSequenceModel.ID) && s.Type.Equals("Heading")).ToList();
                foreach (XmlNode pageModel in pageNodes)
                {
                    translationJobModelModel.Order = psOrder;
                    translationJobModelModel.Order += "-" + (int.Parse(pageModel.Attributes["Order"].Value)).ToString("D4");

                    string pageID = pageModel.Attributes["ID"].Value;
                    Guid pageGuid = new Guid(pageID);

                    string fromObjectPageID = pageModel.Attributes["FromID"] == null ? "" : pageModel.Attributes["FromID"].Value;
                    PageContent fromPageContent = new PageContent();
                    if (string.IsNullOrEmpty(fromObjectPageID))
                    {
                        fromObjectPageID = GetFromObjectID("PageContent", pageID, translationJobModelModel);
                        fromPageContent = Resolve<IPageContentRepository>().GetPageContentByPageGuid(new Guid(fromObjectPageID));
                    }

                    //string fromPageContentHead = (pageModel.Attributes["FromHeading"] == null || pageModel.Attributes["FromHeading"].Value == "") ? (fromPageContent == null ? pageModel.Attributes["Heading"].Value : (fromPageContent.Heading == null ? pageModel.Attributes["Heading"].Value : fromPageContent.Heading)) : pageModel.Attributes["FromHeading"].Value;
                    string fromPageContentHead = (pageModel.Attributes["FromHeading"] == null || pageModel.Attributes["FromHeading"].Value == "") ? (fromPageContent == null ? ((pageModel.Attributes["Heading"] == null || pageModel.Attributes["Heading"].Value == "") ? "" : pageModel.Attributes["Heading"].Value) : (fromPageContent.Heading == null ? ((pageModel.Attributes["Heading"] == null || pageModel.Attributes["Heading"].Value == "") ? "" : pageModel.Attributes["Heading"].Value) : fromPageContent.Heading)) : pageModel.Attributes["FromHeading"].Value;
                    TranslationJobElementModel elementHeadModel = new TranslationJobElementModel();
                    elementHeadModel.MaxLength = null;
                    elementHeadModel.Object = "PageContent";
                    elementHeadModel.FromObjectGUID = fromObjectPageID;
                    elementHeadModel.ToObjectGUID = pageID;
                    elementHeadModel.Position = "Heading";
                    elementHeadModel.StatusID = (int)ChangeTech.Models.TranslationJobStatusEnum.NotStart;
                    elementHeadModel.TranslationJobContentGUID = translationJobContentGuid;
                    elementHeadModel.Original = fromPageContentHead;
                    elementHeadModel.Translated = pageModel.Attributes["Heading"] == null ? "" : pageModel.Attributes["Heading"].Value;
                    elementHeadModel.Words = GetTranslationJobWordsCount(fromPageContentHead);
                    elementHeadModel.Order = translationJobModelModel.Order + "-" + "0001";
                    AddTranslationJobElement(elementHeadModel);

                    //string fromPageContentBody = (pageModel.Attributes["FromBody"] == null || pageModel.Attributes["FromBody"].Value == "") ? (fromPageContent == null ? pageModel.Attributes["Body"].Value : (fromPageContent.Body == null ? pageModel.Attributes["Body"].Value : fromPageContent.Body)) : pageModel.Attributes["FromBody"].Value;
                    string fromPageContentBody = (pageModel.Attributes["FromBody"] == null || pageModel.Attributes["FromBody"].Value == "") ? (fromPageContent == null ? ((pageModel.Attributes["Body"] == null || pageModel.Attributes["Body"].Value == "") ? "" : pageModel.Attributes["Body"].Value) : (fromPageContent.Body == null ? ((pageModel.Attributes["Body"] == null || pageModel.Attributes["Body"].Value == "") ? "" : pageModel.Attributes["Body"].Value) : fromPageContent.Body)) : pageModel.Attributes["FromBody"].Value;
                    TranslationJobElementModel elementBodyModel = new TranslationJobElementModel();
                    elementBodyModel.MaxLength = null;
                    elementBodyModel.Object = "PageContent";
                    elementBodyModel.FromObjectGUID = fromObjectPageID;
                    elementBodyModel.ToObjectGUID = pageID;
                    elementBodyModel.Position = "Body";
                    elementBodyModel.StatusID = (int)ChangeTech.Models.TranslationJobStatusEnum.NotStart;
                    elementBodyModel.TranslationJobContentGUID = translationJobContentGuid;
                    elementBodyModel.Original = fromPageContentBody;
                    elementBodyModel.Translated = pageModel.Attributes["Body"] == null ? "" : pageModel.Attributes["Body"].Value;
                    elementBodyModel.Words = GetTranslationJobWordsCount(fromPageContentBody);
                    elementBodyModel.Order = translationJobModelModel.Order + "-" + "0002";
                    AddTranslationJobElement(elementBodyModel);

                    //string fromPageContentFooterText = (pageModel.Attributes["FromFooterText"] == null || pageModel.Attributes["FromFooterText"].Value == "") ? (fromPageContent == null ? pageModel.Attributes["FooterText"].Value : (fromPageContent.FooterText == null ? pageModel.Attributes["FooterText"].Value : fromPageContent.FooterText)) : pageModel.Attributes["FromFooterText"].Value;
                    string fromPageContentFooterText = (pageModel.Attributes["FromFooterText"] == null || pageModel.Attributes["FromFooterText"].Value == "") ? (fromPageContent == null ? ((pageModel.Attributes["FooterText"] == null || pageModel.Attributes["FooterText"].Value == "") ? "" : pageModel.Attributes["FooterText"].Value) : (fromPageContent.FooterText == null ? ((pageModel.Attributes["FooterText"] == null || pageModel.Attributes["FooterText"].Value == "") ? "" : pageModel.Attributes["FooterText"].Value) : fromPageContent.FooterText)) : pageModel.Attributes["FromFooterText"].Value;
                    TranslationJobElementModel elementFooterModel = new TranslationJobElementModel();
                    elementFooterModel.MaxLength = null;
                    elementFooterModel.Object = "PageContent";
                    elementFooterModel.FromObjectGUID = fromObjectPageID;
                    elementFooterModel.ToObjectGUID = pageID;
                    elementFooterModel.Position = "FooterText";
                    elementFooterModel.StatusID = (int)ChangeTech.Models.TranslationJobStatusEnum.NotStart;
                    elementFooterModel.TranslationJobContentGUID = translationJobContentGuid;
                    elementFooterModel.Original = fromPageContentFooterText;
                    elementFooterModel.Translated = pageModel.Attributes["FooterText"] == null ? "" : pageModel.Attributes["FooterText"].Value;
                    elementFooterModel.Words = GetTranslationJobWordsCount(fromPageContentFooterText);
                    elementFooterModel.Order = translationJobModelModel.Order + "-" + "0003";
                    AddTranslationJobElement(elementFooterModel);

                    //string fromPageContentPrimaryButtonCaption = (pageModel.Attributes["FromPrimaryButtonCaption"] == null || pageModel.Attributes["FromPrimaryButtonCaption"].Value == "") ? (fromPageContent == null ? pageModel.Attributes["PrimaryButtonCaption"].Value : (fromPageContent.PrimaryButtonCaption == null ? pageModel.Attributes["PrimaryButtonCaption"].Value : fromPageContent.PrimaryButtonCaption)) : pageModel.Attributes["FromPrimaryButtonCaption"].Value;
                    string fromPageContentPrimaryButtonCaption = (pageModel.Attributes["FromPrimaryButtonCaption"] == null || pageModel.Attributes["FromPrimaryButtonCaption"].Value == "") ? (fromPageContent == null ? ((pageModel.Attributes["PrimaryButtonCaption"] == null || pageModel.Attributes["PrimaryButtonCaption"].Value == "") ? "" : pageModel.Attributes["PrimaryButtonCaption"].Value) : (fromPageContent.PrimaryButtonCaption == null ? ((pageModel.Attributes["PrimaryButtonCaption"] == null || pageModel.Attributes["PrimaryButtonCaption"].Value == "") ? "" : pageModel.Attributes["PrimaryButtonCaption"].Value) : fromPageContent.PrimaryButtonCaption)) : pageModel.Attributes["FromPrimaryButtonCaption"].Value;
                    TranslationJobElementModel elementPrimaryModel = new TranslationJobElementModel();
                    elementPrimaryModel.MaxLength = 80;
                    elementPrimaryModel.Object = "PageContent";
                    elementPrimaryModel.FromObjectGUID = fromObjectPageID;
                    elementPrimaryModel.ToObjectGUID = pageID;
                    elementPrimaryModel.Position = "PrimaryButtonCaption";
                    elementPrimaryModel.StatusID = (int)ChangeTech.Models.TranslationJobStatusEnum.NotStart;
                    elementPrimaryModel.TranslationJobContentGUID = translationJobContentGuid;
                    elementPrimaryModel.Original = fromPageContentPrimaryButtonCaption;
                    elementPrimaryModel.Translated = pageModel.Attributes["PrimaryButtonCaption"] == null ? "" : pageModel.Attributes["PrimaryButtonCaption"].Value;
                    elementPrimaryModel.Words = GetTranslationJobWordsCount(fromPageContentPrimaryButtonCaption);
                    elementPrimaryModel.Order = translationJobModelModel.Order + "-" + "0004";
                    AddTranslationJobElement(elementPrimaryModel);

                    //string fromPageContentAfterShowExpression = (pageModel.Attributes["FromAfterShowExpression"] == null || pageModel.Attributes["FromAfterShowExpression"].Value == "") ? (fromPageContent == null ? pageModel.Attributes["AfterShowExpression"].Value : (fromPageContent.AfterShowExpression == null ? pageModel.Attributes["AfterShowExpression"].Value : fromPageContent.AfterShowExpression)) : pageModel.Attributes["FromAfterShowExpression"].Value;
                    string fromPageContentAfterShowExpression = (pageModel.Attributes["FromAfterShowExpression"] == null || pageModel.Attributes["FromAfterShowExpression"].Value == "") ? (fromPageContent == null ? ((pageModel.Attributes["AfterShowExpression"] == null || pageModel.Attributes["AfterShowExpression"].Value == "") ? "" : pageModel.Attributes["AfterShowExpression"].Value) : (fromPageContent.AfterShowExpression == null ? ((pageModel.Attributes["AfterShowExpression"] == null || pageModel.Attributes["AfterShowExpression"].Value == "") ? "" : pageModel.Attributes["AfterShowExpression"].Value) : fromPageContent.AfterShowExpression)) : pageModel.Attributes["FromAfterShowExpression"].Value;
                    TranslationJobElementModel elementAfterShowExpressionModel = new TranslationJobElementModel();
                    elementAfterShowExpressionModel.MaxLength = 1000;
                    elementAfterShowExpressionModel.Object = "PageContent";
                    elementAfterShowExpressionModel.FromObjectGUID = fromObjectPageID;
                    elementAfterShowExpressionModel.ToObjectGUID = pageID;
                    elementAfterShowExpressionModel.Position = "AfterShowExpression";
                    elementAfterShowExpressionModel.StatusID = (int)ChangeTech.Models.TranslationJobStatusEnum.NotStart;
                    elementAfterShowExpressionModel.TranslationJobContentGUID = translationJobContentGuid;
                    elementAfterShowExpressionModel.Original = fromPageContentAfterShowExpression;
                    elementAfterShowExpressionModel.Translated = pageModel.Attributes["AfterShowExpression"] == null ? "" : pageModel.Attributes["AfterShowExpression"].Value;
                    elementAfterShowExpressionModel.Words = GetTranslationJobWordsCount(fromPageContentAfterShowExpression);
                    elementAfterShowExpressionModel.Order = translationJobModelModel.Order + "-" + "0005";
                    AddTranslationJobElement(elementAfterShowExpressionModel);

                    //string fromPageContentBeforeShowExpression = (pageModel.Attributes["FromBeforeShowExpression"] == null || pageModel.Attributes["FromBeforeShowExpression"].Value == "") ? (fromPageContent == null ? pageModel.Attributes["BeforeShowExpression"].Value : (fromPageContent.BeforeShowExpression == null ? pageModel.Attributes["BeforeShowExpression"].Value : fromPageContent.BeforeShowExpression)) : pageModel.Attributes["FromBeforeShowExpression"].Value;
                    string fromPageContentBeforeShowExpression = (pageModel.Attributes["FromBeforeShowExpression"] == null || pageModel.Attributes["FromBeforeShowExpression"].Value == "") ? (fromPageContent == null ? ((pageModel.Attributes["BeforeShowExpression"] == null || pageModel.Attributes["BeforeShowExpression"].Value == "") ? "" : pageModel.Attributes["BeforeShowExpression"].Value) : (fromPageContent.BeforeShowExpression == null ? ((pageModel.Attributes["BeforeShowExpression"] == null || pageModel.Attributes["BeforeShowExpression"].Value == "") ? "" : pageModel.Attributes["BeforeShowExpression"].Value) : fromPageContent.BeforeShowExpression)) : pageModel.Attributes["FromBeforeShowExpression"].Value;
                    TranslationJobElementModel elementBeforeShowExpressionModel = new TranslationJobElementModel();
                    elementBeforeShowExpressionModel.MaxLength = 1000;
                    elementBeforeShowExpressionModel.Object = "PageContent";
                    elementBeforeShowExpressionModel.FromObjectGUID = fromObjectPageID;
                    elementBeforeShowExpressionModel.ToObjectGUID = pageID;
                    elementBeforeShowExpressionModel.Position = "BeforeShowExpression";
                    elementBeforeShowExpressionModel.StatusID = (int)ChangeTech.Models.TranslationJobStatusEnum.NotStart;
                    elementBeforeShowExpressionModel.TranslationJobContentGUID = translationJobContentGuid;
                    elementBeforeShowExpressionModel.Original = fromPageContentBeforeShowExpression;
                    elementBeforeShowExpressionModel.Translated = pageModel.Attributes["BeforeShowExpression"] == null ? "" : pageModel.Attributes["BeforeShowExpression"].Value;
                    elementBeforeShowExpressionModel.Words = GetTranslationJobWordsCount(fromPageContentBeforeShowExpression);
                    elementBeforeShowExpressionModel.Order = translationJobModelModel.Order + "-" + "0006";
                    AddTranslationJobElement(elementBeforeShowExpressionModel);

                    int elementOrder = 4;
                    XmlNodeList pageQuestionNodes = xmlDoc.SelectNodes(string.Format("//PageQuestionContents/PageQuestionContent[@DefaultGUID=\"{0}\"]", pageID));
                    //List<TranslationModel> questionList = translationModelList.Where(s => s.Object.Equals("PageQuestionContent") && s.DefaultGUID.ToString().Equals(pageModel.ID) && s.Type.Equals("Caption")).ToList();
                    foreach (XmlNode questionModel in pageQuestionNodes)
                    {
                        string PageQuestionContentID = questionModel.Attributes["ID"].Value;
                        Guid PageQuestionContentGuid = new Guid(questionModel.Attributes["ID"].Value);

                        //string fromObjectQuestionID = questionModel.Attributes["ObjectDefaultGUID"] == null ? "" : questionModel.Attributes["ObjectDefaultGUID"].Value;
                        string fromObjectQuestionID = questionModel.Attributes["FromID"] == null ? "" : questionModel.Attributes["FromID"].Value;
                        PageQuestionContent fromPageQuestionContent = new PageQuestionContent();
                        if (string.IsNullOrEmpty(fromObjectQuestionID))
                        {
                            fromObjectQuestionID = GetFromObjectID("PageQuestionContent", PageQuestionContentID, translationJobModelModel);
                            fromPageQuestionContent = Resolve<IPageQuestionContentRepository>().GetPageQuestionContentByPageQuestionGuid(new Guid(fromObjectQuestionID));
                        }


                        string fromPageQuestionContentCaption = (questionModel.Attributes["FromCaption"] == null || questionModel.Attributes["FromCaption"].Value == "") ? (fromPageQuestionContent == null ? questionModel.Attributes["Caption"].Value : (fromPageQuestionContent.Caption == null ? questionModel.Attributes["Caption"].Value : fromPageQuestionContent.Caption)) : questionModel.Attributes["FromCaption"].Value;
                        TranslationJobElementModel elementCaptionModel = new TranslationJobElementModel();
                        elementCaptionModel.MaxLength = 1024;
                        elementCaptionModel.Object = "PageQuestionContent";
                        elementCaptionModel.FromObjectGUID = fromObjectQuestionID;
                        elementCaptionModel.ToObjectGUID = PageQuestionContentID;
                        elementCaptionModel.Position = "Caption";
                        elementCaptionModel.StatusID = (int)ChangeTech.Models.TranslationJobStatusEnum.NotStart;
                        elementCaptionModel.TranslationJobContentGUID = translationJobContentGuid;
                        elementCaptionModel.Original = fromPageQuestionContentCaption;
                        elementCaptionModel.Translated = questionModel.Attributes["Caption"].Value;
                        elementCaptionModel.Words = GetTranslationJobWordsCount(fromPageQuestionContentCaption);
                        elementOrder += 1;
                        elementCaptionModel.Order = translationJobModelModel.Order + "-" + elementOrder.ToString("D4");
                        AddTranslationJobElement(elementCaptionModel);

                        string fromPageQuestionContentDisableCheckBox = (questionModel.Attributes["FromDisableCheckBox"] == null || questionModel.Attributes["FromDisableCheckBox"].Value == "") ? (fromPageQuestionContent == null ? questionModel.Attributes["DisableCheckBox"].Value : (fromPageQuestionContent.DisableCheckBox == null ? questionModel.Attributes["DisableCheckBox"].Value : fromPageQuestionContent.DisableCheckBox)) : questionModel.Attributes["FromDisableCheckBox"].Value;
                        TranslationJobElementModel elementDisableModel = new TranslationJobElementModel();
                        elementDisableModel.MaxLength = 250;
                        elementDisableModel.Object = "PageQuestionContent";
                        elementDisableModel.FromObjectGUID = fromObjectQuestionID;
                        elementDisableModel.ToObjectGUID = PageQuestionContentID;
                        elementDisableModel.Position = "DisableCheckBox";
                        elementDisableModel.StatusID = (int)ChangeTech.Models.TranslationJobStatusEnum.NotStart;
                        elementDisableModel.TranslationJobContentGUID = translationJobContentGuid;
                        elementDisableModel.Original = fromPageQuestionContentDisableCheckBox;
                        elementDisableModel.Translated = questionModel.Attributes["DisableCheckBox"].Value;
                        elementDisableModel.Words = GetTranslationJobWordsCount(fromPageQuestionContentDisableCheckBox);
                        elementOrder += 1;
                        elementDisableModel.Order = translationJobModelModel.Order + "-" + elementOrder.ToString("D4");


                        AddTranslationJobElement(elementDisableModel);

                        XmlNodeList questionItemNodes = xmlDoc.SelectNodes(string.Format("//PageQuestionItemContents/PageQuestionItemContent[@DefaultGUID=\"{0}\"]", PageQuestionContentID));
                        //List<TranslationModel> questionItemList = translationModelList.Where(s => s.Object.Equals("PageQuestionItemContent") && s.DefaultGUID.ToString().Equals(questionModel.ID)).ToList();
                        foreach (XmlNode questionItemModel in questionItemNodes)
                        {
                            string questionItemID = questionItemModel.Attributes["ID"].Value;
                            Guid questionItemGuid = new Guid(questionItemModel.Attributes["ID"].Value);

                            //string fromObjectPageQuestionItemID = questionItemModel.Attributes["ObjectDefaultGUID"] == null ? "" : questionItemModel.Attributes["ObjectDefaultGUID"].Value;
                            string fromObjectPageQuestionItemID = questionItemModel.Attributes["FromID"] == null ? "" : questionItemModel.Attributes["FromID"].Value;
                            PageQuestionItemContent fromPageQuestionItemContent = new PageQuestionItemContent();
                            if (string.IsNullOrEmpty(fromObjectPageQuestionItemID))
                            {
                                fromObjectPageQuestionItemID = GetFromObjectID("PageQuestionItemContent", questionItemID, translationJobModelModel);
                                fromPageQuestionItemContent = Resolve<IPageQuestionItemContentRepository>().GetPageQuestionItemContentByPageQuestionItemGuid(new Guid(fromObjectPageQuestionItemID));
                            }


                            string fromPageQuestionItemContentItem = (questionItemModel.Attributes["FromItem"] == null || questionItemModel.Attributes["FromItem"].Value == "") ? (fromPageQuestionItemContent == null ? questionItemModel.Attributes["Item"].Value : (fromPageQuestionItemContent.Item == null ? questionItemModel.Attributes["Item"].Value : fromPageQuestionItemContent.Item)) : questionItemModel.Attributes["FromItem"].Value;
                            TranslationJobElementModel elementItemModel = new TranslationJobElementModel();
                            elementItemModel.MaxLength = 1024;
                            elementItemModel.Object = "PageQuestionItemContent";
                            elementItemModel.FromObjectGUID = fromObjectPageQuestionItemID;
                            elementItemModel.ToObjectGUID = questionItemID;
                            elementItemModel.Position = "Item";
                            elementItemModel.StatusID = (int)ChangeTech.Models.TranslationJobStatusEnum.NotStart;
                            elementItemModel.TranslationJobContentGUID = translationJobContentGuid;
                            elementItemModel.Original = fromPageQuestionItemContentItem;
                            elementItemModel.Translated = questionItemModel.Attributes["Item"].Value;
                            elementItemModel.Words = GetTranslationJobWordsCount(fromPageQuestionItemContentItem);
                            elementOrder += 1;
                            elementItemModel.Order = translationJobModelModel.Order + "-" + elementOrder.ToString("D4");
                            AddTranslationJobElement(elementItemModel);

                            string fromPageQuestionItemContentFeedback = (questionItemModel.Attributes["FromFeedback"] == null || questionItemModel.Attributes["FromFeedback"].Value == "") ? (fromPageQuestionItemContent == null ? questionItemModel.Attributes["Feedback"].Value : (fromPageQuestionItemContent.Feedback == null ? questionItemModel.Attributes["Feedback"].Value : fromPageQuestionItemContent.Feedback)) : questionItemModel.Attributes["FromFeedback"].Value;
                            TranslationJobElementModel elementFeedModel = new TranslationJobElementModel();
                            elementFeedModel.MaxLength = 1024;
                            elementFeedModel.Object = "PageQuestionItemContent";
                            elementFeedModel.FromObjectGUID = fromObjectPageQuestionItemID;
                            elementFeedModel.ToObjectGUID = questionItemID;
                            elementFeedModel.Position = "Feedback";
                            elementFeedModel.StatusID = (int)ChangeTech.Models.TranslationJobStatusEnum.NotStart;
                            elementFeedModel.TranslationJobContentGUID = translationJobContentGuid;
                            elementFeedModel.Original = fromPageQuestionItemContentFeedback;
                            elementFeedModel.Translated = questionItemModel.Attributes["Feedback"].Value;
                            elementFeedModel.Words = GetTranslationJobWordsCount(fromPageQuestionItemContentFeedback);
                            elementOrder += 1;
                            elementFeedModel.Order = translationJobModelModel.Order + "-" + elementOrder.ToString("D4");

                            AddTranslationJobElement(elementFeedModel);

                        }
                    }

                    XmlNodeList graphNodes = xmlDoc.SelectNodes(string.Format("//GraphContents/GraphContent[@DefaultGUID=\"{0}\"]", pageID));
                    //List<TranslationModel> graphList = translationModelList.Where(s => s.Object.Equals("GraphContent") && s.DefaultGUID.ToString().Equals(pageModel.ID)).ToList();
                    foreach (XmlNode graphModel in graphNodes)
                    {
                        string GraphContentID = graphModel.Attributes["ID"].Value;
                        Guid GraphContentGuid = new Guid(graphModel.Attributes["ID"].Value);

                        string fromObjectGraphID = graphModel.Attributes["FromID"] == null ? "" : graphModel.Attributes["FromID"].Value;
                        GraphContent fromGraphContent = new GraphContent();
                        if (string.IsNullOrEmpty(fromObjectGraphID))
                        {
                            fromObjectGraphID = GetFromObjectID("GraphContent", GraphContentID, translationJobModelModel);
                            fromGraphContent = Resolve<IGraphContentRepository>().Get(new Guid(fromObjectGraphID));
                        }

                        string fromGraphContentCaption = (graphModel.Attributes["FromCaption"] == null || graphModel.Attributes["FromCaption"].Value == "") ? (fromGraphContent == null ? graphModel.Attributes["Caption"].Value : (fromGraphContent.Caption == null ? graphModel.Attributes["Caption"].Value : fromGraphContent.Caption)) : graphModel.Attributes["FromCaption"].Value;
                        TranslationJobElementModel elementCaptionModel = new TranslationJobElementModel();
                        elementCaptionModel.MaxLength = 200;
                        elementCaptionModel.Object = "GraphContent";
                        elementCaptionModel.FromObjectGUID = fromObjectGraphID;
                        elementCaptionModel.ToObjectGUID = GraphContentID;
                        elementCaptionModel.Position = "Caption";
                        elementCaptionModel.StatusID = (int)ChangeTech.Models.TranslationJobStatusEnum.NotStart;
                        elementCaptionModel.TranslationJobContentGUID = translationJobContentGuid;
                        elementCaptionModel.Original = fromGraphContentCaption;
                        elementCaptionModel.Translated = graphModel.Attributes["Caption"].Value;
                        elementCaptionModel.Words = GetTranslationJobWordsCount(fromGraphContentCaption);
                        elementOrder += 1;
                        elementCaptionModel.Order = translationJobModelModel.Order + "-" + elementOrder.ToString("D4");

                        AddTranslationJobElement(elementCaptionModel);


                        //List<TranslationModel> graphItemList = translationModelList.Where(s => s.Object.Equals("GraphItemContent") && s.DefaultGUID.ToString().Equals(graphModel.ID)).ToList();
                        XmlNodeList graphItemNodes = xmlDoc.SelectNodes(string.Format("//GraphItemContents/GraphItemContent[@DefaultGUID=\"{0}\"]", pageID));
                        foreach (XmlNode graphItemModel in graphItemNodes)
                        {
                            string GraphItemContentID = graphItemModel.Attributes["ID"].Value;
                            Guid GraphItemContentGuid = new Guid(graphItemModel.Attributes["ID"].Value);

                            string fromObjectGraphItemID = graphItemModel.Attributes["FromID"] == null ? "" : graphItemModel.Attributes["FromID"].Value;
                            GraphItemContent fromGraphItemContent = new GraphItemContent();
                            if (string.IsNullOrEmpty(fromObjectGraphItemID))
                            {
                                fromObjectGraphItemID = GetFromObjectID("GraphItemContent", GraphItemContentID, translationJobModelModel);
                                fromGraphItemContent = Resolve<IGraphItemContentRepository>().Get(new Guid(fromObjectGraphItemID));
                            }

                            string fromGraphItemContentName = (graphItemModel.Attributes["FromName"] == null || graphItemModel.Attributes["FromName"].Value == "") ? (fromGraphItemContent == null ? graphItemModel.Attributes["Name"].Value : (fromGraphItemContent.Name == null ? graphItemModel.Attributes["Name"].Value : fromGraphItemContent.Name)) : graphItemModel.Attributes["FromName"].Value;
                            TranslationJobElementModel elementGraItemModel = new TranslationJobElementModel();
                            elementGraItemModel.MaxLength = 200;
                            elementGraItemModel.Object = "GraphItemContent";
                            elementGraItemModel.FromObjectGUID = fromObjectGraphItemID;
                            elementGraItemModel.ToObjectGUID = GraphItemContentID;
                            elementGraItemModel.Position = "Name";
                            elementGraItemModel.StatusID = (int)ChangeTech.Models.TranslationJobStatusEnum.NotStart;
                            elementGraItemModel.TranslationJobContentGUID = translationJobContentGuid;
                            elementGraItemModel.Original = fromGraphItemContentName;
                            elementGraItemModel.Translated = graphItemModel.Attributes["Name"].Value;
                            elementGraItemModel.Words = GetTranslationJobWordsCount(fromGraphItemContentName);
                            elementOrder += 1;
                            elementGraItemModel.Order = translationJobModelModel.Order + "-" + elementOrder.ToString("D4");

                            AddTranslationJobElement(elementGraItemModel);
                        }
                    }

                    XmlNodeList preferenceNodes = xmlDoc.SelectNodes(string.Format("//Preferences/Preference[@DefaultGUID=\"{0}\"]", pageID));
                    //List<TranslationModel> preferenceList = translationModelList.Where(s => s.Object.Equals("Preferences") && s.DefaultGUID.ToString().Equals(pageModel.ID)).ToList();
                    foreach (XmlNode preferenceModel in preferenceNodes)
                    {
                        string preferenceID = preferenceModel.Attributes["ID"].Value;
                        Guid preferenceGuid = new Guid(preferenceModel.Attributes["ID"].Value);

                        string fromObjectPreferID = preferenceModel.Attributes["FromID"] == null ? "" : preferenceModel.Attributes["FromID"].Value;
                        Preferences fromPreferences = new Preferences();
                        if (string.IsNullOrEmpty(fromObjectPreferID))
                        {
                            fromObjectPreferID = GetFromObjectID("Preferences", preferenceID, translationJobModelModel);
                            fromPreferences = Resolve<IPreferencesRepository>().GetPreference(new Guid(fromObjectPreferID));
                        }

                        string fromPreferencesName = (preferenceModel.Attributes["FromName"] == null || preferenceModel.Attributes["FromName"].Value == "") ? (fromPreferences == null ? preferenceModel.Attributes["Name"].Value : (fromPreferences.Name == null ? preferenceModel.Attributes["Name"].Value : fromPreferences.Name)) : preferenceModel.Attributes["FromName"].Value;
                        TranslationJobElementModel elementPreNameModel = new TranslationJobElementModel();
                        elementPreNameModel.MaxLength = 50;
                        elementPreNameModel.Object = "Preferences";
                        elementPreNameModel.FromObjectGUID = fromObjectPreferID;
                        elementPreNameModel.ToObjectGUID = preferenceID;
                        elementPreNameModel.Position = "Name";
                        elementPreNameModel.StatusID = (int)ChangeTech.Models.TranslationJobStatusEnum.NotStart;
                        elementPreNameModel.TranslationJobContentGUID = translationJobContentGuid;
                        elementPreNameModel.Original = fromPreferencesName;
                        elementPreNameModel.Translated = preferenceModel.Attributes["Name"].Value;
                        elementPreNameModel.Words = GetTranslationJobWordsCount(fromPreferencesName);
                        elementOrder += 1;
                        elementPreNameModel.Order = translationJobModelModel.Order + "-" + elementOrder.ToString("D4");
                        AddTranslationJobElement(elementPreNameModel);

                        string fromPreferencesDescription = (preferenceModel.Attributes["FromDescription"] == null || preferenceModel.Attributes["FromDescription"].Value == "") ? (fromPreferences == null ? preferenceModel.Attributes["Description"].Value : (fromPreferences.Description == null ? preferenceModel.Attributes["Description"].Value : fromPreferences.Description)) : preferenceModel.Attributes["FromDescription"].Value;
                        TranslationJobElementModel elementPreDesModel = new TranslationJobElementModel();
                        elementPreDesModel.MaxLength = 200;
                        elementPreDesModel.Object = "Preferences";
                        elementPreDesModel.FromObjectGUID = fromObjectPreferID;
                        elementPreDesModel.ToObjectGUID = preferenceID;
                        elementPreDesModel.Position = "Description";
                        elementPreDesModel.StatusID = (int)ChangeTech.Models.TranslationJobStatusEnum.NotStart;
                        elementPreDesModel.TranslationJobContentGUID = translationJobContentGuid;
                        elementPreDesModel.Original = fromPreferencesDescription;
                        elementPreDesModel.Translated = preferenceModel.Attributes["Description"].Value;
                        elementPreDesModel.Words = GetTranslationJobWordsCount(fromPreferencesDescription);
                        elementOrder += 1;
                        elementPreDesModel.Order = translationJobModelModel.Order + "-" + elementOrder.ToString("D4");
                        AddTranslationJobElement(elementPreDesModel);

                        string fromPreferencesAnswerText = (preferenceModel.Attributes["FromAnswerText"] == null || preferenceModel.Attributes["FromAnswerText"].Value == "") ? (fromPreferences == null ? preferenceModel.Attributes["AnswerText"].Value : (fromPreferences.AnswerText == null ? preferenceModel.Attributes["AnswerText"].Value : fromPreferences.AnswerText)) : preferenceModel.Attributes["FromAnswerText"].Value;
                        TranslationJobElementModel elementPreAnsModel = new TranslationJobElementModel();
                        elementPreAnsModel.MaxLength = 200;
                        elementPreAnsModel.Object = "Preferences";
                        elementPreAnsModel.FromObjectGUID = fromObjectPreferID;
                        elementPreAnsModel.ToObjectGUID = preferenceID;
                        elementPreAnsModel.Position = "AnswerText";
                        elementPreAnsModel.StatusID = (int)ChangeTech.Models.TranslationJobStatusEnum.NotStart;
                        elementPreAnsModel.TranslationJobContentGUID = translationJobContentGuid;
                        elementPreAnsModel.Original = fromPreferencesAnswerText;
                        elementPreAnsModel.Translated = preferenceModel.Attributes["AnswerText"].Value;
                        elementPreAnsModel.Words = GetTranslationJobWordsCount(fromPreferencesAnswerText);
                        elementOrder += 1;
                        elementPreAnsModel.Order = translationJobModelModel.Order + "-" + elementOrder.ToString("D4");
                        AddTranslationJobElement(elementPreAnsModel);

                        string fromPreferencesButtonName = (preferenceModel.Attributes["FromButtonName"] == null || preferenceModel.Attributes["FromButtonName"].Value == "") ? (fromPreferences == null ? preferenceModel.Attributes["ButtonName"].Value : (fromPreferences.ButtonName == null ? preferenceModel.Attributes["ButtonName"].Value : fromPreferences.ButtonName)) : preferenceModel.Attributes["FromButtonName"].Value;
                        TranslationJobElementModel elementPreButModel = new TranslationJobElementModel();
                        elementPreButModel.MaxLength = 200;
                        elementPreButModel.Object = "Preferences";
                        elementPreButModel.FromObjectGUID = fromObjectPreferID;
                        elementPreButModel.ToObjectGUID = preferenceID;
                        elementPreButModel.Position = "ButtonName";
                        elementPreButModel.StatusID = (int)ChangeTech.Models.TranslationJobStatusEnum.NotStart;
                        elementPreButModel.TranslationJobContentGUID = translationJobContentGuid;
                        elementPreButModel.Original = fromPreferencesButtonName;
                        elementPreButModel.Translated = preferenceModel.Attributes["ButtonName"].Value;
                        elementPreButModel.Words = GetTranslationJobWordsCount(fromPreferencesButtonName);
                        elementOrder += 1;
                        elementPreButModel.Order = translationJobModelModel.Order + "-" + elementOrder.ToString("D4");

                        AddTranslationJobElement(elementPreButModel);
                    }
                    //Add ResultLine TranslationJobElement.
                    XmlNodeList pageLineNodes = xmlDoc.SelectNodes(string.Format("//ResultLines/ResultLine[@DefaultGUID=\"{0}\"]", pageID));
                    foreach (XmlNode pageLineModel in pageLineNodes)
                    {
                        string pageLineID = pageLineModel.Attributes["ID"].Value;
                        Guid pageLineGuid = new Guid(pageLineModel.Attributes["ID"].Value);

                        string fromObjectPageLineID = pageLineModel.Attributes["FromID"] == null ? "" : pageLineModel.Attributes["FromID"].Value;
                        ScreenResultTemplatePageLine fromPageLine = new ScreenResultTemplatePageLine();
                        if (string.IsNullOrEmpty(fromObjectPageLineID))
                        {
                            fromObjectPageLineID = GetFromObjectID("ScreenResultTemplatePageLine", pageLineID, translationJobModelModel);
                            fromPageLine = Resolve<IScreenResultTemplatePageLineRepository>().GetPageLine(new Guid(fromObjectPageLineID));
                        }

                        string fromPageLineText = (pageLineModel.Attributes["FromText"] == null || pageLineModel.Attributes["FromText"].Value == "") ? (fromPageLine == null ? pageLineModel.Attributes["Text"].Value : (fromPageLine.Text == null ? pageLineModel.Attributes["Text"].Value : fromPageLine.Text)) : pageLineModel.Attributes["FromText"].Value;
                        TranslationJobElementModel elementPreTextModel = new TranslationJobElementModel();
                        elementPreTextModel.MaxLength = 1000;
                        elementPreTextModel.Object = "ScreenResultTemplatePageLine";
                        elementPreTextModel.FromObjectGUID = fromObjectPageLineID;
                        elementPreTextModel.ToObjectGUID = pageLineID;
                        elementPreTextModel.Position = "Text";
                        elementPreTextModel.StatusID = (int)ChangeTech.Models.TranslationJobStatusEnum.NotStart;
                        elementPreTextModel.TranslationJobContentGUID = translationJobContentGuid;
                        elementPreTextModel.Original = fromPageLineText;
                        elementPreTextModel.Translated = pageLineModel.Attributes["Text"].Value;
                        elementPreTextModel.Words = GetTranslationJobWordsCount(fromPageLineText);
                        elementOrder += 1;
                        elementPreTextModel.Order = translationJobModelModel.Order + "-" + elementOrder.ToString("D4");

                        AddTranslationJobElement(elementPreTextModel);

                        //PageLine URL
                        //string fromPageLineURL = (pageLineModel.Attributes["FromURL"] == null || pageLineModel.Attributes["FromURL"].Value == "") ? (fromPageLine == null ? pageLineModel.Attributes["URL"].Value : (fromPageLine.URL == null ? pageLineModel.Attributes["URL"].Value : fromPageLine.URL)) : pageLineModel.Attributes["FromURL"].Value;
                        //TranslationJobElementModel elementPreURLModel = new TranslationJobElementModel();
                        //elementPreURLModel.MaxLength = 1000;
                        //elementPreURLModel.Object = "ScreenResultTemplatePageLine";
                        //elementPreURLModel.FromObjectGUID = fromObjectPageLineID;
                        //elementPreURLModel.ToObjectGUID = pageLineID;
                        //elementPreURLModel.Position = "URL";
                        //elementPreURLModel.StatusID = (int)TranslationJobStatusEnum.NotStart;
                        //elementPreURLModel.TranslationJobContentGUID = translationJobContentGuid;
                        //elementPreURLModel.Original = fromPageLineURL;
                        //elementPreURLModel.Translated = pageLineModel.Attributes["URL"].Value;
                        //elementPreURLModel.Words = GetTranslationJobWordsCount(fromPageLineURL);
                        //elementOrder += 1;
                        //elementPreURLModel.Order = translationJobModelModel.Order + "-" + elementOrder.ToString("D4");
                        //AddTranslationJobElement(elementPreURLModel);

                    }
                }
            }
        }

        #region private mothod GetFromObjectID
        private string GetFromObjectID(string table, string toID, TranslationJobModel model)
        {
            string fromObjectID = new Guid().ToString();
            ProgramModel programModel = Resolve<IProgramService>().GetProgramByProgramGUIDAndLanguageGUID(model.Program.ProgramGuid, model.FromLanguage.LanguageGUID);
            switch (table)
            {
                case "Program":
                    Guid programGuid = new Guid(toID);
                    fromObjectID = programModel.Guid.ToString();//GetFromProgramGuid(programModel, programGuid).ToString();
                    break;
                case "PageContent":
                    Guid pageGuid = new Guid(toID);
                    fromObjectID = GetFromPageGuid(programModel, pageGuid).ToString();
                    break;
                case "PageQuestionContent":
                    Guid pageQuestionGuid = new Guid(toID);
                    fromObjectID = GetFromPageQuestionGuid(programModel, pageQuestionGuid).ToString();
                    break;
                case "PageQuestionItemContent":
                    Guid pageQuestionItemGuid = new Guid(toID);
                    fromObjectID = GetFromPageQuestionItemGuid(programModel, pageQuestionItemGuid).ToString();
                    break;
                case "ProgramRoom":
                    Guid programRoomGuid = new Guid(toID);
                    fromObjectID = GetFromProgramRoomGuid(programModel, programRoomGuid).ToString();
                    break;
                case "GraphContent":
                    Guid graphContentGuid = new Guid(toID);
                    fromObjectID = GetFromGraphContentGuid(programModel, graphContentGuid).ToString();
                    break;
                case "EmailTemplate":
                    Guid emailTemplateGuid = new Guid(toID);
                    fromObjectID = GetFromEmailTemplateGuid(programModel, emailTemplateGuid).ToString();
                    break;
                case "GraphItemContent":
                    Guid graphItemContentGuid = new Guid(toID);
                    fromObjectID = GetFromGraphItemContentGuid(programModel, graphItemContentGuid).ToString();
                    break;
                case "HelpItem":
                    Guid helpItemGuid = new Guid(toID);
                    fromObjectID = GetFromHelpItemGuid(programModel, helpItemGuid).ToString();
                    break;
                case "Preferences":
                    Guid preferGuid = new Guid(toID);
                    fromObjectID = GetFromPreferencesGuid(programModel, preferGuid).ToString();
                    break;
                case "Session":
                case "ProgramDailySMS":
                    Guid sessionGuid = new Guid(toID);
                    Guid fromObjectGUID = GetFromSessionGuid(programModel, sessionGuid);
                    fromObjectID = fromObjectGUID.ToString();
                    break;
                case "SpecialString":
                    fromObjectID = toID;// Resolve<ISpecialStringRepository>().GetSpecialString(programModel.DefaultLanguage, toID).Name;
                    break;
                case "TipMessage":
                    Guid tipMessageGuid = new Guid(toID);
                    fromObjectID = GetFromTipMessageGuid(programModel, tipMessageGuid).ToString();
                    break;
                case "UserMenu":
                    Guid userMenuGuid = new Guid(toID);
                    fromObjectID = GetFromUserMenuGuid(programModel, userMenuGuid).ToString();
                    break;
                case "AccessoryTemplate":
                    Guid accTemGuid = new Guid(toID);
                    fromObjectID = GetFromAccessoryTemplateGuid(programModel, accTemGuid).ToString();
                    break;
                case "Relapse":
                case "PageSequence":
                    Guid pageSequenceGuid = new Guid(toID);
                    fromObjectID = GetFromSeqGuid(programModel, pageSequenceGuid).ToString();
                    break;
                case "ScreenResultTemplatePageLine":
                    Guid pageLineGUID = new Guid(toID);
                    fromObjectID = GetFromPageLineGuid(programModel, pageLineGUID).ToString();
                    break;
            }
            return fromObjectID;
        }

        private Guid GetFromProgramGuid(ProgramModel programModel, Guid toProgramGuid)
        {
            Guid fromProgramGuid = Guid.Empty;
            ProgramModel toProgramModel = Resolve<IProgramService>().GetProgramByGUID(toProgramGuid);
            if (toProgramModel != null)
            {
                fromProgramGuid = toProgramModel.Guid;
            }
            return fromProgramGuid;
        }

        private Guid GetFromSessionGuid(ProgramModel programModel, Guid toSessionGuid)
        {
            Guid fromSessionGuid = Guid.Empty;
            EditSessionModel toSessionModel = Resolve<ISessionService>().GetSessionBySessonGuid(toSessionGuid);
            if (toSessionModel != null)
            {
                fromSessionGuid = Resolve<ISessionService>().GetSessionGuidByProgarmAndDay(programModel.Guid, toSessionModel.Day);
            }
            return fromSessionGuid;
        }
        private Guid GetFromSeqGuid(ProgramModel programModel, Guid toSequenceGuid)
        {
            Guid fromSeqGuid = Guid.Empty;

            SessionContent toSessionContent = Resolve<ISessionContentRepository>().GetSessionContentByPageSeqGuid(toSequenceGuid).FirstOrDefault();
            if (toSessionContent != null)// page sequence in session
            {
                if (!toSessionContent.SessionReference.IsLoaded)
                {
                    toSessionContent.SessionReference.Load();
                }
                Guid fromSessionGuid = GetFromSessionGuid(programModel, toSessionContent.Session.SessionGUID);
                SessionContent fromSessionContent = Resolve<ISessionContentRepository>().GetSessionContentBySessionGuidAndOrderNO(fromSessionGuid, toSessionContent.PageSequenceOrderNo);
                if (fromSessionContent != null)
                {
                    if (!fromSessionContent.PageSequenceReference.IsLoaded) fromSessionContent.PageSequenceReference.Load();
                    fromSeqGuid = fromSessionContent.PageSequence.PageSequenceGUID;
                }
            }
            else// page sequence in Relapse.
            {
                //can not map
            }
            return fromSeqGuid;
        }
        private Guid GetFromPageGuid(ProgramModel programModel, Guid toPageGuid)
        {
            Guid fromPageGuid = Guid.Empty;
            PageContent toPageContent = Resolve<IPageContentRepository>().GetPageContentByPageGuid(toPageGuid);
            if (toPageContent != null)
            {
                if (!toPageContent.PageReference.IsLoaded) toPageContent.PageReference.Load();
                if (!toPageContent.Page.PageSequenceReference.IsLoaded) toPageContent.Page.PageSequenceReference.Load();
                Guid toPageSeqGuid = toPageContent.Page.PageSequence.PageSequenceGUID;

                Guid fromPageSeqGuid = GetFromSeqGuid(programModel, toPageSeqGuid);
                Page fromPage = Resolve<IPageRepository>().GetPageByPageSequenceAndOrderNo(fromPageSeqGuid, toPageContent.Page.PageOrderNo);
                fromPageGuid = fromPage == null ? Guid.Empty : fromPage.PageGUID;
            }
            return fromPageGuid;
        }

        private Guid GetFromPageLineGuid(ProgramModel programModel, Guid toPageGuid)
        {
            Guid fromPageLineGuid = Guid.Empty;
            ScreenResultTemplatePageLine PageLine = Resolve<IScreenResultTemplatePageLineRepository>().GetPageLine(toPageGuid);
            if (PageLine != null)
            {
                if (!PageLine.PageReference.IsLoaded) PageLine.PageReference.Load();
                Guid fromPageGuid = GetFromPageGuid(programModel, PageLine.Page.PageGUID);
                ScreenResultTemplatePageLine fromPageLine = Resolve<IScreenResultTemplatePageLineRepository>().GetPageLineByPageGuidAndPageLineOrder(fromPageGuid, PageLine.Order.Value);
                fromPageLineGuid = fromPageLine == null ? Guid.Empty : fromPageLine.PageLineGUID;
            }
            return fromPageLineGuid;
        }

        private Guid GetFromPageQuestionGuid(ProgramModel programModel, Guid toPageQuestionGuid)
        {
            Guid fromPageQuestionGuid = Guid.Empty;
            PageQuestionContent pageQueCon = Resolve<IPageQuestionContentRepository>().GetPageQuestionContentByPageQuestionGuid(toPageQuestionGuid);
            if (pageQueCon != null)
            {
                if (!pageQueCon.PageQuestionReference.IsLoaded) pageQueCon.PageQuestionReference.Load();
                if (!pageQueCon.PageQuestion.PageReference.IsLoaded) pageQueCon.PageQuestion.PageReference.Load();
                Guid fromPageGuid = GetFromPageGuid(programModel, pageQueCon.PageQuestion.Page.PageGUID);
                PageQuestion fromPageQue = Resolve<IPageQuestionRepository>().GetPageQuestionByPageGuidAndQuesOrder(fromPageGuid, pageQueCon.PageQuestion.Order);
                fromPageQuestionGuid = fromPageQue == null ? Guid.Empty : fromPageQue.PageQuestionGUID;
            }

            return fromPageQuestionGuid;
        }
        private Guid GetFromPageQuestionItemGuid(ProgramModel programModel, Guid toPageQuestionItemGuid)
        {
            Guid fromPageQuestionItemGuid = Guid.Empty;
            PageQuestionItem toPageQueItem = Resolve<IPageQuestionItemRepository>().Get(toPageQuestionItemGuid);
            if (toPageQueItem != null)
            {
                if (!toPageQueItem.PageQuestionReference.IsLoaded) toPageQueItem.PageQuestionReference.Load();
                Guid fromPageQuestionGuid = GetFromPageQuestionGuid(programModel, toPageQueItem.PageQuestion.PageQuestionGUID);
                PageQuestionItem fromPageQueItem = Resolve<IPageQuestionItemRepository>().GetByPageQuestionGuidAndOrder(fromPageQuestionGuid, (int)toPageQueItem.Order);
                fromPageQuestionItemGuid = fromPageQueItem == null ? Guid.Empty : fromPageQueItem.PageQuestionItemGUID;
            }

            return fromPageQuestionItemGuid;
        }
        private Guid GetFromGraphContentGuid(ProgramModel programModel, Guid toGraphContentGuid)
        {
            Guid fromGraphGuid = Guid.Empty;
            Graph toGraph = Resolve<IGraphRepository>().Get(toGraphContentGuid);
            if (toGraph != null)
            {
                if (!toGraph.PageReference.IsLoaded) toGraph.PageReference.Load();
                Guid fromPageGuid = GetFromPageGuid(programModel, toGraph.Page.PageGUID);
                Graph fromGraph = Resolve<IGraphRepository>().GetGraphByPageGuid(fromPageGuid);
                fromGraphGuid = fromGraph == null ? Guid.Empty : fromGraph.GraphGUID;
                if ((fromGraphGuid == null || fromGraphGuid == Guid.Empty) && toGraph.ParentGraphGUID != null)
                {
                    if (string.IsNullOrEmpty(toGraph.ParentGraphGUID.Value.ToString()))
                    {
                        Graph fromGraphEntity = Resolve<IGraphRepository>().Get(toGraph.ParentGraphGUID.Value);
                        fromGraphGuid = fromGraphEntity != null ? fromGraphEntity.GraphGUID : Guid.Empty;
                    }
                }
            }
            return fromGraphGuid;
        }
        private Guid GetFromGraphItemContentGuid(ProgramModel programModel, Guid toGraphItemGuid)
        {
            Guid fromGraphItemGuid = Guid.Empty;
            GraphItem toGraphItem = Resolve<IGraphItemRepository>().get(toGraphItemGuid);
            if (toGraphItem != null)
            {
                if (!toGraphItem.GraphReference.IsLoaded) toGraphItem.GraphReference.Load();
                if (!toGraphItem.GraphItemContentReference.IsLoaded) toGraphItem.GraphItemContentReference.Load();
                Guid toGrpahGuid = toGraphItem.Graph.GraphGUID;
                Guid fromGraphGuid = GetFromGraphContentGuid(programModel, toGrpahGuid);
                List<GraphItem> fromGraphItems = Resolve<IGraphItemRepository>().GetGraphItemByGraph(fromGraphGuid).ToList();
                if (fromGraphItems != null)
                {
                    foreach (GraphItem fromItem in fromGraphItems)
                    {
                        if (!fromItem.GraphItemContentReference.IsLoaded) fromItem.GraphItemContentReference.Load();
                        if (fromItem.GraphItemContent.Name.Trim() == toGraphItem.GraphItemContent.Name.Trim())
                        {
                            fromGraphItemGuid = fromItem.GraphItemGUID;
                            break;
                        }
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(toGraphItem.ParentGraphItemGUID.Value.ToString()))
                    {
                        GraphItem graphItemEntity = Resolve<IGraphItemRepository>().get(toGraphItem.ParentGraphItemGUID.Value);
                        if (graphItemEntity != null)
                        {
                            fromGraphItemGuid = graphItemEntity.GraphItemGUID;
                        }
                    }
                }
            }
            return fromGraphItemGuid;
        }
        private Guid GetFromPreferencesGuid(ProgramModel programModel, Guid toPreferGuid)
        {
            Guid fromPreferGuid = Guid.Empty;
            Preferences toPrefer = Resolve<IPreferencesRepository>().GetPreference(toPreferGuid);
            if (toPrefer != null)
            {
                if (!toPrefer.PageReference.IsLoaded) toPrefer.PageReference.Load();
                Guid fromPageGuid = GetFromPageGuid(programModel, toPrefer.Page.PageGUID);
                List<Preferences> fromPrefers = Resolve<IPreferencesRepository>().GetPreferenceByPageGuid(fromPageGuid).ToList();
                if (fromPrefers != null)
                {
                    foreach (Preferences fromPrefer in fromPrefers)
                    {
                        if (fromPrefer.Name.Trim() == toPrefer.Name.Trim())
                        {
                            fromPreferGuid = fromPrefer.PreferencesGUID;
                            break;
                        }
                    }
                }
            }
            return fromPreferGuid;
        }
        private Guid GetFromProgramRoomGuid(ProgramModel programModel, Guid toProgramRoomGuid)
        {
            Guid fromProRoomGuid = Guid.Empty;
            ProgramRoom toProgramRoom = Resolve<IProgramRoomRepository>().GetRoom(toProgramRoomGuid);
            if (toProgramRoom != null)
            {
                List<ProgramRoom> fromProgramRooms = Resolve<IProgramRoomRepository>().GetRoomByProgram(programModel.Guid);
                if (fromProgramRooms != null)
                {
                    foreach (ProgramRoom fromProgramRoom in fromProgramRooms)
                    {
                        if (fromProgramRoom.Name.Trim() == toProgramRoom.Name.Trim())
                        {
                            fromProRoomGuid = fromProgramRoom.ProgramRoomGUID;
                            break;
                        }
                    }
                }
            }
            return fromProRoomGuid;
        }
        private Guid GetFromEmailTemplateGuid(ProgramModel programModel, Guid toEmailTemplateGuid)
        {
            Guid fromEmailTemGuid = Guid.Empty;
            EmailTemplate toEmailTemplate = Resolve<IEmailTemplateRepository>().GetEmailTemplate(toEmailTemplateGuid);
            if (toEmailTemplate != null)
            {
                if (!toEmailTemplate.EmailTemplateTypeReference.IsLoaded) toEmailTemplate.EmailTemplateTypeReference.Load();
                EmailTemplate fromEmailTemplate = Resolve<IEmailTemplateRepository>().GetByProgramEmailTemplateType(programModel.Guid, toEmailTemplate.EmailTemplateType.EmailTemplateTypeGUID);
                if (fromEmailTemplate != null)
                {
                    fromEmailTemGuid = fromEmailTemplate.EmailTemplateGUID;
                }
            }
            return fromEmailTemGuid;
        }
        private Guid GetFromHelpItemGuid(ProgramModel programModel, Guid toHelpItemGuid)
        {
            Guid fromHelpItemGuid = Guid.Empty;
            HelpItem toHelpItem = Resolve<IHelpItemRepository>().GetItem(toHelpItemGuid);
            if (toHelpItem != null)
            {
                List<HelpItem> fromHelpItems = Resolve<IHelpItemRepository>().GetItemByProgram(programModel.Guid).ToList();
                if (fromHelpItems != null)
                {
                    foreach (HelpItem fromHelpItem in fromHelpItems)
                    {
                        if ((fromHelpItem.Order != null && toHelpItem.Order != null && fromHelpItem.Order == toHelpItem.Order) || fromHelpItem.Question.Trim() == toHelpItem.Question.Trim())
                        {
                            fromHelpItemGuid = fromHelpItem.HelpItemGUID;
                            break;
                        }
                    }
                }
            }
            return fromHelpItemGuid;
        }
        private Guid GetFromTipMessageGuid(ProgramModel programModel, Guid toTipMessageGuid)
        {
            Guid fromTipMessGuid = Guid.Empty;
            TipMessage toTipMess = Resolve<ITipMessageRepository>().GetTipMessage(toTipMessageGuid);
            if (toTipMess != null)
            {
                List<TipMessage> fromTipMessEs = Resolve<ITipMessageRepository>().GetTipMessageByProgram(programModel.Guid).ToList();
                if (fromTipMessEs != null)
                {
                    foreach (TipMessage fromTipMess in fromTipMessEs)
                    {
                        if (fromTipMess.Message.Trim() == toTipMess.Message.Trim())
                        {
                            fromTipMessGuid = fromTipMess.TipMessageGUID;
                            break;
                        }
                    }
                }
            }
            return fromTipMessGuid;
        }
        private Guid GetFromUserMenuGuid(ProgramModel programModel, Guid toUserMenuGuid)
        {
            Guid fromUserMenuGuid = Guid.Empty;
            UserMenu toUserMenu = Resolve<IUserMenuRepository>().GetUserMenu(toUserMenuGuid);
            if (toUserMenu != null)
            {
                List<UserMenu> fromUserMenus = Resolve<IUserMenuRepository>().GetUserMenuOfProgram(programModel.Guid).ToList();
                if (fromUserMenus != null)
                {
                    foreach (UserMenu formUserMenu in fromUserMenus)
                    {
                        if (formUserMenu.Name.Trim() == toUserMenu.Name.Trim())
                        {
                            fromUserMenuGuid = formUserMenu.MenuItemGUID;
                            break;
                        }
                    }
                }
            }
            return fromUserMenuGuid;
        }
        private Guid GetFromAccessoryTemplateGuid(ProgramModel programModel, Guid toAccTemGuid)
        {
            Guid fromAccTemGuid = Guid.Empty;
            AccessoryTemplate toAccTem = Resolve<IProgramAccessoryRepository>().GetAccessory(toAccTemGuid);
            if (toAccTem != null)
            {
                AccessoryTemplate fromAccTem = Resolve<IProgramAccessoryRepository>().GetAccessory(programModel.Guid, toAccTem.Type);
                if (fromAccTem != null)
                {
                    fromAccTemGuid = fromAccTem.AccessoryTemplateGUID;
                }
            }
            return fromAccTemGuid;
        }

        #endregion

        private void ConstructWorkSheet(XmlNodeList dataList, string objectName, Guid TranslationJobContentGuid, TranslationJobModel TranslationJobModelModel)
        {
            int eleOrder = 0;
            if (dataList.Count > 0)
            {
                foreach (XmlNode model in dataList)
                {
                    //string fromObjectID = model.Attributes["ObjectDefaultGUID"] == null ? "" : model.Attributes["ObjectDefaultGUID"].Value;
                    string fromObjectID = model.Attributes["FromID"] == null ? "" : model.Attributes["FromID"].Value;
                    if (string.IsNullOrEmpty(fromObjectID))
                    {
                        fromObjectID = GetFromObjectID(objectName, model.Attributes["ID"].Value, TranslationJobModelModel);
                    }

                    foreach (XmlAttribute attr in model.Attributes)
                    {
                        if (!attr.Name.Equals("ID") && !attr.Name.Equals("ObjectDefaultGUID") && !attr.Name.Equals("Order") && !attr.Name.Equals("DefaultGUID") && attr.Name.IndexOf("From") < 0)
                        {
                            string fromContent = (model.Attributes["From" + attr.Name] == null || model.Attributes["From" + attr.Name].Value == "") ? model.Attributes[attr.Name].Value : model.Attributes["From" + attr.Name].Value;
                            eleOrder += 1;
                            TranslationJobElementModel elementModel = new TranslationJobElementModel();
                            elementModel.MaxLength = null;
                            elementModel.Object = objectName;
                            elementModel.FromObjectGUID = fromObjectID;
                            elementModel.ToObjectGUID = model.Attributes["ID"].Value;
                            elementModel.Position = attr.Name;
                            elementModel.StatusID = (int)ChangeTech.Models.TranslationJobStatusEnum.NotStart;
                            elementModel.TranslationJobContentGUID = TranslationJobContentGuid;
                            elementModel.Original = fromContent;
                            elementModel.Translated = model.Attributes[attr.Name].Value;
                            elementModel.Words = GetTranslationJobWordsCount(fromContent);
                            elementModel.Order = TranslationJobModelModel.Order + "-" + eleOrder.ToString("D4");

                            Guid elementDescriptionGuid = AddTranslationJobElement(elementModel);
                        }
                    }
                }
            }
        }

        private void CheckprogramDailySMS(XmlNodeList programDailySMSNodes, string objectName, Guid TranslationJobContentGuid, TranslationJobModel TranslationJobModelModel)
        {

            int eleOrder = 0;
            List<ProgramDailySMS> ProgramDailySMSList = new List<ProgramDailySMS>();
            ProgramModel programModel = Resolve<IProgramService>().GetProgramByProgramGUIDAndLanguageGUID(TranslationJobModelModel.Program.ProgramGuid, TranslationJobModelModel.ToLanguage.LanguageGUID);
            if (programModel != null)
            {
                List<Session> sessionListByOriginalProgram = Resolve<ISessionRepository>().GetSessionByProgramGuid(TranslationJobModelModel.Program.ProgramGuid).ToList();
                foreach (var sessionByOriginal in sessionListByOriginalProgram)
                {
                    if (sessionByOriginal.Day != 0)
                    {
                        List<Session> sessionListByNewProgram = Resolve<ISessionRepository>().GetSessionByProgramGuid(programModel.Guid).ToList();
                        foreach (var sessionByNew in sessionListByNewProgram)
                        {
                            if (sessionByNew.Day == sessionByOriginal.Day)
                            {
                                ProgramDailySMS dailySMSByOriginal = Resolve<IShortMessageRepository>().GetProgramDailySMSBySessionGuid(sessionByOriginal.SessionGUID);
                                if (dailySMSByOriginal != null)
                                {
                                    ProgramDailySMS dailySMSByNew = new ProgramDailySMS
                                    {
                                        ProgramDailySMSGUID = Guid.NewGuid(),
                                        Session = sessionByNew,
                                        SMSContent = dailySMSByOriginal.SMSContent,
                                        IsDeleted = null,
                                    };
                                    Resolve<IShortMessageRepository>().AddProgramDailySMS(dailySMSByNew);

                                    //Add TranslationJobElement By ProgramDailySMS Content
                                    eleOrder += 1;
                                    TranslationJobElementModel elementModel = new TranslationJobElementModel();
                                    elementModel.MaxLength = null;
                                    elementModel.Object = objectName;
                                    elementModel.FromObjectGUID = dailySMSByOriginal.ProgramDailySMSGUID.ToString();
                                    elementModel.ToObjectGUID = dailySMSByNew.ProgramDailySMSGUID.ToString();
                                    elementModel.Position = "SessionContent";
                                    elementModel.StatusID = (int)ChangeTech.Models.TranslationJobStatusEnum.NotStart;
                                    elementModel.TranslationJobContentGUID = TranslationJobContentGuid;
                                    elementModel.Original = dailySMSByOriginal.SMSContent;
                                    elementModel.Translated = dailySMSByNew.SMSContent;
                                    elementModel.Words = GetTranslationJobWordsCount(dailySMSByOriginal.SMSContent);
                                    elementModel.Order = TranslationJobModelModel.Order + "-" + eleOrder.ToString("D4");

                                    Guid elementDescriptionGuid = AddTranslationJobElement(elementModel);
                                }
                            }
                        }
                    }
                }
            }
        }

        #endregion

        #region private method GetLanguageCodeForAPI
        private string GetLanguageCodeForAPI(string languageName)
        {
            string languageCode = string.Empty;
            switch (languageName)
            {
                case "English":
                    languageCode = "en";
                    break;
                case "Icelandic":
                    languageCode = "is";
                    break;
                case "Norwegian":
                    languageCode = "no";
                    break;
                case "Danish":
                    languageCode = "da";
                    break;
                case "Norwegian Test":
                    languageCode = "nn";
                    break;
                case "Spanish":
                    languageCode = "es";
                    break;
                case "Swedish":
                    languageCode = "sv";
                    break;
                case "Finnish":
                    languageCode = "fi";
                    break;
                default:
                    languageCode = "no";
                    break;
            }

            return languageCode;
        }
        #endregion

        public string GoogleTranslationJob(string fromString, string fromLanguage, string toLanguage)
        {
            try
            {
                if (fromLanguage == toLanguage)//can't be equal, or the api will get bad request.
                {
                    return fromString;
                }
                else
                {
                    string translatedString = string.Empty;

                    //for test
                    //fromString = "Hello World";
                    //fromLanguage = "en";
                    //toLanguage = "de";

                    string apiKey = System.Configuration.ConfigurationManager.AppSettings.Get("GoogleTranslationAPIV2Key");   //.ConfigurationSettings.AppSettings.Get("GoogleTranslationAPIV2Key");
                    string url = "https://www.googleapis.com/language/translate/v2?key=" + apiKey + "&source=" + fromLanguage + "&target=" + toLanguage + "&callback=translateText&q=" + fromString;
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                    request.Method = "GET";
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                    Stream receiveStream = response.GetResponseStream();
                    StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
                    string strTemp = "";
                    StringBuilder strSource = new StringBuilder();
                    while ((strTemp = readStream.ReadLine()) != null)
                    {
                        if (strTemp.IndexOf("\"translatedText\":") > -1)
                        {
                            int index = strTemp.IndexOf("translatedText");
                            translatedString = strTemp.Replace("\"translatedText\":", "").Replace("\"", "").Trim();
                            break;
                        }
                    }
                    response.Close();
                    readStream.Close();
                    //fromString = "Introduction of program, departments and helpers.";//for test
                    //TranslateClient tClient = new TranslateClient(url);
                    //translatedString = tClient.Translate(fromString, fromLanguage, toLanguage);

                    return translatedString;
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public List<TranslationJobElementModel> GoogleTranslateForElements(List<TranslationJobElementModel> elementList)
        {
            if (elementList.Count > 0)
            {
                TranslationJobContent content = Resolve<ITranslationJobContentRepository>().GetTranslationJobContentByGuid(elementList[0].TranslationJobContentGUID);
                if (!content.TranslationJobReference.IsLoaded)
                {
                    content.TranslationJobReference.Load();
                }
                if (!content.TranslationJob.LanguageReference.IsLoaded) content.TranslationJob.LanguageReference.Load();
                if (!content.TranslationJob.Language1Reference.IsLoaded) content.TranslationJob.Language1Reference.Load();
                string fromLang = content.TranslationJob.Language.Name;
                string toLang = content.TranslationJob.Language1.Name;
                fromLang = GetLanguageCodeForAPI(fromLang);
                toLang = GetLanguageCodeForAPI(toLang);
                string fromText = string.Empty;

                foreach (TranslationJobElementModel element in elementList)
                {
                    if (element.Original.Trim() != "")
                    {
                        if (element.Original.Length <= GoogleTranslationAPIMaxLength)
                        {
                            element.GoogleTranslate = GoogleTranslationJob(element.Original, fromLang, toLang);
                        }
                        else
                        {
                            string str = element.Original;
                            while (str != "")
                            {
                                if (str.Length > GoogleTranslationAPIMaxLength)
                                {
                                    int splitPosition = str.Substring(0, GoogleTranslationAPIMaxLength).LastIndexOf(' ');
                                    element.GoogleTranslate += GoogleTranslationJob(str.Substring(0, splitPosition + 1), fromLang, toLang);
                                    str = str.Remove(0, splitPosition + 1);
                                }
                                else
                                {
                                    element.GoogleTranslate += GoogleTranslationJob(str, fromLang, toLang);
                                    str = "";
                                }

                            }
                        }
                    }
                }
                #region old func
                //foreach (TranslationJobElementModel element in elementList)
                //{
                //    if (element.Original.Trim() == "")
                //    {
                //        fromText += "...." + " ^ ";
                //        //element.Original = "....";
                //    }
                //    else
                //    {
                //        fromText += element.Original + " ^ ";
                //    }
                //}
                //fromText = fromText.Remove(fromText.LastIndexOf('^') - 1);
                //string transText = GoogleTranslationJob(fromText, fromLang, toLang);
                //string[] transTextArray = transText.Split('^');
                //if (transTextArray.Count() == elementList.Count)
                //{
                //    for (int i = 0; i < elementList.Count; i++)
                //    {
                //        if (transTextArray[i].Trim() == "....") transTextArray[i] = "";
                //        elementList[i].GoogleTranslate = transTextArray[i].Trim();
                //    }
                //}
                #endregion
            }
            return elementList;
        }

        public string GoogleTranslateForElement(TranslationJobElementModel elementModel)
        {
            string translatedText = string.Empty;
            TranslationJobContent content = Resolve<ITranslationJobContentRepository>().GetTranslationJobContentByGuid(elementModel.TranslationJobContentGUID);
            if (!content.TranslationJobReference.IsLoaded)
            {
                content.TranslationJobReference.Load();
            }
            if (!content.TranslationJob.LanguageReference.IsLoaded) content.TranslationJob.LanguageReference.Load();
            if (!content.TranslationJob.Language1Reference.IsLoaded) content.TranslationJob.Language1Reference.Load();
            string fromLang = content.TranslationJob.Language.Name;
            string toLang = content.TranslationJob.Language1.Name;
            fromLang = GetLanguageCodeForAPI(fromLang);
            toLang = GetLanguageCodeForAPI(toLang);
            string fromText = string.Empty;


            if (elementModel.Original.Trim() != "")
            {
                if (elementModel.Original.Length <= GoogleTranslationAPIMaxLength)
                {
                    elementModel.GoogleTranslate = GoogleTranslationJob(elementModel.Original, fromLang, toLang);
                }
                else
                {
                    string str = elementModel.Original;
                    while (str != "")
                    {
                        if (str.Length > GoogleTranslationAPIMaxLength)
                        {
                            int splitPosition = str.Substring(0, GoogleTranslationAPIMaxLength).LastIndexOf(' ');
                            elementModel.GoogleTranslate += GoogleTranslationJob(str.Substring(0, splitPosition + 1), fromLang, toLang);
                            str = str.Remove(0, splitPosition + 1);
                        }
                        else
                        {
                            elementModel.GoogleTranslate += GoogleTranslationJob(str, fromLang, toLang);
                            str = "";
                        }

                    }
                }
            }
            translatedText = elementModel.GoogleTranslate;
            return translatedText;
        }

        public Guid GetToLanguageGuidFromTransContentGuid(Guid TranslationJobContentGuid)
        {
            TranslationJobContent content = Resolve<ITranslationJobContentRepository>().GetTranslationJobContentByGuid(TranslationJobContentGuid);
            if (!content.TranslationJobReference.IsLoaded) content.TranslationJobReference.Load();
            if (!content.TranslationJob.Language1Reference.IsLoaded) content.TranslationJob.Language1Reference.Load();

            return content.TranslationJob.Language1.LanguageGUID;
        }

        public void UpdateElementStatusWhenFromObjectUpdated(string position, string objectGuid, string updateValue)
        {
            List<TranslationJobElement> elementEntities = Resolve<ITranslationJobElementRepository>().GetTranslationJobElementByFromObjectGuidAndPosition(objectGuid, position).ToList();
            if (elementEntities != null && elementEntities.Count > 0)
            {
                foreach (TranslationJobElement element in elementEntities)
                {
                    element.FromContent = updateValue;
                    element.StatusGUID = (int)ChangeTech.Models.TranslationJobStatusEnum.Changed;
                    Resolve<ITranslationJobElementRepository>().Update(element);
                }
            }
        }

        public void UpdateElementWhenFromUpdated(string table, string objectGuid, Guid languageGuid)
        {
            switch (table)
            {
                case "PageContent":
                    Guid updateGuid = new Guid(objectGuid);
                    Resolve<ITranslationService>().UpdateElementFromPageContent(updateGuid);
                    break;
                case "PageQuestionContent":
                    Guid pageQuestionGuid = new Guid(objectGuid);
                    Resolve<ITranslationService>().UpdateElementFromPageQuestionContent(pageQuestionGuid);
                    break;
                case "PageQuestionItemContent":
                    Guid pageQuestionItemGuid = new Guid(objectGuid);
                    Resolve<ITranslationService>().UpdateElementFromPageQuestionItemContent(pageQuestionItemGuid);
                    break;
                case "ProgramRoom":
                    Guid programRoomGuid = new Guid(objectGuid);
                    Resolve<ITranslationService>().UpdateElementFromProgramRoom(programRoomGuid);
                    break;
                case "GraphContent":
                    Guid graphContentGuid = new Guid(objectGuid);
                    Resolve<ITranslationService>().UpdateElementFromGraphContent(graphContentGuid);
                    break;
                case "EmailTemplate":
                    Guid emailTemplateGuid = new Guid(objectGuid);
                    Resolve<ITranslationService>().UpdateElementFromEmailTemplate(emailTemplateGuid);
                    break;
                case "GraphItemContent":
                    Guid graphItemContentGuid = new Guid(objectGuid);
                    Resolve<ITranslationService>().UpdateElementFromGraphItemContent(graphItemContentGuid);
                    break;
                case "HelpItem":
                    Guid helpItemGuid = new Guid(objectGuid);
                    Resolve<ITranslationService>().UpdateElementFromHelpItem(helpItemGuid);
                    break;
                case "Preferences":
                    Guid preferGuid = new Guid(objectGuid);
                    Resolve<ITranslationService>().UpdateElementFromPreference(preferGuid);
                    break;
                case "Session":
                    Guid sessionGuid = new Guid(objectGuid);
                    Resolve<ITranslationService>().UpdateElementFromSession(sessionGuid);
                    break;
                case "SpecialString":
                    Resolve<ITranslationService>().UpdateElementFromSepcialString(objectGuid, languageGuid);
                    break;
                case "TipMessage":
                    Guid tipMessageGuid = new Guid(objectGuid);
                    Resolve<ITranslationService>().UpdateElementFromTipMessage(tipMessageGuid);
                    break;
                case "UserMenu":
                    Guid userMenuGuid = new Guid(objectGuid);
                    Resolve<ITranslationService>().UpdateElementFromUserMenu(userMenuGuid);
                    break;
                case "AccessoryTemplate":
                    Guid accTemGuid = new Guid(objectGuid);
                    Resolve<ITranslationService>().UpdateElementFromAccessoryTemplate(accTemGuid);
                    break;
                case "Relapse":
                case "PageSequence":
                    Guid pageSequenceGuid = new Guid(objectGuid);
                    Resolve<ITranslationService>().UpdateElementFromPageSequence(pageSequenceGuid);
                    break;
                case "ScreenResultTemplatePageLine":
                    Guid pageLineGuid = new Guid(objectGuid);
                    Resolve<ITranslationService>().UpdateElementFromScreenResultTemplatePageLine(pageLineGuid);
                    break;
            }
        }

        public TranslationJobElementPagePreviewModel getTranslationJobPagePreviewModel(Guid pageGuid, Guid translationJobContentGuid)
        {
            TranslationJobElementPagePreviewModel pagePreviewModel = new TranslationJobElementPagePreviewModel();
            if (pageGuid != Guid.Empty && translationJobContentGuid != Guid.Empty)
            {
                Page pageEntity = Resolve<IPageRepository>().GetPageByPageGuid(pageGuid);
                if (!pageEntity.PageSequenceReference.IsLoaded) pageEntity.PageSequenceReference.Load();
                Guid pageSequenceGuid = pageEntity.PageSequence.PageSequenceGUID;
                SessionContent sc = Resolve<ISessionContentRepository>().GetSessionContentByPageSeqGuid(pageSequenceGuid).FirstOrDefault();
                if (!sc.SessionReference.IsLoaded) sc.SessionReference.Load();
                TranslationJobContent tjc = Resolve<ITranslationJobContentRepository>().GetTranslationJobContentByGuid(translationJobContentGuid);
                if (!tjc.TranslationJobReference.IsLoaded) tjc.TranslationJobReference.Load();


                pagePreviewModel.LanguageGuid = tjc.TranslationJob.ToLanguageGUID.ToString();
                pagePreviewModel.PageGuid = pageGuid.ToString();
                pagePreviewModel.PageSequenceGuid = pageSequenceGuid.ToString();
                pagePreviewModel.SessionGuid = sc.Session.SessionGUID.ToString();
                UserModel um = System.Web.HttpContext.Current.Session["CurrentAccount"] as UserModel;
                pagePreviewModel.UserGuid = um.UserGuid.ToString();
            }
            return pagePreviewModel;
        }
    }
}