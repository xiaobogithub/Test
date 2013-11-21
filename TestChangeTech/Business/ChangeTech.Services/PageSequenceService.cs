using System.Collections.Generic;
using ChangeTech.Contracts;
using ChangeTech.Models;
using Ethos.DependencyInjection;
using System.Linq;
using ChangeTech.Entities;
using ChangeTech.IDataRepository;
using System;
using Ethos.Utility;

namespace ChangeTech.Services
{
    public class PageSequenceService : ServiceBase, IPageSequenceService
    {
        #region IPageSequenceService Members
        public PageSequenceModel GetPageSequenceBySessionGuidAndPageSeqGuid(Guid sessionGuid,Guid pageSeqGuid)
        {
            PageSequenceModel pageSequenceModel = new PageSequenceModel();
            SessionContent sessionContentEntity = Resolve<ISessionContentRepository>().GetSessionContentBySessionGuidPageSequenceGuid(sessionGuid,pageSeqGuid);
             if(!sessionContentEntity.PageSequenceReference.IsLoaded)
                {
                    sessionContentEntity.PageSequenceReference.Load();
                }

             if (sessionContentEntity.PageSequence != null)
             {
                 if (!sessionContentEntity.PageSequence.InterventReference.IsLoaded)
                 {
                     sessionContentEntity.PageSequence.InterventReference.Load();
                 }
                 if (!sessionContentEntity.PageSequence.Intervent.InterventCategoryReference.IsLoaded)
                 {
                     sessionContentEntity.PageSequence.Intervent.InterventCategoryReference.Load();
                 }
                 if (!sessionContentEntity.PageSequence.Intervent.InterventCategory.PredictorReference.IsLoaded)
                 {
                     sessionContentEntity.PageSequence.Intervent.InterventCategory.PredictorReference.Load();
                 }
                 if (!sessionContentEntity.PageSequence.Page.IsLoaded)
                 {
                     sessionContentEntity.PageSequence.Page.Load();
                 }

                 pageSequenceModel.CountOfPages = sessionContentEntity.PageSequence.Page.Where(p => p.IsDeleted != true).Count();
                 pageSequenceModel.Description = sessionContentEntity.PageSequence.Description;
                 pageSequenceModel.PageSequenceID = sessionContentEntity.PageSequence.PageSequenceGUID;
                 pageSequenceModel.Name = sessionContentEntity.PageSequence.Name;
                 pageSequenceModel.Order = sessionContentEntity.PageSequenceOrderNo;
                 pageSequenceModel.SessionContentID = sessionContentEntity.SessionContentGUID;
                 pageSequenceModel.Predictor = new KeyValuePair<Guid, string>(sessionContentEntity.PageSequence.Intervent.InterventCategory.Predictor.PredictorGUID, sessionContentEntity.PageSequence.Intervent.InterventCategory.Predictor.Name);
                 pageSequenceModel.InterventCategory = new KeyValuePair<Guid, string>(sessionContentEntity.PageSequence.Intervent.InterventCategory.InterventCategoryGUID, sessionContentEntity.PageSequence.Intervent.InterventCategory.Name);

                 if (!sessionContentEntity.ProgramRoomReference.IsLoaded)
                 {
                     sessionContentEntity.ProgramRoomReference.Load();
                 }
                 if (sessionContentEntity.ProgramRoom != null)
                 {
                     pageSequenceModel.ProgramRoom = sessionContentEntity.ProgramRoom.Name;
                 }

                 if (sessionContentEntity.PageSequence.LastUpdatedBy.HasValue)
                 {
                     pageSequenceModel.LastUpdateBy = Resolve<IUserService>().GetUserModelByUserGUID(sessionContentEntity.PageSequence.LastUpdatedBy.Value);
                 }
             }
             return pageSequenceModel;
        }

        public List<PageSequenceModel> GetPageSequenceBySessionGuid(Guid sessionGuid)
        {
            List<PageSequenceModel> pageSequences = new List<PageSequenceModel>();
            List<SessionContent> sessionContentEntities = Resolve<ISessionContentRepository>().GetSessionContentBySessionGuid(sessionGuid).ToList();
            foreach(SessionContent sessionContentEntity in sessionContentEntities)
            {
                if(!sessionContentEntity.PageSequenceReference.IsLoaded)
                {
                    sessionContentEntity.PageSequenceReference.Load();
                }

                if(sessionContentEntity.PageSequence != null)
                {
                    if(!sessionContentEntity.PageSequence.InterventReference.IsLoaded)
                    {
                        sessionContentEntity.PageSequence.InterventReference.Load();
                    }
                    if(!sessionContentEntity.PageSequence.Intervent.InterventCategoryReference.IsLoaded)
                    {
                        sessionContentEntity.PageSequence.Intervent.InterventCategoryReference.Load();
                    }
                    if(!sessionContentEntity.PageSequence.Intervent.InterventCategory.PredictorReference.IsLoaded)
                    {
                        sessionContentEntity.PageSequence.Intervent.InterventCategory.PredictorReference.Load();
                    }
                    if(!sessionContentEntity.PageSequence.Page.IsLoaded)
                    {
                        sessionContentEntity.PageSequence.Page.Load();
                    }

                    PageSequenceModel pageSequenceModel = new PageSequenceModel();
                    pageSequenceModel.CountOfPages = sessionContentEntity.PageSequence.Page.Where(p => p.IsDeleted != true).Count();
                    pageSequenceModel.Description = sessionContentEntity.PageSequence.Description;
                    pageSequenceModel.PageSequenceID = sessionContentEntity.PageSequence.PageSequenceGUID;
                    pageSequenceModel.Name = sessionContentEntity.PageSequence.Name;
                    pageSequenceModel.Order = sessionContentEntity.PageSequenceOrderNo;
                    pageSequenceModel.SessionContentID = sessionContentEntity.SessionContentGUID;
                    pageSequenceModel.Predictor = new KeyValuePair<Guid, string>(sessionContentEntity.PageSequence.Intervent.InterventCategory.Predictor.PredictorGUID, sessionContentEntity.PageSequence.Intervent.InterventCategory.Predictor.Name);
                    pageSequenceModel.InterventCategory = new KeyValuePair<Guid, string>(sessionContentEntity.PageSequence.Intervent.InterventCategory.InterventCategoryGUID, sessionContentEntity.PageSequence.Intervent.InterventCategory.Name);

                    if(!sessionContentEntity.ProgramRoomReference.IsLoaded)
                    {
                        sessionContentEntity.ProgramRoomReference.Load();
                    }
                    if(sessionContentEntity.ProgramRoom != null)
                    {
                        pageSequenceModel.ProgramRoom = sessionContentEntity.ProgramRoom.Name;
                    }

                    if(sessionContentEntity.PageSequence.LastUpdatedBy.HasValue)
                    {
                        pageSequenceModel.LastUpdateBy = Resolve<IUserService>().GetUserModelByUserGUID(sessionContentEntity.PageSequence.LastUpdatedBy.Value);
                    }
                    pageSequences.Add(pageSequenceModel);
                }
            }
            return pageSequences;
        }

        public List<PageSequenceModel> GetPageSequenceByInterventCategory(Guid interventCategoryGuid)
        {
            List<PageSequence> listPageSequence = Resolve<IPageSequenceRepository>().GetPageSequenceByInterventCategoryGuid(interventCategoryGuid);
            List<PageSequenceModel> listPageSequenceModel = new List<PageSequenceModel>();
            foreach(PageSequence pageSeq in listPageSequence)
            {
                PageSequenceModel pageSeqModel = new PageSequenceModel();
                pageSeqModel.PageSequenceID = pageSeq.PageSequenceGUID;
                pageSeqModel.Name = pageSeq.Name;
                pageSeqModel.Description = pageSeq.Description;

                List<Page> pageInSequence = Resolve<IPageRepository>().GetPagesByPageSequenceGuid(pageSeq.PageSequenceGUID);
                if(pageInSequence != null)
                {
                    pageSeqModel.CountOfPages = pageInSequence.Count;
                }

                if(!pageSeq.SessionContent.IsLoaded)
                {
                    pageSeq.SessionContent.Load();
                }

                SortedList<Guid, string> referencedProgram = new SortedList<Guid, string>();
                foreach(SessionContent sessionContent in pageSeq.SessionContent)
                {
                    if(!sessionContent.SessionReference.IsLoaded)
                    {
                        sessionContent.SessionReference.Load();
                    }
                    if(!sessionContent.Session.ProgramReference.IsLoaded)
                    {
                        sessionContent.Session.ProgramReference.Load();
                    }
                    if(!referencedProgram.ContainsKey(sessionContent.Session.Program.ProgramGUID))
                    {
                        referencedProgram.Add(sessionContent.Session.Program.ProgramGUID,
                            sessionContent.Session.Program.Name);
                    }
                }

                for(int index = 0; index < referencedProgram.Count; index++)
                {
                    Guid programGuid = referencedProgram.Keys[index];
                    pageSeqModel.UsedInProgram += referencedProgram[programGuid];
                    if(index < referencedProgram.Count - 1)
                    {
                        pageSeqModel.UsedInProgram += ", ";
                    }
                }

                listPageSequenceModel.Add(pageSeqModel);
            }

            return listPageSequenceModel;
        }

        public List<PageSequenceModel> GetPageSequenceByInterventGuid(Guid interventGuid)
        {
            List<PageSequence> listPageSequence = Resolve<IPageSequenceRepository>().GetPageSequenceByInterventGuid(interventGuid);
            List<PageSequenceModel> listPageSequenceModel = new List<PageSequenceModel>();
            foreach(PageSequence pageSeq in listPageSequence)
            {
                PageSequenceModel pageSeqModel = new PageSequenceModel();
                pageSeqModel.PageSequenceID = pageSeq.PageSequenceGUID;
                pageSeqModel.Name = pageSeq.Name;
                pageSeqModel.Description = pageSeq.Description;

                List<Page> pageInSequence = Resolve<IPageRepository>().GetPagesByPageSequenceGuid(pageSeq.PageSequenceGUID);
                if(pageInSequence != null)
                {
                    pageSeqModel.CountOfPages = pageInSequence.Count(p => (p.IsDeleted.HasValue ? p.IsDeleted == false : true));
                }

                if(!pageSeq.SessionContent.IsLoaded)
                {
                    pageSeq.SessionContent.Load();
                }
                IEnumerable<SessionContent> sessionContents = pageSeq.SessionContent.Where(s => s.IsDeleted != true);

                SortedList<Guid, string> referencedProgram = new SortedList<Guid, string>();
                foreach(SessionContent sessionContent in sessionContents)
                {
                    if(!sessionContent.SessionReference.IsLoaded)
                    {
                        sessionContent.SessionReference.Load();
                    }
                    if(!sessionContent.Session.ProgramReference.IsLoaded)
                    {
                        sessionContent.Session.ProgramReference.Load();
                    }
                    if(!referencedProgram.ContainsKey(sessionContent.Session.Program.ProgramGUID))
                    {
                        referencedProgram.Add(sessionContent.Session.Program.ProgramGUID,
                            sessionContent.Session.Program.Name);
                    }
                }

                // to see if there is a relapse referenced the page sequence
                if(!pageSeq.Relapse.IsLoaded)
                {
                    pageSeq.Relapse.Load();
                }
                foreach(Relapse rel in pageSeq.Relapse)
                {
                    if(!rel.ProgramReference.IsLoaded)
                    {
                        rel.ProgramReference.Load();
                    }

                    if(!referencedProgram.ContainsKey(rel.Program.ProgramGUID))
                    {
                        referencedProgram.Add(rel.Program.ProgramGUID,
                            rel.Program.Name);
                    }
                }

                for(int index = 0; index < referencedProgram.Count; index++)
                {
                    Guid programGuid = referencedProgram.Keys[index];
                    pageSeqModel.UsedInProgram += referencedProgram[programGuid];
                    if(index < referencedProgram.Count - 1)
                    {
                        pageSeqModel.UsedInProgram += ", ";
                    }
                }

                listPageSequenceModel.Add(pageSeqModel);
            }
            return listPageSequenceModel;
        }

        public PageSequenceModel GetRelapsePageSequenceModelBySequenceGuid(Guid sequenceGuid)
        {
            PageSequence pageSeq = Resolve<IPageSequenceRepository>().GetPageSequenceByGuid(sequenceGuid);
            PageSequenceModel pageSequenceModel = new PageSequenceModel();
            pageSequenceModel.Description = pageSeq.Description;
            pageSequenceModel.PageSequenceID = pageSeq.PageSequenceGUID;
            pageSequenceModel.Name = pageSeq.Name;
            pageSequenceModel.Order = 0;
            return pageSequenceModel;
        }

        public EditPageSequenceModel GetPageSequenceBySequenceGuid(Guid sequenceGuid)
        {
            EditPageSequenceModel editPageSeq = new EditPageSequenceModel();
            PageSequence pageSeq = Resolve<IPageSequenceRepository>().GetPageSequenceByGuid(sequenceGuid);
            List<Page> listPage = Resolve<IPageRepository>().GetPagesByPageSequenceGuid(sequenceGuid);
            editPageSeq.Name = pageSeq.Name;
            editPageSeq.Description = pageSeq.Description;
            editPageSeq.ID = pageSeq.PageSequenceGUID;
            editPageSeq.Pages = new List<SimplePageContentModel>();

            foreach(Page page in listPage)
            {
                if(!page.PageTemplateReference.IsLoaded)
                {
                    page.PageTemplateReference.Load();
                }
                SimplePageContentModel pageModel = new SimplePageContentModel();
                pageModel.ID = page.PageGUID;
                pageModel.Order = page.PageOrderNo;
                if(page.PageTemplate != null)
                {
                    pageModel.TemplateGUID = page.PageTemplate.PageTemplateGUID;
                    pageModel.TemplateName = page.PageTemplate.Name;
                }
                //TODO: Need surport multi language
                //
                PageContent pageContentEntity = Resolve<IPageContentRepository>().GetPageContentByPageGuid(page.PageGUID);
                if(pageContentEntity != null)
                {
                    pageModel.Heading = pageContentEntity.Heading;
                    if(!string.IsNullOrEmpty(pageContentEntity.Body) && pageContentEntity.Body.Length > 30)
                    {
                        pageModel.Body = pageContentEntity.Body.Substring(0, 30);

                        if(pageModel.Body.Contains('<'))
                        {
                            pageModel.Body = pageModel.Body.Substring(0, pageModel.Body.IndexOf('<') > 0 ? pageModel.Body.IndexOf('<') - 1 : pageModel.Body.IndexOf('<'));
                        }
                    }
                }
                editPageSeq.Pages.Add(pageModel);
            }

            return editPageSeq;
        }

        public EditPageSequenceModel GetPageSequenceBySessionContetnGuid(Guid sessionContentGuid)
        {
            EditPageSequenceModel EditPageSeq = new EditPageSequenceModel();
            SessionContent sessionContent = Resolve<ISessionContentRepository>().GetSessionContentBySessionContentGuid(sessionContentGuid);
            if(!sessionContent.PageSequenceReference.IsLoaded)
            {
                sessionContent.PageSequenceReference.Load();
            }
            if(!sessionContent.SessionReference.IsLoaded)
            {
                sessionContent.SessionReference.Load();
            }
            if(!sessionContent.Session.ProgramReference.IsLoaded)
            {
                sessionContent.Session.ProgramReference.Load();
            }
            List<Page> listPage = Resolve<IPageRepository>().GetPagesByPageSequenceGuid(sessionContent.PageSequence.PageSequenceGUID);
            EditPageSeq.ProgramName = sessionContent.Session.Program.Name;
            EditPageSeq.ProgramGuid = sessionContent.Session.Program.ProgramGUID;
            EditPageSeq.SessionName = sessionContent.Session.Name;
            EditPageSeq.SessionID = sessionContent.Session.SessionGUID;
            EditPageSeq.Name = sessionContent.PageSequence.Name;
            EditPageSeq.Description = sessionContent.PageSequence.Description;
            EditPageSeq.ID = sessionContent.PageSequence.PageSequenceGUID;
            EditPageSeq.Pages = new List<SimplePageContentModel>();
            foreach(Page page in listPage)
            {
                SimplePageContentModel pageModel = new SimplePageContentModel();
                pageModel.ID = page.PageGUID;
                pageModel.Order = page.PageOrderNo;
                if(!sessionContent.Session.Program.LanguageReference.IsLoaded)
                {
                    sessionContent.Session.Program.LanguageReference.Load();
                }
                //TODO: Language should be passed based on user's selection
                PageContent pageContentEntity = Resolve<IPageContentRepository>().GetPageContentByPageGuid(page.PageGUID);
                pageModel.Heading = pageContentEntity.Heading;
                pageModel.Body = pageContentEntity.Body.Length > 30 ? pageContentEntity.Body.Substring(0, 30) : pageContentEntity.Body;
                EditPageSeq.Pages.Add(pageModel);
            }

            return EditPageSeq;
        }

        public EditPageSequenceModel GetPageSequenceByProgramGuidPageSequenceGuid(Guid programGuid, Guid pageSequenceGuid)
        {
            EditPageSequenceModel EditPageSeq = new EditPageSequenceModel();
            Program program = Resolve<IProgramRepository>().GetProgramByGuid(programGuid);
            if(!program.ProgramStatusReference.IsLoaded)
            {
                program.ProgramStatusReference.Load();
            }

            PageSequence pagesequence = Resolve<IPageSequenceRepository>().GetPageSequenceByGuid(pageSequenceGuid);

            List<Page> listPage = Resolve<IPageRepository>().GetPagesByPageSequenceGuid(pageSequenceGuid);
            EditPageSeq.ProgramName = program.Name;
            EditPageSeq.ProgramGuid = program.ProgramGUID;
            EditPageSeq.ProgramStatusGuid = program.ProgramStatus.ProgramStatusGUID;
            ProgramService programService = new ProgramService();
            EditPageSeq.IsLiveProgram = programService.IsLiveProgram(program.ProgramStatus.ProgramStatusGUID);

            EditPageSeq.Name = pagesequence.Name;
            EditPageSeq.Description = pagesequence.Description;
            EditPageSeq.ID = pagesequence.PageSequenceGUID;
            EditPageSeq.Pages = new List<SimplePageContentModel>();

            foreach(Page page in listPage)
            {
                if(!page.PageTemplateReference.IsLoaded)
                {
                    page.PageTemplateReference.Load();
                }
                SimplePageContentModel pageModel = new SimplePageContentModel();
                pageModel.ID = page.PageGUID;
                pageModel.Order = page.PageOrderNo;
                if(page.PageTemplate != null)
                {
                    pageModel.TemplateGUID = page.PageTemplate.PageTemplateGUID;
                    pageModel.TemplateName = page.PageTemplate.Name;
                }

                PageContent pageContentEntity = Resolve<IPageContentRepository>().GetPageContentByPageGuid(page.PageGUID);
                if(pageContentEntity != null)
                {
                    pageModel.Heading = pageContentEntity.Heading;
                    // Body
                    if(!string.IsNullOrEmpty(pageContentEntity.Body) && pageContentEntity.Body.Length > 30)
                    {
                        pageModel.Body = pageContentEntity.Body.Substring(0, 30);

                        if(pageModel.Body.Contains('<'))
                        {
                            pageModel.Body = pageModel.Body.Substring(0, pageModel.Body.IndexOf('<') > 0 ? pageModel.Body.IndexOf('<') - 1 : pageModel.Body.IndexOf('<'));
                        }
                    }

                    //pageModel.Body = !string.IsNullOrEmpty(pageContentEntity.Body) && pageContentEntity.Body.Length > 30 ? pageContentEntity.Body.Substring(0, 30) : pageContentEntity.Body;
                    pageModel.AfterShowExpression = pageContentEntity.AfterShowExpression;
                    pageModel.BeforeShowExpression = pageContentEntity.BeforeShowExpression;
                }
                pageModel.LastUpdateBy = new UserModel();
                if(page.LastUpdatedBy.HasValue)
                {
                    pageModel.LastUpdateBy.UserName = Resolve<IUserService>().GetUserByUserGuid(page.LastUpdatedBy.Value).UserName;
                }
                EditPageSeq.Pages.Add(pageModel);
            }

            Relapse relapseEntity = Resolve<IRelapseRepository>().GetRelapseByProgramGUIDAndPageSequenceGUID(programGuid, pageSequenceGuid);
            if(relapseEntity != null)
            {
                if(!relapseEntity.ProgramRoomReference.IsLoaded)
                {
                    relapseEntity.ProgramRoomReference.Load();
                }
                if(relapseEntity.ProgramRoom != null)
                {
                    EditPageSeq.ProgramRoomGuid = relapseEntity.ProgramRoom.ProgramRoomGUID;
                }
            }

            return EditPageSeq;
        }

        public EditPageSequenceModel GetPageSequenceBySessionGuidPageSequenceGuid(Guid sessionGuid, Guid PageSequenceGuid)
        {
            EditPageSequenceModel EditPageSeq = new EditPageSequenceModel();
            SessionContent sessionContent = Resolve<ISessionContentRepository>().GetSessionContentBySessionGuidPageSequenceGuid(sessionGuid, PageSequenceGuid);
            if(!sessionContent.PageSequenceReference.IsLoaded)
            {
                sessionContent.PageSequenceReference.Load();
            }
            if(!sessionContent.SessionReference.IsLoaded)
            {
                sessionContent.SessionReference.Load();
            }
            if(!sessionContent.Session.ProgramReference.IsLoaded)
            {
                sessionContent.Session.ProgramReference.Load();
            }
            if(!sessionContent.Session.Program.ProgramStatusReference.IsLoaded)
            {
                sessionContent.Session.Program.ProgramStatusReference.Load();
            }

            List<Page> listPage = Resolve<IPageRepository>().GetPagesByPageSequenceGuid(sessionContent.PageSequence.PageSequenceGUID);
            EditPageSeq.ProgramName = sessionContent.Session.Program.Name;
            EditPageSeq.ProgramGuid = sessionContent.Session.Program.ProgramGUID;
            EditPageSeq.ProgramStatusGuid = sessionContent.Session.Program.ProgramStatus.ProgramStatusGUID;
            ProgramService programService = new ProgramService();
            EditPageSeq.IsLiveProgram = programService.IsLiveProgram(sessionContent.Session.Program.ProgramStatus.ProgramStatusGUID);
            EditPageSeq.SessionName = sessionContent.Session.Name;
            EditPageSeq.SessionID = sessionContent.Session.SessionGUID;
            EditPageSeq.Name = sessionContent.PageSequence.Name;
            EditPageSeq.Description = sessionContent.PageSequence.Description;
            EditPageSeq.ID = sessionContent.PageSequence.PageSequenceGUID;
            EditPageSeq.Pages = new List<SimplePageContentModel>();

            if(!sessionContent.ProgramRoomReference.IsLoaded)
            {
                sessionContent.ProgramRoomReference.Load();
            }
            if(sessionContent.ProgramRoom != null)
            {
                EditPageSeq.ProgramRoomGuid = sessionContent.ProgramRoom.ProgramRoomGUID;
            }

            foreach(Page page in listPage)
            {
                if(!page.PageTemplateReference.IsLoaded)
                {
                    page.PageTemplateReference.Load();
                }
                SimplePageContentModel pageModel = new SimplePageContentModel();
                pageModel.ID = page.PageGUID;
                pageModel.Order = page.PageOrderNo;
                if(page.PageTemplate != null)
                {
                    pageModel.TemplateGUID = page.PageTemplate.PageTemplateGUID;
                    pageModel.TemplateName = page.PageTemplate.Name;
                }

                PageContent pageContentEntity = Resolve<IPageContentRepository>().GetPageContentByPageGuid(page.PageGUID);
                if(pageContentEntity != null)
                {
                    pageModel.Heading = pageContentEntity.Heading;
                    // Body
                    if(!string.IsNullOrEmpty(pageContentEntity.Body) && pageContentEntity.Body.Length > 30)
                    {
                        pageModel.Body = pageContentEntity.Body.Substring(0, 30);

                        if(pageModel.Body.Contains('<'))
                        {
                            pageModel.Body = pageModel.Body.Substring(0, pageModel.Body.IndexOf('<') > 0 ? pageModel.Body.IndexOf('<') - 1 : pageModel.Body.IndexOf('<'));
                        }
                    }
                    //pageModel.Body = !string.IsNullOrEmpty(pageContentEntity.Body) && pageContentEntity.Body.Length > 30 ? pageContentEntity.Body.Substring(0, 30) : pageContentEntity.Body;
                    pageModel.AfterShowExpression = ReplacePageGUIDWithPageNOInExpression(pageContentEntity.AfterShowExpression, sessionGuid);
                    pageModel.BeforeShowExpression = ReplacePageGUIDWithPageNOInExpression(pageContentEntity.BeforeShowExpression, sessionGuid);
                }
                pageModel.LastUpdateBy = new UserModel();
                if(page.LastUpdatedBy.HasValue)
                {
                    pageModel.LastUpdateBy.UserName = Resolve<IUserService>().GetUserByUserGuid(page.LastUpdatedBy.Value).UserName;
                }
                EditPageSeq.Pages.Add(pageModel);
            }

            return EditPageSeq;
        }

        public void UpdateRelapsePageSequence(Guid programGuid, Guid pageSequenceGuid, string name, string description, Guid roomGuid)
        {
            PageSequence pageSequence = Resolve<IPageSequenceRepository>().GetPageSequenceByGuid(pageSequenceGuid);
            pageSequence.Name = name;
            pageSequence.Description = description;
            pageSequence.LastUpdatedBy = Resolve<IUserService>().GetCurrentUser().UserGuid;
            Resolve<IPageSequenceRepository>().UpdatePageSequence(pageSequence);
            Resolve<ITranslationJobService>().UpdateElementWhenFromUpdated("PageSequence", pageSequence.PageSequenceGUID.ToString(), Guid.Empty);

            Relapse relapseEntity = Resolve<IRelapseRepository>().GetRelapseByProgramGUIDAndPageSequenceGUID(programGuid, pageSequenceGuid);
            if(roomGuid != Guid.Empty)
            {
                relapseEntity.ProgramRoom = Resolve<IProgramRoomRepository>().GetRoom(roomGuid);
            }
            else
            {
                relapseEntity.ProgramRoom = null;
            }
            Resolve<IRelapseRepository>().Update(relapseEntity);
        }

        public void UpdatePageSequence(Guid pageSequenceGuid, Guid sessionGuid, Guid roomGuid, string name, string description)
        {
            PageSequence pageSequence = Resolve<IPageSequenceRepository>().GetPageSequenceByGuid(pageSequenceGuid);
            pageSequence.Name = name;
            pageSequence.Description = description;
            pageSequence.LastUpdatedBy = Resolve<IUserService>().GetCurrentUser().UserGuid;
            Resolve<IPageSequenceRepository>().UpdatePageSequence(pageSequence);
            Resolve<ITranslationJobService>().UpdateElementWhenFromUpdated("PageSequence", pageSequence.PageSequenceGUID.ToString(), Guid.Empty);

            SessionContent sc = Resolve<ISessionContentRepository>().GetSessionContentBySessionGuidPageSequenceGuid(sessionGuid, pageSequenceGuid);
            sc.ProgramRoom = Resolve<IProgramRoomRepository>().GetRoom(roomGuid);
            sc.LastUpdatedBy = Resolve<IUserService>().GetCurrentUser().UserGuid;
            Resolve<ISessionContentRepository>().UpdateSessionContent(sc);
        }

        public List<PageSequenceUsedInfo> GetPageSequenceUsedInfo(Guid sessionContentGuid)
        {
            SessionContent sessionContent = Resolve<ISessionContentRepository>().GetSessionContentBySessionContentGuid(sessionContentGuid);
            if(!sessionContent.PageSequenceReference.IsLoaded)
            {
                sessionContent.PageSequenceReference.Load();
            }
            List<PageSequenceUsedInfo> psUsedInfos = new List<PageSequenceUsedInfo>();
            List<SessionContent> usedSessionContent = Resolve<ISessionContentRepository>().GetSessionContentByPageSeqGuid(sessionContent.PageSequence.PageSequenceGUID).ToList<SessionContent>();
            foreach(SessionContent sc in usedSessionContent)
            {
                if(!sc.SessionReference.IsLoaded)
                {
                    sc.SessionReference.Load();
                }

                if(!sc.Session.ProgramReference.IsLoaded)
                {
                    sc.Session.ProgramReference.Load();
                }
                PageSequenceUsedInfo psUsedInfo = new PageSequenceUsedInfo();
                psUsedInfo.ProgramID = sc.Session.Program.ProgramGUID;
                psUsedInfo.SessionContentID = sc.SessionContentGUID;
                psUsedInfo.SessionID = sc.Session.SessionGUID;

                psUsedInfos.Add(psUsedInfo);

            }

            return psUsedInfos;
        }

        public bool PageSequenceReferenced(Guid pageSeqGuid)
        {
            bool flag = false;
            List<PageSequenceUsedInfo> list = GetUseInfoList(pageSeqGuid);
            if(list.Count > 0)
            {
                flag = true;
            }

            return flag;
        }

        public bool PageSequenceInMoreSession(Guid currentSession, Guid pageSeqGuid)//why not judge by the list count of pageseq in session content table
        {
            bool flag = false;
            Session session = Resolve<ISessionRepository>().GetSessionBySessionGuid(currentSession);
            if(!session.ProgramReference.IsLoaded)
            {
                session.ProgramReference.Load();
            }
            Guid currentProgram = session.Program.ProgramGUID;
            List<PageSequenceUsedInfo> list = GetUseInfoList(pageSeqGuid);
            foreach(PageSequenceUsedInfo psui in list)
            {//assert: There is no pageSequence in different program.
                if(psui.SessionID != currentSession && psui.ProgramID == currentProgram)
                {
                    flag = true;
                }
            }
            return flag;
        }

        public void BeforeEditPageSequenceOnly(Guid pageSeqGuid, bool affectFlag)
        {
            List<PageSequenceUsedInfo> list = GetUseInfoList(pageSeqGuid);
            if(list.Count > 0)
            {
                if(!affectFlag)
                {
                    PageSequence pageSeq = Resolve<IPageSequenceRepository>().GetPageSequenceByGuid(pageSeqGuid);
                    //PageSequence ps = ServiceUtility.ClonePageSequenceNotIncludeParentGuid(pageSeq, new List<KeyValuePair<string, string>>());//previous is:ClonePageSequence
                    CloneProgramParameterModel cloneParameterModel = new CloneProgramParameterModel
                    {
                        ProgramGuidOfCopiedToProgramsInDefaultLanguage = Guid.Empty,
                        source = DefaultGuidSourceEnum.FromNull,
                    };
                    PageSequence ps = Resolve<IPageSequenceService>().ClonePageSequence(pageSeq, new List<KeyValuePair<string, string>>(), cloneParameterModel);
                    ps = Resolve<IPageSequenceService>().SetDefaultGuidForPageSequence(ps, cloneParameterModel);


                    foreach(PageSequenceUsedInfo psui in list)
                    {
                        SessionContent updatesc = Resolve<ISessionContentRepository>().GetSessionContentBySessionContentGuid(psui.SessionContentID);
                        updatesc.PageSequence = ps;

                        Resolve<ISessionContentRepository>().UpdateSessionContent(updatesc);
                    }
                }
            }
        }

        public Guid BeforeEditPageSequenceAfterRefactoring(Guid sessionGuid, Guid pageSeqGuid, bool affectFlag)
        {
            bool IsCloned = false;
            PageSequence newPageSequence = new PageSequence();
            Session session = Resolve<ISessionRepository>().GetSessionBySessionGuid(sessionGuid);
            if(!session.ProgramReference.IsLoaded)
            {
                session.ProgramReference.Load();
            }
            Guid programGuid = session.Program.ProgramGUID;
            List<PageSequenceUsedInfo> list = GetUseInfoList(pageSeqGuid);

            if(list.Count > 1)
            {
                foreach(PageSequenceUsedInfo paui in list)
                {// for there is one pagesequence in different program
                    if(paui.SessionID != sessionGuid)
                    {
                        PageSequence currentPageSequence = Resolve<IPageSequenceRepository>().GetPageSequenceByGuid(pageSeqGuid);
                        //newPageSequence = ServiceUtility.ClonePageSequenceNotIncludeParentGuid(currentPageSequence, new List<KeyValuePair<string, string>>());//previous is ClonePageSequence
                        CloneProgramParameterModel cloneParameterModel = new CloneProgramParameterModel
                        {
                            ProgramGuidOfCopiedToProgramsInDefaultLanguage = Guid.Empty,
                            source = DefaultGuidSourceEnum.FromNull,
                        };
                        newPageSequence = Resolve<IPageSequenceService>().ClonePageSequence(currentPageSequence, new List<KeyValuePair<string, string>>(), cloneParameterModel);
                        newPageSequence = Resolve<IPageSequenceService>().SetDefaultGuidForPageSequence(newPageSequence, cloneParameterModel);

                        Resolve<IPageSequenceRepository>().InstertPageSequence(newPageSequence);
                        IsCloned = true;
                        break;
                    }
                }

                if(!affectFlag)//not want to imfact others but the count >1
                {
                    if(!IsCloned)//the >1 count in the same session
                    {
                        foreach(PageSequenceUsedInfo paui in list)
                        {//There is one pagesequence in difference but in one program
                            if(paui.SessionID != sessionGuid && paui.ProgramID == programGuid)
                            {
                                PageSequence currentPageSequence = Resolve<IPageSequenceRepository>().GetPageSequenceByGuid(pageSeqGuid);
                                //newPageSequence = ServiceUtility.ClonePageSequenceNotIncludeParentGuid(currentPageSequence, new List<KeyValuePair<string, string>>());//previous is: ClonePageSequence
                                CloneProgramParameterModel cloneParameterModel = new CloneProgramParameterModel
                                {
                                    ProgramGuidOfCopiedToProgramsInDefaultLanguage = Guid.Empty,
                                    source = DefaultGuidSourceEnum.FromNull,
                                };
                                newPageSequence = Resolve<IPageSequenceService>().ClonePageSequence(currentPageSequence, new List<KeyValuePair<string, string>>(), cloneParameterModel);
                                newPageSequence = Resolve<IPageSequenceService>().SetDefaultGuidForPageSequence(newPageSequence, cloneParameterModel);

                                Resolve<IPageSequenceRepository>().InstertPageSequence(newPageSequence);
                                IsCloned = true;
                                break;
                            }
                        }
                    }
                }
                else//has more than one ps in different ses   and  imfact all others
                {
                    if(IsCloned)
                    {
                        foreach(PageSequenceUsedInfo paui in list)
                        {//There is one pagesequence in difference but in one program
                            if(paui.SessionID != sessionGuid && paui.ProgramID == programGuid)
                            {
                                SessionContent updatesc = Resolve<ISessionContentRepository>().GetSessionContentBySessionContentGuid(paui.SessionContentID);
                                updatesc.PageSequence = Resolve<IPageSequenceRepository>().GetPageSequenceByGuid(newPageSequence.PageSequenceGUID);

                                Resolve<ISessionContentRepository>().UpdateSessionContent(updatesc);
                            }
                        }
                    }
                }

                if(IsCloned)
                {
                    foreach(PageSequenceUsedInfo paui in list)
                    {//There are more than one pagesequence in same session and same program
                        if(paui.SessionID == sessionGuid && paui.ProgramID == programGuid)
                        {
                            SessionContent updatesc = Resolve<ISessionContentRepository>().GetSessionContentBySessionContentGuid(paui.SessionContentID);
                            updatesc.PageSequence = newPageSequence;

                            Resolve<ISessionContentRepository>().UpdateSessionContent(updatesc);
                        }
                    }
                }
            }

            Guid returnGuid = Guid.Empty;
            if(IsCloned)
            {
                returnGuid = newPageSequence.PageSequenceGUID;
            }
            else
            {
                returnGuid = pageSeqGuid;
            }

            return returnGuid;
        }

        public string GetPreviewPageSequenceModelAsXML(Guid languageGuid, Guid sessionGuid, Guid pageSequenceGuid, Guid userGuid)
        {
            return Resolve<IStoreProcedure>().GetPreviewPageSequenceModelAsXMl(languageGuid, sessionGuid, pageSequenceGuid, userGuid);
        }

        public string GetRelapseModelAsXML(Guid languageGuid, Guid programGuid, Guid pageSequenceGuid, Guid userGuid)
        {
            return Resolve<IStoreProcedure>().GetRelapseModelAsXML(languageGuid, programGuid, pageSequenceGuid, userGuid);
        }

        public string GetRelapsePreviewModelAsXML(Guid languageGuid, Guid programGuid, Guid pageSequenceGuid, Guid userGuid)
        {
            return Resolve<IStoreProcedure>().GetRelapsePreviewModelAsXML(languageGuid, programGuid, pageSequenceGuid, userGuid);
        }

        public string GetTempPreviewPageSequenceModelAsXML(Guid languageGuid, Guid sessionGuid, Guid pageSequenceGuid, Guid userGuid)
        {
            return Resolve<IStoreProcedure>().GetTempPreviewPageSequenceModelAsXMl(languageGuid, sessionGuid, pageSequenceGuid, userGuid);
        }

        public void AddPageSequence(string name, string description, Guid interventGuid)
        {
            PageSequence newPageSequence = new PageSequence();
            newPageSequence.Name = name;
            newPageSequence.Description = description;
            newPageSequence.Intervent = Resolve<IInterventRepository>().GetIntervent(interventGuid);
            newPageSequence.PageSequenceGUID = Guid.NewGuid();
            newPageSequence.LastUpdatedBy = Resolve<IUserService>().GetCurrentUser().UserGuid;
            Resolve<IPageSequenceRepository>().InstertPageSequence(newPageSequence);
        }

        public bool DeletePageSequence(Guid pageSequenceGuid)
        {
            bool flag = false;
            // update by DIFUJIE 2010-07-12
            //ISessionContentRepository scRepository = Resolve<ISessionContentRepository>();
            //IQueryable<SessionContent> sessionList = scRepository.GetSessionContentByPageSeqGuid(pageSequenceGuid);
            if(!IsReferencedByProgram(pageSequenceGuid))
            {
                List<Page> list = Resolve<IPageRepository>().GetPagesByPageSequenceGuid(pageSequenceGuid);
                foreach(Page page in list)
                {
                    Resolve<IPageService>().DeletePage(page.PageGUID);
                }
                Resolve<IPageSequenceRepository>().DeletePageSequence(pageSequenceGuid);
                flag = true;
            }
            return flag;
        }

        public int GetCountOfPagesInPageSequence(Guid sequenceGuid)
        {
            List<Page> pages = Resolve<IPageRepository>().GetPagesByPageSequenceGuid(sequenceGuid);
            return pages.Count;
        }

        public void UseExistedPageSequence(Guid sessionGuid, Guid sequenceGuid, Guid roomGuid, int order)
        {
            Session sessionEntity = Resolve<ISessionRepository>().GetSessionBySessionGuid(sessionGuid);
            if(!sessionEntity.ProgramReference.IsLoaded)
            {
                sessionEntity.ProgramReference.Load();
            }

            PageSequence pageSeq = Resolve<IPageSequenceRepository>().GetPageSequenceByGuid(sequenceGuid);

            if(!IsReferencedBySession(sessionEntity.Program.ProgramGUID, pageSeq))
            {
                //pageSeq = ServiceUtility.ClonePageSequenceNotIncludeParentGuid(pageSeq, new List<KeyValuePair<string, string>>());// previous is:ClonePageSequence
                CloneProgramParameterModel cloneParameterModel = new CloneProgramParameterModel
                {
                    ProgramGuidOfCopiedToProgramsInDefaultLanguage = Guid.Empty,
                    source = DefaultGuidSourceEnum.FromNull,
                };
                pageSeq = Resolve<IPageSequenceService>().ClonePageSequence(pageSeq, new List<KeyValuePair<string, string>>(), cloneParameterModel);
                pageSeq = Resolve<IPageSequenceService>().SetDefaultGuidForPageSequence(pageSeq, cloneParameterModel);

            }

            if(!sessionEntity.SessionContent.IsLoaded)
            {
                sessionEntity.SessionContent.Load();
            }
            foreach(SessionContent item in sessionEntity.SessionContent)
            {
                if(item.PageSequenceOrderNo >= order)
                {
                    item.PageSequenceOrderNo++;
                }
            }

            //Copy used pagevariable to the current program
            List<Page>pagesByPageSeq = Resolve<IPageRepository>().GetPagesByPageSequenceGuid(pageSeq.PageSequenceGUID);
            foreach (var page in pagesByPageSeq)
            {
                if (!page.PageVariableReference.IsLoaded) page.PageVariableReference.Load();
                if (page.PageVariable != null)
                {
                    PageVariable pvEntity=page.PageVariable;
                    if (!pvEntity.PageVariableGroupReference.IsLoaded) pvEntity.PageVariableGroupReference.Load();
                    if (!sessionEntity.ProgramReference.IsLoaded) sessionEntity.ProgramReference.Load();
                    PageVariableModel pvModel = new PageVariableModel();
                    pvModel.PageVariableGUID = Guid.NewGuid();
                    if (pvEntity.PageVariableGroup != null)
                    {
                        pvModel.PageVariableGroupGUID = pvEntity.PageVariableGroup.PageVariableGroupGUID;
                    }
                    if (sessionEntity.Program != null)
                    {
                        pvModel.ProgramGUID = sessionEntity.Program.ProgramGUID;
                    }
                    pvModel.PageVariableType = pvEntity.PageVariableType;
                    pvModel.Name = pvEntity.Name;
                    pvModel.Description = pvEntity.Description;
                    pvModel.ValueType = pvEntity.ValueType;
                    Resolve<IPageVariableService>().Add(pvModel);
                }
            }

            sessionEntity.SessionContent.Add(
                new SessionContent
                {
                    SessionContentGUID = Guid.NewGuid(),
                    PageSequence = pageSeq,
                    PageSequenceOrderNo = order,
                    ProgramRoom = Resolve<IProgramRoomRepository>().GetRoom(roomGuid),
                    LastUpdatedBy = Resolve<IUserService>().GetCurrentUser().UserGuid,
                });
            Resolve<ISessionRepository>().UpdateSession(sessionEntity);
            Resolve<ITranslationJobService>().UpdateElementWhenFromUpdated("Session", sessionEntity.SessionGUID.ToString(), Guid.Empty);
        }

        public void MovePageSequenceToAnotherIntervent(Guid sequenceGuid, Guid interventGuid)
        {
            PageSequence sequence = Resolve<IPageSequenceRepository>().GetPageSequenceByGuid(sequenceGuid);
            sequence.Intervent = Resolve<IInterventRepository>().GetIntervent(interventGuid);
            Resolve<IPageSequenceRepository>().UpdatePageSequence(sequence);
        }

        public void MovePageSequenceToAnotherIntervent(string[] sequences, Guid interventGuid)
        {
            foreach(string str in sequences)
            {
                MovePageSequenceToAnotherIntervent(new Guid(str), interventGuid);
            }
        }

        public bool IsReferencedByProgram(Guid sequenceGuid)
        {
            bool flug = false;
            if(Resolve<ISessionContentRepository>().GetSessionContentCoutButPageSequenceGUID(sequenceGuid) > 0)
            {
                flug = true;
            }

            if(flug == false)
            {
                if(Resolve<IRelapseRepository>().GetRelapseCountByPageSequenceGUID(sequenceGuid) > 0)
                {
                    flug = true;
                }
            }

            return flug;
        }

        public PageSequence ClonePageSequence(PageSequence pageSeq, List<KeyValuePair<string, string>> cloneRelapseGUIDList, Dictionary<Guid, Guid> pageDictionary, CloneProgramParameterModel cloneParameterModel)
        {
            try
            {
                if (!pageSeq.InterventReference.IsLoaded)
                {
                    pageSeq.InterventReference.Load();
                }
                if (!pageSeq.Page.IsLoaded)
                {
                    pageSeq.Page.Load();
                }
                PageSequence newPageSeq = new PageSequence();
                newPageSeq.PageSequenceGUID = Guid.NewGuid();
                newPageSeq.Name = string.Format("Copy of {0} on {1}", pageSeq.Name, DateTime.UtcNow.ToString());
                newPageSeq.Description = pageSeq.Description;
                newPageSeq.Intervent = pageSeq.Intervent;
                newPageSeq.IsDeleted = false;
                newPageSeq.ParentPageSequenceGUID = pageSeq.PageSequenceGUID;
                newPageSeq.DefaultGUID = pageSeq.DefaultGUID;

                List<Page> pages = pageSeq.Page.Where(p => p.IsDeleted != true).ToList();
                foreach (Page page in pages)
                {
                    Page newPage = Resolve<IPageService>().ClonePage(page, cloneRelapseGUIDList, cloneParameterModel); //ClonePage(page, cloneRelapseGUIDList);
                    newPage = Resolve<IPageService>().SetDefaultGuidForPage(newPage, cloneParameterModel);
                    newPageSeq.Page.Add(newPage);
                    if (!pageDictionary.ContainsKey(page.PageGUID))
                        pageDictionary.Add(page.PageGUID, newPage.PageGUID);
                }
                return newPageSeq;
            }
            catch (Exception ex)
            {
                Ethos.Utility.LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        public PageSequence ClonePageSequence(PageSequence pageSeq, List<KeyValuePair<string, string>> cloneRelapseGUIDList, CloneProgramParameterModel cloneParameterModel)
        {
            try
            {
                if (!pageSeq.InterventReference.IsLoaded)
                {
                    pageSeq.InterventReference.Load();
                }
                if (!pageSeq.Page.IsLoaded)
                {
                    pageSeq.Page.Load();
                }
                PageSequence newPageSeq = new PageSequence();
                newPageSeq.PageSequenceGUID = Guid.NewGuid();
                newPageSeq.Name = string.Format("Copy of {0} on {1}", pageSeq.Name, DateTime.UtcNow.ToString());
                newPageSeq.Description = pageSeq.Description;
                newPageSeq.Intervent = pageSeq.Intervent;
                newPageSeq.IsDeleted = false;
                newPageSeq.ParentPageSequenceGUID = pageSeq.PageSequenceGUID;
                newPageSeq.DefaultGUID = pageSeq.DefaultGUID;
                newPageSeq.IsDeleted = pageSeq.IsDeleted;

                List<Page> pages = pageSeq.Page.Where(p => p.IsDeleted != true).ToList();
                foreach (Page page in pages)
                {
                    Page newPage = Resolve<IPageService>().ClonePage(page, cloneRelapseGUIDList, cloneParameterModel); //ClonePage(page, cloneRelapseGUIDList);
                    newPage = Resolve<IPageService>().SetDefaultGuidForPage(newPage, cloneParameterModel);

                    newPageSeq.Page.Add(newPage);
                }
                return newPageSeq;
            }
            catch (Exception ex)
            {
                //add log.
                InsertLogModel insertLogModel = new InsertLogModel
                {
                    ActivityLogType = (int)LogTypeEnum.AddProgram,
                    Browser = string.Empty,
                    From = string.Empty,
                    IP = string.Empty,
                    Message = string.Format("ClonePageSequenceException:{0}", ex),
                    PageGuid = Guid.Empty,
                    PageSequenceGuid = Guid.Empty,
                    SessionGuid = Guid.Empty,
                    ProgramGuid = Guid.Empty,
                    UserGuid = Guid.Empty,
                };
                Resolve<IActivityLogService>().Insert(insertLogModel);
                Ethos.Utility.LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        public PageSequence SetDefaultGuidForPageSequence(PageSequence needSetDefaultEntity, CloneProgramParameterModel cloneParameterModel)
        {
            PageSequence newEntity = new PageSequence();

            newEntity = needSetDefaultEntity;
            switch (cloneParameterModel.source)
            {
                case DefaultGuidSourceEnum.FromDefaultGuid:
                    //newEntity.DefaultGUID = needSetDefaultEntity.DefaultGUID;//This is default value, don't need assign again.
                    break;
                case DefaultGuidSourceEnum.FromMatchDefaultGuidFunction:
                    bool isMatchDefaultGuidSuccessful = false;//whether default guid can be matched, if not ,set null.
                    Guid fromPageSequenceGuid = newEntity.ParentPageSequenceGUID == null ? Guid.Empty : (Guid)newEntity.ParentPageSequenceGUID;

                    SessionContent fromSessionContentEntity =Resolve<ISessionContentRepository>().GetSessionContentByPageSeqGuid(fromPageSequenceGuid).FirstOrDefault();//FirstOrDefault has some problems if more than one session content has the same pagesequence.
                    if (fromSessionContentEntity != null)
                    {
                        if (!fromSessionContentEntity.SessionReference.IsLoaded) fromSessionContentEntity.SessionReference.Load();

                        Guid fromSessionGuid = fromSessionContentEntity.Session == null ? Guid.Empty : fromSessionContentEntity.Session.SessionGUID; //newEntity.ParentSessionGUID == null ? Guid.Empty : (Guid)newEntity.ParentSessionGUID;
                        if (fromSessionGuid != Guid.Empty)//Has parent guid. 
                        {
                            Session fromSessionEntity = Resolve<ISessionRepository>().GetSessionBySessionGuid(fromSessionGuid);
                            if (fromSessionEntity != null)
                            {
                                int? dayNum = fromSessionEntity.Day;//if dayNum == null, can't map.
                                if (!fromSessionEntity.ProgramReference.IsLoaded) fromSessionEntity.ProgramReference.Load();
                                Program fromProgramInDefaultLanguage = new Program();
                                if (fromSessionEntity.Program.DefaultGUID.HasValue)//This fromProgram is not default program
                                {
                                    fromProgramInDefaultLanguage = Resolve<IProgramRepository>().GetProgramByGuid((Guid)fromSessionEntity.Program.DefaultGUID);
                                }
                                else//This fromProgram is default program. Or this is wrong data for old Data Version or some other reason.
                                {
                                    fromProgramInDefaultLanguage = Resolve<IProgramRepository>().GetProgramByGuid(fromSessionEntity.Program.ProgramGUID);
                                }
                                if (fromProgramInDefaultLanguage != null)
                                {
                                    Program toProgramInDefaultLanguage = Resolve<IProgramRepository>().GetProgramByGuid(cloneParameterModel.ProgramGuidOfCopiedToProgramsInDefaultLanguage);
                                    if (toProgramInDefaultLanguage != null)
                                    {
                                        if (toProgramInDefaultLanguage.ParentProgramGUID == fromProgramInDefaultLanguage.ProgramGUID)//Match Successful. the toProgram's parentguid == fromDefaultProgram's guid
                                        {
                                            isMatchDefaultGuidSuccessful = true;
                                        }
                                        else
                                        {
                                            List<Program> fromProgramMatchedList = Resolve<IProgramRepository>().GetProgramByDefaultGUID(fromProgramInDefaultLanguage.ProgramGUID).Where(p => p.ProgramGUID == toProgramInDefaultLanguage.ParentProgramGUID).ToList();
                                            if (fromProgramMatchedList.Count > 0)//Match Successful. the toProgram's parent guid is fromProgram's guid which program belongs to the fromDefaultProgram but not the default language.
                                            {
                                                isMatchDefaultGuidSuccessful = true;
                                            }
                                        }
                                    }

                                    //Set Default Guid if match successful
                                    if (isMatchDefaultGuidSuccessful)
                                    {
                                        try
                                        {
                                            Session toDefaultSession = Resolve<ISessionRepository>().GetSessionByProgramGuid(toProgramInDefaultLanguage.ProgramGUID).Where(s => s.Day == dayNum).FirstOrDefault();
                                            SessionContent toDefaultSessionContent = Resolve<ISessionContentRepository>().GetSessionContentBySessionGuidAndOrderNO(toDefaultSession.SessionGUID, fromSessionContentEntity.PageSequenceOrderNo);
                                            if (!toDefaultSessionContent.PageSequenceReference.IsLoaded) toDefaultSessionContent.PageSequenceReference.Load();
                                            newEntity.DefaultGUID = toDefaultSessionContent.PageSequence.PageSequenceGUID;
                                        }
                                        catch (Exception ex)
                                        {
                                            Ethos.Utility.LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                                            isMatchDefaultGuidSuccessful = false;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    //else//don't has parent guid ,so can't match,set the default guid =>null
                    //{
                    //    newEntity.DefaultGUID = null;
                    //}


                    //Can't match. Set default guid =>null.
                    if (!isMatchDefaultGuidSuccessful)
                    {
                        newEntity.DefaultGUID = null;
                    }
                    break;
                case DefaultGuidSourceEnum.FromNull:
                    newEntity.DefaultGUID = null;
                    break;
                case DefaultGuidSourceEnum.FromPrimaryKey:
                    newEntity.DefaultGUID = needSetDefaultEntity.ParentPageSequenceGUID;
                    break;
                default:
                    throw new Exception("Error: Dont't find the DefaultGuidSource for PageSequence Entity.");
                    //break;
            }

            return newEntity;
        }
        #endregion

        #region Private methods

        private bool IsReferencedBySession(Guid guid, PageSequence pageSeq)
        {
            bool flug = true;
            if(!pageSeq.SessionContent.IsLoaded)
            {
                pageSeq.SessionContent.Load();
            }
            IEnumerable<SessionContent> sessionContents = pageSeq.SessionContent.Where(s => s.IsDeleted != true);
            foreach(SessionContent sc in sessionContents)
            {
                if(!sc.SessionReference.IsLoaded)
                {
                    sc.SessionReference.Load();
                }
                //if (!sc.Session.ProgramReference.IsLoaded)
                //{
                //    sc.Session.ProgramReference.Load();
                //}
                if(sc.Session.SessionGUID != guid)
                {
                    flug = false;
                    break;
                }
            }

            if(flug == true)
            {
                if(!pageSeq.Relapse.IsLoaded)
                {
                    pageSeq.Relapse.Load();
                }
                if(pageSeq.Relapse.Count > 0)
                {
                    flug = false;
                }
            }

            return flug;
        }

        private bool IsReferencedByProgram(Guid guid, PageSequence pageSeq)
        {
            bool flug = true;
            if(!pageSeq.SessionContent.IsLoaded)
            {
                pageSeq.SessionContent.Load();
            }
            IEnumerable<SessionContent> sessionContents = pageSeq.SessionContent.Where(s => s.IsDeleted != true);
            foreach(SessionContent sc in sessionContents)
            {
                if(!sc.SessionReference.IsLoaded)
                {
                    sc.SessionReference.Load();
                }
                if(!sc.Session.ProgramReference.IsLoaded)
                {
                    sc.Session.ProgramReference.Load();
                }
                if(sc.Session.Program.ProgramGUID != guid)
                {
                    flug = false;
                    break;
                }
            }

            if(flug == true)
            {
                if(!pageSeq.Relapse.IsLoaded)
                {
                    pageSeq.Relapse.Load();
                }
                if(pageSeq.Relapse.Count > 0)
                {
                    flug = false;
                }
            }

            return flug;
        }

        private List<PageSequenceUsedInfo> GetUseInfoList(Guid pageSeqGuid)
        {
            List<PageSequenceUsedInfo> psUsedInfos = new List<PageSequenceUsedInfo>();
            List<SessionContent> usedSessionContent = Resolve<ISessionContentRepository>().GetSessionContentByPageSeqGuid(pageSeqGuid).ToList<SessionContent>();
            foreach(SessionContent sc in usedSessionContent)
            {
                if(!sc.SessionReference.IsLoaded)
                {
                    sc.SessionReference.Load();
                }

                if(!sc.Session.ProgramReference.IsLoaded)
                {
                    sc.Session.ProgramReference.Load();
                }
                PageSequenceUsedInfo psUsedInfo = new PageSequenceUsedInfo();
                psUsedInfo.ProgramID = sc.Session.Program.ProgramGUID;
                psUsedInfo.SessionContentID = sc.SessionContentGUID;
                psUsedInfo.SessionID = sc.Session.SessionGUID;

                psUsedInfos.Add(psUsedInfo);
            }

            return psUsedInfos;

        }

        private string ReplacePageGUIDWithPageNOInExpression(string expression, Guid sessionGUID)
        {
            if(!string.IsNullOrEmpty(expression))
            {
                string markupPrefix = "{Page:";

                int pageGUIDIndex = 0;
                do
                {
                    pageGUIDIndex = expression.IndexOf(markupPrefix);
                    if(pageGUIDIndex > 0)
                    {
                        string pageGUID = expression.Substring(pageGUIDIndex + markupPrefix.Length, 36);

                        Page pageEntity = Resolve<IPageRepository>().GetPageByPageGuid(new Guid(pageGUID));
                        if(!pageEntity.PageSequenceReference.IsLoaded)
                        {
                            pageEntity.PageSequenceReference.Load();
                        }

                        SessionContent sessionContentEntity = Resolve<ISessionContentRepository>().GetSessionContentBySessionGuidPageSequenceGuid(sessionGUID, pageEntity.PageSequence.PageSequenceGUID);
                        if(sessionContentEntity != null)
                        {
                            expression = expression.Replace(markupPrefix + pageGUID + "}", "[Page:" + sessionContentEntity.PageSequenceOrderNo + "." + pageEntity.PageOrderNo + "]");
                        }
                        else
                        {
                            pageGUIDIndex = 0;
                        }
                    }
                } while(pageGUIDIndex > 0);
            }
            return expression;
        }

        #endregion
    }
}
