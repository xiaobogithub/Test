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
    import com.rictus.reflector.*;
    import flash.filters.*;
    import flash.ui.*;
    import com.redbox.changetech.util.*;
    import flash.external.*;
    import flash.debugger.*;
    import flash.errors.*;
    import flash.printing.*;
    import flash.profiler.*;

    public class BalanceGlassReflectionCanvas extends Canvas implements IBindingClient {

        private var _1939189620copyContainer:Canvas
        private var _925318956roomVO:RoomVO
        mx_internal var _watchers:Array
        private var _197399988borderCol:Number
        private var _1803336908textFieldContainer:VBox
        public var isToDisableBinding:Boolean = true
        private var model:BalanceModelLocator
        public var _BalanceGlassReflectionCanvas_Label1:Label
        public var _BalanceGlassReflectionCanvas_Label2:Label
        public var _BalanceGlassReflectionCanvas_Label3:Label
        public var _BalanceGlassReflectionCanvas_Label4:Label
        public var _BalanceGlassReflectionCanvas_Label5:Label
        public var _BalanceGlassReflectionCanvas_Label6:Label
        public var _BalanceGlassReflectionCanvas_Image1:Image
        private var _copy_str:String
        private var _title_str:String
        private var timer:Timer
        public var _BalanceGlassReflectionCanvas_Reflector1:Reflector
        mx_internal var _bindingsByDestination:Object
        mx_internal var _bindingsBeginWithWord:Object
        public var _BalanceGlassReflectionCanvas_BalanceTextArea1:BalanceTextArea
        public var _BalanceGlassReflectionCanvas_BalanceTextArea2:BalanceTextArea
        public var _BalanceGlassReflectionCanvas_BalanceTextArea3:BalanceTextArea
        private var changeWatcher:ChangeWatcher
        mx_internal var _bindings:Array
        private var _intro_str:String
        private var _documentDescriptor_:UIComponentDescriptor

        private static var _watcherSetupUtil:IWatcherSetupUtil;

        public function BalanceGlassReflectionCanvas(){
            _documentDescriptor_ = new UIComponentDescriptor({type:Canvas, propertiesFactory:function ():Object{
                return ({width:435, childDescriptors:[new UIComponentDescriptor({type:Canvas, id:"copyContainer", propertiesFactory:function ():Object{
                    return ({width:430, styleName:"roundedGradCanvas", childDescriptors:[new UIComponentDescriptor({type:VBox, id:"textFieldContainer", stylesFactory:function ():void{
                        this.paddingLeft = 20;
                        this.paddingRight = 20;
                        this.paddingTop = 20;
                        this.paddingBottom = 50;
                    }, propertiesFactory:function ():Object{
                        return ({percentWidth:100, childDescriptors:[new UIComponentDescriptor({type:BalanceTextArea, id:"_BalanceGlassReflectionCanvas_BalanceTextArea1", stylesFactory:function ():void{
                            this.fontFamily = "Helvetica Neue";
                            this.fontSize = 18;
                        }, propertiesFactory:function ():Object{
                            return ({percentWidth:100});
                        }}), new UIComponentDescriptor({type:BalanceTextArea, id:"_BalanceGlassReflectionCanvas_BalanceTextArea2", stylesFactory:function ():void{
                            this.fontFamily = "Helvetica Neue";
                            this.fontSize = 14;
                        }, propertiesFactory:function ():Object{
                            return ({percentWidth:100});
                        }}), new UIComponentDescriptor({type:BalanceTextArea, id:"_BalanceGlassReflectionCanvas_BalanceTextArea3", stylesFactory:function ():void{
                            this.fontFamily = "Helvetica Neue";
                        }, propertiesFactory:function ():Object{
                            return ({percentWidth:100});
                        }}), new UIComponentDescriptor({type:Image, id:"_BalanceGlassReflectionCanvas_Image1"}), new UIComponentDescriptor({type:HBox, propertiesFactory:function ():Object{
                            return ({percentWidth:100, childDescriptors:[new UIComponentDescriptor({type:VBox, stylesFactory:function ():void{
                                this.horizontalAlign = "center";
                            }, propertiesFactory:function ():Object{
                                return ({percentWidth:33, childDescriptors:[new UIComponentDescriptor({type:Label, id:"_BalanceGlassReflectionCanvas_Label1", stylesFactory:function ():void{
                                    this.fontFamily = "Helvetica Neue";
                                    this.textAlign = "center";
                                    this.fontSize = 18;
                                }}), new UIComponentDescriptor({type:Label, id:"_BalanceGlassReflectionCanvas_Label2", stylesFactory:function ():void{
                                    this.fontWeight = "bold";
                                    this.fontFamily = "Helvetica Neue";
                                    this.textAlign = "center";
                                    this.fontSize = 18;
                                }})]});
                            }}), new UIComponentDescriptor({type:VBox, stylesFactory:function ():void{
                                this.horizontalAlign = "center";
                            }, propertiesFactory:function ():Object{
                                return ({percentWidth:33, childDescriptors:[new UIComponentDescriptor({type:Label, id:"_BalanceGlassReflectionCanvas_Label3", stylesFactory:function ():void{
                                    this.fontFamily = "Helvetica Neue";
                                    this.textAlign = "center";
                                    this.fontSize = 18;
                                }}), new UIComponentDescriptor({type:Label, id:"_BalanceGlassReflectionCanvas_Label4", stylesFactory:function ():void{
                                    this.fontWeight = "bold";
                                    this.fontFamily = "Helvetica Neue";
                                    this.textAlign = "center";
                                    this.fontSize = 18;
                                }})]});
                            }}), new UIComponentDescriptor({type:VBox, stylesFactory:function ():void{
                                this.horizontalAlign = "center";
                            }, propertiesFactory:function ():Object{
                                return ({percentWidth:33, childDescriptors:[new UIComponentDescriptor({type:Label, id:"_BalanceGlassReflectionCanvas_Label5", stylesFactory:function ():void{
                                    this.fontFamily = "Helvetica Neue";
                                    this.textAlign = "center";
                                    this.fontSize = 18;
                                }}), new UIComponentDescriptor({type:Label, id:"_BalanceGlassReflectionCanvas_Label6", stylesFactory:function ():void{
                                    this.fontWeight = "bold";
                                    this.fontFamily = "Helvetica Neue";
                                    this.textAlign = "center";
                                    this.fontSize = 18;
                                }})]});
                            }})]});
                        }})]});
                    }})]});
                }}), new UIComponentDescriptor({type:Reflector, id:"_BalanceGlassReflectionCanvas_Reflector1", propertiesFactory:function ():Object{
                    return ({falloff:0.1, alpha:0.5});
                }})]});
            }});
            _925318956roomVO = RoomVO(Config.ROOM_CONFIGS.getItemAt(BalanceModelLocator.getInstance().room));
            model = BalanceModelLocator.getInstance();
            _bindings = [];
            _watchers = [];
            _bindingsByDestination = {};
            _bindingsBeginWithWord = {};
            super();
            mx_internal::_document = this;
            this.width = 435;
            this.addEventListener("creationComplete", ___BalanceGlassReflectionCanvas_Canvas1_creationComplete);
        }
        private function set _505619865copy_str(_arg1:String):void{
            _copy_str = _arg1;
        }
        private function get borderCol():Number{
            return (this._197399988borderCol);
        }
        override public function initialize():void{
            var target:* = null;
            var watcherSetupUtilClass:* = null;
            mx_internal::setDocumentDescriptor(_documentDescriptor_);
            var bindings:* = _BalanceGlassReflectionCanvas_bindingsSetup();
            var watchers:* = [];
            target = this;
            if (_watcherSetupUtil == null){
                watcherSetupUtilClass = getDefinitionByName("_com_redbox_changetech_view_components_BalanceGlassReflectionCanvasWatcherSetupUtil");
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
        private function _BalanceGlassReflectionCanvas_bindingsSetup():Array{
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
                _BalanceGlassReflectionCanvas_BalanceTextArea1.htmlText = _arg1;
            }, "_BalanceGlassReflectionCanvas_BalanceTextArea1.htmlText");
            result[1] = binding;
            binding = new Binding(this, function ():uint{
                return (roomVO.textColour1);
            }, function (_arg1:uint):void{
                _BalanceGlassReflectionCanvas_BalanceTextArea1.setStyle("color", _arg1);
            }, "_BalanceGlassReflectionCanvas_BalanceTextArea1.color");
            result[2] = binding;
            binding = new Binding(this, function ():Boolean{
                return (!((title_str == null)));
            }, function (_arg1:Boolean):void{
                _BalanceGlassReflectionCanvas_BalanceTextArea1.includeInLayout = _arg1;
            }, "_BalanceGlassReflectionCanvas_BalanceTextArea1.includeInLayout");
            result[3] = binding;
            binding = new Binding(this, function ():Boolean{
                return (!((title_str == null)));
            }, function (_arg1:Boolean):void{
                _BalanceGlassReflectionCanvas_BalanceTextArea1.visible = _arg1;
            }, "_BalanceGlassReflectionCanvas_BalanceTextArea1.visible");
            result[4] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = intro_str;
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                _BalanceGlassReflectionCanvas_BalanceTextArea2.htmlText = _arg1;
            }, "_BalanceGlassReflectionCanvas_BalanceTextArea2.htmlText");
            result[5] = binding;
            binding = new Binding(this, function ():uint{
                return (roomVO.textColour1);
            }, function (_arg1:uint):void{
                _BalanceGlassReflectionCanvas_BalanceTextArea2.setStyle("color", _arg1);
            }, "_BalanceGlassReflectionCanvas_BalanceTextArea2.color");
            result[6] = binding;
            binding = new Binding(this, function ():Boolean{
                return (!((intro_str == null)));
            }, function (_arg1:Boolean):void{
                _BalanceGlassReflectionCanvas_BalanceTextArea2.includeInLayout = _arg1;
            }, "_BalanceGlassReflectionCanvas_BalanceTextArea2.includeInLayout");
            result[7] = binding;
            binding = new Binding(this, function ():Boolean{
                return (!((intro_str == null)));
            }, function (_arg1:Boolean):void{
                _BalanceGlassReflectionCanvas_BalanceTextArea2.visible = _arg1;
            }, "_BalanceGlassReflectionCanvas_BalanceTextArea2.visible");
            result[8] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = copy_str;
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                _BalanceGlassReflectionCanvas_BalanceTextArea3.htmlText = _arg1;
            }, "_BalanceGlassReflectionCanvas_BalanceTextArea3.htmlText");
            result[9] = binding;
            binding = new Binding(this, function ():StyleSheet{
                return (BalanceModelLocator.getInstance().balanceStyleSheet);
            }, function (_arg1:StyleSheet):void{
                _BalanceGlassReflectionCanvas_BalanceTextArea3.styleSheet = _arg1;
            }, "_BalanceGlassReflectionCanvas_BalanceTextArea3.styleSheet");
            result[10] = binding;
            binding = new Binding(this, function ():Boolean{
                return (!((copy_str == null)));
            }, function (_arg1:Boolean):void{
                _BalanceGlassReflectionCanvas_BalanceTextArea3.includeInLayout = _arg1;
            }, "_BalanceGlassReflectionCanvas_BalanceTextArea3.includeInLayout");
            result[11] = binding;
            binding = new Binding(this, function ():Boolean{
                return (!((copy_str == null)));
            }, function (_arg1:Boolean):void{
                _BalanceGlassReflectionCanvas_BalanceTextArea3.visible = _arg1;
            }, "_BalanceGlassReflectionCanvas_BalanceTextArea3.visible");
            result[12] = binding;
            binding = new Binding(this, function ():Object{
                return (Assets.getInstance().glasses_fg);
            }, function (_arg1:Object):void{
                _BalanceGlassReflectionCanvas_Image1.source = _arg1;
            }, "_BalanceGlassReflectionCanvas_Image1.source");
            result[13] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = ("12 cl " + model.languageVO.getLang("Wine"));
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                _BalanceGlassReflectionCanvas_Label1.text = _arg1;
            }, "_BalanceGlassReflectionCanvas_Label1.text");
            result[14] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = ("1 " + model.languageVO.getLang("unit"));
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                _BalanceGlassReflectionCanvas_Label2.text = _arg1;
            }, "_BalanceGlassReflectionCanvas_Label2.text");
            result[15] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = ("4 cl " + model.languageVO.getLang("Spirits"));
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                _BalanceGlassReflectionCanvas_Label3.text = _arg1;
            }, "_BalanceGlassReflectionCanvas_Label3.text");
            result[16] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = ("1 " + model.languageVO.getLang("unit"));
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                _BalanceGlassReflectionCanvas_Label4.text = _arg1;
            }, "_BalanceGlassReflectionCanvas_Label4.text");
            result[17] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = model.languageVO.getLang("a_bottle_of_beer");
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                _BalanceGlassReflectionCanvas_Label5.text = _arg1;
            }, "_BalanceGlassReflectionCanvas_Label5.text");
            result[18] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = ("1 " + model.languageVO.getLang("unit"));
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                _BalanceGlassReflectionCanvas_Label6.text = _arg1;
            }, "_BalanceGlassReflectionCanvas_Label6.text");
            result[19] = binding;
            binding = new Binding(this, function ():UIComponent{
                return (copyContainer);
            }, function (_arg1:UIComponent):void{
                _BalanceGlassReflectionCanvas_Reflector1.target = _arg1;
            }, "_BalanceGlassReflectionCanvas_Reflector1.target");
            result[20] = binding;
            binding = new Binding(this, function ():Number{
                return ((copyContainer.height + 0.1));
            }, function (_arg1:Number):void{
                _BalanceGlassReflectionCanvas_Reflector1.y = _arg1;
            }, "_BalanceGlassReflectionCanvas_Reflector1.y");
            result[21] = binding;
            return (result);
        }
        public function get copyContainer():Canvas{
            return (this._1939189620copyContainer);
        }
        public function set title_str(_arg1:String):void{
            var _local2:Object = this.title_str;
            if (_local2 !== _arg1){
                this._2135415862title_str = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "title_str", _local2, _arg1));
            };
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
        public function set copyContainer(_arg1:Canvas):void{
            var _local2:Object = this._1939189620copyContainer;
            if (_local2 !== _arg1){
                this._1939189620copyContainer = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "copyContainer", _local2, _arg1));
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
        private function _BalanceGlassReflectionCanvas_bindingExprs():void{
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
            _local1 = Assets.getInstance().glasses_fg;
            _local1 = ("12 cl " + model.languageVO.getLang("Wine"));
            _local1 = ("1 " + model.languageVO.getLang("unit"));
            _local1 = ("4 cl " + model.languageVO.getLang("Spirits"));
            _local1 = ("1 " + model.languageVO.getLang("unit"));
            _local1 = model.languageVO.getLang("a_bottle_of_beer");
            _local1 = ("1 " + model.languageVO.getLang("unit"));
            _local1 = copyContainer;
            _local1 = (copyContainer.height + 0.1);
        }
        public function get title_str():String{
            return (_title_str);
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
        public function get textFieldContainer():VBox{
            return (this._1803336908textFieldContainer);
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
        private function set _871841246intro_str(_arg1:String):void{
            _intro_str = _arg1;
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
        public function set intro_str(_arg1:String):void{
            var _local2:Object = this.intro_str;
            if (_local2 !== _arg1){
                this._871841246intro_str = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "intro_str", _local2, _arg1));
            };
        }
        public function ___BalanceGlassReflectionCanvas_Canvas1_creationComplete(_arg1:FlexEvent):void{
            init(_arg1);
        }
        public function get copy_str():String{
            return (_copy_str);
        }

        public static function set watcherSetupUtil(_arg1:IWatcherSetupUtil):void{
            BalanceGlassReflectionCanvas._watcherSetupUtil = _arg1;
        }

    }
}//package com.redbox.changetech.view.components 
