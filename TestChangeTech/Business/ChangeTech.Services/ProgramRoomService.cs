using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ChangeTech.Contracts;
using ChangeTech.IDataRepository;
using Ethos.DependencyInjection;
using ChangeTech.Entities;
using ChangeTech.Models;

namespace ChangeTech.Services
{
    public class ProgramRoomService : ServiceBase, IProgramRoomService
    {
        public ProgramRoomsModel GetRoomByProgram(Guid ProgramGuid)
        {
            ProgramRoomsModel models = new ProgramRoomsModel();
            List<ProgramRoom> list = Resolve<IProgramRoomRepository>().GetRoomByProgram(ProgramGuid);
            foreach (ProgramRoom item in list)
            {
                models.Add(new ProgramRoomModel
                {
                    Description = item.Description,
                    Name = item.Name,
                    ProgramRoomGuid = item.ProgramRoomGUID,
                });
            }
            return models;
        }

        public ProgramRoomModel GetRoom(Guid RoomGuid)
        {
            ProgramRoom room = Resolve<IProgramRoomRepository>().GetRoom(RoomGuid);
            ProgramRoomModel model = new ProgramRoomModel();
            model.Description = room.Description;
            model.Name = room.Name;
            model.TopBarColor = room.PrimaryThemeColor;
            model.ProgramRoomGuid = room.ProgramRoomGUID;
            model.ButtonOver = room.PrimaryButtonColorOver;
            model.ButtonNormal = room.PrimaryButtonColorNormal;
            model.ButtonDown = room.PrimaryButtonColorDown;
            model.ButtonDisable = room.PrimaryButtonColorDisable;
            model.IsCoverShadowVisible = room.CoverShadowVisible.HasValue ? room.CoverShadowVisible.Value : true;

            if (!string.IsNullOrEmpty(room.CoverShadowColor))
            {
                model.CoverShadowColor = room.CoverShadowColor.Substring(2);
            }
            if (!room.ProgramReference.IsLoaded)
            {
                room.ProgramReference.Load();
            }
            model.Program = new ProgramModel
            {
                Guid = room.Program.ProgramGUID,
                ProgramName = room.Program.Name,
            };
            return model;
        }

        public bool Insert(ProgramRoomModel room)
        {
            bool flag = false;

            if (!Resolve<IProgramRoomRepository>().IsExist(room.Name, room.Program.Guid))
            {
                ProgramRoom entity = new ProgramRoom
                {
                    Program = Resolve<IProgramRepository>().GetProgramByGuid(room.Program.Guid),
                    Description = room.Description,
                    Name = room.Name,
                    ProgramRoomGUID = Guid.NewGuid(),
                    LastUpdatedBy = Resolve<IUserService>().GetCurrentUser().UserGuid,
                };
                Resolve<IProgramRoomRepository>().Insert(entity);
                flag = true;
            }
            return flag;
        }

        public void Update(ProgramRoomModel room)
        {
            ProgramRoom entity = Resolve<IProgramRoomRepository>().GetRoom(room.ProgramRoomGuid);
            entity.Description = room.Description;
            entity.Name = room.Name;
            entity.PrimaryThemeColor = room.TopBarColor;
            entity.Program = Resolve<IProgramRepository>().GetProgramByGuid(room.Program.Guid);
            entity.LastUpdatedBy = Resolve<IUserService>().GetCurrentUser().UserGuid;            
            entity.PrimaryButtonColorDisable = room.ButtonDisable;
            if (!string.IsNullOrEmpty(room.ButtonDown))
            {
                entity.PrimaryButtonColorDown = room.ButtonDown;
            }
            else
            {
                entity.PrimaryButtonColorDown = null;
            }
            if (!string.IsNullOrEmpty(room.ButtonNormal))
            {
                entity.PrimaryButtonColorNormal = room.ButtonNormal;
            }
            else
            {
                entity.PrimaryButtonColorNormal = null;
            }
            if (!string.IsNullOrEmpty(room.ButtonOver))
            {
                entity.PrimaryButtonColorOver = room.ButtonOver;
            }
            else
            {
                entity.PrimaryButtonColorOver = null;
            }
            if (!string.IsNullOrEmpty(room.CoverShadowColor))
            {
                entity.CoverShadowColor = room.CoverShadowColor;
            }
            else
            {
                entity.CoverShadowColor = null;
            }
            entity.CoverShadowVisible = room.IsCoverShadowVisible;
            Resolve<IProgramRoomRepository>().Update(entity);
            Resolve<ITranslationJobService>().UpdateElementWhenFromUpdated("ProgramRoom", entity.ProgramRoomGUID.ToString(), Guid.Empty);
        }

        public void Delete(Guid RoomGuid)
        {
            Resolve<IProgramRoomRepository>().Delete(RoomGuid);
        }

        public bool CanDelete(Guid RoomGuid)
        {
            bool flag = false;
            if (Resolve<ISessionContentRepository>().GetSessionContentByRoomGuid(RoomGuid).Count<SessionContent>() == 0)
            {
                flag = true;
            }
            return flag;
        }

        public ProgramRoom CloneProgramRoom(ProgramRoom room)
        {
            try
            {
                ProgramRoom cloneRoom = new ProgramRoom
                {
                    ProgramRoomGUID = Guid.NewGuid(),
                    Name = room.Name,
                    Description = room.Description,
                    PrimaryThemeColor = room.PrimaryThemeColor,
                    SecondaryThemeColor = room.SecondaryThemeColor,
                    LastUpdated = DateTime.UtcNow,
                    ParentProgramRoomGUID = room.ProgramRoomGUID,
                    DefaultGUID = room.DefaultGUID
                };

                return cloneRoom;
            }
            catch (Exception ex)
            {
                Ethos.Utility.LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        public ProgramRoom SetDefaultGuidForProgramRoom(ProgramRoom needSetDefaultEntity, CloneProgramParameterModel cloneParameterModel)
        {
            ProgramRoom newEntity = new ProgramRoom();

            newEntity = needSetDefaultEntity;
            switch (cloneParameterModel.source)
            {
                case DefaultGuidSourceEnum.FromDefaultGuid:
                    //newEntity.DefaultGUID = needSetDefaultEntity.DefaultGUID;//This is default value, don't need assign again.
                    break;
                case DefaultGuidSourceEnum.FromMatchDefaultGuidFunction:
                    bool isMatchDefaultGuidSuccessful = false;//whether default guid can be matched, if not ,set null.
                    Guid fromProgramRoomGuid = newEntity.ParentProgramRoomGUID == null ? Guid.Empty : (Guid)newEntity.ParentProgramRoomGUID;
                    ProgramRoom fromProgramRoomEntity = Resolve<IProgramRoomRepository>().GetRoom(fromProgramRoomGuid);
                    if (fromProgramRoomEntity != null)
                    {
                        if (!fromProgramRoomEntity.ProgramReference.IsLoaded) fromProgramRoomEntity.ProgramReference.Load();
                        Program fromProgramInDefaultLanguage = new Program();
                        if (fromProgramRoomEntity.Program.DefaultGUID.HasValue)//This fromProgram is not default program
                        {
                            fromProgramInDefaultLanguage = Resolve<IProgramRepository>().GetProgramByGuid((Guid)fromProgramRoomEntity.Program.DefaultGUID);
                        }
                        else//This fromProgram is default program. Or this is wrong data for old Data Version or some other reason.
                        {
                            fromProgramInDefaultLanguage = Resolve<IProgramRepository>().GetProgramByGuid(fromProgramRoomEntity.Program.ProgramGUID);
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
                                    //TODO: GetRoomByProgramAndParent need validate  =>  This is not right. can't match
                                    List<ProgramRoom> toDefaultProgramRoomList = Resolve<IProgramRoomRepository>().GetRoomsByProgram(toProgramInDefaultLanguage.ProgramGUID).Where(r => r.Name == fromProgramRoomEntity.Name).ToList();
                                    if (toDefaultProgramRoomList.Count == 1)
                                    {
                                        newEntity.DefaultGUID = toDefaultProgramRoomList[0].ProgramRoomGUID;
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
                    newEntity.DefaultGUID = needSetDefaultEntity.ParentProgramRoomGUID;
                    break;
                default:
                    throw new Exception("Error: Dont't find the DefaultGuidSource for ProgramRoom Entity.");
                    //break;
            }

            return newEntity;
        }
    }
}
