//Created by Action Script Viewer - http://www.buraks.com/asv
package com.redbox.changetech.business {
    import com.redbox.changetech.vo.*;
    import mx.rpc.*;

    public class ServiceDelegate {

        private var service:NcService

        public function ServiceDelegate(_arg1:IResponder){
            service = new NcService(_arg1);
        }
        public function ResetLogin(_arg1:String):void{
            service.ResetLogin(_arg1);
        }
        public function Logout():void{
            service.Logout();
        }
        public function Exit():void{
            service.Exit();
        }
        public function IsActiveSession():void{
            service.IsActiveSession();
        }
        public function SetLapse():void{
            service.SetLapse();
        }
        public function GetConsumer(_arg1:Number, _arg2:String):void{
            service.GetConsumer(_arg1, _arg2);
        }
        public function SetHappinessScore(_arg1:Number):void{
            service.SetHappinessScore(_arg1);
        }
        public function UpdateConsumer(_arg1:Consumer):void{
            service.UpdateConsumer(_arg1);
        }
        public function SendSms(_arg1:String, _arg2:String):void{
            service.SendSms(_arg1, _arg2);
        }
        public function SetPlan(_arg1:Array):void{
            service.SetPlan(_arg1);
        }
        public function GetWelcome(_arg1:Date):void{
            service.GetWelcome(_arg1);
        }
        public function StartScreening(_arg1:Date):void{
            service.StartScreening(_arg1);
        }
        public function Initialise(_arg1:String):void{
            service.Initialise(_arg1);
        }
        public function Report(_arg1:Array):void{
            service.Report(_arg1);
        }
        public function Print():void{
            service.Print();
        }
        public function CompleteScreening(_arg1:Response):void{
            service.CompleteScreening(_arg1);
        }
        public function SetScreeningScore(_arg1:Number):void{
            service.SetScreeningScore(_arg1);
        }
        public function CollectionComplete(_arg1:Response):void{
            service.CollectionComplete(_arg1);
        }
        public function GetTest(_arg1:String):void{
            service.GetTest(_arg1);
        }
        public function SetMobileNumber(_arg1:String):void{
            service.SetMobileNumber(_arg1);
        }
        public function Unsubscribe():void{
            service.Unsubscribe();
        }
        public function GetPreview(_arg1:String):void{
            service.GetPreview(_arg1);
        }
        public function SetCompletionScore(_arg1:Number):void{
            service.SetCompletionScore(_arg1);
        }
        public function CreateConsumer(_arg1:Consumer):void{
            service.CreateConsumer(_arg1);
        }
        public function StartDay(_arg1:Date):void{
            service.StartDay(_arg1);
        }
        public function SetScreeningGender(_arg1:String):void{
            service.SetScreeningGender(_arg1);
        }
        public function SetConsumerTrack1(_arg1:String):void{
            service.SetConsumerTrack1(_arg1);
        }
        public function SetConsumerTrack2(_arg1:String):void{
            service.SetConsumerTrack2(_arg1);
        }

    }
}//package com.redbox.changetech.business 
