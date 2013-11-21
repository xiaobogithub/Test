//Created by Action Script Viewer - http://www.buraks.com/asv
package com.redbox.changetech.util {
    import com.redbox.changetech.vo.*;
    import mx.collections.*;
    import com.redbox.changetech.util.Enumerations.*;

    public class WeekDayUtils {

        public static function previousWeekday(_arg1:String):String{
            var _local2:Number = 0;
            while (_local2 < Config.WEEKDAYS.length) {
                if (_arg1 == Config.WEEKDAYS[_local2]){
                    break;
                };
                _local2++;
            };
            var _local3:Number = (((_local2 - 1))<0) ? (Config.WEEKDAYS.length - 1) : (_local2 - 1);
            return (Config.WEEKDAYS[_local3]);
        }
        public static function getDayOfWeekString(_arg1:Number):String{
            var _local2:Array = [DayOfWeek.Sunday.Text, DayOfWeek.Monday.Text, DayOfWeek.Tuesday.Text, DayOfWeek.Wednesday.Text, DayOfWeek.Thursday.Text, DayOfWeek.Friday.Text, DayOfWeek.Saturday.Text];
            if (_arg1 >= _local2.length){
                _arg1 = 0;
            };
            if (_arg1 == -1){
                _arg1 = (_local2.length - 1);
            };
            return (_local2[_arg1]);
        }
        public static function nextWeekday(_arg1:String):String{
            var _local2:Number = 0;
            while (_local2 < Config.WEEKDAYS.length) {
                if (_arg1 == Config.WEEKDAYS[_local2]){
                    break;
                };
                _local2++;
            };
            var _local3:Number = (((_local2 + 1))==Config.WEEKDAYS.length) ? 0 : (_local2 + 1);
            return (Config.WEEKDAYS[_local3]);
        }
        public static function getWeekdayFromIndex(_arg1:Number):String{
            return (Config.WEEKDAYS[_arg1]);
        }
        public static function getItemByDayOfWeek(_arg1:String, _arg2:ArrayCollection):Consumption{
            var _local4:Consumption;
            var _local3:Number = 0;
            while (_local3 < _arg2.length) {
                _local4 = (_arg2.getItemAt(_local3) as Consumption);
                if (_local4.DayOfWeek == _arg1){
                    return (_local4);
                };
                _local3++;
            };
            return (null);
        }
        public static function getIndexFromWeekday(_arg1:String):Number{
            var _local2:Number = 0;
            while (_local2 < Config.WEEKDAYS.length) {
                if (Config.WEEKDAYS[_local2] == _arg1){
                    break;
                };
                _local2++;
            };
            return (_local2);
        }

    }
}//package com.redbox.changetech.util 
