using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.StorageClient;
using System.Configuration;
using System.IO;
using ChangeTech.Models;
using Ethos.Utility;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace ChangeTech.ResourceMigrateServer
{
    public partial class MainForm : Form
    {
        private CloudBlobClient client;
        private string resourceType;
        private int totalCount;
        private int uploadedCount;
        private BackgroundWorker uploadWorker;
        private string[] files;
        public MainForm()
        {
            InitializeComponent();
        }

        private void InitializeBlobService()
        {
            StorageCredentialsAccountAndKey scak = new StorageCredentialsAccountAndKey(ConfigurationManager.AppSettings["BlobAccountName"], ConfigurationManager.AppSettings["BlobAcccountPassword"]);
            CloudStorageAccount account = new CloudStorageAccount(scak, false);
            client = account.CreateCloudBlobClient();
            client.Timeout = new TimeSpan(0,10,0);
        }

        private void browseBtn_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fd = new FolderBrowserDialog();
            fd.ShowDialog();

            folderPathTxtBox.Text = fd.SelectedPath;
        }

        private void startBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(folderPathTxtBox.Text))
            {
                MessageBox.Show("Please specify the folder path.");
            }
            else if (resourceTypeComboBox.SelectedItem == null)
            {
                MessageBox.Show("Please choose the resource type.");
            }
            else
            {
                files = Directory.GetFiles(folderPathTxtBox.Text);
                resourceType = resourceTypeComboBox.SelectedItem.ToString();
                uploadedCount = 0;
                totalCount = files.Count();
                totalCountNOLbl.Text = totalCount.ToString();
                uploadedFilesNOLbl.Text = uploadedCount.ToString();
                uploadedFilesLstBox.Items.Clear();
                Disable();
                if (files != null && files.Length > 0)
                {
                    UploadFile();
                }
                else
                {
                    MessageBox.Show("No files need to upload under this folder.");
                }
                //foreach (string file in files)
                //{
                    //CloudBlob cloudBlob = GetContainer().GetBlobReference(Path.GetFileName(file));
                    //cloudBlob.Properties.ContentType = contentTypeComboBox.SelectedItem.ToString();
                    //cloudBlob.UploadFile(file);
                    
                //}
            }
        }

        private void Disable()
        {
            resourceTypeComboBox.Enabled = false;
            startBtn.Enabled = false;
            browseBtn.Enabled = false;
            folderPathTxtBox.Enabled = false;
            browseBtn.Enabled = false;
        }

        private void Enable()
        {
            resourceTypeComboBox.Enabled = true;
            startBtn.Enabled = true;
            browseBtn.Enabled = true;
            folderPathTxtBox.Enabled = true;
            browseBtn.Enabled = true;
        }

        private void UploadFile()
        {
            if ((File.GetAttributes(files[uploadedCount]) & FileAttributes.Hidden) == 0)
            {
                uploadWorker = new BackgroundWorker();
                uploadWorker.WorkerReportsProgress = true;
                uploadWorker.DoWork += new DoWorkEventHandler(bw_DoWork);
                uploadWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
                uploadWorker.ProgressChanged += new ProgressChangedEventHandler(uploadWorker_ProgressChanged);
                uploadWorker.RunWorkerAsync(files[uploadedCount]);
            }
            else
            {
                uploadedFilesLstBox.Items.Add(string.Format("Skip hidden file : {0}, {1}", files[uploadedCount], DateTime.Now));
                uploadedCount++;
                uploadedFilesNOLbl.Text = uploadedCount.ToString();
                if (uploadedCount < totalCount)
                {
                    UploadFile();
                }
                else
                {
                    Enable();
                }
            }
        }

        void uploadWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (!InvokeRequired)
            {
                progressNOLbl.Text = e.ProgressPercentage.ToString() + " %, (" + e.UserState.ToString() + " bytes)";
            }
        }

        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
           if (!InvokeRequired)
            {
                uploadedCount++;
                uploadedFilesNOLbl.Text = uploadedCount.ToString();
                uploadedFilesLstBox.Items.Add(string.Format("Successfuly upload: {0}, {1}", e.Result, DateTime.Now));
                if (uploadedCount == totalCount)
                {
                    Enable();
                }
                else
                {
                    UploadFile();
                }
            }
            else
            {

            }
        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {   
            SaveResource(e.Argument.ToString());
            e.Result = e.Argument;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            InitializeBlobService();
        }

        public CloudBlobContainer GetContainer(BlobContainerType containerType)
        {
            return client.GetContainerReference(containerType.ToString().ToLower());
        }

        public void SaveResource(string filePath)
        {
            // Create a blob in container and upload image bytes to it
            CloudBlob blob = null;
            
            switch (resourceType)
            {
                case "Audio":
                    blob = GetContainer(BlobContainerType.AudioContainer).GetBlobReference(Path.GetFileName(filePath));
                    blob.Properties.ContentType = FileUtility.GetMIMEType(Path.GetExtension(filePath));
                    //blob.UploadFile(filePath);
                    UploadFileData(filePath, blob);
                    break;
                case "Video":
                    blob = GetContainer(BlobContainerType.VideoContainer).GetBlobReference(Path.GetFileName(filePath));
                    blob.Properties.ContentType = FileUtility.GetMIMEType(Path.GetExtension(filePath));
                    //blob.UploadFile(filePath);
                    UploadFileData(filePath, blob);
                    break;
                case "Image":
                    // Upload image with thumnail size
                    blob = GetContainer(BlobContainerType.ThumnailContainer).GetBlobReference(Path.GetFileName(filePath));
                    blob.Properties.ContentType = FileUtility.GetMIMEType(Path.GetExtension(filePath));
                    blob.UploadFromStream(CreateThumnailImage(Image.FromFile(filePath), 40, 40));

                    // Upload image with original size
                    blob = GetContainer(BlobContainerType.OriginalImageContainer).GetBlobReference(Path.GetFileName(filePath));
                    blob.Properties.ContentType = FileUtility.GetMIMEType(Path.GetExtension(filePath));
                    //blob.UploadFile(filePath);
                    UploadFileData(filePath, blob);
                    break;
                case "Document":
                    blob = GetContainer(BlobContainerType.DocumentContainer).GetBlobReference(Path.GetFileName(filePath));
                    blob.Properties.ContentType = FileUtility.GetMIMEType(Path.GetExtension(filePath));
                    //blob.UploadFile(filePath);
                    UploadFileData(filePath, blob);
                    break;
                case "Logo":
                    blob = GetContainer(BlobContainerType.LogoContainer).GetBlobReference(Path.GetFileName(filePath));
                    blob.Properties.ContentType = FileUtility.GetMIMEType(Path.GetExtension(filePath));
                    //blob.UploadFile(filePath);
                    UploadFileData(filePath, blob);
                    break;
            }
        }

        private void UploadFileData(string filePath, CloudBlob blob)
        {
            using (FileStream fs = File.OpenRead(filePath))
            {
                using (BlobStream blobStream = blob.OpenWrite())
                {
                    int uploadedBytes = 0;
                    while (true)
                    {
                        byte[] fileData = new byte[20480];
                        int bytesRead = 0;
                        //if (fs.Length > 20480)
                        //{
                            bytesRead = fs.Read(fileData, 0, 20480);
                        //}
                        //else
                        //{
                        //    bytesRead = fs.Read(fileData, 0, (int)fs.Length - 1);
                        //}
                        uploadedBytes += bytesRead;

                        //blob.UploadByteArray(fileData);
                        blobStream.Write(fileData, 0, bytesRead);
                        uploadWorker.ReportProgress((int)(uploadedBytes * 100 / fs.Length), uploadedBytes);
                        if (bytesRead < 20480)
                        {
                            break;
                        }
                    }
                }
            }
        }

        private Stream CreateThumnailImage(Image image, int width, int height)
        {
            Stream thumnailStream = new MemoryStream();
            if (width != 0 && height != 0)
            {
                int thumbnailHeight = height;
                int thumbnailWidth = width;
                int intwidth, intheight;

                if (image.Height > thumbnailHeight)
                {
                    if (image.Width * thumbnailHeight > image.Height * thumbnailWidth)
                    {
                        intwidth = thumbnailWidth;
                        intheight = (image.Height * thumbnailWidth) / image.Width;
                    }
                    else
                    {
                        intheight = thumbnailHeight;
                        intwidth = (image.Width * thumbnailHeight) / image.Height;
                    }
                }
                else
                {
                    if (image.Width > thumbnailWidth)
                    {
                        intwidth = thumbnailWidth;
                        intheight = (image.Height * thumbnailWidth) / image.Width;
                    }
                    else
                    {
                        intwidth = image.Width;
                        intheight = image.Height;
                    }
                }

                //bulid a bitmap image with specific width and height
                using (Bitmap bitmap = new Bitmap(thumbnailWidth, thumbnailHeight))
                {
                    using (Graphics g = Graphics.FromImage(bitmap))
                    {
                        g.Clear(ColorTranslator.FromHtml("#F2F2F2"));
                        g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        g.SmoothingMode = SmoothingMode.HighQuality;
                        //start to draw image
                        g.DrawImage(image, new Rectangle((thumbnailWidth - intwidth) / 2, (thumbnailHeight - intheight) / 2, intwidth, intheight), new Rectangle(0, 0, image.Width, image.Height), GraphicsUnit.Pixel);

                        bitmap.Save(thumnailStream, ImageFormat.Jpeg);
                        //CreateNewImageFile(fileGuid, fileName, bitmap);
                    }
                }
            }
            thumnailStream.Position = 0;
            return thumnailStream;
        }
    }
}
