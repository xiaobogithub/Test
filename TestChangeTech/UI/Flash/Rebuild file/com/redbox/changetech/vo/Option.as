//Created by Action Script Viewer - http://www.buraks.com/asv
package com.redbox.changetech.vo {
    import flash.events.*;
    import mx.events.*;
    import com.adobe.cairngorm.vo.*;

    public class Option implements IValueObject, IEventDispatcher {

        private var _bindingEventDispatcher:EventDispatcher
        private var _2273433Icon:String
        private var _2363Id:Number
        private var _79711858Score:Number
        private var _73174740Label:String
        private var _450051358ActionFlag:String
        private var _56677412Description:String

        public function Option(_arg1:Object=null){
            _bindingEventDispatcher = new EventDispatcher(IEventDispatcher(this));
            super();
        }
        public function dispatchEvent(_arg1:Event):Boolean{
            return (_bindingEventDispatcher.dispatchEvent(_arg1));
        }
        public function hasEventListener(_arg1:String):Boolean{
            return (_bindingEventDispatcher.hasEventListener(_arg1));
        }
        public function set ActionFlag(_arg1:String):void{
            var _local2:Object = this._450051358ActionFlag;
            if (_local2 !== _arg1){
                this._450051358ActionFlag = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "ActionFlag", _local2, _arg1));
            };
        }
        public function willTrigger(_arg1:String):Boolean{
            return (_bindingEventDispatcher.willTrigger(_arg1));
        }
        public function removeEventListener(_arg1:String, _arg2:Function, _arg3:Boolean=false):void{
            _bindingEventDispatcher.removeEventListener(_arg1, _arg2, _arg3);
        }
        public function addEventListener(_arg1:String, _arg2:Function, _arg3:Boolean=false, _arg4:int=0, _arg5:Boolean=false):void{
            _bindingEventDispatcher.addEventListener(_arg1, _arg2, _arg3, _arg4, _arg5);
        }
        public function get Label():String{
            return (this._73174740Label);
        }
        public function get Icon():String{
            return (this._2273433Icon);
        }
        public function get Description():String{
            return (this._56677412Description);
        }
        public function get ActionFlag():String{
            return (this._450051358ActionFlag);
        }
        public function set Score(_arg1:Number):void{
            var _local2:Object = this._79711858Score;
            if (_local2 !== _arg1){
                this._79711858Score = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "Score", _local2, _arg1));
            };
        }
        public function set Label(_arg1:String):void{
            var _local2:Object = this._73174740Label;
            if (_local2 !== _arg1){
                this._73174740Label = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "Label", _local2, _arg1));
            };
        }
        public function set Icon(_arg1:String):void{
            var _local2:Object = this._2273433Icon;
            if (_local2 !== _arg1){
                this._2273433Icon = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "Icon", _local2, _arg1));
            };
        }
        public function set Id(_arg1:Number):void{
            var _local2:Object = this._2363Id;
            if (_local2 !== _arg1){
                this._2363Id = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "Id", _local2, _arg1));
            };
        }
        public function set Description(_arg1:String):void{
            var _local2:Object = this._56677412Description;
            if (_local2 !== _arg1){
                this._56677412Description = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "Description", _local2, _arg1));
            };
        }
        public function get Score():Number{
            return (this._79711858Score);
        }
        public function get Id():Number{
            return (this._2363Id);
        }

    }
}//package com.redbox.changetech.vo 
