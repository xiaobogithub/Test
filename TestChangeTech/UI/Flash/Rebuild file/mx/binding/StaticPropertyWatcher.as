//Created by Action Script Viewer - http://www.buraks.com/asv
package mx.binding {
    import mx.core.*;
    import flash.events.*;
    import mx.events.*;

    public class StaticPropertyWatcher extends Watcher {

        private var propertyGetter:Function
        private var parentObj:Class
        protected var events:Object
        private var _propertyName:String

        mx_internal static const VERSION:String = "3.2.0.3958";

        public function StaticPropertyWatcher(_arg1:String, _arg2:Object, _arg3:Array, _arg4:Function=null){
            super(_arg3);
            _propertyName = _arg1;
            this.events = _arg2;
            this.propertyGetter = _arg4;
        }
        private function eventNamesToString():String{
            var _local2:String;
            var _local1 = " ";
            for (_local2 in events) {
                _local1 = (_local1 + (_local2 + " "));
            };
            return (_local1);
        }
        override public function updateParent(_arg1:Object):void{
            var _local2:String;
            var _local3:IEventDispatcher;
            parentObj = Class(_arg1);
            if (parentObj["staticEventDispatcher"] != null){
                for (_local2 in events) {
                    if (_local2 != "__NoChangeEvent__"){
                        _local3 = parentObj["staticEventDispatcher"];
                        _local3.addEventListener(_local2, eventHandler, false, EventPriority.BINDING, true);
                    };
                };
            };
            wrapUpdate(updateProperty);
        }
        private function updateProperty():void{
            if (parentObj){
                if (propertyGetter != null){
                    value = propertyGetter.apply(parentObj, [_propertyName]);
                } else {
                    value = parentObj[_propertyName];
                };
            } else {
                value = null;
            };
            updateChildren();
        }
        override protected function shallowClone():Watcher{
            var _local1:StaticPropertyWatcher = new StaticPropertyWatcher(_propertyName, events, listeners, propertyGetter);
            return (_local1);
        }
        private function traceInfo():String{
            return ((((((("StaticPropertyWatcher(" + parentObj) + ".") + _propertyName) + "): events = [") + eventNamesToString()) + "]"));
        }
        public function get propertyName():String{
            return (_propertyName);
        }
        public function eventHandler(_arg1:Event):void{
            var _local2:Object;
            if ((_arg1 is PropertyChangeEvent)){
                _local2 = PropertyChangeEvent(_arg1).property;
                if (_local2 != _propertyName){
                    return;
                };
            };
            wrapUpdate(updateProperty);
            notifyListeners(events[_arg1.type]);
        }

    }
}//package mx.binding 
