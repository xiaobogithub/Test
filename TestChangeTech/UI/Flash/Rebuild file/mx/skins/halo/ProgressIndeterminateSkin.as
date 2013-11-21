//Created by Action Script Viewer - http://www.buraks.com/asv
package mx.skins.halo {
    import flash.display.*;
    import mx.styles.*;
    import mx.utils.*;
    import mx.skins.*;

    public class ProgressIndeterminateSkin extends Border {

        mx_internal static const VERSION:String = "3.2.0.3958";

        override public function get measuredWidth():Number{
            return (195);
        }
        override public function get measuredHeight():Number{
            return (6);
        }
        override protected function updateDisplayList(_arg1:Number, _arg2:Number):void{
            super.updateDisplayList(_arg1, _arg2);
            var _local3:* = getStyle("barColor");
            var _local4:uint = (StyleManager.isValidStyleValue(_local3)) ? _local3 : getStyle("themeColor");
            var _local5:Number = ColorUtil.adjustBrightness2(_local4, 60);
            var _local6:Number = getStyle("indeterminateMoveInterval");
            if (isNaN(_local6)){
                _local6 = 28;
            };
            var _local7:Graphics = graphics;
            _local7.clear();
            var _local8:int;
            while (_local8 < _arg1) {
                _local7.beginFill(_local5, 0.8);
                _local7.moveTo(_local8, 1);
                _local7.lineTo(Math.min((_local8 + 14), _arg1), 1);
                _local7.lineTo(Math.min((_local8 + 10), _arg1), (_arg2 - 1));
                _local7.lineTo(Math.max((_local8 - 4), 0), (_arg2 - 1));
                _local7.lineTo(_local8, 1);
                _local7.endFill();
                _local8 = (_local8 + _local6);
            };
        }

    }
}//package mx.skins.halo 
