using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Models;

namespace ChangeTech.Contracts
{
    public interface IOrderEmailTemplateService
    {
        void InsertOrderEmailTemplateModel(OrderEmailTemplateModel oetModel);
        void UpdateOrderEmailTemplateModel(OrderEmailTemplateModel oetModel);
        OrderEmailTemplateModel GetOrderEmailTemplateByEmailTemplateTypeGuidAndLanguageGuid(Guid emailTemplateTypeGuid, Guid languageGuid);
    }
}
