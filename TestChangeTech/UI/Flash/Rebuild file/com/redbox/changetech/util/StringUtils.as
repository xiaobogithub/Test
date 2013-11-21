//Created by Action Script Viewer - http://www.buraks.com/asv
package com.redbox.changetech.util {

    public class StringUtils {

        public function trimBack(_arg1:String, _arg2:String):String{
            if (!_arg1){
                return ("");
            };
            _arg2 = stringToCharacter(_arg2);
            if (_arg1.charAt((_arg1.length - 1)) == _arg2){
                _arg1 = trimBack(_arg1.substring(0, (_arg1.length - 1)), _arg2);
            };
            return (_arg1);
        }
        public function trim(_arg1:String, _arg2:String):String{
            return (trimBack(trimFront(_arg1, _arg2), _arg2));
        }
        public function replace(_arg1:String, _arg2:String, _arg3:String):String{
            return (_arg1.split(_arg2).join(_arg3));
        }
        public function stringToCharacter(_arg1:String):String{
            if (_arg1.length == 1){
                return (_arg1);
            };
            return (_arg1.slice(0, 1));
        }
        public function trimFront(_arg1:String, _arg2:String):String{
            if (!_arg1){
                return ("");
            };
            _arg2 = stringToCharacter(_arg2);
            if (_arg1.charAt(0) == _arg2){
                _arg1 = trimFront(_arg1.substring(1), _arg2);
            };
            return (_arg1);
        }

    }
}//package com.redbox.changetech.util 
