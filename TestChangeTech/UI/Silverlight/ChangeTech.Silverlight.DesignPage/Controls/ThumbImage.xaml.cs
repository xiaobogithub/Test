using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Browser;
using System.Diagnostics;
using System.Windows.Media.Imaging;
using ChangeTech.Silverlight.DesignPage.ChangeTechWCFService;
using ChangeTech.Silverlight.Common;

namespace ChangeTech.Silverlight.DesignPage.Controls
{
    public partial class ThumbImage : UserControl
    {
        public bool ShowOnlySharedCategories
        {
            set
            {
                if (value == true)
                {
                    //Del.Visibility = Visibility.Collapsed;
                }
            }
        }

        public ThumbImage()
        {
            InitializeComponent();

            ImageView.Visibility = Visibility.Collapsed;
            ImageLoadPanel.Visibility = Visibility.Visible;
            ImageLoadStoryboard.Begin();
        }

        public void SetActiveBorder()
        {
            ImageBorder.Style = Application.Current.Resources["ImageViewSelectedStyle"] as Style;
        }

        public void SetNormalBorder()
        {
            ImageBorder.Style = Application.Current.Resources["ImageViewStyle"] as Style;
        }

        internal void ShowImage()
        {
            string imageName = (this.Tag as ResourceModel).Name;
            if (imageName.Length > 12)
            {
                imageName = imageName.Substring(0, 10)+"...";
            }
            ImageNameTextBlock.Text = imageName;
            string url = Constants.ThumbImageDirectory + (this.Tag as ResourceModel).NameOnServer;
            ImageUtility.ShowImage(ImageView, url, DownloadImageProgress);
        }

        private void DownloadImageProgress(object sender, DownloadProgressEventArgs e)
        {
            if (e.Progress == 100)
            {
                ImageView.Visibility = Visibility.Visible;
                ImageLoadPanel.Visibility = Visibility.Collapsed;
                ImageLoadStoryboard.Stop();
            }
        }       
    }
}
