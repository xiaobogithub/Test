//Created by Action Script Viewer - http://www.buraks.com/asv
package mx.validators {

    public class EmailValidator extends Validator {

        private var _missingUsernameError:String
        private var tooManyAtSignsErrorOverride:String
        private var _invalidIPDomainError:String
        private var invalidCharErrorOverride:String
        private var invalidPeriodsInDomainErrorOverride:String
        private var invalidIPDomainErrorOverride:String
        private var _invalidDomainError:String
        private var _missingPeriodInDomainError:String
        private var invalidDomainErrorOverride:String
        private var missingPeriodInDomainErrorOverride:String
        private var _invalidPeriodsInDomainError:String
        private var _tooManyAtSignsError:String
        private var _missingAtSignError:String
        private var _invalidCharError:String
        private var missingAtSignErrorOverride:String
        private var missingUsernameErrorOverride:String

        private static const DISALLOWED_DOMAIN_CHARS:String = "()<>,;:\"[] `~!#$%^&*+={}|/?'";
        mx_internal static const VERSION:String = "3.2.0.3958";
        private static const DISALLOWED_LOCALNAME_CHARS:String = "()<>,;:\"[] `~!#$%^&*={}|/?'";

        public function set missingAtSignError(_arg1:String):void{
            missingAtSignErrorOverride = _arg1;
            _missingAtSignError = ((_arg1)!=null) ? _arg1 : resourceManager.getString("validators", "missingAtSignError");
        }
        public function set invalidPeriodsInDomainError(_arg1:String):void{
            invalidPeriodsInDomainErrorOverride = _arg1;
            _invalidPeriodsInDomainError = ((_arg1)!=null) ? _arg1 : resourceManager.getString("validators", "invalidPeriodsInDomainError");
        }
        public function set invalidIPDomainError(_arg1:String):void{
            invalidIPDomainErrorOverride = _arg1;
            _invalidIPDomainError = ((_arg1)!=null) ? _arg1 : resourceManager.getString("validators", "invalidIPDomainError");
        }
        override protected function doValidation(_arg1:Object):Array{
            var _local2:Array = super.doValidation(_arg1);
            var _local3:String = (_arg1) ? String(_arg1) : "";
            if ((((_local2.length > 0)) || ((((_local3.length == 0)) && (!(required)))))){
                return (_local2);
            };
            return (EmailValidator.validateEmail(this, _arg1, null));
        }
        public function set invalidDomainError(_arg1:String):void{
            invalidDomainErrorOverride = _arg1;
            _invalidDomainError = ((_arg1)!=null) ? _arg1 : resourceManager.getString("validators", "invalidDomainErrorEV");
        }
        public function get invalidCharError():String{
            return (_invalidCharError);
        }
        override protected function resourcesChanged():void{
            super.resourcesChanged();
            invalidCharError = invalidCharErrorOverride;
            invalidDomainError = invalidDomainErrorOverride;
            invalidIPDomainError = invalidIPDomainErrorOverride;
            invalidPeriodsInDomainError = invalidPeriodsInDomainErrorOverride;
            missingAtSignError = missingAtSignErrorOverride;
            missingPeriodInDomainError = missingPeriodInDomainErrorOverride;
            missingUsernameError = missingUsernameErrorOverride;
            tooManyAtSignsError = tooManyAtSignsErrorOverride;
        }
        public function set missingUsernameError(_arg1:String):void{
            missingUsernameErrorOverride = _arg1;
            _missingUsernameError = ((_arg1)!=null) ? _arg1 : resourceManager.getString("validators", "missingUsernameError");
        }
        public function get missingAtSignError():String{
            return (_missingAtSignError);
        }
        public function get invalidIPDomainError():String{
            return (_invalidIPDomainError);
        }
        public function get tooManyAtSignsError():String{
            return (_tooManyAtSignsError);
        }
        public function set tooManyAtSignsError(_arg1:String):void{
            tooManyAtSignsErrorOverride = _arg1;
            _tooManyAtSignsError = ((_arg1)!=null) ? _arg1 : resourceManager.getString("validators", "tooManyAtSignsError");
        }
        public function get invalidPeriodsInDomainError():String{
            return (_invalidPeriodsInDomainError);
        }
        public function get invalidDomainError():String{
            return (_invalidDomainError);
        }
        public function set missingPeriodInDomainError(_arg1:String):void{
            missingPeriodInDomainErrorOverride = _arg1;
            _missingPeriodInDomainError = ((_arg1)!=null) ? _arg1 : resourceManager.getString("validators", "missingPeriodInDomainError");
        }
        public function set invalidCharError(_arg1:String):void{
            invalidCharErrorOverride = _arg1;
            _invalidCharError = ((_arg1)!=null) ? _arg1 : resourceManager.getString("validators", "invalidCharErrorEV");
        }
        public function get missingPeriodInDomainError():String{
            return (_missingPeriodInDomainError);
        }
        public function get missingUsernameError():String{
            return (_missingUsernameError);
        }

        public static function validateEmail(_arg1:EmailValidator, _arg2:Object, _arg3:String):Array{
            var _local8:int;
            var _local9:int;
            var _local13:int;
            var _local14:int;
            var _local15:String;
            var _local4:Array = [];
            var _local5:String = String(_arg2);
            var _local6 = "";
            var _local7 = "";
            var _local10:int = _local5.indexOf("@");
            if (_local10 == -1){
                _local4.push(new ValidationResult(true, _arg3, "missingAtSign", _arg1.missingAtSignError));
                return (_local4);
            };
            if (_local5.indexOf("@", (_local10 + 1)) != -1){
                _local4.push(new ValidationResult(true, _arg3, "tooManyAtSigns", _arg1.tooManyAtSignsError));
                return (_local4);
            };
            _local6 = _local5.substring(0, _local10);
            _local7 = _local5.substring((_local10 + 1));
            var _local11:int = _local6.length;
            if (_local11 == 0){
                _local4.push(new ValidationResult(true, _arg3, "missingUsername", _arg1.missingUsernameError));
                return (_local4);
            };
            _local9 = 0;
            while (_local9 < _local11) {
                if (DISALLOWED_LOCALNAME_CHARS.indexOf(_local6.charAt(_local9)) != -1){
                    _local4.push(new ValidationResult(true, _arg3, "invalidChar", _arg1.invalidCharError));
                    return (_local4);
                };
                _local9++;
            };
            var _local12:int = _local7.length;
            if ((((_local7.charAt(0) == "[")) && ((_local7.charAt((_local12 - 1)) == "]")))){
                if (!isValidIPAddress(_local7.substring(1, (_local12 - 1)))){
                    _local4.push(new ValidationResult(true, _arg3, "invalidIPDomain", _arg1.invalidIPDomainError));
                    return (_local4);
                };
            } else {
                _local13 = _local7.indexOf(".");
                _local14 = 0;
                _local15 = "";
                if (_local13 == -1){
                    _local4.push(new ValidationResult(true, _arg3, "missingPeriodInDomain", _arg1.missingPeriodInDomainError));
                    return (_local4);
                };
                while (true) {
                    _local14 = _local7.indexOf(".", (_local13 + 1));
                    if (_local14 == -1){
                        _local15 = _local7.substring((_local13 + 1));
                        if (((((((!((_local15.length == 3))) && (!((_local15.length == 2))))) && (!((_local15.length == 4))))) && (!((_local15.length == 6))))){
                            _local4.push(new ValidationResult(true, _arg3, "invalidDomain", _arg1.invalidDomainError));
                            return (_local4);
                        };
                        break;
                    } else {
                        if (_local14 == (_local13 + 1)){
                            _local4.push(new ValidationResult(true, _arg3, "invalidPeriodsInDomain", _arg1.invalidPeriodsInDomainError));
                            return (_local4);
                        };
                    };
                    _local13 = _local14;
                };
                _local9 = 0;
                while (_local9 < _local12) {
                    if (DISALLOWED_DOMAIN_CHARS.indexOf(_local7.charAt(_local9)) != -1){
                        _local4.push(new ValidationResult(true, _arg3, "invalidChar", _arg1.invalidCharError));
                        return (_local4);
                    };
                    _local9++;
                };
                if (_local7.charAt(0) == "."){
                    _local4.push(new ValidationResult(true, _arg3, "invalidDomain", _arg1.invalidDomainError));
                    return (_local4);
                };
            };
            return (_local4);
        }
        private static function isValidIPAddress(_arg1:String):Boolean{
            var _local5:Number;
            var _local6:int;
            var _local7:int;
            var _local8:Boolean;
            var _local9:Boolean;
            var _local2:Array = [];
            var _local3:int;
            var _local4:int;
            if (_arg1.indexOf(":") != -1){
                _local8 = !((_arg1.indexOf("::") == -1));
                if (_local8){
                    _arg1 = _arg1.replace(/^::/, "");
                    _arg1 = _arg1.replace(/::/g, ":");
                };
                while (true) {
                    _local4 = _arg1.indexOf(":", _local3);
                    if (_local4 != -1){
                        _local2.push(_arg1.substring(_local3, _local4));
                    } else {
                        _local2.push(_arg1.substring(_local3));
                        break;
                    };
                    _local3 = (_local4 + 1);
                };
                _local6 = _local2.length;
                _local9 = !((_local2[(_local6 - 1)].indexOf(".") == -1));
                if (_local9){
                    if (((((!((_local2.length == 7))) && (!(_local8)))) || ((_local2.length > 7)))){
                        return (false);
                    };
                    _local7 = 0;
                    while (_local7 < _local6) {
                        if (_local7 == (_local6 - 1)){
                            return (isValidIPAddress(_local2[_local7]));
                        };
                        _local5 = parseInt(_local2[_local7], 16);
                        if (_local5 != 0){
                            return (false);
                        };
                        _local7++;
                    };
                } else {
                    if (((((!((_local2.length == 8))) && (!(_local8)))) || ((_local2.length > 8)))){
                        return (false);
                    };
                    _local7 = 0;
                    while (_local7 < _local6) {
                        _local5 = parseInt(_local2[_local7], 16);
                        if (((((isNaN(_local5)) || ((_local5 < 0)))) || ((_local5 > 0xFFFF)))){
                            return (false);
                        };
                        _local7++;
                    };
                };
                return (true);
            };
            if (_arg1.indexOf(".") != -1){
                while (true) {
                    _local4 = _arg1.indexOf(".", _local3);
                    if (_local4 != -1){
                        _local2.push(_arg1.substring(_local3, _local4));
                    } else {
                        _local2.push(_arg1.substring(_local3));
                        break;
                    };
                    _local3 = (_local4 + 1);
                };
                if (_local2.length != 4){
                    return (false);
                };
                _local6 = _local2.length;
                _local7 = 0;
                while (_local7 < _local6) {
                    _local5 = Number(_local2[_local7]);
                    if (((((isNaN(_local5)) || ((_local5 < 0)))) || ((_local5 > 0xFF)))){
                        return (false);
                    };
                    _local7++;
                };
                return (true);
            };
            return (false);
        }

    }
}//package mx.validators 
