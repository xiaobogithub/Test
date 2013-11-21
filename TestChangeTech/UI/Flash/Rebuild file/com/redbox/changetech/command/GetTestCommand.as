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

    public class GetTestCommand implements ICommand, IResponder {

        private var testString:String

        public function fault(_arg1:Object):void{
            var _local2:FaultEvent = (_arg1 as FaultEvent);
            Alert.show(_local2.fault.faultString, "Error");
        }
        public function execute(_arg1:CairngormEvent):void{
            var _local2:ServiceDelegate = new ServiceDelegate(this);
            testString = _arg1.data;
            _local2.GetTest(testString);
        }
        public function result(_arg1:Object):void{
            if (_arg1.result == null){
                Alert.show((("Null value returned for GetTest(" + testString) + ")"));
            } else {
                BalanceModelLocator.getInstance().setRoomContent((_arg1.result as ContentCollection));
            };
        }

    }
}//package com.redbox.changetech.command 
