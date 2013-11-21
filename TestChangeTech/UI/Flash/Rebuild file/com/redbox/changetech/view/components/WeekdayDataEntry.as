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
    import com.redbox.changetech.control.*;
    import mx.containers.*;
    import com.redbox.changetech.model.*;
    import mx.binding.utils.*;
    import com.redbox.changetech.vo.*;
    import flash.utils.*;
    import mx.collections.*;
    import com.adobe.cairngorm.control.*;
    import flash.system.*;
    import flash.accessibility.*;
    import flash.xml.*;
    import flash.net.*;
    import flash.filters.*;
    import flash.ui.*;
    import com.redbox.changetech.util.Enumerations.*;
    import org.osflash.thunderbolt.*;
    import flash.external.*;
    import flash.debugger.*;
    import flash.errors.*;
    import flash.printing.*;
    import flash.profiler.*;

    public class WeekdayDataEntry extends VBox implements IBindingClient {

        private var _1004885724beer_icon_path:String
        private var _1920131255drinks2:WeekdayDataEntrySlider
        public var showPrevious:Boolean = false
        mx_internal var _watchers:Array
        public var showNext:Boolean = false
        private var _851752821wine_icon_path:String
        private var _1920131254drinks1:WeekdayDataEntrySlider
        private var _1451416746showComplete:Boolean = false
        private var _482750770spirits_icon_path:String
        mx_internal var _bindingsByDestination:Object
        mx_internal var _bindingsBeginWithWord:Object
        private var _1470213668_consumption:Consumption
        private var _104069929model:BalanceModelLocator
        mx_internal var _bindings:Array
        private var _696955380_reportedValues:ArrayCollection
        private var _1920131256drinks3:WeekdayDataEntrySlider
        private var _documentDescriptor_:UIComponentDescriptor

        private static var _watcherSetupUtil:IWatcherSetupUtil;

        public function WeekdayDataEntry(){
            _documentDescriptor_ = new UIComponentDescriptor({type:VBox, propertiesFactory:function ():Object{
                return ({childDescriptors:[new UIComponentDescriptor({type:VBox, stylesFactory:function ():void{
                    this.horizontalAlign = "left";
                    this.verticalAlign = "middle";
                }, propertiesFactory:function ():Object{
                    return ({percentWidth:100, percentHeight:100, childDescriptors:[new UIComponentDescriptor({type:WeekdayDataEntrySlider, id:"drinks1"}), new UIComponentDescriptor({type:WeekdayDataEntrySlider, id:"drinks2"}), new UIComponentDescriptor({type:WeekdayDataEntrySlider, id:"drinks3"})]});
                }})]});
            }});
            _104069929model = BalanceModelLocator.getInstance();
            _1004885724beer_icon_path = (("locales/" + model.culture) + "/pictures/slider__icon_beer.png");
            _851752821wine_icon_path = (("locales/" + model.culture) + "/pictures/slider__icon_wine.png");
            _482750770spirits_icon_path = (("locales/" + model.culture) + "/pictures/slider__icon_spirits.png");
            _696955380_reportedValues = new ArrayCollection();
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
            this.addEventListener("creationComplete", ___WeekdayDataEntry_VBox1_creationComplete);
        }
        override public function initialize():void{
            var target:* = null;
            var watcherSetupUtilClass:* = null;
            mx_internal::setDocumentDescriptor(_documentDescriptor_);
            var bindings:* = _WeekdayDataEntry_bindingsSetup();
            var watchers:* = [];
            target = this;
            if (_watcherSetupUtil == null){
                watcherSetupUtilClass = getDefinitionByName("_com_redbox_changetech_view_components_WeekdayDataEntryWatcherSetupUtil");
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
        private function sliderChanged(_arg1:SliderEvent):void{
            var _local5:ConsumptionValue;
            _consumption.Modified = true;
            var _local2:String = BalanceSlider(_arg1.target).valueType;
            var _local3:int = Math.round(BalanceSlider(_arg1.target).slide.value);
            var _local4:Number = 0;
            while (_local4 < _consumption.ReportedValues.length) {
                _local5 = _consumption.ReportedValues[_local4];
                if (ConsumptionValue(_local5).Type == _local2){
                    _local5.Value = _local3;
                    break;
                };
                _local4++;
            };
            enableContinue();
        }
        private function set spirits_icon_path(_arg1:String):void{
            var _local2:Object = this._482750770spirits_icon_path;
            if (_local2 !== _arg1){
                this._482750770spirits_icon_path = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "spirits_icon_path", _local2, _arg1));
            };
        }
        private function previousDay():void{
            var _local1:CairngormEvent = new CairngormEvent(BalanceController.SELECT_PREVIOUS_DAY);
            _local1.data = _consumption;
            _local1.dispatch();
        }
        private function nextDay():void{
            var _local1:CairngormEvent = new CairngormEvent(BalanceController.SELECT_NEXT_DAY);
            _local1.data = _consumption;
            _local1.dispatch();
        }
        private function get model():BalanceModelLocator{
            return (this._104069929model);
        }
        public function get drinks2():WeekdayDataEntrySlider{
            return (this._1920131255drinks2);
        }
        public function get drinks3():WeekdayDataEntrySlider{
            return (this._1920131256drinks3);
        }
        private function _WeekdayDataEntry_bindingExprs():void{
            var _local1:*;
            _local1 = model.languageVO.getLang(DrinkType.Beer.Text).toUpperCase();
            _local1 = DrinkType.Beer.Text;
            _local1 = _reportedValues.getItemAt(0).Value;
            _local1 = beer_icon_path;
            _local1 = model.languageVO.getLang(DrinkType.Wine.Text).toUpperCase();
            _local1 = DrinkType.Wine.Text;
            _local1 = _reportedValues.getItemAt(1).Value;
            _local1 = wine_icon_path;
            _local1 = model.languageVO.getLang(DrinkType.Spirit.Text).toUpperCase();
            _local1 = DrinkType.Spirit.Text;
            _local1 = _reportedValues.getItemAt(2).Value;
            _local1 = spirits_icon_path;
        }
        public function set showComplete(_arg1:Boolean):void{
            var _local2:Object = this._1451416746showComplete;
            if (_local2 !== _arg1){
                this._1451416746showComplete = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "showComplete", _local2, _arg1));
            };
        }
        public function get drinks1():WeekdayDataEntrySlider{
            return (this._1920131254drinks1);
        }
        private function initialise():void{
            BindingUtils.bindSetter(setConsumption, BalanceModelLocator.getInstance(), "currentConsumptionVO");
            drinks1.balanceSlider.addEventListener(SliderEvent.CHANGE, sliderChanged);
            drinks2.balanceSlider.addEventListener(SliderEvent.CHANGE, sliderChanged);
            drinks3.balanceSlider.addEventListener(SliderEvent.CHANGE, sliderChanged);
            drinks1.balanceSlider.addEventListener(SliderEvent.THUMB_RELEASE, sliderChanged);
            drinks2.balanceSlider.addEventListener(SliderEvent.THUMB_RELEASE, sliderChanged);
            drinks3.balanceSlider.addEventListener(SliderEvent.THUMB_RELEASE, sliderChanged);
        }
        private function set beer_icon_path(_arg1:String):void{
            var _local2:Object = this._1004885724beer_icon_path;
            if (_local2 !== _arg1){
                this._1004885724beer_icon_path = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "beer_icon_path", _local2, _arg1));
            };
        }
        private function complete():void{
            Logger.debug("complete");
            var _local1:Event = new Event(BalanceController.MODULE_COLLECTION_COMPLETE, true);
            dispatchEvent(_local1);
        }
        private function set _consumption(_arg1:Consumption):void{
            var _local2:Object = this._1470213668_consumption;
            if (_local2 !== _arg1){
                this._1470213668_consumption = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "_consumption", _local2, _arg1));
            };
        }
        private function get spirits_icon_path():String{
            return (this._482750770spirits_icon_path);
        }
        private function enableContinue():void{
        }
        private function set model(_arg1:BalanceModelLocator):void{
            var _local2:Object = this._104069929model;
            if (_local2 !== _arg1){
                this._104069929model = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "model", _local2, _arg1));
            };
        }
        public function get showComplete():Boolean{
            return (this._1451416746showComplete);
        }
        public function setConsumption(_arg1:Consumption):void{
            if (_arg1 == null){
                return;
            };
            _consumption = _arg1;
            _reportedValues.source = _consumption.ReportedValues;
            enabled = !(_consumption.Closed);
        }
        public function ___WeekdayDataEntry_VBox1_creationComplete(_arg1:FlexEvent):void{
            initialise();
        }
        private function get beer_icon_path():String{
            return (this._1004885724beer_icon_path);
        }
        private function set wine_icon_path(_arg1:String):void{
            var _local2:Object = this._851752821wine_icon_path;
            if (_local2 !== _arg1){
                this._851752821wine_icon_path = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "wine_icon_path", _local2, _arg1));
            };
        }
        private function get _consumption():Consumption{
            return (this._1470213668_consumption);
        }
        private function _WeekdayDataEntry_bindingsSetup():Array{
            var binding:* = null;
            var result:* = [];
            binding = new Binding(this, function ():String{
                var _local1:* = model.languageVO.getLang(DrinkType.Beer.Text).toUpperCase();
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                drinks1.title_str = _arg1;
            }, "drinks1.title_str");
            result[0] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = DrinkType.Beer.Text;
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                drinks1.valueType = _arg1;
            }, "drinks1.valueType");
            result[1] = binding;
            binding = new Binding(this, function ():Number{
                return (_reportedValues.getItemAt(0).Value);
            }, function (_arg1:Number):void{
                drinks1.sliderValue = _arg1;
            }, "drinks1.sliderValue");
            result[2] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = beer_icon_path;
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                drinks1.iconSource = _arg1;
            }, "drinks1.iconSource");
            result[3] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = model.languageVO.getLang(DrinkType.Wine.Text).toUpperCase();
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                drinks2.title_str = _arg1;
            }, "drinks2.title_str");
            result[4] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = DrinkType.Wine.Text;
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                drinks2.valueType = _arg1;
            }, "drinks2.valueType");
            result[5] = binding;
            binding = new Binding(this, function ():Number{
                return (_reportedValues.getItemAt(1).Value);
            }, function (_arg1:Number):void{
                drinks2.sliderValue = _arg1;
            }, "drinks2.sliderValue");
            result[6] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = wine_icon_path;
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                drinks2.iconSource = _arg1;
            }, "drinks2.iconSource");
            result[7] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = model.languageVO.getLang(DrinkType.Spirit.Text).toUpperCase();
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                drinks3.title_str = _arg1;
            }, "drinks3.title_str");
            result[8] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = DrinkType.Spirit.Text;
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                drinks3.valueType = _arg1;
            }, "drinks3.valueType");
            result[9] = binding;
            binding = new Binding(this, function ():Number{
                return (_reportedValues.getItemAt(2).Value);
            }, function (_arg1:Number):void{
                drinks3.sliderValue = _arg1;
            }, "drinks3.sliderValue");
            result[10] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = spirits_icon_path;
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                drinks3.iconSource = _arg1;
            }, "drinks3.iconSource");
            result[11] = binding;
            return (result);
        }
        public function set drinks3(_arg1:WeekdayDataEntrySlider):void{
            var _local2:Object = this._1920131256drinks3;
            if (_local2 !== _arg1){
                this._1920131256drinks3 = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "drinks3", _local2, _arg1));
            };
        }
        public function set drinks1(_arg1:WeekdayDataEntrySlider):void{
            var _local2:Object = this._1920131254drinks1;
            if (_local2 !== _arg1){
                this._1920131254drinks1 = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "drinks1", _local2, _arg1));
            };
        }
        private function get _reportedValues():ArrayCollection{
            return (this._696955380_reportedValues);
        }
        public function set drinks2(_arg1:WeekdayDataEntrySlider):void{
            var _local2:Object = this._1920131255drinks2;
            if (_local2 !== _arg1){
                this._1920131255drinks2 = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "drinks2", _local2, _arg1));
            };
        }
        private function set _reportedValues(_arg1:ArrayCollection):void{
            var _local2:Object = this._696955380_reportedValues;
            if (_local2 !== _arg1){
                this._696955380_reportedValues = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "_reportedValues", _local2, _arg1));
            };
        }
        private function get wine_icon_path():String{
            return (this._851752821wine_icon_path);
        }

        public static function set watcherSetupUtil(_arg1:IWatcherSetupUtil):void{
            WeekdayDataEntry._watcherSetupUtil = _arg1;
        }

    }
}//package com.redbox.changetech.view.components 
