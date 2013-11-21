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

namespace ChangeTech.Silverlight.DesignPage.Controls
{
    public partial class ManagePageVariableGroup : UserControl
    {
        private EditPageVariableGroupModel PageVariableGroupModelInstance { get; set; }
        public delegate void AfterManageGroupHander(object sender, EventArgs args);
        public event AfterManageGroupHander AfterManageGroup;
        public ManagePageVariableGroup()
        {
            InitializeComponent();
            PageVariableGroupModelInstance = new EditPageVariableGroupModel();
            PageVariableGroupModelInstance.VariableGroupModels = new ObservableCollection<PageVariableGroupModel>();
            PageVariableGroupModelInstance.ObjectStatus = new Dictionary<Guid, ModelStatus>();
        }

        public void Show(Guid programGuid)
        {
            Visibility = Visibility.Visible;
            ServiceClient client = ServiceProxyFactory.Instance.ServiceProxy;
            client.GetPageVariableGroupForProgramCompleted += new EventHandler<GetPageVariableGroupForProgramCompletedEventArgs>(client_GetPageVariableGroupForProgramCompleted);
            client.GetPageVariableGroupForProgramAsync(programGuid);
        }

        void client_GetPageVariableGroupForProgramCompleted(object sender, GetPageVariableGroupForProgramCompletedEventArgs e)
        {
            PageVariableGroupModelInstance = e.Result;
            variableGroupGrid.ItemsSource = PageVariableGroupModelInstance.VariableGroupModels;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            ServiceClient client = ServiceProxyFactory.Instance.ServiceProxy;
            client.SavePageVariableGroupCompleted+=new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(client_SavePageVariableGroupCompleted);
            client.SavePageVariableGroupAsync(PageVariableGroupModelInstance);
        }

        void client_SavePageVariableGroupCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {            
            Visibility = Visibility.Collapsed;
            if (AfterManageGroup != null)
            {
                AfterManageGroup(sender, e);
            }
        }
       
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            PageVariableGroupModel newGroupModel = new PageVariableGroupModel
            {
                PageVariableGroupGUID = Guid.NewGuid(),
                ProgramGUID = PageVariableGroupModelInstance.ProgramGUID,           
            };
            PageVariableGroupModelInstance.VariableGroupModels.Add(newGroupModel);
            PageVariableGroupModelInstance.ObjectStatus.Add(newGroupModel.PageVariableGroupGUID, ModelStatus.ModelAdd);
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            PageVariableGroupModel deleGroup = (sender as HyperlinkButton).Tag as PageVariableGroupModel;
            PageVariableGroupModelInstance.VariableGroupModels.Remove(deleGroup);
            if (PageVariableGroupModelInstance.ObjectStatus.ContainsKey(deleGroup.PageVariableGroupGUID))
            {
                if (PageVariableGroupModelInstance.ObjectStatus[deleGroup.PageVariableGroupGUID] == ModelStatus.ModelAdd)
                {
                    PageVariableGroupModelInstance.ObjectStatus.Remove(deleGroup.PageVariableGroupGUID);
                }
                else
                {
                    PageVariableGroupModelInstance.ObjectStatus[deleGroup.PageVariableGroupGUID] = ModelStatus.ModelDelete;
                }
            }
            else
            {
                PageVariableGroupModelInstance.ObjectStatus.Add(deleGroup.PageVariableGroupGUID, ModelStatus.ModelDelete);
            }
        }   

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Collapsed;
            if (AfterManageGroup != null)
            {
                AfterManageGroup(sender, e);
            }
        }

        private void variableGroupGrid_CellEditEnded(object sender, DataGridCellEditEndedEventArgs e)
        {
            PageVariableGroupModel group = e.Row.DataContext as PageVariableGroupModel;
            if (PageVariableGroupModelInstance.ObjectStatus.ContainsKey(group.PageVariableGroupGUID))
            {
                if (PageVariableGroupModelInstance.ObjectStatus[group.PageVariableGroupGUID] != ModelStatus.ModelAdd)
                {
                    PageVariableGroupModelInstance.ObjectStatus[group.PageVariableGroupGUID] = ModelStatus.ModelEdit;
                }
            }
            else
            {
                PageVariableGroupModelInstance.ObjectStatus.Add(group.PageVariableGroupGUID, ModelStatus.ModelEdit);
            }
        }

        private void variableGroupGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            DataGrid dg = sender as DataGrid;
            HyperlinkButton deleButton = dg.Columns[2].GetCellContent(e.Row).FindName("btnDelete") as HyperlinkButton;
            deleButton.Tag = e.Row.DataContext;
        }
    }
}
