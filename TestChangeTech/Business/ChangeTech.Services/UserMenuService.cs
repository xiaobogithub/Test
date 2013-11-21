using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ethos.DependencyInjection;
using ChangeTech.Contracts;
using ChangeTech.Models;
using ChangeTech.IDataRepository;
using ChangeTech.Entities;

namespace ChangeTech.Services
{
    public class UserMenuService : ServiceBase, IUserMenuService
    {
        public List<UserMenuModel> GetUserMenusOfProram(Guid programGuid)
        {
            List<UserMenuModel> userMenuList = new List<UserMenuModel>();
            IQueryable<UserMenu> userMenuEntities = Resolve<IUserMenuRepository>().GetUserMenuOfProgram(programGuid);
            foreach(UserMenu userMenuEntity in userMenuEntities)
            {
                userMenuList.Add(ParaseUserMenu(userMenuEntity));
            }
            return userMenuList;
        }

        public UserMenuModel GetUserMenu(Guid menuItemGUID)
        {
            UserMenuModel userMenuModel = new UserMenuModel();
            UserMenu userMenuEntity = Resolve<IUserMenuRepository>().GetUserMenu(menuItemGUID);
            return ParaseUserMenu(userMenuEntity);
        }

        public void UpdateUserMenu(Guid menuItemGUID, string text, string formTitle, string formText, string formBackButtonText, string formSubmitButtonText)
        {
            UserMenu userMenuEntity = Resolve<IUserMenuRepository>().GetUserMenu(menuItemGUID);
            userMenuEntity.MenuFormBackButtonName = formBackButtonText;
            userMenuEntity.MenuFormSubmitButtonName = formSubmitButtonText;
            userMenuEntity.MenuFormText = formText;
            userMenuEntity.MenuFormTitle = formTitle;
            userMenuEntity.MenuText = text;
            Resolve<IUserMenuRepository>().UpdateUserMenu(userMenuEntity);
            Resolve<ITranslationJobService>().UpdateElementWhenFromUpdated("UserMenu", userMenuEntity.MenuItemGUID.ToString(), Guid.Empty);
        }

        private UserMenuModel ParaseUserMenu(UserMenu userMenuEntity)
        {
            UserMenuModel userMenuModel = new UserMenuModel();
            userMenuModel.Name = userMenuEntity.Name;
            userMenuModel.Text = userMenuEntity.MenuText;
            userMenuModel.FormBackButtonText = userMenuEntity.MenuFormBackButtonName;
            userMenuModel.FormSubmitButtonText = userMenuEntity.MenuFormSubmitButtonName;
            userMenuModel.FormText = userMenuEntity.MenuFormText;
            userMenuModel.FormTitle = userMenuEntity.MenuFormTitle;
            userMenuModel.MenuItemGUID = userMenuEntity.MenuItemGUID;
            userMenuModel.Available = userMenuEntity.Available.HasValue ? userMenuEntity.Available.Value : true;
            return userMenuModel;
        }

        public void CreateUserMenuForProgramWhoDonotHave()
        {
            List<Program> programlist = Resolve<IProgramRepository>().GetAllPrograms().ToList();
            foreach(Program program in programlist)
            {
                if(!program.UserMenu.IsLoaded)
                {
                    program.UserMenu.Load();
                }
            }
            programlist = programlist.Where(p => p.UserMenu.Count == 0).ToList();
            List<UserMenuTemplate> menuTemplates = Resolve<IUserMenuRepository>().GetMenuTempates().ToList();

            foreach(Program nonProgram in programlist)
            {
                foreach(UserMenuTemplate template in menuTemplates)
                {
                    UserMenu menu = new UserMenu
                    {
                        MenuItemGUID = Guid.NewGuid(),
                        MenuFormTitle = template.Name,
                        Name = template.Name,
                        Order = template.Order
                    };
                    nonProgram.UserMenu.Add(menu);
                }
                Resolve<IProgramRepository>().Update(nonProgram);
            }
        }

        public Program addUserMenuFormProgarm(Program programentity)
        {
            List<UserMenuTemplate> menuTemplates = Resolve<IUserMenuRepository>().GetMenuTempates().ToList();
            foreach(UserMenuTemplate template in menuTemplates)
            {
                UserMenu menu = new UserMenu
                {
                    MenuItemGUID = Guid.NewGuid(),
                    MenuFormTitle = template.Name,
                    Name = template.Name,
                    Order = template.Order
                };
                programentity.UserMenu.Add(menu);
            }

            return programentity;
        }

        public void AddUserMenuForProgram(Guid programGuid)
        {
            Program programentity = Resolve<IProgramRepository>().GetProgramByGuid(programGuid);
            if(!programentity.UserMenu.IsLoaded)
            {
                programentity.UserMenu.Load();
            }
            if(programentity.UserMenu.Count > 0)
                return;

            List<UserMenuTemplate> menuTemplates = Resolve<IUserMenuRepository>().GetMenuTempates().ToList();
            foreach(UserMenuTemplate template in menuTemplates)
            {
                UserMenu menu = new UserMenu
                {
                    MenuItemGUID = Guid.NewGuid(),
                    MenuFormTitle = template.Name,
                    Name = template.Name,
                    Order = template.Order
                };
                programentity.UserMenu.Add(menu);
            }

            Resolve<IProgramRepository>().Update(programentity);
        }

        public void EnableUserMenu(Guid menuItemGUID)
        {
            UserMenu menu = Resolve<IUserMenuRepository>().GetUserMenu(menuItemGUID);
            menu.Available = true;
            Resolve<IUserMenuRepository>().UpdateUserMenu(menu);
        }

        public void UnableUserMenu(Guid menuItemGUID)
        {
            UserMenu menu = Resolve<IUserMenuRepository>().GetUserMenu(menuItemGUID);
            menu.Available = false;
            Resolve<IUserMenuRepository>().UpdateUserMenu(menu);
        }

        public UserMenu CloneUserMenu(UserMenu userMenu)
        {
            try
            {
                UserMenu cloneUserMenu = new UserMenu
                {
                    MenuFormBackButtonName = userMenu.MenuFormBackButtonName,
                    MenuFormSubmitButtonName = userMenu.MenuFormSubmitButtonName,
                    MenuFormText = userMenu.MenuFormText,
                    MenuFormTitle = userMenu.MenuFormTitle,
                    MenuItemGUID = Guid.NewGuid(),
                    MenuText = userMenu.MenuText,
                    Name = userMenu.Name,
                    Order = userMenu.Order,
                    ParentUserMenuGUID = userMenu.MenuItemGUID,
                    DefaultGUID = userMenu.DefaultGUID,
                    Available = userMenu.Available 
                };
                return cloneUserMenu;
            }
            catch (Exception ex)
            {
                Ethos.Utility.LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        public UserMenu SetDefaultGuidForUserMenu(UserMenu needSetDefaultEntity, CloneProgramParameterModel cloneParameterModel)
        {
            UserMenu newEntity = new UserMenu();

            newEntity = needSetDefaultEntity;
            switch (cloneParameterModel.source)
            {
                case DefaultGuidSourceEnum.FromDefaultGuid:
                    //newEntity.DefaultGUID = needSetDefaultEntity.DefaultGUID;//This is default value, don't need assign again.
                    break;
                case DefaultGuidSourceEnum.FromMatchDefaultGuidFunction:
                    bool isMatchDefaultGuidSuccessful = false;//whether default guid can be matched, if not ,set null.
                    Guid fromUserMenuGuid = newEntity.ParentUserMenuGUID == null ? Guid.Empty : (Guid)newEntity.ParentUserMenuGUID;
                    UserMenu fromUserMenuEntity = Resolve<IUserMenuRepository>().GetUserMenu(fromUserMenuGuid);
                    if (fromUserMenuEntity != null)
                    {
                        if (!fromUserMenuEntity.ProgramReference.IsLoaded) fromUserMenuEntity.ProgramReference.Load();
                        Program fromProgramInDefaultLanguage = new Program();
                        if (fromUserMenuEntity.Program.DefaultGUID.HasValue)//This fromProgram is not default program
                        {
                            fromProgramInDefaultLanguage = Resolve<IProgramRepository>().GetProgramByGuid((Guid)fromUserMenuEntity.Program.DefaultGUID);
                        }
                        else//This fromProgram is default program. Or this is wrong data for old Data Version or some other reason.
                        {
                            fromProgramInDefaultLanguage = Resolve<IProgramRepository>().GetProgramByGuid(fromUserMenuEntity.Program.ProgramGUID);
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
                                    UserMenu toDefaultUserMenu = Resolve<IUserMenuRepository>().GetUserMenuOfProgram(toProgramInDefaultLanguage.ProgramGUID).Where(u => u.Order == fromUserMenuEntity.Order).FirstOrDefault();
                                    newEntity.DefaultGUID = toDefaultUserMenu.MenuItemGUID;
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
                    newEntity.DefaultGUID = needSetDefaultEntity.ParentUserMenuGUID;
                    break;
                default:
                    throw new Exception("Error: Dont't find the DefaultGuidSource for UserMenu Entity.");
                    //break;
            }

            return newEntity;
        }
    }
}
