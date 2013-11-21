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
using ChangeTech.Silverlight.Common;
using ChangeTech.Silverlight.DesignPage.ChangeTechWCFService;
using System.Globalization;

namespace ChangeTech.Silverlight.DesignPage.PageTemplate
{
    public partial class PushPicturesTemplate : UserControl
    {
        public PushPictureTemplatePageContentModel PageContentModel { get; set; }

        public PushPicturesTemplate()
        {
            InitializeComponent();
            PageContentModel = new PushPictureTemplatePageContentModel();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (!IsEditPagesequenceOnly())
            {
                PageContentModel.SessionGUID = new Guid(StringUtility.GetQueryString(Constants.QUERYSTR_SESSION_GUID));
            }
            ResourceManager.SelectResourceEventHandler += new SelectResourceDelegate(ResourceManager_SelectResourceEventHandler);
            //PageContentModel.LanguageGUID = new Guid(StringUtility.GetQueryString(Constants.QUERYSTR_LANGUAGE_GUID));
            PageContentModel.PageSequenceGUID = new Guid(StringUtility.GetQueryString(Constants.QUERYSTR_PAGE_SEQUENCE_GUID));
            ExpressionBuilder.SetExpressionEventHandler += new SetExpressionDelegate(ExpressionBuilder_SetExpressionEventHandler);
            pageVarible.SelectedEvent += new ChangeTech.Silverlight.DesignPage.Controls.PageVariableManager.SelectedDelegate(pageVarible_SelectedEvent);
            ImageList.OnSelectImage += new SelectPictureDelegate(SelectImageEventHandler);
        }

        private bool IsEditPagesequenceOnly()
        {
            bool flug = false;
            if (!string.IsNullOrEmpty(StringUtility.GetQueryString(Constants.QUERYSTR_EDITMODE)) && StringUtility.GetQueryString(Constants.QUERYSTR_EDITMODE).Equals(Constants.SELF))
            {
                flug = true;
            }

            return flug;
        }

        private void pageVarible_SelectedEvent(object sender, PageVariableEventArgs args)
        {
            ((TextBox)args.sender).Text += " {V:" + args.pageVariable.Name + "}";

            ServiceClient serviceProxy = ServiceProxyFactory.Instance.ServiceProxy;
            serviceProxy.UpdatePageVariableLastAccessTimeAsync(args.pageVariable.PageVariableGUID);
        }

        private void ResourceManager_SelectResourceEventHandler(object sender, SelectResourceEventArgs e)
        {
            if (e.Resource.Type.Equals(ResourceTypeEnum.Audio.ToString()))
            {
                resourceNameTextBlock.Text = e.Resource.Name;
                setVoiceHyperlinkButton.Content = "Edit sound";
                //medi
                PageContentModel.VoiceGUID = e.Resource.ID;
            }
        }

        private void ExpressionBuilder_SetExpressionEventHandler(string expression, ExpressionType expressionType)
        {
            switch (expressionType)
            {
                case ExpressionType.BeforeProperty:
                    PageContentModel.BeforeExpression = expression;
                    break;
                case ExpressionType.AfterProperty:
                    PageContentModel.AfterExpression = expression;
                    break;
            }
        }

        private void SetPresenterImageLinkButton_Click(object sender, RoutedEventArgs e)
        {
            if (PresenterImage.Tag != null)
            {
                ImageList.LastSelectedResource = (ResourceModel)PresenterImage.Tag;
            }
            ImageList.Show();
        }

        private void presenterImageReset_Click(object sender, RoutedEventArgs e)
        {
            if (PageContentModel.PresenterImageGUID != Guid.Empty)
            {
                SetPresenterImageLinkButton.Content = "Set image";
                PresenterImageNameTextBlock.Text = "";
                PageContentModel.PresenterImageGUID = Guid.Empty;
                PresenterImage.Source = null;
            }
        }

        private void SelectImageEventHandler(ResourceModel image)
        {
           SetPresenterImage(image);
        }

        private void SetPresenterImage(ResourceModel resource)
        {
            string imageUri = Constants.OriginalImageDirectory + resource.NameOnServer;
            PresenterImage.Tag = resource;
            ImageUtility.ShowImage(PresenterImage, imageUri);
            SetPresenterImageLinkButton.Content = "Change image";
            PageContentModel.PresenterImageGUID = resource.ID;
            PresenterImageNameTextBlock.Text = resource.Name;
        }

        private void BeforeShowExpressionLink_Click(object sender, RoutedEventArgs e)
        {
            ExpressionBuilder.Show(ExpressionType.BeforeProperty, PageContentModel.BeforeExpression);
        }

        private void AfterPropertyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (AfterPropertyComboBox.SelectedIndex == 0)
            //{
            //    AfterExpressionLink.Visibility = Visibility.Collapsed;
            //}
            //else if (!_isDataBinding)
            //{
            //    AfterExpressionLink.Visibility = Visibility.Visible;
            //    ExpressionBuilder.Show(ExpressionType.AfterProperty, PageContentModel.AfterExpression);
            //}
        }

        private void AfterExpressionLink_Click(object sender, RoutedEventArgs e)
        {
            ExpressionBuilder.Show(ExpressionType.AfterProperty, PageContentModel.AfterExpression);
        }

        private void PresenterImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //ImageInfo.Show((ResourceModel)(((Image)sender).Tag));
        }

        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            ResourceManager.Show(ResourceTypeEnum.Audio);
        }

        private void resetVoiceHyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            PageContentModel.VoiceGUID = Guid.Empty;
            setVoiceHyperlinkButton.Content = "set a voice";
            resourceNameTextBlock.Text = "";
        }

        private void insertPageVariable_Click(object sender, RoutedEventArgs e)
        {
            pageVarible.eventArgs = new PageVariableEventArgs(textTextBox);
            pageVarible.Show();
        }

        #region Public methods
        public void BindPageContent(EditPushPictureTemplatePageContentModel pageContentModel)
        {
            if (pageContentModel.PresenterImage != null)
            {
                SetPresenterImage(pageContentModel.PresenterImage);
            }
            waitTextBox.Text = pageContentModel.Wait;
            textTextBox.Text = pageContentModel.Text;
            PageContentModel.BeforeExpression = pageContentModel.BeforeExpression;
            PageContentModel.AfterExpression = pageContentModel.AfterExpression;

            if (pageContentModel.Media != null)
            {
                setVoiceHyperlinkButton.Content = "edit sound";
                resourceNameTextBlock.Text = pageContentModel.Media.Resource.Name;
                PageContentModel.VoiceGUID = pageContentModel.Media.Resource.ID;
            }
            //if (!string.IsNullOrEmpty(pageContentModel.AfterExpression))
            //{
            //    AfterPropertyComboBox.SelectedIndex = 1;
            //    AfterExpressionLink.Visibility = Visibility.Visible;
            //}
            //else
            //{
            //    AfterPropertyComboBox.SelectedIndex = 0;
            //}
        }

        /// <summary>
        /// Check whether user has fill in all necessary information
        /// </summary>
        /// <returns></returns>
        public string Validate()
        {
            string errorMessage = string.Empty;

            if (string.IsNullOrEmpty(waitTextBox.Text.Trim()))
            {
                errorMessage += "Please set the interval of the image to stay.\n";
            }
            else
            {
                NumberStyles style = NumberStyles.AllowDecimalPoint;
                CultureInfo culture = new CultureInfo("nb-NO");
                double result;
                string value = waitTextBox.Text.Trim();
                if (!double.TryParse(value, style, culture, out result))
                {
                    errorMessage += "Please type the interval like novigan style.";
                }
            }
            //if (PageContentModel.PresenterImageGUID == Guid.Empty)
            //{
            //    errorMessage += "Please choose a picture.";
            //}
        
            return errorMessage;
        }

        public void FillContent()
        {
            PageContentModel.Wait = waitTextBox.Text.Trim();
            PageContentModel.Text = textTextBox.Text.Trim();
        }

        public void EnableControl()
        {
            waitTextBox.IsEnabled = true;
            SetPresenterImageLinkButton.IsEnabled = true;
            presenterImageReset.IsEnabled = true;
            textTextBox.IsEnabled = true;
        }

        public void DiableControl(string reasonStr)
        {
            waitTextBox.IsEnabled = false;
            SetPresenterImageLinkButton.IsEnabled = false;
            presenterImageReset.IsEnabled = false;
            textTextBox.IsEnabled = false;
        }

        #endregion          
    }
}
