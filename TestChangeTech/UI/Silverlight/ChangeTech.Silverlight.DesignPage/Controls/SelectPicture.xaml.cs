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
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;
using System.IO;
using ChangeTech.Silverlight.DesignPage.ChangeTechWCFService;
using System.Windows.Browser;
using System.Windows.Controls.Primitives;
using ChangeTech.Silverlight.Common;
using System.ComponentModel;

namespace ChangeTech.Silverlight.DesignPage.Controls
{
    public partial class SelectPicture : UserControl
    {
        public event SelectPictureDelegate OnSelectImage;
        private ObservableCollection<ResourceCategoryModel> _categories;
        public ResourceModel LastSelectedResource { get; set; }

        public SelectPicture()
        {
            InitializeComponent();
            if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                return;
            }
            UploadManager.UpdateResourceListEventHandler += new UpdateResourceListDelegate(UploadManager_UpdateResourceListEventHandler);
        }

        private void UploadManager_UpdateResourceListEventHandler(List<Guid> categoryWithChange)
        {
            ResourceCategoryModel rsm = cbImageCategory.SelectedItem as ResourceCategoryModel;
            if (rsm.CategoryGuid == Guid.Empty || categoryWithChange.Contains(rsm.CategoryGuid))
            {
                ImageViewer.LoadPhotos();
            }
        }

        private void OnLoad(object sender, RoutedEventArgs args)
        {
            // ThumbView
            ImageViewer.OnSelectImage += OnSelectImage;
            ImageViewer.AfterLoadReourceCompleted += new EventHandler(ImageViewer_AfterLoadReourceCompleted);
            ImageViewer.OnSelectImage += new SelectPictureDelegate(ImageViewer_DisplayPictureInfoEventHandler);
            ImageViewer.OnDoubleClickEvent += new DoubleClickDelegate(ImageViewer_OnDoubleClickEvent);

            // Image info
            ImageInfo.ChangeResourceCategoryEventHandler += new ChangeResourceCategoryDelegate(ImageInfo_ChangeResourceCategoryEventHandler);
            ImageInfo.SelectPictureEventHandler += new SelectPictureDelegate(ImageInfo_SelectPictureEventHandler);
            ImageInfo.PreviewPictureEventHandler += new PreviewPictureDelegate(ImageInfo_PreviewPictureEventHandler);
            ImageInfo.DeleteImageEventHandler += new DeleteImageDelegate(ImageInfo_DeleteImageEventHandler);
            //ImageInfo.CropImageEventHandler += new CropPictureDelegate(ImageInfo_CropImageEventHandler);

            //Preview image
            DetailsView.SelectPictureEventHandler += new SelectPictureDelegate(DetailsView_SelectPictureEventHandler);
            DetailsView.ClosePreviewEnevtnHandler += new ClosePreviewDelegate(DetailsView_ClosePreviewEnevtnHandler);
            DetailsView.CloseCropEnevtnHandler +=new CloseCropDelegate(DetailsView_CloseCropEnevtnHandler);
        }

        void DetailsView_CloseCropEnevtnHandler(ResourceModel image)
        {
            Disable(Constants.MSG_LOADING);
            ImageViewer.LastSelectedResource = image;
            ImageViewer.LoadPhotos();
        }

        void ImageViewer_OnDoubleClickEvent(ResourceModel image)
        {
            PreviewImage(image);
        }       

        private void ImageInfo_DeleteImageEventHandler(ResourceModel image)
        {
            ImageViewer.RefreshAfterDeleteImage(image);
        }

        private void DetailsView_ClosePreviewEnevtnHandler()
        {
            ImageViewer.RefreshAfterClosePreviewImage(DetailsView.CurrentIndex, DetailsView.DeleteCount);
        }

        private void ImageInfo_PreviewPictureEventHandler(ResourceModel image)
        {
            PreviewImage(image);
        }

        private void PreviewImage(ResourceModel image)
        {
            DetailsView = new PopupImage();
            selectPictureGrid.Children.Add(DetailsView);
            DetailsView.Photographs = ImageViewer.Images;
            DetailsView.Photograph = image;
            DetailsView.SelectPictureEventHandler += new SelectPictureDelegate(DetailsView_SelectPictureEventHandler);
            DetailsView.ClosePreviewEnevtnHandler += new ClosePreviewDelegate(DetailsView_ClosePreviewEnevtnHandler);
            DetailsView.CloseCropEnevtnHandler += new CloseCropDelegate(DetailsView_CloseCropEnevtnHandler);
            DetailsView.SetValue(Grid.ColumnProperty, 0);
            DetailsView.SetValue(Grid.RowProperty, 0);
            DetailsView.SetValue(Grid.ColumnSpanProperty, 6);
            DetailsView.SetValue(Grid.RowSpanProperty, 4);
            DetailsView.SetValue(Canvas.ZIndexProperty, 10);
            DetailsView.Visibility = Visibility.Visible;
        }

        private void ImageInfo_SelectPictureEventHandler(ResourceModel image)
        {
            if (OnSelectImage != null)
            {
                OnSelectImage(image);
            }

            Visibility = Visibility.Collapsed;
        }

        private void DetailsView_SelectPictureEventHandler(ResourceModel image)
        {
            if (OnSelectImage != null)
            {
                OnSelectImage(image);
            }

            Visibility = Visibility.Collapsed;
        }

        private void ImageInfo_ChangeResourceCategoryEventHandler()
        {
            Disable(Constants.MSG_LOADING);
            ImageViewer.LoadPhotos();
        }

        private void ImageViewer_DisplayPictureInfoEventHandler(ResourceModel image)
        {
            LastSelectedResource = image;
            ImageInfo.Show(cbImageCategory.ItemsSource, image);
        }

        private void ImageViewer_AfterLoadReourceCompleted(object sender, EventArgs e)
        {
            Enable();
        }

        public void Show()
        {
            Visibility = Visibility.Visible;
            DetailsView.Visibility = Visibility.Collapsed;

            Disable(Constants.MSG_LOADING);

            ServiceClient client = ServiceProxyFactory.Instance.ServiceProxy;
            client.GetResourceCategoryCompleted += new EventHandler<GetResourceCategoryCompletedEventArgs>(client_GetResourceCategoryCompleted);
            client.GetResourceCategoryAsync();
        }

        private void Disable(string message)
        {
            tbMessage.Text = message;
            cbImageCategory.IsEnabled = false;
            ImageViewer.IsEnabled = false;
            ImageInfo.IsEnabled = false;
        }

        private void Enable()
        {
            tbMessage.Text = string.Empty;
            cbImageCategory.IsEnabled = true;
            ImageViewer.IsEnabled = true;
            ImageInfo.IsEnabled = true;
        }

        private void client_GetResourceCategoryCompleted(object sender, GetResourceCategoryCompletedEventArgs e)
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
                _categories = e.Result.Categories;
                cbImageCategory.ItemsSource = _categories;
                if (e.Result.Categories.Count > 0)
                {
                    if (e.Result.LastSelectedResourceCategory != Guid.Empty)
                    {
                        foreach (ResourceCategoryModel model in e.Result.Categories)
                        {
                            if (model.CategoryGuid == e.Result.LastSelectedResourceCategory)
                            {
                                cbImageCategory.SelectedIndex = e.Result.Categories.IndexOf(model);
                                break;
                            }
                        }
                    }
                    else
                    {
                        cbImageCategory.SelectedIndex = 0;
                    }

                    Guid categoryGuid = ((ResourceCategoryModel)cbImageCategory.SelectedItem).CategoryGuid;
                }
            }
        }

        private void cbImageCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbImageCategory.SelectedItem != null)
            {
                Disable(Constants.MSG_LOADING);
                ImageViewer.CategoryGuid = ((ResourceCategoryModel)cbImageCategory.SelectedItem).CategoryGuid;
                ImageViewer.LastSelectedResource = LastSelectedResource;
                ImageViewer.LoadPhotos();
            }
        }

        private void btnUpload_Click(object sender, RoutedEventArgs e)
        {
            UploadManager.Show(ResourceTypeEnum.Image, _categories);
        }

        private void hylbtnMangageCategory_Click(object sender, RoutedEventArgs e)
        {
            ManageCategory magCategory = (ManageCategory)this.FindName("ucManangeCategory");
            magCategory.Visibility = Visibility.Visible;
            magCategory.CloseEvent = AfterManageCategory;
            magCategory.UpdateLayout();
        }

        private void AfterManageCategory(object sender, EventArgs e)
        {
            ServiceClient client = ServiceProxyFactory.Instance.ServiceProxy;
            client.GetResourceCategoryCompleted += client_GetResourceCategoryCompleted;
            client.GetResourceCategoryAsync();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void CloseEvent(object sender, EventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        #region Drag & Drop
        bool isMouseCaptured;
        Point mousePosition;

        private void RenameCategoryPopupPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (ImageInfo.Visibility == Visibility.Collapsed &&
                UploadManager.Visibility == Visibility.Collapsed)
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
