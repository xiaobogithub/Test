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

    public class BalanceTertiaryButtonReflectionCanvas extends Canvas implements IBindingClient {

        mx_internal var _bindingsByDestination:Object
        public var _BalanceTertiaryButtonReflectionCanvas_Reflector1:Reflector
        mx_internal var _bindingsBeginWithWord:Object
        mx_internal var _watchers:Array
        private var _1082042285cta_btn:BalanceButton
        private var _buttonLabel:String
        mx_internal var _bindings:Array
        private var _buttonEnabled:Boolean = true
        private var _documentDescriptor_:UIComponentDescriptor
        private var _1422950858action:ButtonActionVO

        private static var _watcherSetupUtil:IWatcherSetupUtil;

        public function BalanceTertiaryButtonReflectionCanvas(){
            _documentDescriptor_ = new UIComponentDescriptor({type:Canvas, propertiesFactory:function ():Object{
                return ({childDescriptors:[new UIComponentDescriptor({type:BalanceButton, id:"cta_btn"}), new UIComponentDescriptor({type:Reflector, id:"_BalanceTertiaryButtonReflectionCanvas_Reflector1", propertiesFactory:function ():Object{
                    return ({falloff:0.6, alpha:0.3});
                }})]});
            }});
            _bindings = [];
            _watchers = [];
            _bindingsByDestination = {};
            _bindingsBeginWithWord = {};
            super();
            mx_internal::_document = this;
            this.verticalScrollPolicy = "off";
        }
        public function set action(_arg1:ButtonActionVO):void{
            var _local2:Object = this._1422950858action;
            if (_local2 !== _arg1){
                this._1422950858action = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "action", _local2, _arg1));
            };
        }
        public function set buttonLabel(_arg1:String):void{
            var _local2:Object = this.buttonLabel;
            if (_local2 !== _arg1){
                this._1777527070buttonLabel = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "buttonLabel", _local2, _arg1));
            };
        }
        override public function initialize():void{
            var target:* = null;
            var watcherSetupUtilClass:* = null;
            mx_internal::setDocumentDescriptor(_documentDescriptor_);
            var bindings:* = _BalanceTertiaryButtonReflectionCanvas_bindingsSetup();
            var watchers:* = [];
            target = this;
            if (_watcherSetupUtil == null){
                watcherSetupUtilClass = getDefinitionByName("_com_redbox_changetech_view_components_BalanceTertiaryButtonReflectionCanvasWatcherSetupUtil");
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
        private function set _352919633buttonEnabled(_arg1:Boolean):void{
            _buttonEnabled = _arg1;
        }
        private function _BalanceTertiaryButtonReflectionCanvas_bindingsSetup():Array{
            var binding:* = null;
            var result:* = [];
            binding = new Binding(this, function ():Boolean{
                return (buttonEnabled);
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
                var _local1:* = Button.TERTIARY;
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
                _BalanceTertiaryButtonReflectionCanvas_Reflector1.target = _arg1;
            }, "_BalanceTertiaryButtonReflectionCanvas_Reflector1.target");
            result[4] = binding;
            binding = new Binding(this, function ():Number{
                return (cta_btn.height);
            }, function (_arg1:Number):void{
                _BalanceTertiaryButtonReflectionCanvas_Reflector1.y = _arg1;
            }, "_BalanceTertiaryButtonReflectionCanvas_Reflector1.y");
            result[5] = binding;
            return (result);
        }
        private function set _1777527070buttonLabel(_arg1:String):void{
            _buttonLabel = _arg1;
        }
        public function set buttonEnabled(_arg1:Boolean):void{
            var _local2:Object = this.buttonEnabled;
            if (_local2 !== _arg1){
                this._352919633buttonEnabled = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "buttonEnabled", _local2, _arg1));
            };
        }
        public function set cta_btn(_arg1:BalanceButton):void{
            var _local2:Object = this._1082042285cta_btn;
            if (_local2 !== _arg1){
                this._1082042285cta_btn = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "cta_btn", _local2, _arg1));
            };
        }
        public function get buttonLabel():String{
            return (_buttonLabel);
        }
        public function get buttonEnabled():Boolean{
            return (_buttonEnabled);
        }
        private function _BalanceTertiaryButtonReflectionCanvas_bindingExprs():void{
            var _local1:*;
            _local1 = buttonEnabled;
            _local1 = buttonLabel;
            _local1 = Button.TERTIARY;
            _local1 = action;
            _local1 = cta_btn;
            _local1 = cta_btn.height;
        }
        public function get cta_btn():BalanceButton{
            return (this._1082042285cta_btn);
        }
        public function get action():ButtonActionVO{
            return (this._1422950858action);
        }

        public static function set watcherSetupUtil(_arg1:IWatcherSetupUtil):void{
            BalanceTertiaryButtonReflectionCanvas._watcherSetupUtil = _arg1;
        }

    }
}//package com.redbox.changetech.view.components 
