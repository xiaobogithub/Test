//Created by Action Script Viewer - http://www.buraks.com/asv
package com.redbox.changetech.command {
    import com.redbox.changetech.model.*;
    import mx.rpc.*;
    import com.adobe.cairngorm.control.*;
    import com.adobe.cairngorm.commands.*;
    import com.redbox.changetech.business.*;

    public class IsActiveSessionCommand implements ICommand, IResponder {

        public function fault(_arg1:Object):void{
        }
        public function result(_arg1:Object):void{
            var _local2:Boolean = (_arg1.result as Boolean);
            if (_local2){
            } else {
                trace("is active session false");
            };
        }
        public function execute(_arg1:CairngormEvent):void{
            var _local2:ServiceDelegate;
            if (!BalanceModelLocator.getInstance().RPC_OperationInProgress){
                _local2 = new ServiceDelegate(this);
                _local2.IsActiveSession();
            };
        }

    }
}//package com.redbox.changetech.command 
