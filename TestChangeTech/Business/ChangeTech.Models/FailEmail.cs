using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChangeTech.Models
{
    public class FailEmailModel
    {
        public Guid EmailGuid { get; set; }
        public string EmailTo { get; set; }
        public string EmailSubject { get; set; }
        public string EmailContent { get; set; }
        public string ExceptionContent { get; set; }
    }
}
