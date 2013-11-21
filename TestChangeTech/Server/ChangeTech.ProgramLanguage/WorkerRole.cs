using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.StorageClient;

using Ethos.DependencyInjection;
using ChangeTech.Contracts;
using System.Threading;
using ChangeTech.Models;
using ChangeTech.Services;

namespace ChangeTech.LanServer
{
    public class WorkerRole : RoleEntryPoint
    {
        public override void Run()
        {
            Trace.WriteLine(string.Format("Program language service, start to listen: {0}", DateTime.Now));
            IContainerContext context = ContainerManager.GetContainer("container");

            do
            {
                ServiceUtility.ResetObjectContext();
                CloudQueue operationQueue = context.Resolve<IAzureQueueService>().GetCloudQueue("operationqueue");

                try
                {                   
                    if (operationQueue != null && operationQueue.Exists())
                    {
                        CloudQueueMessage cloudQueueMessage = operationQueue.PeekMessage();
                        if (cloudQueueMessage != null)
                        {
                            string operationMsg = cloudQueueMessage.AsString;
                            string[] operationParameter = operationMsg.Split(new char[] { ';' });
                            switch (operationParameter[0])
                            {
                                case "AddProgramLanguage":
                                    context.Resolve<IProgramService>().AddLanguageForProgram(
                                        new Guid(operationParameter[1]),
                                        new Guid(operationParameter[2]),
                                        new Guid(operationParameter[3]));
                                    break;
                                case "RemoveProgramLanguage":
                                    context.Resolve<IProgramLanguageService>().RemoveProgramLanguage(
                                        new Guid(operationParameter[1]),
                                        new Guid(operationParameter[2]));
                                    break;
                                case "ExportProgram":
                                    context.Resolve<IExportService>().ExportProgram(
                                        operationParameter[3],
                                        new Guid(operationParameter[1]),
                                        new Guid(operationParameter[2]),
                                        Convert.ToInt32(operationParameter[4]),
                                        Convert.ToInt32(operationParameter[5]),
                                        Convert.ToBoolean(operationParameter[6]),
                                        Convert.ToBoolean(operationParameter[7]),
                                        Convert.ToBoolean(operationParameter[8]),
                                        Convert.ToBoolean(operationParameter[9]),
                                        Convert.ToBoolean(operationParameter[10]),
                                        Convert.ToBoolean(operationParameter[11]),
                                        Convert.ToBoolean(operationParameter[12]),
                                        Convert.ToBoolean(operationParameter[13]));
                                    break;
                                case "ReportProgram":
                                    context.Resolve<IExportService>().ReportProgram(operationParameter[3], new Guid(operationParameter[1]), new Guid(operationParameter[2]));
                                    break;
                                case "ProgramUserVariable":
                                    context.Resolve<IExportService>().ExportUserPageVariable(operationParameter[2], new Guid(operationParameter[1]));
                                    break;
                            }
                        }
                    }                    
                }
                catch (Exception ex)
                {
                    CloudQueue statusQueue = context.Resolve<IAzureQueueService>().GetCloudQueue("errorqueue");
                    string statusMsg = string.Format(ex.ToString());
                    context.Resolve<IAzureQueueService>().AddQueueMessage(statusQueue, statusMsg, false);
                    Trace.WriteLine("Exception occur: " + ex.ToString());
                }
                finally
                {
                    if (operationQueue != null && operationQueue.Exists())
                    {
                        CloudQueueMessage cloudQueueMessage = operationQueue.GetMessage();
                        if (cloudQueueMessage != null)
                        {
                            operationQueue.DeleteMessage(cloudQueueMessage);
                        }
                    }
                    else {
                        if (!operationQueue.Exists())
                        {
                            operationQueue.Create();
                        }
                    }
                    Thread.Sleep(10000);
                }
            }
            while (true);
        }

        public override bool OnStart()
        {
            // For information on handling configuration changes
            // see the MSDN topic at http://go.microsoft.com/fwlink/?LinkId=166357.
            RoleEnvironment.Changing += RoleEnvironmentChanging;

            // This code sets up a handler to update CloudStorageAccount instances when their corresponding
            // configuration settings change in the service configuration file.

            CloudStorageAccount.SetConfigurationSettingPublisher((configName, configSetter) =>
            {
                // Provide the configSetter with the initial value
                configSetter(RoleEnvironment.GetConfigurationSettingValue(configName));

                RoleEnvironment.Changed += (s, arg) =>
                {
                    if (arg.Changes.OfType<RoleEnvironmentConfigurationSettingChange>()
                        .Any((change) => (change.ConfigurationSettingName == configName)))
                    {
                        // The corresponding configuration setting has changed, propagate the value
                        if (!configSetter(RoleEnvironment.GetConfigurationSettingValue(configName)))
                        {
                            // In this case, the change to the storage account credentials in the
                            // service configuration is significant enough that the role needs to be
                            // recycled in order to use the latest settings. (for example, the 
                            // endpoint has changed)
                            RoleEnvironment.RequestRecycle();
                        }
                    }
                };
            });

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
