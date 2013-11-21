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
    import mx.collections.*;
    import flash.system.*;
    import flash.accessibility.*;
    import flash.xml.*;
    import flash.net.*;
    import com.redbox.changetech.view.modules.*;
    import com.redbox.changetech.event.*;
    import flash.filters.*;
    import flash.ui.*;
    import flash.external.*;
    import flash.debugger.*;
    import flash.errors.*;
    import flash.printing.*;
    import flash.profiler.*;

    public class PersonalValues extends ModuleViewTemplate implements IBindingClient {

        private var _1082042285cta_btn:BalanceButtonReflectionCanvas
        private var _231605483value_9:IconOption
        private var _231605482value_8:IconOption
        mx_internal var _bindingsByDestination:Object
        private var _231605479value_5:IconOption
        private var _3506584row2:GridRow
        private var _231605481value_7:IconOption
        private var _231605478value_4:IconOption
        private var currentQuestion:Question
        private var selectedOptions:Array
        private var _231605480value_6:IconOption
        private var _231605477value_3:IconOption
        mx_internal var _watchers:Array
        private var _1249474914options:ArrayCollection
        private var _231605476value_2:IconOption
        private var _3181382grid:Grid
        private var _1549852825transContainer2:Canvas
        private var _935190845infoOption:IconOption
        private var _231605475value_1:IconOption
        private var _3506583row1:GridRow
        private var _3506585row3:GridRow
        mx_internal var _bindings:Array
        mx_internal var _bindingsBeginWithWord:Object
        private var _1549852824transContainer1:Canvas
        private var _documentDescriptor_:UIComponentDescriptor

        private static var _watcherSetupUtil:IWatcherSetupUtil;

        public function PersonalValues(){
            _documentDescriptor_ = new UIComponentDescriptor({type:ModuleViewTemplate, propertiesFactory:function ():Object{
                return ({childDescriptors:[new UIComponentDescriptor({type:Canvas, id:"transContainer1", propertiesFactory:function ():Object{
                    return ({verticalScrollPolicy:"off", childDescriptors:[new UIComponentDescriptor({type:HBox, stylesFactory:function ():void{
                        this.horizontalAlign = "center";
                    }, propertiesFactory:function ():Object{
                        return ({percentWidth:100, childDescriptors:[new UIComponentDescriptor({type:Grid, id:"grid", stylesFactory:function ():void{
                            this.horizontalGap = 28;
                            this.verticalGap = 50;
                        }, propertiesFactory:function ():Object{
                            return ({childDescriptors:[new UIComponentDescriptor({type:GridRow, id:"row1", propertiesFactory:function ():Object{
                                return ({childDescriptors:[new UIComponentDescriptor({type:GridItem, propertiesFactory:function ():Object{
                                    return ({childDescriptors:[new UIComponentDescriptor({type:IconOption, id:"value_1", events:{click:"__value_1_click"}})]});
                                }}), new UIComponentDescriptor({type:GridItem, propertiesFactory:function ():Object{
                                    return ({childDescriptors:[new UIComponentDescriptor({type:IconOption, id:"value_2", events:{click:"__value_2_click"}})]});
                                }}), new UIComponentDescriptor({type:GridItem, propertiesFactory:function ():Object{
                                    return ({childDescriptors:[new UIComponentDescriptor({type:IconOption, id:"value_3", events:{click:"__value_3_click"}})]});
                                }})]});
                            }}), new UIComponentDescriptor({type:GridRow, id:"row2", propertiesFactory:function ():Object{
                                return ({childDescriptors:[new UIComponentDescriptor({type:GridItem, propertiesFactory:function ():Object{
                                    return ({childDescriptors:[new UIComponentDescriptor({type:IconOption, id:"value_4", events:{click:"__value_4_click"}})]});
                                }}), new UIComponentDescriptor({type:GridItem, propertiesFactory:function ():Object{
                                    return ({childDescriptors:[new UIComponentDescriptor({type:IconOption, id:"value_5", events:{click:"__value_5_click"}})]});
                                }}), new UIComponentDescriptor({type:GridItem, propertiesFactory:function ():Object{
                                    return ({childDescriptors:[new UIComponentDescriptor({type:IconOption, id:"value_6", events:{click:"__value_6_click"}})]});
                                }})]});
                            }}), new UIComponentDescriptor({type:GridRow, id:"row3", propertiesFactory:function ():Object{
                                return ({childDescriptors:[new UIComponentDescriptor({type:GridItem, propertiesFactory:function ():Object{
                                    return ({childDescriptors:[new UIComponentDescriptor({type:IconOption, id:"value_7", events:{click:"__value_7_click"}})]});
                                }}), new UIComponentDescriptor({type:GridItem, propertiesFactory:function ():Object{
                                    return ({childDescriptors:[new UIComponentDescriptor({type:IconOption, id:"value_8", events:{click:"__value_8_click"}})]});
                                }}), new UIComponentDescriptor({type:GridItem, propertiesFactory:function ():Object{
                                    return ({childDescriptors:[new UIComponentDescriptor({type:IconOption, id:"value_9", events:{click:"__value_9_click"}})]});
                                }})]});
                            }})]});
                        }})]});
                    }}), new UIComponentDescriptor({type:IconOption, id:"infoOption", events:{click:"__infoOption_click"}, propertiesFactory:function ():Object{
                        return ({visible:false});
                    }})]});
                }}), new UIComponentDescriptor({type:Canvas, id:"transContainer2", propertiesFactory:function ():Object{
                    return ({verticalScrollPolicy:"off", childDescriptors:[new UIComponentDescriptor({type:BalanceButtonReflectionCanvas, id:"cta_btn"})]});
                }})]});
            }});
            _bindings = [];
            _watchers = [];
            _bindingsByDestination = {};
            _bindingsBeginWithWord = {};
            super();
            mx_internal::_document = this;
            this.addEventListener("creationComplete", ___PersonalValues_ModuleViewTemplate1_creationComplete);
        }
        private function resetSelected():void{
            value_1.selected = (selectedOptions.indexOf(value_1.option) >= 0);
            value_2.selected = (selectedOptions.indexOf(value_2.option) >= 0);
            value_3.selected = (selectedOptions.indexOf(value_3.option) >= 0);
            value_4.selected = (selectedOptions.indexOf(value_4.option) >= 0);
            value_5.selected = (selectedOptions.indexOf(value_5.option) >= 0);
            value_6.selected = (selectedOptions.indexOf(value_6.option) >= 0);
            value_7.selected = (selectedOptions.indexOf(value_7.option) >= 0);
            value_8.selected = (selectedOptions.indexOf(value_8.option) >= 0);
            value_9.selected = (selectedOptions.indexOf(value_9.option) >= 0);
        }
        public function __value_4_click(_arg1:MouseEvent):void{
            select(_arg1);
        }
        private function init(_arg1:FlexEvent):void{
            addEventListener("toggleInfo", revealInfo);
            BasicModule(module).fadeInContainer1.addEventListener(EffectEvent.EFFECT_START, initBounceEffect);
            currentQuestion = content.Questions[0];
            options = new ArrayCollection(currentQuestion.Options);
            selectedOptions = [];
        }
        public function __value_8_click(_arg1:MouseEvent):void{
            select(_arg1);
        }
        private function revealInfo(_arg1:Event):void{
            resetSelected();
            var _local2:IconOption = IconOption(_arg1.target);
            if (_local2 == infoOption){
                infoOption.visible = false;
            } else {
                infoOption.x = ((grid.x + _local2.parent.parent.x) + _local2.parent.x);
                infoOption.y = ((grid.y + _local2.parent.parent.y) + _local2.parent.y);
                infoOption.visible = true;
                infoOption.option = _local2.option;
                infoOption.currentState = "info";
                infoOption.selected = (selectedOptions.indexOf(infoOption.option) >= 0);
            };
        }
        public function set row1(_arg1:GridRow):void{
            var _local2:Object = this._3506583row1;
            if (_local2 !== _arg1){
                this._3506583row1 = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "row1", _local2, _arg1));
            };
        }
        public function set row2(_arg1:GridRow):void{
            var _local2:Object = this._3506584row2;
            if (_local2 !== _arg1){
                this._3506584row2 = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "row2", _local2, _arg1));
            };
        }
        public function set row3(_arg1:GridRow):void{
            var _local2:Object = this._3506585row3;
            if (_local2 !== _arg1){
                this._3506585row3 = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "row3", _local2, _arg1));
            };
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
        private function set options(_arg1:ArrayCollection):void{
            var _local2:Object = this._1249474914options;
            if (_local2 !== _arg1){
                this._1249474914options = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "options", _local2, _arg1));
            };
        }
        public function get grid():Grid{
            return (this._3181382grid);
        }
        public function ___PersonalValues_ModuleViewTemplate1_creationComplete(_arg1:FlexEvent):void{
            init(_arg1);
        }
        public function __value_5_click(_arg1:MouseEvent):void{
            select(_arg1);
        }
        public function __value_1_click(_arg1:MouseEvent):void{
            select(_arg1);
        }
        public function __value_9_click(_arg1:MouseEvent):void{
            select(_arg1);
        }
        private function _PersonalValues_bindingExprs():void{
            var _local1:*;
            _local1 = model.currentStageWidth;
            _local1 = model.currentStageHeight;
            _local1 = (options.getItemAt(0) as Option);
            _local1 = (options.getItemAt(1) as Option);
            _local1 = (options.getItemAt(2) as Option);
            _local1 = (options.getItemAt(3) as Option);
            _local1 = (options.getItemAt(4) as Option);
            _local1 = (options.getItemAt(5) as Option);
            _local1 = (options.getItemAt(6) as Option);
            _local1 = (options.getItemAt(7) as Option);
            _local1 = (options.getItemAt(8) as Option);
            _local1 = (options.getItemAt(3) as Option);
            _local1 = model.currentStageWidth;
            _local1 = model.currentStageHeight;
            _local1 = ((grid.x + grid.width) + 20);
            _local1 = ((grid.y + grid.height) - (cta_btn.height / 2));
            _local1 = content.getCTAButton().Label;
            _local1 = content.getCTAButton().ButtonAction;
            _local1 = (content.getTertiaryButton() == null);
            _local1 = !((content.getCTAButton() == null));
        }
        public function set cta_btn(_arg1:BalanceButtonReflectionCanvas):void{
            var _local2:Object = this._1082042285cta_btn;
            if (_local2 !== _arg1){
                this._1082042285cta_btn = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "cta_btn", _local2, _arg1));
            };
        }
        private function initBounceEffect(_arg1:EffectEvent):void{
        }
        public function set infoOption(_arg1:IconOption):void{
            var _local2:Object = this._935190845infoOption;
            if (_local2 !== _arg1){
                this._935190845infoOption = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "infoOption", _local2, _arg1));
            };
        }
        override public function initialize():void{
            var target:* = null;
            var watcherSetupUtilClass:* = null;
            mx_internal::setDocumentDescriptor(_documentDescriptor_);
            var bindings:* = _PersonalValues_bindingsSetup();
            var watchers:* = [];
            target = this;
            if (_watcherSetupUtil == null){
                watcherSetupUtilClass = getDefinitionByName("_com_redbox_changetech_view_templates_PersonalValuesWatcherSetupUtil");
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
        public function __value_6_click(_arg1:MouseEvent):void{
            select(_arg1);
        }
        public function set grid(_arg1:Grid):void{
            var _local2:Object = this._3181382grid;
            if (_local2 !== _arg1){
                this._3181382grid = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "grid", _local2, _arg1));
            };
        }
        public function __value_2_click(_arg1:MouseEvent):void{
            select(_arg1);
        }
        private function _PersonalValues_bindingsSetup():Array{
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
            binding = new Binding(this, function ():Option{
                return ((options.getItemAt(0) as Option));
            }, function (_arg1:Option):void{
                value_1.option = _arg1;
            }, "value_1.option");
            result[2] = binding;
            binding = new Binding(this, function ():Option{
                return ((options.getItemAt(1) as Option));
            }, function (_arg1:Option):void{
                value_2.option = _arg1;
            }, "value_2.option");
            result[3] = binding;
            binding = new Binding(this, function ():Option{
                return ((options.getItemAt(2) as Option));
            }, function (_arg1:Option):void{
                value_3.option = _arg1;
            }, "value_3.option");
            result[4] = binding;
            binding = new Binding(this, function ():Option{
                return ((options.getItemAt(3) as Option));
            }, function (_arg1:Option):void{
                value_4.option = _arg1;
            }, "value_4.option");
            result[5] = binding;
            binding = new Binding(this, function ():Option{
                return ((options.getItemAt(4) as Option));
            }, function (_arg1:Option):void{
                value_5.option = _arg1;
            }, "value_5.option");
            result[6] = binding;
            binding = new Binding(this, function ():Option{
                return ((options.getItemAt(5) as Option));
            }, function (_arg1:Option):void{
                value_6.option = _arg1;
            }, "value_6.option");
            result[7] = binding;
            binding = new Binding(this, function ():Option{
                return ((options.getItemAt(6) as Option));
            }, function (_arg1:Option):void{
                value_7.option = _arg1;
            }, "value_7.option");
            result[8] = binding;
            binding = new Binding(this, function ():Option{
                return ((options.getItemAt(7) as Option));
            }, function (_arg1:Option):void{
                value_8.option = _arg1;
            }, "value_8.option");
            result[9] = binding;
            binding = new Binding(this, function ():Option{
                return ((options.getItemAt(8) as Option));
            }, function (_arg1:Option):void{
                value_9.option = _arg1;
            }, "value_9.option");
            result[10] = binding;
            binding = new Binding(this, function ():Option{
                return ((options.getItemAt(3) as Option));
            }, function (_arg1:Option):void{
                infoOption.option = _arg1;
            }, "infoOption.option");
            result[11] = binding;
            binding = new Binding(this, function ():Number{
                return (model.currentStageWidth);
            }, function (_arg1:Number):void{
                transContainer2.width = _arg1;
            }, "transContainer2.width");
            result[12] = binding;
            binding = new Binding(this, function ():Number{
                return (model.currentStageHeight);
            }, function (_arg1:Number):void{
                transContainer2.height = _arg1;
            }, "transContainer2.height");
            result[13] = binding;
            binding = new Binding(this, function ():Number{
                return (((grid.x + grid.width) + 20));
            }, function (_arg1:Number):void{
                cta_btn.x = _arg1;
            }, "cta_btn.x");
            result[14] = binding;
            binding = new Binding(this, function ():Number{
                return (((grid.y + grid.height) - (cta_btn.height / 2)));
            }, function (_arg1:Number):void{
                cta_btn.y = _arg1;
            }, "cta_btn.y");
            result[15] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = content.getCTAButton().Label;
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                cta_btn.buttonLabel = _arg1;
            }, "cta_btn.buttonLabel");
            result[16] = binding;
            binding = new Binding(this, function ():ButtonActionVO{
                return (content.getCTAButton().ButtonAction);
            }, function (_arg1:ButtonActionVO):void{
                cta_btn.action = _arg1;
            }, "cta_btn.action");
            result[17] = binding;
            binding = new Binding(this, function ():Boolean{
                return ((content.getTertiaryButton() == null));
            }, function (_arg1:Boolean):void{
                cta_btn.reflectionIsOn = _arg1;
            }, "cta_btn.reflectionIsOn");
            result[18] = binding;
            binding = new Binding(this, function ():Boolean{
                return (!((content.getCTAButton() == null)));
            }, function (_arg1:Boolean):void{
                cta_btn.visible = _arg1;
            }, "cta_btn.visible");
            result[19] = binding;
            return (result);
        }
        public function get row3():GridRow{
            return (this._3506585row3);
        }
        public function get row1():GridRow{
            return (this._3506583row1);
        }
        private function get options():ArrayCollection{
            return (this._1249474914options);
        }
        public function get row2():GridRow{
            return (this._3506584row2);
        }
        public function get transContainer2():Canvas{
            return (this._1549852825transContainer2);
        }
        public function get cta_btn():BalanceButtonReflectionCanvas{
            return (this._1082042285cta_btn);
        }
        public function get transContainer1():Canvas{
            return (this._1549852824transContainer1);
        }
        public function get infoOption():IconOption{
            return (this._935190845infoOption);
        }
        public function set value_1(_arg1:IconOption):void{
            var _local2:Object = this._231605475value_1;
            if (_local2 !== _arg1){
                this._231605475value_1 = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "value_1", _local2, _arg1));
            };
        }
        public function set value_3(_arg1:IconOption):void{
            var _local2:Object = this._231605477value_3;
            if (_local2 !== _arg1){
                this._231605477value_3 = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "value_3", _local2, _arg1));
            };
        }
        public function set value_4(_arg1:IconOption):void{
            var _local2:Object = this._231605478value_4;
            if (_local2 !== _arg1){
                this._231605478value_4 = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "value_4", _local2, _arg1));
            };
        }
        public function __value_3_click(_arg1:MouseEvent):void{
            select(_arg1);
        }
        public function set value_2(_arg1:IconOption):void{
            var _local2:Object = this._231605476value_2;
            if (_local2 !== _arg1){
                this._231605476value_2 = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "value_2", _local2, _arg1));
            };
        }
        public function __value_7_click(_arg1:MouseEvent):void{
            select(_arg1);
        }
        public function set value_8(_arg1:IconOption):void{
            var _local2:Object = this._231605482value_8;
            if (_local2 !== _arg1){
                this._231605482value_8 = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "value_8", _local2, _arg1));
            };
        }
        public function set value_5(_arg1:IconOption):void{
            var _local2:Object = this._231605479value_5;
            if (_local2 !== _arg1){
                this._231605479value_5 = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "value_5", _local2, _arg1));
            };
        }
        public function set value_6(_arg1:IconOption):void{
            var _local2:Object = this._231605480value_6;
            if (_local2 !== _arg1){
                this._231605480value_6 = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "value_6", _local2, _arg1));
            };
        }
        public function set value_7(_arg1:IconOption):void{
            var _local2:Object = this._231605481value_7;
            if (_local2 !== _arg1){
                this._231605481value_7 = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "value_7", _local2, _arg1));
            };
        }
        public function set value_9(_arg1:IconOption):void{
            var _local2:Object = this._231605483value_9;
            if (_local2 !== _arg1){
                this._231605483value_9 = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "value_9", _local2, _arg1));
            };
        }
        public function get value_1():IconOption{
            return (this._231605475value_1);
        }
        public function get value_2():IconOption{
            return (this._231605476value_2);
        }
        public function get value_3():IconOption{
            return (this._231605477value_3);
        }
        public function get value_4():IconOption{
            return (this._231605478value_4);
        }
        public function get value_5():IconOption{
            return (this._231605479value_5);
        }
        public function get value_6():IconOption{
            return (this._231605480value_6);
        }
        public function get value_7():IconOption{
            return (this._231605481value_7);
        }
        public function get value_8():IconOption{
            return (this._231605482value_8);
        }
        public function __infoOption_click(_arg1:MouseEvent):void{
            select(_arg1);
        }
        public function get value_9():IconOption{
            return (this._231605483value_9);
        }
        private function select(_arg1:Event):void{
            var _local2:AnswerSelectedEvent;
            var _local5:Option;
            if (_arg1.target == _arg1.currentTarget.info_icon){
                return;
            };
            var _local3:IconOption = (_arg1.currentTarget as IconOption);
            if (_local3.selected){
                _local3.selected = false;
                removeItemFromArray(selectedOptions, _local3.option);
            } else {
                if (selectedOptions.length > 2){
                    return;
                };
                _local3.selected = true;
                if (selectedOptions.indexOf(_local3.option) < 0){
                    selectedOptions.push(_local3.option);
                };
            };
            BasicModule(module).flushAnswers(currentQuestion.Id);
            var _local4:Number = 0;
            while (_local4 < selectedOptions.length) {
                _local2 = new AnswerSelectedEvent(AnswerSelectedEvent.ANSWER_SELECTED, true, false);
                _local2.id = currentQuestion.Id;
                _local5 = selectedOptions[_local4];
                _local2.optionId = _local5.Id;
                dispatchEvent(_local2);
                _local4++;
            };
        }

        private static function removeItemFromArray(_arg1:Array, _arg2):void{
            var _local3:Number = -1;
            var _local4:Number = 0;
            while (_local4 < _arg1.length) {
                if (_arg1[_local4] == _arg2){
                    _local3 = _local4;
                };
                _local4++;
            };
            if (_local3 > -1){
                _arg1.splice(_local3, 1);
            };
        }
        public static function set watcherSetupUtil(_arg1:IWatcherSetupUtil):void{
            PersonalValues._watcherSetupUtil = _arg1;
        }

    }
}//package com.redbox.changetech.view.templates 
