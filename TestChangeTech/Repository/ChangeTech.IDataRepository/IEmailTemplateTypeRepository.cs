using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ChangeTech.Entities;

namespace ChangeTech.IDataRepository
{
    public interface IEmailTemplateTypeRepository
    {
        List<EmailTemplateType> GetAllEmailTemplateType();
        EmailTemplateType GetEmailTemplateType(Guid typeGUID);
        EmailTemplateTypeContent GetEmailTemplateTypeContent(Guid TypeGuid);
        EmailTemplateType GetEmailTemplateTypeByEmailTemplateTypeGuid(Guid emailTypeTemplateGuid);
        EmailTemplateType GetEmailTemplateTypeByTypeID(int emailTypeId);
        Guid GetEmailTemplateTypeGuidByName(string name);
    }
}
