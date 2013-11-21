using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Ethos.EmailModule.EmailService;
using Ethos.EmailModule.TemplateEngine;
using System.Net.Mail;

namespace ChangeTech.Tests
{
    public class EMailModuleTests
    {
        [Fact]
        public void TestSendEmail_Success()
        {
            MailDispatcher mailDispater = new MailDispatcher();
            MailConfiguration.UserName = "";
            EMail email = new EMail();
            email.Subject = "Test Email";
            email.Body = "Hi, This is a test email";
            email.Sender = new MailAddress("difujie@ethos.com.cn");
            email.To.Add(new MailAddress("pchen@ethos.com.cn"));
            mailDispater.Add(email);
            mailDispater.Process();
            Assert.True(1 == 1);
        }

        [Fact]
        public void TestBuildTemplate()
        {
            string text = "Hi $Name$, welcome you.";
            Dictionary<string, string> paramters = new Dictionary<string, string>();
            paramters.Add("Name", "Chen Pu");
            text = TemplateService.Build(text, paramters);
            Assert.True(text.Equals("Hi Chen Pu, welcome you."));
        }
    }
}
