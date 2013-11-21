//Created by Action Script Viewer - http://www.buraks.com/asv
package mx.binding.utils {
    import mx.core.*;
    import flash.events.*;
    import mx.events.*;
    import mx.utils.*;

    public class ChangeWatcher {

        private var commitOnly:Boolean
        private var host:Object
        private var handler:Function
        private var getter:Function
        private var name:String
        private var events:Object
        private var next:ChangeWatcher

        mx_internal static const VERSION:String = "3.2.0.3958";

        public function ChangeWatcher(_arg1:Object, _arg2:Function, _arg3:Boolean=false, _arg4:ChangeWatcher=null){
            host = null;
            name = ((_arg1 is String)) ? (_arg1 as String) : _arg1.name;
            getter = ((_arg1 is String)) ? null : _arg1.getter;
            this.handler = _arg2;
            this.commitOnly = _arg3;
            this.next = _arg4;
            events = {};
        }
        private function getHostPropertyValue():Object{
            return (((host == null)) ? null : ((getter)!=null) ? getter(host) : host[name]);
        }
        public function isWatching():Boolean{
            return (((!(isEmpty(events))) && ((((next == null)) || (next.isWatching())))));
        }
        public function unwatch():void{
            reset(null);
        }
        private function wrapHandler(_arg1:Event):void{
            if (next){
                next.reset(getHostPropertyValue());
            };
            if ((_arg1 is PropertyChangeEvent)){
                if ((_arg1 as PropertyChangeEvent).property == name){
                    handler((_arg1 as PropertyChangeEvent));
                };
            } else {
                handler(_arg1);
            };
        }
        public function setHandler(_arg1:Function):void{
            this.handler = _arg1;
            if (next){
                next.setHandler(_arg1);
            };
        }
        public function getValue():Object{
            return (((host == null)) ? null : ((next == null)) ? getHostPropertyValue() : next.getValue());
        }
        public function reset(_arg1:Object):void{
            var _local2:String;
            if (host != null){
                for (_local2 in events) {
                    host.removeEventListener(_local2, wrapHandler);
                };
                events = {};
            };
            host = _arg1;
            if (host != null){
                events = getEvents(host, name, commitOnly);
                for (_local2 in events) {
                    host.addEventListener(_local2, wrapHandler, false, EventPriority.BINDING, false);
                };
            };
            if (next){
                next.reset(getHostPropertyValue());
            };
        }

        private static function isEmpty(_arg1:Object):Boolean{
            var _local2:String;
            for (_local2 in _arg1) {
                return (false);
            };
            return (true);
        }
        public static function getEvents(_arg1:Object, _arg2:String, _arg3:Boolean=false):Object{
            var _local4:Object;
            var _local5:Object;
            var _local6:String;
            if ((_arg1 is IEventDispatcher)){
                _local4 = DescribeTypeCache.describeType(_arg1).bindabilityInfo.getChangeEvents(_arg2);
                if (_arg3){
                    _local5 = {};
                    for (_local6 in _local4) {
                        if (_local4[_local6]){
                            _local5[_local6] = true;
                        };
                    };
                    return (_local5);
                } else {
                    return (_local4);
                };
                //unresolved jump
            };
            return ({});
        }
        public static function watch(_arg1:Object, _arg2:Object, _arg3:Function, _arg4:Boolean=false):ChangeWatcher{
            var _local5:ChangeWatcher;
            if (!(_arg2 is Array)){
                _arg2 = [_arg2];
            };
            if (_arg2.length > 0){
                _local5 = new ChangeWatcher(_arg2[0], _arg3, _arg4, watch(null, _arg2.slice(1), _arg3, _arg4));
                _local5.reset(_arg1);
                return (_local5);
            };
            return (null);
        }
        public static function canWatch(_arg1:Object, _arg2:String, _arg3:Boolean=false):Boolean{
            return (!(isEmpty(getEvents(_arg1, _arg2, _arg3))));
        }

    }
}//package mx.binding.utils 
