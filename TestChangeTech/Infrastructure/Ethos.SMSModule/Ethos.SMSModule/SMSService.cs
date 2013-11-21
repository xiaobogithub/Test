using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ethos.SMSModule
{
    public class SMSService
    {
        public static bool SendSM(string sMobileNumber, string sMessage, string sOriginator, string sForeignID, string sUser, string sPass)
        {
            ChangeTechSMS.SystorSMSSoapClient client = new Ethos.SMSModule.ChangeTechSMS.SystorSMSSoapClient();
            return client.SendMessageToMobileNotify(sMobileNumber, sMessage, sOriginator, sForeignID, sUser, sPass);
        }
    }
}
