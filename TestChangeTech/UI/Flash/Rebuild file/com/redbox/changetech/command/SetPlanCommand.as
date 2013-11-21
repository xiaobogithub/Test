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

    public class SetPlanCommand implements ICommand, IResponder {

        private var _viewCallback:Function

        public function fault(_arg1:Object):void{
            BalanceModelLocator.getInstance().RPC_OperationInProgress = false;
            var _local2:FaultEvent = (_arg1 as FaultEvent);
            Alert.show(_local2.fault.faultString, "Error");
        }
        public function result(_arg1:Object):void{
            _viewCallback();
        }
        public function execute(_arg1:CairngormEvent):void{
            var _local2:Array;
            var _local3:ServiceDelegate;
            if (!BalanceModelLocator.getInstance().RPC_OperationInProgress){
                BalanceModelLocator.getInstance().RPC_OperationInProgress = true;
                _viewCallback = (_arg1.data.callback as Function);
                _local2 = BalanceModelLocator.getInstance().consumer.ReductionPlan;
                _local3 = new ServiceDelegate(this);
                _local3.SetPlan(_local2);
            };
        }

    }
}//package com.redbox.changetech.command 
