//Created by Action Script Viewer - http://www.buraks.com/asv
package com.degrafa.events {
    import flash.events.*;

    public class DegrafaEvent extends Event {

        public static const PRE_RENDER:String = "preRender";
        public static const RENDER:String = "render";

        public function DegrafaEvent(_arg1:String, _arg2:Boolean=false, _arg3:Boolean=false):void{
            super(_arg1, _arg2, _arg3);
        }
    }
}//package com.degrafa.events 
