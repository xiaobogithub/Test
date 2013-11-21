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
    import mx.containers.*;
    import com.redbox.changetech.model.*;
    import com.redbox.changetech.vo.*;
    import flash.utils.*;
    import mx.collections.*;
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

    public class InitialPlanWeekday extends GradientCanvas implements IBindingClient {

        private var _1896874934_isEditMode:Boolean = false
        private var _plan:Consumption
        private var _1844825299newGoal:Text
        private var _1827565232_target:Number
        private var _476684794_continue:Boolean
        private var _documentDescriptor_:UIComponentDescriptor
        mx_internal var _watchers:Array
        private var _99228day:Label
        private var model:BalanceModelLocator
        private var _2055659505goalTotal:Number
        private var _548231726targetSlider:BalanceSlider
        public var _InitialPlanWeekday_Spacer1:Spacer
        private var _1754851640selectedGlow:GlowFilter
        private var _437340306defaultGlow:GlowFilter
        mx_internal var _bindingsBeginWithWord:Object
        mx_internal var _bindingsByDestination:Object
        public var _InitialPlanWeekday_SetProperty1:SetProperty
        public var _InitialPlanWeekday_SetProperty2:SetProperty
        private var _consumption:Consumption
        mx_internal var _bindings:Array
        private var _696955380_reportedValues:ArrayCollection
        private var _95845008drank:Text
        private var _1464648123_total:Number

        private static var _watcherSetupUtil:IWatcherSetupUtil;

        public function InitialPlanWeekday(){
            _documentDescriptor_ = new UIComponentDescriptor({type:GradientCanvas, propertiesFactory:function ():Object{
                return ({width:150, height:210, childDescriptors:[new UIComponentDescriptor({type:VBox, stylesFactory:function ():void{
                    this.paddingLeft = 5;
                    this.paddingTop = 5;
                    this.verticalGap = 3;
                }, propertiesFactory:function ():Object{
                    return ({childDescriptors:[new UIComponentDescriptor({type:Label, id:"day", propertiesFactory:function ():Object{
                        return ({styleName:"weekday"});
                    }}), new UIComponentDescriptor({type:HRule, stylesFactory:function ():void{
                        this.strokeWidth = 1;
                        this.strokeColor = 0xDADADA;
                    }, propertiesFactory:function ():Object{
                        return ({percentWidth:100});
                    }}), new UIComponentDescriptor({type:Text, id:"drank", stylesFactory:function ():void{
                        this.color = 2263998;
                        this.fontFamily = "Helvetica Neue";
                        this.fontWeight = "bold";
                        this.fontSize = 12;
                    }, propertiesFactory:function ():Object{
                        return ({condenseWhite:true});
                    }}), new UIComponentDescriptor({type:HRule, stylesFactory:function ():void{
                        this.strokeWidth = 1;
                        this.strokeColor = 0xDADADA;
                    }, propertiesFactory:function ():Object{
                        return ({percentWidth:100});
                    }}), new UIComponentDescriptor({type:Text, id:"newGoal", stylesFactory:function ():void{
                        this.fontFamily = "Helvetica Neue";
                        this.fontWeight = "bold";
                        this.fontSize = 12;
                        this.color = 8892465;
                    }, propertiesFactory:function ():Object{
                        return ({condenseWhite:true});
                    }}), new UIComponentDescriptor({type:Spacer, id:"_InitialPlanWeekday_Spacer1", propertiesFactory:function ():Object{
                        return ({height:10});
                    }}), new UIComponentDescriptor({type:BalanceSlider, id:"targetSlider", propertiesFactory:function ():Object{
                        return ({width:140, height:50, offset:20, bgHeight:30, fillColors:[0xCFCFCF, 0xFFFFFF], fillAlphas:[0, 0], textColor:8892465, x:0, y:150});
                    }})]});
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
                this.fillColors = [0xFFFFFF, 0xCFCFCF];
                this.fillAlphas = [1, 1];
                this.gradientRatio = [0, 0xFF];
                this.angle = [90];
                this.borderAlphas = [0];
                this.cornerRadius = 5;
            };
            this.width = 150;
            this.height = 210;
            this.colorsConfiguration = [2];
            this.horizontalScrollPolicy = "off";
            this.verticalScrollPolicy = "off";
            this.currentState = "default";
            this.states = [_InitialPlanWeekday_State1_c(), _InitialPlanWeekday_State2_c()];
            this.filters = [_InitialPlanWeekday_DropShadowFilter1_c(), _InitialPlanWeekday_GlowFilter1_i(), _InitialPlanWeekday_GlowFilter2_i()];
        }
        private function get _reportedValues():ArrayCollection{
            return (this._696955380_reportedValues);
        }
        private function set _isEditMode(_arg1:Boolean):void{
            var _local2:Object = this._1896874934_isEditMode;
            if (_local2 !== _arg1){
                this._1896874934_isEditMode = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "_isEditMode", _local2, _arg1));
            };
        }
        private function set _848170085consumption(_arg1:Consumption):void{
            _consumption = _arg1;
        }
        private function _InitialPlanWeekday_SetProperty1_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _InitialPlanWeekday_SetProperty1 = _local1;
            _local1.name = "filters";
            BindingManager.executeBindings(this, "_InitialPlanWeekday_SetProperty1", _InitialPlanWeekday_SetProperty1);
            return (_local1);
        }
        private function _InitialPlanWeekday_State2_c():State{
            var _local1:State = new State();
            _local1.name = "default";
            _local1.overrides = [_InitialPlanWeekday_SetProperty2_i()];
            return (_local1);
        }
        private function set _continue(_arg1:Boolean):void{
            var _local2:Object = this._476684794_continue;
            if (_local2 !== _arg1){
                this._476684794_continue = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "_continue", _local2, _arg1));
            };
        }
        private function get _continue():Boolean{
            return (this._476684794_continue);
        }
        private function rollOut(_arg1:MouseEvent):void{
            currentState = "default";
        }
        private function sliderChanged(_arg1:SliderEvent):void{
            plan.Modified = true;
            plan.PlanValue = Math.round(BalanceSlider(_arg1.target).slide.value);
        }
        override public function initialize():void{
            var target:* = null;
            var watcherSetupUtilClass:* = null;
            mx_internal::setDocumentDescriptor(_documentDescriptor_);
            var bindings:* = _InitialPlanWeekday_bindingsSetup();
            var watchers:* = [];
            target = this;
            if (_watcherSetupUtil == null){
                watcherSetupUtilClass = getDefinitionByName("_com_redbox_changetech_view_components_InitialPlanWeekdayWatcherSetupUtil");
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
        private function _InitialPlanWeekday_GlowFilter2_i():GlowFilter{
            var _local1:GlowFilter = new GlowFilter();
            selectedGlow = _local1;
            _local1.color = 2729955;
            _local1.blurX = 7;
            _local1.blurY = 7;
            return (_local1);
        }
        public function get consumption():Consumption{
            return (_consumption);
        }
        public function get day():Label{
            return (this._99228day);
        }
        public function get isEditMode():Boolean{
            return (_isEditMode);
        }
        public function set drank(_arg1:Text):void{
            var _local2:Object = this._95845008drank;
            if (_local2 !== _arg1){
                this._95845008drank = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "drank", _local2, _arg1));
            };
        }
        private function set _total(_arg1:Number):void{
            var _local2:Object = this._1464648123_total;
            if (_local2 !== _arg1){
                this._1464648123_total = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "_total", _local2, _arg1));
            };
        }
        private function _InitialPlanWeekday_bindingExprs():void{
            var _local1:*;
            _local1 = this;
            _local1 = [selectedGlow];
            _local1 = this;
            _local1 = [defaultGlow];
            _local1 = model.languageVO.getLang(consumption.DayOfWeek);
            _local1 = ((((model.languageVO.getLang("drank") + ": ") + consumption.total(false)) + " ") + model.languageVO.getLang("Drinks"));
            _local1 = ((((model.languageVO.getLang("goal") + ": ") + plan.PlanValue) + " ") + model.languageVO.getLang("Drinks"));
            _local1 = isEditMode;
            _local1 = isEditMode;
            _local1 = plan.PlanValue;
            _local1 = isEditMode;
        }
        public function set newGoal(_arg1:Text):void{
            var _local2:Object = this._1844825299newGoal;
            if (_local2 !== _arg1){
                this._1844825299newGoal = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "newGoal", _local2, _arg1));
            };
        }
        public function get plan():Consumption{
            return (_plan);
        }
        private function _InitialPlanWeekday_bindingsSetup():Array{
            var binding:* = null;
            var result:* = [];
            binding = new Binding(this, function ():Object{
                return (this);
            }, function (_arg1:Object):void{
                _InitialPlanWeekday_SetProperty1.target = _arg1;
            }, "_InitialPlanWeekday_SetProperty1.target");
            result[0] = binding;
            binding = new Binding(this, function (){
                return ([selectedGlow]);
            }, function (_arg1):void{
                _InitialPlanWeekday_SetProperty1.value = _arg1;
            }, "_InitialPlanWeekday_SetProperty1.value");
            result[1] = binding;
            binding = new Binding(this, function ():Object{
                return (this);
            }, function (_arg1:Object):void{
                _InitialPlanWeekday_SetProperty2.target = _arg1;
            }, "_InitialPlanWeekday_SetProperty2.target");
            result[2] = binding;
            binding = new Binding(this, function (){
                return ([defaultGlow]);
            }, function (_arg1):void{
                _InitialPlanWeekday_SetProperty2.value = _arg1;
            }, "_InitialPlanWeekday_SetProperty2.value");
            result[3] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = model.languageVO.getLang(consumption.DayOfWeek);
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                day.text = _arg1;
            }, "day.text");
            result[4] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = ((((model.languageVO.getLang("drank") + ": ") + consumption.total(false)) + " ") + model.languageVO.getLang("Drinks"));
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                drank.htmlText = _arg1;
            }, "drank.htmlText");
            result[5] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = ((((model.languageVO.getLang("goal") + ": ") + plan.PlanValue) + " ") + model.languageVO.getLang("Drinks"));
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                newGoal.htmlText = _arg1;
            }, "newGoal.htmlText");
            result[6] = binding;
            binding = new Binding(this, function ():Boolean{
                return (isEditMode);
            }, function (_arg1:Boolean):void{
                newGoal.visible = _arg1;
            }, "newGoal.visible");
            result[7] = binding;
            binding = new Binding(this, function ():Boolean{
                return (isEditMode);
            }, function (_arg1:Boolean):void{
                _InitialPlanWeekday_Spacer1.visible = _arg1;
            }, "_InitialPlanWeekday_Spacer1.visible");
            result[8] = binding;
            binding = new Binding(this, function ():Number{
                return (plan.PlanValue);
            }, function (_arg1:Number):void{
                targetSlider.sliderValue = _arg1;
            }, "targetSlider.sliderValue");
            result[9] = binding;
            binding = new Binding(this, function ():Boolean{
                return (isEditMode);
            }, function (_arg1:Boolean):void{
                targetSlider.visible = _arg1;
            }, "targetSlider.visible");
            result[10] = binding;
            return (result);
        }
        public function set targetSlider(_arg1:BalanceSlider):void{
            var _local2:Object = this._548231726targetSlider;
            if (_local2 !== _arg1){
                this._548231726targetSlider = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "targetSlider", _local2, _arg1));
            };
        }
        public function set isEditMode(_arg1:Boolean):void{
            var _local2:Object = this.isEditMode;
            if (_local2 !== _arg1){
                this._808562199isEditMode = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "isEditMode", _local2, _arg1));
            };
        }
        public function get drank():Text{
            return (this._95845008drank);
        }
        private function initialise():void{
            if (!hasEventListener(MouseEvent.ROLL_OVER)){
                addEventListener(MouseEvent.ROLL_OVER, rollOver);
            };
            if (!hasEventListener(MouseEvent.ROLL_OUT)){
                addEventListener(MouseEvent.ROLL_OUT, rollOut);
            };
            targetSlider.addEventListener(SliderEvent.CHANGE, sliderChanged);
            targetSlider.addEventListener(SliderEvent.THUMB_RELEASE, sliderChanged);
            goalTotal = plan.PlanValue;
        }
        public function set selectedGlow(_arg1:GlowFilter):void{
            var _local2:Object = this._1754851640selectedGlow;
            if (_local2 !== _arg1){
                this._1754851640selectedGlow = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "selectedGlow", _local2, _arg1));
            };
        }
        private function set _808562199isEditMode(_arg1:Boolean):void{
            _isEditMode = _arg1;
        }
        public function get defaultGlow():GlowFilter{
            return (this._437340306defaultGlow);
        }
        public function get selectedGlow():GlowFilter{
            return (this._1754851640selectedGlow);
        }
        private function _InitialPlanWeekday_State1_c():State{
            var _local1:State = new State();
            _local1.name = "selected";
            _local1.overrides = [_InitialPlanWeekday_SetProperty1_i()];
            return (_local1);
        }
        private function get _isEditMode():Boolean{
            return (this._1896874934_isEditMode);
        }
        private function set _3443497plan(_arg1:Consumption):void{
            _plan = _arg1;
            initialise();
        }
        private function _InitialPlanWeekday_SetProperty2_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _InitialPlanWeekday_SetProperty2 = _local1;
            _local1.name = "filters";
            BindingManager.executeBindings(this, "_InitialPlanWeekday_SetProperty2", _InitialPlanWeekday_SetProperty2);
            return (_local1);
        }
        public function get newGoal():Text{
            return (this._1844825299newGoal);
        }
        private function get _total():Number{
            return (this._1464648123_total);
        }
        private function _InitialPlanWeekday_GlowFilter1_i():GlowFilter{
            var _local1:GlowFilter = new GlowFilter();
            defaultGlow = _local1;
            _local1.color = 14342872;
            _local1.blurX = 7;
            _local1.blurY = 7;
            return (_local1);
        }
        private function get _target():Number{
            return (this._1827565232_target);
        }
        public function get targetSlider():BalanceSlider{
            return (this._548231726targetSlider);
        }
        private function set goalTotal(_arg1:Number):void{
            var _local2:Object = this._2055659505goalTotal;
            if (_local2 !== _arg1){
                this._2055659505goalTotal = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "goalTotal", _local2, _arg1));
            };
        }
        public function set day(_arg1:Label):void{
            var _local2:Object = this._99228day;
            if (_local2 !== _arg1){
                this._99228day = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "day", _local2, _arg1));
            };
        }
        private function set _target(_arg1:Number):void{
            var _local2:Object = this._1827565232_target;
            if (_local2 !== _arg1){
                this._1827565232_target = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "_target", _local2, _arg1));
            };
        }
        private function _InitialPlanWeekday_DropShadowFilter1_c():DropShadowFilter{
            var _local1:DropShadowFilter = new DropShadowFilter();
            _local1.distance = 2;
            _local1.alpha = 0.1;
            return (_local1);
        }
        public function set plan(_arg1:Consumption):void{
            var _local2:Object = this.plan;
            if (_local2 !== _arg1){
                this._3443497plan = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "plan", _local2, _arg1));
            };
        }
        private function set _reportedValues(_arg1:ArrayCollection):void{
            var _local2:Object = this._696955380_reportedValues;
            if (_local2 !== _arg1){
                this._696955380_reportedValues = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "_reportedValues", _local2, _arg1));
            };
        }
        private function rollOver(_arg1:MouseEvent):void{
            currentState = "selected";
        }
        public function set defaultGlow(_arg1:GlowFilter):void{
            var _local2:Object = this._437340306defaultGlow;
            if (_local2 !== _arg1){
                this._437340306defaultGlow = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "defaultGlow", _local2, _arg1));
            };
        }
        private function get goalTotal():Number{
            return (this._2055659505goalTotal);
        }
        public function set consumption(_arg1:Consumption):void{
            var _local2:Object = this.consumption;
            if (_local2 !== _arg1){
                this._848170085consumption = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "consumption", _local2, _arg1));
            };
        }

        public static function set watcherSetupUtil(_arg1:IWatcherSetupUtil):void{
            InitialPlanWeekday._watcherSetupUtil = _arg1;
        }

    }
}//package com.redbox.changetech.view.components 
