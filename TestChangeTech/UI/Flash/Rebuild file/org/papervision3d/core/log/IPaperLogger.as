//Created by Action Script Viewer - http://www.buraks.com/asv
package org.papervision3d.core.log {

    public interface IPaperLogger {

        function debug(_arg1:String, _arg2:Object=null, _arg3:Array=null):void;
        function log(_arg1:String, _arg2:Object=null, _arg3:Array=null):void;
        function error(_arg1:String, _arg2:Object=null, _arg3:Array=null):void;
        function fatal(_arg1:String, _arg2:Object=null, _arg3:Array=null):void;
        function warning(_arg1:String, _arg2:Object=null, _arg3:Array=null):void;
        function info(_arg1:String, _arg2:Object=null, _arg3:Array=null):void;

    }
}//package org.papervision3d.core.log 
