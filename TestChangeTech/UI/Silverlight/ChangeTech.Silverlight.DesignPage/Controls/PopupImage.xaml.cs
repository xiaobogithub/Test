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
using System.Windows.Media.Imaging;
using ChangeTech.Silverlight.DesignPage.ChangeTechWCFService;
using ChangeTech.Silverlight.Common;
using System.Windows.Browser;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.ComponentModel;
using System.Windows.Controls.Primitives;

namespace ChangeTech.Silverlight.DesignPage.Controls
{
    public partial class PopupImage : UserControl
    {
        public event SelectPictureDelegate SelectPictureEventHandler;
        public event CloseCropDelegate CloseCropEnevtnHandler = null;

        private ObservableCollection<ResourceModel> _photographs = null;
        private ResourceModel _photograph = null;
        private ServiceClient _client = null;
        private int _deleteCount = 0;

        private int _currentIndex = -1;
        public event ClosePreviewDelegate ClosePreviewEnevtnHandler = null;

        public int DeleteCount
        {
            get { return _deleteCount; }
            set { _deleteCount = value; }
        }

        public int CurrentIndex
        {
            get { return _currentIndex; }
            set { _currentIndex = value; }
        }

        public ObservableCollection<ResourceModel> Photographs
        {
            get { return _photographs; }
            set
            {
                _photographs = value;

                if (_photograph != null)
                {
                    _currentIndex = value.IndexOf(_photograph);
                }
            }
        }

        public ResourceModel Photograph
        {
            get { return _photograph; }
            set
            {
                _photograph = value;

                if (_photographs != null)
                {
                    _currentIndex = _photographs.IndexOf(value);
                }
                ImageUtility.ShowImage(ImageView, Constants.OriginalImageDirectory + _photograph.NameOnServer);
                ImageInfo.Show(_photograph);
            }
        }

        private bool showOnlySharedCategories = false;
        public bool ShowOnlySharedCategories
        {
            set
            {
                showOnlySharedCategories = value;

                if (value == true)
                {
                    Delete.Visibility = Visibility.Collapsed;
                }
            }
        }

        public PopupImage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ClosePopupImage();
        }

        private void ClosePopupImage()
        {
            this.Visibility = Visibility.Collapsed;
            if (ClosePreviewEnevtnHandler != null)
            {
                ClosePreviewEnevtnHandler();
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (HtmlPage.Window.Confirm("Do you want to delete this image?"))
            {
                if (_client == null)
                {
                    _client = ServiceProxyFactory.Instance.ServiceProxy;
                    _client.DeleteResourceCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(_client_DeleteResourceCompleted);
                }

                _client.DeleteResourceAsync(_photograph.ID);
            }
        }

        void _client_DeleteResourceCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
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
                _deleteCount++;
                _photographs.Remove(_photograph);
                // still have image after.
                if (_currentIndex <= _photographs.Count - 1)
                {
                    ShowExactImage();
                }
                else
                {
                    _currentIndex--;
                    if (_currentIndex >= 0)
                    {
                        ShowExactImage();
                    }
                    else
                    {
                        ClosePopupImage();
                    }
                }
            }
        }

        private void ShowExactImage()
        {
            //Debug.Assert(_photographs[_currentIndex] != null);
            //InitialSelectionArea();
            this._photograph = _photographs[_currentIndex];
            //DisplayImageReferenceInfo();

            //ImageNameTextBlock.Text = _photograph.Name;
            ImageUtility.ShowImage(ImageView, Constants.OriginalImageDirectory + this._photograph.NameOnServer);
            ImageInfo.Show(_photograph);
            this.UpdateLayout();
            //PopupImageStroyboard.Begin();
        }

        private void Prev_Click(object sender, RoutedEventArgs e)
        {
            if (_currentIndex > 0)
            {
                _currentIndex--;
                ShowExactImage();
            }
            else
            {
                HtmlPage.Window.Alert("This is the first image!");
            }
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            if (_currentIndex < _photographs.Count - 1)
            {
                _currentIndex++;
                ShowExactImage();
            }
            else
            {
                HtmlPage.Window.Alert("This is the last image!");
            }
        }

        private void SelectButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectPictureEventHandler != null)
            {
                SelectPictureEventHandler(_photograph);
            }
        }

        private void cropButton_Click(object sender, RoutedEventArgs e)
        {
            TipText.Text = "Processing image, please wait for seconds...";
            UnableControls();
            ServiceClient client = ServiceProxyFactory.Instance.ServiceProxy;
            client.CropImageCompleted += new EventHandler<CropImageCompletedEventArgs>(client_CropImageCompleted);
            Guid bigresourceGUID = Photograph.ID;
            if (Photograph.BigResource != null)
            {
                bigresourceGUID = Photograph.BigResource.ID;
            }
            CropImageModel resourceModel = new CropImageModel
            {
                NormalURL = InitialURL(this.Photograph),
                BigImageURL = InitialURL(this.Photograph.BigResource),
                FileGuid = bigresourceGUID,
                FileName = this.Photograph.Name,
                Height = Convert.ToInt32(vsa.Height),
                Width = Convert.ToInt32(vsa.Width),
                XSet = Convert.ToInt32(vsa.Left),
                YSet = Convert.ToInt32(vsa.Top),
                CategoryGUID = _photograph.ResourceCategoryGUID
            };
            client.CropImageAsync(resourceModel);
        }

        void client_CropImageCompleted(object sender, CropImageCompletedEventArgs e)
        {
            TipText.Text = "";
            this.Visibility = Visibility.Collapsed;
            if (CloseCropEnevtnHandler != null)
            {
                CloseCropEnevtnHandler(e.Result);
            }
            EnableControls();
            InitialSelectionArea();
        }

        private string InitialURL(ResourceModel resourceModel)
        {
            string url = "";
            if (resourceModel != null)
                url = StringUtility.GetBlobPath() + Constants.OriginalImageDirectory + resourceModel.NameOnServer;
            return url;
        }

        private void UnableControls()
        {
            CropButton.IsEnabled = false;
            SelectButton.IsEnabled = false;
            Prev.IsEnabled = false;
            Next.IsEnabled = false;
            Delete.IsEnabled = false;
            CloseButton.IsEnabled = false;
        }

        private void EnableControls()
        {
            CropButton.IsEnabled = true;
            SelectButton.IsEnabled = true;
            Prev.IsEnabled = true;
            Next.IsEnabled = true;
            Delete.IsEnabled = true;
            CloseButton.IsEnabled = true;
        }

        VirtualSelectionArea vsa;

        private void cropUL_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            UIElement thumb = (UIElement)sender;
            if (Canvas.GetLeft(thumb) + e.HorizontalChange + 10 < vsa.Right && Canvas.GetLeft(thumb) + e.HorizontalChange >= 0)
            {
                Canvas.SetLeft(thumb, Canvas.GetLeft(thumb) + e.HorizontalChange);
            }
            if (Canvas.GetTop(thumb) + e.VerticalChange + 10 < vsa.Bottom && Canvas.GetTop(thumb) + e.VerticalChange >= 0)
            {
                Canvas.SetTop(thumb, Canvas.GetTop(thumb) + e.VerticalChange);
            }
            UpdateMask();
        }

        private void cropUR_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            UIElement thumb = (UIElement)sender;
            if (Canvas.GetLeft(thumb) + e.HorizontalChange - 10 > vsa.Left && Canvas.GetLeft(thumb) + e.HorizontalChange <= cropCanvas.ActualWidth)
            {
                Canvas.SetLeft(thumb, Canvas.GetLeft(thumb) + e.HorizontalChange);
            }
            if (Canvas.GetTop(thumb) + e.VerticalChange + 10 < vsa.Bottom && Canvas.GetTop(thumb) + e.VerticalChange >= 0)
            {
                Canvas.SetTop(thumb, Canvas.GetTop(thumb) + e.VerticalChange);
            }
            UpdateMask();
        }

        private void cropBL_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            UIElement thumb = (UIElement)sender;
            if (Canvas.GetLeft(thumb) + e.HorizontalChange + 10 < vsa.Right && Canvas.GetLeft(thumb) + e.HorizontalChange >= 0)
            {
                Canvas.SetLeft(thumb, Canvas.GetLeft(thumb) + e.HorizontalChange);
            }
            if (Canvas.GetTop(thumb) + e.VerticalChange - 10 > vsa.Top && Canvas.GetTop(thumb) + e.VerticalChange <= cropCanvas.ActualHeight)
            {
                Canvas.SetTop(thumb, Canvas.GetTop(thumb) + e.VerticalChange);
            }
            UpdateMask();
        }

        private void cropBR_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            UIElement thumb = (UIElement)sender;
            if (Canvas.GetLeft(thumb) + e.HorizontalChange - 10 > vsa.Left && Canvas.GetLeft(thumb) + e.HorizontalChange <= cropCanvas.ActualWidth)
            {
                Canvas.SetLeft(thumb, Canvas.GetLeft(thumb) + e.HorizontalChange);
            }
            if (Canvas.GetTop(thumb) + e.VerticalChange - 10 > vsa.Top && Canvas.GetTop(thumb) + e.VerticalChange <= cropCanvas.ActualHeight)
            {
                Canvas.SetTop(thumb, Canvas.GetTop(thumb) + e.VerticalChange);
            }
            UpdateMask();
        }

        private void cropM_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            UIElement thumb = (UIElement)sender;
            if (Canvas.GetLeft(cropBL) + e.HorizontalChange >= 0 && Canvas.GetLeft(cropBR) + e.HorizontalChange <= cropCanvas.ActualWidth)
            {
                vsa.Left += e.HorizontalChange;
                vsa.Right += e.HorizontalChange;
            }
            if (Canvas.GetTop(cropUL) + e.VerticalChange >= 0 && Canvas.GetTop(cropBR) + e.VerticalChange <= cropCanvas.ActualHeight)
            {
                vsa.Top += e.VerticalChange;
                vsa.Bottom += e.VerticalChange;
            }
            UpdateMask();
        }

        private void UpdateCropBox(UIElement thumb, DragDeltaEventArgs e)
        {
            Canvas.SetLeft(thumb, Canvas.GetLeft(thumb) + e.HorizontalChange);
            Canvas.SetTop(thumb, Canvas.GetTop(thumb) + e.VerticalChange);
            UpdateMask();
        }

        private void UpdateMask()
        {
            topMask.Height = vsa.Top;
            topMask.Width = vsa.Left + vsa.Width;

            leftMask.Height = cropCanvas.ActualHeight - vsa.Top;
            leftMask.Width = vsa.Left;
            Canvas.SetTop(leftMask, vsa.Top);

            rightMask.Height = vsa.Height + vsa.Top;
            rightMask.Width = cropCanvas.ActualWidth - vsa.Left - vsa.Width;
            Canvas.SetLeft(rightMask, vsa.Left + vsa.Width);

            bottomMask.Height = cropCanvas.ActualHeight - vsa.Height - vsa.Top;
            bottomMask.Width = cropCanvas.ActualWidth - vsa.Left;
            Canvas.SetTop(bottomMask, vsa.Height + vsa.Top);
            Canvas.SetLeft(bottomMask, vsa.Left);
        }

        private void InitialMask()
        {
            topMask.Height = 0;
            topMask.Width = 0;

            leftMask.Height = 0;
            leftMask.Width = 0;
            Canvas.SetTop(leftMask, 0);

            rightMask.Height = 0;
            rightMask.Width = 0;
            Canvas.SetLeft(rightMask, 0);

            bottomMask.Height = 0;
            bottomMask.Width = 0;
            Canvas.SetTop(bottomMask, 0);
            Canvas.SetLeft(bottomMask, 0);
        }

        private void InitialSelectionArea()
        {
            vsa = new VirtualSelectionArea() { Bottom = 110, Left = 10, Right = 110, Top = 10 };
            this.LayoutRoot.DataContext = vsa;

            //topMask.
            //UpdateMask();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            InitialSelectionArea();
        }
    }
}
