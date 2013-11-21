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

    public class BalanceCopyReflectionCanvas extends Canvas implements IBindingClient {

        private var _documentDescriptor_:UIComponentDescriptor
        private var _925318956roomVO:RoomVO
        mx_internal var _watchers:Array
        private var _197399988borderCol:Number
        public var _BalanceCopyReflectionCanvas_BalanceTextArea1:BalanceTextArea
        public var _BalanceCopyReflectionCanvas_BalanceTextArea2:BalanceTextArea
        public var _BalanceCopyReflectionCanvas_BalanceTextArea3:BalanceTextArea
        private var _1803336908textFieldContainer:VBox
        public var isToDisableBinding:Boolean = true
        private var timer:Timer
        private var _copy_str:String
        private var _title_str:String
        mx_internal var _bindingsByDestination:Object
        mx_internal var _bindingsBeginWithWord:Object
        public var _BalanceCopyReflectionCanvas_Reflector1:Reflector
        private var _intro_str:String
        mx_internal var _bindings:Array
        private var changeWatcher:ChangeWatcher
        private var _1939189620copyContainer:Canvas

        private static var _watcherSetupUtil:IWatcherSetupUtil;

        public function BalanceCopyReflectionCanvas(){
            _documentDescriptor_ = new UIComponentDescriptor({type:Canvas, propertiesFactory:function ():Object{
                return ({width:435, childDescriptors:[new UIComponentDescriptor({type:Canvas, id:"copyContainer", propertiesFactory:function ():Object{
                    return ({width:430, styleName:"roundedGradCanvas", childDescriptors:[new UIComponentDescriptor({type:VBox, id:"textFieldContainer", stylesFactory:function ():void{
                        this.paddingLeft = 20;
                        this.paddingRight = 20;
                        this.paddingTop = 20;
                        this.paddingBottom = 50;
                    }, propertiesFactory:function ():Object{
                        return ({percentWidth:100, childDescriptors:[new UIComponentDescriptor({type:BalanceTextArea, id:"_BalanceCopyReflectionCanvas_BalanceTextArea1", stylesFactory:function ():void{
                            this.fontFamily = "Helvetica Neue";
                            this.fontSize = 18;
                        }, propertiesFactory:function ():Object{
                            return ({percentWidth:100});
                        }}), new UIComponentDescriptor({type:BalanceTextArea, id:"_BalanceCopyReflectionCanvas_BalanceTextArea2", stylesFactory:function ():void{
                            this.fontFamily = "Helvetica Neue";
                            this.fontSize = 14;
                        }, propertiesFactory:function ():Object{
                            return ({percentWidth:100});
                        }}), new UIComponentDescriptor({type:BalanceTextArea, id:"_BalanceCopyReflectionCanvas_BalanceTextArea3", stylesFactory:function ():void{
                            this.fontFamily = "Helvetica Neue";
                        }, propertiesFactory:function ():Object{
                            return ({percentWidth:100});
                        }})]});
                    }})]});
                }}), new UIComponentDescriptor({type:Reflector, id:"_BalanceCopyReflectionCanvas_Reflector1", propertiesFactory:function ():Object{
                    return ({falloff:0.5, alpha:0.5});
                }})]});
            }});
            _925318956roomVO = RoomVO(Config.ROOM_CONFIGS.getItemAt(BalanceModelLocator.getInstance().room));
            _bindings = [];
            _watchers = [];
            _bindingsByDestination = {};
            _bindingsBeginWithWord = {};
            super();
            mx_internal::_document = this;
            this.width = 435;
            this.addEventListener("creationComplete", ___BalanceCopyReflectionCanvas_Canvas1_creationComplete);
        }
        private function _BalanceCopyReflectionCanvas_bindingExprs():void{
            var _local1:*;
            _local1 = textFieldContainer.height;
            _local1 = title_str;
            _local1 = roomVO.textColour1;
            _local1 = !((title_str == null));
            _local1 = !((title_str == null));
            _local1 = intro_str;
            _local1 = roomVO.textColour1;
            _local1 = !((intro_str == null));
            _local1 = !((intro_str == null));
            _local1 = copy_str;
            _local1 = BalanceModelLocator.getInstance().balanceStyleSheet;
            _local1 = !((copy_str == null));
            _local1 = !((copy_str == null));
            _local1 = copyContainer;
            _local1 = (copyContainer.height + 0.1);
        }
        public function ___BalanceCopyReflectionCanvas_Canvas1_creationComplete(_arg1:FlexEvent):void{
            init(_arg1);
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
        public function get copyContainer():Canvas{
            return (this._1939189620copyContainer);
        }
        override public function initialize():void{
            var target:* = null;
            var watcherSetupUtilClass:* = null;
            mx_internal::setDocumentDescriptor(_documentDescriptor_);
            var bindings:* = _BalanceCopyReflectionCanvas_bindingsSetup();
            var watchers:* = [];
            target = this;
            if (_watcherSetupUtil == null){
                watcherSetupUtilClass = getDefinitionByName("_com_redbox_changetech_view_components_BalanceCopyReflectionCanvasWatcherSetupUtil");
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
        public function set title_str(_arg1:String):void{
            var _local2:Object = this.title_str;
            if (_local2 !== _arg1){
                this._2135415862title_str = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "title_str", _local2, _arg1));
            };
        }
        private function changeRoom(_arg1:int):void{
            roomVO = RoomVO(Config.ROOM_CONFIGS.getItemAt(_arg1));
        }
        private function set roomVO(_arg1:RoomVO):void{
            var _local2:Object = this._925318956roomVO;
            if (_local2 !== _arg1){
                this._925318956roomVO = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "roomVO", _local2, _arg1));
            };
        }
        public function set copyContainer(_arg1:Canvas):void{
            var _local2:Object = this._1939189620copyContainer;
            if (_local2 !== _arg1){
                this._1939189620copyContainer = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "copyContainer", _local2, _arg1));
            };
        }
        public function get textFieldContainer():VBox{
            return (this._1803336908textFieldContainer);
        }
        private function get borderCol():Number{
            return (this._197399988borderCol);
        }
        public function set intro_str(_arg1:String):void{
            var _local2:Object = this.intro_str;
            if (_local2 !== _arg1){
                this._871841246intro_str = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "intro_str", _local2, _arg1));
            };
        }
        private function get roomVO():RoomVO{
            return (this._925318956roomVO);
        }
        public function set copy_str(_arg1:String):void{
            var _local2:Object = this.copy_str;
            if (_local2 !== _arg1){
                this._505619865copy_str = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "copy_str", _local2, _arg1));
            };
        }
        public function get title_str():String{
            return (_title_str);
        }
        private function set _2135415862title_str(_arg1:String):void{
            _title_str = _arg1;
        }
        public function get intro_str():String{
            return (_intro_str);
        }
        private function removeBinding(_arg1:TimerEvent):void{
            changeWatcher.unwatch();
        }
        private function _BalanceCopyReflectionCanvas_bindingsSetup():Array{
            var binding:* = null;
            var result:* = [];
            binding = new Binding(this, function ():Number{
                return (textFieldContainer.height);
            }, function (_arg1:Number):void{
                copyContainer.height = _arg1;
            }, "copyContainer.height");
            result[0] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = title_str;
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                _BalanceCopyReflectionCanvas_BalanceTextArea1.htmlText = _arg1;
            }, "_BalanceCopyReflectionCanvas_BalanceTextArea1.htmlText");
            result[1] = binding;
            binding = new Binding(this, function ():uint{
                return (roomVO.textColour1);
            }, function (_arg1:uint):void{
                _BalanceCopyReflectionCanvas_BalanceTextArea1.setStyle("color", _arg1);
            }, "_BalanceCopyReflectionCanvas_BalanceTextArea1.color");
            result[2] = binding;
            binding = new Binding(this, function ():Boolean{
                return (!((title_str == null)));
            }, function (_arg1:Boolean):void{
                _BalanceCopyReflectionCanvas_BalanceTextArea1.includeInLayout = _arg1;
            }, "_BalanceCopyReflectionCanvas_BalanceTextArea1.includeInLayout");
            result[3] = binding;
            binding = new Binding(this, function ():Boolean{
                return (!((title_str == null)));
            }, function (_arg1:Boolean):void{
                _BalanceCopyReflectionCanvas_BalanceTextArea1.visible = _arg1;
            }, "_BalanceCopyReflectionCanvas_BalanceTextArea1.visible");
            result[4] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = intro_str;
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                _BalanceCopyReflectionCanvas_BalanceTextArea2.htmlText = _arg1;
            }, "_BalanceCopyReflectionCanvas_BalanceTextArea2.htmlText");
            result[5] = binding;
            binding = new Binding(this, function ():uint{
                return (roomVO.textColour1);
            }, function (_arg1:uint):void{
                _BalanceCopyReflectionCanvas_BalanceTextArea2.setStyle("color", _arg1);
            }, "_BalanceCopyReflectionCanvas_BalanceTextArea2.color");
            result[6] = binding;
            binding = new Binding(this, function ():Boolean{
                return (!((intro_str == null)));
            }, function (_arg1:Boolean):void{
                _BalanceCopyReflectionCanvas_BalanceTextArea2.includeInLayout = _arg1;
            }, "_BalanceCopyReflectionCanvas_BalanceTextArea2.includeInLayout");
            result[7] = binding;
            binding = new Binding(this, function ():Boolean{
                return (!((intro_str == null)));
            }, function (_arg1:Boolean):void{
                _BalanceCopyReflectionCanvas_BalanceTextArea2.visible = _arg1;
            }, "_BalanceCopyReflectionCanvas_BalanceTextArea2.visible");
            result[8] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = copy_str;
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                _BalanceCopyReflectionCanvas_BalanceTextArea3.htmlText = _arg1;
            }, "_BalanceCopyReflectionCanvas_BalanceTextArea3.htmlText");
            result[9] = binding;
            binding = new Binding(this, function ():StyleSheet{
                return (BalanceModelLocator.getInstance().balanceStyleSheet);
            }, function (_arg1:StyleSheet):void{
                _BalanceCopyReflectionCanvas_BalanceTextArea3.styleSheet = _arg1;
            }, "_BalanceCopyReflectionCanvas_BalanceTextArea3.styleSheet");
            result[10] = binding;
            binding = new Binding(this, function ():Boolean{
                return (!((copy_str == null)));
            }, function (_arg1:Boolean):void{
                _BalanceCopyReflectionCanvas_BalanceTextArea3.includeInLayout = _arg1;
            }, "_BalanceCopyReflectionCanvas_BalanceTextArea3.includeInLayout");
            result[11] = binding;
            binding = new Binding(this, function ():Boolean{
                return (!((copy_str == null)));
            }, function (_arg1:Boolean):void{
                _BalanceCopyReflectionCanvas_BalanceTextArea3.visible = _arg1;
            }, "_BalanceCopyReflectionCanvas_BalanceTextArea3.visible");
            result[12] = binding;
            binding = new Binding(this, function ():UIComponent{
                return (copyContainer);
            }, function (_arg1:UIComponent):void{
                _BalanceCopyReflectionCanvas_Reflector1.target = _arg1;
            }, "_BalanceCopyReflectionCanvas_Reflector1.target");
            result[13] = binding;
            binding = new Binding(this, function ():Number{
                return ((copyContainer.height + 0.1));
            }, function (_arg1:Number):void{
                _BalanceCopyReflectionCanvas_Reflector1.y = _arg1;
            }, "_BalanceCopyReflectionCanvas_Reflector1.y");
            result[14] = binding;
            return (result);
        }
        public function get copy_str():String{
            return (_copy_str);
        }
        public function set textFieldContainer(_arg1:VBox):void{
            var _local2:Object = this._1803336908textFieldContainer;
            if (_local2 !== _arg1){
                this._1803336908textFieldContainer = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "textFieldContainer", _local2, _arg1));
            };
        }
        private function set borderCol(_arg1:Number):void{
            var _local2:Object = this._197399988borderCol;
            if (_local2 !== _arg1){
                this._197399988borderCol = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "borderCol", _local2, _arg1));
            };
        }
        private function set _871841246intro_str(_arg1:String):void{
            _intro_str = _arg1;
        }
        private function set _505619865copy_str(_arg1:String):void{
            _copy_str = _arg1;
        }

        public static function set watcherSetupUtil(_arg1:IWatcherSetupUtil):void{
            BalanceCopyReflectionCanvas._watcherSetupUtil = _arg1;
        }

    }
}//package com.redbox.changetech.view.components 
