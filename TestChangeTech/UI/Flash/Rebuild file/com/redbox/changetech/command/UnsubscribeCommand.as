//Created by Action Script Viewer - http://www.buraks.com/asv
package com.redbox.changetech.command {
    import mx.controls.*;
    import mx.rpc.events.*;
    import com.redbox.changetech.vo.*;
    import mx.rpc.*;
    import com.adobe.cairngorm.control.*;
    import com.adobe.cairngorm.commands.*;
    import com.redbox.changetech.business.*;

    public class UnsubscribeCommand implements ICommand, IResponder {

        private var _viewCallback:Function

        public function fault(_arg1:Object):void{
            var _local2:FaultEvent = (_arg1 as FaultEvent);
            Alert.show(_local2.fault.faultString, "Error");
        }
        public function result(_arg1:Object):void{
            if (((!((_arg1 == null))) && (_viewCallback))){
                _viewCallback(_arg1.result);
            };
        }
        public function execute(_arg1:CairngormEvent):void{
            var _local2:ServiceDelegate = new ServiceDelegate(this);
            _viewCallback = _arg1.data.callback;
            var _local3:* = new Object();
            _local3.result = true;
            result(_local3);
        }

    }
}//package com.redbox.changetech.command 
