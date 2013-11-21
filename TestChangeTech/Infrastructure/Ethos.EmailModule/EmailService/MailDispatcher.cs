using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ethos.EmailModule.EmailService
{
    public class MailDispatcher
    {
        private MailService _mailService;
        private List<EMail> _mails;

        public event EventHandler<MailEventArgs> BeforeMailSend;
        public event EventHandler<MailEventArgs> AfterMailSend;
        public event EventHandler<MailEventArgs> MailFailed;

        public MailDispatcher()
        {
            _mailService = new MailService();
            this.Initialize();
        }

        public MailDispatcher(int resendTimes)
        {
            _mailService = new MailService(resendTimes);
            this.Initialize();
        }

        public virtual void Initialize()
        {
            _mailService.BeforeMailSend += new EventHandler<MailEventArgs>(manager_BeforeMailSend);
            _mailService.AfterMailSend += new EventHandler<MailEventArgs>(manager_AfterMailSend);
            _mailService.MailFailed += new EventHandler<MailEventArgs>(manager_MailFailed);
            _mails = new List<EMail>();
        }


        /// <summary>
        /// Sync
        /// </summary>
        public void Process()
        {
            _mailService.Send(_mails);
        }

        //the method may be wrong,don't use it
        public void ProcessAsync()
        {
            _mailService.SendAsync(_mails);
        }
        
        public void AsyncProcess()
        {
            _mailService.AsyncSend(_mails);
        }

        public void Add(EMail email)
        {
            _mails.Add(email);
        }

        protected void manager_MailFailed(object sender, MailEventArgs e)
        {
            EventHandler<MailEventArgs> handler = MailFailed;
            if (handler != null)
            {
                handler(sender, e);
            }
        }

        protected void manager_AfterMailSend(object sender, MailEventArgs e)
        {
            EventHandler<MailEventArgs> handler = AfterMailSend;
            if (handler != null)
            {
                handler(sender, e);
            }
        }

        protected void manager_BeforeMailSend(object sender, MailEventArgs e)
        {
            EventHandler<MailEventArgs> handler = BeforeMailSend;
            if (handler != null)
            {
                handler(sender, e);
            }
        }
    }
}
