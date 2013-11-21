//Created by Action Script Viewer - http://www.buraks.com/asv
package org.papervision3d.core.log {
    import org.papervision3d.core.log.event.*;

    public class AbstractPaperLogger implements IPaperLogger {

        public function registerWithPaperLogger(_arg1:PaperLogger):void{
            _arg1.addEventListener(PaperLoggerEvent.TYPE_LOGEVENT, onLogEvent);
        }
        public function debug(_arg1:String, _arg2:Object=null, _arg3:Array=null):void{
        }
        public function warning(_arg1:String, _arg2:Object=null, _arg3:Array=null):void{
        }
        public function log(_arg1:String, _arg2:Object=null, _arg3:Array=null):void{
        }
        public function fatal(_arg1:String, _arg2:Object=null, _arg3:Array=null):void{
        }
        public function error(_arg1:String, _arg2:Object=null, _arg3:Array=null):void{
        }
        protected function onLogEvent(_arg1:PaperLoggerEvent):void{
            var _local2:PaperLogVO = _arg1.paperLogVO;
            switch (_local2.level){
                case LogLevel.LOG:
                    log(_local2.msg, _local2.object, _local2.arg);
                    break;
                case LogLevel.INFO:
                    info(_local2.msg, _local2.object, _local2.arg);
                    break;
                case LogLevel.ERROR:
                    error(_local2.msg, _local2.object, _local2.arg);
                    break;
                case LogLevel.DEBUG:
                    debug(_local2.msg, _local2.object, _local2.arg);
                    break;
                case LogLevel.WARNING:
                    warning(_local2.msg, _local2.object, _local2.arg);
                    break;
                case LogLevel.FATAL:
                    fatal(_local2.msg, _local2.object, _local2.arg);
                    break;
                default:
                    log(_local2.msg, _local2.object, _local2.arg);
                    break;
            };
        }
        public function unregisterFromPaperLogger(_arg1:PaperLogger):void{
            _arg1.removeEventListener(PaperLoggerEvent.TYPE_LOGEVENT, onLogEvent);
        }
        public function info(_arg1:String, _arg2:Object=null, _arg3:Array=null):void{
        }

    }
}//package org.papervision3d.core.log 
