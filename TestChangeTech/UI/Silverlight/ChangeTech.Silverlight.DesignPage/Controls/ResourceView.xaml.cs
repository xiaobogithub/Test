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
using System.Text;

namespace ChangeTech.Silverlight.DesignPage.Controls
{
    public partial class ResourceView : UserControl
    {
        private ResourceModel _resourceModel;
        public ResourceModel resourceModel;

        public ResourceView()
        {
            InitializeComponent();
        }

        public void Show(ResourceModel resourceModel)
        {
            Visibility = Visibility.Visible;
            _resourceModel = resourceModel;
            ResourceNameTextBlock.Text = resourceModel.Name;

            ServiceClient sc = ServiceProxyFactory.Instance.ServiceProxy;
            sc.GetReferencesInfoOfImageCompleted += new EventHandler<GetReferencesInfoOfImageCompletedEventArgs>(sc_GetReferencesInfoOfImageCompleted);
            sc.GetReferencesInfoOfImageAsync(resourceModel.ID);
        }

        void sc_GetReferencesInfoOfImageCompleted(object sender, GetReferencesInfoOfImageCompletedEventArgs e)
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
                StringBuilder resourceViewBuilder=new StringBuilder(1000);
                foreach (ProgramImageReference pir in e.Result)
                {
                    if (pir.SessionImageReference.Count > 0)
                    {
                        resourceViewBuilder.AppendLine(string.Format("{0}:", pir.ProgramName));

                        foreach (SessionImageReference sir in pir.SessionImageReference)
                        {
                            resourceViewBuilder.AppendLine(string.Format("Day-{0}", sir.Day));
                        }
                    }
                }
                ResourceReferenceInfoTextBlock.Text = !string.IsNullOrEmpty(resourceViewBuilder.ToString()) ? resourceViewBuilder.ToString() : string.Empty;
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
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

        private void ResourceListDataGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            clickOnDataGridColumn = true;
        }

        private void ResourceListDataGrid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            clickOnDataGridColumn = false;
        }
        #endregion
    }
}
