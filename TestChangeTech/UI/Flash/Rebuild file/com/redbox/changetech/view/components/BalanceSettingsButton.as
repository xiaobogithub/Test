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
    import com.degrafa.*;
    import com.degrafa.geometry.*;
    import com.degrafa.paint.*;
    import flash.accessibility.*;
    import flash.xml.*;
    import flash.net.*;
    import flash.filters.*;
    import flash.ui.*;
    import com.redbox.changetech.util.*;
    import flexlib.controls.*;
    import flash.external.*;
    import flash.debugger.*;
    import flash.errors.*;
    import flash.printing.*;
    import flash.profiler.*;

    public class BalanceSettingsButton extends CanvasButton implements IBindingClient {

        private var timer:Timer
        private var _115115tri:Surface
        public var _BalanceSettingsButton_SolidFill1:SolidFill
        private var _925318956roomVO:RoomVO
        mx_internal var _bindingsBeginWithWord:Object
        mx_internal var _watchers:Array
        mx_internal var _bindingsByDestination:Object
        private var _358005027buttonBase:HBox
        mx_internal var _bindings:Array
        private var _1782826776buttonField:Label
        private var _documentDescriptor_:UIComponentDescriptor
        private var _358169760buttonGrad:GradientCanvas

        private static var _watcherSetupUtil:IWatcherSetupUtil;

        public function BalanceSettingsButton(){
            _documentDescriptor_ = new UIComponentDescriptor({type:CanvasButton, propertiesFactory:function ():Object{
                return ({childDescriptors:[new UIComponentDescriptor({type:HBox, id:"buttonBase", stylesFactory:function ():void{
                    this.verticalAlign = "middle";
                    this.horizontalAlign = "center";
                    this.horizontalGap = 0;
                }, propertiesFactory:function ():Object{
                    return ({percentWidth:100, childDescriptors:[new UIComponentDescriptor({type:GradientCanvas, id:"buttonGrad", stylesFactory:function ():void{
                        this.fillAlphas = [1, 1];
                        this.gradientRatio = [0, 0xFF];
                        this.angle = [90];
                        this.borderAlphas = [0];
                        this.cornerRadius = 10;
                    }, propertiesFactory:function ():Object{
                        return ({percentWidth:100, height:25, colorsConfiguration:[2], childDescriptors:[new UIComponentDescriptor({type:HBox, stylesFactory:function ():void{
                            this.verticalAlign = "middle";
                            this.horizontalAlign = "center";
                            this.horizontalGap = 0;
                            this.paddingLeft = 7;
                            this.verticalGap = 0;
                        }, propertiesFactory:function ():Object{
                            return ({childDescriptors:[new UIComponentDescriptor({type:Spacer, propertiesFactory:function ():Object{
                                return ({width:5});
                            }}), new UIComponentDescriptor({type:Surface, id:"tri", propertiesFactory:function ():Object{
                                return ({width:8, height:8, graphicsData:[_BalanceSettingsButton_GeometryGroup1_c()]});
                            }}), new UIComponentDescriptor({type:Spacer, propertiesFactory:function ():Object{
                                return ({width:5});
                            }}), new UIComponentDescriptor({type:VRule, stylesFactory:function ():void{
                                this.strokeWidth = 1;
                                this.strokeColor = 0xE9E9E9;
                            }, propertiesFactory:function ():Object{
                                return ({height:23});
                            }}), new UIComponentDescriptor({type:Spacer, propertiesFactory:function ():Object{
                                return ({width:5});
                            }}), new UIComponentDescriptor({type:Label, id:"buttonField", stylesFactory:function ():void{
                                this.paddingTop = 0;
                                this.textAlign = "left";
                                this.fontFamily = "Helvetica Neue";
                                this.fontWeight = "normal";
                                this.fontSize = 11;
                            }})]});
                        }})]});
                    }})]});
                }})]});
            }});
            _925318956roomVO = RoomVO(Config.ROOM_CONFIGS.getItemAt(BalanceModelLocator.getInstance().room));
            _bindings = [];
            _watchers = [];
            _bindingsByDestination = {};
            _bindingsBeginWithWord = {};
            super();
            mx_internal::_document = this;
            this.percentWidth = 100;
            this.useHandCursor = true;
            this.buttonMode = true;
            this.styleName = "balanceButton";
            this.addEventListener("creationComplete", ___BalanceSettingsButton_CanvasButton1_creationComplete);
        }
        private function _BalanceSettingsButton_GraphicPoint1_c():GraphicPoint{
            var _local1:GraphicPoint = new GraphicPoint();
            _local1.x = 0;
            _local1.y = 0;
            return (_local1);
        }
        private function _BalanceSettingsButton_GraphicPoint2_c():GraphicPoint{
            var _local1:GraphicPoint = new GraphicPoint();
            _local1.x = 8;
            _local1.y = 0;
            return (_local1);
        }
        private function _BalanceSettingsButton_GraphicPoint3_c():GraphicPoint{
            var _local1:GraphicPoint = new GraphicPoint();
            _local1.x = 4;
            _local1.y = 8;
            return (_local1);
        }
        public function get tri():Surface{
            return (this._115115tri);
        }
        public function set buttonBase(_arg1:HBox):void{
            var _local2:Object = this._358005027buttonBase;
            if (_local2 !== _arg1){
                this._358005027buttonBase = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "buttonBase", _local2, _arg1));
            };
        }
        public function set buttonGrad(_arg1:GradientCanvas):void{
            var _local2:Object = this._358169760buttonGrad;
            if (_local2 !== _arg1){
                this._358169760buttonGrad = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "buttonGrad", _local2, _arg1));
            };
        }
        private function _BalanceSettingsButton_Polygon1_c():Polygon{
            var _local1:Polygon = new Polygon();
            _local1.fill = _BalanceSettingsButton_SolidFill1_i();
            _local1.points = [_BalanceSettingsButton_GraphicPoint1_c(), _BalanceSettingsButton_GraphicPoint2_c(), _BalanceSettingsButton_GraphicPoint3_c()];
            _local1.initialized(this, null);
            return (_local1);
        }
        public function set buttonField(_arg1:Label):void{
            var _local2:Object = this._1782826776buttonField;
            if (_local2 !== _arg1){
                this._1782826776buttonField = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "buttonField", _local2, _arg1));
            };
        }
        private function get roomVO():RoomVO{
            return (this._925318956roomVO);
        }
        private function set roomVO(_arg1:RoomVO):void{
            var _local2:Object = this._925318956roomVO;
            if (_local2 !== _arg1){
                this._925318956roomVO = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "roomVO", _local2, _arg1));
            };
        }
        private function init(_arg1:FlexEvent):void{
            BindingUtils.bindSetter(initChangeRoom, BalanceModelLocator.getInstance(), "room");
        }
        private function initChangeRoom(_arg1:int):void{
            timer = new Timer(2000, 1);
            timer.addEventListener(TimerEvent.TIMER_COMPLETE, changeRoom);
            timer.start();
        }
        public function set tri(_arg1:Surface):void{
            var _local2:Object = this._115115tri;
            if (_local2 !== _arg1){
                this._115115tri = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "tri", _local2, _arg1));
            };
        }
        override public function initialize():void{
            var target:* = null;
            var watcherSetupUtilClass:* = null;
            mx_internal::setDocumentDescriptor(_documentDescriptor_);
            var bindings:* = _BalanceSettingsButton_bindingsSetup();
            var watchers:* = [];
            target = this;
            if (_watcherSetupUtil == null){
                watcherSetupUtilClass = getDefinitionByName("_com_redbox_changetech_view_components_BalanceSettingsButtonWatcherSetupUtil");
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
        public function get buttonBase():HBox{
            return (this._358005027buttonBase);
        }
        private function _BalanceSettingsButton_bindingExprs():void{
            var _local1:*;
            _local1 = [0xFFFFFF, 0xE9E9E9];
            _local1 = roomVO.textColour1;
            _local1 = roomVO.textColour1;
            _local1 = label;
        }
        private function _BalanceSettingsButton_bindingsSetup():Array{
            var binding:* = null;
            var result:* = [];
            binding = new Binding(this, function ():Array{
                return ([0xFFFFFF, 0xE9E9E9]);
            }, function (_arg1:Array):void{
                buttonGrad.setStyle("fillColors", _arg1);
            }, "buttonGrad.fillColors");
            result[0] = binding;
            binding = new Binding(this, function ():Object{
                return (roomVO.textColour1);
            }, function (_arg1:Object):void{
                _BalanceSettingsButton_SolidFill1.color = _arg1;
            }, "_BalanceSettingsButton_SolidFill1.color");
            result[1] = binding;
            binding = new Binding(this, function ():uint{
                return (roomVO.textColour1);
            }, function (_arg1:uint):void{
                buttonField.setStyle("color", _arg1);
            }, "buttonField.color");
            result[2] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = label;
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                buttonField.text = _arg1;
            }, "buttonField.text");
            result[3] = binding;
            return (result);
        }
        private function changeRoom(_arg1:TimerEvent):void{
            timer.removeEventListener(TimerEvent.TIMER_COMPLETE, changeRoom);
            roomVO = RoomVO(Config.ROOM_CONFIGS.getItemAt(BalanceModelLocator.getInstance().room));
        }
        private function _BalanceSettingsButton_GeometryGroup1_c():GeometryGroup{
            var _local1:GeometryGroup = new GeometryGroup();
            _local1.geometry = [_BalanceSettingsButton_Polygon1_c()];
            _local1.initialized(this, null);
            return (_local1);
        }
        public function get buttonField():Label{
            return (this._1782826776buttonField);
        }
        public function get buttonGrad():GradientCanvas{
            return (this._358169760buttonGrad);
        }
        private function _BalanceSettingsButton_SolidFill1_i():SolidFill{
            var _local1:SolidFill = new SolidFill();
            _BalanceSettingsButton_SolidFill1 = _local1;
            BindingManager.executeBindings(this, "_BalanceSettingsButton_SolidFill1", _BalanceSettingsButton_SolidFill1);
            _local1.initialized(this, "_BalanceSettingsButton_SolidFill1");
            return (_local1);
        }
        public function ___BalanceSettingsButton_CanvasButton1_creationComplete(_arg1:FlexEvent):void{
            init(_arg1);
        }

        public static function set watcherSetupUtil(_arg1:IWatcherSetupUtil):void{
            BalanceSettingsButton._watcherSetupUtil = _arg1;
        }

    }
}//package com.redbox.changetech.view.components 
