//Created by Action Script Viewer - http://www.buraks.com/asv
package com.redbox.changetech.vo {
    import flash.events.*;
    import mx.events.*;
    import com.adobe.cairngorm.vo.*;

    public class ConsumptionValue implements IValueObject, IEventDispatcher {

        private var _2622298Type:String
        private var _82420049Value:int
        private var _bindingEventDispatcher:EventDispatcher

        public function ConsumptionValue(_arg1:String=null, _arg2:int=0){
            _bindingEventDispatcher = new EventDispatcher(IEventDispatcher(this));
            super();
            this.Type = _arg1;
            this.Value = _arg2;
        }
        public function dispatchEvent(_arg1:Event):Boolean{
            return (_bindingEventDispatcher.dispatchEvent(_arg1));
        }
        public function set Value(_arg1:int):void{
            var _local2:Object = this._82420049Value;
            if (_local2 !== _arg1){
                this._82420049Value = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "Value", _local2, _arg1));
            };
        }
        public function hasEventListener(_arg1:String):Boolean{
            return (_bindingEventDispatcher.hasEventListener(_arg1));
        }
        public function willTrigger(_arg1:String):Boolean{
            return (_bindingEventDispatcher.willTrigger(_arg1));
        }
        public function addEventListener(_arg1:String, _arg2:Function, _arg3:Boolean=false, _arg4:int=0, _arg5:Boolean=false):void{
            _bindingEventDispatcher.addEventListener(_arg1, _arg2, _arg3, _arg4, _arg5);
        }
        public function get Value():int{
            return (this._82420049Value);
        }
        public function set Type(_arg1:String):void{
            var _local2:Object = this._2622298Type;
            if (_local2 !== _arg1){
                this._2622298Type = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "Type", _local2, _arg1));
            };
        }
        public function removeEventListener(_arg1:String, _arg2:Function, _arg3:Boolean=false):void{
            _bindingEventDispatcher.removeEventListener(_arg1, _arg2, _arg3);
        }
        public function get Type():String{
            return (this._2622298Type);
        }

    }
}//package com.redbox.changetech.vo 
