using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ethos.DependencyInjection;
using ChangeTech.Contracts;
using ChangeTech.Entities;
using ChangeTech.IDataRepository;
using ChangeTech.Models;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;

namespace ChangeTech.Services
{
    public class ProgramLanguageService : ServiceBase, IProgramLanguageService
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
        private const string PROGRAM_STATUS_UNDER_DEVELOPMENT = "BCB92CEF-FD49-4818-BBB2-8DFC96FF0FE1";

        public void AddProgramLanguage(Guid programGuid, Guid languageGuid)
        {
            //Add record into program language table
            Program programEntity = Resolve<IProgramRepository>().GetProgramByGuid(programGuid);
            ProgramLanguage programLanguageEntity = new ProgramLanguage();
            programLanguageEntity.ProgramLanguageGUID = Guid.NewGuid();
            Language newLanguageEntity = Resolve<ILanguageRepository>().GetLanguage(languageGuid);
            programLanguageEntity.Language = newLanguageEntity;
            if (programEntity.DefaultGUID.HasValue)
            {
                programEntity = Resolve<IProgramRepository>().GetProgramByGuid(programEntity.DefaultGUID.Value);
            }
            if (!programEntity.LanguageReference.IsLoaded)
            {
                programEntity.LanguageReference.Load();
            }
            programLanguageEntity.Program = programEntity;
            Resolve<IProgramLanguageRepository>().AddProgramLanguage(programLanguageEntity);

            Guid newProgramGUID = AddProgramLanguage(programEntity, newLanguageEntity);
            CopyHelpItem(programEntity, newProgramGUID, newLanguageEntity);
            CopySession(newProgramGUID, programEntity, newLanguageEntity, programEntity.Language.LanguageGUID);
        }

        public void RemoveProgramLanguage(Guid programGuid, Guid languageGuid)
        {
            CloudQueue statusQueue = Resolve<IAzureQueueService>().GetCloudQueue(GetProgramLanguageServiceStatusQueueName(programGuid));
            string statusMsg = string.Format("{0}", "Start");
            Resolve<IAzureQueueService>().AddQueueMessage(statusQueue, statusMsg, true);

            Resolve<IProgramLanguageRepository>().RemoveProgramLanaguage(programGuid, languageGuid);
            Program programEntity = Resolve<IProgramRepository>().GetProgramByGuid(programGuid);
            if (programEntity.DefaultGUID.HasValue)
            {
                Resolve<IProgramService>().DeleteProgram(programGuid);
            }
            else
            {
                programEntity = Resolve<IProgramRepository>().GetProgramByProgramDefaultGUIDAndLanguageGUID(programGuid, languageGuid);
                if (programEntity != null)
                {
                    Resolve<IProgramService>().DeleteProgram(programEntity.ProgramGUID);
                }
            }

            statusMsg = string.Format("{0}", "Complete");
            Resolve<IAzureQueueService>().AddQueueMessage(statusQueue, statusMsg, true);
        }

        private string GetProgramLanguageServiceStatusQueueName(Guid programGuid)
        {
            return string.Format("{0}{1}{2}", STATUSQUEUEMAKT, programGuid.ToString().ToLower(), versionNumber);
        }

        public LanguagesModel GetLanguagesNotSupportByProgram(Guid programGuid)
        {
            LanguagesModel languagesNotSupportByProgram = new LanguagesModel();

            Program programEntity = Resolve<IProgramRepository>().GetProgramByGuid(programGuid);
            if (programEntity.DefaultGUID.HasValue)
            {
                programGuid = programEntity.DefaultGUID.Value;
            }

            List<ProgramLanguage> languagesSupportByProgram = Resolve<IProgramLanguageRepository>().GetLanguagesOfProgram(programGuid).ToList();
            List<Language> allLanguages = Resolve<ILanguageRepository>().GetAllLanguages().OrderBy(r => r.Name).ToList();

            foreach (Language languageEntity in allLanguages)
            {
                bool alreadySupport = false;
                foreach (ProgramLanguage programLanguageEntity in languagesSupportByProgram)
                {

                    if (!programLanguageEntity.LanguageReference.IsLoaded)
                    {
                        programLanguageEntity.LanguageReference.Load();
                    }

                    if (programLanguageEntity.Language.LanguageGUID.Equals(languageEntity.LanguageGUID))
                    {
                        alreadySupport = true;
                        break;
                    }
                }

                if (!alreadySupport)
                {
                    LanguageModel languageModel = new LanguageModel();
                    languageModel.LanguageGUID = languageEntity.LanguageGUID;
                    languageModel.Name = languageEntity.Name;
                    languagesNotSupportByProgram.Add(languageModel);
                }
            }

            return languagesNotSupportByProgram;
        }

        public LanguagesModel GetLanguagesSupportByProgram(Guid programGuid)
        {
            LanguagesModel languagesSupportByProgram = new LanguagesModel();
            Program programEntity = Resolve<IProgramRepository>().GetProgramByGuid(programGuid);
            if (programEntity.DefaultGUID.HasValue)
            {
                programGuid = programEntity.DefaultGUID.Value;
            }

            List<ProgramLanguage> languageEntitySupportByProgram = Resolve<IProgramLanguageRepository>().GetLanguagesOfProgram(programGuid).ToList();
            foreach (ProgramLanguage programLanguageEntity in languageEntitySupportByProgram)
            {
                if (!programLanguageEntity.LanguageReference.IsLoaded)
                {
                    programLanguageEntity.LanguageReference.Load();
                }

                if (!programLanguageEntity.ProgramReference.IsLoaded)
                {
                    programLanguageEntity.ProgramReference.Load();
                }

                if (!programLanguageEntity.Program.LanguageReference.IsLoaded)
                {
                    programLanguageEntity.Program.LanguageReference.Load();
                }

                LanguageModel languageModel = new LanguageModel();
                languageModel.LanguageGUID = programLanguageEntity.Language.LanguageGUID;
                languageModel.Name = programLanguageEntity.Language.Name;


                if (programLanguageEntity.Language.LanguageGUID ==
                    programLanguageEntity.Program.Language.LanguageGUID)
                {
                    languageModel.IsDefaultLanguage = true;
                }
                else
                {
                    languageModel.IsDefaultLanguage = false;
                }

                ProgramModel pm = Resolve<IProgramService>().GetProgramByProgramGUIDAndLanguageGUID(programGuid, programLanguageEntity.Language.LanguageGUID);
                languageModel.DaysCount = pm.DaysCount;
                languageModel.StartDay = pm.StartDay;
                languagesSupportByProgram.Add(languageModel);
            }

            return languagesSupportByProgram;
        }

        private Guid AddProgramLanguage(Program programEntity, Language newLanguageEntity)
        {
            //Add new program of this language
            Program newProgramOfLanguage = new Program();
            newProgramOfLanguage.ProgramGUID = Guid.NewGuid();
            newProgramOfLanguage.Name = string.Format("{0} ({1})", programEntity.Name, newLanguageEntity.Name);
            newProgramOfLanguage.Description = string.Format("{0} ({1})", programEntity.Description, newLanguageEntity.Name);
            newProgramOfLanguage.Created = DateTime.UtcNow;
            newProgramOfLanguage.CreatedBy = Resolve<IUserService>().GetCurrentUser().UserGuid;
            newProgramOfLanguage.Language = newLanguageEntity;
            newProgramOfLanguage.LastUpdated = DateTime.UtcNow;
            newProgramOfLanguage.LastUpdatedBy = Resolve<IUserService>().GetCurrentUser().UserGuid;
            //TODO: Add IsDefault field in ProgramStatus table, set Under Development record to true, set Default Status when add new program 
            newProgramOfLanguage.ProgramStatus = Resolve<IProgramStatusRepository>().GetProgramStatusByStatusGuid(new Guid(PROGRAM_STATUS_UNDER_DEVELOPMENT));
            newProgramOfLanguage.DefaultGUID = programEntity.ProgramGUID;
            newProgramOfLanguage.ParentProgramGUID = programEntity.ProgramGUID;
            if (!programEntity.ResourceReference.IsLoaded)
            {
                programEntity.ResourceReference.Load();
            }
            newProgramOfLanguage.Resource = programEntity.Resource;
            newProgramOfLanguage.IsDeleted = false;
            Resolve<IProgramRepository>().InsertProgram(newProgramOfLanguage);

            return newProgramOfLanguage.ProgramGUID;
        }

        private void CopySession(Guid newProgramGUID, Program originalProgramEntity, Language newLanguageEntity, Guid defaultLanguageGuid)
        {
            if (!originalProgramEntity.Session.IsLoaded)
            {
                originalProgramEntity.Session.Load();
            }

            //CloudQueue statusQueue = Resolve<IAzureQueueService>().GetCloudQueue("statusqueue");

            foreach (Session sessionEntity in originalProgramEntity.Session)
            {
                Session newSessionEntity = new Session();
                newSessionEntity.SessionGUID = Guid.NewGuid();
                newSessionEntity.Program = Resolve<IProgramRepository>().GetProgramByGuid(newProgramGUID);
                newSessionEntity.Name = string.Format("{0} ({1})", sessionEntity.Name, newLanguageEntity.Name);
                newSessionEntity.Description = string.Format("{0} ({1})", sessionEntity.Description, newLanguageEntity.Name);
                newSessionEntity.Day = sessionEntity.Day;
                newSessionEntity.IsDeleted = false;
                newSessionEntity.LastUpdated = DateTime.UtcNow;
                newSessionEntity.LastUpdatedBy = Resolve<IUserService>().GetCurrentUser().UserGuid;
                newSessionEntity.IsDeleted = false;
                Resolve<ISessionRepository>().InsertSession(newSessionEntity);

                //string statusMsg = string.Format("{0};{1};{2};{3}", "Copy session " + sessionEntity.Day, "AddProgramLanguage", originalProgramEntity.ProgramGUID, newLanguageEntity.LanguageGUID);
                //statusQueue.Clear();
                //Resolve<IAzureQueueService>().AddQueueMessage(statusQueue, statusMsg);

                CopyPageSequence(newProgramGUID, newSessionEntity.SessionGUID, sessionEntity, newLanguageEntity, defaultLanguageGuid);
            }
        }

        private void CopyPageSequence(Guid newProgramGuid, Guid newSessionGuid, Session originalSessionEntity, Language newLanguageEntity, Guid defaultLanguageGuid)
        {
            if (!originalSessionEntity.SessionContent.IsLoaded)
            {
                originalSessionEntity.SessionContent.Load();
            }

            foreach (SessionContent sessionContentEntity in originalSessionEntity.SessionContent)
            {
                if (!sessionContentEntity.PageSequenceReference.IsLoaded)
                {
                    sessionContentEntity.PageSequenceReference.Load();
                }

                if (!sessionContentEntity.PageSequence.InterventReference.IsLoaded)
                {
                    sessionContentEntity.PageSequence.InterventReference.Load();
                }

                if (!sessionContentEntity.PageSequence.Page.IsLoaded)
                {
                    sessionContentEntity.PageSequence.Page.Load();
                }

                if (!sessionContentEntity.ProgramRoomReference.IsLoaded)
                {
                    sessionContentEntity.ProgramRoomReference.Load();
                }

                PageSequence newPageSequenceEntity = new PageSequence();
                newPageSequenceEntity.PageSequenceGUID = Guid.NewGuid();
                newPageSequenceEntity.Name = string.Format("{0} ({1})", sessionContentEntity.PageSequence.Name, newLanguageEntity.Name);
                newPageSequenceEntity.Description = string.Format("{0} ({1})", sessionContentEntity.PageSequence.Description, newLanguageEntity.Name);
                newPageSequenceEntity.IsDeleted = false;
                newPageSequenceEntity.Intervent = sessionContentEntity.PageSequence.Intervent;
                newPageSequenceEntity.LastUpdated = DateTime.UtcNow;
                newPageSequenceEntity.LastUpdatedBy = Resolve<IUserService>().GetCurrentUser().UserGuid;
                Resolve<IPageSequenceRepository>().InstertPageSequence(newPageSequenceEntity);

                foreach (Page pageEntity in sessionContentEntity.PageSequence.Page)
                {
                    CopyPage(newProgramGuid, newPageSequenceEntity.PageSequenceGUID, pageEntity, newLanguageEntity, defaultLanguageGuid);
                }

                SessionContent newSessionContentEntity = new SessionContent();
                newSessionContentEntity.SessionContentGUID = Guid.NewGuid();
                newSessionContentEntity.Session = Resolve<ISessionRepository>().GetSessionBySessionGuid(newSessionGuid);
                newSessionContentEntity.PageSequence = Resolve<IPageSequenceRepository>().GetPageSequenceByGuid(newPageSequenceEntity.PageSequenceGUID);
                newSessionContentEntity.PageSequenceOrderNo = sessionContentEntity.PageSequenceOrderNo;
                newSessionContentEntity.LastUpdated = DateTime.UtcNow;
                newSessionContentEntity.LastUpdatedBy = Resolve<IUserService>().GetCurrentUser().UserGuid;
                newSessionContentEntity.IsDeleted = false;
                if (sessionContentEntity.ProgramRoom != null)
                {
                    newSessionContentEntity.ProgramRoom = GetProgramRoom(newProgramGuid, sessionContentEntity.ProgramRoom, newLanguageEntity);
                }

                SessionContent sc = Resolve<ISessionContentRepository>().GetSessionContentBySessionContentGuid(newSessionContentEntity.SessionContentGUID);
                if (sc == null)
                {
                    Resolve<ISessionContentRepository>().Insert(newSessionContentEntity);
                }
            }
        }

        private void CopyPage(Guid newProgramGuid, Guid newPageSequenceGuid, Page originalPageEntity, Language newLanguageEntity, Guid defaultLanguageGuid)
        {
            if (!originalPageEntity.PageTemplateReference.IsLoaded)
            {
                originalPageEntity.PageTemplateReference.Load();
            }

            Page newPageEntity = new Page();
            newPageEntity.PageGUID = Guid.NewGuid();
            newPageEntity.PageSequence = Resolve<IPageSequenceRepository>().GetPageSequenceByGuid(newPageSequenceGuid);
            newPageEntity.PageTemplate = originalPageEntity.PageTemplate;
            newPageEntity.Created = DateTime.UtcNow;
            newPageEntity.CreatedBy = Resolve<IUserService>().GetCurrentUser().UserGuid;
            newPageEntity.IsDeleted = false;
            newPageEntity.LastUpdated = DateTime.UtcNow;
            newPageEntity.LastUpdatedBy = Resolve<IUserService>().GetCurrentUser().UserGuid;

            if (originalPageEntity.MaxPreferences != null)
            {
                newPageEntity.MaxPreferences = originalPageEntity.MaxPreferences.Value;
            }

            newPageEntity.PageOrderNo = originalPageEntity.PageOrderNo;
            newPageEntity.Wait = originalPageEntity.Wait;

            if (!originalPageEntity.PageVariableReference.IsLoaded)
            {
                originalPageEntity.PageVariableReference.Load();
            }

            if (originalPageEntity.PageVariable != null)
            {
                ChangeTech.Entities.PageVariable pageVariableEntity = GetPageVariable(newProgramGuid, originalPageEntity.PageVariable, newLanguageEntity);
                newPageEntity.PageVariable = pageVariableEntity;
            }

            if (Resolve<IPageRepository>().GetPageByPageGuid(newPageEntity.PageGUID) == null)
            {
                Resolve<IPageRepository>().InstertPage(newPageEntity);
            }

            CopyPageContent(newPageEntity.PageGUID, originalPageEntity, newLanguageEntity);
            CopyPageMedia(newPageEntity.PageGUID, originalPageEntity, newLanguageEntity);
            CopyGraph(newPageEntity.PageGUID, originalPageEntity, newLanguageEntity);
            CopyPreference(newProgramGuid, originalPageEntity, newPageEntity.PageGUID, newLanguageEntity);

            if (!originalPageEntity.PageQuestion.IsLoaded)
            {
                originalPageEntity.PageQuestion.Load();
            }

            if (originalPageEntity.PageQuestion != null && originalPageEntity.PageQuestion.Count > 0)
            {
                CopyPageQuestion(newProgramGuid, newPageEntity.PageGUID, originalPageEntity, newLanguageEntity);
            }
        }

        private void CopyPageContent(Guid newPageGuid, Page originalPageEntity, Language newLanguageEntity)
        {
            if (!originalPageEntity.PageContentReference.IsLoaded)
            {
                originalPageEntity.PageContentReference.Load();
            }

            if (originalPageEntity.PageContent != null)
            {
                PageContent newPageContentEntity = new PageContent();
                newPageContentEntity.Page = Resolve<IPageRepository>().GetPageByPageGuid(newPageGuid);
                newPageContentEntity.AfterShowExpression = originalPageEntity.PageContent.AfterShowExpression;
                newPageContentEntity.BeforeShowExpression = originalPageEntity.PageContent.BeforeShowExpression;
                newPageContentEntity.Body = string.Format("{0} ({1})", originalPageEntity.PageContent.Body, newLanguageEntity.Name);
                newPageContentEntity.FooterText = string.Format("{0} ({1})", originalPageEntity.PageContent.FooterText, newLanguageEntity.Name);
                newPageContentEntity.Heading = string.Format("{0} ({1})", originalPageEntity.PageContent.Heading, newLanguageEntity.Name);
                newPageContentEntity.IsDeleted = false;
                //newPageContentEntity.Language = newLanguageEntity;
                newPageContentEntity.LastUpdated = DateTime.UtcNow;
                newPageContentEntity.LastUpdatedBy = Resolve<IUserService>().GetCurrentUser().UserGuid;
                newPageContentEntity.PresenterImagePosition = originalPageEntity.PageContent.PresenterImagePosition;
                newPageContentEntity.PrimaryButtonActionParameter = originalPageEntity.PageContent.PrimaryButtonActionParameter;
                newPageContentEntity.PrimaryButtonCaption = originalPageEntity.PageContent.PrimaryButtonCaption;
                if (!originalPageEntity.PageContent.Resource_BackgroundImageReference.IsLoaded)
                {
                    originalPageEntity.PageContent.Resource_BackgroundImageReference.Load();
                }
                newPageContentEntity.Resource_BackgroundImage = originalPageEntity.PageContent.Resource_BackgroundImage;
                if (!originalPageEntity.PageContent.Resource_PresenterImageReference.IsLoaded)
                {
                    originalPageEntity.PageContent.Resource_PresenterImageReference.Load();
                }
                newPageContentEntity.Resource_PresenterImage = originalPageEntity.PageContent.Resource_PresenterImage;
                Resolve<IPageContentRepository>().Add(newPageContentEntity);
            }
        }

        private void CopyPageMedia(Guid newPageGuid, Page originalPageEntity, Language newLanguageEntity)
        {
            if (!originalPageEntity.PageMediaReference.IsLoaded)
            {
                originalPageEntity.PageMediaReference.Load();
            }

            if (originalPageEntity.PageMedia != null)
            {
                //foreach (PageMedia originalPageMediaEntity in originalPageEntity.PageMedia)
                //{
                PageMedia newPageMediaEntity = new PageMedia();
                newPageMediaEntity.IsDeleted = false;
                //newPageMediaEntity.Language = newLanguageEntity;
                newPageMediaEntity.Page = Resolve<IPageRepository>().GetPageByPageGuid(newPageGuid);
                newPageMediaEntity.Type = originalPageEntity.PageMedia.Type;
                if (!originalPageEntity.PageMedia.ResourceReference.IsLoaded)
                {
                    originalPageEntity.PageMedia.ResourceReference.Load();
                }
                newPageMediaEntity.Resource = originalPageEntity.PageMedia.Resource;
                Resolve<IPageMediaRepository>().AddPageMedia(newPageMediaEntity);
                //}
            }
        }

        private void CopyPageQuestion(Guid newProgramGuid, Guid newPageGuid, Page originalPageEntity, Language newLanguageEntity)
        {
            foreach (PageQuestion pageQuestionEntity in originalPageEntity.PageQuestion)
            {
                PageQuestion newPageQuestionEntity = new PageQuestion();
                newPageQuestionEntity.IsDeleted = false;
                newPageQuestionEntity.IsRequired = pageQuestionEntity.IsRequired;
                newPageQuestionEntity.LastUpdated = DateTime.UtcNow;
                newPageQuestionEntity.LastUpdatedBy = Resolve<IUserService>().GetCurrentUser().UserGuid;
                newPageQuestionEntity.Order = pageQuestionEntity.Order;
                newPageQuestionEntity.Page = Resolve<IPageRepository>().GetPageByPageGuid(newPageGuid);
                newPageQuestionEntity.PageQuestionGUID = Guid.NewGuid();
                if (!pageQuestionEntity.QuestionReference.IsLoaded)
                {
                    pageQuestionEntity.QuestionReference.Load();
                }
                newPageQuestionEntity.Question = Resolve<IQuestionRepository>().GetQuestion(pageQuestionEntity.Question.QuestionGUID);
                if (!pageQuestionEntity.PageVariableReference.IsLoaded)
                {
                    pageQuestionEntity.PageVariableReference.Load();
                }
                if (pageQuestionEntity.PageVariable != null)
                {
                    newPageQuestionEntity.PageVariable = GetPageVariable(newProgramGuid, pageQuestionEntity.PageVariable, newLanguageEntity);
                }
            }
        }

        private void CopyPageQuestionContent(Guid originalPageQuestionGuid, Guid newPageGuid, Language newLanguageEntity)
        {
            PageQuestionContent pageQuestionContentEntity = Resolve<IPageQuestionContentRepository>().GetPageQuestionContentByPageQuestionGuid(originalPageQuestionGuid);
            PageQuestionContent newPageQuestionContentEntity = new PageQuestionContent();
            newPageQuestionContentEntity.Caption = pageQuestionContentEntity.Caption;
            newPageQuestionContentEntity.DisableCheckBox = pageQuestionContentEntity.DisableCheckBox;
            newPageQuestionContentEntity.IsDeleted = false;
            //newPageQuestionContentEntity.Language = newLanguageEntity;
            newPageQuestionContentEntity.LastUpdated = DateTime.UtcNow;
            newPageQuestionContentEntity.LastUpdatedBy = Resolve<IUserService>().GetCurrentUser().UserGuid;
            Resolve<IPageQuestionContentRepository>().InsertPageQuestionContent(pageQuestionContentEntity);
        }

        private void CopyGraph(Guid newPageGuid, Page originalPageEntity, Language newLanguageEntity)
        {
            if (!originalPageEntity.Graph.IsLoaded)
            {
                originalPageEntity.Graph.Load();
            }

            if (originalPageEntity.Graph != null && originalPageEntity.Graph.Count > 0)
            {
                foreach (Graph originalGraphEntity in originalPageEntity.Graph)
                {
                    Graph newGraph = new Graph();
                    newGraph.GraphGUID = Guid.NewGuid();
                    newGraph.BadScoreRange = originalGraphEntity.BadScoreRange;
                    newGraph.Caption = originalGraphEntity.Caption;
                    newGraph.GoodScoreRange = originalGraphEntity.GoodScoreRange;
                    newGraph.IsDeleted = false;
                    newGraph.MediumRange = originalGraphEntity.MediumRange;
                    newGraph.Page = Resolve<IPageRepository>().GetPageByPageGuid(newPageGuid);
                    newGraph.ScoreRange = originalGraphEntity.ScoreRange;
                    newGraph.TimeRange = originalGraphEntity.TimeRange;
                    newGraph.TimeUnit = originalGraphEntity.TimeUnit;
                    newGraph.Type = originalGraphEntity.Type;
                    Resolve<IGraphRepository>().Instert(newGraph);

                    CopyGraphContent(originalGraphEntity, newGraph.GraphGUID, newLanguageEntity);
                    CopyGraphItem(originalGraphEntity, newGraph.GraphGUID, newLanguageEntity);
                }
            }
        }

        private void CopyGraphContent(Graph originalGraphEntity, Guid newGraphGuid, Language newLanguageEntity)
        {
            if (!originalGraphEntity.GraphContentReference.IsLoaded)
            {
                originalGraphEntity.GraphContentReference.Load();
            }

            if (originalGraphEntity.GraphContent != null)
            {

                GraphContent graphContent = new GraphContent();
                graphContent.Graph = Resolve<IGraphRepository>().Get(newGraphGuid);
                graphContent.IsDeleted = false;
                graphContent.Caption = originalGraphEntity.GraphContent.Caption;
                //graphContent.Language = newLanguageEntity;
                graphContent.TimeUnit = originalGraphEntity.GraphContent.TimeUnit;
                Resolve<IGraphContentRepository>().Insert(graphContent);

            }
        }

        private void CopyGraphItem(Graph originalGraphEntity, Guid newGraphGuid, Language newLanguageEntity)
        {
            if (!originalGraphEntity.GraphItem.IsLoaded)
            {
                originalGraphEntity.GraphItem.Load();
            }

            if (originalGraphEntity.GraphItem != null && originalGraphEntity.GraphItem.Count > 0)
            {
                foreach (GraphItem originalGraphItemEntity in originalGraphEntity.GraphItem)
                {
                    GraphItem newGraphItemEntity = new GraphItem();
                    newGraphItemEntity.Color = originalGraphItemEntity.Color;
                    newGraphItemEntity.DataItemExpression = originalGraphItemEntity.DataItemExpression;
                    newGraphItemEntity.Graph = Resolve<IGraphRepository>().Get(newGraphGuid);
                    //newGraphItemEntity.GraphItemContent = originalGraphItemEntity.GraphItemContent;
                    newGraphItemEntity.GraphItemGUID = Guid.NewGuid();
                    newGraphItemEntity.IsDeleted = false;
                    newGraphItemEntity.Name = originalGraphItemEntity.Name;
                    newGraphItemEntity.PointType = originalGraphItemEntity.PointType;
                    Resolve<IGraphItemRepository>().Insert(newGraphItemEntity);

                    CopyGraphItemContent(originalGraphItemEntity, newGraphItemEntity.GraphItemGUID, newLanguageEntity);
                }
            }
        }

        private void CopyGraphItemContent(GraphItem originalGraphItemEntity, Guid newGraphItemGuid, Language newLanguageEntity)
        {
            if (!originalGraphItemEntity.GraphItemContentReference.IsLoaded)
            {
                originalGraphItemEntity.GraphItemContentReference.Load();
            }

            if (originalGraphItemEntity.GraphItemContent != null)
            {
                GraphItemContent newGraphItemContentEntity = new GraphItemContent();
                newGraphItemContentEntity.GraphItem = Resolve<IGraphItemRepository>().get(newGraphItemGuid);
                newGraphItemContentEntity.IsDeleted = false;
                //newGraphItemContentEntity.Language = newLanguageEntity;
                newGraphItemContentEntity.Name = originalGraphItemEntity.GraphItemContent.Name;
                Resolve<IGraphItemContentRepository>().Insert(newGraphItemContentEntity);
            }
        }

        private void CopyHelpItem(Program originalProgramEntity, Guid newProgramGuid, Language newLanguageEntity)
        {
            if (!originalProgramEntity.HelpItem.IsLoaded)
            {
                originalProgramEntity.HelpItem.Load();
            }

            if (originalProgramEntity.HelpItem != null && originalProgramEntity.HelpItem.Count > 0)
            {
                foreach (HelpItem originalHelpItemEntity in originalProgramEntity.HelpItem)
                {
                    HelpItem newHelpItemEntity = new HelpItem();
                    newHelpItemEntity.Answer = originalHelpItemEntity.Answer;
                    newHelpItemEntity.HelpItemGUID = Guid.NewGuid();
                    //newHelpItemEntity.Language = newLanguageEntity;
                    newHelpItemEntity.Order = originalHelpItemEntity.Order;
                    newHelpItemEntity.Program = Resolve<IProgramRepository>().GetProgramByGuid(newProgramGuid);
                    newHelpItemEntity.Question = originalHelpItemEntity.Question;

                    Resolve<IHelpItemRepository>().Insert(newHelpItemEntity);
                }
            }
        }

        private void CopyPreference(Guid newProgramGuid, Page originalPageEntity, Guid newPageGuid, Language newLanguageEntity)
        {
            if (!originalPageEntity.Preferences.IsLoaded)
            {
                originalPageEntity.Preferences.Load();
            }

            if (originalPageEntity.Preferences != null && originalPageEntity.Preferences.Count > 0)
            {
                foreach (Preferences originalPreferenceEntity in originalPageEntity.Preferences)
                {
                    Preferences newPreferenceEntity = new Preferences();
                    newPreferenceEntity.AnswerText = originalPreferenceEntity.AnswerText;
                    newPreferenceEntity.ButtonName = originalPreferenceEntity.ButtonName;
                    newPreferenceEntity.Description = originalPreferenceEntity.Description;
                    newPreferenceEntity.IsDeleted = false;
                    //newPreferenceEntity.Language = newLanguageEntity;
                    newPreferenceEntity.LastUpdated = DateTime.UtcNow;
                    newPreferenceEntity.LastUpdatedBy = Resolve<IUserService>().GetCurrentUser().UserGuid;
                    newPreferenceEntity.Name = originalPreferenceEntity.Name;
                    newPreferenceEntity.Page = Resolve<IPageRepository>().GetPageByPageGuid(newPageGuid);
                    if (!originalPreferenceEntity.PageVariableReference.IsLoaded)
                    {
                        originalPreferenceEntity.PageVariableReference.Load();
                    }

                    newPreferenceEntity.PreferencesGUID = Guid.NewGuid();

                    if (!originalPreferenceEntity.ResourceReference.IsLoaded)
                    {
                        originalPreferenceEntity.ResourceReference.Load();
                    }

                    newPreferenceEntity.Resource = originalPreferenceEntity.Resource;

                    if (originalPreferenceEntity.PageVariable != null)
                    {
                        newPreferenceEntity.PageVariable = GetPageVariable(newProgramGuid, originalPreferenceEntity.PageVariable, newLanguageEntity);
                    }

                    if (Resolve<IPreferencesRepository>().GetPreference(newPreferenceEntity.PreferencesGUID) == null)
                    {
                        Resolve<IPreferencesRepository>().InsertPreference(newPreferenceEntity);
                    }
                }
            }
        }

        private ChangeTech.Entities.PageVariable GetPageVariable(Guid newProgramGuid, ChangeTech.Entities.PageVariable originalPageVariableEntity, Language newLanguageEntity)
        {
            ChangeTech.Entities.PageVariable pageVariableEntity = Resolve<IPageVaribleRepository>().GetPageVariableByProgramGuidAndParentPageVariableGuid(newProgramGuid, originalPageVariableEntity.PageVariableGUID);
            if (pageVariableEntity == null)
            {
                if (!originalPageVariableEntity.PageVariableGroupReference.IsLoaded)
                {
                    originalPageVariableEntity.PageVariableGroupReference.Load();
                }

                ChangeTech.Entities.PageVariable newPageVariableEntity = new ChangeTech.Entities.PageVariable();
                newPageVariableEntity.PageVariableGUID = Guid.NewGuid();
                newPageVariableEntity.PageVariableType = originalPageVariableEntity.PageVariableType;
                newPageVariableEntity.Description = string.Format("{0} ({1})", originalPageVariableEntity.Description, newLanguageEntity.Name);
                newPageVariableEntity.LastUpdated = DateTime.UtcNow;
                newPageVariableEntity.LastUpdatedBy = Resolve<IUserService>().GetCurrentUser().UserGuid;
                newPageVariableEntity.Name = string.Format("{0}", originalPageVariableEntity.Name);
                newPageVariableEntity.ValueType = originalPageVariableEntity.ValueType;
                //newPageVariableEntity.ParentPageVariableGUID = originalPageVariableEntity.PageVariableGUID;
                newPageVariableEntity.Program = Resolve<IProgramRepository>().GetProgramByGuid(newProgramGuid);
                if (originalPageVariableEntity.PageVariableGroup != null)
                {
                    newPageVariableEntity.PageVariableGroup = GetPageVariableGroup(newProgramGuid, originalPageVariableEntity.PageVariableGroup, newLanguageEntity);
                }
                if (Resolve<IPageVaribleRepository>().GetItem(newPageVariableEntity.PageVariableGUID) == null)
                {
                    Resolve<IPageVaribleRepository>().Add(newPageVariableEntity);
                }

                pageVariableEntity = Resolve<IPageVaribleRepository>().GetItem(newPageVariableEntity.PageVariableGUID);
            }
            return pageVariableEntity;
        }

        private PageVariableGroup GetPageVariableGroup(Guid newProgramGuid, PageVariableGroup originalPageVariableGroup, Language newLanguageEntity)
        {
            PageVariableGroup pageVariableGroupEntity = Resolve<IPageVariableGroupRepository>().GetPageVariableByProgramAndParentGroupGUID(newProgramGuid, originalPageVariableGroup.PageVariableGroupGUID);
            if (pageVariableGroupEntity == null)
            {
                PageVariableGroup newPageVariableGroupEntity = new PageVariableGroup();
                newPageVariableGroupEntity.PageVariableGroupGUID = Guid.NewGuid();
                newPageVariableGroupEntity.Program = Resolve<IProgramRepository>().GetProgramByGuid(newProgramGuid);
                //newPageVariableGroupEntity.ParentPageVariableGroupGUID = originalPageVariableGroup.PageVariableGroupGUID;
                newPageVariableGroupEntity.LastUpdatdBy = Resolve<IUserService>().GetCurrentUser().UserGuid;
                newPageVariableGroupEntity.LastUpdated = DateTime.UtcNow;
                newPageVariableGroupEntity.Name = string.Format("{0} ({1})", originalPageVariableGroup.Name, newLanguageEntity.Name);
                newPageVariableGroupEntity.Description = string.Format("{0} ({1})", originalPageVariableGroup.Description, newLanguageEntity.Name);
                Resolve<IPageVariableGroupRepository>().Insert(newPageVariableGroupEntity);
                pageVariableGroupEntity = Resolve<IPageVariableGroupRepository>().Get(newPageVariableGroupEntity.PageVariableGroupGUID);
            }
            return pageVariableGroupEntity;
        }

        private ProgramRoom GetProgramRoom(Guid newProgramGuid, ProgramRoom originalProgramRoom, Language newLanguageEntity)
        {
            ProgramRoom programRoomEntity = Resolve<IProgramRoomRepository>().GetRoomByProgramAndParent(newProgramGuid, originalProgramRoom.ProgramRoomGUID);
            if (programRoomEntity == null)
            {
                ProgramRoom newProgramRoomEntity = new ProgramRoom();
                newProgramRoomEntity.Name = string.Format("{0}", originalProgramRoom.Name);
                newProgramRoomEntity.Description = string.Format("{0} ({1})", originalProgramRoom.Description, newLanguageEntity.Name);
                //newProgramRoomEntity.ParentProgramRoomGUID = originalProgramRoom.ProgramRoomGUID;
                newProgramRoomEntity.SecondaryThemeColor = originalProgramRoom.SecondaryThemeColor;
                newProgramRoomEntity.PrimaryThemeColor = originalProgramRoom.PrimaryThemeColor;
                newProgramRoomEntity.Program = Resolve<IProgramRepository>().GetProgramByGuid(newProgramGuid);
                newProgramRoomEntity.ProgramRoomGUID = Guid.NewGuid();
                newProgramRoomEntity.LastUpdatedBy = Resolve<IUserService>().GetCurrentUser().UserGuid;
                newProgramRoomEntity.LastUpdated = DateTime.UtcNow;
                Resolve<IProgramRoomRepository>().Insert(newProgramRoomEntity);

                programRoomEntity = newProgramRoomEntity;
            }
            return programRoomEntity;
        }

        public Guid GetLanguageOfProgramBySessionGUID(Guid sessionGuid)
        {
            Session sessionEntity = Resolve<ISessionRepository>().GetSessionBySessionGuid(sessionGuid);
            if (!sessionEntity.ProgramReference.IsLoaded)
            {
                sessionEntity.ProgramReference.Load();
            }

            if (!sessionEntity.Program.LanguageReference.IsLoaded)
            {
                sessionEntity.Program.LanguageReference.Load();
            }
            return sessionEntity.Program.Language.LanguageGUID;
        }

        public Guid GetLanguageOfProgramByProgramGUID(Guid programGuid)
        {
            Program programEntity = Resolve<IProgramRepository>().GetProgramByGuid(programGuid);
            if (!programEntity.LanguageReference.IsLoaded)
            {
                programEntity.LanguageReference.Load();
            }
            return programEntity.Language.LanguageGUID;
        }

        public Guid GetDefaultProgramGUID(Guid programGUID)
        {
            Guid defaultProgramGUID = Guid.Empty;
            Program programEntity = Resolve<IProgramRepository>().GetProgramByGuid(programGUID);
            if (!programEntity.DefaultGUID.HasValue)
            {
                defaultProgramGUID = programGUID;
            }
            else
            {
                programEntity = Resolve<IProgramRepository>().GetProgramByGuid(programEntity.DefaultGUID.Value);
                defaultProgramGUID = programEntity.ProgramGUID;
            }

            return defaultProgramGUID;
        }
    }
}