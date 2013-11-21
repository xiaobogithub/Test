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
using System.Windows.Navigation;
using ChangeTech.Silverlight.DesignPage.ChangeTechWCFService;

using ChangeTech.Silverlight.Common;
using System.Windows.Browser;
using System.Windows.Controls.Primitives;

namespace ChangeTech.Silverlight.DesignPage
{
    public partial class CTPPPresenterImageSelection : Page
    {
        public CTPPPresenterImageSelection()
        {
            InitializeComponent();      
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ImageList.OnSelectImage += new SelectPictureDelegate(ImageList_OnSelectImage);
            ImageList.CloseButton.Visibility = Visibility.Collapsed;
            ImageList.Show();
        }

        void ImageList_OnSelectImage(ChangeTechWCFService.ResourceModel image)
        {
            ServiceClient serviceProxy = ServiceProxyFactory.Instance.ServiceProxy;
            serviceProxy.SetCTPPPresenterImageCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(serviceProxy_SetCTPPPresenterImageCompleted);
            serviceProxy.SetCTPPPresenterImageAsync(image.ID, new Guid(StringUtility.GetQueryString(Constants.QUERYSTR_PROGRAM_GUID)));
        }

        void serviceProxy_SetCTPPPresenterImageCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
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
                HtmlPage.Window.Eval("$(\"#PresenterImagePanel\").hide();$(\".modalBackground\").hide();");
            }
            ImageList.Visibility = Visibility.Visible;
        }

        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }       
    }
}
