//Created by Action Script Viewer - http://www.buraks.com/asv
package org.papervision3d.core.log {
    import flash.events.*;
    import org.papervision3d.core.log.event.*;

    public class PaperLogger extends EventDispatcher {

        public var traceLogger:PaperTraceLogger

        private static var instance:PaperLogger;

        public function PaperLogger(){
            if (instance){
                throw (new Error("Don't call the PaperLogger constructor directly"));
            };
            traceLogger = new PaperTraceLogger();
            registerLogger(traceLogger);
        }
        public function registerLogger(_arg1:AbstractPaperLogger):void{
            _arg1.registerWithPaperLogger(this);
        }
        public function _debug(_arg1:String, _arg2:Object=null, ... _args):void{
            var _local4:PaperLogVO = new PaperLogVO(LogLevel.DEBUG, _arg1, _arg2, _args);
            var _local5:PaperLoggerEvent = new PaperLoggerEvent(_local4);
            dispatchEvent(_local5);
        }
        public function _log(_arg1:String, _arg2:Object=null, ... _args):void{
            var _local4:PaperLogVO = new PaperLogVO(LogLevel.LOG, _arg1, _arg2, _args);
            var _local5:PaperLoggerEvent = new PaperLoggerEvent(_local4);
            dispatchEvent(_local5);
        }
        public function _error(_arg1:String, _arg2:Object=null, ... _args):void{
            var _local4:PaperLogVO = new PaperLogVO(LogLevel.ERROR, _arg1, _arg2, _args);
            var _local5:PaperLoggerEvent = new PaperLoggerEvent(_local4);
            dispatchEvent(_local5);
        }
        public function unregisterLogger(_arg1:AbstractPaperLogger):void{
            _arg1.unregisterFromPaperLogger(this);
        }
        public function _info(_arg1:String, _arg2:Object=null, ... _args):void{
            var _local4:PaperLogVO = new PaperLogVO(LogLevel.INFO, _arg1, _arg2, _args);
            var _local5:PaperLoggerEvent = new PaperLoggerEvent(_local4);
            dispatchEvent(_local5);
        }
        public function _warning(_arg1:String, _arg2:Object=null, ... _args):void{
            var _local4:PaperLogVO = new PaperLogVO(LogLevel.WARNING, _arg1, _arg2, _args);
            var _local5:PaperLoggerEvent = new PaperLoggerEvent(_local4);
            dispatchEvent(_local5);
        }

        public static function debug(_arg1:String, _arg2:Object=null, ... _args):void{
            getInstance()._debug(_arg1);
        }
        public static function log(_arg1:String, _arg2:Object=null, ... _args):void{
            getInstance()._log(_arg1);
        }
        public static function error(_arg1:String, _arg2:Object=null, ... _args):void{
            getInstance()._error(_arg1);
        }
        public static function getInstance():PaperLogger{
            if (!instance){
                instance = new (PaperLogger);
            };
            return (instance);
        }
        public static function warning(_arg1:String, _arg2:Object=null, ... _args):void{
            getInstance()._warning(_arg1);
        }
        public static function info(_arg1:String, _arg2:Object=null, ... _args):void{
            getInstance()._info(_arg1);
        }

    }
}//package org.papervision3d.core.log 
