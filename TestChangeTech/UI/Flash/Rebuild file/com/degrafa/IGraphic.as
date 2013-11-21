//Created by Action Script Viewer - http://www.buraks.com/asv
package com.degrafa {
    import flash.display.*;
    import flash.geom.*;
    import com.degrafa.core.*;

    public interface IGraphic {

        function get percentWidth():Number;
        function set percentWidth(_arg1:Number):void;
        function get fill():IGraphicsFill;
        function draw(_arg1:Graphics, _arg2:Rectangle):void;
        function get width():Number;
        function get stroke():IGraphicsStroke;
        function removeEventListener(_arg1:String, _arg2:Function, _arg3:Boolean=false):void;
        function get name():String;
        function set width(_arg1:Number):void;
        function addEventListener(_arg1:String, _arg2:Function, _arg3:Boolean=false, _arg4:int=0, _arg5:Boolean=false):void;
        function set height(_arg1:Number):void;
        function get fills():Array;
        function get target():DisplayObjectContainer;
        function get percentHeight():Number;
        function get measuredHeight():Number;
        function set fill(_arg1:IGraphicsFill):void;
        function set name(_arg1:String):void;
        function set percentHeight(_arg1:Number):void;
        function set fills(_arg1:Array):void;
        function set target(_arg1:DisplayObjectContainer):void;
        function get height():Number;
        function get parent():DisplayObjectContainer;
        function get measuredWidth():Number;
        function set x(_arg1:Number):void;
        function set y(_arg1:Number):void;
        function set strokes(_arg1:Array):void;
        function set stroke(_arg1:IGraphicsStroke):void;
        function get x():Number;
        function get y():Number;
        function get strokes():Array;
        function endDraw(_arg1:Graphics):void;

    }
}//package com.degrafa 
