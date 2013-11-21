﻿//Created by Action Script Viewer - http://www.buraks.com/asv
package {
    import mx.core.*;
    import mx.managers.*;
    import flash.system.*;

    public class _Main_mx_managers_SystemManager extends SystemManager implements IFlexModuleFactory {

        override public function create(... _args):Object{
            if ((((_args.length > 0)) && (!((_args[0] is String))))){
                return (super.create.apply(this, _args));
            };
            var _local2:String = ((_args.length == 0)) ? "Main" : String(_args[0]);
            var _local3:Class = Class(getDefinitionByName(_local2));
            if (!_local3){
                return (null);
            };
            var _local4:Object = new (_local3);
            if ((_local4 is IFlexModule)){
                IFlexModule(_local4).moduleFactory = this;
            };
            if (_args.length == 0){
                EmbeddedFontRegistry.registerFonts(info()["fonts"], this);
            };
            return (_local4);
        }
        override public function info():Object{
            return ({applicationComplete:"applicationCompleteHandler()", compiledLocales:["en_US"], compiledResourceBundleNames:["CairngormMessages", "SharedResources", "collections", "containers", "controls", "core", "effects", "formatters", "logging", "messaging", "rpc", "skins", "states", "styles", "validators"], creationComplete:"init();", currentDomain:ApplicationDomain.currentDomain, fonts:{Arial:{regular:true, bold:true, italic:false, boldItalic:false}, Digital-7:{regular:true, bold:false, italic:false, boldItalic:false}, Helvetica Neue:{regular:true, bold:true, italic:true, boldItalic:true}, Trebuchet MS:{regular:true, bold:true, italic:true, boldItalic:true}}, horizontalAlign:"center", layout:"vertical", mainClassName:"Main", mixins:["_Main_FlexInit", "_alertButtonStyleStyle", "_ControlBarStyle", "_ScrollBarStyle", "_activeTabStyleStyle", "_textAreaHScrollBarStyleStyle", "_ToolTipStyle", "_DragManagerStyle", "_TextAreaStyle", "_advancedDataGridStylesStyle", "_comboDropdownStyle", "_ListBaseStyle", "_ProgressBarStyle", "_textAreaVScrollBarStyleStyle", "_ContainerStyle", "_linkButtonStyleStyle", "_globalStyle", "_windowStatusStyle", "_PanelStyle", "_windowStylesStyle", "_activeButtonStyleStyle", "_HSliderStyle", "_NumericStepperStyle", "_ApplicationControlBarStyle", "_errorTipStyle", "_richTextEditorTextAreaStyleStyle", "_FormItemStyle", "_todayStyleStyle", "_CursorManagerStyle", "_dateFieldPopupStyle", "_TextInputStyle", "_HRuleStyle", "_plainStyle", "_dataGridStylesStyle", "_ApplicationStyle", "_FormStyle", "_FormItemLabelStyle", "_SWFLoaderStyle", "_headerDateTextStyle", "_ButtonStyle", "_popUpMenuStyle", "_AlertStyle", "_swatchPanelTextFieldStyle", "_opaquePanelStyle", "_weekDayStyleStyle", "_RadioButtonStyle", "_VRuleStyle", "_headerDragProxyStyleStyle", "_com_redbox_changetech_view_components_BalanceSlimButtonWatcherSetupUtil", "_com_redbox_changetech_view_components_InfoPanelWatcherSetupUtil", "_com_redbox_changetech_view_components_WeekdayDataEntryInfoReflectionCanvasWatcherSetupUtil", "_com_redbox_changetech_view_components_WeeklyConsumptionWeekdayWatcherSetupUtil", "_com_redbox_changetech_view_components_BalancePopupButtonWatcherSetupUtil", "_com_redbox_changetech_view_components_InfoDayBadgeWatcherSetupUtil", "_com_redbox_changetech_view_components_InfoTargetBadgeWatcherSetupUtil", "_com_redbox_changetech_view_components_WeekdayDataEntryInfoWatcherSetupUtil", "_com_redbox_changetech_view_components_BalanceSettingsButtonWatcherSetupUtil", "_com_redbox_changetech_view_components_BalanceSettingsMenuWatcherSetupUtil", "_com_redbox_changetech_view_components_DropDownItemRendererWatcherSetupUtil", "_com_redbox_changetech_view_components_questions_NumericQuestionWatcherSetupUtil", "_com_redbox_changetech_view_components_EditableListElementWatcherSetupUtil", "_com_redbox_changetech_view_components_BalanceValueReflectionCanvasWatcherSetupUtil", "_com_redbox_changetech_view_components_WeekdayWatcherSetupUtil", "_com_redbox_changetech_view_components_questions_SingleLineTextQuestionWatcherSetupUtil", "_com_redbox_changetech_view_components_questions_MultiLineTextQuestionWatcherSetupUtil", "_com_redbox_changetech_view_components_questions_StopwatchWatcherSetupUtil", "_com_redbox_changetech_view_components_questions_RadioButtonQuestionWatcherSetupUtil", "_com_redbox_changetech_view_components_BalanceCopyReflectionCanvasWatcherSetupUtil", "_com_redbox_changetech_view_components_WeekdayDataEntryWatcherSetupUtil", "_com_redbox_changetech_view_components_WeekdayDataEntrySliderWatcherSetupUtil", "_com_redbox_changetech_view_components_BalanceSliderWatcherSetupUtil", "_com_redbox_changetech_view_components_BalanceAudioPlayerWatcherSetupUtil", "_com_redbox_changetech_view_components_questions_SliderQuestionWatcherSetupUtil", "_com_redbox_changetech_view_components_PlanWeekdayWatcherSetupUtil", "_com_redbox_changetech_view_components_BalanceGraphReflectionCanvasWatcherSetupUtil", "_com_redbox_changetech_view_components_WeeklyConsumptionDataEntryWatcherSetupUtil", "_com_redbox_changetech_view_components_IconOptionWatcherSetupUtil", "_com_redbox_changetech_view_components_BalanceGlassReflectionCanvasWatcherSetupUtil", "_com_redbox_changetech_view_components_InitialPlanWeekdayWatcherSetupUtil", "_com_redbox_changetech_view_components_BalanceCustomContentReflectionCanvasWatcherSetupUtil", "_com_redbox_changetech_view_components_BalanceButtonReflectionCanvasWatcherSetupUtil", "_com_redbox_changetech_view_templates_PersonalValueWatcherSetupUtil", "_com_redbox_changetech_view_components_BalanceTertiaryButtonReflectionCanvasWatcherSetupUtil", "_com_redbox_changetech_view_components_BalanceSecondaryButtonReflectionCanvasWatcherSetupUtil", "_com_redbox_changetech_view_components_LoginDialogWatcherSetupUtil", "_com_redbox_changetech_view_components_BadgePopUpWatcherSetupUtil", "_com_redbox_changetech_view_components_BalanceRoomBadgeWatcherSetupUtil", "_com_redbox_changetech_view_components_BottomRightBadgeWatcherSetupUtil", "_com_redbox_changetech_view_components_TransitionPopUpWatcherSetupUtil", "_com_redbox_changetech_view_modules_popup_modules_PopUpModuleLoaderWatcherSetupUtil", "_com_redbox_changetech_view_components_BalanceImageReflectionCanvasWatcherSetupUtil", "_com_redbox_changetech_view_components_MainViewWatcherSetupUtil", "_com_redbox_changetech_view_components_BalanceRoomWatcherSetupUtil", "_com_redbox_changetech_view_modules_BasicModuleWatcherSetupUtil", "_com_redbox_changetech_view_components_BalanceDualFunctionButtonWatcherSetupUtil", "_com_redbox_changetech_view_templates_DailyConsumptionWatcherSetupUtil", "_com_redbox_changetech_view_templates_CueReactivityPromptWatcherSetupUtil", "_com_redbox_changetech_view_templates_ProgressMonitorWatcherSetupUtil", "_com_redbox_changetech_view_templates_TypicalWeekWatcherSetupUtil", "_com_redbox_changetech_view_templates_PicRightInfoTextWatcherSetupUtil", "_com_redbox_changetech_view_templates_PicLeftInfoTextWatcherSetupUtil", "_com_redbox_changetech_view_templates_TypicalWeekCompletionWatcherSetupUtil", "_com_redbox_changetech_view_templates_WeeklyConsumptionWatcherSetupUtil", "_com_redbox_changetech_view_templates_RegistrationWatcherSetupUtil", "_com_redbox_changetech_view_templates_PersonalValuesWatcherSetupUtil", "_com_redbox_changetech_view_templates_PersonalPlanWatcherSetupUtil", "_com_redbox_changetech_view_templates_GlassPictureWatcherSetupUtil", "_com_redbox_changetech_view_components_BalanceButtonWatcherSetupUtil", "_com_redbox_changetech_view_components_QuizPopUpWatcherSetupUtil", "_com_redbox_changetech_view_templates_CompletionGraphWatcherSetupUtil", "_com_redbox_changetech_view_templates_TrafficLightResultsWatcherSetupUtil", "_com_redbox_changetech_view_templates_QuizWatcherSetupUtil", "_com_redbox_changetech_view_components_BalanceContentReflectionCanvasWatcherSetupUtil", "_com_redbox_changetech_view_modules_ScreeningTestWatcherSetupUtil", "_MainWatcherSetupUtil"], verticalAlign:"top", verticalScrollPolicy:"off"});
        }

    }
}//package 