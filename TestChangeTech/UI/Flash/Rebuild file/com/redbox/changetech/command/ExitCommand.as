//Created by Action Script Viewer - http://www.buraks.com/asv
package com.redbox.changetech.command {
    import mx.controls.*;
    import mx.rpc.events.*;
    import com.redbox.changetech.model.*;
    import mx.rpc.*;
    import com.adobe.cairngorm.control.*;
    import com.redbox.changetech.util.*;
    import com.adobe.cairngorm.commands.*;
    import com.redbox.changetech.business.*;

    public class ExitCommand implements ICommand, IResponder {

        public function fault(_arg1:Object):void{
            BalanceModelLocator.getInstance().RPC_OperationInProgress = false;
            var _local2:FaultEvent = (_arg1 as FaultEvent);
            Alert.show(_local2.fault.faultString, "Error");
        }
        public function result(_arg1:Object):void{
            var _local2:JavaScriptUtils = new JavaScriptUtils();
            _local2.closeWindow();
        }
        public function execute(_arg1:CairngormEvent):void{
            var _local2:ServiceDelegate = new ServiceDelegate(this);
            _local2.Exit();
        }

    }
}//package com.redbox.changetech.command 
