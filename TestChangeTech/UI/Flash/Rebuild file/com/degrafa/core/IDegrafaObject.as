//Created by Action Script Viewer - http://www.buraks.com/asv
package com.degrafa.core {
    import flash.events.*;

    public interface IDegrafaObject extends IEventDispatcher {

        function set suppressEventProcessing(_arg1:Boolean):void;
        function set parent(_arg1:IDegrafaObject):void;
        function dispatchPropertyChange(_arg1:Boolean=false, _arg2:Object=null, _arg3:Object=null, _arg4:Object=null, _arg5:Object=null):Boolean;
        function set enableEvents(_arg1:Boolean):void;
        function get parent():IDegrafaObject;
        function get enableEvents():Boolean;
        function get hasEventManager():Boolean;
        function get suppressEventProcessing():Boolean;

    }
}//package com.degrafa.core 
