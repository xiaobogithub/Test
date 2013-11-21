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
    import com.redbox.changetech.control.*;
    import mx.containers.*;
    import com.redbox.changetech.view.components.*;
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
    import com.redbox.changetech.view.modules.*;
    import flash.filters.*;
    import flash.ui.*;
    import com.redbox.changetech.util.*;
    import flash.external.*;
    import flash.debugger.*;
    import flash.errors.*;
    import flash.printing.*;
    import flash.profiler.*;

    public class PersonalPlan extends ModuleViewTemplate implements IBindingClient {

        private var _868055995topRow:HBox
        private var _1572055514thursday:InitialPlanWeekday
        private var _1082042285cta_btn:BalanceButtonReflectionCanvas
        private var _1354958366_consumptionValues:ArrayCollection
        mx_internal var _watchers:Array
        private var _1567139658_planValues:ArrayCollection
        private var _891186736sunday:InitialPlanWeekday
        private var _1646101571leftsideContainer:VBox
        mx_internal var _bindingsByDestination:Object
        private var _1549852825transContainer2:Canvas
        mx_internal var _bindingsBeginWithWord:Object
        private var storeEvent:Event
        private var _1878076494rightsideContainer:VBox
        private var _1393530710wednesday:InitialPlanWeekday
        private var _1549852824transContainer1:Canvas
        mx_internal var _bindings:Array
        private var _1266285217friday:InitialPlanWeekday
        private var _1073910925infoContainer:BalanceCustomContentReflectionCanvas
        private var _documentDescriptor_:UIComponentDescriptor
        private var _2114201671saturday:InitialPlanWeekday
        private var _1855392849bottomRow:HBox
        private var _1068502768monday:InitialPlanWeekday
        private var _977343923tuesday:InitialPlanWeekday

        private static var _watcherSetupUtil:IWatcherSetupUtil;

        public function PersonalPlan(){
            _documentDescriptor_ = new UIComponentDescriptor({type:ModuleViewTemplate, propertiesFactory:function ():Object{
                return ({childDescriptors:[new UIComponentDescriptor({type:Canvas, id:"transContainer1", propertiesFactory:function ():Object{
                    return ({horizontalScrollPolicy:"off", verticalScrollPolicy:"off", childDescriptors:[new UIComponentDescriptor({type:VBox, id:"leftsideContainer", stylesFactory:function ():void{
                        this.verticalGap = 10;
                        this.horizontalAlign = "center";
                    }, propertiesFactory:function ():Object{
                        return ({x:60, y:20, percentWidth:80, childDescriptors:[new UIComponentDescriptor({type:HBox, id:"topRow", stylesFactory:function ():void{
                            this.horizontalGap = 10;
                        }, propertiesFactory:function ():Object{
                            return ({childDescriptors:[new UIComponentDescriptor({type:InitialPlanWeekday, id:"monday", propertiesFactory:function ():Object{
                                return ({isEditMode:true});
                            }}), new UIComponentDescriptor({type:InitialPlanWeekday, id:"tuesday", propertiesFactory:function ():Object{
                                return ({isEditMode:true});
                            }}), new UIComponentDescriptor({type:InitialPlanWeekday, id:"wednesday", propertiesFactory:function ():Object{
                                return ({isEditMode:true});
                            }}), new UIComponentDescriptor({type:InitialPlanWeekday, id:"thursday", propertiesFactory:function ():Object{
                                return ({isEditMode:true});
                            }})]});
                        }}), new UIComponentDescriptor({type:HBox, id:"bottomRow", stylesFactory:function ():void{
                            this.horizontalGap = 10;
                        }, propertiesFactory:function ():Object{
                            return ({childDescriptors:[new UIComponentDescriptor({type:InitialPlanWeekday, id:"friday", propertiesFactory:function ():Object{
                                return ({isEditMode:true});
                            }}), new UIComponentDescriptor({type:InitialPlanWeekday, id:"saturday", propertiesFactory:function ():Object{
                                return ({isEditMode:true});
                            }}), new UIComponentDescriptor({type:InitialPlanWeekday, id:"sunday", propertiesFactory:function ():Object{
                                return ({isEditMode:true});
                            }})]});
                        }})]});
                    }})]});
                }}), new UIComponentDescriptor({type:Canvas, id:"transContainer2", propertiesFactory:function ():Object{
                    return ({horizontalScrollPolicy:"off", verticalScrollPolicy:"off", childDescriptors:[new UIComponentDescriptor({type:VBox, id:"rightsideContainer", propertiesFactory:function ():Object{
                        return ({horizontalScrollPolicy:"off", verticalScrollPolicy:"off", clipContent:false, childDescriptors:[new UIComponentDescriptor({type:BalanceCustomContentReflectionCanvas, id:"infoContainer", propertiesFactory:function ():Object{
                            return ({width:310});
                        }}), new UIComponentDescriptor({type:BalanceButtonReflectionCanvas, id:"cta_btn"})]});
                    }})]});
                }})]});
            }});
            _1354958366_consumptionValues = new ArrayCollection(BalanceModelLocator.getInstance().consumer.ReportedUsage);
            _1567139658_planValues = new ArrayCollection(BalanceModelLocator.getInstance().consumer.ReductionPlan);
            _bindings = [];
            _watchers = [];
            _bindingsByDestination = {};
            _bindingsBeginWithWord = {};
            super();
            mx_internal::_document = this;
            this.verticalScrollPolicy = "on";
            this.addEventListener("creationComplete", ___PersonalPlan_ModuleViewTemplate1_creationComplete);
        }
        public function set saturday(_arg1:InitialPlanWeekday):void{
            var _local2:Object = this._2114201671saturday;
            if (_local2 !== _arg1){
                this._2114201671saturday = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "saturday", _local2, _arg1));
            };
        }
        public function set leftsideContainer(_arg1:VBox):void{
            var _local2:Object = this._1646101571leftsideContainer;
            if (_local2 !== _arg1){
                this._1646101571leftsideContainer = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "leftsideContainer", _local2, _arg1));
            };
        }
        private function checkNextPressed(_arg1:Event):void{
            storeEvent = _arg1;
            _arg1.stopPropagation();
            var _local2:CairngormEvent = new CairngormEvent(BalanceController.SET_PLAN);
            _local2.data = new Object();
            _local2.data.callback = dispatchStoredNextEvent;
            _local2.dispatch();
        }
        public function set topRow(_arg1:HBox):void{
            var _local2:Object = this._868055995topRow;
            if (_local2 !== _arg1){
                this._868055995topRow = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "topRow", _local2, _arg1));
            };
        }
        override public function initialize():void{
            var target:* = null;
            var watcherSetupUtilClass:* = null;
            mx_internal::setDocumentDescriptor(_documentDescriptor_);
            var bindings:* = _PersonalPlan_bindingsSetup();
            var watchers:* = [];
            target = this;
            if (_watcherSetupUtil == null){
                watcherSetupUtilClass = getDefinitionByName("_com_redbox_changetech_view_templates_PersonalPlanWatcherSetupUtil");
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
        private function _PersonalPlan_bindingsSetup():Array{
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
            binding = new Binding(this, function ():Consumption{
                return ((_consumptionValues.getItemAt(0) as Consumption));
            }, function (_arg1:Consumption):void{
                monday.consumption = _arg1;
            }, "monday.consumption");
            result[2] = binding;
            binding = new Binding(this, function ():Consumption{
                return ((_planValues.getItemAt(0) as Consumption));
            }, function (_arg1:Consumption):void{
                monday.plan = _arg1;
            }, "monday.plan");
            result[3] = binding;
            binding = new Binding(this, function ():Consumption{
                return ((_consumptionValues.getItemAt(1) as Consumption));
            }, function (_arg1:Consumption):void{
                tuesday.consumption = _arg1;
            }, "tuesday.consumption");
            result[4] = binding;
            binding = new Binding(this, function ():Consumption{
                return ((_planValues.getItemAt(1) as Consumption));
            }, function (_arg1:Consumption):void{
                tuesday.plan = _arg1;
            }, "tuesday.plan");
            result[5] = binding;
            binding = new Binding(this, function ():Consumption{
                return ((_consumptionValues.getItemAt(2) as Consumption));
            }, function (_arg1:Consumption):void{
                wednesday.consumption = _arg1;
            }, "wednesday.consumption");
            result[6] = binding;
            binding = new Binding(this, function ():Consumption{
                return ((_planValues.getItemAt(2) as Consumption));
            }, function (_arg1:Consumption):void{
                wednesday.plan = _arg1;
            }, "wednesday.plan");
            result[7] = binding;
            binding = new Binding(this, function ():Consumption{
                return ((_consumptionValues.getItemAt(3) as Consumption));
            }, function (_arg1:Consumption):void{
                thursday.consumption = _arg1;
            }, "thursday.consumption");
            result[8] = binding;
            binding = new Binding(this, function ():Consumption{
                return ((_planValues.getItemAt(3) as Consumption));
            }, function (_arg1:Consumption):void{
                thursday.plan = _arg1;
            }, "thursday.plan");
            result[9] = binding;
            binding = new Binding(this, function ():Consumption{
                return ((_consumptionValues.getItemAt(4) as Consumption));
            }, function (_arg1:Consumption):void{
                friday.consumption = _arg1;
            }, "friday.consumption");
            result[10] = binding;
            binding = new Binding(this, function ():Consumption{
                return ((_planValues.getItemAt(4) as Consumption));
            }, function (_arg1:Consumption):void{
                friday.plan = _arg1;
            }, "friday.plan");
            result[11] = binding;
            binding = new Binding(this, function ():Consumption{
                return ((_consumptionValues.getItemAt(5) as Consumption));
            }, function (_arg1:Consumption):void{
                saturday.consumption = _arg1;
            }, "saturday.consumption");
            result[12] = binding;
            binding = new Binding(this, function ():Consumption{
                return ((_planValues.getItemAt(5) as Consumption));
            }, function (_arg1:Consumption):void{
                saturday.plan = _arg1;
            }, "saturday.plan");
            result[13] = binding;
            binding = new Binding(this, function ():Consumption{
                return ((_consumptionValues.getItemAt(6) as Consumption));
            }, function (_arg1:Consumption):void{
                sunday.consumption = _arg1;
            }, "sunday.consumption");
            result[14] = binding;
            binding = new Binding(this, function ():Consumption{
                return ((_planValues.getItemAt(6) as Consumption));
            }, function (_arg1:Consumption):void{
                sunday.plan = _arg1;
            }, "sunday.plan");
            result[15] = binding;
            binding = new Binding(this, function ():Number{
                return (model.currentStageWidth);
            }, function (_arg1:Number):void{
                transContainer2.width = _arg1;
            }, "transContainer2.width");
            result[16] = binding;
            binding = new Binding(this, function ():Number{
                return (model.currentStageHeight);
            }, function (_arg1:Number):void{
                transContainer2.height = _arg1;
            }, "transContainer2.height");
            result[17] = binding;
            binding = new Binding(this, function ():Number{
                return ((sunday.x + 550));
            }, function (_arg1:Number):void{
                rightsideContainer.x = _arg1;
            }, "rightsideContainer.x");
            result[18] = binding;
            binding = new Binding(this, function ():Number{
                return ((bottomRow.y + 15));
            }, function (_arg1:Number):void{
                rightsideContainer.y = _arg1;
            }, "rightsideContainer.y");
            result[19] = binding;
            binding = new Binding(this, function ():Number{
                return ((((infoContainer.height / 2) + 40) * -1));
            }, function (_arg1:Number):void{
                rightsideContainer.setStyle("verticalGap", _arg1);
            }, "rightsideContainer.verticalGap");
            result[20] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = content.Title;
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                infoContainer.title_str = _arg1;
            }, "infoContainer.title_str");
            result[21] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = SerializeContentXML.convertToHTMLText(content.TextLayout);
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                infoContainer.copy_str = _arg1;
            }, "infoContainer.copy_str");
            result[22] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = content.getCTAButton().Label;
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                cta_btn.buttonLabel = _arg1;
            }, "cta_btn.buttonLabel");
            result[23] = binding;
            return (result);
        }
        public function get transContainer2():Canvas{
            return (this._1549852825transContainer2);
        }
        public function set tuesday(_arg1:InitialPlanWeekday):void{
            var _local2:Object = this._977343923tuesday;
            if (_local2 !== _arg1){
                this._977343923tuesday = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "tuesday", _local2, _arg1));
            };
        }
        public function set rightsideContainer(_arg1:VBox):void{
            var _local2:Object = this._1878076494rightsideContainer;
            if (_local2 !== _arg1){
                this._1878076494rightsideContainer = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "rightsideContainer", _local2, _arg1));
            };
        }
        private function get _consumptionValues():ArrayCollection{
            return (this._1354958366_consumptionValues);
        }
        private function init(_arg1:FlexEvent):void{
            BindingUtils.bindProperty(cta_btn.cta_btn, "enabled", module, "mandatoryQuestionsComplete");
            addEventListener(BalanceButton.EVENT_CLICKED, checkNextPressed);
        }
        public function set monday(_arg1:InitialPlanWeekday):void{
            var _local2:Object = this._1068502768monday;
            if (_local2 !== _arg1){
                this._1068502768monday = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "monday", _local2, _arg1));
            };
        }
        private function set _planValues(_arg1:ArrayCollection):void{
            var _local2:Object = this._1567139658_planValues;
            if (_local2 !== _arg1){
                this._1567139658_planValues = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "_planValues", _local2, _arg1));
            };
        }
        public function get cta_btn():BalanceButtonReflectionCanvas{
            return (this._1082042285cta_btn);
        }
        public function get bottomRow():HBox{
            return (this._1855392849bottomRow);
        }
        public function get transContainer1():Canvas{
            return (this._1549852824transContainer1);
        }
        private function dispatchStoredNextEvent():void{
            BasicModule(module).outro();
        }
        public function get monday():InitialPlanWeekday{
            return (this._1068502768monday);
        }
        public function set thursday(_arg1:InitialPlanWeekday):void{
            var _local2:Object = this._1572055514thursday;
            if (_local2 !== _arg1){
                this._1572055514thursday = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "thursday", _local2, _arg1));
            };
        }
        public function set transContainer1(_arg1:Canvas):void{
            var _local2:Object = this._1549852824transContainer1;
            if (_local2 !== _arg1){
                this._1549852824transContainer1 = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "transContainer1", _local2, _arg1));
            };
        }
        public function set sunday(_arg1:InitialPlanWeekday):void{
            var _local2:Object = this._891186736sunday;
            if (_local2 !== _arg1){
                this._891186736sunday = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "sunday", _local2, _arg1));
            };
        }
        private function set _consumptionValues(_arg1:ArrayCollection):void{
            var _local2:Object = this._1354958366_consumptionValues;
            if (_local2 !== _arg1){
                this._1354958366_consumptionValues = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "_consumptionValues", _local2, _arg1));
            };
        }
        public function set friday(_arg1:InitialPlanWeekday):void{
            var _local2:Object = this._1266285217friday;
            if (_local2 !== _arg1){
                this._1266285217friday = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "friday", _local2, _arg1));
            };
        }
        public function set transContainer2(_arg1:Canvas):void{
            var _local2:Object = this._1549852825transContainer2;
            if (_local2 !== _arg1){
                this._1549852825transContainer2 = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "transContainer2", _local2, _arg1));
            };
        }
        public function get leftsideContainer():VBox{
            return (this._1646101571leftsideContainer);
        }
        public function set wednesday(_arg1:InitialPlanWeekday):void{
            var _local2:Object = this._1393530710wednesday;
            if (_local2 !== _arg1){
                this._1393530710wednesday = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "wednesday", _local2, _arg1));
            };
        }
        public function get topRow():HBox{
            return (this._868055995topRow);
        }
        public function get tuesday():InitialPlanWeekday{
            return (this._977343923tuesday);
        }
        public function get rightsideContainer():VBox{
            return (this._1878076494rightsideContainer);
        }
        private function get _planValues():ArrayCollection{
            return (this._1567139658_planValues);
        }
        public function get saturday():InitialPlanWeekday{
            return (this._2114201671saturday);
        }
        public function set bottomRow(_arg1:HBox):void{
            var _local2:Object = this._1855392849bottomRow;
            if (_local2 !== _arg1){
                this._1855392849bottomRow = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "bottomRow", _local2, _arg1));
            };
        }
        public function get friday():InitialPlanWeekday{
            return (this._1266285217friday);
        }
        public function set cta_btn(_arg1:BalanceButtonReflectionCanvas):void{
            var _local2:Object = this._1082042285cta_btn;
            if (_local2 !== _arg1){
                this._1082042285cta_btn = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "cta_btn", _local2, _arg1));
            };
        }
        public function get wednesday():InitialPlanWeekday{
            return (this._1393530710wednesday);
        }
        public function set infoContainer(_arg1:BalanceCustomContentReflectionCanvas):void{
            var _local2:Object = this._1073910925infoContainer;
            if (_local2 !== _arg1){
                this._1073910925infoContainer = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "infoContainer", _local2, _arg1));
            };
        }
        public function ___PersonalPlan_ModuleViewTemplate1_creationComplete(_arg1:FlexEvent):void{
            init(_arg1);
        }
        private function initBounceEffect(_arg1:EffectEvent):void{
        }
        public function get infoContainer():BalanceCustomContentReflectionCanvas{
            return (this._1073910925infoContainer);
        }
        public function get thursday():InitialPlanWeekday{
            return (this._1572055514thursday);
        }
        public function get sunday():InitialPlanWeekday{
            return (this._891186736sunday);
        }
        private function _PersonalPlan_bindingExprs():void{
            var _local1:*;
            _local1 = model.currentStageWidth;
            _local1 = model.currentStageHeight;
            _local1 = (_consumptionValues.getItemAt(0) as Consumption);
            _local1 = (_planValues.getItemAt(0) as Consumption);
            _local1 = (_consumptionValues.getItemAt(1) as Consumption);
            _local1 = (_planValues.getItemAt(1) as Consumption);
            _local1 = (_consumptionValues.getItemAt(2) as Consumption);
            _local1 = (_planValues.getItemAt(2) as Consumption);
            _local1 = (_consumptionValues.getItemAt(3) as Consumption);
            _local1 = (_planValues.getItemAt(3) as Consumption);
            _local1 = (_consumptionValues.getItemAt(4) as Consumption);
            _local1 = (_planValues.getItemAt(4) as Consumption);
            _local1 = (_consumptionValues.getItemAt(5) as Consumption);
            _local1 = (_planValues.getItemAt(5) as Consumption);
            _local1 = (_consumptionValues.getItemAt(6) as Consumption);
            _local1 = (_planValues.getItemAt(6) as Consumption);
            _local1 = model.currentStageWidth;
            _local1 = model.currentStageHeight;
            _local1 = (sunday.x + 550);
            _local1 = (bottomRow.y + 15);
            _local1 = (((infoContainer.height / 2) + 40) * -1);
            _local1 = content.Title;
            _local1 = SerializeContentXML.convertToHTMLText(content.TextLayout);
            _local1 = content.getCTAButton().Label;
        }

        public static function set watcherSetupUtil(_arg1:IWatcherSetupUtil):void{
            PersonalPlan._watcherSetupUtil = _arg1;
        }

    }
}//package com.redbox.changetech.view.templates 
