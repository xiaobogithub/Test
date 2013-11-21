using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Xml;
using ChangeTech.Silverlight.Common;
using ChangeTech.Silverlight.PreviewPage.ChangeTechWCFService;
using System.ServiceModel;

namespace ChangeTech.Silverlight.PreviewPage
{
    public partial class MainPage : UserControl
    {
        private string _webServiceURL;
        public MainPage(string pageGUID, string webserviceUrl)
        {
            _webServiceURL = webserviceUrl;
            InitializeComponent();
            LoadControls(new Guid(pageGUID));
        }

        private void LoadControls(Guid pageGUID)
        {
            ServiceClient proxy = new ServiceClient(new BasicHttpBinding(), new EndpointAddress(_webServiceURL));
            proxy.GetPagePreviewModelXMLCompleted += new EventHandler<GetPagePreviewModelXMLCompletedEventArgs>(proxy_GetPagePreviewModelXMLCompleted);
            proxy.GetPagePreviewModelXMLAsync(pageGUID.ToString());
        }

        void proxy_GetPagePreviewModelXMLCompleted(object sender, GetPagePreviewModelXMLCompletedEventArgs e)
        {
            string xmlSource = e.Result;
            using (XmlReader xr = XmlReader.Create(new StringReader(xmlSource)))
            {
                xr.ReadToFollowing("Control");
                do
                {
                    if (xr.IsStartElement())
                    {
                        string controlType = xr.GetControlTypeArrtibute();
                        switch (controlType)
                        {
                            case "TextBlock":
                                CreateTextBlock(xr);
                                break;
                            case "Button":
                                CreateButton(xr);
                                break;
                            case "RadioButton":
                                CreateRadioButton(xr);
                                break;
                            case "CheckBox":
                                CreateCheckBox(xr);
                                break;
                        }
                    }
                } while (xr.ReadToFollowing("Control"));
            }
        }

        private void CreateButton(XmlReader xr)
        {
            Button btn = new Button();
            xr.ReadToFollowing("Property");
            do
            {
                if (xr.IsStartElement())
                {
                    string propertyName = xr.GetAttribute("Name");
                    switch (propertyName)
                    {
                        case "Width":
                            btn.Width = StringUtility.ConvetToWidth(xr.GetAttribute("Value"));
                            break;
                        case "Height":
                            btn.Height = StringUtility.ConvertToHight(xr.GetAttribute("Value"));
                            break;
                        //case "ForegroundColor":
                        //    btn.Foreground = StringUtility.ConvertToColor(xr.GetAttribute("Value"));
                        //    break;
                        //case "BackgroundColor":
                        //    btn.Background = StringUtility.ConvertToColor(xr.GetAttribute("Value"));
                        //    break;
                        case "PositionX":
                            Canvas.SetLeft(btn, StringUtility.ConvertToPosition(xr.GetAttribute("Value")));
                            break;
                        case "PositionY":
                            Canvas.SetTop(btn, StringUtility.ConvertToPosition(xr.GetAttribute("Value")));
                            break;
                        case "FontSize":
                            btn.FontSize = StringUtility.ConvertToFontSize(xr.GetAttribute("Value"));
                            break;
                        case "Label":
                            btn.Content = xr.GetAttribute("Value");
                            break;
                    }
                }
            } while (xr.ReadToNextSibling("Property"));
            PageContainer.Children.Add(btn);
        }

        private void CreateCheckBox(XmlReader xr)
        {
            CheckBox cb = new CheckBox();
            xr.ReadToFollowing("Property");
            do
            {
                if (xr.IsStartElement())
                {
                    string propertyName = xr.GetAttribute("Name");
                    switch (propertyName)
                    {
                        case "Width":
                            cb.Width = StringUtility.ConvetToWidth(xr.GetAttribute("Value"));
                            break;
                        case "Height":
                            cb.Height = StringUtility.ConvertToHight(xr.GetAttribute("Value"));
                            break;
                        //case "BackgroundColor":
                        //    cb.Background = StringUtility.ConvertToColor(xr.GetAttribute("Value"));
                        //    break;
                        //case "ForegroundColor":
                        //    cb.Foreground = StringUtility.ConvertToColor(xr.GetAttribute("Value"));
                        //    break;
                        case "PositionX":
                            Canvas.SetLeft(cb, StringUtility.ConvertToPosition(xr.GetAttribute("Value")));
                            break;
                        case "PositionY":
                            Canvas.SetTop(cb, StringUtility.ConvertToPosition(xr.GetAttribute("Value")));
                            break;
                        case "FontSize":
                            cb.FontSize = StringUtility.ConvertToFontSize(xr.GetAttribute("Value"));
                            break;
                        case "Label":
                            cb.Content = xr.GetAttribute("Value");
                            break;
                    }
                }
            } while (xr.ReadToNextSibling("Property"));

            PageContainer.Children.Add(cb);
        }

        //private void CreateRadioButtonList(XmlReader xr)
        //{
        //    StackPanel sp = new StackPanel();
        //    sp.Name = xr.GetNameAttribute();
        //    sp.Width = xr.GetWidthAttribute();
        //    sp.Height = xr.GetHeightAttribute();
        //    Point p = xr.GetPositionAttribute();
        //    Canvas.SetLeft(sp, p.X);
        //    Canvas.SetTop(sp, p.Y);

        //    using (XmlReader subXr = xr.ReadSubtree())
        //    {
        //        subXr.ReadToDescendant("Control");
        //        subXr.ReadToDescendant("Control");
        //        do
        //        {
        //            if (subXr.IsStartElement())
        //            {
        //                CreateRadioButton(subXr, sp);
        //            }
        //        } while (subXr.Read());
        //    }

        //    PageContainer.Children.Add(sp);
        //}

        private void CreateRadioButton(XmlReader xr)
        {
            RadioButton rb = new RadioButton();

            xr.ReadToFollowing("Property");
            do
            {
                if (xr.IsStartElement())
                {
                    string propertyName = xr.GetAttribute("Name");
                    switch (propertyName)
                    {
                        case "Width":
                            rb.Width = StringUtility.ConvetToWidth(xr.GetAttribute("Value"));
                            break;
                        case "Height":
                            rb.Height = StringUtility.ConvertToHight(xr.GetAttribute("Value"));
                            break;
                        //case "BackgroundColor":
                        //    rb.Background = StringUtility.ConvertToColor(xr.GetAttribute("Value"));
                        //    break;
                        //case "ForegroundColor":
                        //    rb.Foreground = StringUtility.ConvertToColor(xr.GetAttribute("Value"));
                        //    break;
                        case "PositionX":
                            Canvas.SetLeft(rb, StringUtility.ConvertToPosition(xr.GetAttribute("Value")));
                            break;
                        case "PositionY":
                            Canvas.SetTop(rb, StringUtility.ConvertToPosition(xr.GetAttribute("Value")));
                            break;
                        case "FontSize":
                            rb.FontSize = StringUtility.ConvertToFontSize(xr.GetAttribute("Value"));
                            break;
                        case "Label":
                            rb.Content = xr.GetAttribute("Value");
                            break;
                    }
                }
            } while (xr.ReadToNextSibling("Property"));

            PageContainer.Children.Add(rb);
        }

        private void CreateTextBlock(XmlReader xr)
        { 
            TextBlock tb = new TextBlock();
            //tb.Name = xr.GetNameAttribute();

            xr.ReadToFollowing("Property");
            do
            {
                if (xr.IsStartElement())
                {
                    string propertyName = xr.GetAttribute("Name");
                    switch (propertyName)
                    {
                        case "Width":
                            tb.Width = StringUtility.ConvetToWidth(xr.GetAttribute("Value"));
                            break;
                        case "Height":
                            tb.Height = StringUtility.ConvertToHight(xr.GetAttribute("Value"));
                            break;
                        //case "ForegroundColor":
                        //    tb.Foreground = StringUtility.ConvertToColor(xr.GetAttribute("Value"));
                        //    break;
                        case "PositionX":
                            Canvas.SetLeft(tb, StringUtility.ConvertToPosition(xr.GetAttribute("Value")));
                            break;
                        case "PositionY":
                            Canvas.SetTop(tb, StringUtility.ConvertToPosition(xr.GetAttribute("Value")));
                            break;
                        case "FontSize":
                            tb.FontSize = StringUtility.ConvertToFontSize(xr.GetAttribute("Value"));
                            break;
                        case "Text":
                            tb.Text = xr.GetAttribute("Value");
                            break;
                    }
                }
            } while (xr.ReadToNextSibling("Property"));

            PageContainer.Children.Add(tb);
        }

        private void CreateTextBox(XmlReader xr)
        {
            TextBox tb = new TextBox();
            tb.Name = xr.GetNameAttribute();
            tb.Width = xr.GetWidthAttribute();
            tb.Height = xr.GetHeightAttribute();
            tb.Text = xr.GetValueAttribute();
            Point p = xr.GetPositionAttribute();
            Canvas.SetLeft(tb, p.X);
            Canvas.SetTop(tb, p.Y);

            PageContainer.Children.Add(tb);
        }
    }
}
