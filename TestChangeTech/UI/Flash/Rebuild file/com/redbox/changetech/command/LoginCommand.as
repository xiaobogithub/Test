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
    import com.adobe.crypto.*;

    public class LoginCommand implements ICommand, IResponder {

        private var _viewCallback:Function

        public function fault(_arg1:Object):void{
            BalanceModelLocator.getInstance().RPC_OperationInProgress = false;
            var _local2:FaultEvent = (_arg1 as FaultEvent);
            if (_local2.fault.faultString == "Consumer not found"){
                Alert.show(BalanceModelLocator.getInstance().languageVO.getLang("login_notfound"));
            } else {
                Alert.show(BalanceModelLocator.getInstance().languageVO.getLang("login_error_general"));
            };
            _viewCallback(false);
        }
        public function result(_arg1:Object):void{
            var _local2:CairngormEvent = new CairngormEvent(BalanceController.HIDE_LOGIN_DIALOG);
            _local2.dispatch();
            var _local3:CairngormEvent = new CairngormEvent(BalanceController.GET_CONSUMER_AFTER_LOGIN);
            _local3.dispatch();
            _viewCallback(true);
        }
        public function execute(_arg1:CairngormEvent):void{
            _viewCallback = (_arg1.data.callback as Function);
            trace(((((("username/pwd " + _arg1.data.username) + " ") + MD5.hash(_arg1.data.password)) + "--") + _arg1.data.password));
            var _local2:SecurityServiceDelegate = new SecurityServiceDelegate(this);
            _local2.setCredentials(_arg1.data.username, MD5.hash(_arg1.data.password));
        }

    }
}//package com.redbox.changetech.command 
