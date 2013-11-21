using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ChangeTech.Entities;
using ChangeTech.IDataRepository;

namespace ChangeTech.SQLServerRepository
{
    public class EmailTemplateRepository : RepositoryBase, IEmailTemplateRepository
    {
        public List<EmailTemplate> GetAllEmailTemplate()
        {
            return GetEntities<EmailTemplate>().OrderBy(i => i.EmailTemplateGUID).ToList<EmailTemplate>();
        }

        public EmailTemplate GetEmailTemplate(Guid TemplateGuid)
        {
            return GetEntities<EmailTemplate>(i => i.EmailTemplateGUID == TemplateGuid && 
                (i.IsDeleted.HasValue? i.IsDeleted.Value == false: true)).FirstOrDefault<EmailTemplate>();
        }

        // might have problem if one program have more than one email template for one email template type, so far get the latest one if we find more
        public EmailTemplate GetByProgramEmailTemplateType(Guid ProgramGuid, Guid EmailTemplateTypeGuid)
        {
            return GetEntities<EmailTemplate>(i => i.Program.ProgramGUID == ProgramGuid
                && i.EmailTemplateType.EmailTemplateTypeGUID == EmailTemplateTypeGuid && 
                (i.IsDeleted.HasValue?i.IsDeleted.Value == false: true)).OrderByDescending(e=>e.LastUpdated).FirstOrDefault<EmailTemplate>();
        }

        public void Insert(EmailTemplate template)
        {
            InsertEntity(template);
        }

        public void Update(EmailTemplate template)
        {
            UpdateEntity(template);
        }

        public void Delete(Guid TemplateGuid)
        {
            DeleteEntity<EmailTemplate>(i => i.EmailTemplateGUID == TemplateGuid, Guid.Empty);
        }

        public void DeleteEmailTemplateOfProgram(Guid programGuid)
        {
            DeleteEntity<EmailTemplate>(i => i.Program.ProgramGUID == programGuid, Guid.Empty);
        }

        public IQueryable<EmailTemplate> GetEmailTemplateList(Guid programGuid)
        {
            return GetEntities<EmailTemplate>(e => e.Program.ProgramGUID == programGuid);
        }

        public EmailTemplateTypeContent GetEmailTemplateTypeContent(Guid TemplateGuid)
        {
            return GetEntities<EmailTemplateTypeContent>().Where(ec => ec.EmailTemplateType.EmailTemplateTypeGUID == TemplateGuid).FirstOrDefault();
        }
    }
}
