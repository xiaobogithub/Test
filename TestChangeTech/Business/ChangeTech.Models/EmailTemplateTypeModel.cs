using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChangeTech.Models
{
    public class EmailTemplateTypeModel
    {
        public Guid EmailTemplateTypeGuid { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public int Type { get; set; }
    }

    public class EmailTemplateTypesModel
    {
        public Guid LastSelectedEmailTemplateTypeGuid { get; set; }
        public List<EmailTemplateTypeModel> EmailTemplateTypeList { get; set; }
    }

    public class EmailTemplateTypeContentModel
    {
        public Guid EmailTemplateTypeContentGuid { get; set; }
        public Guid EmailTemplateTypeGuid { get; set; }
        public string HtmlContent { get; set; }
    }
}
