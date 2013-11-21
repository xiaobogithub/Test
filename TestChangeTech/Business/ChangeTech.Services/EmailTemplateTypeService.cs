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
    public class EmailTemplateTypeService : ServiceBase, IEmailTemplateTypeService
    {
        public EmailTemplateTypesModel GetAll()
        {
            EmailTemplateTypesModel models = new EmailTemplateTypesModel();
            List<EmailTemplateTypeModel> emailTemplateTypeList = new List<EmailTemplateTypeModel>();
            List<EmailTemplateType> list = Resolve<IEmailTemplateTypeRepository>().GetAllEmailTemplateType();
            foreach (EmailTemplateType model in list)
            {
                emailTemplateTypeList.Add(new EmailTemplateTypeModel
                {
                    EmailTemplateTypeGuid = model.EmailTemplateTypeGUID,
                    Name = model.Name,
                    Description = model.Description,
                    Type = model.Type.Value
                });
            }
            //models.EmailTemplateTypeList = emailTemplateTypeList;
            models.EmailTemplateTypeList = emailTemplateTypeList.Where(ett => ett.Type == Convert.ToInt32(EmailTemplateTypeEnum.ProgramEmail)).ToList();
            
            User currentUser = Resolve<IUserRepository>().GetUserByGuid(Resolve<IUserService>().GetCurrentUser().UserGuid);
            if (currentUser.LastSelectedEmailTemplateType != null)
                models.LastSelectedEmailTemplateTypeGuid = currentUser.LastSelectedEmailTemplateType.Value;
            return models;
        }

        public EmailTemplateTypeModel GetItem(Guid TypeGuid)
        {
            EmailTemplateType model = Resolve<IEmailTemplateTypeRepository>().GetEmailTemplateType(TypeGuid);
            EmailTemplateTypeModel item = new EmailTemplateTypeModel
            {
                EmailTemplateTypeGuid = model.EmailTemplateTypeGUID,
                Name = model.Name,
                Description = model.Description,
                Type=model.Type.Value
            };
            return item;
        }

        public EmailTemplateTypeContentModel GetEmailTemplateTypeContentByTypeGuid(Guid typeGuid)
        {
            EmailTemplateTypeContent emailTemplateTypeContentEntity = Resolve<IEmailTemplateTypeRepository>().GetEmailTemplateTypeContent(typeGuid);
            EmailTemplateTypeContentModel emailContentModel =null;
            if (emailTemplateTypeContentEntity != null)
            {
                emailContentModel = new EmailTemplateTypeContentModel();
                if (!emailTemplateTypeContentEntity.EmailTemplateTypeReference.IsLoaded) emailTemplateTypeContentEntity.EmailTemplateTypeReference.Load();
                emailContentModel.EmailTemplateTypeGuid = emailTemplateTypeContentEntity.EmailTemplateType.EmailTemplateTypeGUID;
                emailContentModel.EmailTemplateTypeContentGuid = emailTemplateTypeContentEntity.EmailTemplateTypeContentGUID;
                emailContentModel.HtmlContent = emailTemplateTypeContentEntity.HtmlContent;
            }

            return emailContentModel;
        }

        public EmailTemplateTypeModel GetEmailTemplateTypeByTypeID(int emailTypeId)
        {
            EmailTemplateType ettEntity = Resolve<IEmailTemplateTypeRepository>().GetEmailTemplateTypeByTypeID(emailTypeId);
            EmailTemplateTypeModel ettModel = null;
            if (ettEntity!=null)
            {
                ettModel = new EmailTemplateTypeModel
                {
                    EmailTemplateTypeGuid = ettEntity.EmailTemplateTypeGUID,
                    Name = ettEntity.Name,
                    Description = ettEntity.Description,
                    Type = ettEntity.Type.Value
                };
            }
            return ettModel;
        }

        public EmailTemplateTypeModel GetEmailTemplateTypeByEmailTemplateTypeGuid(Guid emailTypeTemplateGuid)
        {
            EmailTemplateType ettEntity = Resolve<IEmailTemplateTypeRepository>().GetEmailTemplateTypeByEmailTemplateTypeGuid(emailTypeTemplateGuid);
            EmailTemplateTypeModel ettModel = null;
            if (ettEntity != null)
            {
                ettModel = new EmailTemplateTypeModel
                {
                    EmailTemplateTypeGuid = ettEntity.EmailTemplateTypeGUID,
                    Name = ettEntity.Name,
                    Description = ettEntity.Description,
                    Type = ettEntity.Type.Value
                };
            }
            return ettModel;
        }
    }
}
