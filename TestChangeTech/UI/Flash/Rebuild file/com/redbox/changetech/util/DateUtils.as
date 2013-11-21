//Created by Action Script Viewer - http://www.buraks.com/asv
package com.redbox.changetech.util {
    import com.redbox.changetech.util.Enumerations.*;

    public class DateUtils {

        public static function getWeekFromIndex(_arg1:Number):String{
            var _local2:Array = [DayOfWeek.Monday.Text, DayOfWeek.Tuesday.Text, DayOfWeek.Wednesday.Text, DayOfWeek.Wednesday.Text, DayOfWeek.Thursday.Text, DayOfWeek.Friday.Text, DayOfWeek.Saturday.Text];
            return (_local2[_arg1]);
        }
        public static function getMonthFromIndex(_arg1:Number):String{
            var _local2:Array = [Months.January.Text, Months.February.Text, Months.March.Text, Months.April.Text, Months.May.Text, Months.June.Text, Months.July.Text, Months.August.Text, Months.September.Text, Months.October.Text, Months.November.Text, Months.December.Text];
            trace(("Months.May.Text=" + Months.May.Text));
            return (_local2[_arg1]);
        }

    }
}//package com.redbox.changetech.util 
