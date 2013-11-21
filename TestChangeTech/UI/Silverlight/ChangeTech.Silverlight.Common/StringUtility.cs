using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Browser;
using System.IO;
using System.Collections;

namespace ChangeTech.Silverlight.Common
{
    public static class StringUtility
    {
        public static SolidColorBrush ConvertToColor(string colorString)
        {
            SolidColorBrush colorBrush = new SolidColorBrush();
            if (!string.IsNullOrEmpty(colorString) && colorString.Length == 8)
            {
                byte a = (byte)(Convert.ToUInt32(colorString.Substring(0, 2), 16));
                byte r = (byte)(Convert.ToUInt32(colorString.Substring(2, 2), 16));
                byte g = (byte)(Convert.ToUInt32(colorString.Substring(4, 2), 16));
                byte b = (byte)(Convert.ToUInt32(colorString.Substring(6, 2), 16));
                colorBrush.Color = Color.FromArgb(a, r, g, b);
            }
            return colorBrush;
        }

        public static double ConvertToFontSize(string fontSize)
        {
            if (!string.IsNullOrEmpty(fontSize))
            {
                return Convert.ToDouble(fontSize);
            }
            else
            {
                return 10;
            }
        }

        public static double ConvertToHight(string height)
        {
            if (string.IsNullOrEmpty(height))
            {
                return 20;
            }
            else
            {
                return Convert.ToDouble(height);
            }
        }

        public static double ConvetToWidth(string width)
        {
            if (string.IsNullOrEmpty(width))
            {
                return 100;
            }
            else
            {
                return Convert.ToDouble(width);
            }
        }

        public static double ConvertToPosition(string position)
        {
            return Convert.ToDouble(position);
        }

        public static string GetApplicationPath()
        {
            string currentPagePath = HtmlPage.Document.DocumentUri.AbsoluteUri;
            int index = currentPagePath.LastIndexOf('/');
            return currentPagePath.Substring(0, index + 1);
        }
        public static string AzureStroageAccountName;
        public static string GetBlobPath()
        {
            if (GetApplicationPath().Contains("https://"))
            {
                return string.Format("https://{0}.blob.core.windows.net/", AzureStroageAccountName);
            }
            else
            {
                return string.Format("http://{0}.blob.core.windows.net/", AzureStroageAccountName);
            }
        }

        public static string GetQueuePath()
        {
            return string.Format("http://{0}.queue.core.windows.net/", AzureStroageAccountName);
        }

        public static string GetTablePath()
        {
            return string.Format("http://{0}.table.core.windows.net/", AzureStroageAccountName);
        }

        public static string GetQueryString(string key)
        {
            string[] queryData = HtmlPage.Document.DocumentUri.Query.Split(new char[] { '?', '&' }, StringSplitOptions.RemoveEmptyEntries);
            if (queryData != null && queryData.Length > 0)
            {
                for (int index = 0; index < queryData.Length; index++)
                {
                    string[] keyvalue = queryData[index].Split('=');
                    if (keyvalue[0].Equals(key))
                    {
                        return keyvalue[1];
                    }
                }
            }
            return string.Empty;
        }
    }
}
