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
    import mx.binding.*;
    import mx.containers.*;
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

    public class WeekdayDataEntrySlider extends VBox implements IBindingClient {

        private var _title_str:String
        private var _valueType:String
        private var _sliderValue:Number
        private var _iconSource:String
        public var _WeekdayDataEntrySlider_Image1:Image
        mx_internal var _bindingsByDestination:Object
        mx_internal var _bindingsBeginWithWord:Object
        mx_internal var _watchers:Array
        public var _WeekdayDataEntrySlider_Label1:Label
        mx_internal var _bindings:Array
        private var _320979235balanceSlider:BalanceSlider
        private var _documentDescriptor_:UIComponentDescriptor

        private static var _watcherSetupUtil:IWatcherSetupUtil;

        public function WeekdayDataEntrySlider(){
            _documentDescriptor_ = new UIComponentDescriptor({type:VBox, propertiesFactory:function ():Object{
                return ({childDescriptors:[new UIComponentDescriptor({type:Label, id:"_WeekdayDataEntrySlider_Label1", stylesFactory:function ():void{
                    this.fontFamily = "Helvetica Neue";
                    this.fontSize = 11;
                    this.color = 2064302;
                }, propertiesFactory:function ():Object{
                    return ({percentHeight:100});
                }}), new UIComponentDescriptor({type:HBox, stylesFactory:function ():void{
                    this.verticalAlign = "top";
                    this.horizontalGap = 0;
                }, propertiesFactory:function ():Object{
                    return ({percentWidth:100, childDescriptors:[new UIComponentDescriptor({type:VBox, stylesFactory:function ():void{
                        this.verticalGap = 0;
                    }, propertiesFactory:function ():Object{
                        return ({percentHeight:100, childDescriptors:[new UIComponentDescriptor({type:Spacer, propertiesFactory:function ():Object{
                            return ({height:6});
                        }}), new UIComponentDescriptor({type:Image, id:"_WeekdayDataEntrySlider_Image1", propertiesFactory:function ():Object{
                            return ({filters:[_WeekdayDataEntrySlider_GlowFilter1_c()]});
                        }})]});
                    }}), new UIComponentDescriptor({type:BalanceSlider, id:"balanceSlider", propertiesFactory:function ():Object{
                        return ({width:240, height:50, offset:22, fillColors:[0xD0D0D0, 0xE6E6E6], textColor:2263998});
                    }})]});
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
                this.verticalGap = 0;
            };
        }
        public function set balanceSlider(_arg1:BalanceSlider):void{
            var _local2:Object = this._320979235balanceSlider;
            if (_local2 !== _arg1){
                this._320979235balanceSlider = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "balanceSlider", _local2, _arg1));
            };
        }
        private function set _501509644iconSource(_arg1:String):void{
            _iconSource = _arg1;
        }
        public function get title_str():String{
            return (_title_str);
        }
        public function set title_str(_arg1:String):void{
            var _local2:Object = this.title_str;
            if (_local2 !== _arg1){
                this._2135415862title_str = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "title_str", _local2, _arg1));
            };
        }
        public function get sliderValue():Number{
            return (_sliderValue);
        }
        public function get valueType():String{
            return (_valueType);
        }
        override public function initialize():void{
            var target:* = null;
            var watcherSetupUtilClass:* = null;
            mx_internal::setDocumentDescriptor(_documentDescriptor_);
            var bindings:* = _WeekdayDataEntrySlider_bindingsSetup();
            var watchers:* = [];
            target = this;
            if (_watcherSetupUtil == null){
                watcherSetupUtilClass = getDefinitionByName("_com_redbox_changetech_view_components_WeekdayDataEntrySliderWatcherSetupUtil");
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
        private function set _765692853valueType(_arg1:String):void{
            _valueType = _arg1;
        }
        private function set _2135415862title_str(_arg1:String):void{
            _title_str = _arg1;
        }
        private function _WeekdayDataEntrySlider_GlowFilter1_c():GlowFilter{
            var _local1:GlowFilter = new GlowFilter();
            _local1.color = 0x666666;
            _local1.alpha = 0.8;
            return (_local1);
        }
        public function set valueType(_arg1:String):void{
            var _local2:Object = this.valueType;
            if (_local2 !== _arg1){
                this._765692853valueType = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "valueType", _local2, _arg1));
            };
        }
        private function _WeekdayDataEntrySlider_bindingExprs():void{
            var _local1:*;
            _local1 = title_str;
            _local1 = iconSource;
            _local1 = valueType;
            _local1 = sliderValue;
        }
        public function set iconSource(_arg1:String):void{
            var _local2:Object = this.iconSource;
            if (_local2 !== _arg1){
                this._501509644iconSource = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "iconSource", _local2, _arg1));
            };
        }
        private function set _66679536sliderValue(_arg1:Number):void{
            _sliderValue = _arg1;
        }
        public function set sliderValue(_arg1:Number):void{
            var _local2:Object = this.sliderValue;
            if (_local2 !== _arg1){
                this._66679536sliderValue = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "sliderValue", _local2, _arg1));
            };
        }
        private function _WeekdayDataEntrySlider_bindingsSetup():Array{
            var binding:* = null;
            var result:* = [];
            binding = new Binding(this, function ():String{
                var _local1:* = title_str;
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                _WeekdayDataEntrySlider_Label1.text = _arg1;
            }, "_WeekdayDataEntrySlider_Label1.text");
            result[0] = binding;
            binding = new Binding(this, function ():Object{
                return (iconSource);
            }, function (_arg1:Object):void{
                _WeekdayDataEntrySlider_Image1.source = _arg1;
            }, "_WeekdayDataEntrySlider_Image1.source");
            result[1] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = valueType;
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                balanceSlider.valueType = _arg1;
            }, "balanceSlider.valueType");
            result[2] = binding;
            binding = new Binding(this, function ():Number{
                return (sliderValue);
            }, function (_arg1:Number):void{
                balanceSlider.sliderValue = _arg1;
            }, "balanceSlider.sliderValue");
            result[3] = binding;
            return (result);
        }
        public function get iconSource():String{
            return (_iconSource);
        }
        public function get balanceSlider():BalanceSlider{
            return (this._320979235balanceSlider);
        }

        public static function set watcherSetupUtil(_arg1:IWatcherSetupUtil):void{
            WeekdayDataEntrySlider._watcherSetupUtil = _arg1;
        }

    }
}//package com.redbox.changetech.view.components 
