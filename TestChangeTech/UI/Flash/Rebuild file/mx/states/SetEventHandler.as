//Created by Action Script Viewer - http://www.buraks.com/asv
package mx.states {
    import mx.core.*;
    import flash.events.*;
    import flash.utils.*;

    public class SetEventHandler extends EventDispatcher implements IOverride {

        public var handlerFunction:Function
        public var name:String
        public var target:EventDispatcher
        private var oldHandlerFunction:Function

        mx_internal static const VERSION:String = "3.2.0.3958";

        private static var installedHandlers:Dictionary;

        public function SetEventHandler(_arg1:EventDispatcher=null, _arg2:String=null){
            this.target = _arg1;
            this.name = _arg2;
        }
        public function apply(_arg1:UIComponent):void{
            var _local4:ComponentDescriptor;
            var _local2:EventDispatcher = (target) ? target : _arg1;
            var _local3:UIComponent = (_local2 as UIComponent);
            if (!installedHandlers){
                installedHandlers = new Dictionary(true);
            };
            if (((installedHandlers[_local2]) && (installedHandlers[_local2][name]))){
                oldHandlerFunction = installedHandlers[_local2][name];
                _local2.removeEventListener(name, oldHandlerFunction);
            } else {
                if (((_local3) && (_local3.descriptor))){
                    _local4 = _local3.descriptor;
                    if (((_local4.events) && (_local4.events[name]))){
                        oldHandlerFunction = _local3.document[_local4.events[name]];
                        _local2.removeEventListener(name, oldHandlerFunction);
                    };
                };
            };
            if (handlerFunction != null){
                _local2.addEventListener(name, handlerFunction, false, 0, true);
                if (installedHandlers[_local2] == undefined){
                    installedHandlers[_local2] = {};
                };
                installedHandlers[_local2][name] = handlerFunction;
            };
        }
        public function initialize():void{
        }
        override public function addEventListener(_arg1:String, _arg2:Function, _arg3:Boolean=false, _arg4:int=0, _arg5:Boolean=false):void{
            if (_arg1 == "handler"){
                handlerFunction = _arg2;
            };
            super.addEventListener(_arg1, _arg2, _arg3, _arg4, _arg5);
        }
        public function remove(_arg1:UIComponent):void{
            var _local3:Boolean;
            var _local4:String;
            var _local2:EventDispatcher = (target) ? target : _arg1;
            if (handlerFunction != null){
                _local2.removeEventListener(name, handlerFunction);
            };
            if (oldHandlerFunction != null){
                _local2.addEventListener(name, oldHandlerFunction, false, 0, true);
            };
            if (installedHandlers[_local2]){
                _local3 = true;
                delete installedHandlers[_local2][name];
                for (_local4 in installedHandlers[_local2]) {
                    _local3 = false;
                    break;
                };
                if (_local3){
                    delete installedHandlers[_local2];
                };
            };
        }

    }
}//package mx.states 
