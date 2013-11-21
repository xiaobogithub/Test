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

    public class BalanceGraphReflectionCanvas extends Canvas implements IBindingClient {

        private var _925318956roomVO:RoomVO
        private var _415102010_copy_str:String
        private var _71054889_completionScore:Number
        private var _197399988borderCol:Number
        private var _1480414811_screeningScore:Number
        mx_internal var _watchers:Array
        private var _1803336908textFieldContainer:VBox
        private var model:BalanceModelLocator
        private var picLoaded:Boolean = false
        private var _670637643_title_str:String
        public var isToDisableBinding:Boolean = true
        private var T1x:Number = 80
        private var _1454123777graphLoader:Image
        private var timer:Timer
        public var _BalanceGraphReflectionCanvas_BalanceTextArea1:BalanceTextArea
        public var _BalanceGraphReflectionCanvas_BalanceTextArea2:BalanceTextArea
        public var _BalanceGraphReflectionCanvas_BalanceTextArea3:BalanceTextArea
        public var _BalanceGraphReflectionCanvas_BalanceTextArea4:BalanceTextArea
        public var _BalanceGraphReflectionCanvas_BalanceTextArea5:BalanceTextArea
        public var _BalanceGraphReflectionCanvas_BalanceTextArea6:BalanceTextArea
        private var _617072545_intro_str:String
        mx_internal var _bindingsBeginWithWord:Object
        public var _BalanceGraphReflectionCanvas_Text1:Text
        private var T2x:Number = 250
        mx_internal var _bindingsByDestination:Object
        private var changeWatcher:ChangeWatcher
        mx_internal var _bindings:Array
        private var graphDrawn:Boolean = false
        private var _documentDescriptor_:UIComponentDescriptor

        private static var _watcherSetupUtil:IWatcherSetupUtil;

        public function BalanceGraphReflectionCanvas(){
            _documentDescriptor_ = new UIComponentDescriptor({type:Canvas, propertiesFactory:function ():Object{
                return ({width:480, childDescriptors:[new UIComponentDescriptor({type:VBox, id:"textFieldContainer", stylesFactory:function ():void{
                    this.paddingLeft = 20;
                    this.paddingRight = 20;
                    this.paddingTop = 20;
                    this.paddingBottom = 50;
                }, propertiesFactory:function ():Object{
                    return ({percentWidth:100, childDescriptors:[new UIComponentDescriptor({type:BalanceTextArea, id:"_BalanceGraphReflectionCanvas_BalanceTextArea1", stylesFactory:function ():void{
                        this.fontFamily = "Helvetica Neue";
                        this.fontSize = 18;
                    }, propertiesFactory:function ():Object{
                        return ({percentWidth:100});
                    }}), new UIComponentDescriptor({type:Canvas, propertiesFactory:function ():Object{
                        return ({percentWidth:100, childDescriptors:[new UIComponentDescriptor({type:Image, id:"graphLoader", events:{complete:"__graphLoader_complete"}, propertiesFactory:function ():Object{
                            return ({x:35, y:10, source:"assets/media/graph.swf"});
                        }}), new UIComponentDescriptor({type:Text, id:"_BalanceGraphReflectionCanvas_Text1", stylesFactory:function ():void{
                            this.fontStyle = "italic";
                            this.fontWeight = "bold";
                            this.color = 5223648;
                            this.fontFamily = "Helvetica Neue";
                        }, propertiesFactory:function ():Object{
                            return ({rotation:-90, y:150});
                        }}), new UIComponentDescriptor({type:Text, stylesFactory:function ():void{
                            this.fontStyle = "italic";
                            this.fontWeight = "bold";
                            this.color = 5223648;
                            this.fontFamily = "Helvetica Neue";
                        }, propertiesFactory:function ():Object{
                            return ({x:95, y:234, text:"T1"});
                        }}), new UIComponentDescriptor({type:Text, stylesFactory:function ():void{
                            this.fontStyle = "italic";
                            this.fontWeight = "bold";
                            this.color = 5223648;
                            this.fontFamily = "Helvetica Neue";
                        }, propertiesFactory:function ():Object{
                            return ({x:185, y:234, text:"Tid"});
                        }}), new UIComponentDescriptor({type:Text, stylesFactory:function ():void{
                            this.fontStyle = "italic";
                            this.fontWeight = "bold";
                            this.color = 5223648;
                            this.fontFamily = "Helvetica Neue";
                        }, propertiesFactory:function ():Object{
                            return ({x:275, y:234, text:"T2"});
                        }}), new UIComponentDescriptor({type:Text, stylesFactory:function ():void{
                            this.fontStyle = "italic";
                            this.fontWeight = "bold";
                            this.color = 5223648;
                            this.fontFamily = "Helvetica Neue";
                        }, propertiesFactory:function ():Object{
                            return ({x:20, y:0, text:"35"});
                        }}), new UIComponentDescriptor({type:Text, stylesFactory:function ():void{
                            this.fontStyle = "italic";
                            this.fontWeight = "bold";
                            this.color = 5223648;
                            this.fontFamily = "Helvetica Neue";
                        }, propertiesFactory:function ():Object{
                            return ({x:20, y:30, text:"30"});
                        }}), new UIComponentDescriptor({type:Text, stylesFactory:function ():void{
                            this.fontStyle = "italic";
                            this.fontWeight = "bold";
                            this.color = 5223648;
                            this.fontFamily = "Helvetica Neue";
                        }, propertiesFactory:function ():Object{
                            return ({x:20, y:60, text:"25"});
                        }}), new UIComponentDescriptor({type:Text, stylesFactory:function ():void{
                            this.fontStyle = "italic";
                            this.fontWeight = "bold";
                            this.color = 5223648;
                            this.fontFamily = "Helvetica Neue";
                        }, propertiesFactory:function ():Object{
                            return ({x:20, y:90, text:"20"});
                        }}), new UIComponentDescriptor({type:Text, stylesFactory:function ():void{
                            this.fontStyle = "italic";
                            this.fontWeight = "bold";
                            this.color = 5223648;
                            this.fontFamily = "Helvetica Neue";
                        }, propertiesFactory:function ():Object{
                            return ({x:20, y:120, text:"15"});
                        }}), new UIComponentDescriptor({type:Text, stylesFactory:function ():void{
                            this.fontStyle = "italic";
                            this.fontWeight = "bold";
                            this.color = 5223648;
                            this.fontFamily = "Helvetica Neue";
                        }, propertiesFactory:function ():Object{
                            return ({x:20, y:150, text:"10"});
                        }}), new UIComponentDescriptor({type:Text, stylesFactory:function ():void{
                            this.fontStyle = "italic";
                            this.fontWeight = "bold";
                            this.color = 5223648;
                            this.fontFamily = "Helvetica Neue";
                        }, propertiesFactory:function ():Object{
                            return ({x:20, y:180, text:"5"});
                        }}), new UIComponentDescriptor({type:Text, stylesFactory:function ():void{
                            this.fontStyle = "italic";
                            this.fontWeight = "bold";
                            this.color = 5223648;
                            this.fontFamily = "Helvetica Neue";
                        }, propertiesFactory:function ():Object{
                            return ({x:20, y:210, text:"0"});
                        }})]});
                    }}), new UIComponentDescriptor({type:BalanceTextArea, id:"_BalanceGraphReflectionCanvas_BalanceTextArea2", stylesFactory:function ():void{
                        this.fontFamily = "Helvetica Neue";
                    }, propertiesFactory:function ():Object{
                        return ({percentWidth:100});
                    }}), new UIComponentDescriptor({type:BalanceTextArea, id:"_BalanceGraphReflectionCanvas_BalanceTextArea3", stylesFactory:function ():void{
                        this.fontFamily = "Helvetica Neue";
                    }, propertiesFactory:function ():Object{
                        return ({percentWidth:100});
                    }}), new UIComponentDescriptor({type:BalanceTextArea, id:"_BalanceGraphReflectionCanvas_BalanceTextArea4", stylesFactory:function ():void{
                        this.fontFamily = "Helvetica Neue";
                        this.fontWeight = "bold";
                    }, propertiesFactory:function ():Object{
                        return ({percentWidth:100});
                    }}), new UIComponentDescriptor({type:BalanceTextArea, id:"_BalanceGraphReflectionCanvas_BalanceTextArea5", stylesFactory:function ():void{
                        this.fontFamily = "Helvetica Neue";
                        this.fontWeight = "bold";
                    }, propertiesFactory:function ():Object{
                        return ({percentWidth:100});
                    }}), new UIComponentDescriptor({type:BalanceTextArea, id:"_BalanceGraphReflectionCanvas_BalanceTextArea6", stylesFactory:function ():void{
                        this.fontFamily = "Helvetica Neue";
                    }, propertiesFactory:function ():Object{
                        return ({percentWidth:100});
                    }})]});
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
            this.styleName = "roundedGradCanvas";
            this.width = 480;
            this.addEventListener("creationComplete", ___BalanceGraphReflectionCanvas_Canvas1_creationComplete);
        }
        public function get graphLoader():Image{
            return (this._1454123777graphLoader);
        }
        public function set graphLoader(_arg1:Image):void{
            var _local2:Object = this._1454123777graphLoader;
            if (_local2 !== _arg1){
                this._1454123777graphLoader = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "graphLoader", _local2, _arg1));
            };
        }
        private function set _copy_str(_arg1:String):void{
            var _local2:Object = this._415102010_copy_str;
            if (_local2 !== _arg1){
                this._415102010_copy_str = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "_copy_str", _local2, _arg1));
            };
        }
        public function get completionScore():Number{
            return (_completionScore);
        }
        override public function initialize():void{
            var target:* = null;
            var watcherSetupUtilClass:* = null;
            mx_internal::setDocumentDescriptor(_documentDescriptor_);
            var bindings:* = _BalanceGraphReflectionCanvas_bindingsSetup();
            var watchers:* = [];
            target = this;
            if (_watcherSetupUtil == null){
                watcherSetupUtilClass = getDefinitionByName("_com_redbox_changetech_view_components_BalanceGraphReflectionCanvasWatcherSetupUtil");
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
        private function _BalanceGraphReflectionCanvas_bindingExprs():void{
            var _local1:*;
            _local1 = title_str;
            _local1 = roomVO.textColour1;
            _local1 = !((title_str == null));
            _local1 = !((title_str == null));
            _local1 = model.languageVO.getLang("graph_xaxis");
            _local1 = ((_screeningScore + " ") + model.languageVO.getLang("graph_screening_explanation"));
            _local1 = BalanceModelLocator.getInstance().balanceStyleSheet;
            _local1 = !((_copy_str == null));
            _local1 = !((_copy_str == null));
            _local1 = ((_completionScore + " ") + model.languageVO.getLang("graph_completion_explanation"));
            _local1 = BalanceModelLocator.getInstance().balanceStyleSheet;
            _local1 = !((_copy_str == null));
            _local1 = !((_copy_str == null));
            _local1 = model.languageVO.getLang("graph_T1");
            _local1 = BalanceModelLocator.getInstance().balanceStyleSheet;
            _local1 = !((_copy_str == null));
            _local1 = !((_copy_str == null));
            _local1 = model.languageVO.getLang("graph_T2");
            _local1 = BalanceModelLocator.getInstance().balanceStyleSheet;
            _local1 = !((_copy_str == null));
            _local1 = !((_copy_str == null));
            _local1 = _copy_str;
            _local1 = BalanceModelLocator.getInstance().balanceStyleSheet;
            _local1 = !((_copy_str == null));
            _local1 = !((_copy_str == null));
        }
        public function __graphLoader_complete(_arg1:Event):void{
            setPicLoaded();
        }
        private function changeRoom(_arg1:int):void{
            roomVO = RoomVO(Config.ROOM_CONFIGS.getItemAt(_arg1));
        }
        public function set completionScore(_arg1:Number):void{
            _completionScore = _arg1;
        }
        private function set _title_str(_arg1:String):void{
            var _local2:Object = this._670637643_title_str;
            if (_local2 !== _arg1){
                this._670637643_title_str = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "_title_str", _local2, _arg1));
            };
        }
        private function set _completionScore(_arg1:Number):void{
            var _local2:Object = this._71054889_completionScore;
            if (_local2 !== _arg1){
                this._71054889_completionScore = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "_completionScore", _local2, _arg1));
            };
        }
        private function set roomVO(_arg1:RoomVO):void{
            var _local2:Object = this._925318956roomVO;
            if (_local2 !== _arg1){
                this._925318956roomVO = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "roomVO", _local2, _arg1));
            };
        }
        public function checkInit():void{
            if (((((((picLoaded) && (!(graphDrawn)))) && (!(isNaN(screeningScore))))) && (!(isNaN(completionScore))))){
                trace("DRAW GRAPH");
                drawGraph();
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
        private function set _intro_str(_arg1:String):void{
            var _local2:Object = this._617072545_intro_str;
            if (_local2 !== _arg1){
                this._617072545_intro_str = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "_intro_str", _local2, _arg1));
            };
        }
        public function get screeningScore():Number{
            return (_screeningScore);
        }
        private function get _copy_str():String{
            return (this._415102010_copy_str);
        }
        public function get title_str():String{
            return (_title_str);
        }
        private function get roomVO():RoomVO{
            return (this._925318956roomVO);
        }
        public function set copy_str(_arg1:String):void{
            _copy_str = _arg1;
        }
        public function get textFieldContainer():VBox{
            return (this._1803336908textFieldContainer);
        }
        private function get _completionScore():Number{
            return (this._71054889_completionScore);
        }
        private function get _screeningScore():Number{
            return (this._1480414811_screeningScore);
        }
        private function set _2135415862title_str(_arg1:String):void{
            _title_str = _arg1;
        }
        private function set _871841246intro_str(_arg1:String):void{
            _intro_str = _arg1;
        }
        private function removeBinding(_arg1:TimerEvent):void{
            changeWatcher.unwatch();
        }
        private function get _title_str():String{
            return (this._670637643_title_str);
        }
        private function _BalanceGraphReflectionCanvas_bindingsSetup():Array{
            var binding:* = null;
            var result:* = [];
            binding = new Binding(this, function ():String{
                var _local1:* = title_str;
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                _BalanceGraphReflectionCanvas_BalanceTextArea1.htmlText = _arg1;
            }, "_BalanceGraphReflectionCanvas_BalanceTextArea1.htmlText");
            result[0] = binding;
            binding = new Binding(this, function ():uint{
                return (roomVO.textColour1);
            }, function (_arg1:uint):void{
                _BalanceGraphReflectionCanvas_BalanceTextArea1.setStyle("color", _arg1);
            }, "_BalanceGraphReflectionCanvas_BalanceTextArea1.color");
            result[1] = binding;
            binding = new Binding(this, function ():Boolean{
                return (!((title_str == null)));
            }, function (_arg1:Boolean):void{
                _BalanceGraphReflectionCanvas_BalanceTextArea1.includeInLayout = _arg1;
            }, "_BalanceGraphReflectionCanvas_BalanceTextArea1.includeInLayout");
            result[2] = binding;
            binding = new Binding(this, function ():Boolean{
                return (!((title_str == null)));
            }, function (_arg1:Boolean):void{
                _BalanceGraphReflectionCanvas_BalanceTextArea1.visible = _arg1;
            }, "_BalanceGraphReflectionCanvas_BalanceTextArea1.visible");
            result[3] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = model.languageVO.getLang("graph_xaxis");
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                _BalanceGraphReflectionCanvas_Text1.text = _arg1;
            }, "_BalanceGraphReflectionCanvas_Text1.text");
            result[4] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = ((_screeningScore + " ") + model.languageVO.getLang("graph_screening_explanation"));
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                _BalanceGraphReflectionCanvas_BalanceTextArea2.htmlText = _arg1;
            }, "_BalanceGraphReflectionCanvas_BalanceTextArea2.htmlText");
            result[5] = binding;
            binding = new Binding(this, function ():StyleSheet{
                return (BalanceModelLocator.getInstance().balanceStyleSheet);
            }, function (_arg1:StyleSheet):void{
                _BalanceGraphReflectionCanvas_BalanceTextArea2.styleSheet = _arg1;
            }, "_BalanceGraphReflectionCanvas_BalanceTextArea2.styleSheet");
            result[6] = binding;
            binding = new Binding(this, function ():Boolean{
                return (!((_copy_str == null)));
            }, function (_arg1:Boolean):void{
                _BalanceGraphReflectionCanvas_BalanceTextArea2.includeInLayout = _arg1;
            }, "_BalanceGraphReflectionCanvas_BalanceTextArea2.includeInLayout");
            result[7] = binding;
            binding = new Binding(this, function ():Boolean{
                return (!((_copy_str == null)));
            }, function (_arg1:Boolean):void{
                _BalanceGraphReflectionCanvas_BalanceTextArea2.visible = _arg1;
            }, "_BalanceGraphReflectionCanvas_BalanceTextArea2.visible");
            result[8] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = ((_completionScore + " ") + model.languageVO.getLang("graph_completion_explanation"));
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                _BalanceGraphReflectionCanvas_BalanceTextArea3.htmlText = _arg1;
            }, "_BalanceGraphReflectionCanvas_BalanceTextArea3.htmlText");
            result[9] = binding;
            binding = new Binding(this, function ():StyleSheet{
                return (BalanceModelLocator.getInstance().balanceStyleSheet);
            }, function (_arg1:StyleSheet):void{
                _BalanceGraphReflectionCanvas_BalanceTextArea3.styleSheet = _arg1;
            }, "_BalanceGraphReflectionCanvas_BalanceTextArea3.styleSheet");
            result[10] = binding;
            binding = new Binding(this, function ():Boolean{
                return (!((_copy_str == null)));
            }, function (_arg1:Boolean):void{
                _BalanceGraphReflectionCanvas_BalanceTextArea3.includeInLayout = _arg1;
            }, "_BalanceGraphReflectionCanvas_BalanceTextArea3.includeInLayout");
            result[11] = binding;
            binding = new Binding(this, function ():Boolean{
                return (!((_copy_str == null)));
            }, function (_arg1:Boolean):void{
                _BalanceGraphReflectionCanvas_BalanceTextArea3.visible = _arg1;
            }, "_BalanceGraphReflectionCanvas_BalanceTextArea3.visible");
            result[12] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = model.languageVO.getLang("graph_T1");
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                _BalanceGraphReflectionCanvas_BalanceTextArea4.htmlText = _arg1;
            }, "_BalanceGraphReflectionCanvas_BalanceTextArea4.htmlText");
            result[13] = binding;
            binding = new Binding(this, function ():StyleSheet{
                return (BalanceModelLocator.getInstance().balanceStyleSheet);
            }, function (_arg1:StyleSheet):void{
                _BalanceGraphReflectionCanvas_BalanceTextArea4.styleSheet = _arg1;
            }, "_BalanceGraphReflectionCanvas_BalanceTextArea4.styleSheet");
            result[14] = binding;
            binding = new Binding(this, function ():Boolean{
                return (!((_copy_str == null)));
            }, function (_arg1:Boolean):void{
                _BalanceGraphReflectionCanvas_BalanceTextArea4.includeInLayout = _arg1;
            }, "_BalanceGraphReflectionCanvas_BalanceTextArea4.includeInLayout");
            result[15] = binding;
            binding = new Binding(this, function ():Boolean{
                return (!((_copy_str == null)));
            }, function (_arg1:Boolean):void{
                _BalanceGraphReflectionCanvas_BalanceTextArea4.visible = _arg1;
            }, "_BalanceGraphReflectionCanvas_BalanceTextArea4.visible");
            result[16] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = model.languageVO.getLang("graph_T2");
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                _BalanceGraphReflectionCanvas_BalanceTextArea5.htmlText = _arg1;
            }, "_BalanceGraphReflectionCanvas_BalanceTextArea5.htmlText");
            result[17] = binding;
            binding = new Binding(this, function ():StyleSheet{
                return (BalanceModelLocator.getInstance().balanceStyleSheet);
            }, function (_arg1:StyleSheet):void{
                _BalanceGraphReflectionCanvas_BalanceTextArea5.styleSheet = _arg1;
            }, "_BalanceGraphReflectionCanvas_BalanceTextArea5.styleSheet");
            result[18] = binding;
            binding = new Binding(this, function ():Boolean{
                return (!((_copy_str == null)));
            }, function (_arg1:Boolean):void{
                _BalanceGraphReflectionCanvas_BalanceTextArea5.includeInLayout = _arg1;
            }, "_BalanceGraphReflectionCanvas_BalanceTextArea5.includeInLayout");
            result[19] = binding;
            binding = new Binding(this, function ():Boolean{
                return (!((_copy_str == null)));
            }, function (_arg1:Boolean):void{
                _BalanceGraphReflectionCanvas_BalanceTextArea5.visible = _arg1;
            }, "_BalanceGraphReflectionCanvas_BalanceTextArea5.visible");
            result[20] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = _copy_str;
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                _BalanceGraphReflectionCanvas_BalanceTextArea6.htmlText = _arg1;
            }, "_BalanceGraphReflectionCanvas_BalanceTextArea6.htmlText");
            result[21] = binding;
            binding = new Binding(this, function ():StyleSheet{
                return (BalanceModelLocator.getInstance().balanceStyleSheet);
            }, function (_arg1:StyleSheet):void{
                _BalanceGraphReflectionCanvas_BalanceTextArea6.styleSheet = _arg1;
            }, "_BalanceGraphReflectionCanvas_BalanceTextArea6.styleSheet");
            result[22] = binding;
            binding = new Binding(this, function ():Boolean{
                return (!((_copy_str == null)));
            }, function (_arg1:Boolean):void{
                _BalanceGraphReflectionCanvas_BalanceTextArea6.includeInLayout = _arg1;
            }, "_BalanceGraphReflectionCanvas_BalanceTextArea6.includeInLayout");
            result[23] = binding;
            binding = new Binding(this, function ():Boolean{
                return (!((_copy_str == null)));
            }, function (_arg1:Boolean):void{
                _BalanceGraphReflectionCanvas_BalanceTextArea6.visible = _arg1;
            }, "_BalanceGraphReflectionCanvas_BalanceTextArea6.visible");
            result[24] = binding;
            return (result);
        }
        public function get intro_str():String{
            return (_intro_str);
        }
        private function get _intro_str():String{
            return (this._617072545_intro_str);
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
        public function ___BalanceGraphReflectionCanvas_Canvas1_creationComplete(_arg1:FlexEvent):void{
            init(_arg1);
        }
        private function drawGraph():void{
            var _local1:Number = 2;
            var _local2:Number = 2;
            var _local3:Number = 6;
            graphLoader.graphics.lineStyle(1, 5223648);
            graphLoader.graphics.beginFill(5223648, 1);
            graphLoader.graphics.drawCircle((_local1 + T1x), (211 - (screeningScore * _local3)), 5);
            graphLoader.graphics.endFill();
            graphLoader.graphics.moveTo((_local1 + T1x), (211 - (screeningScore * _local3)));
            graphLoader.graphics.lineTo((_local1 + T2x), (211 - (completionScore * _local3)));
            graphLoader.graphics.beginFill(5223648, 1);
            graphLoader.graphics.drawCircle((_local1 + T2x), (211 - (completionScore * _local3)), 5);
            graphLoader.graphics.endFill();
            graphDrawn = true;
        }
        private function setPicLoaded():void{
            picLoaded = true;
            checkInit();
        }
        private function set _screeningScore(_arg1:Number):void{
            var _local2:Object = this._1480414811_screeningScore;
            if (_local2 !== _arg1){
                this._1480414811_screeningScore = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "_screeningScore", _local2, _arg1));
            };
        }
        public function set intro_str(_arg1:String):void{
            var _local2:Object = this.intro_str;
            if (_local2 !== _arg1){
                this._871841246intro_str = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "intro_str", _local2, _arg1));
            };
        }
        private function set borderCol(_arg1:Number):void{
            var _local2:Object = this._197399988borderCol;
            if (_local2 !== _arg1){
                this._197399988borderCol = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "borderCol", _local2, _arg1));
            };
        }
        public function set screeningScore(_arg1:Number):void{
            _screeningScore = _arg1;
        }
        private function get borderCol():Number{
            return (this._197399988borderCol);
        }

        public static function set watcherSetupUtil(_arg1:IWatcherSetupUtil):void{
            BalanceGraphReflectionCanvas._watcherSetupUtil = _arg1;
        }

    }
}//package com.redbox.changetech.view.components 
