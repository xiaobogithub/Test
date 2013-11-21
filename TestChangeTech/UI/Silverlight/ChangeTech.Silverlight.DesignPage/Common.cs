using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using ChangeTech.Silverlight.DesignPage.ChangeTechWCFService;
using System.ServiceModel.Description;
using System.ServiceModel;
using System.Windows.Browser;
using ChangeTech.Silverlight.Common;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.ComponentModel;

namespace ChangeTech.Silverlight.DesignPage
{
    public delegate void SelectResourceDelegate(object sender, SelectResourceEventArgs e);
    public delegate void SelectPictureDelegate(ResourceModel image);
    public delegate void PreviewPictureDelegate(ResourceModel image);
    public delegate void DoubleClickDelegate(ResourceModel image);
    public delegate void CropPictureDelegate(ResourceModel image);
    public delegate void DisplayPictureInfoDelegate(ResourceModel image);
    public delegate void SelectPageDelegate(SimplePageContentModel simplePageContentModel, ButtonType buttonType, PositionType positionType);
    public delegate void AddPreferenceDelegate(PreferenceItemModel preferenceItem);
    public delegate void EditPreferenceDelegate(PreferenceItemModel preferenceItem);
    public delegate void SelectQuestionItemDelegate(PageQuestionItemModel questionItem);
    public delegate void SelectRelapseDelegate(RelapseModel relapse);
    public delegate void SetExpressionDelegate(string expression, ExpressionType expressionType);
    public delegate void UpdateExpressionGroupsDelegate(ObservableCollection<ExpressionGroupModel> expressionGroups);
    public delegate void SelectExpressionDelegate(string expressionText);
    public delegate void ChangeResourceCategoryDelegate();
    public delegate void UpdateResourceListDelegate(List<Guid> categoryWithChange);
    public delegate void ClosePreviewDelegate();
    public delegate void CloseCropDelegate(ResourceModel image);
    public delegate void DeleteImageDelegate(ResourceModel image);
    public delegate void SetExportOptionDelegate(LanguageModel lm, string startDay, string endDay, bool includeRelapse, bool includeProgramRoom,
    bool includeAccessoryTemplate, bool includeEmailTemplate, bool includeHelpItem, bool includeUserMenu, bool includeTipMessage, bool includeSpecialString);

    public class SelectResourceEventArgs : EventArgs
    {
        public ResourceModel Resource{get;set;}
        public SelectResourceEventArgs(ResourceModel resource)
        {
            Resource = resource;
        }
    }

    public class PageVariableEventArgs : RoutedEventArgs
    {
        public EditPageVariableModel pageVariable;
        public object sender;

        public PageVariableEventArgs()
        {
            sender = new object();
            pageVariable = new EditPageVariableModel();
        }

        public PageVariableEventArgs(object sender)
        {
            this.sender = sender;
        }
    }

    public static class Constants
    {
        public static readonly string ThumbImageDirectory = "thumnailcontainer/";
        public static readonly string OriginalImageDirectory = "originalimagecontainer/";
        public static readonly string VideoDirectory = "videocontainer/";
        public static readonly string RadioDirectory = "audiocontainer/";
        public static readonly string DocumentDirectory = "documentcontainer/";
        public static readonly string UploadResourceService = "UploadResource.ashx";

        public static readonly string QUERYSTR_SESSION_GUID = "SessionGUID";
        public static readonly string QUERYSTR_PAGE_GUID = "PgGUID";
        public static readonly string QUERYSTR_PAGE_ORDER = "PgOrder";
        public static readonly string QUERYSTR_PAGE_SEQUENCE_GUID = "PgSequenceGUID";
        public static readonly string QUERYSTR_LANGUAGE_GUID = "LanguageGUID";
        public static readonly string QUERYSTR_USER_GUID = "UserGUID";
        public static readonly string QUERYSTR_READONLY = "ReadOnly";
        public static readonly string QUERYSTR_PROGRAM_GUID = "ProgramGUID";

        public static readonly string QUERYSTR_EDITMODE = "EditMode";
        public static readonly string SELF = "Self";

        public static readonly string ERROR_INTERNAL = "Internal error. Please report to Changetech develop team.";

        public static readonly string MSG_LOADING = "Loading data, please wait for seconds......";
        public static readonly string MSG_SAVING = "Saving data, please wait for seconds ......";
        public static readonly string MSG_SUCCESSFUL = "Your operation is saved successfuly.";
        public static readonly string MSG_CANCELLED = "Operation is cancelled.";
        public static readonly string MSG_DELETING = "Deleting data, please wait for seconds......";
        public static readonly string MSG_DELETED = "Operation successfully.";
        public static readonly string MSG_FAILED = "Operation  failed , resource is used ! ";
    }

    #region Page Load
    public enum ButtonType
    {
        PrimaryButton,
        SecondaryButton
    }

    public enum DesignType
    {
        New,
        Edit
    }

    public enum ExpressionType
    {
        BeforeProperty,
        AfterProperty,
        GraphDataItemExpression
    }

    public enum PositionType
    {
        Absolute,
        Relative
    }

    public enum ImageType
    {
        Illustration,
        Background,
        Presenter
    }

    public enum SaveType
    {
        Save,
        Preview
    }
    #endregion

    public class ServiceProxyFactory
    {
        public static ServiceProxyFactory Instance {
            get {
                return Nested.Instance;
            }
        }

        private ServiceClient _serviceProxy;
        public ServiceClient ServiceProxy
        {
            get
            {
                string domainUrl = StringUtility.GetApplicationPath();
                if (domainUrl.Contains("https:"))
                {
                    BasicHttpBinding basicHttpbinding = new BasicHttpBinding();
                    basicHttpbinding.Security.Mode = BasicHttpSecurityMode.Transport;
                    basicHttpbinding.MaxReceivedMessageSize = 2147483647;
                    basicHttpbinding.SendTimeout = new TimeSpan(0, 10, 0);
                    EndpointAddress endPointAddress = new EndpointAddress(domainUrl + "Service.svc");
                    //_serviceProxy.Endpoint.Address = new EndpointAddress(StringUtility.GetApplicationPath() + "Service.svc");
                    _serviceProxy = new ServiceClient(basicHttpbinding, endPointAddress);
                }
                else
                {
                    //EndpointAddress endPointAddress = new EndpointAddress(StringUtility.GetApplicationPath() + "Service.svc");
                    _serviceProxy = new ServiceClient();
                    _serviceProxy.Endpoint.Address = new EndpointAddress(domainUrl + "Service.svc");
                }
                //_serviceProxy.Endpoint.Address = new EndpointAddress("http://localhost:41265/Service.svc");
                //_serviceProxy.Endpoint.Address = new EndpointAddress("http://webpublish.bj.ethos.com.cn/ChangeTechDevInternal/Service.svc");
                return _serviceProxy;
            }
        }

        private ServiceProxyFactory()
        {
        }

        class Nested
        {
            static Nested()
            { 
            }

            internal static readonly ServiceProxyFactory Instance = new ServiceProxyFactory();
        }
    }

    public class VirtualSelectionArea : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private double _Top;
        public double Top
        {
            get
            {
                return _Top;
            }
            set
            {
                _Top = value;
                RaisePropertyChanged("Top");
                VMidPoint = (Top + Bottom) / 2;
            }
        }

        private double _Bottom;
        public double Bottom
        {
            get
            {
                return _Bottom;
            }
            set
            {
                _Bottom = value;
                RaisePropertyChanged("Bottom");
                VMidPoint = (Top + Bottom) / 2;
            }
        }

        private double _Left;
        public double Left
        {
            get
            {
                return _Left;
            }
            set
            {
                _Left = value;
                RaisePropertyChanged("Left");
                HMidPoint = (Left + Right) / 2;
            }
        }

        private double _Right;
        public double Right
        {
            get
            {
                return _Right;
            }
            set
            {
                _Right = value;
                RaisePropertyChanged("Right");
                HMidPoint = (Left + Right) / 2;
            }
        }

        public double Width
        {
            get
            {
                return Right - Left;
            }
        }

        public double Height
        {
            get
            {
                return Bottom - Top;
            }
        }

        private double _VMidPoint;
        public double VMidPoint
        {
            get
            {
                return _VMidPoint;
            }
            set
            {
                _VMidPoint = value;
                RaisePropertyChanged("VMidPoint");
            }
        }

        private double _HMidPoint;
        public double HMidPoint
        {
            get
            {
                return _HMidPoint;
            }
            set
            {
                _HMidPoint = value;
                RaisePropertyChanged("HMidPoint");
            }
        }

    }
}
