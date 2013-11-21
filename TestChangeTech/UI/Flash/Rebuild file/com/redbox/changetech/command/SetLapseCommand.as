//Created by Action Script Viewer - http://www.buraks.com/asv
package com.redbox.changetech.command {
    import com.redbox.changetech.model.*;
    import mx.rpc.*;
    import com.adobe.cairngorm.control.*;
    import com.adobe.cairngorm.commands.*;
    import com.redbox.changetech.business.*;

    public class SetLapseCommand implements ICommand, IResponder {

        private var _viewCallback:Function

        public function fault(_arg1:Object):void{
        }
        public function execute(_arg1:CairngormEvent):void{
            var _local2:ServiceDelegate;
            if (!BalanceModelLocator.getInstance().RPC_OperationInProgress){
                BalanceModelLocator.getInstance().RPC_OperationInProgress = true;
                _viewCallback = (_arg1.data.callback as Function);
                _local2 = new ServiceDelegate(this);
                _local2.SetLapse();
            };
        }
        public function result(_arg1:Object):void{
            if (_viewCallback != null){
                _viewCallback();
            };
        }

    }
}//package com.redbox.changetech.command 
