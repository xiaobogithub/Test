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
    import mx.containers.*;
    import com.redbox.changetech.view.components.*;
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
    import flash.external.*;
    import flash.debugger.*;
    import flash.errors.*;
    import flash.printing.*;
    import flash.profiler.*;

    public class TrafficLightResults extends ModuleViewTemplate implements IBindingClient {

        public var _TrafficLightResults_Label3:Label
        private var _1939189620copyContainer:BalanceCopyReflectionCanvas
        private var _1298893550cta_container:HBox
        private var _3237038info:VBox
        mx_internal var _watchers:Array
        public var _TrafficLightResults_Canvas3:Canvas
        private var _1560460680scoreField:Text
        private var _579457136redPointString:String
        mx_internal var _bindingsByDestination:Object
        private var _1549852825transContainer2:Canvas
        private var _1039406931yellowPointString:String
        private var _975427772forwardEnabled:Boolean = false
        private var _222560717greyGrad:GradientCanvas
        private var _1052128350trafficlightImage:Image
        private var _554501865largeSpeechBubble:VBox
        private var _109264530score:Number
        mx_internal var _bindingsBeginWithWord:Object
        private var _1130586434greenPointString:String
        private var _2038356445largeBlueGrad:GradientCanvas
        private var _1549852824transContainer1:Canvas
        private var _1707945992contentContainer:BalanceContentReflectionCanvas
        mx_internal var _bindings:Array
        private var _documentDescriptor_:UIComponentDescriptor
        public var _TrafficLightResults_Label1:Label
        public var _TrafficLightResults_Label2:Label

        private static var _watcherSetupUtil:IWatcherSetupUtil;

        public function TrafficLightResults(){
            _documentDescriptor_ = new UIComponentDescriptor({type:ModuleViewTemplate, propertiesFactory:function ():Object{
                return ({childDescriptors:[new UIComponentDescriptor({type:Canvas, id:"transContainer1", propertiesFactory:function ():Object{
                    return ({verticalScrollPolicy:"off", horizontalScrollPolicy:"off", alpha:0, childDescriptors:[new UIComponentDescriptor({type:VBox, id:"info", stylesFactory:function ():void{
                        this.horizontalAlign = "left";
                    }, propertiesFactory:function ():Object{
                        return ({width:450, x:30, verticalScrollPolicy:"off", clipContent:false, childDescriptors:[new UIComponentDescriptor({type:BalanceCopyReflectionCanvas, id:"copyContainer"}), new UIComponentDescriptor({type:BalanceContentReflectionCanvas, id:"contentContainer"}), new UIComponentDescriptor({type:HBox, id:"cta_container", stylesFactory:function ():void{
                            this.horizontalAlign = "right";
                        }, propertiesFactory:function ():Object{
                            return ({width:440, childDescriptors:[new UIComponentDescriptor({type:VBox, id:"largeSpeechBubble", stylesFactory:function ():void{
                                this.verticalGap = 0;
                            }, propertiesFactory:function ():Object{
                                return ({filters:[_TrafficLightResults_DropShadowFilter1_c()], childDescriptors:[new UIComponentDescriptor({type:GradientCanvas, id:"largeBlueGrad", stylesFactory:function ():void{
                                    this.fillColors = [2729956, 1731474];
                                    this.fillAlphas = [1, 1];
                                    this.gradientRatio = [0, 0xFF];
                                    this.angle = [90];
                                    this.borderAlphas = [0];
                                    this.cornerRadius = 10;
                                }, propertiesFactory:function ():Object{
                                    return ({width:280, height:190, colorsConfiguration:[2], childDescriptors:[new UIComponentDescriptor({type:VBox, stylesFactory:function ():void{
                                        this.horizontalAlign = "center";
                                        this.verticalAlign = "top";
                                        this.paddingTop = 20;
                                    }, propertiesFactory:function ():Object{
                                        return ({percentWidth:100, percentHeight:100, childDescriptors:[new UIComponentDescriptor({type:GradientCanvas, id:"greyGrad", stylesFactory:function ():void{
                                            this.fillColors = [0xFEFEFE, 15263719];
                                            this.fillAlphas = [1, 1];
                                            this.gradientRatio = [0, 0xFF];
                                            this.angle = [90];
                                            this.borderAlphas = [0];
                                            this.cornerRadius = 10;
                                        }, propertiesFactory:function ():Object{
                                            return ({width:120, height:100, colorsConfiguration:[2], filters:[_TrafficLightResults_DropShadowFilter2_c()], childDescriptors:[new UIComponentDescriptor({type:Text, id:"scoreField", stylesFactory:function ():void{
                                                this.textAlign = "center";
                                            }, propertiesFactory:function ():Object{
                                                return ({width:100, height:80, x:10, y:10, selectable:false, styleName:"helvetica68GreyBold"});
                                            }})]});
                                        }})]});
                                    }})]});
                                }})]});
                            }})]});
                        }})]});
                    }})]});
                }}), new UIComponentDescriptor({type:Canvas, id:"transContainer2", propertiesFactory:function ():Object{
                    return ({alpha:0, childDescriptors:[new UIComponentDescriptor({type:Image, id:"trafficlightImage", events:{complete:"__trafficlightImage_complete"}, propertiesFactory:function ():Object{
                        return ({source:"assets/media/traffic_light.swf", x:550, height:650, scaleContent:false, enabled:false});
                    }}), new UIComponentDescriptor({type:Canvas, id:"_TrafficLightResults_Canvas3", propertiesFactory:function ():Object{
                        return ({width:160, x:800, childDescriptors:[new UIComponentDescriptor({type:HBox, stylesFactory:function ():void{
                            this.verticalAlign = "middle";
                        }, propertiesFactory:function ():Object{
                            return ({y:80, childDescriptors:[new UIComponentDescriptor({type:HRule, stylesFactory:function ():void{
                                this.strokeColor = 0x666666;
                                this.strokeWidth = 1;
                            }, propertiesFactory:function ():Object{
                                return ({width:15});
                            }}), new UIComponentDescriptor({type:Label, id:"_TrafficLightResults_Label1", propertiesFactory:function ():Object{
                                return ({styleName:"helvetica18GreyBold"});
                            }})]});
                        }}), new UIComponentDescriptor({type:HBox, stylesFactory:function ():void{
                            this.verticalAlign = "middle";
                        }, propertiesFactory:function ():Object{
                            return ({y:270, childDescriptors:[new UIComponentDescriptor({type:HRule, stylesFactory:function ():void{
                                this.strokeColor = 0x666666;
                                this.strokeWidth = 1;
                            }, propertiesFactory:function ():Object{
                                return ({width:15});
                            }}), new UIComponentDescriptor({type:Label, id:"_TrafficLightResults_Label2", propertiesFactory:function ():Object{
                                return ({styleName:"helvetica18GreyBold"});
                            }})]});
                        }}), new UIComponentDescriptor({type:HBox, stylesFactory:function ():void{
                            this.verticalAlign = "middle";
                        }, propertiesFactory:function ():Object{
                            return ({y:440, childDescriptors:[new UIComponentDescriptor({type:HRule, stylesFactory:function ():void{
                                this.strokeColor = 0x666666;
                                this.strokeWidth = 1;
                            }, propertiesFactory:function ():Object{
                                return ({width:15});
                            }}), new UIComponentDescriptor({type:Label, id:"_TrafficLightResults_Label3", propertiesFactory:function ():Object{
                                return ({styleName:"helvetica18GreyBold"});
                            }})]});
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
            this.addEventListener("creationComplete", ___TrafficLightResults_ModuleViewTemplate1_creationComplete);
        }
        private function _TrafficLightResults_bindingExprs():void{
            var _local1:*;
            _local1 = model.currentStageWidth;
            _local1 = model.currentStageHeight;
            _local1 = Math.max(((650 - (info.height / 2)) / 4), 0);
            _local1 = (forwardEnabled) ? (((contentContainer.height / 2) + 20) * -1) : (((copyContainer.height / 2) + 20) * -1);
            _local1 = model.languageVO.getLang("traffic_light_title");
            _local1 = model.languageVO.getLang("traffic_light_cta");
            _local1 = !(forwardEnabled);
            _local1 = !(forwardEnabled);
            _local1 = content;
            _local1 = BasicModule(module);
            _local1 = forwardEnabled;
            _local1 = forwardEnabled;
            _local1 = score;
            _local1 = model.currentStageWidth;
            _local1 = model.currentStageHeight;
            _local1 = trafficlightImage.height;
            _local1 = redPointString;
            _local1 = yellowPointString;
            _local1 = greenPointString;
        }
        private function set greenPointString(_arg1:String):void{
            var _local2:Object = this._1130586434greenPointString;
            if (_local2 !== _arg1){
                this._1130586434greenPointString = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "greenPointString", _local2, _arg1));
            };
        }
        public function get contentContainer():BalanceContentReflectionCanvas{
            return (this._1707945992contentContainer);
        }
        private function _TrafficLightResults_DropShadowFilter1_c():DropShadowFilter{
            var _local1:DropShadowFilter = new DropShadowFilter();
            _local1.distance = 3;
            _local1.alpha = 0.5;
            return (_local1);
        }
        public function get scoreField():Text{
            return (this._1560460680scoreField);
        }
        override public function initialize():void{
            var target:* = null;
            var watcherSetupUtilClass:* = null;
            mx_internal::setDocumentDescriptor(_documentDescriptor_);
            var bindings:* = _TrafficLightResults_bindingsSetup();
            var watchers:* = [];
            target = this;
            if (_watcherSetupUtil == null){
                watcherSetupUtilClass = getDefinitionByName("_com_redbox_changetech_view_templates_TrafficLightResultsWatcherSetupUtil");
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
        public function get cta_container():HBox{
            return (this._1298893550cta_container);
        }
        public function get copyContainer():BalanceCopyReflectionCanvas{
            return (this._1939189620copyContainer);
        }
        public function get transContainer2():Canvas{
            return (this._1549852825transContainer2);
        }
        public function __trafficlightImage_complete(_arg1:Event):void{
            setTrafficLightInterface();
        }
        private function init(_arg1:FlexEvent):void{
            score = ScreeningTest(module).totalScore;
            trace(("ScreeningTest(module).firstQuestionScoreIsZero=" + ScreeningTest(module).firstQuestionScoreIsZero));
            if (ScreeningTest(module).firstQuestionScoreIsZero){
                redPointString = "7 - 10";
                yellowPointString = "3 - 6";
                greenPointString = "0 - 2";
            } else {
                redPointString = "10 - 26";
                yellowPointString = "3 - 9";
                greenPointString = "0 - 2";
            };
            trace(("ScreeningTest(module).genderString=" + ScreeningTest(module).genderString));
            BasicModule(module).currentTag = content.Tag;
        }
        public function get trafficlightImage():Image{
            return (this._1052128350trafficlightImage);
        }
        public function get largeBlueGrad():GradientCanvas{
            return (this._2038356445largeBlueGrad);
        }
        public function set cta_container(_arg1:HBox):void{
            var _local2:Object = this._1298893550cta_container;
            if (_local2 !== _arg1){
                this._1298893550cta_container = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "cta_container", _local2, _arg1));
            };
        }
        public function set contentContainer(_arg1:BalanceContentReflectionCanvas):void{
            var _local2:Object = this._1707945992contentContainer;
            if (_local2 !== _arg1){
                this._1707945992contentContainer = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "contentContainer", _local2, _arg1));
            };
        }
        public function set greyGrad(_arg1:GradientCanvas):void{
            var _local2:Object = this._222560717greyGrad;
            if (_local2 !== _arg1){
                this._222560717greyGrad = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "greyGrad", _local2, _arg1));
            };
        }
        private function set score(_arg1:Number):void{
            var _local2:Object = this._109264530score;
            if (_local2 !== _arg1){
                this._109264530score = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "score", _local2, _arg1));
            };
        }
        private function get yellowPointString():String{
            return (this._1039406931yellowPointString);
        }
        public function get info():VBox{
            return (this._3237038info);
        }
        public function set copyContainer(_arg1:BalanceCopyReflectionCanvas):void{
            var _local2:Object = this._1939189620copyContainer;
            if (_local2 !== _arg1){
                this._1939189620copyContainer = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "copyContainer", _local2, _arg1));
            };
        }
        public function ___TrafficLightResults_ModuleViewTemplate1_creationComplete(_arg1:FlexEvent):void{
            init(_arg1);
        }
        private function get score():Number{
            return (this._109264530score);
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
        private function get forwardEnabled():Boolean{
            return (this._975427772forwardEnabled);
        }
        public function set scoreField(_arg1:Text):void{
            var _local2:Object = this._1560460680scoreField;
            if (_local2 !== _arg1){
                this._1560460680scoreField = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "scoreField", _local2, _arg1));
            };
        }
        private function set forwardEnabled(_arg1:Boolean):void{
            var _local2:Object = this._975427772forwardEnabled;
            if (_local2 !== _arg1){
                this._975427772forwardEnabled = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "forwardEnabled", _local2, _arg1));
            };
        }
        public function set trafficlightImage(_arg1:Image):void{
            var _local2:Object = this._1052128350trafficlightImage;
            if (_local2 !== _arg1){
                this._1052128350trafficlightImage = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "trafficlightImage", _local2, _arg1));
            };
        }
        public function get transContainer1():Canvas{
            return (this._1549852824transContainer1);
        }
        public function set largeSpeechBubble(_arg1:VBox):void{
            var _local2:Object = this._554501865largeSpeechBubble;
            if (_local2 !== _arg1){
                this._554501865largeSpeechBubble = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "largeSpeechBubble", _local2, _arg1));
            };
        }
        public function get greyGrad():GradientCanvas{
            return (this._222560717greyGrad);
        }
        private function trafficLightClicked(_arg1:Event):void{
            trace(_arg1.target.name);
            switch (content.Tag){
                case ScreeningTest.GREEN:
                    if (_arg1.target.name == "green"){
                        trafficlightImage.source = Assets.getInstance().traffic_green;
                        forwardEnabled = true;
                    };
                    break;
                case ScreeningTest.YELLOW:
                    if (_arg1.target.name == "yellow"){
                        trafficlightImage.source = Assets.getInstance().traffic_yellow;
                        forwardEnabled = true;
                    };
                    break;
                case ScreeningTest.RED:
                    if (_arg1.target.name == "red"){
                        trafficlightImage.source = Assets.getInstance().traffic_red;
                        forwardEnabled = true;
                    };
                    break;
            };
            if (forwardEnabled){
                if (content.getCTAButton() == null){
                };
            };
        }
        private function setTrafficLightInterface():void{
            var _local1:Array = ["red", "yellow", "green"];
            var _local2:Number = 0;
            while (_local2 < _local1.length) {
                trafficlightImage.content[_local1[_local2]].buttonMode = true;
                trafficlightImage.content[_local1[_local2]].useHandCursor = true;
                trafficlightImage.content[_local1[_local2]].addEventListener(MouseEvent.CLICK, trafficLightClicked);
                _local2++;
            };
        }
        private function set yellowPointString(_arg1:String):void{
            var _local2:Object = this._1039406931yellowPointString;
            if (_local2 !== _arg1){
                this._1039406931yellowPointString = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "yellowPointString", _local2, _arg1));
            };
        }
        public function set info(_arg1:VBox):void{
            var _local2:Object = this._3237038info;
            if (_local2 !== _arg1){
                this._3237038info = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "info", _local2, _arg1));
            };
        }
        private function _TrafficLightResults_DropShadowFilter2_c():DropShadowFilter{
            var _local1:DropShadowFilter = new DropShadowFilter();
            _local1.distance = 3;
            _local1.alpha = 0.5;
            return (_local1);
        }
        private function set redPointString(_arg1:String):void{
            var _local2:Object = this._579457136redPointString;
            if (_local2 !== _arg1){
                this._579457136redPointString = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "redPointString", _local2, _arg1));
            };
        }
        public function set largeBlueGrad(_arg1:GradientCanvas):void{
            var _local2:Object = this._2038356445largeBlueGrad;
            if (_local2 !== _arg1){
                this._2038356445largeBlueGrad = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "largeBlueGrad", _local2, _arg1));
            };
        }
        private function initBounceEffect(_arg1:EffectEvent):void{
        }
        private function _TrafficLightResults_bindingsSetup():Array{
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
            binding = new Binding(this, function ():Number{
                return (Math.max(((650 - (info.height / 2)) / 4), 0));
            }, function (_arg1:Number):void{
                info.y = _arg1;
            }, "info.y");
            result[2] = binding;
            binding = new Binding(this, function ():Number{
                return ((forwardEnabled) ? (((contentContainer.height / 2) + 20) * -1) : (((copyContainer.height / 2) + 20) * -1));
            }, function (_arg1:Number):void{
                info.setStyle("verticalGap", _arg1);
            }, "info.verticalGap");
            result[3] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = model.languageVO.getLang("traffic_light_title");
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                copyContainer.title_str = _arg1;
            }, "copyContainer.title_str");
            result[4] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = model.languageVO.getLang("traffic_light_cta");
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                copyContainer.copy_str = _arg1;
            }, "copyContainer.copy_str");
            result[5] = binding;
            binding = new Binding(this, function ():Boolean{
                return (!(forwardEnabled));
            }, function (_arg1:Boolean):void{
                copyContainer.includeInLayout = _arg1;
            }, "copyContainer.includeInLayout");
            result[6] = binding;
            binding = new Binding(this, function ():Boolean{
                return (!(forwardEnabled));
            }, function (_arg1:Boolean):void{
                copyContainer.visible = _arg1;
            }, "copyContainer.visible");
            result[7] = binding;
            binding = new Binding(this, function ():Content{
                return (content);
            }, function (_arg1:Content):void{
                contentContainer.content = _arg1;
            }, "contentContainer.content");
            result[8] = binding;
            binding = new Binding(this, function ():BasicModule{
                return (BasicModule(module));
            }, function (_arg1:BasicModule):void{
                contentContainer.module = _arg1;
            }, "contentContainer.module");
            result[9] = binding;
            binding = new Binding(this, function ():Boolean{
                return (forwardEnabled);
            }, function (_arg1:Boolean):void{
                contentContainer.includeInLayout = _arg1;
            }, "contentContainer.includeInLayout");
            result[10] = binding;
            binding = new Binding(this, function ():Boolean{
                return (forwardEnabled);
            }, function (_arg1:Boolean):void{
                contentContainer.visible = _arg1;
            }, "contentContainer.visible");
            result[11] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = score;
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                scoreField.text = _arg1;
            }, "scoreField.text");
            result[12] = binding;
            binding = new Binding(this, function ():Number{
                return (model.currentStageWidth);
            }, function (_arg1:Number):void{
                transContainer2.width = _arg1;
            }, "transContainer2.width");
            result[13] = binding;
            binding = new Binding(this, function ():Number{
                return (model.currentStageHeight);
            }, function (_arg1:Number):void{
                transContainer2.height = _arg1;
            }, "transContainer2.height");
            result[14] = binding;
            binding = new Binding(this, function ():Number{
                return (trafficlightImage.height);
            }, function (_arg1:Number):void{
                _TrafficLightResults_Canvas3.height = _arg1;
            }, "_TrafficLightResults_Canvas3.height");
            result[15] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = redPointString;
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                _TrafficLightResults_Label1.text = _arg1;
            }, "_TrafficLightResults_Label1.text");
            result[16] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = yellowPointString;
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                _TrafficLightResults_Label2.text = _arg1;
            }, "_TrafficLightResults_Label2.text");
            result[17] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = greenPointString;
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                _TrafficLightResults_Label3.text = _arg1;
            }, "_TrafficLightResults_Label3.text");
            result[18] = binding;
            return (result);
        }
        private function buttonClick(_arg1:Event):void{
            if (forwardEnabled){
                module.next(_arg1);
            };
        }
        private function get redPointString():String{
            return (this._579457136redPointString);
        }
        public function get largeSpeechBubble():VBox{
            return (this._554501865largeSpeechBubble);
        }
        private function get greenPointString():String{
            return (this._1130586434greenPointString);
        }

        public static function set watcherSetupUtil(_arg1:IWatcherSetupUtil):void{
            TrafficLightResults._watcherSetupUtil = _arg1;
        }

    }
}//package com.redbox.changetech.view.templates 
