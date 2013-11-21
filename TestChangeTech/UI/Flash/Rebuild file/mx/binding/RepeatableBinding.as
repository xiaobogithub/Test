//Created by Action Script Viewer - http://www.buraks.com/asv
package mx.binding {
    import mx.core.*;
    import flash.events.*;

    public class RepeatableBinding extends Binding {

        mx_internal static const VERSION:String = "3.2.0.3958";

        public function RepeatableBinding(_arg1:Object, _arg2:Function, _arg3:Function, _arg4:String){
            super(_arg1, _arg2, _arg3, _arg4);
        }
        public function eventHandler(_arg1:Event):void{
            if (isHandlingEvent){
                return;
            };
            isHandlingEvent = true;
            execute();
            isHandlingEvent = false;
        }
        override public function execute(_arg1:Object=null):void{
            var _local2:String;
            var _local3:Array;
            if (isExecuting){
                return;
            };
            isExecuting = true;
            if (!_arg1){
                _local2 = destString.substring(0, destString.indexOf("."));
                _arg1 = document[_local2];
            } else {
                if (typeof(_arg1) == "number"){
                    _local2 = destString.substring(0, destString.indexOf("."));
                    _local3 = (document[_local2] as Array);
                    if (_local3){
                        _arg1 = _local3[_arg1];
                    } else {
                        _arg1 = null;
                    };
                };
            };
            if (_arg1){
                recursivelyProcessIDArray(_arg1);
            };
            isExecuting = false;
        }
        private function recursivelyProcessIDArray(_arg1:Object):void{
            var array:* = null;
            var n:* = 0;
            var i:* = 0;
            var client:* = null;
            var o:* = _arg1;
            if ((o is Array)){
                array = (o as Array);
                n = array.length;
                i = 0;
                while (i < n) {
                    recursivelyProcessIDArray(array[i]);
                    i = (i + 1);
                };
            } else {
                if ((o is IRepeaterClient)){
                    client = IRepeaterClient(o);
                    wrapFunctionCall(this, function ():void{
                        var _local1:Object = wrapFunctionCall(this, srcFunc, null, client.instanceIndices, client.repeaterIndices);
                        if (BindingManager.debugDestinationStrings[destString]){
                            trace(((("RepeatableBinding: destString = " + destString) + ", srcFunc result = ") + _local1));
                        };
                        destFunc(_local1, client.instanceIndices);
                    }, o);
                };
            };
        }

    }
}//package mx.binding 
