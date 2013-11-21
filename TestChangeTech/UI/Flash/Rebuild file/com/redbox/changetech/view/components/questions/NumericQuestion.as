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

    public class NumericQuestion extends Canvas implements IBindingClient {

        public var _NumericQuestion_NumericStepper1:NumericStepper
        private var _question:Question
        mx_internal var _bindingsBeginWithWord:Object
        mx_internal var _bindingsByDestination:Object
        mx_internal var _watchers:Array
        private var _1598053662_labels:Array
        private var _answer:String
        mx_internal var _bindings:Array
        private var _documentDescriptor_:UIComponentDescriptor

        private static var _watcherSetupUtil:IWatcherSetupUtil;

        public function NumericQuestion(){
            _documentDescriptor_ = new UIComponentDescriptor({type:Canvas, propertiesFactory:function ():Object{
                return ({childDescriptors:[new UIComponentDescriptor({type:HBox, propertiesFactory:function ():Object{
                    return ({childDescriptors:[new UIComponentDescriptor({type:NumericStepper, id:"_NumericQuestion_NumericStepper1", events:{creationComplete:"___NumericQuestion_NumericStepper1_creationComplete", change:"___NumericQuestion_NumericStepper1_change"}, propertiesFactory:function ():Object{
                        return ({minimum:18, maximum:99});
                    }})]});
                }})]});
            }});
            _bindings = [];
            _watchers = [];
            _bindingsByDestination = {};
            _bindingsBeginWithWord = {};
            super();
            mx_internal::_document = this;
            this.addEventListener("creationComplete", ___NumericQuestion_Canvas1_creationComplete);
        }
        public function set answer(_arg1:String):void{
            var _local2:Object = this.answer;
            if (_local2 !== _arg1){
                this._1412808770answer = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "answer", _local2, _arg1));
            };
        }
        public function ___NumericQuestion_Canvas1_creationComplete(_arg1:FlexEvent):void{
            init();
        }
        private function _NumericQuestion_bindingsSetup():Array{
            var binding:* = null;
            var result:* = [];
            binding = new Binding(this, function ():Number{
                return (int(answer));
            }, function (_arg1:Number):void{
                _NumericQuestion_NumericStepper1.value = _arg1;
            }, "_NumericQuestion_NumericStepper1.value");
            result[0] = binding;
            return (result);
        }
        private function init():void{
        }
        private function _NumericQuestion_bindingExprs():void{
            var _local1:*;
            _local1 = int(answer);
        }
        private function set _1165870106question(_arg1:Question):void{
            var _local2:Number;
            _question = _arg1;
            _labels = new Array();
            if (_question.Options != null){
                _local2 = 0;
                while (_local2 < _question.Options.length) {
                    _labels.push(_question.Options[_local2].Label);
                    _local2++;
                };
            };
        }
        override public function initialize():void{
            var target:* = null;
            var watcherSetupUtilClass:* = null;
            mx_internal::setDocumentDescriptor(_documentDescriptor_);
            var bindings:* = _NumericQuestion_bindingsSetup();
            var watchers:* = [];
            target = this;
            if (_watcherSetupUtil == null){
                watcherSetupUtilClass = getDefinitionByName("_com_redbox_changetech_view_components_questions_NumericQuestionWatcherSetupUtil");
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
        private function get _labels():Array{
            return (this._1598053662_labels);
        }
        private function set _1412808770answer(_arg1:String):void{
            _answer = ((_arg1)==null) ? "21" : _arg1;
        }
        public function set question(_arg1:Question):void{
            var _local2:Object = this.question;
            if (_local2 !== _arg1){
                this._1165870106question = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "question", _local2, _arg1));
            };
        }
        public function get question():Question{
            return (_question);
        }
        public function ___NumericQuestion_NumericStepper1_change(_arg1:NumericStepperEvent):void{
            optionSelected(_arg1);
        }
        private function optionSelected(_arg1:Event):void{
            _arg1.stopPropagation();
            var _local2:AnswerSelectedEvent = new AnswerSelectedEvent(AnswerSelectedEvent.ANSWER_SELECTED, true, false);
            _local2.id = question.Id;
            var _local3:Number = NumericStepper(_arg1.target).value;
            _local2.text = String(_local3);
            dispatchEvent(_local2);
        }
        public function ___NumericQuestion_NumericStepper1_creationComplete(_arg1:FlexEvent):void{
            optionSelected(_arg1);
        }
        private function set _labels(_arg1:Array):void{
            var _local2:Object = this._1598053662_labels;
            if (_local2 !== _arg1){
                this._1598053662_labels = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "_labels", _local2, _arg1));
            };
        }
        public function get answer():String{
            return (_answer);
        }

        public static function set watcherSetupUtil(_arg1:IWatcherSetupUtil):void{
            NumericQuestion._watcherSetupUtil = _arg1;
        }

    }
}//package com.redbox.changetech.view.components.questions 
