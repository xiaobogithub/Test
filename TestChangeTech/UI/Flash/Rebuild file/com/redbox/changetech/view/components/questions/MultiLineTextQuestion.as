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

    public class MultiLineTextQuestion extends Canvas implements IBindingClient {

        private var _question:Question
        private var _393139297required:Label
        mx_internal var _watchers:Array
        mx_internal var _bindingsByDestination:Object
        private var _166533772textAreaWidth:Number
        private var _answer:String
        private var _1363257168inputField:TextArea
        mx_internal var _bindingsBeginWithWord:Object
        mx_internal var _bindings:Array
        private var _434584865textAreaHeight:Number
        private var _documentDescriptor_:UIComponentDescriptor

        private static var _watcherSetupUtil:IWatcherSetupUtil;

        public function MultiLineTextQuestion(){
            _documentDescriptor_ = new UIComponentDescriptor({type:Canvas, propertiesFactory:function ():Object{
                return ({childDescriptors:[new UIComponentDescriptor({type:VBox, propertiesFactory:function ():Object{
                    return ({percentWidth:100, childDescriptors:[new UIComponentDescriptor({type:TextArea, id:"inputField", events:{focusOut:"__inputField_focusOut", change:"__inputField_change"}, propertiesFactory:function ():Object{
                        return ({editable:true, percentWidth:100, height:50, styleName:"textAreaInput"});
                    }}), new UIComponentDescriptor({type:Label, id:"required", propertiesFactory:function ():Object{
                        return ({text:"Required Field", visible:false});
                    }})]});
                }})]});
            }});
            _bindings = [];
            _watchers = [];
            _bindingsByDestination = {};
            _bindingsBeginWithWord = {};
            super();
            mx_internal::_document = this;
        }
        public function set textAreaHeight(_arg1:Number):void{
            var _local2:Object = this._434584865textAreaHeight;
            if (_local2 !== _arg1){
                this._434584865textAreaHeight = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "textAreaHeight", _local2, _arg1));
            };
        }
        public function get textAreaHeight():Number{
            return (this._434584865textAreaHeight);
        }
        public function __inputField_focusOut(_arg1:FocusEvent):void{
            optionSelected(_arg1);
        }
        public function set required(_arg1:Label):void{
            var _local2:Object = this._393139297required;
            if (_local2 !== _arg1){
                this._393139297required = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "required", _local2, _arg1));
            };
        }
        override public function initialize():void{
            var target:* = null;
            var watcherSetupUtilClass:* = null;
            mx_internal::setDocumentDescriptor(_documentDescriptor_);
            var bindings:* = _MultiLineTextQuestion_bindingsSetup();
            var watchers:* = [];
            target = this;
            if (_watcherSetupUtil == null){
                watcherSetupUtilClass = getDefinitionByName("_com_redbox_changetech_view_components_questions_MultiLineTextQuestionWatcherSetupUtil");
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
        private function _MultiLineTextQuestion_bindingsSetup():Array{
            var binding:* = null;
            var result:* = [];
            binding = new Binding(this, function ():String{
                var _local1:* = answer;
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                inputField.text = _arg1;
            }, "inputField.text");
            result[0] = binding;
            return (result);
        }
        public function get textAreaWidth():Number{
            return (this._166533772textAreaWidth);
        }
        private function set _1165870106question(_arg1:Question):void{
            _question = _arg1;
        }
        private function _MultiLineTextQuestion_bindingExprs():void{
            var _local1:*;
            _local1 = answer;
        }
        public function __inputField_change(_arg1:Event):void{
            optionSelected(_arg1);
        }
        public function set inputField(_arg1:TextArea):void{
            var _local2:Object = this._1363257168inputField;
            if (_local2 !== _arg1){
                this._1363257168inputField = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "inputField", _local2, _arg1));
            };
        }
        private function set _1412808770answer(_arg1:String):void{
            _answer = ((_arg1)==null) ? "" : _arg1;
        }
        public function get question():Question{
            return (_question);
        }
        public function set textAreaWidth(_arg1:Number):void{
            var _local2:Object = this._166533772textAreaWidth;
            if (_local2 !== _arg1){
                this._166533772textAreaWidth = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "textAreaWidth", _local2, _arg1));
            };
        }
        public function get inputField():TextArea{
            return (this._1363257168inputField);
        }
        private function optionSelected(_arg1:Event):void{
            var _local2:AnswerSelectedEvent = new AnswerSelectedEvent(AnswerSelectedEvent.ANSWER_SELECTED, true, false);
            _local2.id = question.Id;
            _local2.text = inputField.text;
            if (((question.Mandatory) && ((inputField.text == "")))){
                _local2.text = null;
                required.visible = true;
            } else {
                required.visible = false;
            };
            dispatchEvent(_local2);
        }
        public function get required():Label{
            return (this._393139297required);
        }
        public function set question(_arg1:Question):void{
            var _local2:Object = this.question;
            if (_local2 !== _arg1){
                this._1165870106question = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "question", _local2, _arg1));
            };
        }
        public function set answer(_arg1:String):void{
            var _local2:Object = this.answer;
            if (_local2 !== _arg1){
                this._1412808770answer = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "answer", _local2, _arg1));
            };
        }
        public function get answer():String{
            return (_answer);
        }

        public static function set watcherSetupUtil(_arg1:IWatcherSetupUtil):void{
            MultiLineTextQuestion._watcherSetupUtil = _arg1;
        }

    }
}//package com.redbox.changetech.view.components.questions 
