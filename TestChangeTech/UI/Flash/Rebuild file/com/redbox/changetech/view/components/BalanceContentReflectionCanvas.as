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
    import com.redbox.changetech.view.modules.*;
    import com.rictus.reflector.*;
    import flash.filters.*;
    import flash.ui.*;
    import com.redbox.changetech.util.*;
    import com.redbox.changetech.util.Enumerations.*;
    import com.redbox.changetech.view.components.questions.*;
    import flash.external.*;
    import flash.debugger.*;
    import flash.errors.*;
    import flash.printing.*;
    import flash.profiler.*;

    public class BalanceContentReflectionCanvas extends Canvas implements IBindingClient {

        private var _1707945992contentContainer:VBox
        private var _925318956roomVO:RoomVO
        mx_internal var _watchers:Array
        private var _197399988borderCol:Number
        public var isToDisableBinding:Boolean = true
        private var _content:Content
        private var timer:Timer
        mx_internal var _bindingsByDestination:Object
        mx_internal var _bindingsBeginWithWord:Object
        private var titleTextArea:BalanceTextArea
        public var module:BasicModule
        mx_internal var _bindings:Array
        public var _BalanceContentReflectionCanvas_Reflector1:Reflector
        private var _documentDescriptor_:UIComponentDescriptor
        private var changeWatcher:ChangeWatcher
        private var _1939189620copyContainer:Canvas

        private static var _watcherSetupUtil:IWatcherSetupUtil;

        public function BalanceContentReflectionCanvas(){
            _documentDescriptor_ = new UIComponentDescriptor({type:Canvas, propertiesFactory:function ():Object{
                return ({width:435, childDescriptors:[new UIComponentDescriptor({type:Canvas, id:"copyContainer", propertiesFactory:function ():Object{
                    return ({width:430, styleName:"roundedGradCanvas", childDescriptors:[new UIComponentDescriptor({type:VBox, id:"contentContainer", stylesFactory:function ():void{
                        this.verticalGap = 9;
                        this.paddingLeft = 20;
                        this.paddingRight = 20;
                        this.paddingTop = 20;
                        this.paddingBottom = 50;
                    }, propertiesFactory:function ():Object{
                        return ({percentWidth:100});
                    }})]});
                }}), new UIComponentDescriptor({type:Reflector, id:"_BalanceContentReflectionCanvas_Reflector1", propertiesFactory:function ():Object{
                    return ({falloff:0.1, alpha:0.5});
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
            this.verticalScrollPolicy = "off";
            this.addEventListener("creationComplete", ___BalanceContentReflectionCanvas_Canvas1_creationComplete);
        }
        public function get contentContainer():VBox{
            return (this._1707945992contentContainer);
        }
        public function get copyContainer():Canvas{
            return (this._1939189620copyContainer);
        }
        private function _BalanceContentReflectionCanvas_bindingsSetup():Array{
            var binding:* = null;
            var result:* = [];
            binding = new Binding(this, function ():Number{
                return (contentContainer.height);
            }, function (_arg1:Number):void{
                copyContainer.height = _arg1;
            }, "copyContainer.height");
            result[0] = binding;
            binding = new Binding(this, function ():UIComponent{
                return (copyContainer);
            }, function (_arg1:UIComponent):void{
                _BalanceContentReflectionCanvas_Reflector1.target = _arg1;
            }, "_BalanceContentReflectionCanvas_Reflector1.target");
            result[1] = binding;
            binding = new Binding(this, function ():Number{
                return ((copyContainer.height + 0.1));
            }, function (_arg1:Number):void{
                _BalanceContentReflectionCanvas_Reflector1.y = _arg1;
            }, "_BalanceContentReflectionCanvas_Reflector1.y");
            result[2] = binding;
            return (result);
        }
        public function set contentContainer(_arg1:VBox):void{
            var _local2:Object = this._1707945992contentContainer;
            if (_local2 !== _arg1){
                this._1707945992contentContainer = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "contentContainer", _local2, _arg1));
            };
        }
        override public function initialize():void{
            var target:* = null;
            var watcherSetupUtilClass:* = null;
            mx_internal::setDocumentDescriptor(_documentDescriptor_);
            var bindings:* = _BalanceContentReflectionCanvas_bindingsSetup();
            var watchers:* = [];
            target = this;
            if (_watcherSetupUtil == null){
                watcherSetupUtilClass = getDefinitionByName("_com_redbox_changetech_view_components_BalanceContentReflectionCanvasWatcherSetupUtil");
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
        public function set copyContainer(_arg1:Canvas):void{
            var _local2:Object = this._1939189620copyContainer;
            if (_local2 !== _arg1){
                this._1939189620copyContainer = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "copyContainer", _local2, _arg1));
            };
        }
        private function _BalanceContentReflectionCanvas_bindingExprs():void{
            var _local1:*;
            _local1 = contentContainer.height;
            _local1 = copyContainer;
            _local1 = (copyContainer.height + 0.1);
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
        private function changeRoom(_arg1:int):void{
            roomVO = RoomVO(Config.ROOM_CONFIGS.getItemAt(_arg1));
            titleTextArea.setStyle("color", roomVO.textColour1);
        }
        private function set roomVO(_arg1:RoomVO):void{
            var _local2:Object = this._925318956roomVO;
            if (_local2 !== _arg1){
                this._925318956roomVO = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "roomVO", _local2, _arg1));
            };
        }
        private function set _951530617content(_arg1:Content):void{
            _content = _arg1;
            roomVO = RoomVO(Config.ROOM_CONFIGS.getItemAt(BalanceModelLocator.getInstance().room));
            attachContent();
        }
        private function replacePlaceholders(_arg1:String):String{
            _arg1 = _arg1.replace("[SHS_SCORE]", BalanceModelLocator.getInstance().happinessScore.toString());
            return (_arg1);
        }
        private function get borderCol():Number{
            return (this._197399988borderCol);
        }
        private function get roomVO():RoomVO{
            return (this._925318956roomVO);
        }
        public function ___BalanceContentReflectionCanvas_Canvas1_creationComplete(_arg1:FlexEvent):void{
            init(_arg1);
        }
        private function removeBinding(_arg1:TimerEvent):void{
            changeWatcher.unwatch();
        }
        private function set borderCol(_arg1:Number):void{
            var _local2:Object = this._197399988borderCol;
            if (_local2 !== _arg1){
                this._197399988borderCol = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "borderCol", _local2, _arg1));
            };
        }
        public function set content(_arg1:Content):void{
            var _local2:Object = this.content;
            if (_local2 !== _arg1){
                this._951530617content = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "content", _local2, _arg1));
            };
        }
        public function attachContent():void{
            var _local4:BalanceTextArea;
            var _local5:Button;
            var _local6:BalanceButton;
            var _local7:Question;
            var _local8:MultiLineTextQuestion;
            var _local9:NumericQuestion;
            var _local10:RadioButtonQuestion;
            var _local11:SingleLineTextQuestion;
            var _local12:RoomVO;
            var _local13:SliderQuestion;
            var _local14:TimeInputQuestion;
            var _local15:Stopwatch;
            var _local16:EditableList;
            var _local17:Media;
            var _local18:BalanceAudioPlayer;
            var _local1:Array = contentContainer.getChildren();
            var _local2:Number = 0;
            while (_local2 < _local1.length) {
                contentContainer.removeChild(_local1[_local2]);
                _local2++;
            };
            titleTextArea = new BalanceTextArea();
            titleTextArea.htmlText = content.Title;
            titleTextArea.tabEnabled = false;
            titleTextArea.selectable = false;
            titleTextArea.setStyle("focusAlpha", 0);
            titleTextArea.setStyle("color", roomVO.textColour1);
            titleTextArea.setStyle("fontSize", 21);
            titleTextArea.percentWidth = 100;
            contentContainer.addChild(titleTextArea);
            var _local3:Array = content.getLayout();
            _local2 = 0;
            while (_local2 < _local3.length) {
                if (typeof(_local3[_local2]) == "string"){
                    _local4 = new BalanceTextArea();
                    _local4.htmlText = replacePlaceholders(_local3[_local2]);
                    _local4.styleSheet = BalanceModelLocator.getInstance().balanceStyleSheet;
                    _local4.selectable = false;
                    _local4.tabEnabled = false;
                    _local4.setStyle("focusAlpha", 0);
                    _local4.percentWidth = 100;
                    contentContainer.addChild(_local4);
                } else {
                    if ((_local3[_local2] instanceof Button)){
                        _local5 = Button(_local3[_local2]);
                        if (((((!((_local5.Type == Button.PRIMARY))) && (!((_local5.Type == Button.SECONDARY))))) && (!((_local5.Type == Button.TERTIARY))))){
                            _local6 = new BalanceButton();
                            _local6.label = _local5.Label;
                            _local6.buttonType = _local5.Type;
                            _local6.action = _local5.ButtonAction;
                            contentContainer.addChild(_local6);
                        };
                    };
                    if ((_local3[_local2] instanceof Question)){
                        _local7 = Question(_local3[_local2]);
                        switch (_local7.Type){
                            case QuestionType.MultiLineText.Text:
                                _local8 = new MultiLineTextQuestion();
                                _local8.question = _local7;
                                _local8.answer = _local7.PreviousAnswer;
                                _local8.percentWidth = 100;
                                contentContainer.addChild(_local8);
                                break;
                            case QuestionType.Numeric.Text:
                                _local9 = new NumericQuestion();
                                _local9.question = _local7;
                                _local9.answer = _local7.PreviousAnswer;
                                contentContainer.addChild(_local9);
                                break;
                            case QuestionType.RadioButton.Text:
                                _local10 = new RadioButtonQuestion();
                                _local10.question = _local7;
                                contentContainer.addChild(_local10);
                                _local10.content = content;
                                break;
                            case QuestionType.SingleLine.Text:
                                _local11 = new SingleLineTextQuestion();
                                _local11.question = _local7;
                                _local11.answer = _local7.PreviousAnswer;
                                contentContainer.addChild(_local11);
                                break;
                            case QuestionType.Slider.Text:
                                _local12 = RoomVO(Config.ROOM_CONFIGS.getItemAt(BalanceModelLocator.getInstance().room));
                                _local13 = new SliderQuestion();
                                switch (_local12.roomName){
                                    case RoomName.Emotion.Text:
                                        _local13.sliderStyleName = "balanceSliderNumbersEmotion";
                                        break;
                                    case RoomName.Willpower.Text:
                                        _local13.sliderStyleName = "balanceSliderNumbersWillpower";
                                        break;
                                    case RoomName.Motivation.Text:
                                        _local13.sliderStyleName = "balanceSliderNumbersMotivation";
                                        break;
                                    case RoomName.Blank.Text:
                                        _local13.sliderStyleName = "balanceSliderNumbersMotivation";
                                        break;
                                };
                                _local13.question = _local7;
                                contentContainer.addChild(_local13);
                                break;
                            case QuestionType.TimeInputQuestion.Text:
                                _local14 = new TimeInputQuestion();
                                _local14.question = _local7;
                                contentContainer.addChild(_local14);
                                break;
                            case QuestionType.Stopwatch.Text:
                                _local15 = new Stopwatch();
                                _local15.question = _local7;
                                contentContainer.addChild(_local15);
                                break;
                            case QuestionType.EditableList.Text:
                                _local16 = new EditableList();
                                _local16.question = _local7;
                                _local16.answer = _local7.PreviousAnswer;
                                contentContainer.addChild(_local16);
                                break;
                        };
                    };
                    if ((_local3[_local2] instanceof Media)){
                        _local17 = Media(_local3[_local2]);
                        switch (_local17.Type){
                            case Media.AUDIO:
                                _local18 = new BalanceAudioPlayer();
                                _local18.mediaTitle = _local17.Title;
                                _local18.mediaURL = _local17.Url;
                                contentContainer.addChild(_local18);
                                break;
                            case Media.VIDEO:
                                break;
                        };
                    };
                };
                _local2++;
            };
        }
        public function get content():Content{
            return (_content);
        }

        public static function set watcherSetupUtil(_arg1:IWatcherSetupUtil):void{
            BalanceContentReflectionCanvas._watcherSetupUtil = _arg1;
        }

    }
}//package com.redbox.changetech.view.components 
