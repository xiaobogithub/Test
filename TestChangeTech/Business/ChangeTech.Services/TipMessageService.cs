using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Contracts;
using Ethos.DependencyInjection;
using ChangeTech.Entities;
using ChangeTech.Models;
using ChangeTech.IDataRepository;
using System.Web;

namespace ChangeTech.Services
{
    public class TipMessageService : ServiceBase, ITipMessageService
    {
        public TipMessageListModel GetTipMeassageByProgram(Guid programGuid)
        {
            TipMessageListModel tiplist = new TipMessageListModel();
            tiplist.TipMessageModelList = new List<TipMessageModel>();
            List<TipMessage> tipMessageList = Resolve<ITipMessageRepository>().GetTipMessageByProgram(programGuid).ToList();
            foreach (TipMessage message in tipMessageList)
            {
                if (!message.ProgramReference.IsLoaded)
                {
                    message.ProgramReference.Load();
                }
                if (!message.TipMessageTypeReference.IsLoaded)
                {
                    message.TipMessageTypeReference.Load();
                }
                TipMessageModel messageModel = new TipMessageModel
                {
                    TipMessageGUID = message.TipMessageGUID,
                    Message = message.Message,
                    TipMessageTypeGUID=message.TipMessageType.TipMessageTypeGUID,
                    //Name = message.Name,
                    Title = message.Title,
                    BackButtonName = message.BackButtonName,
                    ProgramGUID = message.Program.ProgramGUID
                };

                tiplist.TipMessageModelList.Add(messageModel);
            }
            return tiplist;
        }

        public TipMessageModel GetTipMessageModel(Guid tipMessageGuid)
        {
            TipMessage tipmessage = Resolve<ITipMessageRepository>().GetTipMessage(tipMessageGuid);
            if (!tipmessage.ProgramReference.IsLoaded)
            {
                tipmessage.ProgramReference.Load();
            }
            TipMessageModel tipmessagemodel = new TipMessageModel
            {
                BackButtonName = tipmessage.BackButtonName,
                ProgramGUID = tipmessage.Program.ProgramGUID,
                Message = tipmessage.Message,
                //Name = tipmessage.Name,
                TipMessageGUID = tipmessage.TipMessageGUID,
                Title = tipmessage.Title
            };
            return tipmessagemodel;
        }

        public void UpdateTipMessageModel(TipMessageModel tipmessagemodel)
        {
            TipMessage message = Resolve<ITipMessageRepository>().GetTipMessage(tipmessagemodel.TipMessageTypeGUID, tipmessagemodel.ProgramGUID);
            message.Message = tipmessagemodel.Message;
            message.Title = tipmessagemodel.Title;
            message.BackButtonName = tipmessagemodel.BackButtonName;
            Resolve<ITipMessageRepository>().UpdateTipMessage(message);
            Resolve<ITranslationJobService>().UpdateElementWhenFromUpdated("TipMessage", message.TipMessageGUID.ToString(), Guid.Empty);
        }

        public List<TipMessageTypeModel> GetAllTipMessageType()
        {
            List<TipMessageTypeModel> tipMessageTypeModelList = new List<TipMessageTypeModel>();
            List<TipMessageType> tipMessageTypeList = Resolve<ITipMessageRepository>().GetAllTipMessageType().ToList();
            foreach (TipMessageType type in tipMessageTypeList)
            {
                TipMessageTypeModel typeModel = new TipMessageTypeModel
                {
                    TipMessageTypeGUID = type.TipMessageTypeGUID,
                    TipMessageTypeName = type.TipMessageTypeName
                };
                tipMessageTypeModelList.Add(typeModel);
            }
            return tipMessageTypeModelList;
        }

        public TipMessageModel GetTipMessageModel(Guid tipMessageTypeGuid, Guid languageGuid)
        {
            TipMessage tipmessage = Resolve<ITipMessageRepository>().GetTipMessage(tipMessageTypeGuid, languageGuid);
            TipMessageType tipmessagetype = Resolve<ITipMessageRepository>().GetTipMessageType(tipMessageTypeGuid);
            TipMessageModel messageModel = new TipMessageModel();
            if (tipmessage != null)
            {
                if (!tipmessage.ProgramReference.IsLoaded)
                {
                    tipmessage.ProgramReference.Load();
                }
                if (!tipmessage.TipMessageTypeReference.IsLoaded)
                {
                    tipmessage.TipMessageTypeReference.Load();
                }

                messageModel.BackButtonName = tipmessage.BackButtonName;
                messageModel.ProgramGUID = tipmessage.Program.ProgramGUID;
                messageModel.Message = tipmessage.Message;
                messageModel.TipMessageGUID = tipmessage.TipMessageGUID;
                messageModel.TipMessageTypeGUID = tipmessage.TipMessageType.TipMessageTypeGUID;
                messageModel.Title = tipmessage.Title;
                messageModel.Explanation = tipmessagetype.Explanation;
            }

            return messageModel;
        }

        public void InsertTipMessage(TipMessageModel tipmessagemodel)
        {
            TipMessage tipMessage = new TipMessage
            {
                Title = tipmessagemodel.Title,
                Message = tipmessagemodel.Message,
                TipMessageGUID = Guid.NewGuid(),
                TipMessageType = Resolve<ITipMessageRepository>().GetTipMessageType(tipmessagemodel.TipMessageTypeGUID),
                Program = Resolve<IProgramRepository>().GetProgramByGuid(tipmessagemodel.ProgramGUID),
                BackButtonName = tipmessagemodel.BackButtonName,
            };
            Resolve<ITipMessageRepository>().Insert(tipMessage);
        }

        public string GetTipMessageText(Guid programGuid, string tipMessageTypeName)
        {
            string result = string.Empty;
            TipMessage message = Resolve<ITipMessageRepository>().GetTipMessage(programGuid, tipMessageTypeName);
            if (message != null)
            {
                result = message.Message;
            }
            else
            {
                // '<' and '>' in xml can not be displayed correctly in flash
                result = "Tip message type &quot;" + tipMessageTypeName + "&quot; is not configured yet.";
            }

            return result;
        }

        public bool CopyTipMessageFromProgram(Guid programGuid, Guid originalProgramGuid)
        {
            bool flag = false;
            try
            {
                if (programGuid != originalProgramGuid)
                {
                    Program programEntity = Resolve<IProgramRepository>().GetProgramByGuid(programGuid);
                    List<TipMessage> tipMessageList = Resolve<ITipMessageRepository>().GetTipMessageByProgram(originalProgramGuid).ToList();
                    Resolve<ITipMessageRepository>().DeleteTipMessagesByProgramGuid(programGuid);
                    //if (!programEntity.LanguageReference.IsLoaded) programEntity.LanguageReference.Load();

                    foreach (TipMessage tipMessageEntity in tipMessageList)
                    {
                        if (!tipMessageEntity.TipMessageTypeReference.IsLoaded)
                        {
                            tipMessageEntity.TipMessageTypeReference.Load();
                        }
                        TipMessage newTipMessage = new TipMessage
                        {
                            TipMessageType = tipMessageEntity.TipMessageType,
                            TipMessageGUID = Guid.NewGuid(),
                            BackButtonName = tipMessageEntity.BackButtonName,
                            Message = tipMessageEntity.Message,
                            Title = tipMessageEntity.Title,
                            ParentTipMessageGUID = tipMessageEntity.TipMessageGUID
                        };

                        //set TipMessage's DefaultGuid
                        if (programEntity.DefaultGUID.HasValue)
                        {
                            Program originalProgram = Resolve<IProgramRepository>().GetProgramByGuid(programEntity.DefaultGUID.Value);
                            TipMessage tipMessageByOriginalProgram = Resolve<ITipMessageRepository>().GetTipMessageByProgram(originalProgram.ProgramGUID).Where(tm => tm.TipMessageType.TipMessageTypeGUID == newTipMessage.TipMessageType.TipMessageTypeGUID).FirstOrDefault();
                            if (tipMessageByOriginalProgram != null)
                            {
                                newTipMessage.DefaultGUID = tipMessageByOriginalProgram.TipMessageGUID;
                            }
                        }
                        else
                        {
                            newTipMessage.DefaultGUID = null;
                        }

                        programEntity.TipMessage.Add(newTipMessage);
                    }
                    Resolve<IProgramRepository>().Update(programEntity);

                    // add log
                    InsertLogModel model = new InsertLogModel
                    {
                        ActivityLogType = (int)LogTypeEnum.CopyTipMessage,
                        Browser = HttpContext.Current.Request.UserAgent,
                        IP = HttpContext.Current.Request.UserHostAddress,
                        Message = "Copy Tip Message FromProgramGUID : " + originalProgramGuid.ToString(),
                        ProgramGuid = programGuid,
                        PageGuid = Guid.Empty,
                        PageSequenceGuid = Guid.Empty,
                        SessionGuid = Guid.Empty,
                        UserGuid = Resolve<IUserService>().GetCurrentUser().UserGuid,
                        From = string.Empty
                    };
                    Resolve<IActivityLogService>().Insert(model);
                    flag = true;
                }
                else
                {
                    flag = true;
                }
            }
            catch (Exception ex)
            {
                Ethos.Utility.LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
            return flag;
        }

        public void CopyTipMessageForProgram(Guid programGUID)
        {
            Program programentity = Resolve<IProgramRepository>().GetProgramByGuid(programGUID);
            if (!programentity.LanguageReference.IsLoaded)
            {
                programentity.LanguageReference.Load();
            }
            List<TipMessage> messagelist = GetTipmessage(programentity.Language.LanguageGUID);
            foreach (TipMessage message in messagelist)
            {
                if(!message.TipMessageTypeReference.IsLoaded)
                {
                    message.TipMessageTypeReference.Load();
                }
                TipMessage newtipmessage = new TipMessage
                {
                    TipMessageType = message.TipMessageType,
                    TipMessageGUID = Guid.NewGuid(),
                    BackButtonName = message.BackButtonName,
                    Message = message.Message,
                    Title = message.Title
                };

                programentity.TipMessage.Add(newtipmessage);
            }

            Resolve<IProgramRepository>().Update(programentity);
        }

        private List<TipMessage> GetTipmessage(Guid languageGuid)
        {
            Guid programguid = Guid.Empty;
            switch (languageGuid.ToString().ToUpper())
            {
                // english
                case "4C8140B5-25E0-46AC-A99E-6438B445C5B4":
                    programguid = new Guid("6A0EC3CB-9A72-4CB1-AC20-487DC763C9B1"); break;
                // norwegian
                case "FB5AE1DC-4CAF-4613-9739-7397429DDF25":
                    programguid = new Guid("14A6C26E-61BD-4466-AE5E-3001BAB9B6C2"); break;
                // danish
                case "1D181168-59F7-4F69-84A7-7A8391760760":
                    programguid = new Guid("F31857FC-EB07-4A79-A7C6-7994852B375E"); break;
                // swedish
                case "19058246-3A39-4BEC-ACEA-A1E1B4466E62":
                    programguid = new Guid("FE2A7F13-CF08-4D6C-9671-1DA9C460D33B"); break;
                // Finnish
                case "D8C900AD-9A11-499D-88E6-ECE687127B9E":
                    programguid = new Guid("E884F07F-6FBD-4DF7-B668-5AA956312DD6"); break;
            }
            return Resolve<ITipMessageRepository>().GetTipMessageByProgram(programguid).ToList();
        }

        //public string GetTipMessagePage(Guid programGuid, Guid userGuid, Guid languageGuid, string tipMessageTypeName)
        //{
        //    string tipMessagePageXML = string.Empty;
        //    string message = GetTipMessageText(languageGuid, tipMessageTypeName);
        //    tipMessagePageXML = Resolve<IStoreProcedure>().GetTipMessagePageAsXML(programGuid, userGuid, languageGuid, message);

        //    return tipMessagePageXML;
        //}

        private Guid GetTipMessageTypeGuid(string tipMessageTypeName)
        {
            Guid tipMessageTypeGuid = Guid.Empty;
            TipMessageType messageType = Resolve<ITipMessageRepository>().GetTipMessageType(tipMessageTypeName);
            if (messageType != null)
            {
                tipMessageTypeGuid = messageType.TipMessageTypeGUID;
            }

            return tipMessageTypeGuid;
        }

        public TipMessage CloneTipMessage(TipMessage message)
        {
            try
            {
                if (!message.TipMessageTypeReference.IsLoaded)
                {
                    message.TipMessageTypeReference.Load();
                }
                if (!message.ProgramReference.IsLoaded)
                {
                    message.ProgramReference.Load();
                }

                TipMessage clonedMessage = new TipMessage
                {
                    BackButtonName = message.BackButtonName,
                    Message = message.Message,
                    TipMessageGUID = Guid.NewGuid(),
                    Title = message.Title,
                    //Program = message.Program,
                    TipMessageType = message.TipMessageType,
                    ParentTipMessageGUID = message.TipMessageGUID,
                    DefaultGUID = message.DefaultGUID
                };

                return clonedMessage;
            }
            catch (Exception ex)
            {
                Ethos.Utility.LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        public TipMessage SetDefaultGuidForTipMessage(TipMessage needSetDefaultEntity, CloneProgramParameterModel cloneParameterModel)
        {
            TipMessage newEntity = new TipMessage();

            newEntity = needSetDefaultEntity;
            switch (cloneParameterModel.source)
            {
                case DefaultGuidSourceEnum.FromDefaultGuid:
                    //newEntity.DefaultGUID = needSetDefaultEntity.DefaultGUID;//This is default value, don't need assign again.
                    break;
                case DefaultGuidSourceEnum.FromMatchDefaultGuidFunction:
                    bool isMatchDefaultGuidSuccessful = false;//whether default guid can be matched, if not ,set null.
                    Guid fromTipMessageGuid = newEntity.ParentTipMessageGUID == null ? Guid.Empty : (Guid)newEntity.ParentTipMessageGUID;
                    TipMessage fromTipMessageEntity = Resolve<ITipMessageRepository>().GetTipMessage(fromTipMessageGuid);
                    if (fromTipMessageEntity != null)
                    {
                        if (!fromTipMessageEntity.ProgramReference.IsLoaded) fromTipMessageEntity.ProgramReference.Load();
                        Program fromProgramInDefaultLanguage = new Program();
                        if (fromTipMessageEntity.Program.DefaultGUID.HasValue)//This fromProgram is not default program
                        {
                            fromProgramInDefaultLanguage = Resolve<IProgramRepository>().GetProgramByGuid((Guid)fromTipMessageEntity.Program.DefaultGUID);
                        }
                        else//This fromProgram is default program. Or this is wrong data for old Data Version or some other reason.
                        {
                            fromProgramInDefaultLanguage = Resolve<IProgramRepository>().GetProgramByGuid(fromTipMessageEntity.Program.ProgramGUID);
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
                                    if (!fromTipMessageEntity.TipMessageTypeReference.IsLoaded) fromTipMessageEntity.TipMessageTypeReference.Load();
                                    TipMessage toDefaultTipMessage = Resolve<ITipMessageRepository>().GetTipMessage(fromTipMessageEntity.TipMessageType.TipMessageTypeGUID, toProgramInDefaultLanguage.ProgramGUID);
                                    newEntity.DefaultGUID = toDefaultTipMessage.TipMessageGUID;
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
                    newEntity.DefaultGUID = needSetDefaultEntity.ParentTipMessageGUID;
                    break;
                default:
                    throw new Exception("Error: Dont't find the DefaultGuidSource for TipMessage Entity.");
                    //break;
            }

            return newEntity;
        }
    }
}
