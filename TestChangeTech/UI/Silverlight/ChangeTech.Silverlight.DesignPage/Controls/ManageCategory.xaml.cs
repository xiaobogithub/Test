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
using System.Windows.Browser;

namespace ChangeTech.Silverlight.DesignPage.Controls
{
    public partial class ManageCategory : UserControl
    {
        public EventHandler CloseEvent { get; set; }

        private ObservableCollection<ResourceCategoryModel> categoriesModel;
        private Guid newCategoryGuid;
        private ResourceCategoryModel delcategory;
        public ManageCategory()
        {
            InitializeComponent();
            //TODO: Should connect server only when this control is displayed
            CategoryChanged();
        }

        private void CategoryChanged()
        {
            ServiceClient client = ServiceProxyFactory.Instance.ServiceProxy;
            client.GetResourceCategoryCompleted += new EventHandler<GetResourceCategoryCompletedEventArgs>(client_GetResourceCategoryCompleted);
            client.GetResourceCategoryAsync();
        }

        private void BindGrid()
        {
            categoryGrid.ItemsSource = categoriesModel;
        }

        private void  client_GetResourceCategoryCompleted(object sender, GetResourceCategoryCompletedEventArgs e)
        {
            categoriesModel = e.Result.Categories;
            BindGrid();
        }

        private void categoryGrid_CellEditEnded(object sender, DataGridCellEditEndedEventArgs e)
        {
            ResourceCategoryModel category = (ResourceCategoryModel)e.Row.DataContext;
            if (!string.IsNullOrEmpty(category.CategoryName))
            {
                ServiceClient client = ServiceProxyFactory.Instance.ServiceProxy;
                client.SaveResourceCategoryCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(client_SaveResourceCategoryCompleted);
                client.SaveResourceCategoryAsync(category.CategoryGuid, category.CategoryName, string.Empty);
            }
            else
            {
                HtmlPage.Window.Alert("Please input category name.");
            }
        }

        void client_SaveResourceCategoryCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            CategoryChanged();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            CloseEvent(this, new EventArgs());
            this.Visibility = Visibility.Collapsed;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            bool isThereTemporyFile = false;
            foreach (ResourceCategoryModel resourceCategory in categoriesModel)
            {
                if (resourceCategory.CategoryName.Equals("[New category]"))
                {
                    HtmlPage.Window.Alert("Please rename tempory category name [New Category] firstly.");
                    isThereTemporyFile = true;
                    break;
                }
            }
            if (!isThereTemporyFile)
            {
                newCategoryGuid = Guid.NewGuid();
                ServiceClient client = ServiceProxyFactory.Instance.ServiceProxy;
                client.InsertResourceCategoryCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(client_InsertResourceCategoryCompleted);
                client.InsertResourceCategoryAsync(newCategoryGuid, "[New category]", string.Empty);
            }
        }

        private void client_InsertResourceCategoryCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                ResourceCategoryModel tempCategory = new ResourceCategoryModel();
                tempCategory.CategoryGuid = newCategoryGuid;
                tempCategory.CategoryName = "[New category]";
                categoriesModel.Add(tempCategory);
            }
            else
            {
                HtmlPage.Window.Alert("Internal Error, please contact with ChangeTech development team.");
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            HyperlinkButton delbtn = (HyperlinkButton)sender;
            delcategory = (ResourceCategoryModel)delbtn.Tag;
            ServiceClient client = ServiceProxyFactory.Instance.ServiceProxy;
            client.DeleteResourceCategoryCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(client_DeleteResourceCategoryCompleted);
            client.DeleteResourceCategoryAsync(delcategory.CategoryGuid);
        }

        void client_DeleteResourceCategoryCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                if (categoriesModel.Contains(delcategory))
                {
                    categoriesModel.Remove(delcategory);
                }
            }
            else
            {
                HtmlPage.Window.Alert("This category has resources, you must delete them firstly.");
            }
        }

        private void categoryGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            DataGrid dg = (DataGrid)sender;
            HyperlinkButton hlb = dg.Columns[1].GetCellContent(e.Row).FindName("btnDelete") as HyperlinkButton;
            hlb.Tag = e.Row.DataContext;
        }
    }
}
