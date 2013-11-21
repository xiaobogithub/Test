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
    import mx.controls.*;
    import mx.effects.*;
    import mx.binding.*;
    import com.redbox.changetech.control.*;
    import mx.containers.*;
    import com.redbox.changetech.vo.*;
    import flash.utils.*;
    import com.adobe.cairngorm.control.*;
    import flash.system.*;
    import mx.modules.*;
    import flash.accessibility.*;
    import flash.xml.*;
    import flash.net.*;
    import com.redbox.changetech.view.modules.*;
    import flash.filters.*;
    import flash.ui.*;
    import com.redbox.changetech.util.*;
    import mx.effects.easing.*;
    import flash.external.*;
    import flash.debugger.*;
    import flash.errors.*;
    import flash.printing.*;
    import flash.profiler.*;

    public class BalanceRoom extends HBox implements IBindingClient {

        private var _contentCollection:ContentCollection
        private var _module:IBalanceModule
        private var _795452954roomModuleLoader:ModuleLoader
        mx_internal var _watchers:Array
        private var _173503994roomName:String
        mx_internal var _bindingsByDestination:Object
        mx_internal var _bindingsBeginWithWord:Object
        private var _404469597fadeOutContainer2:Fade
        private var _1253964260_moduleUrl:String
        mx_internal var _bindings:Array
        private var _404469598fadeOutContainer1:Fade
        private var _documentDescriptor_:UIComponentDescriptor

        private static var _watcherSetupUtil:IWatcherSetupUtil;

        public function BalanceRoom(){
            _documentDescriptor_ = new UIComponentDescriptor({type:HBox, propertiesFactory:function ():Object{
                return ({childDescriptors:[new UIComponentDescriptor({type:ModuleLoader, id:"roomModuleLoader", events:{loading:"__roomModuleLoader_loading", ready:"__roomModuleLoader_ready"}, stylesFactory:function ():void{
                    this.horizontalAlign = "center";
                    this.verticalAlign = "top";
                }})]});
            }});
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
            };
            this.percentWidth = 100;
            this.percentHeight = 100;
            this.horizontalScrollPolicy = "off";
            this.verticalScrollPolicy = "off";
            _BalanceRoom_Fade1_i();
            _BalanceRoom_Fade2_i();
            this.addEventListener("creationComplete", ___BalanceRoom_HBox1_creationComplete);
        }
        private function _BalanceRoom_bindingExprs():void{
            var _local1:*;
            _local1 = _moduleUrl;
            _local1 = AbstractBalanceModule(module).transitionContainer1;
            _local1 = Linear.easeInOut;
            _local1 = AbstractBalanceModule(module).transitionContainer2;
            _local1 = Linear.easeInOut;
        }
        public function get roomName():String{
            return (this._173503994roomName);
        }
        private function _BalanceRoom_bindingsSetup():Array{
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
            binding = new Binding(this, function ():Object{
                return (AbstractBalanceModule(module).transitionContainer1);
            }, function (_arg1:Object):void{
                fadeOutContainer1.target = _arg1;
            }, "fadeOutContainer1.target");
            result[1] = binding;
            binding = new Binding(this, function ():Function{
                return (Linear.easeInOut);
            }, function (_arg1:Function):void{
                fadeOutContainer1.easingFunction = _arg1;
            }, "fadeOutContainer1.easingFunction");
            result[2] = binding;
            binding = new Binding(this, function ():Object{
                return (AbstractBalanceModule(module).transitionContainer2);
            }, function (_arg1:Object):void{
                fadeOutContainer2.target = _arg1;
            }, "fadeOutContainer2.target");
            result[3] = binding;
            binding = new Binding(this, function ():Function{
                return (Linear.easeInOut);
            }, function (_arg1:Function):void{
                fadeOutContainer2.easingFunction = _arg1;
            }, "fadeOutContainer2.easingFunction");
            result[4] = binding;
            return (result);
        }
        public function set roomName(_arg1:String):void{
            var _local2:Object = this._173503994roomName;
            if (_local2 !== _arg1){
                this._173503994roomName = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "roomName", _local2, _arg1));
            };
        }
        private function set _1068784020module(_arg1:IBalanceModule):void{
            _module = _arg1;
        }
        override public function initialize():void{
            var target:* = null;
            var watcherSetupUtilClass:* = null;
            mx_internal::setDocumentDescriptor(_documentDescriptor_);
            var bindings:* = _BalanceRoom_bindingsSetup();
            var watchers:* = [];
            target = this;
            if (_watcherSetupUtil == null){
                watcherSetupUtilClass = getDefinitionByName("_com_redbox_changetech_view_components_BalanceRoomWatcherSetupUtil");
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
        public function get roomModuleLoader():ModuleLoader{
            return (this._795452954roomModuleLoader);
        }
        private function set _479839241contentCollection(_arg1:ContentCollection):void{
            trace("setting content collection");
            if (((!((contentCollection == null))) && ((contentCollection.Type == _arg1.Type)))){
                trace("&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&");
                _contentCollection = _arg1;
                navigateToRoom();
                setModuleContent();
            } else {
                if (_contentCollection == null){
                    trace("2");
                    _contentCollection = _arg1;
                    _moduleUrl = Config.MODULE_SWFS[_contentCollection.Type];
                } else {
                    trace("3");
                    _contentCollection = _arg1;
                    initModuleOutroSequence();
                };
            };
        }
        public function get fadeOutContainer1():Fade{
            return (this._404469598fadeOutContainer1);
        }
        public function get fadeOutContainer2():Fade{
            return (this._404469597fadeOutContainer2);
        }
        public function __roomModuleLoader_ready(_arg1:ModuleEvent):void{
            readyHandler(_arg1);
        }
        private function readyHandler(_arg1:Event):void{
            module = (roomModuleLoader.child as IBalanceModule);
            setModuleContent();
            var _local2:CairngormEvent = new CairngormEvent(BalanceController.HIDE_LOADER);
            _local2.dispatch();
            roomModuleLoader.child.addEventListener(FlexEvent.CREATION_COMPLETE, moduleCreationComplete);
        }
        private function setModuleContent():void{
            if (roomModuleLoader.child != null){
                module.contentCollection = contentCollection;
            } else {
                Alert.show(("Error: unable to load module: " + _moduleUrl));
            };
        }
        private function initModuleOutroSequence():void{
            fadeOutContainer1.addEventListener(EffectEvent.EFFECT_END, exitModuleOutroSequence);
            trace("test");
            if (BasicModule(module).forceCollectionComplete){
                _moduleUrl = Config.MODULE_SWFS[_contentCollection.Type];
            } else {
                fadeOutContainer1.play();
                fadeOutContainer2.play();
            };
        }
        public function set roomModuleLoader(_arg1:ModuleLoader):void{
            var _local2:Object = this._795452954roomModuleLoader;
            if (_local2 !== _arg1){
                this._795452954roomModuleLoader = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "roomModuleLoader", _local2, _arg1));
            };
        }
        private function _BalanceRoom_Fade2_i():Fade{
            var _local1:Fade = new Fade();
            fadeOutContainer2 = _local1;
            _local1.alphaFrom = 1;
            _local1.alphaTo = 0;
            _local1.duration = 500;
            BindingManager.executeBindings(this, "fadeOutContainer2", fadeOutContainer2);
            return (_local1);
        }
        private function moduleCreationComplete(_arg1:FlexEvent):void{
            roomModuleLoader.child.removeEventListener(FlexEvent.CREATION_COMPLETE, moduleCreationComplete);
            dispatchEvent(new Event("balanceRoomReady"));
            callLater(navigateToRoom);
        }
        public function set fadeOutContainer2(_arg1:Fade):void{
            var _local2:Object = this._404469597fadeOutContainer2;
            if (_local2 !== _arg1){
                this._404469597fadeOutContainer2 = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "fadeOutContainer2", _local2, _arg1));
            };
        }
        private function navigateToRoom():void{
            var _local1:CairngormEvent = new CairngormEvent(BalanceController.NAVIGATE_TO_ROOM);
            _local1.data = contentCollection.Type;
            _local1.dispatch();
        }
        private function set _moduleUrl(_arg1:String):void{
            var _local2:Object = this._1253964260_moduleUrl;
            if (_local2 !== _arg1){
                this._1253964260_moduleUrl = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "_moduleUrl", _local2, _arg1));
            };
        }
        public function set fadeOutContainer1(_arg1:Fade):void{
            var _local2:Object = this._404469598fadeOutContainer1;
            if (_local2 !== _arg1){
                this._404469598fadeOutContainer1 = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "fadeOutContainer1", _local2, _arg1));
            };
        }
        public function set module(_arg1:IBalanceModule):void{
            var _local2:Object = this.module;
            if (_local2 !== _arg1){
                this._1068784020module = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "module", _local2, _arg1));
            };
        }
        private function exitModuleOutroSequence(_arg1:EffectEvent):void{
            fadeOutContainer1.removeEventListener(EffectEvent.EFFECT_END, exitModuleOutroSequence);
            _moduleUrl = Config.MODULE_SWFS[_contentCollection.Type];
        }
        private function get _moduleUrl():String{
            return (this._1253964260_moduleUrl);
        }
        public function get module():IBalanceModule{
            return (_module);
        }
        private function loadingHandler(_arg1:Event):void{
            var _local2:CairngormEvent = new CairngormEvent(BalanceController.SHOW_LOADER);
            _local2.data = roomModuleLoader;
            _local2.dispatch();
        }
        public function ___BalanceRoom_HBox1_creationComplete(_arg1:FlexEvent):void{
            creationCompleteHandler();
        }
        private function creationCompleteHandler():void{
            roomModuleLoader.addEventListener("moduleReady", readyHandler);
        }
        public function set contentCollection(_arg1:ContentCollection):void{
            var _local2:Object = this.contentCollection;
            if (_local2 !== _arg1){
                this._479839241contentCollection = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "contentCollection", _local2, _arg1));
            };
        }
        public function __roomModuleLoader_loading(_arg1:Event):void{
            loadingHandler(_arg1);
        }
        private function _BalanceRoom_Fade1_i():Fade{
            var _local1:Fade = new Fade();
            fadeOutContainer1 = _local1;
            _local1.alphaFrom = 1;
            _local1.alphaTo = 0;
            _local1.duration = 500;
            BindingManager.executeBindings(this, "fadeOutContainer1", fadeOutContainer1);
            return (_local1);
        }
        public function get contentCollection():ContentCollection{
            return (_contentCollection);
        }

        public static function set watcherSetupUtil(_arg1:IWatcherSetupUtil):void{
            BalanceRoom._watcherSetupUtil = _arg1;
        }

    }
}//package com.redbox.changetech.view.components 
