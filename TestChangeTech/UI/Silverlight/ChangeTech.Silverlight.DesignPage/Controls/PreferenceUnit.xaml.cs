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
using System.Windows.Browser;
using ChangeTech.Silverlight.Common;

namespace ChangeTech.Silverlight.DesignPage.Controls 
{
    public partial class PreferenceUnit : UserControl 
    {
        public event AddPreferenceDelegate AddPreferenceEventHandler;
        private PreferenceItemModel PreferenceItemModel { get; set; }

        public PreferenceUnit() 
        {
            InitializeComponent();
            VariableList.SelectedEvent += new PageVariableManager.SelectedDelegate(VariableList_SelectedEvent);
            ImageList.OnSelectImage += new SelectPictureDelegate(SelectImageEventHandler);
        }

        private void VariableList_SelectedEvent(object sender, PageVariableEventArgs args)
        {
            PreferenceItemModel.Variable = new SimplePageVariableModel
            { 
                Name = args.pageVariable.Name,
                PageVariableGuid = args.pageVariable.PageVariableGUID
            };

            SetVariableLink.Content = "{V:" + args.pageVariable.Name + "}";

            ServiceClient serviceProxy = ServiceProxyFactory.Instance.ServiceProxy;
            serviceProxy.UpdatePageVariableLastAccessTimeAsync(args.pageVariable.PageVariableGUID);
        }

        private void SetVariableLink_Click(object sender, RoutedEventArgs e)
        {
            VariableList.Show();
        }

        private void ChooseImageLinkButton_Click(object sender, RoutedEventArgs e) 
        {
            if (ImageView.Tag != null)
            {
                ImageList.LastSelectedResource = (ResourceModel)ImageView.Tag;
            }
            ImageList.Show();
        }

        private void ResetImageLinkButton_Click(object sender, RoutedEventArgs e) 
        {
            if (PreferenceItemModel.Resource != null)
            {
                PreferenceItemModel.Resource = null;
                ChooseImageLinkButton.Content = "Choose image";
                ImageNameTextBlock.Text = string.Empty;
                ImageView.Source = null;
            }
        }

        private void SelectImageEventHandler(ResourceModel image) 
        {            
            string imageUri = Constants.OriginalImageDirectory + image.NameOnServer;
            ImageUtility.ShowImage(ImageView, imageUri);
            ChooseImageLinkButton.Content = "Change image";
            PreferenceItemModel.Resource = image;
            ImageView.Tag = image;
            ImageNameTextBlock.Text = image.Name;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e) 
        {
            Visibility = Visibility.Collapsed;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            string validateMsg = Validate();
            if (!string.IsNullOrEmpty(validateMsg))
            {
                HtmlPage.Window.Alert(validateMsg);
            }
            else if (AddPreferenceEventHandler != null)
            {
                PreferenceItemModel.AnswerText = AnswerTextBox.Text.Trim();
                PreferenceItemModel.Description = DescriptionTextBox.Text.Trim();
                PreferenceItemModel.Name = NameTextBox.Text.Trim();
                PreferenceItemModel.ButtonName = ButtonNameTextBox.Text.Trim();
                Visibility = Visibility.Collapsed;
                AddPreferenceEventHandler(PreferenceItemModel);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Collapsed;
        }

        public void Show(PreferenceItemModel preferenceItemModel)
        {
            if (preferenceItemModel != null)
            {
                PreferenceItemModel = preferenceItemModel;
                
                NameTextBox.Text = PreferenceItemModel.Name;
                DescriptionTextBox.Text = PreferenceItemModel.Description != null ? preferenceItemModel.Description : "";
                AnswerTextBox.Text = PreferenceItemModel.AnswerText != null ? preferenceItemModel.AnswerText : "";
                ButtonNameTextBox.Text = PreferenceItemModel.ButtonName != null ? preferenceItemModel.ButtonName : "";
                string imageUri = Constants.OriginalImageDirectory + PreferenceItemModel.Resource.NameOnServer;
                ImageNameTextBlock.Text = PreferenceItemModel.Resource.Name;
                ImageView.Tag = PreferenceItemModel.Resource;
                ImageUtility.ShowImage(ImageView, imageUri);

                if (preferenceItemModel.Variable != null)
                {
                    SetVariableLink.Content = "{V:" + preferenceItemModel.Variable.Name + "}";
                }
                else
                {
                    SetVariableLink.Content = "Click to set";
                }

                ChooseImageLinkButton.Content = "Change image";
            }
            else
            {
                PreferenceItemModel = new PreferenceItemModel();
                PreferenceItemModel.PreferenceGUID = Guid.NewGuid();
                NameTextBox.Text = string.Empty;
                DescriptionTextBox.Text = string.Empty;
                AnswerTextBox.Text = string.Empty;
                ImageView.Source = null;
                ImageNameTextBlock.Text = string.Empty;
                ButtonNameTextBox.Text = string.Empty;
                ChooseImageLinkButton.Content = "Choose image";
            }
            Visibility = Visibility.Visible;
        }

        private string Validate()
        {
            string errorMsg = string.Empty;

            if (string.IsNullOrEmpty(NameTextBox.Text.Trim()))
            {
                errorMsg += "Name is a required field, you must fill in.\n";
            }
            else if (NameTextBox.Text.Trim().Length > 50)
            {
                errorMsg += string.Format("The length of name cannot exceed 50 characters, but you have {0}.\n", NameTextBox.Text.Trim().Length);
            }
            //if (string.IsNullOrEmpty(DescriptionTextBox.Text.Trim()))
            //{
            //    errorMsg += "Description is a required field, you must fill in.\n";
            //}
            //else 
            if (DescriptionTextBox.Text.Trim().Length > 200)
            {
                errorMsg += string.Format("The length of description cannot exceed 200 characters, but you have {0}.\n", DescriptionTextBox.Text.Trim().Length);
            }
            //if (string.IsNullOrEmpty(AnswerTextBox.Text.Trim()))
            //{
            //    errorMsg += "Answer text is a required field, you must fill in.\n";
            //}
            //else 
            if (AnswerTextBox.Text.Trim().Length > 200)
            {
                errorMsg += string.Format("The length of answer cannot exceed 200 characters, but you have {0}.\n", AnswerTextBox.Text.Trim().Length);
            }
            if (PreferenceItemModel.Resource == null)
            {
                errorMsg += "Preference image must be set.\n";
            }
            //if (string.IsNullOrEmpty(ButtonNameTextBox.Text.Trim()))
            //{
            //    errorMsg += "Button name is a required filed, you must fill in.";
            //}
            //else 
            if (ButtonNameTextBox.Text.Trim().Length > 200)
            {
                errorMsg += string.Format("The length of button name cannot exceed 200 characters, but you have {0}.\n", ButtonNameTextBox.Text.Trim().Length);
            }
            return errorMsg;
        }

        private void ImageView_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //ImageInfo.Show((ResourceModel)(((Image)sender).Tag));
        }

        #region Drag & Drop
        bool isMouseCaptured;
        Point mousePosition;

        private void RenameCategoryPopupPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            if (ImageList.Visibility == Visibility.Collapsed &&
                VariableList.Visibility == Visibility.Collapsed)
            {
                FrameworkElement item = sender as FrameworkElement;
                mousePosition = e.GetPosition(null);
                isMouseCaptured = true;
                item.CaptureMouse();
                item.Cursor = Cursors.Hand;
            }
        }

        private void RenameCategoryPopupPanel_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
            FrameworkElement item = sender as FrameworkElement;
            isMouseCaptured = false;
            item.ReleaseMouseCapture();
            mousePosition.X = mousePosition.Y = 0;
            item.Cursor = null;
        }

        private void RenameCategoryPopupPanel_MouseMove(object sender, MouseEventArgs e) {
            FrameworkElement item = sender as FrameworkElement;
            if (isMouseCaptured) {

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
