using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ChangeTech.Models
{
    [DataContract]
    public class SimplePageContentModel
    {
        [DataMember]
        public int Order { get; set; }
        [DataMember]
        public Guid ID { get; set; }
        [DataMember]
        public Guid PageSequenceGUID { get; set; }
        [DataMember]
        public string Heading { get; set; }
        [DataMember]
        public string Body { get; set; }
        [DataMember]
        public string Footer { get; set; }
        [DataMember]
        public string PrimaryButtonCaption { get; set; }
        [DataMember]
        public string PresenterImageUrl { get; set; }
        [DataMember]
        public Guid TemplateGUID { get; set; }
        [DataMember]
        public string TemplateName { get; set; }
        [DataMember]
        public int SequenceOrder { get; set; }
        [DataMember]
        public string BeforeShowExpression { get; set; }
        [DataMember]
        public string AfterShowExpression { get; set; }
        [DataMember]
        public UserModel LastUpdateBy { get; set; }
    }

    [DataContract]
    public class GraphTemplatePageContentModel
    {
        [DataMember]
        public Guid ProgramGUID { get; set; }
        [DataMember]
        public Guid TemplateGUID { get; set; }
        //[DataMember]
        //public Guid LanguageGUID { get; set; }
        [DataMember]
        public Guid SessionGUID { get; set; }
        [DataMember]
        public Guid PageSequenceGUID { get; set; }
        [DataMember]
        public int PageOrder { get; set; }
        [DataMember]
        public string Heading { get; set; }
        [DataMember]
        public string Body { get; set; }
        [DataMember]
        public string PrimaryButtonCaption { get; set; }
        [DataMember]
        public string GraphCaption { get; set; }
        [DataMember]
        public string GraphType { get; set; }
        [DataMember]
        public string ScoreRange { get; set; }
        [DataMember]
        public string GoodScoreRange { get; set; }
        [DataMember]
        public string MediumScoreRange { get; set; }
        [DataMember]
        public string BadScoreRange { get; set; }
        [DataMember]
        public string TimeRange { get; set; }
        [DataMember]
        public string TimeUnit { get; set; }
        [DataMember]
        public Guid GraphGUID { get; set; }
        [DataMember]
        public List<GraphItemModel> GraphItem { get; set; }
        [DataMember]
        public SortedList<Guid, ModelStatus> ObjectStatus { get; set; }
    }

    [DataContract]
    public class GraphItemModel
    {
        [DataMember]
        public Guid GraphGUID { get; set; }
        [DataMember]
        public Guid GraphItemModelGUID { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Color { get; set; }
        [DataMember]
        public int PointType { get; set; }
        [DataMember]
        public string Expression { get; set; }
    }

    [DataContract]
    public class ChoosePreferencesTemplatePageContentModel
    {
        [DataMember]
        public Guid TemplateGUID { get; set; }
        [DataMember]
        public Guid SessionGUID { get; set; }
        [DataMember]
        public Guid PageSequenceGUID { get; set; }
        [DataMember]
        public int MaxPreferences { get; set; }
        [DataMember]
        public string PrimaryButtonName { get; set; }
        [DataMember]
        public List<PreferenceItemModel> Preferences { get; set; }
        [DataMember]
        public SortedList<Guid, ModelStatus> PreferenceStatus { get; set; }
        [DataMember]
        public int PageOrder { get; set; }
        [DataMember]
        public Guid ProgramGuid { get; set; }
        [DataMember]
        public string AfterExpression { get; set; }
        [DataMember]
        public string BeforeExpression { get; set; }
    }

    [DataContract]
    public class EditChoosePreferencesTemplatePageContentModel : EditPageContentModelBase
    {
        [DataMember]
        public string PrimaryButtonName { get; set; }
        [DataMember]
        public int MaxPrefereneces { get; set; }
        [DataMember]
        public List<PreferenceItemModel> Preferences { get; set; }
    }

    [DataContract]
    public class PreferenceItemModel
    {
        [DataMember]
        public Guid PreferenceGUID { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string AnswerText { get; set; }
        [DataMember]
        public ResourceModel Resource { get; set; }
        [DataMember]
        public string ButtonName { get; set; }
        [DataMember]
        public SimplePageVariableModel Variable { get; set; }
    }

    [DataContract]
    [KnownType(typeof(EditStandardTemplatePageContentModel))]
    [KnownType(typeof(EditGetInfoTemplatePageContentModel))]
    [KnownType(typeof(EditScreenResultsTemplatePageContentModel))]
    [KnownType(typeof(EditPushPictureTemplatePageContentModel))]
    [KnownType(typeof(EditTimerTemplatePageContentModel))]
    [KnownType(typeof(EditAccountCreationTemplatePageContentModel))]
    [KnownType(typeof(EditChoosePreferencesTemplatePageContentModel))]
    [KnownType(typeof(EditGraphTemplatePageContentModel))]
    [KnownType(typeof(EditSMSTemplatePageContentModel))]
    public class EditPageContentModelBase
    {
        [DataMember]
        public PageTemplateModel Template { get; set; }
        [DataMember]
        public int OrderNo { get; set; }
        [DataMember]
        public string AfterExpression { get; set; }
        [DataMember]
        public string BeforeExpression { get; set; }
    }


    [DataContract]
    public class StandardTemplatePageContentModel
    {
        [DataMember]
        public Guid TemplateGUID { get; set; }
        //[DataMember]
        //public Guid LanguageGUID { get; set; }
        [DataMember]
        public Guid SessionGUID { get; set; }
        [DataMember]
        public Guid PageSequenceGUID { get; set; }
        [DataMember]
        public string Heading { get; set; }
        [DataMember]
        public string Body { get; set; }
        [DataMember]
        public Guid VideoGUID { get; set; }
        [DataMember]
        public Guid RadioGUID { get; set; }
        [DataMember]
        public string PrimaryButtonCaption { get; set; }
        [DataMember]
        public string PrimaryButtonAction { get; set; }
        [DataMember]
        public string PresenterImagePosition { get; set; }
        [DataMember]
        public string PresenterMode { get; set; }
        [DataMember]
        public int PageOrder { get; set; }
        [DataMember]
        public string AfterExpression { get; set; }
        [DataMember]
        public string BeforeExpression { get; set; }
        [DataMember]
        public Guid ProgramGuid { get; set; }
        [DataMember]
        public UserModel LastUpdateBy { get; set; }
        [DataMember]
        public Guid PresenterImageGUID { get; set; }
        [DataMember]
        public Guid IllustrationImageGUID { get; set; }
        [DataMember]
        public Guid BackgroundImageGUID { get; set; }
        [DataMember]
        public ImageModeEnum ImageMode { get; set; }
    }


    [DataContract]
    public class EditStandardTemplatePageContentModel : EditPageContentModelBase
    {
        [DataMember]
        public string Heading { get; set; }
        [DataMember]
        public string Body { get; set; }
        [DataMember]
        public PageMediaModel Media { get; set; }
        [DataMember]
        public ResourceModel BackgroudImage { get; set; }
        [DataMember]
        public ResourceModel PresenterImage { get; set; }
        [DataMember]
        public ResourceModel IllustrationImage { get; set; }
        [DataMember]
        public string PresenterImageMode { get; set; }
        [DataMember]
        public string PresenterImagePosition { get; set; }
        [DataMember]
        public string PrimaryButtonCaption { get; set; }
        [DataMember]
        public string PrimaryButtonAction { get; set; }
        [DataMember]
        public ImageModeEnum ImageMode { get; set; }
    }

    [DataContract]
    public class PushPictureTemplatePageContentModel
    {
        [DataMember]
        public Guid TemplateGUID { get; set; }
        //[DataMember]
        //public Guid LanguageGUID { get; set; }
        [DataMember]
        public Guid SessionGUID { get; set; }
        [DataMember]
        public Guid PageSequenceGUID { get; set; }
        [DataMember]
        public Guid PresenterImageGUID { get; set; }
        [DataMember]
        public Guid IllustrationImageGUID { get; set; }
        [DataMember]
        public Guid BackgroundImageGUID { get; set; }
        [DataMember]
        public ImageModeEnum ImageMode { get; set; }
        [DataMember]
        public string Wait { get; set; }
        [DataMember]
        public string Text { get; set; }
        [DataMember]
        public int PageOrder { get; set; }
        [DataMember]
        public Guid ProgramGuid { get; set; }
        [DataMember]
        public string AfterExpression { get; set; }
        [DataMember]
        public string BeforeExpression { get; set; }
        [DataMember]
        public Guid VoiceGUID { get; set; }
    }

    [DataContract]
    public class EditPushPictureTemplatePageContentModel : EditPageContentModelBase
    {
        [DataMember]
        public ResourceModel PresenterImage { get; set; }
        [DataMember]
        public ResourceModel BackgroudImage { get; set; }
        [DataMember]
        public ResourceModel IllustrationImage { get; set; }
        [DataMember]
        public ImageModeEnum ImageMode { get; set; }
        [DataMember]
        public string Wait { get; set; }
        [DataMember]
        public string Text { get; set; }
        [DataMember]
        public PageMediaModel Media { get; set; }
    }

    [DataContract]
    public class TimerTemplatePageContentModel
    {
        [DataMember]
        public Guid TemplateGUID { get; set; }
        //[DataMember]
        //public Guid LanguageGUID { get; set; }
        [DataMember]
        public Guid SessionGUID { get; set; }
        [DataMember]
        public Guid PageSequenceGUID { get; set; }
        [DataMember]
        public Guid PageVariableGUID { get; set; }
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public string Text { get; set; }
        [DataMember]
        public string PrimaryButtonCaption { get; set; }
        [DataMember]
        public string PrimaryButtonAction { get; set; }
        [DataMember]
        public int PageOrder { get; set; }
        [DataMember]
        public Guid ProgramGuid { get; set; }
        [DataMember]
        public string AfterExpression { get; set; }
        [DataMember]
        public string BeforeExpression { get; set; }
    }

    [DataContract]
    public class EditTimerTemplatePageContentModel : EditPageContentModelBase
    {
        [DataMember]
        public SimplePageVariableModel PageVariable { get; set; }
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public string Text { get; set; }
        [DataMember]
        public string PrimaryButtonCaption { get; set; }
        [DataMember]
        public string PrimaryButtonAction { get; set; }
    }

    [DataContract]
    public class AccountCreationTemplatePageContentModel
    {
        [DataMember]
        public Guid TemplateGUID { get; set; }
        //[DataMember]
        //public Guid LanguageGUID { get; set; }
        [DataMember]
        public Guid SessionGUID { get; set; }
        [DataMember]
        public Guid PageSequenceGUID { get; set; }
        [DataMember]
        public Guid PageVariableGUID { get; set; }
        [DataMember]
        public string Heading { get; set; }
        [DataMember]
        public string Body { get; set; }
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public string RepeatPassword { get; set; }
        [DataMember]
        public string Mobile { get; set; }
        [DataMember]
        public string SNText { get; set; }
        [DataMember]
        public string CheckBoxText { get; set; }
        [DataMember]
        public string PrimaryButtonCaption { get; set; }
        [DataMember]
        public string PrimaryButtonAction { get; set; }
        [DataMember]
        public int PageOrder { get; set; }
        [DataMember]
        public Guid ProgramGuid { get; set; }
        [DataMember]
        public string AfterExpression { get; set; }
        [DataMember]
        public string BeforeExpression { get; set; }
    }

    [DataContract]
    public class EditAccountCreationTemplatePageContentModel : EditPageContentModelBase
    {
        [DataMember]
        public SimplePageVariableModel PageVariable { get; set; }
        [DataMember]
        public string Heading { get; set; }
        [DataMember]
        public string Body { get; set; }
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public string RepeatPassword { get; set; }
        [DataMember]
        public string Mobile { get; set; }
        [DataMember]
        public string SNText { get; set; }
        [DataMember]
        public string CheckBoxText { get; set; }
        [DataMember]
        public string PrimaryButtonCaption { get; set; }
        [DataMember]
        public string PrimaryButtonAction { get; set; }
    }

    [DataContract]
    public class SMSTemplatePageContentModel
    {
        [DataMember]
        public Guid TemplateGUID { get; set; }
        [DataMember]
        public Guid SessionGUID { get; set; }
        [DataMember]
        public Guid PageSequenceGUID { get; set; }
        [DataMember]
        public int PageOrder { get; set; }
        [DataMember]
        public string Text { get; set; }
        [DataMember]
        public string Time { get; set; }
        [DataMember]
        public int DaysToSend { get; set; }
        [DataMember]
        public Guid PageVariableGUID { get; set; }
    }

    [DataContract]
    public class EditSMSTemplatePageContentModel : EditPageContentModelBase
    {
        [DataMember]
        public string Text { get; set; }
        [DataMember]
        public string Time { get; set; }
        [DataMember]
        public string DaysToSend { get; set; }
        [DataMember]
        public SimplePageVariableModel PageVariable { get; set; }
    }

    [DataContract]
    public class GetInfoTemplatePageContentModel
    {
        [DataMember]
        public Guid TemplateGUID { get; set; }
        //[DataMember]
        //public Guid LanguageGUID { get; set; }
        [DataMember]
        public Guid SessionGUID { get; set; }
        [DataMember]
        public Guid PageSequenceGUID { get; set; }
        [DataMember]
        public string Heading { get; set; }
        [DataMember]
        public string Body { get; set; }
        [DataMember]
        public string FooterText { get; set; }
        [DataMember]
        public string PrimaryButtonCaption { get; set; }
        [DataMember]
        public string PrimaryButtonAction { get; set; }
        [DataMember]
        public Guid PresenterImageGUID { get; set; }
        [DataMember]
        public Guid BackgroundImageGUID { get; set; }
        [DataMember]
        public Guid IllustrationImageGUID { get; set; }
        [DataMember]
        public ImageModeEnum ImageMode { get; set; }
        [DataMember]
        public string PresenterImagePosition { get; set; }
        [DataMember]
        public string PresenterMode { get; set; }
        [DataMember]
        public List<PageQuestionModel> PageQuestions { get; set; }
        [DataMember]
        public SortedList<Guid, ModelStatus> ObjectStatus { get; set; }
        [DataMember]
        public int PageOrder { get; set; }
        [DataMember]
        public Guid ProgramGuid { get; set; }
        [DataMember]
        public string AfterExpression { get; set; }
        [DataMember]
        public string BeforeExpression { get; set; }
        [DataMember]
        public UserModel LastUpdateBy { get; set; }
    }

    [DataContract]
    public class EditGetInfoTemplatePageContentModel : EditPageContentModelBase
    {
        [DataMember]
        public string Heading { get; set; }
        [DataMember]
        public string Body { get; set; }
        [DataMember]
        public string FooterText { get; set; }
        [DataMember]
        public ResourceModel PresenterImage { get; set; }
        [DataMember]
        public ResourceModel BackgroudImage { get; set; }
        [DataMember]
        public ResourceModel IllustrationImage { get; set; }
        [DataMember]
        public ImageModeEnum ImageMode { get; set; }
        [DataMember]
        public string PresenterImagePosition { get; set; }
        [DataMember]
        public string PrimaryButtonCaption { get; set; }
        [DataMember]
        public string SecondaryButtonCaption { get; set; }
        [DataMember]
        public string PrimaryButtonAction { get; set; }
        [DataMember]
        public List<PageQuestionModel> PageQuestions { get; set; }
        [DataMember]
        public List<QuestionModel> Questions { get; set; }
        [DataMember]
        public string PresenterMode { get; set; }
    }

    [DataContract]
    public class EditGraphTemplatePageContentModel : EditPageContentModelBase
    {
        [DataMember]
        public string Heading { get; set; }
        [DataMember]
        public string Body { get; set; }
        [DataMember]
        public string PrimaryButtonCaption { get; set; }
        [DataMember]
        public string GraphCaption { get; set; }
        [DataMember]
        public string GraphType { get; set; }
        [DataMember]
        public string ScoreRange { get; set; }
        [DataMember]
        public string GoodScoreRange { get; set; }
        [DataMember]
        public string MediumScoreRange { get; set; }
        [DataMember]
        public string BadScoreRange { get; set; }
        [DataMember]
        public string TimeRange { get; set; }
        [DataMember]
        public string TimeUnit { get; set; }
        [DataMember]
        public Guid GraphGUID { get; set; }
        [DataMember]
        public List<GraphItemModel> GraphItem { get; set; }
    }

    [DataContract]
    public class PageMediaModel
    {
        [DataMember]
        public string Type { get; set; }
        [DataMember]
        public ResourceModel Resource { get; set; }
    }

    /// <summary>
    /// Contain the data needed by adding page
    /// </summary>
    [DataContract]
    public class PageMaterials
    {
        [DataMember]
        public List<PageTemplateModel> PageTemplates { get; set; }
        [DataMember]
        public List<QuestionModel> Questions { get; set; }
    }

    [DataContract]
    public class PageTemplateModel
    {
        [DataMember]
        public Guid Guid { get; set; }
        [DataMember]
        public string Name { get; set; }
    }

    [DataContract]
    public class QuestionModel
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public Guid Guid { get; set; }
        [DataMember]
        public bool HasSubItem { get; set; }
    }

    [DataContract]
    public class PageQuestionModel
    {
        [DataMember]
        public Guid QuestionGuid { get; set; }
        [DataMember]
        public int Order { get; set; }
        [DataMember]
        public Guid Guid { get; set; }
        [DataMember]
        public List<PageQuestionItemModel> SubItems { get; set; }
        [DataMember]
        public string Caption { get; set; }
        [DataMember]
        public bool IsRequired { get; set; }
        [DataMember]
        public PageVariableModel PageVariable { get; set; }
        [DataMember]
        public string BeginContent { get; set; }
        [DataMember]
        public string EndContent { get; set; }
        [DataMember]
        public List<OrderObject> ItemOrderList { get; set; }
    }
    [DataContract]
    public class OrderObject
    {
        [DataMember]
        public int OrderNo { get; set; }
        [DataMember]
        public Guid PageQuestionGUID { get; set; }
        [DataMember]
        public Guid PageResultLineGUID { get; set; }
    }

    [DataContract]
    public class PageQuestionItemModel
    {
        [DataMember]
        public Guid Guid { get; set; }
        [DataMember]
        public int Order { get; set; }
        [DataMember]
        public string Item { get; set; }
        [DataMember]
        public string Feedback { get; set; }
        [DataMember]
        public int Score { get; set; }
    }

    /// <remarks>
    /// CHANGELOG:
    /// ...
    /// 2010-01-21: [Chen Pu]   Add property IsPageSequenceUsedInOtherSession, ProgramGUID
    /// 
    /// </remarks>
    [DataContract]
    public class EditPageModel
    {
        [DataMember]
        public EditPageContentModelBase Page { get; set; }
        [DataMember]
        public PageMaterials PageMaterials { get; set; }
        [DataMember]
        public string ProgramName { get; set; }
        [DataMember]
        public string SessionName { get; set; }
        [DataMember]
        public string PageSequenceName { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public bool IsPageSequenceUsedInOtherSession { get; set; }
        [DataMember]
        public Guid ProgramGUID { get; set; }
        [DataMember]
        public List<ProgramLanguageModel> ProgramLanguages { get; set; }
    }

    [KnownType(typeof(EditPageModel))]
    [DataContract]
    public class PageVariableModel
    {
        [DataMember]
        public Guid PageVariableGUID { get; set; }
        [DataMember]
        public Guid PageVariableGroupGUID { get; set; }
        [DataMember]
        public Guid ProgramGUID { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string ValueType { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string PageVariableType { get; set; }
        [DataMember]
        public string Value { get; set; }
        [DataMember]
        public int UsedTimes { get; set; }
        [DataMember]
        public List<PageQuestionModel> Questions { get; set; }
    }

    [DataContract]
    public class EditPageVariableModel : PageVariableModel
    {
        [DataMember]
        public ModelStatus modelStatus { get; set; }
    }

    //[KnownType(typeof(EditPageVariableGroupModel))]
    [DataContract]
    public class PageVariableGroupModel
    {
        [DataMember]
        public Guid PageVariableGroupGUID { get; set; }
        [DataMember]
        public Guid ProgramGUID { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Description { get; set; }
    }

    public class AccessoryPageModel
    {
        public Guid AccessoryTemplateGUID { get; set; }
        public Guid ProgramGUID { get; set; }
        //public Guid LanguageGUID { get; set; }
        public string Heading { get; set; }
        public string Text { get; set; }
        public string UserNameText { get; set; }
        public string PasswordText { get; set; }
        public string PrimaryButtonText { get; set; }
        public string SecondaryButtonText { get; set; }
        public string Type { get; set; }
    }

    public class AccessoryPageListModel
    {
        public List<AccessoryPageListModel> AccessoryPageList { get; set; }
    }

    [DataContract]
    public class PageReportModel
    {
        [DataMember]
        public string Type { get; set; }
        [DataMember]
        public int Order { get; set; }
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public string Text { get; set; }
        [DataMember]
        public string FooterText { get; set; }
        [DataMember]
        public string ButtonCaption { get; set; }
        [DataMember]
        public string BeforeShowExpression { get; set; }
        [DataMember]
        public string AfterShowExpression { get; set; }
    }

    [DataContract]
    public class PageContentForCTPPLastPageModel
    {
        [DataMember]
        public Guid PageGUID { get; set; }
        [DataMember]
        public string Heading { get; set; }
        [DataMember]
        public string Body { get; set; }
        [DataMember]
        public ResourceModel BackgroudImage { get; set; }
        [DataMember]
        public ResourceModel PresenterImage { get; set; }
        [DataMember]
        public string PresenterImageMode { get; set; }
        [DataMember]
        public string PresenterImagePosition { get; set; }


    }

    [DataContract]
    public class PageContentForPageReviewModel
    {
        [DataMember]
        public Guid PageGUID { get; set; }
        [DataMember]
        public string Heading { get; set; }
        [DataMember]
        public string Body { get; set; }
        [DataMember]
        public string PrimaryButtonCaption { get; set; }
        [DataMember]
        public Guid PresenterImageGUID { get; set; }
    }


    [DataContract]
    public class PageUpdateForPageReviewModel
    {
        [DataMember]
        public bool IsEditPageSequenceSelf { get; set; }//string.IsNullOrEmpty(Request.QueryString[Constants.QUERYSTR_EDITMODE])//refer to EditPageSequence.aspx.cs protected bool IsEditPageSequenceSelf()
        [DataMember]
        public bool IsUpdatePageSequence { get; set; }//refer to EditPageSequence.aspx.cs. private Guid BeforeEditPageSequence();
        [DataMember]
        public Guid SessionGUID { get; set; }
        [DataMember]
        public Guid PageSequenceGUID { get; set; }
        [DataMember]
        public Guid PageGUID { get; set; }
        [DataMember]
        public int PageOrder { get; set; }

        [DataMember]
        public string Heading { get; set; }
        [DataMember]
        public string Body { get; set; }
        [DataMember]
        public string PrimaryButtonCaption { get; set; }
        [DataMember]
        public Guid PresenterImageGUID { get; set; }
    }


    [DataContract]
    public class ScreenResultTemplatePageGraphModel
    {
        [DataMember]
        public Guid GraphGuid { get; set; }
        [DataMember]
        public Guid PageGuid { get; set; }
        [DataMember]
        public Guid ResourceGuid { get; set; }
        [DataMember]
        public Guid GraphTemplateGuid { get; set; }
        [DataMember]
        public float VariableMinValue { get; set; }
        [DataMember]
        public float VariableMaxValue { get; set; }
        [DataMember]
        public UserModel LastUpdateBy { get; set; }
    }

    [DataContract]
    public class ScreenResultTemplatePageLineModel
    {
        [DataMember]
        public Guid PageLineGuid { get; set; }
        [DataMember]
        public Guid PageGuid { get; set; }
        [DataMember]
        public Guid PageVariableGuid { get; set; }
        [DataMember]
        public PageVariableModel PageVariable { get; set; }
        [DataMember]
        public string Text { get; set; }
        [DataMember]
        public string URL { get; set; }
        [DataMember]
        public int Order { get; set; }
        [DataMember]
        public UserModel LastUpdateBy { get; set; }
    }

    [DataContract]
    public class GraphTemplateModel
    {
        [DataMember]
        public Guid GraphTemplateGuid { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public float VariableMinValue { get; set; }
        [DataMember]
        public float VariableMaxValue { get; set; }
    }

    [DataContract]
    public class EditScreenResultsTemplatePageContentModel :EditPageContentModelBase
    {
        [DataMember]
        public string Heading { get; set; }
        [DataMember]
        public string Body { get; set; }
        [DataMember]
        public ResourceModel PresenterImage { get; set; }
        [DataMember]
        public ResourceModel BackgroudImage { get; set; }
        [DataMember]
        public ResourceModel IllustrationImage { get; set; }
        [DataMember]
        public ImageModeEnum ImageMode { get; set; }
        [DataMember]
        public string PresenterImagePosition { get; set; }
        [DataMember]
        public string PrimaryButtonCaption { get; set; }
        [DataMember]
        public string SecondaryButtonCaption { get; set; }
        [DataMember]
        public string PrimaryButtonAction { get; set; }
        [DataMember]
        public List<ScreenResultTemplatePageLineModel> PageLines { get; set; }
        [DataMember]
        public string PresenterMode { get; set; }
        [DataMember]
        public Guid PageGraphic1GUID { get; set; }
        [DataMember]
        public Guid PageGraphic2GUID { get; set; }
        [DataMember]
        public Guid PageGraphic3GUID { get; set; }
        [DataMember]
        public ResourceModel PageGraphic1Image { get; set; }
        [DataMember]
        public ResourceModel PageGraphic2Image { get; set; }
        [DataMember]
        public ResourceModel PageGraphic3Image { get; set; }
        //[DataMember]
        //public List<ScreenResultTemplatePageGraphModel> PageGraphics { get; set; }
    }


    [DataContract]
    public class ScreenResultTemplatePageContentModel
    {
        [DataMember]
        public Guid TemplateGUID { get; set; }
        [DataMember]
        public Guid SessionGUID { get; set; }
        [DataMember]
        public Guid PageSequenceGUID { get; set; }
        [DataMember]
        public string Heading { get; set; }
        [DataMember]
        public string Body { get; set; }
        [DataMember]
        public string PrimaryButtonCaption { get; set; }
        [DataMember]
        public string PrimaryButtonAction { get; set; }
        [DataMember]
        public Guid PresenterImageGUID { get; set; }
        [DataMember]
        public Guid IllustrationImageGUID { get; set; }
        [DataMember]
        public Guid BackgroundImageGUID { get; set; }
        [DataMember]
        public ImageModeEnum ImageMode { get; set; }
        [DataMember]
        public string PresenterImagePosition { get; set; }
        [DataMember]
        public string PresenterMode { get; set; }
        [DataMember]
        public int PageOrder { get; set; }
        [DataMember]
        public Guid ProgramGuid { get; set; }
        [DataMember]
        public string AfterExpression { get; set; }
        [DataMember]
        public string BeforeExpression { get; set; }
        [DataMember]
        public UserModel LastUpdateBy { get; set; }
        //[DataMember]
        //public List<ScreenResultTemplatePageGraphModel> PageGraphics { get; set; }
        [DataMember]
        public Guid PageGraphic1GUID { get; set; }
        [DataMember]
        public Guid PageGraphic2GUID { get; set; }
        [DataMember]
        public Guid PageGraphic3GUID { get; set; }
        [DataMember]
        public List<ScreenResultTemplatePageLineModel> PageLines { get; set; }
        [DataMember]
        public SortedList<Guid, ModelStatus> ObjectStatus { get; set; }
    }

    public class SimplePageModel
    {
        public Guid PageGUID { get; set; }
        public Guid PageSequenceGUID { get; set; }
        public string Wait { get; set; }
        public int PageOrderNo { get; set; }
        public string PageHeading { get; set; }
        public int MaxPreferences { get; set; }
    }

}
