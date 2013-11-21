using System.Linq;
using System.Collections.Generic;
using ChangeTech.Contracts;
using ChangeTech.IDataRepository;
using Ethos.DependencyInjection;
using ChangeTech.Entities;
using ChangeTech.Models;
using System;

namespace ChangeTech.Services
{
    public class SessionContentService: ServiceBase, ISessionContentService
    {
        #region ISessionContentService Members

        public void UpOrderNO(Guid guid)
        {
            SessionContent sessionContentLower = Resolve<ISessionContentRepository>().GetSessionContentBySessionContentGuid(guid);
            int searchOrderNO = sessionContentLower.PageSequenceOrderNo-1;
            if(!sessionContentLower.SessionReference.IsLoaded)
            {
                sessionContentLower.SessionReference.Load();
            }
            Guid searchGuid = sessionContentLower.Session.SessionGUID;
            SessionContent sessionContentUpper = Resolve<ISessionContentRepository>().GetSessionContentBySessionGuidAndOrderNO(searchGuid, searchOrderNO);
            // TODO: adjust page sequence by change PageSequenceOrderNo
            sessionContentLower.PageSequenceOrderNo--;
            Resolve<ISessionContentRepository>().UpdateSessionContent(sessionContentLower);
            sessionContentUpper.PageSequenceOrderNo++;
            Resolve<ISessionContentRepository>().UpdateSessionContent(sessionContentUpper);            
        }

        public void DownOrderNO(Guid guid)
        {
            SessionContent sessionContentUpper = Resolve<ISessionContentRepository>().GetSessionContentBySessionContentGuid(guid);
            int searchOrderNO = sessionContentUpper.PageSequenceOrderNo + 1;
            if (!sessionContentUpper.SessionReference.IsLoaded)
            {
                sessionContentUpper.SessionReference.Load();
            }
            Guid searchGuid = sessionContentUpper.Session.SessionGUID;
            SessionContent sessionContentLower = Resolve<ISessionContentRepository>().GetSessionContentBySessionGuidAndOrderNO(searchGuid, searchOrderNO);
            // TODO: adjust page sequence by change PageSequenceOrderNo 
            sessionContentUpper.PageSequenceOrderNo++;
            Resolve<ISessionContentRepository>().UpdateSessionContent(sessionContentUpper);
            sessionContentLower.PageSequenceOrderNo--;
            Resolve<ISessionContentRepository>().UpdateSessionContent(sessionContentLower);   
        }

        public SessionContent GetSessionContentByPageSeqGuidAndSessionGuid(Guid pageSeqGuid, Guid sessionGuid)
        {
            return Resolve<ISessionContentRepository>().GetSessionContentBySessionGuidPageSequenceGuid(sessionGuid, pageSeqGuid);
        }

        public void AddNewSessionContent(Guid sessionID, Guid pageSeqID, Guid RoomGuid, int pageSeqOrder)
        {
            //SessionContent sessionContent = InicialSessionContent(sessionID);
            //if (!sessionContent.PageSequenceReference.IsLoaded)
            //{
            //    sessionContent.PageSequenceReference.Load();
            //}
            //sessionContent.PageSequence = Resolve<IPageSequenceRepository>().GetPageSequenceByGuid(pageSeqID);

            //Resolve<ISessionContentRepository>().Insert(sessionContent);
            Session session = Resolve<ISessionRepository>().GetSessionBySessionGuid(sessionID);
            if (!session.SessionContent.IsLoaded)
            {
                session.SessionContent.Load();
            }
            foreach (SessionContent item in session.SessionContent)
            {
                if (item.PageSequenceOrderNo >= pageSeqOrder)
                {
                    item.PageSequenceOrderNo++;
                }
            }
            session.SessionContent.Add(
                new SessionContent
                {
                    SessionContentGUID = Guid.NewGuid(),
                    Session = Resolve<ISessionRepository>().GetSessionBySessionGuid(sessionID),
                    PageSequence = Resolve<IPageSequenceRepository>().GetPageSequenceByGuid(pageSeqID),
                    PageSequenceOrderNo = pageSeqOrder,
                    ProgramRoom = Resolve<IProgramRoomRepository>().GetRoom(RoomGuid),
                    LastUpdatedBy = Resolve<IUserService>().GetCurrentUser().UserGuid,
                });
            Resolve<ISessionRepository>().UpdateSession(session);
        }

        public void AddNewSessionContent(Guid sessionGuid, PageSequenceModel pageSeqModel)
        {
            SessionContent sessionContent = InicialSessionContent(sessionGuid);

            //TODO: new pageSequence
            PageSequence pageSeq = new PageSequence();
            pageSeq.Name = pageSeqModel.Name;
            pageSeq.PageSequenceGUID = Guid.NewGuid();
            pageSeq.Description = pageSeqModel.Description;
            pageSeq.Intervent = Resolve<IInterventRepository>().GetIntervent(pageSeqModel.InterventID);
            sessionContent.PageSequence = pageSeq;
            sessionContent.LastUpdatedBy = Resolve<IUserService>().GetCurrentUser().UserGuid;
            Resolve<ISessionContentRepository>().Insert(sessionContent);
        }

        public void AddNewSessionContentBaseOnExistPageSequence(Guid sessionGuid, Guid pageSeqGuid)
        {
            SessionContent sessionContent = InicialSessionContent(sessionGuid);

            //TODO: copy page sequence
            PageSequence pageSeq = Resolve<IPageSequenceRepository>().GetPageSequenceByGuid(pageSeqGuid);
            PageSequence newPageSeq = new PageSequence();
            newPageSeq.PageSequenceGUID = Guid.NewGuid();
            newPageSeq.Name = pageSeq.Name;
            newPageSeq.Description = pageSeq.Description;

            if(!pageSeq.InterventReference.IsLoaded)
            {
                pageSeq.InterventReference.Load();
            }
            newPageSeq.Intervent = pageSeq.Intervent;

            //TODO: copy page
            if (!pageSeq.Page.IsLoaded)
            {
                pageSeq.Page.Load();
            }
            foreach (Page page in pageSeq.Page)
            {
                Page newPage = new Page();
                newPage.PageGUID = Guid.NewGuid();
                newPage.PageOrderNo = page.PageOrderNo;
                //newPage.Name = page.Name;
                //newPage.BodyTitle = page.BodyTitle;
                //newPage.BodyText = page.BodyText;
                //newPage.ButtonPrimaryCaption = page.ButtonPrimaryCaption;
                //newPage.ButtonPrimaryAction = page.ButtonPrimaryAction;
                //newPage.ButtonSecondaryAction = page.ButtonSecondaryAction;
                //newPage.ButtonSecondaryCaption = page.ButtonSecondaryCaption;
                newPageSeq.Page.Add(newPage);
            }

            sessionContent.PageSequence = newPageSeq;
            sessionContent.LastUpdatedBy = Resolve<IUserService>().GetCurrentUser().UserGuid;
            Resolve<ISessionContentRepository>().Insert(sessionContent);
 
        }

        public void DeleteSessionContent(Guid guid)
        {
            SessionContent delSessionContent = Resolve<ISessionContentRepository>().GetSessionContentBySessionContentGuid(guid);

            AdjustSessionContent(delSessionContent);

            Resolve<ISessionContentRepository>().DeleteSessionContent(guid);
        }

        public void MakeCopySessionContent(Guid guid)
        {
            SessionContent sessionContent = Resolve<ISessionContentRepository>().GetSessionContentBySessionContentGuid(guid);
            if(!sessionContent.SessionReference.IsLoaded)
            {
                sessionContent.SessionReference.Load();
            }
            if (!sessionContent.PageSequenceReference.IsLoaded)
            {
                sessionContent.PageSequenceReference.Load();
            }
            SessionContent lastSessionContent = Resolve<ISessionContentRepository>().GetLastSessionContent(sessionContent.Session.SessionGUID);

            int newSessionContentOrderNo = 1;
            if (lastSessionContent != null)
            {
                newSessionContentOrderNo = lastSessionContent.PageSequenceOrderNo + 1;
            }
            SessionContent newSessionContent = new SessionContent();
            newSessionContent.SessionContentGUID = Guid.NewGuid();
            newSessionContent.PageSequenceOrderNo = newSessionContentOrderNo;
            newSessionContent.Session = sessionContent.Session;
            //newSessionContent.PageSequence = ServiceUtility.ClonePageSequenceNotIncludeParentGuid(sessionContent.PageSequence, new List<KeyValuePair<string, string>>());//previous is :ClonePageSequence
            CloneProgramParameterModel cloneParameterModel = new CloneProgramParameterModel
            {
                ProgramGuidOfCopiedToProgramsInDefaultLanguage = Guid.Empty,
                source = DefaultGuidSourceEnum.FromNull,
            };
            newSessionContent.PageSequence = Resolve<IPageSequenceService>().ClonePageSequence(sessionContent.PageSequence, new List<KeyValuePair<string, string>>(), cloneParameterModel);
            newSessionContent.PageSequence = Resolve<IPageSequenceService>().SetDefaultGuidForPageSequence(newSessionContent.PageSequence, cloneParameterModel);

            Resolve<ISessionContentRepository>().Insert(newSessionContent);
        }

        public void CopySessionContentWithCopyPageSequence(Guid sessionContentGuid)
        {
            SessionContent clonedSessionContent = Resolve<ISessionContentRepository>().GetSessionContentBySessionContentGuid(sessionContentGuid);

            if (!clonedSessionContent.PageSequenceReference.IsLoaded)
            {
                clonedSessionContent.PageSequenceReference.Load();
            }
            //SessionContent newSessionContent = ServiceUtility.CloneSessionContentWithNewPageSequence(clonedSessionContent);
            //clonedSessionContent.PageSequence = ServiceUtility.ClonePageSequenceNotIncludeParentGuid(clonedSessionContent.PageSequence, new List<KeyValuePair<string, string>>());//previous is:ClonePageSequence
            CloneProgramParameterModel cloneParameterModel = new CloneProgramParameterModel
            {
                ProgramGuidOfCopiedToProgramsInDefaultLanguage = Guid.Empty,
                source = DefaultGuidSourceEnum.FromNull,
            };
            clonedSessionContent.PageSequence = Resolve<IPageSequenceService>().ClonePageSequence(clonedSessionContent.PageSequence, new List<KeyValuePair<string, string>>(), cloneParameterModel);
            clonedSessionContent.PageSequence = Resolve<IPageSequenceService>().SetDefaultGuidForPageSequence(clonedSessionContent.PageSequence, cloneParameterModel);

            //Resolve<ISessionContentRepository>().DeleteSessionContent(sessionContentGuid);
            //Resolve<ISessionContentRepository>().Insert(newSessionContent);
            Resolve<ISessionContentRepository>().UpdateSessionContent(clonedSessionContent);
            //return newSessionContent.SessionContentGUID;
        }

        public int HowManySessionContentCiteThePageSequence(Guid pageSeqGuid)
        {
            return Resolve<ISessionContentRepository>().GetSessionContentByPageSeqGuid(pageSeqGuid).Count();
        }

        public Guid GetPageSequenceGuidBySessionGuidAndPagSeqOrder(Guid sessionGuid, int pageSeqOrder)
        {
            Guid pageSeqGuid = Guid.Empty;


            return pageSeqGuid;
        }

        public SessionContent CloneSessionContent(SessionContent sessionContent, List<KeyValuePair<string, string>> cloneRelapseGUIDList, Dictionary<Guid, Guid> pageDictionary, CloneProgramParameterModel cloneParameterModel)
        {
            try
            {
                if (!sessionContent.PageSequenceReference.IsLoaded)
                {
                    sessionContent.PageSequenceReference.Load();
                }
                if (!sessionContent.ProgramRoomReference.IsLoaded)
                {
                    sessionContent.ProgramRoomReference.Load();
                }
                SessionContent cloneSessionContent = new SessionContent();

                cloneSessionContent.SessionContentGUID = Guid.NewGuid();
                cloneSessionContent.ParentSessionContentGUID = sessionContent.SessionContentGUID;
                cloneSessionContent.DefaultGUID = sessionContent.DefaultGUID;
                PageSequence clonedPageSequence = Resolve<IPageSequenceService>().ClonePageSequence(sessionContent.PageSequence, cloneRelapseGUIDList, pageDictionary, cloneParameterModel);
                clonedPageSequence = Resolve<IPageSequenceService>().SetDefaultGuidForPageSequence(clonedPageSequence, cloneParameterModel);
                cloneSessionContent.PageSequence = clonedPageSequence;  //ClonePageSequence(sessionContent.PageSequence, cloneRelapseGUIDList, pageDictionary);
                cloneSessionContent.PageSequenceOrderNo = sessionContent.PageSequenceOrderNo;
                cloneSessionContent.ProgramRoom = sessionContent.ProgramRoom;


                return cloneSessionContent;
            }
            catch (Exception ex)
            {
                Ethos.Utility.LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        public SessionContent SetDefaultGuidForSessionContent(SessionContent needSetDefaultEntity, CloneProgramParameterModel cloneParameterModel)
        {
            SessionContent newEntity = new SessionContent();

            newEntity = needSetDefaultEntity;
            switch (cloneParameterModel.source)
            {
                case DefaultGuidSourceEnum.FromDefaultGuid:
                    //newEntity.DefaultGUID = needSetDefaultEntity.DefaultGUID;//This is default value, don't need assign again.
                    break;
                case DefaultGuidSourceEnum.FromMatchDefaultGuidFunction:
                    bool isMatchDefaultGuidSuccessful = false;//whether default guid can be matched, if not ,set null.
                    Guid fromSessionContentGuid = newEntity.ParentSessionContentGUID == null ? Guid.Empty : (Guid)newEntity.ParentSessionContentGUID;

                    SessionContent fromSessionContentEntity = Resolve<ISessionContentRepository>().GetSessionContentBySessionContentGuid(fromSessionContentGuid);
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
                                            SessionContent toDefaultSessionContent = Resolve<ISessionContentRepository>().GetSessionContentBySessionGuidAndOrderNO(toDefaultSession.SessionGUID, newEntity.PageSequenceOrderNo);
                                            newEntity.DefaultGUID = toDefaultSessionContent.SessionContentGUID;
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
                    newEntity.DefaultGUID = needSetDefaultEntity.ParentSessionContentGUID;
                    break;
                default:
                    throw new Exception("Error: Dont't find the DefaultGuidSource for SessionContent Entity.");
                    //break;
            }

            return newEntity;
        }
        #endregion

        #region

        private void AdjustSessionContent(SessionContent sessionContent)
        {
            if (!sessionContent.SessionReference.IsLoaded)
            {
                sessionContent.SessionReference.Load();
            }
            if (!sessionContent.Session.SessionContent.IsLoaded)
            {
                sessionContent.Session.SessionContent.Load();
            }

            foreach (SessionContent adjSessiionContent in sessionContent.Session.SessionContent)
            {
                if (adjSessiionContent.PageSequenceOrderNo > sessionContent.PageSequenceOrderNo)
                {
                    adjSessiionContent.PageSequenceOrderNo--;
                }
            }

            Resolve<ISessionRepository>().UpdateSession(sessionContent.Session);

        }

        private SessionContent InicialSessionContent(Guid sessionGuid)
        {
            SessionContent sessionContent = new SessionContent();
            sessionContent.SessionContentGUID = Guid.NewGuid();
            int orderNo = 1;

            SessionContent lastSessionContet = Resolve<ISessionContentRepository>().GetLastSessionContent(sessionGuid);
            if (lastSessionContet != null)
            {
                orderNo = lastSessionContet.PageSequenceOrderNo + 1;
            }
            sessionContent.PageSequenceOrderNo = orderNo;
            sessionContent.Session = Resolve<ISessionRepository>().GetSessionBySessionGuid(sessionGuid);

            return sessionContent;
        }

        #endregion
    }
}
