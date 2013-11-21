//Created by Action Script Viewer - http://www.buraks.com/asv
package com.degrafa {
    import flash.display.*;
    import flash.geom.*;
    import com.degrafa.core.*;

    public interface IGeometry {

        function set stroke(_arg1:IGraphicsStroke):void;
        function get fill():IGraphicsFill;
        function draw(_arg1:Graphics, _arg2:Rectangle):void;
        function set fill(_arg1:IGraphicsFill):void;
        function get stroke():IGraphicsStroke;
        function get data():String;
        function set data(_arg1:String):void;
        function endDraw(_arg1:Graphics):void;

    }
}//package com.degrafa 
