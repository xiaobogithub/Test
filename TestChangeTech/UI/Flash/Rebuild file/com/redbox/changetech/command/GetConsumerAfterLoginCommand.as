//Created by Action Script Viewer - http://www.buraks.com/asv
package com.redbox.changetech.command {
    import mx.controls.*;
    import com.redbox.changetech.control.*;
    import com.redbox.changetech.model.*;
    import com.redbox.changetech.vo.*;
    import mx.rpc.*;
    import com.adobe.cairngorm.control.*;
    import com.adobe.cairngorm.commands.*;
    import com.redbox.changetech.business.*;

    public class GetConsumerAfterLoginCommand implements ICommand, IResponder {

        public function fault(_arg1:Object):void{
        }
        public function result(_arg1:Object):void{
            var _local2:CairngormEvent;
            if (_arg1.result == null){
                Alert.show("There was a problem logging on: consumer came back null on GetConsumerAfterLoginCommand.");
            } else {
                BalanceModelLocator.getInstance().consumer = (_arg1.result as Consumer);
                _local2 = new CairngormEvent(BalanceController.START_DAY);
                _local2.dispatch();
            };
        }
        public function execute(_arg1:CairngormEvent):void{
            var _local2:ServiceDelegate = new ServiceDelegate(this);
            var _local3:Number = Number(BalanceModelLocator.getInstance().flashVars.dayOveride);
            _local2.GetConsumer(_local3, null);
        }

    }
}//package com.redbox.changetech.command 
