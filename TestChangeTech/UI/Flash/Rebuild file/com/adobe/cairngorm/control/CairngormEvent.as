//Created by Action Script Viewer - http://www.buraks.com/asv
package com.adobe.cairngorm.control {
    import flash.events.*;

    public class CairngormEvent extends Event {

        public var data

        public function CairngormEvent(_arg1:String, _arg2:Boolean=false, _arg3:Boolean=false){
            super(_arg1, _arg2, _arg3);
        }
        public function dispatch():Boolean{
            return (CairngormEventDispatcher.getInstance().dispatchEvent(this));
        }

    }
}//package com.adobe.cairngorm.control 
