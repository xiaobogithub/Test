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
    public class EmailTemplateService : ServiceBase, IEmailTemplateService
    {
        // so far, not use
        // Wang Jun 2011/08/02
        public EmailTemplatesModel GetAll()
        {
            EmailTemplatesModel models = new EmailTemplatesModel();
            List<EmailTemplate> list = Resolve<IEmailTemplateRepository>().GetAllEmailTemplate();
            foreach(EmailTemplate item in list)
            {
                models.Add(new EmailTemplateModel
                {
                    Body = item.Body,
                    EmailTemplateGuid = item.EmailTemplateGUID,
                    Name = item.Name,
                    Subject = item.Subject,
                    LinkText = item.LinkText
                });
            }
            return models;
        }

        public EmailTemplateModel GetByProgramEmailTemplageType(Guid ProgramGuid, Guid EmailTemplateTypeGuid)
        {
            EmailTemplate emailTemplate = Resolve<IEmailTemplateRepository>().GetByProgramEmailTemplateType(ProgramGuid, EmailTemplateTypeGuid);

            EmailTemplateModel model = null;
            if(emailTemplate != null)
            {
                model = new EmailTemplateModel();
                model.Body = emailTemplate.Body;
                model.Name = emailTemplate.Name;
                model.Subject = emailTemplate.Subject;
                model.EmailTemplateGuid = emailTemplate.EmailTemplateGUID;
                model.LinkText = emailTemplate.LinkText;
            }
            return model;
        }

        public void Update(EmailTemplateModel emailTemplate)
        {
            EmailTemplate template = Resolve<IEmailTemplateRepository>().GetEmailTemplate(emailTemplate.EmailTemplateGuid);
            template.Body = emailTemplate.Body;
            template.Name = emailTemplate.Name;
            template.EmailTemplateType = Resolve<IEmailTemplateTypeRepository>().GetEmailTemplateType(emailTemplate.Type.EmailTemplateTypeGuid);
            //template.Language = Resolve<ILanguageRepository>().GetLanguage(emailTemplate.Language.LanguageGUID);
            template.Program = Resolve<IProgramRepository>().GetProgramByGuid(emailTemplate.Program.Guid);
            template.Subject = emailTemplate.Subject;
            template.LinkText = emailTemplate.LinkText;
            template.LastUpdatedBy = Resolve<IUserService>().GetCurrentUser().UserGuid;
            Resolve<IEmailTemplateRepository>().Update(template);
            Resolve<ITranslationJobService>().UpdateElementWhenFromUpdated("EmailTemplate", template.EmailTemplateGUID.ToString(), Guid.Empty);

            User operater = Resolve<IUserRepository>().GetUserByGuid(Resolve<IUserService>().GetCurrentUser().UserGuid);
            operater.LastSelectedEmailTemplateType = emailTemplate.Type.EmailTemplateTypeGuid;
            Resolve<IUserRepository>().UpdateUser(operater);
        }

        public void Insert(EmailTemplateModel emailTemplate)
        {
            EmailTemplate template = Resolve<IEmailTemplateRepository>().GetByProgramEmailTemplateType(emailTemplate.Program.Guid, emailTemplate.Type.EmailTemplateTypeGuid);
            if (template != null)
            {
                template.Body = emailTemplate.Body;
                template.Name = emailTemplate.Name;
                template.EmailTemplateType = Resolve<IEmailTemplateTypeRepository>().GetEmailTemplateType(emailTemplate.Type.EmailTemplateTypeGuid);
                //template.Language = Resolve<ILanguageRepository>().GetLanguage(emailTemplate.Language.LanguageGUID);
                template.Program = Resolve<IProgramRepository>().GetProgramByGuid(emailTemplate.Program.Guid);
                template.Subject = emailTemplate.Subject;
                template.LinkText = emailTemplate.LinkText;
                template.LastUpdatedBy = Resolve<IUserService>().GetCurrentUser().UserGuid;
                Resolve<IEmailTemplateRepository>().Update(template);
                Resolve<ITranslationJobService>().UpdateElementWhenFromUpdated("EmailTemplate", template.EmailTemplateGUID.ToString(), Guid.Empty);
            }
            else
            {
                template = new EmailTemplate
                {
                    Body = emailTemplate.Body,
                    Name = emailTemplate.Name,
                    EmailTemplateGUID = emailTemplate.EmailTemplateGuid,
                    EmailTemplateType = Resolve<IEmailTemplateTypeRepository>().GetEmailTemplateType(emailTemplate.Type.EmailTemplateTypeGuid),
                    //Language = Resolve<ILanguageRepository>().GetLanguage(emailTemplate.Language.LanguageGUID),
                    Program = Resolve<IProgramRepository>().GetProgramByGuid(emailTemplate.Program.Guid),
                    Subject = emailTemplate.Subject,
                    LinkText = emailTemplate.LinkText,
                    LastUpdatedBy = Resolve<IUserService>().GetCurrentUser().UserGuid,
                };
                Resolve<IEmailTemplateRepository>().Insert(template);
            }

            User operater = Resolve<IUserRepository>().GetUserByGuid(Resolve<IUserService>().GetCurrentUser().UserGuid);
            operater.LastSelectedEmailTemplateType = emailTemplate.Type.EmailTemplateTypeGuid;
            Resolve<IUserRepository>().UpdateUser(operater);
        }

        public EmailTemplate SetDefaultGuidForEmailTemplate(EmailTemplate needSetDefaultEntity, CloneProgramParameterModel cloneParameterModel)
        {
            EmailTemplate newEntity = new EmailTemplate();

            newEntity = needSetDefaultEntity;
            switch (cloneParameterModel.source)
            {
                case DefaultGuidSourceEnum.FromDefaultGuid:
                    //newEntity.DefaultGUID = needSetDefaultEntity.DefaultGUID;//This is default value, don't need assign again.
                    break;
                case DefaultGuidSourceEnum.FromMatchDefaultGuidFunction:
                    bool isMatchDefaultGuidSuccessful = false;//whether default guid can be matched, if not ,set null.
                    Guid fromEmailTemplateGuid = newEntity.ParentEmailTemplateGUID == null ? Guid.Empty : (Guid)newEntity.ParentEmailTemplateGUID;
                    EmailTemplate fromEmailTemplateEntity = Resolve<IEmailTemplateRepository>().GetEmailTemplate(fromEmailTemplateGuid);
                    if (fromEmailTemplateEntity != null)
                    {
                        if (!fromEmailTemplateEntity.ProgramReference.IsLoaded) fromEmailTemplateEntity.ProgramReference.Load();
                        Program fromProgramInDefaultLanguage = new Program();
                        if (fromEmailTemplateEntity.Program.DefaultGUID.HasValue)//This fromProgram is not default program
                        {
                            fromProgramInDefaultLanguage = Resolve<IProgramRepository>().GetProgramByGuid((Guid)fromEmailTemplateEntity.Program.DefaultGUID);
                        }
                        else//This fromProgram is default program. Or this is wrong data for old Data Version or some other reason.
                        {
                            fromProgramInDefaultLanguage = Resolve<IProgramRepository>().GetProgramByGuid(fromEmailTemplateEntity.Program.ProgramGUID);
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
                                    if (!fromEmailTemplateEntity.EmailTemplateTypeReference.IsLoaded) fromEmailTemplateEntity.EmailTemplateTypeReference.Load();
                                    EmailTemplate toDefaultEmailTemplate = Resolve<IEmailTemplateRepository>().GetByProgramEmailTemplateType(toProgramInDefaultLanguage.ProgramGUID, fromEmailTemplateEntity.EmailTemplateType.EmailTemplateTypeGUID);
                                    newEntity.DefaultGUID = toDefaultEmailTemplate.EmailTemplateGUID;
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
                    newEntity.DefaultGUID = needSetDefaultEntity.ParentEmailTemplateGUID;
                    break;
                default:
                    throw new Exception("Error: Dont't find the DefaultGuidSource for EmailTemplate Entity.");
                    //break;
            }

            return newEntity;
        }

        public EmailTemplate CloneEmailTemplate(EmailTemplate template)
        {
            try
            {
                if (!template.EmailTemplateTypeReference.IsLoaded)
                {
                    template.EmailTemplateTypeReference.Load();
                }
                EmailTemplate emailTemplate = new EmailTemplate
                {
                    EmailTemplateGUID = Guid.NewGuid(),
                    EmailTemplateType = template.EmailTemplateType,
                    Name = template.Name,
                    Subject = template.Subject,
                    Body = template.Body,
                    LastUpdated = DateTime.UtcNow,
                    ParentEmailTemplateGUID = template.EmailTemplateGUID,
                    DefaultGUID = template.DefaultGUID,
                    IsDeleted=template.IsDeleted,
                    LinkText = template.LinkText
                };

                return emailTemplate;
            }
            catch (Exception ex)
            {
                Ethos.Utility.LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }
    }
}
