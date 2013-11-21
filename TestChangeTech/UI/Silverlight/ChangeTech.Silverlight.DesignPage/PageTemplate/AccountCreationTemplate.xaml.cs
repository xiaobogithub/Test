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
using System.Collections.ObjectModel;
using ChangeTech.Silverlight.Common;

namespace ChangeTech.Silverlight.DesignPage.PageTemplate
{
    public partial class AccountCreationTemplate : UserControl
    {
        public AccountCreationTemplatePageContentModel PageContentModel { get; set; }
        public AccountCreationTemplate()
        {
            InitializeComponent();
            PageContentModel = new AccountCreationTemplatePageContentModel();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if(!IsEditPagesequenceOnly())
            {
                PageContentModel.SessionGUID = new Guid(StringUtility.GetQueryString(Constants.QUERYSTR_SESSION_GUID));
            }
            //PageContentModel.LanguageGUID = new Guid(StringUtility.GetQueryString(Constants.QUERYSTR_LANGUAGE_GUID));
            PageContentModel.PageSequenceGUID = new Guid(StringUtility.GetQueryString(Constants.QUERYSTR_PAGE_SEQUENCE_GUID));
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

        private void pageVariableHyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            VariableList.Show();
        }

        private void SelectVaribleHandler(object sender, PageVariableEventArgs e)
        {
            if(e.pageVariable != null)
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
            pageVariableHyperlinkButton.Content = "{V:" + variable.Name + "}";
            PageContentModel.PageVariableGUID = variable.PageVariableGuid;
        }

        #region Public methods

        public string Validate()
        {
            string errorMessage = string.Empty;

            if(string.IsNullOrEmpty(titleTextBox.Text.Trim()))
            {
                errorMessage += "Please set the title text.\n";
            }
            if(string.IsNullOrEmpty(textTextBox.Text.Trim()))
            {
                errorMessage += "Please set the body text.\n";
            }
            else
            {
                if(textTextBox.Text.Contains(';'))
                {
                    errorMessage += "Please don't type ';' in text. \n";
                }
            }
            if(string.IsNullOrEmpty(usernameTextBox.Text.Trim()))
            {
                errorMessage += "Please set the username text. \n";
            }
            else
            {
                if(usernameTextBox.Text.Contains(';'))
                {
                    errorMessage += "Please don't type ';' in user name text. \n";
                }
            }
            if(string.IsNullOrEmpty(passwordTextBox.Text.Trim()))
            {
                errorMessage += "Please set the password text. \n";
            }
            else
            {
                if(passwordTextBox.Text.Contains(';'))
                {
                    errorMessage += "Please don't type ';' in password text. \n";
                }
            }
            if(string.IsNullOrEmpty(repeatPasswordText.Text.Trim()))
            {
                errorMessage += "Please set the confirm password text. \n";
            }
            else
            {
                if(repeatPasswordText.Text.Contains(';'))
                {
                    errorMessage += "Please don't type ';' in confirm password text. \n";
                }
            }
            if(string.IsNullOrEmpty(checkBoxTextTextBox.Text.Trim()))
            {
                errorMessage += "Please set the checkbox text. \n";
            }
            else
            {
                if(checkBoxTextTextBox.Text.Contains(';'))
                {
                    errorMessage += "Please don't type ';' in checkbox text. \n";
                }
            }
            if(string.IsNullOrEmpty(mobileText.Text.Trim()))
            {
                errorMessage += "Please set the checkbox text. \n";
            }
            else
            {
                if(mobileText.Text.Contains(';'))
                {
                    errorMessage += "Please don't type ';' in checkbox text. \n";
                }
            }

            return errorMessage;
        }

        public void FillContent()
        {
            PageContentModel.Heading = titleTextBox.Text.Trim();
            //PageContentModel.Body = textTextBox.Text.Trim() + ";" + usernameTextBox.Text.Trim() + ";" + passwordTextBox.Text.Trim() + ";" + repeatPasswordText.Text.Trim() + ";" + mobileText.Text.Trim() + ";" + checkBoxTextTextBox.Text.Trim() + ";" + serialNumberTextBox.Text;
            PageContentModel.Body = textTextBox.Text.Trim();
            PageContentModel.UserName = usernameTextBox.Text.Trim();
            PageContentModel.Password = passwordTextBox.Text.Trim();
            PageContentModel.RepeatPassword = repeatPasswordText.Text.Trim();
            PageContentModel.Mobile = mobileText.Text.Trim();
            PageContentModel.CheckBoxText = checkBoxTextTextBox.Text.Trim();
            PageContentModel.SNText = serialNumberTextBox.Text;
            PageContentModel.PrimaryButtonCaption = buttonPrimaryNameTextBox.Text.Trim();
            PageContentModel.PrimaryButtonAction = "0";
        }

        //public void AddPageQuestion()
        //{
        //    Guid questionGuid = Guid.Empty;
        //    foreach (QuestionModel qm in Questions)
        //    {
        //        if (qm.Name == "Singleline")
        //        {
        //            questionGuid = qm.Guid;
        //            break;
        //        }
        //    }
        //    PageContentModel.PageQuestions.Add(
        //        new PageQuestionModel
        //        {
        //            QuestionGuid = Guid.NewGuid(),
        //            Order = 1,
        //            Guid = questionGuid,
        //            Caption = "Email"
        //        });
        //    PageContentModel.PageQuestions.Add(
        //        new PageQuestionModel
        //        {
        //            QuestionGuid = Guid.NewGuid(),
        //            Order = 2,
        //            Guid = questionGuid,
        //            Caption = "Password"
        //        });
        //    PageContentModel.PageQuestions.Add(
        //        new PageQuestionModel
        //        {
        //            QuestionGuid = Guid.NewGuid(),
        //            Order = 3,
        //            Guid = questionGuid,
        //            Caption = "RepeatPassword"
        //        });
        //}

        public void BindPageContent(EditAccountCreationTemplatePageContentModel ContentModel)
        {
            titleTextBox.Text = ContentModel.Heading;
            textTextBox.Text = ContentModel.Body;
            usernameTextBox.Text = ContentModel.UserName;
            passwordTextBox.Text = ContentModel.Password;
            repeatPasswordText.Text = ContentModel.RepeatPassword;
            mobileText.Text = ContentModel.Mobile;
            checkBoxTextTextBox.Text = ContentModel.CheckBoxText;
            serialNumberTextBox.Text = ContentModel.SNText;
            buttonPrimaryNameTextBox.Text = ContentModel.PrimaryButtonCaption;
            if (ContentModel.PageVariable != null)
            {
                SetPageVariable(ContentModel.PageVariable);
            }
        }

        public void EnableControl()
        {
            titleTextBox.IsEnabled = true;
            textTextBox.IsEnabled = true;
            usernameTextBox.IsEnabled = true;
            passwordTextBox.IsEnabled = true;
            repeatPasswordText.IsEnabled = true;
            buttonPrimaryNameTextBox.IsEnabled = true;
            checkBoxTextTextBox.IsEnabled = true;
            pageVariableHyperlinkButton.IsEnabled = true;
            mobileText.IsEnabled = true;
            serialNumberTextBox.IsEnabled = true;
        }

        public void DisableControl(string reasonStr)
        {
            titleTextBox.IsEnabled = false;
            textTextBox.IsEnabled = false;
            usernameTextBox.IsEnabled = false;
            passwordTextBox.IsEnabled = false;
            repeatPasswordText.IsEnabled = false;
            buttonPrimaryNameTextBox.IsEnabled = false;
            checkBoxTextTextBox.IsEnabled = false;
            pageVariableHyperlinkButton.IsEnabled = false;
            serialNumberTextBox.IsEnabled = false;
            mobileText.IsEnabled = false;
        }
        #endregion
    }
}
