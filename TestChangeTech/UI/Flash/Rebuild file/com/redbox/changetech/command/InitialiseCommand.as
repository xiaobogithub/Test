//Created by Action Script Viewer - http://www.buraks.com/asv
package com.redbox.changetech.command {
    import mx.controls.*;
    import mx.rpc.events.*;
    import com.redbox.changetech.control.*;
    import com.redbox.changetech.model.*;
    import com.redbox.changetech.vo.*;
    import mx.rpc.*;
    import com.adobe.cairngorm.control.*;
    import com.adobe.cairngorm.commands.*;
    import com.redbox.changetech.business.*;

    public class InitialiseCommand implements ICommand, IResponder {

        public static var PUBLIC:String = "PUBLIC";
        public static var TEST:String = "TEST";
        public static var PREVIEW:String = "PREVIEW";
        public static var DAILY:String = "DAILY";

        public function fault(_arg1:Object):void{
            BalanceModelLocator.getInstance().RPC_OperationInProgress = false;
            var _local2:FaultEvent = (_arg1 as FaultEvent);
            Alert.show(_local2.fault.toString(), "Error: InitialiseCommand");
        }
        public function result(_arg1:Object):void{
            var _local3:CairngormEvent;
            var _local4:CairngormEvent;
            var _local5:CairngormEvent;
            var _local6:CairngormEvent;
            var _local2:Boolean = (_arg1.result as Boolean);
            if (_local2){
                switch (BalanceModelLocator.getInstance().flashVars.mode){
                    case PREVIEW:
                        _local3 = new CairngormEvent(BalanceController.GET_PREVIEW);
                        _local3.data = BalanceModelLocator.getInstance().flashVars.previewCode;
                        _local3.dispatch();
                        break;
                    case DAILY:
                        _local4 = new CairngormEvent(BalanceController.GET_CONSUMER);
                        _local4.dispatch();
                        break;
                    case PUBLIC:
                        trace("dispatching get welcome");
                        _local5 = new CairngormEvent(BalanceController.START_SCREENING);
                        _local5.dispatch();
                        break;
                    case TEST:
                        _local6 = new CairngormEvent(BalanceController.GET_TEST_COMMAND);
                        _local6.data = BalanceModelLocator.getInstance().flashVars.previewCode;
                        _local6.dispatch();
                        break;
                };
            } else {
                Alert.show("There was a problem initialising the application");
            };
        }
        public function execute(_arg1:CairngormEvent):void{
            var _local2:ServiceDelegate = new ServiceDelegate(this);
            _local2.Initialise(BalanceModelLocator.getInstance().culture);
        }

    }
}//package com.redbox.changetech.command 
