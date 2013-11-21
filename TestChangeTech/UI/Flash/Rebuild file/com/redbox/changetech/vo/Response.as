//Created by Action Script Viewer - http://www.buraks.com/asv
package com.redbox.changetech.vo {
    import flash.events.*;
    import mx.events.*;
    import com.adobe.cairngorm.vo.*;

    public class Response implements IValueObject, IEventDispatcher {

        private var _bindingEventDispatcher:EventDispatcher
        private var _2622298Type:String
        private var _817254485Answers:Array
        private var _1489060077NextType:String

        public function Response(){
            _bindingEventDispatcher = new EventDispatcher(IEventDispatcher(this));
            super();
        }
        public function dispatchEvent(_arg1:Event):Boolean{
            return (_bindingEventDispatcher.dispatchEvent(_arg1));
        }
        public function set NextType(_arg1:String):void{
            var _local2:Object = this._1489060077NextType;
            if (_local2 !== _arg1){
                this._1489060077NextType = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "NextType", _local2, _arg1));
            };
        }
        public function hasEventListener(_arg1:String):Boolean{
            return (_bindingEventDispatcher.hasEventListener(_arg1));
        }
        public function willTrigger(_arg1:String):Boolean{
            return (_bindingEventDispatcher.willTrigger(_arg1));
        }
        public function set Answers(_arg1:Array):void{
            var _local2:Object = this._817254485Answers;
            if (_local2 !== _arg1){
                this._817254485Answers = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "Answers", _local2, _arg1));
            };
        }
        public function get NextType():String{
            return (this._1489060077NextType);
        }
        public function removeEventListener(_arg1:String, _arg2:Function, _arg3:Boolean=false):void{
            _bindingEventDispatcher.removeEventListener(_arg1, _arg2, _arg3);
        }
        public function set Type(_arg1:String):void{
            var _local2:Object = this._2622298Type;
            if (_local2 !== _arg1){
                this._2622298Type = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "Type", _local2, _arg1));
            };
        }
        public function get Answers():Array{
            return (this._817254485Answers);
        }
        public function get Type():String{
            return (this._2622298Type);
        }
        public function addEventListener(_arg1:String, _arg2:Function, _arg3:Boolean=false, _arg4:int=0, _arg5:Boolean=false):void{
            _bindingEventDispatcher.addEventListener(_arg1, _arg2, _arg3, _arg4, _arg5);
        }

    }
}//package com.redbox.changetech.vo 
