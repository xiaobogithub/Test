//Created by Action Script Viewer - http://www.buraks.com/asv
package com.redbox.changetech.business {
    import mx.rpc.*;

    public class SecurityServiceDelegate {

        private var service:NcSecurityService

        public function SecurityServiceDelegate(_arg1:IResponder){
            service = new NcSecurityService(_arg1);
        }
        public function Logout():void{
        }
        public function setCredentials(_arg1:String, _arg2:String):void{
            service.CheckCredentials(_arg1, _arg2);
        }

    }
}//package com.redbox.changetech.business 
