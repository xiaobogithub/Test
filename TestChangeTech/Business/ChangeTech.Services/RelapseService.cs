using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ethos.DependencyInjection;
using ChangeTech.Contracts;
using ChangeTech.Entities;
using ChangeTech.IDataRepository;
using ChangeTech.Models;

namespace ChangeTech.Services
{
    public class RelapseService : ServiceBase, IRelapseService
    {
        #region IRelapseService Members

        public void AddRelapseForProgram(Guid programGuid, Guid sequenceGuid, Guid programRoomGuid)
        {
            PageSequence sequence = Resolve<IPageSequenceRepository>().GetPageSequenceByGuid(sequenceGuid);
            if (Resolve<IPageSequenceService>().IsReferencedByProgram(sequenceGuid))
            {
                //sequence = ServiceUtility.ClonePageSequenceNotIncludeParentGuid(sequence, new List<KeyValuePair<string, string>>());//previous is :ClonePageSequence
                CloneProgramParameterModel cloneParameterModel = new CloneProgramParameterModel
                {
                    ProgramGuidOfCopiedToProgramsInDefaultLanguage = Guid.Empty,
                    source = DefaultGuidSourceEnum.FromNull,
                };
                sequence = Resolve<IPageSequenceService>().ClonePageSequence(sequence, new List<KeyValuePair<string, string>>(), cloneParameterModel);
                sequence = Resolve<IPageSequenceService>().SetDefaultGuidForPageSequence(sequence, cloneParameterModel);
            }

            Relapse relapse = new Relapse
            {
                RelapseGUID = Guid.NewGuid(),
                PageSequence = sequence,
                Program = Resolve<IProgramRepository>().GetProgramByGuid(programGuid)
            };

            if (programGuid != Guid.Empty)
            {
                relapse.ProgramRoom = Resolve<IProgramRoomRepository>().GetRoom(programRoomGuid);
            }
            Resolve<IRelapseRepository>().Add(relapse);
        }

        public void DeleteRelapse(Guid relapseGUID)
        {
            Resolve<IRelapseRepository>().Delete(relapseGUID);
        }

        public List<RelapseModel> GetRelapseModelList(Guid programGUID)
        {
            List<RelapseModel> modellist = new List<RelapseModel>();
            List<Relapse> relapseList = Resolve<IRelapseRepository>().GetRelapseList(programGUID);
            foreach (Relapse rel in relapseList)
            {
                if (!rel.PageSequenceReference.IsLoaded)
                {
                    rel.PageSequenceReference.Load();
                }
                if (!rel.ProgramRoomReference.IsLoaded)
                {
                    rel.ProgramRoomReference.Load();
                }
                RelapseModel model = new RelapseModel
                {
                    RelapseGUID = rel.RelapseGUID,
                    PageSequenceName = rel.PageSequence.Name,
                    PageSequenceDescription = rel.PageSequence.Description,
                    PageSequenceGUID = rel.PageSequence.PageSequenceGUID
                };
                if (rel.ProgramRoom != null)
                {
                    model.ProgramRoomGUID = rel.ProgramRoom.ProgramRoomGUID;
                }
                modellist.Add(model);
            }
            modellist = modellist.OrderBy(p => p.PageSequenceName).ToList();
            return modellist;
        }

        public List<RelapseModel> GetRelapseModelListBySessionGUID(Guid sessionGuid)
        {
            Session session = Resolve<ISessionRepository>().GetSessionBySessionGuid(sessionGuid);
            if (!session.ProgramReference.IsLoaded)
            {
                session.ProgramReference.Load();
            }
            return GetRelapseModelList(session.Program.ProgramGUID);
        }

        public RelapseModel GetRelapseModel(Guid programGUID, Guid pageSequnceGUID)
        {
            Relapse relapseEntity = Resolve<IRelapseRepository>().GetRelapseByProgramGUIDAndPageSequenceGUID(programGUID, pageSequnceGUID);
            if (!relapseEntity.PageSequenceReference.IsLoaded)
            {
                relapseEntity.PageSequenceReference.Load();
            }
            if (!relapseEntity.ProgramRoomReference.IsLoaded)
            {
                relapseEntity.ProgramRoomReference.Load();
            }
            RelapseModel model = new RelapseModel
            {
                RelapseGUID = relapseEntity.RelapseGUID,
                PageSequenceName = relapseEntity.PageSequence.Name,
                PageSequenceDescription = relapseEntity.PageSequence.Description,
                PageSequenceGUID = relapseEntity.PageSequence.PageSequenceGUID
            };
            if (relapseEntity.ProgramRoom != null)
            {
                model.ProgramRoomGUID = relapseEntity.ProgramRoom.ProgramRoomGUID;
            }
            return model;
        }

        public Relapse CloneRelapse(Relapse rel, CloneProgramParameterModel cloneParameterModel)
        {
            try
            {
                if (!rel.PageSequenceReference.IsLoaded)
                {
                    rel.PageSequenceReference.Load();
                }

                PageSequence clonedPageSequence = Resolve<IPageSequenceService>().ClonePageSequence(rel.PageSequence, new List<KeyValuePair<string, string>>(), cloneParameterModel);
                clonedPageSequence = Resolve<IPageSequenceService>().SetDefaultGuidForPageSequence(clonedPageSequence, cloneParameterModel);

                Relapse clonerelapse = new Relapse
                {
                    RelapseGUID = Guid.NewGuid(),
                    PageSequence = clonedPageSequence,//ClonePageSequence(rel.PageSequence, new List<KeyValuePair<string, string>>()),
                    ParentRelapseGUID = rel.RelapseGUID,
                    DefaultGUID = rel.DefaultGUID
                };
                //need copy PageVariable and PageVariableGroup

                return clonerelapse;
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
                    Message = string.Format("CloneRelapseException:{0}", ex),
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

        public Relapse SetDefaultGuidForRelapse(Relapse needSetDefaultEntity, CloneProgramParameterModel cloneParameterModel)
        {
            Relapse newEntity = new Relapse();
            
            newEntity = needSetDefaultEntity;
            switch (cloneParameterModel.source)
            {
                case DefaultGuidSourceEnum.FromDefaultGuid:
                    //newEntity.DefaultGUID = needSetDefaultEntity.DefaultGUID;//This is default value, don't need assign again.
                    break;
                case DefaultGuidSourceEnum.FromMatchDefaultGuidFunction:
                    bool isMatchDefaultGuidSuccessful = false;//whether default guid can be matched, if not ,set null.
                    Guid fromRelapseGuid = newEntity.ParentRelapseGUID == null ? Guid.Empty : (Guid)newEntity.ParentRelapseGUID;
                    Relapse fromRelapseEntity = Resolve<IRelapseRepository>().Get(fromRelapseGuid);
                    if (fromRelapseEntity != null)
                    {
                        if (!fromRelapseEntity.ProgramReference.IsLoaded) fromRelapseEntity.ProgramReference.Load();
                        Program fromProgramInDefaultLanguage = new Program();
                        if (fromRelapseEntity.Program.DefaultGUID.HasValue)//This fromProgram is not default program
                        {
                            fromProgramInDefaultLanguage = Resolve<IProgramRepository>().GetProgramByGuid((Guid)fromRelapseEntity.Program.DefaultGUID);
                        }
                        else//This fromProgram is default program. Or this is wrong data for old Data Version or some other reason.
                        {
                            fromProgramInDefaultLanguage = Resolve<IProgramRepository>().GetProgramByGuid(fromRelapseEntity.Program.ProgramGUID);
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
                                    List<Relapse> toDefaultRelapseList = Resolve<IRelapseRepository>().GetRelapseList(toProgramInDefaultLanguage.ProgramGUID).ToList();
                                    if (toDefaultRelapseList.Count == 1)
                                    {
                                        newEntity.DefaultGUID = toDefaultRelapseList[0].RelapseGUID;
                                    }
                                    else
                                    {
                                        isMatchDefaultGuidSuccessful = false;
                                    }
                                }
                                catch(Exception ex)
                                {
                                    Ethos.Utility.LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                                    isMatchDefaultGuidSuccessful = false;
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
                    newEntity.DefaultGUID = needSetDefaultEntity.ParentRelapseGUID;
                    break;
                default:
                    throw new Exception("Error: Dont't find the DefaultGuidSource for Relapse Entity.");
                    //break;
            }

            return newEntity;
        }
        #endregion
    }
}
