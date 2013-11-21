//Created by Action Script Viewer - http://www.buraks.com/asv
package org.papervision3d.core.log {

    public class PaperTraceLogger extends AbstractPaperLogger implements IPaperLogger {

        override public function warning(_arg1:String, _arg2:Object=null, _arg3:Array=null):void{
            trace("WARNING:", _arg1, _arg3);
        }
        override public function log(_arg1:String, _arg2:Object=null, _arg3:Array=null):void{
            trace("LOG:", _arg1, _arg3);
        }
        override public function error(_arg1:String, _arg2:Object=null, _arg3:Array=null):void{
            trace("ERROR:", _arg1, _arg3);
        }
        override public function fatal(_arg1:String, _arg2:Object=null, _arg3:Array=null):void{
            trace("FATAL:", _arg1, _arg3);
        }
        override public function info(_arg1:String, _arg2:Object=null, _arg3:Array=null):void{
            trace("INFO:", _arg1, _arg3);
        }
        override public function debug(_arg1:String, _arg2:Object=null, _arg3:Array=null):void{
            trace("DEBUG:", _arg1, _arg3);
        }

    }
}//package org.papervision3d.core.log 
