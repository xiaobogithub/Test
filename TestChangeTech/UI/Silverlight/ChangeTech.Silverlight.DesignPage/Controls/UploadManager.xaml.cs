using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using ChangeTech.Silverlight.DesignPage.ChangeTechWCFService;
using System.Collections.ObjectModel;
using System.IO;
using ChangeTech.Silverlight.Common;
using System.Windows.Browser;
using System.Threading;
using System.Net.Browser;

namespace ChangeTech.Silverlight.DesignPage.Controls
{
    public partial class UploadManager : UserControl
    {
        public event UpdateResourceListDelegate UpdateResourceListEventHandler;

        private ResourceTypeEnum _resourceType;
        private ObservableCollection<ResourceCategoryModel> _resourceCategories;
        private ObservableCollection<FileInfo> _uploadFiles;
        private ObservableCollection<string> _uploadedFiles;
        private FileInfo _uploadFileInfo;
        private List<Guid> _categoryWithChange;

        public UploadManager()
        {
            InitializeComponent();
        }

        public void Show(ResourceTypeEnum resourceType, ObservableCollection<ResourceCategoryModel> resourceCategories)
        {
            _resourceType = resourceType;
            _resourceCategories = resourceCategories;
            _uploadFiles = new ObservableCollection<FileInfo>();
            _uploadedFiles = new ObservableCollection<string>();
            _categoryWithChange = new List<Guid>();

            ResourceCategroyComboBox.ItemsSource = _resourceCategories;

            switch (_resourceType)
            {
                case ResourceTypeEnum.Audio:
                    ResourceTypeComboBox.SelectedIndex = 3;
                    break;
                case ResourceTypeEnum.Document:
                    ResourceTypeComboBox.SelectedIndex = 1;
                    break;
                case ResourceTypeEnum.Image:
                    ResourceTypeComboBox.SelectedIndex = 0;
                    break;
                case ResourceTypeEnum.Video:
                    ResourceTypeComboBox.SelectedIndex = 2;
                    break;
            }
            //ResourceTypeComboBox.IsEnabled = false;

            ResourcesToUploadListBox.ItemsSource = _uploadFiles;
            ResourcesUploadedListBox.ItemsSource = _uploadedFiles;

            Visibility = Visibility.Visible;
        }

        private void EnableControls()
        {
            BrowseButton.IsEnabled = true;
            StartButton.IsEnabled = true;
            CloseButton.IsEnabled = true;
            ResetButton.IsEnabled = true;
            ClearButton.IsEnabled = true;
            ResourceCategroyComboBox.IsEnabled = true;
            ResourceTypeComboBox.IsEnabled = true;
            MessageTextBlock.Text = string.Empty;
        }

        private void DisableControls(string msg)
        {
            MessageTextBlock.Text = msg;
            BrowseButton.IsEnabled = false;
            StartButton.IsEnabled = false;
            CloseButton.IsEnabled = false;
            ResetButton.IsEnabled = false;
            ClearButton.IsEnabled = false;
            ResourceTypeComboBox.IsEnabled = false;
            ResourceCategroyComboBox.IsEnabled = false;
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            _uploadFiles.Clear();
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            _uploadedFiles.Clear();
        }

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            string filter = string.Empty;
            switch (_resourceType)
            {
                case ResourceTypeEnum.Audio:
                    filter = "Mp3 Files (*.mp3)|*.mp3";
                    break;
                case ResourceTypeEnum.Document:
                    filter = "PDF Files (*.pdf)|*.pdf|Miscrosoft Word Files (*.doc)|*.doc|Microsoft Word 2007 Files (*.docx)|*.docx";
                    break;
                case ResourceTypeEnum.Image:
                    filter = "Jpeg Files (*.jpg)|*.jpg|Png Files (*.png)|*.png";
                    break;
                case ResourceTypeEnum.Video:
                    filter = "Flv Files (*.flv)|*.flv";
                    break;
            }

            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = filter,
                Multiselect = true
            };
            if (openFileDialog.ShowDialog() == true)
            {
                foreach (FileInfo fi in openFileDialog.Files)
                {
                    _uploadFiles.Add(fi);
                }
            }
        }

        private Guid _uploadingFileGUID;
        private string _uploadingFileName;

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            int count = _uploadFiles.Count;
            if (count > 0)
            {
                if (ResourceCategroyComboBox.SelectedItem != null)
                {
                    DisableControls(Constants.MSG_SAVING);
                    ResourceCategoryModel rsm = ResourceCategroyComboBox.SelectedItem as ResourceCategoryModel;
                    if (!_categoryWithChange.Contains(rsm.CategoryGuid))
                    {
                        _categoryWithChange.Add(rsm.CategoryGuid);
                    }

                    GetOneToUpload();
                }
                else
                {
                    HtmlPage.Window.Alert("Please choose which category you want to upload files to.");
                }
            }
            else
            {
                HtmlPage.Window.Alert("No files is chosn to upload yet.");
            }
        }

        private void ResourceTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (ResourceTypeComboBox.SelectedIndex)
            {
                case 0:
                    _resourceType = ResourceTypeEnum.Image;
                    break;
                case 1:
                    _resourceType = ResourceTypeEnum.Document;
                    break;
                case 2:
                    _resourceType = ResourceTypeEnum.Video;
                    break;
                case 3:
                    _resourceType = ResourceTypeEnum.Audio;
                    break;
            }
        }

        private void GetOneToUpload()
        {
            _uploadFileInfo = _uploadFiles[0];
            MessageTextBlock.Text = "Uploading " + _uploadFileInfo.Name;

            _uploadingFileGUID = Guid.NewGuid();
            _uploadingFileName = _uploadFileInfo.Name;
            Guid categoryGuid = ((ResourceCategoryModel)ResourceCategroyComboBox.SelectedItem).CategoryGuid;

            ServiceClient serviceClient = ServiceProxyFactory.Instance.ServiceProxy;
            serviceClient.SaveResourceCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(serviceClient_SaveResourceCompleted);

            if (_resourceType == ResourceTypeEnum.Image)
            {
                //serviceClient.GetBlobURLWithSharedWriteAccessAsync("OriginalImage", _uploadingFileGUID.ToString());

                WebClient webclient = new WebClient();
                webclient.OpenWriteCompleted += new OpenWriteCompletedEventHandler(webclient_OpenWriteCompleted);
                string uploadLocation = StringUtility.GetApplicationPath() +
                        Constants.UploadResourceService +
                        "?FileGuid=" + _uploadingFileGUID +
                        "&FileName=" + HttpUtility.UrlEncode(_uploadFileInfo.Name) +
                        "&Type=" + _resourceType.ToString() +
                        "&CategoryGuid=" + categoryGuid.ToString();
                webclient.OpenWriteAsync(
                    new Uri(uploadLocation,
                        UriKind.Absolute),
                    "POST",
                    _uploadFileInfo.OpenRead());
            }
            else
            {                
                serviceClient.SaveResourceAsync(_uploadingFileGUID, categoryGuid, _uploadingFileName, _resourceType.ToString());
            }
        }

        void serviceClient_SaveResourceCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                HtmlPage.Window.Alert(Constants.MSG_CANCELLED);
            }
            else if (e.Error != null)
            {
                HtmlPage.Window.Alert(Constants.ERROR_INTERNAL);
            }
            else
            {
                ServiceClient serviceClient = ServiceProxyFactory.Instance.ServiceProxy;
                serviceClient.GetBlobURLWithSharedWriteAccessCompleted += new EventHandler<GetBlobURLWithSharedWriteAccessCompletedEventArgs>(serviceClient_GetBlobURLWithSharedWriteAccessCompleted);
                serviceClient.GetBlobURLWithSharedWriteAccessAsync(_resourceType.ToString(), _uploadingFileGUID + GetCurrentUploadingFileExtension());
            }
        }

        void serviceClient_GetBlobURLWithSharedWriteAccessCompleted(object sender, GetBlobURLWithSharedWriteAccessCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                HtmlPage.Window.Alert(Constants.MSG_CANCELLED);
            }
            else if (e.Error != null)
            {
                HtmlPage.Window.Alert(Constants.ERROR_INTERNAL);
            }
            else
            {
                string blobURL = e.Result;
                WindowsAzureBlobUploader blobUploader = new WindowsAzureBlobUploader(_uploadFileInfo, blobURL);
                blobUploader.ReportUploadProgressEventHandler += new ReportUploadProgressDelegate(blobUploader_ReportUploadProgressEventHandler);
                blobUploader.FinishUploadProgressEventHandler += new EventHandler(blobUploader_FinishUploadProgressEventHandler);
                blobUploader.ReportErrorEventHandler += new EventHandler(blobUploader_ReportErrorEventHandler);
                blobUploader.Upload();
            }
        }

        void blobUploader_ReportErrorEventHandler(object sender, EventArgs e)
        {
            Dispatcher.BeginInvoke(delegate()
            {
                MessageTextBlock.Text = string.Format("There is error when {0} is uploaded, please try again later.", _uploadingFileName);
                EnableControls();
            });
        }

        void blobUploader_FinishUploadProgressEventHandler(object sender, EventArgs e)
        {
            Dispatcher.BeginInvoke(delegate()
            {
                //MessageTextBlock.Text = string.Format("{0} is uploaded", _uploadingFileName);

                Dispatcher.BeginInvoke(() =>
                {
                    _uploadFiles.RemoveAt(0);
                    _uploadedFiles.Add(_uploadingFileName);

                    if (updateUploadingStatusTimer != null)
                    {
                        updateUploadingStatusTimer.Dispose();
                    }

                    MessageTextBlock.Text = string.Format("{0} is uploaded successfully, {1} seconds are used.", _uploadingFileName, (DateTime.UtcNow - startUploadDateTime).Seconds);

                    if (_uploadFiles.Count == 0)
                    {
                        EnableControls();
                    }
                    else
                    {
                        GetOneToUpload();
                    }
                });
            });
        }

        private int _uploadPercentage = 0;
        void blobUploader_ReportUploadProgressEventHandler(int percentage)
        {
            _uploadPercentage = percentage;
            Dispatcher.BeginInvoke(new UpdateUploadProgressDelegate(UpdateUploadProgressHanlder));
        }

        private delegate void UpdateUploadProgressDelegate();
        private void UpdateUploadProgressHanlder()
        {
            MessageTextBlock.Text = string.Format("Uploading {0}, finish {1}%", _uploadingFileName, _uploadPercentage);
        }

        private void webclient_OpenWriteCompleted(object sender, OpenWriteCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                HtmlPage.Window.Alert(Constants.MSG_CANCELLED);
            }
            else if (e.Error != null)
            {
                HtmlPage.Window.Alert(Constants.ERROR_INTERNAL);
            }
            else
            {
                UploadResource(e.UserState as Stream, e.Result);
            }
        }

        private Timer checkImageUploadTimer;
        private Timer updateUploadingStatusTimer;

        private DateTime startUploadDateTime = DateTime.UtcNow;
        private void UploadResource(Stream inputStream, Stream outputStream)
        {
            byte[] buffer = new byte[4096];
            int bytesRead = 0;

            TimerCallback updateUploadingStatusCallBack = new TimerCallback(UpdateUploadingStatus);
            updateUploadingStatusTimer = new Timer(updateUploadingStatusCallBack, null, 1000, 2000);

            MessageTextBlock.Text = string.Format("Initialize uploading of {0}...", _uploadingFileName);
            startUploadDateTime = DateTime.UtcNow;
            while ((bytesRead = inputStream.Read(buffer, 0, buffer.Length)) > 0)
            {
                outputStream.Write(buffer, 0, bytesRead);
            }

            outputStream.Close();

            MessageTextBlock.Text = string.Format("{0}({1} MB) is uploading, please DO NOT close this window, {2} seconds has used.", _uploadingFileName, Math.Round(((double)_uploadFileInfo.Length / (double)(1024 * 1024)), 2), Math.Round((DateTime.UtcNow - startUploadDateTime).TotalSeconds));

            TimerCallback timerCallBack = new TimerCallback(CheckImageWhetherFinishUpload);
            // Check whether the image is finish upload every 30 seconds
            long fileLenghtRate = _uploadFileInfo.Length / (1024 * 1024);

            if (_uploadFileInfo.Length > 1024 * 1024)
            {
                // if file length is less than 1MB, check whether file is uploaded after 20 seconds, and then check every 30 seconds
                checkImageUploadTimer = new Timer(timerCallBack, null, 20000, 30000);
            }
            else
            {
                // if file leght is more than 1MB, ie x MB, check whether file is uploaded after x minutes, and then check every 30 seconds
                checkImageUploadTimer = new Timer(timerCallBack, null, 60000 * fileLenghtRate, 30000);
            }
            //SaveResourceTODB();
        }

        private void UpdateUploadingStatus(object state)
        {
            Dispatcher.BeginInvoke(delegate()
            {
                MessageTextBlock.Text = string.Format("{0}({1} MB) is uploading, please DO NOT close this window, {2} seconds has used.", _uploadingFileName, Math.Round(((double)_uploadFileInfo.Length / (double)(1024 * 1024)), 2), Math.Round((DateTime.UtcNow - startUploadDateTime).TotalSeconds, 0));
            });
        }

        private void CheckImageWhetherFinishUpload(object state)
        {
            Dispatcher.BeginInvoke(delegate()
            {
                // MessageTextBlock.Text = string.Format("{0} is uploading, please wait for seconds, {1} seconds has used.", _uploadingFileName, (DateTime.UtcNow - startUploadDateTime).TotalSeconds);

                ServiceClient serviceProxy = ServiceProxyFactory.Instance.ServiceProxy;
                serviceProxy.CheckThumnailImageWhetherExistCompleted += new EventHandler<CheckThumnailImageWhetherExistCompletedEventArgs>(serviceProxy_CheckThumnailImageWhetherExistCompleted);
                serviceProxy.CheckThumnailImageWhetherExistAsync(_uploadingFileGUID + GetCurrentUploadingFileExtension());
            });
        }

        private void serviceProxy_CheckThumnailImageWhetherExistCompleted(object sender, CheckThumnailImageWhetherExistCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                HtmlPage.Window.Alert(Constants.MSG_CANCELLED);
            }
            else if (e.Error != null)
            {
                HtmlPage.Window.Alert(Constants.ERROR_INTERNAL);
            }
            else
            {
                if (e.Result)
                {
                    checkImageUploadTimer.Dispose();
                    //SaveResourceTODB();

                    Dispatcher.BeginInvoke(() =>
                    {
                        _uploadFiles.RemoveAt(0);
                        _uploadedFiles.Add(_uploadingFileName);

                        if (updateUploadingStatusTimer != null)
                        {
                            updateUploadingStatusTimer.Dispose();
                        }

                        MessageTextBlock.Text = string.Format("{0} is uploaded successfully, {1} seconds are used.", _uploadingFileName, (DateTime.UtcNow - startUploadDateTime).Seconds);

                        if (_uploadFiles.Count == 0)
                        {
                            EnableControls();
                        }
                        else
                        {
                            GetOneToUpload();
                        }
                    });
                }
            }
        }

        private string GetCurrentUploadingFileExtension()
        {
            string[] strType = _uploadFileInfo.Name.Split('.');
            string fileExtension = "." + strType.Last();
            return fileExtension;
        }

        private void SaveResourceTODB()
        {
            Guid categoryGuid = ((ResourceCategoryModel)ResourceCategroyComboBox.SelectedItem).CategoryGuid;

            ServiceClient serviceProxy = ServiceProxyFactory.Instance.ServiceProxy;
            serviceProxy.SaveResourceCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(wcfClient_SaveResourceCompleted);
            serviceProxy.SaveResourceAsync(_uploadingFileGUID, categoryGuid, _uploadingFileName, _resourceType.ToString());
        }

        private void wcfClient_SaveResourceCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                HtmlPage.Window.Alert(Constants.MSG_CANCELLED);
            }
            else if (e.Error != null)
            {
                HtmlPage.Window.Alert(Constants.ERROR_INTERNAL);
            }
            else
            {
                Dispatcher.BeginInvoke(() =>
                {
                    _uploadFiles.RemoveAt(0);
                    _uploadedFiles.Add(_uploadingFileName);

                    if (updateUploadingStatusTimer != null)
                    {
                        updateUploadingStatusTimer.Dispose();
                    }

                    MessageTextBlock.Text = string.Format("{0} is uploaded successfully, {1} seconds are used.", _uploadingFileName, (DateTime.UtcNow - startUploadDateTime).Seconds);

                    if (_uploadFiles.Count == 0)
                    {
                        EnableControls();
                    }
                    else
                    {
                        GetOneToUpload();
                    }
                });
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Collapsed;

            if (UpdateResourceListEventHandler != null)
            {
                UpdateResourceListEventHandler(_categoryWithChange);
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Collapsed;

            if (UpdateResourceListEventHandler != null)
            {
                UpdateResourceListEventHandler(_categoryWithChange);
            }
        }

        #region Drag & Drop
        bool isMouseCaptured;
        Point mousePosition;
        bool clickOnDataGridColumn;
        private void RenameCategoryPopupPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!clickOnDataGridColumn)
            {
                FrameworkElement item = sender as FrameworkElement;
                mousePosition = e.GetPosition(null);
                isMouseCaptured = true;
                item.CaptureMouse();
                item.Cursor = Cursors.Hand;
            }
        }

        private void RenameCategoryPopupPanel_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            FrameworkElement item = sender as FrameworkElement;
            isMouseCaptured = false;
            clickOnDataGridColumn = false;
            item.ReleaseMouseCapture();
            mousePosition.X = mousePosition.Y = 0;
            item.Cursor = null;
        }

        private void RenameCategoryPopupPanel_MouseMove(object sender, MouseEventArgs e)
        {
            FrameworkElement item = sender as FrameworkElement;
            if (isMouseCaptured)
            {

                // Calculate the current position of the object.
                double deltaV = e.GetPosition(null).Y - mousePosition.Y;
                double deltaH = e.GetPosition(null).X - mousePosition.X;
                double newTop = deltaV + (double)item.GetValue(Canvas.TopProperty);
                double newLeft = deltaH + (double)item.GetValue(Canvas.LeftProperty);

                // Set new position of object.
                item.SetValue(Canvas.TopProperty, newTop);
                item.SetValue(Canvas.LeftProperty, newLeft);

                // Update position global variables.
                mousePosition = e.GetPosition(null);
            }
        }
        #endregion
    }
}
