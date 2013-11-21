using System.Linq;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure;
using System;
using System.Diagnostics;

namespace ChangeTech.DeveloperWeb
{
    public class WebRole : RoleEntryPoint
    {
        public override bool OnStart()
        {
            //DiagnosticMonitor.Start("DiagnosticsConnectionString");

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

            //Trace.Listeners.Add(new Microsoft.WindowsAzure.Diagnostics.DiagnosticMonitorTraceListener());
            this.ConfigureDiagnosticMonitor();

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

        /// <summary> 
        /// Performs initial configurarion for Windows Azure Diagnostics for the instance.
        /// </summary> 
        private void ConfigureDiagnosticMonitor()
        {
            DiagnosticMonitorConfiguration diagnosticMonitorConfiguration = DiagnosticMonitor.GetDefaultInitialConfiguration();

            diagnosticMonitorConfiguration.Directories.ScheduledTransferPeriod = TimeSpan.FromMinutes(1d);
            diagnosticMonitorConfiguration.Directories.BufferQuotaInMB = 100;

            diagnosticMonitorConfiguration.Logs.ScheduledTransferPeriod = TimeSpan.FromMinutes(1d);
            diagnosticMonitorConfiguration.Logs.ScheduledTransferLogLevelFilter = LogLevel.Verbose;

            diagnosticMonitorConfiguration.WindowsEventLog.DataSources.Add("Application!*");
            diagnosticMonitorConfiguration.WindowsEventLog.DataSources.Add("System!*");
            diagnosticMonitorConfiguration.WindowsEventLog.ScheduledTransferPeriod = TimeSpan.FromMinutes(1d);

            PerformanceCounterConfiguration performanceCounterConfiguration = new PerformanceCounterConfiguration();
            performanceCounterConfiguration.CounterSpecifier = @"\Processor(_Total)\% Processor Time";
            performanceCounterConfiguration.SampleRate = System.TimeSpan.FromSeconds(10d);
            diagnosticMonitorConfiguration.PerformanceCounters.DataSources.Add(performanceCounterConfiguration);
            diagnosticMonitorConfiguration.PerformanceCounters.ScheduledTransferPeriod = TimeSpan.FromMinutes(1d);

            DiagnosticMonitor.Start("Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString", diagnosticMonitorConfiguration);
        }
    }
}
