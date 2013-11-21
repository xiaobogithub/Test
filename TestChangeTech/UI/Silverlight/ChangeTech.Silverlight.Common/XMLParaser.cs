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
using System.Xml;
using System.Linq;
using System.Collections;

namespace ChangeTech.Silverlight.Common
{
    public static class XMLParaser
    {
        public static Point GetPositionAttribute(this XmlReader xr)
        {
            string positionX = xr.GetAttribute("PositionX");
            string positionY = xr.GetAttribute("PositionY");
            Point p = new Point(Convert.ToDouble(positionX), Convert.ToDouble(positionY));
            return p;
        }

        public static string GetControlTypeArrtibute(this XmlReader xr)
        {
            return xr.GetAttribute("Type");
        }

        public static string GetNameAttribute(this XmlReader xr)
        {
            return xr.GetAttribute("Name");
        }

        public static string GetValueAttribute(this XmlReader xr)
        {
            string value = xr.GetAttribute("Value");
            if (string.IsNullOrEmpty(value))
            {
                value = string.Empty;
            }
            return value;
        }

        public static string GetTextAttribute(this XmlReader xr)
        {
            string value = xr.GetAttribute("Text");
            if (string.IsNullOrEmpty(value))
            {
                value = string.Empty;
            }
            return value;
        }

        public static double GetWidthAttribute(this XmlReader xr)
        {
            string width = xr.GetAttribute("Width");
            if (string.IsNullOrEmpty(width))
            {
                return 100;
            }
            else
            {
                return Convert.ToDouble(width);
            }
        }

        public static double GetHeightAttribute(this XmlReader xr)
        {
            string height = xr.GetAttribute("Height");
            if (string.IsNullOrEmpty(height))
            {
                return 20;
            }
            else
            {
                return Convert.ToDouble(height);
            }
        }

        public static bool GetCheckedAttribute(this XmlReader xr)
        {
            string isChecked = xr.GetAttribute("Checked");
            if (string.IsNullOrEmpty(isChecked))
            {
                return false;
            }
            else
            {
                return Convert.ToBoolean(isChecked);
            }
        }

        public static SolidColorBrush GetForegroundColor(this XmlReader xr)
        {
            SolidColorBrush colorBrush = new SolidColorBrush();
            string colorStr = xr.GetAttribute("ForegroundColor");
            if (!string.IsNullOrEmpty(colorStr) && colorStr.Length == 8)
            {
               byte a = (byte)(Convert.ToUInt32(colorStr.Substring(1, 2), 16));
               byte r = (byte)(Convert.ToUInt32(colorStr.Substring(3, 2), 16));
               byte g = (byte)(Convert.ToUInt32(colorStr.Substring(5, 2), 16));
               byte b = (byte)(Convert.ToUInt32(colorStr.Substring(7, 2), 16));
               colorBrush.Color = Color.FromArgb(a, r, g, b);
            }
            return colorBrush;
        }

        public static SolidColorBrush GetBackgroundColor(this XmlReader xr)
        {
            SolidColorBrush colorBrush = new SolidColorBrush();
            string colorStr = xr.GetAttribute("BackgroundColor");
            if (!string.IsNullOrEmpty(colorStr) && colorStr.Length == 8)
            {
                byte a = (byte)(Convert.ToUInt32(colorStr.Substring(1, 2), 16));
                byte r = (byte)(Convert.ToUInt32(colorStr.Substring(3, 2), 16));
                byte g = (byte)(Convert.ToUInt32(colorStr.Substring(5, 2), 16));
                byte b = (byte)(Convert.ToUInt32(colorStr.Substring(7, 2), 16));
                colorBrush.Color = Color.FromArgb(a, r, g, b);
            }
            return colorBrush;
        }

        public static double GetFontSizeAttribute(this XmlReader xr)
        {
            string fontSize = xr.GetAttribute("FontSize");
            if (!string.IsNullOrEmpty(fontSize))
            {
                return Convert.ToDouble(fontSize);
            }
            else
            {
                return 10;
            }
        }

        public static string GetLabelAttribute(this XmlReader xr)
        {
            return xr.GetAttribute("Label");
        }
    }
}
