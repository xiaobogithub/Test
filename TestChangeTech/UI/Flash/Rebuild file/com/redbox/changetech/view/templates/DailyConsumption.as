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
    import mx.controls.*;
    import mx.states.*;
    import mx.binding.*;
    import com.redbox.changetech.control.*;
    import mx.containers.*;
    import com.redbox.changetech.view.components.*;
    import com.redbox.changetech.model.*;
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

    public class DailyConsumption extends ModuleViewTemplate implements IBindingClient {

        private var _1462574798copy1_str:String
        private var _1082042285cta_btn:BalanceButtonReflectionCanvas
        public var _DailyConsumption_SetProperty2:SetProperty
        public var _DailyConsumption_SetProperty3:SetProperty
        public var _DailyConsumption_SetProperty4:SetProperty
        public var _DailyConsumption_SetProperty5:SetProperty
        public var _DailyConsumption_SetProperty1:SetProperty
        private var CONSUMPTION_OVER:String = "ConsumptionOver"
        private var _1646101571leftsideContainer:VBox
        private var storeEvent:Event
        mx_internal var _bindingsByDestination:Object
        private var CONSUMPTION_EQUAL:String = "ConsumptionEqual"
        private var _1878076494rightsideContainer:VBox
        public var _DailyConsumption_Weekday1:Weekday
        public var _DailyConsumption_Weekday2:Weekday
        public var _DailyConsumption_Weekday3:Weekday
        private var _1354958366_consumptionValues:ArrayCollection
        private var _387554584dataEntry:WeekdayDataEntry
        private var _18480789yesterday_weekday:Weekday
        mx_internal var _watchers:Array
        private var _676947314weekdayGrid:VBox
        private var _1782234803questions:VBox
        private var _1733702023dataEntryContainer:BalanceCustomContentReflectionCanvas
        private var _3181382grid:Grid
        public var _DailyConsumption_WeekdayDataEntryInfoReflectionCanvas1:WeekdayDataEntryInfoReflectionCanvas
        private var _1549852825transContainer2:Canvas
        mx_internal var _bindingsBeginWithWord:Object
        private var _711328688rb_group:RadioButtonGroup
        private var _1549852824transContainer1:Canvas
        mx_internal var _bindings:Array
        private var CONSUMPTION_UNDER:String = "ConsumptionUnder"
        private var _documentDescriptor_:UIComponentDescriptor

        private static var _watcherSetupUtil:IWatcherSetupUtil;

        public function DailyConsumption(){
            _documentDescriptor_ = new UIComponentDescriptor({type:ModuleViewTemplate, propertiesFactory:function ():Object{
                return ({childDescriptors:[new UIComponentDescriptor({type:Canvas, id:"transContainer1", propertiesFactory:function ():Object{
                    return ({horizontalScrollPolicy:"off", verticalScrollPolicy:"off", childDescriptors:[new UIComponentDescriptor({type:VBox, id:"leftsideContainer", stylesFactory:function ():void{
                        this.verticalGap = 30;
                    }, propertiesFactory:function ():Object{
                        return ({x:30, y:30, percentWidth:60, childDescriptors:[new UIComponentDescriptor({type:VBox, id:"weekdayGrid", stylesFactory:function ():void{
                            this.horizontalAlign = "center";
                            this.verticalGap = 28;
                        }, propertiesFactory:function ():Object{
                            return ({percentWidth:100, childDescriptors:[new UIComponentDescriptor({type:Grid, id:"grid", stylesFactory:function ():void{
                                this.horizontalGap = 28;
                                this.verticalGap = 28;
                            }, propertiesFactory:function ():Object{
                                return ({childDescriptors:[new UIComponentDescriptor({type:GridRow, propertiesFactory:function ():Object{
                                    return ({childDescriptors:[new UIComponentDescriptor({type:GridItem, propertiesFactory:function ():Object{
                                        return ({childDescriptors:[new UIComponentDescriptor({type:Weekday, id:"_DailyConsumption_Weekday1"})]});
                                    }}), new UIComponentDescriptor({type:GridItem, propertiesFactory:function ():Object{
                                        return ({childDescriptors:[new UIComponentDescriptor({type:Weekday, id:"_DailyConsumption_Weekday2"})]});
                                    }})]});
                                }}), new UIComponentDescriptor({type:GridRow, propertiesFactory:function ():Object{
                                    return ({childDescriptors:[new UIComponentDescriptor({type:GridItem, propertiesFactory:function ():Object{
                                        return ({childDescriptors:[new UIComponentDescriptor({type:Weekday, id:"_DailyConsumption_Weekday3"})]});
                                    }}), new UIComponentDescriptor({type:GridItem, propertiesFactory:function ():Object{
                                        return ({childDescriptors:[new UIComponentDescriptor({type:Weekday, id:"yesterday_weekday"})]});
                                    }})]});
                                }})]});
                            }}), new UIComponentDescriptor({type:WeekdayDataEntryInfoReflectionCanvas, id:"_DailyConsumption_WeekdayDataEntryInfoReflectionCanvas1"})]});
                        }})]});
                    }})]});
                }}), new UIComponentDescriptor({type:Canvas, id:"transContainer2", propertiesFactory:function ():Object{
                    return ({horizontalScrollPolicy:"off", verticalScrollPolicy:"off", childDescriptors:[new UIComponentDescriptor({type:VBox, id:"rightsideContainer", stylesFactory:function ():void{
                        this.horizontalAlign = "left";
                    }, propertiesFactory:function ():Object{
                        return ({horizontalScrollPolicy:"off", verticalScrollPolicy:"off", clipContent:false, childDescriptors:[new UIComponentDescriptor({type:BalanceCustomContentReflectionCanvas, id:"dataEntryContainer", propertiesFactory:function ():Object{
                            return ({width:330, content_arr:[_DailyConsumption_Spacer1_c(), _DailyConsumption_WeekdayDataEntry1_i(), _DailyConsumption_VBox4_i()]});
                        }}), new UIComponentDescriptor({type:HBox, stylesFactory:function ():void{
                            this.horizontalAlign = "right";
                        }, propertiesFactory:function ():Object{
                            return ({width:350, childDescriptors:[new UIComponentDescriptor({type:BalanceButtonReflectionCanvas, id:"cta_btn"})]});
                        }})]});
                    }})]});
                }})]});
            }});
            _1354958366_consumptionValues = new ArrayCollection(BalanceModelLocator.getInstance().consumer.ReportedUsage);
            _bindings = [];
            _watchers = [];
            _bindingsByDestination = {};
            _bindingsBeginWithWord = {};
            super();
            mx_internal::_document = this;
            this.horizontalScrollPolicy = "off";
            this.verticalScrollPolicy = "off";
            this.states = [_DailyConsumption_State1_c()];
            _DailyConsumption_String1_i();
            _DailyConsumption_RadioButtonGroup1_i();
            this.addEventListener("creationComplete", ___DailyConsumption_ModuleViewTemplate1_creationComplete);
        }
        private function _DailyConsumption_State1_c():State{
            var _local1:State = new State();
            _local1.name = "questionsMode";
            _local1.overrides = [_DailyConsumption_SetProperty1_i(), _DailyConsumption_SetProperty2_i(), _DailyConsumption_SetProperty3_i(), _DailyConsumption_SetProperty4_i(), _DailyConsumption_SetProperty5_i()];
            return (_local1);
        }
        private function _DailyConsumption_SetProperty5_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _DailyConsumption_SetProperty5 = _local1;
            _local1.name = "copy_str";
            BindingManager.executeBindings(this, "_DailyConsumption_SetProperty5", _DailyConsumption_SetProperty5);
            return (_local1);
        }
        private function _DailyConsumption_SetProperty1_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _DailyConsumption_SetProperty1 = _local1;
            _local1.name = "visible";
            _local1.value = true;
            BindingManager.executeBindings(this, "_DailyConsumption_SetProperty1", _DailyConsumption_SetProperty1);
            return (_local1);
        }
        private function _DailyConsumption_RadioButtonGroup1_i():RadioButtonGroup{
            var _local1:RadioButtonGroup = new RadioButtonGroup();
            rb_group = _local1;
            _local1.initialized(this, "rb_group");
            return (_local1);
        }
        private function _DailyConsumption_bindingsSetup():Array{
            var binding:* = null;
            var result:* = [];
            binding = new Binding(this, function ():Object{
                return (questions);
            }, function (_arg1:Object):void{
                _DailyConsumption_SetProperty1.target = _arg1;
            }, "_DailyConsumption_SetProperty1.target");
            result[0] = binding;
            binding = new Binding(this, function ():Object{
                return (questions);
            }, function (_arg1:Object):void{
                _DailyConsumption_SetProperty2.target = _arg1;
            }, "_DailyConsumption_SetProperty2.target");
            result[1] = binding;
            binding = new Binding(this, function ():Object{
                return (dataEntry);
            }, function (_arg1:Object):void{
                _DailyConsumption_SetProperty3.target = _arg1;
            }, "_DailyConsumption_SetProperty3.target");
            result[2] = binding;
            binding = new Binding(this, function ():Object{
                return (dataEntry);
            }, function (_arg1:Object):void{
                _DailyConsumption_SetProperty4.target = _arg1;
            }, "_DailyConsumption_SetProperty4.target");
            result[3] = binding;
            binding = new Binding(this, function ():Object{
                return (dataEntryContainer);
            }, function (_arg1:Object):void{
                _DailyConsumption_SetProperty5.target = _arg1;
            }, "_DailyConsumption_SetProperty5.target");
            result[4] = binding;
            binding = new Binding(this, function (){
                return (copy1_str);
            }, function (_arg1):void{
                _DailyConsumption_SetProperty5.value = _arg1;
            }, "_DailyConsumption_SetProperty5.value");
            result[5] = binding;
            binding = new Binding(this, function ():Number{
                return (model.currentStageWidth);
            }, function (_arg1:Number):void{
                transContainer1.width = _arg1;
            }, "transContainer1.width");
            result[6] = binding;
            binding = new Binding(this, function ():Number{
                return (model.currentStageHeight);
            }, function (_arg1:Number):void{
                transContainer1.height = _arg1;
            }, "transContainer1.height");
            result[7] = binding;
            binding = new Binding(this, function ():Consumption{
                return ((_consumptionValues.getItemAt(3) as Consumption));
            }, function (_arg1:Consumption):void{
                _DailyConsumption_Weekday1.consumption = _arg1;
            }, "_DailyConsumption_Weekday1.consumption");
            result[8] = binding;
            binding = new Binding(this, function ():Consumption{
                return ((_consumptionValues.getItemAt(4) as Consumption));
            }, function (_arg1:Consumption):void{
                _DailyConsumption_Weekday2.consumption = _arg1;
            }, "_DailyConsumption_Weekday2.consumption");
            result[9] = binding;
            binding = new Binding(this, function ():Consumption{
                return ((_consumptionValues.getItemAt(5) as Consumption));
            }, function (_arg1:Consumption):void{
                _DailyConsumption_Weekday3.consumption = _arg1;
            }, "_DailyConsumption_Weekday3.consumption");
            result[10] = binding;
            binding = new Binding(this, function ():Consumption{
                return ((_consumptionValues.getItemAt(6) as Consumption));
            }, function (_arg1:Consumption):void{
                yesterday_weekday.consumption = _arg1;
            }, "yesterday_weekday.consumption");
            result[11] = binding;
            binding = new Binding(this, function ():Number{
                return (grid.width);
            }, function (_arg1:Number):void{
                _DailyConsumption_WeekdayDataEntryInfoReflectionCanvas1.width = _arg1;
            }, "_DailyConsumption_WeekdayDataEntryInfoReflectionCanvas1.width");
            result[12] = binding;
            binding = new Binding(this, function ():Number{
                return (model.currentStageWidth);
            }, function (_arg1:Number):void{
                transContainer2.width = _arg1;
            }, "transContainer2.width");
            result[13] = binding;
            binding = new Binding(this, function ():Number{
                return (model.currentStageHeight);
            }, function (_arg1:Number):void{
                transContainer2.height = _arg1;
            }, "transContainer2.height");
            result[14] = binding;
            binding = new Binding(this, function ():Number{
                return (((grid.x + grid.width) + 60));
            }, function (_arg1:Number):void{
                rightsideContainer.x = _arg1;
            }, "rightsideContainer.x");
            result[15] = binding;
            binding = new Binding(this, function ():Number{
                return (leftsideContainer.y);
            }, function (_arg1:Number):void{
                rightsideContainer.y = _arg1;
            }, "rightsideContainer.y");
            result[16] = binding;
            binding = new Binding(this, function ():Number{
                return (weekdayGrid.y);
            }, function (_arg1:Number):void{
                rightsideContainer.setStyle("paddingTop", _arg1);
            }, "rightsideContainer.paddingTop");
            result[17] = binding;
            binding = new Binding(this, function ():Number{
                return ((((dataEntryContainer.height / 2) + 40) * -1));
            }, function (_arg1:Number):void{
                rightsideContainer.setStyle("verticalGap", _arg1);
            }, "rightsideContainer.verticalGap");
            result[18] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = content.Title;
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                dataEntryContainer.title_str = _arg1;
            }, "dataEntryContainer.title_str");
            result[19] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = SerializeContentXML.convertToHTMLText(content.TextLayout);
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                dataEntryContainer.copy_str = _arg1;
            }, "dataEntryContainer.copy_str");
            result[20] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = content.getCTAButton().Label;
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                cta_btn.buttonLabel = _arg1;
            }, "cta_btn.buttonLabel");
            result[21] = binding;
            return (result);
        }
        private function _DailyConsumption_WeekdayDataEntry1_i():WeekdayDataEntry{
            var _local1:WeekdayDataEntry = new WeekdayDataEntry();
            dataEntry = _local1;
            _local1.showComplete = true;
            _local1.showNext = true;
            _local1.id = "dataEntry";
            if (!_local1.document){
                _local1.document = this;
            };
            return (_local1);
        }
        private function get _consumptionValues():ArrayCollection{
            return (this._1354958366_consumptionValues);
        }
        public function set weekdayGrid(_arg1:VBox):void{
            var _local2:Object = this._676947314weekdayGrid;
            if (_local2 !== _arg1){
                this._676947314weekdayGrid = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "weekdayGrid", _local2, _arg1));
            };
        }
        private function dispatchStoredNextEvent():void{
            BasicModule(module).outro();
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
        private function _DailyConsumption_SetProperty4_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _DailyConsumption_SetProperty4 = _local1;
            _local1.name = "includeInLayout";
            _local1.value = false;
            BindingManager.executeBindings(this, "_DailyConsumption_SetProperty4", _DailyConsumption_SetProperty4);
            return (_local1);
        }
        public function get grid():Grid{
            return (this._3181382grid);
        }
        private function _DailyConsumption_RadioButton2_c():RadioButton{
            var _local1:RadioButton = new RadioButton();
            _local1.label = "I think I may be in trouble.";
            _local1.groupName = "rb_group";
            _local1.labelPlacement = "right";
            if (!_local1.document){
                _local1.document = this;
            };
            return (_local1);
        }
        private function set _consumptionValues(_arg1:ArrayCollection):void{
            var _local2:Object = this._1354958366_consumptionValues;
            if (_local2 !== _arg1){
                this._1354958366_consumptionValues = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "_consumptionValues", _local2, _arg1));
            };
        }
        public function get questions():VBox{
            return (this._1782234803questions);
        }
        public function get rightsideContainer():VBox{
            return (this._1878076494rightsideContainer);
        }
        public function set transContainer1(_arg1:Canvas):void{
            var _local2:Object = this._1549852824transContainer1;
            if (_local2 !== _arg1){
                this._1549852824transContainer1 = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "transContainer1", _local2, _arg1));
            };
        }
        public function set cta_btn(_arg1:BalanceButtonReflectionCanvas):void{
            var _local2:Object = this._1082042285cta_btn;
            if (_local2 !== _arg1){
                this._1082042285cta_btn = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "cta_btn", _local2, _arg1));
            };
        }
        public function set rb_group(_arg1:RadioButtonGroup):void{
            var _local2:Object = this._711328688rb_group;
            if (_local2 !== _arg1){
                this._711328688rb_group = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "rb_group", _local2, _arg1));
            };
        }
        private function _DailyConsumption_VBox4_i():VBox{
            var _local1:VBox = new VBox();
            questions = _local1;
            _local1.visible = false;
            _local1.includeInLayout = false;
            _local1.id = "questions";
            if (!_local1.document){
                _local1.document = this;
            };
            _local1.addChild(_DailyConsumption_RadioButton1_c());
            _local1.addChild(_DailyConsumption_RadioButton2_c());
            return (_local1);
        }
        public function get dataEntryContainer():BalanceCustomContentReflectionCanvas{
            return (this._1733702023dataEntryContainer);
        }
        public function get yesterday_weekday():Weekday{
            return (this._18480789yesterday_weekday);
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
            var _local2:CairngormEvent = new CairngormEvent(BalanceController.REPORT_USAGE);
            _local2.data = new Object();
            _local2.data.callback = usageSent;
            _local2.dispatch();
        }
        private function _DailyConsumption_Spacer1_c():Spacer{
            var _local1:Spacer = new Spacer();
            _local1.height = 10;
            if (!_local1.document){
                _local1.document = this;
            };
            return (_local1);
        }
        private function _DailyConsumption_String1_i():String{
            var _local1 = "\n\t\t\t<h2>What is happening?</h2>\n\t\t";
            copy1_str = _local1;
            return (_local1);
        }
        private function _DailyConsumption_RadioButton1_c():RadioButton{
            var _local1:RadioButton = new RadioButton();
            _local1.label = "It was just a lapse. I'm OK.";
            _local1.groupName = "rb_group";
            _local1.labelPlacement = "right";
            if (!_local1.document){
                _local1.document = this;
            };
            return (_local1);
        }
        public function get weekdayGrid():VBox{
            return (this._676947314weekdayGrid);
        }
        public function set grid(_arg1:Grid):void{
            var _local2:Object = this._3181382grid;
            if (_local2 !== _arg1){
                this._3181382grid = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "grid", _local2, _arg1));
            };
        }
        override public function initialize():void{
            var target:* = null;
            var watcherSetupUtilClass:* = null;
            mx_internal::setDocumentDescriptor(_documentDescriptor_);
            var bindings:* = _DailyConsumption_bindingsSetup();
            var watchers:* = [];
            target = this;
            if (_watcherSetupUtil == null){
                watcherSetupUtilClass = getDefinitionByName("_com_redbox_changetech_view_templates_DailyConsumptionWatcherSetupUtil");
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
        public function set questions(_arg1:VBox):void{
            var _local2:Object = this._1782234803questions;
            if (_local2 !== _arg1){
                this._1782234803questions = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "questions", _local2, _arg1));
            };
        }
        public function set copy1_str(_arg1:String):void{
            var _local2:Object = this._1462574798copy1_str;
            if (_local2 !== _arg1){
                this._1462574798copy1_str = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "copy1_str", _local2, _arg1));
            };
        }
        private function subCreationComplete(_arg1:FlexEvent):void{
            transContainer1.alpha = 0;
            transContainer2.alpha = 0;
            var _local2:String = WeekDayUtils.getWeekdayFromIndex(BalanceModelLocator.getInstance().currentDay);
            var _local3:Consumption = (_consumptionValues.getItemAt(6) as Consumption);
            BalanceModelLocator.getInstance().setCurrentConsumptionVO(_local3.DayOfWeek);
            addEventListener(BalanceButton.EVENT_CLICKED, checkNextPressed);
        }
        public function set rightsideContainer(_arg1:VBox):void{
            var _local2:Object = this._1878076494rightsideContainer;
            if (_local2 !== _arg1){
                this._1878076494rightsideContainer = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "rightsideContainer", _local2, _arg1));
            };
        }
        public function get transContainer2():Canvas{
            return (this._1549852825transContainer2);
        }
        public function get cta_btn():BalanceButtonReflectionCanvas{
            return (this._1082042285cta_btn);
        }
        public function get rb_group():RadioButtonGroup{
            return (this._711328688rb_group);
        }
        private function _DailyConsumption_SetProperty3_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _DailyConsumption_SetProperty3 = _local1;
            _local1.name = "visible";
            _local1.value = false;
            BindingManager.executeBindings(this, "_DailyConsumption_SetProperty3", _DailyConsumption_SetProperty3);
            return (_local1);
        }
        public function get transContainer1():Canvas{
            return (this._1549852824transContainer1);
        }
        private function _DailyConsumption_SetProperty2_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _DailyConsumption_SetProperty2 = _local1;
            _local1.name = "includeInLayout";
            _local1.value = true;
            BindingManager.executeBindings(this, "_DailyConsumption_SetProperty2", _DailyConsumption_SetProperty2);
            return (_local1);
        }
        public function ___DailyConsumption_ModuleViewTemplate1_creationComplete(_arg1:FlexEvent):void{
            subCreationComplete(_arg1);
        }
        public function get copy1_str():String{
            return (this._1462574798copy1_str);
        }
        private function _DailyConsumption_bindingExprs():void{
            var _local1:*;
            _local1 = questions;
            _local1 = questions;
            _local1 = dataEntry;
            _local1 = dataEntry;
            _local1 = dataEntryContainer;
            _local1 = copy1_str;
            _local1 = model.currentStageWidth;
            _local1 = model.currentStageHeight;
            _local1 = (_consumptionValues.getItemAt(3) as Consumption);
            _local1 = (_consumptionValues.getItemAt(4) as Consumption);
            _local1 = (_consumptionValues.getItemAt(5) as Consumption);
            _local1 = (_consumptionValues.getItemAt(6) as Consumption);
            _local1 = grid.width;
            _local1 = model.currentStageWidth;
            _local1 = model.currentStageHeight;
            _local1 = ((grid.x + grid.width) + 60);
            _local1 = leftsideContainer.y;
            _local1 = weekdayGrid.y;
            _local1 = (((dataEntryContainer.height / 2) + 40) * -1);
            _local1 = content.Title;
            _local1 = SerializeContentXML.convertToHTMLText(content.TextLayout);
            _local1 = content.getCTAButton().Label;
        }
        private function usageSent():void{
            var _local1:Number = Number(model.consumer.getCurrentDayTarget());
            var _local2:* = model.consumer;
            var _local3:Number = Number(model.consumer.getYesterdayDayConsumption());
            trace(("plannedConsumption=" + _local1));
            trace(("actualConsumption=" + _local3));
            switch (true){
                case (_local3 < _local1):
                    BasicModule(module).currentTag = CONSUMPTION_UNDER;
                    break;
                case (_local3 == _local1):
                    BasicModule(module).currentTag = CONSUMPTION_EQUAL;
                    break;
                case (_local3 == (_local1 + 1)):
                    BasicModule(module).currentTag = CONSUMPTION_EQUAL;
                    break;
                case (_local3 > (_local1 + 1)):
                    BasicModule(module).currentTag = CONSUMPTION_OVER;
                    break;
            };
            trace(("BasicModule(module).currentTag=" + BasicModule(module).currentTag));
            dispatchStoredNextEvent();
        }
        public function set yesterday_weekday(_arg1:Weekday):void{
            var _local2:Object = this._18480789yesterday_weekday;
            if (_local2 !== _arg1){
                this._18480789yesterday_weekday = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "yesterday_weekday", _local2, _arg1));
            };
        }
        public function set dataEntry(_arg1:WeekdayDataEntry):void{
            var _local2:Object = this._387554584dataEntry;
            if (_local2 !== _arg1){
                this._387554584dataEntry = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "dataEntry", _local2, _arg1));
            };
        }
        public function get dataEntry():WeekdayDataEntry{
            return (this._387554584dataEntry);
        }
        public function set dataEntryContainer(_arg1:BalanceCustomContentReflectionCanvas):void{
            var _local2:Object = this._1733702023dataEntryContainer;
            if (_local2 !== _arg1){
                this._1733702023dataEntryContainer = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "dataEntryContainer", _local2, _arg1));
            };
        }

        public static function set watcherSetupUtil(_arg1:IWatcherSetupUtil):void{
            DailyConsumption._watcherSetupUtil = _arg1;
        }

    }
}//package com.redbox.changetech.view.templates 
