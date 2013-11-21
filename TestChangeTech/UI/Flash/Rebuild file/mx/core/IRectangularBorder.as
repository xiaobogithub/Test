//Created by Action Script Viewer - http://www.buraks.com/asv
package mx.core {
    import flash.geom.*;

    public interface IRectangularBorder extends IBorder {

        function get backgroundImageBounds():Rectangle;
        function get hasBackgroundImage():Boolean;
        function set backgroundImageBounds(_arg1:Rectangle):void;
        function layoutBackgroundImage():void;

    }
}//package mx.core 
