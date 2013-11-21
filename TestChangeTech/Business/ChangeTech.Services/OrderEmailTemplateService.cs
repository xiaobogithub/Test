using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Models;
using Ethos.DependencyInjection;
using ChangeTech.Contracts;
using ChangeTech.Entities;
using ChangeTech.IDataRepository;

namespace ChangeTech.Services
{
    public class OrderEmailTemplateService : ServiceBase, IOrderEmailTemplateService
    {
        public void InsertOrderEmailTemplateModel(OrderEmailTemplateModel oetModel)
        {
            OrderEmailTemplate oetEntity = new OrderEmailTemplate
            {
                OrderEmailTemplateGUID = oetModel.OrderEmailTemplateGUID,
                EmailTemplateTypeGUID = oetModel.EmailTemplateTypeGUID,
                LanguageGUID = oetModel.LanguageGUID,
                Name = oetModel.Name,
                Subject = oetModel.Subject,
                Body = oetModel.Body,
                LastUpdated = oetModel.LastUpdated,
                LastUpdatedBy = oetModel.LastUpdatedBy,
                IsDeleted = null
            };
            Resolve<IOrderEmailTemplateRepository>().Insert(oetEntity);
        }
        public void UpdateOrderEmailTemplateModel(OrderEmailTemplateModel oetModel)
        {
            OrderEmailTemplate oetEntity = Resolve<IOrderEmailTemplateRepository>().GetOrderEmailTemplateByOrderEmailTemplateGuid(oetModel.OrderEmailTemplateGUID);
            
                //OrderEmailTemplateGUID = oetModel.OrderEmailTemplateGUID,
                oetEntity.EmailTemplateTypeGUID = oetModel.EmailTemplateTypeGUID;
                oetEntity.LanguageGUID = oetModel.LanguageGUID;
                oetEntity.Name = oetModel.Name;
                oetEntity.Subject = oetModel.Subject;
                oetEntity.Body = oetModel.Body;
                oetEntity.LastUpdated = oetModel.LastUpdated;
                oetEntity.LastUpdatedBy = oetModel.LastUpdatedBy;
                oetEntity.IsDeleted = oetModel.IsDeleted;
                Resolve<IOrderEmailTemplateRepository>().Update(oetEntity);
        }
        public OrderEmailTemplateModel GetOrderEmailTemplateByEmailTemplateTypeGuidAndLanguageGuid(Guid emailTemplateTypeGuid, Guid languageGuid)
        {
            OrderEmailTemplate oetEntity = Resolve<IOrderEmailTemplateRepository>().GetOrderEmailTemplateByEmailTemplateTypeGuidAndLanguageGuid(emailTemplateTypeGuid, languageGuid);
            OrderEmailTemplateModel oetModel = null;
            if (oetEntity != null)
            {
                oetModel = new OrderEmailTemplateModel
                {
                    OrderEmailTemplateGUID = oetEntity.OrderEmailTemplateGUID,
                    EmailTemplateTypeGUID = oetEntity.EmailTemplateTypeGUID.Value,
                    LanguageGUID = oetEntity.LanguageGUID.Value,
                    Name = oetEntity.Name,
                    Subject = oetEntity.Subject,
                    Body = oetEntity.Body,
                    LastUpdated = oetEntity.LastUpdated.Value,
                    LastUpdatedBy = oetEntity.LastUpdatedBy.Value
                };
            }

            return oetModel;
        }
    }
}
