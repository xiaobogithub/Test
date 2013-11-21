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

namespace ChangeTech.Silverlight.DesignPage.Controls
{
    public partial class PageList : UserControl
    {
        public event SelectPageDelegate SelectPageEventHandler;
        private ButtonType _buttonType;
        private bool _isBinding = false;
        public PageList()
        {
            InitializeComponent();
        }

        public void Show(ButtonType buttonType)
        {
            _buttonType = buttonType;
            Visibility = Visibility.Visible;
            if (!_isBinding)
            {
                BindData();
            }
        }

        private void BindData()
        {
            ServiceClient serviceProxy = ServiceProxyFactory.Instance.ServiceProxy;
            if (!IsEditPagesequenceOnly())
            {
                serviceProxy.GetSessionCompleted += new EventHandler<GetSessionCompletedEventArgs>(serviceProxy_GetSessionCompleted);
                serviceProxy.GetSessionAsync(new Guid(StringUtility.GetQueryString(Constants.QUERYSTR_SESSION_GUID)));
            }
            else
            {
                serviceProxy.GetPageSequenceCompleted += new EventHandler<GetPageSequenceCompletedEventArgs>(serviceProxy_GetPageSequenceCompleted);
                serviceProxy.GetPageSequenceAsync(new Guid(StringUtility.GetQueryString(Constants.QUERYSTR_PAGE_SEQUENCE_GUID)));
            }
        }

        void serviceProxy_GetPageSequenceCompleted(object sender, GetPageSequenceCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                HtmlPage.Window.Alert("Operation is cancelled.");
            }
            else if (e.Error != null)
            {
                HtmlPage.Window.Alert("Error occurs: " + Constants.ERROR_INTERNAL);
            }
            else
            {
                if (ComboBoxPageSequence.ItemsSource != null)
                {
                    ComboBoxPageSequence.ItemsSource = null;
                }
                ComboBoxPageSequence.Items.Add(e.Result);
                ComboBoxPageSequence.SelectedIndex = 0;

                //PageListDataGrid.ItemsSource = e.Result.Pages;
            }
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

        private void serviceProxy_GetSessionCompleted(object sender, GetSessionCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                HtmlPage.Window.Alert("Operation is cancelled.");
            }
            else if (e.Error != null)
            {
                HtmlPage.Window.Alert("Error occurs: " + Constants.ERROR_INTERNAL);
            }
            else
            {
                ComboBoxPageSequence.ItemsSource = null;
                ComboBoxPageSequence.ItemsSource = e.Result.PageSequences;

                PageSequenceModel currentPageSequence = null;
                foreach (PageSequenceModel pageSequenceModel in ComboBoxPageSequence.Items)
                {
                    if (pageSequenceModel.PageSequenceID.ToString().Equals(StringUtility.GetQueryString(Constants.QUERYSTR_PAGE_SEQUENCE_GUID)))
                    {
                        currentPageSequence = pageSequenceModel;
                    }
                }

                ComboBoxPageSequence.SelectedItem = currentPageSequence;
                _isBinding = true;
            }
        }

        void serviceProxy_GetPagesOfPageSequenceCompleted(object sender, GetPagesOfPageSequenceCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                HtmlPage.Window.Alert("Operation is cancelled.");
            }
            else if (e.Error != null)
            {
                HtmlPage.Window.Alert(Constants.ERROR_INTERNAL);
            }
            else
            {
                PageListDataGrid.ItemsSource = e.Result;
            }
        }

        private void SelectPageLinkButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectPageEventHandler != null)
            {
                Visibility = Visibility.Collapsed;

                if (AbsolutePositionRadioButton.IsChecked.HasValue && AbsolutePositionRadioButton.IsChecked.Value)
                {
                    SelectPageEventHandler((SimplePageContentModel)(((HyperlinkButton)sender).Tag), _buttonType, PositionType.Absolute);
                }
                else
                {
                    SelectPageEventHandler((SimplePageContentModel)(((HyperlinkButton)sender).Tag), _buttonType, PositionType.Relative);
                }
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void PageListDataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            DataGrid dg = (DataGrid)sender;
            HyperlinkButton selectLinkButton = dg.Columns[3].GetCellContent(e.Row).FindName("SelectPageLinkButton") as HyperlinkButton;
            selectLinkButton.Tag = e.Row.DataContext;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ServiceClient serviceProxy = ServiceProxyFactory.Instance.ServiceProxy;
            serviceProxy.GetPagesOfPageSequenceCompleted += new EventHandler<GetPagesOfPageSequenceCompletedEventArgs>(serviceProxy_GetPagesOfPageSequenceCompleted);
            //serviceProxy.GetPagesOfPageSequenceAsync(((PageSequenceModel)ComboBoxPageSequence.SelectedItem).PageSequenceID, new Guid(StringUtility.GetQueryString(Constants.QUERYSTR_LANGUAGE_GUID)));
            serviceProxy.GetPagesOfPageSequenceAsync(((PageSequenceModel)ComboBoxPageSequence.SelectedItem).PageSequenceID, Guid.Empty);
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

        private void PageListDataGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            clickOnDataGridColumn = true;
        }

        private void PageListDataGrid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            clickOnDataGridColumn = false;
        }
        #endregion
    }
}
