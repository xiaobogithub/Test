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

    public class NcService {

        private var responder:Responder
        private var gateway:String
        private var command:IResponder
        private var connection:NetConnection
        private var model:BalanceModelLocator

        public function NcService(_arg1:IResponder){
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
        public function onFault(_arg1:Object):void{
            switch (_arg1.description){
                case "9000":
                    Alert.show(model.languageVO.getLang("email_address_already_exists"));
                    break;
                case "9999":
                    Alert.show(model.languageVO.getLang("error logging on"));
                    break;
                default:
                    Alert.show(((_arg1.description + ":") + _arg1.details), "Error");
            };
            model.RPC_OperationInProgress = false;
            disConnect();
        }
        public function SetMobileNumber(_arg1:String):void{
            connection.call("Redbox.ChangeTech.Integration.Controller.SetMobileNumber", responder, _arg1);
        }
        public function IsActiveSession():void{
            connection.call("Redbox.ChangeTech.Integration.Controller.IsActiveSession", responder);
        }
        public function ResetLogin(_arg1:String):void{
            trace(("NcService.ResetLogin(username)..." + _arg1));
            connection.call("Redbox.ChangeTech.Integration.Controller.ResetLogin", responder, _arg1);
        }
        public function GetWelcome(_arg1:Date):void{
            connection.call("Redbox.ChangeTech.Integration.Controller.GetWelcome", responder, _arg1);
        }
        public function SetHappinessScore(_arg1:Number):void{
            connection.call("Redbox.ChangeTech.Integration.Controller.SetHappinessScore", responder, _arg1);
        }
        public function UpdateConsumer(_arg1:Consumer):void{
            connection.call("Redbox.ChangeTech.Integration.Controller.UpdateConsumer", responder, _arg1);
        }
        public function SendSms(_arg1:String, _arg2:String):void{
            connection.call("Redbox.ChangeTech.Integration.Controller.SendSms", responder, _arg1, _arg2);
        }
        public function SetPlan(_arg1:Array):void{
            connection.call("Redbox.ChangeTech.Integration.Controller.SetPlan", responder, _arg1);
        }
        public function GetConsumer(_arg1:Number, _arg2:String):void{
            connection.call("Redbox.ChangeTech.Integration.Controller.GetConsumer", responder, _arg1, _arg2);
        }
        public function Report(_arg1:Array):void{
            connection.call("Redbox.ChangeTech.Integration.Controller.Report", responder, _arg1);
        }
        public function Initialise(_arg1:String):void{
            connection.call("Redbox.ChangeTech.Integration.Controller.Initialise", responder, _arg1);
        }
        public function StartScreening(_arg1:Date):void{
            connection.call("Redbox.ChangeTech.Integration.Controller.StartScreening", responder, _arg1);
        }
        public function onResult(_arg1:Object):void{
            model.RPC_OperationInProgress = false;
            var _local2:Object = new Object();
            _local2.result = _arg1;
            command.result(_local2);
            disConnect();
        }
        public function CompleteScreening(_arg1:Response):void{
            connection.call("Redbox.ChangeTech.Integration.Controller.CompleteScreening", responder, _arg1);
        }
        public function CollectionComplete(_arg1:Response):void{
            connection.call("Redbox.ChangeTech.Integration.Controller.CollectionComplete", responder, _arg1);
        }
        public function Exit():void{
            connection.call("Redbox.ChangeTech.Integration.Controller.Exit", responder);
        }
        public function SetLapse():void{
            connection.call("Redbox.ChangeTech.Integration.Controller.SetLapse", responder);
        }
        public function GetTest(_arg1:String):void{
            connection.call("Redbox.ChangeTech.Integration.Controller.GetTest", responder, _arg1);
        }
        public function Logout():void{
            connection.call("Redbox.ChangeTech.Integration.Controller.Logout", responder);
        }
        public function SetScreeningScore(_arg1:Number):void{
            trace("calling set screening score in nc service delegate");
            connection.call("Redbox.ChangeTech.Integration.Controller.SetScreeningScore", responder, _arg1);
        }
        public function Print():void{
            connection.call("Redbox.ChangeTech.Integration.Controller.Print", responder);
        }
        public function Unsubscribe():void{
            connection.call("Redbox.ChangeTech.Integration.Controller.Unsubscribe", responder);
        }
        public function GetPreview(_arg1:String):void{
            connection.call("Redbox.ChangeTech.Integration.Controller.GetPreview", responder, _arg1);
        }
        public function SetCompletionScore(_arg1:Number):void{
            connection.call("Redbox.ChangeTech.Integration.Controller.SetCompletionScore", responder, _arg1);
        }
        public function SetScreeningGender(_arg1:String):void{
            connection.call("Redbox.ChangeTech.Integration.Controller.SetScreeningGender", responder, _arg1);
        }
        public function CreateConsumer(_arg1:Consumer):void{
            connection.call("Redbox.ChangeTech.Integration.Controller.CreateConsumer", responder, _arg1);
        }
        public function StartDay(_arg1:Date):void{
            connection.call("Redbox.ChangeTech.Integration.Controller.StartDay", responder, _arg1);
        }
        private function disConnect():void{
            CursorManager.removeAllCursors();
            connection.close();
        }
        public function SetConsumerTrack1(_arg1:String):void{
            connection.call("Redbox.ChangeTech.Integration.Controller.SetConsumerTrack1", responder, _arg1);
        }
        public function SetConsumerTrack2(_arg1:String):void{
            connection.call("Redbox.ChangeTech.Integration.Controller.SetConsumerTrack2", responder, _arg1);
        }

    }
}//package com.redbox.changetech.business 
