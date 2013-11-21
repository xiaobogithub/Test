//Created by Action Script Viewer - http://www.buraks.com/asv
package com.redbox.changetech.vo {
    import com.redbox.changetech.util.*;

    public class LanguageVo {

        private var xliff:XML
        public var helpData:Array

        public function LanguageVo(_arg1:XML){
            xliff = _arg1;
            helpData = getHelp();
        }
        public function getLang(_arg1:String):String{
            var aResName:* = _arg1;
            return (xliff.file.body["trans-unit"].(@resname == aResName).source);
        }
        private function getHelp():Array{
            var _local2:*;
            var _local3:Object;
            var _local1:Array = [];
            trace(("xliff.file.structured.help=" + xliff.file.structured.help));
            for each (_local2 in xliff.file.structured.help.item) {
                _local3 = new Object();
                _local3.header = _local2.header;
                _local3.info = _local2.info;
                _local1.push(_local3);
            };
            return (_local1);
        }
        public function getLocalDateString(_arg1:Date):String{
            var _local2:Number = _arg1.day;
            var _local3:Number = _arg1.date;
            var _local4:Number = _arg1.month;
            var _local5:Number = _arg1.fullYear;
            var _local6:String = getLang(DateUtils.getWeekFromIndex(_local2)).substr(0, 3);
            var _local7:String = getLang(DateUtils.getMonthFromIndex(_local4));
            return (((((((_local6 + " ") + _local3) + " ") + _local7) + " ") + _local5));
        }

    }
}//package com.redbox.changetech.vo 
