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

    public class ResetLoginCommand implements ICommand, IResponder {

        private var _viewCallback:Function

        public function fault(_arg1:Object):void{
            BalanceModelLocator.getInstance().RPC_OperationInProgress = false;
            var _local2:FaultEvent = (_arg1 as FaultEvent);
            if (_local2.fault.faultString == "Consumer not found"){
                Alert.show("Account details not found:please create an account first.");
            } else {
                Alert.show("There was an error resetting your login.");
            };
            _viewCallback(false);
        }
        public function result(_arg1:Object):void{
            _viewCallback(_arg1);
        }
        public function execute(_arg1:CairngormEvent):void{
            _viewCallback = (_arg1.data.callback as Function);
            trace(("reset login username " + _arg1.data.username));
            var _local2:ServiceDelegate = new ServiceDelegate(this);
            _local2.ResetLogin(_arg1.data.username);
        }

    }
}//package com.redbox.changetech.command 
