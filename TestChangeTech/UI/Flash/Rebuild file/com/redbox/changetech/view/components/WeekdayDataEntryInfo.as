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
    import com.redbox.changetech.model.*;
    import mx.binding.utils.*;
    import com.redbox.changetech.vo.*;
    import flash.utils.*;
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

    public class WeekdayDataEntryInfo extends GradientCanvas implements IBindingClient {

        private var timer:Timer
        private var _925318956roomVO:RoomVO
        mx_internal var _bindingsBeginWithWord:Object
        mx_internal var _bindingsByDestination:Object
        mx_internal var _watchers:Array
        public var _WeekdayDataEntryInfo_HRule1:HRule
        public var _WeekdayDataEntryInfo_HRule2:HRule
        public var isToDisableBinding:Boolean = true
        private var _104069929model:BalanceModelLocator
        private var _2040570774currentTarget:String
        private var changeWatcher:ChangeWatcher
        mx_internal var _bindings:Array
        public var _WeekdayDataEntryInfo_Label1:Label
        public var _WeekdayDataEntryInfo_Label2:Label
        private var _documentDescriptor_:UIComponentDescriptor

        private static var _watcherSetupUtil:IWatcherSetupUtil;

        public function WeekdayDataEntryInfo(){
            _documentDescriptor_ = new UIComponentDescriptor({type:GradientCanvas, propertiesFactory:function ():Object{
                return ({childDescriptors:[new UIComponentDescriptor({type:HBox, stylesFactory:function ():void{
                    this.horizontalAlign = "center";
                    this.verticalAlign = "middle";
                    this.horizontalGap = 0;
                    this.verticalGap = 0;
                    this.paddingLeft = 10;
                    this.paddingRight = 10;
                    this.paddingBottom = 10;
                    this.paddingTop = 10;
                }, propertiesFactory:function ():Object{
                    return ({percentWidth:100, childDescriptors:[new UIComponentDescriptor({type:VBox, stylesFactory:function ():void{
                        this.verticalGap = 0;
                    }, propertiesFactory:function ():Object{
                        return ({percentWidth:50, childDescriptors:[new UIComponentDescriptor({type:HBox, stylesFactory:function ():void{
                            this.verticalAlign = "middle";
                        }, propertiesFactory:function ():Object{
                            return ({percentWidth:100, childDescriptors:[new UIComponentDescriptor({type:Label, id:"_WeekdayDataEntryInfo_Label1", stylesFactory:function ():void{
                                this.textAlign = "left";
                                this.fontFamily = "Helvetica Neue";
                                this.fontSize = 18;
                                this.color = 0xFFFFFF;
                            }})]});
                        }}), new UIComponentDescriptor({type:HRule, id:"_WeekdayDataEntryInfo_HRule1", stylesFactory:function ():void{
                            this.strokeWidth = 1;
                        }, propertiesFactory:function ():Object{
                            return ({percentWidth:100});
                        }}), new UIComponentDescriptor({type:HRule, id:"_WeekdayDataEntryInfo_HRule2", stylesFactory:function ():void{
                            this.strokeWidth = 1;
                        }, propertiesFactory:function ():Object{
                            return ({percentWidth:100});
                        }})]});
                    }}), new UIComponentDescriptor({type:Label, id:"_WeekdayDataEntryInfo_Label2", stylesFactory:function ():void{
                        this.textAlign = "center";
                        this.fontFamily = "Helvetica Neue";
                        this.fontSize = 86;
                        this.color = 0xFFFFFF;
                    }, propertiesFactory:function ():Object{
                        return ({percentWidth:50});
                    }})]});
                }})]});
            }});
            _104069929model = BalanceModelLocator.getInstance();
            _925318956roomVO = RoomVO(Config.ROOM_CONFIGS.getItemAt(model.room));
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
                this.fillAlphas = [1, 1];
                this.gradientRatio = [0, 0xFF];
                this.angle = [90];
                this.borderAlphas = [0];
                this.cornerRadius = 10;
            };
            this.percentWidth = 100;
            this.percentHeight = 100;
            this.colorsConfiguration = [2];
            this.addEventListener("creationComplete", ___WeekdayDataEntryInfo_GradientCanvas1_creationComplete);
        }
        private function _WeekdayDataEntryInfo_bindingExprs():void{
            var _local1:*;
            _local1 = [roomVO.buttonGradColour1, roomVO.buttonGradColour2];
            _local1 = model.languageVO.getLang("Today").toUpperCase();
            _local1 = roomVO.buttonGradColour2;
            _local1 = roomVO.buttonGradColour1;
            _local1 = currentTarget;
        }
        public function ___WeekdayDataEntryInfo_GradientCanvas1_creationComplete(_arg1:FlexEvent):void{
            init(_arg1);
        }
        private function set model(_arg1:BalanceModelLocator):void{
            var _local2:Object = this._104069929model;
            if (_local2 !== _arg1){
                this._104069929model = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "model", _local2, _arg1));
            };
        }
        override public function initialize():void{
            var target:* = null;
            var watcherSetupUtilClass:* = null;
            mx_internal::setDocumentDescriptor(_documentDescriptor_);
            var bindings:* = _WeekdayDataEntryInfo_bindingsSetup();
            var watchers:* = [];
            target = this;
            if (_watcherSetupUtil == null){
                watcherSetupUtilClass = getDefinitionByName("_com_redbox_changetech_view_components_WeekdayDataEntryInfoWatcherSetupUtil");
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
        private function get roomVO():RoomVO{
            return (this._925318956roomVO);
        }
        private function removeBinding(_arg1:TimerEvent):void{
            changeWatcher.unwatch();
        }
        private function set currentTarget(_arg1:String):void{
            var _local2:Object = this._2040570774currentTarget;
            if (_local2 !== _arg1){
                this._2040570774currentTarget = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "currentTarget", _local2, _arg1));
            };
        }
        private function init(_arg1:FlexEvent):void{
            changeWatcher = BindingUtils.bindSetter(changeRoom, model, "room");
            timer = new Timer(1000, 1);
            timer.addEventListener(TimerEvent.TIMER_COMPLETE, removeBinding);
            if (isToDisableBinding){
                timer.start();
            };
            currentTarget = model.consumer.getCurrentDayTarget();
        }
        private function get currentTarget():String{
            return (this._2040570774currentTarget);
        }
        private function changeRoom(_arg1:Number):void{
            roomVO = RoomVO(Config.ROOM_CONFIGS.getItemAt(model.room));
        }
        private function set roomVO(_arg1:RoomVO):void{
            var _local2:Object = this._925318956roomVO;
            if (_local2 !== _arg1){
                this._925318956roomVO = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "roomVO", _local2, _arg1));
            };
        }
        private function get model():BalanceModelLocator{
            return (this._104069929model);
        }
        private function _WeekdayDataEntryInfo_bindingsSetup():Array{
            var binding:* = null;
            var result:* = [];
            binding = new Binding(this, function ():Array{
                return ([roomVO.buttonGradColour1, roomVO.buttonGradColour2]);
            }, function (_arg1:Array):void{
                this.setStyle("fillColors", _arg1);
            }, "this.fillColors");
            result[0] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = model.languageVO.getLang("Today").toUpperCase();
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                _WeekdayDataEntryInfo_Label1.text = _arg1;
            }, "_WeekdayDataEntryInfo_Label1.text");
            result[1] = binding;
            binding = new Binding(this, function ():uint{
                return (roomVO.buttonGradColour2);
            }, function (_arg1:uint):void{
                _WeekdayDataEntryInfo_HRule1.setStyle("strokeColor", _arg1);
            }, "_WeekdayDataEntryInfo_HRule1.strokeColor");
            result[2] = binding;
            binding = new Binding(this, function ():uint{
                return (roomVO.buttonGradColour1);
            }, function (_arg1:uint):void{
                _WeekdayDataEntryInfo_HRule2.setStyle("strokeColor", _arg1);
            }, "_WeekdayDataEntryInfo_HRule2.strokeColor");
            result[3] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = currentTarget;
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                _WeekdayDataEntryInfo_Label2.text = _arg1;
            }, "_WeekdayDataEntryInfo_Label2.text");
            result[4] = binding;
            return (result);
        }

        public static function set watcherSetupUtil(_arg1:IWatcherSetupUtil):void{
            WeekdayDataEntryInfo._watcherSetupUtil = _arg1;
        }

    }
}//package com.redbox.changetech.view.components 
