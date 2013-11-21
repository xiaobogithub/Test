//Created by Action Script Viewer - http://www.buraks.com/asv
package com.redbox.changetech.command {
    import com.redbox.changetech.model.*;
    import com.redbox.changetech.vo.*;
    import com.adobe.cairngorm.control.*;
    import com.adobe.cairngorm.commands.*;

    public class SelectNextDayCommand implements ICommand {

        public function execute(_arg1:CairngormEvent):void{
            var _local2:Consumption = (_arg1.data as Consumption);
            var _local3:Array = BalanceModelLocator.getInstance().consumer.ReportedUsage;
            var _local4:Number = 0;
            while (_local4 < _local3.length) {
                if (_local3[_local4] == _local2){
                    break;
                };
                _local4++;
            };
            var _local5:Number = (((_local4 + 1))==_local3.length) ? (_local3.length - 1) : (_local4 + 1);
            BalanceModelLocator.getInstance().currentConsumptionVO = _local3[_local5];
        }

    }
}//package com.redbox.changetech.command 
