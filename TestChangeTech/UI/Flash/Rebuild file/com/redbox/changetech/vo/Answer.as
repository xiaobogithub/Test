//Created by Action Script Viewer - http://www.buraks.com/asv
package com.redbox.changetech.vo {
    import flash.events.*;
    import mx.events.*;
    import com.adobe.cairngorm.vo.*;

    public class Answer implements IValueObject, IEventDispatcher {

        private var _bindingEventDispatcher:EventDispatcher
        private var _2603341Text:String
        private var _14373744OptionId:Number
        private var _1716207679QuestionId:int
        private var _79711858Score:Number

        public function Answer(){
            _bindingEventDispatcher = new EventDispatcher(IEventDispatcher(this));
            super();
        }
        public function dispatchEvent(_arg1:Event):Boolean{
            return (_bindingEventDispatcher.dispatchEvent(_arg1));
        }
        public function get QuestionId():int{
            return (this._1716207679QuestionId);
        }
        public function set QuestionId(_arg1:int):void{
            var _local2:Object = this._1716207679QuestionId;
            if (_local2 !== _arg1){
                this._1716207679QuestionId = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "QuestionId", _local2, _arg1));
            };
        }
        public function willTrigger(_arg1:String):Boolean{
            return (_bindingEventDispatcher.willTrigger(_arg1));
        }
        public function addEventListener(_arg1:String, _arg2:Function, _arg3:Boolean=false, _arg4:int=0, _arg5:Boolean=false):void{
            _bindingEventDispatcher.addEventListener(_arg1, _arg2, _arg3, _arg4, _arg5);
        }
        public function get OptionId():Number{
            return (this._14373744OptionId);
        }
        public function removeEventListener(_arg1:String, _arg2:Function, _arg3:Boolean=false):void{
            _bindingEventDispatcher.removeEventListener(_arg1, _arg2, _arg3);
        }
        public function set Text(_arg1:String):void{
            var _local2:Object = this._2603341Text;
            if (_local2 !== _arg1){
                this._2603341Text = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "Text", _local2, _arg1));
            };
        }
        public function get Text():String{
            return (this._2603341Text);
        }
        public function set OptionId(_arg1:Number):void{
            var _local2:Object = this._14373744OptionId;
            if (_local2 !== _arg1){
                this._14373744OptionId = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "OptionId", _local2, _arg1));
            };
        }
        public function set Score(_arg1:Number):void{
            var _local2:Object = this._79711858Score;
            if (_local2 !== _arg1){
                this._79711858Score = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "Score", _local2, _arg1));
            };
        }
        public function get Score():Number{
            return (this._79711858Score);
        }
        public function hasEventListener(_arg1:String):Boolean{
            return (_bindingEventDispatcher.hasEventListener(_arg1));
        }

    }
}//package com.redbox.changetech.vo 
