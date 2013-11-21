//Created by Action Script Viewer - http://www.buraks.com/asv
package com.redbox.changetech.view.modules.popup_modules {
    import flash.display.*;
    import flash.geom.*;
    import flash.media.*;
    import flash.text.*;
    import mx.core.*;
    import flash.events.*;
    import mx.events.*;
    import mx.styles.*;
    import mx.controls.*;
    import mx.binding.*;
    import com.redbox.changetech.control.*;
    import mx.containers.*;
    import com.redbox.changetech.model.*;
    import flash.utils.*;
    import com.adobe.cairngorm.control.*;
    import flash.system.*;
    import mx.modules.*;
    import flash.accessibility.*;
    import flash.xml.*;
    import flash.net.*;
    import com.redbox.changetech.event.*;
    import flash.filters.*;
    import flash.ui.*;
    import flash.external.*;
    import flash.debugger.*;
    import flash.errors.*;
    import flash.printing.*;
    import flash.profiler.*;

    public class PopUpModuleLoader extends Canvas implements IBindingClient {

        mx_internal var _bindingsByDestination:Object
        mx_internal var _bindingsBeginWithWord:Object
        private var _795452954roomModuleLoader:ModuleLoader
        mx_internal var _watchers:Array
        private var _1253964260_moduleUrl:String
        private var model:BalanceModelLocator
        mx_internal var _bindings:Array
        private var _documentDescriptor_:UIComponentDescriptor
        private var _312699062closeButton:Button

        private static var _watcherSetupUtil:IWatcherSetupUtil;

        public function PopUpModuleLoader(){
            _documentDescriptor_ = new UIComponentDescriptor({type:Canvas, propertiesFactory:function ():Object{
                return ({width:500, height:400, childDescriptors:[new UIComponentDescriptor({type:ModuleLoader, id:"roomModuleLoader", events:{loading:"__roomModuleLoader_loading", ready:"__roomModuleLoader_ready"}, stylesFactory:function ():void{
                    this.horizontalAlign = "center";
                    this.verticalAlign = "top";
                }}), new UIComponentDescriptor({type:Button, id:"closeButton", events:{click:"__closeButton_click"}, propertiesFactory:function ():Object{
                    return ({styleName:"closeButton", y:8});
                }})]});
            }});
            model = BalanceModelLocator.getInstance();
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
                this.backgroundAlpha = 0;
                this.backgroundColor = 0xFFFFFF;
            };
            this.width = 500;
            this.height = 400;
            this.horizontalScrollPolicy = "off";
            this.verticalScrollPolicy = "off";
        }
        public function set moduleUrl(_arg1:String):void{
            _moduleUrl = _arg1;
        }
        private function set _moduleUrl(_arg1:String):void{
            var _local2:Object = this._1253964260_moduleUrl;
            if (_local2 !== _arg1){
                this._1253964260_moduleUrl = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "_moduleUrl", _local2, _arg1));
            };
        }
        private function _PopUpModuleLoader_bindingExprs():void{
            var _local1:*;
            _local1 = _moduleUrl;
            _local1 = ((this.width - closeButton.width) - 3);
        }
        override public function initialize():void{
            var target:* = null;
            var watcherSetupUtilClass:* = null;
            mx_internal::setDocumentDescriptor(_documentDescriptor_);
            var bindings:* = _PopUpModuleLoader_bindingsSetup();
            var watchers:* = [];
            target = this;
            if (_watcherSetupUtil == null){
                watcherSetupUtilClass = getDefinitionByName("_com_redbox_changetech_view_modules_popup_modules_PopUpModuleLoaderWatcherSetupUtil");
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
        public function get closeButton():Button{
            return (this._312699062closeButton);
        }
        public function set closeButton(_arg1:Button):void{
            var _local2:Object = this._312699062closeButton;
            if (_local2 !== _arg1){
                this._312699062closeButton = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "closeButton", _local2, _arg1));
            };
        }
        public function __roomModuleLoader_ready(_arg1:ModuleEvent):void{
            readyHandler(_arg1);
        }
        public function get roomModuleLoader():ModuleLoader{
            return (this._795452954roomModuleLoader);
        }
        public function get moduleUrl():String{
            return (_moduleUrl);
        }
        private function get _moduleUrl():String{
            return (this._1253964260_moduleUrl);
        }
        private function readyHandler(_arg1:Event):void{
            var _local2:CairngormEvent = new CairngormEvent(BalanceController.HIDE_LOADER);
            _local2.dispatch();
        }
        private function closeMe():void{
            trace("dispatching bubbling event");
            dispatchEvent(new PopupSelectedEvent(PopupSelectedEvent.POP_UP_CLOSE, true, false, null));
        }
        public function set roomModuleLoader(_arg1:ModuleLoader):void{
            var _local2:Object = this._795452954roomModuleLoader;
            if (_local2 !== _arg1){
                this._795452954roomModuleLoader = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "roomModuleLoader", _local2, _arg1));
            };
        }
        private function loadingHandler(_arg1:Event):void{
        }
        public function __roomModuleLoader_loading(_arg1:Event):void{
            loadingHandler(_arg1);
        }
        public function __closeButton_click(_arg1:MouseEvent):void{
            closeMe();
        }
        private function _PopUpModuleLoader_bindingsSetup():Array{
            var binding:* = null;
            var result:* = [];
            binding = new Binding(this, function ():String{
                var _local1:* = _moduleUrl;
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                roomModuleLoader.url = _arg1;
            }, "roomModuleLoader.url");
            result[0] = binding;
            binding = new Binding(this, function ():Number{
                return (((this.width - closeButton.width) - 3));
            }, function (_arg1:Number):void{
                closeButton.x = _arg1;
            }, "closeButton.x");
            result[1] = binding;
            return (result);
        }

        public static function set watcherSetupUtil(_arg1:IWatcherSetupUtil):void{
            PopUpModuleLoader._watcherSetupUtil = _arg1;
        }

    }
}//package com.redbox.changetech.view.modules.popup_modules 
