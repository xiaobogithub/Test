using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Ethos.EmailModule.EmailService
{
    public class MailManager
    {
        public static event EventHandler<MailEventArgs> BeforeSend;
        public static event EventHandler<MailEventArgs> AfterSend;
        public static event EventHandler<MailEventArgs> SendFailed;

        public static int _maxResendTimes;

        public static void Send(EMail mail)
        {
            //if (mail == null)
            //{
            //    throw new ArgumentNullException("mail");
            //}
            MailEventArgs e = new MailEventArgs(mail, MailActionEnum.Waiting);
            OnBeforeSend(mail, e);
            try
            {
                SmtpClient client = new SmtpClient(MailConfiguration.MailServer, 25);
                if (!string.IsNullOrEmpty(MailConfiguration.UserName))
                {
                    client.Credentials = new NetworkCredential(MailConfiguration.UserName, MailConfiguration.Password);
                }
                //client.Port = MailConfiguration.Port;
                client.EnableSsl = true;

                client.Send(mail);
                OnAfterSend(mail, e);

                Log(mail, null);
            }
            catch (Exception ex)
            {
                e.Error = ex;
                Log(mail, ex);
                Trace.TraceError(string.Format("Send Mail Error:{0}::{1}", DateTime.UtcNow, ex.ToString()));
                OnSendFailed(mail, e);
            }
            finally
            {
                mail.Dispose();
                mail = null;
            }
        }


        public static void Send(List<EMail> mails)
        {
            SmtpClient client = new SmtpClient(MailConfiguration.MailServer, 25);
            if (!string.IsNullOrEmpty(MailConfiguration.UserName))
            {
                client.Credentials = new NetworkCredential(MailConfiguration.UserName, MailConfiguration.Password);
            }
            //client.Port = MailConfiguration.Port;
            client.EnableSsl = true;

            foreach (EMail mail in mails)
            {
                int round = 0;
                do
                {
                    round++;

                    MailEventArgs e = new MailEventArgs(mail, MailActionEnum.Waiting);
                    OnBeforeSend(mail, e);
                    try
                    {
                        // test begin
                        //if (mail.From.Address.Contains("kaizhilin@163.com"))
                        //{
                        //    throw new Exception("test"); 
                        //}
                        // test end.

                        client.Send(mail);
                        OnAfterSend(mail, e);

                        Log(mail, null);
                        mail.Dispose();
                        break;
                    }
                    catch (Exception ex)
                    {
                        e.Error = ex;
                        Log(mail, ex);
                        Trace.TraceError(string.Format("Send Mail Error:{0}::{1}", DateTime.UtcNow, ex.ToString()));
                        OnSendFailed(mail, e);
                    }
                    //finally
                    //{
                    //    mail.Dispose();
                    //    mail = null;
                    //}
                }
                while (round <= _maxResendTimes);
            }
        }

        //the method may be wrong,don't use it
        public static void SendAsync(EMail mail)
        {
            ThreadPool.QueueUserWorkItem(
                (obj) =>
                {
                    Send(mail);
                }
                );

            //ParallelOptions options = new ParallelOptions { 
            //};
        }

        //async send email
        public static void AsyncSend(List<EMail> mails)
        {
            DateTime currentTime = DateTime.UtcNow;

            //Thread count 
            int coreCount = 10;
            //mail count 
            int nCount = mails.Count;
            int sendCount = nCount;
            //mail sent count per a thread 
            int batchSize = nCount / coreCount;

            int pending = coreCount;
            using (var mre = new ManualResetEvent(false))
            {
                for (int batchCount = 0; batchCount < coreCount; batchCount++)
                {
                    int lower = batchCount * batchSize;
                    int upper = (batchCount == coreCount - 1) ? nCount : lower + batchSize;
                    ThreadPool.QueueUserWorkItem(st =>
                    {

                        for (int nI = lower; nI < upper; nI++)
                        {
                            EMail mail = mails[nI];
                            OperatorEmail operatorEmail = new OperatorEmail();
                            operatorEmail.BeforeSend += new EventHandler<MailEventArgs>(MailManager.BeforeSend);
                            operatorEmail.AfterSend += new EventHandler<MailEventArgs>(MailManager.AfterSend);
                            operatorEmail.SendFailed += new EventHandler<MailEventArgs>(MailManager.SendFailed);
                            operatorEmail.SendEmail(mail);
                        }
                        if (Interlocked.Decrement(ref pending) == 0)
                            mre.Set();
                    });
                } 
                mre.WaitOne();
            }

        }

        private static string MergeLog(EMail mail, Exception ex)
        {
            var sb = new StringBuilder();
            sb.AppendFormat("[{0}]", DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"));
            sb.AppendLine();
            sb.AppendFormat("STATUS: {0}", ex == null ? "SUCCESS" : "FAILED");
            sb.AppendLine();
            sb.AppendFormat("FROM: {0}", mail.From.Address);
            sb.AppendLine();
            sb.Append("TO: ");
            foreach (var to in mail.To)
            {
                sb.AppendFormat("{0}, ", to.Address);
            }
            sb.AppendLine();
            sb.AppendFormat("SUBJECT: {0}", mail.Subject);
            sb.AppendLine();
            sb.AppendLine("BODY:");
            sb.AppendLine("-- start --");
            sb.AppendLine(mail.Body);
            sb.AppendLine("--  end  --");
            if (ex != null)
            {
                sb.AppendLine("EXCEPTION:");
                sb.AppendLine("-- start --");
                sb.AppendLine(ex.ToString());
                sb.AppendLine("--  end  --");
            }
            return sb.ToString();
        }

        private static void Log(EMail mail, Exception ex)
        {
            FileStream fs = null;
            StreamWriter sw = null;
            try
            {
                if (!string.IsNullOrEmpty(MailConfiguration.LogFileDirectory))
                {
                    if (!Directory.Exists(MailConfiguration.LogFileDirectory))
                    {
                        Directory.CreateDirectory(MailConfiguration.LogFileDirectory);
                    }
                }
                else
                {
                    MailConfiguration.LogFileDirectory = AppDomain.CurrentDomain.BaseDirectory;
                }

                if (string.IsNullOrEmpty(MailConfiguration.LogFileName))
                {
                    MailConfiguration.LogFileName = "Email.Log";
                }
                fs = new FileStream(Path.Combine(MailConfiguration.LogFileDirectory, MailConfiguration.LogFileName), FileMode.Append, FileAccess.Write, FileShare.Write);
                sw = new StreamWriter(fs);
                sw.WriteLine(MergeLog(mail, ex));
            }
            catch
            {
                // Absorb all excpetions occurred here.
            }
            finally
            {
                if (sw != null)
                {
                    sw.Close();
                }
                if (fs != null)
                {
                    fs.Close();
                }
            }
        }


        protected static void OnBeforeSend(object sender, MailEventArgs e)
        {
            EventHandler<MailEventArgs> handler = BeforeSend;
            if (handler != null)
            {
                handler(sender, e);
            }
        }
        protected static void OnAfterSend(object sender, MailEventArgs e)
        {
            EventHandler<MailEventArgs> handler = AfterSend;
            if (handler != null)
            {
                handler(sender, e);
            }


        }
        protected static void OnSendFailed(object sender, MailEventArgs e)
        {
            EventHandler<MailEventArgs> handler = SendFailed;
            if (handler != null)
            {
                handler(sender, e);
            }
        }
    }

    public enum EmailStatus
    {
        INITIAL = 0,
        SENDEMAIL = 1,
        AFTERSEND = 2,
        LOG = 3
    }

    /// <summary>
    /// the class services to async sending email
    /// </summary>
    public class OperatorEmail
    {
        public event EventHandler<MailEventArgs> BeforeSend;
        public event EventHandler<MailEventArgs> AfterSend;
        public event EventHandler<MailEventArgs> SendFailed;

        public void SendEmail(EMail mail)
        {
            int done = 0;
            EmailStatus mailStatus = EmailStatus.INITIAL;
            while (done < MailManager._maxResendTimes)
            {
                SmtpClient client = new SmtpClient(MailConfiguration.MailServer, 25);

                if (!string.IsNullOrEmpty(MailConfiguration.UserName))
                {
                    client.Credentials = new NetworkCredential(MailConfiguration.UserName, MailConfiguration.Password);
                }
                client.EnableSsl = true;


                MailEventArgs e = new MailEventArgs(mail, MailActionEnum.Waiting);
                OnBeforeSend(mail, e);
                try
                {
                    client.Send(mail);
                    mailStatus = EmailStatus.SENDEMAIL;
                    OnAfterSend(mail, e);
                    mailStatus = EmailStatus.AFTERSEND;
                    Log(mail, null);
                    mailStatus = EmailStatus.LOG;
                    mail.Dispose();
                    break;
                }
                catch (Exception ex)
                {
                    e.Error = ex;
                    if (mailStatus == EmailStatus.INITIAL) //need to resend
                    {
                        Log(mail, ex);
                        Trace.TraceError(string.Format("Send Asynchronously Mail Error:{0}::{1}", DateTime.UtcNow, ex.ToString()));
                        OnSendFailed(mail, e);
                    }
                    else  //send email successfully and throw a exception in log
                    {
                        done = MailManager._maxResendTimes; //the while is completed
                        Log(mail, ex);
                        Trace.TraceError(string.Format("After Send Asynchronously Mail Error:{0}::{1}", DateTime.UtcNow, ex.ToString()));
                        OnSendFailed(mail, e);
                    }
                }
                done++;
            }

        }

        protected void OnBeforeSend(object sender, MailEventArgs e)
        {
            EventHandler<MailEventArgs> handler = BeforeSend;
            if (handler != null)
            {
                handler(sender, e);
            }
        }
        protected void OnAfterSend(object sender, MailEventArgs e)
        {
            EventHandler<MailEventArgs> handler = AfterSend;
            if (handler != null)
            {
                handler(sender, e);
            }


        }
        protected void OnSendFailed(object sender, MailEventArgs e)
        {
            EventHandler<MailEventArgs> handler = SendFailed;
            if (handler != null)
            {
                handler(sender, e);
            }
        }

        private string MergeLog(EMail mail, Exception ex)
        {
            var sb = new StringBuilder();
            sb.AppendFormat("[{0}]", DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"));
            sb.AppendLine();
            sb.AppendFormat("STATUS: {0}", ex == null ? "SUCCESS" : "FAILED");
            sb.AppendLine();
            sb.AppendFormat("FROM: {0}", mail.From.Address);
            sb.AppendLine();
            sb.Append("TO: ");
            foreach (var to in mail.To)
            {
                sb.AppendFormat("{0}, ", to.Address);
            }
            sb.AppendLine();
            sb.AppendFormat("SUBJECT: {0}", mail.Subject);
            sb.AppendLine();
            sb.AppendLine("BODY:");
            sb.AppendLine("-- start --");
            sb.AppendLine(mail.Body);
            sb.AppendLine("--  end  --");
            if (ex != null)
            {
                sb.AppendLine("EXCEPTION:");
                sb.AppendLine("-- start --");
                sb.AppendLine(ex.ToString());
                sb.AppendLine("--  end  --");
            }
            return sb.ToString();
        }

        private void Log(EMail mail, Exception ex)
        {
            FileStream fs = null;
            StreamWriter sw = null;
            try
            {
                if (!string.IsNullOrEmpty(MailConfiguration.LogFileDirectory))
                {
                    if (!Directory.Exists(MailConfiguration.LogFileDirectory))
                    {
                        Directory.CreateDirectory(MailConfiguration.LogFileDirectory);
                    }
                }
                else
                {
                    MailConfiguration.LogFileDirectory = AppDomain.CurrentDomain.BaseDirectory;
                }

                if (string.IsNullOrEmpty(MailConfiguration.LogFileName))
                {
                    MailConfiguration.LogFileName = "Email.Log";
                }
                fs = new FileStream(Path.Combine(MailConfiguration.LogFileDirectory, MailConfiguration.LogFileName), FileMode.Append, FileAccess.Write, FileShare.Write);
                sw = new StreamWriter(fs);
                sw.WriteLine(MergeLog(mail, ex));
            }
            catch
            {
                // Absorb all excpetions occurred here.
            }
            finally
            {
                if (sw != null)
                {
                    sw.Close();
                }
                if (fs != null)
                {
                    fs.Close();
                }
            }
        }



    }
}
