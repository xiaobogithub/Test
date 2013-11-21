//Created by Action Script Viewer - http://www.buraks.com/asv
package com.degrafa {
    import flash.geom.*;
    import flash.events.*;

    public class GraphicPoint extends Point implements IGraphicPoint, IEventDispatcher {

        private var _bindingEventDispatcher:EventDispatcher

        public function GraphicPoint(_arg1:Number=0, _arg2:Number=0){
            _bindingEventDispatcher = new EventDispatcher(IEventDispatcher(this));
            super(_arg1, _arg2);
        }
        public function dispatchEvent(_arg1:Event):Boolean{
            return (_bindingEventDispatcher.dispatchEvent(_arg1));
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
        public function removeEventListener(_arg1:String, _arg2:Function, _arg3:Boolean=false):void{
            _bindingEventDispatcher.removeEventListener(_arg1, _arg2, _arg3);
        }

    }
}//package com.degrafa 
