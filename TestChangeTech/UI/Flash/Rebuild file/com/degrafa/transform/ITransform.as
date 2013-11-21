//Created by Action Script Viewer - http://www.buraks.com/asv
package com.degrafa.transform {
    import flash.geom.*;
    import com.degrafa.*;
    import com.degrafa.core.*;

    public interface ITransform extends IDegrafaObject {

        function hasExplicitSetting():Boolean;
        function get centerX():Number;
        function get centerY():Number;
        function set centerX(_arg1:Number):void;
        function set centerY(_arg1:Number):void;
        function get data():String;
        function get transformMatrix():Matrix;
        function set registrationPoint(_arg1:String):void;
        function get isIdentity():Boolean;
        function getRegPoint(_arg1:IGeometryComposition):Point;
        function getTransformFor(_arg1:IGeometryComposition):Matrix;
        function set data(_arg1:String):void;
        function get scaleX():Number;
        function get scaleY():Number;
        function get registrationPoint():String;
        function getRegPointForRectangle(_arg1:Rectangle):Point;
        function get y():Number;
        function get angle():Number;
        function get skewX():Number;
        function get skewY():Number;
        function get x():Number;

    }
}//package com.degrafa.transform 
