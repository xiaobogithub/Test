//Created by Action Script Viewer - http://www.buraks.com/asv
package com.redbox.changetech.command {
    import mx.rpc.*;
    import com.adobe.cairngorm.control.*;
    import com.adobe.cairngorm.commands.*;
    import com.redbox.changetech.business.*;

    public class SetMobileNumberCommand implements ICommand, IResponder {

        private var callBackObject:Object
        private var callBack:Function

        public function fault(_arg1:Object):void{
        }
        public function execute(_arg1:CairngormEvent):void{
            callBack = _arg1.data.callBack;
            callBackObject = _arg1.data.callBackObject;
            var _local2:ServiceDelegate = new ServiceDelegate(this);
            _local2.SetMobileNumber(_arg1.data.mobileNumber);
        }
        public function result(_arg1:Object):void{
            callBack.apply(callBackObject);
        }

    }
}//package com.redbox.changetech.command 
