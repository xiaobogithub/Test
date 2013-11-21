using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.WindowsAzure.StorageClient;

namespace ChangeTech.Contracts
{
    public interface IAzureQueueService
    {
        CloudQueue GetCloudQueue(string queueName);
        CloudQueueMessage AddQueueMessage(CloudQueue queue, string message, bool clearBeforeAdd);
        CloudQueueMessage AddQueueMessageWithoutClearAndPeek(CloudQueue queue, string message);
    }
}
