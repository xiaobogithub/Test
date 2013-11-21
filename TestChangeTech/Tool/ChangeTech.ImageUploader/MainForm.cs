using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ChangeTech.Contracts;
using ChangeTech.Models;
using Ethos.DependencyInjection;
using System.IO;
using System.Drawing.Drawing2D;
using System.Configuration;
using Ethos.Utility;

namespace ChangeTech.ImageUploader
{
    public partial class MainForm : Form
    {
        private BackgroundWorker _uploadWorkder;
        private Guid _imageCategoryGuid;
        private string _type;
        private string _currentUploadingFile;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            GetImageCategories();
            typeComboBox.SelectedIndex = 0;
        }

        private void GetImageCategories()
        {
            ResourceCategoriesModel resourceCategoriesModel = ClientInstance.ContaionerContext.Resolve<IResourceCategoryService>().GetAllCategory();
            categoryNameComboBox.DataSource = resourceCategoriesModel.Categories;
            categoryNameComboBox.DisplayMember = "CategoryName";
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = openFileDialog.ShowDialog();
            switch (dialogResult)
            {
                case DialogResult.OK:
                    //filesDataGridView.Rows.Clear();

                    foreach (string fileName in openFileDialog.FileNames)
                    {
                        filesDataGridView.Rows.Add(Path.GetFileName(fileName), fileName, "Pending");
                        ClientInstance.FilesToUploadQueue.Enqueue(fileName);
                    }
                    break;
                default:
                    break;
            }
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            filesDataGridView.Rows.Clear();
            ClientInstance.FilesToUploadQueue.Clear();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Disable()
        {
            categoryNameComboBox.Enabled = false;
            typeComboBox.Enabled = false;
            browseButton.Enabled = false;
            clearButton.Enabled = false;
            startButton.Enabled = false;
        }

        private void Enable()
        {
            categoryNameComboBox.Enabled = true;
            typeComboBox.Enabled = true;
            browseButton.Enabled = true;
            clearButton.Enabled = true;
            startButton.Enabled = true;
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            if (ClientInstance.FilesToUploadQueue.Count > 0)
            {
                if (_uploadWorkder == null)
                {
                    _uploadWorkder = new BackgroundWorker();
                    _uploadWorkder.DoWork += new DoWorkEventHandler(_uploadWorkder_DoWork);
                    _uploadWorkder.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_uploadWorkder_RunWorkerCompleted);
                    _uploadWorkder.ProgressChanged += new ProgressChangedEventHandler(_uploadWorkder_ProgressChanged);
                }

                if (!_uploadWorkder.IsBusy)
                {
                    Disable();

                    totalFileCountLabel.Text = ClientInstance.FilesToUploadQueue.Count.ToString();
                    leftFileCountLabel.Text = ClientInstance.FilesToUploadQueue.Count.ToString();

                    _imageCategoryGuid = ((ResourceCategoryModel)categoryNameComboBox.SelectedItem).CategoryGuid;
                    _type = typeComboBox.SelectedItem.ToString();

                    _uploadWorkder.RunWorkerAsync();
                }
                else
                {
                    MessageBox.Show("Please wait for current process to complete.");
                }
            }
            else
            {
                MessageBox.Show("No files is selected to upload.");
            }
        }

        private void _uploadWorkder_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        private void _uploadWorkder_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Enable();
        }

        private void _uploadWorkder_DoWork(object sender, DoWorkEventArgs e)
        {
            while (ClientInstance.FilesToUploadQueue.Count > 0)
            {
                string fileName = ClientInstance.FilesToUploadQueue.Dequeue();
                _currentUploadingFile = fileName;

                StartUploadNewResource();
                Guid resourceGUID = Guid.NewGuid();

                LogUtility.LogUtilityIntance.LogMessgae(string.Format("ResouceGUID:{0}, FileName{1}, Type:{2}", resourceGUID, fileName, _type));

                switch(_type)
                {
                    case "Image":
                        File.Copy(fileName, ConfigurationManager.AppSettings["ResouceRootDirectory"] + @"ProgramImages\Original\" + resourceGUID + Path.GetExtension(fileName));
                        Image image = Image.FromFile(fileName);
                        FormatOutputImage(image, 40, 40, resourceGUID, Path.GetExtension(fileName));
                        break;
                    case "Audio":
                        File.Copy(fileName, ConfigurationManager.AppSettings["ResouceRootDirectory"] + @"Video\" + resourceGUID + Path.GetExtension(fileName));
                        break;
                    case "Video":
                        File.Copy(fileName, ConfigurationManager.AppSettings["ResouceRootDirectory"] + @"Audio\" + resourceGUID + Path.GetExtension(fileName));
                        break;
                }

                ClientInstance.ContaionerContext.Resolve<IResourceService>().SaveResource(resourceGUID, _imageCategoryGuid, Path.GetFileName(fileName), _type);

                FinishUploadNewResource();
            }
        }

        private void StartUploadNewResource()
        {
            Invoke(new MethodInvoker(UpdateUploadStatusOnStart));
        }

        private void FinishUploadNewResource()
        {
            Invoke(new MethodInvoker(UpdateUploadStatusOnFinish));
        }

        private void UpdateUploadStatusOnStart()
        {
            currentFileLabel.Text = Path.GetFileName(_currentUploadingFile);
            foreach (DataGridViewRow row in filesDataGridView.Rows)
            {
                if (Convert.ToString(row.Cells["PathColumn"].Value).Equals(_currentUploadingFile))
                {
                    row.Cells["StatusColumn"].Value = "Uploading";
                    break;
                }
            }
        }

        private void UpdateUploadStatusOnFinish()
        {
            //if (ClientInstance.FilesToUploadQueue.Count > 0)
            //{
                leftFileCountLabel.Text = ClientInstance.FilesToUploadQueue.Count.ToString();
            //}

            foreach (DataGridViewRow row in filesDataGridView.Rows)
            {
                if (Convert.ToString(row.Cells["PathColumn"].Value).Equals(_currentUploadingFile))
                {
                    row.Cells["StatusColumn"].Value = "Uploaded";
                    break;
                }
            }
        }

        private void FormatOutputImage(Image image, int width, int height, Guid fileGuid, string fileName)
        {
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
                        //bitmap.Save(Response.OutputStream, ImageFormat.Jpeg);
                        CreateNewImageFile(fileGuid, fileName, bitmap);
                    }
                }
            }
        }

        private void CreateNewImageFile(Guid fileGuid, string fileName, Image image)
        {
            image.Save(ConfigurationManager.AppSettings["ResouceRootDirectory"] + @"ProgramImages\CutDown\" + fileGuid + fileName);
        }
    }
}
