//Created by Action Script Viewer - http://www.buraks.com/asv
package com.redbox.changetech.command {
    import com.redbox.changetech.model.*;
    import com.redbox.changetech.vo.*;
    import com.adobe.cairngorm.control.*;
    import com.adobe.cairngorm.commands.*;

    public class SelectDayCommand implements ICommand {

        public function execute(_arg1:CairngormEvent):void{
            BalanceModelLocator.getInstance().currentConsumptionVO = (_arg1.data as Consumption);
        }

    }
}//package com.redbox.changetech.command 
