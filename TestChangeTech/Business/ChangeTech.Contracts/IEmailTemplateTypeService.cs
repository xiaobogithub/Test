using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ChangeTech.Models;

namespace ChangeTech.Contracts
{
    public interface IEmailTemplateTypeService
    {
        EmailTemplateTypesModel GetAll();
        EmailTemplateTypeModel GetItem(Guid TypeGuid);
        EmailTemplateTypeContentModel GetEmailTemplateTypeContentByTypeGuid(Guid typeGuid);
        EmailTemplateTypeModel GetEmailTemplateTypeByEmailTemplateTypeGuid(Guid emailTypeTemplateGuid);
        EmailTemplateTypeModel GetEmailTemplateTypeByTypeID(int emailTypeId);
    }
}
