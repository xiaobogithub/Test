//Created by Action Script Viewer - http://www.buraks.com/asv
package com.redbox.changetech.vo {
    import flash.events.*;
    import mx.events.*;
    import com.adobe.cairngorm.vo.*;

    public class Question implements IValueObject, IEventDispatcher {

        private var _803039614ReadOnly:Boolean
        private var _2363Id:int
        private var _bindingEventDispatcher:EventDispatcher
        private var _1535550027PreviousAnswer:String
        private var _73174740Label:String
        private var _415178366Options:Array
        private var _1443553092PresenterImageUrl:String
        private var _1590918055AllowMultiple:Boolean
        private var _809145774isEditable:Boolean
        private var _1955883606Action:String
        private var _2622298Type:String
        private var _1611057593Mandatory:Boolean = false

        public function Question(_arg1:Object=null){
            var _local2:Number;
            _bindingEventDispatcher = new EventDispatcher(IEventDispatcher(this));
            super();
            if (_arg1 != null){
                this.Id = _arg1.Id;
                this.Type = _arg1.Type;
                this.Label = _arg1.Label;
                this.PresenterImageUrl = _arg1.PresenterImageUrl;
                this.Mandatory = _arg1.Mandatory;
                this.Options = [];
                if (_arg1.Options != null){
                    _local2 = 0;
                    while (_local2 < _arg1.Options.length) {
                        this.Options.push(new Option(_arg1.Options[_local2]));
                        _local2++;
                    };
                };
            };
        }
        public function set ReadOnly(_arg1:Boolean):void{
            var _local2:Object = this._803039614ReadOnly;
            if (_local2 !== _arg1){
                this._803039614ReadOnly = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "ReadOnly", _local2, _arg1));
            };
        }
        public function willTrigger(_arg1:String):Boolean{
            return (_bindingEventDispatcher.willTrigger(_arg1));
        }
        public function get ReadOnly():Boolean{
            return (this._803039614ReadOnly);
        }
        public function set Label(_arg1:String):void{
            var _local2:Object = this._73174740Label;
            if (_local2 !== _arg1){
                this._73174740Label = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "Label", _local2, _arg1));
            };
        }
        public function set Action(_arg1:String):void{
            var _local2:Object = this._1955883606Action;
            if (_local2 !== _arg1){
                this._1955883606Action = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "Action", _local2, _arg1));
            };
        }
        public function get isEditable():Boolean{
            return (this._809145774isEditable);
        }
        public function set AllowMultiple(_arg1:Boolean):void{
            var _local2:Object = this._1590918055AllowMultiple;
            if (_local2 !== _arg1){
                this._1590918055AllowMultiple = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "AllowMultiple", _local2, _arg1));
            };
        }
        public function set Type(_arg1:String):void{
            var _local2:Object = this._2622298Type;
            if (_local2 !== _arg1){
                this._2622298Type = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "Type", _local2, _arg1));
            };
        }
        public function get PreviousAnswer():String{
            return (this._1535550027PreviousAnswer);
        }
        public function get Mandatory():Boolean{
            return (this._1611057593Mandatory);
        }
        public function get PresenterImageUrl():String{
            return (this._1443553092PresenterImageUrl);
        }
        public function dispatchEvent(_arg1:Event):Boolean{
            return (_bindingEventDispatcher.dispatchEvent(_arg1));
        }
        public function get Id():int{
            return (this._2363Id);
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
        public function get Action():String{
            return (this._1955883606Action);
        }
        public function get AllowMultiple():Boolean{
            return (this._1590918055AllowMultiple);
        }
        public function get Type():String{
            return (this._2622298Type);
        }
        public function set isEditable(_arg1:Boolean):void{
            var _local2:Object = this._809145774isEditable;
            if (_local2 !== _arg1){
                this._809145774isEditable = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "isEditable", _local2, _arg1));
            };
        }
        public function set PreviousAnswer(_arg1:String):void{
            var _local2:Object = this._1535550027PreviousAnswer;
            if (_local2 !== _arg1){
                this._1535550027PreviousAnswer = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "PreviousAnswer", _local2, _arg1));
            };
        }
        public function set Mandatory(_arg1:Boolean):void{
            var _local2:Object = this._1611057593Mandatory;
            if (_local2 !== _arg1){
                this._1611057593Mandatory = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "Mandatory", _local2, _arg1));
            };
        }
        public function hasEventListener(_arg1:String):Boolean{
            return (_bindingEventDispatcher.hasEventListener(_arg1));
        }
        public function set Id(_arg1:int):void{
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
        public function set Options(_arg1:Array):void{
            var _local2:Object = this._415178366Options;
            if (_local2 !== _arg1){
                this._415178366Options = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "Options", _local2, _arg1));
            };
        }
        public function get Options():Array{
            return (this._415178366Options);
        }

    }
}//package com.redbox.changetech.vo 
