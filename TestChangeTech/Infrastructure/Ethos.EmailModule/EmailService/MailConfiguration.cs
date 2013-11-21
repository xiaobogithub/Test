using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ethos.EmailModule.EmailService
{
    public class MailConfiguration
    {
        public static string MailServer { get; set; }
        public static int Port { get; set; }
        public static bool EnableSSL { get; set; }
        public static string UserName { get; set; }
        public static string Password { get; set; }
        // Need absolute path
        public static string LogFileDirectory { get; set; }
        public static string LogFileName { get; set; }
    }
}
