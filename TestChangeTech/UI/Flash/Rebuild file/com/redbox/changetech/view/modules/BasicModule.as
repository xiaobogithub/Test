//Created by Action Script Viewer - http://www.buraks.com/asv
package com.redbox.changetech.view.modules {
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
    import mx.effects.*;
    import mx.binding.*;
    import com.redbox.changetech.control.*;
    import mx.containers.*;
    import com.redbox.changetech.view.components.*;
    import com.redbox.changetech.model.*;
    import mx.binding.utils.*;
    import com.redbox.changetech.vo.*;
    import flash.utils.*;
    import com.adobe.cairngorm.control.*;
    import flash.system.*;
    import flash.accessibility.*;
    import flash.xml.*;
    import flash.net.*;
    import mx.utils.*;
    import com.redbox.changetech.event.*;
    import com.redbox.changetech.view.templates.*;
    import flash.filters.*;
    import flash.ui.*;
    import mx.effects.easing.*;
    import flash.external.*;
    import flash.debugger.*;
    import flash.errors.*;
    import flash.printing.*;
    import flash.profiler.*;

    public class BasicModule extends AbstractBalanceModule implements IBalanceModule, IBindingClient {

        private var _1304009756template13:CueReactivityPrompt
        private var keyIsDown:Boolean = false
        public var forceCollectionComplete:Boolean = false
        private var imageIsLoaded:Boolean
        mx_internal var _bindingsByDestination:Object
        private var _1981727479template1:PicLeftInfoText
        public var previousContentIndex:int
        private var current_Template:String = null
        private var _404469598fadeOutContainer1:Fade
        private var lastPresenterImageURL:String = "default"
        private var _1981727487template9:PersonalPlan
        private var _1304009802template1a:Quiz
        private var _1981727480template2:PicRightInfoText
        public var currentTemplate:IBalanceModuleTemplate
        private var _335735951fadeInContainer1:Fade
        private var _1304009755template12:Registration
        public var _BasicModule_SetEventHandler10:SetEventHandler
        public var _BasicModule_SetEventHandler11:SetEventHandler
        public var _BasicModule_SetEventHandler12:SetEventHandler
        public var _BasicModule_SetEventHandler13:SetEventHandler
        public var _BasicModule_SetEventHandler14:SetEventHandler
        public var _BasicModule_SetEventHandler15:SetEventHandler
        public var _BasicModule_SetEventHandler16:SetEventHandler
        public var _BasicModule_SetEventHandler17:SetEventHandler
        public var _BasicModule_SetEventHandler18:SetEventHandler
        public var _BasicModule_SetEventHandler19:SetEventHandler
        private var _1981727484template6:TrafficLightResults
        private var _1304009758template15:CompletionGraph
        public var _BasicModule_SetProperty10:SetProperty
        public var _BasicModule_SetProperty11:SetProperty
        public var _BasicModule_SetProperty12:SetProperty
        public var _BasicModule_SetProperty13:SetProperty
        public var _BasicModule_SetProperty14:SetProperty
        public var _BasicModule_SetProperty15:SetProperty
        public var _BasicModule_SetProperty16:SetProperty
        public var _BasicModule_SetProperty17:SetProperty
        public var _BasicModule_SetProperty18:SetProperty
        public var _BasicModule_SetProperty19:SetProperty
        private var tempContentIndex:int
        public var _BasicModule_SetProperty20:SetProperty
        public var _BasicModule_SetProperty21:SetProperty
        public var _BasicModule_SetProperty22:SetProperty
        public var _BasicModule_SetProperty23:SetProperty
        public var _BasicModule_SetProperty24:SetProperty
        public var _BasicModule_SetProperty25:SetProperty
        public var _BasicModule_SetProperty26:SetProperty
        public var _BasicModule_SetProperty28:SetProperty
        public var _BasicModule_SetProperty29:SetProperty
        public var _BasicModule_SetProperty27:SetProperty
        mx_internal var _bindingsBeginWithWord:Object
        public var _BasicModule_SetProperty30:SetProperty
        public var _BasicModule_SetProperty31:SetProperty
        public var _BasicModule_SetProperty32:SetProperty
        public var _BasicModule_SetProperty33:SetProperty
        public var _BasicModule_SetProperty34:SetProperty
        public var _BasicModule_SetProperty35:SetProperty
        public var _BasicModule_SetProperty36:SetProperty
        public var _BasicModule_SetProperty37:SetProperty
        public var _BasicModule_SetProperty38:SetProperty
        public var _BasicModule_SetProperty39:SetProperty
        public var _BasicModule_SetProperty40:SetProperty
        public var _BasicModule_SetProperty41:SetProperty
        public var _BasicModule_SetProperty42:SetProperty
        public var _BasicModule_SetProperty43:SetProperty
        public var _BasicModule_SetProperty44:SetProperty
        public var _BasicModule_SetProperty45:SetProperty
        public var _BasicModule_SetProperty46:SetProperty
        public var _BasicModule_SetProperty47:SetProperty
        public var _BasicModule_SetProperty48:SetProperty
        public var _BasicModule_SetProperty49:SetProperty
        private var _1981727481template3:DailyConsumption
        public var _BasicModule_SetProperty50:SetProperty
        public var _BasicModule_SetProperty51:SetProperty
        public var _BasicModule_SetProperty52:SetProperty
        public var _BasicModule_SetProperty53:SetProperty
        public var _BasicModule_SetProperty54:SetProperty
        public var _BasicModule_SetProperty55:SetProperty
        public var _BasicModule_SetProperty56:SetProperty
        public var _BasicModule_SetProperty57:SetProperty
        public var _BasicModule_SetProperty58:SetProperty
        public var _BasicModule_SetProperty59:SetProperty
        public var _BasicModule_SetProperty1:SetProperty
        public var _BasicModule_SetProperty2:SetProperty
        public var _BasicModule_SetProperty3:SetProperty
        public var _BasicModule_SetProperty4:SetProperty
        public var _BasicModule_SetProperty5:SetProperty
        public var _BasicModule_SetProperty6:SetProperty
        public var _BasicModule_SetProperty7:SetProperty
        public var _BasicModule_SetProperty8:SetProperty
        public var _BasicModule_SetProperty62:SetProperty
        public var _BasicModule_SetProperty64:SetProperty
        public var _BasicModule_SetProperty66:SetProperty
        public var _BasicModule_SetProperty60:SetProperty
        public var _BasicModule_SetProperty61:SetProperty
        public var _BasicModule_SetProperty63:SetProperty
        public var _BasicModule_VBox1:VBox
        public var _BasicModule_SetProperty65:SetProperty
        public var _BasicModule_SetProperty68:SetProperty
        public var _BasicModule_SetProperty69:SetProperty
        public var _BasicModule_SetProperty9:SetProperty
        public var _BasicModule_SetProperty67:SetProperty
        public var _BasicModule_SetProperty70:SetProperty
        public var _BasicModule_SetProperty71:SetProperty
        public var _BasicModule_SetProperty72:SetProperty
        public var _BasicModule_SetProperty73:SetProperty
        public var _BasicModule_SetProperty74:SetProperty
        public var _BasicModule_SetProperty75:SetProperty
        public var _BasicModule_SetProperty76:SetProperty
        public var _BasicModule_SetProperty77:SetProperty
        public var _BasicModule_SetProperty78:SetProperty
        public var _BasicModule_SetProperty79:SetProperty
        public var nextContentIndex:int
        private var _1304009754template11:PersonalValue
        private var _1981727485template7:TypicalWeek
        private var _404469597fadeOutContainer2:Fade
        private var isToCompleteCollection:Boolean = false
        private var _1304009757template14:WeeklyConsumption
        public var _BasicModule_TextArea1:TextArea
        public var _BasicModule_SetEventHandler1:SetEventHandler
        public var _BasicModule_SetEventHandler2:SetEventHandler
        public var _BasicModule_SetEventHandler3:SetEventHandler
        public var _BasicModule_SetEventHandler4:SetEventHandler
        public var _BasicModule_SetEventHandler5:SetEventHandler
        public var _BasicModule_SetEventHandler6:SetEventHandler
        public var _BasicModule_SetEventHandler7:SetEventHandler
        public var _BasicModule_SetEventHandler8:SetEventHandler
        public var _BasicModule_SetEventHandler9:SetEventHandler
        private var _335735950fadeInContainer2:Fade
        private var direction:String
        mx_internal var _watchers:Array
        private var _1304009988template7a:TypicalWeekCompletion
        private var _1810141563bounceDownEffect:Move
        private var _1304009803template1b:GlassPicture
        private var _1981727486template8:ProgressMonitor
        private var _1304009753template10:PersonalValues
        mx_internal var _bindings:Array
        private var _documentDescriptor_:UIComponentDescriptor

        public static var NEXT:String = "NEXT";
        public static var BACK:String = "BACK";
        private static var _watcherSetupUtil:IWatcherSetupUtil;

        public function BasicModule(){
            _documentDescriptor_ = new UIComponentDescriptor({type:AbstractBalanceModule});
            _bindings = [];
            _watchers = [];
            _bindingsByDestination = {};
            _bindingsBeginWithWord = {};
            super();
            mx_internal::_document = this;
            this.states = [_BasicModule_State1_c(), _BasicModule_State2_c(), _BasicModule_State3_c(), _BasicModule_State4_c(), _BasicModule_State5_c(), _BasicModule_State6_c(), _BasicModule_State7_c(), _BasicModule_State8_c(), _BasicModule_State9_c(), _BasicModule_State10_c(), _BasicModule_State11_c(), _BasicModule_State12_c(), _BasicModule_State13_c(), _BasicModule_State14_c(), _BasicModule_State15_c(), _BasicModule_State16_c(), _BasicModule_State17_c()];
            _BasicModule_Move1_i();
            _BasicModule_Fade1_i();
            _BasicModule_Fade2_i();
            _BasicModule_Fade3_i();
            _BasicModule_Fade4_i();
            this.addEventListener("contentChange", ___BasicModule_AbstractBalanceModule1_contentChange);
            this.addEventListener("currentStateChange", ___BasicModule_AbstractBalanceModule1_currentStateChange);
            this.addEventListener("creationComplete", ___BasicModule_AbstractBalanceModule1_creationComplete);
            this.addEventListener("removed", ___BasicModule_AbstractBalanceModule1_removed);
        }
        private function _BasicModule_SetProperty26_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _BasicModule_SetProperty26 = _local1;
            _local1.name = "currentTemplate";
            BindingManager.executeBindings(this, "_BasicModule_SetProperty26", _BasicModule_SetProperty26);
            return (_local1);
        }
        private function _BasicModule_TypicalWeek1_i():TypicalWeek{
            var _local1:TypicalWeek = new TypicalWeek();
            template7 = _local1;
            _local1.id = "template7";
            BindingManager.executeBindings(this, "template7", template7);
            if (!_local1.document){
                _local1.document = this;
            };
            return (_local1);
        }
        override protected function storeAnswer(_arg1:AnswerSelectedEvent):void{
            super.storeAnswer(_arg1);
        }
        private function _BasicModule_AddChild15_c():AddChild{
            var _local1:AddChild = new AddChild();
            _local1.creationPolicy = "all";
            _local1.targetFactory = new DeferredInstanceFromFunction(_BasicModule_CueReactivityPrompt1_i);
            return (_local1);
        }
        private function _BasicModule_SetProperty49_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _BasicModule_SetProperty49 = _local1;
            _local1.name = "ctaBtn";
            BindingManager.executeBindings(this, "_BasicModule_SetProperty49", _BasicModule_SetProperty49);
            return (_local1);
        }
        private function _BasicModule_PersonalValues1_i():PersonalValues{
            var _local1:PersonalValues = new PersonalValues();
            template10 = _local1;
            _local1.id = "template10";
            BindingManager.executeBindings(this, "template10", template10);
            if (!_local1.document){
                _local1.document = this;
            };
            return (_local1);
        }
        public function get template10():PersonalValues{
            return (this._1304009753template10);
        }
        public function get template11():PersonalValue{
            return (this._1304009754template11);
        }
        public function get template12():Registration{
            return (this._1304009755template12);
        }
        private function _BasicModule_State15_c():State{
            var _local1:State = new State();
            _local1.name = "CueReactivityPrompt";
            _local1.overrides = [_BasicModule_AddChild15_c(), _BasicModule_SetEventHandler17_i(), _BasicModule_SetProperty65_i(), _BasicModule_SetProperty66_i(), _BasicModule_SetProperty67_i(), _BasicModule_SetProperty68_i(), _BasicModule_SetProperty69_i()];
            return (_local1);
        }
        public function get template14():WeeklyConsumption{
            return (this._1304009757template14);
        }
        private function _BasicModule_SetEventHandler18_i():SetEventHandler{
            var _local1:SetEventHandler = new SetEventHandler();
            _BasicModule_SetEventHandler18 = _local1;
            _local1.addEventListener("handler", ___BasicModule_SetEventHandler18_handler);
            BindingManager.executeBindings(this, "_BasicModule_SetEventHandler18", _BasicModule_SetEventHandler18);
            return (_local1);
        }
        private function _BasicModule_SetProperty37_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _BasicModule_SetProperty37 = _local1;
            _local1.name = "transitionContainer2";
            BindingManager.executeBindings(this, "_BasicModule_SetProperty37", _BasicModule_SetProperty37);
            return (_local1);
        }
        public function get template13():CueReactivityPrompt{
            return (this._1304009756template13);
        }
        public function get template15():CompletionGraph{
            return (this._1304009758template15);
        }
        private function _BasicModule_SetProperty14_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _BasicModule_SetProperty14 = _local1;
            _local1.name = "contentImage";
            BindingManager.executeBindings(this, "_BasicModule_SetProperty14", _BasicModule_SetProperty14);
            return (_local1);
        }
        public function set template10(_arg1:PersonalValues):void{
            var _local2:Object;
            _local2 = this._1304009753template10;
            if (_local2 !== _arg1){
                this._1304009753template10 = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "template10", _local2, _arg1));
            };
        }
        public function set template11(_arg1:PersonalValue):void{
            var _local2:Object;
            _local2 = this._1304009754template11;
            if (_local2 !== _arg1){
                this._1304009754template11 = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "template11", _local2, _arg1));
            };
        }
        public function set template12(_arg1:Registration):void{
            var _local2:Object;
            _local2 = this._1304009755template12;
            if (_local2 !== _arg1){
                this._1304009755template12 = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "template12", _local2, _arg1));
            };
        }
        public function set template13(_arg1:CueReactivityPrompt):void{
            var _local2:Object;
            _local2 = this._1304009756template13;
            if (_local2 !== _arg1){
                this._1304009756template13 = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "template13", _local2, _arg1));
            };
        }
        private function _BasicModule_AddChild4_c():AddChild{
            var _local1:AddChild = new AddChild();
            _local1.creationPolicy = "all";
            _local1.targetFactory = new DeferredInstanceFromFunction(_BasicModule_Quiz1_i);
            return (_local1);
        }
        public function set template14(_arg1:WeeklyConsumption):void{
            var _local2:Object;
            _local2 = this._1304009757template14;
            if (_local2 !== _arg1){
                this._1304009757template14 = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "template14", _local2, _arg1));
            };
        }
        public function set template15(_arg1:CompletionGraph):void{
            var _local2:Object;
            _local2 = this._1304009758template15;
            if (_local2 !== _arg1){
                this._1304009758template15 = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "template15", _local2, _arg1));
            };
        }
        private function _BasicModule_SetProperty48_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _BasicModule_SetProperty48 = _local1;
            _local1.name = "contentImage";
            BindingManager.executeBindings(this, "_BasicModule_SetProperty48", _BasicModule_SetProperty48);
            return (_local1);
        }
        private function _BasicModule_AddChild14_c():AddChild{
            var _local1:AddChild = new AddChild();
            _local1.creationPolicy = "all";
            _local1.targetFactory = new DeferredInstanceFromFunction(_BasicModule_Registration1_i);
            return (_local1);
        }
        private function _BasicModule_SetProperty25_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _BasicModule_SetProperty25 = _local1;
            _local1.name = "ctaBtn";
            BindingManager.executeBindings(this, "_BasicModule_SetProperty25", _BasicModule_SetProperty25);
            return (_local1);
        }
        private function _BasicModule_VBox1_i():VBox{
            var _local1:VBox = new VBox();
            _BasicModule_VBox1 = _local1;
            _local1.percentWidth = 100;
            _local1.percentHeight = 100;
            _local1.setStyle("verticalAlign", "middle");
            _local1.setStyle("horizontalAlign", "left");
            _local1.id = "_BasicModule_VBox1";
            BindingManager.executeBindings(this, "_BasicModule_VBox1", _BasicModule_VBox1);
            if (!_local1.document){
                _local1.document = this;
            };
            _local1.addChild(_BasicModule_Label1_c());
            _local1.addChild(_BasicModule_TextArea1_i());
            return (_local1);
        }
        public function get template1b():GlassPicture{
            return (this._1304009803template1b);
        }
        private function _BasicModule_CompletionGraph1_i():CompletionGraph{
            var _local1:CompletionGraph = new CompletionGraph();
            template15 = _local1;
            _local1.id = "template15";
            BindingManager.executeBindings(this, "template15", template15);
            if (!_local1.document){
                _local1.document = this;
            };
            return (_local1);
        }
        public function ___BasicModule_SetEventHandler7_handler(_arg1:Object):void{
            imageLoaded();
        }
        private function _BasicModule_SetEventHandler9_i():SetEventHandler{
            var _local1:SetEventHandler = new SetEventHandler();
            _BasicModule_SetEventHandler9 = _local1;
            _local1.name = "imageLoaded";
            _local1.addEventListener("handler", ___BasicModule_SetEventHandler9_handler);
            BindingManager.executeBindings(this, "_BasicModule_SetEventHandler9", _BasicModule_SetEventHandler9);
            return (_local1);
        }
        public function ___BasicModule_SetEventHandler3_handler(_arg1:Object):void{
            imageLoaded();
        }
        public function get template1a():Quiz{
            return (this._1304009802template1a);
        }
        public function ___BasicModule_SetEventHandler12_handler(_arg1:Object):void{
            outro();
        }
        private function _BasicModule_State14_c():State{
            var _local1:State = new State();
            _local1.name = "Registration";
            _local1.overrides = [_BasicModule_AddChild14_c(), _BasicModule_SetEventHandler16_i(), _BasicModule_SetProperty60_i(), _BasicModule_SetProperty61_i(), _BasicModule_SetProperty62_i(), _BasicModule_SetProperty63_i(), _BasicModule_SetProperty64_i()];
            return (_local1);
        }
        private function _BasicModule_SetProperty13_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _BasicModule_SetProperty13 = _local1;
            _local1.name = "transitionContainer2";
            BindingManager.executeBindings(this, "_BasicModule_SetProperty13", _BasicModule_SetProperty13);
            return (_local1);
        }
        private function _BasicModule_SetProperty59_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _BasicModule_SetProperty59 = _local1;
            _local1.name = "ctaBtn";
            BindingManager.executeBindings(this, "_BasicModule_SetProperty59", _BasicModule_SetProperty59);
            return (_local1);
        }
        public function ___BasicModule_SetEventHandler16_handler(_arg1:Object):void{
            outro();
        }
        private function _BasicModule_SetProperty36_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _BasicModule_SetProperty36 = _local1;
            _local1.name = "transitionContainer1";
            BindingManager.executeBindings(this, "_BasicModule_SetProperty36", _BasicModule_SetProperty36);
            return (_local1);
        }
        private function _BasicModule_Move1_i():Move{
            var _local1:Move = new Move();
            bounceDownEffect = _local1;
            _local1.duration = 1000;
            BindingManager.executeBindings(this, "bounceDownEffect", bounceDownEffect);
            return (_local1);
        }
        public function ___BasicModule_AbstractBalanceModule1_removed(_arg1:Event):void{
            cleanUpEvents();
        }
        protected function nextButtonPressed(_arg1:String):void{
            if (_arg1 == BACK){
                previous();
            } else {
                outro();
            };
        }
        private function _BasicModule_SetEventHandler17_i():SetEventHandler{
            var _local1:SetEventHandler = new SetEventHandler();
            _BasicModule_SetEventHandler17 = _local1;
            _local1.addEventListener("handler", ___BasicModule_SetEventHandler17_handler);
            BindingManager.executeBindings(this, "_BasicModule_SetEventHandler17", _BasicModule_SetEventHandler17);
            return (_local1);
        }
        private function _BasicModule_AddChild3_c():AddChild{
            var _local1:AddChild = new AddChild();
            _local1.creationPolicy = "all";
            _local1.targetFactory = new DeferredInstanceFromFunction(_BasicModule_GlassPicture1_i);
            return (_local1);
        }
        private function _BasicModule_SetProperty24_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _BasicModule_SetProperty24 = _local1;
            _local1.name = "contentImage";
            BindingManager.executeBindings(this, "_BasicModule_SetProperty24", _BasicModule_SetProperty24);
            return (_local1);
        }
        private function _BasicModule_AddChild13_c():AddChild{
            var _local1:AddChild = new AddChild();
            _local1.creationPolicy = "all";
            _local1.targetFactory = new DeferredInstanceFromFunction(_BasicModule_PersonalValue1_i);
            return (_local1);
        }
        private function _BasicModule_SetProperty47_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _BasicModule_SetProperty47 = _local1;
            _local1.name = "transitionContainer2";
            BindingManager.executeBindings(this, "_BasicModule_SetProperty47", _BasicModule_SetProperty47);
            return (_local1);
        }
        protected function buttonPressed(_arg1:Event):void{
            var _local3:CairngormEvent;
            var _local2:ButtonActionVO = _arg1.target.action;
            trace(("buttonAction=" + _local2));
            if (_local2 == null){
                nextButtonPressed(_arg1.target.direction);
                return;
            };
            trace(("buttonAction.Type=" + _local2.Type));
            switch (_local2.Type){
                case ButtonActionVO.JUMP_TO:
                    trace(response.NextType);
                    response.NextType = _local2.NextType;
                    collectionComplete(new Event(ButtonActionVO.JUMP_TO));
                    break;
                case ButtonActionVO.NEXT:
                    nextButtonPressed(_arg1.target.direction);
                    break;
                case ButtonActionVO.PRINT:
                    break;
                case ButtonActionVO.LINK:
                    break;
                case ButtonActionVO.EXIT:
                    _local3 = new CairngormEvent(BalanceController.EXIT_COMMAND);
                    _local3.dispatch();
                    break;
            };
        }
        private function _BasicModule_State13_c():State{
            var _local1:State = new State();
            _local1.name = "PersonalValue";
            _local1.overrides = [_BasicModule_AddChild13_c(), _BasicModule_SetEventHandler15_i(), _BasicModule_SetProperty55_i(), _BasicModule_SetProperty56_i(), _BasicModule_SetProperty57_i(), _BasicModule_SetProperty58_i(), _BasicModule_SetProperty59_i()];
            return (_local1);
        }
        public function set template1b(_arg1:GlassPicture):void{
            var _local2:Object;
            _local2 = this._1304009803template1b;
            if (_local2 !== _arg1){
                this._1304009803template1b = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "template1b", _local2, _arg1));
            };
        }
        private function _BasicModule_SetEventHandler8_i():SetEventHandler{
            var _local1:SetEventHandler = new SetEventHandler();
            _BasicModule_SetEventHandler8 = _local1;
            _local1.name = "ioError";
            _local1.addEventListener("handler", ___BasicModule_SetEventHandler8_handler);
            BindingManager.executeBindings(this, "_BasicModule_SetEventHandler8", _BasicModule_SetEventHandler8);
            return (_local1);
        }
        private function _BasicModule_SetEventHandler16_i():SetEventHandler{
            var _local1:SetEventHandler = new SetEventHandler();
            _BasicModule_SetEventHandler16 = _local1;
            _local1.addEventListener("handler", ___BasicModule_SetEventHandler16_handler);
            BindingManager.executeBindings(this, "_BasicModule_SetEventHandler16", _BasicModule_SetEventHandler16);
            return (_local1);
        }
        private function _BasicModule_SetProperty35_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _BasicModule_SetProperty35 = _local1;
            _local1.name = "currentTemplate";
            BindingManager.executeBindings(this, "_BasicModule_SetProperty35", _BasicModule_SetProperty35);
            return (_local1);
        }
        private function _BasicModule_SetProperty12_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _BasicModule_SetProperty12 = _local1;
            _local1.name = "transitionContainer1";
            BindingManager.executeBindings(this, "_BasicModule_SetProperty12", _BasicModule_SetProperty12);
            return (_local1);
        }
        private function _BasicModule_TrafficLightResults1_i():TrafficLightResults{
            var _local1:TrafficLightResults = new TrafficLightResults();
            template6 = _local1;
            _local1.id = "template6";
            BindingManager.executeBindings(this, "template6", template6);
            if (!_local1.document){
                _local1.document = this;
            };
            return (_local1);
        }
        protected function contentIni(_arg1:Event=null):void{
            CursorManager.removeAllCursors();
            if (!imageIsLoaded){
                imageIsLoaded = true;
                startIntro();
                BalanceModelLocator.getInstance().isOutroActive = false;
                lastPresenterImageURL = content.PresenterImageUrl;
                if (contentIndex == 0){
                    moduleReady();
                };
            };
        }
        private function _BasicModule_PersonalValue1_i():PersonalValue{
            var _local1:PersonalValue = new PersonalValue();
            template11 = _local1;
            _local1.id = "template11";
            BindingManager.executeBindings(this, "template11", template11);
            if (!_local1.document){
                _local1.document = this;
            };
            return (_local1);
        }
        protected function imageLoadedError():void{
            Alert.show(("ERROR LOADING IMAGE:" + content.PresenterImageUrl));
            imageLoaded();
        }
        public function get fadeOutContainer1():Fade{
            return (this._404469598fadeOutContainer1);
        }
        public function get fadeOutContainer2():Fade{
            return (this._404469597fadeOutContainer2);
        }
        private function _BasicModule_GlassPicture1_i():GlassPicture{
            var _local1:GlassPicture = new GlassPicture();
            template1b = _local1;
            _local1.id = "template1b";
            BindingManager.executeBindings(this, "template1b", template1b);
            if (!_local1.document){
                _local1.document = this;
            };
            return (_local1);
        }
        public function set template1a(_arg1:Quiz):void{
            var _local2:Object;
            _local2 = this._1304009802template1a;
            if (_local2 !== _arg1){
                this._1304009802template1a = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "template1a", _local2, _arg1));
            };
        }
        private function _BasicModule_AddChild2_c():AddChild{
            var _local1:AddChild = new AddChild();
            _local1.creationPolicy = "all";
            _local1.targetFactory = new DeferredInstanceFromFunction(_BasicModule_PicLeftInfoText1_i);
            return (_local1);
        }
        private function _BasicModule_SetProperty58_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _BasicModule_SetProperty58 = _local1;
            _local1.name = "contentImage";
            BindingManager.executeBindings(this, "_BasicModule_SetProperty58", _BasicModule_SetProperty58);
            return (_local1);
        }
        private function _BasicModule_SetProperty23_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _BasicModule_SetProperty23 = _local1;
            _local1.name = "transitionContainer2";
            BindingManager.executeBindings(this, "_BasicModule_SetProperty23", _BasicModule_SetProperty23);
            return (_local1);
        }
        private function _BasicModule_SetProperty9_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _BasicModule_SetProperty9 = _local1;
            _local1.name = "contentImage";
            BindingManager.executeBindings(this, "_BasicModule_SetProperty9", _BasicModule_SetProperty9);
            return (_local1);
        }
        private function _BasicModule_SetProperty69_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _BasicModule_SetProperty69 = _local1;
            _local1.name = "ctaBtn";
            BindingManager.executeBindings(this, "_BasicModule_SetProperty69", _BasicModule_SetProperty69);
            return (_local1);
        }
        private function _BasicModule_AddChild12_c():AddChild{
            var _local1:AddChild = new AddChild();
            _local1.creationPolicy = "all";
            _local1.targetFactory = new DeferredInstanceFromFunction(_BasicModule_PersonalValues1_i);
            return (_local1);
        }
        private function _BasicModule_WeeklyConsumption1_i():WeeklyConsumption{
            var _local1:WeeklyConsumption = new WeeklyConsumption();
            template14 = _local1;
            _local1.id = "template14";
            BindingManager.executeBindings(this, "template14", template14);
            if (!_local1.document){
                _local1.document = this;
            };
            return (_local1);
        }
        private function _BasicModule_SetProperty46_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _BasicModule_SetProperty46 = _local1;
            _local1.name = "transitionContainer1";
            BindingManager.executeBindings(this, "_BasicModule_SetProperty46", _BasicModule_SetProperty46);
            return (_local1);
        }
        private function _BasicModule_State12_c():State{
            var _local1:State = new State();
            _local1.name = "PersonalValues";
            _local1.overrides = [_BasicModule_AddChild12_c(), _BasicModule_SetEventHandler14_i(), _BasicModule_SetProperty50_i(), _BasicModule_SetProperty51_i(), _BasicModule_SetProperty52_i(), _BasicModule_SetProperty53_i(), _BasicModule_SetProperty54_i()];
            return (_local1);
        }
        private function _BasicModule_SetEventHandler7_i():SetEventHandler{
            var _local1:SetEventHandler = new SetEventHandler();
            _BasicModule_SetEventHandler7 = _local1;
            _local1.name = "imageLoaded";
            _local1.addEventListener("handler", ___BasicModule_SetEventHandler7_handler);
            BindingManager.executeBindings(this, "_BasicModule_SetEventHandler7", _BasicModule_SetEventHandler7);
            return (_local1);
        }
        private function _BasicModule_DailyConsumption1_i():DailyConsumption{
            var _local1:DailyConsumption = new DailyConsumption();
            template3 = _local1;
            _local1.id = "template3";
            BindingManager.executeBindings(this, "template3", template3);
            if (!_local1.document){
                _local1.document = this;
            };
            return (_local1);
        }
        private function _BasicModule_SetProperty11_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _BasicModule_SetProperty11 = _local1;
            _local1.name = "currentTemplate";
            BindingManager.executeBindings(this, "_BasicModule_SetProperty11", _BasicModule_SetProperty11);
            return (_local1);
        }
        private function _BasicModule_SetEventHandler15_i():SetEventHandler{
            var _local1:SetEventHandler = new SetEventHandler();
            _BasicModule_SetEventHandler15 = _local1;
            _local1.name = "click";
            _local1.addEventListener("handler", ___BasicModule_SetEventHandler15_handler);
            BindingManager.executeBindings(this, "_BasicModule_SetEventHandler15", _BasicModule_SetEventHandler15);
            return (_local1);
        }
        private function _BasicModule_SetProperty34_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _BasicModule_SetProperty34 = _local1;
            _local1.name = "ctaBtn";
            BindingManager.executeBindings(this, "_BasicModule_SetProperty34", _BasicModule_SetProperty34);
            return (_local1);
        }
        private function _BasicModule_SetProperty57_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _BasicModule_SetProperty57 = _local1;
            _local1.name = "transitionContainer2";
            BindingManager.executeBindings(this, "_BasicModule_SetProperty57", _BasicModule_SetProperty57);
            return (_local1);
        }
        private function _BasicModule_PicRightInfoText1_i():PicRightInfoText{
            var _local1:PicRightInfoText = new PicRightInfoText();
            template2 = _local1;
            _local1.id = "template2";
            BindingManager.executeBindings(this, "template2", template2);
            if (!_local1.document){
                _local1.document = this;
            };
            return (_local1);
        }
        private function _BasicModule_AddChild1_c():AddChild{
            var _local1:AddChild = new AddChild();
            _local1.creationPolicy = "all";
            _local1.targetFactory = new DeferredInstanceFromFunction(_BasicModule_VBox1_i);
            return (_local1);
        }
        private function cleanUpEvents():void{
        }
        private function _BasicModule_SetProperty8_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _BasicModule_SetProperty8 = _local1;
            _local1.name = "transitionContainer2";
            BindingManager.executeBindings(this, "_BasicModule_SetProperty8", _BasicModule_SetProperty8);
            return (_local1);
        }
        private function _BasicModule_SetProperty68_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _BasicModule_SetProperty68 = _local1;
            _local1.name = "contentImage";
            BindingManager.executeBindings(this, "_BasicModule_SetProperty68", _BasicModule_SetProperty68);
            return (_local1);
        }
        private function _BasicModule_AddChild11_c():AddChild{
            var _local1:AddChild = new AddChild();
            _local1.creationPolicy = "all";
            _local1.targetFactory = new DeferredInstanceFromFunction(_BasicModule_PersonalPlan1_i);
            return (_local1);
        }
        private function _BasicModule_SetProperty22_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _BasicModule_SetProperty22 = _local1;
            _local1.name = "transitionContainer1";
            BindingManager.executeBindings(this, "_BasicModule_SetProperty22", _BasicModule_SetProperty22);
            return (_local1);
        }
        private function _BasicModule_SetProperty45_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _BasicModule_SetProperty45 = _local1;
            _local1.name = "currentTemplate";
            BindingManager.executeBindings(this, "_BasicModule_SetProperty45", _BasicModule_SetProperty45);
            return (_local1);
        }
        public function get template7a():TypicalWeekCompletion{
            return (this._1304009988template7a);
        }
        private function _BasicModule_bindingExprs():void{
            var _local1:*;
            _local1 = (content.Template == null);
            _local1 = (content.Template == null);
            _local1 = ObjectUtil.toString(content);
            _local1 = this;
            _local1 = this.content;
            _local1 = template1.contentImage;
            _local1 = template1.contentImage;
            _local1 = this;
            _local1 = template1;
            _local1 = this;
            _local1 = template1.transContainer1;
            _local1 = this;
            _local1 = template1.transContainer2;
            _local1 = this;
            _local1 = template1.contentImage;
            _local1 = this;
            _local1 = template1.cta_btn;
            _local1 = this;
            _local1 = this.content;
            _local1 = template1b.contentImage;
            _local1 = template1b.contentImage;
            _local1 = this;
            _local1 = template1b;
            _local1 = this;
            _local1 = template1b.transContainer1;
            _local1 = this;
            _local1 = template1b.transContainer2;
            _local1 = this;
            _local1 = template1b.contentImage;
            _local1 = this;
            _local1 = template1b.cta_btn;
            _local1 = this;
            _local1 = this.content;
            _local1 = template1a.contentImage;
            _local1 = template1a.contentImage;
            _local1 = this;
            _local1 = template1a;
            _local1 = this;
            _local1 = template1a.transContainer1;
            _local1 = this;
            _local1 = template1a.transContainer2;
            _local1 = this;
            _local1 = template1a.contentImage;
            _local1 = this;
            _local1 = template1a.cta_btn;
            _local1 = this;
            _local1 = this.content;
            _local1 = template2.contentImage;
            _local1 = template2.contentImage;
            _local1 = this;
            _local1 = template2;
            _local1 = this;
            _local1 = template2.transContainer1;
            _local1 = this;
            _local1 = template2.transContainer2;
            _local1 = this;
            _local1 = template2.contentImage;
            _local1 = this;
            _local1 = template2.cta_btn;
            _local1 = this;
            _local1 = this.content;
            _local1 = this;
            _local1 = template3;
            _local1 = this;
            _local1 = template3.transContainer1;
            _local1 = this;
            _local1 = template3.transContainer2;
            _local1 = this;
            _local1 = null;
            _local1 = this;
            _local1 = template3.cta_btn;
            _local1 = this;
            _local1 = this.content;
            _local1 = template6.trafficlightImage;
            _local1 = this;
            _local1 = template6;
            _local1 = this;
            _local1 = template6.transContainer1;
            _local1 = this;
            _local1 = template6.transContainer2;
            _local1 = this;
            _local1 = null;
            _local1 = this;
            _local1 = this.content;
            _local1 = template7.cta_btn;
            _local1 = this;
            _local1 = template7;
            _local1 = this;
            _local1 = template7.transContainer1;
            _local1 = this;
            _local1 = template7.transContainer2;
            _local1 = this;
            _local1 = null;
            _local1 = this;
            _local1 = template7.cta_btn;
            _local1 = this;
            _local1 = this.content;
            _local1 = template7a.cta_btn;
            _local1 = this;
            _local1 = template7a;
            _local1 = this;
            _local1 = template7a.transContainer1;
            _local1 = this;
            _local1 = template7a.transContainer2;
            _local1 = this;
            _local1 = null;
            _local1 = this;
            _local1 = template7a.cta_btn;
            _local1 = this;
            _local1 = this.content;
            _local1 = template8.cta_btn;
            _local1 = this;
            _local1 = template8;
            _local1 = this;
            _local1 = template8.transContainer1;
            _local1 = this;
            _local1 = template8.transContainer2;
            _local1 = this;
            _local1 = null;
            _local1 = this;
            _local1 = template8.cta_btn;
            _local1 = this;
            _local1 = this.content;
            _local1 = template9.cta_btn;
            _local1 = this;
            _local1 = template9;
            _local1 = this;
            _local1 = template9.transContainer1;
            _local1 = this;
            _local1 = template9.transContainer2;
            _local1 = this;
            _local1 = null;
            _local1 = this;
            _local1 = template9.cta_btn;
            _local1 = this;
            _local1 = this.content;
            _local1 = template10.cta_btn;
            _local1 = this;
            _local1 = template10;
            _local1 = this;
            _local1 = template10.transContainer1;
            _local1 = this;
            _local1 = template10.transContainer2;
            _local1 = this;
            _local1 = null;
            _local1 = this;
            _local1 = template10.cta_btn;
            _local1 = this;
            _local1 = this.content;
            _local1 = template11.cta_btn;
            _local1 = this;
            _local1 = template11;
            _local1 = this;
            _local1 = template11.transContainer1;
            _local1 = this;
            _local1 = template11.transContainer2;
            _local1 = this;
            _local1 = null;
            _local1 = this;
            _local1 = template11.cta_btn;
            _local1 = this;
            _local1 = this.content;
            _local1 = template12;
            _local1 = Event.COMPLETE;
            _local1 = this;
            _local1 = template12;
            _local1 = this;
            _local1 = template12.transContainer1;
            _local1 = this;
            _local1 = template12.transContainer2;
            _local1 = this;
            _local1 = template12.contentImage;
            _local1 = this;
            _local1 = template12.cta_btn;
            _local1 = this;
            _local1 = this.content;
            _local1 = template13;
            _local1 = Event.COMPLETE;
            _local1 = this;
            _local1 = template13;
            _local1 = this;
            _local1 = null;
            _local1 = this;
            _local1 = null;
            _local1 = this;
            _local1 = null;
            _local1 = this;
            _local1 = null;
            _local1 = this;
            _local1 = this.content;
            _local1 = template14;
            _local1 = Event.COMPLETE;
            _local1 = this;
            _local1 = template14;
            _local1 = this;
            _local1 = null;
            _local1 = this;
            _local1 = null;
            _local1 = this;
            _local1 = null;
            _local1 = this;
            _local1 = template14.cta_btn;
            _local1 = this;
            _local1 = this.content;
            _local1 = template15;
            _local1 = Event.COMPLETE;
            _local1 = this;
            _local1 = template15;
            _local1 = this;
            _local1 = null;
            _local1 = this;
            _local1 = null;
            _local1 = this;
            _local1 = null;
            _local1 = this;
            _local1 = template15.cta_btn;
            _local1 = transitionContainer1;
            _local1 = Linear.easeInOut;
            _local1 = transitionContainer2;
            _local1 = Linear.easeInOut;
            _local1 = transitionContainer1;
            _local1 = Linear.easeInOut;
            _local1 = transitionContainer2;
            _local1 = Linear.easeInOut;
            _local1 = Bounce.easeOut;
        }
        private function init(_arg1:FlexEvent):void{
            BindingUtils.bindSetter(resetModuleReady, BalanceModelLocator.getInstance(), "room");
            addEventListener(BalanceButton.EVENT_CLICKED, buttonPressed);
            stage.addEventListener(KeyboardEvent.KEY_DOWN, keyDown, false, 0, true);
            stage.addEventListener(KeyboardEvent.KEY_UP, keyUp, false, 0, true);
            ChangeWatcher.watch(this, "currentState", initState);
        }
        private function _BasicModule_State11_c():State{
            var _local1:State = new State();
            _local1.name = "PersonalPlan";
            _local1.overrides = [_BasicModule_AddChild11_c(), _BasicModule_SetEventHandler13_i(), _BasicModule_SetProperty45_i(), _BasicModule_SetProperty46_i(), _BasicModule_SetProperty47_i(), _BasicModule_SetProperty48_i(), _BasicModule_SetProperty49_i()];
            return (_local1);
        }
        private function _BasicModule_SetEventHandler6_i():SetEventHandler{
            var _local1:SetEventHandler = new SetEventHandler();
            _BasicModule_SetEventHandler6 = _local1;
            _local1.name = "ioError";
            _local1.addEventListener("handler", ___BasicModule_SetEventHandler6_handler);
            BindingManager.executeBindings(this, "_BasicModule_SetEventHandler6", _BasicModule_SetEventHandler6);
            return (_local1);
        }
        private function _BasicModule_SetEventHandler14_i():SetEventHandler{
            var _local1:SetEventHandler = new SetEventHandler();
            _BasicModule_SetEventHandler14 = _local1;
            _local1.name = "click";
            _local1.addEventListener("handler", ___BasicModule_SetEventHandler14_handler);
            BindingManager.executeBindings(this, "_BasicModule_SetEventHandler14", _BasicModule_SetEventHandler14);
            return (_local1);
        }
        private function _BasicModule_SetProperty10_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _BasicModule_SetProperty10 = _local1;
            _local1.name = "ctaBtn";
            BindingManager.executeBindings(this, "_BasicModule_SetProperty10", _BasicModule_SetProperty10);
            return (_local1);
        }
        private function _BasicModule_SetProperty33_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _BasicModule_SetProperty33 = _local1;
            _local1.name = "contentImage";
            BindingManager.executeBindings(this, "_BasicModule_SetProperty33", _BasicModule_SetProperty33);
            return (_local1);
        }
        private function _BasicModule_Fade4_i():Fade{
            var _local1:Fade = new Fade();
            fadeOutContainer2 = _local1;
            _local1.alphaFrom = 1;
            _local1.alphaTo = 0;
            _local1.duration = 500;
            BindingManager.executeBindings(this, "fadeOutContainer2", fadeOutContainer2);
            return (_local1);
        }
        private function _BasicModule_SetProperty79_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _BasicModule_SetProperty79 = _local1;
            _local1.name = "ctaBtn";
            BindingManager.executeBindings(this, "_BasicModule_SetProperty79", _BasicModule_SetProperty79);
            return (_local1);
        }
        private function _BasicModule_SetProperty56_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _BasicModule_SetProperty56 = _local1;
            _local1.name = "transitionContainer1";
            BindingManager.executeBindings(this, "_BasicModule_SetProperty56", _BasicModule_SetProperty56);
            return (_local1);
        }
        private function _BasicModule_CueReactivityPrompt1_i():CueReactivityPrompt{
            var _local1:CueReactivityPrompt = new CueReactivityPrompt();
            template13 = _local1;
            _local1.id = "template13";
            BindingManager.executeBindings(this, "template13", template13);
            if (!_local1.document){
                _local1.document = this;
            };
            return (_local1);
        }
        private function _BasicModule_SetProperty7_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _BasicModule_SetProperty7 = _local1;
            _local1.name = "transitionContainer1";
            BindingManager.executeBindings(this, "_BasicModule_SetProperty7", _BasicModule_SetProperty7);
            return (_local1);
        }
        private function _BasicModule_State9_c():State{
            var _local1:State = new State();
            _local1.name = "TypicalWeekCompletion";
            _local1.overrides = [_BasicModule_AddChild9_c(), _BasicModule_SetEventHandler11_i(), _BasicModule_SetProperty35_i(), _BasicModule_SetProperty36_i(), _BasicModule_SetProperty37_i(), _BasicModule_SetProperty38_i(), _BasicModule_SetProperty39_i()];
            return (_local1);
        }
        private function _BasicModule_SetProperty21_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _BasicModule_SetProperty21 = _local1;
            _local1.name = "currentTemplate";
            BindingManager.executeBindings(this, "_BasicModule_SetProperty21", _BasicModule_SetProperty21);
            return (_local1);
        }
        private function _BasicModule_SetProperty44_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _BasicModule_SetProperty44 = _local1;
            _local1.name = "ctaBtn";
            BindingManager.executeBindings(this, "_BasicModule_SetProperty44", _BasicModule_SetProperty44);
            return (_local1);
        }
        private function _BasicModule_SetProperty67_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _BasicModule_SetProperty67 = _local1;
            _local1.name = "transitionContainer2";
            BindingManager.executeBindings(this, "_BasicModule_SetProperty67", _BasicModule_SetProperty67);
            return (_local1);
        }
        private function _BasicModule_AddChild10_c():AddChild{
            var _local1:AddChild = new AddChild();
            _local1.creationPolicy = "all";
            _local1.targetFactory = new DeferredInstanceFromFunction(_BasicModule_ProgressMonitor1_i);
            return (_local1);
        }
        public function set fadeOutContainer1(_arg1:Fade):void{
            var _local2:Object;
            _local2 = this._404469598fadeOutContainer1;
            if (_local2 !== _arg1){
                this._404469598fadeOutContainer1 = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "fadeOutContainer1", _local2, _arg1));
            };
        }
        public function set fadeOutContainer2(_arg1:Fade):void{
            var _local2:Object;
            _local2 = this._404469597fadeOutContainer2;
            if (_local2 !== _arg1){
                this._404469597fadeOutContainer2 = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "fadeOutContainer2", _local2, _arg1));
            };
        }
        protected function imageLoaded(_arg1:Event=null):void{
            t = new Timer(100, 1);
            t.addEventListener(TimerEvent.TIMER_COMPLETE, contentIni);
            t.start();
        }
        public function ___BasicModule_SetEventHandler2_handler(_arg1:Object):void{
            imageLoadedError();
        }
        private function _BasicModule_SetEventHandler5_i():SetEventHandler{
            var _local1:SetEventHandler = new SetEventHandler();
            _BasicModule_SetEventHandler5 = _local1;
            _local1.name = "imageLoaded";
            _local1.addEventListener("handler", ___BasicModule_SetEventHandler5_handler);
            BindingManager.executeBindings(this, "_BasicModule_SetEventHandler5", _BasicModule_SetEventHandler5);
            return (_local1);
        }
        public function ___BasicModule_SetEventHandler6_handler(_arg1:Object):void{
            imageLoadedError();
        }
        private function _BasicModule_State10_c():State{
            var _local1:State = new State();
            _local1.name = "ProgressMonitor";
            _local1.overrides = [_BasicModule_AddChild10_c(), _BasicModule_SetEventHandler12_i(), _BasicModule_SetProperty40_i(), _BasicModule_SetProperty41_i(), _BasicModule_SetProperty42_i(), _BasicModule_SetProperty43_i(), _BasicModule_SetProperty44_i()];
            return (_local1);
        }
        public function ___BasicModule_SetEventHandler11_handler(_arg1:Object):void{
            outro();
        }
        private function keyUp(_arg1:KeyboardEvent):void{
            keyIsDown = false;
        }
        private function _BasicModule_SetProperty32_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _BasicModule_SetProperty32 = _local1;
            _local1.name = "transitionContainer2";
            BindingManager.executeBindings(this, "_BasicModule_SetProperty32", _BasicModule_SetProperty32);
            return (_local1);
        }
        private function _BasicModule_Fade3_i():Fade{
            var _local1:Fade = new Fade();
            fadeOutContainer1 = _local1;
            _local1.alphaFrom = 1;
            _local1.alphaTo = 0;
            _local1.duration = 500;
            BindingManager.executeBindings(this, "fadeOutContainer1", fadeOutContainer1);
            return (_local1);
        }
        private function initState(_arg1:Event):void{
            CursorManager.setBusyCursor();
            if (transitionContainer1 != null){
                transitionContainer1.alpha = 0;
            };
            if (transitionContainer2 != null){
                transitionContainer2.alpha = 0;
            };
            decideDelayCallImageLoaded();
        }
        public function ___BasicModule_SetEventHandler15_handler(_arg1:Object):void{
            outro();
        }
        private function _BasicModule_SetProperty78_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _BasicModule_SetProperty78 = _local1;
            _local1.name = "contentImage";
            BindingManager.executeBindings(this, "_BasicModule_SetProperty78", _BasicModule_SetProperty78);
            return (_local1);
        }
        public function ___BasicModule_SetEventHandler19_handler(_arg1:Object):void{
            outro();
        }
        private function _BasicModule_SetEventHandler13_i():SetEventHandler{
            var _local1:SetEventHandler = new SetEventHandler();
            _BasicModule_SetEventHandler13 = _local1;
            _local1.name = "click";
            _local1.addEventListener("handler", ___BasicModule_SetEventHandler13_handler);
            BindingManager.executeBindings(this, "_BasicModule_SetEventHandler13", _BasicModule_SetEventHandler13);
            return (_local1);
        }
        private function _BasicModule_SetProperty55_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _BasicModule_SetProperty55 = _local1;
            _local1.name = "currentTemplate";
            BindingManager.executeBindings(this, "_BasicModule_SetProperty55", _BasicModule_SetProperty55);
            return (_local1);
        }
        public function ___BasicModule_AbstractBalanceModule1_contentChange(_arg1:Event):void{
            change();
        }
        private function _BasicModule_SetProperty20_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _BasicModule_SetProperty20 = _local1;
            _local1.name = "ctaBtn";
            BindingManager.executeBindings(this, "_BasicModule_SetProperty20", _BasicModule_SetProperty20);
            return (_local1);
        }
        private function _BasicModule_SetProperty6_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _BasicModule_SetProperty6 = _local1;
            _local1.name = "currentTemplate";
            BindingManager.executeBindings(this, "_BasicModule_SetProperty6", _BasicModule_SetProperty6);
            return (_local1);
        }
        private function _BasicModule_SetProperty66_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _BasicModule_SetProperty66 = _local1;
            _local1.name = "transitionContainer1";
            BindingManager.executeBindings(this, "_BasicModule_SetProperty66", _BasicModule_SetProperty66);
            return (_local1);
        }
        private function _BasicModule_State8_c():State{
            var _local1:State = new State();
            _local1.name = "TypicalWeek";
            _local1.overrides = [_BasicModule_AddChild8_c(), _BasicModule_SetEventHandler10_i(), _BasicModule_SetProperty30_i(), _BasicModule_SetProperty31_i(), _BasicModule_SetProperty32_i(), _BasicModule_SetProperty33_i(), _BasicModule_SetProperty34_i()];
            return (_local1);
        }
        public function get bounceDownEffect():Move{
            return (this._1810141563bounceDownEffect);
        }
        private function _BasicModule_SetProperty43_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _BasicModule_SetProperty43 = _local1;
            _local1.name = "contentImage";
            BindingManager.executeBindings(this, "_BasicModule_SetProperty43", _BasicModule_SetProperty43);
            return (_local1);
        }
        private function _BasicModule_SetEventHandler4_i():SetEventHandler{
            var _local1:SetEventHandler = new SetEventHandler();
            _BasicModule_SetEventHandler4 = _local1;
            _local1.name = "ioError";
            _local1.addEventListener("handler", ___BasicModule_SetEventHandler4_handler);
            BindingManager.executeBindings(this, "_BasicModule_SetEventHandler4", _BasicModule_SetEventHandler4);
            return (_local1);
        }
        private function _BasicModule_SetEventHandler12_i():SetEventHandler{
            var _local1:SetEventHandler = new SetEventHandler();
            _BasicModule_SetEventHandler12 = _local1;
            _local1.name = "click";
            _local1.addEventListener("handler", ___BasicModule_SetEventHandler12_handler);
            BindingManager.executeBindings(this, "_BasicModule_SetEventHandler12", _BasicModule_SetEventHandler12);
            return (_local1);
        }
        private function _BasicModule_SetProperty31_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _BasicModule_SetProperty31 = _local1;
            _local1.name = "transitionContainer1";
            BindingManager.executeBindings(this, "_BasicModule_SetProperty31", _BasicModule_SetProperty31);
            return (_local1);
        }
        private function _BasicModule_SetProperty77_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _BasicModule_SetProperty77 = _local1;
            _local1.name = "transitionContainer2";
            BindingManager.executeBindings(this, "_BasicModule_SetProperty77", _BasicModule_SetProperty77);
            return (_local1);
        }
        private function _BasicModule_Fade2_i():Fade{
            var _local1:Fade = new Fade();
            fadeInContainer2 = _local1;
            _local1.alphaFrom = 0;
            _local1.alphaTo = 1;
            _local1.duration = 500;
            _local1.startDelay = 500;
            BindingManager.executeBindings(this, "fadeInContainer2", fadeInContainer2);
            return (_local1);
        }
        private function _BasicModule_SetProperty54_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _BasicModule_SetProperty54 = _local1;
            _local1.name = "ctaBtn";
            BindingManager.executeBindings(this, "_BasicModule_SetProperty54", _BasicModule_SetProperty54);
            return (_local1);
        }
        private function _BasicModule_SetProperty5_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _BasicModule_SetProperty5 = _local1;
            _local1.name = "ctaBtn";
            BindingManager.executeBindings(this, "_BasicModule_SetProperty5", _BasicModule_SetProperty5);
            return (_local1);
        }
        private function _BasicModule_State7_c():State{
            var _local1:State = new State();
            _local1.name = "TrafficLightResults";
            _local1.overrides = [_BasicModule_AddChild7_c(), _BasicModule_SetEventHandler9_i(), _BasicModule_SetProperty26_i(), _BasicModule_SetProperty27_i(), _BasicModule_SetProperty28_i(), _BasicModule_SetProperty29_i()];
            return (_local1);
        }
        private function _BasicModule_SetProperty42_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _BasicModule_SetProperty42 = _local1;
            _local1.name = "transitionContainer2";
            BindingManager.executeBindings(this, "_BasicModule_SetProperty42", _BasicModule_SetProperty42);
            return (_local1);
        }
        private function _BasicModule_SetProperty65_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _BasicModule_SetProperty65 = _local1;
            _local1.name = "currentTemplate";
            BindingManager.executeBindings(this, "_BasicModule_SetProperty65", _BasicModule_SetProperty65);
            return (_local1);
        }
        private function _BasicModule_bindingsSetup():Array{
            var binding:* = null;
            var result:* = [];
            binding = new Binding(this, function ():Boolean{
                return ((content.Template == null));
            }, function (_arg1:Boolean):void{
                _BasicModule_VBox1.visible = _arg1;
            }, "_BasicModule_VBox1.visible");
            result[0] = binding;
            binding = new Binding(this, function ():Boolean{
                return ((content.Template == null));
            }, function (_arg1:Boolean):void{
                _BasicModule_VBox1.includeInLayout = _arg1;
            }, "_BasicModule_VBox1.includeInLayout");
            result[1] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = ObjectUtil.toString(content);
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                _BasicModule_TextArea1.text = _arg1;
            }, "_BasicModule_TextArea1.text");
            result[2] = binding;
            binding = new Binding(this, function ():IBalanceModule{
                return (this);
            }, function (_arg1:IBalanceModule):void{
                template1.module = _arg1;
            }, "template1.module");
            result[3] = binding;
            binding = new Binding(this, function ():Content{
                return (this.content);
            }, function (_arg1:Content):void{
                template1.content = _arg1;
            }, "template1.content");
            result[4] = binding;
            binding = new Binding(this, function ():EventDispatcher{
                return (template1.contentImage);
            }, function (_arg1:EventDispatcher):void{
                _BasicModule_SetEventHandler1.target = _arg1;
            }, "_BasicModule_SetEventHandler1.target");
            result[5] = binding;
            binding = new Binding(this, function ():EventDispatcher{
                return (template1.contentImage);
            }, function (_arg1:EventDispatcher):void{
                _BasicModule_SetEventHandler2.target = _arg1;
            }, "_BasicModule_SetEventHandler2.target");
            result[6] = binding;
            binding = new Binding(this, function ():Object{
                return (this);
            }, function (_arg1:Object):void{
                _BasicModule_SetProperty1.target = _arg1;
            }, "_BasicModule_SetProperty1.target");
            result[7] = binding;
            binding = new Binding(this, function (){
                return (template1);
            }, function (_arg1):void{
                _BasicModule_SetProperty1.value = _arg1;
            }, "_BasicModule_SetProperty1.value");
            result[8] = binding;
            binding = new Binding(this, function ():Object{
                return (this);
            }, function (_arg1:Object):void{
                _BasicModule_SetProperty2.target = _arg1;
            }, "_BasicModule_SetProperty2.target");
            result[9] = binding;
            binding = new Binding(this, function (){
                return (template1.transContainer1);
            }, function (_arg1):void{
                _BasicModule_SetProperty2.value = _arg1;
            }, "_BasicModule_SetProperty2.value");
            result[10] = binding;
            binding = new Binding(this, function ():Object{
                return (this);
            }, function (_arg1:Object):void{
                _BasicModule_SetProperty3.target = _arg1;
            }, "_BasicModule_SetProperty3.target");
            result[11] = binding;
            binding = new Binding(this, function (){
                return (template1.transContainer2);
            }, function (_arg1):void{
                _BasicModule_SetProperty3.value = _arg1;
            }, "_BasicModule_SetProperty3.value");
            result[12] = binding;
            binding = new Binding(this, function ():Object{
                return (this);
            }, function (_arg1:Object):void{
                _BasicModule_SetProperty4.target = _arg1;
            }, "_BasicModule_SetProperty4.target");
            result[13] = binding;
            binding = new Binding(this, function (){
                return (template1.contentImage);
            }, function (_arg1):void{
                _BasicModule_SetProperty4.value = _arg1;
            }, "_BasicModule_SetProperty4.value");
            result[14] = binding;
            binding = new Binding(this, function ():Object{
                return (this);
            }, function (_arg1:Object):void{
                _BasicModule_SetProperty5.target = _arg1;
            }, "_BasicModule_SetProperty5.target");
            result[15] = binding;
            binding = new Binding(this, function (){
                return (template1.cta_btn);
            }, function (_arg1):void{
                _BasicModule_SetProperty5.value = _arg1;
            }, "_BasicModule_SetProperty5.value");
            result[16] = binding;
            binding = new Binding(this, function ():IBalanceModule{
                return (this);
            }, function (_arg1:IBalanceModule):void{
                template1b.module = _arg1;
            }, "template1b.module");
            result[17] = binding;
            binding = new Binding(this, function ():Content{
                return (this.content);
            }, function (_arg1:Content):void{
                template1b.content = _arg1;
            }, "template1b.content");
            result[18] = binding;
            binding = new Binding(this, function ():EventDispatcher{
                return (template1b.contentImage);
            }, function (_arg1:EventDispatcher):void{
                _BasicModule_SetEventHandler3.target = _arg1;
            }, "_BasicModule_SetEventHandler3.target");
            result[19] = binding;
            binding = new Binding(this, function ():EventDispatcher{
                return (template1b.contentImage);
            }, function (_arg1:EventDispatcher):void{
                _BasicModule_SetEventHandler4.target = _arg1;
            }, "_BasicModule_SetEventHandler4.target");
            result[20] = binding;
            binding = new Binding(this, function ():Object{
                return (this);
            }, function (_arg1:Object):void{
                _BasicModule_SetProperty6.target = _arg1;
            }, "_BasicModule_SetProperty6.target");
            result[21] = binding;
            binding = new Binding(this, function (){
                return (template1b);
            }, function (_arg1):void{
                _BasicModule_SetProperty6.value = _arg1;
            }, "_BasicModule_SetProperty6.value");
            result[22] = binding;
            binding = new Binding(this, function ():Object{
                return (this);
            }, function (_arg1:Object):void{
                _BasicModule_SetProperty7.target = _arg1;
            }, "_BasicModule_SetProperty7.target");
            result[23] = binding;
            binding = new Binding(this, function (){
                return (template1b.transContainer1);
            }, function (_arg1):void{
                _BasicModule_SetProperty7.value = _arg1;
            }, "_BasicModule_SetProperty7.value");
            result[24] = binding;
            binding = new Binding(this, function ():Object{
                return (this);
            }, function (_arg1:Object):void{
                _BasicModule_SetProperty8.target = _arg1;
            }, "_BasicModule_SetProperty8.target");
            result[25] = binding;
            binding = new Binding(this, function (){
                return (template1b.transContainer2);
            }, function (_arg1):void{
                _BasicModule_SetProperty8.value = _arg1;
            }, "_BasicModule_SetProperty8.value");
            result[26] = binding;
            binding = new Binding(this, function ():Object{
                return (this);
            }, function (_arg1:Object):void{
                _BasicModule_SetProperty9.target = _arg1;
            }, "_BasicModule_SetProperty9.target");
            result[27] = binding;
            binding = new Binding(this, function (){
                return (template1b.contentImage);
            }, function (_arg1):void{
                _BasicModule_SetProperty9.value = _arg1;
            }, "_BasicModule_SetProperty9.value");
            result[28] = binding;
            binding = new Binding(this, function ():Object{
                return (this);
            }, function (_arg1:Object):void{
                _BasicModule_SetProperty10.target = _arg1;
            }, "_BasicModule_SetProperty10.target");
            result[29] = binding;
            binding = new Binding(this, function (){
                return (template1b.cta_btn);
            }, function (_arg1):void{
                _BasicModule_SetProperty10.value = _arg1;
            }, "_BasicModule_SetProperty10.value");
            result[30] = binding;
            binding = new Binding(this, function ():IBalanceModule{
                return (this);
            }, function (_arg1:IBalanceModule):void{
                template1a.module = _arg1;
            }, "template1a.module");
            result[31] = binding;
            binding = new Binding(this, function ():Content{
                return (this.content);
            }, function (_arg1:Content):void{
                template1a.content = _arg1;
            }, "template1a.content");
            result[32] = binding;
            binding = new Binding(this, function ():EventDispatcher{
                return (template1a.contentImage);
            }, function (_arg1:EventDispatcher):void{
                _BasicModule_SetEventHandler5.target = _arg1;
            }, "_BasicModule_SetEventHandler5.target");
            result[33] = binding;
            binding = new Binding(this, function ():EventDispatcher{
                return (template1a.contentImage);
            }, function (_arg1:EventDispatcher):void{
                _BasicModule_SetEventHandler6.target = _arg1;
            }, "_BasicModule_SetEventHandler6.target");
            result[34] = binding;
            binding = new Binding(this, function ():Object{
                return (this);
            }, function (_arg1:Object):void{
                _BasicModule_SetProperty11.target = _arg1;
            }, "_BasicModule_SetProperty11.target");
            result[35] = binding;
            binding = new Binding(this, function (){
                return (template1a);
            }, function (_arg1):void{
                _BasicModule_SetProperty11.value = _arg1;
            }, "_BasicModule_SetProperty11.value");
            result[36] = binding;
            binding = new Binding(this, function ():Object{
                return (this);
            }, function (_arg1:Object):void{
                _BasicModule_SetProperty12.target = _arg1;
            }, "_BasicModule_SetProperty12.target");
            result[37] = binding;
            binding = new Binding(this, function (){
                return (template1a.transContainer1);
            }, function (_arg1):void{
                _BasicModule_SetProperty12.value = _arg1;
            }, "_BasicModule_SetProperty12.value");
            result[38] = binding;
            binding = new Binding(this, function ():Object{
                return (this);
            }, function (_arg1:Object):void{
                _BasicModule_SetProperty13.target = _arg1;
            }, "_BasicModule_SetProperty13.target");
            result[39] = binding;
            binding = new Binding(this, function (){
                return (template1a.transContainer2);
            }, function (_arg1):void{
                _BasicModule_SetProperty13.value = _arg1;
            }, "_BasicModule_SetProperty13.value");
            result[40] = binding;
            binding = new Binding(this, function ():Object{
                return (this);
            }, function (_arg1:Object):void{
                _BasicModule_SetProperty14.target = _arg1;
            }, "_BasicModule_SetProperty14.target");
            result[41] = binding;
            binding = new Binding(this, function (){
                return (template1a.contentImage);
            }, function (_arg1):void{
                _BasicModule_SetProperty14.value = _arg1;
            }, "_BasicModule_SetProperty14.value");
            result[42] = binding;
            binding = new Binding(this, function ():Object{
                return (this);
            }, function (_arg1:Object):void{
                _BasicModule_SetProperty15.target = _arg1;
            }, "_BasicModule_SetProperty15.target");
            result[43] = binding;
            binding = new Binding(this, function (){
                return (template1a.cta_btn);
            }, function (_arg1):void{
                _BasicModule_SetProperty15.value = _arg1;
            }, "_BasicModule_SetProperty15.value");
            result[44] = binding;
            binding = new Binding(this, function ():IBalanceModule{
                return (this);
            }, function (_arg1:IBalanceModule):void{
                template2.module = _arg1;
            }, "template2.module");
            result[45] = binding;
            binding = new Binding(this, function ():Content{
                return (this.content);
            }, function (_arg1:Content):void{
                template2.content = _arg1;
            }, "template2.content");
            result[46] = binding;
            binding = new Binding(this, function ():EventDispatcher{
                return (template2.contentImage);
            }, function (_arg1:EventDispatcher):void{
                _BasicModule_SetEventHandler7.target = _arg1;
            }, "_BasicModule_SetEventHandler7.target");
            result[47] = binding;
            binding = new Binding(this, function ():EventDispatcher{
                return (template2.contentImage);
            }, function (_arg1:EventDispatcher):void{
                _BasicModule_SetEventHandler8.target = _arg1;
            }, "_BasicModule_SetEventHandler8.target");
            result[48] = binding;
            binding = new Binding(this, function ():Object{
                return (this);
            }, function (_arg1:Object):void{
                _BasicModule_SetProperty16.target = _arg1;
            }, "_BasicModule_SetProperty16.target");
            result[49] = binding;
            binding = new Binding(this, function (){
                return (template2);
            }, function (_arg1):void{
                _BasicModule_SetProperty16.value = _arg1;
            }, "_BasicModule_SetProperty16.value");
            result[50] = binding;
            binding = new Binding(this, function ():Object{
                return (this);
            }, function (_arg1:Object):void{
                _BasicModule_SetProperty17.target = _arg1;
            }, "_BasicModule_SetProperty17.target");
            result[51] = binding;
            binding = new Binding(this, function (){
                return (template2.transContainer1);
            }, function (_arg1):void{
                _BasicModule_SetProperty17.value = _arg1;
            }, "_BasicModule_SetProperty17.value");
            result[52] = binding;
            binding = new Binding(this, function ():Object{
                return (this);
            }, function (_arg1:Object):void{
                _BasicModule_SetProperty18.target = _arg1;
            }, "_BasicModule_SetProperty18.target");
            result[53] = binding;
            binding = new Binding(this, function (){
                return (template2.transContainer2);
            }, function (_arg1):void{
                _BasicModule_SetProperty18.value = _arg1;
            }, "_BasicModule_SetProperty18.value");
            result[54] = binding;
            binding = new Binding(this, function ():Object{
                return (this);
            }, function (_arg1:Object):void{
                _BasicModule_SetProperty19.target = _arg1;
            }, "_BasicModule_SetProperty19.target");
            result[55] = binding;
            binding = new Binding(this, function (){
                return (template2.contentImage);
            }, function (_arg1):void{
                _BasicModule_SetProperty19.value = _arg1;
            }, "_BasicModule_SetProperty19.value");
            result[56] = binding;
            binding = new Binding(this, function ():Object{
                return (this);
            }, function (_arg1:Object):void{
                _BasicModule_SetProperty20.target = _arg1;
            }, "_BasicModule_SetProperty20.target");
            result[57] = binding;
            binding = new Binding(this, function (){
                return (template2.cta_btn);
            }, function (_arg1):void{
                _BasicModule_SetProperty20.value = _arg1;
            }, "_BasicModule_SetProperty20.value");
            result[58] = binding;
            binding = new Binding(this, function ():IBalanceModule{
                return (this);
            }, function (_arg1:IBalanceModule):void{
                template3.module = _arg1;
            }, "template3.module");
            result[59] = binding;
            binding = new Binding(this, function ():Content{
                return (this.content);
            }, function (_arg1:Content):void{
                template3.content = _arg1;
            }, "template3.content");
            result[60] = binding;
            binding = new Binding(this, function ():Object{
                return (this);
            }, function (_arg1:Object):void{
                _BasicModule_SetProperty21.target = _arg1;
            }, "_BasicModule_SetProperty21.target");
            result[61] = binding;
            binding = new Binding(this, function (){
                return (template3);
            }, function (_arg1):void{
                _BasicModule_SetProperty21.value = _arg1;
            }, "_BasicModule_SetProperty21.value");
            result[62] = binding;
            binding = new Binding(this, function ():Object{
                return (this);
            }, function (_arg1:Object):void{
                _BasicModule_SetProperty22.target = _arg1;
            }, "_BasicModule_SetProperty22.target");
            result[63] = binding;
            binding = new Binding(this, function (){
                return (template3.transContainer1);
            }, function (_arg1):void{
                _BasicModule_SetProperty22.value = _arg1;
            }, "_BasicModule_SetProperty22.value");
            result[64] = binding;
            binding = new Binding(this, function ():Object{
                return (this);
            }, function (_arg1:Object):void{
                _BasicModule_SetProperty23.target = _arg1;
            }, "_BasicModule_SetProperty23.target");
            result[65] = binding;
            binding = new Binding(this, function (){
                return (template3.transContainer2);
            }, function (_arg1):void{
                _BasicModule_SetProperty23.value = _arg1;
            }, "_BasicModule_SetProperty23.value");
            result[66] = binding;
            binding = new Binding(this, function ():Object{
                return (this);
            }, function (_arg1:Object):void{
                _BasicModule_SetProperty24.target = _arg1;
            }, "_BasicModule_SetProperty24.target");
            result[67] = binding;
            binding = new Binding(this, function (){
                return (null);
            }, function (_arg1):void{
                _BasicModule_SetProperty24.value = _arg1;
            }, "_BasicModule_SetProperty24.value");
            result[68] = binding;
            binding = new Binding(this, function ():Object{
                return (this);
            }, function (_arg1:Object):void{
                _BasicModule_SetProperty25.target = _arg1;
            }, "_BasicModule_SetProperty25.target");
            result[69] = binding;
            binding = new Binding(this, function (){
                return (template3.cta_btn);
            }, function (_arg1):void{
                _BasicModule_SetProperty25.value = _arg1;
            }, "_BasicModule_SetProperty25.value");
            result[70] = binding;
            binding = new Binding(this, function ():IBalanceModule{
                return (this);
            }, function (_arg1:IBalanceModule):void{
                template6.module = _arg1;
            }, "template6.module");
            result[71] = binding;
            binding = new Binding(this, function ():Content{
                return (this.content);
            }, function (_arg1:Content):void{
                template6.content = _arg1;
            }, "template6.content");
            result[72] = binding;
            binding = new Binding(this, function ():EventDispatcher{
                return (template6.trafficlightImage);
            }, function (_arg1:EventDispatcher):void{
                _BasicModule_SetEventHandler9.target = _arg1;
            }, "_BasicModule_SetEventHandler9.target");
            result[73] = binding;
            binding = new Binding(this, function ():Object{
                return (this);
            }, function (_arg1:Object):void{
                _BasicModule_SetProperty26.target = _arg1;
            }, "_BasicModule_SetProperty26.target");
            result[74] = binding;
            binding = new Binding(this, function (){
                return (template6);
            }, function (_arg1):void{
                _BasicModule_SetProperty26.value = _arg1;
            }, "_BasicModule_SetProperty26.value");
            result[75] = binding;
            binding = new Binding(this, function ():Object{
                return (this);
            }, function (_arg1:Object):void{
                _BasicModule_SetProperty27.target = _arg1;
            }, "_BasicModule_SetProperty27.target");
            result[76] = binding;
            binding = new Binding(this, function (){
                return (template6.transContainer1);
            }, function (_arg1):void{
                _BasicModule_SetProperty27.value = _arg1;
            }, "_BasicModule_SetProperty27.value");
            result[77] = binding;
            binding = new Binding(this, function ():Object{
                return (this);
            }, function (_arg1:Object):void{
                _BasicModule_SetProperty28.target = _arg1;
            }, "_BasicModule_SetProperty28.target");
            result[78] = binding;
            binding = new Binding(this, function (){
                return (template6.transContainer2);
            }, function (_arg1):void{
                _BasicModule_SetProperty28.value = _arg1;
            }, "_BasicModule_SetProperty28.value");
            result[79] = binding;
            binding = new Binding(this, function ():Object{
                return (this);
            }, function (_arg1:Object):void{
                _BasicModule_SetProperty29.target = _arg1;
            }, "_BasicModule_SetProperty29.target");
            result[80] = binding;
            binding = new Binding(this, function (){
                return (null);
            }, function (_arg1):void{
                _BasicModule_SetProperty29.value = _arg1;
            }, "_BasicModule_SetProperty29.value");
            result[81] = binding;
            binding = new Binding(this, function ():IBalanceModule{
                return (this);
            }, function (_arg1:IBalanceModule):void{
                template7.module = _arg1;
            }, "template7.module");
            result[82] = binding;
            binding = new Binding(this, function ():Content{
                return (this.content);
            }, function (_arg1:Content):void{
                template7.content = _arg1;
            }, "template7.content");
            result[83] = binding;
            binding = new Binding(this, function ():EventDispatcher{
                return (template7.cta_btn);
            }, function (_arg1:EventDispatcher):void{
                _BasicModule_SetEventHandler10.target = _arg1;
            }, "_BasicModule_SetEventHandler10.target");
            result[84] = binding;
            binding = new Binding(this, function ():Object{
                return (this);
            }, function (_arg1:Object):void{
                _BasicModule_SetProperty30.target = _arg1;
            }, "_BasicModule_SetProperty30.target");
            result[85] = binding;
            binding = new Binding(this, function (){
                return (template7);
            }, function (_arg1):void{
                _BasicModule_SetProperty30.value = _arg1;
            }, "_BasicModule_SetProperty30.value");
            result[86] = binding;
            binding = new Binding(this, function ():Object{
                return (this);
            }, function (_arg1:Object):void{
                _BasicModule_SetProperty31.target = _arg1;
            }, "_BasicModule_SetProperty31.target");
            result[87] = binding;
            binding = new Binding(this, function (){
                return (template7.transContainer1);
            }, function (_arg1):void{
                _BasicModule_SetProperty31.value = _arg1;
            }, "_BasicModule_SetProperty31.value");
            result[88] = binding;
            binding = new Binding(this, function ():Object{
                return (this);
            }, function (_arg1:Object):void{
                _BasicModule_SetProperty32.target = _arg1;
            }, "_BasicModule_SetProperty32.target");
            result[89] = binding;
            binding = new Binding(this, function (){
                return (template7.transContainer2);
            }, function (_arg1):void{
                _BasicModule_SetProperty32.value = _arg1;
            }, "_BasicModule_SetProperty32.value");
            result[90] = binding;
            binding = new Binding(this, function ():Object{
                return (this);
            }, function (_arg1:Object):void{
                _BasicModule_SetProperty33.target = _arg1;
            }, "_BasicModule_SetProperty33.target");
            result[91] = binding;
            binding = new Binding(this, function (){
                return (null);
            }, function (_arg1):void{
                _BasicModule_SetProperty33.value = _arg1;
            }, "_BasicModule_SetProperty33.value");
            result[92] = binding;
            binding = new Binding(this, function ():Object{
                return (this);
            }, function (_arg1:Object):void{
                _BasicModule_SetProperty34.target = _arg1;
            }, "_BasicModule_SetProperty34.target");
            result[93] = binding;
            binding = new Binding(this, function (){
                return (template7.cta_btn);
            }, function (_arg1):void{
                _BasicModule_SetProperty34.value = _arg1;
            }, "_BasicModule_SetProperty34.value");
            result[94] = binding;
            binding = new Binding(this, function ():IBalanceModule{
                return (this);
            }, function (_arg1:IBalanceModule):void{
                template7a.module = _arg1;
            }, "template7a.module");
            result[95] = binding;
            binding = new Binding(this, function ():Content{
                return (this.content);
            }, function (_arg1:Content):void{
                template7a.content = _arg1;
            }, "template7a.content");
            result[96] = binding;
            binding = new Binding(this, function ():EventDispatcher{
                return (template7a.cta_btn);
            }, function (_arg1:EventDispatcher):void{
                _BasicModule_SetEventHandler11.target = _arg1;
            }, "_BasicModule_SetEventHandler11.target");
            result[97] = binding;
            binding = new Binding(this, function ():Object{
                return (this);
            }, function (_arg1:Object):void{
                _BasicModule_SetProperty35.target = _arg1;
            }, "_BasicModule_SetProperty35.target");
            result[98] = binding;
            binding = new Binding(this, function (){
                return (template7a);
            }, function (_arg1):void{
                _BasicModule_SetProperty35.value = _arg1;
            }, "_BasicModule_SetProperty35.value");
            result[99] = binding;
            binding = new Binding(this, function ():Object{
                return (this);
            }, function (_arg1:Object):void{
                _BasicModule_SetProperty36.target = _arg1;
            }, "_BasicModule_SetProperty36.target");
            result[100] = binding;
            binding = new Binding(this, function (){
                return (template7a.transContainer1);
            }, function (_arg1):void{
                _BasicModule_SetProperty36.value = _arg1;
            }, "_BasicModule_SetProperty36.value");
            result[101] = binding;
            binding = new Binding(this, function ():Object{
                return (this);
            }, function (_arg1:Object):void{
                _BasicModule_SetProperty37.target = _arg1;
            }, "_BasicModule_SetProperty37.target");
            result[102] = binding;
            binding = new Binding(this, function (){
                return (template7a.transContainer2);
            }, function (_arg1):void{
                _BasicModule_SetProperty37.value = _arg1;
            }, "_BasicModule_SetProperty37.value");
            result[103] = binding;
            binding = new Binding(this, function ():Object{
                return (this);
            }, function (_arg1:Object):void{
                _BasicModule_SetProperty38.target = _arg1;
            }, "_BasicModule_SetProperty38.target");
            result[104] = binding;
            binding = new Binding(this, function (){
                return (null);
            }, function (_arg1):void{
                _BasicModule_SetProperty38.value = _arg1;
            }, "_BasicModule_SetProperty38.value");
            result[105] = binding;
            binding = new Binding(this, function ():Object{
                return (this);
            }, function (_arg1:Object):void{
                _BasicModule_SetProperty39.target = _arg1;
            }, "_BasicModule_SetProperty39.target");
            result[106] = binding;
            binding = new Binding(this, function (){
                return (template7a.cta_btn);
            }, function (_arg1):void{
                _BasicModule_SetProperty39.value = _arg1;
            }, "_BasicModule_SetProperty39.value");
            result[107] = binding;
            binding = new Binding(this, function ():IBalanceModule{
                return (this);
            }, function (_arg1:IBalanceModule):void{
                template8.module = _arg1;
            }, "template8.module");
            result[108] = binding;
            binding = new Binding(this, function ():Content{
                return (this.content);
            }, function (_arg1:Content):void{
                template8.content = _arg1;
            }, "template8.content");
            result[109] = binding;
            binding = new Binding(this, function ():EventDispatcher{
                return (template8.cta_btn);
            }, function (_arg1:EventDispatcher):void{
                _BasicModule_SetEventHandler12.target = _arg1;
            }, "_BasicModule_SetEventHandler12.target");
            result[110] = binding;
            binding = new Binding(this, function ():Object{
                return (this);
            }, function (_arg1:Object):void{
                _BasicModule_SetProperty40.target = _arg1;
            }, "_BasicModule_SetProperty40.target");
            result[111] = binding;
            binding = new Binding(this, function (){
                return (template8);
            }, function (_arg1):void{
                _BasicModule_SetProperty40.value = _arg1;
            }, "_BasicModule_SetProperty40.value");
            result[112] = binding;
            binding = new Binding(this, function ():Object{
                return (this);
            }, function (_arg1:Object):void{
                _BasicModule_SetProperty41.target = _arg1;
            }, "_BasicModule_SetProperty41.target");
            result[113] = binding;
            binding = new Binding(this, function (){
                return (template8.transContainer1);
            }, function (_arg1):void{
                _BasicModule_SetProperty41.value = _arg1;
            }, "_BasicModule_SetProperty41.value");
            result[114] = binding;
            binding = new Binding(this, function ():Object{
                return (this);
            }, function (_arg1:Object):void{
                _BasicModule_SetProperty42.target = _arg1;
            }, "_BasicModule_SetProperty42.target");
            result[115] = binding;
            binding = new Binding(this, function (){
                return (template8.transContainer2);
            }, function (_arg1):void{
                _BasicModule_SetProperty42.value = _arg1;
            }, "_BasicModule_SetProperty42.value");
            result[116] = binding;
            binding = new Binding(this, function ():Object{
                return (this);
            }, function (_arg1:Object):void{
                _BasicModule_SetProperty43.target = _arg1;
            }, "_BasicModule_SetProperty43.target");
            result[117] = binding;
            binding = new Binding(this, function (){
                return (null);
            }, function (_arg1):void{
                _BasicModule_SetProperty43.value = _arg1;
            }, "_BasicModule_SetProperty43.value");
            result[118] = binding;
            binding = new Binding(this, function ():Object{
                return (this);
            }, function (_arg1:Object):void{
                _BasicModule_SetProperty44.target = _arg1;
            }, "_BasicModule_SetProperty44.target");
            result[119] = binding;
            binding = new Binding(this, function (){
                return (template8.cta_btn);
            }, function (_arg1):void{
                _BasicModule_SetProperty44.value = _arg1;
            }, "_BasicModule_SetProperty44.value");
            result[120] = binding;
            binding = new Binding(this, function ():IBalanceModule{
                return (this);
            }, function (_arg1:IBalanceModule):void{
                template9.module = _arg1;
            }, "template9.module");
            result[121] = binding;
            binding = new Binding(this, function ():Content{
                return (this.content);
            }, function (_arg1:Content):void{
                template9.content = _arg1;
            }, "template9.content");
            result[122] = binding;
            binding = new Binding(this, function ():EventDispatcher{
                return (template9.cta_btn);
            }, function (_arg1:EventDispatcher):void{
                _BasicModule_SetEventHandler13.target = _arg1;
            }, "_BasicModule_SetEventHandler13.target");
            result[123] = binding;
            binding = new Binding(this, function ():Object{
                return (this);
            }, function (_arg1:Object):void{
                _BasicModule_SetProperty45.target = _arg1;
            }, "_BasicModule_SetProperty45.target");
            result[124] = binding;
            binding = new Binding(this, function (){
                return (template9);
            }, function (_arg1):void{
                _BasicModule_SetProperty45.value = _arg1;
            }, "_BasicModule_SetProperty45.value");
            result[125] = binding;
            binding = new Binding(this, function ():Object{
                return (this);
            }, function (_arg1:Object):void{
                _BasicModule_SetProperty46.target = _arg1;
            }, "_BasicModule_SetProperty46.target");
            result[126] = binding;
            binding = new Binding(this, function (){
                return (template9.transContainer1);
            }, function (_arg1):void{
                _BasicModule_SetProperty46.value = _arg1;
            }, "_BasicModule_SetProperty46.value");
            result[127] = binding;
            binding = new Binding(this, function ():Object{
                return (this);
            }, function (_arg1:Object):void{
                _BasicModule_SetProperty47.target = _arg1;
            }, "_BasicModule_SetProperty47.target");
            result[128] = binding;
            binding = new Binding(this, function (){
                return (template9.transContainer2);
            }, function (_arg1):void{
                _BasicModule_SetProperty47.value = _arg1;
            }, "_BasicModule_SetProperty47.value");
            result[129] = binding;
            binding = new Binding(this, function ():Object{
                return (this);
            }, function (_arg1:Object):void{
                _BasicModule_SetProperty48.target = _arg1;
            }, "_BasicModule_SetProperty48.target");
            result[130] = binding;
            binding = new Binding(this, function (){
                return (null);
            }, function (_arg1):void{
                _BasicModule_SetProperty48.value = _arg1;
            }, "_BasicModule_SetProperty48.value");
            result[131] = binding;
            binding = new Binding(this, function ():Object{
                return (this);
            }, function (_arg1:Object):void{
                _BasicModule_SetProperty49.target = _arg1;
            }, "_BasicModule_SetProperty49.target");
            result[132] = binding;
            binding = new Binding(this, function (){
                return (template9.cta_btn);
            }, function (_arg1):void{
                _BasicModule_SetProperty49.value = _arg1;
            }, "_BasicModule_SetProperty49.value");
            result[133] = binding;
            binding = new Binding(this, function ():IBalanceModule{
                return (this);
            }, function (_arg1:IBalanceModule):void{
                template10.module = _arg1;
            }, "template10.module");
            result[134] = binding;
            binding = new Binding(this, function ():Content{
                return (this.content);
            }, function (_arg1:Content):void{
                template10.content = _arg1;
            }, "template10.content");
            result[135] = binding;
            binding = new Binding(this, function ():EventDispatcher{
                return (template10.cta_btn);
            }, function (_arg1:EventDispatcher):void{
                _BasicModule_SetEventHandler14.target = _arg1;
            }, "_BasicModule_SetEventHandler14.target");
            result[136] = binding;
            binding = new Binding(this, function ():Object{
                return (this);
            }, function (_arg1:Object):void{
                _BasicModule_SetProperty50.target = _arg1;
            }, "_BasicModule_SetProperty50.target");
            result[137] = binding;
            binding = new Binding(this, function (){
                return (template10);
            }, function (_arg1):void{
                _BasicModule_SetProperty50.value = _arg1;
            }, "_BasicModule_SetProperty50.value");
            result[138] = binding;
            binding = new Binding(this, function ():Object{
                return (this);
            }, function (_arg1:Object):void{
                _BasicModule_SetProperty51.target = _arg1;
            }, "_BasicModule_SetProperty51.target");
            result[139] = binding;
            binding = new Binding(this, function (){
                return (template10.transContainer1);
            }, function (_arg1):void{
                _BasicModule_SetProperty51.value = _arg1;
            }, "_BasicModule_SetProperty51.value");
            result[140] = binding;
            binding = new Binding(this, function ():Object{
                return (this);
            }, function (_arg1:Object):void{
                _BasicModule_SetProperty52.target = _arg1;
            }, "_BasicModule_SetProperty52.target");
            result[141] = binding;
            binding = new Binding(this, function (){
                return (template10.transContainer2);
            }, function (_arg1):void{
                _BasicModule_SetProperty52.value = _arg1;
            }, "_BasicModule_SetProperty52.value");
            result[142] = binding;
            binding = new Binding(this, function ():Object{
                return (this);
            }, function (_arg1:Object):void{
                _BasicModule_SetProperty53.target = _arg1;
            }, "_BasicModule_SetProperty53.target");
            result[143] = binding;
            binding = new Binding(this, function (){
                return (null);
            }, function (_arg1):void{
                _BasicModule_SetProperty53.value = _arg1;
            }, "_BasicModule_SetProperty53.value");
            result[144] = binding;
            binding = new Binding(this, function ():Object{
                return (this);
            }, function (_arg1:Object):void{
                _BasicModule_SetProperty54.target = _arg1;
            }, "_BasicModule_SetProperty54.target");
            result[145] = binding;
            binding = new Binding(this, function (){
                return (template10.cta_btn);
            }, function (_arg1):void{
                _BasicModule_SetProperty54.value = _arg1;
            }, "_BasicModule_SetProperty54.value");
            result[146] = binding;
            binding = new Binding(this, function ():IBalanceModule{
                return (this);
            }, function (_arg1:IBalanceModule):void{
                template11.module = _arg1;
            }, "template11.module");
            result[147] = binding;
            binding = new Binding(this, function ():Content{
                return (this.content);
            }, function (_arg1:Content):void{
                template11.content = _arg1;
            }, "template11.content");
            result[148] = binding;
            binding = new Binding(this, function ():EventDispatcher{
                return (template11.cta_btn);
            }, function (_arg1:EventDispatcher):void{
                _BasicModule_SetEventHandler15.target = _arg1;
            }, "_BasicModule_SetEventHandler15.target");
            result[149] = binding;
            binding = new Binding(this, function ():Object{
                return (this);
            }, function (_arg1:Object):void{
                _BasicModule_SetProperty55.target = _arg1;
            }, "_BasicModule_SetProperty55.target");
            result[150] = binding;
            binding = new Binding(this, function (){
                return (template11);
            }, function (_arg1):void{
                _BasicModule_SetProperty55.value = _arg1;
            }, "_BasicModule_SetProperty55.value");
            result[151] = binding;
            binding = new Binding(this, function ():Object{
                return (this);
            }, function (_arg1:Object):void{
                _BasicModule_SetProperty56.target = _arg1;
            }, "_BasicModule_SetProperty56.target");
            result[152] = binding;
            binding = new Binding(this, function (){
                return (template11.transContainer1);
            }, function (_arg1):void{
                _BasicModule_SetProperty56.value = _arg1;
            }, "_BasicModule_SetProperty56.value");
            result[153] = binding;
            binding = new Binding(this, function ():Object{
                return (this);
            }, function (_arg1:Object):void{
                _BasicModule_SetProperty57.target = _arg1;
            }, "_BasicModule_SetProperty57.target");
            result[154] = binding;
            binding = new Binding(this, function (){
                return (template11.transContainer2);
            }, function (_arg1):void{
                _BasicModule_SetProperty57.value = _arg1;
            }, "_BasicModule_SetProperty57.value");
            result[155] = binding;
            binding = new Binding(this, function ():Object{
                return (this);
            }, function (_arg1:Object):void{
                _BasicModule_SetProperty58.target = _arg1;
            }, "_BasicModule_SetProperty58.target");
            result[156] = binding;
            binding = new Binding(this, function (){
                return (null);
            }, function (_arg1):void{
                _BasicModule_SetProperty58.value = _arg1;
            }, "_BasicModule_SetProperty58.value");
            result[157] = binding;
            binding = new Binding(this, function ():Object{
                return (this);
            }, function (_arg1:Object):void{
                _BasicModule_SetProperty59.target = _arg1;
            }, "_BasicModule_SetProperty59.target");
            result[158] = binding;
            binding = new Binding(this, function (){
                return (template11.cta_btn);
            }, function (_arg1):void{
                _BasicModule_SetProperty59.value = _arg1;
            }, "_BasicModule_SetProperty59.value");
            result[159] = binding;
            binding = new Binding(this, function ():IBalanceModule{
                return (this);
            }, function (_arg1:IBalanceModule):void{
                template12.module = _arg1;
            }, "template12.module");
            result[160] = binding;
            binding = new Binding(this, function ():Content{
                return (this.content);
            }, function (_arg1:Content):void{
                template12.content = _arg1;
            }, "template12.content");
            result[161] = binding;
            binding = new Binding(this, function ():EventDispatcher{
                return (template12);
            }, function (_arg1:EventDispatcher):void{
                _BasicModule_SetEventHandler16.target = _arg1;
            }, "_BasicModule_SetEventHandler16.target");
            result[162] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = Event.COMPLETE;
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                _BasicModule_SetEventHandler16.name = _arg1;
            }, "_BasicModule_SetEventHandler16.name");
            result[163] = binding;
            binding = new Binding(this, function ():Object{
                return (this);
            }, function (_arg1:Object):void{
                _BasicModule_SetProperty60.target = _arg1;
            }, "_BasicModule_SetProperty60.target");
            result[164] = binding;
            binding = new Binding(this, function (){
                return (template12);
            }, function (_arg1):void{
                _BasicModule_SetProperty60.value = _arg1;
            }, "_BasicModule_SetProperty60.value");
            result[165] = binding;
            binding = new Binding(this, function ():Object{
                return (this);
            }, function (_arg1:Object):void{
                _BasicModule_SetProperty61.target = _arg1;
            }, "_BasicModule_SetProperty61.target");
            result[166] = binding;
            binding = new Binding(this, function (){
                return (template12.transContainer1);
            }, function (_arg1):void{
                _BasicModule_SetProperty61.value = _arg1;
            }, "_BasicModule_SetProperty61.value");
            result[167] = binding;
            binding = new Binding(this, function ():Object{
                return (this);
            }, function (_arg1:Object):void{
                _BasicModule_SetProperty62.target = _arg1;
            }, "_BasicModule_SetProperty62.target");
            result[168] = binding;
            binding = new Binding(this, function (){
                return (template12.transContainer2);
            }, function (_arg1):void{
                _BasicModule_SetProperty62.value = _arg1;
            }, "_BasicModule_SetProperty62.value");
            result[169] = binding;
            binding = new Binding(this, function ():Object{
                return (this);
            }, function (_arg1:Object):void{
                _BasicModule_SetProperty63.target = _arg1;
            }, "_BasicModule_SetProperty63.target");
            result[170] = binding;
            binding = new Binding(this, function (){
                return (template12.contentImage);
            }, function (_arg1):void{
                _BasicModule_SetProperty63.value = _arg1;
            }, "_BasicModule_SetProperty63.value");
            result[171] = binding;
            binding = new Binding(this, function ():Object{
                return (this);
            }, function (_arg1:Object):void{
                _BasicModule_SetProperty64.target = _arg1;
            }, "_BasicModule_SetProperty64.target");
            result[172] = binding;
            binding = new Binding(this, function (){
                return (template12.cta_btn);
            }, function (_arg1):void{
                _BasicModule_SetProperty64.value = _arg1;
            }, "_BasicModule_SetProperty64.value");
            result[173] = binding;
            binding = new Binding(this, function ():IBalanceModule{
                return (this);
            }, function (_arg1:IBalanceModule):void{
                template13.module = _arg1;
            }, "template13.module");
            result[174] = binding;
            binding = new Binding(this, function ():Content{
                return (this.content);
            }, function (_arg1:Content):void{
                template13.content = _arg1;
            }, "template13.content");
            result[175] = binding;
            binding = new Binding(this, function ():EventDispatcher{
                return (template13);
            }, function (_arg1:EventDispatcher):void{
                _BasicModule_SetEventHandler17.target = _arg1;
            }, "_BasicModule_SetEventHandler17.target");
            result[176] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = Event.COMPLETE;
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                _BasicModule_SetEventHandler17.name = _arg1;
            }, "_BasicModule_SetEventHandler17.name");
            result[177] = binding;
            binding = new Binding(this, function ():Object{
                return (this);
            }, function (_arg1:Object):void{
                _BasicModule_SetProperty65.target = _arg1;
            }, "_BasicModule_SetProperty65.target");
            result[178] = binding;
            binding = new Binding(this, function (){
                return (template13);
            }, function (_arg1):void{
                _BasicModule_SetProperty65.value = _arg1;
            }, "_BasicModule_SetProperty65.value");
            result[179] = binding;
            binding = new Binding(this, function ():Object{
                return (this);
            }, function (_arg1:Object):void{
                _BasicModule_SetProperty66.target = _arg1;
            }, "_BasicModule_SetProperty66.target");
            result[180] = binding;
            binding = new Binding(this, function (){
                return (null);
            }, function (_arg1):void{
                _BasicModule_SetProperty66.value = _arg1;
            }, "_BasicModule_SetProperty66.value");
            result[181] = binding;
            binding = new Binding(this, function ():Object{
                return (this);
            }, function (_arg1:Object):void{
                _BasicModule_SetProperty67.target = _arg1;
            }, "_BasicModule_SetProperty67.target");
            result[182] = binding;
            binding = new Binding(this, function (){
                return (null);
            }, function (_arg1):void{
                _BasicModule_SetProperty67.value = _arg1;
            }, "_BasicModule_SetProperty67.value");
            result[183] = binding;
            binding = new Binding(this, function ():Object{
                return (this);
            }, function (_arg1:Object):void{
                _BasicModule_SetProperty68.target = _arg1;
            }, "_BasicModule_SetProperty68.target");
            result[184] = binding;
            binding = new Binding(this, function (){
                return (null);
            }, function (_arg1):void{
                _BasicModule_SetProperty68.value = _arg1;
            }, "_BasicModule_SetProperty68.value");
            result[185] = binding;
            binding = new Binding(this, function ():Object{
                return (this);
            }, function (_arg1:Object):void{
                _BasicModule_SetProperty69.target = _arg1;
            }, "_BasicModule_SetProperty69.target");
            result[186] = binding;
            binding = new Binding(this, function (){
                return (null);
            }, function (_arg1):void{
                _BasicModule_SetProperty69.value = _arg1;
            }, "_BasicModule_SetProperty69.value");
            result[187] = binding;
            binding = new Binding(this, function ():IBalanceModule{
                return (this);
            }, function (_arg1:IBalanceModule):void{
                template14.module = _arg1;
            }, "template14.module");
            result[188] = binding;
            binding = new Binding(this, function ():Content{
                return (this.content);
            }, function (_arg1:Content):void{
                template14.content = _arg1;
            }, "template14.content");
            result[189] = binding;
            binding = new Binding(this, function ():EventDispatcher{
                return (template14);
            }, function (_arg1:EventDispatcher):void{
                _BasicModule_SetEventHandler18.target = _arg1;
            }, "_BasicModule_SetEventHandler18.target");
            result[190] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = Event.COMPLETE;
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                _BasicModule_SetEventHandler18.name = _arg1;
            }, "_BasicModule_SetEventHandler18.name");
            result[191] = binding;
            binding = new Binding(this, function ():Object{
                return (this);
            }, function (_arg1:Object):void{
                _BasicModule_SetProperty70.target = _arg1;
            }, "_BasicModule_SetProperty70.target");
            result[192] = binding;
            binding = new Binding(this, function (){
                return (template14);
            }, function (_arg1):void{
                _BasicModule_SetProperty70.value = _arg1;
            }, "_BasicModule_SetProperty70.value");
            result[193] = binding;
            binding = new Binding(this, function ():Object{
                return (this);
            }, function (_arg1:Object):void{
                _BasicModule_SetProperty71.target = _arg1;
            }, "_BasicModule_SetProperty71.target");
            result[194] = binding;
            binding = new Binding(this, function (){
                return (null);
            }, function (_arg1):void{
                _BasicModule_SetProperty71.value = _arg1;
            }, "_BasicModule_SetProperty71.value");
            result[195] = binding;
            binding = new Binding(this, function ():Object{
                return (this);
            }, function (_arg1:Object):void{
                _BasicModule_SetProperty72.target = _arg1;
            }, "_BasicModule_SetProperty72.target");
            result[196] = binding;
            binding = new Binding(this, function (){
                return (null);
            }, function (_arg1):void{
                _BasicModule_SetProperty72.value = _arg1;
            }, "_BasicModule_SetProperty72.value");
            result[197] = binding;
            binding = new Binding(this, function ():Object{
                return (this);
            }, function (_arg1:Object):void{
                _BasicModule_SetProperty73.target = _arg1;
            }, "_BasicModule_SetProperty73.target");
            result[198] = binding;
            binding = new Binding(this, function (){
                return (null);
            }, function (_arg1):void{
                _BasicModule_SetProperty73.value = _arg1;
            }, "_BasicModule_SetProperty73.value");
            result[199] = binding;
            binding = new Binding(this, function ():Object{
                return (this);
            }, function (_arg1:Object):void{
                _BasicModule_SetProperty74.target = _arg1;
            }, "_BasicModule_SetProperty74.target");
            result[200] = binding;
            binding = new Binding(this, function (){
                return (template14.cta_btn);
            }, function (_arg1):void{
                _BasicModule_SetProperty74.value = _arg1;
            }, "_BasicModule_SetProperty74.value");
            result[201] = binding;
            binding = new Binding(this, function ():IBalanceModule{
                return (this);
            }, function (_arg1:IBalanceModule):void{
                template15.module = _arg1;
            }, "template15.module");
            result[202] = binding;
            binding = new Binding(this, function ():Content{
                return (this.content);
            }, function (_arg1:Content):void{
                template15.content = _arg1;
            }, "template15.content");
            result[203] = binding;
            binding = new Binding(this, function ():EventDispatcher{
                return (template15);
            }, function (_arg1:EventDispatcher):void{
                _BasicModule_SetEventHandler19.target = _arg1;
            }, "_BasicModule_SetEventHandler19.target");
            result[204] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = Event.COMPLETE;
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                _BasicModule_SetEventHandler19.name = _arg1;
            }, "_BasicModule_SetEventHandler19.name");
            result[205] = binding;
            binding = new Binding(this, function ():Object{
                return (this);
            }, function (_arg1:Object):void{
                _BasicModule_SetProperty75.target = _arg1;
            }, "_BasicModule_SetProperty75.target");
            result[206] = binding;
            binding = new Binding(this, function (){
                return (template15);
            }, function (_arg1):void{
                _BasicModule_SetProperty75.value = _arg1;
            }, "_BasicModule_SetProperty75.value");
            result[207] = binding;
            binding = new Binding(this, function ():Object{
                return (this);
            }, function (_arg1:Object):void{
                _BasicModule_SetProperty76.target = _arg1;
            }, "_BasicModule_SetProperty76.target");
            result[208] = binding;
            binding = new Binding(this, function (){
                return (null);
            }, function (_arg1):void{
                _BasicModule_SetProperty76.value = _arg1;
            }, "_BasicModule_SetProperty76.value");
            result[209] = binding;
            binding = new Binding(this, function ():Object{
                return (this);
            }, function (_arg1:Object):void{
                _BasicModule_SetProperty77.target = _arg1;
            }, "_BasicModule_SetProperty77.target");
            result[210] = binding;
            binding = new Binding(this, function (){
                return (null);
            }, function (_arg1):void{
                _BasicModule_SetProperty77.value = _arg1;
            }, "_BasicModule_SetProperty77.value");
            result[211] = binding;
            binding = new Binding(this, function ():Object{
                return (this);
            }, function (_arg1:Object):void{
                _BasicModule_SetProperty78.target = _arg1;
            }, "_BasicModule_SetProperty78.target");
            result[212] = binding;
            binding = new Binding(this, function (){
                return (null);
            }, function (_arg1):void{
                _BasicModule_SetProperty78.value = _arg1;
            }, "_BasicModule_SetProperty78.value");
            result[213] = binding;
            binding = new Binding(this, function ():Object{
                return (this);
            }, function (_arg1:Object):void{
                _BasicModule_SetProperty79.target = _arg1;
            }, "_BasicModule_SetProperty79.target");
            result[214] = binding;
            binding = new Binding(this, function (){
                return (template15.cta_btn);
            }, function (_arg1):void{
                _BasicModule_SetProperty79.value = _arg1;
            }, "_BasicModule_SetProperty79.value");
            result[215] = binding;
            binding = new Binding(this, function ():Object{
                return (transitionContainer1);
            }, function (_arg1:Object):void{
                fadeInContainer1.target = _arg1;
            }, "fadeInContainer1.target");
            result[216] = binding;
            binding = new Binding(this, function ():Function{
                return (Linear.easeInOut);
            }, function (_arg1:Function):void{
                fadeInContainer1.easingFunction = _arg1;
            }, "fadeInContainer1.easingFunction");
            result[217] = binding;
            binding = new Binding(this, function ():Object{
                return (transitionContainer2);
            }, function (_arg1:Object):void{
                fadeInContainer2.target = _arg1;
            }, "fadeInContainer2.target");
            result[218] = binding;
            binding = new Binding(this, function ():Function{
                return (Linear.easeInOut);
            }, function (_arg1:Function):void{
                fadeInContainer2.easingFunction = _arg1;
            }, "fadeInContainer2.easingFunction");
            result[219] = binding;
            binding = new Binding(this, function ():Object{
                return (transitionContainer1);
            }, function (_arg1:Object):void{
                fadeOutContainer1.target = _arg1;
            }, "fadeOutContainer1.target");
            result[220] = binding;
            binding = new Binding(this, function ():Function{
                return (Linear.easeInOut);
            }, function (_arg1:Function):void{
                fadeOutContainer1.easingFunction = _arg1;
            }, "fadeOutContainer1.easingFunction");
            result[221] = binding;
            binding = new Binding(this, function ():Object{
                return (transitionContainer2);
            }, function (_arg1:Object):void{
                fadeOutContainer2.target = _arg1;
            }, "fadeOutContainer2.target");
            result[222] = binding;
            binding = new Binding(this, function ():Function{
                return (Linear.easeInOut);
            }, function (_arg1:Function):void{
                fadeOutContainer2.easingFunction = _arg1;
            }, "fadeOutContainer2.easingFunction");
            result[223] = binding;
            binding = new Binding(this, function ():Function{
                return (Bounce.easeOut);
            }, function (_arg1:Function):void{
                bounceDownEffect.easingFunction = _arg1;
            }, "bounceDownEffect.easingFunction");
            result[224] = binding;
            return (result);
        }
        private function _BasicModule_SetEventHandler3_i():SetEventHandler{
            var _local1:SetEventHandler = new SetEventHandler();
            _BasicModule_SetEventHandler3 = _local1;
            _local1.name = "imageLoaded";
            _local1.addEventListener("handler", ___BasicModule_SetEventHandler3_handler);
            BindingManager.executeBindings(this, "_BasicModule_SetEventHandler3", _BasicModule_SetEventHandler3);
            return (_local1);
        }
        private function _BasicModule_SetEventHandler11_i():SetEventHandler{
            var _local1:SetEventHandler = new SetEventHandler();
            _BasicModule_SetEventHandler11 = _local1;
            _local1.name = "click";
            _local1.addEventListener("handler", ___BasicModule_SetEventHandler11_handler);
            BindingManager.executeBindings(this, "_BasicModule_SetEventHandler11", _BasicModule_SetEventHandler11);
            return (_local1);
        }
        private function _BasicModule_Fade1_i():Fade{
            var _local1:Fade = new Fade();
            fadeInContainer1 = _local1;
            _local1.alphaFrom = 0;
            _local1.alphaTo = 1;
            _local1.duration = 500;
            _local1.startDelay = 1000;
            BindingManager.executeBindings(this, "fadeInContainer1", fadeInContainer1);
            return (_local1);
        }
        private function _BasicModule_SetProperty76_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _BasicModule_SetProperty76 = _local1;
            _local1.name = "transitionContainer1";
            BindingManager.executeBindings(this, "_BasicModule_SetProperty76", _BasicModule_SetProperty76);
            return (_local1);
        }
        private function _BasicModule_SetProperty30_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _BasicModule_SetProperty30 = _local1;
            _local1.name = "currentTemplate";
            BindingManager.executeBindings(this, "_BasicModule_SetProperty30", _BasicModule_SetProperty30);
            return (_local1);
        }
        private function _BasicModule_SetProperty53_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _BasicModule_SetProperty53 = _local1;
            _local1.name = "contentImage";
            BindingManager.executeBindings(this, "_BasicModule_SetProperty53", _BasicModule_SetProperty53);
            return (_local1);
        }
        private function _BasicModule_Label1_c():Label{
            var _local1:Label = new Label();
            _local1.text = "NO TEMPLATE SPECIFIED";
            _local1.setStyle("fontWeight", "bold");
            if (!_local1.document){
                _local1.document = this;
            };
            return (_local1);
        }
        private function _BasicModule_SetProperty4_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _BasicModule_SetProperty4 = _local1;
            _local1.name = "contentImage";
            BindingManager.executeBindings(this, "_BasicModule_SetProperty4", _BasicModule_SetProperty4);
            return (_local1);
        }
        private function _BasicModule_State6_c():State{
            var _local1:State = new State();
            _local1.name = "DailyConsumption";
            _local1.overrides = [_BasicModule_AddChild6_c(), _BasicModule_SetProperty21_i(), _BasicModule_SetProperty22_i(), _BasicModule_SetProperty23_i(), _BasicModule_SetProperty24_i(), _BasicModule_SetProperty25_i()];
            return (_local1);
        }
        public function set template1(_arg1:PicLeftInfoText):void{
            var _local2:Object;
            _local2 = this._1981727479template1;
            if (_local2 !== _arg1){
                this._1981727479template1 = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "template1", _local2, _arg1));
            };
        }
        private function _BasicModule_SetProperty41_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _BasicModule_SetProperty41 = _local1;
            _local1.name = "transitionContainer1";
            BindingManager.executeBindings(this, "_BasicModule_SetProperty41", _BasicModule_SetProperty41);
            return (_local1);
        }
        private function _BasicModule_SetProperty64_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _BasicModule_SetProperty64 = _local1;
            _local1.name = "ctaBtn";
            BindingManager.executeBindings(this, "_BasicModule_SetProperty64", _BasicModule_SetProperty64);
            return (_local1);
        }
        public function set template8(_arg1:ProgressMonitor):void{
            var _local2:Object;
            _local2 = this._1981727486template8;
            if (_local2 !== _arg1){
                this._1981727486template8 = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "template8", _local2, _arg1));
            };
        }
        public function set template9(_arg1:PersonalPlan):void{
            var _local2:Object;
            _local2 = this._1981727487template9;
            if (_local2 !== _arg1){
                this._1981727487template9 = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "template9", _local2, _arg1));
            };
        }
        public function set template2(_arg1:PicRightInfoText):void{
            var _local2:Object;
            _local2 = this._1981727480template2;
            if (_local2 !== _arg1){
                this._1981727480template2 = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "template2", _local2, _arg1));
            };
        }
        public function set template3(_arg1:DailyConsumption):void{
            var _local2:Object;
            _local2 = this._1981727481template3;
            if (_local2 !== _arg1){
                this._1981727481template3 = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "template3", _local2, _arg1));
            };
        }
        public function set template6(_arg1:TrafficLightResults):void{
            var _local2:Object;
            _local2 = this._1981727484template6;
            if (_local2 !== _arg1){
                this._1981727484template6 = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "template6", _local2, _arg1));
            };
        }
        public function set template7a(_arg1:TypicalWeekCompletion):void{
            var _local2:Object;
            _local2 = this._1304009988template7a;
            if (_local2 !== _arg1){
                this._1304009988template7a = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "template7a", _local2, _arg1));
            };
        }
        public function set template7(_arg1:TypicalWeek):void{
            var _local2:Object;
            _local2 = this._1981727485template7;
            if (_local2 !== _arg1){
                this._1981727485template7 = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "template7", _local2, _arg1));
            };
        }
        public function outro(_arg1:Event=null):void{
            BalanceModelLocator.getInstance().isOutroActive = true;
            if (contentIndex < (contentCollection.Contents.length - 1)){
                fadeOutContainer1.addEventListener(EffectEvent.EFFECT_END, outroEnd);
                fadeOutContainer1.play();
                if (!compareNextImageURL()){
                    fadeOutContainer2.play();
                };
            } else {
                outroEnd();
            };
        }
        private function _BasicModule_SetEventHandler2_i():SetEventHandler{
            var _local1:SetEventHandler = new SetEventHandler();
            _BasicModule_SetEventHandler2 = _local1;
            _local1.name = "ioError";
            _local1.addEventListener("handler", ___BasicModule_SetEventHandler2_handler);
            BindingManager.executeBindings(this, "_BasicModule_SetEventHandler2", _BasicModule_SetEventHandler2);
            return (_local1);
        }
        private function _BasicModule_PicLeftInfoText1_i():PicLeftInfoText{
            var _local1:PicLeftInfoText = new PicLeftInfoText();
            template1 = _local1;
            _local1.id = "template1";
            BindingManager.executeBindings(this, "template1", template1);
            if (!_local1.document){
                _local1.document = this;
            };
            return (_local1);
        }
        private function _BasicModule_SetProperty75_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _BasicModule_SetProperty75 = _local1;
            _local1.name = "currentTemplate";
            BindingManager.executeBindings(this, "_BasicModule_SetProperty75", _BasicModule_SetProperty75);
            return (_local1);
        }
        private function _BasicModule_SetEventHandler10_i():SetEventHandler{
            var _local1:SetEventHandler = new SetEventHandler();
            _BasicModule_SetEventHandler10 = _local1;
            _local1.name = "click";
            _local1.addEventListener("handler", ___BasicModule_SetEventHandler10_handler);
            BindingManager.executeBindings(this, "_BasicModule_SetEventHandler10", _BasicModule_SetEventHandler10);
            return (_local1);
        }
        private function _BasicModule_SetProperty52_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _BasicModule_SetProperty52 = _local1;
            _local1.name = "transitionContainer2";
            BindingManager.executeBindings(this, "_BasicModule_SetProperty52", _BasicModule_SetProperty52);
            return (_local1);
        }
        private function _BasicModule_SetProperty3_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _BasicModule_SetProperty3 = _local1;
            _local1.name = "transitionContainer2";
            BindingManager.executeBindings(this, "_BasicModule_SetProperty3", _BasicModule_SetProperty3);
            return (_local1);
        }
        private function _BasicModule_State5_c():State{
            var _local1:State = new State();
            _local1.name = "PicRightInfoText";
            _local1.overrides = [_BasicModule_AddChild5_c(), _BasicModule_SetEventHandler7_i(), _BasicModule_SetEventHandler8_i(), _BasicModule_SetProperty16_i(), _BasicModule_SetProperty17_i(), _BasicModule_SetProperty18_i(), _BasicModule_SetProperty19_i(), _BasicModule_SetProperty20_i()];
            return (_local1);
        }
        private function _BasicModule_SetProperty40_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _BasicModule_SetProperty40 = _local1;
            _local1.name = "currentTemplate";
            BindingManager.executeBindings(this, "_BasicModule_SetProperty40", _BasicModule_SetProperty40);
            return (_local1);
        }
        private function _BasicModule_SetProperty63_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _BasicModule_SetProperty63 = _local1;
            _local1.name = "contentImage";
            BindingManager.executeBindings(this, "_BasicModule_SetProperty63", _BasicModule_SetProperty63);
            return (_local1);
        }
        private function _BasicModule_TextArea1_i():TextArea{
            var _local1:TextArea = new TextArea();
            _BasicModule_TextArea1 = _local1;
            _local1.width = 800;
            _local1.height = 600;
            _local1.id = "_BasicModule_TextArea1";
            BindingManager.executeBindings(this, "_BasicModule_TextArea1", _BasicModule_TextArea1);
            if (!_local1.document){
                _local1.document = this;
            };
            return (_local1);
        }
        private function _BasicModule_SetProperty74_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _BasicModule_SetProperty74 = _local1;
            _local1.name = "ctaBtn";
            BindingManager.executeBindings(this, "_BasicModule_SetProperty74", _BasicModule_SetProperty74);
            return (_local1);
        }
        public function ___BasicModule_SetEventHandler10_handler(_arg1:Object):void{
            outro();
        }
        private function _BasicModule_SetProperty51_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _BasicModule_SetProperty51 = _local1;
            _local1.name = "transitionContainer1";
            BindingManager.executeBindings(this, "_BasicModule_SetProperty51", _BasicModule_SetProperty51);
            return (_local1);
        }
        public function ___BasicModule_SetEventHandler5_handler(_arg1:Object):void{
            imageLoaded();
        }
        public function ___BasicModule_SetEventHandler9_handler(_arg1:Object):void{
            imageLoaded();
        }
        public function ___BasicModule_SetEventHandler1_handler(_arg1:Object):void{
            imageLoaded();
        }
        public function ___BasicModule_SetEventHandler18_handler(_arg1:Object):void{
            outro();
        }
        private function _BasicModule_SetProperty2_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _BasicModule_SetProperty2 = _local1;
            _local1.name = "transitionContainer1";
            BindingManager.executeBindings(this, "_BasicModule_SetProperty2", _BasicModule_SetProperty2);
            return (_local1);
        }
        private function _BasicModule_State4_c():State{
            var _local1:State = new State();
            _local1.name = "Quiz";
            _local1.overrides = [_BasicModule_AddChild4_c(), _BasicModule_SetEventHandler5_i(), _BasicModule_SetEventHandler6_i(), _BasicModule_SetProperty11_i(), _BasicModule_SetProperty12_i(), _BasicModule_SetProperty13_i(), _BasicModule_SetProperty14_i(), _BasicModule_SetProperty15_i()];
            return (_local1);
        }
        public function ___BasicModule_AbstractBalanceModule1_currentStateChange(_arg1:StateChangeEvent):void{
            initState(_arg1);
        }
        private function _BasicModule_SetProperty62_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _BasicModule_SetProperty62 = _local1;
            _local1.name = "transitionContainer2";
            BindingManager.executeBindings(this, "_BasicModule_SetProperty62", _BasicModule_SetProperty62);
            return (_local1);
        }
        private function _BasicModule_SetEventHandler1_i():SetEventHandler{
            var _local1:SetEventHandler = new SetEventHandler();
            _BasicModule_SetEventHandler1 = _local1;
            _local1.name = "imageLoaded";
            _local1.addEventListener("handler", ___BasicModule_SetEventHandler1_handler);
            BindingManager.executeBindings(this, "_BasicModule_SetEventHandler1", _BasicModule_SetEventHandler1);
            return (_local1);
        }
        public function ___BasicModule_SetEventHandler14_handler(_arg1:Object):void{
            outro();
        }
        private function _BasicModule_TypicalWeekCompletion1_i():TypicalWeekCompletion{
            var _local1:TypicalWeekCompletion = new TypicalWeekCompletion();
            template7a = _local1;
            _local1.id = "template7a";
            BindingManager.executeBindings(this, "template7a", template7a);
            if (!_local1.document){
                _local1.document = this;
            };
            return (_local1);
        }
        private function _BasicModule_Registration1_i():Registration{
            var _local1:Registration = new Registration();
            template12 = _local1;
            _local1.id = "template12";
            BindingManager.executeBindings(this, "template12", template12);
            if (!_local1.document){
                _local1.document = this;
            };
            return (_local1);
        }
        private function _BasicModule_SetProperty50_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _BasicModule_SetProperty50 = _local1;
            _local1.name = "currentTemplate";
            BindingManager.executeBindings(this, "_BasicModule_SetProperty50", _BasicModule_SetProperty50);
            return (_local1);
        }
        private function _BasicModule_SetProperty73_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _BasicModule_SetProperty73 = _local1;
            _local1.name = "contentImage";
            BindingManager.executeBindings(this, "_BasicModule_SetProperty73", _BasicModule_SetProperty73);
            return (_local1);
        }
        private function _BasicModule_State3_c():State{
            var _local1:State = new State();
            _local1.name = "GlassPicture";
            _local1.overrides = [_BasicModule_AddChild3_c(), _BasicModule_SetEventHandler3_i(), _BasicModule_SetEventHandler4_i(), _BasicModule_SetProperty6_i(), _BasicModule_SetProperty7_i(), _BasicModule_SetProperty8_i(), _BasicModule_SetProperty9_i(), _BasicModule_SetProperty10_i()];
            return (_local1);
        }
        private function _BasicModule_SetProperty1_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _BasicModule_SetProperty1 = _local1;
            _local1.name = "currentTemplate";
            BindingManager.executeBindings(this, "_BasicModule_SetProperty1", _BasicModule_SetProperty1);
            return (_local1);
        }
        private function _BasicModule_SetProperty61_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _BasicModule_SetProperty61 = _local1;
            _local1.name = "transitionContainer1";
            BindingManager.executeBindings(this, "_BasicModule_SetProperty61", _BasicModule_SetProperty61);
            return (_local1);
        }
        protected function outroEnd(_arg1:EffectEvent=null):void{
            fadeOutContainer1.removeEventListener(EffectEvent.EFFECT_END, outroEnd);
            if (direction == BACK){
                previous();
            } else {
                next();
            };
        }
        private function _BasicModule_ProgressMonitor1_i():ProgressMonitor{
            var _local1:ProgressMonitor = new ProgressMonitor();
            template8 = _local1;
            _local1.id = "template8";
            BindingManager.executeBindings(this, "template8", template8);
            if (!_local1.document){
                _local1.document = this;
            };
            return (_local1);
        }
        private function _BasicModule_SetProperty72_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _BasicModule_SetProperty72 = _local1;
            _local1.name = "transitionContainer2";
            BindingManager.executeBindings(this, "_BasicModule_SetProperty72", _BasicModule_SetProperty72);
            return (_local1);
        }
        private function _BasicModule_SetProperty19_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _BasicModule_SetProperty19 = _local1;
            _local1.name = "contentImage";
            BindingManager.executeBindings(this, "_BasicModule_SetProperty19", _BasicModule_SetProperty19);
            return (_local1);
        }
        public function set fadeInContainer2(_arg1:Fade):void{
            var _local2:Object;
            _local2 = this._335735950fadeInContainer2;
            if (_local2 !== _arg1){
                this._335735950fadeInContainer2 = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "fadeInContainer2", _local2, _arg1));
            };
        }
        private function _BasicModule_State2_c():State{
            var _local1:State = new State();
            _local1.name = "PicLeftInfoText";
            _local1.overrides = [_BasicModule_AddChild2_c(), _BasicModule_SetEventHandler1_i(), _BasicModule_SetEventHandler2_i(), _BasicModule_SetProperty1_i(), _BasicModule_SetProperty2_i(), _BasicModule_SetProperty3_i(), _BasicModule_SetProperty4_i(), _BasicModule_SetProperty5_i()];
            return (_local1);
        }
        public function set fadeInContainer1(_arg1:Fade):void{
            var _local2:Object;
            _local2 = this._335735951fadeInContainer1;
            if (_local2 !== _arg1){
                this._335735951fadeInContainer1 = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "fadeInContainer1", _local2, _arg1));
            };
        }
        override public function next(_arg1:Event=null):Boolean{
            return (super.next(_arg1));
        }
        public function set bounceDownEffect(_arg1:Move):void{
            var _local2:Object;
            _local2 = this._1810141563bounceDownEffect;
            if (_local2 !== _arg1){
                this._1810141563bounceDownEffect = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "bounceDownEffect", _local2, _arg1));
            };
        }
        private function _BasicModule_SetProperty60_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _BasicModule_SetProperty60 = _local1;
            _local1.name = "currentTemplate";
            BindingManager.executeBindings(this, "_BasicModule_SetProperty60", _BasicModule_SetProperty60);
            return (_local1);
        }
        public function get template2():PicRightInfoText{
            return (this._1981727480template2);
        }
        public function get template3():DailyConsumption{
            return (this._1981727481template3);
        }
        public function get template6():TrafficLightResults{
            return (this._1981727484template6);
        }
        public function get template7():TypicalWeek{
            return (this._1981727485template7);
        }
        public function get template9():PersonalPlan{
            return (this._1981727487template9);
        }
        private function _BasicModule_AddChild9_c():AddChild{
            var _local1:AddChild = new AddChild();
            _local1.creationPolicy = "all";
            _local1.targetFactory = new DeferredInstanceFromFunction(_BasicModule_TypicalWeekCompletion1_i);
            return (_local1);
        }
        public function get template1():PicLeftInfoText{
            return (this._1981727479template1);
        }
        public function get template8():ProgressMonitor{
            return (this._1981727486template8);
        }
        private function _BasicModule_SetProperty71_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _BasicModule_SetProperty71 = _local1;
            _local1.name = "transitionContainer1";
            BindingManager.executeBindings(this, "_BasicModule_SetProperty71", _BasicModule_SetProperty71);
            return (_local1);
        }
        private function _BasicModule_State1_c():State{
            var _local1:State = new State();
            _local1.name = "defaultState";
            _local1.overrides = [_BasicModule_AddChild1_c()];
            return (_local1);
        }
        protected function startIntro():void{
            if (fadeInContainer1 != null){
                fadeInContainer1.play();
            };
            if (fadeInContainer2 != null){
                if (transitionContainer2 != null){
                    if (transitionContainer2.alpha == 0){
                        fadeInContainer2.play();
                    };
                };
            };
        }
        private function _BasicModule_SetProperty18_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _BasicModule_SetProperty18 = _local1;
            _local1.name = "transitionContainer2";
            BindingManager.executeBindings(this, "_BasicModule_SetProperty18", _BasicModule_SetProperty18);
            return (_local1);
        }
        protected function initIntro():void{
            callLater(startIntro);
        }
        private function _BasicModule_AddChild8_c():AddChild{
            var _local1:AddChild = new AddChild();
            _local1.creationPolicy = "all";
            _local1.targetFactory = new DeferredInstanceFromFunction(_BasicModule_TypicalWeek1_i);
            return (_local1);
        }
        private function _BasicModule_SetProperty29_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _BasicModule_SetProperty29 = _local1;
            _local1.name = "contentImage";
            BindingManager.executeBindings(this, "_BasicModule_SetProperty29", _BasicModule_SetProperty29);
            return (_local1);
        }
        public function ___BasicModule_SetEventHandler4_handler(_arg1:Object):void{
            imageLoadedError();
        }
        public function ___BasicModule_SetEventHandler8_handler(_arg1:Object):void{
            imageLoadedError();
        }
        private function _BasicModule_SetProperty70_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _BasicModule_SetProperty70 = _local1;
            _local1.name = "currentTemplate";
            BindingManager.executeBindings(this, "_BasicModule_SetProperty70", _BasicModule_SetProperty70);
            return (_local1);
        }
        public function ___BasicModule_AbstractBalanceModule1_creationComplete(_arg1:FlexEvent):void{
            init(_arg1);
        }
        public function ___BasicModule_SetEventHandler13_handler(_arg1:Object):void{
            outro();
        }
        public function ___BasicModule_SetEventHandler17_handler(_arg1:Object):void{
            outro();
        }
        private function _BasicModule_Quiz1_i():Quiz{
            var _local1:Quiz = new Quiz();
            template1a = _local1;
            _local1.id = "template1a";
            BindingManager.executeBindings(this, "template1a", template1a);
            if (!_local1.document){
                _local1.document = this;
            };
            return (_local1);
        }
        private function _BasicModule_SetProperty17_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _BasicModule_SetProperty17 = _local1;
            _local1.name = "transitionContainer1";
            BindingManager.executeBindings(this, "_BasicModule_SetProperty17", _BasicModule_SetProperty17);
            return (_local1);
        }
        public function get fadeInContainer1():Fade{
            return (this._335735951fadeInContainer1);
        }
        public function get fadeInContainer2():Fade{
            return (this._335735950fadeInContainer2);
        }
        private function _BasicModule_AddChild7_c():AddChild{
            var _local1:AddChild = new AddChild();
            _local1.creationPolicy = "all";
            _local1.targetFactory = new DeferredInstanceFromFunction(_BasicModule_TrafficLightResults1_i);
            return (_local1);
        }
        private function _BasicModule_SetProperty28_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _BasicModule_SetProperty28 = _local1;
            _local1.name = "transitionContainer2";
            BindingManager.executeBindings(this, "_BasicModule_SetProperty28", _BasicModule_SetProperty28);
            return (_local1);
        }
        private function decideDelayCallImageLoaded():void{
            if (contentImage != null){
                if ((((((content.PresenterImageUrl == null)) || ((content.PresenterImageUrl.length == 0)))) || ((content.PresenterImageUrl == lastPresenterImageURL)))){
                    delayCallImageLoaded();
                };
            } else {
                delayCallImageLoaded();
            };
        }
        private function _BasicModule_AddChild17_c():AddChild{
            var _local1:AddChild = new AddChild();
            _local1.creationPolicy = "all";
            _local1.targetFactory = new DeferredInstanceFromFunction(_BasicModule_CompletionGraph1_i);
            return (_local1);
        }
        override public function initialize():void{
            var target:* = null;
            var watcherSetupUtilClass:* = null;
            mx_internal::setDocumentDescriptor(_documentDescriptor_);
            var bindings:* = _BasicModule_bindingsSetup();
            var watchers:* = [];
            target = this;
            if (_watcherSetupUtil == null){
                watcherSetupUtilClass = getDefinitionByName("_com_redbox_changetech_view_modules_BasicModuleWatcherSetupUtil");
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
        private function _BasicModule_State17_c():State{
            var _local1:State = new State();
            _local1.name = "CompletionGraph";
            _local1.overrides = [_BasicModule_AddChild17_c(), _BasicModule_SetEventHandler19_i(), _BasicModule_SetProperty75_i(), _BasicModule_SetProperty76_i(), _BasicModule_SetProperty77_i(), _BasicModule_SetProperty78_i(), _BasicModule_SetProperty79_i()];
            return (_local1);
        }
        private function keyDown(_arg1:KeyboardEvent):void{
            if (keyIsDown){
                return;
            };
            if (_arg1.keyCode == Keyboard.RIGHT){
                next();
            };
            keyIsDown = true;
        }
        private function _BasicModule_SetProperty39_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _BasicModule_SetProperty39 = _local1;
            _local1.name = "ctaBtn";
            BindingManager.executeBindings(this, "_BasicModule_SetProperty39", _BasicModule_SetProperty39);
            return (_local1);
        }
        private function _BasicModule_PersonalPlan1_i():PersonalPlan{
            var _local1:PersonalPlan = new PersonalPlan();
            template9 = _local1;
            _local1.id = "template9";
            BindingManager.executeBindings(this, "template9", template9);
            if (!_local1.document){
                _local1.document = this;
            };
            return (_local1);
        }
        private function _BasicModule_SetProperty16_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _BasicModule_SetProperty16 = _local1;
            _local1.name = "currentTemplate";
            BindingManager.executeBindings(this, "_BasicModule_SetProperty16", _BasicModule_SetProperty16);
            return (_local1);
        }
        protected function compareNextImageURL():Boolean{
            var _local2:String;
            var _local3:int;
            var _local4:Content;
            var _local1:String = content.PresenterImageUrl;
            if (direction == BACK){
                _local3 = ((contentIndex)!=0) ? (contentIndex - 1) : contentIndex;
            } else {
                _local3 = ((contentIndex)!=(contentCollection.Contents.length - 1)) ? (contentIndex + 1) : contentIndex;
            };
            if (_local3 == contentIndex){
                _local2 = "";
            } else {
                _local4 = Content(contentCollection.Contents[_local3]);
                _local2 = _local4.PresenterImageUrl;
            };
            return ((_local1 == _local2));
        }
        protected function change():void{
            if (content == null){
                return;
            };
            if (content.Template != null){
            };
        }
        private function _BasicModule_AddChild6_c():AddChild{
            var _local1:AddChild = new AddChild();
            _local1.creationPolicy = "all";
            _local1.targetFactory = new DeferredInstanceFromFunction(_BasicModule_DailyConsumption1_i);
            return (_local1);
        }
        private function _BasicModule_SetProperty27_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _BasicModule_SetProperty27 = _local1;
            _local1.name = "transitionContainer1";
            BindingManager.executeBindings(this, "_BasicModule_SetProperty27", _BasicModule_SetProperty27);
            return (_local1);
        }
        private function _BasicModule_AddChild16_c():AddChild{
            var _local1:AddChild = new AddChild();
            _local1.creationPolicy = "all";
            _local1.targetFactory = new DeferredInstanceFromFunction(_BasicModule_WeeklyConsumption1_i);
            return (_local1);
        }
        private function _BasicModule_State16_c():State{
            var _local1:State = new State();
            _local1.name = "WeeklyConsumption";
            _local1.overrides = [_BasicModule_AddChild16_c(), _BasicModule_SetEventHandler18_i(), _BasicModule_SetProperty70_i(), _BasicModule_SetProperty71_i(), _BasicModule_SetProperty72_i(), _BasicModule_SetProperty73_i(), _BasicModule_SetProperty74_i()];
            return (_local1);
        }
        private function _BasicModule_SetProperty15_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _BasicModule_SetProperty15 = _local1;
            _local1.name = "ctaBtn";
            BindingManager.executeBindings(this, "_BasicModule_SetProperty15", _BasicModule_SetProperty15);
            return (_local1);
        }
        private function _BasicModule_SetProperty38_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _BasicModule_SetProperty38 = _local1;
            _local1.name = "contentImage";
            BindingManager.executeBindings(this, "_BasicModule_SetProperty38", _BasicModule_SetProperty38);
            return (_local1);
        }
        private function _BasicModule_SetEventHandler19_i():SetEventHandler{
            var _local1:SetEventHandler = new SetEventHandler();
            _BasicModule_SetEventHandler19 = _local1;
            _local1.addEventListener("handler", ___BasicModule_SetEventHandler19_handler);
            BindingManager.executeBindings(this, "_BasicModule_SetEventHandler19", _BasicModule_SetEventHandler19);
            return (_local1);
        }
        private function _BasicModule_AddChild5_c():AddChild{
            var _local1:AddChild = new AddChild();
            _local1.creationPolicy = "all";
            _local1.targetFactory = new DeferredInstanceFromFunction(_BasicModule_PicRightInfoText1_i);
            return (_local1);
        }
        override protected function contentChanged(){
            imageIsLoaded = false;
            if (current_Template == content.Template){
                decideDelayCallImageLoaded();
            };
            current_Template = String(content.Template);
        }
        protected function delayCallImageLoaded():void{
            t = new Timer(100, 1);
            t.addEventListener(TimerEvent.TIMER_COMPLETE, imageLoaded);
            t.start();
        }

        public static function set watcherSetupUtil(_arg1:IWatcherSetupUtil):void{
            BasicModule._watcherSetupUtil = _arg1;
        }

    }
}//package com.redbox.changetech.view.modules 
