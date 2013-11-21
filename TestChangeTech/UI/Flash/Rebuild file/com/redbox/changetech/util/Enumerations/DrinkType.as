//Created by Action Script Viewer - http://www.buraks.com/asv
package com.redbox.changetech.util.Enumerations {
    import flash.events.*;
    import mx.events.*;

    public class DrinkType implements IEventDispatcher {

        private var _bindingEventDispatcher:EventDispatcher
        private var _2603341Text:String

        public static const Beer:DrinkType = new (DrinkType);
;
        public static const Spirit:DrinkType = new (DrinkType);
;
        public static const Wine:DrinkType = new (DrinkType);
;
        public static const Total:DrinkType = new (DrinkType);
;

        public function DrinkType(){
            _bindingEventDispatcher = new EventDispatcher(IEventDispatcher(this));
            super();
        }
        public function dispatchEvent(_arg1:Event):Boolean{
            return (_bindingEventDispatcher.dispatchEvent(_arg1));
        }
        public function get Text():String{
            return (this._2603341Text);
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
        public function set Text(_arg1:String):void{
            var _local2:Object = this._2603341Text;
            if (_local2 !== _arg1){
                this._2603341Text = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "Text", _local2, _arg1));
            };
        }
        public function hasEventListener(_arg1:String):Boolean{
            return (_bindingEventDispatcher.hasEventListener(_arg1));
        }

        CStringUtils.InitEnumConstants(DrinkType);
    }
}//package com.redbox.changetech.util.Enumerations 
