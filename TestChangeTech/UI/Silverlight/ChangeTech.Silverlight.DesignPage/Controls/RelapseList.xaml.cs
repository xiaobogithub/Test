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

namespace ChangeTech.Silverlight.DesignPage.Controls
{
    public partial class RelapseList : UserControl
    {
        public event SelectRelapseDelegate SelectRelapseHandler;

        public RelapseList()
        {
            InitializeComponent();
        }

        private void SelectPageLinkButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectRelapseHandler != null)
            {
                RelapseModel relapse = ((HyperlinkButton)sender).Tag as RelapseModel;
                SelectRelapseHandler(relapse);
                Visibility = Visibility.Collapsed;
            }
        }

        private void RelapseDataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            DataGrid dg = sender as DataGrid;
            HyperlinkButton selectButton = dg.Columns[2].GetCellContent(e.Row).FindName("SelectPageLinkButton") as HyperlinkButton;
            selectButton.Tag = e.Row.DataContext;
        }

        public void DisplayRelapse()
        {
            Visibility = Visibility.Visible;
            ServiceClient serviceproxy = ServiceProxyFactory.Instance.ServiceProxy;
            if (IsEditPagesequenceOnly())
            {
                serviceproxy.GetRelapsePageSequenceModelListCompleted += new EventHandler<GetRelapsePageSequenceModelListCompletedEventArgs>(serviceproxy_GetRelapsePageSequenceModelListCompleted);
                serviceproxy.GetRelapsePageSequenceModelListAsync(new Guid(StringUtility.GetQueryString(Constants.QUERYSTR_PROGRAM_GUID)));
            }
            else
            {
                serviceproxy.GetRelapsePageSequenceModelListBySessionGuidCompleted += new EventHandler<GetRelapsePageSequenceModelListBySessionGuidCompletedEventArgs>(serviceproxy_GetRelapsePageSequenceModelListBySessionGuidCompleted);
                serviceproxy.GetRelapsePageSequenceModelListBySessionGuidAsync(new Guid(StringUtility.GetQueryString(Constants.QUERYSTR_SESSION_GUID)));
            }
        }

        void serviceproxy_GetRelapsePageSequenceModelListBySessionGuidCompleted(object sender, GetRelapsePageSequenceModelListBySessionGuidCompletedEventArgs e)
        {
            RelapseDataGrid.ItemsSource = e.Result;
        }

        void serviceproxy_GetRelapsePageSequenceModelListCompleted(object sender, GetRelapsePageSequenceModelListCompletedEventArgs e)
        {
            RelapseDataGrid.ItemsSource = e.Result;
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

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Collapsed;
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
