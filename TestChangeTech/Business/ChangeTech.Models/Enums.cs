using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.Serialization;

namespace ChangeTech.Models
{
    public enum ProgramStausEnum : int
    {
        UnderDevelopment = 1,
        Live = 2
    }

    [Flags]
    [DataContract]
    public enum PermissionEnum
    {
        [EnumMember()]
        ApplicationNone = 0x0,
        [EnumMember()]
        ApplicationAdmin = 0x1,
        [EnumMember()]
        ApplicationCreate = 0x2,
        [EnumMember()]
        ApplicationEdit = 0x4,
        [EnumMember()]
        ApplicationDelete = 0x8,
        [EnumMember()]
        ApplicationSuperAdmin = 0xF,

        [EnumMember()]
        ProgramNone = 0x10,
        [EnumMember()]
        ProgramCreate = 0x20,
        [EnumMember()]
        ProgramEdit = 0x40,
        [EnumMember()]
        ProgramDelete = 0x80,
        [EnumMember()]
        ProgramAdmin = 0x100,
        [EnumMember()]
        ProgramView = 0x200,
    }

    public enum QestionTypeEnum
    {
        TimePickerHM,
        TimePickerDH
    }

    public enum GenderEnum : int
    {
        Male = 0,
        Female = 1,
        Empty = 2,
    }

    public enum ResultEnum
    {
        Success = 0,            // no error
        Error = 1
    }

    //[Flags]
    [DataContract]
    public enum ResourceTypeEnum
    {
        [EnumMember()]
        Video,
        [EnumMember()]
        Audio,
        [EnumMember()]
        Document,
        [EnumMember()]
        Image,
        [EnumMember()]
        Logo
    }

    public enum LogTypeEnum
    {
        NoLog = 0,
        SubmitPage = 1,
        Login = 2,
        EndDay = 3,
        PageAssignment = 4,
        StartDay = 5,
        SendReminderEmail = 6,
        PageStart = 7,
        PageEnd = 8,
        PageSequenceStart = 9,
        PageSequenceEnd = 10,
        SendPinCodeSM = 11,
        SendShortMessage = 12,
        RelapseStart = 13,
        RelapseEnd = 14,
        SetShortMessage = 15,
        ModifyProgramUser = 16,
        SendEmailToCustomer = 17,
        ModifyPageVariable = 18,
        RegisterWin8ProgramUser = 19,
        CopyTipMessage = 20,
        UpdateProgramUser = 21,
        GetCurrentTimeByTimeZone = 22,
        LFAuthenticateTimeOut = 23,
        DecryptGoWebURL=24,
        AddProgram = 101,
        ModifyProgram = 102,
        DeleteProgram = 103,
        AddDay = 111,
        ModifyDay = 112,
        DeleteDay = 113,
        AddPageSequence = 121,
        ModifyPageSequence = 122,
        DeletePageSequence = 123,
        AddPage = 131,
        ModifyPage = 132,
        DeletePage = 133,
        MonitorWorkerRoleEmail = 141,
        MonitorWorkerRoleStatus = 142,
        AddTranslationJob = 150,
        AddOrder = 161,
        CancelOrder = 162,
        ExpiredOrder = 163,
        LFAuthenticateSuccess = 171,
        LFAuthenticateFail = 172,
        LFAuthenticateException = 173
    }

    public enum UserTypeEnum
    {
        All = 0,
        User = 1,
        Administrator = 2, // super admin
        Tester = 3,
        Customer = 4,
        ProjectManager = 5,
        ProgramAdministrator = 6, // program admin
        ProgramEditor = 7,
        SupportPersonell = 8,
        Translator = 9,
        Monitor = 10,
        Reseller = 11,
        HPReseller = 12
    }

    public enum OrderStatusEnum : int
    {
        Active = 1,
        Cancel = 2,
        Expired = 3
    }

    public enum OrderTypeEnum
    {
        Simplelicence,
        Openlicence
    }

    //public enum OrderErrorMessageEnum
    //{
    //    OrderHasExpired,
    //    OrderHasCancelled,
    //    NoOrderLicence
    //}

    public enum LoginFailedTypeEnum
    {
        UserNotExisted,
        HaveDoneAllClassed,
        HaveFinishedTodaysClass,
        NoClassYet,
        NeedToJoinProgram,
        PasswordWrong,
        WaitUntilNextModay,
        InvalidateCompany,
        PinCodeWrong,
        NoSchedule,
        ProgramIsPaused,
        ErrorExist,
        NoScheduleForCurrentSession,
        OrderHasExpired,
        OrderHasCancelled,
        NoOrderLicence
    }

    [DataContract]
    public enum JoinProgramResultEnum
    {
        [EnumMember()]
        RegisteredInProgram,
        [EnumMember()]
        RegisteredButPasswordWrong,
        [EnumMember()]
        InvalidEmailAddress,
        [EnumMember()]
        InvalidMobile,
        [EnumMember()]
        Success,
        [EnumMember()]
        RegisteredFailed
    }

    public enum ProgramUserStatusEnum
    {
        Screening,
        Registered,
        Active,
        Terminated,
        Completed,
        Paused
    }

    public enum ApplicationStatusEnum
    {
        OnProcess = 1,
        Approved = 2,
        Declined = 3
    }

    public enum BlobContainerTypeEnum
    {
        OriginalImageContainer,
        ThumnailContainer,
        LogoContainer,
        VideoContainer,
        AudioContainer,
        DocumentContainer,
        ExportContainer
    }

    public enum PictureModeEnum
    {
        Crop,
        Normal,
        Big
    }

    public enum OutlineDaysEnum
    {
        Wed = -5,
        Thu = -4,
        Fri = -3,
        Sta = -2,
        Sun = -1
    }

    public enum SMTypeEnum
    {
        PinCode
    }

    public enum EmailTypeEnum
    {
        Reminder,
        Welcome,
        InviteFriend
    }

    public enum TranslationJobStatusEnum
    {
        NotStart = 0,
        InProgress = 1,
        Stopped = 2,
        Changed = 3,
        Finished = 4,
        Cancelled = 5
    }

    public enum DefaultContentInTranslatedFieldEnum
    {
        OriginalText = 1,
        GoogleTranslation = 2,
        Nothing = 3
    }
    
    /// <summary>
    /// if this is changed, need change the RedirectChoiceFor together
    /// </summary>
    public enum FlashOrHtml5Enum
    {
        FlashOnly = 0,      //Default Choice.  This means: uses Flash on all browsers
        AutoDetection = 1,  //This means: uses HTML5 if browser supports it, if not Flash will be used
        HTML5Only = 2       //This means: Uses HTML5 on compatible browsers, message is shown to user if browsers does not support HTML5
    }

    /// <summary>
    /// 0,1,2 need be equal to FlashOrHtml5 all the time
    /// </summary>
    public enum RedirectChoiceEnum : int
    {
        /// <summary>
        /// 0,1,2 belongs to the old enum FlashOrHtml5
        /// </summary>
        FlashOnly = 0,      //Default Choice.  This means: uses Flash on all browsers
        AutoDetection = 1,  //This means: uses HTML5 if browser supports it, if not Flash will be used
        HTML5Only = 2,      //This means: Uses HTML5 on compatible browsers, message is shown to user if browsers does not support HTML5


        ToCTPPWithQueryStringCTPP = 11
    }


    /// <summary>
    /// DefaultGuidSource defines where the new copied default guid come from.
    /// eg. 1. Add language, 1.1 the copied from language is default language, then choose FromPrimaryKey; 
    /// 1.2 the from language is not default language,then choose FromDefaultGuid, it means fromlanguage's defaultGuid will be assigned as the new datatable's defaultGuid
    /// 2. Copy language. 2.1, the copy to program is default program, then choose FromNull, it means the new program is defaulst program and the new datatable's defaultGuid should be set null.
    /// 2.2, the copy to program is not default program, then choose FromMatchDefaultGuidFunction. it need a function to find the default guid.
    /// </summary>
    public enum DefaultGuidSourceEnum
    {
        FromPrimaryKey = 0,
        FromDefaultGuid = 1,
        FromNull = 2,
        FromMatchDefaultGuidFunction = 3
    }

    //These string(Consider case) is the same as the value saved in PageVariable.ValueType column
    //When need update this, need update Silverlight.Common.cs.VariableTypeEnum
    public enum VariableTypeEnum
    {
        Program = 0,
        General = 1
    }

    public enum CTPPRelapseEnum : int
    {
        HelpButtonRelapse = 1,
        ReportButtonRelapse = 2
    }

    public enum UserTaskTypeEnum
    {
        Login = 0,
        TakeSession = 1,
        RetakeSession = 2,
        HelpInCTPP = 3,
        ReportInCTPP = 4,
        RegisteOfOrderSystem = 5,
        RegisteOfHPOrderSystem=6
    }

    /// <summary>
    /// PCVersion is default version.
    /// </summary>
    public enum CTPPVersionEnum : int
    {
        PCVersion = 0,
        SmartphoneVersion
    }

    [DataContract]
    public enum ModelStatus
    {
        [EnumMember()]
        QuestionNoChange,
        [EnumMember()]
        QuestionUpdated,
        [EnumMember()]
        QuestionDeleted,
        [EnumMember()]
        QuestionAdded,
        [EnumMember()]
        QuestionItemNoChange,
        [EnumMember()]
        QuestionItemUpdated,
        [EnumMember()]
        QuestionItemDeleted,
        [EnumMember()]
        QuestionItemAdded,
        [EnumMember()]
        ModelAdd,
        [EnumMember()]
        ModelEdit,
        [EnumMember()]
        ModelNoChange,
        [EnumMember()]
        ModelDelete,
        [EnumMember()]
        GraphItemAdded,
        [EnumMember()]
        GraphItemUpdated,
        [EnumMember()]
        GraphItemDeleted,
        [EnumMember()]
        PageLineNoChange,
        [EnumMember()]
        PageLineAdded,
        [EnumMember()]
        PageLineUpdated,
        [EnumMember()]
        PageLineDeleted
    }

    public enum OriginalPageEnum
    {
        CTPP = 0,
        Program = 1
    }

    public enum HelpAndReportButtonDisplaySettingEnum : int
    {
        InVisible = 0,
        Actual = 1,
        Complete = 2,
        UnAvailable = 3
    }

    public enum EmailTemplateTypeEnum : int
    {
        ProgramEmail=1,
        OrderEmail=2
    }

    public enum ReturnMessageCodeEnum
    {
        ErrorCode=0,
        SuccessCode=1,
        CtppCode=2,
        SessionEnding=3
    }

    [DataContract]
    public enum SessionStatusEnum
    {
        [EnumMember()]
        NOTDONE,
        [EnumMember()]
        DONE,
        [EnumMember()]
        STARTTODO
    }

    [DataContract]
    public enum ImageModeEnum
    {
        [EnumMember()]
        PresenterMode,
        [EnumMember()]
        FullscreenMode,
        [EnumMember()]
        IllustrationMode
    }

    public enum ResultTypeEnum
    {
        ResultLine = 0
    }

}
