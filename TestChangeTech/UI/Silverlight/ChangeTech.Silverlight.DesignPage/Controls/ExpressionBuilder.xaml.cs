using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ChangeTech.Silverlight.DesignPage.ChangeTechWCFService;
using System.Windows.Browser;
using ChangeTech.Silverlight.Common;

namespace ChangeTech.Silverlight.DesignPage.Controls
{
    public partial class ExpressionBuilder : UserControl
    {
        public PageVariableModel Variable { get; set; }
        public SimplePageContentModel Target { get; set; }
        public ExpressionType ExpressionType { get; set; }
        public event SetExpressionDelegate SetExpressionEventHandler;

        public ExpressionBuilder()
        {
            InitializeComponent();

            VariableList.SelectedEvent += new PageVariableManager.SelectedDelegate(VariableList_SelectedEvent);
            PageList.SelectPageEventHandler += new SelectPageDelegate(PageList_SelectPageEventHandler);
            VariableQuestion.SelectQuestionItemEventHandler += new SelectQuestionItemDelegate(VariableQuestion_SelectQuestionItemEventHandler);
            ExpressionManager.SelectExpressionEventHandler += new SelectExpressionDelegate(ExpressionManager_SelectExpressionEventHandler);
            RelapseList.SelectRelapseHandler += new SelectRelapseDelegate(RelapseList_SelectRelapseHandler);
        }

        private void RelapseList_SelectRelapseHandler(RelapseModel relapse)
        {
            ExpressionTextBox.Text += " {Relapse:" + relapse.PageSequenceGUID.ToString().ToUpper() + "}";
        }

        private void ExpressionManager_SelectExpressionEventHandler(string expressionText)
        {
            ExpressionTextBox.Text += " " + expressionText;
        }

        private void VariableQuestion_SelectQuestionItemEventHandler(PageQuestionItemModel questionItem)
        {
            //ValueTextBox.Text = Convert.ToString(questionItem.Score);
        }

        private void PageList_SelectPageEventHandler(SimplePageContentModel simplePageContentModel, ButtonType buttonType, PositionType positionType)
        {
            Target = simplePageContentModel;

            if (positionType == PositionType.Relative)
            {
                ExpressionTextBox.Text += string.Format(" {0}.{1}", Target.SequenceOrder, Target.Order);
            }
            else
            {
                ExpressionTextBox.Text += " [Page:" + string.Format("{0}.{1}", Target.SequenceOrder, Target.Order) + "]";
            }
        }

        private void VariableList_SelectedEvent(object sender, PageVariableEventArgs args)
        {
            Variable = args.pageVariable;
            //VariableTextBox.Text = "{V:"+args.pageVariable.Name+"}";
            ExpressionTextBox.Text += " {V:" + Variable.Name + "}";

            ServiceClient serviceProxy = ServiceProxyFactory.Instance.ServiceProxy;
            serviceProxy.UpdatePageVariableLastAccessTimeAsync(Variable.PageVariableGUID);
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            bool result = HtmlPage.Window.Confirm("You haven't saved your data, do you want to close?");
            if (result == true)
            {
                Visibility = Visibility.Collapsed;
            }
        }

        public void Show(ExpressionType expressionType, string expression)
        {
            ExpressionType = expressionType;
            if (!string.IsNullOrEmpty(expression))
            {
                ExpressionTextBox.Text = expression;
            }
            else
            {
                ExpressionTextBox.Text = string.Empty;
            }
            Visibility = Visibility.Visible;
        }       

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(ExpressionTextBox.Text) && ValidateExpression(true))
            {
                Visibility = Visibility.Collapsed;

                if (SetExpressionEventHandler != null)
                {
                    SetExpressionEventHandler(ExpressionTextBox.Text.Trim(), ExpressionType);
                }
            }
            else if (string.IsNullOrEmpty(ExpressionTextBox.Text))
            {
                MessageBoxResult result = MessageBox.Show("Do you want to set expression as empty?", "Warn", MessageBoxButton.OKCancel);
                switch (result)
                {
                    case MessageBoxResult.OK:
                        Visibility = Visibility.Collapsed;
                        if (SetExpressionEventHandler != null)
                        {
                            SetExpressionEventHandler(ExpressionTextBox.Text.Trim(), ExpressionType);
                        }
                        break;
                    case MessageBoxResult.Cancel:
                        break;
                }
            }
        }

        private void SelectVariableLink_Click(object sender, RoutedEventArgs e)
        {
            VariableList.Show();
        }

        private void PageListLink_Click(object sender, RoutedEventArgs e)
        {
            PageList.Show(ButtonType.PrimaryButton);
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Collapsed;
        }

        private void SelectValueLink_Click(object sender, RoutedEventArgs e)
        {
            VariableQuestion.Show(Variable);
        }

        private void ValidateButton_Click(object sender, RoutedEventArgs e)
        {
            ValidateExpression(false);
        }

        private bool ValidateExpression(bool silentOnValid)
        {
            bool isValid = false;
            try
            {
                switch (ExpressionType)
                {
                    case ExpressionType.GraphDataItemExpression:
                        isValid = ExpressionParseUtility.ParseGraphDateItemExpression(ExpressionTextBox.Text);
                        break;
                    case ExpressionType.AfterProperty:
                    case ExpressionType.BeforeProperty:
                        isValid = ExpressionParseUtility.IsValidExpression(ExpressionTextBox.Text);
                        break;
                }

                if (!isValid)
                {
                    MessageBox.Show("Invalid page expression.", "Error", MessageBoxButton.OK);
                }
                else if (!silentOnValid)
                {
                    MessageBox.Show("Your expression has passed validation.", "Info", MessageBoxButton.OK);
                }
            }
            catch (ArgumentException ae)
            {
                MessageBox.Show(ae.Message);
            }

            return isValid;
        }

        private void AddFunctionButton_Click(object sender, RoutedEventArgs e)
        {
            if (FuctionComboBox.SelectedItem != null)
            {
                ExpressionTextBox.Text += string.Format(" {0}", ((ComboBoxItem)FuctionComboBox.SelectedItem).Content.ToString());
            }
            else
            {
                MessageBox.Show("No function is selected.", "Error", MessageBoxButton.OK);
            }
        }

        private void AddOperatorButton_Click(object sender, RoutedEventArgs e)
        {
            if (OperatorComboBox.SelectedItem != null)
            {
                ExpressionTextBox.Text += string.Format(" {0}", ((ComboBoxItem)OperatorComboBox.SelectedItem).Content.ToString());
            }
            else
            {
                MessageBox.Show("No operator is selected.", "Error", MessageBoxButton.OK);
            }
        }

        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            string documentURL = HtmlPage.Document.DocumentUri.ToString();
            string[] temp = documentURL.Split(new char[] { '?' });
            string helperURL = string.Empty;
            if (temp[0].Contains("EditPage.aspx"))
            {
                helperURL = temp[0].Replace("EditPage.aspx", "ExpressionHelper.htm");
            }
            else
            {
                helperURL = temp[0].Replace("AddPage.aspx", "ExpressionHelper.htm");
            }

            HtmlPage.Window.Navigate(new Uri(helperURL), "_blank");
        }

        private void SelectExpressionButton_Click(object sender, RoutedEventArgs e)
        {
            ExpressionManager.Show();
        }

        private void AddExpressionButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateExpression(true))
            {
                AddExpression.Show(ExpressionTextBox.Text.Trim());
            }
        }

        private void AddRelapseButton_Click(object sender, RoutedEventArgs e)
        {
            RelapseList.DisplayRelapse();
        }

        #region Drag & Drop
        bool isMouseCaptured;
        Point mousePosition;
        bool clickOnDataGridColumn;

        private void RenameCategoryPopupPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            FrameworkElement item = sender as FrameworkElement;

            if (!clickOnDataGridColumn &&
                VariableList.Visibility == Visibility.Collapsed &&
                PageList.Visibility == Visibility.Collapsed &&
                VariableQuestion.Visibility == Visibility.Collapsed &&
                AddExpression.Visibility == Visibility.Collapsed &&
                ExpressionManager.Visibility == Visibility.Collapsed &&
                RelapseList.Visibility == Visibility.Collapsed)
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
