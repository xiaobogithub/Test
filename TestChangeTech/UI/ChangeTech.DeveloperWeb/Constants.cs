using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChangeTech.DeveloperWeb
{
    public static class Constants
    {
        public static readonly string QUERYSTR_PROGRAM_GUID = "ProgramGUID";
        // page number of the current program
        public static readonly string QUERYSTR_PROGRAM_PAGE = "ProgramPg";
        public static readonly string QUERYSTR_PROGRAM_ROOM_GUID = "ProgramRoomGUID";
        public static readonly string QUERYSTR_SESSION_GUID = "SessionGUID";
        public static readonly string QUERYSTR_GUID = "GUID";
        // page number of the current session
        public static readonly string QUERYSTR_SESSION_PAGE = "SessionPg";
        public static readonly string QUERYSTR_PAGE_GUID = "PgGUID";
        public static readonly string QUERYSTR_PAGE_ORDER = "PgOrder";
        // page number of the current page
        public static readonly string QUERYSTR_PAGE_PAGE = "PgPg";
        public static readonly string QUERYSTR_PAGE_SEQUENCE_GUID = "PgSequenceGUID";
        // page number of the current page sequence
        public static readonly string QUERYSTR_PAGE_SEQUENCE_PAGE = "PgSequencePg";
        public static readonly string QUERYSTR_LANGUAGE_GUID = "LanguageGUID";
        public static readonly string QUERYSTR_INTERVENT_GUID = "InterventGUID";
        public static readonly string QUERYSTR_INTERVENTCATEGORY_GUID = "InterventCategoryGUID";
        public static readonly string QUERYSTR_PREDICTOR_GUID = "PredictorGUID";
        public static readonly string QUERYSTR_USER_GUID = "UserGUID";
        public static readonly string QUERYSTR_USER_EMAIL = "UserEmail";
        public static readonly string QUERYSTR_USER_MOBILE = "Mobile";
        public static readonly string QUERYSTR_DAY = "Day";
        public static readonly string QUERYSTR_OPTION = "Option";
        public static readonly string QUERYSTR_MODE = "Mode";
        public static readonly string QUERYSTR_TYPE = "Type";
        public static readonly string QUERYSTR_ACCESSORY_GUID = "AccessoryGUID";
        public static readonly string QUERYSTR_TIPMESSAGE_GUID = "TipMessageGUID";
        public static readonly string QUERYSTR_HELPITEM_GUID = "HelpItemGUID";
        public static readonly string QUERYSTR_SECURITY = "Security";
        public static readonly string QUERYSTR_SECURITY_ORDER = "SecurityOrder";
        public static readonly string MD5_KEY = "psycholo";
        public static readonly string QUERYSTR_USERTYPE = "UserType";
        public static readonly string QUERYSTR_USERGROUP_GUID = "UserGroup";
        public static readonly string QUERYSTR_USERSTATUS = "UserStatus";
        public static readonly string QUERYSTR_FROMWEBSITE = "FromWebSite";
        public static readonly string QUERYSTR_MENUITEM_GUID = "MenuItemGUID";
        public static readonly string QUERYSTR_ACTIVITY_TYPE = "ActivityType";
        public static readonly string QUERYSTR_READONLY = "ReadOnly";
        public static readonly string QUERYSTR_EDITMODE = "EditMode";
        public static readonly string QUERYSTR_PREVIOUSPAGE = "PrePg";
        public static readonly string QUERYSTR_QUERY = "Query";
        public static readonly string SELF = "Self";
        public static readonly string LAYOUT_SETTING = "<Settings><Setting Name=\"BackgroundColor\" Value=\"0xFFFFFF\" /><Setting Name=\"TopBarHeight\" Value=\"5\" /><Setting Name=\"BackgroundTopShadowColors\" Value=\"0xD7D7CA,0xD7D7CA,0xD7D7CA\" /><Setting Name=\"BackgroundTopShadowAlphas\" Value=\"1,0.3,0\" /><Setting Name=\"BackgroundTopShadowRatios\" Value=\"0,95,255\" /><Setting Name=\"BackgroundTopShadowHeight\" Value=\"120\" /><Setting Name=\"BackgroundTopCornerColors\" Value=\"0xD7D7CA,0xD7D7CA,0xD7D7CA\" /><Setting Name=\"BackgroundTopCornerAlphas\" Value=\"1,0.3,0\" /><Setting Name=\"BackgroundTopCornerRatios\" Value=\"0,160,255\" /><Setting Name=\"BackgroundTopCornerGradientWidth\" Value=\"200\" /><Setting Name=\"BackgroundTopCornerGradientHeight\" Value=\"200\" /><Setting Name=\"BackgroundTopCornerWidth\" Value=\"400\" /><Setting Name=\"BackgroundTopCornerHeight\" Value=\"400\" /><Setting Name=\"BackgroundBottomShadowColors\" Value=\"0xFFFFFF,0xD0D0C1\" /><Setting Name=\"BackgroundBottomShadowAlphas\" Value=\"1,1\" /><Setting Name=\"BackgroundBottomShadowRatios\" Value=\"0,255\" /><Setting Name=\"BackgroundBottomShadowHeight\" Value=\"310\" /><Setting Name=\"BackgroundBottomCornerColors\" Value=\"0xC6C3BB,0xD0CEC1,0xD3D1C4\" /><Setting Name=\"BackgroundBottomCornerAlphas\" Value=\"1,0.3,0\" /><Setting Name=\"BackgroundBottomCornerRatios\" Value=\"0,160,255\" /><Setting Name=\"BackgroundBottomCornerGradientWidth\" Value=\"300\" /><Setting Name=\"BackgroundBottomCornerGradientHeight\" Value=\"200\" /><Setting Name=\"BackgroundBottomCornerWidth\" Value=\"425\" /><Setting Name=\"BackgroundBottomCornerHeight\" Value=\"455\" /><Setting Name=\"LogoLeftMargin\" Value=\"30\" /><Setting Name=\"LogoTopMargin\" Value=\"18\" /><Setting Name=\"LogoMaxHeight\" Value=\"60\" /><Setting Name=\"ToolBarRightMargin\" Value=\"30\" /><Setting Name=\"ToolBarTopMargin\" Value=\"18\" /><Setting Name=\"ToolBarInnerMargin\" Value=\"20\" /><Setting Name=\"SettingMenuWidth\" Value=\"175\" /><Setting Name=\"ProgramButtonRightMargin\" Value=\"30\" /><Setting Name=\"ProgramButtonButtomMargin\" Value=\"25\" /><Setting Name=\"ProgramButtonMinWidth\" Value=\"200\" /><Setting Name=\"ProgramButtonTextHorizontalMargin\" Value=\"25\" /><Setting Name=\"TextFieldWidth\" Value=\"590\" /><Setting Name=\"TextFieldMaxHeight\" Value=\"360\" /><Setting Name=\"TextFieldMinHeight\" Value=\"200\" /><Setting Name=\"TextFieldPadding\" Value=\"15\" /><Setting Name=\"TextFieldTextSpacing\" Value=\"20\" /><Setting Name=\"TextFieldScrollBarWidth\" Value=\"20\" /><Setting Name=\"NavigationBarOverlapX\" Value=\"570\" /><Setting Name=\"NavigationBarOverlapY\" Value=\"20\" /><Setting Name=\"NavigationBarWidth\" Value=\"360\" /><Setting Name=\"PrimaryButtonColorNormal\" Value=\"0xFDFBFB,0x0A8342\" /><Setting Name=\"PrimaryButtonColorOver\" Value=\"0xCCC8C8,0x096F31\" /><Setting Name=\"PrimaryButtonColorDown\" Value=\"0x5CAB3C,0x0A8342\" /><Setting Name=\"PrimaryButtonColorDisable\" Value=\"0x5CAB3C,0x0A8342\" /><Setting Name=\"PresenterImageHeight\" Value=\"513\" /><Setting Name=\"PresenterImageMaxWidth\" Value=\"300\" /><Setting Name=\"PresenterImageVerticalTopDiff\" Value=\"33\" /><Setting Name=\"PresenterImageHorizontalOverlap\" Value=\"10\" /><Setting Name=\"CoverShadowHorizontalOverlap\" Value=\"795\" /><Setting Name=\"CoverShadowEnterHorizontalOverlap\" Value=\"205\" /><Setting Name=\"RoomLoaderMixWidth\" Value=\"200\" /><Setting Name=\"RoomLoaderTextHorizontalMargin\" Value=\"20\" /><Setting Name=\"MessageBoxWidth\" Value=\"500\" /><Setting Name=\"MessageBoxMinHeight\" Value=\"200\" /><Setting Name=\"MessageBoxMaxHeight\" Value=\"400\" /><Setting Name=\"MessageBoxTextMargin\" Value=\"15\" /><Setting Name=\"MessageBoxScrollBarWidth\" Value=\"20\" /><Setting Name=\"MessageBoxImageWidthMaxPercentage\" Value=\"0.5\" /><Setting Name=\"TipFriendSubmitButtonWidth\" Value=\"100\" /><Setting Name=\"ProfileChangeSubmitButtonWidth\" Value=\"100\" /><Setting Name=\"PauseProgramSubmitButtonWidth\" Value=\"100\" /><Setting Name=\"ExitProgramSubmitButtonWidth\" Value=\"200\" /><Setting Name=\"MediaWidthPercentage\" Value=\"1\" /><Setting Name=\"GraphMinHeight\" Value=\"300\" /><Setting Name=\"DemoMaxWidth\" Value=\"200\" /></Settings>";
        public static readonly string QUERYSTR_BEGINTIME = "BeginTime";
        public static readonly string QUERYSTR_ENDTIME = "EndTime";
        public static readonly string QUERYSTR_COMPANY_RIGHT_GUID = "CompanyRightGUID";
        public static readonly string QUERYSTR_COMPANY_GUID = "CompanyGUID";
        // can't use Page as it use "Page=" in PagingSortingService
        public static readonly string QUERYSTR_COMPANY_PAGE = "CompanyPgs";
        public static readonly string QUERYSTR_SPECIALSTRING_NAME = "SpecialStringName";
        public static readonly string QUERYSTR_FILE_GUID = "FileGUID";
        public static readonly string QUERYSTR_PROFRAM_USER_GUID = "PUID";
        public static readonly string QUERYSTR_PROGRAM_SHORT_NAME = "Program";
        public static readonly string QUERYSTR_PROGRAM_CODE = "P";
        public static readonly string QUERYSTR_PROGRAM_NAME = "PName";
        public static readonly string QUERYSTR_STUDY_GUID = "StudyGUID";
        public static readonly string QUERYSTR_STUDY_SHORT_NAME = "S";
        public static readonly string QUERYSTR_STUDY_CONTENT_GUID = "SC";
        public static readonly string QUERYSTR_COMPANY_CODE = "C";
        public static readonly string QUERYSTR_BRAND_GUID = "BrandGUID";
        public static readonly string QUERYSTR_HP_SECURITY = "HpSecurity";
        public static readonly string QUERYSTR_HPORDER_CODE = "code";
        public static readonly string EMAILTEMPLATETYEP_HP_ORDER = "HP Order Email";

        public static readonly string QUERYSTR_TRANSLATION_JOB_GUID = "TranslationJobGUID";
        public static readonly string QUERYSTR_TRANSLATION_JOB_CONTENT_GUID = "TranslationJobContentGUID";

        public static readonly string QUERYSTR_PAYMENT_TRANSATION_ID = "PaymentTransationID";

        public static readonly string FLASHXMLATTR_PAGE_GUID = "PageGUID";
        public static readonly string FLASHXMLATTR_PAGE_SEQUENCE_GUID = "PageSequenceGUID";
        public static readonly string FLASHXMLATTR_IS_HELP_IN_CTPP = "IsHelpInCTPP";
        public static readonly string FLASHXMLATTR_IS_REPORT_IN_CTPP = "IsReportInCTPP";

        public static readonly string QUERYSTR_PRESENTERIMAGE_MODE = "PresenterMode";

        public static readonly string QUERYSTR_LOG_TYPE_ID = "LogType";

        public static readonly string QUERYSTR_CTPP_GUID = "CTPPGUID";
        public static readonly string QUERYSTR_CTPP_TYPE = "CT";

        public static readonly string QUERYSTR_ORDERGUID = "OrderGUID";

        public static readonly string URL_CHANGETECH = "http://changetech.no";
        public static readonly string URL_DEVELOPER_HOME = "~/Home.aspx";

        public static readonly string URL_ORDERSYSTEM_HOME = "~/OrderSystem/OrdersHome.aspx";
        public static readonly string URL_ORDERSYSTEM_LIST = "~/OrderSystem/ListOrder.aspx";
        public static readonly string URL_MAINTENANCE_HOME = "~/MaintenanceHome.aspx";
        public static readonly string URL_TRANSLATOR_HOME = "~/Translator/TranslatorHome.aspx";
        public static readonly string URL_MONITOR_HOME = "~/Monitor/MonitorHome.aspx";
        public static readonly string URL_DEVELOPER_LIST_PROGRAM = "ListProgram.aspx";
        public static readonly string URL_DEVELOPER_MANAGE_APPLICATION_SECURITY = "ManageApplicationSecurity.aspx";
        public static readonly string URL_DEVELOPER_LIST_PREDICTOR_CATEGORY = "ListPredictorCategory.aspx";
        public static readonly string URL_DEVELOPER_LIST_INTERVENT = "ListIntervent.aspx";
        public static readonly string URL_DEVELOPER_LIST_PREDICTOR = "ListPredictor.aspx";
        public static readonly string URL_DEVELOPER_LIST_SPECIFIC_INTERVENT = "ListSpecificIntervent.aspx";
        public static readonly string URL_DEVELOPER_PROFILE = "Profile.aspx";
        public static readonly string URL_DEVELOPER_MANAGE_LANGUAGE = "ManageLanguage.aspx";
        public static readonly string URL_DEVELOPER_MANAGE_DELETE_PROGRAM = "ManageDeleteProgram.aspx";
        public static readonly string URL_DEVELOPER_MANAGE_TRANSLATIONJOB = "ManageTranslationJob.aspx";
        public static readonly string URL_DEVELOPER_LIST_PAGE_VARIABLE = "PageVariableList.aspx";
        public static readonly string URL_DEVELOPER_LIST_INTERVENT_CATEGORY = "ListInterventCategory.aspx";
        public static readonly string URL_DEVELOPER_LIST_ACTIVITY_LOG = "ActivityLogList.aspx";
        public static readonly string URL_DEVELOPER_MANAGE_PROGRAM_SECURITY = "ManageProgramSecurity.aspx";
        public static readonly string URL_DEVELOPER_MOVE_PAGE_SEQUENCE = "MovePageSequence.aspx";
        public static readonly string URL_DEVELOPER_MANAGE_SPECIAL_STRING = "ManageSpecialString.aspx";
        public static readonly string URL_DEVELOPER_MANAGE_BRAND = "ManageBrand.aspx";
        public static readonly string URL_DEVELOPER_MANAGE_PAYMENT_ORDER = "ManagePaymentOrder.aspx";
        public static readonly string URL_DEVELOPER_LIST_STUDY = "ListStudy.aspx";
        public static readonly string URL_DEVELOPER_MANAGE_ORDER_EMAIL_TEMPLATE = "ManageOrderEmailTemplate.aspx";
        public static readonly string URL_DEVELOPER_CTPP = "CTPP.aspx";
        public static readonly string URL_DEVELOPER_CTPPS = "CTPPS.aspx";
        public static readonly string URL_ORDERSYSTEM_ADDNEWORDER = "~/OrderSystem/AddNewOrder.aspx";
        public static readonly string URL_ORDERSYSTEM_ORDERINFO = "~/OrderSystem/OrderInfo.aspx";
        //Monitor Log
        public static readonly string URL_Monitor_LIST_USERLOG = "ListUserLog.aspx";
        public static readonly string URL_Monitor_LIST_INACTIVEUSER = "ListInactiveUser.aspx";
        public static readonly string URL_Monitor_LIST_LOGINUSER = "ListLoginUser.aspx";
        public static readonly string URL_Monitor_LIST_REGISTEREDUSER = "ListRegisteredUser.aspx";
        public static readonly string URL_Monitor_LIST_MISSEDCLASSUSER = "ListMissedClassUser.aspx";
        public static readonly string URL_Monitor_LIST_CTDLOG = "ListCTDLog.aspx";
        public static readonly string URL_Monitor_LIST_ERRORLOG = "ListErrorLog.aspx";
        public static readonly string URL_Monitor_LIST_SYSTEMLOG = "ListSystemLog.aspx";
        public static readonly string URL_Monitor_LIST_SQLAZURE = "ListSQLAzure.aspx";
        public static readonly string URL_Monitor_LIST_LOGTYPE = "ListLogType.aspx";

        public static readonly string URL_HPSYSTEM_MAINPAGE = "~/HealthProfileSystem/MainPage.aspx";
        public static readonly string URL_HPSYSTEM_ADDNEWORDER = "~/HealthProfileSystem/AddNewOrder.aspx";
        public static readonly string URL_HPSYSTEM_EDITORDER = "~/HealthProfileSystem/EditOrder.aspx";
        public static readonly string URL_HPSYSTEM_STARTPAGEONLINE = "~/HealthProfileSystem/halsoprofil-setup-independent.aspx";
        public static readonly string URL_HPSYSTEM_STARTPAGEOFFLINE = "~/HealthProfileSystem/halsoprofil-setup-offline-independent.aspx";
        public static readonly string QUERYSTR_HPORDERGUID = "HPOrderGuid";

        public const int PAGE_SIZE = 50;
        public const string PAGE_REVIEW = "Page review";
        public const string PREVIEW = "Preview";
        public const string MAKE_COPY = "Make copy";
        public const string DELETE = "Delete";
    }
}
