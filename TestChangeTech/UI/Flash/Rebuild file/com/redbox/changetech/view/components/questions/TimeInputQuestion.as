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

    public class TimeInputQuestion extends Canvas {

        private var _1347780814minutes1:TextInput
        private var _question:Question
        private var _1347780813minutes2:TextInput
        private var _1211426045hours2:TextInput
        private var _166533772textAreaWidth:Number
        private var _1211426046hours1:TextInput
        private var _answer:String
        private var _documentDescriptor_:UIComponentDescriptor

        public function TimeInputQuestion(){
            _documentDescriptor_ = new UIComponentDescriptor({type:Canvas, propertiesFactory:function ():Object{
                return ({childDescriptors:[new UIComponentDescriptor({type:HBox, propertiesFactory:function ():Object{
                    return ({childDescriptors:[new UIComponentDescriptor({type:TextInput, id:"hours1", events:{change:"__hours1_change"}, stylesFactory:function ():void{
                        this.fontFamily = "Digital-7";
                        this.fontSize = 12;
                    }, propertiesFactory:function ():Object{
                        return ({restrict:"012", maxChars:1});
                    }}), new UIComponentDescriptor({type:TextInput, id:"hours2", events:{change:"__hours2_change"}, stylesFactory:function ():void{
                        this.fontFamily = "Digital-7";
                        this.fontSize = 12;
                    }, propertiesFactory:function ():Object{
                        return ({restrict:"0123456789", maxChars:1});
                    }}), new UIComponentDescriptor({type:Label, propertiesFactory:function ():Object{
                        return ({text:":"});
                    }}), new UIComponentDescriptor({type:TextInput, id:"minutes1", events:{change:"__minutes1_change"}, stylesFactory:function ():void{
                        this.fontFamily = "Digital-7";
                        this.fontSize = 12;
                    }, propertiesFactory:function ():Object{
                        return ({restrict:"0123456", maxChars:1});
                    }}), new UIComponentDescriptor({type:TextInput, id:"minutes2", events:{change:"__minutes2_change"}, stylesFactory:function ():void{
                        this.fontFamily = "Digital-7";
                        this.fontSize = 12;
                    }, propertiesFactory:function ():Object{
                        return ({restrict:"0123456789", maxChars:1});
                    }})]});
                }})]});
            }});
            super();
            mx_internal::_document = this;
        }
        public function get hours2():TextInput{
            return (this._1211426045hours2);
        }
        public function set hours2(_arg1:TextInput):void{
            var _local2:Object = this._1211426045hours2;
            if (_local2 !== _arg1){
                this._1211426045hours2 = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "hours2", _local2, _arg1));
            };
        }
        public function __hours2_change(_arg1:Event):void{
            optionSelected(_arg1);
        }
        override public function initialize():void{
            mx_internal::setDocumentDescriptor(_documentDescriptor_);
            super.initialize();
        }
        public function get hours1():TextInput{
            return (this._1211426046hours1);
        }
        public function set hours1(_arg1:TextInput):void{
            var _local2:Object = this._1211426046hours1;
            if (_local2 !== _arg1){
                this._1211426046hours1 = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "hours1", _local2, _arg1));
            };
        }
        private function set _1165870106question(_arg1:Question):void{
            _question = _arg1;
        }
        public function get minutes2():TextInput{
            return (this._1347780813minutes2);
        }
        public function set textAreaWidth(_arg1:Number):void{
            var _local2:Object = this._166533772textAreaWidth;
            if (_local2 !== _arg1){
                this._166533772textAreaWidth = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "textAreaWidth", _local2, _arg1));
            };
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
        private function set _1412808770answer(_arg1:String):void{
            _answer = ((_arg1)==null) ? "" : _arg1;
        }
        public function get question():Question{
            return (_question);
        }
        public function __minutes2_change(_arg1:Event):void{
            optionSelected(_arg1);
        }
        public function __hours1_change(_arg1:Event):void{
            optionSelected(_arg1);
        }
        public function get textAreaWidth():Number{
            return (this._166533772textAreaWidth);
        }
        private function optionSelected(_arg1:Event):void{
            var _local2:AnswerSelectedEvent = new AnswerSelectedEvent(AnswerSelectedEvent.ANSWER_SELECTED, true, false);
            _local2.id = question.Id;
            _local2.text = ((((hours1.text + hours2.text) + ":") + minutes1.text) + minutes2.text);
            dispatchEvent(_local2);
        }
        public function get minutes1():TextInput{
            return (this._1347780814minutes1);
        }
        public function set minutes2(_arg1:TextInput):void{
            var _local2:Object = this._1347780813minutes2;
            if (_local2 !== _arg1){
                this._1347780813minutes2 = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "minutes2", _local2, _arg1));
            };
        }
        public function set minutes1(_arg1:TextInput):void{
            var _local2:Object = this._1347780814minutes1;
            if (_local2 !== _arg1){
                this._1347780814minutes1 = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "minutes1", _local2, _arg1));
            };
        }
        public function __minutes1_change(_arg1:Event):void{
            optionSelected(_arg1);
        }
        public function get answer():String{
            return (_answer);
        }

    }
}//package com.redbox.changetech.view.components.questions 
