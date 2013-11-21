using System;
using System.Windows;
using System.Windows.Browser;
using System.Windows.Controls;
using System.Windows.Input;
using ChangeTech.Silverlight.Common;
using ChangeTech.Silverlight.DesignPage.ChangeTechWCFService;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ChangeTech.Silverlight.DesignPage.Controls
{
    public partial class AddExpression : UserControl
    {
        public AddExpression()
        {
            InitializeComponent();

            ExpressionGroupManager.UpdateExpressionGroupsEvent += new UpdateExpressionGroupsDelegate(ExpressionGroupManager_UpdateExpressionGroupsEvent);
        }

        private void ExpressionGroupManager_UpdateExpressionGroupsEvent(ObservableCollection<ExpressionGroupModel> expressionGroups)
        {
            ComboBoxExpressionGroup.ItemsSource = null;
            ComboBoxExpressionGroup.ItemsSource = expressionGroups;
        }

        public void Show(string expressionText)
        {
            ExpressionTextTextBox.Text = expressionText;
            Reset();

            Visibility = Visibility.Visible;

            Disabale("Loading data, please wait for seconds......");

            ServiceClient serviceClient = ServiceProxyFactory.Instance.ServiceProxy;
            serviceClient.GetExpressionGroupsOfProgramCompleted += new EventHandler<GetExpressionGroupsOfProgramCompletedEventArgs>(serviceClient_GetExpressionGroupsOfProgramCompleted);
            serviceClient.GetExpressionGroupsOfProgramAsync(new Guid(StringUtility.GetQueryString(Constants.QUERYSTR_SESSION_GUID)));
        }

        private void Reset()
        {
            ComboBoxExpressionGroup.SelectedItem = null;
            ExpressionNameTextbox.Text = string.Empty;
        }

        private void Enable()
        {
            PromptTextBlock.Text = string.Empty;
            OKButton.IsEnabled = true;
            ComboBoxExpressionGroup.IsEnabled = true;
            ExpressionNameTextbox.IsEnabled = true;
            ExpressionTextTextBox.IsEnabled = true;
            //CancelButton.IsEnabled = true;
        }

        private void Disabale(string message)
        {
            PromptTextBlock.Text = message;
            OKButton.IsEnabled = false;
            ComboBoxExpressionGroup.IsEnabled = false;
            ExpressionNameTextbox.IsEnabled = false;
            ExpressionTextTextBox.IsEnabled = false;
            //CancelButton.IsEnabled = false;
        }

        private void serviceClient_GetExpressionGroupsOfProgramCompleted(object sender, GetExpressionGroupsOfProgramCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                HtmlPage.Window.Alert("Operation is cancelled.");
            }
            else if (e.Error != null)
            {
                HtmlPage.Window.Alert("Internal error, please contact develop team.");
            }
            else
            {
                ComboBoxExpressionGroup.ItemsSource = null;
                ComboBoxExpressionGroup.ItemsSource = e.Result;

                Enable();
            }
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(ExpressionTextTextBox.Text.Trim()) &&
                !string.IsNullOrEmpty(ExpressionNameTextbox.Text.Trim()) &&
                ComboBoxExpressionGroup.SelectedItem != null)
            {
                Disabale("Saving data, please wait for seconds......");
                ServiceClient serviceProxy = ServiceProxyFactory.Instance.ServiceProxy;
                serviceProxy.AddExpressionCompleted += new EventHandler<AsyncCompletedEventArgs>(serviceProxy_AddExpressionCompleted);
                ExpressionModel expressionModel = new ExpressionModel();
                expressionModel.Name = ExpressionNameTextbox.Text.Trim();
                expressionModel.ExpressionText = ExpressionTextTextBox.Text.Trim();
                expressionModel.ExpressionGUID = Guid.NewGuid();
                expressionModel.ExpressionGroupGUID = ((ExpressionGroupModel)ComboBoxExpressionGroup.SelectedItem).ExpressionGroupGUID;
                serviceProxy.AddExpressionAsync(expressionModel);
            }
            else if (string.IsNullOrEmpty(ExpressionTextTextBox.Text.Trim()))
            {
                HtmlPage.Window.Alert("Expression text cannot be empty.");
            }
            else if (string.IsNullOrEmpty(ExpressionNameTextbox.Text.Trim()))
            {
                HtmlPage.Window.Alert("Expression name cannot be empty.");
            }
            else
            {
                HtmlPage.Window.Alert("Please choose a group.");
            }
        }

        private void serviceProxy_AddExpressionCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                HtmlPage.Window.Alert("Operation is cancelled.");
                Enable();
            }
            else if (e.Error != null)
            {
                HtmlPage.Window.Alert("Internal error, please report to ChangeTech develop team.");
            }
            else
            {
                HtmlPage.Window.Alert("Your operation is successful.");
                Enable();

                Visibility = Visibility.Collapsed;
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Collapsed;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Collapsed;
        }

        private void ManageExpressionGroupLink_Click(object sender, RoutedEventArgs e)
        {
            ExpressionGroupManager.Show();
        }

        #region Drag & Drop
        bool isMouseCaptured;
        Point mousePosition;
        bool clickOnDataGridColumn;

        private void RenameCategoryPopupPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            FrameworkElement item = sender as FrameworkElement;

            if (!clickOnDataGridColumn &&
                ExpressionGroupManager.Visibility == Visibility.Collapsed)
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
