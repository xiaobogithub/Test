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
using System.Windows.Controls.Primitives;
using System.ComponentModel;
using System.Windows.Media.Imaging;
using ChangeTech.Silverlight.DesignPage.ChangeTechWCFService;
using ChangeTech.Silverlight.Common;

namespace ChangeTech.Silverlight.DesignPage.Controls
{
    public partial class CropImage : UserControl
    {
        public event CloseCropDelegate CloseCropEnevtnHandler = null;
        private ResourceModel _photograph = null;
        Guid _bigImageGuid = Guid.Empty;
        public ResourceModel Photograph
        {
            get { return _photograph; }
            set
            {
                _photograph = value;

                _bigImageGuid = _photograph.ID;
                if (_photograph.BigResource != null)
                {
                    _bigImageGuid = _photograph.BigResource.ID;
                }
                ImageUtility.ShowImage(CropImageView, Constants.OriginalImageDirectory + _bigImageGuid + this.Photograph.Extension);              
            }
        }


        public CropImage()
        {
            InitializeComponent();
        }

        private void cropButton_Click(object sender, RoutedEventArgs e)
        {
            TipText.Text = "Please wait ...";
            UnableControls();
            ServiceClient client = ServiceProxyFactory.Instance.ServiceProxy;
            client.CropImageCompleted += new EventHandler<AsyncCompletedEventArgs>(client_CropImageCompleted);
            client.CropImageAsync(StringUtility.GetBlobPath() + Constants.OriginalImageDirectory + _bigImageGuid + this.Photograph.Extension, 
                Photograph.ID.ToString(), this.Photograph.Name, Convert.ToInt32(vsa.Height), 
                Convert.ToInt32(vsa.Width), Convert.ToInt32(vsa.Left), Convert.ToInt32(vsa.Top),
                _photograph.ResourceCategoryGUID);
        }

        void client_CropImageCompleted(object sender, AsyncCompletedEventArgs e)
        {
            TipText.Text = "";
            EnableControls();
            ClosePopupImage();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ClosePopupImage();
        }

        private void ClosePopupImage()
        {
            this.Visibility = Visibility.Collapsed;
            if (CloseCropEnevtnHandler != null)
            {
                CloseCropEnevtnHandler();
            }
        }

        private void EnableControls()
        {
            CloseButton.IsEnabled = true;
            CropButton.IsEnabled = true;
        }

        private void UnableControls()
        {
            CloseButton.IsEnabled = false;
            CropButton.IsEnabled = false;
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
            if (Canvas.GetTop(cropUL) + e.VerticalChange >= 0 && Canvas.GetTop(cropUL) + e.VerticalChange <= cropCanvas.ActualHeight)
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

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //WriteableBitmap wb = new WriteableBitmap(CropImageView, null);
            //CropGrid.Width = wb.PixelWidth;
            //CropGrid.Height = wb.PixelHeight;

            vsa = new VirtualSelectionArea() { Bottom = 110, Left = 10, Right = 110, Top = 10 };
            this.LayoutRoot.DataContext = vsa;
        }
    }   
}
