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
using System.Text.RegularExpressions;

namespace ChangeTech.Silverlight.DesignPage.PageTemplate
{
    public partial class Graph : UserControl
    {
        public GraphTemplatePageContentModel PageContentModel { get; set; }
        GraphItemModel currentItem { get; set; }
        bool _isBinding = false;
        public Graph()
        {
            InitializeComponent();
            PageContentModel = new GraphTemplatePageContentModel();
            PageContentModel.GraphItem = new ObservableCollection<GraphItemModel>();
            PageContentModel.ObjectStatus = new Dictionary<Guid, ModelStatus>();
            pageVarible.SelectedEvent += new ChangeTech.Silverlight.DesignPage.Controls.PageVariableManager.SelectedDelegate(SelectedEvent);
        }

        private void AddItemHyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            GraphItemModel graphItem = new GraphItemModel();
            graphItem.GraphItemModelGUID = Guid.NewGuid();
            graphItem.Color = "0xff0000";
            graphItem.PointType = 1;
            PageContentModel.ObjectStatus.Add(graphItem.GraphItemModelGUID, ModelStatus.GraphItemAdded);
            PageContentModel.GraphItem.Add(graphItem);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (!IsEditPagesequenceOnly())
            {
                PageContentModel.SessionGUID = new Guid(StringUtility.GetQueryString(Constants.QUERYSTR_SESSION_GUID));
            }
            //PageContentModel.LanguageGUID = new Guid(StringUtility.GetQueryString(Constants.QUERYSTR_LANGUAGE_GUID));
            PageContentModel.PageSequenceGUID = new Guid(StringUtility.GetQueryString(Constants.QUERYSTR_PAGE_SEQUENCE_GUID));
            graphItemDataGrid.ItemsSource = PageContentModel.GraphItem;
            graphTypeComboBox.Items.Add("Line graph");

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
            currentItem.Expression = expression;
            HyperlinkButton expressionButton = graphItemDataGrid.Columns[3].GetCellContent(currentItem).FindName("hbtnBindExpression") as HyperlinkButton;
            expressionButton.Content = currentItem.Expression;
            if (PageContentModel.ObjectStatus.ContainsKey(currentItem.GraphItemModelGUID))
            {
                if (PageContentModel.ObjectStatus[currentItem.GraphItemModelGUID] != ModelStatus.GraphItemAdded)
                {
                    PageContentModel.ObjectStatus[currentItem.GraphItemModelGUID] = ModelStatus.GraphItemUpdated;
                }
            }
            else
            {
                PageContentModel.ObjectStatus[currentItem.GraphItemModelGUID] = ModelStatus.GraphItemUpdated;
            }

        }

        private void hbtnDeleteDataItem_Click(object sender, RoutedEventArgs e)
        {
            HyperlinkButton deleteHyperlinkButton = sender as HyperlinkButton;
            GraphItemModel item = deleteHyperlinkButton.Tag as GraphItemModel;
            PageContentModel.GraphItem.Remove(item);
            if (PageContentModel.ObjectStatus.ContainsKey(item.GraphItemModelGUID))
            {
                if (PageContentModel.ObjectStatus[item.GraphItemModelGUID] == ModelStatus.GraphItemAdded)
                {
                    PageContentModel.ObjectStatus.Remove(item.GraphItemModelGUID);
                }
                else if (PageContentModel.ObjectStatus[item.GraphItemModelGUID] == ModelStatus.GraphItemUpdated)
                {
                    PageContentModel.ObjectStatus[item.GraphItemModelGUID] = ModelStatus.GraphItemDeleted;
                }
            }
            else
            {
                PageContentModel.ObjectStatus.Add(item.GraphItemModelGUID, ModelStatus.GraphItemDeleted);
            }
        }

        private void graphItemDataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            _isBinding = true;

            DataGrid dg = (DataGrid)sender;
            // expression button
            HyperlinkButton expressionHyperlinkButton = dg.Columns[3].GetCellContent(e.Row).FindName("hbtnBindExpression") as HyperlinkButton;
            expressionHyperlinkButton.Tag = e.Row.DataContext;
            if (!string.IsNullOrEmpty(((GraphItemModel)e.Row.DataContext).Expression))
            {
                expressionHyperlinkButton.Content = ((GraphItemModel)e.Row.DataContext).Expression;
            }
            else
            {
                expressionHyperlinkButton.Content = "Bind Expression";
            }
            // point type combobox
            ComboBox pointTypeCombobox = dg.Columns[2].GetCellContent(e.Row).FindName("pointTypeCombobox") as ComboBox;
            pointTypeCombobox.Tag = e.Row.DataContext;
            if (((GraphItemModel)e.Row.DataContext).PointType > 0)
            {
                pointTypeCombobox.SelectedIndex = ((GraphItemModel)e.Row.DataContext).PointType - 1;
            }
            else
            {
                pointTypeCombobox.SelectedIndex = 0;
            }
            // color combobox
            ComboBox ColorTypeCombobox = dg.Columns[1].GetCellContent(e.Row).FindName("ColorCombobox") as ComboBox;
            ColorTypeCombobox.Tag = e.Row.DataContext;
            if (string.IsNullOrEmpty(((GraphItemModel)e.Row.DataContext).Color))
            {
                ColorTypeCombobox.SelectedIndex = 0;
            }
            else
            {
                switch (((GraphItemModel)e.Row.DataContext).Color)
                {
                    case "0xff0000": ColorTypeCombobox.SelectedIndex = 0; break;
                    case "0x00ff00": ColorTypeCombobox.SelectedIndex = 1; break;
                    case "0x0000ff": ColorTypeCombobox.SelectedIndex = 2; break;
                }
            }
            // delete item button
            HyperlinkButton deleteHyperlinkButton = dg.Columns[4].GetCellContent(e.Row).FindName("hbtnDeleteDataItem") as HyperlinkButton;
            deleteHyperlinkButton.Tag = e.Row.DataContext;

            _isBinding = false;
        }

        private void hbtnBindExpression_Click(object sender, RoutedEventArgs e)
        {
            GraphItemModel itemModel = ((HyperlinkButton)sender).Tag as GraphItemModel;
            currentItem = itemModel;
            ExpressionBuilder.Show(ExpressionType.GraphDataItemExpression, itemModel.Expression);
        }

        private void pointTypeCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!_isBinding)
            {
                ((GraphItemModel)((ComboBox)sender).Tag).PointType = ((ComboBox)sender).SelectedIndex + 1;

                if (PageContentModel.ObjectStatus.ContainsKey(((GraphItemModel)((ComboBox)sender).Tag).GraphItemModelGUID))
                {
                    if (PageContentModel.ObjectStatus[((GraphItemModel)((ComboBox)sender).Tag).GraphItemModelGUID] != ModelStatus.GraphItemAdded)
                    {
                        PageContentModel.ObjectStatus[((GraphItemModel)((ComboBox)sender).Tag).GraphItemModelGUID] = ModelStatus.GraphItemUpdated;
                    }
                }
                else
                {
                    PageContentModel.ObjectStatus.Add(((GraphItemModel)((ComboBox)sender).Tag).GraphItemModelGUID, ModelStatus.GraphItemUpdated);
                }
            }
        }

        private void ColorCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!_isBinding)
            {
                ComboBox colorCombobox = sender as ComboBox;
                switch (colorCombobox.SelectedIndex)
                {
                    case 0: ((GraphItemModel)colorCombobox.Tag).Color = "0xff0000"; break;
                    case 1: ((GraphItemModel)colorCombobox.Tag).Color = "0x00ff00"; break;
                    case 2: ((GraphItemModel)colorCombobox.Tag).Color = "0x0000ff"; break;
                }
                if (PageContentModel.ObjectStatus.ContainsKey(((GraphItemModel)colorCombobox.Tag).GraphItemModelGUID))
                {
                    if (PageContentModel.ObjectStatus[((GraphItemModel)colorCombobox.Tag).GraphItemModelGUID] != ModelStatus.GraphItemAdded)
                    {
                        PageContentModel.ObjectStatus[((GraphItemModel)colorCombobox.Tag).GraphItemModelGUID] = ModelStatus.GraphItemUpdated;
                    }
                }
                else
                {
                    PageContentModel.ObjectStatus.Add(((GraphItemModel)colorCombobox.Tag).GraphItemModelGUID, ModelStatus.GraphItemUpdated);
                }
            }
        }

        private void graphItemDataGrid_CellEditEnded(object sender, DataGridCellEditEndedEventArgs e)
        {
            Guid updateItemGuid = ((GraphItemModel)e.Row.DataContext).GraphItemModelGUID;
            if (PageContentModel.ObjectStatus.ContainsKey(updateItemGuid))
            {
                if (PageContentModel.ObjectStatus[updateItemGuid] != ModelStatus.GraphItemAdded)
                {
                    PageContentModel.ObjectStatus[((GraphItemModel)e.Row.DataContext).GraphItemModelGUID] = ModelStatus.GraphItemUpdated;
                }
            }
            else
            {
                PageContentModel.ObjectStatus.Add(((GraphItemModel)e.Row.DataContext).GraphItemModelGUID, ModelStatus.GraphItemUpdated);
            }
        }

        #region Public Methods

        public string Validate()
        {
            string validateStr = string.Empty;
            string regexp = "^[0-9]+-[0-9]+$";
            if (string.IsNullOrEmpty(titleTextBox.Text.Trim()))
            {
                validateStr += "Please input title textbox. \n";
            }

            if (string.IsNullOrEmpty(timeRangeTextBox.Text.Trim()))
            {
                validateStr += "Please input time range textbox.\n";
            }
            else
            {
                if (!Regex.IsMatch(timeRangeTextBox.Text.Trim(), regexp))
                {
                    validateStr += "Please time range textbox like 'number-number'.\n";
                }
            }

            if (string.IsNullOrEmpty(scoreRangeTextBox.Text.Trim()))
            {
                validateStr += "Please input score range textbox.\n";
            }
            else
            {
                if (!Regex.IsMatch(scoreRangeTextBox.Text.Trim(), regexp))
                {
                    validateStr += "Please score range textbox like 'number-number'.\n";
                }
            }

            if (graphTypeComboBox.SelectedIndex < 0)
            {
                validateStr += "Please select graph type. \n";
            }

            if (timeUnitCombobox.SelectedIndex < 0)
            {
                validateStr += "Please select time unit. \n";
            }

            if (string.IsNullOrEmpty(buttonPrimaryNameTextBox.Text.Trim()))
            {
                validateStr += "Please input primary button textbox.\n";
            }

            if (PageContentModel.GraphItem.Count == 0)
            {
                validateStr += "Please add a graph item at least.\n";
            }
            else
            {
                foreach (GraphItemModel itemModel in PageContentModel.GraphItem)
                {
                    if (string.IsNullOrEmpty(itemModel.Expression))
                    {
                        validateStr += string.Format("Please give the graph item {0} an expression. \n", PageContentModel.GraphItem.IndexOf(itemModel) + 1);
                    }
                    if (string.IsNullOrEmpty(itemModel.Name))
                    {
                        validateStr += string.Format("Please input the caption of graph item {0}. \n", PageContentModel.GraphItem.IndexOf(itemModel) + 1);
                    }
                }
            }

            if (!string.IsNullOrEmpty(goodScoreRangeTextBox.Text.Trim()))
            {
                if (!RegularExpressValidate(goodScoreRangeTextBox.Text.Trim()))
                {
                    validateStr += "Please good score range textbox like 'number-number'.\n";
                }
            }

            if (!string.IsNullOrEmpty(mediumScoreRangeTextBox.Text.Trim()))
            {
                if (!RegularExpressValidate(mediumScoreRangeTextBox.Text.Trim()))
                {
                    validateStr += "Please medium score range textbox like 'number-number'.\n";
                }
            }

            if (!string.IsNullOrEmpty(badScoreRangeTextBox.Text.Trim()))
            {
                if (!RegularExpressValidate(badScoreRangeTextBox.Text.Trim()))
                {
                    validateStr += "Please bad score range textbox like 'number-number'.\n";
                }
            }

            return validateStr;
        }

        private void InserVariableHyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            pageVarible.eventArgs = new PageVariableEventArgs(textTextBox);
            pageVarible.Show();
        }

        private void SelectedEvent(object sender, PageVariableEventArgs args)
        {
            if (args.sender is TextBox)
            {
                ((TextBox)args.sender).Text += " {V:" + args.pageVariable.Name + "}";

                ServiceClient serviceProxy = ServiceProxyFactory.Instance.ServiceProxy;
                serviceProxy.UpdatePageVariableLastAccessTimeAsync(args.pageVariable.PageVariableGUID);
            }
        }

        private bool RegularExpressValidate(string validateStr)
        {
            bool flag = true;
            string regexp = "^[0-9]+-[0-9]+$";
            string[] str = validateStr.Split(';');
            foreach (string regStr in str)
            {
                if (!Regex.IsMatch(regStr, regexp))
                {
                    flag = false;
                    break;
                }
            }

            return flag;
        }

        public void FillContent()
        {
            //TODO: temp solution
            if (graphTypeComboBox.SelectedItem.Equals("Line graph"))
            {
                PageContentModel.GraphType = "Line graph";
            }
            PageContentModel.GoodScoreRange = goodScoreRangeTextBox.Text;
            PageContentModel.MediumScoreRange = mediumScoreRangeTextBox.Text;
            PageContentModel.BadScoreRange = badScoreRangeTextBox.Text;
            PageContentModel.Body = textTextBox.Text;
            PageContentModel.Heading = titleTextBox.Text;
            PageContentModel.PrimaryButtonCaption = buttonPrimaryNameTextBox.Text;
            PageContentModel.ScoreRange = scoreRangeTextBox.Text;
            PageContentModel.TimeRange = timeRangeTextBox.Text;
            // bind time uinit
            switch (timeUnitCombobox.SelectedIndex)
            {
                case 0: PageContentModel.TimeUnit = "Week"; break;
                case 1: PageContentModel.TimeUnit = "Day"; break;
            }
            PageContentModel.GraphCaption = graphNameTextBox.Text;
        }

        public void BindContent(EditGraphTemplatePageContentModel graphPageContent)
        {
            _isBinding = true;
            goodScoreRangeTextBox.Text = graphPageContent.GoodScoreRange;
            mediumScoreRangeTextBox.Text = graphPageContent.MediumScoreRange;
            badScoreRangeTextBox.Text = graphPageContent.BadScoreRange;
            textTextBox.Text = graphPageContent.Body;
            titleTextBox.Text = graphPageContent.Heading;
            buttonPrimaryNameTextBox.Text = graphPageContent.PrimaryButtonCaption;
            scoreRangeTextBox.Text = graphPageContent.ScoreRange;
            timeRangeTextBox.Text = graphPageContent.TimeRange;
            switch (graphPageContent.TimeUnit)
            {
                case "Week": timeUnitCombobox.SelectedIndex = 0; break;
                case "Day": timeUnitCombobox.SelectedIndex = 1; break;
            }
            graphNameTextBox.Text = graphPageContent.GraphCaption;
            //TODO:Temp solution
            graphTypeComboBox.SelectedIndex = 0;
            PageContentModel.GraphItem = graphPageContent.GraphItem;
            graphItemDataGrid.ItemsSource = graphPageContent.GraphItem;
            _isBinding = false;
        }

        public void EnableControl()
        {
            titleTextBox.IsEnabled = true;
            textTextBox.IsEnabled = true;
            graphNameTextBox.IsEnabled = true;
            graphTypeComboBox.IsEnabled = true;
            timeRangeTextBox.IsEnabled = true;
            timeUnitCombobox.IsEnabled = true;
            scoreRangeTextBox.IsEnabled = true;
            goodScoreRangeTextBox.IsEnabled = true;
            mediumScoreRangeTextBox.IsEnabled = true;
            badScoreRangeTextBox.IsEnabled = true;
            buttonPrimaryNameTextBox.IsEnabled = true;
            AddItemHyperlinkButton.IsEnabled = true;
            graphItemDataGrid.IsEnabled = true;
            InserVariableHyperlinkButton.IsEnabled = true;
        }

        public void DisableControl(string reasonStr)
        {
            titleTextBox.IsEnabled = false;
            textTextBox.IsEnabled = false;
            graphNameTextBox.IsEnabled = false;
            graphTypeComboBox.IsEnabled = false;
            timeRangeTextBox.IsEnabled = false;
            timeUnitCombobox.IsEnabled = false;
            scoreRangeTextBox.IsEnabled = false;
            goodScoreRangeTextBox.IsEnabled = false;
            mediumScoreRangeTextBox.IsEnabled = false;
            badScoreRangeTextBox.IsEnabled = false;
            buttonPrimaryNameTextBox.IsEnabled = false;
            AddItemHyperlinkButton.IsEnabled = false;
            graphItemDataGrid.IsEnabled = false;
            InserVariableHyperlinkButton.IsEnabled = false;
        }

        public void ResetAfterSave()
        {
            PageContentModel.ObjectStatus.Clear();
        }

        #endregion
    }
}
