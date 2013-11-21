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
    import com.redbox.changetech.view.modules.*;
    import flash.filters.*;
    import flash.ui.*;
    import flash.external.*;
    import flash.debugger.*;
    import flash.errors.*;
    import flash.printing.*;
    import flash.profiler.*;

    public class PicRightInfoText extends ModuleViewTemplate implements IBindingClient {

        private var _811296866contentImage:BalanceImageReflectionCanvas
        private var _1082042285cta_btn:BalanceButtonReflectionCanvas
        private var _1298893550cta_container:HBox
        private var _1538893297aux_btn2:BalanceTertiaryButtonReflectionCanvas
        mx_internal var _watchers:Array
        private var _3237038info:VBox
        private var _643094943aux_btn:BalanceSecondaryButtonReflectionCanvas
        mx_internal var _bindingsByDestination:Object
        private var _1549852825transContainer2:Canvas
        mx_internal var _bindingsBeginWithWord:Object
        private var _1549852824transContainer1:Canvas
        mx_internal var _bindings:Array
        private var _documentDescriptor_:UIComponentDescriptor
        public var _PicRightInfoText_HBox2:HBox
        private var _1939189620copyContainer:BalanceContentReflectionCanvas

        private static var _watcherSetupUtil:IWatcherSetupUtil;

        public function PicRightInfoText(){
            _documentDescriptor_ = new UIComponentDescriptor({type:ModuleViewTemplate, propertiesFactory:function ():Object{
                return ({childDescriptors:[new UIComponentDescriptor({type:Canvas, id:"transContainer1", propertiesFactory:function ():Object{
                    return ({verticalScrollPolicy:"off", childDescriptors:[new UIComponentDescriptor({type:VBox, id:"info", stylesFactory:function ():void{
                        this.horizontalAlign = "left";
                    }, propertiesFactory:function ():Object{
                        return ({width:450, verticalScrollPolicy:"off", clipContent:false, childDescriptors:[new UIComponentDescriptor({type:BalanceContentReflectionCanvas, id:"copyContainer"}), new UIComponentDescriptor({type:HBox, id:"cta_container", stylesFactory:function ():void{
                            this.horizontalAlign = "right";
                        }, propertiesFactory:function ():Object{
                            return ({width:440, childDescriptors:[new UIComponentDescriptor({type:BalanceSecondaryButtonReflectionCanvas, id:"aux_btn"}), new UIComponentDescriptor({type:VBox, stylesFactory:function ():void{
                                this.verticalAlign = "top";
                            }, propertiesFactory:function ():Object{
                                return ({childDescriptors:[new UIComponentDescriptor({type:BalanceButtonReflectionCanvas, id:"cta_btn"}), new UIComponentDescriptor({type:BalanceTertiaryButtonReflectionCanvas, id:"aux_btn2"})]});
                            }})]});
                        }})]});
                    }})]});
                }}), new UIComponentDescriptor({type:Canvas, id:"transContainer2", propertiesFactory:function ():Object{
                    return ({verticalScrollPolicy:"off", childDescriptors:[new UIComponentDescriptor({type:HBox, id:"_PicRightInfoText_HBox2", propertiesFactory:function ():Object{
                        return ({childDescriptors:[new UIComponentDescriptor({type:BalanceImageReflectionCanvas, id:"contentImage"})]});
                    }})]});
                }})]});
            }});
            _bindings = [];
            _watchers = [];
            _bindingsByDestination = {};
            _bindingsBeginWithWord = {};
            super();
            mx_internal::_document = this;
            this.addEventListener("creationComplete", ___PicRightInfoText_ModuleViewTemplate1_creationComplete);
        }
        public function get copyContainer():BalanceContentReflectionCanvas{
            return (this._1939189620copyContainer);
        }
        override public function initialize():void{
            var target:* = null;
            var watcherSetupUtilClass:* = null;
            mx_internal::setDocumentDescriptor(_documentDescriptor_);
            var bindings:* = _PicRightInfoText_bindingsSetup();
            var watchers:* = [];
            target = this;
            if (_watcherSetupUtil == null){
                watcherSetupUtilClass = getDefinitionByName("_com_redbox_changetech_view_templates_PicRightInfoTextWatcherSetupUtil");
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
        public function get transContainer2():Canvas{
            return (this._1549852825transContainer2);
        }
        public function set cta_container(_arg1:HBox):void{
            var _local2:Object = this._1298893550cta_container;
            if (_local2 !== _arg1){
                this._1298893550cta_container = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "cta_container", _local2, _arg1));
            };
        }
        public function set copyContainer(_arg1:BalanceContentReflectionCanvas):void{
            var _local2:Object = this._1939189620copyContainer;
            if (_local2 !== _arg1){
                this._1939189620copyContainer = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "copyContainer", _local2, _arg1));
            };
        }
        public function ___PicRightInfoText_ModuleViewTemplate1_creationComplete(_arg1:FlexEvent):void{
            init(_arg1);
        }
        public function get cta_btn():BalanceButtonReflectionCanvas{
            return (this._1082042285cta_btn);
        }
        public function get transContainer1():Canvas{
            return (this._1549852824transContainer1);
        }
        private function init(_arg1:FlexEvent):void{
            trace(("content.PresenterImageUrl" + content.PresenterImageUrl));
            BasicModule(module).fadeInContainer1.addEventListener(EffectEvent.EFFECT_START, initBounceEffect);
        }
        public function get info():VBox{
            return (this._3237038info);
        }
        public function set transContainer1(_arg1:Canvas):void{
            var _local2:Object = this._1549852824transContainer1;
            if (_local2 !== _arg1){
                this._1549852824transContainer1 = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "transContainer1", _local2, _arg1));
            };
        }
        public function set transContainer2(_arg1:Canvas):void{
            var _local2:Object = this._1549852825transContainer2;
            if (_local2 !== _arg1){
                this._1549852825transContainer2 = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "transContainer2", _local2, _arg1));
            };
        }
        private function _PicRightInfoText_bindingExprs():void{
            var _local1:*;
            _local1 = model.currentStageWidth;
            _local1 = model.currentStageHeight;
            _local1 = ((transContainer1.width / 2) - 285);
            _local1 = Math.max(((650 - (info.height / 2)) / 4), 0);
            _local1 = (((copyContainer.height / 2) + 20) * -1);
            _local1 = content;
            _local1 = BasicModule(module);
            _local1 = content.getSecondaryButton().Label;
            _local1 = !((content.getSecondaryButton() == null));
            _local1 = content.getCTAButton().Label;
            _local1 = (content.getTertiaryButton() == null);
            _local1 = !((content.getCTAButton() == null));
            _local1 = content.getTertiaryButton().Label;
            _local1 = cta_btn.height;
            _local1 = !((content.getTertiaryButton() == null));
            _local1 = model.currentStageWidth;
            _local1 = model.currentStageHeight;
            _local1 = ((info.x + info.width) - 40);
            _local1 = content.PresenterImageUrl;
        }
        public function set contentImage(_arg1:BalanceImageReflectionCanvas):void{
            var _local2:Object = this._811296866contentImage;
            if (_local2 !== _arg1){
                this._811296866contentImage = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "contentImage", _local2, _arg1));
            };
        }
        public function set aux_btn(_arg1:BalanceSecondaryButtonReflectionCanvas):void{
            var _local2:Object = this._643094943aux_btn;
            if (_local2 !== _arg1){
                this._643094943aux_btn = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "aux_btn", _local2, _arg1));
            };
        }
        override protected function reset():void{
            aux_btn2.y = (cta_btn.height + 5);
        }
        public function set cta_btn(_arg1:BalanceButtonReflectionCanvas):void{
            var _local2:Object = this._1082042285cta_btn;
            if (_local2 !== _arg1){
                this._1082042285cta_btn = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "cta_btn", _local2, _arg1));
            };
        }
        private function _PicRightInfoText_bindingsSetup():Array{
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
                return (((transContainer1.width / 2) - 285));
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
                return (content);
            }, function (_arg1:Content):void{
                copyContainer.content = _arg1;
            }, "copyContainer.content");
            result[5] = binding;
            binding = new Binding(this, function ():BasicModule{
                return (BasicModule(module));
            }, function (_arg1:BasicModule):void{
                copyContainer.module = _arg1;
            }, "copyContainer.module");
            result[6] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = content.getSecondaryButton().Label;
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                aux_btn.buttonLabel = _arg1;
            }, "aux_btn.buttonLabel");
            result[7] = binding;
            binding = new Binding(this, function ():Boolean{
                return (!((content.getSecondaryButton() == null)));
            }, function (_arg1:Boolean):void{
                aux_btn.visible = _arg1;
            }, "aux_btn.visible");
            result[8] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = content.getCTAButton().Label;
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                cta_btn.buttonLabel = _arg1;
            }, "cta_btn.buttonLabel");
            result[9] = binding;
            binding = new Binding(this, function ():Boolean{
                return ((content.getTertiaryButton() == null));
            }, function (_arg1:Boolean):void{
                cta_btn.reflectionIsOn = _arg1;
            }, "cta_btn.reflectionIsOn");
            result[10] = binding;
            binding = new Binding(this, function ():Boolean{
                return (!((content.getCTAButton() == null)));
            }, function (_arg1:Boolean):void{
                cta_btn.visible = _arg1;
            }, "cta_btn.visible");
            result[11] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = content.getTertiaryButton().Label;
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                aux_btn2.buttonLabel = _arg1;
            }, "aux_btn2.buttonLabel");
            result[12] = binding;
            binding = new Binding(this, function ():Number{
                return (cta_btn.height);
            }, function (_arg1:Number):void{
                aux_btn2.y = _arg1;
            }, "aux_btn2.y");
            result[13] = binding;
            binding = new Binding(this, function ():Boolean{
                return (!((content.getTertiaryButton() == null)));
            }, function (_arg1:Boolean):void{
                aux_btn2.visible = _arg1;
            }, "aux_btn2.visible");
            result[14] = binding;
            binding = new Binding(this, function ():Number{
                return (model.currentStageWidth);
            }, function (_arg1:Number):void{
                transContainer2.width = _arg1;
            }, "transContainer2.width");
            result[15] = binding;
            binding = new Binding(this, function ():Number{
                return (model.currentStageHeight);
            }, function (_arg1:Number):void{
                transContainer2.height = _arg1;
            }, "transContainer2.height");
            result[16] = binding;
            binding = new Binding(this, function ():Number{
                return (((info.x + info.width) - 40));
            }, function (_arg1:Number):void{
                _PicRightInfoText_HBox2.x = _arg1;
            }, "_PicRightInfoText_HBox2.x");
            result[17] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = content.PresenterImageUrl;
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                contentImage.source = _arg1;
            }, "contentImage.source");
            result[18] = binding;
            return (result);
        }
        public function get contentImage():BalanceImageReflectionCanvas{
            return (this._811296866contentImage);
        }
        public function set info(_arg1:VBox):void{
            var _local2:Object = this._3237038info;
            if (_local2 !== _arg1){
                this._3237038info = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "info", _local2, _arg1));
            };
        }
        public function get aux_btn():BalanceSecondaryButtonReflectionCanvas{
            return (this._643094943aux_btn);
        }
        public function set aux_btn2(_arg1:BalanceTertiaryButtonReflectionCanvas):void{
            var _local2:Object = this._1538893297aux_btn2;
            if (_local2 !== _arg1){
                this._1538893297aux_btn2 = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "aux_btn2", _local2, _arg1));
            };
        }
        private function initBounceEffect(_arg1:EffectEvent):void{
            BasicModule(module).bounceDownEffect.target = copyContainer;
            BasicModule(module).bounceDownEffect.yFrom = -50;
            BasicModule(module).bounceDownEffect.yTo = 0;
            BasicModule(module).bounceDownEffect.play();
        }
        public function get aux_btn2():BalanceTertiaryButtonReflectionCanvas{
            return (this._1538893297aux_btn2);
        }

        public static function set watcherSetupUtil(_arg1:IWatcherSetupUtil):void{
            PicRightInfoText._watcherSetupUtil = _arg1;
        }

    }
}//package com.redbox.changetech.view.templates 
