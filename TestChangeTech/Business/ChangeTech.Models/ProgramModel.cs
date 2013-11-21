using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.Serialization;

namespace ChangeTech.Models
{
    [DataContract]
    public class ProgramBaseModel
    {
        [DataMember]
        public Guid ProgramGuid { get; set; }
        [DataMember]
        public string Name { get; set; }
    }

    [DataContract]
    public class ProgramModel
    {
        [DataMember]
        public Guid Guid { get; set; }
        [DataMember]
        public string ProgramName { get; set; }
        [DataMember]
        public string NameInDeveloper { get; set; }
        [DataMember]
        public string ShortName { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public Guid StatusGuid { get; set; }
        [DataMember]
        public string StatusName { get; set; }
        [DataMember]
        public int NumberOfUsers { get; set; }
        [DataMember]
        public Guid DefaultLanguage { get; set; }
        [DataMember]
        public string DefaultLanguageName { get; set; }
        [DataMember]
        public ResourceModel ProgramLogo { get; set; }
        [DataMember]
        public DateTime Created { get; set; }
        [DataMember]
        public UserModel LastUpdateBy { get; set; }
        [DataMember]
        public DateTime LastUpdated { get; set; }
        [DataMember]
        public string ProjectManager { get; set; }
        [DataMember]
        public List<ProgramLanguageModel> languages { get; set; }
        [DataMember]
        public string ScreenURL { get; set; }
        [DataMember]
        public string LoginURL { get; set; }
        [DataMember]
        public int DaysCount { get; set; }
        [DataMember]
        public int StartDay { get; set; }
        [DataMember]
        public bool IsNeedPinCode { get; set; }
        [DataMember]
        public string Code { get; set; }
        [DataMember]
        public bool IsCTPPEnable { get; set; }
        [DataMember]
        public bool IsNoCatchUp { get; set; }
        [DataMember]
        public bool IsSupportTimeZone { get; set; }
        [DataMember]
        public decimal TimeZone { get; set; }
        [DataMember]
        public bool IsOrderProgram { get; set; }
        [DataMember]
        public bool IsHPOrderProgram { get; set; }
        [DataMember]
        public bool IsInvisibleStartButton { get; set; }
        [DataMember]
        public bool IsInvisibleDayAndSetMenu { get; set; }
        [DataMember]
        public string OrderProgramText { get; set; }
    }

    public class ProgramLanguageModel
    {
        public ProgramModel program { get; set; }
        public LanguageModel language { get; set; }
    }

    public class EditProgramModel
    {
        public Guid Guid { get; set; }
        public string ProgramName { get; set; }
        public string NameInDeveloper { get; set; }
        public string ShortName { get; set; }
        public string Description { get; set; }
        public bool IsNeedPinCode { get; set; }
        public List<KeyValuePair<Guid, string>> ProgramStatusList { get; set; }
        public string ProgramStatus { get; set; }
        public bool IsLiveProgram { get; set; }
        public string GeneralColor { get; set; }
        public Guid DefaultLanguage { get; set; }
        public ResourceModel ProgramLogo { get; set; }
        public List<SessionModel> Sessions { get; set; }
        public List<ProgramLanguageModel> languages { get; set; }
    }

    public class ProgramsModel : List<ProgramModel>
    {
        public ProgramsModel() { }

        public ProgramsModel(List<ProgramModel> programs)
        {
            AddRange(programs);
        }
    }

    public class SimpleProgramModel
    {
        public Guid ProgramGuid { get; set; }
        public string ProgramName { get; set; }
        public string Description { get; set; }
    }

    public class PrimaryButtonColorModel
    {
        public string normalFrom { get; set; }
        public string normalTo { get; set; }
        public string overFrom { get; set; }
        public string overTo { get; set; }
        public string downFrom { get; set; }
        public string downTo { get; set; }
        public string disableFrom { get; set; }
        public string disableTo { get; set; }
    }

    public class ProgramColorModel
    {
        public string GeneralColor { get; set; }
        public string CoverShadowColor { get; set; }
        public bool IsShadowVisible { get; set; }
    }

    public class ProgramScheduleModel
    {
        public int week { get; set; }
        public bool monday { get; set; }
        public bool tuesday { get; set; }
        public bool wednesday { get; set; }
        public bool thursday { get; set; }
        public bool friday { get; set; }
        public bool saterday { get; set; }
        public bool sunday { get; set; }
    }
    [DataContract]
    public class ProgramReportModel
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string Langeuage { get; set; }
        [DataMember]
        public List<SessionReportModel> Sessions { get; set; }
    }

    public class ProgramPropertyModel
    {
        public Guid ProgramGUID { get; set; }
        public string NameForDeveloper { get; set; }
        public bool IsNeedPinCode { get; set; }
        public bool IsNeedPayment { get; set; }
        public string Price { get; set; }
        public bool IsNeedCutConnect { get; set; }
        public string Weeks { get; set; }
        public bool IsContainTwoParts { get; set; }
        public int SwitchDay { get; set; }
        public bool IsSupportHttps { get; set; }
        public bool SeparateGender { get; set; }
        public string ProgramCode { get; set; }
        public bool IsNeedSerialNumber { get; set; }
        public string SupportEmail { get; set; }
        public string SupportName { get; set; }
        public bool IsCTPPEnable { get; set; }
        public bool IsHTML5PreviewEnable { get; set; }
        public bool EnableHTML5NewUI { get; set; }
        public bool IsNoCatchUp { get; set; }
        public bool IsSupportTimeZone { get; set; }
        public decimal TimeZone { get; set; }
        public int FlashOrHTML5 { get; set; }
        public bool IsOrderProgram { get; set; }
        public bool IsHPOrderProgram { get; set; }
        public bool IsInvisibleStartButton { get; set; }
        public bool IsInvisibleDayAndSetMenu { get; set; }
        public string OrderProgramText { get; set; }
        public ResourceModel ProgramLogo { get; set; }
        public string ShortName { get; set; }
    }

    public class CloneProgramParameterModel
    {
        public DefaultGuidSourceEnum source { get; set; }
        public Guid ProgramGuidOfCopiedToProgramsInDefaultLanguage { get; set; }
    }

    /// <summary>
    /// Now this is used for ChangeTech.html where to redirect
    /// </summary>
    public class RedirectInfoModel
    {
        public RedirectChoiceEnum RedirectChoice { get; set; } // reference to public enum RedirectChoiceEnum
        public string RedirectToCTPPURL { get; set; } //eg. http://program.changtech.no/ctpp.aspx?ctpp=....  without http://program.changtech.no/
        public bool IsNewHtml5UIEnable { get; set; }
    }

    public class ParametersForRedirectInfoModel
    {
        public string ProgramCodeOrGuid;
        public string Mode;
        public string Security;
    }

    [DataContract]
    public class ProgramCategoryModel
    {
        [DataMember]
        public Guid CategoryGuid { get; set; }
        [DataMember]
        public string CategoryName { get; set; }
        [DataMember]
        public List<ProgramInfoModel> Programs { get; set; }
    }

    [DataContract]
    public class ProgramInfoModel
    {
        [DataMember]
        public Guid ProgramGuid { get; set; }
        [DataMember]
        public string ProgramName { get; set; }
        [DataMember]
        public string ProgramImage { get; set; }
        [DataMember]
        public string SubTitle { get; set; }
        [DataMember]
        public string ProgramPrimaryColor { get; set; }
        [DataMember]
        public string ProgramSecondaryColor { get; set; }
        [DataMember]
        public string ProgramDescription { get; set; }
        [DataMember]
        public string OfferToken { get; set; }
        [DataMember]
        public bool IsPaid { get; set; }
        [DataMember]
        public int CurrentSessionNumber { get; set; }
        [DataMember]
        public int NumberOfSessions { get; set; }
        [DataMember]
        public string CurrentSessionUrl { get; set; }
        [DataMember]
        public decimal Price { get; set; }
        [DataMember]
        public string HelpUrl { get; set; }
        [DataMember]
        public string ReportUrl { get; set; }
        [DataMember]
        public bool IsCompleted { get; set; }
        [DataMember]
        public bool IsTakenCurrentSession { get; set; }
        [DataMember]
        public DateTime? CurrentSessionDate { get; set; }

        [DataMember]
        public string ProgramPurpose { get; set; }
        [DataMember]
        public string ProgramFunction { get; set; }
        [DataMember]
        public string ProgramPresenterImage { get; set; }
        [DataMember]
        public string ProgramPresenterSmallImageUrl { get; set; }

        [DataMember]
        public string ProductImage { get; set; }
        [DataMember]
        public string ProductImageLarge { get; set; }
        [DataMember]
        public string ProductImageSmall { get; set; }
        [DataMember]
        public string ProductInstructorImage { get; set; }
        [DataMember]
        public string ProductImagePresenter { get; set; }
    }

    [DataContract]
    public class ProgramContentModel
    {
        [DataMember]
        public Guid ProgramGuid { get; set; }
        [DataMember]
        public string ProgramName { get; set; }
        [DataMember]
        public string ProgramImage { get; set; }
        [DataMember]
        public string SubTitle { get; set; }
        [DataMember]
        public string ProgramPrimaryColor { get; set; }
        [DataMember]
        public string ProgramSecondaryColor { get; set; }
        [DataMember]
        public string ProgramDescription { get; set; }
        [DataMember]
        public string OfferToken { get; set; }
        [DataMember]
        public int CurrentSessionNumber { get; set; }
        [DataMember]
        public string CurrentSessionUrl { get; set; }
        [DataMember]
        public string HelpUrl { get; set; }
        [DataMember]
        public string ReportUrl { get; set; }
        [DataMember]
        public int NextSession { get; set; }
        [DataMember]
        public decimal Price { get; set; }
        [DataMember]
        public bool IsCompleted { get; set; }

        [DataMember]
        public string ProgramPurpose { get; set; }
        [DataMember]
        public string ProgramFunction { get; set; }
        [DataMember]
        public string ProgramPresenterSmallImageUrl { get; set; }

        [DataMember]
        public string ProductImage { get; set; }
        [DataMember]
        public string ProductDescription { get; set; }
        [DataMember]
        public string ProductInstructorImage { get; set; }

        [DataMember]
        public string ProductImageLarge { get; set; }
        [DataMember]
        public string ProductImageSmall { get; set; }
        [DataMember]
        public string ProductImagePresenter { get; set; }
        [DataMember]
        public string Screenshot1 { get; set; }
        [DataMember]
        public string Screenshot2 { get; set; }
        [DataMember]
        public string Screenshot3 { get; set; }
        [DataMember]
        public string SessionText { get; set; }

        [DataMember]
        public int? NotificationTime { get; set; }

        [DataMember]
        public string ProgramPresenterImage { get; set; }
        [DataMember]
        public string FactHeader1  { get; set; }
        [DataMember]
        public string FactHeader2 { get; set; }
        [DataMember]
        public string FactHeader3 { get; set; }
        [DataMember]
        public string FactHeader4 { get; set; }
        [DataMember]
        public string FactContent1 { get; set; }
        [DataMember]
        public string FactContent2 { get; set; }
        [DataMember]
        public string FactContent3 { get; set; }
        [DataMember]
        public string FactContent4 { get; set; }
        [DataMember]
        public string ProgramDescriptionFromCTPP { get; set; }
        [DataMember]
        public string ProgramDescriptionTitleFromCTPP { get; set; }
        [DataMember]
        public string ProgramDescriptionForMobileFromCTPP { get; set; }
        [DataMember]
        public string ProgramDescriptionTitleForMobileFromCTPP { get; set; }
        [DataMember]
        public List<SessionInfoModel> Sessions { get; set; }
    }

    [DataContract]
    public class SessionInfoModel
    {
        [DataMember]
        public Guid SessionGuid { get; set; }
        [DataMember]
        public string SessionName { get; set; }
        [DataMember]
        public int SessionDay { get; set; }
        [DataMember]
        public bool IsTaken { get; set; }
        [DataMember]
        public SessionStatusEnum HasAvailableSession { get; set; }
        [DataMember]
        public string SessionDescription { get; set; }
        [DataMember]
        public string RunSessionUrl { get; set; }
        [DataMember]
        public string NotificationText { get; set; }
        [DataMember]
        public DateTime? NotificationDate { get; set; }
        [DataMember]
        public List<ResourceInfoModel> Resources { get; set; }
    }

    [DataContract]
    public class ResourceInfoModel
    {
        [DataMember]
        public string ResourceName { get; set; }
        [DataMember]
        public string ResourceType { get; set; }
        [DataMember]
        public string ReourceURL { get; set; }
    }

    [DataContract]
    public class RegisterWin8ProgramUsersModel
    {
        [DataMember]
        public string WindowsLiveId { get; set; }
        [DataMember]
        public List<string> ProgramIds { get; set; }
    }

    [DataContract]
    public class RegisterMessageModel
    {
        [DataMember]
        public JoinProgramResultEnum Result { get; set; }
        [DataMember]
        public string ErrorMessage { get; set; }
    }

    public class OperateModel
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}