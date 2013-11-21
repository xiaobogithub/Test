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
    import com.redbox.changetech.util.Enumerations.*;
    import flash.external.*;
    import flash.debugger.*;
    import flash.errors.*;
    import flash.printing.*;
    import flash.profiler.*;

    public class TypicalWeek extends ModuleViewTemplate implements IBindingClient {

        private var _1082042285cta_btn:BalanceButtonReflectionCanvas
        public var _TypicalWeek_Weekday1:Weekday
        public var _TypicalWeek_Weekday2:Weekday
        public var _TypicalWeek_Weekday3:Weekday
        public var _TypicalWeek_Weekday4:Weekday
        public var _TypicalWeek_Weekday5:Weekday
        public var _TypicalWeek_Weekday6:Weekday
        public var _TypicalWeek_Weekday7:Weekday
        private var _891186736sunday:GridItem
        private var storeEvent:Event
        private var _1646101571leftsideContainer:VBox
        mx_internal var _bindingsByDestination:Object
        private var _1878076494rightsideContainer:VBox
        private var _3506584row2:GridRow
        private var _977343923tuesday:GridItem
        private var _2114201671saturday:GridItem
        private var _1572055514thursday:GridItem
        private var _1068502768monday:GridItem
        private var _1354958366_consumptionValues:ArrayCollection
        private var _387554584dataEntry:WeekdayDataEntry
        mx_internal var _watchers:Array
        private var _676947314weekdayGrid:VBox
        private var _1733702023dataEntryContainer:BalanceCustomContentReflectionCanvas
        private var _3181382grid:Grid
        private var _1549852825transContainer2:Canvas
        mx_internal var _bindingsBeginWithWord:Object
        private var _1393530710wednesday:GridItem
        private var _3506583row1:GridRow
        private var _1266285217friday:GridItem
        private var _1549852824transContainer1:Canvas
        mx_internal var _bindings:Array
        private var _documentDescriptor_:UIComponentDescriptor

        private static var _watcherSetupUtil:IWatcherSetupUtil;

        public function TypicalWeek(){
            _documentDescriptor_ = new UIComponentDescriptor({type:ModuleViewTemplate, propertiesFactory:function ():Object{
                return ({childDescriptors:[new UIComponentDescriptor({type:Canvas, id:"transContainer1", propertiesFactory:function ():Object{
                    return ({horizontalScrollPolicy:"off", verticalScrollPolicy:"off", alpha:0, childDescriptors:[new UIComponentDescriptor({type:VBox, id:"leftsideContainer", stylesFactory:function ():void{
                        this.verticalGap = 30;
                        this.horizontalAlign = "center";
                    }, propertiesFactory:function ():Object{
                        return ({x:30, y:30, percentWidth:100, childDescriptors:[new UIComponentDescriptor({type:VBox, id:"weekdayGrid", stylesFactory:function ():void{
                            this.horizontalAlign = "center";
                            this.verticalGap = 28;
                        }, propertiesFactory:function ():Object{
                            return ({percentWidth:100, childDescriptors:[new UIComponentDescriptor({type:Grid, id:"grid", stylesFactory:function ():void{
                                this.horizontalGap = 28;
                                this.verticalGap = 28;
                            }, propertiesFactory:function ():Object{
                                return ({childDescriptors:[new UIComponentDescriptor({type:GridRow, id:"row1", propertiesFactory:function ():Object{
                                    return ({childDescriptors:[new UIComponentDescriptor({type:GridItem, id:"monday", propertiesFactory:function ():Object{
                                        return ({childDescriptors:[new UIComponentDescriptor({type:Weekday, id:"_TypicalWeek_Weekday1"})]});
                                    }}), new UIComponentDescriptor({type:GridItem, id:"tuesday", propertiesFactory:function ():Object{
                                        return ({childDescriptors:[new UIComponentDescriptor({type:Weekday, id:"_TypicalWeek_Weekday2"})]});
                                    }}), new UIComponentDescriptor({type:GridItem, id:"wednesday", propertiesFactory:function ():Object{
                                        return ({childDescriptors:[new UIComponentDescriptor({type:Weekday, id:"_TypicalWeek_Weekday3"})]});
                                    }}), new UIComponentDescriptor({type:GridItem, id:"thursday", propertiesFactory:function ():Object{
                                        return ({childDescriptors:[new UIComponentDescriptor({type:Weekday, id:"_TypicalWeek_Weekday4"})]});
                                    }}), new UIComponentDescriptor({type:GridItem, id:"friday", propertiesFactory:function ():Object{
                                        return ({childDescriptors:[new UIComponentDescriptor({type:Weekday, id:"_TypicalWeek_Weekday5"})]});
                                    }})]});
                                }}), new UIComponentDescriptor({type:GridRow, id:"row2", propertiesFactory:function ():Object{
                                    return ({childDescriptors:[new UIComponentDescriptor({type:GridItem, id:"saturday", propertiesFactory:function ():Object{
                                        return ({childDescriptors:[new UIComponentDescriptor({type:Weekday, id:"_TypicalWeek_Weekday6"})]});
                                    }}), new UIComponentDescriptor({type:GridItem, id:"sunday", propertiesFactory:function ():Object{
                                        return ({childDescriptors:[new UIComponentDescriptor({type:Weekday, id:"_TypicalWeek_Weekday7"})]});
                                    }})]});
                                }})]});
                            }})]});
                        }})]});
                    }})]});
                }}), new UIComponentDescriptor({type:Canvas, id:"transContainer2", propertiesFactory:function ():Object{
                    return ({horizontalScrollPolicy:"off", verticalScrollPolicy:"off", childDescriptors:[new UIComponentDescriptor({type:VBox, id:"rightsideContainer", stylesFactory:function ():void{
                        this.horizontalAlign = "left";
                    }, propertiesFactory:function ():Object{
                        return ({horizontalScrollPolicy:"off", verticalScrollPolicy:"off", clipContent:false, childDescriptors:[new UIComponentDescriptor({type:BalanceCustomContentReflectionCanvas, id:"dataEntryContainer", propertiesFactory:function ():Object{
                            return ({width:330, content_arr:[_TypicalWeek_Spacer1_c(), _TypicalWeek_WeekdayDataEntry1_i()]});
                        }}), new UIComponentDescriptor({type:HBox, stylesFactory:function ():void{
                            this.horizontalAlign = "center";
                        }, propertiesFactory:function ():Object{
                            return ({width:330, childDescriptors:[new UIComponentDescriptor({type:BalanceButtonReflectionCanvas, id:"cta_btn"})]});
                        }})]});
                    }})]});
                }})]});
            }});
            _bindings = [];
            _watchers = [];
            _bindingsByDestination = {};
            _bindingsBeginWithWord = {};
            super();
            mx_internal::_document = this;
            this.addEventListener("creationComplete", ___TypicalWeek_ModuleViewTemplate1_creationComplete);
        }
        private function init(_arg1:FlexEvent):void{
            var _local2:BalanceModelLocator = BalanceModelLocator.getInstance();
            BasicModule(module).fadeInContainer1.addEventListener(EffectEvent.EFFECT_START, initBounceEffect);
            addEventListener(BalanceButton.EVENT_CLICKED, checkNextPressed);
            _consumptionValues = new ArrayCollection();
            var _local3:Array = [DayOfWeek.Monday.Text, DayOfWeek.Tuesday.Text, DayOfWeek.Wednesday.Text, DayOfWeek.Thursday.Text, DayOfWeek.Friday.Text, DayOfWeek.Saturday.Text, DayOfWeek.Sunday.Text];
            var _local4:ArrayCollection = new ArrayCollection(BalanceModelLocator.getInstance().consumer.ReportedUsage);
            var _local5:* = 0;
            while (_local5 < _local3.length) {
                _consumptionValues.addItem(WeekDayUtils.getItemByDayOfWeek(_local3[_local5], _local4));
                _local5++;
            };
        }
        public function get monday():GridItem{
            return (this._1068502768monday);
        }
        public function set weekdayGrid(_arg1:VBox):void{
            var _local2:Object = this._676947314weekdayGrid;
            if (_local2 !== _arg1){
                this._676947314weekdayGrid = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "weekdayGrid", _local2, _arg1));
            };
        }
        private function get _consumptionValues():ArrayCollection{
            return (this._1354958366_consumptionValues);
        }
        public function set monday(_arg1:GridItem):void{
            var _local2:Object = this._1068502768monday;
            if (_local2 !== _arg1){
                this._1068502768monday = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "monday", _local2, _arg1));
            };
        }
        public function set row1(_arg1:GridRow):void{
            var _local2:Object = this._3506583row1;
            if (_local2 !== _arg1){
                this._3506583row1 = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "row1", _local2, _arg1));
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
        public function set row2(_arg1:GridRow):void{
            var _local2:Object = this._3506584row2;
            if (_local2 !== _arg1){
                this._3506584row2 = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "row2", _local2, _arg1));
            };
        }
        public function set transContainer1(_arg1:Canvas):void{
            var _local2:Object = this._1549852824transContainer1;
            if (_local2 !== _arg1){
                this._1549852824transContainer1 = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "transContainer1", _local2, _arg1));
            };
        }
        public function get leftsideContainer():VBox{
            return (this._1646101571leftsideContainer);
        }
        private function set _consumptionValues(_arg1:ArrayCollection):void{
            var _local2:Object = this._1354958366_consumptionValues;
            if (_local2 !== _arg1){
                this._1354958366_consumptionValues = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "_consumptionValues", _local2, _arg1));
            };
        }
        public function get grid():Grid{
            return (this._3181382grid);
        }
        private function _TypicalWeek_Spacer1_c():Spacer{
            var _local1:Spacer = new Spacer();
            _local1.height = 10;
            if (!_local1.document){
                _local1.document = this;
            };
            return (_local1);
        }
        public function get tuesday():GridItem{
            return (this._977343923tuesday);
        }
        public function get wednesday():GridItem{
            return (this._1393530710wednesday);
        }
        public function set cta_btn(_arg1:BalanceButtonReflectionCanvas):void{
            var _local2:Object = this._1082042285cta_btn;
            if (_local2 !== _arg1){
                this._1082042285cta_btn = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "cta_btn", _local2, _arg1));
            };
        }
        public function get rightsideContainer():VBox{
            return (this._1878076494rightsideContainer);
        }
        public function get friday():GridItem{
            return (this._1266285217friday);
        }
        private function initBounceEffect(_arg1:EffectEvent):void{
        }
        public function get dataEntryContainer():BalanceCustomContentReflectionCanvas{
            return (this._1733702023dataEntryContainer);
        }
        public function set saturday(_arg1:GridItem):void{
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
        public function set grid(_arg1:Grid):void{
            var _local2:Object = this._3181382grid;
            if (_local2 !== _arg1){
                this._3181382grid = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "grid", _local2, _arg1));
            };
        }
        private function checkNextPressed(_arg1:Event):void{
            usageSent();
        }
        override public function initialize():void{
            var target:* = null;
            var watcherSetupUtilClass:* = null;
            mx_internal::setDocumentDescriptor(_documentDescriptor_);
            var bindings:* = _TypicalWeek_bindingsSetup();
            var watchers:* = [];
            target = this;
            if (_watcherSetupUtil == null){
                watcherSetupUtilClass = getDefinitionByName("_com_redbox_changetech_view_templates_TypicalWeekWatcherSetupUtil");
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
        private function _TypicalWeek_bindingExprs():void{
            var _local1:*;
            _local1 = model.currentStageWidth;
            _local1 = model.currentStageHeight;
            _local1 = (_consumptionValues.getItemAt(0) as Consumption);
            _local1 = (_consumptionValues.getItemAt(1) as Consumption);
            _local1 = (_consumptionValues.getItemAt(2) as Consumption);
            _local1 = (_consumptionValues.getItemAt(3) as Consumption);
            _local1 = (_consumptionValues.getItemAt(4) as Consumption);
            _local1 = (_consumptionValues.getItemAt(5) as Consumption);
            _local1 = (_consumptionValues.getItemAt(6) as Consumption);
            _local1 = model.currentStageWidth;
            _local1 = model.currentStageHeight;
            _local1 = ((leftsideContainer.x + grid.x) + wednesday.x);
            _local1 = ((weekdayGrid.y + row2.y) + 30);
            _local1 = (((dataEntryContainer.height / 2) + 40) * -1);
            _local1 = content.Title;
            _local1 = SerializeContentXML.convertToHTMLText(content.TextLayout);
            _local1 = content.getCTAButton().Label;
        }
        public function get transContainer2():Canvas{
            return (this._1549852825transContainer2);
        }
        public function set tuesday(_arg1:GridItem):void{
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
        public function set sunday(_arg1:GridItem):void{
            var _local2:Object = this._891186736sunday;
            if (_local2 !== _arg1){
                this._891186736sunday = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "sunday", _local2, _arg1));
            };
        }
        public function get row1():GridRow{
            return (this._3506583row1);
        }
        public function get row2():GridRow{
            return (this._3506584row2);
        }
        public function set wednesday(_arg1:GridItem):void{
            var _local2:Object = this._1393530710wednesday;
            if (_local2 !== _arg1){
                this._1393530710wednesday = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "wednesday", _local2, _arg1));
            };
        }
        public function get cta_btn():BalanceButtonReflectionCanvas{
            return (this._1082042285cta_btn);
        }
        public function get transContainer1():Canvas{
            return (this._1549852824transContainer1);
        }
        public function set thursday(_arg1:GridItem):void{
            var _local2:Object = this._1572055514thursday;
            if (_local2 !== _arg1){
                this._1572055514thursday = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "thursday", _local2, _arg1));
            };
        }
        public function get saturday():GridItem{
            return (this._2114201671saturday);
        }
        public function get weekdayGrid():VBox{
            return (this._676947314weekdayGrid);
        }
        public function set friday(_arg1:GridItem):void{
            var _local2:Object = this._1266285217friday;
            if (_local2 !== _arg1){
                this._1266285217friday = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "friday", _local2, _arg1));
            };
        }
        private function reportUsage():void{
            var _local1:CairngormEvent = new CairngormEvent(BalanceController.REPORT_USAGE);
            _local1.data = new Object();
            _local1.data.callback = usageSent;
            _local1.dispatch();
        }
        public function ___TypicalWeek_ModuleViewTemplate1_creationComplete(_arg1:FlexEvent):void{
            init(_arg1);
        }
        private function _TypicalWeek_bindingsSetup():Array{
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
                _TypicalWeek_Weekday1.consumption = _arg1;
            }, "_TypicalWeek_Weekday1.consumption");
            result[2] = binding;
            binding = new Binding(this, function ():Consumption{
                return ((_consumptionValues.getItemAt(1) as Consumption));
            }, function (_arg1:Consumption):void{
                _TypicalWeek_Weekday2.consumption = _arg1;
            }, "_TypicalWeek_Weekday2.consumption");
            result[3] = binding;
            binding = new Binding(this, function ():Consumption{
                return ((_consumptionValues.getItemAt(2) as Consumption));
            }, function (_arg1:Consumption):void{
                _TypicalWeek_Weekday3.consumption = _arg1;
            }, "_TypicalWeek_Weekday3.consumption");
            result[4] = binding;
            binding = new Binding(this, function ():Consumption{
                return ((_consumptionValues.getItemAt(3) as Consumption));
            }, function (_arg1:Consumption):void{
                _TypicalWeek_Weekday4.consumption = _arg1;
            }, "_TypicalWeek_Weekday4.consumption");
            result[5] = binding;
            binding = new Binding(this, function ():Consumption{
                return ((_consumptionValues.getItemAt(4) as Consumption));
            }, function (_arg1:Consumption):void{
                _TypicalWeek_Weekday5.consumption = _arg1;
            }, "_TypicalWeek_Weekday5.consumption");
            result[6] = binding;
            binding = new Binding(this, function ():Consumption{
                return ((_consumptionValues.getItemAt(5) as Consumption));
            }, function (_arg1:Consumption):void{
                _TypicalWeek_Weekday6.consumption = _arg1;
            }, "_TypicalWeek_Weekday6.consumption");
            result[7] = binding;
            binding = new Binding(this, function ():Consumption{
                return ((_consumptionValues.getItemAt(6) as Consumption));
            }, function (_arg1:Consumption):void{
                _TypicalWeek_Weekday7.consumption = _arg1;
            }, "_TypicalWeek_Weekday7.consumption");
            result[8] = binding;
            binding = new Binding(this, function ():Number{
                return (model.currentStageWidth);
            }, function (_arg1:Number):void{
                transContainer2.width = _arg1;
            }, "transContainer2.width");
            result[9] = binding;
            binding = new Binding(this, function ():Number{
                return (model.currentStageHeight);
            }, function (_arg1:Number):void{
                transContainer2.height = _arg1;
            }, "transContainer2.height");
            result[10] = binding;
            binding = new Binding(this, function ():Number{
                return (((leftsideContainer.x + grid.x) + wednesday.x));
            }, function (_arg1:Number):void{
                rightsideContainer.x = _arg1;
            }, "rightsideContainer.x");
            result[11] = binding;
            binding = new Binding(this, function ():Number{
                return (((weekdayGrid.y + row2.y) + 30));
            }, function (_arg1:Number):void{
                rightsideContainer.y = _arg1;
            }, "rightsideContainer.y");
            result[12] = binding;
            binding = new Binding(this, function ():Number{
                return ((((dataEntryContainer.height / 2) + 40) * -1));
            }, function (_arg1:Number):void{
                rightsideContainer.setStyle("verticalGap", _arg1);
            }, "rightsideContainer.verticalGap");
            result[13] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = content.Title;
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                dataEntryContainer.title_str = _arg1;
            }, "dataEntryContainer.title_str");
            result[14] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = SerializeContentXML.convertToHTMLText(content.TextLayout);
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                dataEntryContainer.copy_str = _arg1;
            }, "dataEntryContainer.copy_str");
            result[15] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = content.getCTAButton().Label;
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                cta_btn.buttonLabel = _arg1;
            }, "cta_btn.buttonLabel");
            result[16] = binding;
            return (result);
        }
        private function usageSent():void{
            trace("usageSent");
            dispatchStoredNextEvent();
        }
        public function set dataEntry(_arg1:WeekdayDataEntry):void{
            var _local2:Object = this._387554584dataEntry;
            if (_local2 !== _arg1){
                this._387554584dataEntry = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "dataEntry", _local2, _arg1));
            };
        }
        private function _TypicalWeek_WeekdayDataEntry1_i():WeekdayDataEntry{
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
        public function get thursday():GridItem{
            return (this._1572055514thursday);
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
        public function get sunday():GridItem{
            return (this._891186736sunday);
        }

        public static function set watcherSetupUtil(_arg1:IWatcherSetupUtil):void{
            TypicalWeek._watcherSetupUtil = _arg1;
        }

    }
}//package com.redbox.changetech.view.templates 
