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
    public class ProgramAccessoryService : ServiceBase, IProgramAccessoryService
    {
        public AccessoryPageModel GetProgramAccessoryByProgarm(Guid programGuid, string type)
        {
            AccessoryPageModel pageModel = new AccessoryPageModel();
            AccessoryTemplate accessoryTemplate = Resolve<IProgramAccessoryRepository>().GetAccessory(programGuid, type);
            if(accessoryTemplate != null)
            {
                //if (!accessoryTemplate.LanguageReference.IsLoaded)
                //{
                //    accessoryTemplate.LanguageReference.Load();
                //}
                if(!accessoryTemplate.ProgramReference.IsLoaded)
                {
                    accessoryTemplate.ProgramReference.Load();
                }

                pageModel.AccessoryTemplateGUID = accessoryTemplate.AccessoryTemplateGUID;
                pageModel.Heading = accessoryTemplate.Heading;
                //pageModel.LanguageGUID = accessoryTemplate.Language.LanguageGUID;
                pageModel.PasswordText = accessoryTemplate.PasswordText;
                pageModel.PrimaryButtonText = accessoryTemplate.PrimaryButtonText;
                pageModel.ProgramGUID = accessoryTemplate.Program.ProgramGUID;
                pageModel.SecondaryButtonText = accessoryTemplate.SecondaryButtonText;
                pageModel.Text = accessoryTemplate.Text;
                pageModel.Type = accessoryTemplate.Type;
                pageModel.UserNameText = accessoryTemplate.UserNameText;
            }

            return pageModel;
        }

        public void AddAccessroy(AccessoryPageModel pageModel)
        {
            AccessoryTemplate newTemplate = new AccessoryTemplate
            {
                AccessoryTemplateGUID = Guid.NewGuid(),
                Heading = pageModel.Heading,
                Text = pageModel.Text,
                UserNameText = pageModel.UserNameText,
                PasswordText = pageModel.PasswordText,
                PrimaryButtonText = pageModel.PrimaryButtonText,
                SecondaryButtonText = pageModel.SecondaryButtonText,
                Type = pageModel.Type,
                Order = pageModel.Type == "Password reminder" ? 2 : 1,
            };
            newTemplate.Program = Resolve<IProgramRepository>().GetProgramByGuid(pageModel.ProgramGUID);
            //newTemplate.Language = Resolve<ILanguageRepository>().GetLanguage(pageModel.LanguageGUID);

            Resolve<IProgramAccessoryRepository>().Insert(newTemplate);
        }

        public void UpdateAccessory(AccessoryPageModel pageModel)
        {
            AccessoryTemplate accessory = Resolve<IProgramAccessoryRepository>().GetAccessory(pageModel.AccessoryTemplateGUID);
            accessory.Heading = pageModel.Heading;
            accessory.Text = pageModel.Text;
            accessory.UserNameText = pageModel.UserNameText;
            accessory.PrimaryButtonText = pageModel.PrimaryButtonText;
            accessory.SecondaryButtonText = pageModel.SecondaryButtonText;
            accessory.PasswordText = pageModel.PasswordText;
            Resolve<IProgramAccessoryRepository>().Update(accessory);
            Resolve<ITranslationJobService>().UpdateElementWhenFromUpdated("AccessoryTemplate", accessory.AccessoryTemplateGUID.ToString(), Guid.Empty);
        }

        public void DeleteAccessory(Guid AccessoryGuid)
        {
            throw new NotImplementedException();
        }

        public AccessoryPageModel GetAccessory(Guid accessoryGuid)
        {
            AccessoryTemplate accessory = Resolve<IProgramAccessoryRepository>().GetAccessory(accessoryGuid);
            //if (!accessory.LanguageReference.IsLoaded)
            //{
            //    accessory.LanguageReference.Load();
            //}
            if(!accessory.ProgramReference.IsLoaded)
            {
                accessory.ProgramReference.Load();
            }
            AccessoryPageModel pageModel = new AccessoryPageModel
            {
                AccessoryTemplateGUID = accessory.AccessoryTemplateGUID,
                Heading = accessory.Heading,
                //LanguageGUID = accessory.Language.LanguageGUID,
                PasswordText = accessory.PasswordText,
                PrimaryButtonText = accessory.PrimaryButtonText,
                ProgramGUID = accessory.Program.ProgramGUID,
                SecondaryButtonText = accessory.SecondaryButtonText,
                Text = accessory.Text,
                Type = accessory.Type,
                UserNameText = accessory.UserNameText
            };
            return pageModel;
        }

        public bool IsExist(Guid programGuid, string type)
        {
            bool flag = false;
            AccessoryTemplate accessory = Resolve<IProgramAccessoryRepository>().GetAccessory(programGuid, type);
            if(accessory != null)
            {
                flag = true;
            }

            return flag;
        }

        public string GetProgramAccessoryModelAsXML(Guid programGuid, Guid languageGuid)
        {
            return Resolve<IStoreProcedure>().GetProgramAccessoryModelAsXML(programGuid, languageGuid);
        }

        public string GetSessionEndingPageModelAsXML(Guid programGuid, Guid languageGuid, Guid userGuid)
        {
            int day = 0;
            ProgramUser pu = Resolve<IProgramUserRepository>().GetProgramUserByProgramGuidAndUserGuid(programGuid, userGuid);
            //TODO:Need change status as Enum, then remove .ToString()
            if(!pu.Status.Equals(ProgramUserStatusEnum.Screening.ToString()))
            {
                day = 1;
            }
            return Resolve<IStoreProcedure>().GetProgramSessionEndingModelAsXML(programGuid, languageGuid, userGuid, day);
        }

        public string GetPinCodePageModelAsXML(Guid programGuid, Guid lanaugeGuid, Guid userGuid)
        {
            return Resolve<IStoreProcedure>().GetPinCodeModelAsXML(programGuid, lanaugeGuid, userGuid);
        }

        public string GetPaymentModelAsXML(Guid programGuid, Guid languageGuid, Guid userGuid)
        {
            return Resolve<IStoreProcedure>().GetPaymentModelAsXML(programGuid, languageGuid, userGuid);
        }

        public AccessoryTemplate CloneAccessoryTemplate(AccessoryTemplate accessoryTemplate)
        {
            try
            {
                AccessoryTemplate cloneAccessoryTemplate = new AccessoryTemplate
                {
                    AccessoryTemplateGUID = Guid.NewGuid(),
                    Heading = accessoryTemplate.Heading,
                    Order = accessoryTemplate.Order,
                    PasswordText = accessoryTemplate.PasswordText,
                    PrimaryButtonText = accessoryTemplate.PrimaryButtonText,
                    SecondaryButtonText = accessoryTemplate.SecondaryButtonText,
                    Text = accessoryTemplate.Text,
                    Type = accessoryTemplate.Type,
                    UserNameText = accessoryTemplate.UserNameText,
                    ParentAccessoryTemplateGUID = accessoryTemplate.AccessoryTemplateGUID,
                    DefaultGUID = accessoryTemplate.DefaultGUID
                };
                return cloneAccessoryTemplate;
            }
            catch (Exception ex)
            {
                Ethos.Utility.LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        public AccessoryTemplate SetDefaultGuidForAccessoryTemplate(AccessoryTemplate needSetDefaultEntity, CloneProgramParameterModel cloneParameterModel)
        {
            AccessoryTemplate newEntity = new AccessoryTemplate();

            newEntity = needSetDefaultEntity;
            switch (cloneParameterModel.source)
            {
                case DefaultGuidSourceEnum.FromDefaultGuid:
                    //newEntity.DefaultGUID = needSetDefaultEntity.DefaultGUID;//This is default value, don't need assign again.
                    break;
                case DefaultGuidSourceEnum.FromMatchDefaultGuidFunction:
                    bool isMatchDefaultGuidSuccessful = false;//whether default guid can be matched, if not ,set null.
                    Guid fromAccessoryTemplateGuid = newEntity.ParentAccessoryTemplateGUID == null ? Guid.Empty : (Guid)newEntity.ParentAccessoryTemplateGUID;
                    AccessoryTemplate fromAccessoryTemplateEntity = Resolve<IProgramAccessoryRepository>().GetAccessory(fromAccessoryTemplateGuid);

                    if (fromAccessoryTemplateEntity != null)
                    {
                        if (!fromAccessoryTemplateEntity.ProgramReference.IsLoaded) fromAccessoryTemplateEntity.ProgramReference.Load();
                        Program fromProgramInDefaultLanguage = new Program();
                        if (fromAccessoryTemplateEntity.Program.DefaultGUID.HasValue)//This fromProgram is not default program
                        {
                            fromProgramInDefaultLanguage = Resolve<IProgramRepository>().GetProgramByGuid((Guid)fromAccessoryTemplateEntity.Program.DefaultGUID);
                        }
                        else//This fromProgram is default program. Or this is wrong data for old Data Version or some other reason.
                        {
                            fromProgramInDefaultLanguage = Resolve<IProgramRepository>().GetProgramByGuid(fromAccessoryTemplateEntity.Program.ProgramGUID);
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
                                    AccessoryTemplate toDefaultAccessoryTemplate = Resolve<IProgramAccessoryRepository>().GetAccessory(toProgramInDefaultLanguage.ProgramGUID, fromAccessoryTemplateEntity.Type);
                                    newEntity.DefaultGUID = toDefaultAccessoryTemplate.AccessoryTemplateGUID;
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
                    newEntity.DefaultGUID = needSetDefaultEntity.ParentAccessoryTemplateGUID;
                    break;
                default:
                    throw new Exception("Error: Dont't find the DefaultGuidSource for AccessoryTemplate Entity.");
                    //break;
            }

            return newEntity;
        }
    }
}
