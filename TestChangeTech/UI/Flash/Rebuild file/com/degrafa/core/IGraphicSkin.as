//Created by Action Script Viewer - http://www.buraks.com/asv
package com.degrafa.core {
    import com.degrafa.*;
    import com.degrafa.core.collections.*;

    public interface IGraphicSkin extends IGraphic {

        function get geometryCollection():GeometryCollection;
        function get geometry():Array;
        function set geometry(_arg1:Array):void;
        function invalidateDisplayList():void;

    }
}//package com.degrafa.core 
