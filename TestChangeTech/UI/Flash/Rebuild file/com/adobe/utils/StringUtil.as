//Created by Action Script Viewer - http://www.buraks.com/asv
package com.adobe.utils {

    public class StringUtil {

        public static function beginsWith(_arg1:String, _arg2:String):Boolean{
            return ((_arg2 == _arg1.substring(0, _arg2.length)));
        }
        public static function trim(_arg1:String):String{
            return (StringUtil.ltrim(StringUtil.rtrim(_arg1)));
        }
        public static function stringsAreEqual(_arg1:String, _arg2:String, _arg3:Boolean):Boolean{
            if (_arg3){
                return ((_arg1 == _arg2));
            };
            return ((_arg1.toUpperCase() == _arg2.toUpperCase()));
        }
        public static function replace(_arg1:String, _arg2:String, _arg3:String):String{
            var _local9:Number;
            var _local4:String = new String();
            var _local5:Boolean;
            var _local6:Number = _arg1.length;
            var _local7:Number = _arg2.length;
            var _local8:Number = 0;
            while (_local8 < _local6) {
                if (_arg1.charAt(_local8) == _arg2.charAt(0)){
                    _local5 = true;
                    _local9 = 0;
                    while (_local9 < _local7) {
                        if (_arg1.charAt((_local8 + _local9)) != _arg2.charAt(_local9)){
                            _local5 = false;
                            break;
                        };
                        _local9++;
                    };
                    //unresolved if
                    _local4 = (_local4 + _arg3);
                    _local8 = (_local8 + (_local7 - 1));
                } else {
                    _local4 = (_local4 + _arg1.charAt(_local8));
                };
                _local8++;
            };
            return (_local4);
        }
        public static function rtrim(_arg1:String):String{
            var _local2:Number = _arg1.length;
            var _local3:Number = _local2;
            while (_local3 > 0) {
                if (_arg1.charCodeAt((_local3 - 1)) > 32){
                    return (_arg1.substring(0, _local3));
                };
                _local3--;
            };
            return ("");
        }
        public static function endsWith(_arg1:String, _arg2:String):Boolean{
            return ((_arg2 == _arg1.substring((_arg1.length - _arg2.length))));
        }
        public static function stringHasValue(_arg1:String):Boolean{
            return (((!((_arg1 == null))) && ((_arg1.length > 0))));
        }
        public static function remove(_arg1:String, _arg2:String):String{
            return (StringUtil.replace(_arg1, _arg2, ""));
        }
        public static function ltrim(_arg1:String):String{
            var _local2:Number = _arg1.length;
            var _local3:Number = 0;
            while (_local3 < _local2) {
                if (_arg1.charCodeAt(_local3) > 32){
                    return (_arg1.substring(_local3));
                };
                _local3++;
            };
            return ("");
        }

    }
}//package com.adobe.utils 
