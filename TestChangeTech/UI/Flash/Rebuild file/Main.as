//Created by Action Script Viewer - http://www.buraks.com/asv
package {
    import flash.display.*;
    import flash.geom.*;
    import flash.media.*;
    import flash.text.*;
    import mx.core.*;
    import mx.managers.*;
    import flash.events.*;
    import mx.events.*;
    import mx.styles.*;
    import mx.controls.*;
    import mx.states.*;
    import mx.binding.*;
    import mx.rpc.events.*;
    import com.redbox.changetech.control.*;
    import mx.rpc.http.mxml.*;
    import mx.containers.*;
    import com.redbox.changetech.view.components.*;
    import com.redbox.changetech.model.*;
    import mx.binding.utils.*;
    import assets.*;
    import com.redbox.changetech.vo.*;
    import flash.utils.*;
    import com.adobe.cairngorm.control.*;
    import flash.system.*;
    import flash.accessibility.*;
    import flash.xml.*;
    import flash.net.*;
    import com.redbox.changetech.event.*;
    import flash.filters.*;
    import flash.ui.*;
    import com.redbox.changetech.skins.*;
    import ws.tink.flex.skins.*;
    import com.redbox.changetech.util.*;
    import com.flashgen.skins.*;
    import com.redbox.changetech.view.modules.popup_modules.*;
    import com.lyraspace.skins.*;
    import flash.external.*;
    import flash.debugger.*;
    import flash.errors.*;
    import flash.printing.*;
    import flash.profiler.*;

    public class Main extends Application implements IBindingClient {

        private var _embed_css_styles_scrollbar_downArrow_png_105236926:Class
        private var _embed__font_Arial_bold_normal_635218853:Class
        private var _embed_css_styles_scrollbar_scrollThumb_png_843266370:Class
        private var _documentDescriptor_:UIComponentDescriptor
        private var _61913020toggleToolTipText = "fullscreen"
        private var genericPopup:QuizPopUp
        private var _embed__font_Trebuchet_MS_bold_italic_1215885907:Class
        private var _embed_css_styles_accordion_header_down_png_687653172:Class
        private var _embed_css____assets_transparent_png_991515083:Class
        private var _embed_css_assets_icons_pause_png_1258992250:Class
        private var _embed_css____assets_balance_slider_png_1098006639:Class
        private var _embed_css_assets_icons_play_png_635297854:Class
        private var _embed__font_Trebuchet_MS_medium_italic_1952528680:Class
        private var _1024130777appCanvas:Canvas
        private var _embed__font_Helvetica_Neue_bold_italic_1755100884:Class
        private var _95458899debug:Label
        private var _1221270899header:Header
        private var _embed__font_Helvetica_Neue_bold_normal_2100285935:Class
        private var _embed_css_styles_scrollbar_upArrow_png_81364740:Class
        private var iPopUpManager:IPopUpManager
        private var _embed__font_Arial_medium_normal_1371861608:Class
        mx_internal var _bindingsByDestination:Object
        private var transitionPopup:IFlexDisplayObject
        private var _1407821277mainViewContainer:MainView
        private var _embed_css____assets_balance_audio_slider_green_png_851077007:Class
        private var _embed_css_styles_close_btn_png_1779263772:Class
        private var _1268861541footer:Footer
        private var _embed_css____assets_balance_slider_large_orange_png_2122909247:Class
        private var _embed_css_styles_scrollbar_track_png_1741243514:Class
        private var _embed_css_styles_accordion_down_icon_png_493783552:Class
        private var _embed_css____assets_slider_thumb_white_png_548198765:Class
        private var _637428636controller:BalanceController
        private var _embed_css____assets_balance_slider_large_green_png_725001071:Class
        private var _embed__font_Digital_7_medium_normal_4548212:Class
        mx_internal var _watchers:Array
        private var modulePopup:IFlexDisplayObject
        private var _embed__font_Helvetica_Neue_medium_normal_1458038484:Class
        private var _embed_css_assets_radio_button_ON_png_1114731548:Class
        private var _embed_css_assets_icons_mute_png_312419448:Class
        private var iDragManager:IDragManager
        private var _embed_css____assets_balance_slider_large_blue_png_1163403871:Class
        private var _embed_css_styles_accordion_header_up_png_102004674:Class
        private var _embed_css_assets_continue_icon_png_1824304090:Class
        private var _embed__font_Helvetica_Neue_medium_italic_1018458002:Class
        private var _embed__font_Trebuchet_MS_bold_normal_776319023:Class
        public var _Main_Image1:Image
        private var _embed_css_styles_accordion_up_icon_png_1008286910:Class
        private var _embed_css_assets_radio_button_OFF_png_339325176:Class
        private var _embed_css____assets_continue_icon_png_1906780433:Class
        mx_internal var _bindingsBeginWithWord:Object
        private var _embed__font_Trebuchet_MS_medium_normal_1512961872:Class
        private var _104069929model:BalanceModelLocator
        private var _680002019footerX:Number = 0
        private var _1707945992contentContainer:HBox
        private var _1456329459httpService:HTTPService
        mx_internal var _bindings:Array

        mx_internal static var _Main_StylesInit_done:Boolean = false;
        private static var _watcherSetupUtil:IWatcherSetupUtil;

        public function Main(){
            _documentDescriptor_ = new UIComponentDescriptor({type:Application});
            _104069929model = BalanceModelLocator.getInstance();
            _embed__font_Arial_bold_normal_635218853 = Main__embed__font_Arial_bold_normal_635218853;
            _embed__font_Arial_medium_normal_1371861608 = Main__embed__font_Arial_medium_normal_1371861608;
            _embed__font_Digital_7_medium_normal_4548212 = Main__embed__font_Digital_7_medium_normal_4548212;
            _embed__font_Helvetica_Neue_bold_italic_1755100884 = Main__embed__font_Helvetica_Neue_bold_italic_1755100884;
            _embed__font_Helvetica_Neue_bold_normal_2100285935 = Main__embed__font_Helvetica_Neue_bold_normal_2100285935;
            _embed__font_Helvetica_Neue_medium_italic_1018458002 = Main__embed__font_Helvetica_Neue_medium_italic_1018458002;
            _embed__font_Helvetica_Neue_medium_normal_1458038484 = Main__embed__font_Helvetica_Neue_medium_normal_1458038484;
            _embed__font_Trebuchet_MS_bold_italic_1215885907 = Main__embed__font_Trebuchet_MS_bold_italic_1215885907;
            _embed__font_Trebuchet_MS_bold_normal_776319023 = Main__embed__font_Trebuchet_MS_bold_normal_776319023;
            _embed__font_Trebuchet_MS_medium_italic_1952528680 = Main__embed__font_Trebuchet_MS_medium_italic_1952528680;
            _embed__font_Trebuchet_MS_medium_normal_1512961872 = Main__embed__font_Trebuchet_MS_medium_normal_1512961872;
            _embed_css____assets_balance_audio_slider_green_png_851077007 = Main__embed_css____assets_balance_audio_slider_green_png_851077007;
            _embed_css____assets_balance_slider_large_blue_png_1163403871 = Main__embed_css____assets_balance_slider_large_blue_png_1163403871;
            _embed_css____assets_balance_slider_large_green_png_725001071 = Main__embed_css____assets_balance_slider_large_green_png_725001071;
            _embed_css____assets_balance_slider_large_orange_png_2122909247 = Main__embed_css____assets_balance_slider_large_orange_png_2122909247;
            _embed_css____assets_balance_slider_png_1098006639 = Main__embed_css____assets_balance_slider_png_1098006639;
            _embed_css____assets_continue_icon_png_1906780433 = Main__embed_css____assets_continue_icon_png_1906780433;
            _embed_css____assets_slider_thumb_white_png_548198765 = Main__embed_css____assets_slider_thumb_white_png_548198765;
            _embed_css____assets_transparent_png_991515083 = Main__embed_css____assets_transparent_png_991515083;
            _embed_css_assets_continue_icon_png_1824304090 = Main__embed_css_assets_continue_icon_png_1824304090;
            _embed_css_assets_icons_mute_png_312419448 = Main__embed_css_assets_icons_mute_png_312419448;
            _embed_css_assets_icons_pause_png_1258992250 = Main__embed_css_assets_icons_pause_png_1258992250;
            _embed_css_assets_icons_play_png_635297854 = Main__embed_css_assets_icons_play_png_635297854;
            _embed_css_assets_radio_button_OFF_png_339325176 = Main__embed_css_assets_radio_button_OFF_png_339325176;
            _embed_css_assets_radio_button_ON_png_1114731548 = Main__embed_css_assets_radio_button_ON_png_1114731548;
            _embed_css_styles_accordion_down_icon_png_493783552 = Main__embed_css_styles_accordion_down_icon_png_493783552;
            _embed_css_styles_accordion_header_down_png_687653172 = Main__embed_css_styles_accordion_header_down_png_687653172;
            _embed_css_styles_accordion_header_up_png_102004674 = Main__embed_css_styles_accordion_header_up_png_102004674;
            _embed_css_styles_accordion_up_icon_png_1008286910 = Main__embed_css_styles_accordion_up_icon_png_1008286910;
            _embed_css_styles_close_btn_png_1779263772 = Main__embed_css_styles_close_btn_png_1779263772;
            _embed_css_styles_scrollbar_downArrow_png_105236926 = Main__embed_css_styles_scrollbar_downArrow_png_105236926;
            _embed_css_styles_scrollbar_scrollThumb_png_843266370 = Main__embed_css_styles_scrollbar_scrollThumb_png_843266370;
            _embed_css_styles_scrollbar_track_png_1741243514 = Main__embed_css_styles_scrollbar_track_png_1741243514;
            _embed_css_styles_scrollbar_upArrow_png_81364740 = Main__embed_css_styles_scrollbar_upArrow_png_81364740;
            _bindings = [];
            _watchers = [];
            _bindingsByDestination = {};
            _bindingsBeginWithWord = {};
            super();
            mx_internal::_document = this;
            if (!this.styleDeclaration){
                this.styleDeclaration = new CSSStyleDeclaration();
            };
            this.styleDeclaration.defaultFactory = function ():void{
                this.horizontalAlign = "center";
                this.verticalAlign = "top";
            };
            mx_internal::_Main_StylesInit();
            this.layout = "vertical";
            this.verticalScrollPolicy = "off";
            this.states = [_Main_State1_c(), _Main_State2_c()];
            _Main_BalanceController1_i();
            _Main_HTTPService1_i();
            this.addEventListener("creationComplete", ___Main_Application1_creationComplete);
            this.addEventListener("applicationComplete", ___Main_Application1_applicationComplete);
        }
        public function get contentContainer():HBox{
            return (this._1707945992contentContainer);
        }
        private function _Main_HBox1_c():HBox{
            var _local1:HBox = new HBox();
            _local1.percentWidth = 50;
            _local1.setStyle("horizontalAlign", "left");
            _local1.setStyle("verticalAlign", "top");
            _local1.setStyle("paddingTop", 30);
            _local1.setStyle("paddingLeft", 30);
            if (!_local1.document){
                _local1.document = this;
            };
            _local1.addChild(_Main_Image1_i());
            return (_local1);
        }
        public function get httpService():HTTPService{
            return (this._1456329459httpService);
        }
        private function init():void{
            ToolTipManager.enabled = false;
            model.flashVars = Application.application.parameters;
            httpService.send();
            BindingUtils.bindSetter(transitionBlur, model, "isTransitionActive");
            var _local1:CairngormEvent = new CairngormEvent(BalanceController.GET_CONFIG);
            _local1.data = new Object();
            _local1.data.callBackObject = this;
            _local1.data.callBackFunction = gatewayReady;
            _local1.dispatch();
        }
        mx_internal function _Main_StylesInit():void{
            var style:* = null;
            var effects:* = null;
            if (mx_internal::_Main_StylesInit_done){
                return;
            };
            mx_internal::_Main_StylesInit_done = true;
            style = StyleManager.getStyleDeclaration("TextArea");
            if (!style){
                style = new CSSStyleDeclaration();
                StyleManager.setStyleDeclaration("TextArea", style, false);
            };
            if (style.factory == null){
                style.factory = function ():void{
                    this.fontAntiAliasType = "advanced";
                    this.fontSize = 14;
                    this.backgroundAlpha = 0;
                    this.borderStyle = "none";
                    this.fontFamily = "Helvetica Neue";
                };
            };
            style = StyleManager.getStyleDeclaration(".planDrinksSelected");
            if (!style){
                style = new CSSStyleDeclaration();
                StyleManager.setStyleDeclaration(".planDrinksSelected", style, false);
            };
            if (style.factory == null){
                style.factory = function ():void{
                    this.color = 2729955;
                    this.fontWeight = "bold";
                    this.fontSize = 14;
                    this.fontFamily = "Arial";
                };
            };
            style = StyleManager.getStyleDeclaration(".resultsButton");
            if (!style){
                style = new CSSStyleDeclaration();
                StyleManager.setStyleDeclaration(".resultsButton", style, false);
            };
            if (style.factory == null){
                style.factory = function ():void{
                    this.downFillColors = [0xFEFEFE, 15263719];
                    this.upSkin = EnhancedButtonSkin;
                    this.overFillAlphas = [1, 1];
                    this.overFillColors = [0xFFFFFF, 0xFEFEFE];
                    this.overSkin = EnhancedButtonSkin;
                    this.selectedDisabledSkin = EnhancedButtonSkin;
                    this.borderAlpha = 0;
                    this.selectedFillAlphas = [1, 1];
                    this.fillAlphas = [1, 1];
                    this.downFillAlphas = [1, 1];
                    this.selectedDownSkin = EnhancedButtonSkin;
                    this.cornerRadii = [10, 10, 10, 10];
                    this.selectedUpSkin = EnhancedButtonSkin;
                    this.fillColors = [0xFEFEFE, 15263719];
                    this.selectedOverSkin = EnhancedButtonSkin;
                    this.downSkin = EnhancedButtonSkin;
                    this.borderThickness = 0;
                    this.disabledSkin = EnhancedButtonSkin;
                    this.selectedFillColors = [0xFEFEFE, 15263719];
                };
            };
            style = StyleManager.getStyleDeclaration("Application");
            if (!style){
                style = new CSSStyleDeclaration();
                StyleManager.setStyleDeclaration("Application", style, false);
            };
            if (style.factory == null){
                style.factory = function ():void{
                    this.paddingTop = 0;
                    this.paddingLeft = 0;
                    this.paddingRight = 0;
                    this.backgroundImage = ApplicationBackground;
                    this.paddingBottom = 0;
                    this.backgroundSize = "100%";
                    this.backgroundColor = 0xFFFFFF;
                };
            };
            style = StyleManager.getStyleDeclaration(".date");
            if (!style){
                style = new CSSStyleDeclaration();
                StyleManager.setStyleDeclaration(".date", style, false);
            };
            if (style.factory == null){
                style.factory = function ():void{
                    this.color = 10197658;
                    this.fontWeight = "normal";
                    this.fontface = "Trebuchet MS";
                    this.fontSize = 10;
                    this.fontFamily = "Trebuchet MS";
                };
            };
            style = StyleManager.getStyleDeclaration(".continueButton");
            if (!style){
                style = new CSSStyleDeclaration();
                StyleManager.setStyleDeclaration(".continueButton", style, false);
            };
            if (style.factory == null){
                style.factory = function ():void{
                    this.color = 0xFFFFFF;
                    this.fontWeight = "normal";
                    this.icon = _embed_css____assets_continue_icon_png_1906780433;
                    this.fontSize = 18;
                    this.fontFamily = "Helvetica Neue";
                };
            };
            style = StyleManager.getStyleDeclaration(".trebuchet24grey");
            if (!style){
                style = new CSSStyleDeclaration();
                StyleManager.setStyleDeclaration(".trebuchet24grey", style, false);
            };
            if (style.factory == null){
                style.factory = function ():void{
                    this.color = 5920595;
                    this.fontWeight = "bold";
                    this.fontSize = 23.5;
                    this.fontFamily = "Trebuchet MS";
                };
            };
            style = StyleManager.getStyleDeclaration(".textAreaInput");
            if (!style){
                style = new CSSStyleDeclaration();
                StyleManager.setStyleDeclaration(".textAreaInput", style, false);
            };
            if (style.factory == null){
                style.factory = function ():void{
                    this.fontAntiAliasType = "advanced";
                    this.fontSize = 12;
                    this.backgroundAlpha = 1;
                    this.borderStyle = "inset";
                    this.backgroundColor = 0xFFFFFF;
                    this.fontFamily = "Helvetica Neue";
                };
            };
            style = StyleManager.getStyleDeclaration(".audioGreenSlider");
            if (!style){
                style = new CSSStyleDeclaration();
                StyleManager.setStyleDeclaration(".audioGreenSlider", style, false);
            };
            if (style.factory == null){
                style.factory = function ():void{
                    this.trackSkin = _embed_css____assets_transparent_png_991515083;
                    this.thumbSkin = _embed_css____assets_balance_audio_slider_green_png_851077007;
                };
            };
            style = StyleManager.getStyleDeclaration(".helvetica16WhiteBold");
            if (!style){
                style = new CSSStyleDeclaration();
                StyleManager.setStyleDeclaration(".helvetica16WhiteBold", style, false);
            };
            if (style.factory == null){
                style.factory = function ():void{
                    this.color = 0xFFFFFF;
                    this.fontWeight = "bold";
                    this.fontSize = 16;
                    this.fontFamily = "Helvetica Neue";
                };
            };
            style = StyleManager.getStyleDeclaration(".helvNeueMed18Italicwhite");
            if (!style){
                style = new CSSStyleDeclaration();
                StyleManager.setStyleDeclaration(".helvNeueMed18Italicwhite", style, false);
            };
            if (style.factory == null){
                style.factory = function ():void{
                    this.color = 0xFFFFFF;
                    this.fontWeight = "normal";
                    this.fontStyle = "italic";
                    this.fontSize = 18;
                    this.fontFamily = "Helvetica Neue";
                };
            };
            style = StyleManager.getStyleDeclaration(".infoDayBadgeNumber");
            if (!style){
                style = new CSSStyleDeclaration();
                StyleManager.setStyleDeclaration(".infoDayBadgeNumber", style, false);
            };
            if (style.factory == null){
                style.factory = function ():void{
                    this.textAlign = "center";
                    this.color = 14650922;
                    this.fontWeight = "bold";
                    this.fontSize = 24;
                    this.fontFamily = "Helvetica Neue";
                };
            };
            style = StyleManager.getStyleDeclaration(".helvetica14BoldGrey");
            if (!style){
                style = new CSSStyleDeclaration();
                StyleManager.setStyleDeclaration(".helvetica14BoldGrey", style, false);
            };
            if (style.factory == null){
                style.factory = function ():void{
                    this.color = 5920595;
                    this.fontWeight = "bold";
                    this.fontSize = 14;
                    this.fontFamily = "Helvetica Neue";
                };
            };
            style = StyleManager.getStyleDeclaration(".helvetica16White");
            if (!style){
                style = new CSSStyleDeclaration();
                StyleManager.setStyleDeclaration(".helvetica16White", style, false);
            };
            if (style.factory == null){
                style.factory = function ():void{
                    this.color = 0xFFFFFF;
                    this.fontWeight = "normal";
                    this.fontSize = 16;
                    this.fontFamily = "Helvetica Neue";
                };
            };
            style = StyleManager.getStyleDeclaration("NumericStepper");
            if (!style){
                style = new CSSStyleDeclaration();
                StyleManager.setStyleDeclaration("NumericStepper", style, false);
            };
            if (style.factory == null){
                style.factory = function ():void{
                    this.fillAlphas = [1, 1, 1, 1];
                    this.highlightAlphas = [1, 1];
                    this.color = 5920595;
                    this.cornerRadius = 0;
                    this.fontWeight = "bold";
                    this.fillColors = [0xFFFFFF, 0xFFFFFF, 0xFFFFFF, 0xFFFFFF];
                    this.fontAntiAliasType = "advanced";
                    this.backgroundAlpha = 0;
                    this.fontSize = 48;
                    this.borderStyle = "none";
                    this.themeColor = 0xFFFFFF;
                    this.fontFamily = "Helvetica Neue";
                };
            };
            style = StyleManager.getStyleDeclaration(".trebuchet13grey");
            if (!style){
                style = new CSSStyleDeclaration();
                StyleManager.setStyleDeclaration(".trebuchet13grey", style, false);
            };
            if (style.factory == null){
                style.factory = function ():void{
                    this.color = 6578267;
                    this.fontWeight = "normal";
                    this.leading = 6;
                    this.fontSize = 13;
                    this.fontFamily = "Trebuchet MS";
                };
            };
            style = StyleManager.getStyleDeclaration(".blankIconButton");
            if (!style){
                style = new CSSStyleDeclaration();
                StyleManager.setStyleDeclaration(".blankIconButton", style, false);
            };
            if (style.factory == null){
                style.factory = function ():void{
                    this.color = 0xFFFFFF;
                    this.fontWeight = "normal";
                    this.textRollOverColor = 0xFFFFFF;
                    this.upSkin = BlankSkin;
                    this.icon = _embed_css_assets_continue_icon_png_1824304090;
                    this.fontSize = 18;
                    this.downSkin = BlankSkin;
                    this.overSkin = BlankSkin;
                    this.disabledSkin = BlankSkin;
                    this.textSelectedColor = 0xFFFFFF;
                    this.fontFamily = "Helvetica Neue";
                };
            };
            style = StyleManager.getStyleDeclaration(".sliderNumberLargeEmotion");
            if (!style){
                style = new CSSStyleDeclaration();
                StyleManager.setStyleDeclaration(".sliderNumberLargeEmotion", style, false);
            };
            if (style.factory == null){
                style.factory = function ():void{
                    this.color = 14057232;
                    this.fontSize = 12;
                    this.fontFamily = "Helvetica Neue";
                };
            };
            style = StyleManager.getStyleDeclaration(".sliderNumber");
            if (!style){
                style = new CSSStyleDeclaration();
                StyleManager.setStyleDeclaration(".sliderNumber", style, false);
            };
            if (style.factory == null){
                style.factory = function ():void{
                    this.color = 2790341;
                    this.fontSize = 12;
                    this.fontFamily = "Helvetica Neue";
                };
            };
            style = StyleManager.getStyleDeclaration(".helvNeue18whiteBold");
            if (!style){
                style = new CSSStyleDeclaration();
                StyleManager.setStyleDeclaration(".helvNeue18whiteBold", style, false);
            };
            if (style.factory == null){
                style.factory = function ():void{
                    this.color = 0xFFFFFF;
                    this.fontWeight = "bold";
                    this.fontSize = 18;
                    this.fontFamily = "Helvetica Neue";
                };
            };
            style = StyleManager.getStyleDeclaration(".header");
            if (!style){
                style = new CSSStyleDeclaration();
                StyleManager.setStyleDeclaration(".header", style, false);
            };
            if (style.factory == null){
                style.factory = function ():void{
                    this.color = 10197658;
                    this.fontWeight = "bold";
                    this.fontSize = 22;
                    this.fontFamily = "Trebuchet MS";
                };
            };
            style = StyleManager.getStyleDeclaration(".videoControlBarScrubBar");
            if (!style){
                style = new CSSStyleDeclaration();
                StyleManager.setStyleDeclaration(".videoControlBarScrubBar", style, false);
            };
            if (style.factory == null){
                style.factory = function ():void{
                    this.borderColor = 0xFFFFFF;
                    this.borderSkin = ScrubBarSkin;
                    this.backgroundColor = 0x666666;
                };
            };
            style = StyleManager.getStyleDeclaration(".balanceRoomBadge");
            if (!style){
                style = new CSSStyleDeclaration();
                StyleManager.setStyleDeclaration(".balanceRoomBadge", style, false);
            };
            if (style.factory == null){
                style.factory = function ():void{
                    this.fillAlphas = [1, 0];
                    this.cornerRadius = 0;
                    this.gradientType = "radial";
                    this.fillColors = [0xFFFFFF, 0xFFFFFF];
                    this.borderSkin = GradientBackground;
                    this.borderThickness = 0;
                };
            };
            style = StyleManager.getStyleDeclaration("HBox");
            if (!style){
                style = new CSSStyleDeclaration();
                StyleManager.setStyleDeclaration("HBox", style, false);
            };
            style = StyleManager.getStyleDeclaration("VBox");
            if (!style){
                style = new CSSStyleDeclaration();
                StyleManager.setStyleDeclaration("VBox", style, false);
            };
            style = StyleManager.getStyleDeclaration("AccordionHeader");
            if (!style){
                style = new CSSStyleDeclaration();
                StyleManager.setStyleDeclaration("AccordionHeader", style, false);
            };
            if (style.factory == null){
                style.factory = function ():void{
                    this.color = "#728289";
                    this.skin = _embed_css_styles_accordion_header_up_png_102004674;
                    this.selectedDownSkin = _embed_css_styles_accordion_header_down_png_687653172;
                    this.selectedUpSkin = _embed_css_styles_accordion_header_down_png_687653172;
                    this.selectedOverIcon = _embed_css_styles_accordion_down_icon_png_493783552;
                    this.selectedOverSkin = _embed_css_styles_accordion_header_down_png_687653172;
                    this.upSkin = _embed_css_styles_accordion_header_up_png_102004674;
                    this.icon = _embed_css_styles_accordion_up_icon_png_1008286910;
                    this.fontSize = "16";
                    this.selectedUpIcon = _embed_css_styles_accordion_down_icon_png_493783552;
                    this.fontFamily = "Helvetica Neue";
                };
            };
            style = StyleManager.getStyleDeclaration(".helvNeue18Italic");
            if (!style){
                style = new CSSStyleDeclaration();
                StyleManager.setStyleDeclaration(".helvNeue18Italic", style, false);
            };
            if (style.factory == null){
                style.factory = function ():void{
                    this.color = 0x333333;
                    this.fontWeight = "normal";
                    this.fontStyle = "italic";
                    this.fontSize = 18;
                    this.fontFamily = "Helvetica Neue";
                };
            };
            style = StyleManager.getStyleDeclaration(".planDrinks");
            if (!style){
                style = new CSSStyleDeclaration();
                StyleManager.setStyleDeclaration(".planDrinks", style, false);
            };
            if (style.factory == null){
                style.factory = function ():void{
                    this.color = 0x999999;
                    this.fontWeight = "bold";
                    this.fontSize = 14;
                    this.fontFamily = "Arial";
                };
            };
            style = StyleManager.getStyleDeclaration(".helvetica48WhiteBold");
            if (!style){
                style = new CSSStyleDeclaration();
                StyleManager.setStyleDeclaration(".helvetica48WhiteBold", style, false);
            };
            if (style.factory == null){
                style.factory = function ():void{
                    this.color = 0xFFFFFF;
                    this.fontWeight = "bold";
                    this.fontSize = 48;
                    this.fontFamily = "Helvetica Neue";
                };
            };
            style = StyleManager.getStyleDeclaration(".helvNeueMed18white");
            if (!style){
                style = new CSSStyleDeclaration();
                StyleManager.setStyleDeclaration(".helvNeueMed18white", style, false);
            };
            if (style.factory == null){
                style.factory = function ():void{
                    this.color = 0xFFFFFF;
                    this.fontWeight = "normal";
                    this.fontSize = 18;
                    this.fontFamily = "Helvetica Neue";
                };
            };
            style = StyleManager.getStyleDeclaration(".helvetica14White");
            if (!style){
                style = new CSSStyleDeclaration();
                StyleManager.setStyleDeclaration(".helvetica14White", style, false);
            };
            if (style.factory == null){
                style.factory = function ():void{
                    this.color = 0xFFFFFF;
                    this.fontWeight = "normal";
                    this.fontSize = 14;
                    this.fontFamily = "Helvetica Neue";
                };
            };
            style = StyleManager.getStyleDeclaration(".helvetica68GreyBold");
            if (!style){
                style = new CSSStyleDeclaration();
                StyleManager.setStyleDeclaration(".helvetica68GreyBold", style, false);
            };
            if (style.factory == null){
                style.factory = function ():void{
                    this.color = 5920595;
                    this.fontWeight = "bold";
                    this.fontSize = 68;
                    this.fontFamily = "Helvetica Neue";
                };
            };
            style = StyleManager.getStyleDeclaration("Box");
            if (!style){
                style = new CSSStyleDeclaration();
                StyleManager.setStyleDeclaration("Box", style, false);
            };
            style = StyleManager.getStyleDeclaration(".subheaderblue");
            if (!style){
                style = new CSSStyleDeclaration();
                StyleManager.setStyleDeclaration(".subheaderblue", style, false);
            };
            if (style.factory == null){
                style.factory = function ():void{
                    this.color = 2729955;
                    this.fontWeight = "normal";
                    this.fontSize = 18;
                    this.fontFamily = "Trebuchet MS";
                };
            };
            style = StyleManager.getStyleDeclaration(".infoPlacemarkBadge");
            if (!style){
                style = new CSSStyleDeclaration();
                StyleManager.setStyleDeclaration(".infoPlacemarkBadge", style, false);
            };
            if (style.factory == null){
                style.factory = function ():void{
                    this.textAlign = "center";
                    this.color = 0x333333;
                    this.fontWeight = "normal";
                    this.fontSize = 14;
                    this.fontFamily = "Helvetica Neue";
                };
            };
            style = StyleManager.getStyleDeclaration("ScrollBar");
            if (!style){
                style = new CSSStyleDeclaration();
                StyleManager.setStyleDeclaration("ScrollBar", style, false);
            };
            if (style.factory == null){
                style.factory = function ():void{
                    this.thumbUpSkin = _embed_css_styles_scrollbar_scrollThumb_png_843266370;
                    this.downArrowSkin = _embed_css_styles_scrollbar_downArrow_png_105236926;
                    this.thumbDownSkin = _embed_css_styles_scrollbar_scrollThumb_png_843266370;
                    this.trackSkin = _embed_css_styles_scrollbar_track_png_1741243514;
                    this.upArrowSkin = _embed_css_styles_scrollbar_upArrow_png_81364740;
                    this.thumbOverSkin = _embed_css_styles_scrollbar_scrollThumb_png_843266370;
                };
            };
            style = StyleManager.getStyleDeclaration(".dateSelected");
            if (!style){
                style = new CSSStyleDeclaration();
                StyleManager.setStyleDeclaration(".dateSelected", style, false);
            };
            if (style.factory == null){
                style.factory = function ():void{
                    this.color = 2729955;
                    this.fontWeight = "normal";
                    this.fontface = "Trebuchet MS";
                    this.fontSize = 10;
                    this.fontFamily = "Trebuchet MS";
                };
            };
            style = StyleManager.getStyleDeclaration(".alignMiddleCenterContainer");
            if (!style){
                style = new CSSStyleDeclaration();
                StyleManager.setStyleDeclaration(".alignMiddleCenterContainer", style, false);
            };
            if (style.factory == null){
                style.factory = function ():void{
                    this.horizontalAlign = "center";
                    this.verticalAlign = "middle";
                };
            };
            style = StyleManager.getStyleDeclaration(".helvetica14LightGrey");
            if (!style){
                style = new CSSStyleDeclaration();
                StyleManager.setStyleDeclaration(".helvetica14LightGrey", style, false);
            };
            if (style.factory == null){
                style.factory = function ():void{
                    this.color = 5920595;
                    this.fontSize = 14;
                    this.fontFamily = "Helvetica Neue";
                };
            };
            style = StyleManager.getStyleDeclaration(".videoControlBarPlayButton");
            if (!style){
                style = new CSSStyleDeclaration();
                StyleManager.setStyleDeclaration(".videoControlBarPlayButton", style, false);
            };
            if (style.factory == null){
                style.factory = function ():void{
                    this.disabledIcon = _embed_css_assets_icons_play_png_635297854;
                    this.borderColor = 0xFFFFFF;
                    this.selectedDisabledIcon = _embed_css_assets_icons_pause_png_1258992250;
                    this.fillAlphas = [1, 1, 1, 1];
                    this.selectedOverIcon = _embed_css_assets_icons_pause_png_1258992250;
                    this.fillColors = [11319487, 11319487, 0x666666, 0x666666];
                    this.overIcon = _embed_css_assets_icons_play_png_635297854;
                    this.upIcon = _embed_css_assets_icons_play_png_635297854;
                    this.selectedUpIcon = _embed_css_assets_icons_pause_png_1258992250;
                    this.selectedDownIcon = _embed_css_assets_icons_pause_png_1258992250;
                    this.downIcon = _embed_css_assets_icons_play_png_635297854;
                };
            };
            style = StyleManager.getStyleDeclaration(".dropDownItemRenderer");
            if (!style){
                style = new CSSStyleDeclaration();
                StyleManager.setStyleDeclaration(".dropDownItemRenderer", style, false);
            };
            style = StyleManager.getStyleDeclaration(".videoControlBarMuteButton");
            if (!style){
                style = new CSSStyleDeclaration();
                StyleManager.setStyleDeclaration(".videoControlBarMuteButton", style, false);
            };
            if (style.factory == null){
                style.factory = function ():void{
                    this.borderColor = 0xFFFFFF;
                    this.fillAlphas = [1, 1, 1, 1];
                    this.fillColors = [11319487, 11319487, 0x666666, 0x666666];
                    this.icon = _embed_css_assets_icons_mute_png_312419448;
                };
            };
            style = StyleManager.getStyleDeclaration(".accordionCanvas");
            if (!style){
                style = new CSSStyleDeclaration();
                StyleManager.setStyleDeclaration(".accordionCanvas", style, false);
            };
            if (style.factory == null){
                style.factory = function ():void{
                    this.color = "#728289";
                    this.fontSize = "12";
                    this.backgroundAlpha = "0";
                    this.backgroundImage = _embed_css_styles_accordion_header_up_png_102004674;
                    this.fontFamily = "Helvetica Neue";
                };
            };
            style = StyleManager.getStyleDeclaration(".infoDayBadgeDay");
            if (!style){
                style = new CSSStyleDeclaration();
                StyleManager.setStyleDeclaration(".infoDayBadgeDay", style, false);
            };
            if (style.factory == null){
                style.factory = function ():void{
                    this.textAlign = "center";
                    this.color = 0x333333;
                    this.fontWeight = "normal";
                    this.fontSize = 12;
                    this.fontFamily = "Helvetica Neue";
                };
            };
            style = StyleManager.getStyleDeclaration("RadioButton");
            if (!style){
                style = new CSSStyleDeclaration();
                StyleManager.setStyleDeclaration("RadioButton", style, false);
            };
            if (style.factory == null){
                style.factory = function ():void{
                    this.selectedDisabledIcon = _embed_css_assets_radio_button_ON_png_1114731548;
                    this.fontWeight = "normal";
                    this.selectedOverIcon = _embed_css_assets_radio_button_ON_png_1114731548;
                    this.textRollOverColor = 5920595;
                    this.overIcon = _embed_css_assets_radio_button_OFF_png_339325176;
                    this.fontSize = 14;
                    this.selectedDownIcon = _embed_css_assets_radio_button_ON_png_1114731548;
                    this.fontFamily = "Helvetica Neue";
                    this.disabledIcon = _embed_css_assets_radio_button_OFF_png_339325176;
                    this.textAlign = "left";
                    this.color = 5920595;
                    this.upIcon = _embed_css_assets_radio_button_OFF_png_339325176;
                    this.selectedUpIcon = _embed_css_assets_radio_button_ON_png_1114731548;
                    this.textSelectedColor = 5920595;
                    this.downIcon = _embed_css_assets_radio_button_OFF_png_339325176;
                };
            };
            style = StyleManager.getStyleDeclaration(".helvetica12White");
            if (!style){
                style = new CSSStyleDeclaration();
                StyleManager.setStyleDeclaration(".helvetica12White", style, false);
            };
            if (style.factory == null){
                style.factory = function ():void{
                    this.color = 0xFFFFFF;
                    this.fontWeight = "normal";
                    this.fontSize = 12;
                    this.fontFamily = "Helvetica Neue";
                };
            };
            style = StyleManager.getStyleDeclaration(".comboDropDown");
            if (!style){
                style = new CSSStyleDeclaration();
                StyleManager.setStyleDeclaration(".comboDropDown", style, false);
            };
            if (style.factory == null){
                style.factory = function ():void{
                    this.color = 1999536;
                    this.fontSize = 12;
                    this.fontFamily = "Helvetica Neue";
                };
            };
            style = StyleManager.getStyleDeclaration(".balanceButton");
            if (!style){
                style = new CSSStyleDeclaration();
                StyleManager.setStyleDeclaration(".balanceButton", style, false);
            };
            if (style.factory == null){
                style.factory = function ():void{
                    this.upSkin = BlankSkin;
                    this.downSkin = BlankSkin;
                    this.overSkin = BlankSkin;
                    this.disabledSkin = BlankSkin;
                };
            };
            style = StyleManager.getStyleDeclaration(".badgePopUp");
            if (!style){
                style = new CSSStyleDeclaration();
                StyleManager.setStyleDeclaration(".badgePopUp", style, false);
            };
            if (style.factory == null){
                style.factory = function ():void{
                    this.borderColor = 9471879;
                    this.modalTransparency = 0.4;
                    this.fillAlphas = [1, 0.5];
                    this.cornerRadius = 20;
                    this.gradientType = "linear";
                    this.modalTransparencyBlur = 10;
                    this.fillColors = [0xFFFFFF, 0xFFFFFF];
                    this.modalTransparencyColor = 0xFFFFFF;
                    this.borderSkin = RoundedGradRectSkin;
                    this.borderThickness = 2;
                    this.modalTransparencyDuration = 500;
                };
            };
            style = StyleManager.getStyleDeclaration("HRule");
            if (!style){
                style = new CSSStyleDeclaration();
                StyleManager.setStyleDeclaration("HRule", style, false);
            };
            if (style.factory == null){
                style.factory = function ():void{
                    this.strokeWidth = 1;
                    this.strokeColor = 5920595;
                };
            };
            style = StyleManager.getStyleDeclaration(".weekdaySelected");
            if (!style){
                style = new CSSStyleDeclaration();
                StyleManager.setStyleDeclaration(".weekdaySelected", style, false);
            };
            if (style.factory == null){
                style.factory = function ():void{
                    this.color = 2729955;
                    this.fontSize = 16;
                    this.fontFamily = "Helvetica Neue";
                };
            };
            style = StyleManager.getStyleDeclaration(".drinksSelected");
            if (!style){
                style = new CSSStyleDeclaration();
                StyleManager.setStyleDeclaration(".drinksSelected", style, false);
            };
            if (style.factory == null){
                style.factory = function ():void{
                    this.color = 2729955;
                    this.fontWeight = "normal";
                    this.fontSize = 42;
                    this.fontFamily = "Helvetica Neue";
                };
            };
            style = StyleManager.getStyleDeclaration(".helvetica14WhiteBold");
            if (!style){
                style = new CSSStyleDeclaration();
                StyleManager.setStyleDeclaration(".helvetica14WhiteBold", style, false);
            };
            if (style.factory == null){
                style.factory = function ():void{
                    this.color = 0xFFFFFF;
                    this.fontWeight = "bold";
                    this.fontSize = 14;
                    this.fontFamily = "Helvetica Neue";
                };
            };
            style = StyleManager.getStyleDeclaration(".headerblack");
            if (!style){
                style = new CSSStyleDeclaration();
                StyleManager.setStyleDeclaration(".headerblack", style, false);
            };
            if (style.factory == null){
                style.factory = function ():void{
                    this.color = 0x666666;
                    this.fontWeight = "bold";
                    this.fontSize = 22;
                    this.fontFamily = "Trebuchet MS";
                };
            };
            style = StyleManager.getStyleDeclaration(".roundedGradCanvas");
            if (!style){
                style = new CSSStyleDeclaration();
                StyleManager.setStyleDeclaration(".roundedGradCanvas", style, false);
            };
            if (style.factory == null){
                style.factory = function ():void{
                    this.borderSkin = RoundedGradRectCopyBoxSkin;
                };
            };
            style = StyleManager.getStyleDeclaration(".stopWatchButton");
            if (!style){
                style = new CSSStyleDeclaration();
                StyleManager.setStyleDeclaration(".stopWatchButton", style, false);
            };
            if (style.factory == null){
                style.factory = function ():void{
                    this.disabledIcon = _embed_css_assets_radio_button_OFF_png_339325176;
                    this.textAlign = "left";
                    this.color = 5920595;
                    this.fontWeight = "normal";
                    this.textRollOverColor = 5920595;
                    this.overIcon = _embed_css_assets_radio_button_OFF_png_339325176;
                    this.upIcon = _embed_css_assets_radio_button_OFF_png_339325176;
                    this.fontSize = 14;
                    this.textSelectedColor = 5920595;
                    this.fontFamily = "Helvetica Neue";
                    this.downIcon = _embed_css_assets_radio_button_OFF_png_339325176;
                };
            };
            style = StyleManager.getStyleDeclaration(".balanceSlider");
            if (!style){
                style = new CSSStyleDeclaration();
                StyleManager.setStyleDeclaration(".balanceSlider", style, false);
            };
            if (style.factory == null){
                style.factory = function ():void{
                    this.trackSkin = _embed_css____assets_transparent_png_991515083;
                    this.thumbSkin = _embed_css____assets_slider_thumb_white_png_548198765;
                };
            };
            style = StyleManager.getStyleDeclaration(".helvNeue18white");
            if (!style){
                style = new CSSStyleDeclaration();
                StyleManager.setStyleDeclaration(".helvNeue18white", style, false);
            };
            if (style.factory == null){
                style.factory = function ():void{
                    this.color = 0xFFFFFF;
                    this.fontWeight = "normal";
                    this.fontSize = 18;
                    this.fontFamily = "Helvetica Neue";
                };
            };
            style = StyleManager.getStyleDeclaration(".weekday");
            if (!style){
                style = new CSSStyleDeclaration();
                StyleManager.setStyleDeclaration(".weekday", style, false);
            };
            if (style.factory == null){
                style.factory = function ():void{
                    this.color = 0;
                    this.fontWeight = "bold";
                    this.fontSize = 16;
                    this.fontFamily = "Helvetica Neue";
                };
            };
            style = StyleManager.getStyleDeclaration(".balanceSliderNumbersWillpower");
            if (!style){
                style = new CSSStyleDeclaration();
                StyleManager.setStyleDeclaration(".balanceSliderNumbersWillpower", style, false);
            };
            if (style.factory == null){
                style.factory = function ():void{
                    this.labelStyleName = "sliderNumberLargeWillpower";
                    this.slideEasingFunction = "mx.effects.easing.Bounce.easeOut";
                    this.trackSkin = _embed_css____assets_transparent_png_991515083;
                    this.thumbSkin = _embed_css____assets_balance_slider_large_green_png_725001071;
                    this.labelOffset = 15;
                    this.tickLength = 0;
                };
            };
            style = StyleManager.getStyleDeclaration(".helvNeue14white");
            if (!style){
                style = new CSSStyleDeclaration();
                StyleManager.setStyleDeclaration(".helvNeue14white", style, false);
            };
            if (style.factory == null){
                style.factory = function ():void{
                    this.color = 0xFFFFFF;
                    this.fontWeight = "normal";
                    this.fontSize = 14;
                    this.fontFamily = "Helvetica Neue";
                };
            };
            style = StyleManager.getStyleDeclaration(".helvetica14Grey");
            if (!style){
                style = new CSSStyleDeclaration();
                StyleManager.setStyleDeclaration(".helvetica14Grey", style, false);
            };
            if (style.factory == null){
                style.factory = function ():void{
                    this.color = 5920595;
                    this.fontSize = 14;
                    this.fontFamily = "Helvetica Neue";
                };
            };
            style = StyleManager.getStyleDeclaration(".sliderNumberLargeWillpower");
            if (!style){
                style = new CSSStyleDeclaration();
                StyleManager.setStyleDeclaration(".sliderNumberLargeWillpower", style, false);
            };
            if (style.factory == null){
                style.factory = function ():void{
                    this.color = 8892465;
                    this.fontSize = 12;
                    this.fontFamily = "Helvetica Neue";
                };
            };
            style = StyleManager.getStyleDeclaration(".helvNeueMed14white");
            if (!style){
                style = new CSSStyleDeclaration();
                StyleManager.setStyleDeclaration(".helvNeueMed14white", style, false);
            };
            if (style.factory == null){
                style.factory = function ():void{
                    this.color = 0xFFFFFF;
                    this.fontWeight = "normal";
                    this.fontSize = 14;
                    this.fontFamily = "Helvetica Neue";
                };
            };
            style = StyleManager.getStyleDeclaration(".helvetica18GreyBold");
            if (!style){
                style = new CSSStyleDeclaration();
                StyleManager.setStyleDeclaration(".helvetica18GreyBold", style, false);
            };
            if (style.factory == null){
                style.factory = function ():void{
                    this.color = 5920595;
                    this.fontWeight = "bold";
                    this.fontSize = 18;
                    this.fontFamily = "Helvetica Neue";
                };
            };
            style = StyleManager.getStyleDeclaration(".header1");
            if (!style){
                style = new CSSStyleDeclaration();
                StyleManager.setStyleDeclaration(".header1", style, false);
            };
            if (style.factory == null){
                style.factory = function ():void{
                    this.fontSize = 22;
                    this.fontFamily = "Arial";
                };
            };
            style = StyleManager.getStyleDeclaration(".drinks");
            if (!style){
                style = new CSSStyleDeclaration();
                StyleManager.setStyleDeclaration(".drinks", style, false);
            };
            if (style.factory == null){
                style.factory = function ():void{
                    this.color = 0x666666;
                    this.fontWeight = "normal";
                    this.fontSize = 42;
                    this.fontFamily = "Helvetica Neue";
                };
            };
            style = StyleManager.getStyleDeclaration(".balanceSliderNumbersEmotion");
            if (!style){
                style = new CSSStyleDeclaration();
                StyleManager.setStyleDeclaration(".balanceSliderNumbersEmotion", style, false);
            };
            if (style.factory == null){
                style.factory = function ():void{
                    this.labelStyleName = "sliderNumberLarge";
                    this.slideEasingFunction = "mx.effects.easing.Bounce.easeOut";
                    this.trackSkin = _embed_css____assets_transparent_png_991515083;
                    this.thumbSkin = _embed_css____assets_balance_slider_large_orange_png_2122909247;
                    this.labelOffset = 15;
                    this.tickLength = 0;
                };
            };
            style = StyleManager.getStyleDeclaration(".registrationInputLabel");
            if (!style){
                style = new CSSStyleDeclaration();
                StyleManager.setStyleDeclaration(".registrationInputLabel", style, false);
            };
            if (style.factory == null){
                style.factory = function ():void{
                    this.color = 5920595;
                    this.fontSize = 11;
                    this.fontFamily = "Helvetica Neue";
                };
            };
            style = StyleManager.getStyleDeclaration(".description");
            if (!style){
                style = new CSSStyleDeclaration();
                StyleManager.setStyleDeclaration(".description", style, false);
            };
            if (style.factory == null){
                style.factory = function ():void{
                    this.color = 0;
                    this.fontSize = 16;
                    this.fontFamily = "Helvetica Neue";
                };
            };
            style = StyleManager.getStyleDeclaration(".closeButton");
            if (!style){
                style = new CSSStyleDeclaration();
                StyleManager.setStyleDeclaration(".closeButton", style, false);
            };
            if (style.factory == null){
                style.factory = function ():void{
                    this.skin = _embed_css_styles_close_btn_png_1779263772;
                };
            };
            style = StyleManager.getStyleDeclaration(".sliderNumberLargeMotivation");
            if (!style){
                style = new CSSStyleDeclaration();
                StyleManager.setStyleDeclaration(".sliderNumberLargeMotivation", style, false);
            };
            if (style.factory == null){
                style.factory = function ():void{
                    this.color = 2263998;
                    this.fontSize = 12;
                    this.fontFamily = "Helvetica Neue";
                };
            };
            style = StyleManager.getStyleDeclaration(".balanceSliderNumbersMotivation");
            if (!style){
                style = new CSSStyleDeclaration();
                StyleManager.setStyleDeclaration(".balanceSliderNumbersMotivation", style, false);
            };
            if (style.factory == null){
                style.factory = function ():void{
                    this.labelStyleName = "sliderNumberLargeMotivation";
                    this.slideEasingFunction = "mx.effects.easing.Bounce.easeOut";
                    this.trackSkin = _embed_css____assets_transparent_png_991515083;
                    this.thumbSkin = _embed_css____assets_balance_slider_large_blue_png_1163403871;
                    this.labelOffset = 15;
                    this.tickLength = 0;
                };
            };
            style = StyleManager.getStyleDeclaration(".balanceSlider1");
            if (!style){
                style = new CSSStyleDeclaration();
                StyleManager.setStyleDeclaration(".balanceSlider1", style, false);
            };
            if (style.factory == null){
                style.factory = function ():void{
                    this.thumbSkin = _embed_css____assets_balance_slider_png_1098006639;
                };
            };
            style = StyleManager.getStyleDeclaration(".registrationLabel");
            if (!style){
                style = new CSSStyleDeclaration();
                StyleManager.setStyleDeclaration(".registrationLabel", style, false);
            };
            if (style.factory == null){
                style.factory = function ():void{
                    this.color = 5920595;
                    this.fontSize = 12;
                    this.fontFamily = "Helvetica Neue";
                };
            };
            var _local2 = StyleManager;
            _local2.mx_internal::initProtoChainRoots();
        }
        public function set header(_arg1:Header):void{
            var _local2:Object = this._1221270899header;
            if (_local2 !== _arg1){
                this._1221270899header = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "header", _local2, _arg1));
            };
        }
        private function _Main_HTTPService1_i():HTTPService{
            var _local1:HTTPService = new HTTPService();
            httpService = _local1;
            _local1.url = "styles/runtime_stylesheet.css";
            _local1.resultFormat = "text";
            _local1.addEventListener("result", __httpService_result);
            _local1.initialized(this, "httpService");
            return (_local1);
        }
        private function get footerX():Number{
            return (this._680002019footerX);
        }
        public function set contentContainer(_arg1:HBox):void{
            var _local2:Object = this._1707945992contentContainer;
            if (_local2 !== _arg1){
                this._1707945992contentContainer = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "contentContainer", _local2, _arg1));
            };
        }
        public function set httpService(_arg1:HTTPService):void{
            var _local2:Object = this._1456329459httpService;
            if (_local2 !== _arg1){
                this._1456329459httpService = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "httpService", _local2, _arg1));
            };
        }
        public function get mainViewContainer():MainView{
            return (this._1407821277mainViewContainer);
        }
        private function get model():BalanceModelLocator{
            return (this._104069929model);
        }
        private function languageReady():void{
            currentState = "active";
            trace(("THIS IS A LOCALE DATE STRING!" + model.languageVO.getLocalDateString(new Date())));
        }
        private function _Main_Label1_i():Label{
            var _local1:Label = new Label();
            debug = _local1;
            _local1.width = 100;
            _local1.selectable = true;
            _local1.x = 0;
            _local1.id = "debug";
            BindingManager.executeBindings(this, "debug", debug);
            if (!_local1.document){
                _local1.document = this;
            };
            return (_local1);
        }
        public function set controller(_arg1:BalanceController):void{
            var _local2:Object = this._637428636controller;
            if (_local2 !== _arg1){
                this._637428636controller = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "controller", _local2, _arg1));
            };
        }
        private function set footerX(_arg1:Number):void{
            var _local2:Object = this._680002019footerX;
            if (_local2 !== _arg1){
                this._680002019footerX = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "footerX", _local2, _arg1));
            };
        }
        private function _Main_State2_c():State{
            var _local1:State = new State();
            _local1.name = "active";
            _local1.overrides = [_Main_AddChild1_c()];
            return (_local1);
        }
        public function set footer(_arg1:Footer):void{
            var _local2:Object = this._1268861541footer;
            if (_local2 !== _arg1){
                this._1268861541footer = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "footer", _local2, _arg1));
            };
        }
        private function _Main_Header1_i():Header{
            var _local1:Header = new Header();
            header = _local1;
            _local1.percentWidth = 100;
            _local1.minWidth = 1000;
            _local1.height = 100;
            _local1.id = "header";
            if (!_local1.document){
                _local1.document = this;
            };
            return (_local1);
        }
        private function _Main_bindingsSetup():Array{
            var binding:* = null;
            var result:* = [];
            binding = new Binding(this, function ():Object{
                return (Assets.getInstance().balance_logo);
            }, function (_arg1:Object):void{
                _Main_Image1.source = _arg1;
            }, "_Main_Image1.source");
            result[0] = binding;
            binding = new Binding(this, function ():Number{
                return (header.height);
            }, function (_arg1:Number):void{
                contentContainer.y = _arg1;
            }, "contentContainer.y");
            result[1] = binding;
            binding = new Binding(this, function ():Number{
                return (header.height);
            }, function (_arg1:Number):void{
                mainViewContainer.yOffset = _arg1;
            }, "mainViewContainer.yOffset");
            result[2] = binding;
            binding = new Binding(this, function ():Boolean{
                return (model.showControls);
            }, function (_arg1:Boolean):void{
                footer.visible = _arg1;
            }, "footer.visible");
            result[3] = binding;
            binding = new Binding(this, function ():Number{
                return ((Math.max(750, appCanvas.height) - footer.height));
            }, function (_arg1:Number):void{
                footer.y = _arg1;
            }, "footer.y");
            result[4] = binding;
            binding = new Binding(this, function ():Number{
                return (footerX);
            }, function (_arg1:Number):void{
                footer.x = _arg1;
            }, "footer.x");
            result[5] = binding;
            binding = new Binding(this, function ():Number{
                return (0);
            }, function (_arg1:Number):void{
                debug.y = _arg1;
            }, "debug.y");
            result[6] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = model.currentContentCode;
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                debug.text = _arg1;
            }, "debug.text");
            result[7] = binding;
            binding = new Binding(this, function ():Boolean{
                return (model.isDebugMode);
            }, function (_arg1:Boolean):void{
                debug.visible = _arg1;
            }, "debug.visible");
            result[8] = binding;
            return (result);
        }
        private function doTransitionPopup():void{
            transitionPopup = PopUpManager.createPopUp(this, TransitionPopUp, true);
            PopUpManager.centerPopUp(transitionPopup);
        }
        public function set mainViewContainer(_arg1:MainView):void{
            var _local2:Object = this._1407821277mainViewContainer;
            if (_local2 !== _arg1){
                this._1407821277mainViewContainer = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "mainViewContainer", _local2, _arg1));
            };
        }
        public function toggleFullScreen(_arg1=null):void{
            var _local2:JavaScriptUtils = new JavaScriptUtils();
            _local2.toggleWindowSize();
        }
        private function set model(_arg1:BalanceModelLocator):void{
            var _local2:Object = this._104069929model;
            if (_local2 !== _arg1){
                this._104069929model = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "model", _local2, _arg1));
            };
        }
        public function get debug():Label{
            return (this._95458899debug);
        }
        private function transitionBlur(_arg1:Boolean):void{
            if (_arg1){
                doTransitionPopup();
            } else {
                hideTransitionPopup();
            };
        }
        private function applicationCompleteHandler():void{
            root.stage.addEventListener(Event.RESIZE, resizeHandler);
            resizeHandler(new Event(Event.INIT));
            if (ExternalInterface.available){
                ExternalInterface.addCallback("showToggle", showToggle);
                ExternalInterface.addCallback("setToggleStatusFullScreen", setToggleStatusFullScreen);
                ExternalInterface.addCallback("setToggleStatusNormal", setToggleStatusNormal);
            };
        }
        private function _Main_Image1_i():Image{
            var _local1:Image = new Image();
            _Main_Image1 = _local1;
            _local1.id = "_Main_Image1";
            BindingManager.executeBindings(this, "_Main_Image1", _Main_Image1);
            if (!_local1.document){
                _local1.document = this;
            };
            return (_local1);
        }
        private function _Main_State1_c():State{
            var _local1:State = new State();
            _local1.name = "defaultState";
            return (_local1);
        }
        public function __httpService_result(_arg1:ResultEvent):void{
            httpService_result(_arg1);
        }
        private function _Main_Footer1_i():Footer{
            var _local1:Footer = new Footer();
            footer = _local1;
            _local1.verticalScrollPolicy = "off";
            _local1.horizontalScrollPolicy = "off";
            _local1.addEventListener("creationComplete", __footer_creationComplete);
            _local1.id = "footer";
            BindingManager.executeBindings(this, "footer", footer);
            if (!_local1.document){
                _local1.document = this;
            };
            return (_local1);
        }
        public function __footer_creationComplete(_arg1:FlexEvent):void{
            initFooterBadge(_arg1);
        }
        private function checkIsActiveSession():void{
            var _local1:CairngormEvent = new CairngormEvent(BalanceController.IS_ACTIVE_SESSION);
            _local1.dispatch();
        }
        public function openGenericPopup(_arg1:String, _arg2:String):void{
            genericPopup = QuizPopUp(PopUpManager.createPopUp(this, QuizPopUp, true));
            genericPopup.headerCopy = _arg1;
            genericPopup.bodyCopy = _arg2;
            PopUpManager.centerPopUp(genericPopup);
        }
        private function iniCheckIsActiveSession():void{
            setInterval(checkIsActiveSession, 500);
        }
        private function closeModulePopup(_arg1:Event=null):void{
            modulePopup.removeEventListener(PopupSelectedEvent.POP_UP_CLOSE, closeModulePopup);
            PopUpManager.removePopUp(modulePopup);
        }
        private function _Main_BalanceController1_i():BalanceController{
            var _local1:BalanceController = new BalanceController();
            controller = _local1;
            return (_local1);
        }
        private function httpService_result(_arg1:ResultEvent):void{
            var _local2:String = (_arg1.result as String);
            model.balanceStyleSheet = new StyleSheet();
            model.balanceStyleSheet.parseCSS(_local2);
        }
        override public function initialize():void{
            var target:* = null;
            var watcherSetupUtilClass:* = null;
            mx_internal::setDocumentDescriptor(_documentDescriptor_);
            var bindings:* = _Main_bindingsSetup();
            var watchers:* = [];
            target = this;
            if (_watcherSetupUtil == null){
                watcherSetupUtilClass = getDefinitionByName("_MainWatcherSetupUtil");
                var _local2 = watcherSetupUtilClass;
                _local2["init"](null);
            };
            _watcherSetupUtil.setup(this, function (_arg1:String){
                return (target[_arg1]);
            }, bindings, watchers);
            var i:* = 0;
            while (i < bindings.length) {
                Binding(bindings[i]).execute();
                i = (i + 1);
            };
            mx_internal::_bindings = mx_internal::_bindings.concat(bindings);
            mx_internal::_watchers = mx_internal::_watchers.concat(watchers);
            super.initialize();
        }
        private function initFooterBadge(_arg1:FlexEvent):void{
            footer.addEventListener(MouseEvent.CLICK, badgeClick, true);
        }
        private function set toggleToolTipText(_arg1):void{
            var _local2:Object = this._61913020toggleToolTipText;
            if (_local2 !== _arg1){
                this._61913020toggleToolTipText = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "toggleToolTipText", _local2, _arg1));
            };
        }
        private function _Main_Canvas1_i():Canvas{
            var _local1:Canvas = new Canvas();
            appCanvas = _local1;
            _local1.percentWidth = 100;
            _local1.percentHeight = 100;
            _local1.verticalScrollPolicy = "auto";
            _local1.horizontalScrollPolicy = "off";
            _local1.id = "appCanvas";
            if (!_local1.document){
                _local1.document = this;
            };
            _local1.addChild(_Main_HBox1_c());
            _local1.addChild(_Main_Header1_i());
            _local1.addChild(_Main_HBox2_i());
            _local1.addChild(_Main_Footer1_i());
            _local1.addChild(_Main_Label1_i());
            return (_local1);
        }
        public function ___Main_Application1_creationComplete(_arg1:FlexEvent):void{
            init();
        }
        public function get footer():Footer{
            return (this._1268861541footer);
        }
        private function badgeClick(_arg1:MouseEvent):void{
            var _local2:IFlexDisplayObject = PopUpManager.createPopUp(this, BadgePopUp, true);
            PopUpManager.centerPopUp(_local2);
        }
        public function set debug(_arg1:Label):void{
            var _local2:Object = this._95458899debug;
            if (_local2 !== _arg1){
                this._95458899debug = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "debug", _local2, _arg1));
            };
        }
        public function get controller():BalanceController{
            return (this._637428636controller);
        }
        public function showToggle(){
        }
        private function hideTransitionPopup():void{
            PopUpManager.removePopUp(transitionPopup);
        }
        public function set appCanvas(_arg1:Canvas):void{
            var _local2:Object = this._1024130777appCanvas;
            if (_local2 !== _arg1){
                this._1024130777appCanvas = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "appCanvas", _local2, _arg1));
            };
        }
        private function gatewayReady():void{
            var _local1:CairngormEvent = new CairngormEvent(BalanceController.GET_LANGUAGE_COMMAND);
            _local1.data = new Object();
            _local1.data.callBackObject = this;
            _local1.data.callBackFunction = languageReady;
            _local1.dispatch();
        }
        private function _Main_bindingExprs():void{
            var _local1:*;
            _local1 = Assets.getInstance().balance_logo;
            _local1 = header.height;
            _local1 = header.height;
            _local1 = model.showControls;
            _local1 = (Math.max(750, appCanvas.height) - footer.height);
            _local1 = footerX;
            _local1 = 0;
            _local1 = model.currentContentCode;
            _local1 = model.isDebugMode;
        }
        private function get toggleToolTipText(){
            return (this._61913020toggleToolTipText);
        }
        public function testExit(){
            var _local1:CairngormEvent = new CairngormEvent(BalanceController.EXIT_COMMAND);
            _local1.dispatch();
        }
        private function _Main_HBox2_i():HBox{
            var _local1:HBox = new HBox();
            contentContainer = _local1;
            _local1.percentWidth = 100;
            _local1.percentHeight = 100;
            _local1.setStyle("horizontalAlign", "center");
            _local1.setStyle("verticalAlign", "top");
            _local1.setStyle("paddingTop", 0);
            _local1.setStyle("paddingBottom", 0);
            _local1.setStyle("paddingLeft", 0);
            _local1.setStyle("paddingRight", 0);
            _local1.id = "contentContainer";
            BindingManager.executeBindings(this, "contentContainer", contentContainer);
            if (!_local1.document){
                _local1.document = this;
            };
            _local1.addChild(_Main_MainView1_i());
            return (_local1);
        }
        private function _Main_AddChild1_c():AddChild{
            var _local1:AddChild = new AddChild();
            _local1.targetFactory = new DeferredInstanceFromFunction(_Main_Canvas1_i);
            return (_local1);
        }
        public function get appCanvas():Canvas{
            return (this._1024130777appCanvas);
        }
        public function get header():Header{
            return (this._1221270899header);
        }
        public function setToggleStatusFullScreen(){
            trace("toggle full screen");
            model.isFullScreen = true;
            this.dispatchEvent(new Event(ScreenToggleEvent.SCREEN_TOGGLE));
        }
        public function launchModulePopup(_arg1:String):void{
            modulePopup = PopUpManager.createPopUp(this, PopUpModuleLoader, true);
            modulePopup.addEventListener(PopupSelectedEvent.POP_UP_CLOSE, closeModulePopup);
            PopUpModuleLoader(modulePopup).moduleUrl = model.dropDownMenuVO.getModuleURL(_arg1);
            PopUpManager.centerPopUp(modulePopup);
        }
        public function setToggleStatusNormal(){
            model.isFullScreen = false;
            dispatchEvent(new Event(ScreenToggleEvent.SCREEN_TOGGLE));
        }
        public function ___Main_Application1_applicationComplete(_arg1:FlexEvent):void{
            applicationCompleteHandler();
        }
        private function _Main_MainView1_i():MainView{
            var _local1:MainView = new MainView();
            mainViewContainer = _local1;
            _local1.setStyle("borderThickness", 1);
            _local1.id = "mainViewContainer";
            BindingManager.executeBindings(this, "mainViewContainer", mainViewContainer);
            if (!_local1.document){
                _local1.document = this;
            };
            return (_local1);
        }
        private function resizeHandler(_arg1:Event):void{
            model.currentStageWidth = root.stage.width;
            model.currentStageHeight = root.stage.height;
            footerX = (model.currentStageWidth - 175);
        }

        public static function set watcherSetupUtil(_arg1:IWatcherSetupUtil):void{
            Main._watcherSetupUtil = _arg1;
        }

    }
}//package 
