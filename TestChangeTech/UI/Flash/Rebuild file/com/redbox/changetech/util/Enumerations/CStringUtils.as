//Created by Action Script Viewer - http://www.buraks.com/asv
package com.redbox.changetech.util.Enumerations {
    import flash.utils.*;

    public class CStringUtils {

        public static function InitEnumConstants(_arg1):void{
            var _local3:XML;
            var _local2:XML = describeType(_arg1);
            for each (_local3 in _local2.constant) {
                _arg1[_local3.@name].Text = _local3.@name;
            };
        }

    }
}//package com.redbox.changetech.util.Enumerations 
