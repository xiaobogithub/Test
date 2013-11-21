using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Services.Client;

namespace ChangeTech.Contracts
{
    public interface IAzureTableService
    {
        DataServiceContext GetAzureTableServiceContext();
    }
}
