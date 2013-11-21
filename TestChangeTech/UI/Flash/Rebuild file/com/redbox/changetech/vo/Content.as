//Created by Action Script Viewer - http://www.buraks.com/asv
package com.redbox.changetech.vo {
    import flash.events.*;
    import mx.events.*;
    import mx.controls.*;
    import com.adobe.cairngorm.vo.*;

    public class Content implements IValueObject, IEventDispatcher {

        private var _80818744Title:String
        private var _2363Id:Number
        private var _15188893CollectionFeedback:Array
        private var _979075851ContentMedia:Array
        private var _1443553092PresenterImageUrl:String
        private var _bindingEventDispatcher:EventDispatcher
        private var _2105869Code:String = "NOT SET"
        private var _1906005857Buttons:Array
        private var _2006763084IdMessage:Number
        private var _1256902502Template:Object
        private var _83834Tag:String = null
        private var _221733165Questions:Array
        private var _1062605528Conditions:ConditionCollection
        private var _667372841TextLayout:XML

        public function Content(_arg1:Object=null){
            _bindingEventDispatcher = new EventDispatcher(IEventDispatcher(this));
            super();
        }
        private function getMediaByID(_arg1:Number):Media{
            var _local3:Media;
            var _local2:Number = 0;
            while (_local2 < ContentMedia.length) {
                _local3 = ContentMedia[_local2];
                if (_local3.Id == _arg1){
                    return (_local3);
                };
                _local2++;
            };
            Alert.show((("Error: media id " + String(_arg1)) + " in markup but not in array"));
            return (null);
        }
        public function get Tag():String{
            return (this._83834Tag);
        }
        public function willTrigger(_arg1:String):Boolean{
            return (_bindingEventDispatcher.willTrigger(_arg1));
        }
        public function get ContentMedia():Array{
            return (this._979075851ContentMedia);
        }
        public function get Code():String{
            return (this._2105869Code);
        }
        public function getSecondaryButton():Button{
            return (getButtonByType(Button.SECONDARY));
        }
        private function removeBreakTags(_arg1:XML):XML{
            var _local2:String = _arg1.toString();
            var _local3:Array = _local2.split("<br/>");
            var _local4 = "";
            var _local5:Number = 0;
            while (_local5 < _local3.length) {
                _local4 = (_local4 + _local3[_local5]);
                _local5++;
            };
            return (new XML(_local4));
        }
        public function set ContentMedia(_arg1:Array):void{
            var _local2:Object = this._979075851ContentMedia;
            if (_local2 !== _arg1){
                this._979075851ContentMedia = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "ContentMedia", _local2, _arg1));
            };
        }
        public function getTextLayoutAsOneString():String{
            var _local2:XML;
            var _local1 = "";
            for each (_local2 in TextLayout.p) {
                if (_local2.hasSimpleContent()){
                    if (String(_local2.valueOf()).length > 0){
                        _local1 = (_local1 + (String(_local2.valueOf()) + "\r"));
                    };
                };
            };
            return (_local1);
        }
        public function getTertiaryButton():Button{
            return (getButtonByType(Button.TERTIARY));
        }
        public function getLayout():Array{
            var _local2:XML;
            var _local3:XML;
            var _local4:Object;
            var _local1:Array = [];
            for each (_local2 in TextLayout.p) {
                for each (_local3 in _local2.children()) {
                    if (_local3.name() == "button"){
                        _local1.push(getButtonByID(Number(_local3.@id)));
                    } else {
                        if (_local3.name() == "question"){
                            _local1.push(getQuestionByID(Number(_local3.@id)));
                        } else {
                            if (_local3.name() == "media"){
                                _local1.push(getMediaByID(Number(_local3.@id)));
                            } else {
                                if (_local3.name() == "picture"){
                                    _local4 = new Object();
                                    _local4.Id = _local2.picture.@id;
                                    _local4.source = _local2.picture;
                                    _local4.type = "Picture";
                                    _local1.push(_local4);
                                } else {
                                    _local1.push(String(_local3.valueOf()));
                                };
                            };
                        };
                    };
                };
            };
            return (_local1);
        }
        public function get Template():Object{
            return (this._1256902502Template);
        }
        public function set Code(_arg1:String):void{
            var _local2:Object = this._2105869Code;
            if (_local2 !== _arg1){
                this._2105869Code = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "Code", _local2, _arg1));
            };
        }
        public function getCTAButton():Button{
            return (getButtonByType(Button.PRIMARY));
        }
        public function get Questions():Array{
            return (this._221733165Questions);
        }
        public function set TextLayout(_arg1:XML):void{
            var _local2:Object = this._667372841TextLayout;
            if (_local2 !== _arg1){
                this._667372841TextLayout = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "TextLayout", _local2, _arg1));
            };
        }
        public function set Conditions(_arg1:ConditionCollection):void{
            var _local2:Object = this._1062605528Conditions;
            if (_local2 !== _arg1){
                this._1062605528Conditions = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "Conditions", _local2, _arg1));
            };
        }
        public function get Title():String{
            return (this._80818744Title);
        }
        public function set IdMessage(_arg1:Number):void{
            var _local2:Object = this._2006763084IdMessage;
            if (_local2 !== _arg1){
                this._2006763084IdMessage = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "IdMessage", _local2, _arg1));
            };
        }
        public function get Id():Number{
            return (this._2363Id);
        }
        public function get PresenterImageUrl():String{
            return (this._1443553092PresenterImageUrl);
        }
        private function getQuestionByID(_arg1:Number):Question{
            var _local3:Question;
            var _local2:Number = 0;
            while (_local2 < Questions.length) {
                _local3 = Questions[_local2];
                if (_local3.Id == _arg1){
                    return (_local3);
                };
                _local2++;
            };
            Alert.show((("Error: question id " + String(_arg1)) + " in markup but not in array"));
            return (null);
        }
        public function dispatchEvent(_arg1:Event):Boolean{
            return (_bindingEventDispatcher.dispatchEvent(_arg1));
        }
        public function set Buttons(_arg1:Array):void{
            var _local2:Object = this._1906005857Buttons;
            if (_local2 !== _arg1){
                this._1906005857Buttons = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "Buttons", _local2, _arg1));
            };
        }
        public function set Template(_arg1:Object):void{
            var _local2:Object = this._1256902502Template;
            if (_local2 !== _arg1){
                this._1256902502Template = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "Template", _local2, _arg1));
            };
        }
        public function set CollectionFeedback(_arg1:Array):void{
            var _local2:Object = this._15188893CollectionFeedback;
            if (_local2 !== _arg1){
                this._15188893CollectionFeedback = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "CollectionFeedback", _local2, _arg1));
            };
        }
        public function addEventListener(_arg1:String, _arg2:Function, _arg3:Boolean=false, _arg4:int=0, _arg5:Boolean=false):void{
            _bindingEventDispatcher.addEventListener(_arg1, _arg2, _arg3, _arg4, _arg5);
        }
        public function get IdMessage():Number{
            return (this._2006763084IdMessage);
        }
        public function get Conditions():ConditionCollection{
            return (this._1062605528Conditions);
        }
        public function removeEventListener(_arg1:String, _arg2:Function, _arg3:Boolean=false):void{
            _bindingEventDispatcher.removeEventListener(_arg1, _arg2, _arg3);
        }
        public function set Questions(_arg1:Array):void{
            var _local2:Object = this._221733165Questions;
            if (_local2 !== _arg1){
                this._221733165Questions = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "Questions", _local2, _arg1));
            };
        }
        public function get TextLayout():XML{
            return (this._667372841TextLayout);
        }
        public function get CollectionFeedback():Array{
            return (this._15188893CollectionFeedback);
        }
        public function get Buttons():Array{
            return (this._1906005857Buttons);
        }
        public function set Title(_arg1:String):void{
            var _local2:Object = this._80818744Title;
            if (_local2 !== _arg1){
                this._80818744Title = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "Title", _local2, _arg1));
            };
        }
        public function set Tag(_arg1:String):void{
            var _local2:Object = this._83834Tag;
            if (_local2 !== _arg1){
                this._83834Tag = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "Tag", _local2, _arg1));
            };
        }
        public function set Id(_arg1:Number):void{
            var _local2:Object = this._2363Id;
            if (_local2 !== _arg1){
                this._2363Id = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "Id", _local2, _arg1));
            };
        }
        public function set PresenterImageUrl(_arg1:String):void{
            var _local2:Object = this._1443553092PresenterImageUrl;
            if (_local2 !== _arg1){
                this._1443553092PresenterImageUrl = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "PresenterImageUrl", _local2, _arg1));
            };
        }
        private function getButtonByType(_arg1:String):Button{
            var _local3:Button;
            if (Buttons == null){
                return (null);
            };
            var _local2:Number = 0;
            while (_local2 < Buttons.length) {
                _local3 = Buttons[_local2];
                if (_local3.Type == _arg1){
                    return (_local3);
                };
                _local2++;
            };
            return (null);
        }
        private function getButtonByID(_arg1:Number):Button{
            var _local3:Button;
            if (Button == null){
                Alert.show("No buttons in array.");
            };
            var _local2:Number = 0;
            while (_local2 < Buttons.length) {
                _local3 = Buttons[_local2];
                if (_local3.IdButton == _arg1){
                    return (_local3);
                };
                _local2++;
            };
            Alert.show((("Error: button id " + String(_arg1)) + " in markup but not in array"));
            return (null);
        }
        public function hasEventListener(_arg1:String):Boolean{
            return (_bindingEventDispatcher.hasEventListener(_arg1));
        }

    }
}//package com.redbox.changetech.vo 
