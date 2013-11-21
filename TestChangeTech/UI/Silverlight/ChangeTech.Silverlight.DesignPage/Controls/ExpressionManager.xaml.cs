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
using System.Collections.ObjectModel;

namespace ChangeTech.Silverlight.DesignPage.Controls
{
    public partial class ExpressionManager : UserControl
    {
        public event SelectExpressionDelegate SelectExpressionEventHandler;
        private bool _hasChanges;
        private bool _isDataBinding;
        private ObservableCollection<ExpressionGroupModel> _expressionGroups;
        private ObservableCollection<ExpressionGroupModel> _expressionGroupsWithAll;
        private EditExpressionModel _editExpressionModel;
        
        public ExpressionManager()
        {
            InitializeComponent();
        }

        public void Show()
        {
            _hasChanges = false;
            _editExpressionModel = new EditExpressionModel();
            _editExpressionModel.ObjectStatus = new Dictionary<Guid, ModelStatus>();

            Visibility = Visibility.Visible;

            GetExpresionGroups();
        }

        private void GetExpresionGroups()
        {
            Disable(Constants.MSG_LOADING);

            ServiceClient serviceProxy = ServiceProxyFactory.Instance.ServiceProxy;
            serviceProxy.GetExpressionGroupsOfProgramCompleted += new EventHandler<GetExpressionGroupsOfProgramCompletedEventArgs>(serviceProxy_GetExpressionGroupsOfProgramCompleted);
            serviceProxy.GetExpressionGroupsOfProgramAsync(new Guid(StringUtility.GetQueryString(Constants.QUERYSTR_SESSION_GUID)));
        }

        private void serviceProxy_GetExpressionGroupsOfProgramCompleted(object sender, GetExpressionGroupsOfProgramCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                HtmlPage.Window.Alert(Constants.MSG_CANCELLED);
                Enable();
            }
            else if (e.Error != null)
            {
                HtmlPage.Window.Alert(Constants.ERROR_INTERNAL);
            }
            else
            {
                _expressionGroups = e.Result;
                _expressionGroupsWithAll = new ObservableCollection<ExpressionGroupModel>();
                _expressionGroupsWithAll.Add(new ExpressionGroupModel
                { 
                    Name = "All",
                    ExpressionGroupGUID = Guid.Empty
                });

                foreach (ExpressionGroupModel expressionGroupModel in e.Result)
                {
                    _expressionGroupsWithAll.Add(expressionGroupModel);
                }

                GroupCombobox.ItemsSource = null;
                GroupCombobox.ItemsSource = _expressionGroupsWithAll;
                GroupCombobox.SelectedIndex = 0;
                Enable();
            }
        }

        private void Enable()
        {
            PromptTextBlock.Text = string.Empty;
            GroupCombobox.IsEnabled = true;
            ExpressionDataGrid.IsEnabled = true;
            SaveButton.IsEnabled = true;
        }

        private void Disable(string message)
        {
            PromptTextBlock.Text = message;
            GroupCombobox.IsEnabled = false;
            ExpressionDataGrid.IsEnabled = false;
            SaveButton.IsEnabled = false;
        }

        private void DataGridGroup_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            HyperlinkButton selectLink = ExpressionDataGrid.Columns[3].GetCellContent(e.Row).FindName("SelectExpressionLink") as HyperlinkButton;
            selectLink.Tag = e.Row.DataContext;

            HyperlinkButton deleteLink = ExpressionDataGrid.Columns[4].GetCellContent(e.Row).FindName("DeleteLink") as HyperlinkButton;
            deleteLink.Tag = e.Row.DataContext;

            ComboBox itemGroupComoBox = ExpressionDataGrid.Columns[2].GetCellContent(e.Row).FindName("ItemGroupComboBox") as ComboBox;
            itemGroupComoBox.ItemsSource = null;
            itemGroupComoBox.ItemsSource = _expressionGroups;
            itemGroupComoBox.Tag = e.Row.DataContext;

            for (int index = 0; index < itemGroupComoBox.Items.Count; index++)
            {
                if (((ExpressionGroupModel)itemGroupComoBox.Items[index]).ExpressionGroupGUID == ((ExpressionModel)e.Row.DataContext).ExpressionGroupGUID)
                {
                    itemGroupComoBox.SelectedIndex = index;
                    break;
                }
            }

            BindExpressionControlsExcludeCurrentRow((ExpressionModel)e.Row.DataContext);
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Collapsed;
        }

        private void SelectExpressionLink_Click(object sender, RoutedEventArgs e)
        {
            if (SelectExpressionEventHandler != null)
            {
                SelectExpressionEventHandler(((ExpressionModel)((HyperlinkButton)sender).Tag).ExpressionText);
                Visibility = Visibility.Collapsed;
            }
        }

        private void DataGridGroup_RowEditEnded(object sender, DataGridRowEditEndedEventArgs e)
        {
            _editExpressionModel.ObjectStatus[((ExpressionModel)e.Row.DataContext).ExpressionGUID] = ModelStatus.ModelEdit;
        }

        private void DeleteLink_Click(object sender, RoutedEventArgs e)
        {
            HyperlinkButton deleteLink = sender as HyperlinkButton;
            _editExpressionModel.Expressions.Remove((ExpressionModel)deleteLink.Tag);
            _editExpressionModel.ObjectStatus[((ExpressionModel)deleteLink.Tag).ExpressionGUID] = ModelStatus.ModelDelete;
            _hasChanges = true;
        }

        private void ItemGroupComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!_isDataBinding)
            {
                ComboBox itemGroupComboBox = sender as ComboBox;
                Guid expressionGUID = ((ExpressionModel)itemGroupComboBox.Tag).ExpressionGUID;
                _editExpressionModel.ObjectStatus[expressionGUID] = ModelStatus.ModelEdit;
                foreach (ExpressionModel expressionModel in _editExpressionModel.Expressions)
                {
                    if (expressionModel.ExpressionGUID == expressionGUID)
                    {
                        expressionModel.ExpressionGroupGUID = ((ExpressionGroupModel)itemGroupComboBox.SelectedItem).ExpressionGroupGUID;
                        break;
                    }
                }
                _hasChanges = true;
            }
        }

        private void GroupCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Disable(Constants.MSG_LOADING);
            //_editExpressionModel.ObjectStatus.Clear();
            ServiceClient serviceProxy = ServiceProxyFactory.Instance.ServiceProxy;
            if (GroupCombobox.SelectedItem != null)
            {
                Guid expressionGroupGuid = ((ExpressionGroupModel)GroupCombobox.SelectedItem).ExpressionGroupGUID;
                if (expressionGroupGuid != Guid.Empty)
                {
                    serviceProxy.GetExpressionsOfGroupCompleted += new EventHandler<GetExpressionsOfGroupCompletedEventArgs>(serviceProxy_GetExpressionsOfGroupCompleted);
                    serviceProxy.GetExpressionsOfGroupAsync(expressionGroupGuid);
                }
                else
                {
                    serviceProxy.GetExpressionsOfProgramCompleted += new EventHandler<GetExpressionsOfProgramCompletedEventArgs>(serviceProxy_GetExpressionsOfProgramCompleted);
                    serviceProxy.GetExpressionsOfProgramAsync(new Guid(StringUtility.GetQueryString(Constants.QUERYSTR_SESSION_GUID)));
                }
            }
            else
            {
                serviceProxy.GetExpressionsOfProgramCompleted += new EventHandler<GetExpressionsOfProgramCompletedEventArgs>(serviceProxy_GetExpressionsOfProgramCompleted);
                serviceProxy.GetExpressionsOfProgramAsync(new Guid(StringUtility.GetQueryString(Constants.QUERYSTR_SESSION_GUID)));
            }
        }

        private void serviceProxy_GetExpressionsOfGroupCompleted(object sender, GetExpressionsOfGroupCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                HtmlPage.Window.Alert(Constants.MSG_CANCELLED);
                Enable();
            }
            else if (e.Error != null)
            {
                HtmlPage.Window.Alert(Constants.ERROR_INTERNAL);
            }
            else
            {
                ExpressionDataGrid.ItemsSource = null;
                ExpressionDataGrid.ItemsSource = e.Result;
                _editExpressionModel.Expressions = e.Result;
                InitializeObjectStatus();
                //BindExpressionControls();
                Enable();
            }
        }

        private void serviceProxy_GetExpressionsOfProgramCompleted(object sender, GetExpressionsOfProgramCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                HtmlPage.Window.Alert(Constants.MSG_CANCELLED);
                Enable();
            }
            else if (e.Error != null)
            {
                HtmlPage.Window.Alert(Constants.ERROR_INTERNAL);
            }
            else
            {
                ExpressionDataGrid.ItemsSource = null;
                ExpressionDataGrid.ItemsSource = e.Result;
                _editExpressionModel.Expressions = e.Result;
                InitializeObjectStatus();
                //BindExpressionControls();
                Enable();
            }
        }

        private void InitializeObjectStatus()
        {
            _editExpressionModel.ObjectStatus.Clear();

            foreach (ExpressionModel expressionModel in ExpressionDataGrid.ItemsSource)
            {
                //if (!_editExpressionModel.ObjectStatus.ContainsKey(expressionModel.ExpressionGUID))
                //{
                    _editExpressionModel.ObjectStatus.Add(expressionModel.ExpressionGUID, ModelStatus.ModelNoChange);
                //}
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (_hasChanges)
            {
                Disable(Constants.MSG_SAVING);

                ServiceClient serviceProxy = ServiceProxyFactory.Instance.ServiceProxy;
                serviceProxy.SaveExpressionCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(serviceProxy_SaveExpressionCompleted);
                serviceProxy.SaveExpressionAsync(_editExpressionModel);
            }
            else
            {
                Visibility = Visibility.Collapsed;
            }
        }

        void serviceProxy_SaveExpressionCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                HtmlPage.Window.Alert(Constants.MSG_CANCELLED);
                Enable();
            }
            else if (e.Error != null)
            {
                HtmlPage.Window.Alert(Constants.ERROR_INTERNAL);
            }
            else
            {
                HtmlPage.Window.Alert(Constants.MSG_SUCCESSFUL);
                Enable();
                Visibility = Visibility.Collapsed;
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Collapsed;
        }

        private void BindExpressionControlsExcludeCurrentRow(ExpressionModel currentModel)
        {
            //if (ExpressionDataGrid.ItemsSource != null && _needReload)
            //{ 
                _isDataBinding = true;
                foreach (ExpressionModel expressionModel in ExpressionDataGrid.ItemsSource)
                {
                    if (expressionModel != currentModel)
                    {
                        if (ExpressionDataGrid.Columns[3].GetCellContent(expressionModel) != null)
                        {
                            HyperlinkButton selectLink = ExpressionDataGrid.Columns[3].GetCellContent(expressionModel).FindName("SelectExpressionLink") as HyperlinkButton;
                            selectLink.Tag = expressionModel;

                            HyperlinkButton deleteLink = ExpressionDataGrid.Columns[4].GetCellContent(expressionModel).FindName("DeleteLink") as HyperlinkButton;
                            deleteLink.Tag = expressionModel;

                            ComboBox itemGroupComoBox = ExpressionDataGrid.Columns[2].GetCellContent(expressionModel).FindName("ItemGroupComboBox") as ComboBox;
                            itemGroupComoBox.ItemsSource = null;
                            itemGroupComoBox.ItemsSource = _expressionGroups;
                            itemGroupComoBox.Tag = expressionModel;

                            for (int index = 0; index < itemGroupComoBox.Items.Count; index++)
                            {
                                if (((ExpressionGroupModel)itemGroupComoBox.Items[index]).ExpressionGroupGUID == expressionModel.ExpressionGroupGUID)
                                {
                                    itemGroupComoBox.SelectedIndex = index;
                                    break;
                                }
                            }
                        }
                    }
                }
                _isDataBinding = false;
            //    _needReload = false;
            //}
        }

        #region Drag & Drop
        bool isMouseCaptured;
        Point mousePosition;
        bool clickOnDataGridColumn;

        private void RenameCategoryPopupPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            FrameworkElement item = sender as FrameworkElement;

            if (!clickOnDataGridColumn)
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

        private void DataGridGroup_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            clickOnDataGridColumn = true;
        }

        private void DataGridGroup_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            clickOnDataGridColumn = false;
        }
        #endregion
    }
}
