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

    public class CollectionCompleteCommand implements ICommand, IResponder {

        public function fault(_arg1:Object):void{
            BalanceModelLocator.getInstance().RPC_OperationInProgress = false;
            var _local2:FaultEvent = (_arg1 as FaultEvent);
            Alert.show(_local2.fault.faultString, "Error");
        }
        public function result(_arg1:Object):void{
            if (_arg1 == null){
                return;
            };
            BalanceModelLocator.getInstance().RPC_OperationInProgress = false;
            var _local2:ContentCollection = (_arg1.result as ContentCollection);
            BalanceModelLocator.getInstance().setRoomContent(_local2);
        }
        private function sendResponse(_arg1:Response):void{
            var _local2:ServiceDelegate = new ServiceDelegate(this);
            _local2.CollectionComplete(_arg1);
        }
        public function execute(_arg1:CairngormEvent):void{
            var _local2:Response;
            if (!BalanceModelLocator.getInstance().RPC_OperationInProgress){
                _local2 = Response(_arg1.data.response);
                sendResponse(_local2);
            };
        }

    }
}//package com.redbox.changetech.command 
