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

namespace ChangeTech.Silverlight.DesignPage.Controls
{
    public partial class PageVariableManager : UserControl
    {
        public ObservableCollection<EditPageVariableModel> list;
        Guid ProgramGuid;
        public EditPageVariableModel lastSelectedPageVariable;
        public delegate void SelectedDelegate(object sender, PageVariableEventArgs args);
        public event SelectedDelegate SelectedEvent;
        public PageVariableEventArgs eventArgs;
        ObservableCollection<PageVariableGroupModel> groupList;
        ObservableCollection<PageVariableGroupModel> groupListWithAll;
        Guid deletePageVariableGuid;
        bool ddlflag = true;
        private int pageSize = 9;

        public PageVariableManager()
        {
            //ProgramGuid = ProgramGUID;
            InitializeComponent();
            lastSelectedPageVariable = new EditPageVariableModel();
            list = new ObservableCollection<EditPageVariableModel>();
            groupList = new ObservableCollection<PageVariableGroupModel>();
            groupListWithAll = new ObservableCollection<PageVariableGroupModel>();
            eventArgs = new PageVariableEventArgs();
        }

        private void Enable()
        {
            comboPageVariableType.IsEnabled = true;
            comboPageVariableGroup.IsEnabled = true;
            dgList.IsEnabled = true;
            if (comboPageVariableGroup.SelectedIndex == 0)
            {
                btnAdd.IsEnabled = true;
            }
            ManageGroupLink.IsEnabled = true;
            promptTextBlock.Text = string.Empty;
            PageNoComboBox.IsEnabled = true;
        }

        private void Disable(string msg)
        {
            promptTextBlock.Text = msg;
            comboPageVariableGroup.IsEnabled = false;
            comboPageVariableType.IsEnabled = false;
            btnAdd.IsEnabled = false;
            dgList.IsEnabled = false;
            ManageGroupLink.IsEnabled = false;
            PageNoComboBox.IsEnabled = false;
        }

        public void Show()
        {
            Visibility = Visibility.Visible;

            Disable(Constants.MSG_LOADING);

            if (IsEditPagesequenceOnly())
            {
                ProgramGuid = new Guid(StringUtility.GetQueryString(Constants.QUERYSTR_PROGRAM_GUID));
                ServiceClient sc = ServiceProxyFactory.Instance.ServiceProxy;
                sc.GetPageVariableGroupForProgramCompleted += new EventHandler<GetPageVariableGroupForProgramCompletedEventArgs>(sc_GetPageVariableGroupForProgramCompleted);
                sc.GetPageVariableGroupForProgramAsync(ProgramGuid);
            }
            else
            {
                ServiceClient sc = ServiceProxyFactory.Instance.ServiceProxy;
                sc.GetSessionCompleted += new EventHandler<GetSessionCompletedEventArgs>(sc_GetSessionCompleted);
                sc.GetSessionAsync(new Guid(StringUtility.GetQueryString(Constants.QUERYSTR_SESSION_GUID)));
            }
        }

        private void sc_GetSessionCompleted(object sender, GetSessionCompletedEventArgs e)
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
                ProgramGuid = ((EditSessionModel)e.Result).ProgramGuid;

                if (groupList.Count == 0)
                {
                    ServiceClient sc = ServiceProxyFactory.Instance.ServiceProxy;
                    sc.GetPageVariableGroupForProgramCompleted += new EventHandler<GetPageVariableGroupForProgramCompletedEventArgs>(sc_GetPageVariableGroupForProgramCompleted);
                    sc.GetPageVariableGroupForProgramAsync(ProgramGuid);
                }
                else
                {
                    Enable();
                }
            }
        }

        private void comboPageVariableType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dgList.ItemsSource = null;
            if (comboPageVariableType.SelectedIndex == 0)
            {
                btnAdd.IsEnabled = true;
                comboPageVariableGroup.ItemsSource = null;
                comboPageVariableGroup.ItemsSource = groupListWithAll;
            }
            else
            {
                btnAdd.IsEnabled = false;
                comboPageVariableGroup.ItemsSource = null;
            }

            if (ddlflag)
            {
                GetCountOfVariables();
            }
        }

        private void GetCountOfVariables()
        {
            Disable(Constants.MSG_LOADING);
            Guid groupGuid = Guid.Empty;
            string pageVariableType = string.Empty;
            if (comboPageVariableGroup.SelectedItem != null)
            {
                groupGuid = ((PageVariableGroupModel)comboPageVariableGroup.SelectedItem).PageVariableGroupGUID;
            }
            ServiceClient sc = ServiceProxyFactory.Instance.ServiceProxy;
            sc.GetCountOfVariableByProgramCompleted += new EventHandler<GetCountOfVariableByProgramCompletedEventArgs>(sc_GetCountOfVariableByProgramCompleted);
            if (comboPageVariableType.SelectedIndex == 1)
            {
                sc.GetCountOfVariableByProgramAsync(ProgramGuid, VariableTypeEnum.General, groupGuid);
            }
            else
            {
                sc.GetCountOfVariableByProgramAsync(ProgramGuid, VariableTypeEnum.Program, groupGuid);
            }

        }

        private void sc_GetPageVariableByProgramCompleted(object sender, GetPageVariableByProgramCompletedEventArgs e)
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
                list = e.Result;
                ddlflag = false;
                dgList.ItemsSource = null;
                dgList.ItemsSource = list;
                ddlflag = true;
                Enable();
            }
        }

        private void sc_GetPageVariableGroupForProgramCompleted(object sender, GetPageVariableGroupForProgramCompletedEventArgs e)
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
                lastSelectedPageVariable = e.Result.LastSelectedPageVariable;
                ddlflag = false;
                groupList = e.Result.VariableGroupModels;
                groupListWithAll.Clear();

                groupListWithAll.Add(new PageVariableGroupModel()
                {
                    Name = "All",
                    PageVariableGroupGUID = Guid.Empty
                });

                foreach (PageVariableGroupModel pageVariableGroup in e.Result.VariableGroupModels)
                {
                    groupListWithAll.Add(pageVariableGroup);
                }

                comboPageVariableGroup.ItemsSource = null;
                comboPageVariableGroup.ItemsSource = groupListWithAll;
                ddlflag = true;



                if (comboPageVariableType.SelectedItem == null)
                {
                    if (lastSelectedPageVariable != null)
                    {
                        VariableTypeEnum VariableType;
                        Enum.TryParse(lastSelectedPageVariable.PageVariableType, out VariableType);
                        switch (VariableType)
                        {
                            case VariableTypeEnum.Program:
                                comboPageVariableType.SelectedIndex = 0;
                                break;
                            case VariableTypeEnum.General:
                                comboPageVariableType.SelectedIndex = 1;
                                break;
                        }
                    }
                    else
                    {
                        comboPageVariableType.SelectedIndex = 0;
                    }
                }

                if (comboPageVariableGroup.SelectedItem == null)
                {
                    if (lastSelectedPageVariable != null && lastSelectedPageVariable.PageVariableGroupGUID != null)
                    {
                        for (int index = 0; index < comboPageVariableGroup.Items.Count; index++)
                        {
                            if (((PageVariableGroupModel)comboPageVariableGroup.Items[index]).PageVariableGroupGUID == lastSelectedPageVariable.PageVariableGroupGUID)
                            {
                                comboPageVariableGroup.SelectedIndex = index;
                                break;
                            }
                        }
                    }
                    else
                        comboPageVariableGroup.SelectedIndex = 0;
                }

                Enable();
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

        private void CallGetPageVariableMethod()
        {
            Disable(Constants.MSG_LOADING);
            Guid groupGuid = Guid.Empty;
            string pageVariableType = string.Empty;
            if (comboPageVariableGroup.SelectedItem != null)
            {
                groupGuid = ((PageVariableGroupModel)comboPageVariableGroup.SelectedItem).PageVariableGroupGUID;
            }

            ServiceClient sc = ServiceProxyFactory.Instance.ServiceProxy;
            sc.GetPageVariableByProgramCompleted += new EventHandler<GetPageVariableByProgramCompletedEventArgs>(sc_GetPageVariableByProgramCompleted);
            if (comboPageVariableType.SelectedIndex == 1)
            {
                sc.GetPageVariableByProgramAsync(ProgramGuid, VariableTypeEnum.General, new Guid(StringUtility.GetQueryString(Constants.QUERYSTR_USER_GUID)), groupGuid, pageSize, Convert.ToInt32(PageNoComboBox.SelectedItem.ToString()));
                dgList.Columns[6].Visibility = Visibility.Collapsed;
                dgList.Columns[2].Visibility = Visibility.Collapsed;
                dgList.IsReadOnly = true;
            }
            else
            {
                sc.GetPageVariableByProgramAsync(ProgramGuid, VariableTypeEnum.Program, new Guid(StringUtility.GetQueryString(Constants.QUERYSTR_USER_GUID)), groupGuid, pageSize, Convert.ToInt32(PageNoComboBox.SelectedItem.ToString()));
                dgList.Columns[2].Visibility = Visibility.Visible;
                dgList.Columns[6].Visibility = Visibility.Visible;
                dgList.IsReadOnly = false;
                dgList.Columns[4].IsReadOnly = true; //Page variable used times don't modify.
            }
        }

        void sc_GetCountOfVariableByProgramCompleted(object sender, GetCountOfVariableByProgramCompletedEventArgs e)
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
                PageNoComboBox.Items.Clear();
                for (int index = 1; index <= e.Result / pageSize + 1; index++)
                {
                    PageNoComboBox.Items.Add(index);
                }
                PageNoComboBox.SelectedIndex = 0;

                Enable();
                //CallGetPageVariableMethod();
            }
        }


        private void hbtnDelete_Click(object sender, RoutedEventArgs e)
        {
            EditPageVariableModel pageVariable = ((HyperlinkButton)sender).Tag as EditPageVariableModel;
            deletePageVariableGuid = pageVariable.PageVariableGUID;

            Disable(Constants.MSG_DELETING);
            ServiceClient sc = ServiceProxyFactory.Instance.ServiceProxy;
            sc.BeforeDeletePageVariableCompleted += new EventHandler<BeforeDeletePageVariableCompletedEventArgs>(sc_BeforeDeletePageVariableCompleted);
            sc.BeforeDeletePageVariableAsync(pageVariable.PageVariableGUID);
        }

        private void sc_BeforeDeletePageVariableCompleted(object sender, BeforeDeletePageVariableCompletedEventArgs e)
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
                switch (e.Result)
                {
                    case 1:
                        HtmlPage.Window.Alert("The program is pulished, you can't delete page variable.");
                        Enable();
                        break;
                    case 2:
                        if (HtmlPage.Window.Confirm("The page variable has been used, do you want to delete it?"))
                        {
                            ServiceClient sc = ServiceProxyFactory.Instance.ServiceProxy;
                            sc.DeletePageVariableCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(sc_DeletePageVariableCompleted);
                            sc.DeletePageVariableAsync(deletePageVariableGuid);
                        }
                        else
                        {
                            Enable();
                        }
                        break;
                    default:
                        CallGetPageVariableMethod();
                        break;
                }
            }
        }

        private void sc_DeletePageVariableCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
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
                CallGetPageVariableMethod();
            }
        }

        private void dgList_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            //ddlflag = false;
            //ComboBox GroupComoBox = ((ComboBox)dgList.Columns[2].GetCellContent(e.Row).FindName("comboGroup"));
            //GroupComoBox.Tag = e.Row.DataContext;
            //GroupComoBox.ItemsSource = groupList;
            //GroupComoBox.SelectedItem = GetGroup(((EditPageVariable)e.Row.DataContext).PageVariableGroupGUID);

            //((HyperlinkButton)dgList.Columns[4].GetCellContent(e.Row).FindName("hbtnDelete")).Tag = e.Row.DataContext;
            //((HyperlinkButton)dgList.Columns[5].GetCellContent(e.Row).FindName("hbtnSelect")).Tag = e.Row.DataContext;
            //ddlflag = true;
        }

        // Save PageVariableList.
        private void dgList_LayoutUpdated(object sender, EventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                ddlflag = false;
                foreach (EditPageVariableModel pageVariable in list)
                {
                    if (dgList.Columns[2].GetCellContent(pageVariable) != null)
                    {
                        ComboBox GroupComoBox = ((ComboBox)dgList.Columns[2].GetCellContent(pageVariable).FindName("comboGroup"));
                        GroupComoBox.Tag = pageVariable;
                        GroupComoBox.ItemsSource = groupList;
                        PageVariableGroupModel pg = GetGroup(pageVariable.PageVariableGroupGUID);
                        if (pg.PageVariableGroupGUID == Guid.Empty)
                        {
                            GroupComoBox.SelectedItem = null;
                        }
                        else
                        {
                            GroupComoBox.SelectedItem = pg;
                        }

                        ((HyperlinkButton)dgList.Columns[6].GetCellContent(pageVariable).FindName("hbtnDelete")).Tag = pageVariable;
                        ((HyperlinkButton)dgList.Columns[5].GetCellContent(pageVariable).FindName("hbtnSelect")).Tag = pageVariable;
                    }
                    //HyperlinkButton deleteHyperLinkButton = ((HyperlinkButton)dgList.Columns[4].GetCellContent(pageVariable).FindName("hbtnDelete"));

                    //((HyperlinkButton)dgList.Columns[4].GetCellContent(pageVariable).FindName("hbtnDelete")).Tag = pageVariable;
                    //((HyperlinkButton)dgList.Columns[5].GetCellContent(pageVariable).FindName("hbtnSelect")).Tag = pageVariable;
                }
                ddlflag = true;
            }
        }

        private PageVariableGroupModel GetGroup(Guid pageVariableGroupGuid)
        {
            PageVariableGroupModel result = new PageVariableGroupModel();
            foreach (PageVariableGroupModel variableGroup in groupList)
            {
                if (variableGroup.PageVariableGroupGUID == pageVariableGroupGuid)
                {
                    result = variableGroup;
                    break;
                }
            }
            return result;
        }

        private void dgList_RowEditEnded(object sender, DataGridRowEditEndedEventArgs e)
        {
            if (HtmlPage.Window.Confirm("Do you want to update pageVariableName ?"))
            {
                EditPageVariableModel pageVariable = e.Row.DataContext as EditPageVariableModel;

                if (!string.IsNullOrEmpty(pageVariable.Name))
                {
                    Disable(Constants.MSG_SAVING);
                    if (pageVariable.modelStatus == ModelStatus.ModelNoChange)
                    {
                        pageVariable.modelStatus = ModelStatus.ModelEdit;
                    }

                    ServiceClient sc = ServiceProxyFactory.Instance.ServiceProxy;
                    sc.SavePageVariableCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(sc_SavePageVariableCompleted);
                    sc.SavePageVariableAsync(pageVariable);
                    pageVariable.modelStatus = ModelStatus.ModelNoChange;
                }
                else
                {
                    HtmlPage.Window.Alert("Page variable name cannot be empty.");
                }
            }
            else
            {
                //not update page variable name
            }
            
        }

        private void sc_SavePageVariableCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
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
                Enable();
                promptTextBlock.Text = Constants.MSG_SUCCESSFUL;
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            EditPageVariableModel pv = new EditPageVariableModel();
            pv.ProgramGUID = ProgramGuid;
            pv.PageVariableGUID = Guid.NewGuid();
            pv.modelStatus = ModelStatus.ModelAdd;
            pv.PageVariableType = VariableTypeEnum.Program.ToString();
            pv.PageVariableGroupGUID = Guid.Empty;
            list.Add(pv);
        }

        private void hbtnSelect_Click(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Collapsed;
            eventArgs.pageVariable = (EditPageVariableModel)((HyperlinkButton)sender).Tag;
            if (SelectedEvent != null)
            {
                Visibility = Visibility.Collapsed;
                SelectedEvent(sender, eventArgs);
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Collapsed;
        }

        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            ManageVariableGroup.Show(ProgramGuid);
        }

        private void comboPageVariableGroup_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ddlflag)
            {
                GetCountOfVariables();
            }
        }

        private void AfterManageGroup(object sender, EventArgs e)
        {
            Disable(Constants.MSG_LOADING);

            ServiceClient sc = ServiceProxyFactory.Instance.ServiceProxy;
            sc.GetPageVariableGroupForProgramCompleted += new EventHandler<GetPageVariableGroupForProgramCompletedEventArgs>(sc_GetPageVariableGroupForProgramCompleted);
            sc.GetPageVariableGroupForProgramAsync(ProgramGuid);
        }

        private void comboGroup_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ddlflag)
            {
                ComboBox combo = sender as ComboBox;
                if (combo.SelectedItem != null)
                {
                    EditPageVariableModel editPageVariable = (EditPageVariableModel)combo.Tag;
                    editPageVariable.PageVariableGroupGUID = ((PageVariableGroupModel)combo.SelectedItem).PageVariableGroupGUID;
                    if (editPageVariable.modelStatus == ModelStatus.ModelNoChange)
                    {
                        Disable(Constants.MSG_SAVING);

                        editPageVariable.modelStatus = ModelStatus.ModelEdit;
                        ServiceClient sc = ServiceProxyFactory.Instance.ServiceProxy;
                        sc.SavePageVariableCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(sc_SavePageVariableCompleted);
                        sc.SavePageVariableAsync(editPageVariable);
                    }
                }
            }
        }

        private void PageNoComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PageNoComboBox.Items.Count > 0)
            {
                CallGetPageVariableMethod();
            }
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
