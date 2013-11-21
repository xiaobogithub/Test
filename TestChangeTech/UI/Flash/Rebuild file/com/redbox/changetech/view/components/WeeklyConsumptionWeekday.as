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
    import mx.states.*;
    import mx.binding.*;
    import com.redbox.changetech.control.*;
    import com.redbox.changetech.model.*;
    import mx.binding.utils.*;
    import assets.*;
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
    import com.redbox.changetech.util.*;
    import flash.external.*;
    import flash.debugger.*;
    import flash.errors.*;
    import flash.printing.*;
    import flash.profiler.*;

    public class WeeklyConsumptionWeekday extends GradientCanvas implements IBindingClient {

        public var _WeeklyConsumptionWeekday_SetProperty6:SetProperty
        private var _99228day:Label
        mx_internal var _bindingsByDestination:Object
        private var _437340306defaultGlow:GlowFilter
        private var _1167081665totalDrinks:int = 0
        private var _1323533605drinks:Label
        public var _WeeklyConsumptionWeekday_Label3:Label
        mx_internal var _watchers:Array
        private var _227118224drinks_label:Label
        private var _1330838027drinks_string:String
        private var _97197634tickImage:Image
        public var _WeeklyConsumptionWeekday_SetProperty10:SetProperty
        public var _WeeklyConsumptionWeekday_SetProperty11:SetProperty
        public var _WeeklyConsumptionWeekday_SetProperty12:SetProperty
        public var _WeeklyConsumptionWeekday_SetProperty13:SetProperty
        public var _WeeklyConsumptionWeekday_SetProperty14:SetProperty
        public var _WeeklyConsumptionWeekday_SetProperty15:SetProperty
        public var _WeeklyConsumptionWeekday_SetProperty16:SetProperty
        private var _1754851640selectedGlow:GlowFilter
        mx_internal var _bindingsBeginWithWord:Object
        private var _532037983dateString:String
        private var _consumption:Consumption
        public var _WeeklyConsumptionWeekday_SetProperty1:SetProperty
        mx_internal var _bindings:Array
        public var _WeeklyConsumptionWeekday_SetProperty3:SetProperty
        public var _WeeklyConsumptionWeekday_SetProperty4:SetProperty
        public var _WeeklyConsumptionWeekday_SetProperty5:SetProperty
        public var _WeeklyConsumptionWeekday_SetProperty7:SetProperty
        public var _WeeklyConsumptionWeekday_SetProperty8:SetProperty
        public var _WeeklyConsumptionWeekday_SetProperty2:SetProperty
        private var _104069929model:BalanceModelLocator
        private var _documentDescriptor_:UIComponentDescriptor
        public var _WeeklyConsumptionWeekday_SetProperty9:SetProperty
        private var _696955380_reportedValues:ArrayCollection

        private static var _watcherSetupUtil:IWatcherSetupUtil;

        public function WeeklyConsumptionWeekday(){
            _documentDescriptor_ = new UIComponentDescriptor({type:GradientCanvas, propertiesFactory:function ():Object{
                return ({width:145, height:125, childDescriptors:[new UIComponentDescriptor({type:Image, id:"tickImage", propertiesFactory:function ():Object{
                    return ({x:60, y:23});
                }}), new UIComponentDescriptor({type:Label, id:"day", propertiesFactory:function ():Object{
                    return ({x:5, y:2, styleName:"weekday"});
                }}), new UIComponentDescriptor({type:Label, id:"drinks", propertiesFactory:function ():Object{
                    return ({x:5, y:65, styleName:"drinks"});
                }}), new UIComponentDescriptor({type:Label, id:"_WeeklyConsumptionWeekday_Label3", propertiesFactory:function ():Object{
                    return ({x:5, y:65, text:"_", styleName:"drinks"});
                }}), new UIComponentDescriptor({type:Label, id:"drinks_label", propertiesFactory:function ():Object{
                    return ({styleName:"weekday", x:60, y:90});
                }})]});
            }});
            _104069929model = BalanceModelLocator.getInstance();
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
                this.fillColors = [0xFFFFFF, 0xCFCFCF];
                this.fillAlphas = [1, 1];
                this.gradientRatio = [0, 0xFF];
                this.angle = [90];
                this.borderAlphas = [0];
                this.cornerRadius = 0;
            };
            this.colorsConfiguration = [2];
            this.width = 145;
            this.height = 125;
            this.horizontalScrollPolicy = "off";
            this.verticalScrollPolicy = "off";
            this.currentState = "default";
            this.states = [_WeeklyConsumptionWeekday_State1_c(), _WeeklyConsumptionWeekday_State2_c(), _WeeklyConsumptionWeekday_State3_c(), _WeeklyConsumptionWeekday_State4_c()];
            this.filters = [_WeeklyConsumptionWeekday_DropShadowFilter1_c(), _WeeklyConsumptionWeekday_GlowFilter1_i(), _WeeklyConsumptionWeekday_GlowFilter2_i()];
            this.addEventListener("creationComplete", ___WeeklyConsumptionWeekday_GradientCanvas1_creationComplete);
        }
        private function selectDay(_arg1:MouseEvent):void{
            var _local2:CairngormEvent = new CairngormEvent(BalanceController.SELECT_DAY);
            _local2.data = consumption;
            _local2.dispatch();
        }
        public function get drinks_label():Label{
            return (this._227118224drinks_label);
        }
        private function get _reportedValues():ArrayCollection{
            return (this._696955380_reportedValues);
        }
        private function init():void{
            if (consumption.ConsumptionDate == null){
                dateString = "";
            } else {
                dateString = getMonthDateString(consumption.ConsumptionDate);
            };
            drinks_string = model.languageVO.getLang("Drinks").toUpperCase();
        }
        private function set dateString(_arg1:String):void{
            var _local2:Object = this._532037983dateString;
            if (_local2 !== _arg1){
                this._532037983dateString = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "dateString", _local2, _arg1));
            };
        }
        public function set selectedGlow(_arg1:GlowFilter):void{
            var _local2:Object = this._1754851640selectedGlow;
            if (_local2 !== _arg1){
                this._1754851640selectedGlow = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "selectedGlow", _local2, _arg1));
            };
        }
        private function _WeeklyConsumptionWeekday_SetProperty4_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _WeeklyConsumptionWeekday_SetProperty4 = _local1;
            _local1.name = "styleName";
            _local1.value = "weekdaySelected";
            BindingManager.executeBindings(this, "_WeeklyConsumptionWeekday_SetProperty4", _WeeklyConsumptionWeekday_SetProperty4);
            return (_local1);
        }
        public function checkStatus(_arg1:Event=null):void{
            var _local2 = (BalanceModelLocator.getInstance().currentConsumptionVO == consumption);
            currentState = (_local2) ? "selected" : (consumption.Modified) ? "completed" : "default";
        }
        private function _WeeklyConsumptionWeekday_SetProperty8_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _WeeklyConsumptionWeekday_SetProperty8 = _local1;
            _local1.name = "styleName";
            _local1.value = "drinks";
            BindingManager.executeBindings(this, "_WeeklyConsumptionWeekday_SetProperty8", _WeeklyConsumptionWeekday_SetProperty8);
            return (_local1);
        }
        private function _WeeklyConsumptionWeekday_State4_c():State{
            var _local1:State = new State();
            _local1.name = "hidden";
            _local1.overrides = [_WeeklyConsumptionWeekday_SetProperty16_i()];
            return (_local1);
        }
        private function _WeeklyConsumptionWeekday_GlowFilter2_i():GlowFilter{
            var _local1:GlowFilter = new GlowFilter();
            selectedGlow = _local1;
            _local1.color = 2729955;
            _local1.blurX = 5;
            _local1.blurY = 5;
            return (_local1);
        }
        public function set drinks_label(_arg1:Label):void{
            var _local2:Object = this._227118224drinks_label;
            if (_local2 !== _arg1){
                this._227118224drinks_label = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "drinks_label", _local2, _arg1));
            };
        }
        private function get model():BalanceModelLocator{
            return (this._104069929model);
        }
        private function _WeeklyConsumptionWeekday_SetProperty12_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _WeeklyConsumptionWeekday_SetProperty12 = _local1;
            _local1.name = "styleName";
            _local1.value = "weekday";
            BindingManager.executeBindings(this, "_WeeklyConsumptionWeekday_SetProperty12", _WeeklyConsumptionWeekday_SetProperty12);
            return (_local1);
        }
        private function _WeeklyConsumptionWeekday_SetProperty16_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _WeeklyConsumptionWeekday_SetProperty16 = _local1;
            _local1.name = "visible";
            _local1.value = false;
            BindingManager.executeBindings(this, "_WeeklyConsumptionWeekday_SetProperty16", _WeeklyConsumptionWeekday_SetProperty16);
            return (_local1);
        }
        public function set consumption(_arg1:Consumption):void{
            var _local2:Object = this.consumption;
            if (_local2 !== _arg1){
                this._848170085consumption = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "consumption", _local2, _arg1));
            };
        }
        public function set day(_arg1:Label):void{
            var _local2:Object = this._99228day;
            if (_local2 !== _arg1){
                this._99228day = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "day", _local2, _arg1));
            };
        }
        private function get totalDrinks():int{
            return (this._1167081665totalDrinks);
        }
        public function ___WeeklyConsumptionWeekday_GradientCanvas1_creationComplete(_arg1:FlexEvent):void{
            init();
        }
        private function set model(_arg1:BalanceModelLocator):void{
            var _local2:Object = this._104069929model;
            if (_local2 !== _arg1){
                this._104069929model = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "model", _local2, _arg1));
            };
        }
        private function _WeeklyConsumptionWeekday_SetProperty3_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _WeeklyConsumptionWeekday_SetProperty3 = _local1;
            _local1.name = "styleName";
            _local1.value = "drinksSelected";
            BindingManager.executeBindings(this, "_WeeklyConsumptionWeekday_SetProperty3", _WeeklyConsumptionWeekday_SetProperty3);
            return (_local1);
        }
        private function _WeeklyConsumptionWeekday_SetProperty7_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _WeeklyConsumptionWeekday_SetProperty7 = _local1;
            _local1.name = "styleName";
            _local1.value = "weekday";
            BindingManager.executeBindings(this, "_WeeklyConsumptionWeekday_SetProperty7", _WeeklyConsumptionWeekday_SetProperty7);
            return (_local1);
        }
        private function _WeeklyConsumptionWeekday_State3_c():State{
            var _local1:State = new State();
            _local1.name = "completed";
            _local1.overrides = [_WeeklyConsumptionWeekday_SetProperty11_i(), _WeeklyConsumptionWeekday_SetProperty12_i(), _WeeklyConsumptionWeekday_SetProperty13_i(), _WeeklyConsumptionWeekday_SetProperty14_i(), _WeeklyConsumptionWeekday_SetProperty15_i()];
            return (_local1);
        }
        private function _WeeklyConsumptionWeekday_GlowFilter1_i():GlowFilter{
            var _local1:GlowFilter = new GlowFilter();
            defaultGlow = _local1;
            _local1.color = 14342872;
            _local1.blurX = 5;
            _local1.blurY = 5;
            return (_local1);
        }
        private function get dateString():String{
            return (this._532037983dateString);
        }
        public function get defaultGlow():GlowFilter{
            return (this._437340306defaultGlow);
        }
        private function _WeeklyConsumptionWeekday_SetProperty11_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _WeeklyConsumptionWeekday_SetProperty11 = _local1;
            _local1.name = "source";
            BindingManager.executeBindings(this, "_WeeklyConsumptionWeekday_SetProperty11", _WeeklyConsumptionWeekday_SetProperty11);
            return (_local1);
        }
        private function _WeeklyConsumptionWeekday_SetProperty15_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _WeeklyConsumptionWeekday_SetProperty15 = _local1;
            _local1.name = "filters";
            BindingManager.executeBindings(this, "_WeeklyConsumptionWeekday_SetProperty15", _WeeklyConsumptionWeekday_SetProperty15);
            return (_local1);
        }
        private function set _848170085consumption(_arg1:Consumption):void{
            _consumption = _arg1;
            _reportedValues = new ArrayCollection(consumption.ReportedValues);
            if (!hasEventListener(MouseEvent.CLICK)){
                addEventListener(MouseEvent.CLICK, selectDay);
            };
            if (!hasEventListener(MouseEvent.ROLL_OVER)){
                addEventListener(MouseEvent.ROLL_OVER, rollOver);
            };
            if (!hasEventListener(MouseEvent.ROLL_OUT)){
                addEventListener(MouseEvent.ROLL_OUT, rollOut);
            };
            ChangeWatcher.watch(BalanceModelLocator.getInstance(), "currentConsumptionVO", checkStatus);
            checkStatus();
        }
        public function set tickImage(_arg1:Image):void{
            var _local2:Object = this._97197634tickImage;
            if (_local2 !== _arg1){
                this._97197634tickImage = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "tickImage", _local2, _arg1));
            };
        }
        private function set totalDrinks(_arg1:int):void{
            var _local2:Object = this._1167081665totalDrinks;
            if (_local2 !== _arg1){
                this._1167081665totalDrinks = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "totalDrinks", _local2, _arg1));
            };
        }
        private function rollOut(_arg1:MouseEvent):void{
            checkStatus();
        }
        override public function initialize():void{
            var target:* = null;
            var watcherSetupUtilClass:* = null;
            mx_internal::setDocumentDescriptor(_documentDescriptor_);
            var bindings:* = _WeeklyConsumptionWeekday_bindingsSetup();
            var watchers:* = [];
            target = this;
            if (_watcherSetupUtil == null){
                watcherSetupUtilClass = getDefinitionByName("_com_redbox_changetech_view_components_WeeklyConsumptionWeekdayWatcherSetupUtil");
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
        public function get day():Label{
            return (this._99228day);
        }
        private function _WeeklyConsumptionWeekday_SetProperty2_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _WeeklyConsumptionWeekday_SetProperty2 = _local1;
            _local1.name = "styleName";
            _local1.value = "weekdaySelected";
            BindingManager.executeBindings(this, "_WeeklyConsumptionWeekday_SetProperty2", _WeeklyConsumptionWeekday_SetProperty2);
            return (_local1);
        }
        public function get consumption():Consumption{
            return (_consumption);
        }
        private function _WeeklyConsumptionWeekday_SetProperty6_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _WeeklyConsumptionWeekday_SetProperty6 = _local1;
            _local1.name = "source";
            BindingManager.executeBindings(this, "_WeeklyConsumptionWeekday_SetProperty6", _WeeklyConsumptionWeekday_SetProperty6);
            return (_local1);
        }
        private function _WeeklyConsumptionWeekday_bindingExprs():void{
            var _local1:*;
            _local1 = tickImage;
            _local1 = Assets.getInstance().tick;
            _local1 = day;
            _local1 = drinks;
            _local1 = drinks_label;
            _local1 = this;
            _local1 = [selectedGlow];
            _local1 = tickImage;
            _local1 = Assets.getInstance().tickGrey;
            _local1 = day;
            _local1 = drinks;
            _local1 = drinks_label;
            _local1 = this;
            _local1 = [defaultGlow];
            _local1 = tickImage;
            _local1 = Assets.getInstance().tickGrey;
            _local1 = day;
            _local1 = drinks;
            _local1 = drinks_label;
            _local1 = this;
            _local1 = [defaultGlow];
            _local1 = this;
            _local1 = ((consumption.Modified) || (consumption.Closed));
            _local1 = model.languageVO.getLang(consumption.DayOfWeek).toUpperCase();
            _local1 = ((consumption.Modified) || (consumption.Closed));
            _local1 = _reportedValues.getItemAt(3).Value;
            _local1 = false;
            _local1 = drinks_string;
            _local1 = drinks.visible;
        }
        private function _WeeklyConsumptionWeekday_State2_c():State{
            var _local1:State = new State();
            _local1.name = "default";
            _local1.overrides = [_WeeklyConsumptionWeekday_SetProperty6_i(), _WeeklyConsumptionWeekday_SetProperty7_i(), _WeeklyConsumptionWeekday_SetProperty8_i(), _WeeklyConsumptionWeekday_SetProperty9_i(), _WeeklyConsumptionWeekday_SetProperty10_i()];
            return (_local1);
        }
        private function _WeeklyConsumptionWeekday_SetProperty14_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _WeeklyConsumptionWeekday_SetProperty14 = _local1;
            _local1.name = "styleName";
            _local1.value = "weekday";
            BindingManager.executeBindings(this, "_WeeklyConsumptionWeekday_SetProperty14", _WeeklyConsumptionWeekday_SetProperty14);
            return (_local1);
        }
        private function _WeeklyConsumptionWeekday_SetProperty10_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _WeeklyConsumptionWeekday_SetProperty10 = _local1;
            _local1.name = "filters";
            BindingManager.executeBindings(this, "_WeeklyConsumptionWeekday_SetProperty10", _WeeklyConsumptionWeekday_SetProperty10);
            return (_local1);
        }
        public function set drinks(_arg1:Label):void{
            var _local2:Object = this._1323533605drinks;
            if (_local2 !== _arg1){
                this._1323533605drinks = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "drinks", _local2, _arg1));
            };
        }
        private function _WeeklyConsumptionWeekday_DropShadowFilter1_c():DropShadowFilter{
            var _local1:DropShadowFilter = new DropShadowFilter();
            _local1.distance = 2;
            _local1.alpha = 0.1;
            return (_local1);
        }
        private function _WeeklyConsumptionWeekday_bindingsSetup():Array{
            var binding:* = null;
            var result:* = [];
            binding = new Binding(this, function ():Object{
                return (tickImage);
            }, function (_arg1:Object):void{
                _WeeklyConsumptionWeekday_SetProperty1.target = _arg1;
            }, "_WeeklyConsumptionWeekday_SetProperty1.target");
            result[0] = binding;
            binding = new Binding(this, function (){
                return (Assets.getInstance().tick);
            }, function (_arg1):void{
                _WeeklyConsumptionWeekday_SetProperty1.value = _arg1;
            }, "_WeeklyConsumptionWeekday_SetProperty1.value");
            result[1] = binding;
            binding = new Binding(this, function ():Object{
                return (day);
            }, function (_arg1:Object):void{
                _WeeklyConsumptionWeekday_SetProperty2.target = _arg1;
            }, "_WeeklyConsumptionWeekday_SetProperty2.target");
            result[2] = binding;
            binding = new Binding(this, function ():Object{
                return (drinks);
            }, function (_arg1:Object):void{
                _WeeklyConsumptionWeekday_SetProperty3.target = _arg1;
            }, "_WeeklyConsumptionWeekday_SetProperty3.target");
            result[3] = binding;
            binding = new Binding(this, function ():Object{
                return (drinks_label);
            }, function (_arg1:Object):void{
                _WeeklyConsumptionWeekday_SetProperty4.target = _arg1;
            }, "_WeeklyConsumptionWeekday_SetProperty4.target");
            result[4] = binding;
            binding = new Binding(this, function ():Object{
                return (this);
            }, function (_arg1:Object):void{
                _WeeklyConsumptionWeekday_SetProperty5.target = _arg1;
            }, "_WeeklyConsumptionWeekday_SetProperty5.target");
            result[5] = binding;
            binding = new Binding(this, function (){
                return ([selectedGlow]);
            }, function (_arg1):void{
                _WeeklyConsumptionWeekday_SetProperty5.value = _arg1;
            }, "_WeeklyConsumptionWeekday_SetProperty5.value");
            result[6] = binding;
            binding = new Binding(this, function ():Object{
                return (tickImage);
            }, function (_arg1:Object):void{
                _WeeklyConsumptionWeekday_SetProperty6.target = _arg1;
            }, "_WeeklyConsumptionWeekday_SetProperty6.target");
            result[7] = binding;
            binding = new Binding(this, function (){
                return (Assets.getInstance().tickGrey);
            }, function (_arg1):void{
                _WeeklyConsumptionWeekday_SetProperty6.value = _arg1;
            }, "_WeeklyConsumptionWeekday_SetProperty6.value");
            result[8] = binding;
            binding = new Binding(this, function ():Object{
                return (day);
            }, function (_arg1:Object):void{
                _WeeklyConsumptionWeekday_SetProperty7.target = _arg1;
            }, "_WeeklyConsumptionWeekday_SetProperty7.target");
            result[9] = binding;
            binding = new Binding(this, function ():Object{
                return (drinks);
            }, function (_arg1:Object):void{
                _WeeklyConsumptionWeekday_SetProperty8.target = _arg1;
            }, "_WeeklyConsumptionWeekday_SetProperty8.target");
            result[10] = binding;
            binding = new Binding(this, function ():Object{
                return (drinks_label);
            }, function (_arg1:Object):void{
                _WeeklyConsumptionWeekday_SetProperty9.target = _arg1;
            }, "_WeeklyConsumptionWeekday_SetProperty9.target");
            result[11] = binding;
            binding = new Binding(this, function ():Object{
                return (this);
            }, function (_arg1:Object):void{
                _WeeklyConsumptionWeekday_SetProperty10.target = _arg1;
            }, "_WeeklyConsumptionWeekday_SetProperty10.target");
            result[12] = binding;
            binding = new Binding(this, function (){
                return ([defaultGlow]);
            }, function (_arg1):void{
                _WeeklyConsumptionWeekday_SetProperty10.value = _arg1;
            }, "_WeeklyConsumptionWeekday_SetProperty10.value");
            result[13] = binding;
            binding = new Binding(this, function ():Object{
                return (tickImage);
            }, function (_arg1:Object):void{
                _WeeklyConsumptionWeekday_SetProperty11.target = _arg1;
            }, "_WeeklyConsumptionWeekday_SetProperty11.target");
            result[14] = binding;
            binding = new Binding(this, function (){
                return (Assets.getInstance().tickGrey);
            }, function (_arg1):void{
                _WeeklyConsumptionWeekday_SetProperty11.value = _arg1;
            }, "_WeeklyConsumptionWeekday_SetProperty11.value");
            result[15] = binding;
            binding = new Binding(this, function ():Object{
                return (day);
            }, function (_arg1:Object):void{
                _WeeklyConsumptionWeekday_SetProperty12.target = _arg1;
            }, "_WeeklyConsumptionWeekday_SetProperty12.target");
            result[16] = binding;
            binding = new Binding(this, function ():Object{
                return (drinks);
            }, function (_arg1:Object):void{
                _WeeklyConsumptionWeekday_SetProperty13.target = _arg1;
            }, "_WeeklyConsumptionWeekday_SetProperty13.target");
            result[17] = binding;
            binding = new Binding(this, function ():Object{
                return (drinks_label);
            }, function (_arg1:Object):void{
                _WeeklyConsumptionWeekday_SetProperty14.target = _arg1;
            }, "_WeeklyConsumptionWeekday_SetProperty14.target");
            result[18] = binding;
            binding = new Binding(this, function ():Object{
                return (this);
            }, function (_arg1:Object):void{
                _WeeklyConsumptionWeekday_SetProperty15.target = _arg1;
            }, "_WeeklyConsumptionWeekday_SetProperty15.target");
            result[19] = binding;
            binding = new Binding(this, function (){
                return ([defaultGlow]);
            }, function (_arg1):void{
                _WeeklyConsumptionWeekday_SetProperty15.value = _arg1;
            }, "_WeeklyConsumptionWeekday_SetProperty15.value");
            result[20] = binding;
            binding = new Binding(this, function ():Object{
                return (this);
            }, function (_arg1:Object):void{
                _WeeklyConsumptionWeekday_SetProperty16.target = _arg1;
            }, "_WeeklyConsumptionWeekday_SetProperty16.target");
            result[21] = binding;
            binding = new Binding(this, function ():Boolean{
                return (((consumption.Modified) || (consumption.Closed)));
            }, function (_arg1:Boolean):void{
                tickImage.visible = _arg1;
            }, "tickImage.visible");
            result[22] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = model.languageVO.getLang(consumption.DayOfWeek).toUpperCase();
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                day.text = _arg1;
            }, "day.text");
            result[23] = binding;
            binding = new Binding(this, function ():Boolean{
                return (((consumption.Modified) || (consumption.Closed)));
            }, function (_arg1:Boolean):void{
                drinks.visible = _arg1;
            }, "drinks.visible");
            result[24] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = _reportedValues.getItemAt(3).Value;
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                drinks.text = _arg1;
            }, "drinks.text");
            result[25] = binding;
            binding = new Binding(this, function ():Boolean{
                return (false);
            }, function (_arg1:Boolean):void{
                _WeeklyConsumptionWeekday_Label3.visible = _arg1;
            }, "_WeeklyConsumptionWeekday_Label3.visible");
            result[26] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = drinks_string;
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                drinks_label.text = _arg1;
            }, "drinks_label.text");
            result[27] = binding;
            binding = new Binding(this, function ():Boolean{
                return (drinks.visible);
            }, function (_arg1:Boolean):void{
                drinks_label.visible = _arg1;
            }, "drinks_label.visible");
            result[28] = binding;
            return (result);
        }
        public function get tickImage():Image{
            return (this._97197634tickImage);
        }
        public function get selectedGlow():GlowFilter{
            return (this._1754851640selectedGlow);
        }
        private function getMonthDateString(_arg1:Date):String{
            return (((((_arg1.getDate() + " ") + model.languageVO.getLang(DateUtils.getMonthFromIndex(_arg1.getMonth()))) + " ") + _arg1.getFullYear()));
        }
        private function _WeeklyConsumptionWeekday_State1_c():State{
            var _local1:State = new State();
            _local1.name = "selected";
            _local1.overrides = [_WeeklyConsumptionWeekday_SetProperty1_i(), _WeeklyConsumptionWeekday_SetProperty2_i(), _WeeklyConsumptionWeekday_SetProperty3_i(), _WeeklyConsumptionWeekday_SetProperty4_i(), _WeeklyConsumptionWeekday_SetProperty5_i()];
            return (_local1);
        }
        public function get drinks():Label{
            return (this._1323533605drinks);
        }
        private function _WeeklyConsumptionWeekday_SetProperty1_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _WeeklyConsumptionWeekday_SetProperty1 = _local1;
            _local1.name = "source";
            BindingManager.executeBindings(this, "_WeeklyConsumptionWeekday_SetProperty1", _WeeklyConsumptionWeekday_SetProperty1);
            return (_local1);
        }
        private function _WeeklyConsumptionWeekday_SetProperty9_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _WeeklyConsumptionWeekday_SetProperty9 = _local1;
            _local1.name = "styleName";
            _local1.value = "weekday";
            BindingManager.executeBindings(this, "_WeeklyConsumptionWeekday_SetProperty9", _WeeklyConsumptionWeekday_SetProperty9);
            return (_local1);
        }
        private function _WeeklyConsumptionWeekday_SetProperty5_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _WeeklyConsumptionWeekday_SetProperty5 = _local1;
            _local1.name = "filters";
            BindingManager.executeBindings(this, "_WeeklyConsumptionWeekday_SetProperty5", _WeeklyConsumptionWeekday_SetProperty5);
            return (_local1);
        }
        private function set drinks_string(_arg1:String):void{
            var _local2:Object = this._1330838027drinks_string;
            if (_local2 !== _arg1){
                this._1330838027drinks_string = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "drinks_string", _local2, _arg1));
            };
        }
        private function rollOver(_arg1:MouseEvent):void{
            currentState = "selected";
        }
        private function set _reportedValues(_arg1:ArrayCollection):void{
            var _local2:Object = this._696955380_reportedValues;
            if (_local2 !== _arg1){
                this._696955380_reportedValues = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "_reportedValues", _local2, _arg1));
            };
        }
        private function _WeeklyConsumptionWeekday_SetProperty13_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _WeeklyConsumptionWeekday_SetProperty13 = _local1;
            _local1.name = "styleName";
            _local1.value = "drinks";
            BindingManager.executeBindings(this, "_WeeklyConsumptionWeekday_SetProperty13", _WeeklyConsumptionWeekday_SetProperty13);
            return (_local1);
        }
        private function get drinks_string():String{
            return (this._1330838027drinks_string);
        }
        public function set defaultGlow(_arg1:GlowFilter):void{
            var _local2:Object = this._437340306defaultGlow;
            if (_local2 !== _arg1){
                this._437340306defaultGlow = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "defaultGlow", _local2, _arg1));
            };
        }

        public static function set watcherSetupUtil(_arg1:IWatcherSetupUtil):void{
            WeeklyConsumptionWeekday._watcherSetupUtil = _arg1;
        }

    }
}//package com.redbox.changetech.view.components 
