//Created by Action Script Viewer - http://www.buraks.com/asv
package mx.skins.halo {
    import flash.display.*;
    import mx.styles.*;
    import mx.utils.*;
    import mx.skins.*;

    public class RadioButtonIcon extends Border {

        mx_internal static const VERSION:String = "3.2.0.3958";

        private static var cache:Object = {};

        override public function get measuredWidth():Number{
            return (14);
        }
        override public function get measuredHeight():Number{
            return (14);
        }
        override protected function updateDisplayList(_arg1:Number, _arg2:Number):void{
            var _local13:Array;
            var _local14:Array;
            var _local15:Array;
            var _local16:Array;
            var _local18:Array;
            var _local19:Array;
            super.updateDisplayList(_arg1, _arg2);
            var _local3:uint = getStyle("iconColor");
            var _local4:uint = getStyle("borderColor");
            var _local5:Array = getStyle("fillAlphas");
            var _local6:Array = getStyle("fillColors");
            StyleManager.getColorNames(_local6);
            var _local7:Array = getStyle("highlightAlphas");
            var _local8:uint = getStyle("themeColor");
            var _local9:Object = calcDerivedStyles(_local8, _local4, _local6[0], _local6[1]);
            var _local10:Number = ColorUtil.adjustBrightness2(_local4, -50);
            var _local11:Number = ColorUtil.adjustBrightness2(_local8, -25);
            var _local12:Number = (width / 2);
            var _local17:Graphics = graphics;
            _local17.clear();
            switch (name){
                case "upIcon":
                    _local13 = [_local6[0], _local6[1]];
                    _local14 = [_local5[0], _local5[1]];
                    _local17.beginGradientFill(GradientType.LINEAR, [_local4, _local10], [100, 100], [0, 0xFF], verticalGradientMatrix(0, 0, _arg1, _arg2));
                    _local17.drawCircle(_local12, _local12, _local12);
                    _local17.drawCircle(_local12, _local12, (_local12 - 1));
                    _local17.endFill();
                    _local17.beginGradientFill(GradientType.LINEAR, _local13, _local14, [0, 0xFF], verticalGradientMatrix(1, 1, (_arg1 - 2), (_arg2 - 2)));
                    _local17.drawCircle(_local12, _local12, (_local12 - 1));
                    _local17.endFill();
                    drawRoundRect(1, 1, (_arg1 - 2), ((_arg2 - 2) / 2), {tl:_local12, tr:_local12, bl:0, br:0}, [0xFFFFFF, 0xFFFFFF], _local7, verticalGradientMatrix(0, 0, (_arg1 - 2), ((_arg2 - 2) / 2)));
                    break;
                case "overIcon":
                    if (_local6.length > 2){
                        _local18 = [_local6[2], _local6[3]];
                    } else {
                        _local18 = [_local6[0], _local6[1]];
                    };
                    if (_local5.length > 2){
                        _local19 = [_local5[2], _local5[3]];
                    } else {
                        _local19 = [_local5[0], _local5[1]];
                    };
                    _local17.beginGradientFill(GradientType.LINEAR, [_local8, _local11], [100, 100], [0, 0xFF], verticalGradientMatrix(0, 0, _arg1, _arg2));
                    _local17.drawCircle(_local12, _local12, _local12);
                    _local17.drawCircle(_local12, _local12, (_local12 - 1));
                    _local17.endFill();
                    _local17.beginGradientFill(GradientType.LINEAR, _local18, _local19, [0, 0xFF], verticalGradientMatrix(1, 1, (_arg1 - 2), (_arg2 - 2)));
                    _local17.drawCircle(_local12, _local12, (_local12 - 1));
                    _local17.endFill();
                    drawRoundRect(1, 1, (_arg1 - 2), ((_arg2 - 2) / 2), {tl:_local12, tr:_local12, bl:0, br:0}, [0xFFFFFF, 0xFFFFFF], _local7, verticalGradientMatrix(0, 0, (_arg1 - 2), ((_arg2 - 2) / 2)));
                    break;
                case "downIcon":
                    _local17.beginGradientFill(GradientType.LINEAR, [_local8, _local11], [100, 100], [0, 0xFF], verticalGradientMatrix(0, 0, _arg1, _arg2));
                    _local17.drawCircle(_local12, _local12, _local12);
                    _local17.endFill();
                    _local17.beginGradientFill(GradientType.LINEAR, [_local9.fillColorPress1, _local9.fillColorPress2], [100, 100], [0, 0xFF], verticalGradientMatrix(1, 1, (_arg1 - 2), (_arg2 - 2)));
                    _local17.drawCircle(_local12, _local12, (_local12 - 1));
                    _local17.endFill();
                    drawRoundRect(1, 1, (_arg1 - 2), ((_arg2 - 2) / 2), {tl:_local12, tr:_local12, bl:0, br:0}, [0xFFFFFF, 0xFFFFFF], _local7, verticalGradientMatrix(0, 0, (_arg1 - 2), ((_arg2 - 2) / 2)));
                    break;
                case "disabledIcon":
                    _local15 = [_local6[0], _local6[1]];
                    _local16 = [Math.max(0, (_local5[0] - 0.15)), Math.max(0, (_local5[1] - 0.15))];
                    _local17.beginGradientFill(GradientType.LINEAR, [_local4, _local10], [0.5, 0.5], [0, 0xFF], verticalGradientMatrix(0, 0, _arg1, _arg2));
                    _local17.drawCircle(_local12, _local12, _local12);
                    _local17.drawCircle(_local12, _local12, (_local12 - 1));
                    _local17.endFill();
                    _local17.beginGradientFill(GradientType.LINEAR, _local15, _local16, [0, 0xFF], verticalGradientMatrix(1, 1, (_arg1 - 2), (_arg2 - 2)));
                    _local17.drawCircle(_local12, _local12, (_local12 - 1));
                    _local17.endFill();
                    break;
                case "selectedUpIcon":
                case "selectedOverIcon":
                case "selectedDownIcon":
                    _local13 = [_local6[0], _local6[1]];
                    _local14 = [_local5[0], _local5[1]];
                    _local17.beginGradientFill(GradientType.LINEAR, [_local4, _local10], [100, 100], [0, 0xFF], verticalGradientMatrix(0, 0, _arg1, _arg2));
                    _local17.drawCircle(_local12, _local12, _local12);
                    _local17.drawCircle(_local12, _local12, (_local12 - 1));
                    _local17.endFill();
                    _local17.beginGradientFill(GradientType.LINEAR, _local13, _local14, [0, 0xFF], verticalGradientMatrix(1, 1, (_arg1 - 2), (_arg2 - 2)));
                    _local17.drawCircle(_local12, _local12, (_local12 - 1));
                    _local17.endFill();
                    drawRoundRect(1, 1, (_arg1 - 2), ((_arg2 - 2) / 2), {tl:_local12, tr:_local12, bl:0, br:0}, [0xFFFFFF, 0xFFFFFF], _local7, verticalGradientMatrix(0, 0, (_arg1 - 2), ((_arg2 - 2) / 2)));
                    _local17.beginFill(_local3);
                    _local17.drawCircle(_local12, _local12, 2);
                    _local17.endFill();
                    break;
                case "selectedDisabledIcon":
                    _local15 = [_local6[0], _local6[1]];
                    _local16 = [Math.max(0, (_local5[0] - 0.15)), Math.max(0, (_local5[1] - 0.15))];
                    _local17.beginGradientFill(GradientType.LINEAR, [_local4, _local10], [0.5, 0.5], [0, 0xFF], verticalGradientMatrix(0, 0, _arg1, _arg2));
                    _local17.drawCircle(_local12, _local12, _local12);
                    _local17.drawCircle(_local12, _local12, (_local12 - 1));
                    _local17.endFill();
                    _local17.beginGradientFill(GradientType.LINEAR, _local15, _local16, [0, 0xFF], verticalGradientMatrix(1, 1, (_arg1 - 2), (_arg2 - 2)));
                    _local17.drawCircle(_local12, _local12, (_local12 - 1));
                    _local17.endFill();
                    _local3 = getStyle("disabledIconColor");
                    _local17.beginFill(_local3);
                    _local17.drawCircle(_local12, _local12, 2);
                    _local17.endFill();
                    break;
            };
        }

        private static function calcDerivedStyles(_arg1:uint, _arg2:uint, _arg3:uint, _arg4:uint):Object{
            var _local6:Object;
            var _local5:String = HaloColors.getCacheKey(_arg1, _arg2, _arg3, _arg4);
            if (!cache[_local5]){
                _local6 = (cache[_local5] = {});
                HaloColors.addHaloColors(_local6, _arg1, _arg3, _arg4);
                _local6.borderColorDrk1 = ColorUtil.adjustBrightness2(_arg2, -60);
            };
            return (cache[_local5]);
        }

    }
}//package mx.skins.halo 
