//Created by Action Script Viewer - http://www.buraks.com/asv
package mx.messaging.events {
    import flash.events.*;
    import mx.messaging.messages.*;

    public class MessageEvent extends Event {

        public var message:IMessage

        public static const RESULT:String = "result";
        public static const MESSAGE:String = "message";

        public function MessageEvent(_arg1:String, _arg2:Boolean=false, _arg3:Boolean=false, _arg4:IMessage=null){
            super(_arg1, _arg2, _arg3);
            this.message = _arg4;
        }
        public function get messageId():String{
            if (message != null){
                return (message.messageId);
            };
            return (null);
        }
        override public function toString():String{
            return (formatToString("MessageEvent", "messageId", "type", "bubbles", "cancelable", "eventPhase"));
        }
        override public function clone():Event{
            return (new MessageEvent(type, bubbles, cancelable, message));
        }

        public static function createEvent(_arg1:String, _arg2:IMessage):MessageEvent{
            return (new MessageEvent(_arg1, false, false, _arg2));
        }

    }
}//package mx.messaging.events 
