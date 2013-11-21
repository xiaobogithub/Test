//Created by Action Script Viewer - http://www.buraks.com/asv
package com.degrafa {
    import flash.display.*;
    import flash.geom.*;
    import com.degrafa.geometry.command.*;

    public interface IGeometryComposition {

        function draw(_arg1:Graphics, _arg2:Rectangle):void;
        function get bounds():Rectangle;
        function preDraw():void;
        function set commandStack(_arg1:CommandStack):void;
        function get commandStack():CommandStack;
        function endDraw(_arg1:Graphics):void;

    }
}//package com.degrafa 
