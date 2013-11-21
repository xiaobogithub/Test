//Created by Action Script Viewer - http://www.buraks.com/asv
package com.degrafa.core.utils {
    import com.degrafa.paint.palette.*;

    public class ColorUtil {

        private static function resolveColorFromString(_arg1:String, _arg2:uint=0):uint{
            if (_arg1.search(",") == 3){
                if (_arg1.search("%") != -1){
                    return (resolveColorFromRGBPercent((_arg1 as String)));
                };
                return (resolveColorFromRGB((_arg1 as String)));
            } else {
                if (_arg1.search(",") == 4){
                    return (resolveColorFromCMYK((_arg1 as String)));
                };
            };
            var _local3:uint;
            if ((((String(_arg1).charAt(0) == "#")) || ((String(_arg1).substr(0, 2) == "0x")))){
                _arg1 = _arg1.replace("#", "");
                if (_arg1.length == 3){
                    _local3 = parseColorNotation(_arg1);
                } else {
                    _local3 = parseInt(_arg1, 16);
                };
            } else {
                _local3 = parseInt(String(_arg1), 10);
            };
            if (((isNaN(_local3)) || ((_local3 == 0)))){
                _local3 = resolveColorFromKey((_arg1 as String), _arg2);
            };
            return (_local3);
        }
        public static function hexToRgb(_arg1:Number):Object{
            var _local2:Number = (_arg1 >> 16);
            var _local3:Number = (_arg1 - (_local2 << 16));
            var _local4:Number = (_local3 >> 8);
            var _local5:Number = (_local3 - (_local4 << 8));
            return ({red:_local2, green:_local4, blue:_local5});
        }
        public static function resolveColor(_arg1:Object, _arg2:uint=0):uint{
            if ((_arg1 is uint)){
                if (_arg1.toString(16).length == 3){
                    return (parseColorNotation(_arg1.toString(16)));
                };
                return ((_arg1 as uint));
                //unresolved jump
            };
            if ((_arg1 is String)){
                return (resolveColorFromString((_arg1 as String), _arg2));
            };
            if ((_arg1 is PaletteEntry)){
                return (resolveColor(PaletteEntry(_arg1).value, _arg2));
            };
            return (0);
        }
        public static function resolveColorFromRGBPercent(_arg1:String):uint{
            var _local2:Array = _arg1.replace(/%/g, "").split(",");
            return (uint((((int(Math.round(((_local2[0] / 100) * 0xFF))) << 16) | (int(Math.round(((_local2[1] / 100) * 0xFF))) << 8)) | int(Math.round(((_local2[2] / 100) * 0xFF))))));
        }
        public static function parseColorNotation(_arg1:String):uint{
            _arg1 = _arg1.replace("#", "");
            _arg1 = (((((("0x" + _arg1.charAt(0)) + _arg1.charAt(0)) + _arg1.charAt(1)) + _arg1.charAt(1)) + _arg1.charAt(2)) + _arg1.charAt(2));
            return (uint(_arg1));
        }
        public static function resolveColorFromRGB(_arg1:String):Number{
            var _local2:Array = _arg1.split(",");
            return (uint((((int(_local2[0]) << 16) | (int(_local2[1]) << 8)) | int(_local2[2]))));
        }
        public static function resolveColorFromCMYK(_arg1:String):uint{
            var _local2:Array = _arg1.split(",");
            var _local3:int = ((0xFF * _local2[0]) / 100);
            var _local4:int = ((0xFF * _local2[1]) / 100);
            var _local5:int = ((0xFF * _local2[2]) / 100);
            var _local6:int = ((0xFF * _local2[3]) / 100);
            var _local7:int = int(decColorToHex(Math.round((((0xFF - _local3) * (0xFF - _local6)) / 0xFF))));
            var _local8:int = int(decColorToHex(Math.round((((0xFF - _local4) * (0xFF - _local6)) / 0xFF))));
            var _local9:int = int(decColorToHex(Math.round((((0xFF - _local5) * (0xFF - _local6)) / 0xFF))));
            return (resolveColorFromRGB(((((_local7 + ",") + _local8) + ",") + _local9)));
        }
        public static function resolveColorFromKey(_arg1:String, _arg2:uint=0):uint{
            var _local3:String = _arg1.toUpperCase();
            if (ColorKeys[_local3] != null){
                return (uint(ColorKeys[_local3]));
            };
            return (_arg2);
        }
        public static function decColorToHex(_arg1:uint, _arg2:String="0x"):String{
            var _local3:String = ("00000" + _arg1.toString(16).toUpperCase()).substr(-6);
            return ((_arg2 + _local3));
        }

    }
}//package com.degrafa.core.utils 
