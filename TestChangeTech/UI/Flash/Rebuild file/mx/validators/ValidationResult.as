//Created by Action Script Viewer - http://www.buraks.com/asv
package mx.validators {

    public class ValidationResult {

        public var subField:String
        public var errorCode:String
        public var isError:Boolean
        public var errorMessage:String

        mx_internal static const VERSION:String = "3.2.0.3958";

        public function ValidationResult(_arg1:Boolean, _arg2:String="", _arg3:String="", _arg4:String=""){
            this.isError = _arg1;
            this.subField = _arg2;
            this.errorMessage = _arg4;
            this.errorCode = _arg3;
        }
    }
}//package mx.validators 
