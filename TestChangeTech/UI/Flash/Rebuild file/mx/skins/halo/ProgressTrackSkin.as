//Created by Action Script Viewer - http://www.buraks.com/asv
package mx.skins.halo {
    import mx.styles.*;
    import mx.utils.*;
    import mx.skins.*;

    public class ProgressTrackSkin extends Border {

        mx_internal static const VERSION:String = "3.2.0.3958";

        override public function get measuredWidth():Number{
            return (200);
        }
        override public function get measuredHeight():Number{
            return (6);
        }
        override protected function updateDisplayList(_arg1:Number, _arg2:Number):void{
            super.updateDisplayList(_arg1, _arg2);
            var _local3:uint = getStyle("borderColor");
            var _local4:Array = (getStyle("trackColors") as Array);
            StyleManager.getColorNames(_local4);
            var _local5:Number = ColorUtil.adjustBrightness2(_local3, -30);
            graphics.clear();
            drawRoundRect(0, 0, _arg1, _arg2, 0, [_local5, _local3], 1, verticalGradientMatrix(0, 0, _arg1, _arg2));
            drawRoundRect(1, 1, (_arg1 - 2), (_arg2 - 2), 0, _local4, 1, verticalGradientMatrix(1, 1, (_arg1 - 2), (_arg2 - 2)));
        }

    }
}//package mx.skins.halo 
