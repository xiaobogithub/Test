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

namespace ChangeTech.Silverlight.DesignPage.PageTemplate
{
    public partial class TimerTemplate : UserControl
    {
        public TimerTemplatePageContentModel PageContentModel { get; set; }
        private bool _isDataBinding = false;

        public TimerTemplate()
        {
            InitializeComponent();
            PageContentModel = new TimerTemplatePageContentModel();
            ExpressionBuilder.SetExpressionEventHandler += new SetExpressionDelegate(ExpressionBuilder_SetExpressionEventHandler);
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

        private void UserControl_Loaded(object sender, RoutedEventArgs e) 
        {
            if (!IsEditPagesequenceOnly())
            {
                PageContentModel.SessionGUID = new Guid(StringUtility.GetQueryString(Constants.QUERYSTR_SESSION_GUID));
            }
            //PageContentModel.LanguageGUID = new Guid(StringUtility.GetQueryString(Constants.QUERYSTR_LANGUAGE_GUID));
            PageContentModel.PageSequenceGUID = new Guid(StringUtility.GetQueryString(Constants.QUERYSTR_PAGE_SEQUENCE_GUID));
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

        private void SelectVaribleHandler(object sender, PageVariableEventArgs e)
        {
            if (e.pageVariable != null)
            {
                SimplePageVariableModel variableModel = new SimplePageVariableModel 
                {
                    Name = e.pageVariable.Name,
                    PageVariableGuid = e.pageVariable.PageVariableGUID
                };
                SetPageVariable(variableModel);
            }
        }

        private void SetPageVariable(SimplePageVariableModel variable)
        {
            variableHyperlinkButtion.Content = "Edit variable";
            PageContentModel.PageVariableGUID = variable.PageVariableGuid;
            variableNameTextBlock.Text = "{V:" + variable.Name + "}";
        }

        private void variableHyperlinkButtion_Click(object sender, RoutedEventArgs e)
        {
            VariableList.Show();
        }

        #region Public methods

        public void BindPageContent(EditTimerTemplatePageContentModel pageContentModel)
        {
            _isDataBinding = true;
            titleTextBox.Text = pageContentModel.Title;
            textTextBox.Text = pageContentModel.Text;
            buttonPrimaryNameTextBox.Text = pageContentModel.PrimaryButtonCaption;
            PageContentModel.AfterExpression = pageContentModel.AfterExpression;
            PageContentModel.BeforeExpression = pageContentModel.BeforeExpression;

            // for bind primary button
            if (string.IsNullOrEmpty(pageContentModel.AfterExpression))
            {
                AfterPropertyComboBox.SelectedIndex = 0;
            }
            else
            {
                AfterPropertyComboBox.SelectedIndex = 1;
                AfterExpressionLink.Visibility = Visibility.Visible;
            }

            // for page variable
            if (pageContentModel.PageVariable != null)
            {
                SetPageVariable(pageContentModel.PageVariable);
            }

            _isDataBinding = false;
        }

        /// <summary>
        /// Check whether user has fill in all necessary information
        /// </summary>
        /// <returns></returns>
        public string Validate() 
        {
            string errorMessage = string.Empty;

            if (string.IsNullOrEmpty(titleTextBox.Text.Trim())) 
            {
                errorMessage += "Please fill in title textbox.\n";
            }
            else if (titleTextBox.Text.Trim().Length > 80)
            {
                errorMessage += string.Format("The length of title cannot exceed 80 characters, but you have {0}.\n", titleTextBox.Text.Trim().Length);
            }

            if (string.IsNullOrEmpty(textTextBox.Text.Trim())) 
            {
                errorMessage += "Please fill in text textbox.\n";
            }
            if (string.IsNullOrEmpty(buttonPrimaryNameTextBox.Text.Trim()))
            {
                errorMessage += "Please fill in button name of primary button.\n";
            }
            else if (buttonPrimaryNameTextBox.Text.Trim().Length > 80)
            {
                errorMessage += string.Format("The length of primary button name cannot exceed 80 characters, but you have {0}. \n", buttonPrimaryNameTextBox.Text.Trim().Length);
            }

            if (AfterPropertyComboBox.SelectedItem == null)
            {
                errorMessage += "Please choose action of primary button.\n";
            }

            return errorMessage;
        }

        public void FillContent() {
            PageContentModel.Title = titleTextBox.Text.Trim();
            PageContentModel.Text = textTextBox.Text.Trim();
            PageContentModel.PrimaryButtonCaption = buttonPrimaryNameTextBox.Text.Trim();
        }

        public void EnableControl()
        {
            titleTextBox.IsEnabled = true;
            textTextBox.IsEnabled = true;
            buttonPrimaryNameTextBox.IsEnabled = true;
            BeforeShowExpressionLink.IsEnabled = true;
            AfterPropertyComboBox.IsEnabled = true;
            AfterExpressionLink.IsEnabled = true;
            variableHyperlinkButtion.IsEnabled = true;
        }

        public void DisableControl(string reasonStr)
        {
            titleTextBox.IsEnabled = false;
            textTextBox.IsEnabled = false;
            buttonPrimaryNameTextBox.IsEnabled = false;
            BeforeShowExpressionLink.IsEnabled = false;
            AfterPropertyComboBox.IsEnabled = false;
            AfterExpressionLink.IsEnabled = false;
            variableHyperlinkButtion.IsEnabled = false;
        }

        #endregion

        private void AfterExpressionLink_Click(object sender, RoutedEventArgs e)
        {
            ExpressionBuilder.Show(ExpressionType.AfterProperty, PageContentModel.AfterExpression);
        }

        private void AfterPropertyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {            
            if (AfterPropertyComboBox.SelectedIndex == 0)
            {
                PageContentModel.PrimaryButtonAction = "0";
                PageContentModel.AfterExpression = string.Empty;
                AfterExpressionLink.Visibility = Visibility.Collapsed;
            }
            else if (!_isDataBinding)
            {
                AfterExpressionLink.Visibility = Visibility.Visible;
                ExpressionBuilder.Show(ExpressionType.AfterProperty, PageContentModel.AfterExpression);
            }
        }

        private void BeforeShowExpressionLink_Click(object sender, RoutedEventArgs e)
        {
            ExpressionBuilder.Show(ExpressionType.BeforeProperty, PageContentModel.BeforeExpression);
        }
    }
}
