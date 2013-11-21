using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;

using ChangeTech.Contracts;
using Ethos.DependencyInjection;
using ChangeTech.Services;
namespace ChangeTech.MailServer
{
    public class WorkerRole : RoleEntryPoint
    {
        public override void Run()
        {
            // This is a sample worker implementation. Replace with your logic.
            Trace.WriteLine("ChangeTech.MailServer entry point called", "Information");
            Trace.WriteLine(string.Format("Start to send reminder: {0}", DateTime.Now));

            try
            {
                DateTime emailCheckTime = DateTime.MinValue;
                DateTime shortMessageLastCheckTime = DateTime.MinValue;
                DateTime cutConnectLastCheckTime = DateTime.MinValue;
                do
                {
                    ServiceUtility.ResetObjectContext();

                    IContainerContext context = ContainerManager.GetContainer("container");
                    if(emailCheckTime == DateTime.MinValue || emailCheckTime <= DateTime.Now)
                    {
                        context.Resolve<IProgramUserService>().SendEmailToUser(emailCheckTime);
                        emailCheckTime = DateTime.Now.AddMinutes(60 - DateTime.Now.Minute);                        
                    }
                    if(shortMessageLastCheckTime == DateTime.MinValue || shortMessageLastCheckTime.AddMinutes(15) <= DateTime.Now)
                    {
                        shortMessageLastCheckTime = DateTime.Now;
                        context.Resolve<IShortMessageService>().SendShortMessageQueue();
                    }
                    if(cutConnectLastCheckTime == DateTime.MinValue || cutConnectLastCheckTime.AddDays(1) <= DateTime.Now)
                    {
                        cutConnectLastCheckTime = DateTime.Now;
                        context.Resolve<IProgramUserService>().CutConnection();
                    }
                    //Trace.WriteLine(string.Format("Finish send reminder: {0}", DateTime.Now));
                    //Trace.WriteLine(string.Format("Next time will start at {0}", lastCheckTime.AddMinutes(60 - lastCheckTime.Minute)));
                    //lastCheckTime = DateTime.Now;
                    //Thread.Sleep((60 - lastCheckTime.Minute) * 60000);
                    //if((DateTime.Now - lastCheckTime).Minutes < 15)
                    //{
                    //    Thread.Sleep((15 - (DateTime.Now - lastCheckTime).Minutes) * 60000);
                    //}
                    Thread.Sleep(5 * 60000);
                }
                while(true);
            }
            catch(Exception ex)
            {
                //Console.Write(ex.ToString());
                //Console.Read();
            }
        }

        public override bool OnStart()
        {
            // Set the maximum number of concurrent connections 
            ServicePointManager.DefaultConnectionLimit = 12;

            //DiagnosticMonitor.Start("DiagnosticsConnectionString");

            // For information on handling configuration changes
            // see the MSDN topic at http://go.microsoft.com/fwlink/?LinkId=166357.
            RoleEnvironment.Changing += RoleEnvironmentChanging;

            return base.OnStart();
        }

        private void RoleEnvironmentChanging(object sender, RoleEnvironmentChangingEventArgs e)
        {
            // If a configuration setting is changing
            if(e.Changes.Any(change => change is RoleEnvironmentConfigurationSettingChange))
            {
                // Set e.Cancel to true to restart this role instance
                e.Cancel = true;
            }
        }
    }
}
