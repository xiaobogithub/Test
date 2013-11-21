//Created by Action Script Viewer - http://www.buraks.com/asv
package mx.logging {
    import flash.events.*;

    public class LogEvent extends Event {

        public var level:int
        public var message:String

        mx_internal static const VERSION:String = "3.2.0.3958";
        public static const LOG:String = "log";

        public function LogEvent(_arg1:String="", _arg2:int=0){
            super(LogEvent.LOG, false, false);
            this.message = _arg1;
            this.level = _arg2;
        }
        override public function clone():Event{
            return (new LogEvent(message, level));
        }

        public static function getLevelString(_arg1:uint):String{
            switch (_arg1){
                case LogEventLevel.INFO:
                    return ("INFO");
                case LogEventLevel.DEBUG:
                    return ("DEBUG");
                case LogEventLevel.ERROR:
                    return ("ERROR");
                case LogEventLevel.WARN:
                    return ("WARN");
                case LogEventLevel.FATAL:
                    return ("FATAL");
                case LogEventLevel.ALL:
                    return ("ALL");
            };
            return ("UNKNOWN");
        }

    }
}//package mx.logging 
