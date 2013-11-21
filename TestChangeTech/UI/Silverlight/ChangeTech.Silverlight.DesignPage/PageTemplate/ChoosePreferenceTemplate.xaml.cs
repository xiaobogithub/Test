using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using ChangeTech.Silverlight.Common;
using ChangeTech.Silverlight.DesignPage.ChangeTechWCFService;
using System.Windows.Browser;
using System.Collections.Generic;

namespace ChangeTech.Silverlight.DesignPage.PageTemplate
{
    public partial class ChoosePreferenceTemplate : UserControl
    {
        public ChoosePreferencesTemplatePageContentModel PageContentModel { get; set; }
        public DesignType DesignType { get; set; }
        public ChoosePreferenceTemplate()
        {
            InitializeComponent();
            PageContentModel = new ChoosePreferencesTemplatePageContentModel();
            PageContentModel.Preferences = new ObservableCollection<PreferenceItemModel>();
            if (!IsEditPagesequenceOnly())
            {
                PageContentModel.SessionGUID = new Guid(StringUtility.GetQueryString(Constants.QUERYSTR_SESSION_GUID));
            }
            //PageContentModel.LanguageGUID = new Guid(StringUtility.GetQueryString(Constants.QUERYSTR_LANGUAGE_GUID));
            PageContentModel.PageSequenceGUID = new Guid(StringUtility.GetQueryString(Constants.QUERYSTR_PAGE_SEQUENCE_GUID));
            PageContentModel.PreferenceStatus = new Dictionary<Guid, ModelStatus>();
            PreferenceUnit.AddPreferenceEventHandler += new AddPreferenceDelegate(PreferenceUnit_AddPreferenceEventHandler);
            ExpressionBuilder.SetExpressionEventHandler += new SetExpressionDelegate(ExpressionBuilder_SetExpressionEventHandler);
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

        private void PreferenceUnit_AddPreferenceEventHandler(PreferenceItemModel preferenceItem)
        {
            if (!PageContentModel.Preferences.Contains(preferenceItem))
            {
                PageContentModel.PreferenceStatus.Add(preferenceItem.PreferenceGUID, ModelStatus.ModelAdd);
                PageContentModel.Preferences.Add(preferenceItem);
            }
            else
            {
                int index = PageContentModel.Preferences.IndexOf(preferenceItem);
                PageContentModel.PreferenceStatus[preferenceItem.PreferenceGUID] = ModelStatus.ModelEdit;
                PageContentModel.Preferences[index] = preferenceItem;
            }
        }

        private void AddPreferenceLinkButton_Click(object sender, RoutedEventArgs e)
        {
            if (PageContentModel.Preferences.Count == 9)
            {
                HtmlPage.Window.Alert("You can only add up to 9 preferences on one page.");
            }
            else
            {
                PreferenceUnit.Show(null);
            }
        }

        private void PreferenceGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            HyperlinkButton editPreferenceLink = PreferenceGrid.Columns[3].GetCellContent(e.Row).FindName("EditPreferenceLinkButton") as HyperlinkButton;
            editPreferenceLink.Tag = e.Row.DataContext;
            HyperlinkButton deletePreferenceLink = PreferenceGrid.Columns[4].GetCellContent(e.Row).FindName("DeletePreferenceLinkButton") as HyperlinkButton;
            deletePreferenceLink.Tag = e.Row.DataContext;

            if (DesignType == DesignType.Edit)
            {
                PreferenceItemModel preferenceItem = (PreferenceItemModel)e.Row.DataContext;
                if (!PageContentModel.PreferenceStatus.ContainsKey(preferenceItem.PreferenceGUID))
                {
                    PageContentModel.PreferenceStatus.Add(preferenceItem.PreferenceGUID, ModelStatus.ModelNoChange);
                }
            }
        }

        private void DeletePreferenceLinkButton_Click(object sender, RoutedEventArgs e)
        {
            PreferenceItemModel deletedPreferenceItemModel = ((HyperlinkButton)sender).Tag as PreferenceItemModel;
            PageContentModel.PreferenceStatus[deletedPreferenceItemModel.PreferenceGUID] = ModelStatus.ModelDelete;
            PageContentModel.Preferences.Remove(deletedPreferenceItemModel);
        }

        private void EditPreferenceLinkButton_Click(object sender, RoutedEventArgs e)
        {
            PreferenceUnit.Show(((HyperlinkButton)sender).Tag as PreferenceItemModel);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            PreferenceGrid.ItemsSource = PageContentModel.Preferences;
        }

        private void AfterPropertyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AfterPropertyComboBox.SelectedIndex == 0)
            {
                PageContentModel.AfterExpression = string.Empty;
                AfterPropertyExpressionLink.Visibility = Visibility.Collapsed;
            }
            else if (!_isDataBinding)
            {
                AfterPropertyExpressionLink.Visibility = Visibility.Visible;
                ExpressionBuilder.Show(ExpressionType.AfterProperty, PageContentModel.AfterExpression);
            }
        }

        private void BeforeShowExpressionLink_Click(object sender, RoutedEventArgs e)
        {
            ExpressionBuilder.Show(ExpressionType.BeforeProperty, PageContentModel.BeforeExpression);
        }

        private void AfterPropertyExpressionLink_Click(object sender, RoutedEventArgs e)
        {
            ExpressionBuilder.Show(ExpressionType.AfterProperty, PageContentModel.AfterExpression);
        }

        #region Public methods
        private bool _isDataBinding = false;
        public void BindPageContent(EditChoosePreferencesTemplatePageContentModel pageContentModel)
        {
            _isDataBinding = true;
            PageContentModel.Preferences = pageContentModel.Preferences;
            PageContentModel.MaxPreferences = pageContentModel.MaxPrefereneces;
            PageContentModel.PrimaryButtonName = pageContentModel.PrimaryButtonName;
            PreferenceGrid.ItemsSource = PageContentModel.Preferences;
            maxPreferebceTextBox.Text = PageContentModel.MaxPreferences.ToString();
            PrimaryButtonNameTextBox.Text = pageContentModel.PrimaryButtonName;
            PageContentModel.AfterExpression = pageContentModel.AfterExpression;
            PageContentModel.BeforeExpression = pageContentModel.BeforeExpression;
            if (!string.IsNullOrEmpty(pageContentModel.AfterExpression))
            {
                AfterPropertyComboBox.SelectedIndex = 1;
                AfterPropertyExpressionLink.Visibility = Visibility.Visible;
            }
            else
            {
                AfterPropertyComboBox.SelectedIndex = 0;
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

            if (PageContentModel.Preferences.Count == 0)
            {
                errorMessage += "You must add at least one preference.\n";
            }
            if (string.IsNullOrEmpty(maxPreferebceTextBox.Text.Trim()))
            {
                errorMessage += "You must set how many preferences user can choose.\n";
            }
            else
            {
                int maxPreferences = 0;
                if (!Int32.TryParse(maxPreferebceTextBox.Text.Trim(), out maxPreferences))
                {
                    errorMessage += "Your input for how many preference to choose is not recgonized as a valid numeric.\n";
                }
                else if (PageContentModel.Preferences.Count < Int32.Parse(maxPreferebceTextBox.Text.Trim()))
                {
                    errorMessage += "Preference count is less than how many preference to choose, are you sure?\n";
                }
            }
            if (string.IsNullOrEmpty(PrimaryButtonNameTextBox.Text.Trim()))
            {
                errorMessage += "You must set primary button name.";
            }

            return errorMessage;
        }

        public void FillContent()
        {
            PageContentModel.MaxPreferences = Int32.Parse(maxPreferebceTextBox.Text.Trim());
            PageContentModel.PrimaryButtonName = PrimaryButtonNameTextBox.Text.Trim();
        }

        public void EnableControl()
        {
            maxPreferebceTextBox.IsEnabled = true;
            PrimaryButtonNameTextBox.IsEnabled = true;
            BeforeShowExpressionLink.IsEnabled = true;
            AfterPropertyComboBox.IsEnabled = true;
            AfterPropertyExpressionLink.IsEnabled = true;
            AddPreferenceLinkButton.IsEnabled = true;
            PreferenceGrid.IsEnabled = true;
        }

        public void DisableControl(string reasonStr)
        {
            maxPreferebceTextBox.IsEnabled = false;
            PrimaryButtonNameTextBox.IsEnabled = false;
            BeforeShowExpressionLink.IsEnabled = false;
            AfterPropertyComboBox.IsEnabled = false;
            AfterPropertyExpressionLink.IsEnabled = false;
            AddPreferenceLinkButton.IsEnabled = false;
            PreferenceGrid.IsEnabled = false;
        }

        public void ResetAfterSave()
        {
            List<Guid> keys = new List<Guid>();
            foreach (Guid preferenceGUID in PageContentModel.PreferenceStatus.Keys)
            {
                keys.Add(preferenceGUID);
            }

            foreach (Guid preferenceGUID in keys)
            {
                PageContentModel.PreferenceStatus[preferenceGUID] = ModelStatus.ModelNoChange;
            }
        }
        #endregion
    }
}
