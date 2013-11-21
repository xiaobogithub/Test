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

    public class StartScreeningCommand implements ICommand, IResponder {

        private var model:BalanceModelLocator

        public function StartScreeningCommand(){
            model = BalanceModelLocator.getInstance();
            super();
        }
        public function fault(_arg1:Object):void{
            var _local2:FaultEvent = (_arg1 as FaultEvent);
            Alert.show(_local2.fault.faultString, "Error");
        }
        public function result(_arg1:Object):void{
            model.setRoomContent(_arg1.result);
        }
        public function execute(_arg1:CairngormEvent):void{
            model.consumer = new Consumer();
            var _local2:ServiceDelegate = new ServiceDelegate(this);
            _local2.StartScreening(new Date());
        }

    }
}//package com.redbox.changetech.command 
