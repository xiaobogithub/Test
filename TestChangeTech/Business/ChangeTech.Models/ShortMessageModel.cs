using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChangeTech.Models
{
    public class ShortMessageModel
    {
        public string Text { get; set; }
        public DateTime SendDateTime { get; set; }
        public string MobileNo { get; set; }
        public Guid UserGUID { get; set; }
        public Guid ProgramGUID { get; set; }
        public Guid SessionGUID { get; set; }
    }

    public class MessageModel
    {
        public Guid ProgramGUID { get; set; }
        public string Text { get; set; }
        public string Type { get; set; }
        public Guid MessageGUID { get; set; }
    }

    public class GetMessageModel
    {
        public string sMobileNumber { get; set; }
        public string sMessage { get; set; }
        public string sOrigrinator { get; set; }
        public string sForeignID { get; set; }
        public string sUser { get; set; }
        public string sPass { get; set; }
    }

    public class ShortMessageQueueModel
    {
        public string Email { get; set; }
        public string MobilePhone { get; set; }
        public string Message { get; set; }
        public string SendDate { get; set; }
        public bool IsSent { get; set; }
    }

    public class SMSRecordModel
    {
        public string ProgramShortName { get; set; }
        public string UserMobile { get; set; }
    }

    public class DailySMSContentModel
    {
        public Guid ProgramDailySMSGuid { get; set; }
        public Guid SessionGuid { get; set; }
        public string SessionNum { get; set; } //just for get and display, so string is better than int.
        public string SessionDescription { get; set; }
        public string DailySMSContent { get; set; }
    }
}
