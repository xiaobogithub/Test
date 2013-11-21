//Created by Action Script Viewer - http://www.buraks.com/asv
package com.redbox.changetech.skins {
    import flash.geom.*;
    import mx.graphics.*;
    import mx.skins.*;

    public class ApplicationBackground extends ProgrammaticSkin {

        override public function get measuredWidth():Number{
            return (20);
        }
        override public function get measuredHeight():Number{
            return (20);
        }
        override protected function updateDisplayList(_arg1:Number, _arg2:Number):void{
            var _local3:Number = _arg1;
            var _local4:Number = _arg2;
            graphics.lineStyle(0, 3394815, 0);
            var _local5:LinearGradient = new LinearGradient();
            var _local6:GradientEntry = new GradientEntry(0xFFFFFF, 0.33, 1);
            var _local7:GradientEntry = new GradientEntry(0xFFFFFF, 0.66, 1);
            var _local8:GradientEntry = new GradientEntry(0xB0B0B0, 0.99, 1);
            _local5.entries = [_local6, _local7, _local8];
            _local5.angle = 90;
            graphics.moveTo(0, 0);
            _local5.begin(graphics, new Rectangle(0, 0, _local3, _local4));
            graphics.lineTo(_local3, 0);
            graphics.lineTo(_local3, _local4);
            graphics.lineTo(0, _local4);
            graphics.lineTo(0, 0);
            _local5.end(graphics);
        }

    }
}//package com.redbox.changetech.skins 
