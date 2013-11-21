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
                DateTime lastCheckTime = DateTime.Now;

                do
                {
                    IContainerContext context = ContainerManager.GetContainer("container");
                    context.Resolve<IProgramUserService>().SendEmailToUser();

                    Trace.WriteLine(string.Format("Finish send reminder: {0}", DateTime.Now));
                    Trace.WriteLine(string.Format("Next time will start at {0}", lastCheckTime.AddMinutes(60 - lastCheckTime.Minute)));
                    lastCheckTime = DateTime.Now;
                    Thread.Sleep((60 - lastCheckTime.Minute) * 60000);
                }
                while (true);
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Read();
            }         
        }

        public override bool OnStart()
        {
            // Set the maximum number of concurrent connections 
            ServicePointManager.DefaultConnectionLimit = 12;

            DiagnosticMonitor.Start("DiagnosticsConnectionString");

            // For information on handling configuration changes
            // see the MSDN topic at http://go.microsoft.com/fwlink/?LinkId=166357.
            RoleEnvironment.Changing += RoleEnvironmentChanging;

            return base.OnStart();
        }

        private void RoleEnvironmentChanging(object sender, RoleEnvironmentChangingEventArgs e)
        {
            // If a configuration setting is changing
            if (e.Changes.Any(change => change is RoleEnvironmentConfigurationSettingChange))
            {
                // Set e.Cancel to true to restart this role instance
                e.Cancel = true;
            }
        }
    }
}
