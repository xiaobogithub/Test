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

    public class CompletionGraph extends ModuleViewTemplate implements IBindingClient {

        private var _811296866contentImage:BalanceImageReflectionCanvas
        private var LOWER_RED_DRINKS:Number
        private var UPPER_YELLOW_DRINKS:Number
        private var screeningColor:String
        private var _1082042285cta_btn:BalanceButtonReflectionCanvas
        private var _1298893550cta_container:HBox
        private var _1538893297aux_btn2:BalanceTertiaryButtonReflectionCanvas
        public var _CompletionGraph_Spacer1:Spacer
        mx_internal var _watchers:Array
        private var _3237038info:VBox
        private var LOWER_YELLOW_DRINKS:Number
        private var _643094943aux_btn:BalanceSecondaryButtonReflectionCanvas
        mx_internal var _bindingsByDestination:Object
        private var _1549852825transContainer2:Canvas
        mx_internal var _bindingsBeginWithWord:Object
        private var _3086276screeningScore:Number
        private var UPPER_GREEN_DRINKS:Number
        private var completionColor:String
        private var _1549852824transContainer1:Canvas
        mx_internal var _bindings:Array
        private var _documentDescriptor_:UIComponentDescriptor
        private var _1185051670completionScore:Number
        private var _98615630graph:BalanceGraphReflectionCanvas

        private static var CompletionFrequency:String = "CompletionFrequency";
        private static var YellowToYellow:String = "YellowToYellow";
        private static var CompletionBinge:String = "CompletionBinge";
        private static var GreenToYellow:String = "GreenToYellow";
        private static var YellowToGreen:String = "YellowToGreen";
        private static var RedToGreen:String = "RedToGreen";
        private static var GreenToGreen:String = "GreenToGreen";
        private static var CompletionQuantity:String = "CompletionQuantity";
        private static var YellowToRed:String = "YellowToRed";
        private static var RedToRed:String = "RedToRed";
        private static var _watcherSetupUtil:IWatcherSetupUtil;
        private static var GreenToRed:String = "GreenToRed";

        public function CompletionGraph(){
            _documentDescriptor_ = new UIComponentDescriptor({type:ModuleViewTemplate, propertiesFactory:function ():Object{
                return ({childDescriptors:[new UIComponentDescriptor({type:Canvas, id:"transContainer1", propertiesFactory:function ():Object{
                    return ({verticalScrollPolicy:"off", childDescriptors:[new UIComponentDescriptor({type:VBox, id:"info", stylesFactory:function ():void{
                        this.verticalGap = -50;
                        this.horizontalAlign = "center";
                    }, propertiesFactory:function ():Object{
                        return ({width:450, y:0, verticalScrollPolicy:"off", clipContent:false, childDescriptors:[new UIComponentDescriptor({type:BalanceGraphReflectionCanvas, id:"graph"}), new UIComponentDescriptor({type:HBox, id:"cta_container", stylesFactory:function ():void{
                            this.horizontalAlign = "right";
                        }, propertiesFactory:function ():Object{
                            return ({width:440, childDescriptors:[new UIComponentDescriptor({type:BalanceSecondaryButtonReflectionCanvas, id:"aux_btn"}), new UIComponentDescriptor({type:VBox, stylesFactory:function ():void{
                                this.verticalAlign = "top";
                            }, propertiesFactory:function ():Object{
                                return ({childDescriptors:[new UIComponentDescriptor({type:BalanceButtonReflectionCanvas, id:"cta_btn"}), new UIComponentDescriptor({type:BalanceTertiaryButtonReflectionCanvas, id:"aux_btn2"})]});
                            }})]});
                        }})]});
                    }})]});
                }}), new UIComponentDescriptor({type:Canvas, id:"transContainer2", propertiesFactory:function ():Object{
                    return ({verticalScrollPolicy:"off", childDescriptors:[new UIComponentDescriptor({type:HBox, propertiesFactory:function ():Object{
                        return ({childDescriptors:[new UIComponentDescriptor({type:Spacer, id:"_CompletionGraph_Spacer1"}), new UIComponentDescriptor({type:BalanceImageReflectionCanvas, id:"contentImage"})]});
                    }})]});
                }})]});
            }});
            _bindings = [];
            _watchers = [];
            _bindingsByDestination = {};
            _bindingsBeginWithWord = {};
            super();
            mx_internal::_document = this;
            this.addEventListener("creationComplete", ___CompletionGraph_ModuleViewTemplate1_creationComplete);
        }
        override public function initialize():void{
            var target:* = null;
            var watcherSetupUtilClass:* = null;
            mx_internal::setDocumentDescriptor(_documentDescriptor_);
            var bindings:* = _CompletionGraph_bindingsSetup();
            var watchers:* = [];
            target = this;
            if (_watcherSetupUtil == null){
                watcherSetupUtilClass = getDefinitionByName("_com_redbox_changetech_view_templates_CompletionGraphWatcherSetupUtil");
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
        public function set graph(_arg1:BalanceGraphReflectionCanvas):void{
            var _local2:Object = this._98615630graph;
            if (_local2 !== _arg1){
                this._98615630graph = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "graph", _local2, _arg1));
            };
        }
        private function getScoreFromCompletionUsage():Number{
            var _local5:Consumption;
            var _local6:*;
            var _local1:Number = 0;
            var _local2:Number = 0;
            var _local3:Number = 0;
            while (_local3 < model.completionUsage.length) {
                _local5 = model.completionUsage[_local3];
                _local6 = Number(_local5.total(false));
                if (_local6 > 0){
                    _local2++;
                };
                _local1 = (_local1 + _local6);
                _local3++;
            };
            var _local4:Number = 0;
            switch (true){
                case (((_local1 >= 0)) && ((_local1 <= UPPER_GREEN_DRINKS))):
                    _local4 = (_local4 + 0);
                    break;
                case (((_local1 >= LOWER_YELLOW_DRINKS)) && ((_local1 <= UPPER_YELLOW_DRINKS))):
                    _local4 = (_local4 + 3);
                    break;
                case (_local1 > LOWER_RED_DRINKS):
                    _local4 = (_local4 + 7);
                    break;
            };
            if (_local2 > 6){
                _local4 = (_local4 + 3);
            };
            return (_local4);
        }
        public function get transContainer2():Canvas{
            return (this._1549852825transContainer2);
        }
        public function ___CompletionGraph_ModuleViewTemplate1_creationComplete(_arg1:FlexEvent):void{
            init(_arg1);
        }
        private function get completionScore():Number{
            return (this._1185051670completionScore);
        }
        public function get transContainer1():Canvas{
            return (this._1549852824transContainer1);
        }
        public function set cta_container(_arg1:HBox):void{
            var _local2:Object = this._1298893550cta_container;
            if (_local2 !== _arg1){
                this._1298893550cta_container = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "cta_container", _local2, _arg1));
            };
        }
        public function get cta_btn():BalanceButtonReflectionCanvas{
            return (this._1082042285cta_btn);
        }
        public function set transContainer1(_arg1:Canvas):void{
            var _local2:Object = this._1549852824transContainer1;
            if (_local2 !== _arg1){
                this._1549852824transContainer1 = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "transContainer1", _local2, _arg1));
            };
        }
        public function get cta_container():HBox{
            return (this._1298893550cta_container);
        }
        public function get info():VBox{
            return (this._3237038info);
        }
        private function set completionScore(_arg1:Number):void{
            var _local2:Object = this._1185051670completionScore;
            if (_local2 !== _arg1){
                this._1185051670completionScore = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "completionScore", _local2, _arg1));
            };
        }
        private function get screeningScore():Number{
            return (this._3086276screeningScore);
        }
        private function init(_arg1:FlexEvent):void{
            if (model.consumer.Gender == "Male"){
                UPPER_GREEN_DRINKS = 13;
                LOWER_YELLOW_DRINKS = 14;
                UPPER_YELLOW_DRINKS = 20;
                LOWER_RED_DRINKS = 21;
            } else {
                UPPER_GREEN_DRINKS = 8;
                LOWER_YELLOW_DRINKS = 9;
                UPPER_YELLOW_DRINKS = 13;
                LOWER_RED_DRINKS = 14;
            };
            completionScore = (getScoreFromCompletionUsage() + BasicModule(module).getTotalScore());
            trace(("getScoreFromCompletionUsage()=" + getScoreFromCompletionUsage()));
            screeningScore = model.consumer.ScreeningScore;
            trace("TEMPLATE:");
            trace(("completionScore=" + completionScore));
            trace(("screeningScore=" + screeningScore));
            trace("-----------------------------------");
            switch (true){
                case (((completionScore >= 0)) && ((completionScore <= 2))):
                    completionColor = ScreeningTest.GREEN;
                    break;
                case (((completionScore >= 3)) && ((completionScore <= 6))):
                    completionColor = ScreeningTest.YELLOW;
                    break;
                case (completionScore > 7):
                    completionColor = ScreeningTest.RED;
                    break;
            };
            screeningColor = model.consumer.ScreeningColor;
            switch (true){
                case (((screeningColor == ScreeningTest.RED)) && ((completionColor == ScreeningTest.GREEN))):
                    BasicModule(module).currentTag = RedToGreen;
                    break;
                case (((screeningColor == ScreeningTest.RED)) && ((completionColor == ScreeningTest.GREEN))):
                    BasicModule(module).currentTag = YellowToGreen;
                    break;
                case (((screeningColor == ScreeningTest.RED)) && ((completionColor == ScreeningTest.GREEN))):
                    BasicModule(module).currentTag = GreenToGreen;
                    break;
                case (((screeningColor == ScreeningTest.RED)) && ((completionColor == ScreeningTest.GREEN))):
                    BasicModule(module).currentTag = YellowToYellow;
                    break;
                case (((screeningColor == ScreeningTest.RED)) && ((completionColor == ScreeningTest.GREEN))):
                    BasicModule(module).currentTag = RedToRed;
                    break;
                case (((screeningColor == ScreeningTest.RED)) && ((completionColor == ScreeningTest.GREEN))):
                    BasicModule(module).currentTag = YellowToRed;
                    break;
                case (((screeningColor == ScreeningTest.RED)) && ((completionColor == ScreeningTest.GREEN))):
                    BasicModule(module).currentTag = GreenToYellow;
                    break;
                case (((screeningColor == ScreeningTest.RED)) && ((completionColor == ScreeningTest.GREEN))):
                    BasicModule(module).currentTag = GreenToRed;
                    break;
            };
            graph.checkInit();
        }
        public function set transContainer2(_arg1:Canvas):void{
            var _local2:Object = this._1549852825transContainer2;
            if (_local2 !== _arg1){
                this._1549852825transContainer2 = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "transContainer2", _local2, _arg1));
            };
        }
        private function _CompletionGraph_bindingsSetup():Array{
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
                return ((contentImage.x + contentImage.width));
            }, function (_arg1:Number):void{
                info.x = _arg1;
            }, "info.x");
            result[2] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = content.Title;
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                graph.title_str = _arg1;
            }, "graph.title_str");
            result[3] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = content.getTextLayoutAsOneString();
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                graph.copy_str = _arg1;
            }, "graph.copy_str");
            result[4] = binding;
            binding = new Binding(this, function ():Number{
                return (screeningScore);
            }, function (_arg1:Number):void{
                graph.screeningScore = _arg1;
            }, "graph.screeningScore");
            result[5] = binding;
            binding = new Binding(this, function ():Number{
                return (completionScore);
            }, function (_arg1:Number):void{
                graph.completionScore = _arg1;
            }, "graph.completionScore");
            result[6] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = content.getSecondaryButton().Label;
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                aux_btn.buttonLabel = _arg1;
            }, "aux_btn.buttonLabel");
            result[7] = binding;
            binding = new Binding(this, function ():Boolean{
                return (!((content.getSecondaryButton() == null)));
            }, function (_arg1:Boolean):void{
                aux_btn.visible = _arg1;
            }, "aux_btn.visible");
            result[8] = binding;
            binding = new Binding(this, function ():ButtonActionVO{
                return (content.getSecondaryButton()().ButtonAction);
            }, function (_arg1:ButtonActionVO):void{
                aux_btn.action = _arg1;
            }, "aux_btn.action");
            result[9] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = content.getCTAButton().Label;
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                cta_btn.buttonLabel = _arg1;
            }, "cta_btn.buttonLabel");
            result[10] = binding;
            binding = new Binding(this, function ():ButtonActionVO{
                return (content.getCTAButton().ButtonAction);
            }, function (_arg1:ButtonActionVO):void{
                cta_btn.action = _arg1;
            }, "cta_btn.action");
            result[11] = binding;
            binding = new Binding(this, function ():Boolean{
                return ((content.getTertiaryButton() == null));
            }, function (_arg1:Boolean):void{
                cta_btn.reflectionIsOn = _arg1;
            }, "cta_btn.reflectionIsOn");
            result[12] = binding;
            binding = new Binding(this, function ():Boolean{
                return (true);
            }, function (_arg1:Boolean):void{
                cta_btn.visible = _arg1;
            }, "cta_btn.visible");
            result[13] = binding;
            binding = new Binding(this, function ():Boolean{
                return (BasicModule(module).mandatoryQuestionsComplete);
            }, function (_arg1:Boolean):void{
                cta_btn.buttonEnabled = _arg1;
            }, "cta_btn.buttonEnabled");
            result[14] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = content.getTertiaryButton().Label;
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                aux_btn2.buttonLabel = _arg1;
            }, "aux_btn2.buttonLabel");
            result[15] = binding;
            binding = new Binding(this, function ():Number{
                return (cta_btn.height);
            }, function (_arg1:Number):void{
                aux_btn2.y = _arg1;
            }, "aux_btn2.y");
            result[16] = binding;
            binding = new Binding(this, function ():Boolean{
                return (!((content.getTertiaryButton() == null)));
            }, function (_arg1:Boolean):void{
                aux_btn2.visible = _arg1;
            }, "aux_btn2.visible");
            result[17] = binding;
            binding = new Binding(this, function ():ButtonActionVO{
                return (content.getTertiaryButton().ButtonAction);
            }, function (_arg1:ButtonActionVO):void{
                aux_btn2.action = _arg1;
            }, "aux_btn2.action");
            result[18] = binding;
            binding = new Binding(this, function ():Number{
                return (model.currentStageWidth);
            }, function (_arg1:Number):void{
                transContainer2.width = _arg1;
            }, "transContainer2.width");
            result[19] = binding;
            binding = new Binding(this, function ():Number{
                return (model.currentStageHeight);
            }, function (_arg1:Number):void{
                transContainer2.height = _arg1;
            }, "transContainer2.height");
            result[20] = binding;
            binding = new Binding(this, function ():Number{
                return (Math.max(((model.currentStageWidth - (contentImage.width + info.width)) / 3), 0));
            }, function (_arg1:Number):void{
                _CompletionGraph_Spacer1.width = _arg1;
            }, "_CompletionGraph_Spacer1.width");
            result[21] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = content.PresenterImageUrl;
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                contentImage.source = _arg1;
            }, "contentImage.source");
            result[22] = binding;
            return (result);
        }
        public function set contentImage(_arg1:BalanceImageReflectionCanvas):void{
            var _local2:Object = this._811296866contentImage;
            if (_local2 !== _arg1){
                this._811296866contentImage = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "contentImage", _local2, _arg1));
            };
        }
        public function set aux_btn(_arg1:BalanceSecondaryButtonReflectionCanvas):void{
            var _local2:Object = this._643094943aux_btn;
            if (_local2 !== _arg1){
                this._643094943aux_btn = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "aux_btn", _local2, _arg1));
            };
        }
        public function get graph():BalanceGraphReflectionCanvas{
            return (this._98615630graph);
        }
        public function set info(_arg1:VBox):void{
            var _local2:Object = this._3237038info;
            if (_local2 !== _arg1){
                this._3237038info = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "info", _local2, _arg1));
            };
        }
        public function get aux_btn():BalanceSecondaryButtonReflectionCanvas{
            return (this._643094943aux_btn);
        }
        public function set aux_btn2(_arg1:BalanceTertiaryButtonReflectionCanvas):void{
            var _local2:Object = this._1538893297aux_btn2;
            if (_local2 !== _arg1){
                this._1538893297aux_btn2 = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "aux_btn2", _local2, _arg1));
            };
        }
        private function _CompletionGraph_bindingExprs():void{
            var _local1:*;
            _local1 = model.currentStageWidth;
            _local1 = model.currentStageHeight;
            _local1 = (contentImage.x + contentImage.width);
            _local1 = content.Title;
            _local1 = content.getTextLayoutAsOneString();
            _local1 = screeningScore;
            _local1 = completionScore;
            _local1 = content.getSecondaryButton().Label;
            _local1 = !((content.getSecondaryButton() == null));
            _local1 = content.getSecondaryButton()().ButtonAction;
            _local1 = content.getCTAButton().Label;
            _local1 = content.getCTAButton().ButtonAction;
            _local1 = (content.getTertiaryButton() == null);
            _local1 = true;
            _local1 = BasicModule(module).mandatoryQuestionsComplete;
            _local1 = content.getTertiaryButton().Label;
            _local1 = cta_btn.height;
            _local1 = !((content.getTertiaryButton() == null));
            _local1 = content.getTertiaryButton().ButtonAction;
            _local1 = model.currentStageWidth;
            _local1 = model.currentStageHeight;
            _local1 = Math.max(((model.currentStageWidth - (contentImage.width + info.width)) / 3), 0);
            _local1 = content.PresenterImageUrl;
        }
        public function get contentImage():BalanceImageReflectionCanvas{
            return (this._811296866contentImage);
        }
        public function get aux_btn2():BalanceTertiaryButtonReflectionCanvas{
            return (this._1538893297aux_btn2);
        }
        public function set cta_btn(_arg1:BalanceButtonReflectionCanvas):void{
            var _local2:Object = this._1082042285cta_btn;
            if (_local2 !== _arg1){
                this._1082042285cta_btn = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "cta_btn", _local2, _arg1));
            };
        }
        private function set screeningScore(_arg1:Number):void{
            var _local2:Object = this._3086276screeningScore;
            if (_local2 !== _arg1){
                this._3086276screeningScore = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "screeningScore", _local2, _arg1));
            };
        }

        public static function set watcherSetupUtil(_arg1:IWatcherSetupUtil):void{
            CompletionGraph._watcherSetupUtil = _arg1;
        }

    }
}//package com.redbox.changetech.view.templates 
