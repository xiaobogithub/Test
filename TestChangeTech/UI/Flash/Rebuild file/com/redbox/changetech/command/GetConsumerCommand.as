//Created by Action Script Viewer - http://www.buraks.com/asv
package com.redbox.changetech.command {
    import com.redbox.changetech.model.*;
    import com.redbox.changetech.vo.*;
    import mx.rpc.*;
    import com.adobe.cairngorm.control.*;
    import com.adobe.cairngorm.commands.*;
    import com.redbox.changetech.business.*;

    public class GetConsumerCommand implements ICommand, IResponder {

        private var model:BalanceModelLocator

        public function GetConsumerCommand(){
            model = BalanceModelLocator.getInstance();
            super();
        }
        public function fault(_arg1:Object):void{
        }
        public function execute(_arg1:CairngormEvent):void{
            var _local2:ServiceDelegate = new ServiceDelegate(this);
            var _local3:Number = Number(BalanceModelLocator.getInstance().flashVars.dayOveride);
            trace(("dayOveride=" + _local3));
            if (model.flashVars.forceUserId == null){
                _local2.GetConsumer(_local3, null);
            } else {
                _local2.GetConsumer(_local3, model.flashVars.forceUserId);
            };
        }
        public function result(_arg1:Object):void{
            if (_arg1.result == null){
                model.isFullLoginRequired = true;
            } else {
                model.isFullLoginRequired = false;
                model.consumer = (_arg1.result as Consumer);
            };
            model.showLogin = true;
        }

    }
}//package com.redbox.changetech.command 
