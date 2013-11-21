using System;

namespace Ethos.EmailModule.EmailService
{
    public class MailEventArgs : EventArgs
    {
        public MailEventArgs(EMail message)
            : this(message, MailActionEnum.Sending)
        {
        }

        public MailEventArgs(EMail message, MailActionEnum action)
            : this(message, action, null)
        {
        }

        public MailEventArgs(EMail message, MailActionEnum action, Exception error)
        {
            this.Message = message;
            this.Action = action;
            this.Error = error;
        }

        public EMail Message { get; set; }
        public MailActionEnum Action { get; set; }
        public Exception Error { get; set; }
    }
}
