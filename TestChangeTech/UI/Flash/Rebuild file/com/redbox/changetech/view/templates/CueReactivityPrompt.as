//Created by Action Script Viewer - http://www.buraks.com/asv
package com.redbox.changetech.view.templates {
    import flash.display.*;
    import flash.geom.*;
    import flash.media.*;
    import flash.text.*;
    import mx.core.*;
    import flash.events.*;
    import mx.events.*;
    import mx.styles.*;
    import mx.controls.*;
    import mx.states.*;
    import mx.binding.*;
    import mx.containers.*;
    import com.redbox.changetech.view.components.*;
    import flash.utils.*;
    import flash.system.*;
    import flash.accessibility.*;
    import flash.xml.*;
    import flash.net.*;
    import com.redbox.changetech.view.modules.*;
    import com.rictus.reflector.*;
    import flash.filters.*;
    import flash.ui.*;
    import flash.external.*;
    import flash.debugger.*;
    import flash.errors.*;
    import flash.printing.*;
    import flash.profiler.*;

    public class CueReactivityPrompt extends ModuleViewTemplate implements IBindingClient {

        private var _811296866contentImage:Image
        public var _CueReactivityPrompt_State1:State
        public var _CueReactivityPrompt_State2:State
        private var IMAGE_LOADED:String = "imageLoaded"
        private var _1214659555displayNumberCanvas:Canvas
        private var _1541255226stimulus:Canvas
        private var _30089661currentDisplayNum:Number
        private var _1803336908textFieldContainer:VBox
        mx_internal var _watchers:Array
        private var _1319908771_presenterImageUrl:String
        private var timer:Timer = null
        mx_internal var _bindingsBeginWithWord:Object
        mx_internal var _bindingsByDestination:Object
        private var _1240251170stim_title:BalanceTextArea
        private var _1448659846numReflector:Reflector
        mx_internal var _bindings:Array
        private var _2129146488displayNumberText:Text
        private var _documentDescriptor_:UIComponentDescriptor

        private static var COUNTDOWN:String = "COUNTDOWN";
        private static var _watcherSetupUtil:IWatcherSetupUtil;
        private static var STIMULI:String = "STIMULI";
        private static var COUNTER_DEFAULT:Number = 4;

        public function CueReactivityPrompt(){
            _documentDescriptor_ = new UIComponentDescriptor({type:ModuleViewTemplate});
            _30089661currentDisplayNum = COUNTER_DEFAULT;
            _bindings = [];
            _watchers = [];
            _bindingsByDestination = {};
            _bindingsBeginWithWord = {};
            super();
            mx_internal::_document = this;
            this.states = [_CueReactivityPrompt_State1_i(), _CueReactivityPrompt_State2_i()];
            this.addEventListener("creationComplete", ___CueReactivityPrompt_ModuleViewTemplate1_creationComplete);
        }
        private function _CueReactivityPrompt_Reflector1_i():Reflector{
            var _local1:Reflector = new Reflector();
            numReflector = _local1;
            _local1.falloff = 0.2;
            _local1.alpha = 0.8;
            _local1.id = "numReflector";
            BindingManager.executeBindings(this, "numReflector", numReflector);
            if (!_local1.document){
                _local1.document = this;
            };
            return (_local1);
        }
        private function set currentDisplayNum(_arg1:Number):void{
            var _local2:Object = this._30089661currentDisplayNum;
            if (_local2 !== _arg1){
                this._30089661currentDisplayNum = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "currentDisplayNum", _local2, _arg1));
            };
        }
        private function contentImageLoaded():void{
            trace("contentImageLoaded");
            dispatchEvent(new Event(IMAGE_LOADED, true));
            init();
        }
        private function init():void{
            currentState = COUNTDOWN;
            timer = new Timer(1500);
            timer.addEventListener(TimerEvent.TIMER, iniNumDisplay);
            timer.start();
            iniNumDisplay(new Event(Event.INIT));
            trace(" cue reactivity PROPMPT INIT ");
        }
        private function _CueReactivityPrompt_AddChild2_c():AddChild{
            var _local1:AddChild = new AddChild();
            _local1.targetFactory = new DeferredInstanceFromFunction(_CueReactivityPrompt_Canvas2_i);
            return (_local1);
        }
        override public function initialize():void{
            var target:* = null;
            var watcherSetupUtilClass:* = null;
            mx_internal::setDocumentDescriptor(_documentDescriptor_);
            var bindings:* = _CueReactivityPrompt_bindingsSetup();
            var watchers:* = [];
            target = this;
            if (_watcherSetupUtil == null){
                watcherSetupUtilClass = getDefinitionByName("_com_redbox_changetech_view_templates_CueReactivityPromptWatcherSetupUtil");
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
        public function __contentImage_complete(_arg1:Event):void{
            contentImageLoaded();
        }
        private function _CueReactivityPrompt_BalanceTextArea1_i():BalanceTextArea{
            var _local1:BalanceTextArea = new BalanceTextArea();
            stim_title = _local1;
            _local1.visible = false;
            _local1.setStyle("fontFamily", "Helvetica Neue");
            _local1.setStyle("fontSize", 18);
            _local1.id = "stim_title";
            BindingManager.executeBindings(this, "stim_title", stim_title);
            if (!_local1.document){
                _local1.document = this;
            };
            return (_local1);
        }
        private function _CueReactivityPrompt_Canvas1_i():Canvas{
            var _local1:Canvas = new Canvas();
            displayNumberCanvas = _local1;
            _local1.id = "displayNumberCanvas";
            BindingManager.executeBindings(this, "displayNumberCanvas", displayNumberCanvas);
            if (!_local1.document){
                _local1.document = this;
            };
            _local1.addChild(_CueReactivityPrompt_VBox1_i());
            return (_local1);
        }
        public function __contentImage_ioError(_arg1:IOErrorEvent):void{
            dispatchEvent(new Event(IOErrorEvent.IO_ERROR, true));
        }
        private function _CueReactivityPrompt_State1_i():State{
            var _local1:State = new State();
            _CueReactivityPrompt_State1 = _local1;
            _local1.overrides = [_CueReactivityPrompt_AddChild1_c()];
            BindingManager.executeBindings(this, "_CueReactivityPrompt_State1", _CueReactivityPrompt_State1);
            return (_local1);
        }
        private function _CueReactivityPrompt_VBox1_i():VBox{
            var _local1:VBox = new VBox();
            textFieldContainer = _local1;
            _local1.percentWidth = 100;
            _local1.percentHeight = 75;
            _local1.setStyle("horizontalAlign", "center");
            _local1.setStyle("verticalAlign", "middle");
            _local1.setStyle("paddingLeft", 20);
            _local1.setStyle("paddingRight", 20);
            _local1.setStyle("paddingTop", 20);
            _local1.setStyle("paddingBottom", 50);
            _local1.id = "textFieldContainer";
            if (!_local1.document){
                _local1.document = this;
            };
            _local1.addChild(_CueReactivityPrompt_Text1_i());
            _local1.addChild(_CueReactivityPrompt_Reflector1_i());
            return (_local1);
        }
        private function _CueReactivityPrompt_Image1_i():Image{
            var _local1:Image = new Image();
            contentImage = _local1;
            _local1.visible = false;
            _local1.addEventListener("ioError", __contentImage_ioError);
            _local1.addEventListener("complete", __contentImage_complete);
            _local1.id = "contentImage";
            BindingManager.executeBindings(this, "contentImage", contentImage);
            if (!_local1.document){
                _local1.document = this;
            };
            return (_local1);
        }
        public function ___CueReactivityPrompt_ModuleViewTemplate1_creationComplete(_arg1:FlexEvent):void{
            onCreationComplete();
        }
        private function onCreationComplete():void{
            currentState = STIMULI;
            _presenterImageUrl = content.PresenterImageUrl;
        }
        public function get stimulus():Canvas{
            return (this._1541255226stimulus);
        }
        private function set _presenterImageUrl(_arg1:String):void{
            var _local2:Object = this._1319908771_presenterImageUrl;
            if (_local2 !== _arg1){
                this._1319908771_presenterImageUrl = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "_presenterImageUrl", _local2, _arg1));
            };
        }
        public function set stim_title(_arg1:BalanceTextArea):void{
            var _local2:Object = this._1240251170stim_title;
            if (_local2 !== _arg1){
                this._1240251170stim_title = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "stim_title", _local2, _arg1));
            };
        }
        public function set contentImage(_arg1:Image):void{
            var _local2:Object = this._811296866contentImage;
            if (_local2 !== _arg1){
                this._811296866contentImage = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "contentImage", _local2, _arg1));
            };
        }
        public function set displayNumberCanvas(_arg1:Canvas):void{
            var _local2:Object = this._1214659555displayNumberCanvas;
            if (_local2 !== _arg1){
                this._1214659555displayNumberCanvas = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "displayNumberCanvas", _local2, _arg1));
            };
        }
        private function _CueReactivityPrompt_bindingsSetup():Array{
            var binding:* = null;
            var result:* = [];
            binding = new Binding(this, function ():String{
                var _local1:* = COUNTDOWN;
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                _CueReactivityPrompt_State1.name = _arg1;
            }, "_CueReactivityPrompt_State1.name");
            result[0] = binding;
            binding = new Binding(this, function ():Number{
                return (model.currentStageWidth);
            }, function (_arg1:Number):void{
                displayNumberCanvas.width = _arg1;
            }, "displayNumberCanvas.width");
            result[1] = binding;
            binding = new Binding(this, function ():Number{
                return (model.currentStageHeight);
            }, function (_arg1:Number):void{
                displayNumberCanvas.height = _arg1;
            }, "displayNumberCanvas.height");
            result[2] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = String(currentDisplayNum);
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                displayNumberText.text = _arg1;
            }, "displayNumberText.text");
            result[3] = binding;
            binding = new Binding(this, function ():Number{
                return (displayNumberCanvas.width);
            }, function (_arg1:Number):void{
                displayNumberText.width = _arg1;
            }, "displayNumberText.width");
            result[4] = binding;
            binding = new Binding(this, function ():Number{
                return (((displayNumberText.y + displayNumberText.height) + 0.1));
            }, function (_arg1:Number):void{
                numReflector.y = _arg1;
            }, "numReflector.y");
            result[5] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = STIMULI;
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                _CueReactivityPrompt_State2.name = _arg1;
            }, "_CueReactivityPrompt_State2.name");
            result[6] = binding;
            binding = new Binding(this, function ():Number{
                return (model.currentStageWidth);
            }, function (_arg1:Number):void{
                stimulus.width = _arg1;
            }, "stimulus.width");
            result[7] = binding;
            binding = new Binding(this, function ():Number{
                return (model.currentStageHeight);
            }, function (_arg1:Number):void{
                stimulus.height = _arg1;
            }, "stimulus.height");
            result[8] = binding;
            binding = new Binding(this, function ():Number{
                return (((model.currentStageWidth / 2) - (contentImage.width / 2)));
            }, function (_arg1:Number):void{
                contentImage.x = _arg1;
            }, "contentImage.x");
            result[9] = binding;
            binding = new Binding(this, function ():Number{
                return (((model.currentStageHeight / 2) - (contentImage.height / 2)));
            }, function (_arg1:Number):void{
                contentImage.y = _arg1;
            }, "contentImage.y");
            result[10] = binding;
            binding = new Binding(this, function ():Object{
                return (_presenterImageUrl);
            }, function (_arg1:Object):void{
                contentImage.source = _arg1;
            }, "contentImage.source");
            result[11] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = content.Title;
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                stim_title.htmlText = _arg1;
            }, "stim_title.htmlText");
            result[12] = binding;
            return (result);
        }
        override protected function reset():void{
            currentDisplayNum = COUNTER_DEFAULT;
            contentImage.visible = false;
            stim_title.visible = false;
            currentState = STIMULI;
            _presenterImageUrl = content.PresenterImageUrl;
        }
        private function _CueReactivityPrompt_AddChild1_c():AddChild{
            var _local1:AddChild = new AddChild();
            _local1.targetFactory = new DeferredInstanceFromFunction(_CueReactivityPrompt_Canvas1_i);
            return (_local1);
        }
        private function _CueReactivityPrompt_VBox2_c():VBox{
            var _local1:VBox = new VBox();
            _local1.percentWidth = 100;
            _local1.setStyle("horizontalAlign", "center");
            if (!_local1.document){
                _local1.document = this;
            };
            _local1.addChild(_CueReactivityPrompt_Image1_i());
            _local1.addChild(_CueReactivityPrompt_BalanceTextArea1_i());
            return (_local1);
        }
        private function get _presenterImageUrl():String{
            return (this._1319908771_presenterImageUrl);
        }
        public function get stim_title():BalanceTextArea{
            return (this._1240251170stim_title);
        }
        private function _CueReactivityPrompt_Canvas2_i():Canvas{
            var _local1:Canvas = new Canvas();
            stimulus = _local1;
            _local1.id = "stimulus";
            BindingManager.executeBindings(this, "stimulus", stimulus);
            if (!_local1.document){
                _local1.document = this;
            };
            _local1.addChild(_CueReactivityPrompt_VBox2_c());
            return (_local1);
        }
        public function get textFieldContainer():VBox{
            return (this._1803336908textFieldContainer);
        }
        public function get contentImage():Image{
            return (this._811296866contentImage);
        }
        public function get displayNumberCanvas():Canvas{
            return (this._1214659555displayNumberCanvas);
        }
        private function iniNumDisplay(_arg1:Event):void{
            trace(("currentState=" + currentState));
            switch (currentState){
                case COUNTDOWN:
                    currentDisplayNum--;
                    numReflector.target = null;
                    numReflector.target = displayNumberText;
                    trace(("counting down" + currentDisplayNum));
                    if (currentDisplayNum == 0){
                        currentState = STIMULI;
                        contentImage.visible = true;
                        stim_title.visible = true;
                        timer.delay = 1000;
                    };
                    break;
                case STIMULI:
                    timer.removeEventListener(TimerEvent.TIMER, iniNumDisplay);
                    if (model.flashVars.mode != "PREVIEW"){
                        BasicModule(module).next();
                    };
                    break;
            };
        }
        public function set textFieldContainer(_arg1:VBox):void{
            var _local2:Object = this._1803336908textFieldContainer;
            if (_local2 !== _arg1){
                this._1803336908textFieldContainer = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "textFieldContainer", _local2, _arg1));
            };
        }
        private function _CueReactivityPrompt_Text1_i():Text{
            var _local1:Text = new Text();
            displayNumberText = _local1;
            _local1.x = 0;
            _local1.height = 143;
            _local1.setStyle("textAlign", "center");
            _local1.setStyle("fontFamily", "Digital-7");
            _local1.setStyle("fontSize", 1000);
            _local1.setStyle("color", 3512262);
            _local1.id = "displayNumberText";
            BindingManager.executeBindings(this, "displayNumberText", displayNumberText);
            if (!_local1.document){
                _local1.document = this;
            };
            return (_local1);
        }
        private function get currentDisplayNum():Number{
            return (this._30089661currentDisplayNum);
        }
        private function _CueReactivityPrompt_State2_i():State{
            var _local1:State = new State();
            _CueReactivityPrompt_State2 = _local1;
            _local1.overrides = [_CueReactivityPrompt_AddChild2_c()];
            BindingManager.executeBindings(this, "_CueReactivityPrompt_State2", _CueReactivityPrompt_State2);
            return (_local1);
        }
        public function set numReflector(_arg1:Reflector):void{
            var _local2:Object = this._1448659846numReflector;
            if (_local2 !== _arg1){
                this._1448659846numReflector = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "numReflector", _local2, _arg1));
            };
        }
        public function set displayNumberText(_arg1:Text):void{
            var _local2:Object = this._2129146488displayNumberText;
            if (_local2 !== _arg1){
                this._2129146488displayNumberText = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "displayNumberText", _local2, _arg1));
            };
        }
        public function get displayNumberText():Text{
            return (this._2129146488displayNumberText);
        }
        public function get numReflector():Reflector{
            return (this._1448659846numReflector);
        }
        public function set stimulus(_arg1:Canvas):void{
            var _local2:Object = this._1541255226stimulus;
            if (_local2 !== _arg1){
                this._1541255226stimulus = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "stimulus", _local2, _arg1));
            };
        }
        private function _CueReactivityPrompt_bindingExprs():void{
            var _local1:*;
            _local1 = COUNTDOWN;
            _local1 = model.currentStageWidth;
            _local1 = model.currentStageHeight;
            _local1 = String(currentDisplayNum);
            _local1 = displayNumberCanvas.width;
            _local1 = ((displayNumberText.y + displayNumberText.height) + 0.1);
            _local1 = STIMULI;
            _local1 = model.currentStageWidth;
            _local1 = model.currentStageHeight;
            _local1 = ((model.currentStageWidth / 2) - (contentImage.width / 2));
            _local1 = ((model.currentStageHeight / 2) - (contentImage.height / 2));
            _local1 = _presenterImageUrl;
            _local1 = content.Title;
        }

        public static function set watcherSetupUtil(_arg1:IWatcherSetupUtil):void{
            CueReactivityPrompt._watcherSetupUtil = _arg1;
        }

    }
}//package com.redbox.changetech.view.templates 
