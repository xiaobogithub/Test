using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using Ethos.EmailModule.TemplateEngine;

namespace Ethos.EmailModule.EmailService
{
    public class EMail : MailMessage
    {
        public object UserToken { get; set; }
    }
}
