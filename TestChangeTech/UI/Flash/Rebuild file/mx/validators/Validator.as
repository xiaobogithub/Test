//Created by Action Script Viewer - http://www.buraks.com/asv
package mx.validators {
    import mx.core.*;
    import flash.events.*;
    import mx.events.*;
    import mx.resources.*;

    public class Validator extends EventDispatcher implements IMXMLObject {

        private var _resourceManager:IResourceManager
        private var _enabled:Boolean = true
        private var _listener:Object
        protected var subFields:Array
        private var document:Object
        public var required:Boolean = true
        private var requiredFieldErrorOverride:String
        private var _triggerEvent:String = "valueCommit"
        private var _source:Object
        private var _property:String
        private var _requiredFieldError:String
        private var _trigger:IEventDispatcher

        protected static const DECIMAL_DIGITS:String = "0123456789";
        mx_internal static const VERSION:String = "3.2.0.3958";
        protected static const ROMAN_LETTERS:String = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

        public function Validator(){
            _resourceManager = ResourceManager.getInstance();
            subFields = [];
            super();
            resourceManager.addEventListener(Event.CHANGE, resourceManager_changeHandler, false, 0, true);
            resourcesChanged();
        }
        private function triggerHandler(_arg1:Event):void{
            validate();
        }
        public function set enabled(_arg1:Boolean):void{
            _enabled = _arg1;
        }
        public function set triggerEvent(_arg1:String):void{
            if (_triggerEvent == _arg1){
                return;
            };
            removeTriggerHandler();
            _triggerEvent = _arg1;
            addTriggerHandler();
        }
        protected function getValueFromSource():Object{
            var _local1:String;
            if (((_source) && (_property))){
                return (_source[_property]);
            };
            if (((!(_source)) && (_property))){
                _local1 = resourceManager.getString("validators", "SAttributeMissing");
                throw (new Error(_local1));
            };
            if (((_source) && (!(_property)))){
                _local1 = resourceManager.getString("validators", "PAttributeMissing");
                throw (new Error(_local1));
            };
            return (null);
        }
        protected function addListenerHandler():void{
            var _local1:Object;
            var _local2:Array = actualListeners;
            var _local3:int = _local2.length;
            var _local4:int;
            while (_local4 < _local3) {
                _local1 = _local2[_local4];
                if ((_local1 is IValidatorListener)){
                    addEventListener(ValidationResultEvent.VALID, IValidatorListener(_local1).validationResultHandler);
                    addEventListener(ValidationResultEvent.INVALID, IValidatorListener(_local1).validationResultHandler);
                };
                _local4++;
            };
        }
        private function removeTriggerHandler():void{
            if (actualTrigger){
                actualTrigger.removeEventListener(_triggerEvent, triggerHandler);
            };
        }
        public function validate(_arg1:Object=null, _arg2:Boolean=false):ValidationResultEvent{
            if (_arg1 == null){
                _arg1 = getValueFromSource();
            };
            if (((isRealValue(_arg1)) || (required))){
                return (processValidation(_arg1, _arg2));
            };
            return (new ValidationResultEvent(ValidationResultEvent.VALID));
        }
        public function get source():Object{
            return (_source);
        }
        public function set property(_arg1:String):void{
            _property = _arg1;
        }
        public function get requiredFieldError():String{
            return (_requiredFieldError);
        }
        protected function handleResults(_arg1:Array):ValidationResultEvent{
            var _local2:ValidationResultEvent;
            var _local3:Object;
            var _local4:String;
            var _local5:int;
            var _local6:int;
            if (_arg1.length > 0){
                _local2 = new ValidationResultEvent(ValidationResultEvent.INVALID);
                _local2.results = _arg1;
                if (subFields.length > 0){
                    _local3 = {};
                    _local5 = _arg1.length;
                    _local6 = 0;
                    while (_local6 < _local5) {
                        _local4 = _arg1[_local6].subField;
                        if (_local4){
                            _local3[_local4] = true;
                        };
                        _local6++;
                    };
                    _local5 = subFields.length;
                    _local6 = 0;
                    while (_local6 < _local5) {
                        if (!_local3[subFields[_local6]]){
                            _arg1.push(new ValidationResult(false, subFields[_local6]));
                        };
                        _local6++;
                    };
                };
            } else {
                _local2 = new ValidationResultEvent(ValidationResultEvent.VALID);
            };
            return (_local2);
        }
        public function get listener():Object{
            return (_listener);
        }
        public function get trigger():IEventDispatcher{
            return (_trigger);
        }
        public function set source(_arg1:Object):void{
            var _local2:String;
            if (_source == _arg1){
                return;
            };
            if ((_arg1 is String)){
                _local2 = resourceManager.getString("validators", "SAttribute", [_arg1]);
                throw (new Error(_local2));
            };
            removeTriggerHandler();
            removeListenerHandler();
            _source = _arg1;
            addTriggerHandler();
            addListenerHandler();
        }
        protected function get resourceManager():IResourceManager{
            return (_resourceManager);
        }
        public function get enabled():Boolean{
            return (_enabled);
        }
        private function processValidation(_arg1:Object, _arg2:Boolean):ValidationResultEvent{
            var _local3:ValidationResultEvent;
            var _local4:Array;
            if (_enabled){
                _local4 = doValidation(_arg1);
                _local3 = handleResults(_local4);
            } else {
                _arg2 = true;
            };
            if (!_arg2){
                dispatchEvent(_local3);
            };
            return (_local3);
        }
        public function get triggerEvent():String{
            return (_triggerEvent);
        }
        protected function get actualTrigger():IEventDispatcher{
            if (_trigger){
                return (_trigger);
            };
            if (_source){
                return ((_source as IEventDispatcher));
            };
            return (null);
        }
        protected function resourcesChanged():void{
            requiredFieldError = requiredFieldErrorOverride;
        }
        protected function get actualListeners():Array{
            var _local1:Array = [];
            if (_listener){
                _local1.push(_listener);
            } else {
                if (_source){
                    _local1.push(_source);
                };
            };
            return (_local1);
        }
        protected function removeListenerHandler():void{
            var _local1:Object;
            var _local2:Array = actualListeners;
            var _local3:int = _local2.length;
            var _local4:int;
            while (_local4 < _local3) {
                _local1 = _local2[_local4];
                if ((_local1 is IValidatorListener)){
                    removeEventListener(ValidationResultEvent.VALID, IValidatorListener(_local1).validationResultHandler);
                    removeEventListener(ValidationResultEvent.INVALID, IValidatorListener(_local1).validationResultHandler);
                };
                _local4++;
            };
        }
        public function initialized(_arg1:Object, _arg2:String):void{
            this.document = _arg1;
        }
        public function get property():String{
            return (_property);
        }
        public function set requiredFieldError(_arg1:String):void{
            requiredFieldErrorOverride = _arg1;
            _requiredFieldError = ((_arg1)!=null) ? _arg1 : resourceManager.getString("validators", "requiredFieldError");
        }
        private function validateRequired(_arg1:Object):ValidationResult{
            var _local2:String;
            if (required){
                _local2 = ((_arg1)!=null) ? String(_arg1) : "";
                _local2 = trimString(_local2);
                if (_local2.length == 0){
                    return (new ValidationResult(true, "", "requiredField", requiredFieldError));
                };
            };
            return (null);
        }
        protected function doValidation(_arg1:Object):Array{
            var _local2:Array = [];
            var _local3:ValidationResult = validateRequired(_arg1);
            if (_local3){
                _local2.push(_local3);
            };
            return (_local2);
        }
        public function set listener(_arg1:Object):void{
            removeListenerHandler();
            _listener = _arg1;
            addListenerHandler();
        }
        protected function isRealValue(_arg1:Object):Boolean{
            return (!((_arg1 == null)));
        }
        public function set trigger(_arg1:IEventDispatcher):void{
            removeTriggerHandler();
            _trigger = _arg1;
            addTriggerHandler();
        }
        private function addTriggerHandler():void{
            if (actualTrigger){
                actualTrigger.addEventListener(_triggerEvent, triggerHandler);
            };
        }
        private function resourceManager_changeHandler(_arg1:Event):void{
            resourcesChanged();
        }

        private static function findObjectFromString(_arg1:Object, _arg2:String):Object{
            var resourceManager:* = null;
            var message:* = null;
            var doc:* = _arg1;
            var value:* = _arg2;
            var obj:* = doc;
            var parts:* = value.split(".");
            var n:* = parts.length;
            var i:* = 0;
            while (i < n) {
                try {
                    obj = obj[parts[i]];
                    if (obj == null){
                    };
                } catch(error:Error) {
                    if ((((error is TypeError)) && (!((error.message.indexOf("null has no properties") == -1))))){
                        resourceManager = ResourceManager.getInstance();
                        message = resourceManager.getString("validators", "fieldNotFound", [value]);
                        throw (new Error(message));
                    } else {
                        throw (error);
                    };
                };
                i = (i + 1);
            };
            return (obj);
        }
        private static function trimString(_arg1:String):String{
            var _local2:int;
            while (_arg1.indexOf(" ", _local2) == _local2) {
                _local2++;
            };
            var _local3:int = (_arg1.length - 1);
            while (_arg1.lastIndexOf(" ", _local3) == _local3) {
                _local3--;
            };
            return (((_local3 >= _local2)) ? _arg1.slice(_local2, (_local3 + 1)) : "");
        }
        public static function validateAll(_arg1:Array):Array{
            var _local5:Validator;
            var _local6:ValidationResultEvent;
            var _local2:Array = [];
            var _local3:int = _arg1.length;
            var _local4:int;
            while (_local4 < _local3) {
                _local5 = Validator(_arg1[_local4]);
                if (_local5.enabled){
                    _local6 = _local5.validate();
                    if (_local6.type != ValidationResultEvent.VALID){
                        _local2.push(_local6);
                    };
                };
                _local4++;
            };
            return (_local2);
        }

    }
}//package mx.validators 
