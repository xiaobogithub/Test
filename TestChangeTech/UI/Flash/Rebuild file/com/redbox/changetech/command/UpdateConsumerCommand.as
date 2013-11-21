//Created by Action Script Viewer - http://www.buraks.com/asv
package com.redbox.changetech.command {
    import mx.controls.*;
    import mx.rpc.events.*;
    import com.redbox.changetech.model.*;
    import com.redbox.changetech.vo.*;
    import mx.rpc.*;
    import com.adobe.cairngorm.control.*;
    import mx.utils.*;
    import com.adobe.cairngorm.commands.*;
    import com.redbox.changetech.business.*;

    public class UpdateConsumerCommand implements ICommand, IResponder {

        private var _viewCallback:Function
        private var model:BalanceModelLocator

        public function UpdateConsumerCommand(){
            model = BalanceModelLocator.getInstance();
            super();
        }
        public function execute(_arg1:CairngormEvent):void{
            BalanceModelLocator.getInstance().RPC_OperationInProgress = true;
            var _local2:Consumer = new Consumer();
            _local2 = Consumer(ObjectUtil.copy(model.consumer));
            _local2.EmailAddress = _arg1.data.EmailAddress;
            _local2.FirstName = _arg1.data.FirstName;
            _local2.MobilePhoneNumber = _arg1.data.MobilePhoneNumber;
            if (_arg1.data.callback != null){
                _viewCallback = (_arg1.data.callback as Function);
            };
            var _local3:ServiceDelegate = new ServiceDelegate(this);
            _local3.UpdateConsumer(model.consumer);
        }
        public function fault(_arg1:Object):void{
            BalanceModelLocator.getInstance().RPC_OperationInProgress = false;
            var _local2:FaultEvent = (_arg1 as FaultEvent);
            Alert.show(_local2.fault.faultString);
            if (_local2.fault.faultString == "Consumer not found"){
            };
            _viewCallback(false);
        }
        public function result(_arg1:Object):void{
            trace(("callback is " + _viewCallback));
            if (_viewCallback != null){
                _viewCallback(_arg1.result);
            };
        }

    }
}//package com.redbox.changetech.command 
