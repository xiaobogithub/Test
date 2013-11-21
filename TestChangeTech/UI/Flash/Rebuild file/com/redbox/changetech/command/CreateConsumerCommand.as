//Created by Action Script Viewer - http://www.buraks.com/asv
package com.redbox.changetech.command {
    import mx.rpc.events.*;
    import com.redbox.changetech.control.*;
    import com.redbox.changetech.model.*;
    import com.redbox.changetech.vo.*;
    import mx.rpc.*;
    import com.adobe.cairngorm.control.*;
    import com.adobe.cairngorm.commands.*;
    import com.redbox.changetech.business.*;

    public class CreateConsumerCommand implements ICommand, IResponder {

        private var _viewCallback:Function
        private var model:BalanceModelLocator

        public function CreateConsumerCommand(){
            model = BalanceModelLocator.getInstance();
            super();
        }
        public function fault(_arg1:Object):void{
            BalanceModelLocator.getInstance().RPC_OperationInProgress = false;
            var _local2:FaultEvent = (_arg1 as FaultEvent);
            if (_local2.fault.faultString == "Consumer not found"){
            };
            _viewCallback(false);
        }
        public function result(_arg1:Object):void{
            BalanceModelLocator.getInstance().RPC_OperationInProgress = false;
            var _local2:Consumer = (_arg1.result as Consumer);
            BalanceModelLocator.getInstance().consumer = _local2;
            if (_viewCallback != null){
                _viewCallback(true);
            };
            reportUsage();
        }
        private function reportUsage():void{
            var _local1:CairngormEvent = new CairngormEvent(BalanceController.REPORT_USAGE);
            _local1.data = new Object();
            _local1.dispatch();
        }
        public function execute(_arg1:CairngormEvent):void{
            BalanceModelLocator.getInstance().RPC_OperationInProgress = true;
            if (_arg1.data.callback != null){
                _viewCallback = (_arg1.data.callback as Function);
            };
            var _local2:Consumer = new Consumer();
            _local2.Age = _arg1.data.Age;
            _local2.CurrentDate = new Date();
            _local2.CurrentDay = 0;
            _local2.EmailAddress = _arg1.data.EmailAddress;
            _local2.FirstName = _arg1.data.FirstName;
            if (model.flashVars.companyId != null){
                _local2.CompanyId = model.flashVars.companyId;
            };
            var _local3:ServiceDelegate = new ServiceDelegate(this);
            _local3.CreateConsumer(_local2);
        }

    }
}//package com.redbox.changetech.command 
