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
using System.Collections;
using System.Windows.Browser;

namespace ChangeTech.Silverlight.DesignPage.Controls
{
    public partial class ImageReferenceInfo : UserControl
    {      
        public event ChangeResourceCategoryDelegate ChangeResourceCategoryEventHandler;
        public event SelectPictureDelegate SelectPictureEventHandler;
        public event PreviewPictureDelegate PreviewPictureEventHandler;
        public event DeleteImageDelegate DeleteImageEventHandler;
        public event CropPictureDelegate CropImageEventHandler;
        public bool IsSimpleMode { get; set; }

        private ResourceModel _imageModel;

        public ImageReferenceInfo()
        {
            InitializeComponent();

            IsEnabled = false;            
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (IsSimpleMode)
            {
                Grid.SetRowSpan(ImageReferenceInfoScrollViewer, 5);
                ChangeCategoryButton.Visibility = Visibility.Collapsed;
                CategoryComoBox.Visibility = Visibility.Collapsed;
                SelectButton.Visibility = Visibility.Collapsed;
                PreviewButton.Visibility = Visibility.Collapsed;
                DeleteButton.Visibility = Visibility.Collapsed;
            }
        }

        public void Show(IEnumerable categories, ResourceModel imageModel)
        {
            IsEnabled = true;

            _imageModel = imageModel;

            ImageNameTextBlock.Text = imageModel.Name;
            CategoryComoBox.ItemsSource = categories;
            foreach (ResourceCategoryModel resourceCatgeoryModel in CategoryComoBox.ItemsSource)
            {
                if (resourceCatgeoryModel.CategoryGuid == imageModel.ResourceCategoryGUID)
                {
                    CategoryComoBox.SelectedItem = resourceCatgeoryModel;
                    break;
                }
            }
            DisplayImageReferenceInfo(imageModel);
        }

        public void Show(ResourceModel imageModel)
        {
            IsEnabled = true;

            _imageModel = imageModel;
            ImageNameTextBlock.Text = imageModel.Name;
            DisplayImageReferenceInfo(imageModel);

            Visibility = Visibility.Visible;
        }

        private void DisplayImageReferenceInfo(ResourceModel imageModel)
        {
            Disable("");
            ServiceClient serviceProxy = ServiceProxyFactory.Instance.ServiceProxy;
            serviceProxy.GetReferencesInfoOfImageCompleted += new EventHandler<GetReferencesInfoOfImageCompletedEventArgs>(serviceProxy_GetReferencesInfoOfImageCompleted);
            serviceProxy.GetReferencesInfoOfImageAsync(imageModel.ID);
        }

        private void serviceProxy_GetReferencesInfoOfImageCompleted(object sender, GetReferencesInfoOfImageCompletedEventArgs e)
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
                ImageReferenceInfoTextBlock.Text = "";
                foreach (ProgramImageReference pi in e.Result)
                {
                    if (pi.SessionImageReference.Count > 0)
                    {
                        ImageReferenceInfoTextBlock.Text += pi.ProgramName + ":\n";

                        foreach (SessionImageReference si in pi.SessionImageReference)
                        {
                            ImageReferenceInfoTextBlock.Text += string.Format("Day-{0}\n", si.Day);
                        }
                    }
                }
                Enable();
            }
        }

        private void Disable(string msg)
        {
            //StatusTextBlock.Text = msg;
            CategoryComoBox.IsEnabled = false;
            ChangeCategoryButton.IsEnabled = false;
            SelectButton.IsEnabled = false;
            PreviewButton.IsEnabled = false;
            //CropButton.IsEnabled = false;
            DeleteButton.IsEnabled = false;
        }

        private void Enable()
        {
            //StatusTextBlock.Text = string.Empty;
            CategoryComoBox.IsEnabled = true;
            ChangeCategoryButton.IsEnabled = true;
            SelectButton.IsEnabled = true;
            PreviewButton.IsEnabled = true;
            //CropButton.IsEnabled = true;
            DeleteButton.IsEnabled = true;
        }

        private void ChangeCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            if (_imageModel.ResourceCategoryGUID != ((ResourceCategoryModel)CategoryComoBox.SelectedItem).CategoryGuid)
            {
                bool confirmResult = HtmlPage.Window.Confirm(string.Format("Do you want to move this image to {0}?", ((ResourceCategoryModel)CategoryComoBox.SelectedItem).CategoryName));
                if (confirmResult)
                {
                    Disable(Constants.MSG_SAVING);
                    ServiceClient serviceProxy = ServiceProxyFactory.Instance.ServiceProxy;
                    serviceProxy.UpdateResourceCategoryCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(serviceProxy_UpdateResourceCategoryCompleted);
                    serviceProxy.UpdateResourceCategoryAsync(_imageModel.ID, ((ResourceCategoryModel)CategoryComoBox.SelectedItem).CategoryGuid);
                }
            }
        }

        private void serviceProxy_UpdateResourceCategoryCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
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
                if (ChangeResourceCategoryEventHandler != null)
                {
                    ChangeResourceCategoryEventHandler();
                }

                HtmlPage.Window.Alert(Constants.MSG_SUCCESSFUL);

                Enable();
             }
        }

        private void SelectButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectPictureEventHandler != null)
            {
                SelectPictureEventHandler(_imageModel);
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {

            if (HtmlPage.Window.Confirm("Do you want to delete this image?"))
            {
                ServiceClient serviceProxy = ServiceProxyFactory.Instance.ServiceProxy;
                serviceProxy.DeleteResourceCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(serviceProxy_DeleteResourceCompleted);
                serviceProxy.DeleteResourceAsync(_imageModel.ID);
            }
        }

        private void serviceProxy_DeleteResourceCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                HtmlPage.Window.Alert(Constants.MSG_CANCELLED);
            }
            else if (e.Error != null)
            {
                HtmlPage.Window.Alert("This image is used, you cannot delete.");
            }
            else
            {
                if (DeleteImageEventHandler != null)
                {
                    DeleteImageEventHandler(_imageModel);
                }
            }
        }

        private void CropButton_Click(object sender, RoutedEventArgs e)
        {
            if (CropImageEventHandler != null)
            {
                CropImageEventHandler(_imageModel);
            }
        }

        private void PreviewButton_Click(object sender, RoutedEventArgs e)
        {
            if (PreviewPictureEventHandler != null)
            {
                PreviewPictureEventHandler(_imageModel);
            }
        }

        #region Drag & Drop
        bool isMouseCaptured;
        Point mousePosition;
        bool clickOnDataGridColumn;

        private void RenameCategoryPopupPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            FrameworkElement item = sender as FrameworkElement;

            if (!clickOnDataGridColumn)
            {
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
