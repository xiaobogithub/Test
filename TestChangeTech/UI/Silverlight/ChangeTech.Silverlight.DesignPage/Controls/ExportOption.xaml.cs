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

namespace ChangeTech.Silverlight.DesignPage.Controls
{
    public partial class ExportOption : UserControl
    {
        public event SetExportOptionDelegate SetExportOptionEventHandler;
        public event EventHandler CancelExportOptionEventHandler;

        private LanguageModel _languageModel;

        public ExportOption()
        {
            InitializeComponent();
        }

        public void Show(LanguageModel lm)
        {
            _languageModel = lm;

            for (int index = lm.StartDay; index <= lm.DaysCount - 1; index++)
            {
                StartDayDDL.Items.Add(index);
                EndDayDDL.Items.Add(index);
            }

            Visibility = Visibility.Visible;
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            string startDay = string.Empty;
            string endDay = string.Empty;

            if (StartDayDDL.SelectedItem != null &&
                EndDayDDL.SelectedItem != null)
            {
                startDay = StartDayDDL.SelectedItem.ToString();
                endDay = EndDayDDL.SelectedItem.ToString();
            }
            else
            {
                startDay = "-1";
                endDay = "-1";
            }

            Visibility = Visibility.Collapsed;
            if (SetExportOptionEventHandler != null)
            {
                SetExportOptionEventHandler(_languageModel, startDay, endDay, IncludeRelapse.IsChecked.Value, IncludeProgramRoom.IsChecked.Value,
                    IncludeAccessoryTemplate.IsChecked.Value, IncludeEmailTemplate.IsChecked.Value, IncludeHelpItem.IsChecked.Value, IncludeUserMenu.IsChecked.Value,
                    IncludeTipMessage.IsChecked.Value, IncludeSpecialString.IsChecked.Value);
            }  
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Collapsed;

            if (CancelExportOptionEventHandler != null)
            {
                CancelExportOptionEventHandler(sender, e);
            }
        }

        #region Drag & Drop
        bool isMouseCaptured;
        Point mousePosition;
        bool clickOnDataGridColumn;
        private void RenameCategoryPopupPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!clickOnDataGridColumn)
            {
                FrameworkElement item = sender as FrameworkElement;
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
