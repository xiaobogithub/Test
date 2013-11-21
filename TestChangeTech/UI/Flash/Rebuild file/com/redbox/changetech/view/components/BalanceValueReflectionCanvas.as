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
    import com.rictus.reflector.*;
    import flash.filters.*;
    import flash.ui.*;
    import com.redbox.changetech.util.*;
    import flash.external.*;
    import flash.debugger.*;
    import flash.errors.*;
    import flash.printing.*;
    import flash.profiler.*;

    public class BalanceValueReflectionCanvas extends Canvas implements IBindingClient {

        private var _925318956roomVO:RoomVO
        mx_internal var _watchers:Array
        private var _197399988borderCol:Number
        private var _1231798911copyHolder:VBox
        private var _1088955738value_label:String
        public var _BalanceValueReflectionCanvas_Image1:Image
        private var _1465045550value_description:String
        public var _BalanceValueReflectionCanvas_TextArea1:TextArea
        public var _BalanceValueReflectionCanvas_TextArea2:TextArea
        public var isToDisableBinding:Boolean = true
        public var _BalanceValueReflectionCanvas_Reflector1:Reflector
        private var _content:Content
        private var _2042995335value_icon:String
        private var timer:Timer
        mx_internal var _bindingsBeginWithWord:Object
        mx_internal var _bindingsByDestination:Object
        mx_internal var _bindings:Array
        private var stuffToRemove:Array
        private var _documentDescriptor_:UIComponentDescriptor
        private var changeWatcher:ChangeWatcher
        private var _1939189620copyContainer:Canvas

        private static var _watcherSetupUtil:IWatcherSetupUtil;

        public function BalanceValueReflectionCanvas(){
            _documentDescriptor_ = new UIComponentDescriptor({type:Canvas, propertiesFactory:function ():Object{
                return ({width:435, childDescriptors:[new UIComponentDescriptor({type:Canvas, id:"copyContainer", propertiesFactory:function ():Object{
                    return ({width:430, styleName:"roundedGradCanvas", childDescriptors:[new UIComponentDescriptor({type:VBox, id:"copyHolder", stylesFactory:function ():void{
                        this.paddingLeft = 20;
                        this.paddingRight = 20;
                        this.paddingTop = 20;
                        this.paddingBottom = 50;
                    }, propertiesFactory:function ():Object{
                        return ({percentWidth:100, childDescriptors:[new UIComponentDescriptor({type:HBox, stylesFactory:function ():void{
                            this.paddingLeft = 0;
                            this.paddingRight = 0;
                            this.paddingTop = 0;
                            this.paddingBottom = 0;
                            this.verticalAlign = "middle";
                        }, propertiesFactory:function ():Object{
                            return ({childDescriptors:[new UIComponentDescriptor({type:Image, id:"_BalanceValueReflectionCanvas_Image1"}), new UIComponentDescriptor({type:VBox, stylesFactory:function ():void{
                                this.verticalGap = -10;
                                this.verticalAlign = "middle";
                            }, propertiesFactory:function ():Object{
                                return ({childDescriptors:[new UIComponentDescriptor({type:TextArea, id:"_BalanceValueReflectionCanvas_TextArea1", stylesFactory:function ():void{
                                    this.fontFamily = "Helvetica Neue";
                                    this.fontSize = 18;
                                }, propertiesFactory:function ():Object{
                                    return ({percentWidth:100});
                                }}), new UIComponentDescriptor({type:TextArea, id:"_BalanceValueReflectionCanvas_TextArea2", stylesFactory:function ():void{
                                    this.fontFamily = "Helvetica Neue";
                                    this.fontSize = 14;
                                }, propertiesFactory:function ():Object{
                                    return ({percentWidth:100});
                                }})]});
                            }})]});
                        }})]});
                    }})]});
                }}), new UIComponentDescriptor({type:Reflector, id:"_BalanceValueReflectionCanvas_Reflector1", propertiesFactory:function ():Object{
                    return ({falloff:0.5, alpha:0.5});
                }})]});
            }});
            _925318956roomVO = RoomVO(Config.ROOM_CONFIGS.getItemAt(BalanceModelLocator.getInstance().room));
            stuffToRemove = [];
            _bindings = [];
            _watchers = [];
            _bindingsByDestination = {};
            _bindingsBeginWithWord = {};
            super();
            mx_internal::_document = this;
            this.width = 435;
            this.addEventListener("creationComplete", ___BalanceValueReflectionCanvas_Canvas1_creationComplete);
        }
        public function set copyHolder(_arg1:VBox):void{
            var _local2:Object = this._1231798911copyHolder;
            if (_local2 !== _arg1){
                this._1231798911copyHolder = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "copyHolder", _local2, _arg1));
            };
        }
        private function get value_icon():String{
            return (this._2042995335value_icon);
        }
        public function get copyHolder():VBox{
            return (this._1231798911copyHolder);
        }
        public function get copyContainer():Canvas{
            return (this._1939189620copyContainer);
        }
        private function set value_icon(_arg1:String):void{
            var _local2:Object = this._2042995335value_icon;
            if (_local2 !== _arg1){
                this._2042995335value_icon = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "value_icon", _local2, _arg1));
            };
        }
        private function iniGraphics():void{
            var _local3:Option;
            var _local5:Number;
            var _local8:BalanceTextArea;
            if (content.Conditions == null){
                return;
            };
            var _local1:Condition = content.Conditions.Conditions[0];
            var _local2:Question = BalanceModelLocator.getInstance().getQuestionById(_local1.QuestionId);
            var _local4:Number = 0;
            while (_local4 < _local2.Options.length) {
                if (_local2.Options[_local4].Id == _local1.OptionId){
                    _local3 = _local2.Options[_local4];
                    break;
                };
                _local4++;
            };
            value_icon = _local3.Icon;
            value_label = _local3.Label;
            value_description = _local3.Description;
            _local5 = 0;
            while (_local5 < stuffToRemove.length) {
                copyHolder.removeChild(stuffToRemove[_local5]);
                _local5++;
            };
            stuffToRemove = [];
            var _local6:TextArea = new TextArea();
            _local6.percentWidth = 100;
            _local6.text = content.Title;
            _local6.styleName = "subheaderblue";
            copyHolder.addChild(_local6);
            stuffToRemove.push(_local6);
            var _local7:Array = content.getLayout();
            _local5 = 0;
            while (_local5 < _local7.length) {
                if (typeof(_local7[_local5]) == "string"){
                    _local8 = new BalanceTextArea();
                    _local8.htmlText = _local7[_local5];
                    _local8.styleSheet = BalanceModelLocator.getInstance().balanceStyleSheet;
                    _local8.percentWidth = 100;
                    copyHolder.addChild(_local8);
                    stuffToRemove.push(_local8);
                };
                _local5++;
            };
        }
        override public function initialize():void{
            var target:* = null;
            var watcherSetupUtilClass:* = null;
            mx_internal::setDocumentDescriptor(_documentDescriptor_);
            var bindings:* = _BalanceValueReflectionCanvas_bindingsSetup();
            var watchers:* = [];
            target = this;
            if (_watcherSetupUtil == null){
                watcherSetupUtilClass = getDefinitionByName("_com_redbox_changetech_view_components_BalanceValueReflectionCanvasWatcherSetupUtil");
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
        private function init(_arg1:FlexEvent):void{
            borderCol = roomVO.boxColour2;
            changeWatcher = BindingUtils.bindSetter(changeRoom, BalanceModelLocator.getInstance(), "room");
            timer = new Timer(1000, 1);
            timer.addEventListener(TimerEvent.TIMER_COMPLETE, removeBinding);
            if (isToDisableBinding){
                timer.start();
            };
        }
        public function ___BalanceValueReflectionCanvas_Canvas1_creationComplete(_arg1:FlexEvent):void{
            init(_arg1);
        }
        public function set copyContainer(_arg1:Canvas):void{
            var _local2:Object = this._1939189620copyContainer;
            if (_local2 !== _arg1){
                this._1939189620copyContainer = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "copyContainer", _local2, _arg1));
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
        private function get borderCol():Number{
            return (this._197399988borderCol);
        }
        private function get value_label():String{
            return (this._1088955738value_label);
        }
        private function get value_description():String{
            return (this._1465045550value_description);
        }
        private function _BalanceValueReflectionCanvas_bindingsSetup():Array{
            var binding:* = null;
            var result:* = [];
            binding = new Binding(this, function ():Number{
                return (copyHolder.height);
            }, function (_arg1:Number):void{
                copyContainer.height = _arg1;
            }, "copyContainer.height");
            result[0] = binding;
            binding = new Binding(this, function ():Object{
                return (value_icon);
            }, function (_arg1:Object):void{
                _BalanceValueReflectionCanvas_Image1.source = _arg1;
            }, "_BalanceValueReflectionCanvas_Image1.source");
            result[1] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = value_label;
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                _BalanceValueReflectionCanvas_TextArea1.text = _arg1;
            }, "_BalanceValueReflectionCanvas_TextArea1.text");
            result[2] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = value_description;
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                _BalanceValueReflectionCanvas_TextArea2.text = _arg1;
            }, "_BalanceValueReflectionCanvas_TextArea2.text");
            result[3] = binding;
            binding = new Binding(this, function ():UIComponent{
                return (copyContainer);
            }, function (_arg1:UIComponent):void{
                _BalanceValueReflectionCanvas_Reflector1.target = _arg1;
            }, "_BalanceValueReflectionCanvas_Reflector1.target");
            result[4] = binding;
            binding = new Binding(this, function ():Number{
                return ((copyContainer.height + 0.1));
            }, function (_arg1:Number):void{
                _BalanceValueReflectionCanvas_Reflector1.y = _arg1;
            }, "_BalanceValueReflectionCanvas_Reflector1.y");
            result[5] = binding;
            return (result);
        }
        private function set value_label(_arg1:String):void{
            var _local2:Object = this._1088955738value_label;
            if (_local2 !== _arg1){
                this._1088955738value_label = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "value_label", _local2, _arg1));
            };
        }
        private function get roomVO():RoomVO{
            return (this._925318956roomVO);
        }
        private function removeBinding(_arg1:TimerEvent):void{
            changeWatcher.unwatch();
        }
        private function set value_description(_arg1:String):void{
            var _local2:Object = this._1465045550value_description;
            if (_local2 !== _arg1){
                this._1465045550value_description = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "value_description", _local2, _arg1));
            };
        }
        private function _BalanceValueReflectionCanvas_bindingExprs():void{
            var _local1:*;
            _local1 = copyHolder.height;
            _local1 = value_icon;
            _local1 = value_label;
            _local1 = value_description;
            _local1 = copyContainer;
            _local1 = (copyContainer.height + 0.1);
        }
        private function set borderCol(_arg1:Number):void{
            var _local2:Object = this._197399988borderCol;
            if (_local2 !== _arg1){
                this._197399988borderCol = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "borderCol", _local2, _arg1));
            };
        }
        public function set content(_arg1:Content):void{
            _content = _arg1;
            iniGraphics();
        }
        public function get content():Content{
            return (_content);
        }

        public static function set watcherSetupUtil(_arg1:IWatcherSetupUtil):void{
            BalanceValueReflectionCanvas._watcherSetupUtil = _arg1;
        }

    }
}//package com.redbox.changetech.view.components 
