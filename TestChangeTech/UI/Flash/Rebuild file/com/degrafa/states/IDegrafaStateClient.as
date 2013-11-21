//Created by Action Script Viewer - http://www.buraks.com/asv
package com.degrafa.states {
    import flash.events.*;
    import com.degrafa.*;
    import com.degrafa.core.collections.*;

    public interface IDegrafaStateClient extends IEventDispatcher, IStateClient, IGeometry {

        function get geometryCollection():GeometryCollection;
        function get states():Array;
        function set geometry(_arg1:Array):void;
        function get geometry():Array;
        function get isInitialized():Boolean;
        function set states(_arg1:Array):void;

    }
}//package com.degrafa.states 
