﻿//Created by Action Script Viewer - http://www.buraks.com/asv
package mx.collections {

    public class CursorBookmark {

        private var _value:Object

        mx_internal static const VERSION:String = "3.2.0.3958";

        private static var _first:CursorBookmark;
        private static var _last:CursorBookmark;
        private static var _current:CursorBookmark;

        public function CursorBookmark(_arg1:Object){
            _value = _arg1;
        }
        public function get value():Object{
            return (_value);
        }
        public function getViewIndex():int{
            return (-1);
        }

        public static function get LAST():CursorBookmark{
            if (!_last){
                _last = new CursorBookmark("${L}");
            };
            return (_last);
        }
        public static function get FIRST():CursorBookmark{
            if (!_first){
                _first = new CursorBookmark("${F}");
            };
            return (_first);
        }
        public static function get CURRENT():CursorBookmark{
            if (!_current){
                _current = new CursorBookmark("${C}");
            };
            return (_current);
        }

    }
}//package mx.collections 
