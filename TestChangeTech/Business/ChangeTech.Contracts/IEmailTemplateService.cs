using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ChangeTech.Models;
using ChangeTech.Entities;

namespace ChangeTech.Contracts
{
    public interface IEmailTemplateService
    {
        EmailTemplatesModel GetAll();
        EmailTemplateModel GetByProgramEmailTemplageType(Guid ProgramGuid ,Guid EmailTemplateTypeGuid);
        void Insert(EmailTemplateModel emailTemplate);
        void Update(EmailTemplateModel emailTemplate);
        EmailTemplate SetDefaultGuidForEmailTemplate(EmailTemplate needSetDefaultEntity, CloneProgramParameterModel cloneParameterModel);
        EmailTemplate CloneEmailTemplate(EmailTemplate template);
    }
}
