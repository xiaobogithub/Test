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

using System.Collections.ObjectModel;
using ChangeTech.Silverlight.DesignPage.ChangeTechWCFService;
using ChangeTech.Silverlight.Common;
using System.Windows.Browser;

namespace ChangeTech.Silverlight.DesignPage.PageTemplate
{
    public partial class ScreeningResultsTemplate : UserControl
    {
        public ObservableCollection<ScreenResultTemplatePageLineModel> PageResultLines { get; set; } //PageResultLines
        private ObservableCollection<OrderObject> orders { get; set; }
        //private ObservableCollection<PageQuestionItemModel> currentItems;
        private bool _isDataBinding = false;
        private bool _isClickOnInsertResourceBodyLink;

        public ObservableCollection<ScreenResultTemplatePageLineModel> PageLines { get; set; }
        public ScreenResultTemplatePageContentModel PageContentModel { get; set; }
        public DesignType DesignType { get; set; }
        public const string EditItem = "Edit Items";
        public const string AddItem = "Add Items";
        bool ddlFlag = true;



        public ScreeningResultsTemplate()
        {
            InitializeComponent();
            PageResultLines = new ObservableCollection<ScreenResultTemplatePageLineModel>();
            PageContentModel = new ScreenResultTemplatePageContentModel();
            PageContentModel.ObjectStatus = new Dictionary<Guid, ModelStatus>();
            orders = new ObservableCollection<OrderObject>();
            PageContentModel.PageLines = new ObservableCollection<ScreenResultTemplatePageLineModel>();

            pageVarible.SelectedEvent += new ChangeTech.Silverlight.DesignPage.Controls.PageVariableManager.SelectedDelegate(SelectedEvent);
            ExpressionBuilder.SetExpressionEventHandler += new SetExpressionDelegate(ExpressionBuilder_SetExpressionEventHandler);
            ResourceManager.SelectResourceEventHandler += new SelectResourceDelegate(ResourceManager_SelectResourceEventHandler);

        }

        private void ResourceManager_SelectResourceEventHandler(object sender, SelectResourceEventArgs e)
        {
            string resourceURL = StringUtility.GetBlobPath() + "{0}{1}";
            string fordownloadresourceURL = StringUtility.GetApplicationPath() + "RequestResource.aspx?target={0}&media={1}&name={2}";
            string downloadLink = "<A href='{0}' target='_blank'>{1}</A>";
            if (e.Resource.Type.Equals(ResourceTypeEnum.Image.ToString()))
            {
                resourceURL = string.Format(fordownloadresourceURL, Constants.OriginalImageDirectory, e.Resource.NameOnServer, e.Resource.Name);
            }
            else if (e.Resource.Type.Equals(ResourceTypeEnum.Document.ToString()))
            {
                resourceURL = string.Format(fordownloadresourceURL, ResourceTypeEnum.Document.ToString(), e.Resource.NameOnServer, e.Resource.Name);
            }
            else if (e.Resource.Type.Equals(ResourceTypeEnum.Video.ToString()))
            {
                resourceURL = string.Format(fordownloadresourceURL, ResourceTypeEnum.Video, e.Resource.NameOnServer, e.Resource.Name);
            }
            else if (e.Resource.Type.Equals(ResourceTypeEnum.Audio.ToString()))
            {
                resourceURL = string.Format(fordownloadresourceURL, ResourceTypeEnum.Audio, e.Resource.NameOnServer, e.Resource.Name);
            }

            if (_isClickOnInsertResourceBodyLink)
            {
                if (!string.IsNullOrEmpty(BodyTextBox.Text))
                {
                    BodyTextBox.Text += "\n" + string.Format(downloadLink, resourceURL, e.Resource.Name);
                }
                else
                {
                    BodyTextBox.Text += string.Format(downloadLink, resourceURL, e.Resource.Name);
                }
            }
            else
            {
                //FooterTextBox.Text
            }
        }

        private void ExpressionBuilder_SetExpressionEventHandler(string expression, ExpressionType expressionType)
        {
            switch (expressionType)
            {
                case ExpressionType.AfterProperty:
                    PageContentModel.AfterExpression = expression;
                    break;
                case ExpressionType.BeforeProperty:
                    PageContentModel.BeforeExpression = expression;
                    break;
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (!IsEditPagesequenceOnly())
            {
                PageContentModel.SessionGUID = new Guid(StringUtility.GetQueryString(Constants.QUERYSTR_SESSION_GUID));
            }
            //PageContentModel.LanguageGUID = new Guid(StringUtility.GetQueryString(Constants.QUERYSTR_LANGUAGE_GUID));
            PageContentModel.PageSequenceGUID = new Guid(StringUtility.GetQueryString(Constants.QUERYSTR_PAGE_SEQUENCE_GUID));
            dgResults.ItemsSource = PageContentModel.PageLines;

            PresenterImagePositionComboBox.SelectedIndex = 0;
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

        #region Button Action
        private void PrimaryButtonActionComoboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PrimaryButtonActionComoboBox.SelectedIndex == 0)
            {
                PageContentModel.PrimaryButtonAction = "0";
                PageContentModel.AfterExpression = string.Empty;
                PrimaryButtonActionLinkButton.Visibility = Visibility.Collapsed;
            }
            else if (!_isDataBinding)
            {
                ExpressionBuilder.Show(ExpressionType.AfterProperty, PageContentModel.AfterExpression);
                PrimaryButtonActionLinkButton.Visibility = Visibility.Visible;
            }
        }

        private void PrimaryButtonActionLinkButton_Click(object sender, RoutedEventArgs e)
        {
            ExpressionBuilder.Show(ExpressionType.AfterProperty, PageContentModel.AfterExpression);
        }

        private void BeforeShowExpressionLink_Click(object sender, RoutedEventArgs e)
        {
            ExpressionBuilder.Show(ExpressionType.BeforeProperty, PageContentModel.BeforeExpression);
        }
        #endregion

        #region Presenter image
        private void presenterImageReset_Click(object sender, RoutedEventArgs e)
        {
            if (PageContentModel.PresenterImageGUID != Guid.Empty)
            {
                SetPresenterImageLinkButton.Content = "Set presenter image";
                PresenterImageNameTextBlock.Text = "";
                PageContentModel.PresenterImageGUID = Guid.Empty;
                PresenterImage.Source = null;
                PresenterImagePositionComboBox.SelectedIndex = -1;
            }
        }

        private void SetPresenterImageLinkButton_Click(object sender, RoutedEventArgs e)
        {
            if (PresenterImage.Tag != null)
            {
                ImageList.LastSelectedResource = (ResourceModel)PresenterImage.Tag;
            }
            ImageList.Show();

            ImageList.OnSelectImage -= SelectGraphic1ImageEventHandler;
            ImageList.OnSelectImage -= SelectGraphic2ImageEventHandler;
            ImageList.OnSelectImage -= SelectGraphic3ImageEventHandler;
            ImageList.OnSelectImage += new SelectPictureDelegate(SelectImageEventHandler);
        }

        private void SelectImageEventHandler(ResourceModel image)
        {
            SetPresenterImage(image);
        }

        private void SetPresenterImage(ResourceModel resource)
        {
            string imageUri = Constants.ThumbImageDirectory + resource.NameOnServer;
            PresenterImage.Tag = resource;

            ImageUtility.ShowImage(PresenterImage, imageUri);
            PresenterImageNameTextBlock.Text = resource.Name;
            SetPresenterImageLinkButton.Content = "Change presenter image";
            PageContentModel.PresenterImageGUID = resource.ID;
        }

        private void PresenterImagePositionComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PresenterImagePositionComboBox.SelectedIndex == 0)
            {
                PageContentModel.PresenterImagePosition = "Left";
            }
            else if (PresenterImagePositionComboBox.SelectedIndex == 1)
            {
                PageContentModel.PresenterImagePosition = "Right";
            }
            else
            {
                PageContentModel.PresenterImagePosition = string.Empty;
            }
        }

        private void PresenterModeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PresenterModeComboBox.SelectedIndex == 0)
            {
                PageContentModel.PresenterMode = "Normal";
            }
            else if (PresenterModeComboBox.SelectedIndex == 1)
            {
                PageContentModel.PresenterMode = "Big";
            }
            else
            {
                PageContentModel.PresenterMode = string.Empty;
            }
        }
        #endregion

        private void dgResults_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            ddlFlag = false;
            DataGrid dg = (DataGrid)sender;
            //ComboBox cbName = dg.Columns[1].GetCellContent(e.Row).FindName("cbName") as ComboBox;
            //cbName.ItemsSource = Questions;
            //cbName.Tag = e.Row;
            bool existFlag = false;
            foreach (OrderObject orderobject in orders)
            {
                if (orderobject.OrderNo == ((ScreenResultTemplatePageLineModel)e.Row.DataContext).Order)
                {
                    existFlag = true;
                }
            }
            if (!existFlag)
            {
                orders.Add(new OrderObject
                {
                    OrderNo = ((ScreenResultTemplatePageLineModel)e.Row.DataContext).Order,
                    PageResultLineGUID = ((ScreenResultTemplatePageLineModel)e.Row.DataContext).PageLineGuid
                });
            }
            ComboBox cbOrder = dg.Columns[0].GetCellContent(e.Row).FindName("cbOrder") as ComboBox;
            cbOrder.ItemsSource = orders;
            cbOrder.Tag = e.Row.DataContext;
            cbOrder.SelectedIndex = ((ScreenResultTemplatePageLineModel)e.Row.DataContext).Order - 1;


            //(dg.Columns[4].GetCellContent(e.Row).FindName("hbtnAddItem") as HyperlinkButton).Tag = e.Row.DataContext;
            (dg.Columns[2].GetCellContent(e.Row).FindName("hbtnBindPageVariable") as HyperlinkButton).Tag = e.Row;
            (dg.Columns[4].GetCellContent(e.Row).FindName("hbtnDeleteResultLine") as HyperlinkButton).Tag = e.Row.DataContext;
            (dg.Columns[5].GetCellContent(e.Row).FindName("hbtnCopy") as HyperlinkButton).Tag = e.Row;
            if (((ScreenResultTemplatePageLineModel)e.Row.DataContext).PageVariable != null && ((ScreenResultTemplatePageLineModel)e.Row.DataContext).PageVariable.Name != "")
            {
                (dg.Columns[2].GetCellContent(e.Row).FindName("hbtnBindPageVariable") as HyperlinkButton).Content = ((ScreenResultTemplatePageLineModel)e.Row.DataContext).PageVariable.Name;
            }
            else
            {
                (dg.Columns[2].GetCellContent(e.Row).FindName("hbtnBindPageVariable") as HyperlinkButton).Content = "Bind Variable";
            }

            if (!PageContentModel.ObjectStatus.ContainsKey(((ScreenResultTemplatePageLineModel)e.Row.DataContext).PageLineGuid))
            {
                PageContentModel.ObjectStatus.Add(((ScreenResultTemplatePageLineModel)e.Row.DataContext).PageLineGuid, ModelStatus.PageLineNoChange);
            }

            ScreenResultTemplatePageLineModel pageLine = e.Row.DataContext as ScreenResultTemplatePageLineModel;

            //foreach (object item in cbName.Items)
            //{
            //    if (((QuestionModel)item).Guid == question.Guid)
            //    {
            //        cbName.SelectedItem = item;
            //        if (((QuestionModel)item).Name == "Slider")
            //        {
            //            (dg.Columns[4].GetCellContent(e.Row).FindName("hbtnAddItem") as HyperlinkButton).Content = EditItem;
            //        }
            //        else
            //        {
            //            (dg.Columns[4].GetCellContent(e.Row).FindName("hbtnAddItem") as HyperlinkButton).Content = AddItem;
            //        }
            //        break;
            //    }
            //}

            dg.Columns[2].IsReadOnly = true;//Bind Variable
            dg.Columns[4].IsReadOnly = true;//Delete
            dg.Columns[5].IsReadOnly = true;//Copy

            ddlFlag = true;
        }

        private void dgResults_RowEditEnded(object sender, DataGridRowEditEndedEventArgs e)
        {
            Guid pageLineGuid = ((ScreenResultTemplatePageLineModel)e.Row.DataContext).PageLineGuid;
            if (PageContentModel.ObjectStatus.ContainsKey(pageLineGuid))
            {
                if (PageContentModel.ObjectStatus[pageLineGuid] != ModelStatus.PageLineAdded)
                {
                    PageContentModel.ObjectStatus[pageLineGuid] = ModelStatus.PageLineUpdated;
                }
            }
        }

        /// <summary>
        /// PageLine Deleted
        /// </summary>
        private void hbtnDeleteResultLine_Click(object sender, RoutedEventArgs e)
        {
            // make the dropdownlist selectchangeevent don't work
            ddlFlag = false;
            ScreenResultTemplatePageLineModel currentPageLine = (ScreenResultTemplatePageLineModel)((HyperlinkButton)sender).Tag;
            if (PageContentModel.ObjectStatus.ContainsKey(((ScreenResultTemplatePageLineModel)((HyperlinkButton)sender).Tag).PageLineGuid))
            {
                if (PageContentModel.ObjectStatus[((ScreenResultTemplatePageLineModel)((HyperlinkButton)sender).Tag).PageLineGuid] != ModelStatus.PageLineAdded)
                {
                    PageContentModel.ObjectStatus[(((HyperlinkButton)sender).Tag as ScreenResultTemplatePageLineModel).PageLineGuid] = ModelStatus.PageLineDeleted;
                }
                else
                {
                    PageContentModel.ObjectStatus.Remove((((HyperlinkButton)sender).Tag as ScreenResultTemplatePageLineModel).PageLineGuid);
                }
            }
            else
            {
                PageContentModel.ObjectStatus.Add((((HyperlinkButton)sender).Tag as ScreenResultTemplatePageLineModel).PageLineGuid, ModelStatus.PageLineDeleted);
            }

            //string question = "{Q:" + ((PageQuestionModel)((HyperlinkButton)sender).Tag).QuestionGuid.ToString() + "}";
            //if (BodyTextBox.Text.Contains(question))
            //{
            //    BodyTextBox.Text = BodyTextBox.Text.Replace(question, "");
            //}
            foreach (ScreenResultTemplatePageLineModel pageLine in PageContentModel.PageLines)
            {
                if (pageLine.Order > currentPageLine.Order)
                {
                    ComboBox cbOrder = dgResults.Columns[0].GetCellContent(pageLine).FindName("cbOrder") as ComboBox;
                    pageLine.Order--;
                    cbOrder.SelectedIndex = pageLine.Order - 1;
                }
            }
            PageContentModel.PageLines.Remove(((HyperlinkButton)sender).Tag as ScreenResultTemplatePageLineModel);
            orders.RemoveAt(orders.Count - 1);
            ddlFlag = true;
        }
        
        private void hbtnAddResultLine_Click(object sender, RoutedEventArgs e)
        {
            //if (PageContentModel.PageLines.Count >= 5)
            //{
            //    HtmlPage.Window.Alert("You can only add up to 5 pageLines on one page.");
            //}
            //else
            {
                ScreenResultTemplatePageLineModel model = new ScreenResultTemplatePageLineModel();
                model.PageLineGuid = Guid.NewGuid();
                model.Order = PageContentModel.PageLines.Count + 1;
                //model.PageLineGuid = PageResultLines[0].PageLineGuid;
                PageContentModel.ObjectStatus.Add(model.PageLineGuid, ModelStatus.PageLineAdded);
                PageContentModel.PageLines.Add(model);
            }
        }

        /// <summary>
        /// Check whether user has fill in all necessary information
        /// </summary>
        /// <returns></returns>
        public string Validate()
        {
            string errorMessage = string.Empty;

            if (string.IsNullOrEmpty(HeadingTextBox.Text.Trim()))
            {
                errorMessage += "Please fill in title of page.\n";
            }
            else if (HeadingTextBox.Text.Trim().Length > 80)
            {
                errorMessage += string.Format("The length of title cannot exceed 80 characters, but you have {0}.\n", HeadingTextBox.Text.Trim().Length);
            }
            //if (string.IsNullOrEmpty(BodyTextBox.Text.Trim()))
            //{
            //    errorMessage += "Please fill in text of page.\n";
            //}
            if (string.IsNullOrEmpty(PrimaryButtonTextBox.Text.Trim()))
            {
                errorMessage += "Please fill in button name of primary button.\n";
            }
            else if (PrimaryButtonTextBox.Text.Trim().Length > 80)
            {
                errorMessage += string.Format("The length of primary button name cannot exceed 80 characters, but you have {0}. \n", PrimaryButtonTextBox.Text.Trim().Length);
            }
            if (PrimaryButtonActionComoboBox.SelectedItem == null)
            {
                errorMessage += "Please choose action of primary button.\n";
            }
            if (PageContentModel.PresenterImageGUID != Guid.Empty &&
                PresenterImagePositionComboBox.SelectedItem == null)
            {
                errorMessage += "Please choose position of presenter image.\n";
            }
            if (PageContentModel.PageLines.Count == 0)
            {
                errorMessage += "Please add at least one screenResultLine.\n";
            }
            else
            {
                foreach (ScreenResultTemplatePageLineModel pageLine in PageContentModel.PageLines)
                {
                    if (string.IsNullOrEmpty(pageLine.Text))
                    {
                        errorMessage += string.Format("Text of pageLine {0} cannot be null or empty. \n", PageContentModel.PageLines.IndexOf(pageLine) + 1);
                    }
                    else if (pageLine.Text.Contains(";"))
                    {
                        errorMessage += string.Format("Text of pageLine {0} contians invalid character ';'. \n", PageContentModel.PageLines.IndexOf(pageLine) + 1);
                    }
                    if (pageLine.PageVariable==null)
                    {
                        errorMessage += string.Format("Bind PageVariable of pageLine {0} cannot be null or empty. \n", PageContentModel.PageLines.IndexOf(pageLine) + 1);
                    }
                }
            }
            return errorMessage;
        }

        public void FillContent()
        {
            PageContentModel.Heading = HeadingTextBox.Text.Trim();
            PageContentModel.Body = BodyTextBox.Text.Trim();
            //if (string.IsNullOrEmpty(SubmitButtonTextBox.Text.Trim()))
            //{
            PageContentModel.PrimaryButtonCaption = PrimaryButtonTextBox.Text.Trim();
            //}
            //else
            //{
            //    PageContentModel.PrimaryButtonCaption = string.Format("{0};{1}", SubmitButtonTextBox.Text.Trim(), PrimaryButtonTextBox.Text.Trim());
            //}
        }

        public void BindPageContent(EditScreenResultsTemplatePageContentModel editScreenResultsTemplatePageContentModel)
        {
            _isDataBinding = true;

            HeadingTextBox.Text = editScreenResultsTemplatePageContentModel.Heading;
            BodyTextBox.Text = editScreenResultsTemplatePageContentModel.Body;
            //SubmitButtonTextBox.Text = editGetInfoTemplatePageContentModel.SecondaryButtonCaption;
            PrimaryButtonTextBox.Text = editScreenResultsTemplatePageContentModel.PrimaryButtonCaption;
            PageContentModel.PrimaryButtonAction = editScreenResultsTemplatePageContentModel.PrimaryButtonAction;
            PageContentModel.AfterExpression = editScreenResultsTemplatePageContentModel.AfterExpression;
            PageContentModel.BeforeExpression = editScreenResultsTemplatePageContentModel.BeforeExpression;

            if (string.IsNullOrEmpty(editScreenResultsTemplatePageContentModel.AfterExpression))
            {
                PrimaryButtonActionComoboBox.SelectedIndex = 0;
            }
            else
            {
                PrimaryButtonActionComoboBox.SelectedIndex = 1;
                PrimaryButtonActionLinkButton.Visibility = Visibility.Visible;
            }

            if (editScreenResultsTemplatePageContentModel.PageGraphic1Image != null)
            {
                GraphicOneNameTextBlock.Text = editScreenResultsTemplatePageContentModel.PageGraphic1Image.Name;
                GraphicOneLinkButton.Content = "Change";
            }
            else
            {
                GraphicOneNameTextBlock.Text = "";
                GraphicOneLinkButton.Content = "Set image";
            }

            if (editScreenResultsTemplatePageContentModel.PageGraphic2Image != null)
            {
                GraphicTwoNameTextBlock.Text = editScreenResultsTemplatePageContentModel.PageGraphic2Image.Name;
                GraphicTwoLinkButton.Content = "Change";
            }
            else
            {
                GraphicTwoNameTextBlock.Text = "";
                GraphicTwoLinkButton.Content = "Set image";
            }

            if (editScreenResultsTemplatePageContentModel.PageGraphic3Image != null)
            {
                GraphicThreeNameTextBlock.Text = editScreenResultsTemplatePageContentModel.PageGraphic3Image.Name;
                GraphicThreeLinkButton.Content = "Change";
            }
            else
            {
                GraphicThreeNameTextBlock.Text = "";
                GraphicThreeLinkButton.Content = "Set image";
            }

            PresenterModeComboBox.SelectedIndex = 0;
            PageContentModel.PresenterMode = "Normal";
            if (editScreenResultsTemplatePageContentModel.PresenterImage != null)
            {
                SetPresenterImage(editScreenResultsTemplatePageContentModel.PresenterImage);

                if (editScreenResultsTemplatePageContentModel.PresenterImagePosition.Equals("Left"))
                {
                    PresenterImagePositionComboBox.SelectedIndex = 0;
                    PageContentModel.PresenterImagePosition = "Left";
                }
                else
                {
                    PresenterImagePositionComboBox.SelectedIndex = 1;
                    PageContentModel.PresenterImagePosition = "Right";
                }

                if (!string.IsNullOrEmpty(editScreenResultsTemplatePageContentModel.PresenterMode))
                {
                    if (editScreenResultsTemplatePageContentModel.PresenterMode.Equals("Big"))
                    {
                        PresenterModeComboBox.SelectedIndex = 1;
                        PageContentModel.PresenterMode = "Big";
                    }
                    else
                    {
                        PresenterModeComboBox.SelectedIndex = 0;
                        PageContentModel.PresenterMode = "Normal";
                    }
                }
            }

            PageResultLines = editScreenResultsTemplatePageContentModel.PageLines;
            PageContentModel.PageLines = editScreenResultsTemplatePageContentModel.PageLines;
            dgResults.ItemsSource = editScreenResultsTemplatePageContentModel.PageLines;

            //for (int index = 0; index < PageContentModel.PageQuestions.Count; index++)
            //{
            //    orders.Add(new OrderObject { OrderNo = index + 1, PageQuestionGUID = PageContentModel.PageQuestions[index].QuestionGuid});
            //}
            _isDataBinding = false;
        }

        public void ResetAfterSave()
        {
            _isDataBinding = true;
            //Reset
            PageContentModel.ObjectStatus.Clear();
            dgResults.ItemsSource = null;
            dgResults.ItemsSource = PageContentModel.PageLines;
            _isDataBinding = false;
        }

        public void EnableControl()
        {
            HeadingTextBox.IsEnabled = true;
            BodyTextBox.IsEnabled = true;
            PrimaryButtonTextBox.IsEnabled = true;
            BeforeShowExpressionLink.IsEnabled = true;
            PrimaryButtonActionComoboBox.IsEnabled = true;
            PrimaryButtonActionLinkButton.IsEnabled = true;
            BodyLink.IsEnabled = true;
            hbtnAddResultLine.IsEnabled = true;
            dgResults.IsEnabled = true;
            SetPresenterImageLinkButton.IsEnabled = true;
            presenterImageReset.IsEnabled = true;
            PresenterImagePositionComboBox.IsEnabled = true;
            InsertResourceBodyLink.IsEnabled = true;
            PresenterModeComboBox.IsEnabled = true;
        }

        public void DisableControl(string reasonStr)
        {
            HeadingTextBox.IsEnabled = false;
            BodyTextBox.IsEnabled = false;
            PrimaryButtonTextBox.IsEnabled = false;
            BeforeShowExpressionLink.IsEnabled = false;
            PrimaryButtonActionComoboBox.IsEnabled = false;
            PrimaryButtonActionLinkButton.IsEnabled = false;
            BodyLink.IsEnabled = false;
            hbtnAddResultLine.IsEnabled = false;
            dgResults.IsEnabled = false;
            SetPresenterImageLinkButton.IsEnabled = false;
            presenterImageReset.IsEnabled = false;
            PresenterImagePositionComboBox.IsEnabled = false;
            InsertResourceBodyLink.IsEnabled = false;
            PresenterModeComboBox.IsEnabled = false;
        }

        private void HeadingLink_Click(object sender, RoutedEventArgs e)
        {
            pageVarible.eventArgs = new PageVariableEventArgs(HeadingTextBox);
            pageVarible.Show();
        }

        void SelectedEvent(object sender, PageVariableEventArgs args)
        {
            if (args.sender is TextBox)
            {
                ((TextBox)args.sender).Text += " {V:" + args.pageVariable.Name + "}";
            }
            else if (args.sender is DataGridRow)
            {
                DataGridRow row = (DataGridRow)args.sender;
                ScreenResultTemplatePageLineModel pageLine = (ScreenResultTemplatePageLineModel)row.DataContext;
                (dgResults.Columns[2].GetCellContent(row).FindName("hbtnBindPageVariable") as HyperlinkButton).Content = args.pageVariable.Name;
                pageLine.PageVariable = new PageVariableModel
                {
                    Description = args.pageVariable.Description,
                    Name = args.pageVariable.Name,
                    PageVariableGUID = args.pageVariable.PageVariableGUID,
                    ProgramGUID = args.pageVariable.ProgramGUID,
                };

                Guid pageLineGuid = pageLine.PageLineGuid;
                if (PageContentModel.ObjectStatus.ContainsKey(pageLineGuid))
                {
                    if (PageContentModel.ObjectStatus[pageLineGuid] != ModelStatus.PageLineAdded)
                    {
                        PageContentModel.ObjectStatus[pageLineGuid] = ModelStatus.PageLineUpdated;
                    }
                }
            }

            ServiceClient serviceProxy = ServiceProxyFactory.Instance.ServiceProxy;
            serviceProxy.UpdatePageVariableLastAccessTimeAsync(args.pageVariable.PageVariableGUID);
        }

        private void BodyLink_Click(object sender, RoutedEventArgs e)
        {
            pageVarible.eventArgs = new PageVariableEventArgs(BodyTextBox);
            pageVarible.Show();
        }

        private void hbtnBindPageVariable_Click(object sender, RoutedEventArgs e)
        {
            pageVarible.eventArgs = new PageVariableEventArgs((DataGridRow)((HyperlinkButton)sender).Tag);
            pageVarible.Show();
           
        }

        private void hbtnCopy_Click(object sender, RoutedEventArgs e)
        {
            //if (PageContentModel.PageLines.Count >= 5)
            //{
            //HtmlPage.Window.Alert("You can only add up to 5 questions on one page.");
            //}
            ScreenResultTemplatePageLineModel pageLine = (ScreenResultTemplatePageLineModel)((DataGridRow)((HyperlinkButton)sender).Tag).DataContext;

            ScreenResultTemplatePageLineModel model = new ScreenResultTemplatePageLineModel()
            {
                PageLineGuid = Guid.NewGuid(),
                Text = pageLine.Text,
                PageVariable = pageLine.PageVariable,
                URL = pageLine.URL,
                Order = PageContentModel.PageLines.Count + 1,
                PageGuid = pageLine.PageGuid
            };
            PageContentModel.ObjectStatus.Add(model.PageLineGuid, ModelStatus.PageLineAdded);

            #region MyRegion
            ////model.SubItems = new ObservableCollection<PageQuestionItemModel>();
            ////foreach (PageQuestionItemModel item in question.SubItems)
            ////{
            ////    PageQuestionItemModel itemModel = new PageQuestionItemModel
            ////    {
            ////        Feedback = item.Feedback,
            ////        Guid = Guid.NewGuid(),
            ////        Item = item.Item,
            ////        Score = item.Score,
            ////        Order = item.Order,
            ////    };
            ////    model.SubItems.Add(itemModel);
            ////} 
            #endregion

            PageContentModel.PageLines.Add(model);
        }

        private void cbOrder_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ddlFlag)
            {
                ddlFlag = false;
                if (e.RemovedItems.Count > 0 && e.AddedItems.Count > 0)
                {
                    ComboBox cbOrder = (ComboBox)sender;

                    int currentOrder = ((OrderObject)e.AddedItems[0]).OrderNo;
                    int originalOrder = ((OrderObject)e.RemovedItems[0]).OrderNo;
                    //((PageQuestionModel)cbOrder.Tag).QuestionGuid;
                    if (originalOrder < currentOrder)
                    {
                        foreach (ScreenResultTemplatePageLineModel pageLine in PageContentModel.PageLines)
                        {
                            if (pageLine.Order <= currentOrder &&
                                pageLine.Order > originalOrder)
                            {
                                pageLine.Order -= 1;
                                ////((ComboBox)dgResults.Columns[0].GetCellContent(questionModel).FindName("cbOrder")).SelectedIndex = pageLine.Order - 1;
                            }
                            else if (pageLine.PageLineGuid == ((ScreenResultTemplatePageLineModel)cbOrder.Tag).PageLineGuid)
                            {
                                pageLine.Order = currentOrder;
                            }
                        }
                    }
                    else if (originalOrder > currentOrder)
                    {
                        foreach (ScreenResultTemplatePageLineModel pageLine in PageContentModel.PageLines)
                        {
                            if (pageLine.Order >= currentOrder &&
                                pageLine.Order < originalOrder)
                            {
                                pageLine.Order += 1;
                                ////((ComboBox)dgResults.Columns[0].GetCellContent(questionModel).FindName("cbOrder")).SelectedIndex = pageLine.Order - 1;
                            }
                            else if (pageLine.PageLineGuid == ((ScreenResultTemplatePageLineModel)cbOrder.Tag).PageLineGuid)
                            {
                                pageLine.Order = currentOrder;
                            }
                        }
                    }

                }
                ddlFlag = true;
            }
        }

        private void PresenterImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //ImageInfo.Show((ResourceModel)(((Image)sender).Tag));
        }

        private void InsertResourceBodyLink_Click(object sender, RoutedEventArgs e)
        {
            _isClickOnInsertResourceBodyLink = true;
            ResourceManager.Show();
        }

        #region Set PageGraphicImage
        private void GraphicOneLinkButton_Click(object sender, RoutedEventArgs e)
        {
            if (GraphicOneNameTextBlock.Tag != null)
            {
                ImageList.LastSelectedResource = (ResourceModel)GraphicOneNameTextBlock.Tag;
            }
            ImageList.Show();
            ImageList.OnSelectImage -= SelectImageEventHandler;
            ImageList.OnSelectImage -= SelectGraphic2ImageEventHandler;
            ImageList.OnSelectImage -= SelectGraphic3ImageEventHandler;
            ImageList.OnSelectImage += new SelectPictureDelegate(SelectGraphic1ImageEventHandler);
        }

        private void GraphicTwoLinkButton_Click(object sender, RoutedEventArgs e)
        {
            if (GraphicTwoNameTextBlock.Tag != null)
            {
                ImageList.LastSelectedResource = (ResourceModel)GraphicTwoNameTextBlock.Tag;
            }
            ImageList.Show();
            ImageList.OnSelectImage -= SelectImageEventHandler;
            ImageList.OnSelectImage -= SelectGraphic1ImageEventHandler;
            ImageList.OnSelectImage -= SelectGraphic3ImageEventHandler;
            ImageList.OnSelectImage += new SelectPictureDelegate(SelectGraphic2ImageEventHandler);
        }

        private void GraphicThreeLinkButton_Click(object sender, RoutedEventArgs e)
        {
            if (GraphicThreeNameTextBlock.Tag != null)
            {
                ImageList.LastSelectedResource = (ResourceModel)GraphicThreeNameTextBlock.Tag;
            }
            ImageList.Show();
            ImageList.OnSelectImage -= SelectImageEventHandler;
            ImageList.OnSelectImage -= SelectGraphic1ImageEventHandler;
            ImageList.OnSelectImage -= SelectGraphic2ImageEventHandler;
            ImageList.OnSelectImage += new SelectPictureDelegate(SelectGraphic3ImageEventHandler);
        }

        private void SelectGraphic1ImageEventHandler(ResourceModel resource)
        {
            SetGraphic1Image(resource);
        }

        private void SetGraphic1Image(ResourceModel resource)
        {
            string imageUri = Constants.ThumbImageDirectory + resource.NameOnServer;
            GraphicOneNameTextBlock.Tag = resource;
            GraphicOneNameTextBlock.Text = resource.Name;
            GraphicOneLinkButton.Content = "Change";
            PageContentModel.PageGraphic1GUID = resource.ID;
        }

        private void SelectGraphic2ImageEventHandler(ResourceModel resource)
        {
            SetGraphic2Image(resource);
        }

        private void SetGraphic2Image(ResourceModel resource)
        {
            string imageUri = Constants.ThumbImageDirectory + resource.NameOnServer;
            GraphicTwoNameTextBlock.Tag = resource;
            GraphicTwoNameTextBlock.Text = resource.Name;
            GraphicTwoLinkButton.Content = "Change";
            PageContentModel.PageGraphic2GUID = resource.ID;
        }

        private void SelectGraphic3ImageEventHandler(ResourceModel resource)
        {
            SetGraphic3Image(resource);
        }

        private void SetGraphic3Image(ResourceModel resource)
        {
            string imageUri = Constants.ThumbImageDirectory + resource.NameOnServer;
            GraphicThreeNameTextBlock.Tag = resource;
            GraphicThreeNameTextBlock.Text = resource.Name;
            GraphicThreeLinkButton.Content = "Change";
            PageContentModel.PageGraphic3GUID = resource.ID;
        } 
        #endregion

        ////#region PageLines
        ////private void dgResults_RowDetailsVisibilityChanged(object sender, DataGridRowDetailsEventArgs e)
        ////{
        ////    if ((QuestionModel)((ComboBox)dgResults.Columns[1].GetCellContent(e.Row).FindName("cbName")).SelectedItem != null)
        ////    {
        ////        if (((QuestionModel)((ComboBox)dgResults.Columns[1].GetCellContent(e.Row).FindName("cbName")).SelectedItem).HasSubItem)
        ////        {
        ////            currentItems = ((PageQuestionModel)((DataGrid)e.DetailsElement).DataContext).SubItems;
        ////            //if (e.Row.DetailsTemplate != null)
        ////            //((DataGrid)e.Row.FindName("dgQuestionItem")).Visibility = Visibility.Visible;
        ////            ((HyperlinkButton)dgResults.Columns[4].GetCellContent(e.Row).FindName("hbtnAddItem")).Visibility = Visibility.Visible;

        ////        }
        ////        else
        ////        {
        ////            currentItems = null;
        ////            //if (e.Row.DetailsTemplate != null)
        ////            //((DataGrid)e.Row.FindName("dgQuestionItem")).Visibility = Visibility.Collapsed;
        ////            ((HyperlinkButton)dgResults.Columns[4].GetCellContent(e.Row).FindName("hbtnAddItem")).Visibility = Visibility.Collapsed;
        ////        }

        ////    }
        ////}

        ////private void cbName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        ////{
        ////    ComboBox cb = sender as ComboBox;
        ////    DataGridRow row = cb.Tag as DataGridRow;
        ////    PageQuestionModel question = row.DataContext as PageQuestionModel;
        ////    QuestionModel questionModel = (QuestionModel)cb.SelectedItem;
        ////    question.Guid = questionModel.Guid;

        ////    //TODO: If page question type is changed, set status
        ////    if (PageContentModel.ObjectStatus.ContainsKey(question.QuestionGuid))
        ////    {
        ////        if (PageContentModel.ObjectStatus[question.QuestionGuid] != ModelStatus.QuestionAdded)
        ////        {
        ////            PageContentModel.ObjectStatus[question.QuestionGuid] = ModelStatus.QuestionUpdated;
        ////        }
        ////    }

        ////    //TODO: Display Add Item link or not
        ////    if (!questionModel.HasSubItem)
        ////    {
        ////        if (question.SubItems != null)
        ////        {
        ////            foreach (PageQuestionItemModel item in question.SubItems)
        ////            {
        ////                if (PageContentModel.ObjectStatus.ContainsKey(item.Guid))
        ////                {
        ////                    PageContentModel.ObjectStatus[item.Guid] = ModelStatus.QuestionItemDeleted;
        ////                }
        ////                else
        ////                {
        ////                    PageContentModel.ObjectStatus.Add(item.Guid, ModelStatus.QuestionItemDeleted);
        ////                }
        ////            }
        ////        }
        ////        question.SubItems = new ObservableCollection<PageQuestionItemModel>();
        ////        //if(row.DetailsTemplate != null)
        ////        //((DataGrid)row.FindName("dgQuestionItem")).Visibility = Visibility.Collapsed;
        ////        ((HyperlinkButton)dgResults.Columns[4].GetCellContent(row).FindName("hbtnAddItem")).Visibility = Visibility.Collapsed;
        ////    }
        ////    else
        ////    {
        ////        //if (row.DetailsTemplate != null)
        ////        //((DataGrid)row.FindName("dgQuestionItem")).Visibility = Visibility.Visible;
        ////        ((HyperlinkButton)dgResults.Columns[4].GetCellContent(row).FindName("hbtnAddItem")).Visibility = Visibility.Visible;
        ////        if (question.SubItems == null)
        ////        {
        ////            question.SubItems = new ObservableCollection<PageQuestionItemModel>();
        ////        }
        ////        if (((sender as ComboBox).SelectedItem as QuestionModel).Name == "Slider")
        ////        {
        ////            ((HyperlinkButton)dgResults.Columns[4].GetCellContent(row).FindName("hbtnAddItem")).Content = EditItem;
        ////        }
        ////        else
        ////        {
        ////            ((HyperlinkButton)dgResults.Columns[4].GetCellContent(row).FindName("hbtnAddItem")).Content = AddItem;
        ////        }
        ////        currentItems = question.SubItems;
        ////    }
        ////}

        ////private void hbtnAddItem_Click(object sender, RoutedEventArgs e)
        ////{
        ////    HyperlinkButton hb = (HyperlinkButton)sender;
        ////    if (hb.Content.ToString() != EditItem)
        ////    {
        ////        PageQuestionModel question = (PageQuestionModel)((HyperlinkButton)sender).Tag;
        ////        if (question.SubItems != null)
        ////        {
        ////            //isNewItem = true;
        ////            PageQuestionItemModel model = new PageQuestionItemModel();
        ////            model.Guid = Guid.NewGuid();
        ////            model.Order = question.SubItems.Count + 1;
        ////            PageContentModel.ObjectStatus.Add(model.Guid, ModelStatus.QuestionItemAdded);
        ////            question.SubItems.Add(model);
        ////        }
        ////    }
        ////    else
        ////    {
        ////        EditSlider.ObjectStatus = PageContentModel.ObjectStatus;
        ////        EditSlider.question = hb.Tag as PageQuestionModel;
        ////        EditSlider.Show();
        ////    }
        ////}



        ////private void dgResultItem_LoadingRow(object sender, DataGridRowEventArgs e)
        ////{
        ////    //if (lodingItemFlag)
        ////    //{
        ////    ddlFlag = false;
        ////    DataGrid dg = sender as DataGrid;
        ////    ComboBox cbItemOrder = dg.Columns[0].GetCellContent(e.Row).FindName("cbItemOrder") as ComboBox;
        ////    bool existFlag = false;
        ////    if (((PageQuestionModel)dg.DataContext).ItemOrderList == null)
        ////    {
        ////        ((PageQuestionModel)dg.DataContext).ItemOrderList = new ObservableCollection<OrderObject>();
        ////    }
        ////    foreach (OrderObject order in ((PageQuestionModel)dg.DataContext).ItemOrderList)
        ////    {
        ////        if (order.OrderNo == ((PageQuestionItemModel)e.Row.DataContext).Order)
        ////        {
        ////            existFlag = true;
        ////        }
        ////    }
        ////    if (!existFlag)
        ////    {
        ////        ((PageQuestionModel)dg.DataContext).ItemOrderList.Add(new OrderObject { OrderNo = ((PageQuestionItemModel)e.Row.DataContext).Order, PageQuestionGUID = ((PageQuestionItemModel)e.Row.DataContext).Guid });
        ////    }
        ////    cbItemOrder.ItemsSource = ((PageQuestionModel)dg.DataContext).ItemOrderList;
        ////    cbItemOrder.SelectedIndex = ((PageQuestionItemModel)e.Row.DataContext).Order - 1;
        ////    cbItemOrder.Tag = dg;

        ////    HyperlinkButton hlb = dg.Columns[4].GetCellContent(e.Row).FindName("hbtnDeleteItem") as HyperlinkButton;
        ////    hlb.Tag = dg;
        ////    if (!PageContentModel.ObjectStatus.ContainsKey(((PageQuestionItemModel)e.Row.DataContext).Guid))
        ////    {
        ////        PageContentModel.ObjectStatus.Add(((PageQuestionItemModel)e.Row.DataContext).Guid, ModelStatus.QuestionItemUpdated);
        ////    }
        ////    ddlFlag = true;
        ////    //}
        ////}

        ////private void dgResultItem_RowEditEnded(object sender, DataGridRowEditEndedEventArgs e)
        ////{
        ////    Guid guid = ((PageQuestionItemModel)e.Row.DataContext).Guid;
        ////    if (PageContentModel.ObjectStatus.ContainsKey(guid))
        ////    {
        ////        if (PageContentModel.ObjectStatus[guid] != ModelStatus.QuestionItemAdded)
        ////        {
        ////            PageContentModel.ObjectStatus[guid] = ModelStatus.QuestionItemUpdated;
        ////        }
        ////    }
        ////}

        ////private void hbtnDeleteItem_Click(object sender, RoutedEventArgs e)
        ////{
        ////    ddlFlag = false;
        ////    PageQuestionItemModel currentItemModel = DataGridRow.GetRowContainingElement((HyperlinkButton)sender).DataContext as PageQuestionItemModel;
        ////    if (PageContentModel.ObjectStatus.ContainsKey(currentItemModel.Guid))
        ////    {
        ////        if (PageContentModel.ObjectStatus[currentItemModel.Guid] != ModelStatus.QuestionItemAdded)
        ////        {
        ////            PageContentModel.ObjectStatus[currentItemModel.Guid] = ModelStatus.QuestionItemDeleted;
        ////        }
        ////        else
        ////        {
        ////            PageContentModel.ObjectStatus.Remove(currentItemModel.Guid);
        ////        }
        ////    }
        ////    else
        ////    {
        ////        PageContentModel.ObjectStatus.Add(currentItemModel.Guid, ModelStatus.QuestionItemDeleted);
        ////    }

        ////    DataGrid dg = ((HyperlinkButton)sender).Tag as DataGrid;
        ////    foreach (PageQuestionItemModel itemModel in currentItems)
        ////    {
        ////        if (itemModel.Order > currentItemModel.Order)
        ////        {
        ////            ComboBox cbItemOrder = dg.Columns[0].GetCellContent(itemModel).FindName("cbItemOrder") as ComboBox;
        ////            itemModel.Order--;
        ////            cbItemOrder.SelectedIndex = itemModel.Order - 1;
        ////        }
        ////    }
        ////    currentItems.Remove(currentItemModel);
        ////    // update dropdownlist
        ////    PageQuestionModel questionModel = (PageQuestionModel)dgResults.SelectedItem;
        ////    questionModel.ItemOrderList.RemoveAt(questionModel.ItemOrderList.Count - 1);
        ////    ddlFlag = true;
        ////}

        ////#endregion

        //not use
        //private void cbItemOrder_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    if (ddlFlag)
        //    {
        //        ddlFlag = false;
        //        if (e.AddedItems.Count > 0 && e.RemovedItems.Count > 0)
        //        {
        //            ComboBox cbOrder = (ComboBox)sender;

        //            //DataGrid dg = (DataGrid)sender;
        //            int currentOrder = ((OrderObject)e.AddedItems[0]).OrderNo;
        //            int originalOrder = ((OrderObject)e.RemovedItems[0]).OrderNo;
        //            Guid reOrderItemGuid = Guid.Empty;
        //            foreach (PageQuestionItemModel questionItemModel in currentItems)
        //            {
        //                if (questionItemModel.Order == originalOrder)
        //                {
        //                    reOrderItemGuid = questionItemModel.Guid;
        //                    break;
        //                }
        //            }

        //            //((PageQuestionModel)dg.DataContext).SubItems;
        //            if (originalOrder < currentOrder)
        //            {
        //                foreach (PageQuestionItemModel questionItemModel in currentItems)
        //                {
        //                    if (questionItemModel.Order <= currentOrder &&
        //                        questionItemModel.Order > originalOrder)
        //                    {
        //                        ((ComboBox)((DataGrid)cbOrder.Tag).Columns[0].GetCellContent(questionItemModel).FindName("cbItemOrder")).SelectedIndex = questionItemModel.Order - 2;
        //                        questionItemModel.Order -= 1;
        //                        //if (PageContentModel.ObjectStatus.ContainsKey(questionItemModel.Guid))
        //                        //{
        //                        //    if (PageContentModel.ObjectStatus[questionItemModel.Guid] != ModelStatus.QuestionItemAdded && PageContentModel.ObjectStatus[questionItemModel.Guid]!=ModelStatus.QuestionItemDeleted)
        //                        //    {
        //                        //        PageContentModel.ObjectStatus[questionItemModel.Guid] = ModelStatus.QuestionItemUpdated;
        //                        //    }
        //                        //}
        //                    }
        //                    else if (questionItemModel.Guid == reOrderItemGuid)
        //                    {
        //                        questionItemModel.Order = currentOrder;
        //                    }
        //                }
        //            }
        //            else if (originalOrder > currentOrder)
        //            {
        //                foreach (PageQuestionItemModel questionItemModel in currentItems)
        //                {
        //                    if (questionItemModel.Order >= currentOrder &&
        //                        questionItemModel.Order < originalOrder)
        //                    {
        //                        questionItemModel.Order += 1;
        //                        ((ComboBox)((DataGrid)cbOrder.Tag).Columns[0].GetCellContent(questionItemModel).FindName("cbItemOrder")).SelectedIndex = questionItemModel.Order - 1;
        //                    }
        //                    else if (questionItemModel.Guid == reOrderItemGuid)
        //                    {
        //                        questionItemModel.Order = currentOrder;
        //                    }
        //                }
        //            }
        //        }
        //        ddlFlag = true;
        //    }
        //}


        //private bool HasSubItems(PageQuestionModel pagequestion)
        //{
        //    bool hasSubItemFlag = true;
        //    foreach (QuestionModel questionModel in Questions)
        //    {
        //        if (questionModel.Guid == pagequestion.Guid)
        //        {
        //            hasSubItemFlag = questionModel.HasSubItem;
        //            break;
        //        }
        //    }
        //    return hasSubItemFlag;
        //}
    }
}
