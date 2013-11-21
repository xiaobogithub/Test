using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ChangeTech.Entities;

namespace ChangeTech.IDataRepository
{
    public interface IEmailTemplateRepository
    {
        List<EmailTemplate> GetAllEmailTemplate();
        EmailTemplate GetEmailTemplate(Guid TemplateGuid);
        EmailTemplate GetByProgramEmailTemplateType(Guid ProgramGuid, Guid EmailTemplateTypeGuid);
        void Insert(EmailTemplate template);
        void Update(EmailTemplate template);
        // so far, not use
        // Wang Jun 2011/08/02
        void Delete(Guid TemplateGuid);
        void DeleteEmailTemplateOfProgram(Guid programGuid);
        IQueryable<EmailTemplate> GetEmailTemplateList(Guid programGuid);
        EmailTemplateTypeContent GetEmailTemplateTypeContent(Guid TemplateGuid);
    }
}