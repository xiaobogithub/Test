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
using System.Windows.Browser;
using ChangeTech.Silverlight.Common;

namespace ChangeTech.Silverlight.DesignPage
{
    public partial class NewPage : Page
    {
        public const string CHANGETECHPAGEFLASH = "ChangeTechF.html";
        public const string CHANGETECHPAGEHTML5 = "ChangeTech5.html";
        public const string CHANGETECHPAGEHTML5R = "ChangeTech5r.html";
        Guid ProgramGuid;
        Guid PageSequenceGuid;
        Guid _pageGuid;
        bool IsPageSequenceInMoreSession;

        private Guid _pageFirstSessionSequenceGuid;
        private bool isFirstSession = false;//used the first session in serviceProxy_GetFistSessionOfProgramCompleted function

        public NewPage()
        {
            InitializeComponent();
        }

        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            PageSequenceGuid = new Guid(StringUtility.GetQueryString(Constants.QUERYSTR_PAGE_SEQUENCE_GUID));

            ServiceClient serviceProxy = ServiceProxyFactory.Instance.ServiceProxy;
            serviceProxy.GetPageMaterialsCompleted += new EventHandler<GetPageMaterialsCompletedEventArgs>(serviceProxy_GetPageMaterialsCompleted);
            serviceProxy.GetPageMaterialsAsync();

            serviceProxy.GetCountOfPagesInPageSequenceCompleted += new EventHandler<GetCountOfPagesInPageSequenceCompletedEventArgs>(serviceProxy_GetCountOfPagesInPageSequenceCompleted);
            serviceProxy.GetCountOfPagesInPageSequenceAsync(new Guid(StringUtility.GetQueryString(Constants.QUERYSTR_PAGE_SEQUENCE_GUID)));

            if (IsEditPagesequenceOnly())
            {
                ProgramGuid = new Guid(StringUtility.GetQueryString(Constants.QUERYSTR_PROGRAM_GUID));
            }
            else
            {
                serviceProxy.GetSessionCompleted += new EventHandler<GetSessionCompletedEventArgs>(sc_GetSessionCompleted);
                serviceProxy.GetSessionAsync(new Guid(StringUtility.GetQueryString(Constants.QUERYSTR_SESSION_GUID)));

                serviceProxy.IsPageSequenceUsedInSameProgramButNotInSameSessionCompleted += new EventHandler<IsPageSequenceUsedInSameProgramButNotInSameSessionCompletedEventArgs>(serviceProxy_IsPageSequenceUsedInSameProgramButNotInSameSessionCompleted);
                serviceProxy.IsPageSequenceUsedInSameProgramButNotInSameSessionAsync(new Guid(StringUtility.GetQueryString(Constants.QUERYSTR_SESSION_GUID)), new Guid(StringUtility.GetQueryString(Constants.QUERYSTR_PAGE_SEQUENCE_GUID)));
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

        void serviceProxy_IsPageSequenceUsedInSameProgramButNotInSameSessionCompleted(object sender, IsPageSequenceUsedInSameProgramButNotInSameSessionCompletedEventArgs e)
        {
            IsPageSequenceInMoreSession = e.Result;
        }

        void serviceProxy_GetCountOfPagesInPageSequenceCompleted(object sender, GetCountOfPagesInPageSequenceCompletedEventArgs e)
        {
            cbPageNO.Items.Clear();
            for (int i = 1; i <= e.Result + 1; i++)
            {
                cbPageNO.Items.Add(i);
            }

            cbPageNO.SelectedIndex = e.Result;
        }

        private void serviceProxy_GetPageMaterialsCompleted(object sender, GetPageMaterialsCompletedEventArgs e)
        {
            if (e.Result != null)
            {
                PageMaterials pageMaterials = e.Result;
                cbTemplate.ItemsSource = pageMaterials.PageTemplates;

                GetInformationTemplate.Questions = pageMaterials.Questions;
            }
        }

        private void cbTemplate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CollapseAllTemplate();
            PageTemplateModel selectedTemplate = ((PageTemplateModel)cbTemplate.SelectedItem);
            if (selectedTemplate.Name.Equals("Standard"))
            {
                StandardTemplate.Visibility = Visibility.Visible;
                StandardTemplate.PageContentModel.TemplateGUID = selectedTemplate.Guid;
            }
            else if (selectedTemplate.Name.Equals("Get information"))
            {
                GetInformationTemplate.Visibility = Visibility.Visible;
                GetInformationTemplate.PageContentModel.TemplateGUID = selectedTemplate.Guid;
            }
            else if (selectedTemplate.Name.Equals("Screening results"))
            {
                ScreeningResultsTemplate.Visibility = Visibility.Visible;
                ScreeningResultsTemplate.PageContentModel.TemplateGUID = selectedTemplate.Guid;
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
            }
            else if (selectedTemplate.Name.Equals("Timer"))
            {
                TimerTemplate.Visibility = Visibility.Visible;
                TimerTemplate.PageContentModel.TemplateGUID = selectedTemplate.Guid;
            }
            else if (selectedTemplate.Name.Equals("Account creation"))
            {
                AccountCreationTemplate.Visibility = Visibility.Visible;
                AccountCreationTemplate.PageContentModel.TemplateGUID = selectedTemplate.Guid;
            }
            else if (selectedTemplate.Name.Equals("Graph"))
            {
                GraphTemplate.Visibility = Visibility.Visible;
                GraphTemplate.PageContentModel.TemplateGUID = selectedTemplate.Guid;
            }
            else if (selectedTemplate.Name.Equals("SMS"))
            {
                SMSTemplate.Visibility = Visibility.Visible;
                SMSTemplate.PageContentModel.TemplateGUID = selectedTemplate.Guid;
            }
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

        void sc_GetSessionCompleted(object sender, GetSessionCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                HtmlPage.Window.Alert("Operation is cancelled.");
            }
            else if (e.Error != null)
            {
                HtmlPage.Window.Alert(Constants.ERROR_INTERNAL);
            }
            else
            {
                ProgramGuid = ((EditSessionModel)e.Result).ProgramGuid;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Disable();
            if (cbTemplate.SelectedItem != null)
            {
                string validateStr = ValidatePage();
                if (string.IsNullOrEmpty(validateStr))
                {
                    if (IsEditPagesequenceOnly())
                    {
                        SavePage(new Guid(StringUtility.GetQueryString(Constants.QUERYSTR_PAGE_SEQUENCE_GUID)));
                    }
                    else
                    {
                        bool affectFlag = false;
                        if (IsPageSequenceInMoreSession)
                        {
                            if (HtmlPage.Window.Confirm("Do you want to imfact other session which used this pageSquence?\n if 'yes' press 'ok'\n if 'no' press 'cancel'"))
                            {
                                affectFlag = true;
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
                    HtmlPage.Window.Alert(validateStr);
                    Enable();
                }
            }
            else
            {
                HtmlPage.Window.Alert("Please choose one template to create page.");
                Enable();
            }
        }

        void serviceProxy_BeforeEditPageSequenceCompleted(object sender, BeforeEditPageSequenceCompletedEventArgs e)
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
                PageSequenceGuid = e.Result;

                SavePage(PageSequenceGuid);
            }
        }

        private void SavePage(Guid pagesequenceguid)
        {
            PageTemplateModel selectedTemplate = ((PageTemplateModel)cbTemplate.SelectedItem);
            ServiceClient serviceProxy = ServiceProxyFactory.Instance.ServiceProxy;
            string validateMessage = string.Empty;
            int order = cbPageNO.SelectedIndex;
            switch (selectedTemplate.Name)
            {
                case "Standard":
                    StandardTemplate.FillContent();
                    StandardTemplate.DisableControl("Saveing");
                    StandardTemplate.PageContentModel.ProgramGuid = ProgramGuid;
                    StandardTemplate.PageContentModel.PageOrder = cbPageNO.SelectedIndex + 1;
                    StandardTemplate.PageContentModel.PageSequenceGUID = pagesequenceguid;
                    SetImageModeForStandardTemplatePageContentModel("StandardTemplate");
                    serviceProxy.SaveStandardTemplatePageCompleted += new EventHandler<SaveStandardTemplatePageCompletedEventArgs>(serviceProxy_SaveStandardTemplatePageCompleted);
                    //serviceProxy.SaveStandardTemplatePageCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(serviceProxy_SavePageCompleted);
                    serviceProxy.SaveStandardTemplatePageAsync(StandardTemplate.PageContentModel);
                    break;
                case "Get information":
                    GetInformationTemplate.FillContent();
                    GetInformationTemplate.DisableControl("Saving");
                    GetInformationTemplate.PageContentModel.ProgramGuid = ProgramGuid;
                    GetInformationTemplate.PageContentModel.PageOrder = cbPageNO.SelectedIndex + 1;
                    GetInformationTemplate.PageContentModel.PageSequenceGUID = pagesequenceguid;
                    SetImageModeForStandardTemplatePageContentModel("GetInformationTemplate");
                    serviceProxy.SaveGetInfoTemplatePageCompleted += new EventHandler<SaveGetInfoTemplatePageCompletedEventArgs>(serviceProxy_SaveGetInfoTemplatePageCompleted);
                    //serviceProxy.SaveGetInfoTemplatePageCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(serviceProxy_SavePageCompleted);
                    serviceProxy.SaveGetInfoTemplatePageAsync(GetInformationTemplate.PageContentModel);
                    break;
                case "Screening results":
                    ScreeningResultsTemplate.FillContent();
                    ScreeningResultsTemplate.DisableControl("Saving");
                    ScreeningResultsTemplate.PageContentModel.ProgramGuid = ProgramGuid;
                    ScreeningResultsTemplate.PageContentModel.PageOrder = cbPageNO.SelectedIndex + 1;
                    ScreeningResultsTemplate.PageContentModel.PageSequenceGUID = pagesequenceguid;
                    SetImageModeForStandardTemplatePageContentModel("ScreeningResultsTemplate");
                    serviceProxy.SaveScreenResultsTemplatePageCompleted += new EventHandler<SaveScreenResultsTemplatePageCompletedEventArgs>(serviceProxy_SaveScreenResultsTemplatePageCompleted);
                    //serviceProxy.SaveGetInfoTemplatePageCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(serviceProxy_SavePageCompleted);
                    serviceProxy.SaveScreenResultsTemplatePageAsync(ScreeningResultsTemplate.PageContentModel);
                    break;
                case "Push pictures":
                    PushPicturesTemplate.FillContent();
                    PushPicturesTemplate.DiableControl("Saving");
                    PushPicturesTemplate.PageContentModel.ProgramGuid = ProgramGuid;
                    PushPicturesTemplate.PageContentModel.PageOrder = cbPageNO.SelectedIndex + 1;
                    PushPicturesTemplate.PageContentModel.PageSequenceGUID = pagesequenceguid;
                    serviceProxy.SavePushPicturesTemplatePageCompleted += new EventHandler<SavePushPicturesTemplatePageCompletedEventArgs>(serviceProxy_SavePushPicturesTemplatePageCompleted);
                    //serviceProxy.SavePushPicturesTemplatePageCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(serviceProxy_SavePageCompleted);
                    serviceProxy.SavePushPicturesTemplatePageAsync(PushPicturesTemplate.PageContentModel);
                    break;
                case "Timer":
                    TimerTemplate.FillContent();
                    TimerTemplate.DisableControl("Saving");
                    TimerTemplate.PageContentModel.ProgramGuid = ProgramGuid;
                    TimerTemplate.PageContentModel.PageOrder = cbPageNO.SelectedIndex + 1;
                    TimerTemplate.PageContentModel.PageSequenceGUID = pagesequenceguid;
                    serviceProxy.SaveTimerTemplatePageCompleted += new EventHandler<SaveTimerTemplatePageCompletedEventArgs>(serviceProxy_SaveTimerTemplatePageCompleted);
                    //serviceProxy.SaveTimerTemplatePageCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(serviceProxy_SavePageCompleted);
                    serviceProxy.SaveTimerTemplatePageAsync(TimerTemplate.PageContentModel);
                    break;
                case "Choose preferences":
                    ChoosePreferenceTemplate.FillContent();
                    ChoosePreferenceTemplate.DisableControl("Saving");
                    ChoosePreferenceTemplate.PageContentModel.ProgramGuid = ProgramGuid;
                    ChoosePreferenceTemplate.PageContentModel.PageOrder = cbPageNO.SelectedIndex + 1;
                    ChoosePreferenceTemplate.PageContentModel.PageSequenceGUID = pagesequenceguid;
                    serviceProxy.SaveChoosePreferenceTemplatePageContentModelCompleted += new EventHandler<SaveChoosePreferenceTemplatePageContentModelCompletedEventArgs>(serviceProxy_SaveChoosePreferenceTemplatePageContentModelCompleted);
                    //serviceProxy.SaveChoosePreferenceTemplatePageContentModelCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(serviceProxy_SavePageCompleted);
                    serviceProxy.SaveChoosePreferenceTemplatePageContentModelAsync(ChoosePreferenceTemplate.PageContentModel);
                    break;
                case "Account creation":
                    AccountCreationTemplate.FillContent();
                    AccountCreationTemplate.DisableControl("Saving");
                    AccountCreationTemplate.PageContentModel.ProgramGuid = ProgramGuid;
                    AccountCreationTemplate.PageContentModel.PageOrder = cbPageNO.SelectedIndex + 1;
                    AccountCreationTemplate.PageContentModel.PageSequenceGUID = pagesequenceguid;
                    //AccountCreationTemplate.AddPageQuestion();
                    serviceProxy.SaveAccountCreationTemplatePageCompleted += new EventHandler<SaveAccountCreationTemplatePageCompletedEventArgs>(serviceProxy_SaveAccountCreationTemplatePageCompleted);
                    serviceProxy.SaveAccountCreationTemplatePageAsync(AccountCreationTemplate.PageContentModel);
                    break;
                case "Graph":
                    GraphTemplate.FillContent();
                    GraphTemplate.DisableControl("Saving");
                    GraphTemplate.PageContentModel.ProgramGUID = ProgramGuid;
                    GraphTemplate.PageContentModel.PageOrder = cbPageNO.SelectedIndex + 1;
                    GraphTemplate.PageContentModel.PageSequenceGUID = pagesequenceguid;
                    serviceProxy.SaveGraphTemplatePageContentModelCompleted += new EventHandler<SaveGraphTemplatePageContentModelCompletedEventArgs>(serviceProxy_SaveGraphTemplatePageContentModelCompleted);
                    serviceProxy.SaveGraphTemplatePageContentModelAsync(GraphTemplate.PageContentModel);
                    break;
                case "SMS":
                    SMSTemplate.FillContent();
                    SMSTemplate.DisableControl("Saving");
                    SMSTemplate.PageContentModel.PageOrder = cbPageNO.SelectedIndex + 1;
                    SMSTemplate.PageContentModel.PageSequenceGUID = pagesequenceguid;
                    serviceProxy.SaveSMSTemplatePageCompleted += new EventHandler<SaveSMSTemplatePageCompletedEventArgs>(serviceProxy_SaveSMSTemplatePageCompleted);
                    serviceProxy.SaveSMSTemplatePageAsync(SMSTemplate.PageContentModel);
                    break;
                default:
                    HtmlPage.Window.Alert(string.Format("Unrecgonized template {0}.", selectedTemplate.Name));
                    break;
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

        private void Disable()
        {
            cbTemplate.IsEnabled = false;
            cbPageNO.IsEnabled = false;
            SaveButton.IsEnabled = false;
            CancelButton.IsEnabled = false;
        }

        private void Enable()
        {
            cbTemplate.IsEnabled = true;
            cbPageNO.IsEnabled = true;
            SaveButton.IsEnabled = true;
            CancelButton.IsEnabled = true;
        }

        private string ValidatePage()
        {
            PageTemplateModel selectedTemplate = ((PageTemplateModel)cbTemplate.SelectedItem);
            string validateMessage = string.Empty;
            switch (selectedTemplate.Name)
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
                    validateMessage = string.Format("Unrecgonized template {0}.", selectedTemplate.Name);
                    break;
            }
            return validateMessage;
        }

        void serviceProxy_SaveSMSTemplatePageCompleted(object sender, SaveSMSTemplatePageCompletedEventArgs e)
        {
            SMSTemplate.EnableControl();
            Enable();
            serviceProxy_SavePageCompleted(sender, e, e.Result);
        }

        void serviceProxy_SaveGraphTemplatePageContentModelCompleted(object sender, SaveGraphTemplatePageContentModelCompletedEventArgs e)
        {
            GraphTemplate.EnableControl();
            Enable();
            serviceProxy_SavePageCompleted(sender, e, e.Result);
        }

        void serviceProxy_SaveAccountCreationTemplatePageCompleted(object sender, SaveAccountCreationTemplatePageCompletedEventArgs e)
        {
            AccountCreationTemplate.EnableControl();
            Enable();
            serviceProxy_SavePageCompleted(sender, e, e.Result);
        }

        void serviceProxy_SaveScreenResultsTemplatePageCompleted(object sender, SaveScreenResultsTemplatePageCompletedEventArgs e)
        {
            ScreeningResultsTemplate.EnableControl();
            Enable();
            serviceProxy_SavePageCompleted(sender, e, e.Result);
        }      

        void serviceProxy_SaveChoosePreferenceTemplatePageContentModelCompleted(object sender, SaveChoosePreferenceTemplatePageContentModelCompletedEventArgs e)
        {
            ChoosePreferenceTemplate.EnableControl();
            Enable();
            serviceProxy_SavePageCompleted(sender, e, e.Result);
        }

        void serviceProxy_SaveTimerTemplatePageCompleted(object sender, SaveTimerTemplatePageCompletedEventArgs e)
        {
            TimerTemplate.EnableControl();
            Enable();
            serviceProxy_SavePageCompleted(sender, e, e.Result);
        }

        void serviceProxy_SaveStandardTemplatePageCompleted(object sender, SaveStandardTemplatePageCompletedEventArgs e)
        {
            StandardTemplate.EnableControl();
            Enable();
            serviceProxy_SavePageCompleted(sender, e, e.Result);
        }

        void serviceProxy_SaveGetInfoTemplatePageCompleted(object sender, SaveGetInfoTemplatePageCompletedEventArgs e)
        {
            GetInformationTemplate.EnableControl();
            Enable();
            serviceProxy_SavePageCompleted(sender, e, e.Result);
        }

        void serviceProxy_SavePushPicturesTemplatePageCompleted(object sender, SavePushPicturesTemplatePageCompletedEventArgs e)
        {
            PushPicturesTemplate.EnableControl();
            Enable();
            serviceProxy_SavePageCompleted(sender, e, e.Result);
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            GoBackWebPage();
        }

        private void GoBackWebPage()
        {
            string documentURL = HtmlPage.Document.DocumentUri.ToString();
            if (IsEditPagesequenceOnly())
            {
                HtmlPage.Window.Navigate(new Uri(documentURL.Replace("AddPage.aspx", "EditPageSequence.aspx")));
            }
            else
            {
                string[] temp = documentURL.Split(new char[] { '?' });
                string backUrl = temp[0].Replace("AddPage.aspx", "EditPageSequence.aspx?");
                HtmlPage.Window.Navigate(new Uri(backUrl + Constants.QUERYSTR_SESSION_GUID + "=" + StringUtility.GetQueryString(Constants.QUERYSTR_SESSION_GUID) + "&"
                    + Constants.QUERYSTR_PAGE_SEQUENCE_GUID + "=" + PageSequenceGuid
                    + "&" + Constants.QUERYSTR_LANGUAGE_GUID + "=" + StringUtility.GetQueryString(Constants.QUERYSTR_LANGUAGE_GUID)
                    + "&" + Constants.QUERYSTR_USER_GUID + "=" + StringUtility.GetQueryString(Constants.QUERYSTR_USER_GUID) +
                    "&DateTime=" + DateTime.UtcNow.Millisecond));
            }
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
                //HtmlPage.Window.Alert("This page has been saved successfully.");
                if (!IsEditPagesequenceOnly())
                {                    
                    ServiceClient serviceProxy = ServiceProxyFactory.Instance.ServiceProxy;
                    serviceProxy.IsEnableHTML5NewUICompleted += new EventHandler<IsEnableHTML5NewUICompletedEventArgs>(serviceProxy_IsEnableHTML5NewUICompleted);
                    serviceProxy.IsEnableHTML5NewUIAsync(new Guid(StringUtility.GetQueryString(Constants.QUERYSTR_PROGRAM_GUID)));
                }
                else
                {
                    ServiceClient serviceProxy = ServiceProxyFactory.Instance.ServiceProxy;
                    serviceProxy.GetFistSessionOfProgramCompleted += new EventHandler<GetFistSessionOfProgramCompletedEventArgs>(serviceProxy_GetFistSessionOfProgramCompleted);
                    serviceProxy.GetFistSessionOfProgramAsync(new Guid(StringUtility.GetQueryString(Constants.QUERYSTR_PROGRAM_GUID)));
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
                string backUrl = temp[0].Replace("AddPage.aspx", CHANGETECHPAGEFLASH + "?");
                if (e.Result)
                {
                    string backUrl5 = temp[0].Replace("AddPage.aspx", CHANGETECHPAGEHTML5R + "?");
                    if (!isFirstSession)
                    {
                        HtmlPage.Window.Navigate(new Uri(backUrl5 + "Object=Page&Mode=Preview&" + Constants.QUERYSTR_PAGE_GUID + "=" + _pageGuid
                           + "&" + Constants.QUERYSTR_SESSION_GUID + "=" + StringUtility.GetQueryString(Constants.QUERYSTR_SESSION_GUID)
                           + "&" + Constants.QUERYSTR_PAGE_SEQUENCE_GUID + "=" + PageSequenceGuid
                           + "&" + Constants.QUERYSTR_LANGUAGE_GUID + "=" + StringUtility.GetQueryString(Constants.QUERYSTR_LANGUAGE_GUID)
                           + "&" + Constants.QUERYSTR_USER_GUID + "=" + StringUtility.GetQueryString(Constants.QUERYSTR_USER_GUID)), "_blank");
                        
                        HtmlPage.Window.Navigate(new Uri(backUrl + "Object=Page&Mode=Preview&" + Constants.QUERYSTR_PAGE_GUID + "=" + _pageGuid
                          + "&" + Constants.QUERYSTR_SESSION_GUID + "=" + StringUtility.GetQueryString(Constants.QUERYSTR_SESSION_GUID)
                          + "&" + Constants.QUERYSTR_PAGE_SEQUENCE_GUID + "=" + PageSequenceGuid
                          + "&" + Constants.QUERYSTR_LANGUAGE_GUID + "=" + StringUtility.GetQueryString(Constants.QUERYSTR_LANGUAGE_GUID)
                          + "&" + Constants.QUERYSTR_USER_GUID + "=" + StringUtility.GetQueryString(Constants.QUERYSTR_USER_GUID)), "_blank");
                        GoBackWebPage();
                    }
                    else {
                        HtmlPage.Window.Navigate(new Uri(backUrl5 + "Object=Page&Mode=Preview&" + Constants.QUERYSTR_PAGE_GUID + "=" + _pageGuid
                          + "&" + Constants.QUERYSTR_SESSION_GUID + "=" + StringUtility.GetQueryString(Constants.QUERYSTR_SESSION_GUID)
                          + "&" + Constants.QUERYSTR_PAGE_SEQUENCE_GUID + "=" + _pageFirstSessionSequenceGuid
                          + "&" + Constants.QUERYSTR_LANGUAGE_GUID + "=" + StringUtility.GetQueryString(Constants.QUERYSTR_LANGUAGE_GUID)
                          + "&" + Constants.QUERYSTR_USER_GUID + "=" + StringUtility.GetQueryString(Constants.QUERYSTR_USER_GUID)), "_blank");

                        HtmlPage.Window.Navigate(new Uri(backUrl + "Object=Page&Mode=Preview&" + Constants.QUERYSTR_PAGE_GUID + "=" + _pageGuid
                          + "&" + Constants.QUERYSTR_SESSION_GUID + "=" + StringUtility.GetQueryString(Constants.QUERYSTR_SESSION_GUID)
                          + "&" + Constants.QUERYSTR_PAGE_SEQUENCE_GUID + "=" + _pageFirstSessionSequenceGuid
                          + "&" + Constants.QUERYSTR_LANGUAGE_GUID + "=" + StringUtility.GetQueryString(Constants.QUERYSTR_LANGUAGE_GUID)
                          + "&" + Constants.QUERYSTR_USER_GUID + "=" + StringUtility.GetQueryString(Constants.QUERYSTR_USER_GUID)), "_blank");
                        GoBackWebPage();
                        isFirstSession = false;
                    }
                }
                else
                {
                    string backUrl5 = temp[0].Replace("AddPage.aspx", CHANGETECHPAGEHTML5 + "?");
                    if (!isFirstSession)
                    {
                        HtmlPage.Window.Navigate(new Uri(backUrl5 + "Object=Page&Mode=Preview&" + Constants.QUERYSTR_PAGE_GUID + "=" + _pageGuid
                           + "&" + Constants.QUERYSTR_SESSION_GUID + "=" + StringUtility.GetQueryString(Constants.QUERYSTR_SESSION_GUID)
                           + "&" + Constants.QUERYSTR_PAGE_SEQUENCE_GUID + "=" + PageSequenceGuid
                           + "&" + Constants.QUERYSTR_LANGUAGE_GUID + "=" + StringUtility.GetQueryString(Constants.QUERYSTR_LANGUAGE_GUID)
                           + "&" + Constants.QUERYSTR_USER_GUID + "=" + StringUtility.GetQueryString(Constants.QUERYSTR_USER_GUID)), "_blank");
                        GoBackWebPage();
                    }
                    else {
                      
                            HtmlPage.Window.Navigate(new Uri(backUrl5 + "Object=Page&Mode=Preview&" + Constants.QUERYSTR_PAGE_GUID + "=" + _pageGuid
                               + "&" + Constants.QUERYSTR_SESSION_GUID + "=" + StringUtility.GetQueryString(Constants.QUERYSTR_SESSION_GUID)
                               + "&" + Constants.QUERYSTR_PAGE_SEQUENCE_GUID + "=" + _pageFirstSessionSequenceGuid
                               + "&" + Constants.QUERYSTR_LANGUAGE_GUID + "=" + StringUtility.GetQueryString(Constants.QUERYSTR_LANGUAGE_GUID)
                               + "&" + Constants.QUERYSTR_USER_GUID + "=" + StringUtility.GetQueryString(Constants.QUERYSTR_USER_GUID)), "_blank");
                            GoBackWebPage();
                      
                    }
                }
            }
        }

        void serviceProxy_GetFistSessionOfProgramCompleted(object sender, GetFistSessionOfProgramCompletedEventArgs e)
        {
            isFirstSession = true;
            _pageFirstSessionSequenceGuid = e.Result;           
            ServiceClient serviceProxy = ServiceProxyFactory.Instance.ServiceProxy;
            serviceProxy.IsEnableHTML5NewUICompleted += new EventHandler<IsEnableHTML5NewUICompletedEventArgs>(serviceProxy_IsEnableHTML5NewUICompleted);
            serviceProxy.IsEnableHTML5NewUIAsync(new Guid(StringUtility.GetQueryString(Constants.QUERYSTR_PROGRAM_GUID)));
        }
    }
}
