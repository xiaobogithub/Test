//Created by Action Script Viewer - http://www.buraks.com/asv
package mx.events {
    import flash.events.*;

    public class CloseEvent extends Event {

        public var detail:int

        mx_internal static const VERSION:String = "3.2.0.3958";
        public static const CLOSE:String = "close";

        public function CloseEvent(_arg1:String, _arg2:Boolean=false, _arg3:Boolean=false, _arg4:int=-1){
            super(_arg1, _arg2, _arg3);
            this.detail = _arg4;
        }
        override public function clone():Event{
            return (new CloseEvent(type, bubbles, cancelable, detail));
        }

    }
}//package mx.events 
