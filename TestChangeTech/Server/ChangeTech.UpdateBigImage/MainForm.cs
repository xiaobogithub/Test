using System;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ChangeTech.Contracts;
using ChangeTech.Models;
using ChangeTech.Services;
using Ethos.DependencyInjection;
using Ethos.Utility;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;
using System.Drawing.Imaging;

namespace ChangeTech.UpdateBigImage
{
    public partial class MainForm : Form
    {
        private CloudBlobClient client;
        private BackgroundWorker processWorker;
        private string resourceCategory;
        private int resourceCount;
        private int finishedCount;
        private string resouce;
        private string azureAccountName;
        private string azureAccountKey;
        private IContainerContext context;

        public MainForm()
        {
            InitializeComponent();
            context = ContainerManager.GetContainer("container");
        }

        private void InitializeBlobService()
        {
            azureAccountName = context.Resolve<ISystemSettingService>().GetSettingValue("AzureStorageAccountName");
            azureAccountKey = context.Resolve<ISystemSettingService>().GetSettingValue("AzureStorageAccountKey");

            StorageCredentialsAccountAndKey scak = new StorageCredentialsAccountAndKey(azureAccountName, azureAccountKey);
            CloudStorageAccount account = new CloudStorageAccount(scak, false);
            client = account.CreateCloudBlobClient();
            client.Timeout = new TimeSpan(0, 10, 0);
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            okButton.Enabled = false;
            processWorker = new BackgroundWorker();
            processWorker.WorkerReportsProgress = true;
            processWorker.ProgressChanged += new ProgressChangedEventHandler(processWorker_ProgressChanged);
            processWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(processWorker_RunWorkerCompleted);
            processWorker.DoWork += new DoWorkEventHandler(processWorker_DoWork);
            processWorker.RunWorkerAsync();
        }

        void processWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            InitializeBlobService();

            try
            {
                ResourceCategoriesModel rcs = context.Resolve<IResourceCategoryService>().GetAllCategory();
                foreach (ResourceCategoryModel rc in rcs.Categories)
                {
                    resourceCategory = rc.CategoryName;

                    ResourceListModel rs = context.Resolve<IResourceService>().GetResourceNameByCategoryGuid(rc.CategoryGuid, ResourceType.Image);
                    resourceCount = rs.Resources.Count;
                    finishedCount = 0;
                    foreach (ResourceModel rm in rs.Resources)
                    {
                        resouce = rm.Name;
                        try
                        {
                            //if (!rm.HasProcessed)
                            //{
                            //    CloudBlockBlob blob = client.GetBlockBlob(string.Format("{0}originalimagecontainer/{1}", ServiceUtility.GetBlobPath(azureAccountName), rm.NameOnServer));

                            //    MemoryStream ms = new MemoryStream();
                            //    blob.DownloadToStream(ms);
                            //    Image image = Image.FromStream(ms);

                            //    // Bigger image
                            //    if (image.Width > 1000)
                            //    {
                            //        // Create normal image resource model
                            //        Guid newresourceguid = Guid.NewGuid();                                    
                            //        // Resize image and save to azure blob
                            //        Stream normalImageStream = ImageUtility.GetResizeImageFile(ms, 550);
                            //        context.Resolve<IResourceService>().SaveResourceToAzureBlobStorage(normalImageStream, newresourceguid.ToString(), rm.Name, rm.Type, rm.ResourceCategoryGUID);

                            //        context.Resolve<IResourceService>().UpdateResource(rm.ID, PictureMode.Big.ToString(), newresourceguid);
                            //    }
                            //    // Normal image
                            //    else
                            //    {
                            //        context.Resolve<IResourceService>().UpdateResource(rm.ID, PictureMode.Normal.ToString(), Guid.Empty);
                            //    }
                            //}               

                            if (rm.Extension.Equals(".png"))
                            {
                                CloudBlockBlob blob = client.GetBlockBlob(string.Format("{0}originalimagecontainer/{1}", ServiceUtility.GetBlobPath(azureAccountName), rm.NameOnServer));

                                MemoryStream ms = new MemoryStream();
                                blob.DownloadToStream(ms);
                                Image image = Image.FromStream(ms);

                                if (image.Height == 550)
                                {
                                    context.Resolve<IResourceService>().ConvertResourceToJpg(rm, image);
                                    LogUtility.LogUtilityIntance.LogMessgae(rm.ID.ToString());
                                }                                
                            }
                        }
                        catch (Exception se)
                        {
                            LogUtility.LogUtilityIntance.LogException(se, rm.ID.ToString());
                        }
                        finally
                        {
                            finishedCount++;
                            processWorker.ReportProgress(0);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, "");
            }
        }

        void processWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            finishCountLbl.Text = finishedCount.ToString();
            if (!InvokeRequired)
            {
                okButton.Enabled = true;
            }
        }

        void processWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (!InvokeRequired)
            {
                resourceCategoryLbl.Text = resourceCategory;
                totalCountLbl.Text = resourceCount.ToString();
                finishCountLbl.Text = finishedCount.ToString();
                resourceLstBox.Items.Add(resouce);
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }
}
