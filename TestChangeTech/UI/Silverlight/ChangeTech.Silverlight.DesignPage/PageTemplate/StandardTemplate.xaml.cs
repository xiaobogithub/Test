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
using ChangeTech.Silverlight.Common;
using System.Windows.Browser;

namespace ChangeTech.Silverlight.DesignPage.PageTemplate
{
    public partial class StandardTemplate : UserControl
    {
        public StandardTemplatePageContentModel PageContentModel { get; set; }
        public Guid ImageGuid { get; set; }
        //private ImageType _currentSetImageType;
        private bool _isDataBinding = false;
        private bool _isClickOnDownloadLink = false;

        public StandardTemplate()
        {
            InitializeComponent();
        }

        private void LayoutRoot_Loaded(object sender, RoutedEventArgs e)
        {
            PageContentModel = new StandardTemplatePageContentModel();
            if(!IsEditPagesequenceOnly())
            {
                PageContentModel.SessionGUID = new Guid(StringUtility.GetQueryString(Constants.QUERYSTR_SESSION_GUID));
            }
            //PageContentModel.LanguageGUID = new Guid(StringUtility.GetQueryString(Constants.QUERYSTR_LANGUAGE_GUID));
            PageContentModel.PageSequenceGUID = new Guid(StringUtility.GetQueryString(Constants.QUERYSTR_PAGE_SEQUENCE_GUID));
            ExpressionBuilder.SetExpressionEventHandler += new SetExpressionDelegate(ExpressionBuilder_SetExpressionEventHandler);
            ResourceManager.SelectResourceEventHandler += new SelectResourceDelegate(ResourceManager_SelectResourceEventHandler);
            pageVarible.SelectedEvent += new ChangeTech.Silverlight.DesignPage.Controls.PageVariableManager.SelectedDelegate(pageVarible_SelectedEvent);
            ImageList.OnSelectImage += new SelectPictureDelegate(SelectImageEventHandler);

            PresenterImagePositionComboBox.SelectedIndex = 0;
            PresenterModeComboBox.SelectedIndex = 0;
        }

        private bool IsEditPagesequenceOnly()
        {
            bool flug = false;
            if(!string.IsNullOrEmpty(StringUtility.GetQueryString(Constants.QUERYSTR_EDITMODE)) && StringUtility.GetQueryString(Constants.QUERYSTR_EDITMODE).Equals(Constants.SELF))
            {
                flug = true;
            }

            return flug;
        }

        private void ExpressionBuilder_SetExpressionEventHandler(string expression, ExpressionType expressionType)
        {
            switch(expressionType)
            {
                case ExpressionType.AfterProperty:
                    PageContentModel.AfterExpression = expression;
                    break;
                case ExpressionType.BeforeProperty:
                    PageContentModel.BeforeExpression = expression;
                    break;
            }
        }

        private void pageVarible_SelectedEvent(object sender, PageVariableEventArgs args)
        {
            BodyTextBox.Text += " {V:" + args.pageVariable.Name + "}";

            ServiceClient serviceProxy = ServiceProxyFactory.Instance.ServiceProxy;
            serviceProxy.UpdatePageVariableLastAccessTimeAsync(args.pageVariable.PageVariableGUID);
        }

        private void VideoRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if(PageContentModel.VideoGUID == Guid.Empty)
            {
                IVRTextBlock.Text = "No video is set";
                IVRLinkButton.Content = "Set video";

                PageContentModel.RadioGUID = Guid.Empty;
                //IllustrationImage.Source = null;
                //PageContentModel.IllustrationImageGUID = Guid.Empty;
                //presenterImageReset.IsEnabled = true;
                //SetPresenterImageLinkButton.IsEnabled = true;
            }
        }

        private void RadioRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if(PageContentModel.RadioGUID == Guid.Empty)
            {
                IVRTextBlock.Text = "No sound clip is set";
                IVRLinkButton.Content = "Set sound clip";

                PageContentModel.VideoGUID = Guid.Empty;
                //IllustrationImage.Source = null;
                //PageContentModel.IllustrationImageGUID = Guid.Empty;
                //presenterImageReset.IsEnabled = true;
                //SetPresenterImageLinkButton.IsEnabled = true;
            }
        }

        private void IllustrationImageRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            ////if (PageContentModel.IllustrationImageGUID == Guid.Empty)
            ////{
            //IVRTextBlock.Text = "No illustration image is set";
            //IVRLinkButton.Content = "Set illustration image";
            //PageContentModel.VideoGUID = Guid.Empty;
            //PageContentModel.RadioGUID = Guid.Empty;
            ////}
        }

        private void IVRLinkButton_Click(object sender, RoutedEventArgs e)
        {
            _isClickOnDownloadLink = false;

            //if(IllustrationImageRadioButton.IsChecked.Value)
            //{
            //    _currentSetImageType = ImageType.Illustration;
            //    if(IllustrationImage.Tag != null)
            //    {
            //        ImageList.LastSelectedResource = (ResourceModel)IllustrationImage.Tag;
            //    }
            //    ImageList.Show();
            //}
            if(VideoRadioButton.IsChecked.Value)
            {
                ResourceManager.Show(ResourceTypeEnum.Video);
                //VidelList.Show();
            }
            else if(RadioRadioButton.IsChecked.Value)
            {
                ResourceManager.Show(ResourceTypeEnum.Audio);
                //RadioList.Show();
            }
        }

        private void SetImageLinkButton_Click(object sender, RoutedEventArgs e)
        {
            if(BackgroundImage.Tag != null)
            {
                ImageList.LastSelectedResource = (ResourceModel)BackgroundImage.Tag;
            }
            ImageList.Show();

            //if (PresenterModeRadioButton.IsChecked.Value)
            //{
            //    _currentSetImageType = ImageType.Presenter;
            //}
            //else if (IllustrationModeRadioButton.IsChecked.Value)
            //{
            //    _currentSetImageType = ImageType.Illustration;
            //}
            //else if (FullscreenModeRadioButton.IsChecked.Value)
            //{
            //    _currentSetImageType = ImageType.Background;
            //}
        }

        private void SetPresenterImageLinkButton_Click(object sender, RoutedEventArgs e)
        {
            //if(PresenterImage.Tag != null)
            //{
            //    ImageList.LastSelectedResource = (ResourceModel)PresenterImage.Tag;
            //}
            //ImageList.Show();
            //_currentSetImageType = ImageType.Presenter;
        }

        private void SelectImageEventHandler(ResourceModel image)
        {
            SetImage(image);
            //if(_currentSetImageType == ImageType.Background)
            //{
            //    SetImage(image);
            //}
            //else if(_currentSetImageType == ImageType.Presenter)
            //{
            //    SetPresenterImage(image);
            //}
            //else if(_currentSetImageType == ImageType.Illustration)
            //{
            //    SetIllustrationImage(image);
            //}
        }

        private void bgiReset_Click(object sender, RoutedEventArgs e)
        {
            if(PageContentModel.BackgroundImageGUID != Guid.Empty)
            {
                SetImageLinkButton.Content = "Set image";
                BackgroundImageNameTextBlock.Text = "";
                BackgroundImage.Source = null;
                PageContentModel.BackgroundImageGUID = Guid.Empty;
            }
        }

        private void presenterImageReset_Click(object sender, RoutedEventArgs e)
        {
            ////if (PageContentModel.PresenterImageGUID != Guid.Empty)
            ////{
            //PresenterImageNameTextBlock.Text = "";
            //SetPresenterImageLinkButton.Content = "Set presernter image";
            //PresenterImage.Source = null;
            //PageContentModel.PresenterImageGUID = Guid.Empty;
            //PresenterImagePositionComboBox.SelectedIndex = 0;
            ////}
            ////IllustrationImageRadioButton.IsEnabled = true;
        }

        private void SetImage(ResourceModel resource)
        {
            string imageUri = Constants.ThumbImageDirectory + resource.NameOnServer;
            BackgroundImage.Tag = resource;

            ImageUtility.ShowImage(BackgroundImage, imageUri);
            BackgroundImageNameTextBlock.Text = resource.Name;
            SetImageLinkButton.Content = "Change image";
            PageContentModel.BackgroundImageGUID = resource.ID;
            ImageGuid = resource.ID;
            presenterImageReset.IsEnabled = true;
            SetPresenterImageLinkButton.IsEnabled = true;
        }

        private void SetPresenterImage(ResourceModel resource)
        {
            //string imageUri = Constants.ThumbImageDirectory + resource.NameOnServer;
            //PresenterImage.Tag = resource;

            //ImageUtility.ShowImage(PresenterImage, imageUri);
            //PresenterImageNameTextBlock.Text = resource.Name;
            //SetPresenterImageLinkButton.Content = "Change presenter image";
            //PageContentModel.PresenterImageGUID = resource.ID;
            ////IllustrationImageRadioButton.IsEnabled = false;
            #region MyRegion
            //if (IllustrationImageRadioButton.IsChecked.HasValue && IllustrationImageRadioButton.IsChecked.Value)
            //{
            //    IllustrationImageRadioButton.IsChecked = false;                
            //    IVRTextBlock.Text = "";
            //    IVRLinkButton.Content = "";

            //    IllustrationImage.Source = null;
            //    PageContentModel.IllustrationImageGUID = Guid.Empty;
            //} 
            #endregion
        }

        private void SetIllustrationImage(ResourceModel resource)
        {
            //string imageUri = Constants.ThumbImageDirectory + resource.NameOnServer;
            //IllustrationImage.Tag = resource;

            //ImageUtility.ShowImage(IllustrationImage, imageUri);
            //PageContentModel.IllustrationImageGUID = resource.ID;
            //IVRLinkButton.Content = "Edit illustration image";
            //IVRTextBlock.Text = resource.Name;
            //IllustrationImageRadioButton.IsChecked = true;
            ////presenterImageReset.IsEnabled = false;
            ////SetPresenterImageLinkButton.IsEnabled = false;
        }

        private void ResourceManager_SelectResourceEventHandler(object sender, SelectResourceEventArgs e)
        {
            if(_isClickOnDownloadLink)
            {
                string resourceURL = StringUtility.GetApplicationPath() + "{0}{1}";
                string fordownloadresourceURL = StringUtility.GetApplicationPath() + "RequestResource.aspx?target={0}&media={1}&name={2}";
                string downloadLink = "<A href='{0}' target='_blank'>{1}</A>";
                if (e.Resource.Type.Equals(ResourceTypeEnum.Image.ToString()))
                {
                    resourceURL = string.Format(fordownloadresourceURL, Constants.OriginalImageDirectory, e.Resource.NameOnServer, e.Resource.Name);
                }
                else if (e.Resource.Type.Equals(ResourceTypeEnum.Document.ToString()))
                {
                    resourceURL = string.Format(fordownloadresourceURL, ResourceTypeEnum.Document.ToString(), e.Resource.NameOnServer, e.Resource.Name);
                }
                else if (e.Resource.Type.Equals(ResourceTypeEnum.Video.ToString()))
                {
                    resourceURL = string.Format(fordownloadresourceURL, ResourceTypeEnum.Video, e.Resource.NameOnServer, e.Resource.Name);
                }
                else if (e.Resource.Type.Equals(ResourceTypeEnum.Audio.ToString()))
                {
                    resourceURL = string.Format(fordownloadresourceURL, ResourceTypeEnum.Audio, e.Resource.NameOnServer, e.Resource.Name);
                }

                if(!string.IsNullOrEmpty(BodyTextBox.Text))
                {
                    BodyTextBox.Text += "\n" + string.Format(downloadLink, resourceURL, e.Resource.Name);
                }
                else
                {
                    BodyTextBox.Text += string.Format(downloadLink, resourceURL, e.Resource.Name);
                }
            }
            else if (e.Resource.Type.Equals(ResourceTypeEnum.Audio.ToString()))
            {
                IVRTextBlock.Text = e.Resource.Name;
                IVRLinkButton.Content = "Edit sound clip";
                PageContentModel.RadioGUID = e.Resource.ID;
            }
            else if (e.Resource.Type.Equals(ResourceTypeEnum.Video.ToString()))
            {
                IVRTextBlock.Text = e.Resource.Name;
                IVRLinkButton.Content = "Edit video";
                PageContentModel.VideoGUID = e.Resource.ID;
            }
        }

        private void PrimaryButtonActionComoboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(PrimaryButtonActionComoboBox.SelectedIndex == 0)
            {
                PageContentModel.PrimaryButtonAction = "0";
                PageContentModel.AfterExpression = string.Empty;
                PrimaryButtonActionLinkButton.Visibility = Visibility.Collapsed;
            }
            else if (PrimaryButtonActionComoboBox.SelectedIndex == 1)
            {
                PageContentModel.AfterExpression = "EndPage";
                PrimaryButtonActionLinkButton.Visibility = Visibility.Collapsed;
            }
            else if(!_isDataBinding)
            {
                ExpressionBuilder.Show(ExpressionType.AfterProperty, PageContentModel.AfterExpression);
                PrimaryButtonActionLinkButton.Visibility = Visibility.Visible;
            }
        }

        private void PrimaryButtonActionLinkButton_Click(object sender, RoutedEventArgs e)
        {
            ExpressionBuilder.Show(ExpressionType.AfterProperty, PageContentModel.AfterExpression);
        }

        public void FillContent()
        {
            PageContentModel.Heading = HeadingTextBox.Text.Trim();
            PageContentModel.PrimaryButtonCaption = PrimaryButtonTextBox.Text.Trim();
            PageContentModel.Body = BodyTextBox.Text.Trim();
        }

        /// <summary>
        /// Check whether user has fill in all necessary information
        /// </summary>
        /// <returns></returns>
        public string Validate()
        {
            string errorMessage = string.Empty;

            if(string.IsNullOrEmpty(HeadingTextBox.Text.Trim()))
            {
                //errorMessage += "Please fill in title of page.\n";
            }
            else if(HeadingTextBox.Text.Trim().Length > 80)
            {
                errorMessage += string.Format("The length of title cannot exceed 80 characters, but you have {0}.\n", HeadingTextBox.Text.Trim().Length);
            }

            if(string.IsNullOrEmpty(BodyTextBox.Text.Trim()))
            {
                //errorMessage += "Please fill in text of page.\n";
            }
            if(string.IsNullOrEmpty(PrimaryButtonTextBox.Text.Trim()))
            {
                errorMessage += "Please fill in button name of primary button.\n";
            }
            else if(PrimaryButtonTextBox.Text.Trim().Length > 80)
            {
                errorMessage += string.Format("The length of primary button name cannot exceed 80 characters, but you have {0}. \n", PrimaryButtonTextBox.Text.Trim().Length);
            }

            if(PrimaryButtonActionComoboBox.SelectedItem == null)
            {
                errorMessage += "Please choose action of primary button.\n";
            }
            //if (PageContentModel.PresenterImageGUID == Guid.Empty 
            //    && PageContentModel.BackgroundImageGUID == Guid.Empty
            //    && PageContentModel.IllustrationImageGUID == Guid.Empty)
            //{
            //    errorMessage += "Please choose image and image mode.\n";
            //}
            //if(PageContentModel.PresenterImageGUID != Guid.Empty &&
            //    PresenterImagePositionComboBox.SelectedItem == null)
            //{
            //    errorMessage += "Please choose position of presenter image.\n";
            //}
            //if(PageContentModel.PresenterImageGUID != Guid.Empty &&
            //    PresenterModeComboBox.SelectedItem == null)
            //{
            //    errorMessage += "Please choose mode of presenter image.\n";
            //}

            return errorMessage;
        }

        /// <summary>
        /// Bind existing data in Edit mode
        /// </summary>
        /// <param name="editStandardTemplatePageContentModel"></param>
        public void BindPageContent(EditStandardTemplatePageContentModel editStandardTemplatePageContentModel)
        {
            _isDataBinding = true;
            HeadingTextBox.Text = editStandardTemplatePageContentModel.Heading;
            BodyTextBox.Text = editStandardTemplatePageContentModel.Body;
            PrimaryButtonTextBox.Text = editStandardTemplatePageContentModel.PrimaryButtonCaption;
            PageContentModel.BeforeExpression = editStandardTemplatePageContentModel.BeforeExpression;
            PageContentModel.AfterExpression = editStandardTemplatePageContentModel.AfterExpression;

            if(string.IsNullOrEmpty(editStandardTemplatePageContentModel.AfterExpression))
            {
                PrimaryButtonActionComoboBox.SelectedIndex = 0;
            }
            else if (editStandardTemplatePageContentModel.AfterExpression.Trim().ToLower().Equals("endpage"))
            {
                PrimaryButtonActionComoboBox.SelectedIndex = 1;
                PrimaryButtonActionLinkButton.Visibility = Visibility.Visible;
            }
            else
            {
                PrimaryButtonActionComoboBox.SelectedIndex = 2;
                PrimaryButtonActionLinkButton.Visibility = Visibility.Visible;
            }

            if(editStandardTemplatePageContentModel.Media != null)
            {
                switch(editStandardTemplatePageContentModel.Media.Type)
                {
                    //case "Illustration":
                    //    SetIllustrationImage(editStandardTemplatePageContentModel.Media.Resource);
                    //    break;
                    case "Video":
                        PageContentModel.VideoGUID = editStandardTemplatePageContentModel.Media.Resource.ID;
                        VideoRadioButton.IsChecked = true;
                        IVRTextBlock.Text = editStandardTemplatePageContentModel.Media.Resource.Name;
                        IVRLinkButton.Content = "Edit video";
                        break;
                    case "Audio":
                        PageContentModel.RadioGUID = editStandardTemplatePageContentModel.Media.Resource.ID;
                        RadioRadioButton.IsChecked = true;
                        IVRTextBlock.Text = editStandardTemplatePageContentModel.Media.Resource.Name;
                        IVRLinkButton.Content = "Edit sound clip";
                        break;
                }
            }

            if(editStandardTemplatePageContentModel.BackgroudImage != null)
            {
                SetImage(editStandardTemplatePageContentModel.BackgroudImage);
                if (editStandardTemplatePageContentModel.ImageMode == ImageModeEnum.FullscreenMode)
                {
                    FullscreenModeRadioButton.IsChecked = true;
                }
            }
            else if (editStandardTemplatePageContentModel.PresenterImage!=null)
            {
                SetImage(editStandardTemplatePageContentModel.PresenterImage);
                if (editStandardTemplatePageContentModel.ImageMode == ImageModeEnum.PresenterMode)
                {
                    PresenterModeRadioButton.IsChecked = true;
                }
            }
            else if (editStandardTemplatePageContentModel.IllustrationImage != null)
            {
                SetImage(editStandardTemplatePageContentModel.IllustrationImage);
                if (editStandardTemplatePageContentModel.ImageMode == ImageModeEnum.IllustrationMode)
                {
                    IllustrationModeRadioButton.IsChecked = true;
                }
            }

            PresenterModeComboBox.SelectedIndex = 0;
            PageContentModel.PresenterMode = "Normal";
            #region MyRegion
            //if(editStandardTemplatePageContentModel.PresenterImage != null)
            //{
            //    SetPresenterImage(editStandardTemplatePageContentModel.PresenterImage);

            //    if(editStandardTemplatePageContentModel.PresenterImagePosition.Equals("Left"))
            //    {
            //        PresenterImagePositionComboBox.SelectedIndex = 0;
            //        PageContentModel.PresenterImagePosition = "Left";
            //    }
            //    else
            //    {
            //        PresenterImagePositionComboBox.SelectedIndex = 1;
            //        PageContentModel.PresenterImagePosition = "Right";
            //    }

            //    if(!string.IsNullOrEmpty(editStandardTemplatePageContentModel.PresenterImageMode))
            //    {
            //        if(editStandardTemplatePageContentModel.PresenterImageMode.Equals("Big"))
            //        {
            //            PresenterModeComboBox.SelectedIndex = 1;
            //            PageContentModel.PresenterMode = "Big";
            //        }
            //        else
            //        {
            //            PresenterModeComboBox.SelectedIndex = 0;
            //            PageContentModel.PresenterMode = "Normal";
            //        }
            //    }
            //} 
            #endregion
            _isDataBinding = false;
        }

        public void EnableControl()
        {
            HeadingTextBox.IsEnabled = true;
            PrimaryButtonTextBox.IsEnabled = true;
            BeforeShowExpressionLink.IsEnabled = true;
            PrimaryButtonActionComoboBox.IsEnabled = true;
            PrimaryButtonActionLinkButton.IsEnabled = true;
            SetTextVariableLink.IsEnabled = true;
            IllustrationImageRadioButton.IsEnabled = true;
            VideoRadioButton.IsEnabled = true;
            RadioRadioButton.IsEnabled = true;
            SetImageLinkButton.IsEnabled = true;
            bgiReset.IsEnabled = true;
            //SetPresenterImageLinkButton.IsEnabled = true;
            //presenterImageReset.IsEnabled = true;
            //PresenterImagePositionComboBox.IsEnabled = true;
            //PresenterModeComboBox.IsEnabled = true;
            BodyTextBox.IsEnabled = true;
            AddDownloadResourceLink.IsEnabled = true;
            IllustrationModeRadioButton.IsEnabled = true;
            PresenterModeRadioButton.IsEnabled = true;
            FullscreenModeRadioButton.IsEnabled = true;
        }

        public void DisableControl(string reasonStr)
        {
            HeadingTextBox.IsEnabled = false;
            PrimaryButtonTextBox.IsEnabled = false;
            BeforeShowExpressionLink.IsEnabled = false;
            PrimaryButtonActionComoboBox.IsEnabled = false;
            PrimaryButtonActionLinkButton.IsEnabled = false;
            SetTextVariableLink.IsEnabled = false;
            IllustrationImageRadioButton.IsEnabled = false;
            VideoRadioButton.IsEnabled = false;
            RadioRadioButton.IsEnabled = false;
            SetImageLinkButton.IsEnabled = false;
            bgiReset.IsEnabled = false;
            //SetPresenterImageLinkButton.IsEnabled = false;
            //presenterImageReset.IsEnabled = false;
            //PresenterImagePositionComboBox.IsEnabled = false;
            //PresenterModeComboBox.IsEnabled = false;
            BodyTextBox.IsEnabled = false;
            AddDownloadResourceLink.IsEnabled = false;
            IllustrationModeRadioButton.IsEnabled = false;
            PresenterModeRadioButton.IsEnabled = false;
            FullscreenModeRadioButton.IsEnabled = false;
        }

        private void SetTextVariableLink_Click(object sender, RoutedEventArgs e)
        {
            pageVarible.Show();
        }

        private void BeforeShowExpressionLink_Click(object sender, RoutedEventArgs e)
        {
            ExpressionBuilder.Show(ExpressionType.BeforeProperty, PageContentModel.BeforeExpression);
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //ImageInfo.Show((ResourceModel)(((Image)sender).Tag));
        }

        private void AddDownloadResourceLink_Click(object sender, RoutedEventArgs e)
        {
            _isClickOnDownloadLink = true;

            ResourceManager.Show();
        }

        private void PresenterImagePositionComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PresenterImagePositionComboBox.SelectedIndex == 0)
            {
                PageContentModel.PresenterImagePosition = "Left";
            }
            else if (PresenterImagePositionComboBox.SelectedIndex == 1)
            {
                PageContentModel.PresenterImagePosition = "Right";
            }
            else
            {
                PageContentModel.PresenterImagePosition = string.Empty;
            }
        }

        private void PresenterModeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PresenterModeComboBox.SelectedIndex == 0)
            {
                PageContentModel.PresenterMode = "Normal";
            }
            else if (PresenterModeComboBox.SelectedIndex == 1)
            {
                PageContentModel.PresenterMode = "Big";
            }
            else
            {
                PageContentModel.PresenterMode = string.Empty;
            }
        }

    }
}
