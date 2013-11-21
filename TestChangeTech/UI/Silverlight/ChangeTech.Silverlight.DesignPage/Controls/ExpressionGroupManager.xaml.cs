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
    public partial class ExpressionGroupManager : UserControl
    {
        public event UpdateExpressionGroupsDelegate UpdateExpressionGroupsEvent;

        private EditExpressionGroupModel _editExpressionGroupModel;

        public ExpressionGroupManager()
        {
            InitializeComponent();
        }

        public void Show()
        {
            Visibility = Visibility.Visible;
            GetExpressionGroups();
        }

        private void GetExpressionGroups()
        {
            Disable(Constants.MSG_LOADING);

            _editExpressionGroupModel = new EditExpressionGroupModel();
            _editExpressionGroupModel.SessionGUID = new Guid(StringUtility.GetQueryString(Constants.QUERYSTR_SESSION_GUID));
            _editExpressionGroupModel.ProgramGUID = Guid.Empty;
            _editExpressionGroupModel.ObjectStatus = new Dictionary<Guid, ModelStatus>();

            ServiceClient serviceClient = ServiceProxyFactory.Instance.ServiceProxy;
            serviceClient.GetExpressionGroupsOfProgramCompleted += new EventHandler<GetExpressionGroupsOfProgramCompletedEventArgs>(serviceClient_GetExpressionGroupsOfProgramCompleted);
            serviceClient.GetExpressionGroupsOfProgramAsync(new Guid(StringUtility.GetQueryString(Constants.QUERYSTR_SESSION_GUID)));
        }

        private void serviceClient_GetExpressionGroupsOfProgramCompleted(object sender, GetExpressionGroupsOfProgramCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                HtmlPage.Window.Alert(Constants.MSG_CANCELLED);
            }
            else if (e.Error != null)
            {
                HtmlPage.Window.Alert(Constants.ERROR_INTERNAL);
            }
            else
            {
                _editExpressionGroupModel.ExpressionGroups = e.Result;
                DataGridGroup.ItemsSource = null;
                DataGridGroup.ItemsSource = _editExpressionGroupModel.ExpressionGroups;
                foreach (ExpressionGroupModel expressionGroup in e.Result)
                {
                    _editExpressionGroupModel.ObjectStatus.Add(expressionGroup.ExpressionGroupGUID, ModelStatus.ModelNoChange); 
                }
            }
            Enable();
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            Disable(Constants.MSG_SAVING);

            ServiceClient serviceProxy = ServiceProxyFactory.Instance.ServiceProxy;
            serviceProxy.SaveExpressionGroupCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(serviceProxy_SaveExpressionGroupCompleted);
            serviceProxy.SaveExpressionGroupAsync(_editExpressionGroupModel);
        }

        private void serviceProxy_SaveExpressionGroupCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                HtmlPage.Window.Alert(Constants.MSG_CANCELLED);
                GetExpressionGroups();
            }
            else if (e.Error != null)
            {
                HtmlPage.Window.Alert(Constants.ERROR_INTERNAL);
                GetExpressionGroups();
            }
            else
            {
                HtmlPage.Window.Alert(Constants.MSG_SUCCESSFUL);

                Enable();
                Visibility = Visibility.Collapsed;

                if (UpdateExpressionGroupsEvent != null)
                {
                    UpdateExpressionGroupsEvent(_editExpressionGroupModel.ExpressionGroups);
                }
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Collapsed;
        }

        private void NewGroupButton_Click(object sender, RoutedEventArgs e)
        {
            ExpressionGroupModel expressionGroupModel = new ExpressionGroupModel();
            if (_editExpressionGroupModel.ProgramGUID != Guid.Empty)
            {
                expressionGroupModel.ProgramGUID = _editExpressionGroupModel.ProgramGUID;
            }
            expressionGroupModel.Name = string.Empty;
            expressionGroupModel.Description = string.Empty;
            expressionGroupModel.ExpressionGroupGUID = Guid.NewGuid();
            _editExpressionGroupModel.ExpressionGroups.Add(expressionGroupModel);
            _editExpressionGroupModel.ObjectStatus.Add(expressionGroupModel.ExpressionGroupGUID, ModelStatus.ModelAdd);
        }

        private void hbtnDelete_Click(object sender, RoutedEventArgs e)
        {
            HyperlinkButton deleteHyperLink = sender as HyperlinkButton;
            _editExpressionGroupModel.ExpressionGroups.Remove((ExpressionGroupModel)deleteHyperLink.Tag);

            switch (_editExpressionGroupModel.ObjectStatus[((ExpressionGroupModel)deleteHyperLink.Tag).ExpressionGroupGUID])
            {
                case ModelStatus.ModelAdd:
                    _editExpressionGroupModel.ObjectStatus.Remove(((ExpressionGroupModel)deleteHyperLink.Tag).ExpressionGroupGUID);
                    break;
                default:
                    _editExpressionGroupModel.ObjectStatus[((ExpressionGroupModel)deleteHyperLink.Tag).ExpressionGroupGUID] = ModelStatus.ModelDelete;
                    break;
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Collapsed;
        }

        private void DataGridGroup_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            HyperlinkButton deleteHyperLink = DataGridGroup.Columns[2].GetCellContent(e.Row).FindName("hbtnDelete") as HyperlinkButton;
            deleteHyperLink.Tag = e.Row.DataContext;
            _editExpressionGroupModel.ProgramGUID = ((ExpressionGroupModel)e.Row.DataContext).ProgramGUID;
        }

        private void Enable()
        {
            PromptTextBlock.Text = string.Empty;
            PromptTextBlock.Visibility = Visibility.Collapsed;
            OKButton.IsEnabled = true;
            CancelButton.IsEnabled = true;
            NewGroupButton.IsEnabled = true;
            DataGridGroup.IsEnabled = true;
        }

        private void Disable(string message)
        {
            OKButton.IsEnabled = false;
            CancelButton.IsEnabled = false;
            NewGroupButton.IsEnabled = false;
            DataGridGroup.IsEnabled = false;
            PromptTextBlock.Text = message;
            PromptTextBlock.Visibility = Visibility.Visible;
        }

        private void DataGridGroup_RowEditEnded(object sender, DataGridRowEditEndedEventArgs e)
        {
            ExpressionGroupModel expressionGroup = e.Row.DataContext as ExpressionGroupModel;
            if (_editExpressionGroupModel.ObjectStatus[expressionGroup.ExpressionGroupGUID] != ModelStatus.ModelAdd)
            {
                _editExpressionGroupModel.ObjectStatus[expressionGroup.ExpressionGroupGUID] = ModelStatus.ModelEdit;
            }
        }

        #region Drag & Drop
        bool isMouseCaptured;
        Point mousePosition;
        bool clickOnDataGridColumn;

        private void DataGridGroup_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            clickOnDataGridColumn = true;
        }

        private void DataGridGroup_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            clickOnDataGridColumn = false;
        }

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
        #endregion
    }
}
