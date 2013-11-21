//Created by Action Script Viewer - http://www.buraks.com/asv
package com.redbox.changetech.control {
    import com.adobe.cairngorm.control.*;
    import com.redbox.changetech.command.*;

    public class BalanceController extends FrontController {

        public static const SHOW_LOGIN_DIALOG:String = "showLoginDialog";
        public static const INTRO_FINISHED:String = "intro_finished";
        public static const SET_PLAN:String = "setPlan";
        public static const PRINT_COMMAND:String = "PRINT_COMMAND";
        public static const MODULE_COLLECTION_COMPLETE:String = "moduleCollectionComplete";
        public static const NEXT_CONTENT:String = "nextContent";
        public static const SELECT_NEXT_DAY:String = "selectNextDay";
        public static const IS_ACTIVE_SESSION:String = "isActiveSession";
        public static const SHOW_LOADER:String = "showLoader";
        public static const SELECT_PREVIOUS_DAY:String = "selectPreviousDay";
        public static const PREVIOUS_CONTENT:String = "nextContent";
        public static const START_SCREENING:String = "startScreening";
        public static const SET_COMPLETIONSCORE_COMMAND:String = "SET_COMPLETIONSCORE_COMMAND";
        public static const GET_PREVIEW:String = "getPreview";
        public static const UNSUBSCRIBE_COMMAND:String = "UNSUBSCRIBE_COMMAND";
        public static const SET_SCREENING_GENDER:String = "setScreeningGender";
        public static const UPDATE_CONSUMER:String = "updateConsumer";
        public static const GET_CONSUMER_AFTER_LOGIN:String = "getConsumerAfterLogin";
        public static const HIDE_LOADER:String = "hideLoader";
        public static const GET_LANGUAGE_COMMAND:String = "getLanguage";
        public static const REPORT_USAGE:String = "reportUsage";
        public static const INITIALISE:String = "initialise";
        public static const GET_WELCOME:String = "get_welcome";
        public static const COLLECTION_COMPLETE:String = "collectionComplete";
        public static const SET_MOBILENUMBER_COMMAND:String = "SET_MOBILENUMBER_COMMAND";
        public static const SET_LAPSE_COMMAND:String = "SET_LAPSE_COMMAND";
        public static const SELECT_DAY:String = "selectDay";
        public static const EXIT_COMMAND:String = "EXIT_COMMAND";
        public static const RESET_LOGIN:String = "resetLogin";
        public static const LOGIN:String = "login";
        public static const COMPLETE_SCREENING:String = "completeScreening";
        public static const NAVIGATE_TO_ROOM:String = "navigateToRoom";
        public static const SET_HAPPINESSSCORE_COMMAND:String = "SET_HAPPINESSSCORE_COMMAND";
        public static const CREATE_CONSUMER:String = "createConsumer";
        public static const HIDE_LOGIN_DIALOG:String = "hideLoginDialog";
        public static const STARTUP:String = "startUp";
        public static const GET_CONSUMER:String = "getConsumer";
        public static const SET_CONSUMERTRACK1_COMMAND:String = "SET_CONSUMERTRACK1_COMMAND";
        public static const SET_CONSUMERTRACK2_COMMAND:String = "SET_CONSUMERTRACK2_COMMAND";
        public static const GET_TEST_COMMAND:String = "getTest";
        public static const TRANSITION_COMPLETE:String = "transitionComplete";
        public static const GET_CONFIG:String = "getConfig";
        public static const SEND_SMS_COMMAND:String = "SEND_SMS_COMMAND";
        public static const LOGOUT_COMMAND:String = "LOGOUT_COMMAND";
        public static const SET_SCREENINGSCORE_COMMAND:String = "SET_SCREENINGSCORE_COMMAND";
        public static const START_DAY:String = "startDay";

        public function BalanceController(){
            addCommand(STARTUP, StartUpCommand);
            addCommand(INTRO_FINISHED, IntroFinishedCommand);
            addCommand(GET_WELCOME, GetWelcomeCommand);
            addCommand(INITIALISE, InitialiseCommand);
            addCommand(SHOW_LOADER, ShowLoaderCommand);
            addCommand(HIDE_LOADER, HideLoaderCommand);
            addCommand(SHOW_LOGIN_DIALOG, ShowLoginDialogCommand);
            addCommand(HIDE_LOGIN_DIALOG, HideLoginDialogCommand);
            addCommand(LOGIN, LoginCommand);
            addCommand(RESET_LOGIN, ResetLoginCommand);
            addCommand(START_DAY, StartDayCommand);
            addCommand(COLLECTION_COMPLETE, CollectionCompleteCommand);
            addCommand(NAVIGATE_TO_ROOM, NavigateToRoomCommand);
            addCommand(START_SCREENING, StartScreeningCommand);
            addCommand(COMPLETE_SCREENING, CompleteScreeningCommand);
            addCommand(SET_SCREENING_GENDER, SetScreeningGenderCommand);
            addCommand(SELECT_DAY, SelectDayCommand);
            addCommand(SELECT_NEXT_DAY, SelectNextDayCommand);
            addCommand(SELECT_PREVIOUS_DAY, SelectPreviousDayCommand);
            addCommand(REPORT_USAGE, ReportUsageCommand);
            addCommand(SET_PLAN, SetPlanCommand);
            addCommand(CREATE_CONSUMER, CreateConsumerCommand);
            addCommand(GET_PREVIEW, GetPreviewCommand);
            addCommand(GET_CONFIG, GetConfigCommand);
            addCommand(GET_CONSUMER, GetConsumerCommand);
            addCommand(GET_CONSUMER_AFTER_LOGIN, GetConsumerAfterLoginCommand);
            addCommand(IS_ACTIVE_SESSION, IsActiveSessionCommand);
            addCommand(GET_LANGUAGE_COMMAND, GetLanguageCommand);
            addCommand(GET_TEST_COMMAND, GetTestCommand);
            addCommand(SET_CONSUMERTRACK1_COMMAND, SetConsumerTrack1Command);
            addCommand(SET_CONSUMERTRACK2_COMMAND, SetConsumerTrack2Command);
            addCommand(SET_HAPPINESSSCORE_COMMAND, SetHappinessScoreCommand);
            addCommand(SET_LAPSE_COMMAND, SetLapseCommand);
            addCommand(EXIT_COMMAND, ExitCommand);
            addCommand(UNSUBSCRIBE_COMMAND, UnsubscribeCommand);
            addCommand(LOGOUT_COMMAND, LogoutCommand);
            addCommand(SEND_SMS_COMMAND, SendSmsCommand);
            addCommand(SET_MOBILENUMBER_COMMAND, SetMobileNumberCommand);
            addCommand(UPDATE_CONSUMER, UpdateConsumerCommand);
        }
    }
}//package com.redbox.changetech.control 
