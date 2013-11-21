//Created by Action Script Viewer - http://www.buraks.com/asv
package com.redbox.changetech.business {
    import mx.managers.*;
    import flash.events.*;
    import mx.controls.*;
    import com.redbox.changetech.model.*;
    import com.redbox.changetech.vo.*;
    import mx.rpc.*;
    import flash.net.*;
    import com.redbox.changetech.util.Enumerations.*;

    public class NcSecurityService {

        private var responder:Responder
        private var gateway:String
        private var model:BalanceModelLocator
        private var command:IResponder
        private var connection:NetConnection

        public function NcSecurityService(_arg1:IResponder){
            model = BalanceModelLocator.getInstance();
            super();
            gateway = model.gateway;
            command = _arg1;
            responder = new Responder(onResult, onFault);
            connection = new NetConnection();
            connection.addEventListener(NetStatusEvent.NET_STATUS, netStatusErrorEvent);
            connection.connect(gateway);
            model.RPC_OperationInProgress = true;
            CursorManager.setBusyCursor();
        }
        private function netStatusErrorEvent(_arg1:NetStatusEvent):void{
            if (_arg1.info.level == "error"){
                Alert.show(_arg1.info.code, "Error");
                model.RPC_OperationInProgress = false;
                disConnect();
            };
        }
        public function onResult(_arg1:Object):void{
            var _local2:Object = new Object();
            _local2.result = _arg1;
            command.result(_local2);
            model.RPC_OperationInProgress = false;
            disConnect();
        }
        public function Logout():void{
        }
        public function CheckCredentials(_arg1:String, _arg2:String):void{
            connection.call("Redbox.ChangeTech.Integration.Security.Authenticate.CheckCredentials", responder, _arg1, _arg2, null);
        }
        private function disConnect():void{
            CursorManager.removeAllCursors();
            connection.close();
        }
        public function onFault(_arg1:Object):void{
            switch (_arg1.code){
                case 9000:
                    Alert.show(model.languageVO.getLang("email_address_already_exists"));
                    break;
                case 9999:
                    Alert.show(model.languageVO.getLang("error_logging_on"));
                    break;
                default:
                    Alert.show(((_arg1.description + ":") + _arg1.details), "Error");
            };
            model.RPC_OperationInProgress = false;
            disConnect();
        }

    }
}//package com.redbox.changetech.business 
