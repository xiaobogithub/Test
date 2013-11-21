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
    import com.redbox.changetech.view.components.*;
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

    public class EditableList extends Canvas {

        private var _question:Question
        private var listHolderChildren:Array
        private var _answer:String
        private var model:BalanceModelLocator
        private var _45284330listHolder:VBox
        private var listElements:Array
        private var _documentDescriptor_:UIComponentDescriptor

        private static var DEFAULT_LIST_ELEMENT_STRING:String = "default_list_element_string";
        private static var listString:String = "list element 1|list element 2|list element 3|list element 4|list element 5|list element 6";

        public function EditableList(){
            _documentDescriptor_ = new UIComponentDescriptor({type:Canvas, propertiesFactory:function ():Object{
                return ({childDescriptors:[new UIComponentDescriptor({type:VBox, id:"listHolder"})]});
            }});
            model = BalanceModelLocator.getInstance();
            listHolderChildren = [];
            super();
            mx_internal::_document = this;
            this.addEventListener("creationComplete", ___EditableList_Canvas1_creationComplete);
        }
        public function set answer(_arg1:String):void{
            var _local2:Object = this.answer;
            if (_local2 !== _arg1){
                this._1412808770answer = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "answer", _local2, _arg1));
            };
        }
        private function deleteAlistElement(_arg1:Event):void{
            var _local2:EditableListElement = EditableListElement(_arg1.currentTarget.parent);
            var _local3:Number = 0;
            while (_local3 < listElements.length) {
                if (_local2.listElementObject == listElements[_local3]){
                    listElements.splice(_local3, 1);
                };
                _local3++;
            };
            render();
            optionSelected(new Event(Event.CHANGE));
        }
        public function set question(_arg1:Question):void{
            var _local2:Object = this.question;
            if (_local2 !== _arg1){
                this._1165870106question = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "question", _local2, _arg1));
            };
        }
        public function get answer():String{
            return (_answer);
        }
        private function reset():void{
            var _local1:Number = 0;
            while (_local1 < listHolderChildren.length) {
                listHolder.removeChild(listHolderChildren[_local1]);
                _local1++;
            };
            listHolderChildren = [];
        }
        override public function initialize():void{
            mx_internal::setDocumentDescriptor(_documentDescriptor_);
            super.initialize();
        }
        private function set _1165870106question(_arg1:Question):void{
            _question = _arg1;
        }
        private function render():void{
            var _local3:EditableListElement;
            reset();
            trace(("listElements=" + listElements));
            var _local1:Number = 0;
            while (_local1 < listElements.length) {
                _local3 = new EditableListElement();
                trace(("editableListElement=" + _local3));
                listHolder.addChild(_local3);
                _local3.close_button.addEventListener(MouseEvent.CLICK, deleteAlistElement);
                _local3.listElementObject = listElements[_local1];
                _local3.addEventListener(Event.CHANGE, optionSelected);
                listHolderChildren.push(_local3);
                _local1++;
            };
            var _local2:Button = new Button();
            _local2.label = model.languageVO.getLang("add");
            listHolder.addChild(_local2);
            listHolderChildren.push(_local2);
            _local2.x = ((listHolder.width - _local2.width) - 3);
            _local2.addEventListener(MouseEvent.CLICK, addAlistElement);
        }
        public function set listHolder(_arg1:VBox):void{
            var _local2:Object = this._45284330listHolder;
            if (_local2 !== _arg1){
                this._45284330listHolder = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "listHolder", _local2, _arg1));
            };
        }
        private function init():void{
            var _local3:Object;
            var _local1:Array = question.PreviousAnswer.split("|");
            listElements = [];
            trace(("listArray=" + _local1));
            var _local2:Number = 0;
            while (_local2 < _local1.length) {
                _local3 = new Object();
                _local3.text = _local1[_local2];
                listElements.push(_local3);
                _local2++;
            };
            trace(("listElements=" + listElements));
            render();
        }
        private function set _1412808770answer(_arg1:String):void{
            _answer = ((_arg1)==null) ? "" : _arg1;
        }
        public function get question():Question{
            return (_question);
        }
        private function optionSelected(_arg1:Event):void{
            trace(("optionSelected:" + _arg1.currentTarget));
            var _local2:AnswerSelectedEvent = new AnswerSelectedEvent(AnswerSelectedEvent.ANSWER_SELECTED, true, false);
            _local2.id = question.Id;
            _local2.text = getPipedString();
            dispatchEvent(_local2);
        }
        private function getPipedString():String{
            var _local1 = "";
            var _local2:* = 0;
            while (_local2 < listElements.length) {
                if (_local2 == (listElements.length - 1)){
                    _local1 = (_local1 + listElements[_local2].text);
                } else {
                    _local1 = (_local1 + (listElements[_local2].text + "|"));
                };
                _local2++;
            };
            return (_local1);
        }
        public function get listHolder():VBox{
            return (this._45284330listHolder);
        }
        public function ___EditableList_Canvas1_creationComplete(_arg1:FlexEvent):void{
            init();
        }
        private function addAlistElement(_arg1:Event):void{
            var _local2:Object = new Object();
            _local2.text = model.languageVO.getLang(DEFAULT_LIST_ELEMENT_STRING);
            listElements.push(_local2);
            render();
            optionSelected(new Event(Event.CHANGE));
        }

    }
}//package com.redbox.changetech.view.components.questions 
