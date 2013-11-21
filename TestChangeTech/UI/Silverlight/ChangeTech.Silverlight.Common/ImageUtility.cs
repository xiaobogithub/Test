using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Browser;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.IO;
using System.Windows.Shapes;

namespace ChangeTech.Silverlight.Common
{
    public class ImageUtility
    {
        public static void ShowImage(Image image, string uri, EventHandler<DownloadProgressEventArgs> downEvent)
        {
            BitmapImage bitmap = new BitmapImage();
            uri = StringUtility.GetBlobPath() + uri;
            bitmap.UriSource = new Uri(uri, UriKind.Absolute);
            bitmap.DownloadProgress += downEvent;
            image.Source = bitmap;

        }

        public static void ShowImage(Image image, string uri)
        {
            BitmapImage bitmap = new BitmapImage();
            uri = StringUtility.GetBlobPath() + uri;
            bitmap.UriSource = new Uri(uri, UriKind.Absolute);
            image.Source = bitmap;
        }

        public static void ShowImage(ImageBrush imageBrush, string uri)
        {
            uri = StringUtility.GetBlobPath() + uri;
            imageBrush.ImageSource = new BitmapImage(new Uri(uri, UriKind.Absolute));
        }
    }
}