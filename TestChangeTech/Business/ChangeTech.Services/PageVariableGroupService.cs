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
    public class PageVariableGroupService : ServiceBase, IPageVariableGroupService
    {
        public EditPageVariableGroupModel GetPageVariableGroupByProgram(Guid programGuid)
        {
            EditPageVariableGroupModel groupModel = new EditPageVariableGroupModel();
            groupModel.VariableGroupModels = new List<PageVariableGroupModel>();
            Program program = Resolve<IProgramRepository>().GetProgramByGuid(programGuid);
            groupModel.ProgramGUID = program.ProgramGUID;
            groupModel.ProgramName = program.Name;
            List<PageVariableGroup> grouplist = Resolve<IPageVariableGroupRepository>().GetGroupByProgram(programGuid).ToList();
            foreach (PageVariableGroup group in grouplist)
            {
                if (!group.ProgramReference.IsLoaded)
                {
                    group.ProgramReference.Load();
                }
                PageVariableGroupModel variableGroup = new PageVariableGroupModel();
                variableGroup.ProgramGUID = group.Program.ProgramGUID;
                variableGroup.PageVariableGroupGUID = group.PageVariableGroupGUID;
                variableGroup.Name = group.Name;
                variableGroup.Description = group.Description;
                groupModel.VariableGroupModels.Add(variableGroup);
            }
            User operater = Resolve<IUserRepository>().GetUserByGuid(Resolve<IUserService>().GetCurrentUser().UserGuid);
            ChangeTech.Entities.PageVariable lastSelectedPageVariable = operater.LastSelectedPageVariable == null ? null : Resolve<IPageVaribleRepository>().GetItem(operater.LastSelectedPageVariable.Value);
            
            if (lastSelectedPageVariable != null)
            {
                if (!lastSelectedPageVariable.PageVariableGroupReference.IsLoaded)
                {
                    lastSelectedPageVariable.PageVariableGroupReference.Load();
                }
                if (!lastSelectedPageVariable.ProgramReference.IsLoaded)
                {
                    lastSelectedPageVariable.ProgramReference.Load();
                }
                if (!lastSelectedPageVariable.PageVariableGroupReference.IsLoaded)
                {
                    lastSelectedPageVariable.PageVariableGroupReference.Load();
                }
                groupModel.LastSelectedPageVariable = new ChangeTech.Models.EditPageVariableModel
                {
                    Description = lastSelectedPageVariable.Description,
                    Name = lastSelectedPageVariable.Name,
                    PageVariableGUID = lastSelectedPageVariable.PageVariableGUID,
                    ProgramGUID = lastSelectedPageVariable.Program.ProgramGUID,
                    modelStatus = ModelStatus.ModelNoChange,
                    PageVariableType = lastSelectedPageVariable.PageVariableType,
                    ValueType = lastSelectedPageVariable.ValueType,
                    PageVariableGroupGUID = lastSelectedPageVariable.PageVariableGroup == null ? Guid.Empty : lastSelectedPageVariable.PageVariableGroup.PageVariableGroupGUID
                };
            }            

            groupModel.ObjectStatus = new SortedList<Guid, ModelStatus>();
            return groupModel;
        }

        public void SavePageVariabeGroup(EditPageVariableGroupModel groupModel)
        {
            foreach (PageVariableGroupModel model in groupModel.VariableGroupModels)
            {
                if (groupModel.ObjectStatus.ContainsKey(model.PageVariableGroupGUID))
                {
                    switch (groupModel.ObjectStatus[model.PageVariableGroupGUID])
                    {
                        case ModelStatus.ModelAdd:
                            PageVariableGroup group = new PageVariableGroup
                            {
                                PageVariableGroupGUID = model.PageVariableGroupGUID,
                                Name = model.Name,
                                Description = model.Description,
                                LastUpdated = DateTime.UtcNow,
                                LastUpdatdBy = Resolve<IUserService>().GetCurrentUser().UserGuid,
                            };
                            group.Program = Resolve<IProgramRepository>().GetProgramByGuid(model.ProgramGUID);

                            Resolve<IPageVariableGroupRepository>().Insert(group);
                            break;
                        case ModelStatus.ModelEdit:
                            PageVariableGroup updateGroup = Resolve<IPageVariableGroupRepository>().Get(model.PageVariableGroupGUID);
                            updateGroup.Name = model.Name;
                            updateGroup.Description = model.Description;
                            updateGroup.LastUpdatdBy = Resolve<IUserService>().GetCurrentUser().UserGuid;
                            Resolve<IPageVariableGroupRepository>().Update(updateGroup);
                            break;                      
                    }
                }
            }

            //delete group
            foreach (KeyValuePair<Guid, ModelStatus> item in groupModel.ObjectStatus)
            {
                if (item.Value == ModelStatus.ModelDelete)
                {
                    Resolve<IPageVariableGroupRepository>().Delete(item.Key);
                }
            }
        }

        public PageVariableGroup CloneVariableGroup(PageVariableGroup group)
        {
            try
            {
                PageVariableGroup cloneGroup = new PageVariableGroup
                {
                    PageVariableGroupGUID = Guid.NewGuid(),
                    Name = group.Name,
                    Description = group.Description,
                    LastUpdated = DateTime.UtcNow,
                    ParentPageVariableGroupGUID = group.PageVariableGroupGUID,
                    DefaultGUID = group.DefaultGUID
                };
                return cloneGroup;
            }
            catch (Exception ex)
            {
                Ethos.Utility.LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        public PageVariableGroup SetDefaultGuidForPageVariableGroup(PageVariableGroup needSetDefaultEntity, CloneProgramParameterModel cloneParameterModel)
        {
            PageVariableGroup newEntity = new PageVariableGroup();

            newEntity = needSetDefaultEntity;
            switch (cloneParameterModel.source)
            {
                case DefaultGuidSourceEnum.FromDefaultGuid:
                    //newEntity.DefaultGUID = needSetDefaultEntity.DefaultGUID;//This is default value, don't need assign again.
                    break;
                case DefaultGuidSourceEnum.FromMatchDefaultGuidFunction:
                    bool isMatchDefaultGuidSuccessful = false;//whether default guid can be matched, if not ,set null.
                    Guid fromPageVariableGroupGuid = newEntity.ParentPageVariableGroupGUID == null ? Guid.Empty : (Guid)newEntity.ParentPageVariableGroupGUID;
                    PageVariableGroup fromPageVariableGroupEntity = Resolve<IPageVariableGroupRepository>().Get(fromPageVariableGroupGuid);

                    if (fromPageVariableGroupEntity != null)
                    {
                        if (!fromPageVariableGroupEntity.ProgramReference.IsLoaded) fromPageVariableGroupEntity.ProgramReference.Load();
                        Program fromProgramInDefaultLanguage = new Program();
                        if (fromPageVariableGroupEntity.Program.DefaultGUID.HasValue)//This fromProgram is not default program
                        {
                            fromProgramInDefaultLanguage = Resolve<IProgramRepository>().GetProgramByGuid((Guid)fromPageVariableGroupEntity.Program.DefaultGUID);
                        }
                        else//This fromProgram is default program. Or this is wrong data for old Data Version or some other reason.
                        {
                            fromProgramInDefaultLanguage = Resolve<IProgramRepository>().GetProgramByGuid(fromPageVariableGroupEntity.Program.ProgramGUID);
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
                                    PageVariableGroup toDefaultPageVariableGroup = Resolve<IPageVariableGroupRepository>().GetPageVariableByProgramAndParentGroupGUID(toProgramInDefaultLanguage.ProgramGUID, fromPageVariableGroupEntity.PageVariableGroupGUID);
                                    newEntity.DefaultGUID = toDefaultPageVariableGroup.PageVariableGroupGUID;
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
                    newEntity.DefaultGUID = needSetDefaultEntity.ParentPageVariableGroupGUID;
                    break;
                default:
                    throw new Exception("Error: Dont't find the DefaultGuidSource for PageVariableGroup Entity.");
                    //break;
            }

            return newEntity;
        }
    }
}
