//Created by Action Script Viewer - http://www.buraks.com/asv
package mx.binding {
    import mx.core.*;
    import flash.events.*;

    public class FunctionReturnWatcher extends Watcher {

        private var parameterFunction:Function
        private var functionName:String
        private var functionGetter:Function
        public var parentWatcher:Watcher
        private var parentObj:Object
        private var events:Object
        private var document:Object

        mx_internal static const VERSION:String = "3.2.0.3958";

        public function FunctionReturnWatcher(_arg1:String, _arg2:Object, _arg3:Function, _arg4:Object, _arg5:Array, _arg6:Function=null){
            super(_arg5);
            this.functionName = _arg1;
            this.document = _arg2;
            this.parameterFunction = _arg3;
            this.events = _arg4;
            this.functionGetter = _arg6;
        }
        override protected function shallowClone():Watcher{
            var _local1:FunctionReturnWatcher = new FunctionReturnWatcher(functionName, document, parameterFunction, events, listeners, functionGetter);
            return (_local1);
        }
        override public function updateParent(_arg1:Object):void{
            if (!(_arg1 is Watcher)){
                setupParentObj(_arg1);
            } else {
                if (_arg1 == parentWatcher){
                    setupParentObj(parentWatcher.value);
                };
            };
            updateFunctionReturn();
        }
        private function setupParentObj(_arg1:Object):void{
            var _local2:IEventDispatcher;
            var _local3:String;
            if (((((!((parentObj == null))) && ((parentObj is IEventDispatcher)))) && (!((events == null))))){
                _local2 = (parentObj as IEventDispatcher);
                for (_local3 in events) {
                    _local2.removeEventListener(_local3, eventHandler);
                };
            };
            parentObj = _arg1;
            if (((((!((parentObj == null))) && ((parentObj is IEventDispatcher)))) && (!((events == null))))){
                _local2 = (parentObj as IEventDispatcher);
                for (_local3 in events) {
                    if (_local3 != "__NoChangeEvent__"){
                        _local2.addEventListener(_local3, eventHandler, false, EventPriority.BINDING, true);
                    };
                };
            };
        }
        public function updateFunctionReturn():void{
            wrapUpdate(function ():void{
                if (functionGetter != null){
                    value = functionGetter(functionName).apply(parentObj, parameterFunction.apply(document));
                } else {
                    value = parentObj[functionName].apply(parentObj, parameterFunction.apply(document));
                };
                updateChildren();
            });
        }
        public function eventHandler(_arg1:Event):void{
            updateFunctionReturn();
            notifyListeners(events[_arg1.type]);
        }

    }
}//package mx.binding 
