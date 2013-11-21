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

using ChangeTech.Silverlight.Common;
using ChangeTech.Silverlight.DesignPage.ChangeTechWCFService;
using System.Windows.Browser;
using System.Windows.Controls.Primitives;

namespace ChangeTech.Silverlight.DesignPage
{
    public class SimplePageModel
    {
        public string Heading { get; set; }
        public string Body { get; set; }
        public string PrimaryButtonCaption { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// CHANGELOG:
    /// ...
    /// 2010-01-21: [Chen Pu]   Refactor code: Combine the logic to get page materials, page sequence references status,
    ///                         and page data with one method.
    ///                         
    /// </remarks>
    public partial class EditPage : Page
    {
        public const string CHANGETECHPAGEFLASH = "ChangeTechF.html";
        public const string CHANGETECHPAGEHTML5 = "ChangeTech5.html";
        public const string CHANGETECHPAGEHTML5R = "ChangeTech5r.html";

        private Guid _programGuid;
        private Guid _pageSequenceGuid;
        private Guid _pageGuid;
        private SaveType _saveType;
        private Guid _pageFirstSessionSequenceGuid;
        private bool isFirstSession = false;//used the first session in serviceProxy_GetFistSessionOfProgramCompleted function

        // Indicate whether page sequence was used in different session in one program
        private bool _isPageSequenceInMoreSession;
        private EditPageModel _originalPageModel = new EditPageModel();

        public EditPage()
        {
            InitializeComponent();
        }

        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

        }

        // not find the place to set QUERYSTR_EDITMODE in this page, so I will always return false now.
        private bool IsEditPagesequenceOnly()
        {
            bool flug = false;
            if (!string.IsNullOrEmpty(StringUtility.GetQueryString(Constants.QUERYSTR_EDITMODE)) && StringUtility.GetQueryString(Constants.QUERYSTR_EDITMODE).Equals(Constants.SELF))
            {
                flug = true;
            }

            return flug;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ServiceClient serviceProxy = ServiceProxyFactory.Instance.ServiceProxy;

            if (IsEditPagesequenceOnly())
            {
                serviceProxy.GetEditPageModelByProgramCompleted += new EventHandler<GetEditPageModelByProgramCompletedEventArgs>(serviceProxy_GetEditPageModelByProgramCompleted);
                serviceProxy.GetEditPageModelByProgramAsync(new Guid(StringUtility.GetQueryString(Constants.QUERYSTR_PAGE_GUID)),
                    new Guid(StringUtility.GetQueryString(Constants.QUERYSTR_PROGRAM_GUID)));
            }
            else
            {
                serviceProxy.GetEditPageModelCompleted += new EventHandler<GetEditPageModelCompletedEventArgs>(serviceProxy_GetEditPageModelCompleted);
                serviceProxy.GetEditPageModelAsync(new Guid(StringUtility.GetQueryString(Constants.QUERYSTR_PAGE_GUID)),
                    new Guid(StringUtility.GetQueryString(Constants.QUERYSTR_SESSION_GUID)),
                    new Guid(StringUtility.GetQueryString(Constants.QUERYSTR_PAGE_SEQUENCE_GUID)));
            }
        }

        void serviceProxy_GetEditPageModelByProgramCompleted(object sender, GetEditPageModelByProgramCompletedEventArgs e)
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
                InitialPage(e.Result);
            }
        }

        private void serviceProxy_GetEditPageModelCompleted(object sender, GetEditPageModelCompletedEventArgs e)
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
                InitialPage(e.Result);
            }
        }

        private void InitialPage(EditPageModel pageModel)
        {
            PageMaterials pageMaterials = pageModel.PageMaterials;
            GetInformationTemplate.Questions = pageMaterials.Questions;
            ////ScreeningResultsTemplate.PageLines = pageMaterials.PageLines;
            _originalPageModel = pageModel;
            TemplateComboBox.ItemsSource = pageMaterials.PageTemplates;
            TemplateComboBox.SelectedItem = GetOriginalTemplateModel();
            //TemplateComboBox.Text = pageModel.Page.Template.Name;
            _programGuid = pageModel.ProgramGUID;
            _originalPageModel = pageModel;
            switch (pageModel.Page.Template.Name)
            {
                case "Standard":
                    //StandardTemplate.PageContentModel.LanguageGUID = new Guid(StringUtility.GetQueryString(Constants.QUERYSTR_LANGUAGE_GUID));
                    StandardTemplate.Visibility = Visibility.Visible;
                    StandardTemplate.BindPageContent(pageModel.Page as EditStandardTemplatePageContentModel);
                    break;
                case "Get information":
                    //GetInformationTemplate.PageContentModel.LanguageGUID = new Guid(StringUtility.GetQueryString(Constants.QUERYSTR_LANGUAGE_GUID));
                    GetInformationTemplate.DesignType = DesignType.Edit;
                    GetInformationTemplate.Visibility = Visibility.Visible;
                    GetInformationTemplate.BindPageContent(pageModel.Page as EditGetInfoTemplatePageContentModel);
                    break;
                case "Screening results":
                    //GetInformationTemplate.PageContentModel.LanguageGUID = new Guid(StringUtility.GetQueryString(Constants.QUERYSTR_LANGUAGE_GUID));
                    ScreeningResultsTemplate.DesignType = DesignType.Edit;
                    ScreeningResultsTemplate.Visibility = Visibility.Visible;
                    ScreeningResultsTemplate.BindPageContent(pageModel.Page as EditScreenResultsTemplatePageContentModel);
                    break;
                case "Push pictures":
                    //PushPicturesTemplate.PageContentModel.LanguageGUID = new Guid(StringUtility.GetQueryString(Constants.QUERYSTR_LANGUAGE_GUID));
                    PushPicturesTemplate.Visibility = Visibility.Visible;
                    PushPicturesTemplate.BindPageContent(pageModel.Page as EditPushPictureTemplatePageContentModel);
                    break;
                case "Choose preferences":
                    //ChoosePreferenceTemplate.PageContentModel.LanguageGUID = new Guid(StringUtility.GetQueryString(Constants.QUERYSTR_LANGUAGE_GUID));
                    ChoosePreferenceTemplate.DesignType = DesignType.Edit;
                    ChoosePreferenceTemplate.Visibility = Visibility.Visible;
                    ChoosePreferenceTemplate.BindPageContent(pageModel.Page as EditChoosePreferencesTemplatePageContentModel);
                    break;
                case "Timer":
                    //TimerTemplate.PageContentModel.LanguageGUID = new Guid(StringUtility.GetQueryString(Constants.QUERYSTR_LANGUAGE_GUID));
                    TimerTemplate.Visibility = Visibility.Visible;
                    TimerTemplate.BindPageContent(pageModel.Page as EditTimerTemplatePageContentModel);
                    break;
                case "Account creation":
                    //AccountCreationTemplate.PageContentModel.LanguageGUID = new Guid(StringUtility.GetQueryString(Constants.QUERYSTR_LANGUAGE_GUID));
                    AccountCreationTemplate.Visibility = Visibility.Visible;
                    AccountCreationTemplate.BindPageContent(pageModel.Page as EditAccountCreationTemplatePageContentModel);
                    break;
                case "Graph":
                    //GraphTemplate.PageContentModel.LanguageGUID = new Guid(StringUtility.GetQueryString(Constants.QUERYSTR_LANGUAGE_GUID));
                    GraphTemplate.Visibility = Visibility.Visible;
                    GraphTemplate.BindContent(pageModel.Page as EditGraphTemplatePageContentModel);
                    break;
                case "SMS":
                    SMSTemplate.Visibility = Visibility.Visible;
                    SMSTemplate.BindPageContent(pageModel.Page as EditSMSTemplatePageContentModel);
                    break;
            }

            if (StringUtility.GetQueryString(Constants.QUERYSTR_READONLY).Equals("True"))
            {
                Disable();
                PrviewButton.IsEnabled = true;
                CancelButton.IsEnabled = true;

                StandardTemplate.DisableControl("");
                GetInformationTemplate.DisableControl("");
                PushPicturesTemplate.DiableControl("");
                ChoosePreferenceTemplate.DisableControl("");
                TimerTemplate.DisableControl("");
                AccountCreationTemplate.DisableControl("");
                GraphTemplate.DisableControl("");
                ScreeningResultsTemplate.DisableControl("");
            }
        }

        private PageTemplateModel GetOriginalTemplateModel()
        {
            PageTemplateModel template = new PageTemplateModel();
            foreach (PageTemplateModel model in TemplateComboBox.ItemsSource)
            {
                if (model.Name == _originalPageModel.Page.Template.Name)
                {
                    template = model;
                }
            }
            return template;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Disable();

            string validateResult = ValidatePage();
            if (string.IsNullOrEmpty(validateResult))
            {
                _saveType = SaveType.Save;
                if (IsEditPagesequenceOnly())
                {
                    _pageSequenceGuid = new Guid(StringUtility.GetQueryString(Constants.QUERYSTR_PAGE_SEQUENCE_GUID));
                    SavePage(new Guid(StringUtility.GetQueryString(Constants.QUERYSTR_PAGE_GUID)));
                }
                else
                {
                    bool affectFlag = false;
                    if (_isPageSequenceInMoreSession)
                    {
                        if (HtmlPage.Window.Confirm("Do you want to imfact other session which used this pageSquence?\n if 'yes' press 'ok'\n if 'no' press 'cancel'"))
                        {
                            affectFlag = true;
                        }
                        else
                        {
                            _isPageSequenceInMoreSession = false;
                        }
                    }
                    ServiceClient serviceProxy = ServiceProxyFactory.Instance.ServiceProxy;
                    //serviceProxy.BeforeEditPageSequenceCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(serviceProxy_BeforeEditPageSequenceCompleted);
                    serviceProxy.BeforeEditPageSequenceCompleted += new EventHandler<BeforeEditPageSequenceCompletedEventArgs>(serviceProxy_BeforeEditPageSequenceCompleted);
                    serviceProxy.BeforeEditPageSequenceAsync(new Guid(StringUtility.GetQueryString(Constants.QUERYSTR_SESSION_GUID)), new Guid(StringUtility.GetQueryString(Constants.QUERYSTR_PAGE_SEQUENCE_GUID)), affectFlag);
                }
            }
            else
            {
                HtmlPage.Window.Alert(validateResult);
                Enable();
            }
        }

        private void serviceProxy_BeforeEditPageSequenceCompleted(object sender, BeforeEditPageSequenceCompletedEventArgs e)
        {
            _pageSequenceGuid = e.Result;
            ServiceClient serviceProxy = ServiceProxyFactory.Instance.ServiceProxy;            
            serviceProxy.GetNewPageGuidByPageSequenceAndOldPageGuidCompleted += new EventHandler<GetNewPageGuidByPageSequenceAndOldPageGuidCompletedEventArgs>(serviceProxy_GetNewPageGuidByPageSequenceAndOldPageGuidCompleted);
            serviceProxy.GetNewPageGuidByPageSequenceAndOldPageGuidAsync(_pageSequenceGuid, new Guid(StringUtility.GetQueryString(Constants.QUERYSTR_PAGE_GUID)));
        }

        private void serviceProxy_GetNewPageGuidByPageSequenceAndOldPageGuidCompleted(object sender, GetNewPageGuidByPageSequenceAndOldPageGuidCompletedEventArgs e)
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
                SavePage(e.Result);
            }
        }
        public void SetImageModeForStandardTemplatePageContentModel(string pageTemplateName)
        {
            switch (pageTemplateName)
            {
                case "StandardTemplate":
                    if (StandardTemplate.PresenterModeRadioButton.IsChecked.Value)
                    {
                        StandardTemplate.PageContentModel.ImageMode = ImageModeEnum.PresenterMode;
                        StandardTemplate.PageContentModel.PresenterImageGUID = StandardTemplate.ImageGuid;
                        StandardTemplate.PageContentModel.BackgroundImageGUID = Guid.Empty;
                    }
                    else if (StandardTemplate.FullscreenModeRadioButton.IsChecked.Value)
                    {
                        StandardTemplate.PageContentModel.ImageMode = ImageModeEnum.FullscreenMode;
                        StandardTemplate.PageContentModel.BackgroundImageGUID = StandardTemplate.ImageGuid;
                    }
                    else if (StandardTemplate.IllustrationModeRadioButton.IsChecked.Value)
                    {
                        StandardTemplate.PageContentModel.ImageMode = ImageModeEnum.IllustrationMode;
                        StandardTemplate.PageContentModel.IllustrationImageGUID = StandardTemplate.ImageGuid;
                        StandardTemplate.PageContentModel.BackgroundImageGUID = Guid.Empty;
                    }
                    break;
                case "GetInformationTemplate":
                    if (GetInformationTemplate.PresenterModeRadioButton.IsChecked.Value)
                    {
                        GetInformationTemplate.PageContentModel.ImageMode = ImageModeEnum.PresenterMode;
                        GetInformationTemplate.PageContentModel.PresenterImageGUID = GetInformationTemplate.ImageGuid;
                        GetInformationTemplate.PageContentModel.BackgroundImageGUID = Guid.Empty;
                    }
                    else if (GetInformationTemplate.FullscreenModeRadioButton.IsChecked.Value)
                    {
                        GetInformationTemplate.PageContentModel.ImageMode = ImageModeEnum.FullscreenMode;
                        GetInformationTemplate.PageContentModel.BackgroundImageGUID = GetInformationTemplate.ImageGuid;
                    }
                    else if (GetInformationTemplate.IllustrationModeRadioButton.IsChecked.Value)
                    {
                        GetInformationTemplate.PageContentModel.ImageMode = ImageModeEnum.IllustrationMode;
                        GetInformationTemplate.PageContentModel.IllustrationImageGUID = GetInformationTemplate.ImageGuid;
                        GetInformationTemplate.PageContentModel.BackgroundImageGUID = Guid.Empty;
                    }
                    break;
                case "PushPicturesTemplate":
                    //if (PushPicturesTemplate.PresenterModeRadioButton.IsChecked.Value)
                    //{
                    //    PushPicturesTemplate.PageContentModel.ImageMode = ImageModeEnum.PresenterMode;
                    //    PushPicturesTemplate.PageContentModel.PresenterImageGUID = PushPicturesTemplate.ImageGuid;
                    //    PushPicturesTemplate.PageContentModel.BackgroundImageGUID = Guid.Empty;
                    //}
                    //else if (PushPicturesTemplate.FullscreenModeRadioButton.IsChecked.Value)
                    //{
                    //    PushPicturesTemplate.PageContentModel.ImageMode = ImageModeEnum.FullscreenMode;
                    //    PushPicturesTemplate.PageContentModel.BackgroundImageGUID = PushPicturesTemplate.ImageGuid;
                    //}
                    //else if (PushPicturesTemplate.IllustrationModeRadioButton.IsChecked.Value)
                    //{
                    //    PushPicturesTemplate.PageContentModel.ImageMode = ImageModeEnum.IllustrationMode;
                    //    PushPicturesTemplate.PageContentModel.IllustrationImageGUID = PushPicturesTemplate.ImageGuid;
                    //    PushPicturesTemplate.PageContentModel.BackgroundImageGUID = Guid.Empty;
                    //}
                    break;
                case "ScreeningResultsTemplate":
                    if (ScreeningResultsTemplate.PresenterModeRadioButton.IsChecked.Value)
                    {
                        ScreeningResultsTemplate.PageContentModel.ImageMode = ImageModeEnum.PresenterMode;
                    }
                    //else if (ScreeningResultsTemplate.FullscreenModeRadioButton.IsChecked.Value)
                    //{
                    //    ScreeningResultsTemplate.PageContentModel.ImageMode = ImageModeEnum.FullscreenMode;
                    //    ScreeningResultsTemplate.PageContentModel.BackgroundImageGUID = ScreeningResultsTemplate.ImageGuid;
                    //}
                    //else if (ScreeningResultsTemplate.IllustrationModeRadioButton.IsChecked.Value)
                    //{
                    //    ScreeningResultsTemplate.PageContentModel.ImageMode = ImageModeEnum.IllustrationMode;
                    //    ScreeningResultsTemplate.PageContentModel.IllustrationImageGUID = ScreeningResultsTemplate.ImageGuid;
                    //    ScreeningResultsTemplate.PageContentModel.BackgroundImageGUID = Guid.Empty;
                    //}
                    break;
                default:
                    break;
            }
           
        }

        private void SavePage(Guid pageGuid)
        {
            StringUtility.GetQueryString(Constants.QUERYSTR_PAGE_SEQUENCE_GUID);
            ServiceClient serviceProxy = ServiceProxyFactory.Instance.ServiceProxy;
            _pageGuid = pageGuid;
            switch (GetCurrentTemplate().Name)
            {
                case "Standard":
                    StandardTemplate.DisableControl("Saving");
                    StandardTemplate.FillContent();
                    if (IsChangeTemplate())
                    {
                        StandardTemplate.PageContentModel.TemplateGUID = GetCurrentTemplate().Guid;
                        StandardTemplate.PageContentModel.ProgramGuid = _programGuid;
                        SetImageModeForStandardTemplatePageContentModel("StandardTemplate");
                        serviceProxy.SaveChangedToStandardTemplatePageCompleted += new EventHandler<SaveChangedToStandardTemplatePageCompletedEventArgs>(serviceProxy_SaveChangedToStandardTemplatePageCompleted);
                        serviceProxy.SaveChangedToStandardTemplatePageAsync(pageGuid, StandardTemplate.PageContentModel);
                    }
                    else
                    {
                        SetImageModeForStandardTemplatePageContentModel("StandardTemplate");
                        serviceProxy.UpdateStandardTemplatePageCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(serviceProxy_UpdateStandardTemplatePageCompleted);
                        serviceProxy.UpdateStandardTemplatePageAsync(StandardTemplate.PageContentModel, pageGuid);
                    }
                    break;
                case "Get information":
                    GetInformationTemplate.DisableControl("Saving");
                    GetInformationTemplate.FillContent();
                    if (IsChangeTemplate())
                    {
                        GetInformationTemplate.PageContentModel.TemplateGUID = GetCurrentTemplate().Guid;
                        GetInformationTemplate.PageContentModel.ProgramGuid = _programGuid;
                        SetImageModeForStandardTemplatePageContentModel("GetInformationTemplate");
                        serviceProxy.SaveChangedToGetInfoTemplatePageCompleted += new EventHandler<SaveChangedToGetInfoTemplatePageCompletedEventArgs>(serviceProxy_SaveChangedToGetInfoTemplatePageCompleted);
                        serviceProxy.SaveChangedToGetInfoTemplatePageAsync(pageGuid, GetInformationTemplate.PageContentModel);
                    }
                    else
                    {
                        SetImageModeForStandardTemplatePageContentModel("GetInformationTemplate");
                        serviceProxy.UpdateGetInfoTemplatePageCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(serviceProxy_UpdateGetInfoTemplatePageCompleted);
                        serviceProxy.UpdateGetInfoTemplatePageAsync(GetInformationTemplate.PageContentModel, pageGuid);
                    }
                    break;
                case "Screening results":
                    ScreeningResultsTemplate.DisableControl("Saving");
                    ScreeningResultsTemplate.FillContent();
                    if (IsChangeTemplate())
                    {
                        ScreeningResultsTemplate.PageContentModel.TemplateGUID = GetCurrentTemplate().Guid;
                        ScreeningResultsTemplate.PageContentModel.ProgramGuid = _programGuid;
                        SetImageModeForStandardTemplatePageContentModel("ScreeningResultsTemplate");
                        serviceProxy.SaveChangedToScreenResultsTemplatePageCompleted += new EventHandler<SaveChangedToScreenResultsTemplatePageCompletedEventArgs>(serviceProxy_SaveChangedToScreenResultsTemplatePageCompleted);
                        serviceProxy.SaveChangedToScreenResultsTemplatePageAsync(pageGuid, ScreeningResultsTemplate.PageContentModel);
                    }
                    else
                    {
                        SetImageModeForStandardTemplatePageContentModel("ScreeningResultsTemplate");
                        serviceProxy.UpdateScreenResultsTemplatePageCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(serviceProxy_UpdateScreenResultsTemplatePageCompleted); //.UpdateGetInfoTemplatePageCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(serviceProxy_UpdateGetInfoTemplatePageCompleted);
                        serviceProxy.UpdateScreenResultsTemplatePageAsync(ScreeningResultsTemplate.PageContentModel, pageGuid); //.UpdateGetInfoTemplatePageAsync(GetInformationTemplate.PageContentModel, pageGuid);
                    }
                    break;
                case "Push pictures":
                    PushPicturesTemplate.DiableControl("Saving");
                    PushPicturesTemplate.FillContent();
                    if (IsChangeTemplate())
                    {
                        PushPicturesTemplate.PageContentModel.TemplateGUID = GetCurrentTemplate().Guid;
                        PushPicturesTemplate.PageContentModel.ProgramGuid = _programGuid;
                        serviceProxy.SaveChangedToPushPicturesTemplatePageCompleted += new EventHandler<SaveChangedToPushPicturesTemplatePageCompletedEventArgs>(serviceProxy_SaveChangedToPushPicturesTemplatePageCompleted);
                        serviceProxy.SaveChangedToPushPicturesTemplatePageAsync(pageGuid, PushPicturesTemplate.PageContentModel);
                    }
                    else
                    {
                        serviceProxy.UpdatePushPictureTemplatePageCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(serviceProxy_UpdatePushPictureTemplatePageCompleted);
                        serviceProxy.UpdatePushPictureTemplatePageAsync(PushPicturesTemplate.PageContentModel, pageGuid);
                    }
                    break;
                case "Timer":
                    TimerTemplate.DisableControl("Saving");
                    TimerTemplate.FillContent();
                    if (IsChangeTemplate())
                    {
                        TimerTemplate.PageContentModel.TemplateGUID = GetCurrentTemplate().Guid;
                        TimerTemplate.PageContentModel.ProgramGuid = _programGuid;
                        serviceProxy.SaveChangedToTimerTemplatePageCompleted += new EventHandler<SaveChangedToTimerTemplatePageCompletedEventArgs>(serviceProxy_SaveChangedToTimerTemplatePageCompleted);
                        serviceProxy.SaveChangedToTimerTemplatePageAsync(pageGuid, TimerTemplate.PageContentModel);
                    }
                    else
                    {
                        serviceProxy.UpdateTimerTemplatePageCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(serviceProxy_UpdateTimerTemplatePageCompleted);
                        serviceProxy.UpdateTimerTemplatePageAsync(TimerTemplate.PageContentModel, pageGuid);
                    }
                    break;
                case "Choose preferences":
                    ChoosePreferenceTemplate.DisableControl("Saving");
                    ChoosePreferenceTemplate.FillContent();
                    if (IsChangeTemplate())
                    {
                        ChoosePreferenceTemplate.PageContentModel.TemplateGUID = GetCurrentTemplate().Guid;
                        ChoosePreferenceTemplate.PageContentModel.ProgramGuid = _programGuid;
                        serviceProxy.SaveChangedToChoosePreferenceTemplatePageCompleted += new EventHandler<SaveChangedToChoosePreferenceTemplatePageCompletedEventArgs>(serviceProxy_SaveChangedToChoosePreferenceTemplatePageCompleted);
                        serviceProxy.SaveChangedToChoosePreferenceTemplatePageAsync(pageGuid, ChoosePreferenceTemplate.PageContentModel);
                    }
                    else
                    {
                        serviceProxy.UpdateChoosePreferencesTemplatePageContentModelCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(serviceProxy_UpdateChoosePreferencesTemplatePageContentModelCompleted);
                        serviceProxy.UpdateChoosePreferencesTemplatePageContentModelAsync(ChoosePreferenceTemplate.PageContentModel, pageGuid);
                    }
                    break;
                case "Account creation":
                    AccountCreationTemplate.DisableControl("Saving");
                    AccountCreationTemplate.FillContent();
                    if (IsChangeTemplate())
                    {
                        AccountCreationTemplate.PageContentModel.TemplateGUID = GetCurrentTemplate().Guid;
                        AccountCreationTemplate.PageContentModel.ProgramGuid = _programGuid;
                        serviceProxy.SaveChangedToAccountCreationTemplatePageCompleted += new EventHandler<SaveChangedToAccountCreationTemplatePageCompletedEventArgs>(serviceProxy_SaveChangedToAccountCreationTemplatePageCompleted);
                        serviceProxy.SaveChangedToAccountCreationTemplatePageAsync(pageGuid, AccountCreationTemplate.PageContentModel);
                    }
                    else
                    {
                        serviceProxy.UpdateAccoutCrationTemplatePageCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(serviceProxy_UpdateAccoutCrationTemplatePageCompleted);
                        serviceProxy.UpdateAccoutCrationTemplatePageAsync(AccountCreationTemplate.PageContentModel, pageGuid);
                    }
                    break;
                case "Graph":
                    GraphTemplate.DisableControl("Saving");
                    GraphTemplate.FillContent();
                    if (IsChangeTemplate())
                    {
                        GraphTemplate.PageContentModel.TemplateGUID = GetCurrentTemplate().Guid;
                        GraphTemplate.PageContentModel.ProgramGUID = _programGuid;
                        serviceProxy.SaveChangedToGraphTemplatePageContentModelCompleted += new EventHandler<SaveChangedToGraphTemplatePageContentModelCompletedEventArgs>(serviceProxy_SaveChangedToGraphTemplatePageContentModelCompleted);
                        serviceProxy.SaveChangedToGraphTemplatePageContentModelAsync(pageGuid, GraphTemplate.PageContentModel);
                    }
                    else
                    {
                        serviceProxy.UpdateGraphTemplatePageCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(serviceProxy_UpdateGraphTemplatePageCompleted);
                        serviceProxy.UpdateGraphTemplatePageAsync(GraphTemplate.PageContentModel, pageGuid);
                    }
                    break;
                case "SMS":
                    SMSTemplate.DisableControl("Saving");
                    SMSTemplate.FillContent();
                    if (IsChangeTemplate())
                    {
                        SMSTemplate.PageContentModel.TemplateGUID = GetCurrentTemplate().Guid;
                        serviceProxy.SaveChangedToSMSTemplatePageCompleted += new EventHandler<SaveChangedToSMSTemplatePageCompletedEventArgs>(serviceProxy_SaveChangedToSMSTemplatePageCompleted);
                        serviceProxy.SaveChangedToSMSTemplatePageAsync(pageGuid, SMSTemplate.PageContentModel);
                    }
                    else
                    {
                        serviceProxy.UpdateSMSTemplatePageCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(serviceProxy_UpdateSMSTemplatePageCompleted);
                        serviceProxy.UpdateSMSTemplatePageAsync(SMSTemplate.PageContentModel, pageGuid);
                    }
                    break;
                default:
                    HtmlPage.Window.Alert(string.Format("Template {0} is not recgonized as a valid template.", GetCurrentTemplate().Name));
                    break;
            }
        }


        public void Enable()
        {
            SaveButton.IsEnabled = true;
            PrviewButton.IsEnabled = true;
            CancelButton.IsEnabled = true;
            TemplateComboBox.IsEnabled = true;
        }

        public void Disable()
        {
            SaveButton.IsEnabled = false;
            PrviewButton.IsEnabled = false;
            CancelButton.IsEnabled = false;
            TemplateComboBox.IsEnabled = false;
        }

        void serviceProxy_SaveChangedToScreenResultsTemplatePageCompleted(object sender, SaveChangedToScreenResultsTemplatePageCompletedEventArgs e)
        {
            Enable();
            serviceProxy_SavePageCompleted(sender, e, e.Result);
        }      

        void serviceProxy_SaveChangedToSMSTemplatePageCompleted(object sender, SaveChangedToSMSTemplatePageCompletedEventArgs e)
        {
            Enable();
            serviceProxy_SavePageCompleted(sender, e, e.Result);
        }

        private void serviceProxy_SaveChangedToGraphTemplatePageContentModelCompleted(object sender, SaveChangedToGraphTemplatePageContentModelCompletedEventArgs e)
        {
            Enable();
            serviceProxy_SavePageCompleted(sender, e, e.Result);
        }

        private void serviceProxy_SaveChangedToAccountCreationTemplatePageCompleted(object sender, SaveChangedToAccountCreationTemplatePageCompletedEventArgs e)
        {
            Enable();
            serviceProxy_SavePageCompleted(sender, e, e.Result);
        }

        private void serviceProxy_SaveChangedToChoosePreferenceTemplatePageCompleted(object sender, SaveChangedToChoosePreferenceTemplatePageCompletedEventArgs e)
        {
            Enable();
            serviceProxy_SavePageCompleted(sender, e, e.Result);
        }

        private void serviceProxy_SaveChangedToTimerTemplatePageCompleted(object sender, SaveChangedToTimerTemplatePageCompletedEventArgs e)
        {
            Enable();
            serviceProxy_SavePageCompleted(sender, e, e.Result);
        }

        private void serviceProxy_SaveChangedToPushPicturesTemplatePageCompleted(object sender, SaveChangedToPushPicturesTemplatePageCompletedEventArgs e)
        {
            Enable();
            serviceProxy_SavePageCompleted(sender, e, e.Result);
        }

        private void serviceProxy_SaveChangedToGetInfoTemplatePageCompleted(object sender, SaveChangedToGetInfoTemplatePageCompletedEventArgs e)
        {
            Enable();
            serviceProxy_SavePageCompleted(sender, e, e.Result);
        }

        private void serviceProxy_SaveChangedToStandardTemplatePageCompleted(object sender, SaveChangedToStandardTemplatePageCompletedEventArgs e)
        {
            Enable();
            serviceProxy_SavePageCompleted(sender, e, e.Result);
        }



        void serviceProxy_UpdateScreenResultsTemplatePageCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            ScreeningResultsTemplate.EnableControl();
            Enable();
            serviceProxy_UpdatePageCompleted(sender, e);
        }

        void serviceProxy_UpdateSMSTemplatePageCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            SMSTemplate.EnableControl();
            Enable();
            serviceProxy_UpdatePageCompleted(sender, e);
        }

        private void serviceProxy_UpdateAccoutCrationTemplatePageCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            AccountCreationTemplate.EnableControl();
            Enable();
            serviceProxy_UpdatePageCompleted(sender, e);
        }

        private void serviceProxy_UpdateChoosePreferencesTemplatePageContentModelCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            ChoosePreferenceTemplate.EnableControl();
            Enable();
            serviceProxy_UpdatePageCompleted(sender, e);
        }

        private void serviceProxy_UpdateGraphTemplatePageCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            GraphTemplate.EnableControl();
            Enable();
            serviceProxy_UpdatePageCompleted(sender, e);
        }

        private void serviceProxy_UpdateTimerTemplatePageCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            TimerTemplate.EnableControl();
            Enable();
            serviceProxy_UpdatePageCompleted(sender, e);
        }

        private void serviceProxy_UpdatePushPictureTemplatePageCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            PushPicturesTemplate.EnableControl();
            Enable();
            serviceProxy_UpdatePageCompleted(sender, e);
        }

        private void serviceProxy_UpdateGetInfoTemplatePageCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            GetInformationTemplate.EnableControl();
            Enable();
            serviceProxy_UpdatePageCompleted(sender, e);
        }

        private void serviceProxy_UpdateStandardTemplatePageCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            StandardTemplate.EnableControl();
            Enable();
            serviceProxy_UpdatePageCompleted(sender, e);
        }

        private string ValidatePage()
        {
            string validateMessage = string.Empty;
            switch (GetCurrentTemplate().Name)
            {
                case "Standard":
                    validateMessage = StandardTemplate.Validate();
                    break;
                case "Get information":
                    validateMessage = GetInformationTemplate.Validate();
                    break;
                case "Screening results":
                    validateMessage = ScreeningResultsTemplate.Validate();
                    break;
                case "Push pictures":
                    validateMessage = PushPicturesTemplate.Validate();
                    break;
                case "Timer":
                    validateMessage = TimerTemplate.Validate();
                    break;
                case "Choose preferences":
                    validateMessage = ChoosePreferenceTemplate.Validate();
                    break;
                case "Account creation":
                    validateMessage = AccountCreationTemplate.Validate();
                    break;
                case "Graph":
                    validateMessage = GraphTemplate.Validate();
                    break;
                case "SMS":
                    validateMessage = SMSTemplate.Validate();
                    break;
                default:
                    validateMessage = string.Format("Template {0} is not recgonized as a valid template.", GetCurrentTemplate().Name);
                    break;
            }

            return validateMessage;
        }

        private PageTemplateModel GetCurrentTemplate()
        {
            return TemplateComboBox.SelectedItem as PageTemplateModel;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            GoBackWebPage();
        }

        private void serviceProxy_UpdatePageCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
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
                //HtmlPage.Window.Alert("This page has been updated successfully.");

                if (GetInformationTemplate.Visibility == Visibility.Visible)
                {
                    GetInformationTemplate.ResetAfterSave();
                }
                else if (GraphTemplate.Visibility == Visibility.Visible)
                {
                    GraphTemplate.ResetAfterSave();
                }
                else if (ChoosePreferenceTemplate.Visibility == Visibility.Visible)
                {
                    ChoosePreferenceTemplate.ResetAfterSave();
                }

                if (_saveType == SaveType.Preview)
                {
                    if (!IsEditPagesequenceOnly())
                    { 
                        ServiceClient serviceProxy = ServiceProxyFactory.Instance.ServiceProxy;
                        serviceProxy.IsEnableHTML5NewUICompleted += new EventHandler<IsEnableHTML5NewUICompletedEventArgs>(serviceProxy_IsEnableHTML5NewUICompleted);
                        serviceProxy.IsEnableHTML5NewUIAsync(_programGuid);
                        
                    }
                    else
                    {
                        ServiceClient serviceProxy = ServiceProxyFactory.Instance.ServiceProxy;
                        serviceProxy.GetFistSessionOfProgramCompleted += new EventHandler<GetFistSessionOfProgramCompletedEventArgs>(serviceProxy_GetFistSessionOfProgramCompleted);
                        serviceProxy.GetFistSessionOfProgramAsync(new Guid(StringUtility.GetQueryString(Constants.QUERYSTR_PROGRAM_GUID)));
                    }
                }
                if (_saveType == SaveType.Save)
                {
                    HtmlPage.Window.Alert(Constants.MSG_SUCCESSFUL);
                    GoWebPage();
                    //GoBackWebPage();
                }
            }
        }

        void serviceProxy_IsEnableHTML5NewUICompleted(object sender, IsEnableHTML5NewUICompletedEventArgs e)
        {
            string documentURL = HtmlPage.Document.DocumentUri.ToString();           
            string[] temp = documentURL.Split(new char[] { '?' });
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
                string previewURL = temp[0].Replace("EditPage.aspx", CHANGETECHPAGEFLASH + "?");
                if (e.Result)//enable html5 new ui
                {
                    string previewURL5 = temp[0].Replace("EditPage.aspx", CHANGETECHPAGEHTML5R + "?");                    
                    if (!isFirstSession)
                    {
                        HtmlPage.Window.Navigate(new Uri(previewURL5 + "Object=Page&Mode=Preview&" + Constants.QUERYSTR_PAGE_GUID + "=" + _pageGuid
                          + "&" + Constants.QUERYSTR_SESSION_GUID + "=" + StringUtility.GetQueryString(Constants.QUERYSTR_SESSION_GUID)
                          + "&" + Constants.QUERYSTR_PAGE_SEQUENCE_GUID + "=" + _pageSequenceGuid
                          + "&" + Constants.QUERYSTR_LANGUAGE_GUID + "=" + StringUtility.GetQueryString(Constants.QUERYSTR_LANGUAGE_GUID)
                          + "&" + Constants.QUERYSTR_USER_GUID + "=" + StringUtility.GetQueryString(Constants.QUERYSTR_USER_GUID)), "_blank");

                        HtmlPage.Window.Navigate(new Uri(previewURL + "Object=Page&Mode=Preview&" + Constants.QUERYSTR_PAGE_GUID + "=" + _pageGuid
                           + "&" + Constants.QUERYSTR_SESSION_GUID + "=" + StringUtility.GetQueryString(Constants.QUERYSTR_SESSION_GUID)
                           + "&" + Constants.QUERYSTR_PAGE_SEQUENCE_GUID + "=" + _pageSequenceGuid
                           + "&" + Constants.QUERYSTR_LANGUAGE_GUID + "=" + StringUtility.GetQueryString(Constants.QUERYSTR_LANGUAGE_GUID)
                           + "&" + Constants.QUERYSTR_USER_GUID + "=" + StringUtility.GetQueryString(Constants.QUERYSTR_USER_GUID)), "_blank");
                                                
                        GoWebPage();
                    }
                    else
                    {
                        HtmlPage.Window.Navigate(new Uri(previewURL5 + "Object=Page&Mode=Preview&" + Constants.QUERYSTR_PAGE_GUID + "=" + _pageGuid
                          + "&" + Constants.QUERYSTR_SESSION_GUID + "=" + StringUtility.GetQueryString(Constants.QUERYSTR_SESSION_GUID)
                          + "&" + Constants.QUERYSTR_PAGE_SEQUENCE_GUID + "=" + _pageFirstSessionSequenceGuid
                          + "&" + Constants.QUERYSTR_LANGUAGE_GUID + "=" + StringUtility.GetQueryString(Constants.QUERYSTR_LANGUAGE_GUID)
                          + "&" + Constants.QUERYSTR_USER_GUID + "=" + StringUtility.GetQueryString(Constants.QUERYSTR_USER_GUID)), "_blank");

                        HtmlPage.Window.Navigate(new Uri(previewURL + "Object=Page&Mode=Preview&" + Constants.QUERYSTR_PAGE_GUID + "=" + _pageGuid
                           + "&" + Constants.QUERYSTR_SESSION_GUID + "=" + StringUtility.GetQueryString(Constants.QUERYSTR_SESSION_GUID)
                           + "&" + Constants.QUERYSTR_PAGE_SEQUENCE_GUID + "=" + _pageFirstSessionSequenceGuid
                           + "&" + Constants.QUERYSTR_LANGUAGE_GUID + "=" + StringUtility.GetQueryString(Constants.QUERYSTR_LANGUAGE_GUID)
                           + "&" + Constants.QUERYSTR_USER_GUID + "=" + StringUtility.GetQueryString(Constants.QUERYSTR_USER_GUID)), "_blank");
                        GoWebPage();
                        isFirstSession = false; 
                    }
                }
                else //unable html 5 new ui
                {
                    string previewURL5 = temp[0].Replace("EditPage.aspx", CHANGETECHPAGEHTML5 + "?");                   
                    if (!isFirstSession)
                    {
                        HtmlPage.Window.Navigate(new Uri(previewURL5 + "Object=Page&Mode=Preview&" + Constants.QUERYSTR_PAGE_GUID + "=" + _pageGuid
                          + "&" + Constants.QUERYSTR_SESSION_GUID + "=" + StringUtility.GetQueryString(Constants.QUERYSTR_SESSION_GUID)
                          + "&" + Constants.QUERYSTR_PAGE_SEQUENCE_GUID + "=" + _pageSequenceGuid
                          + "&" + Constants.QUERYSTR_LANGUAGE_GUID + "=" + StringUtility.GetQueryString(Constants.QUERYSTR_LANGUAGE_GUID)
                          + "&" + Constants.QUERYSTR_USER_GUID + "=" + StringUtility.GetQueryString(Constants.QUERYSTR_USER_GUID)), "_blank");

                        HtmlPage.Window.Navigate(new Uri(previewURL + "Object=Page&Mode=Preview&" + Constants.QUERYSTR_PAGE_GUID + "=" + _pageGuid
                           + "&" + Constants.QUERYSTR_SESSION_GUID + "=" + StringUtility.GetQueryString(Constants.QUERYSTR_SESSION_GUID)
                           + "&" + Constants.QUERYSTR_PAGE_SEQUENCE_GUID + "=" + _pageSequenceGuid
                           + "&" + Constants.QUERYSTR_LANGUAGE_GUID + "=" + StringUtility.GetQueryString(Constants.QUERYSTR_LANGUAGE_GUID)
                           + "&" + Constants.QUERYSTR_USER_GUID + "=" + StringUtility.GetQueryString(Constants.QUERYSTR_USER_GUID)), "_blank");
                        GoWebPage();
                    }
                    else {
                        HtmlPage.Window.Navigate(new Uri(previewURL5 + "Object=Page&Mode=Preview&" + Constants.QUERYSTR_PAGE_GUID + "=" + _pageGuid
                           + "&" + Constants.QUERYSTR_SESSION_GUID + "=" + StringUtility.GetQueryString(Constants.QUERYSTR_SESSION_GUID)
                           + "&" + Constants.QUERYSTR_PAGE_SEQUENCE_GUID + "=" + _pageFirstSessionSequenceGuid
                           + "&" + Constants.QUERYSTR_LANGUAGE_GUID + "=" + StringUtility.GetQueryString(Constants.QUERYSTR_LANGUAGE_GUID)
                           + "&" + Constants.QUERYSTR_USER_GUID + "=" + StringUtility.GetQueryString(Constants.QUERYSTR_USER_GUID)), "_blank");

                        HtmlPage.Window.Navigate(new Uri(previewURL + "Object=Page&Mode=Preview&" + Constants.QUERYSTR_PAGE_GUID + "=" + _pageGuid
                           + "&" + Constants.QUERYSTR_SESSION_GUID + "=" + StringUtility.GetQueryString(Constants.QUERYSTR_SESSION_GUID)
                           + "&" + Constants.QUERYSTR_PAGE_SEQUENCE_GUID + "=" + _pageFirstSessionSequenceGuid
                           + "&" + Constants.QUERYSTR_LANGUAGE_GUID + "=" + StringUtility.GetQueryString(Constants.QUERYSTR_LANGUAGE_GUID)
                           + "&" + Constants.QUERYSTR_USER_GUID + "=" + StringUtility.GetQueryString(Constants.QUERYSTR_USER_GUID)), "_blank");
                        GoWebPage();
                        isFirstSession = false; 
                    }
                }
            }
        }

        void serviceProxy_GetFistSessionOfProgramCompleted(object sender, GetFistSessionOfProgramCompletedEventArgs e)
        {
            _pageFirstSessionSequenceGuid = e.Result;
            isFirstSession = true;
            ServiceClient serviceProxy = ServiceProxyFactory.Instance.ServiceProxy;
            serviceProxy.IsEnableHTML5NewUICompleted += new EventHandler<IsEnableHTML5NewUICompletedEventArgs>(serviceProxy_IsEnableHTML5NewUICompleted);
            serviceProxy.IsEnableHTML5NewUIAsync(_programGuid);
        }

        private void GoWebPage()
        {
            string documentURL = HtmlPage.Document.DocumentUri.ToString();
            documentURL = documentURL.Replace(StringUtility.GetQueryString(Constants.QUERYSTR_PAGE_SEQUENCE_GUID), _pageSequenceGuid.ToString());
            documentURL = documentURL.Replace(StringUtility.GetQueryString(Constants.QUERYSTR_PAGE_GUID), _pageGuid.ToString());
            HtmlPage.Window.Navigate(new Uri(documentURL));
        }

        private void serviceProxy_SavePageCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e, Guid pageGuid)
        {
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
                _pageGuid = pageGuid;
                if (_saveType == SaveType.Preview)
                {                   
                    ServiceClient serviceProxy = ServiceProxyFactory.Instance.ServiceProxy;
                    serviceProxy.IsEnableHTML5NewUICompleted += new EventHandler<IsEnableHTML5NewUICompletedEventArgs>(serviceProxy_IsEnableHTML5NewUICompleted);
                    serviceProxy.IsEnableHTML5NewUIAsync(_programGuid);
                }
                if (_saveType == SaveType.Save)
                {
                    HtmlPage.Window.Alert(Constants.MSG_SUCCESSFUL);
                    GoWebPage();
                    //GoBackWebPage();
                }
            }
        }

        private void GoBackWebPage()
        {
            string documentURL = HtmlPage.Document.DocumentUri.ToString();
            //string[] temp = documentURL.Split(new char[] { '?' });
            //string backUrl = temp[0].Replace("EditPage.aspx", "EditPageSequence.aspx?");
            //HtmlPage.Window.Navigate(new Uri(backUrl + Constants.QUERYSTR_SESSION_GUID + "=" + StringUtility.GetQueryString(Constants.QUERYSTR_SESSION_GUID) + "&"
            //    + Constants.QUERYSTR_PAGE_SEQUENCE_GUID + "=" + StringUtility.GetQueryString(Constants.QUERYSTR_PAGE_SEQUENCE_GUID)
            //    + "&" + Constants.QUERYSTR_LANGUAGE_GUID + "=" + StringUtility.GetQueryString(Constants.QUERYSTR_LANGUAGE_GUID)
            //    + "&DateTime=" + DateTime.UtcNow.Millisecond));
            string backurl = documentURL.Replace("EditPage.aspx", "EditPageSequence.aspx");
            HtmlPage.Window.Navigate(new Uri(backurl));
        }

        private void comboboxLanguage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //FormatPage();
        }

        private void PrviewButton_Click(object sender, RoutedEventArgs e)
        {
            Disable();
            string validateResult = ValidatePage();
            if (string.IsNullOrEmpty(validateResult))
            {
                _saveType = SaveType.Preview;
                if (IsEditPagesequenceOnly())
                {
                    _pageSequenceGuid = new Guid(StringUtility.GetQueryString(Constants.QUERYSTR_PAGE_SEQUENCE_GUID));
                    SavePage(new Guid(StringUtility.GetQueryString(Constants.QUERYSTR_PAGE_GUID)));
                }
                else
                {
                    bool affectFlag = false;
                    if (_isPageSequenceInMoreSession)
                    {
                        if (HtmlPage.Window.Confirm("This page is also used by other session, do you want to change it in other session?\n If you want to change in other session, press 'ok'\n otherwise press 'cancel'."))
                        {
                            affectFlag = true;
                        }
                        else
                        {
                            _isPageSequenceInMoreSession = false;
                        }
                    }
                    ServiceClient serviceProxy = ServiceProxyFactory.Instance.ServiceProxy;
                    serviceProxy.BeforeEditPageSequenceCompleted += new EventHandler<BeforeEditPageSequenceCompletedEventArgs>(serviceProxy_BeforeEditPageSequenceCompleted);
                    serviceProxy.BeforeEditPageSequenceAsync(new Guid(StringUtility.GetQueryString(Constants.QUERYSTR_SESSION_GUID)), new Guid(StringUtility.GetQueryString(Constants.QUERYSTR_PAGE_SEQUENCE_GUID)), affectFlag);
                }
            }
            else
            {
                HtmlPage.Window.Alert(validateResult);
                Enable();
            }
        }

        private void TemplateComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CollapseAllTemplate();
            PageTemplateModel selectedTemplate = ((PageTemplateModel)TemplateComboBox.SelectedItem);
            SimplePageModel pageModel = GetSimplePage(_originalPageModel);
            if (selectedTemplate.Name.Equals("Standard"))
            {
                StandardTemplate.Visibility = Visibility.Visible;
                StandardTemplate.PageContentModel.TemplateGUID = selectedTemplate.Guid;
                StandardTemplate.HeadingTextBox.Text = pageModel.Heading;
                StandardTemplate.BodyTextBox.Text = pageModel.Body;
                StandardTemplate.PrimaryButtonTextBox.Text = pageModel.PrimaryButtonCaption;
            }
            else if (selectedTemplate.Name.Equals("Get information"))
            {
                GetInformationTemplate.Visibility = Visibility.Visible;
                GetInformationTemplate.PageContentModel.TemplateGUID = selectedTemplate.Guid;
                GetInformationTemplate.HeadingTextBox.Text = pageModel.Heading;
                GetInformationTemplate.BodyTextBox.Text = pageModel.Body;
                GetInformationTemplate.PrimaryButtonTextBox.Text = pageModel.PrimaryButtonCaption;
            }
            else if (selectedTemplate.Name.Equals("Screening results"))
            {
                ScreeningResultsTemplate.Visibility = Visibility.Visible;
                ScreeningResultsTemplate.PageContentModel.TemplateGUID = selectedTemplate.Guid;
                ScreeningResultsTemplate.HeadingTextBox.Text = pageModel.Heading;
                ScreeningResultsTemplate.BodyTextBox.Text = pageModel.Body;
                ScreeningResultsTemplate.PrimaryButtonTextBox.Text = pageModel.PrimaryButtonCaption;
            }
            else if (selectedTemplate.Name.Equals("Push pictures"))
            {
                PushPicturesTemplate.Visibility = Visibility.Visible;
                PushPicturesTemplate.PageContentModel.TemplateGUID = selectedTemplate.Guid;
            }
            else if (selectedTemplate.Name.Equals("Choose preferences"))
            {
                ChoosePreferenceTemplate.Visibility = Visibility.Visible;
                ChoosePreferenceTemplate.PageContentModel.TemplateGUID = selectedTemplate.Guid;
                ChoosePreferenceTemplate.PrimaryButtonNameTextBox.Text = pageModel.PrimaryButtonCaption;
            }
            else if (selectedTemplate.Name.Equals("Timer"))
            {
                TimerTemplate.Visibility = Visibility.Visible;
                TimerTemplate.PageContentModel.TemplateGUID = selectedTemplate.Guid;
                TimerTemplate.titleTextBox.Text = pageModel.Heading;
                TimerTemplate.textTextBox.Text = pageModel.Body;
                TimerTemplate.buttonPrimaryNameTextBox.Text = pageModel.PrimaryButtonCaption;
            }
            else if (selectedTemplate.Name.Equals("Account creation"))
            {
                AccountCreationTemplate.Visibility = Visibility.Visible;
                AccountCreationTemplate.PageContentModel.TemplateGUID = selectedTemplate.Guid;
                AccountCreationTemplate.titleTextBox.Text = pageModel.Heading;
                AccountCreationTemplate.textTextBox.Text = pageModel.Body;
                AccountCreationTemplate.buttonPrimaryNameTextBox.Text = pageModel.PrimaryButtonCaption;
            }
            else if (selectedTemplate.Name.Equals("Graph"))
            {
                GraphTemplate.Visibility = Visibility.Visible;
                GraphTemplate.PageContentModel.TemplateGUID = selectedTemplate.Guid;
                GraphTemplate.titleTextBox.Text = pageModel.Heading;
                GraphTemplate.textTextBox.Text = pageModel.Body;
                GraphTemplate.buttonPrimaryNameTextBox.Text = pageModel.PrimaryButtonCaption;
            }
            else if (selectedTemplate.Name.Equals("SMS"))
            {
                SMSTemplate.Visibility = Visibility.Visible;
                SMSTemplate.PageContentModel.TemplateGUID = selectedTemplate.Guid;
                SMSTemplate.textTextBox.Text = pageModel.Body;
            }
        }

        private SimplePageModel GetSimplePage(EditPageModel _originalPageModel)
        {
            SimplePageModel simpleModel = new SimplePageModel();
            switch (_originalPageModel.Page.Template.Name)
            {
                case "Standard":
                    EditStandardTemplatePageContentModel standardModel = _originalPageModel.Page as EditStandardTemplatePageContentModel;

                    simpleModel.Body = standardModel.Body;
                    simpleModel.Heading = standardModel.Heading;
                    simpleModel.PrimaryButtonCaption = standardModel.PrimaryButtonCaption;

                    break;
                case "Get information":
                    EditGetInfoTemplatePageContentModel getInfoModel = _originalPageModel.Page as EditGetInfoTemplatePageContentModel;

                    simpleModel.Body = getInfoModel.Body;
                    simpleModel.Heading = getInfoModel.Heading;
                    simpleModel.PrimaryButtonCaption = getInfoModel.PrimaryButtonCaption;

                    break;
                case "Screening results":
                    EditScreenResultsTemplatePageContentModel screenResultsModel = _originalPageModel.Page as EditScreenResultsTemplatePageContentModel;

                    simpleModel.Body = screenResultsModel.Body;
                    simpleModel.Heading = screenResultsModel.Heading;
                    simpleModel.PrimaryButtonCaption = screenResultsModel.PrimaryButtonCaption;

                    break;
                case "Push pictures":
                    EditPushPictureTemplatePageContentModel pushPictureModel = _originalPageModel.Page as EditPushPictureTemplatePageContentModel;

                    simpleModel.Body = string.Empty;
                    simpleModel.Heading = string.Empty;
                    simpleModel.PrimaryButtonCaption = string.Empty;

                    break;
                case "Timer":
                    EditTimerTemplatePageContentModel timerModel = _originalPageModel.Page as EditTimerTemplatePageContentModel;

                    simpleModel.Body = timerModel.Text;
                    simpleModel.Heading = timerModel.Title;
                    simpleModel.PrimaryButtonCaption = timerModel.PrimaryButtonCaption;

                    break;
                case "Choose preferences":
                    EditChoosePreferencesTemplatePageContentModel choosePreferenceModel = _originalPageModel.Page as EditChoosePreferencesTemplatePageContentModel;

                    simpleModel.Body = string.Empty;
                    simpleModel.Heading = string.Empty;
                    simpleModel.PrimaryButtonCaption = choosePreferenceModel.PrimaryButtonName;

                    break;
                case "Account creation":
                    EditAccountCreationTemplatePageContentModel accountModel = _originalPageModel.Page as EditAccountCreationTemplatePageContentModel;

                    simpleModel.Body = accountModel.Body;
                    simpleModel.Heading = accountModel.Heading;
                    simpleModel.PrimaryButtonCaption = accountModel.PrimaryButtonCaption;

                    break;
                case "Graph":
                    EditGraphTemplatePageContentModel graphModel = _originalPageModel.Page as EditGraphTemplatePageContentModel;

                    simpleModel.Body = graphModel.Body;
                    simpleModel.Heading = graphModel.Heading;
                    simpleModel.PrimaryButtonCaption = graphModel.PrimaryButtonCaption;

                    break;

                case "SMS":
                    EditSMSTemplatePageContentModel smsModel = _originalPageModel.Page as EditSMSTemplatePageContentModel;

                    simpleModel.Body = smsModel.Text;
                    simpleModel.Heading = string.Empty;
                    simpleModel.PrimaryButtonCaption = string.Empty;

                    break;
            }
            return simpleModel;
        }

        private void CollapseAllTemplate()
        {
            StandardTemplate.Visibility = Visibility.Collapsed;
            GetInformationTemplate.Visibility = Visibility.Collapsed;
            PushPicturesTemplate.Visibility = Visibility.Collapsed;
            ChoosePreferenceTemplate.Visibility = Visibility.Collapsed;
            TimerTemplate.Visibility = Visibility.Collapsed;
            AccountCreationTemplate.Visibility = Visibility.Collapsed;
            GraphTemplate.Visibility = Visibility.Collapsed;
            ScreeningResultsTemplate.Visibility = Visibility.Collapsed;
            SMSTemplate.Visibility = Visibility.Collapsed;
        }

        private bool IsChangeTemplate()
        {
            bool flag = true;
            if (_originalPageModel.Page.Template.Name.Equals(GetCurrentTemplate().Name))
            {
                flag = false;
            }

            return flag;
        }
    }
}
