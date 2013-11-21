//Created by Action Script Viewer - http://www.buraks.com/asv
package com.adobe.cairngorm.control {
    import flash.events.*;

    public class CairngormEventDispatcher {

        private var eventDispatcher:IEventDispatcher

        private static var instance:CairngormEventDispatcher;

        public function CairngormEventDispatcher(_arg1:IEventDispatcher=null){
            eventDispatcher = new EventDispatcher(_arg1);
        }
        public function dispatchEvent(_arg1:CairngormEvent):Boolean{
            return (eventDispatcher.dispatchEvent(_arg1));
        }
        public function hasEventListener(_arg1:String):Boolean{
            return (eventDispatcher.hasEventListener(_arg1));
        }
        public function willTrigger(_arg1:String):Boolean{
            return (eventDispatcher.willTrigger(_arg1));
        }
        public function removeEventListener(_arg1:String, _arg2:Function, _arg3:Boolean=false):void{
            eventDispatcher.removeEventListener(_arg1, _arg2, _arg3);
        }
        public function addEventListener(_arg1:String, _arg2:Function, _arg3:Boolean=false, _arg4:int=0, _arg5:Boolean=false):void{
            eventDispatcher.addEventListener(_arg1, _arg2, _arg3, _arg4, _arg5);
        }

        public static function getInstance():CairngormEventDispatcher{
            if (instance == null){
                instance = new (CairngormEventDispatcher);
            };
            return (instance);
        }

    }
}//package com.adobe.cairngorm.control 
