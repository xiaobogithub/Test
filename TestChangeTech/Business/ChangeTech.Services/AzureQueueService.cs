using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ethos.DependencyInjection;
using ChangeTech.Contracts;
using ChangeTech.IDataRepository;
using Microsoft.WindowsAzure.StorageClient;
using Microsoft.WindowsAzure;
using System.Web.Configuration;

namespace ChangeTech.Services
{
    public class AzureQueueService : ServiceBase, IAzureQueueService
    {
        /// <summary>
        /// GetCloudQueue
        /// </summary>
        /// <param name="queueName">if this is queueName but not URI, it should be a valid dns name. eg. 3-63 characters, lower case etc.</param>
        /// <returns></returns>
        public CloudQueue GetCloudQueue(string queueName)
        {
            //if queueName refers to the URI of the queue, this length is not right
            //if (!string.IsNullOrEmpty(queueName) && queueName.Length > 63)
            //{
            //    queueName = queueName.Substring(queueName.Length - 63);
            //}
            string accountName = Resolve<ISystemSettingRepository>().GetSettingValue("AzureStorageAccountName");
            string accountKey = Resolve<ISystemSettingRepository>().GetSettingValue("AzureStorageAccountKey");
            StorageCredentialsAccountAndKey securityKey = new StorageCredentialsAccountAndKey(accountName,
                accountKey);

            //CloudStorageAccount account = CloudStorageAccount.DevelopmentStorageAccount;
            CloudStorageAccount account = new CloudStorageAccount(securityKey, false);
            CloudQueueClient client = account.CreateCloudQueueClient();
            CloudQueue queue = client.GetQueueReference(queueName);
            return queue;
        }

        public CloudQueueMessage AddQueueMessage(CloudQueue queue, string message, bool clearBeforeAdd)
        {
            queue.CreateIfNotExist();
            if (clearBeforeAdd)
            {
                queue.Clear();
            }
            IEnumerable<CloudQueueMessage> messagesInQueue = queue.PeekMessages(32);// remove 32 messages for this new message to be got earlier.
            CloudQueueMessage queueMessageToInsert = new CloudQueueMessage(message);
            queue.AddMessage(queueMessageToInsert, new TimeSpan(1, 0, 0));
            return queueMessageToInsert;
        }


        public CloudQueueMessage AddQueueMessageWithoutClearAndPeek(CloudQueue queue, string message)
        {
            queue.CreateIfNotExist();
            CloudQueueMessage queueMessageToInsert = new CloudQueueMessage(message);
            queue.AddMessage(queueMessageToInsert, new TimeSpan(3, 0, 0));
            return queueMessageToInsert;
        }
    }
}
