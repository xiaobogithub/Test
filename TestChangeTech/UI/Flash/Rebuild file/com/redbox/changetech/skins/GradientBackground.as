//Created by Action Script Viewer - http://www.buraks.com/asv
package com.redbox.changetech.skins {
    import flash.geom.*;
    import mx.skins.*;

    public class GradientBackground extends ProgrammaticSkin {

        override public function get measuredWidth():Number{
            return (20);
        }
        override public function get measuredHeight():Number{
            return (20);
        }
        override protected function updateDisplayList(_arg1:Number, _arg2:Number):void{
            var _local3:Array = getStyle("fillColors");
            var _local4:Array = getStyle("fillAlphas");
            var _local5:int = getStyle("cornerRadius");
            var _local6:String = getStyle("gradientType");
            var _local7:Number = getStyle("angle");
            var _local8:Number = getStyle("focalPointRatio");
            if (_local3 == null){
                _local3 = [0xEEEEEE, 0x999999];
            };
            if (_local4 == null){
                _local4 = [1, 1];
            };
            if ((((_local6 == "")) || ((_local6 == null)))){
                _local6 = "linear";
            };
            if (isNaN(_local7)){
                _local7 = 90;
            };
            if (isNaN(_local8)){
                _local8 = 0.5;
            };
            var _local9:Matrix = new Matrix();
            _local9.createGradientBox(_arg1, _arg2, ((_local7 * Math.PI) / 180));
            graphics.beginGradientFill(_local6, _local3, _local4, [0, 0xFF], _local9, "pad", "rgb", _local8);
            graphics.drawRoundRect(0, 0, _arg1, _arg2, (_local5 * 0.5), (_local5 * 0.5));
            graphics.endFill();
        }

    }
}//package com.redbox.changetech.skins 
