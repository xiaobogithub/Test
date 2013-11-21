using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Entities;
using ChangeTech.IDataRepository;

namespace ChangeTech.SQLServerRepository
{
    public class OrderEmailTemplateRepository : RepositoryBase, IOrderEmailTemplateRepository
    {
        public void Insert(OrderEmailTemplate oetModel)
        {
            InsertEntity(oetModel);
        }
        public void Update(OrderEmailTemplate oetModel)
        {
            UpdateEntity(oetModel);
        }

        public OrderEmailTemplate GetOrderEmailTemplateByOrderEmailTemplateGuid(Guid orderEmailTemplateGuid)
        {
            return GetEntities<OrderEmailTemplate>().Where(oet => oet.OrderEmailTemplateGUID == orderEmailTemplateGuid && (!oet.IsDeleted.HasValue || oet.IsDeleted.HasValue && oet.IsDeleted.Value == false)).FirstOrDefault();
        }

        public OrderEmailTemplate GetOrderEmailTemplateByEmailTemplateTypeGuidAndLanguageGuid(Guid emailTemplateTypeGuid, Guid languageGuid)
        {
            return GetEntities<OrderEmailTemplate>().Where(oet => oet.EmailTemplateTypeGUID == emailTemplateTypeGuid && oet.LanguageGUID == languageGuid && (!oet.IsDeleted.HasValue || oet.IsDeleted.HasValue && oet.IsDeleted.Value == false)).FirstOrDefault();
        }
    }
}
