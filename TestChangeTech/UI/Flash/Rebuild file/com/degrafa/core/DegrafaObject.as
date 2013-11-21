//Created by Action Script Viewer - http://www.buraks.com/asv
package com.degrafa.core {
    import mx.core.*;
    import flash.events.*;
    import mx.events.*;
    import mx.utils.*;

    public class DegrafaObject implements IDegrafaObject, IMXMLObject {

        public var objectBindings:Array
        private var _suppressEventProcessing:Boolean = false
        private var _eventDispatcher:EventDispatcher
        private var _id:String
        private var _parent:IDegrafaObject
        private var _isInitialized:Boolean
        private var _enableEvents:Boolean = true
        private var _document:Object

        public function willTrigger(_arg1:String):Boolean{
            return (eventDispatcher.willTrigger(_arg1));
        }
        public function set enableEvents(_arg1:Boolean):void{
            _enableEvents = _arg1;
        }
        public function get name():String{
            return (id);
        }
        public function hasEventListener(_arg1:String):Boolean{
            return (eventDispatcher.hasEventListener(_arg1));
        }
        public function dispatchPropertyChange(_arg1:Boolean=false, _arg2:Object=null, _arg3:Object=null, _arg4:Object=null, _arg5:Object=null):Boolean{
            return (dispatchEvent(new PropertyChangeEvent("propertyChange", _arg1, false, PropertyChangeEventKind.UPDATE, _arg2, _arg3, _arg4, _arg5)));
        }
        public function get hasEventManager():Boolean{
            return ((_eventDispatcher) ? true : false);
        }
        public function get id():String{
            if (_id){
                return (_id);
            };
            _id = NameUtil.createUniqueName(this);
            return (_id);
        }
        public function get parent():IDegrafaObject{
            return (_parent);
        }
        public function get document():Object{
            return (_document);
        }
        public function set suppressEventProcessing(_arg1:Boolean):void{
            if ((((_suppressEventProcessing == true)) && ((_arg1 == false)))){
                _suppressEventProcessing = _arg1;
                initChange("suppressEventProcessing", false, true, this);
            } else {
                _suppressEventProcessing = _arg1;
            };
        }
        public function set id(_arg1:String):void{
            _id = _arg1;
        }
        public function dispatchEvent(_arg1:Event):Boolean{
            if (_suppressEventProcessing){
                _arg1.stopImmediatePropagation();
                return (false);
            };
            return (eventDispatcher.dispatchEvent(_arg1));
        }
        protected function set eventDispatcher(_arg1:EventDispatcher):void{
            _eventDispatcher = _arg1;
        }
        public function removeEventListener(_arg1:String, _arg2:Function, _arg3:Boolean=false):void{
            eventDispatcher.removeEventListener(_arg1, _arg2, _arg3);
        }
        public function addEventListener(_arg1:String, _arg2:Function, _arg3:Boolean=false, _arg4:int=0, _arg5:Boolean=false):void{
            eventDispatcher.addEventListener(_arg1, _arg2, _arg3, _arg4);
        }
        public function get isInitialized():Boolean{
            return (_isInitialized);
        }
        public function set parent(_arg1:IDegrafaObject):void{
            _parent = _arg1;
        }
        public function get suppressEventProcessing():Boolean{
            return (_suppressEventProcessing);
        }
        public function initChange(_arg1:String, _arg2:Object, _arg3:Object, _arg4:Object):void{
            if (hasEventManager){
                dispatchPropertyChange(false, _arg1, _arg2, _arg3, _arg4);
            };
        }
        protected function get eventDispatcher():EventDispatcher{
            if (!_eventDispatcher){
                _eventDispatcher = new EventDispatcher(this);
            };
            return (_eventDispatcher);
        }
        public function initialized(_arg1:Object, _arg2:String):void{
            if (!_id){
                if (_arg2){
                    _id = _arg2;
                } else {
                    _id = NameUtil.createUniqueName(this);
                };
            };
            _document = _arg1;
            _isInitialized = true;
            if (((enableEvents) && (!(_suppressEventProcessing)))){
                dispatchEvent(new FlexEvent(FlexEvent.INITIALIZE));
            };
        }
        public function get enableEvents():Boolean{
            return (_enableEvents);
        }

    }
}//package com.degrafa.core 
