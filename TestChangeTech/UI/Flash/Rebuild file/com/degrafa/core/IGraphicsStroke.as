//Created by Action Script Viewer - http://www.buraks.com/asv
package com.degrafa.core {
    import flash.display.*;
    import flash.geom.*;

    public interface IGraphicsStroke extends IDegrafaObject {

        function get caps():String;
        function set caps(_arg1:String):void;
        function get joints():String;
        function set pixelHinting(_arg1:Boolean):void;
        function set miterLimit(_arg1:Number):void;
        function get scaleMode():String;
        function apply(_arg1:Graphics, _arg2:Rectangle):void;
        function set weight(_arg1:Number):void;
        function set joints(_arg1:String):void;
        function get pixelHinting():Boolean;
        function get miterLimit():Number;
        function get weight():Number;
        function set scaleMode(_arg1:String):void;

    }
}//package com.degrafa.core 
