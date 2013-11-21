//Created by Action Script Viewer - http://www.buraks.com/asv
package com.degrafa.triggers {
    import flash.events.*;
    import com.degrafa.states.*;

    public interface ITrigger {

        function get triggerParent():IDegrafaStateClient;
        function set triggerParent(_arg1:IDegrafaStateClient):void;
        function set source(_arg1:IEventDispatcher):void;
        function get source():IEventDispatcher;
        function get id():String;
        function set id(_arg1:String):void;

    }
}//package com.degrafa.triggers 
