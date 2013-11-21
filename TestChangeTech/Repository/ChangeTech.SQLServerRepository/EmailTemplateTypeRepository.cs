using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ChangeTech.Entities;
using ChangeTech.IDataRepository;

namespace ChangeTech.SQLServerRepository
{
    public class EmailTemplateTypeRepository : RepositoryBase, IEmailTemplateTypeRepository
    {
        public List<EmailTemplateType> GetAllEmailTemplateType()
        {
            return GetEntities<EmailTemplateType>().OrderBy(i => i.EmailTemplateTypeGUID).ToList<EmailTemplateType>();
        }

        public EmailTemplateType GetEmailTemplateType(Guid typeGuid)
        {
            return GetEntities<EmailTemplateType>(i => i.EmailTemplateTypeGUID == typeGuid).FirstOrDefault<EmailTemplateType>();
        }

        public EmailTemplateTypeContent GetEmailTemplateTypeContent(Guid typeGuid)
        {
            return GetEntities<EmailTemplateTypeContent>(e => e.EmailTemplateType.EmailTemplateTypeGUID == typeGuid).FirstOrDefault();
        }

        public EmailTemplateType GetEmailTemplateTypeByEmailTemplateTypeGuid(Guid emailTypeTemplateGuid)
        {
            return GetEntities<EmailTemplateType>(e => e.EmailTemplateTypeGUID == emailTypeTemplateGuid).FirstOrDefault();
        }

        public EmailTemplateType GetEmailTemplateTypeByTypeID(int emailTypeId)
        {
            return GetEntities<EmailTemplateType>(e => e.Type == emailTypeId).FirstOrDefault();
        }

        public Guid GetEmailTemplateTypeGuidByName(string name)
        {
            return GetEntities<EmailTemplateType>(e => e.Name == name).FirstOrDefault().EmailTemplateTypeGUID;
        }
    }
}
