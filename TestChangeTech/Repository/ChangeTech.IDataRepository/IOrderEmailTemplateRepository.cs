using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Entities;

namespace ChangeTech.IDataRepository
{
    public interface IOrderEmailTemplateRepository
    {
        void Insert(OrderEmailTemplate oetModel);
        void Update(OrderEmailTemplate oetModel);
        OrderEmailTemplate GetOrderEmailTemplateByOrderEmailTemplateGuid(Guid orderEmailTemplateGuid);
        OrderEmailTemplate GetOrderEmailTemplateByEmailTemplateTypeGuidAndLanguageGuid(Guid emailTemplateTypeGuid, Guid languageGuid);
    }
}
