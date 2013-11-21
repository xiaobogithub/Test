//Created by Action Script Viewer - http://www.buraks.com/asv
package org.osflash.thunderbolt {
    import flash.utils.*;
    import flash.system.*;
    import flash.external.*;

    public class Logger {

        public static const WARN:String = "warn";
        private static const AUTHOR:String = "Jens Krause [www.websector.de]";
        private static const GROUP_END:String = "groupEnd";
        private static const FIELD_SEPERATOR:String = " :: ";
        public static const LOG:String = "log";
        public static const ERROR:String = "error";
        private static const GROUP_START:String = "group";
        public static const INFO:String = "info";
        private static const MAX_DEPTH:int = 0xFF;
        private static const VERSION:String = "2.0";

        private static var _depth:int;
        private static var _logLevel:String;
        public static var includeTime:Boolean = true;
        private static var _firstRun:Boolean = true;
        private static var _hide:Boolean = false;
        private static var _stopLog:Boolean = false;
        private static var _firebug:Boolean = false;

        private static function primitiveType(_arg1:String):Boolean{
            var _local2:Boolean;
            switch (_arg1){
                case "Boolean":
                case "void":
                case "int":
                case "uint":
                case "Number":
                case "String":
                case "undefined":
                case "null":
                    _local2 = true;
                    break;
                default:
                    _local2 = false;
            };
            return (_local2);
        }
        private static function timeToValidString(_arg1:Number):String{
            return (((_arg1 > 9)) ? _arg1.toString() : ("0" + _arg1.toString()));
        }
        public static function debug(_arg1:String=null, ... _args):void{
            Logger.log(Logger.LOG, _arg1, _args);
        }
        public static function about():void{
            var _local1:String;
            _local1 = (((("+++ Welcome to ThunderBolt AS3 | VERSION: " + Logger.VERSION) + " | AUTHOR: ") + Logger.AUTHOR) + " | Happy logging +++");
            Logger.info(_local1);
        }
        public static function warn(_arg1:String=null, ... _args):void{
            Logger.log(Logger.WARN, _arg1, _args);
        }
        public static function error(_arg1:String=null, ... _args):void{
            Logger.log(Logger.ERROR, _arg1, _args);
        }
        public static function memorySnapshot():String{
            var _local1:uint;
            var _local2:String;
            _local1 = System.totalMemory;
            _local2 = (((("Memory Snapshot: " + (Math.round((((_local1 / 0x0400) / 0x0400) * 100)) / 100)) + " MB (") + Math.round((_local1 / 0x0400))) + " kb)");
            return (_local2);
        }
        public static function set hide(_arg1:Boolean):void{
            _hide = _arg1;
        }
        private static function call(_arg1:String=""):void{
            if (_firebug){
                ExternalInterface.call(("console." + _logLevel), _arg1);
            } else {
                trace(((_logLevel + " ") + _arg1));
            };
        }
        public static function log(_arg1:String, _arg2:String="", _arg3:Array=null):void{
            var _local4:String;
            var _local5:Boolean;
            var _local6:int;
            var _local7:int;
            if (!_hide){
                if (_firstRun){
                    _local5 = (((Capabilities.playerType == "ActiveX")) || ((Capabilities.playerType == "PlugIn")));
                    trace(("isBrowser " + _local5));
                    if (((_local5) && (ExternalInterface.available))){
                        if (ExternalInterface.call("function(){ return typeof window.console == 'object' && typeof console.firebug == 'string'}")){
                            _firebug = true;
                        };
                    };
                    _firstRun = false;
                };
                _depth = 0;
                _logLevel = _arg1;
                _local4 = "";
                if (includeTime){
                    _local4 = (_local4 + getCurrentTime());
                };
                _local4 = (_local4 + _arg2);
                Logger.call(_local4);
                if (_arg3 != null){
                    _local6 = 0;
                    _local7 = _arg3.length;
                    _local6 = 0;
                    while (_local6 < _local7) {
                        Logger.logObject(_arg3[_local6]);
                        _local6++;
                    };
                };
            };
        }
        private static function logObject(_arg1, _arg2:String=null):void{
            var _local3:String;
            var _local4:XML;
            var _local5:String;
            var _local6:String;
            var _local7:String;
            var _local8:int;
            var _local9:int;
            var _local10:XMLList;
            var _local11:XML;
            var _local12:String;
            var _local13:String;
            var _local14:String;
            var _local15:*;
            if (_depth < Logger.MAX_DEPTH){
                _depth++;
                _local3 = ((_arg2) || (""));
                _local4 = describeType(_arg1);
                _local5 = _local4.@name;
                if (primitiveType(_local5)){
                    _local6 = (_local3.length) ? ((((("[" + _local5) + "] ") + _local3) + " = ") + _arg1) : ((("[" + _local5) + "] ") + _arg1);
                    Logger.call(_local6);
                } else {
                    if (_local5 == "Object"){
                        Logger.callGroupAction(GROUP_START, ("[Object] " + _local3));
                        for (_local7 in _arg1) {
                            logObject(_arg1[_local7], _local7);
                        };
                        Logger.callGroupAction(GROUP_END);
                    } else {
                        if (_local5 == "Array"){
                            Logger.callGroupAction(GROUP_START, ("[Array] " + _local3));
                            _local8 = 0;
                            _local9 = _arg1.length;
                            while (_local8 < _local9) {
                                logObject(_arg1[_local8]);
                                _local8++;
                            };
                            Logger.callGroupAction(GROUP_END);
                        } else {
                            _local10 = _local4..accessor;
                            if (_local10.length()){
                                for each (_local11 in _local10) {
                                    _local12 = _local11.@name;
                                    _local13 = _local11.@type;
                                    _local14 = _local11.@access;
                                    if (((_local14) && (!((_local14 == "writeonly"))))){
                                        _local15 = _arg1[_local12];
                                        Logger.logObject(_local15, _local12);
                                    };
                                };
                            } else {
                                Logger.logObject(_arg1, _local5);
                            };
                        };
                    };
                };
            } else {
                if (!_stopLog){
                    Logger.call((("STOP LOGGING: More than " + _depth) + " nested objects or properties."));
                    _stopLog = true;
                };
            };
        }
        public static function info(_arg1:String=null, ... _args):void{
            Logger.log(Logger.INFO, _arg1, _args);
        }
        private static function getCurrentTime():String{
            var _local1:Date;
            var _local2:String;
            _local1 = new Date();
            _local2 = (((((((("time " + timeToValidString(_local1.getHours())) + ":") + timeToValidString(_local1.getMinutes())) + ":") + timeToValidString(_local1.getSeconds())) + ".") + timeToValidString(_local1.getMilliseconds())) + FIELD_SEPERATOR);
            return (_local2);
        }
        private static function callGroupAction(_arg1:String, _arg2:String=""):void{
            if (_firebug){
                if (_arg1 == GROUP_START){
                    ExternalInterface.call("console.group", _arg2);
                } else {
                    if (_arg1 == GROUP_END){
                        ExternalInterface.call("console.groupEnd");
                    } else {
                        ExternalInterface.call(("console." + Logger.ERROR), "group type has not defined");
                    };
                };
            } else {
                if (_arg1 == GROUP_START){
                    trace(((((_logLevel + ".") + GROUP_START) + " ") + _arg2));
                } else {
                    if (_arg1 == GROUP_END){
                        trace(((((_logLevel + ".") + GROUP_END) + " ") + _arg2));
                    } else {
                        trace((ERROR + "group type has not defined"));
                    };
                };
            };
        }
        public static function set console(_arg1:Boolean):void{
            _firstRun = false;
            _firebug = !(_arg1);
        }

    }
}//package org.osflash.thunderbolt 
