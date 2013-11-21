using System;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;
using System.Collections.Generic;
using ChangeTech.Silverlight.DesignPage.ChangeTechWCFService;
using ChangeTech.Silverlight.Common;
using System.Windows.Browser;

namespace ChangeTech.Silverlight.DesignPage.Controls
{
    public partial class ThumbView : UserControl
    {
        public Guid CategoryGuid { get; set; }
        public event SelectPictureDelegate OnSelectImage;
        public event EventHandler AfterLoadReourceCompleted;
        public event DoubleClickDelegate OnDoubleClickEvent;
        //public EventHandler OnSelectEvent { get; set; }
        //public event DisplayPictureInfoDelegate DisplayPictureInfoEventHandler;
        public ResourceModel LastSelectedResource { get; set; }

        private ThumbImage _image;
        private int _currentPage = 1;
        private ServiceClient _client = null;
        private ObservableCollection<ResourceModel> _imagesModel = null;
        public ObservableCollection<ResourceModel> Images
        {
            get
            {
                return _imagesModel;
            }
        }

        private bool showOnlySharedCategories = false;
        public bool ShowOnlySharedCategories
        {
            set
            {
                showOnlySharedCategories = value;
            }
        }

        private bool _isDataBinding;

        public ThumbView()
        {
            // Required to initialize variables
            InitializeComponent();

            LayoutRoot.Visibility = Visibility.Collapsed;
            _client = ServiceProxyFactory.Instance.ServiceProxy;
            _client.GetResourceNameCompleted += new EventHandler<GetResourceNameCompletedEventArgs>(_client_GetResourceNameCompleted);
        }

        void _client_GetResourceNameCompleted(object sender, GetResourceNameCompletedEventArgs e)
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
                _imagesModel = e.Result.Resources;
                InitPage(_imagesModel.Count);
                if (LastSelectedResource == null)
                {
                    if (e.Result.LastSelectedResource != null)
                    {
                        foreach (ResourceModel rm in _imagesModel)
                        {
                            if (rm.ID == e.Result.LastSelectedResource.ID)
                            {
                                LastSelectedResource = rm;
                                break;
                            }
                        }
                        _currentPage = GetPageNO(_imagesModel.IndexOf(LastSelectedResource));
                    }
                    else
                    {
                        _currentPage = 1;
                    }
                }
                else
                {
                    foreach (ResourceModel rm in _imagesModel)
                    {
                        if (rm.ID == LastSelectedResource.ID)
                        {
                            LastSelectedResource = rm;
                            break;
                        }
                    }
                    _currentPage = GetPageNO(_imagesModel.IndexOf(LastSelectedResource));
                }
                if (_imagesModel.Count > 0)
                {
                    PageNoComboBox.SelectedIndex = _currentPage - 1;
                }
                RefreshImageViewer();
            }
        }

        public void LoadPhotos()
        {
            _isDataBinding = false;
            DoCategoryIDChanged();
        }

        private void DoCategoryIDChanged()
        {
            if (_client != null)
            {
                _client.GetResourceNameAsync(CategoryGuid, ResourceTypeEnum.Image);
            }
        }

        private bool InCurrentPage(Grid grid, int index, int currentPage)
        {
            int imageCountPerPage = grid.ColumnDefinitions.Count * grid.RowDefinitions.Count;
            int minNumber = (currentPage - 1) * imageCountPerPage;
            int maxNumber = currentPage * imageCountPerPage - 1;
            return ((index >= minNumber) && (index <= maxNumber));
        }

        private int GetPageNO(int index)
        {
            Grid gridContainer = GetDestContainer();
            int imageCountPerPage = gridContainer.ColumnDefinitions.Count * gridContainer.RowDefinitions.Count;
            return index / imageCountPerPage + 1;
        }

        private void RefreshImageViewer()
        {
            if (_imagesModel != null)
            {
                Grid gridContainer = GetDestContainer();
                LayoutRoot.Visibility = Visibility.Collapsed;

                gridContainer.Children.Clear();
                //int firstIndex = 0;
                foreach (ResourceModel photograph in _imagesModel)
                {
                    int index = _imagesModel.IndexOf(photograph);
                    if (!InCurrentPage(gridContainer, index, _currentPage))
                    { continue; }

                    ThumbImage image = new ThumbImage();
                    image.ShowOnlySharedCategories = showOnlySharedCategories;
                    InitImage(image, photograph, index);
                    gridContainer.Children.Add(image);

                    //if (_image != null)
                    //{
                    //    LastSelectedResource = _image.Tag as ResourceModel;
                    //}

                    if (LastSelectedResource == null)
                    {
                        LastSelectedResource = photograph;
                    }
                    else if (photograph.ID == LastSelectedResource.ID)
                    {
                        _image = image;
                        image.SetActiveBorder();
                    }
                }
                if (LastSelectedResource != null && OnSelectImage != null)
                {
                    OnSelectImage(LastSelectedResource);
                }
                if (gridContainer.Children.Count > 0)
                {
                    LayoutRoot.Visibility = Visibility.Visible;
                    LayoutRoot.UpdateLayout();
                }
            }

            if (AfterLoadReourceCompleted != null)
            {
                AfterLoadReourceCompleted(this, null);
            }
        }

        private int GetPageCount()
        {
            Grid gridContainer = GetDestContainer();
            return Convert.ToInt32(Math.Floor(_imagesModel.Count / (gridContainer.ColumnDefinitions.Count * gridContainer.RowDefinitions.Count)));
        }

        private void InitPage(int count)
        {
            _isDataBinding = true;

            Grid gridContainer = GetDestContainer();

            int pageCount = GetPageCount();
            if (count % (gridContainer.ColumnDefinitions.Count * gridContainer.RowDefinitions.Count) != 0)
                pageCount++;
            PageNoComboBox.Items.Clear();
            for (int index = 0; index < pageCount; index++)
            {
                PageNoComboBox.Items.Add(index + 1);
            }

            //if (PageNoComboBox.Items.Count > 0)
            //{
            //    PageNoComboBox.SelectedIndex = 0;
            //}

            _isDataBinding = false;
        }

        private Grid GetDestPager()
        {
            return PageGrid;
        }

        private Grid GetDestContainer()
        {
            return ImageGrid;
        }

        private void InitImage(ThumbImage image, ResourceModel imageModel, int index)
        {
            Grid grid = GetDestContainer();
            int imageCountPerPage = grid.ColumnDefinitions.Count * grid.RowDefinitions.Count;
            index = index - (_currentPage - 1) * imageCountPerPage;
            image.MouseLeftButtonDown += new MouseButtonEventHandler(Image_MouseLeftButtonDown);
            //image.OnDeleteImageCompleted = new EventHandler(DeleteImageCompleted);
            //image.OnSelectImage = OnSelectImage;
            //image.OnSlectEvent = OnSelectEvent;
            //image.DisplayPictureInfoEventHandler += new DisplayPictureInfoDelegate(image_DisplayPictureInfoEventHandler);
            image.SetValue(Grid.ColumnProperty, Convert.ToInt32(index % grid.ColumnDefinitions.Count));
            image.SetValue(Grid.RowProperty, Convert.ToInt32(index / grid.ColumnDefinitions.Count));
            image.Tag = imageModel;
            image.ShowImage();
        }

        public void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (OnSelectImage != null)
            {
                ThumbImage selectImage = (ThumbImage)sender;
                if (((ResourceModel)selectImage.Tag).ID == (LastSelectedResource.ID))
                {
                    OnDoubleClickEvent(LastSelectedResource);
                }
                else
                {
                    OnSelectImage((ResourceModel)selectImage.Tag);                   
                    if (_image != null)
                    {
                        _image.SetNormalBorder();
                    }

                    _image = selectImage;
                    _image = (ThumbImage)sender;
                    _image.SetActiveBorder();

                    LastSelectedResource = (ResourceModel)selectImage.Tag;
                }                
            }
        }

        public void RefreshAfterClosePreviewImage(int currentIndex, int deleteCount)
        {
            Grid gridContainer = GetDestContainer();
            int pageImageCount = gridContainer.ColumnDefinitions.Count * gridContainer.RowDefinitions.Count;
            _currentPage = Convert.ToInt32(Math.Floor((currentIndex + 1) / pageImageCount));
            if ((currentIndex + 1) % pageImageCount != 0)
                _currentPage++;
            RefreshImageViewer();
            //if (deleteCount == 0)
            //{
            //    RefreshImageViewer();
            //}
            //else
            //{
            //    DoCategoryIDChanged();
            //}
        }

        public void RefreshAfterDeleteImage(ResourceModel image)
        {
            int currentIndex = _imagesModel.IndexOf(image);
            Grid gridContainer = GetDestContainer();
            int pageImageCount = gridContainer.ColumnDefinitions.Count * gridContainer.RowDefinitions.Count;
            if ((currentIndex == _imagesModel.Count - 1) && (currentIndex % pageImageCount == 0))
            {
                _currentPage--;
                LastSelectedResource = _imagesModel[currentIndex - 1];
            }
            else if (_imagesModel.Count == 1)
            {
                LastSelectedResource = _imagesModel[currentIndex];
            }
            else
            {
                LastSelectedResource = _imagesModel[currentIndex + 1];
            }
            //RefreshImageViewer();
            //LastSelectedResource = _imagesModel[currentIndex + 1];
            _client.GetResourceNameAsync(CategoryGuid, ResourceTypeEnum.Image);
        }
        
        private void PageNoComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!_isDataBinding)
            {
                _currentPage = Convert.ToInt32(((ComboBox)sender).SelectedItem.ToString());
                RefreshImageViewer();
            }
        }

        private void LastPageLink_Click(object sender, RoutedEventArgs e)
        {
            if (_currentPage > 1)
            {
                _currentPage = _currentPage - 1;
                PageNoComboBox.SelectedIndex = _currentPage - 1;
                //RefreshImageViewer();
            }
            else
            {
                HtmlPage.Window.Alert("This is the first page.");
            }
        }

        private void NextPageLink_Click(object sender, RoutedEventArgs e)
        {
            if (_currentPage < GetPageCount()+1)
            {
                _currentPage++;
                PageNoComboBox.SelectedIndex = _currentPage - 1;
            }
            else
            {
                HtmlPage.Window.Alert("This is the last page.");
            }
        }
    }
}
