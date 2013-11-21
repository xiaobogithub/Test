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
using System.Collections.ObjectModel;
using ChangeTech.Silverlight.Common;

namespace ChangeTech.Silverlight.DesignPage.Controls
{
    public partial class ResourceManager : UserControl
    {
        public event SelectResourceDelegate SelectResourceEventHandler;
        
        private ObservableCollection<ResourceCategoryModel> _resourceCategoriesWithAll;
        private ObservableCollection<ResourceCategoryModel> _resourceCategories;
        private ObservableCollection<ResourceModel> _resources;
        private ResourceModel _deletedResource;
        private ResourceTypeEnum _resourceType;
        private bool flag = false;
        public ResourceModel resource;

        public ResourceManager()
        {
            InitializeComponent();

            _deletedResource = new ResourceModel();
            _resources = new ObservableCollection<ResourceModel>();
            _resourceCategories = new ObservableCollection<ResourceCategoryModel>();
            _resourceCategoriesWithAll = new ObservableCollection<ResourceCategoryModel>();
            UploadManager.UpdateResourceListEventHandler += new UpdateResourceListDelegate(UploadManager_UpdateResourceListEventHandler);
        }

        private void UploadManager_UpdateResourceListEventHandler(List<Guid> categoryWithChange)
        {
            ResourceCategoryModel rsm = ResourceCategoryComboBox.SelectedItem as ResourceCategoryModel;
            if (categoryWithChange.Contains(rsm.CategoryGuid) || rsm.CategoryGuid == Guid.Empty)
            {
                GetResources();
            }
        }

        public void Show()
        {
            Visibility = Visibility.Visible;
            ResourceTypeComboBox.IsEnabled = true;
            ResourceTypeComboBox.SelectedItem = null;

            DisableControls(Constants.MSG_LOADING);

            ServiceClient serviceProxy = ServiceProxyFactory.Instance.ServiceProxy;
            serviceProxy.GetResourceCategoryCompleted += new EventHandler<GetResourceCategoryCompletedEventArgs>(serviceProxy_GetResourceCategoryCompleted);
            serviceProxy.GetResourceCategoryAsync();
        }

        public void Show(ResourceTypeEnum resourceType)
        {
            Visibility = Visibility.Visible;
            _resourceType = resourceType;

            switch (_resourceType)
            {
                case ResourceTypeEnum.Image:
                    ResourceTypeComboBox.SelectedIndex = 0;
                    break;
                case ResourceTypeEnum.Document:
                    ResourceTypeComboBox.SelectedIndex = 1;
                    break;
                case ResourceTypeEnum.Video:
                    ResourceTypeComboBox.SelectedIndex = 2;
                    break;
                case ResourceTypeEnum.Audio:
                    ResourceTypeComboBox.SelectedIndex = 3;
                    break;
            }
            ResourceTypeComboBox.IsEnabled = false;

            DisableControls(Constants.MSG_LOADING);

            ServiceClient serviceProxy = ServiceProxyFactory.Instance.ServiceProxy;
            serviceProxy.GetResourceCategoryCompleted += new EventHandler<GetResourceCategoryCompletedEventArgs>(serviceProxy_GetResourceCategoryCompleted);
            serviceProxy.GetResourceCategoryAsync();

        }

        private void ResourceListDataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            flag = false;
            ResourceModel resourceModel = (ResourceModel)e.Row.DataContext;
            ComboBox resourceCategoryComboBox = ((ComboBox)ResourceListDataGrid.Columns[1].GetCellContent(e.Row).FindName("comboResourceCategory"));
            resourceCategoryComboBox.Tag = resourceModel;
            resourceCategoryComboBox.ItemsSource = _resourceCategories;
            ResourceCategoryModel rcModel = GetResourceCategory(resourceModel.ResourceCategoryGUID);
            if (rcModel.CategoryGuid == Guid.Empty)
            {
                resourceCategoryComboBox.SelectedItem = null;
            }
            else
            {
                resourceCategoryComboBox.SelectedItem = rcModel;
            }

            ((HyperlinkButton)ResourceListDataGrid.Columns[3].GetCellContent(e.Row).FindName("hbtnSelect")).Tag = resourceModel;
            ((HyperlinkButton)ResourceListDataGrid.Columns[4].GetCellContent(e.Row).FindName("hbtnDelete")).Tag = resourceModel;
            ((HyperlinkButton)ResourceListDataGrid.Columns[5].GetCellContent(e.Row).FindName("hbtnView")).Tag = resourceModel;
            flag = true;
        }

        // Edit Resource Name Event.
        private void ResourceListDataGrid_RowEditEnded(object sender, DataGridRowEditEndedEventArgs e)
        {
            if (HtmlPage.Window.Confirm("Do you really want to update resource ?"))
            {
                ResourceModel resourceModel = e.Row.DataContext as ResourceModel;
                if (!string.IsNullOrEmpty(resourceModel.Name))
                {
                    ComboBox resourceCategoryComboBox = ((ComboBox)ResourceListDataGrid.Columns[1].GetCellContent(e.Row).FindName("comboResourceCategory"));
                    resourceModel.ResourceCategoryGUID = ((ResourceCategoryModel)resourceCategoryComboBox.SelectedItem).CategoryGuid;
                    
                    DisableControls(Constants.MSG_SAVING);
                    ServiceClient sc = ServiceProxyFactory.Instance.ServiceProxy;
                    sc.UpdateResourceEntityCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(sc_UpdateResourceEntityCompleted);
                    sc.UpdateResourceEntityAsync(resourceModel);
                }
                else
                {
                    HtmlPage.Window.Alert("Resource name cannot be empty.");
                }
            }
        }

        private void ResourceCategoryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ResourceCategoryComboBox.SelectedItem != null && ResourceTypeComboBox.SelectedItem != null)
            {
                GetResources();
            }
            else
            {
                EnableControls();
            }
        }

        private void comboResourceCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (flag)
            {
                if (HtmlPage.Window.Confirm("Do you really want to update resource's category ?"))
                {
                    ComboBox resourceCategoryCbb = sender as ComboBox;
                    if (resourceCategoryCbb.SelectedItem != null)
                    {
                        ResourceModel resourceModel = (ResourceModel)resourceCategoryCbb.Tag;
                        resourceModel.ResourceCategoryGUID = ((ResourceCategoryModel)resourceCategoryCbb.SelectedItem).CategoryGuid;

                        DisableControls(Constants.MSG_SAVING);
                        ServiceClient sc = ServiceProxyFactory.Instance.ServiceProxy;
                        sc.UpdateResourceEntityCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(sc_UpdateResourceEntityCompleted);
                        sc.UpdateResourceEntityAsync(resourceModel);
                    }
                }
            }
        }

        private void ResourceListDataGrid_LayoutUpdated(object sender, EventArgs e)
        {
            //if (Visibility == Visibility.Visible)
            //{
            //    foreach (ResourceModel resourceModel in _resources)
            //    {
            //        if (ResourceListDataGrid.Columns[1].GetCellContent(resourceModel) != null)
            //        {
            //            ComboBox resourceCategoryComboBox = ((ComboBox)ResourceListDataGrid.Columns[2].GetCellContent(resourceModel).FindName("comboResourceCategory"));
            //            resourceCategoryComboBox.Tag = resourceModel;
            //            resourceCategoryComboBox.ItemsSource = _resourceCategories;
            //            ResourceCategoryModel rcModel = GetResourceCategory(resourceModel.ResourceCategoryGUID);
            //            if (rcModel.CategoryGuid == Guid.Empty)
            //            {
            //                resourceCategoryComboBox.SelectedItem = null;
            //            }
            //            else
            //            {
            //                resourceCategoryComboBox.SelectedItem = rcModel;
            //            }

            //            ((HyperlinkButton)ResourceListDataGrid.Columns[4].GetCellContent(resourceModel).FindName("hbtnDelete")).Tag = resourceModel;
            //            ((HyperlinkButton)ResourceListDataGrid.Columns[3].GetCellContent(resourceModel).FindName("hbtnSelect")).Tag = resourceModel;
            //        }
            //        //HyperlinkButton deleteHyperLinkButton = ((HyperlinkButton)dgList.Columns[4].GetCellContent(pageVariable).FindName("hbtnDelete"));

            //        //((HyperlinkButton)dgList.Columns[4].GetCellContent(pageVariable).FindName("hbtnDelete")).Tag = pageVariable;
            //        //((HyperlinkButton)dgList.Columns[5].GetCellContent(pageVariable).FindName("hbtnSelect")).Tag = pageVariable;
            //    }
            //}
        }

        private void GetResources()
        {
            DisableControls(Constants.MSG_LOADING);

            switch (ResourceTypeComboBox.SelectedIndex)
            {
                case 0:
                    _resourceType = ResourceTypeEnum.Image;
                    break;
                case 1:
                    _resourceType = ResourceTypeEnum.Document;
                    break;
                case 2:
                    _resourceType = ResourceTypeEnum.Video;
                    break;
                case 3:
                    _resourceType = ResourceTypeEnum.Audio;
                    break;
            }

            ServiceClient serviceProxy = ServiceProxyFactory.Instance.ServiceProxy;
            serviceProxy.GetResourceNameCompleted += new EventHandler<GetResourceNameCompletedEventArgs>(serviceProxy_GetResourceNameCompleted);
            serviceProxy.GetResourceNameAsync(((ResourceCategoryModel)ResourceCategoryComboBox.SelectedItem).CategoryGuid, _resourceType);
        }

        private ResourceCategoryModel GetResourceCategory(Guid resourceCategoryGuid)
        {
            ResourceCategoryModel resourceCategotyModel = new ResourceCategoryModel();
            foreach (ResourceCategoryModel rcModel in _resourceCategories)
            {
                if (rcModel.CategoryGuid == resourceCategoryGuid)
                {
                    resourceCategotyModel = rcModel;
                    break;
                }
            }
            return resourceCategotyModel;
        }

        void sc_UpdateResourceEntityCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
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
                MessageTextBlock.Text = Constants.MSG_SUCCESSFUL;
                EnableControls();
            }
        }

        private void serviceProxy_GetResourceCategoryCompleted(object sender, GetResourceCategoryCompletedEventArgs e)
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
                _resourceCategories = e.Result.Categories;
                flag = false;

                _resourceCategoriesWithAll = new ObservableCollection<ResourceCategoryModel>();
                _resourceCategoriesWithAll.Add(new ResourceCategoryModel
                {
                    CategoryGuid = Guid.Empty,
                    CategoryName = "All"
                });
                foreach (ResourceCategoryModel rm in e.Result.Categories)
                {
                    _resourceCategoriesWithAll.Add(rm);
                }
                ResourceCategoryComboBox.ItemsSource = _resourceCategoriesWithAll;
                if (e.Result.Categories.Count > 0 && e.Result.LastSelectedResourceCategory != Guid.Empty)
                {
                    foreach (ResourceCategoryModel model in e.Result.Categories)
                    {
                        if (model.CategoryGuid == e.Result.LastSelectedResourceCategory)
                        {
                            ResourceCategoryComboBox.SelectedIndex = e.Result.Categories.IndexOf(model) + 1;
                            break;
                        }
                    }
                }
                else
                {
                    ResourceCategoryComboBox.SelectedIndex = 0;
                }
                switch (e.Result.LastSelectedResourceType)
                {
                    case "Image":
                        ResourceTypeComboBox.SelectedIndex = 0;
                        break;
                    case "Document":
                        ResourceTypeComboBox.SelectedIndex = 1;
                        break;
                    case "Video":
                        ResourceTypeComboBox.SelectedIndex = 2;
                        break;
                    case "Audio":
                        ResourceTypeComboBox.SelectedIndex = 3;
                        break;
                }
                GetResources();
                //EnableControls();
                flag = true;

            }
        }

        private void serviceProxy_GetResourceNameCompleted(object sender, GetResourceNameCompletedEventArgs e)
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
                flag = false;
                _resources = e.Result.Resources;
                ResourceListDataGrid.ItemsSource = _resources;
                ResourceListDataGrid.Columns[2].IsReadOnly = true;
                EnableControls();
                flag = true;
            }
        }
        
        private void UploadButton_Click(object sender, RoutedEventArgs e)
        {
            UploadManager.Show(_resourceType, _resourceCategories);
        }

        private void EnableControls()
        {
            ResourceCategoryComboBox.IsEnabled = true;
            ResourceTypeComboBox.IsEnabled = true;
            ResourceListDataGrid.IsEnabled = true;

            MessageTextBlock.Text = string.Empty;
        }

        private void DisableControls(string message)
        {
            MessageTextBlock.Text = message;

            ResourceCategoryComboBox.IsEnabled = false;
            ResourceListDataGrid.IsEnabled = false;
            ResourceTypeComboBox.IsEnabled = false;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Collapsed;
        }

        //Select Resource
        private void hbtnSelectItem_Click(object sender, RoutedEventArgs e)
        {
            if (SelectResourceEventHandler != null)
            {
                SelectResourceEventHandler(this,
                    new SelectResourceEventArgs((ResourceModel)(((HyperlinkButton)sender).Tag)));

                Visibility = Visibility.Collapsed;
            }
        }

        //View Resource
        private void hbtnView_Click(object sender, RoutedEventArgs e)
        {
            ResourceModel resourceModelView = (ResourceModel)((HyperlinkButton)sender).Tag;
            ResourceView.Show(resourceModelView);
        }

        //Delete Resource
        private void hbtnDeleteItem_Click(object sender, RoutedEventArgs e)
        {
            bool confirmResult = HtmlPage.Window.Confirm("Do you want to delete this resource?");
            if (confirmResult)
            {
                DisableControls(Constants.MSG_DELETING);
                _deletedResource = (ResourceModel)((HyperlinkButton)sender).Tag;
                ServiceClient serviceProxy = ServiceProxyFactory.Instance.ServiceProxy;
                serviceProxy.DeleteNotUsedResourceCompleted += new EventHandler<DeleteNotUsedResourceCompletedEventArgs>(serviceProxy_DeleteNotUsedResourceCompleted);
                serviceProxy.DeleteNotUsedResourceAsync(_deletedResource.ID);
            }
        }

        void serviceProxy_DeleteNotUsedResourceCompleted(object sender, DeleteNotUsedResourceCompletedEventArgs e)
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
                EnableControls();
                if (e.Result)
                {
                    _resources.Remove(_deletedResource);
                    MessageTextBlock.Text = Constants.MSG_DELETED;
                }
                else
                {
                    MessageTextBlock.Text = Constants.MSG_FAILED;
                }
            }
        }

        #region Drag & Drop
        bool isMouseCaptured;
        Point mousePosition;
        bool clickOnDataGridColumn;
        private void RenameCategoryPopupPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!clickOnDataGridColumn &&
                (UploadManager.Visibility == Visibility.Collapsed && ResourceView.Visibility == Visibility.Collapsed))
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
