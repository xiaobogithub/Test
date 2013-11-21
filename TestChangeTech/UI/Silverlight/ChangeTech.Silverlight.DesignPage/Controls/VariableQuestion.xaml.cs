﻿using System;
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

namespace ChangeTech.Silverlight.DesignPage.Controls
{
    public partial class VariableQuestion : UserControl
    {
        public event SelectQuestionItemDelegate SelectQuestionItemEventHandler;

        public VariableQuestion()
        {
            InitializeComponent();
        }

        public void Show(PageVariableModel variable)
        {
            if (variable != null)
            {
                PromptLabel.Text = string.Format("Here are the question bound to the variable {0} you choose, please select which vaule you want to compare.", variable.Name);
                VariableQuestionDataGrid.ItemsSource = variable.Questions;
            }
            else
            {
                PromptLabel.Text = "No variable is selected by you yet.";
            }
            Visibility = Visibility.Visible;
        }

        private void VariableQuestion_RowDetailsVisibilityChanged(object sender, DataGridRowDetailsEventArgs e)
        {

        }

        private void SelectItemLink_Click(object sender, RoutedEventArgs e)
        {
            if (SelectQuestionItemEventHandler != null)
            {
                SelectQuestionItemEventHandler(((HyperlinkButton)sender).Tag as PageQuestionItemModel);
            }
        }

        private void QuestionItemGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            DataGrid dg = sender as DataGrid;
            HyperlinkButton hlb = dg.Columns[3].GetCellContent(e.Row).FindName("SelectItemLink") as HyperlinkButton;
            hlb.Tag = e.Row.DataContext;
        }


        private void VariableQuestionDataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {

        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
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

        private void VariableQuestionDataGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            clickOnDataGridColumn = true;
        }

        private void VariableQuestionDataGrid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            clickOnDataGridColumn = false;
        }
        #endregion
    }
}
