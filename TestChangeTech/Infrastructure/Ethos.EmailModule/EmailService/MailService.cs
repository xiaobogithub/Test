using System;
using System.Collections.Generic;
using Ethos.DependencyInjection;

namespace Ethos.EmailModule.EmailService
{
    public class MailService : ServiceBase
    {
        public event EventHandler<MailEventArgs> BeforeMailSend;
        public event EventHandler<MailEventArgs> AfterMailSend;
        public event EventHandler<MailEventArgs> MailFailed;

        private const int DEFAULTRESENDTIMES = 0;
        private const int MAXVALIDRESENDTIMES = 10;
        private readonly int _maxResendTimes;

        public MailService()
            : this(DEFAULTRESENDTIMES)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="resendTimes">Max value is 10. Min value is 0. Default value is 0.</param>
        public MailService(int resendTimes)
        {
            this._maxResendTimes = resendTimes > MAXVALIDRESENDTIMES ? MAXVALIDRESENDTIMES : (resendTimes < 0 ? 0 : resendTimes);
        }

        /// <summary>
        /// Now this function will not be executed.
        /// </summary>
        /// <param name="mail"></param>
        public void Send(EMail mail)
        {
            try
            {
                MailManager._maxResendTimes = this._maxResendTimes;
                MailManager.BeforeSend += new EventHandler<MailEventArgs>(OnBeforeMailSend);
                MailManager.AfterSend += new EventHandler<MailEventArgs>(OnAfterMailSend);
                MailManager.SendFailed += new EventHandler<MailEventArgs>(OnMailFailed);

                //Sync
                MailManager.Send(mail);
            }
            catch (Exception ex)
            {
                //e.Error = ex;
                //OnMailFailed(mail, e);
                throw ex;
            }
            finally
            {
                MailManager.BeforeSend -= new EventHandler<MailEventArgs>(OnBeforeMailSend);
                MailManager.AfterSend -= new EventHandler<MailEventArgs>(OnAfterMailSend);
                MailManager.SendFailed -= new EventHandler<MailEventArgs>(OnMailFailed);
            }
        }

        //public void Send(List<EMail> mails)
        //{
        //    foreach (var item in mails)
        //    {
        //        Send(item);
        //    }
        //}
        public void Send(List<EMail> mails)
        {
            try
            {
                MailManager._maxResendTimes = this._maxResendTimes;
                MailManager.BeforeSend += new EventHandler<MailEventArgs>(OnBeforeMailSend);
                MailManager.AfterSend += new EventHandler<MailEventArgs>(OnAfterMailSend);
                MailManager.SendFailed += new EventHandler<MailEventArgs>(OnMailFailed);

                //Sync.... Send   email list
                MailManager.Send(mails);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                MailManager.BeforeSend -= new EventHandler<MailEventArgs>(OnBeforeMailSend);
                MailManager.AfterSend -= new EventHandler<MailEventArgs>(OnAfterMailSend);
                MailManager.SendFailed -= new EventHandler<MailEventArgs>(OnMailFailed);
            }
        }

        public void SendAsync(EMail mail)
        {
            try
            {
                MailManager._maxResendTimes = this._maxResendTimes;
                MailManager.BeforeSend += new EventHandler<MailEventArgs>(OnBeforeMailSend);
                MailManager.AfterSend += new EventHandler<MailEventArgs>(OnAfterMailSend);
                MailManager.SendFailed += new EventHandler<MailEventArgs>(OnMailFailed);

                MailManager.SendAsync(mail);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                MailManager.BeforeSend -= new EventHandler<MailEventArgs>(OnBeforeMailSend);
                MailManager.AfterSend -= new EventHandler<MailEventArgs>(OnAfterMailSend);
                MailManager.SendFailed -= new EventHandler<MailEventArgs>(OnMailFailed);
            }
        }

        //the method may be wrong, don't use it
        public void SendAsync(List<EMail> mails)
        {
            foreach (var item in mails)
            {
                SendAsync(item);
            }
        }

        //async sending email
        public void AsyncSend(List<EMail> mails)
        {
            try
            {
                MailManager._maxResendTimes = this._maxResendTimes;
                MailManager.BeforeSend += new EventHandler<MailEventArgs>(OnBeforeMailSend);
                MailManager.AfterSend += new EventHandler<MailEventArgs>(OnAfterMailSend);
                MailManager.SendFailed += new EventHandler<MailEventArgs>(OnMailFailed);
                //Sync.... Send   email list
                MailManager.AsyncSend(mails);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                MailManager.BeforeSend -= new EventHandler<MailEventArgs>(OnBeforeMailSend);
                MailManager.AfterSend -= new EventHandler<MailEventArgs>(OnAfterMailSend);
                MailManager.SendFailed -= new EventHandler<MailEventArgs>(OnMailFailed);
            }

        }

        protected void OnBeforeMailSend(object sender, MailEventArgs e)
        {
            EventHandler<MailEventArgs> handler = BeforeMailSend;
            if (handler != null)
            {
                handler(sender, e);
            }
        }
        protected void OnAfterMailSend(object sender, MailEventArgs e)
        {
            EventHandler<MailEventArgs> handler = AfterMailSend;
            if (handler != null)
            {
                handler(sender, e);
            }
        }
        protected void OnMailFailed(object sender, MailEventArgs e)
        {
            EventHandler<MailEventArgs> handler = MailFailed;
            if (handler != null)
            {
                handler(sender, e);
            }
        }
    }
}
