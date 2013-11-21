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
    import mx.binding.*;
    import mx.containers.*;
    import com.redbox.changetech.view.components.*;
    import com.redbox.changetech.vo.*;
    import flash.utils.*;
    import flash.system.*;
    import flash.accessibility.*;
    import flash.xml.*;
    import flash.net.*;
    import flash.filters.*;
    import flash.ui.*;
    import flash.external.*;
    import flash.debugger.*;
    import flash.errors.*;
    import flash.printing.*;
    import flash.profiler.*;

    public class PersonalValue extends ModuleViewTemplate implements IBindingClient {

        mx_internal var _bindingsByDestination:Object
        private var _1549852825transContainer2:Canvas
        mx_internal var _bindingsBeginWithWord:Object
        private var _1082042285cta_btn:BalanceButtonReflectionCanvas
        private var _1298893550cta_container:HBox
        private var _3237038info:VBox
        mx_internal var _watchers:Array
        private var _1549852824transContainer1:Canvas
        mx_internal var _bindings:Array
        private var _documentDescriptor_:UIComponentDescriptor
        private var _1939189620copyContainer:BalanceValueReflectionCanvas

        private static var _watcherSetupUtil:IWatcherSetupUtil;

        public function PersonalValue(){
            _documentDescriptor_ = new UIComponentDescriptor({type:ModuleViewTemplate, propertiesFactory:function ():Object{
                return ({childDescriptors:[new UIComponentDescriptor({type:Canvas, id:"transContainer1", propertiesFactory:function ():Object{
                    return ({verticalScrollPolicy:"off", childDescriptors:[new UIComponentDescriptor({type:VBox, id:"info", stylesFactory:function ():void{
                        this.horizontalAlign = "center";
                    }, propertiesFactory:function ():Object{
                        return ({width:383, verticalScrollPolicy:"off", clipContent:false, childDescriptors:[new UIComponentDescriptor({type:BalanceValueReflectionCanvas, id:"copyContainer"}), new UIComponentDescriptor({type:HBox, id:"cta_container", stylesFactory:function ():void{
                            this.horizontalAlign = "right";
                        }, propertiesFactory:function ():Object{
                            return ({width:440, childDescriptors:[new UIComponentDescriptor({type:BalanceButtonReflectionCanvas, id:"cta_btn"})]});
                        }})]});
                    }})]});
                }}), new UIComponentDescriptor({type:Canvas, id:"transContainer2", propertiesFactory:function ():Object{
                    return ({verticalScrollPolicy:"off"});
                }})]});
            }});
            _bindings = [];
            _watchers = [];
            _bindingsByDestination = {};
            _bindingsBeginWithWord = {};
            super();
            mx_internal::_document = this;
            this.addEventListener("creationComplete", ___PersonalValue_ModuleViewTemplate1_creationComplete);
        }
        private function _PersonalValue_bindingExprs():void{
            var _local1:*;
            _local1 = model.currentStageWidth;
            _local1 = model.currentStageHeight;
            _local1 = ((model.currentStageWidth / 2) - 225);
            _local1 = Math.max(((650 - (info.height / 2)) / 4), 0);
            _local1 = (((copyContainer.height / 2) + 20) * -1);
            _local1 = _content;
            _local1 = content.getCTAButton().Label;
            _local1 = content.getCTAButton().ButtonAction;
            _local1 = (content.getTertiaryButton() == null);
            _local1 = !((content.getCTAButton() == null));
            _local1 = model.currentStageWidth;
            _local1 = model.currentStageHeight;
        }
        private function _PersonalValue_bindingsSetup():Array{
            var binding:* = null;
            var result:* = [];
            binding = new Binding(this, function ():Number{
                return (model.currentStageWidth);
            }, function (_arg1:Number):void{
                transContainer1.width = _arg1;
            }, "transContainer1.width");
            result[0] = binding;
            binding = new Binding(this, function ():Number{
                return (model.currentStageHeight);
            }, function (_arg1:Number):void{
                transContainer1.height = _arg1;
            }, "transContainer1.height");
            result[1] = binding;
            binding = new Binding(this, function ():Number{
                return (((model.currentStageWidth / 2) - 225));
            }, function (_arg1:Number):void{
                info.x = _arg1;
            }, "info.x");
            result[2] = binding;
            binding = new Binding(this, function ():Number{
                return (Math.max(((650 - (info.height / 2)) / 4), 0));
            }, function (_arg1:Number):void{
                info.y = _arg1;
            }, "info.y");
            result[3] = binding;
            binding = new Binding(this, function ():Number{
                return ((((copyContainer.height / 2) + 20) * -1));
            }, function (_arg1:Number):void{
                info.setStyle("verticalGap", _arg1);
            }, "info.verticalGap");
            result[4] = binding;
            binding = new Binding(this, function ():Content{
                return (_content);
            }, function (_arg1:Content):void{
                copyContainer.content = _arg1;
            }, "copyContainer.content");
            result[5] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = content.getCTAButton().Label;
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                cta_btn.buttonLabel = _arg1;
            }, "cta_btn.buttonLabel");
            result[6] = binding;
            binding = new Binding(this, function ():ButtonActionVO{
                return (content.getCTAButton().ButtonAction);
            }, function (_arg1:ButtonActionVO):void{
                cta_btn.action = _arg1;
            }, "cta_btn.action");
            result[7] = binding;
            binding = new Binding(this, function ():Boolean{
                return ((content.getTertiaryButton() == null));
            }, function (_arg1:Boolean):void{
                cta_btn.reflectionIsOn = _arg1;
            }, "cta_btn.reflectionIsOn");
            result[8] = binding;
            binding = new Binding(this, function ():Boolean{
                return (!((content.getCTAButton() == null)));
            }, function (_arg1:Boolean):void{
                cta_btn.visible = _arg1;
            }, "cta_btn.visible");
            result[9] = binding;
            binding = new Binding(this, function ():Number{
                return (model.currentStageWidth);
            }, function (_arg1:Number):void{
                transContainer2.width = _arg1;
            }, "transContainer2.width");
            result[10] = binding;
            binding = new Binding(this, function ():Number{
                return (model.currentStageHeight);
            }, function (_arg1:Number):void{
                transContainer2.height = _arg1;
            }, "transContainer2.height");
            result[11] = binding;
            return (result);
        }
        override public function initialize():void{
            var target:* = null;
            var watcherSetupUtilClass:* = null;
            mx_internal::setDocumentDescriptor(_documentDescriptor_);
            var bindings:* = _PersonalValue_bindingsSetup();
            var watchers:* = [];
            target = this;
            if (_watcherSetupUtil == null){
                watcherSetupUtilClass = getDefinitionByName("_com_redbox_changetech_view_templates_PersonalValueWatcherSetupUtil");
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
        public function get cta_container():HBox{
            return (this._1298893550cta_container);
        }
        public function get copyContainer():BalanceValueReflectionCanvas{
            return (this._1939189620copyContainer);
        }
        public function get transContainer2():Canvas{
            return (this._1549852825transContainer2);
        }
        public function set info(_arg1:VBox):void{
            var _local2:Object = this._3237038info;
            if (_local2 !== _arg1){
                this._3237038info = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "info", _local2, _arg1));
            };
        }
        private function init(_arg1:FlexEvent):void{
        }
        public function set cta_container(_arg1:HBox):void{
            var _local2:Object = this._1298893550cta_container;
            if (_local2 !== _arg1){
                this._1298893550cta_container = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "cta_container", _local2, _arg1));
            };
        }
        public function set copyContainer(_arg1:BalanceValueReflectionCanvas):void{
            var _local2:Object = this._1939189620copyContainer;
            if (_local2 !== _arg1){
                this._1939189620copyContainer = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "copyContainer", _local2, _arg1));
            };
        }
        public function set cta_btn(_arg1:BalanceButtonReflectionCanvas):void{
            var _local2:Object = this._1082042285cta_btn;
            if (_local2 !== _arg1){
                this._1082042285cta_btn = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "cta_btn", _local2, _arg1));
            };
        }
        public function get info():VBox{
            return (this._3237038info);
        }
        public function get transContainer1():Canvas{
            return (this._1549852824transContainer1);
        }
        public function set transContainer2(_arg1:Canvas):void{
            var _local2:Object = this._1549852825transContainer2;
            if (_local2 !== _arg1){
                this._1549852825transContainer2 = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "transContainer2", _local2, _arg1));
            };
        }
        public function get cta_btn():BalanceButtonReflectionCanvas{
            return (this._1082042285cta_btn);
        }
        public function set transContainer1(_arg1:Canvas):void{
            var _local2:Object = this._1549852824transContainer1;
            if (_local2 !== _arg1){
                this._1549852824transContainer1 = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "transContainer1", _local2, _arg1));
            };
        }
        public function ___PersonalValue_ModuleViewTemplate1_creationComplete(_arg1:FlexEvent):void{
            init(_arg1);
        }

        public static function set watcherSetupUtil(_arg1:IWatcherSetupUtil):void{
            PersonalValue._watcherSetupUtil = _arg1;
        }

    }
}//package com.redbox.changetech.view.templates 
