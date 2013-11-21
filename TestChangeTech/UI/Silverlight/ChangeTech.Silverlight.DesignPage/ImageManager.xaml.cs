using System;
using System.Windows;
using System.Windows.Browser;
using System.Windows.Controls;
using System.Windows.Navigation;
using ChangeTech.Silverlight.Common;
using ChangeTech.Silverlight.DesignPage.ChangeTechWCFService;

namespace ChangeTech.Silverlight.DesignPage
{
    public partial class ImageManager : Page
    {
        //private ImageType _currentSetImageType;
        private string _type;
        private Guid _imageGuid;

        public ImageManager()
        {
            InitializeComponent();
        }

        public ImageManager(string type)
        {
            _type = type;
            InitializeComponent();
        }

        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ImageList.OnSelectImage += new SelectPictureDelegate(ImageList_OnSelectImage);
            ImageList.CloseButton.Visibility = Visibility.Collapsed;
            ImageList.Show();
        }

        private void ImageList_OnSelectImage(ChangeTechWCFService.ResourceModel image)
        {
            _imageGuid = image.ID;
            ServiceClient serviceProxy = ServiceProxyFactory.Instance.ServiceProxy;
            switch (_type)
            {
                case "PagePresenter":
                    serviceProxy.IsPageHasMoreReferenceCompleted += new EventHandler<IsPageHasMoreReferenceCompletedEventArgs>(serviceProxy_IsPageHasMoreReferenceCompleted);
                    serviceProxy.IsPageHasMoreReferenceAsync(new Guid(StringUtility.GetQueryString(Constants.QUERYSTR_SESSION_GUID)), new Guid(StringUtility.GetQueryString(Constants.QUERYSTR_PAGE_SEQUENCE_GUID)));
                    break;
                case "CTPPPresenter":
                    serviceProxy.SetCTPPPresenterImageCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(serviceProxy_SetCTPPPresenterImageCompleted);
                    serviceProxy.SetCTPPPresenterImageAsync(_imageGuid, new Guid(StringUtility.GetQueryString(Constants.QUERYSTR_PROGRAM_GUID)));
                    break;
            }
        }

        private void serviceProxy_IsPageHasMoreReferenceCompleted(object sender, IsPageHasMoreReferenceCompletedEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.Cancelled)
            {
                HtmlPage.Window.Alert("This operation is cancelled");
            }
            else if (e.Error != null)
            {
                HtmlPage.Window.Alert(Constants.ERROR_INTERNAL);
            }
            else
            {
                ServiceClient serviceProxy = ServiceProxyFactory.Instance.ServiceProxy;
                PageUpdateForPageReviewModel pageUpdateForPageReviewModel = new PageUpdateForPageReviewModel();

                if (e.Result)
                {
                    if (HtmlPage.Window.Confirm("Do you want to imfact other session which used this pageSquence?\n if 'yes' press 'ok'\n if 'no' press 'cancel'"))
                    {
                        pageUpdateForPageReviewModel.IsUpdatePageSequence = true;
                    }
                    else
                    {
                        pageUpdateForPageReviewModel.IsUpdatePageSequence = false;
                    }
                }
                else
                {
                    pageUpdateForPageReviewModel.IsUpdatePageSequence = false;
                }
                pageUpdateForPageReviewModel.PageGUID = new Guid(StringUtility.GetQueryString(Constants.QUERYSTR_PAGE_GUID));
                pageUpdateForPageReviewModel.PageOrder = int.Parse(StringUtility.GetQueryString(Constants.QUERYSTR_PAGE_ORDER));
                pageUpdateForPageReviewModel.PageSequenceGUID = new Guid(StringUtility.GetQueryString(Constants.QUERYSTR_PAGE_SEQUENCE_GUID));
                pageUpdateForPageReviewModel.PresenterImageGUID = _imageGuid;
                pageUpdateForPageReviewModel.SessionGUID = new Guid(StringUtility.GetQueryString(Constants.QUERYSTR_SESSION_GUID));

                //switch (_type)
                //{
                //    case "PagePresenter":
                serviceProxy.SetPagePresenterImageCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(serviceProxy_SetPagePresenterImageCompleted);
                serviceProxy.SetPagePresenterImageAsync(pageUpdateForPageReviewModel);
                //        break;
                //    case "CTPPPresenter":
                //        serviceProxy.SetCTPPPresenterImageCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(serviceProxy_SetCTPPPresenterImageCompleted);
                //        serviceProxy.SetCTPPPresenterImageAsync(_imageGuid, new Guid(StringUtility.GetQueryString(Constants.QUERYSTR_PROGRAM_GUID)));
                //        break;
                //}
            }
        }

        private void serviceProxy_SetPagePresenterImageCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.Cancelled)
            {
                HtmlPage.Window.Alert("This operation is cancelled");
            }
            else if (e.Error != null)
            {
                HtmlPage.Window.Alert(Constants.ERROR_INTERNAL);
            }
            else
            {
                //HtmlPage.Window.Eval("$(\"#PresenterImagePanel\").hide();$(\".modalBackground\").hide();");
                HtmlPage.Window.Eval("window.parent.location=window.parent.location; ");
            }
            ImageList.Visibility = Visibility.Visible;
        }

        private void serviceProxy_SetCTPPPresenterImageCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.Cancelled)
            {
                HtmlPage.Window.Alert("This operation is cancelled");
            }
            else if (e.Error != null)
            {
                HtmlPage.Window.Alert(Constants.ERROR_INTERNAL);
            }
            else
            {
                //HtmlPage.Window.Eval("$(\"#PresenterImagePanel\").hide();$(\".modalBackground\").hide();");
                HtmlPage.Window.Eval("window.parent.location=window.parent.location; ");
                //HtmlPage.Window.Eval("document.location = window.location.hash.substring(1);");
            }
            ImageList.Visibility = Visibility.Visible;
        }
    }
}
