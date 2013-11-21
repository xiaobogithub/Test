using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Contracts;
using Ethos.DependencyInjection;
using System.Data.Services.Client;
using System.Web.Configuration;

namespace ChangeTech.Services
{
    public class AzureTableService:ServiceBase, IAzureTableService
    {
        public DataServiceContext GetAzureTableServiceContext()
        {
            string accountName = Resolve<ISystemSettingService>().GetSettingValue("AzureStorageAccountName");
            Uri serviceUri = new Uri(ServiceUtility.GetTablePath(accountName));
            DataServiceContext context = new DataServiceContext(serviceUri);
            
            return context;
        }
    }
}
