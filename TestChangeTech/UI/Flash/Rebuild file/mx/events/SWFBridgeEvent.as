//Created by Action Script Viewer - http://www.buraks.com/asv
package mx.events {
    import mx.core.*;
    import flash.events.*;

    public class SWFBridgeEvent extends Event {

        public var data:Object

        public static const BRIDGE_FOCUS_MANAGER_ACTIVATE:String = "bridgeFocusManagerActivate";
        public static const BRIDGE_WINDOW_ACTIVATE:String = "bridgeWindowActivate";
        public static const BRIDGE_WINDOW_DEACTIVATE:String = "brdigeWindowDeactivate";
        mx_internal static const VERSION:String = "3.2.0.3958";
        public static const BRIDGE_NEW_APPLICATION:String = "bridgeNewApplication";
        public static const BRIDGE_APPLICATION_UNLOADING:String = "bridgeApplicationUnloading";
        public static const BRIDGE_APPLICATION_ACTIVATE:String = "bridgeApplicationActivate";

        public function SWFBridgeEvent(_arg1:String, _arg2:Boolean=false, _arg3:Boolean=false, _arg4:Object=null){
            super(_arg1, _arg2, _arg3);
            this.data = _arg4;
        }
        override public function clone():Event{
            return (new SWFBridgeEvent(type, bubbles, cancelable, data));
        }

        public static function marshal(_arg1:Event):SWFBridgeEvent{
            var _local2:Object = _arg1;
            return (new SWFBridgeEvent(_local2.type, _local2.bubbles, _local2.cancelable, _local2.data));
        }

    }
}//package mx.events 
