//Created by Action Script Viewer - http://www.buraks.com/asv
package com.redbox.changetech.view.components {
    import flash.display.*;
    import flash.geom.*;
    import flash.media.*;
    import flash.text.*;
    import mx.core.*;
    import flash.events.*;
    import mx.events.*;
    import mx.styles.*;
    import mx.states.*;
    import mx.binding.*;
    import mx.containers.*;
    import com.redbox.changetech.vo.*;
    import flash.utils.*;
    import flash.system.*;
    import flash.accessibility.*;
    import flash.xml.*;
    import flash.net.*;
    import com.rictus.reflector.*;
    import flash.filters.*;
    import flash.ui.*;
    import flash.external.*;
    import flash.debugger.*;
    import flash.errors.*;
    import flash.printing.*;
    import flash.profiler.*;

    public class BalanceButtonReflectionCanvas extends Canvas implements IBindingClient {

        private var _414727447cta_btn_without_relector:BalanceDualFunctionButton
        private var _1136348752_buttonEnabled:Boolean = true
        private var _1082042285cta_btn:BalanceDualFunctionButton
        mx_internal var _watchers:Array
        private var _buttonLabel:String
        public var _BalanceButtonReflectionCanvas_Reflector1:Reflector
        private var _1422950858action:ButtonActionVO
        mx_internal var _bindingsByDestination:Object
        mx_internal var _bindingsBeginWithWord:Object
        mx_internal var _bindings:Array
        public var reflectionIsOn:Boolean = true
        private var _documentDescriptor_:UIComponentDescriptor

        private static var _watcherSetupUtil:IWatcherSetupUtil;

        public function BalanceButtonReflectionCanvas(){
            _documentDescriptor_ = new UIComponentDescriptor({type:Canvas});
            _bindings = [];
            _watchers = [];
            _bindingsByDestination = {};
            _bindingsBeginWithWord = {};
            super();
            mx_internal::_document = this;
            this.verticalScrollPolicy = "off";
            this.states = [_BalanceButtonReflectionCanvas_State1_c(), _BalanceButtonReflectionCanvas_State2_c()];
            this.addEventListener("creationComplete", ___BalanceButtonReflectionCanvas_Canvas1_creationComplete);
        }
        private function _BalanceButtonReflectionCanvas_BalanceDualFunctionButton1_i():BalanceDualFunctionButton{
            var _local1:BalanceDualFunctionButton = new BalanceDualFunctionButton();
            cta_btn = _local1;
            _local1.id = "cta_btn";
            BindingManager.executeBindings(this, "cta_btn", cta_btn);
            if (!_local1.document){
                _local1.document = this;
            };
            return (_local1);
        }
        override public function initialize():void{
            var target:* = null;
            var watcherSetupUtilClass:* = null;
            mx_internal::setDocumentDescriptor(_documentDescriptor_);
            var bindings:* = _BalanceButtonReflectionCanvas_bindingsSetup();
            var watchers:* = [];
            target = this;
            if (_watcherSetupUtil == null){
                watcherSetupUtilClass = getDefinitionByName("_com_redbox_changetech_view_components_BalanceButtonReflectionCanvasWatcherSetupUtil");
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
        private function _BalanceButtonReflectionCanvas_State2_c():State{
            var _local1:State = new State();
            _local1.name = "reflectionOff";
            _local1.overrides = [_BalanceButtonReflectionCanvas_AddChild3_c()];
            return (_local1);
        }
        private function init():void{
            if (reflectionIsOn){
                currentState = "defaultState";
            } else {
                currentState = "reflectionOff";
            };
        }
        private function _BalanceButtonReflectionCanvas_bindingsSetup():Array{
            var binding:* = null;
            var result:* = [];
            binding = new Binding(this, function ():Boolean{
                return (_buttonEnabled);
            }, function (_arg1:Boolean):void{
                cta_btn.enabled = _arg1;
            }, "cta_btn.enabled");
            result[0] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = buttonLabel;
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                cta_btn.label = _arg1;
            }, "cta_btn.label");
            result[1] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = Button.PRIMARY;
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                cta_btn.buttonType = _arg1;
            }, "cta_btn.buttonType");
            result[2] = binding;
            binding = new Binding(this, function ():ButtonActionVO{
                return (action);
            }, function (_arg1:ButtonActionVO):void{
                cta_btn.action = _arg1;
            }, "cta_btn.action");
            result[3] = binding;
            binding = new Binding(this, function ():UIComponent{
                return (cta_btn);
            }, function (_arg1:UIComponent):void{
                _BalanceButtonReflectionCanvas_Reflector1.target = _arg1;
            }, "_BalanceButtonReflectionCanvas_Reflector1.target");
            result[4] = binding;
            binding = new Binding(this, function ():Number{
                return (cta_btn.height);
            }, function (_arg1:Number):void{
                _BalanceButtonReflectionCanvas_Reflector1.y = _arg1;
            }, "_BalanceButtonReflectionCanvas_Reflector1.y");
            result[5] = binding;
            binding = new Binding(this, function ():Boolean{
                return (_buttonEnabled);
            }, function (_arg1:Boolean):void{
                cta_btn_without_relector.enabled = _arg1;
            }, "cta_btn_without_relector.enabled");
            result[6] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = buttonLabel;
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                cta_btn_without_relector.label = _arg1;
            }, "cta_btn_without_relector.label");
            result[7] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = Button.PRIMARY;
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                cta_btn_without_relector.buttonType = _arg1;
            }, "cta_btn_without_relector.buttonType");
            result[8] = binding;
            binding = new Binding(this, function ():ButtonActionVO{
                return (action);
            }, function (_arg1:ButtonActionVO):void{
                cta_btn_without_relector.action = _arg1;
            }, "cta_btn_without_relector.action");
            result[9] = binding;
            return (result);
        }
        private function _BalanceButtonReflectionCanvas_bindingExprs():void{
            var _local1:*;
            _local1 = _buttonEnabled;
            _local1 = buttonLabel;
            _local1 = Button.PRIMARY;
            _local1 = action;
            _local1 = cta_btn;
            _local1 = cta_btn.height;
            _local1 = _buttonEnabled;
            _local1 = buttonLabel;
            _local1 = Button.PRIMARY;
            _local1 = action;
        }
        private function _BalanceButtonReflectionCanvas_Reflector1_i():Reflector{
            var _local1:Reflector = new Reflector();
            _BalanceButtonReflectionCanvas_Reflector1 = _local1;
            _local1.falloff = 0.6;
            _local1.alpha = 0.3;
            _local1.id = "_BalanceButtonReflectionCanvas_Reflector1";
            BindingManager.executeBindings(this, "_BalanceButtonReflectionCanvas_Reflector1", _BalanceButtonReflectionCanvas_Reflector1);
            if (!_local1.document){
                _local1.document = this;
            };
            return (_local1);
        }
        public function ___BalanceButtonReflectionCanvas_Canvas1_creationComplete(_arg1:FlexEvent):void{
            init();
        }
        public function get cta_btn():BalanceDualFunctionButton{
            return (this._1082042285cta_btn);
        }
        private function _BalanceButtonReflectionCanvas_AddChild3_c():AddChild{
            var _local1:AddChild = new AddChild();
            _local1.targetFactory = new DeferredInstanceFromFunction(_BalanceButtonReflectionCanvas_BalanceDualFunctionButton2_i);
            return (_local1);
        }
        private function get _buttonEnabled():Boolean{
            return (this._1136348752_buttonEnabled);
        }
        private function _BalanceButtonReflectionCanvas_AddChild1_c():AddChild{
            var _local1:AddChild = new AddChild();
            _local1.targetFactory = new DeferredInstanceFromFunction(_BalanceButtonReflectionCanvas_BalanceDualFunctionButton1_i);
            return (_local1);
        }
        public function get buttonEnabled():Boolean{
            return (_buttonEnabled);
        }
        public function get action():ButtonActionVO{
            return (this._1422950858action);
        }
        public function set buttonLabel(_arg1:String):void{
            var _local2:Object = this.buttonLabel;
            if (_local2 !== _arg1){
                this._1777527070buttonLabel = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "buttonLabel", _local2, _arg1));
            };
        }
        private function _BalanceButtonReflectionCanvas_BalanceDualFunctionButton2_i():BalanceDualFunctionButton{
            var _local1:BalanceDualFunctionButton = new BalanceDualFunctionButton();
            cta_btn_without_relector = _local1;
            _local1.id = "cta_btn_without_relector";
            BindingManager.executeBindings(this, "cta_btn_without_relector", cta_btn_without_relector);
            if (!_local1.document){
                _local1.document = this;
            };
            return (_local1);
        }
        private function _BalanceButtonReflectionCanvas_State1_c():State{
            var _local1:State = new State();
            _local1.name = "defaultState";
            _local1.overrides = [_BalanceButtonReflectionCanvas_AddChild1_c(), _BalanceButtonReflectionCanvas_AddChild2_c()];
            return (_local1);
        }
        public function set cta_btn_without_relector(_arg1:BalanceDualFunctionButton):void{
            var _local2:Object = this._414727447cta_btn_without_relector;
            if (_local2 !== _arg1){
                this._414727447cta_btn_without_relector = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "cta_btn_without_relector", _local2, _arg1));
            };
        }
        private function set _1777527070buttonLabel(_arg1:String):void{
            _buttonLabel = _arg1;
        }
        public function set buttonEnabled(_arg1:Boolean):void{
            _buttonEnabled = _arg1;
        }
        private function set _buttonEnabled(_arg1:Boolean):void{
            var _local2:Object = this._1136348752_buttonEnabled;
            if (_local2 !== _arg1){
                this._1136348752_buttonEnabled = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "_buttonEnabled", _local2, _arg1));
            };
        }
        public function set cta_btn(_arg1:BalanceDualFunctionButton):void{
            var _local2:Object = this._1082042285cta_btn;
            if (_local2 !== _arg1){
                this._1082042285cta_btn = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "cta_btn", _local2, _arg1));
            };
        }
        public function get buttonLabel():String{
            return (_buttonLabel);
        }
        public function get cta_btn_without_relector():BalanceDualFunctionButton{
            return (this._414727447cta_btn_without_relector);
        }
        public function set action(_arg1:ButtonActionVO):void{
            var _local2:Object = this._1422950858action;
            if (_local2 !== _arg1){
                this._1422950858action = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "action", _local2, _arg1));
            };
        }
        private function _BalanceButtonReflectionCanvas_AddChild2_c():AddChild{
            var _local1:AddChild = new AddChild();
            _local1.targetFactory = new DeferredInstanceFromFunction(_BalanceButtonReflectionCanvas_Reflector1_i);
            return (_local1);
        }

        public static function set watcherSetupUtil(_arg1:IWatcherSetupUtil):void{
            BalanceButtonReflectionCanvas._watcherSetupUtil = _arg1;
        }

    }
}//package com.redbox.changetech.view.components 
