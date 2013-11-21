//Created by Action Script Viewer - http://www.buraks.com/asv
package com.redbox.changetech.command {
    import mx.controls.*;
    import mx.rpc.events.*;
    import com.redbox.changetech.control.*;
    import com.redbox.changetech.model.*;
    import com.redbox.changetech.vo.*;
    import mx.rpc.*;
    import com.adobe.cairngorm.control.*;
    import com.adobe.cairngorm.commands.*;
    import com.redbox.changetech.business.*;

    public class StartDayCommand implements ICommand, IResponder {

        public function fault(_arg1:Object):void{
            var _local2:FaultEvent = (_arg1 as FaultEvent);
            Alert.show(_local2.fault.faultString, "Error");
        }
        public function result(_arg1:Object):void{
            BalanceModelLocator.getInstance().setRoomContent((_arg1.result as ContentCollection));
        }
        public function execute(_arg1:CairngormEvent):void{
            var _local2:ServiceDelegate = new ServiceDelegate(this);
            _local2.StartDay(new Date());
            var _local3:CairngormEvent = new CairngormEvent(BalanceController.HIDE_LOGIN_DIALOG);
            _local3.dispatch();
        }

    }
}//package com.redbox.changetech.command 
