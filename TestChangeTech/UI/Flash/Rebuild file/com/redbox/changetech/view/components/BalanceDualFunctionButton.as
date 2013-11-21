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
    import assets.*;
    import com.redbox.changetech.vo.*;
    import flash.utils.*;
    import flash.system.*;
    import flash.accessibility.*;
    import flash.xml.*;
    import flash.net.*;
    import com.redbox.changetech.view.modules.*;
    import flash.filters.*;
    import flash.ui.*;
    import com.redbox.changetech.util.*;
    import flexlib.controls.*;
    import flash.external.*;
    import flash.debugger.*;
    import flash.errors.*;
    import flash.printing.*;
    import flash.profiler.*;

    public class BalanceDualFunctionButton extends CanvasButton implements IBindingClient {

        private var _isMultiLine:Boolean = true
        private var _31958127LEFT_BUTTON_WIDTH:Number = 45
        private var _537102376left_buttonGrad:GradientCanvas
        public var isToDisableBinding:Boolean = true
        public var action:ButtonActionVO
        private var timer:Timer
        mx_internal var _bindingsByDestination:Object
        private var _1416535937iconClass:Class
        private var _620623609buttonHeight:Number
        private var _1767127628buttonWidth:Number
        private var _2131892397fillColor_2:Number
        public var direction:String
        private var mouseOverRight:Boolean
        public var buttonType:String
        private var _925318956roomVO:RoomVO
        mx_internal var _watchers:Array
        private var _358005027buttonBase:HBox
        public var _BalanceDualFunctionButton_VRule1:VRule
        public var _BalanceDualFunctionButton_VRule2:VRule
        private var _1410965406iconImage:Image
        private var _434067805right_buttonGrad:GradientCanvas
        private var _1782826776buttonField:Text
        private var _defaultTextWidth:Number = 175
        private var mouseOverLeft:Boolean
        private var _264524434contentBox:HBox
        mx_internal var _bindingsBeginWithWord:Object
        private var _2131892398fillColor_1:Number
        private var _527713131right_contentBox:HBox
        private var changeWatcher:ChangeWatcher
        private var remTransform:ColorTransform
        private var contentChangeWatcher:ChangeWatcher
        mx_internal var _bindings:Array
        private var _documentDescriptor_:UIComponentDescriptor

        public static var EVENT_CLICKED:String = "EVENT_CLICKED";
        private static var _watcherSetupUtil:IWatcherSetupUtil;

        public function BalanceDualFunctionButton(){
            _documentDescriptor_ = new UIComponentDescriptor({type:CanvasButton, propertiesFactory:function ():Object{
                return ({childDescriptors:[new UIComponentDescriptor({type:HBox, id:"buttonBase", stylesFactory:function ():void{
                    this.verticalAlign = "middle";
                    this.horizontalGap = 0;
                }, propertiesFactory:function ():Object{
                    return ({childDescriptors:[new UIComponentDescriptor({type:GradientCanvas, id:"left_buttonGrad", stylesFactory:function ():void{
                        this.fillAlphas = [1, 1];
                        this.gradientRatio = [0, 0xFF];
                        this.angle = [90];
                        this.borderAlphas = [0];
                        this.topRightRadius = 0;
                        this.bottomRightRadius = 0;
                        this.topLeftRadius = 10;
                        this.bottomLeftRadius = 10;
                    }, propertiesFactory:function ():Object{
                        return ({colorsConfiguration:[2], verticalScrollPolicy:"off", horizontalScrollPolicy:"off", childDescriptors:[new UIComponentDescriptor({type:HBox, id:"contentBox", stylesFactory:function ():void{
                            this.verticalAlign = "middle";
                            this.horizontalGap = 0;
                            this.paddingLeft = 10;
                            this.verticalGap = 0;
                        }, propertiesFactory:function ():Object{
                            return ({childDescriptors:[new UIComponentDescriptor({type:Image, id:"iconImage", stylesFactory:function ():void{
                                this.verticalAlign = "middle";
                            }, propertiesFactory:function ():Object{
                                return ({height:27, scaleContent:false});
                            }}), new UIComponentDescriptor({type:Spacer, propertiesFactory:function ():Object{
                                return ({width:10});
                            }})]});
                        }})]});
                    }}), new UIComponentDescriptor({type:GradientCanvas, id:"right_buttonGrad", stylesFactory:function ():void{
                        this.fillAlphas = [1, 1];
                        this.gradientRatio = [0, 0xFF];
                        this.angle = [90];
                        this.borderAlphas = [0];
                        this.topRightRadius = 10;
                        this.bottomRightRadius = 10;
                        this.topLeftRadius = 0;
                        this.bottomLeftRadius = 0;
                    }, propertiesFactory:function ():Object{
                        return ({colorsConfiguration:[2], verticalScrollPolicy:"off", horizontalScrollPolicy:"off", childDescriptors:[new UIComponentDescriptor({type:VRule, id:"_BalanceDualFunctionButton_VRule1", stylesFactory:function ():void{
                            this.strokeWidth = 1;
                        }}), new UIComponentDescriptor({type:VRule, id:"_BalanceDualFunctionButton_VRule2", stylesFactory:function ():void{
                            this.strokeWidth = 1;
                        }}), new UIComponentDescriptor({type:HBox, id:"right_contentBox", stylesFactory:function ():void{
                            this.verticalAlign = "middle";
                            this.horizontalGap = 0;
                            this.paddingLeft = 10;
                            this.verticalGap = 0;
                        }, propertiesFactory:function ():Object{
                            return ({childDescriptors:[new UIComponentDescriptor({type:Spacer, propertiesFactory:function ():Object{
                                return ({width:10});
                            }}), new UIComponentDescriptor({type:Text, id:"buttonField", events:{creationComplete:"__buttonField_creationComplete"}, stylesFactory:function ():void{
                                this.paddingTop = 2;
                                this.textAlign = "left";
                            }, propertiesFactory:function ():Object{
                                return ({selectable:false, styleName:"helvNeueMed14white"});
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
            this.useHandCursor = true;
            this.buttonMode = true;
            this.styleName = "balanceButton";
            this.toolTip = "";
            this.addEventListener("creationComplete", ___BalanceDualFunctionButton_CanvasButton1_creationComplete);
        }
        private function get buttonHeight():Number{
            return (this._620623609buttonHeight);
        }
        private function set buttonHeight(_arg1:Number):void{
            var _local2:Object = this._620623609buttonHeight;
            if (_local2 !== _arg1){
                this._620623609buttonHeight = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "buttonHeight", _local2, _arg1));
            };
        }
        private function set iconClass(_arg1:Class):void{
            var _local2:Object = this._1416535937iconClass;
            if (_local2 !== _arg1){
                this._1416535937iconClass = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "iconClass", _local2, _arg1));
            };
        }
        private function mouseOutLeftHandler():void{
            iconOutHandler();
            left_buttonGrad.transform.colorTransform = remTransform;
            mouseOverLeft = false;
        }
        private function clickedHandler(_arg1:MouseEvent):void{
            dispatchEvent(new Event(EVENT_CLICKED, true, false));
        }
        public function set contentBox(_arg1:HBox):void{
            var _local2:Object = this._264524434contentBox;
            if (_local2 !== _arg1){
                this._264524434contentBox = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "contentBox", _local2, _arg1));
            };
        }
        public function get buttonBase():HBox{
            return (this._358005027buttonBase);
        }
        public function set right_contentBox(_arg1:HBox):void{
            var _local2:Object = this._527713131right_contentBox;
            if (_local2 !== _arg1){
                this._527713131right_contentBox = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "right_contentBox", _local2, _arg1));
            };
        }
        private function _BalanceDualFunctionButton_bindingExprs():void{
            var _local1:*;
            _local1 = LEFT_BUTTON_WIDTH;
            _local1 = buttonHeight;
            _local1 = getFillColors();
            _local1 = buttonHeight;
            _local1 = iconClass;
            _local1 = ((buttonWidth - LEFT_BUTTON_WIDTH) - 2);
            _local1 = buttonHeight;
            _local1 = getFillColors();
            _local1 = buttonHeight;
            _local1 = fillColor_2;
            _local1 = buttonHeight;
            _local1 = fillColor_1;
            _local1 = buttonHeight;
            _local1 = label;
        }
        private function set LEFT_BUTTON_WIDTH(_arg1:Number):void{
            var _local2:Object = this._31958127LEFT_BUTTON_WIDTH;
            if (_local2 !== _arg1){
                this._31958127LEFT_BUTTON_WIDTH = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "LEFT_BUTTON_WIDTH", _local2, _arg1));
            };
        }
        public function set buttonBase(_arg1:HBox):void{
            var _local2:Object = this._358005027buttonBase;
            if (_local2 !== _arg1){
                this._358005027buttonBase = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "buttonBase", _local2, _arg1));
            };
        }
        private function mouseOverRightHandler():void{
            mouseOverRight = true;
            var _local1:Number = right_buttonGrad.transform.colorTransform.redOffset;
            var _local2:Number = right_buttonGrad.transform.colorTransform.blueOffset;
            var _local3:Number = right_buttonGrad.transform.colorTransform.greenOffset;
            remTransform = new ColorTransform(1, 1, 1, 1, _local1, _local3, _local2, 0);
            right_buttonGrad.transform.colorTransform = new ColorTransform(1, 1, 1, 1, (_local1 + 30), (_local3 + 30), (_local2 + 30), 0);
        }
        private function completed(_arg1:FlexEvent):void{
            addEventListener(MouseEvent.MOUSE_OVER, checkRollover);
            addEventListener(MouseEvent.MOUSE_OUT, mouseOutHandler);
            addEventListener(MouseEvent.CLICK, clickedHandler);
            addEventListener(MouseEvent.MOUSE_MOVE, checkRollover);
            textField.visible = false;
            changeWatcher = BindingUtils.bindSetter(changeRoom, BalanceModelLocator.getInstance(), "room");
            contentChangeWatcher = BindingUtils.bindSetter(changeContent, BalanceModelLocator.getInstance(), "collectionContentIndex");
            timer = new Timer(1000, 1);
            timer.addEventListener(TimerEvent.TIMER_COMPLETE, removeBinding);
            if (isToDisableBinding){
                timer.start();
            };
            buttonWidth = getDims().width;
            buttonHeight = getDims().height;
            iconClass = getIconClass();
            direction = BasicModule.NEXT;
            fillColor_1 = getFillColors()[0];
            fillColor_2 = getFillColors()[1];
        }
        private function get fillColor_2():Number{
            return (this._2131892397fillColor_2);
        }
        public function set right_buttonGrad(_arg1:GradientCanvas):void{
            var _local2:Object = this._434067805right_buttonGrad;
            if (_local2 !== _arg1){
                this._434067805right_buttonGrad = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "right_buttonGrad", _local2, _arg1));
            };
        }
        public function get defaultTextWidth():Number{
            return (_defaultTextWidth);
        }
        private function get fillColor_1():Number{
            return (this._2131892398fillColor_1);
        }
        private function set _765914968defaultTextWidth(_arg1:Number):void{
            _defaultTextWidth = _arg1;
        }
        private function removeBinding(_arg1:TimerEvent):void{
            changeWatcher.unwatch();
        }
        public function get iconImage():Image{
            return (this._1410965406iconImage);
        }
        private function mouseOverLeftHandler():void{
            mouseOverLeft = true;
            var _local1:Number = left_buttonGrad.transform.colorTransform.redOffset;
            var _local2:Number = left_buttonGrad.transform.colorTransform.blueOffset;
            var _local3:Number = left_buttonGrad.transform.colorTransform.greenOffset;
            remTransform = new ColorTransform(1, 1, 1, 1, _local1, _local3, _local2, 0);
            left_buttonGrad.transform.colorTransform = new ColorTransform(1, 1, 1, 1, (_local1 + 30), (_local3 + 30), (_local2 + 30), 0);
        }
        private function set buttonWidth(_arg1:Number):void{
            var _local2:Object = this._1767127628buttonWidth;
            if (_local2 !== _arg1){
                this._1767127628buttonWidth = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "buttonWidth", _local2, _arg1));
            };
        }
        private function getDims():Object{
            switch (buttonType){
                case Button.PRIMARY:
                    return ({width:275, height:85});
                case Button.SECONDARY:
                    return ({width:150, height:85});
                case Button.TERTIARY:
                    return ({width:275, height:30});
                case Button.EMBED_1:
                    return ({width:275, height:30});
                case Button.EMBED_2:
                    return ({width:275, height:30});
            };
            return ({width:20, height:20});
        }
        private function _BalanceDualFunctionButton_bindingsSetup():Array{
            var binding:* = null;
            var result:* = [];
            binding = new Binding(this, function ():Number{
                return (LEFT_BUTTON_WIDTH);
            }, function (_arg1:Number):void{
                left_buttonGrad.width = _arg1;
            }, "left_buttonGrad.width");
            result[0] = binding;
            binding = new Binding(this, function ():Number{
                return (buttonHeight);
            }, function (_arg1:Number):void{
                left_buttonGrad.height = _arg1;
            }, "left_buttonGrad.height");
            result[1] = binding;
            binding = new Binding(this, function ():Array{
                return (getFillColors());
            }, function (_arg1:Array):void{
                left_buttonGrad.setStyle("fillColors", _arg1);
            }, "left_buttonGrad.fillColors");
            result[2] = binding;
            binding = new Binding(this, function ():Number{
                return (buttonHeight);
            }, function (_arg1:Number):void{
                contentBox.height = _arg1;
            }, "contentBox.height");
            result[3] = binding;
            binding = new Binding(this, function ():Object{
                return (iconClass);
            }, function (_arg1:Object):void{
                iconImage.source = _arg1;
            }, "iconImage.source");
            result[4] = binding;
            binding = new Binding(this, function ():Number{
                return (((buttonWidth - LEFT_BUTTON_WIDTH) - 2));
            }, function (_arg1:Number):void{
                right_buttonGrad.width = _arg1;
            }, "right_buttonGrad.width");
            result[5] = binding;
            binding = new Binding(this, function ():Number{
                return (buttonHeight);
            }, function (_arg1:Number):void{
                right_buttonGrad.height = _arg1;
            }, "right_buttonGrad.height");
            result[6] = binding;
            binding = new Binding(this, function ():Array{
                return (getFillColors());
            }, function (_arg1:Array):void{
                right_buttonGrad.setStyle("fillColors", _arg1);
            }, "right_buttonGrad.fillColors");
            result[7] = binding;
            binding = new Binding(this, function ():Number{
                return (buttonHeight);
            }, function (_arg1:Number):void{
                _BalanceDualFunctionButton_VRule1.height = _arg1;
            }, "_BalanceDualFunctionButton_VRule1.height");
            result[8] = binding;
            binding = new Binding(this, function ():uint{
                return (fillColor_2);
            }, function (_arg1:uint):void{
                _BalanceDualFunctionButton_VRule1.setStyle("strokeColor", _arg1);
            }, "_BalanceDualFunctionButton_VRule1.strokeColor");
            result[9] = binding;
            binding = new Binding(this, function ():Number{
                return (buttonHeight);
            }, function (_arg1:Number):void{
                _BalanceDualFunctionButton_VRule2.height = _arg1;
            }, "_BalanceDualFunctionButton_VRule2.height");
            result[10] = binding;
            binding = new Binding(this, function ():uint{
                return (fillColor_1);
            }, function (_arg1:uint):void{
                _BalanceDualFunctionButton_VRule2.setStyle("strokeColor", _arg1);
            }, "_BalanceDualFunctionButton_VRule2.strokeColor");
            result[11] = binding;
            binding = new Binding(this, function ():Number{
                return (buttonHeight);
            }, function (_arg1:Number):void{
                right_contentBox.height = _arg1;
            }, "right_contentBox.height");
            result[12] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = label;
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                buttonField.text = _arg1;
            }, "buttonField.text");
            result[13] = binding;
            return (result);
        }
        private function iconOutHandler():void{
            if (buttonType == Button.PRIMARY){
                direction = BasicModule.NEXT;
            };
        }
        public function set isMultiLine(_arg1:Boolean):void{
            _isMultiLine = _arg1;
        }
        private function get iconClass():Class{
            return (this._1416535937iconClass);
        }
        public function __buttonField_creationComplete(_arg1:FlexEvent):void{
            buttonField.width = (isMultiLine) ? defaultTextWidth : (buttonField.textWidth + 10);
        }
        override public function initialize():void{
            var target:* = null;
            var watcherSetupUtilClass:* = null;
            mx_internal::setDocumentDescriptor(_documentDescriptor_);
            var bindings:* = _BalanceDualFunctionButton_bindingsSetup();
            var watchers:* = [];
            target = this;
            if (_watcherSetupUtil == null){
                watcherSetupUtilClass = getDefinitionByName("_com_redbox_changetech_view_components_BalanceDualFunctionButtonWatcherSetupUtil");
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
        public function get contentBox():HBox{
            return (this._264524434contentBox);
        }
        public function ___BalanceDualFunctionButton_CanvasButton1_creationComplete(_arg1:FlexEvent):void{
            completed(_arg1);
        }
        public function get right_contentBox():HBox{
            return (this._527713131right_contentBox);
        }
        private function iconOverHandler():void{
            direction = BasicModule.BACK;
        }
        private function mouseOutRightHandler():void{
            right_buttonGrad.transform.colorTransform = remTransform;
            mouseOverRight = false;
        }
        private function get LEFT_BUTTON_WIDTH():Number{
            return (this._31958127LEFT_BUTTON_WIDTH);
        }
        private function mouseOutHandler(_arg1:Event):void{
            mouseOutLeftHandler();
            mouseOutRightHandler();
            iconOutHandler();
        }
        public function get right_buttonGrad():GradientCanvas{
            return (this._434067805right_buttonGrad);
        }
        private function set fillColor_2(_arg1:Number):void{
            var _local2:Object = this._2131892397fillColor_2;
            if (_local2 !== _arg1){
                this._2131892397fillColor_2 = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "fillColor_2", _local2, _arg1));
            };
        }
        private function set roomVO(_arg1:RoomVO):void{
            var _local2:Object = this._925318956roomVO;
            if (_local2 !== _arg1){
                this._925318956roomVO = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "roomVO", _local2, _arg1));
            };
        }
        private function changeContent(_arg1:Number):void{
            iconClass = getIconClass();
        }
        private function changeRoom(_arg1:int):void{
            roomVO = RoomVO(Config.ROOM_CONFIGS.getItemAt(_arg1));
        }
        public function set left_buttonGrad(_arg1:GradientCanvas):void{
            var _local2:Object = this._537102376left_buttonGrad;
            if (_local2 !== _arg1){
                this._537102376left_buttonGrad = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "left_buttonGrad", _local2, _arg1));
            };
        }
        private function set fillColor_1(_arg1:Number):void{
            var _local2:Object = this._2131892398fillColor_1;
            if (_local2 !== _arg1){
                this._2131892398fillColor_1 = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "fillColor_1", _local2, _arg1));
            };
        }
        private function get buttonWidth():Number{
            return (this._1767127628buttonWidth);
        }
        public function set defaultTextWidth(_arg1:Number):void{
            var _local2:Object = this.defaultTextWidth;
            if (_local2 !== _arg1){
                this._765914968defaultTextWidth = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "defaultTextWidth", _local2, _arg1));
            };
        }
        public function get isMultiLine():Boolean{
            return (_isMultiLine);
        }
        public function set iconImage(_arg1:Image):void{
            var _local2:Object = this._1410965406iconImage;
            if (_local2 !== _arg1){
                this._1410965406iconImage = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "iconImage", _local2, _arg1));
            };
        }
        private function getFillColors():Array{
            switch (buttonType){
                case Button.PRIMARY:
                    return ([roomVO.buttonGradColour1, roomVO.buttonGradColour2]);
                case Button.SECONDARY:
                    return ([0xE2E2E2, 0x8B8B8B]);
                case Button.TERTIARY:
                    return ([0xE2E2E2, 0x8B8B8B]);
                case Button.EMBED_1:
                    return ([roomVO.buttonGradColour1, roomVO.buttonGradColour2]);
                case Button.EMBED_2:
                    return ([0xE2E2E2, 0x8B8B8B]);
            };
            return ([0xFF0000, 0xFF0000]);
        }
        private function get roomVO():RoomVO{
            return (this._925318956roomVO);
        }
        public function set buttonField(_arg1:Text):void{
            var _local2:Object = this._1782826776buttonField;
            if (_local2 !== _arg1){
                this._1782826776buttonField = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "buttonField", _local2, _arg1));
            };
        }
        public function get left_buttonGrad():GradientCanvas{
            return (this._537102376left_buttonGrad);
        }
        private function getIconClass():Class{
            switch (buttonType){
                case Button.PRIMARY:
                    return (Assets.getInstance().back_icon);
                case Button.SECONDARY:
                    return (Assets.getInstance().continue_icon);
                case Button.TERTIARY:
                    return (Assets.getInstance().continue_icon);
                case Button.EMBED_1:
                    return (Assets.getInstance().continue_icon);
                case Button.EMBED_2:
                    return (Assets.getInstance().continue_icon);
            };
            return (Assets.getInstance().continue_icon);
        }
        public function get buttonField():Text{
            return (this._1782826776buttonField);
        }
        private function checkRollover(_arg1:MouseEvent):void{
            if (BalanceModelLocator.getInstance().collectionContentIndex == 0){
                if (!mouseOverLeft){
                    mouseOverLeftHandler();
                    mouseOverRightHandler();
                };
            } else {
                if (mouseX < LEFT_BUTTON_WIDTH){
                    if (!mouseOverLeft){
                        mouseOverLeftHandler();
                        mouseOutRightHandler();
                        iconOverHandler();
                    };
                } else {
                    if (!mouseOverRight){
                        mouseOverRightHandler();
                        mouseOutLeftHandler();
                        iconOutHandler();
                    };
                };
            };
        }

        public static function set watcherSetupUtil(_arg1:IWatcherSetupUtil):void{
            BalanceDualFunctionButton._watcherSetupUtil = _arg1;
        }

    }
}//package com.redbox.changetech.view.components 
