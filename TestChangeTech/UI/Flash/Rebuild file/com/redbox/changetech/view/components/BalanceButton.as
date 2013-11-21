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

    public class BalanceButton extends CanvasButton implements IBindingClient {

        private var _isMultiLine:Boolean = true
        public var buttonType:String
        private var _925318956roomVO:RoomVO
        mx_internal var _watchers:Array
        private var _358005027buttonBase:HBox
        private var _1410965406iconImage:Image
        public var isToDisableBinding:Boolean = true
        public var _BalanceButton_VRule1:VRule
        public var _BalanceButton_VRule2:VRule
        private var _358169760buttonGrad:GradientCanvas
        private var _1782826776buttonField:Text
        private var _defaultTextWidth:Number = 175
        public var action:ButtonActionVO
        private var _264524434contentBox:HBox
        private var timer:Timer
        mx_internal var _bindingsByDestination:Object
        mx_internal var _bindingsBeginWithWord:Object
        private var _1416535937iconClass:Class
        private var _2131892398fillColor_1:Number
        private var _620623609buttonHeight:Number
        private var changeWatcher:ChangeWatcher
        mx_internal var _bindings:Array
        private var _1767127628buttonWidth:Number
        private var remTransform:ColorTransform
        private var _documentDescriptor_:UIComponentDescriptor
        private var _2131892397fillColor_2:Number
        public var direction:String

        public static var EVENT_CLICKED:String = "EVENT_CLICKED";
        private static var _watcherSetupUtil:IWatcherSetupUtil;

        public function BalanceButton(){
            _documentDescriptor_ = new UIComponentDescriptor({type:CanvasButton, propertiesFactory:function ():Object{
                return ({childDescriptors:[new UIComponentDescriptor({type:HBox, id:"buttonBase", stylesFactory:function ():void{
                    this.verticalAlign = "middle";
                    this.horizontalGap = 0;
                }, propertiesFactory:function ():Object{
                    return ({childDescriptors:[new UIComponentDescriptor({type:GradientCanvas, id:"buttonGrad", stylesFactory:function ():void{
                        this.fillAlphas = [1, 1];
                        this.gradientRatio = [0, 0xFF];
                        this.angle = [90];
                        this.borderAlphas = [0];
                        this.cornerRadius = 10;
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
                            }}), new UIComponentDescriptor({type:VRule, id:"_BalanceButton_VRule1", stylesFactory:function ():void{
                                this.strokeWidth = 1;
                            }}), new UIComponentDescriptor({type:VRule, id:"_BalanceButton_VRule2", stylesFactory:function ():void{
                                this.strokeWidth = 1;
                            }}), new UIComponentDescriptor({type:Spacer, propertiesFactory:function ():Object{
                                return ({width:10});
                            }}), new UIComponentDescriptor({type:Text, id:"buttonField", stylesFactory:function ():void{
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
            this.addEventListener("creationComplete", ___BalanceButton_CanvasButton1_creationComplete);
        }
        public function get buttonField():Text{
            return (this._1782826776buttonField);
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
        public function set isMultiLine(_arg1:Boolean):void{
            _isMultiLine = _arg1;
        }
        private function get iconClass():Class{
            return (this._1416535937iconClass);
        }
        private function _BalanceButton_bindingExprs():void{
            var _local1:*;
            _local1 = buttonWidth;
            _local1 = buttonHeight;
            _local1 = getFillColors();
            _local1 = iconClass;
            _local1 = buttonHeight;
            _local1 = fillColor_2;
            _local1 = buttonHeight;
            _local1 = fillColor_1;
            _local1 = label;
            _local1 = ((buttonWidth - buttonField.x) - 3);
        }
        private function checkIconRollover(_arg1:MouseEvent):void{
            if ((((((((mouseX > iconImage.x)) && ((mouseX < (iconImage.x + iconImage.width))))) && ((mouseY > iconImage.y)))) && ((mouseY < (iconImage.y + iconImage.height))))){
                iconOverHandler();
            } else {
                iconOutHandler();
            };
        }
        private function set buttonWidth(_arg1:Number):void{
            var _local2:Object = this._1767127628buttonWidth;
            if (_local2 !== _arg1){
                this._1767127628buttonWidth = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "buttonWidth", _local2, _arg1));
            };
        }
        override public function initialize():void{
            var target:* = null;
            var watcherSetupUtilClass:* = null;
            mx_internal::setDocumentDescriptor(_documentDescriptor_);
            var bindings:* = _BalanceButton_bindingsSetup();
            var watchers:* = [];
            target = this;
            if (_watcherSetupUtil == null){
                watcherSetupUtilClass = getDefinitionByName("_com_redbox_changetech_view_components_BalanceButtonWatcherSetupUtil");
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
        private function iconOverHandler():void{
            if (buttonType == Button.PRIMARY){
                if (BalanceModelLocator.getInstance().collectionContentIndex > 0){
                    iconClass = Assets.getInstance().back_icon;
                    direction = BasicModule.BACK;
                };
            };
        }
        public function ___BalanceButton_CanvasButton1_creationComplete(_arg1:FlexEvent):void{
            completed(_arg1);
        }
        private function mouseOutHandler(_arg1:MouseEvent):void{
            buttonGrad.transform.colorTransform = remTransform;
        }
        private function clickedHandler(_arg1:MouseEvent):void{
            dispatchEvent(new Event(EVENT_CLICKED, true, false));
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
        private function changeRoom(_arg1:int):void{
            roomVO = RoomVO(Config.ROOM_CONFIGS.getItemAt(_arg1));
        }
        public function get buttonBase():HBox{
            return (this._358005027buttonBase);
        }
        public function get buttonGrad():GradientCanvas{
            return (this._358169760buttonGrad);
        }
        public function set contentBox(_arg1:HBox):void{
            var _local2:Object = this._264524434contentBox;
            if (_local2 !== _arg1){
                this._264524434contentBox = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "contentBox", _local2, _arg1));
            };
        }
        public function set defaultTextWidth(_arg1:Number):void{
            var _local2:Object = this.defaultTextWidth;
            if (_local2 !== _arg1){
                this._765914968defaultTextWidth = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "defaultTextWidth", _local2, _arg1));
            };
        }
        public function set buttonGrad(_arg1:GradientCanvas):void{
            var _local2:Object = this._358169760buttonGrad;
            if (_local2 !== _arg1){
                this._358169760buttonGrad = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "buttonGrad", _local2, _arg1));
            };
        }
        public function get isMultiLine():Boolean{
            return (_isMultiLine);
        }
        public function set buttonBase(_arg1:HBox):void{
            var _local2:Object = this._358005027buttonBase;
            if (_local2 !== _arg1){
                this._358005027buttonBase = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "buttonBase", _local2, _arg1));
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
        private function completed(_arg1:FlexEvent):void{
            addEventListener(MouseEvent.MOUSE_OVER, mouseOverHandler);
            addEventListener(MouseEvent.MOUSE_OUT, mouseOutHandler);
            addEventListener(MouseEvent.CLICK, clickedHandler);
            addEventListener(MouseEvent.MOUSE_MOVE, checkIconRollover);
            textField.visible = false;
            changeWatcher = BindingUtils.bindSetter(changeRoom, BalanceModelLocator.getInstance(), "room");
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
        private function set fillColor_1(_arg1:Number):void{
            var _local2:Object = this._2131892398fillColor_1;
            if (_local2 !== _arg1){
                this._2131892398fillColor_1 = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "fillColor_1", _local2, _arg1));
            };
        }
        public function set iconImage(_arg1:Image):void{
            var _local2:Object = this._1410965406iconImage;
            if (_local2 !== _arg1){
                this._1410965406iconImage = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "iconImage", _local2, _arg1));
            };
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
        private function get fillColor_1():Number{
            return (this._2131892398fillColor_1);
        }
        private function set _765914968defaultTextWidth(_arg1:Number):void{
            _defaultTextWidth = _arg1;
        }
        public function get defaultTextWidth():Number{
            return (_defaultTextWidth);
        }
        private function removeBinding(_arg1:TimerEvent):void{
            changeWatcher.unwatch();
        }
        public function get iconImage():Image{
            return (this._1410965406iconImage);
        }
        private function _BalanceButton_bindingsSetup():Array{
            var binding:* = null;
            var result:* = [];
            binding = new Binding(this, function ():Number{
                return (buttonWidth);
            }, function (_arg1:Number):void{
                buttonGrad.width = _arg1;
            }, "buttonGrad.width");
            result[0] = binding;
            binding = new Binding(this, function ():Number{
                return (buttonHeight);
            }, function (_arg1:Number):void{
                buttonGrad.height = _arg1;
            }, "buttonGrad.height");
            result[1] = binding;
            binding = new Binding(this, function ():Array{
                return (getFillColors());
            }, function (_arg1:Array):void{
                buttonGrad.setStyle("fillColors", _arg1);
            }, "buttonGrad.fillColors");
            result[2] = binding;
            binding = new Binding(this, function ():Object{
                return (iconClass);
            }, function (_arg1:Object):void{
                iconImage.source = _arg1;
            }, "iconImage.source");
            result[3] = binding;
            binding = new Binding(this, function ():Number{
                return (buttonHeight);
            }, function (_arg1:Number):void{
                _BalanceButton_VRule1.height = _arg1;
            }, "_BalanceButton_VRule1.height");
            result[4] = binding;
            binding = new Binding(this, function ():uint{
                return (fillColor_2);
            }, function (_arg1:uint):void{
                _BalanceButton_VRule1.setStyle("strokeColor", _arg1);
            }, "_BalanceButton_VRule1.strokeColor");
            result[5] = binding;
            binding = new Binding(this, function ():Number{
                return (buttonHeight);
            }, function (_arg1:Number):void{
                _BalanceButton_VRule2.height = _arg1;
            }, "_BalanceButton_VRule2.height");
            result[6] = binding;
            binding = new Binding(this, function ():uint{
                return (fillColor_1);
            }, function (_arg1:uint):void{
                _BalanceButton_VRule2.setStyle("strokeColor", _arg1);
            }, "_BalanceButton_VRule2.strokeColor");
            result[7] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = label;
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                buttonField.text = _arg1;
            }, "buttonField.text");
            result[8] = binding;
            binding = new Binding(this, function ():Number{
                return (((buttonWidth - buttonField.x) - 3));
            }, function (_arg1:Number):void{
                buttonField.width = _arg1;
            }, "buttonField.width");
            result[9] = binding;
            return (result);
        }
        private function get fillColor_2():Number{
            return (this._2131892397fillColor_2);
        }
        private function getIconClass():Class{
            switch (buttonType){
                case Button.PRIMARY:
                    return (Assets.getInstance().continue_icon);
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
        private function getDims():Object{
            switch (buttonType){
                case Button.PRIMARY:
                    return ({width:275, height:85});
                case Button.SECONDARY:
                    return ({width:150, height:85});
                case Button.TERTIARY:
                    return ({width:275, height:30});
                case Button.EMBED_1:
                    return ({width:375, height:60});
                case Button.EMBED_2:
                    return ({width:375, height:60});
            };
            return ({width:20, height:20});
        }
        private function mouseOverHandler(_arg1:MouseEvent):void{
            var _local2:Number = buttonGrad.transform.colorTransform.redOffset;
            var _local3:Number = buttonGrad.transform.colorTransform.blueOffset;
            var _local4:Number = buttonGrad.transform.colorTransform.greenOffset;
            remTransform = new ColorTransform(1, 1, 1, 1, _local2, _local4, _local3, 0);
            buttonGrad.transform.colorTransform = new ColorTransform(1, 1, 1, 1, (_local2 + 30), (_local4 + 30), (_local3 + 30), 0);
        }
        private function get buttonHeight():Number{
            return (this._620623609buttonHeight);
        }
        private function get buttonWidth():Number{
            return (this._1767127628buttonWidth);
        }
        private function iconOutHandler():void{
            if (buttonType == Button.PRIMARY){
                iconClass = Assets.getInstance().continue_icon;
                direction = BasicModule.NEXT;
            };
        }

        public static function set watcherSetupUtil(_arg1:IWatcherSetupUtil):void{
            BalanceButton._watcherSetupUtil = _arg1;
        }

    }
}//package com.redbox.changetech.view.components 
