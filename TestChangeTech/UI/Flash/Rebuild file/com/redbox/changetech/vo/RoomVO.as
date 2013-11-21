//Created by Action Script Viewer - http://www.buraks.com/asv
package com.redbox.changetech.vo {
    import flash.events.*;
    import mx.events.*;
    import com.adobe.cairngorm.vo.*;

    public class RoomVO implements IValueObject, IEventDispatcher {

        private var _455609243boxColour2:Number
        private var _109613561textColour2:Number
        private var _455609242boxColour1:Number
        private var _1053175053badgeAsset:Class
        private var _865657638buttonGradColour2:Number
        private var _109613560textColour1:Number
        private var _865657637buttonGradColour1:Number
        private var _173503994roomName:String
        private var _1584243778demoPopUpCopy:String
        private var _bindingEventDispatcher:EventDispatcher

        public function RoomVO(){
            _bindingEventDispatcher = new EventDispatcher(IEventDispatcher(this));
            super();
        }
        public function hasEventListener(_arg1:String):Boolean{
            return (_bindingEventDispatcher.hasEventListener(_arg1));
        }
        public function set buttonGradColour1(_arg1:Number):void{
            var _local2:Object = this._865657637buttonGradColour1;
            if (_local2 !== _arg1){
                this._865657637buttonGradColour1 = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "buttonGradColour1", _local2, _arg1));
            };
        }
        public function get roomName():String{
            return (this._173503994roomName);
        }
        public function dispatchEvent(_arg1:Event):Boolean{
            return (_bindingEventDispatcher.dispatchEvent(_arg1));
        }
        public function removeEventListener(_arg1:String, _arg2:Function, _arg3:Boolean=false):void{
            _bindingEventDispatcher.removeEventListener(_arg1, _arg2, _arg3);
        }
        public function set textColour1(_arg1:Number):void{
            var _local2:Object = this._109613560textColour1;
            if (_local2 !== _arg1){
                this._109613560textColour1 = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "textColour1", _local2, _arg1));
            };
        }
        public function set buttonGradColour2(_arg1:Number):void{
            var _local2:Object = this._865657638buttonGradColour2;
            if (_local2 !== _arg1){
                this._865657638buttonGradColour2 = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "buttonGradColour2", _local2, _arg1));
            };
        }
        public function get badgeAsset():Class{
            return (this._1053175053badgeAsset);
        }
        public function willTrigger(_arg1:String):Boolean{
            return (_bindingEventDispatcher.willTrigger(_arg1));
        }
        public function get boxColour1():Number{
            return (this._455609242boxColour1);
        }
        public function addEventListener(_arg1:String, _arg2:Function, _arg3:Boolean=false, _arg4:int=0, _arg5:Boolean=false):void{
            _bindingEventDispatcher.addEventListener(_arg1, _arg2, _arg3, _arg4, _arg5);
        }
        public function set badgeAsset(_arg1:Class):void{
            var _local2:Object = this._1053175053badgeAsset;
            if (_local2 !== _arg1){
                this._1053175053badgeAsset = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "badgeAsset", _local2, _arg1));
            };
        }
        public function get boxColour2():Number{
            return (this._455609243boxColour2);
        }
        public function set roomName(_arg1:String):void{
            var _local2:Object = this._173503994roomName;
            if (_local2 !== _arg1){
                this._173503994roomName = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "roomName", _local2, _arg1));
            };
        }
        public function get buttonGradColour1():Number{
            return (this._865657637buttonGradColour1);
        }
        public function get buttonGradColour2():Number{
            return (this._865657638buttonGradColour2);
        }
        public function set textColour2(_arg1:Number):void{
            var _local2:Object = this._109613561textColour2;
            if (_local2 !== _arg1){
                this._109613561textColour2 = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "textColour2", _local2, _arg1));
            };
        }
        public function set boxColour1(_arg1:Number):void{
            var _local2:Object = this._455609242boxColour1;
            if (_local2 !== _arg1){
                this._455609242boxColour1 = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "boxColour1", _local2, _arg1));
            };
        }
        public function set boxColour2(_arg1:Number):void{
            var _local2:Object = this._455609243boxColour2;
            if (_local2 !== _arg1){
                this._455609243boxColour2 = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "boxColour2", _local2, _arg1));
            };
        }
        public function get textColour2():Number{
            return (this._109613561textColour2);
        }
        public function set demoPopUpCopy(_arg1:String):void{
            var _local2:Object = this._1584243778demoPopUpCopy;
            if (_local2 !== _arg1){
                this._1584243778demoPopUpCopy = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "demoPopUpCopy", _local2, _arg1));
            };
        }
        public function get demoPopUpCopy():String{
            return (this._1584243778demoPopUpCopy);
        }
        public function get textColour1():Number{
            return (this._109613560textColour1);
        }

    }
}//package com.redbox.changetech.vo 
