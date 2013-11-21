using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChangeTech.Models
{
    public class EmailTemplateModel
    {
        public Guid EmailTemplateGuid { get; set; }
        //public LanguageModel Language { get; set; }
        public ProgramModel Program { get; set; }
        public EmailTemplateTypeModel Type { get; set; }
        public String Name { get; set; }
        public String Subject { get; set; }
        public String Body { get; set; }
        public string LinkText { get; set; }
    }

    public class EmailTemplatesModel : List<EmailTemplateModel>
    { }

    public class ReminderEmailInfoModel
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        public string ToAddress { get; set; }
        //public string ToName{get{return this.ToAddress;}}
        public string FromAddress { get; set; }
        public string FromName { get; set; }
        public Guid ProgramGuid { get; set; }
        public Guid UserGuid { get; set; }
        public LogTypeEnum ReminderType { get; set; }
        public string Password { get; set; }
    }
}
