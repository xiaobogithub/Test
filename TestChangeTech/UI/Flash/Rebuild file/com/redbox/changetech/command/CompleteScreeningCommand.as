//Created by Action Script Viewer - http://www.buraks.com/asv
package com.redbox.changetech.command {
    import mx.controls.*;
    import mx.rpc.events.*;
    import com.redbox.changetech.model.*;
    import com.redbox.changetech.vo.*;
    import mx.rpc.*;
    import com.adobe.cairngorm.control.*;
    import com.adobe.cairngorm.commands.*;
    import com.redbox.changetech.business.*;

    public class CompleteScreeningCommand implements ICommand, IResponder {

        public function fault(_arg1:Object):void{
            var _local2:FaultEvent = (_arg1 as FaultEvent);
            Alert.show(_local2.fault.faultString, "Error");
        }
        public function result(_arg1:Object):void{
            BalanceModelLocator.getInstance().setRoomContent((_arg1.result as ContentCollection));
        }
        public function execute(_arg1:CairngormEvent):void{
            var _local2:ServiceDelegate = new ServiceDelegate(this);
            _arg1.data.NextType = "Registration";
            _local2.CompleteScreening(_arg1.data.response);
        }

    }
}//package com.redbox.changetech.command 
