using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChangeTech.Models
{
    public class OrderEmailTemplateModel
    {
        public Guid OrderEmailTemplateGUID { get; set; }
        public Guid EmailTemplateTypeGUID { get; set; }
        public Guid LanguageGUID { get; set; }
        public string Name { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public Guid LastUpdatedBy { get; set; }
        public DateTime LastUpdated { get; set; }
        public bool IsDeleted { get; set; }
    }
}
