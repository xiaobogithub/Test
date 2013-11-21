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
    public partial class GetInformationTemplate : UserControl
    {
        public Guid ImageGuid { get; set; }
        public ObservableCollection<QuestionModel> Questions { get; set; }
        private ObservableCollection<OrderObject> orders { get; set; }
        private ObservableCollection<PageQuestionItemModel> currentItems;
        private bool _isDataBinding = false;
        private bool _isClickOnInsertResourceBodyLink;

        public GetInfoTemplatePageContentModel PageContentModel { get; set; }
        public DesignType DesignType { get; set; }
        public const string EditItem = "Edit Items";
        public const string AddItem = "Add Items";
        bool ddlFlag = true;

        //bool isNewItem = false;

        public GetInformationTemplate()
        {
            InitializeComponent();

            Questions = new ObservableCollection<QuestionModel>();
            PageContentModel = new GetInfoTemplatePageContentModel();
            orders = new ObservableCollection<OrderObject>();
            PageContentModel.ObjectStatus = new Dictionary<Guid, ModelStatus>();
            PageContentModel.PageQuestions = new ObservableCollection<PageQuestionModel>();
            pageVarible.SelectedEvent += new ChangeTech.Silverlight.DesignPage.Controls.PageVariableManager.SelectedDelegate(SelectedEvent);
            ExpressionBuilder.SetExpressionEventHandler += new SetExpressionDelegate(ExpressionBuilder_SetExpressionEventHandler);
            ResourceManager.SelectResourceEventHandler += new SelectResourceDelegate(ResourceManager_SelectResourceEventHandler);
            ImageList.OnSelectImage += new SelectPictureDelegate(SelectImageEventHandler);
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
                if (!string.IsNullOrEmpty(FooterTextBox.Text))
                {
                    FooterTextBox.Text += "\n" + string.Format(downloadLink, resourceURL, e.Resource.Name);
                }
                else
                {
                    FooterTextBox.Text += string.Format(downloadLink, resourceURL, e.Resource.Name);
                }
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
            if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                return;
            }
            if (!IsEditPagesequenceOnly())
            {
                PageContentModel.SessionGUID = new Guid(StringUtility.GetQueryString(Constants.QUERYSTR_SESSION_GUID));
            }
            //PageContentModel.LanguageGUID = new Guid(StringUtility.GetQueryString(Constants.QUERYSTR_LANGUAGE_GUID));
            PageContentModel.PageSequenceGUID = new Guid(StringUtility.GetQueryString(Constants.QUERYSTR_PAGE_SEQUENCE_GUID));
            dgQuestions.ItemsSource = PageContentModel.PageQuestions;

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
        //Reset image
        private void presenterImageReset_Click(object sender, RoutedEventArgs e)
        {
            if (PageContentModel.PresenterImageGUID != Guid.Empty
                || PageContentModel.BackgroundImageGUID != Guid.Empty
                || PageContentModel.IllustrationImageGUID != Guid.Empty)
            {
                SetPresenterImageLinkButton.Content = "Set image";
                PresenterImageNameTextBlock.Text = "";
                PageContentModel.PresenterImageGUID = Guid.Empty;
                PageContentModel.BackgroundImageGUID = Guid.Empty;
                PageContentModel.IllustrationImageGUID = Guid.Empty;
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
        }

        private void SelectImageEventHandler(ResourceModel image)
        {
            SetImage(image);
        }

        private void SetImage(ResourceModel resource)
        {
            string imageUri = Constants.ThumbImageDirectory + resource.NameOnServer;
            PresenterImage.Tag = resource;

            ImageUtility.ShowImage(PresenterImage, imageUri);
            PresenterImageNameTextBlock.Text = resource.Name;
            SetPresenterImageLinkButton.Content = "Change image";
            PageContentModel.BackgroundImageGUID = resource.ID;
            ImageGuid = resource.ID;
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

        #region Questions
        private void dgQuestions_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            ddlFlag = false;
            DataGrid dg = (DataGrid)sender;
            ComboBox cbName = dg.Columns[1].GetCellContent(e.Row).FindName("cbName") as ComboBox;
            cbName.ItemsSource = Questions;
            cbName.Tag = e.Row;
            bool existFlag = false;
            foreach (OrderObject orderobject in orders)
            {
                if (orderobject.OrderNo == ((PageQuestionModel)e.Row.DataContext).Order)
                {
                    existFlag = true;
                }
            }
            if (!existFlag)
            {
                orders.Add(new OrderObject
                {
                    OrderNo = ((PageQuestionModel)e.Row.DataContext).Order,
                    PageQuestionGUID = ((PageQuestionModel)e.Row.DataContext).QuestionGuid
                });
            }
            ComboBox cbOrder = dg.Columns[0].GetCellContent(e.Row).FindName("cbOrder") as ComboBox;
            cbOrder.ItemsSource = orders;
            cbOrder.Tag = e.Row.DataContext;
            cbOrder.SelectedIndex = ((PageQuestionModel)e.Row.DataContext).Order - 1;


            (dg.Columns[4].GetCellContent(e.Row).FindName("hbtnAddItem") as HyperlinkButton).Tag = e.Row.DataContext;
            (dg.Columns[5].GetCellContent(e.Row).FindName("hbtnDeleteQuestion") as HyperlinkButton).Tag = e.Row.DataContext;
            (dg.Columns[6].GetCellContent(e.Row).FindName("hbtnBindPageVariable") as HyperlinkButton).Tag = e.Row;
            (dg.Columns[7].GetCellContent(e.Row).FindName("hbtnCopy") as HyperlinkButton).Tag = e.Row;
            if (((PageQuestionModel)e.Row.DataContext).PageVariable != null && ((PageQuestionModel)e.Row.DataContext).PageVariable.Name != "")
            {
                (dg.Columns[6].GetCellContent(e.Row).FindName("hbtnBindPageVariable") as HyperlinkButton).Content = ((PageQuestionModel)e.Row.DataContext).PageVariable.Name;
            }
            else
            {
                (dg.Columns[6].GetCellContent(e.Row).FindName("hbtnBindPageVariable") as HyperlinkButton).Content = "Bind Variable";
            }

            if (!PageContentModel.ObjectStatus.ContainsKey(((PageQuestionModel)e.Row.DataContext).QuestionGuid))
            {
                PageContentModel.ObjectStatus.Add(((PageQuestionModel)e.Row.DataContext).QuestionGuid, ModelStatus.QuestionNoChange);
            }

            PageQuestionModel question = e.Row.DataContext as PageQuestionModel;
            //// for item order list
            //question.ItemOrderList = new ObservableCollection<OrderObject>();
            //foreach(PageQuestionItemModel pqim in question.SubItems)
            //{
            //    question.ItemOrderList.Add(new OrderObject { OrderNo=pqim.Order,PageQuestionGUID=pqim.Guid});
            //}

            foreach (object item in cbName.Items)
            {
                if (((QuestionModel)item).Guid == question.Guid)
                {
                    cbName.SelectedItem = item;
                    if (((QuestionModel)item).Name == "Slider")
                    {
                        (dg.Columns[4].GetCellContent(e.Row).FindName("hbtnAddItem") as HyperlinkButton).Content = EditItem;
                    }
                    else
                    {
                        (dg.Columns[4].GetCellContent(e.Row).FindName("hbtnAddItem") as HyperlinkButton).Content = AddItem;
                    }
                    break;
                }
            }

            dg.Columns[4].IsReadOnly = true;
            dg.Columns[5].IsReadOnly = true;
            dg.Columns[6].IsReadOnly = true;
            dg.Columns[7].IsReadOnly = true;

            ddlFlag = true;
        }

        private void dgQuestions_RowDetailsVisibilityChanged(object sender, DataGridRowDetailsEventArgs e)
        {
            if ((QuestionModel)((ComboBox)dgQuestions.Columns[1].GetCellContent(e.Row).FindName("cbName")).SelectedItem != null)
            {
                if (((QuestionModel)((ComboBox)dgQuestions.Columns[1].GetCellContent(e.Row).FindName("cbName")).SelectedItem).HasSubItem)
                {
                    currentItems = ((PageQuestionModel)((DataGrid)e.DetailsElement).DataContext).SubItems;
                    //if (e.Row.DetailsTemplate != null)
                    //((DataGrid)e.Row.FindName("dgQuestionItem")).Visibility = Visibility.Visible;
                    ((HyperlinkButton)dgQuestions.Columns[4].GetCellContent(e.Row).FindName("hbtnAddItem")).Visibility = Visibility.Visible;

                }
                else
                {
                    currentItems = null;
                    //if (e.Row.DetailsTemplate != null)
                    //((DataGrid)e.Row.FindName("dgQuestionItem")).Visibility = Visibility.Collapsed;
                    ((HyperlinkButton)dgQuestions.Columns[4].GetCellContent(e.Row).FindName("hbtnAddItem")).Visibility = Visibility.Collapsed;
                }

            }
        }

        private void dgQuestions_RowEditEnded(object sender, DataGridRowEditEndedEventArgs e)
        {
            Guid guid = ((PageQuestionModel)e.Row.DataContext).QuestionGuid;
            if (PageContentModel.ObjectStatus.ContainsKey(guid))
            {
                if (PageContentModel.ObjectStatus[guid] != ModelStatus.QuestionAdded)
                {
                    PageContentModel.ObjectStatus[guid] = ModelStatus.QuestionUpdated;
                }
            }
        }

        private void cbName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            DataGridRow row = cb.Tag as DataGridRow;
            PageQuestionModel question = row.DataContext as PageQuestionModel;
            QuestionModel questionModel = (QuestionModel)cb.SelectedItem;
            question.Guid = questionModel.Guid;

            //TODO: If page question type is changed, set status
            if (PageContentModel.ObjectStatus.ContainsKey(question.QuestionGuid))
            {
                if (PageContentModel.ObjectStatus[question.QuestionGuid] != ModelStatus.QuestionAdded)
                {
                    PageContentModel.ObjectStatus[question.QuestionGuid] = ModelStatus.QuestionUpdated;
                }
            }

            //TODO: Display Add Item link or not
            if (!questionModel.HasSubItem)
            {
                if (question.SubItems != null)
                {
                    foreach (PageQuestionItemModel item in question.SubItems)
                    {
                        if (PageContentModel.ObjectStatus.ContainsKey(item.Guid))
                        {
                            PageContentModel.ObjectStatus[item.Guid] = ModelStatus.QuestionItemDeleted;
                        }
                        else
                        {
                            PageContentModel.ObjectStatus.Add(item.Guid, ModelStatus.QuestionItemDeleted);
                        }
                    }
                }
                question.SubItems = new ObservableCollection<PageQuestionItemModel>();
                //if(row.DetailsTemplate != null)
                //((DataGrid)row.FindName("dgQuestionItem")).Visibility = Visibility.Collapsed;
                ((HyperlinkButton)dgQuestions.Columns[4].GetCellContent(row).FindName("hbtnAddItem")).Visibility = Visibility.Collapsed;
            }
            else
            {
                //if (row.DetailsTemplate != null)
                //((DataGrid)row.FindName("dgQuestionItem")).Visibility = Visibility.Visible;
                ((HyperlinkButton)dgQuestions.Columns[4].GetCellContent(row).FindName("hbtnAddItem")).Visibility = Visibility.Visible;
                if (question.SubItems == null)
                {
                    question.SubItems = new ObservableCollection<PageQuestionItemModel>();
                }
                if (((sender as ComboBox).SelectedItem as QuestionModel).Name == "Slider")
                {
                    ((HyperlinkButton)dgQuestions.Columns[4].GetCellContent(row).FindName("hbtnAddItem")).Content = EditItem;
                }
                else
                {
                    ((HyperlinkButton)dgQuestions.Columns[4].GetCellContent(row).FindName("hbtnAddItem")).Content = AddItem;
                }
                currentItems = question.SubItems;
            }
        }

        private void hbtnAddItem_Click(object sender, RoutedEventArgs e)
        {
            HyperlinkButton hb = (HyperlinkButton)sender;
            if (hb.Content.ToString() != EditItem)
            {
                PageQuestionModel question = (PageQuestionModel)((HyperlinkButton)sender).Tag;
                if (question.SubItems != null)
                {
                    //isNewItem = true;
                    PageQuestionItemModel model = new PageQuestionItemModel();
                    model.Guid = Guid.NewGuid();
                    model.Order = question.SubItems.Count + 1;
                    PageContentModel.ObjectStatus.Add(model.Guid, ModelStatus.QuestionItemAdded);
                    question.SubItems.Add(model);
                }
            }
            else
            {
                EditSlider.ObjectStatus = PageContentModel.ObjectStatus;
                EditSlider.question = hb.Tag as PageQuestionModel;
                EditSlider.Show();
            }
        }

        private void hbtnDeleteQuestion_Click(object sender, RoutedEventArgs e)
        {
            // make the dropdownlist selectchangeevent don't work
            ddlFlag = false;
            PageQuestionModel currentPageQuestion = (PageQuestionModel)((HyperlinkButton)sender).Tag;
            if (PageContentModel.ObjectStatus.ContainsKey(((PageQuestionModel)((HyperlinkButton)sender).Tag).QuestionGuid))
            {
                if (PageContentModel.ObjectStatus[((PageQuestionModel)((HyperlinkButton)sender).Tag).QuestionGuid] != ModelStatus.QuestionAdded)
                {
                    PageContentModel.ObjectStatus[(((HyperlinkButton)sender).Tag as PageQuestionModel).QuestionGuid] = ModelStatus.QuestionDeleted;
                }
                else
                {
                    PageContentModel.ObjectStatus.Remove((((HyperlinkButton)sender).Tag as PageQuestionModel).QuestionGuid);
                }
            }
            else
            {
                PageContentModel.ObjectStatus.Add((((HyperlinkButton)sender).Tag as PageQuestionModel).QuestionGuid, ModelStatus.QuestionDeleted);
            }

            //string question = "{Q:" + ((PageQuestionModel)((HyperlinkButton)sender).Tag).QuestionGuid.ToString() + "}";
            //if (BodyTextBox.Text.Contains(question))
            //{
            //    BodyTextBox.Text = BodyTextBox.Text.Replace(question, "");
            //}
            foreach (PageQuestionModel questionModel in PageContentModel.PageQuestions)
            {
                if (questionModel.Order > currentPageQuestion.Order)
                {
                    ComboBox cbOrder = dgQuestions.Columns[0].GetCellContent(questionModel).FindName("cbOrder") as ComboBox;
                    questionModel.Order--;
                    cbOrder.SelectedIndex = questionModel.Order - 1;
                }
            }
            PageContentModel.PageQuestions.Remove(((HyperlinkButton)sender).Tag as PageQuestionModel);
            orders.RemoveAt(orders.Count - 1);
            ddlFlag = true;
        }

        private void dgQuestionItem_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            //if (lodingItemFlag)
            //{
            ddlFlag = false;
            DataGrid dg = sender as DataGrid;
            ComboBox cbItemOrder = dg.Columns[0].GetCellContent(e.Row).FindName("cbItemOrder") as ComboBox;
            bool existFlag = false;
            if (((PageQuestionModel)dg.DataContext).ItemOrderList == null)
            {
                ((PageQuestionModel)dg.DataContext).ItemOrderList = new ObservableCollection<OrderObject>();
            }
            foreach (OrderObject order in ((PageQuestionModel)dg.DataContext).ItemOrderList)
            {
                if (order.OrderNo == ((PageQuestionItemModel)e.Row.DataContext).Order)
                {
                    existFlag = true;
                }
            }
            if (!existFlag)
            {
                ((PageQuestionModel)dg.DataContext).ItemOrderList.Add(new OrderObject { OrderNo = ((PageQuestionItemModel)e.Row.DataContext).Order, PageQuestionGUID = ((PageQuestionItemModel)e.Row.DataContext).Guid });
            }
            cbItemOrder.ItemsSource = ((PageQuestionModel)dg.DataContext).ItemOrderList;
            cbItemOrder.SelectedIndex = ((PageQuestionItemModel)e.Row.DataContext).Order - 1;
            cbItemOrder.Tag = dg;

            HyperlinkButton hlb = dg.Columns[4].GetCellContent(e.Row).FindName("hbtnDeleteItem") as HyperlinkButton;
            hlb.Tag = dg;
            if (!PageContentModel.ObjectStatus.ContainsKey(((PageQuestionItemModel)e.Row.DataContext).Guid))
            {
                PageContentModel.ObjectStatus.Add(((PageQuestionItemModel)e.Row.DataContext).Guid, ModelStatus.QuestionItemUpdated);
            }
            ddlFlag = true;
            //}
        }

        private void dgQuestionItem_RowEditEnded(object sender, DataGridRowEditEndedEventArgs e)
        {
            Guid guid = ((PageQuestionItemModel)e.Row.DataContext).Guid;
            if (PageContentModel.ObjectStatus.ContainsKey(guid))
            {
                if (PageContentModel.ObjectStatus[guid] != ModelStatus.QuestionItemAdded)
                {
                    PageContentModel.ObjectStatus[guid] = ModelStatus.QuestionItemUpdated;
                }
            }
        }

        private void hbtnDeleteItem_Click(object sender, RoutedEventArgs e)
        {
            ddlFlag = false;
            PageQuestionItemModel currentItemModel = DataGridRow.GetRowContainingElement((HyperlinkButton)sender).DataContext as PageQuestionItemModel;
            if (PageContentModel.ObjectStatus.ContainsKey(currentItemModel.Guid))
            {
                if (PageContentModel.ObjectStatus[currentItemModel.Guid] != ModelStatus.QuestionItemAdded)
                {
                    PageContentModel.ObjectStatus[currentItemModel.Guid] = ModelStatus.QuestionItemDeleted;
                }
                else
                {
                    PageContentModel.ObjectStatus.Remove(currentItemModel.Guid);
                }
            }
            else
            {
                PageContentModel.ObjectStatus.Add(currentItemModel.Guid, ModelStatus.QuestionItemDeleted);
            }

            DataGrid dg = ((HyperlinkButton)sender).Tag as DataGrid;
            foreach (PageQuestionItemModel itemModel in currentItems)
            {
                if (itemModel.Order > currentItemModel.Order)
                {
                    ComboBox cbItemOrder = dg.Columns[0].GetCellContent(itemModel).FindName("cbItemOrder") as ComboBox;
                    itemModel.Order--;
                    cbItemOrder.SelectedIndex = itemModel.Order - 1;
                }
            }
            currentItems.Remove(currentItemModel);
            // update dropdownlist
            PageQuestionModel questionModel = (PageQuestionModel)dgQuestions.SelectedItem;
            questionModel.ItemOrderList.RemoveAt(questionModel.ItemOrderList.Count - 1);
            ddlFlag = true;
        }

        private void hbtnAddQuestion_Click(object sender, RoutedEventArgs e)
        {
            if (PageContentModel.PageQuestions.Count >= 5)
            {
                HtmlPage.Window.Alert("You can only add up to 5 questions on one page.");
            }
            else
            {
                //isNewItem = true;
                PageQuestionModel model = new PageQuestionModel();
                model.QuestionGuid = Guid.NewGuid();
                //BodyTextBox.Text += "\r{Q:" + model.QuestionGuid + "}";
                model.Order = PageContentModel.PageQuestions.Count + 1;
                model.Guid = Questions[0].Guid;
                model.SubItems = new ObservableCollection<PageQuestionItemModel>();
                PageContentModel.ObjectStatus.Add(model.QuestionGuid, ModelStatus.QuestionAdded);
                PageContentModel.PageQuestions.Add(model);
                //BodyTextBox.Text += string.Format("[PageQuestion:{0}]", model.QuestionGuid);
                //orders.Add(new OrderObject { OrderNo = orders.Count + 1, PageQuestionGUID = model.QuestionGuid });
            }
        }
        #endregion

        #region Validate page content
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
            //if (PageContentModel.PresenterImageGUID == Guid.Empty
            //   && PageContentModel.BackgroundImageGUID == Guid.Empty
            //   && PageContentModel.IllustrationImageGUID == Guid.Empty)
            //{
            //    errorMessage += "Please choose image and image mode.\n";
            //}
            //if (PageContentModel.PresenterImageGUID != Guid.Empty &&
            //    PresenterImagePositionComboBox.SelectedItem == null)
            //{
            //    errorMessage += "Please choose position of presenter image.\n";
            //}
            if (PageContentModel.PageQuestions.Count == 0)
            {
                errorMessage += "Please add at least one question.\n";
            }
            else
            {
                foreach (PageQuestionModel question in PageContentModel.PageQuestions)
                {
                    if (string.IsNullOrEmpty(question.Caption))
                    {
                        errorMessage += string.Format("Caption of question {0} cannot be null or empty. \n", PageContentModel.PageQuestions.IndexOf(question) + 1);
                    }
                    else if (question.Caption.Contains(";"))
                    {
                        errorMessage += string.Format("Caption of question {0} contians invalid character ';'. \n", PageContentModel.PageQuestions.IndexOf(question) + 1);
                    }

                    // validate question item
                    if (HasSubItems(question))
                    {
                        if (question.SubItems.Count > 0)
                        {
                            foreach (PageQuestionItemModel questionItem in question.SubItems)
                            {
                                if (string.IsNullOrEmpty(questionItem.Item))
                                {
                                    errorMessage += string.Format("Item {0} of question {1} cannot be null or empty. \n", question.SubItems.IndexOf(questionItem), PageContentModel.PageQuestions.IndexOf(question));
                                }
                            }
                        }
                        else
                        {
                            errorMessage += string.Format("Please add items for Quesion {0}. \n", PageContentModel.PageQuestions.IndexOf(question) + 1);
                        }
                    }
                }
            }
            return errorMessage;
        } 
        #endregion

        private bool HasSubItems(PageQuestionModel pagequestion)
        {
            bool hasSubItemFlag = true;
            foreach (QuestionModel questionModel in Questions)
            {
                if (questionModel.Guid == pagequestion.Guid)
                {
                    hasSubItemFlag = questionModel.HasSubItem;
                    break;
                }
            }
            return hasSubItemFlag;
        }

        public void FillContent()
        {
            PageContentModel.Heading = HeadingTextBox.Text.Trim();
            PageContentModel.Body = BodyTextBox.Text.Trim();
            PageContentModel.FooterText = FooterTextBox.Text.Trim();
            //if (string.IsNullOrEmpty(SubmitButtonTextBox.Text.Trim()))
            //{
            PageContentModel.PrimaryButtonCaption = PrimaryButtonTextBox.Text.Trim();
            //}
            //else
            //{
            //    PageContentModel.PrimaryButtonCaption = string.Format("{0};{1}", SubmitButtonTextBox.Text.Trim(), PrimaryButtonTextBox.Text.Trim());
            //}
        }

        public void BindPageContent(EditGetInfoTemplatePageContentModel editGetInfoTemplatePageContentModel)
        {
            _isDataBinding = true;

            HeadingTextBox.Text = editGetInfoTemplatePageContentModel.Heading;
            BodyTextBox.Text = editGetInfoTemplatePageContentModel.Body;
            if (editGetInfoTemplatePageContentModel.FooterText == null)
            {
                editGetInfoTemplatePageContentModel.FooterText = string.Empty;
            }
            FooterTextBox.Text = editGetInfoTemplatePageContentModel.FooterText;
            //SubmitButtonTextBox.Text = editGetInfoTemplatePageContentModel.SecondaryButtonCaption;
            PrimaryButtonTextBox.Text = editGetInfoTemplatePageContentModel.PrimaryButtonCaption;
            PageContentModel.PrimaryButtonAction = editGetInfoTemplatePageContentModel.PrimaryButtonAction;
            PageContentModel.AfterExpression = editGetInfoTemplatePageContentModel.AfterExpression;
            PageContentModel.BeforeExpression = editGetInfoTemplatePageContentModel.BeforeExpression;

            if (string.IsNullOrEmpty(editGetInfoTemplatePageContentModel.AfterExpression))
            {
                PrimaryButtonActionComoboBox.SelectedIndex = 0;
            }
            else
            {
                PrimaryButtonActionComoboBox.SelectedIndex = 1;
                PrimaryButtonActionLinkButton.Visibility = Visibility.Visible;
            }

            if (editGetInfoTemplatePageContentModel.BackgroudImage != null)
            {
                SetImage(editGetInfoTemplatePageContentModel.BackgroudImage);
                if (editGetInfoTemplatePageContentModel.ImageMode == ImageModeEnum.FullscreenMode)
                {
                    FullscreenModeRadioButton.IsChecked = true;
                }
            }
            else if (editGetInfoTemplatePageContentModel.PresenterImage != null)
            {
                SetImage(editGetInfoTemplatePageContentModel.PresenterImage);
                if (editGetInfoTemplatePageContentModel.ImageMode == ImageModeEnum.PresenterMode)
                {
                    PresenterModeRadioButton.IsChecked = true;
                }
            }
            else if (editGetInfoTemplatePageContentModel.IllustrationImage != null)
            {
                SetImage(editGetInfoTemplatePageContentModel.IllustrationImage);
                if (editGetInfoTemplatePageContentModel.ImageMode == ImageModeEnum.IllustrationMode)
                {
                    IllustrationModeRadioButton.IsChecked = true;
                }
            }

            PresenterModeComboBox.SelectedIndex = 0;
            PageContentModel.PresenterMode = "Normal";
            #region MyRegion
            //if (editGetInfoTemplatePageContentModel.PresenterImage != null)
            //{
            //    SetImage(editGetInfoTemplatePageContentModel.PresenterImage);

            //    if (editGetInfoTemplatePageContentModel.PresenterImagePosition.Equals("Left"))
            //    {
            //        PresenterImagePositionComboBox.SelectedIndex = 0;
            //        PageContentModel.PresenterImagePosition = "Left";
            //    }
            //    else
            //    {
            //        PresenterImagePositionComboBox.SelectedIndex = 1;
            //        PageContentModel.PresenterImagePosition = "Right";
            //    }

            //    if (!string.IsNullOrEmpty(editGetInfoTemplatePageContentModel.PresenterMode))
            //    {
            //        if (editGetInfoTemplatePageContentModel.PresenterMode.Equals("Big"))
            //        {
            //            PresenterModeComboBox.SelectedIndex = 1;
            //            PageContentModel.PresenterMode = "Big";
            //        }
            //        else
            //        {
            //            PresenterModeComboBox.SelectedIndex = 0;
            //            PageContentModel.PresenterMode = "Normal";
            //        }
            //    }
            //} 
            #endregion

            Questions = editGetInfoTemplatePageContentModel.Questions;
            PageContentModel.PageQuestions = editGetInfoTemplatePageContentModel.PageQuestions;
            foreach (PageQuestionModel pagequestion in PageContentModel.PageQuestions)
            {
                pagequestion.ItemOrderList = new ObservableCollection<OrderObject>();
            }

            dgQuestions.ItemsSource = editGetInfoTemplatePageContentModel.PageQuestions;

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
            dgQuestions.ItemsSource = null;
            dgQuestions.ItemsSource = PageContentModel.PageQuestions;
            _isDataBinding = false;
        }

        public void EnableControl()
        {
            HeadingTextBox.IsEnabled = true;
            BodyTextBox.IsEnabled = true;
            PrimaryButtonTextBox.IsEnabled = true;
            //SubmitButtonTextBox.IsEnabled = true;
            BeforeShowExpressionLink.IsEnabled = true;
            PrimaryButtonActionComoboBox.IsEnabled = true;
            PrimaryButtonActionLinkButton.IsEnabled = true;
            BodyLink.IsEnabled = true;
            FooterTextLink.IsEnabled = true;
            FooterTextBox.IsEnabled = true;
            hbtnAddQuestion.IsEnabled = true;
            dgQuestions.IsEnabled = true;
            SetPresenterImageLinkButton.IsEnabled = true;
            presenterImageReset.IsEnabled = true;
            PresenterImagePositionComboBox.IsEnabled = true;
            InsertResourceBodyLink.IsEnabled = true;
            InsertResourceFooterLink.IsEnabled = true;
            PresenterModeComboBox.IsEnabled = true;
            IllustrationModeRadioButton.IsEnabled = true;
            PresenterModeRadioButton.IsEnabled = true;
            FullscreenModeRadioButton.IsEnabled = true;
        }

        public void DisableControl(string reasonStr)
        {
            HeadingTextBox.IsEnabled = false;
            BodyTextBox.IsEnabled = false;
            PrimaryButtonTextBox.IsEnabled = false;
            //SubmitButtonTextBox.IsEnabled = false;
            BeforeShowExpressionLink.IsEnabled = false;
            PrimaryButtonActionComoboBox.IsEnabled = false;
            PrimaryButtonActionLinkButton.IsEnabled = false;
            BodyLink.IsEnabled = false;
            FooterTextLink.IsEnabled = false;
            FooterTextBox.IsEnabled = false;
            hbtnAddQuestion.IsEnabled = false;
            dgQuestions.IsEnabled = false;
            SetPresenterImageLinkButton.IsEnabled = false;
            presenterImageReset.IsEnabled = false;
            PresenterImagePositionComboBox.IsEnabled = false;
            InsertResourceBodyLink.IsEnabled = false;
            InsertResourceFooterLink.IsEnabled = false;
            PresenterModeComboBox.IsEnabled = false;
            IllustrationModeRadioButton.IsEnabled = false;
            PresenterModeRadioButton.IsEnabled = false;
            FullscreenModeRadioButton.IsEnabled = false;
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
                PageQuestionModel question = (PageQuestionModel)row.DataContext;
                (dgQuestions.Columns[6].GetCellContent(row).FindName("hbtnBindPageVariable") as HyperlinkButton).Content = args.pageVariable.Name;
                question.PageVariable = new PageVariableModel
                {
                    Description = args.pageVariable.Description,
                    Name = args.pageVariable.Name,
                    PageVariableGUID = args.pageVariable.PageVariableGUID,
                    ProgramGUID = args.pageVariable.ProgramGUID,
                };
            }

            ServiceClient serviceProxy = ServiceProxyFactory.Instance.ServiceProxy;
            serviceProxy.UpdatePageVariableLastAccessTimeAsync(args.pageVariable.PageVariableGUID);
        }

        private void PrimaryLink_Click(object sender, RoutedEventArgs e)
        {
            pageVarible.eventArgs = new PageVariableEventArgs(PrimaryButtonTextBox);
            pageVarible.Show();
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
            if (PageContentModel.PageQuestions.Count >= 5)
            {
                HtmlPage.Window.Alert("You can only add up to 5 questions on one page.");
            }
            else
            {
                PageQuestionModel question = (PageQuestionModel)((DataGridRow)((HyperlinkButton)sender).Tag).DataContext;

                PageQuestionModel model = new PageQuestionModel()
                {
                    Guid = question.Guid,
                    QuestionGuid = Guid.NewGuid(),
                    Caption = question.Caption,
                    IsRequired = question.IsRequired,
                    PageVariable = question.PageVariable,
                    Order = PageContentModel.PageQuestions.Count + 1,
                    BeginContent = question.BeginContent,
                    EndContent = question.EndContent,
                };
                PageContentModel.ObjectStatus.Add(model.QuestionGuid, ModelStatus.QuestionAdded);
                //BodyTextBox.Text += "\r{Q:" + model.QuestionGuid + "}";

                model.SubItems = new ObservableCollection<PageQuestionItemModel>();
                foreach (PageQuestionItemModel item in question.SubItems)
                {
                    PageQuestionItemModel itemModel = new PageQuestionItemModel
                    {
                        Feedback = item.Feedback,
                        Guid = Guid.NewGuid(),
                        Item = item.Item,
                        Score = item.Score,
                        Order = item.Order,
                    };
                    model.SubItems.Add(itemModel);
                }

                PageContentModel.PageQuestions.Add(model);
            }
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
                        foreach (PageQuestionModel questionModel in PageContentModel.PageQuestions)
                        {
                            if (questionModel.Order <= currentOrder &&
                                questionModel.Order > originalOrder)
                            {
                                questionModel.Order -= 1;
                                ((ComboBox)dgQuestions.Columns[0].GetCellContent(questionModel).FindName("cbOrder")).SelectedIndex = questionModel.Order - 1;
                            }
                            else if (questionModel.QuestionGuid == ((PageQuestionModel)cbOrder.Tag).QuestionGuid)
                            {
                                questionModel.Order = currentOrder;
                            }
                        }
                    }
                    else if (originalOrder > currentOrder)
                    {
                        foreach (PageQuestionModel questionModel in PageContentModel.PageQuestions)
                        {
                            if (questionModel.Order >= currentOrder &&
                                questionModel.Order < originalOrder)
                            {
                                questionModel.Order += 1;
                                ((ComboBox)dgQuestions.Columns[0].GetCellContent(questionModel).FindName("cbOrder")).SelectedIndex = questionModel.Order - 1;
                            }
                            else if (questionModel.QuestionGuid == ((PageQuestionModel)cbOrder.Tag).QuestionGuid)
                            {
                                questionModel.Order = currentOrder;
                            }
                        }
                    }

                }
                ddlFlag = true;

            }
        }

        private void cbItemOrder_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ddlFlag)
            {
                ddlFlag = false;
                if (e.AddedItems.Count > 0 && e.RemovedItems.Count > 0)
                {
                    ComboBox cbOrder = (ComboBox)sender;

                    //DataGrid dg = (DataGrid)sender;
                    int currentOrder = ((OrderObject)e.AddedItems[0]).OrderNo;
                    int originalOrder = ((OrderObject)e.RemovedItems[0]).OrderNo;
                    Guid reOrderItemGuid = Guid.Empty;
                    foreach (PageQuestionItemModel questionItemModel in currentItems)
                    {
                        if (questionItemModel.Order == originalOrder)
                        {
                            reOrderItemGuid = questionItemModel.Guid;
                            break;
                        }
                    }

                    //((PageQuestionModel)dg.DataContext).SubItems;
                    if (originalOrder < currentOrder)
                    {
                        foreach (PageQuestionItemModel questionItemModel in currentItems)
                        {
                            if (questionItemModel.Order <= currentOrder &&
                                questionItemModel.Order > originalOrder)
                            {
                                ((ComboBox)((DataGrid)cbOrder.Tag).Columns[0].GetCellContent(questionItemModel).FindName("cbItemOrder")).SelectedIndex = questionItemModel.Order - 2;
                                questionItemModel.Order -= 1;
                                //if (PageContentModel.ObjectStatus.ContainsKey(questionItemModel.Guid))
                                //{
                                //    if (PageContentModel.ObjectStatus[questionItemModel.Guid] != ModelStatus.QuestionItemAdded && PageContentModel.ObjectStatus[questionItemModel.Guid]!=ModelStatus.QuestionItemDeleted)
                                //    {
                                //        PageContentModel.ObjectStatus[questionItemModel.Guid] = ModelStatus.QuestionItemUpdated;
                                //    }
                                //}
                            }
                            else if (questionItemModel.Guid == reOrderItemGuid)
                            {
                                questionItemModel.Order = currentOrder;
                            }
                        }
                    }
                    else if (originalOrder > currentOrder)
                    {
                        foreach (PageQuestionItemModel questionItemModel in currentItems)
                        {
                            if (questionItemModel.Order >= currentOrder &&
                                questionItemModel.Order < originalOrder)
                            {
                                questionItemModel.Order += 1;
                                ((ComboBox)((DataGrid)cbOrder.Tag).Columns[0].GetCellContent(questionItemModel).FindName("cbItemOrder")).SelectedIndex = questionItemModel.Order - 1;
                            }
                            else if (questionItemModel.Guid == reOrderItemGuid)
                            {
                                questionItemModel.Order = currentOrder;
                            }
                        }
                    }
                }
                ddlFlag = true;
            }
        }

        private void FooterTextLink_Click(object sender, RoutedEventArgs e)
        {
            pageVarible.eventArgs = new PageVariableEventArgs(FooterTextBox);
            pageVarible.Show();
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

        private void InsertResourceFooterLink_Click(object sender, RoutedEventArgs e)
        {
            _isClickOnInsertResourceBodyLink = false;
            ResourceManager.Show();
        }
    }
}
