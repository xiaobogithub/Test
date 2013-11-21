//Created by Action Script Viewer - http://www.buraks.com/asv
package com.redbox.changetech.view.components.questions {
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
    import com.redbox.changetech.vo.*;
    import flash.utils.*;
    import flash.system.*;
    import flash.accessibility.*;
    import flash.xml.*;
    import flash.net.*;
    import com.redbox.changetech.event.*;
    import flash.filters.*;
    import flash.ui.*;
    import flash.external.*;
    import flash.debugger.*;
    import flash.errors.*;
    import flash.printing.*;
    import flash.profiler.*;

    public class Stopwatch extends Canvas implements IBindingClient {

        private var started:Boolean = false
        mx_internal var _watchers:Array
        private var _1377687758button:Button
        private var _166533772textAreaWidth:Number
        private var _793284926timeString:String = "00:00:00"
        private var model:BalanceModelLocator
        private var _1004197030textArea:TextArea
        private var _question:Question
        mx_internal var _bindingsBeginWithWord:Object
        private var startTime:Number
        mx_internal var _bindingsByDestination:Object
        private var _answer:String
        private var _1941678611currentModeString:String
        mx_internal var _bindings:Array
        private var _documentDescriptor_:UIComponentDescriptor

        private static var _watcherSetupUtil:IWatcherSetupUtil;

        public function Stopwatch(){
            _documentDescriptor_ = new UIComponentDescriptor({type:Canvas, propertiesFactory:function ():Object{
                return ({childDescriptors:[new UIComponentDescriptor({type:Button, id:"button", events:{click:"__button_click"}, propertiesFactory:function ():Object{
                    return ({styleName:"roundedGradCanvas", width:75});
                }}), new UIComponentDescriptor({type:TextArea, id:"textArea", stylesFactory:function ():void{
                    this.fontFamily = "Digital-7";
                    this.fontSize = 24;
                }, propertiesFactory:function ():Object{
                    return ({x:80, y:3});
                }})]});
            }});
            model = BalanceModelLocator.getInstance();
            _1941678611currentModeString = model.languageVO.getLang("start");
            _bindings = [];
            _watchers = [];
            _bindingsByDestination = {};
            _bindingsBeginWithWord = {};
            super();
            mx_internal::_document = this;
            this.addEventListener("creationComplete", ___Stopwatch_Canvas1_creationComplete);
        }
        private function set timeString(_arg1:String):void{
            var _local2:Object = this._793284926timeString;
            if (_local2 !== _arg1){
                this._793284926timeString = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "timeString", _local2, _arg1));
            };
        }
        private function get currentModeString():String{
            return (this._1941678611currentModeString);
        }
        public function get textAreaWidth():Number{
            return (this._166533772textAreaWidth);
        }
        private function init():void{
            addEventListener(Event.ENTER_FRAME, calculateTimeString);
        }
        override public function initialize():void{
            var target:* = null;
            var watcherSetupUtilClass:* = null;
            mx_internal::setDocumentDescriptor(_documentDescriptor_);
            var bindings:* = _Stopwatch_bindingsSetup();
            var watchers:* = [];
            target = this;
            if (_watcherSetupUtil == null){
                watcherSetupUtilClass = getDefinitionByName("_com_redbox_changetech_view_components_questions_StopwatchWatcherSetupUtil");
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
        public function __button_click(_arg1:MouseEvent):void{
            toggleStartStop();
        }
        private function toggleStartStop():void{
            started = !(started);
            if (started){
                startTime = new Date().getTime();
                currentModeString = model.languageVO.getLang("stop");
                optionSelected(new Event(Event.SELECT));
            } else {
                currentModeString = model.languageVO.getLang("start");
            };
        }
        public function get button():Button{
            return (this._1377687758button);
        }
        public function set textArea(_arg1:TextArea):void{
            var _local2:Object = this._1004197030textArea;
            if (_local2 !== _arg1){
                this._1004197030textArea = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "textArea", _local2, _arg1));
            };
        }
        private function optionSelected(_arg1:Event):void{
            var _local2:AnswerSelectedEvent = new AnswerSelectedEvent(AnswerSelectedEvent.ANSWER_SELECTED, true, false);
            _local2.id = question.Id;
            _local2.score = (new Date().getTime() - startTime);
            _local2.text = timeString;
            dispatchEvent(_local2);
        }
        private function getTimeString(_arg1:Number):String{
            var _local3:Number;
            var _local5:Number;
            var _local2:Number = Math.floor((_arg1 / (1000 * 60)));
            if (_local2 == 0){
                _local3 = Math.floor((_arg1 / 1000));
            } else {
                _local3 = Math.floor(((_arg1 % ((_local2 * 60) * 1000)) / 1000));
            };
            var _local4:Number = Math.max(Math.floor((_arg1 / 1000)), 1);
            if (_local4 == 0){
                _local5 = Math.floor(((_arg1 % _local4) / 10));
            } else {
                _local5 = Math.floor((_arg1 / 10));
            };
            var _local6:String = String(_local2);
            var _local7:String = String(_local3);
            var _local8:String = String(_local5);
            if (_local6.length < 2){
                _local6 = ("0" + _local6);
            };
            if (_local7.length < 2){
                _local7 = ("0" + _local7);
            };
            if (_local8.length > 2){
                _local8 = _local8.substr((_local8.length - 2));
            };
            return (((((_local6 + ":") + _local7) + ":") + _local8));
        }
        private function calculateTimeString(_arg1:Event):void{
            if (started){
                timeString = getTimeString((new Date().getTime() - startTime));
            };
        }
        private function _Stopwatch_bindingExprs():void{
            var _local1:*;
            _local1 = currentModeString;
            _local1 = timeString;
        }
        public function get answer():String{
            return (_answer);
        }
        public function set question(_arg1:Question):void{
            var _local2:Object = this.question;
            if (_local2 !== _arg1){
                this._1165870106question = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "question", _local2, _arg1));
            };
        }
        private function set _1165870106question(_arg1:Question):void{
            _question = _arg1;
        }
        public function ___Stopwatch_Canvas1_creationComplete(_arg1:FlexEvent):void{
            init();
        }
        public function set textAreaWidth(_arg1:Number):void{
            var _local2:Object = this._166533772textAreaWidth;
            if (_local2 !== _arg1){
                this._166533772textAreaWidth = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "textAreaWidth", _local2, _arg1));
            };
        }
        private function get timeString():String{
            return (this._793284926timeString);
        }
        public function get textArea():TextArea{
            return (this._1004197030textArea);
        }
        private function set _1412808770answer(_arg1:String):void{
            _answer = ((_arg1)==null) ? "" : _arg1;
        }
        public function set button(_arg1:Button):void{
            var _local2:Object = this._1377687758button;
            if (_local2 !== _arg1){
                this._1377687758button = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "button", _local2, _arg1));
            };
        }
        public function get question():Question{
            return (_question);
        }
        private function set currentModeString(_arg1:String):void{
            var _local2:Object = this._1941678611currentModeString;
            if (_local2 !== _arg1){
                this._1941678611currentModeString = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "currentModeString", _local2, _arg1));
            };
        }
        public function set answer(_arg1:String):void{
            var _local2:Object = this.answer;
            if (_local2 !== _arg1){
                this._1412808770answer = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "answer", _local2, _arg1));
            };
        }
        private function _Stopwatch_bindingsSetup():Array{
            var binding:* = null;
            var result:* = [];
            binding = new Binding(this, function ():String{
                var _local1:* = currentModeString;
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                button.label = _arg1;
            }, "button.label");
            result[0] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = timeString;
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                textArea.text = _arg1;
            }, "textArea.text");
            result[1] = binding;
            return (result);
        }

        public static function set watcherSetupUtil(_arg1:IWatcherSetupUtil):void{
            Stopwatch._watcherSetupUtil = _arg1;
        }

    }
}//package com.redbox.changetech.view.components.questions 
