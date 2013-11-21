//Created by Action Script Viewer - http://www.buraks.com/asv
package com.redbox.changetech.skins {
    import flash.geom.*;
    import mx.skins.*;

    public class RoundedGradRectSkin extends ProgrammaticSkin {

        override public function get measuredWidth():Number{
            return (20);
        }
        override public function get measuredHeight():Number{
            return (20);
        }
        override protected function updateDisplayList(_arg1:Number, _arg2:Number):void{
            var _local3:String = ((getStyle("gradientType"))!=null) ? getStyle("gradientType") : "linear";
            var _local4:Number = (isNaN(getStyle("angle"))) ? 90 : getStyle("angle");
            var _local5:Number = (isNaN(getStyle("focalPointRatio"))) ? 0.5 : getStyle("focalPointRatio");
            var _local6:Array = ((getStyle("fillColors"))!=null) ? getStyle("fillColors") : [0, 0xFFFFFF];
            var _local7:Array = ((getStyle("fillAlphas"))!=null) ? getStyle("fillAlphas") : [1, 1];
            var _local8:int = (isNaN(getStyle("cornerRadius"))) ? 20 : getStyle("cornerRadius");
            var _local9:Number = (isNaN(getStyle("borderColor"))) ? 0 : getStyle("borderColor");
            var _local10:Number = (isNaN(getStyle("borderAlpha"))) ? 1 : getStyle("borderAlpha");
            var _local11:Number = (isNaN(getStyle("borderThickness"))) ? 2 : getStyle("borderThickness");
            var _local12:Number = _arg1;
            var _local13:Number = _arg2;
            var _local14:Matrix = new Matrix();
            _local14.createGradientBox(_local12, _local13, ((_local4 * Math.PI) / 180));
            graphics.clear();
            graphics.lineStyle(_local11, _local9, _local10);
            graphics.beginGradientFill(_local3, _local6, _local7, [0, 0xFF], _local14, "pad", "rgb", _local5);
            graphics.drawRoundRect(0, 0, _local12, _local13, _local8, _local8);
            graphics.endFill();
        }

    }
}//package com.redbox.changetech.skins 
