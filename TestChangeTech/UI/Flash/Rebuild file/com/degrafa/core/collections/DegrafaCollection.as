//Created by Action Script Viewer - http://www.buraks.com/asv
package com.degrafa.core.collections {
    import mx.events.*;
    import flash.utils.*;
    import com.degrafa.core.*;

    public class DegrafaCollection extends DegrafaObject {

        private var _items:Array
        protected var _cursor:DegrafaCursor
        private var _enableTypeChecking:Boolean = true
        private var type:Class

        public function DegrafaCollection(_arg1:Class, _arg2:Array=null, _arg3:Boolean=false, _arg4:Boolean=true){
            _items = [];
            super();
            this.type = _arg1;
            _enableTypeChecking = _arg4;
            suppressEventProcessing = _arg3;
            if (_arg2){
                items = _arg2;
            };
        }
        public function get items():Array{
            return (_items);
        }
        public function get cursor():DegrafaCursor{
            if (!_cursor){
                _cursor = new DegrafaCursor(items);
            };
            return (_cursor);
        }
        public function reverse():Array{
            var _local1:Array = _items;
            items = items.reverse();
            initChange("items", _local1, _items, this);
            return (items);
        }
        private function checkValidTypes(_arg1:Array):void{
            var _local2:Object;
            if (_enableTypeChecking){
                for each (_local2 in _arg1) {
                    if ((!(_local2) is type)){
                        throw (new TypeError(((getQualifiedClassName(_local2) + " is not a valid ") + getQualifiedClassName(type))));
                    };
                };
            };
        }
        public function set items(_arg1:Array):void{
            var _local2:Array;
            checkValidTypes(_arg1);
            if (_arg1 != _items){
                if (((enableEvents) && (hasEventManager))){
                    removeListeners();
                };
                _local2 = _items;
                _items = _arg1;
                if (((enableEvents) && (hasEventManager))){
                    initChange("items", _local2, _items, this);
                };
            };
            addListeners();
        }
        public function indexOf(_arg1, _arg2:int=0):int{
            return (items.indexOf(_arg1, _arg2));
        }
        public function unshift(... _args):uint{
            checkValidTypes(_args);
            var _local2:Array = _items;
            var _local3:int;
            var _local4:int = _args.length;
            while (_local3 < _local4) {
                addListener(_args[_local3]);
                items.unshift(_args[_local3]);
                _local3++;
            };
            initChange("items", _local2, _items, this);
            return (items.length);
        }
        public function pop(){
            removeListener(items[(items.length - 1)]);
            var _local1:Array = _items;
            var _local2:* = items.pop();
            initChange("items", _local1, _items, this);
            return (_local2);
        }
        public function slice(_arg1:int=0, _arg2:int=0xFFFFFF):Array{
            return (items.slice(_arg1, _arg2));
        }
        public function lastIndexOf(_arg1, _arg2:int=2147483647):int{
            return (items.lastIndexOf(_arg1, _arg2));
        }
        public function concat(... _args):Array{
            var _local2:Array = _items;
            checkValidTypes(_args);
            var _local3:int;
            var _local4:int = _args.length;
            while (_local3 < _local4) {
                addListener(_args[_local3]);
                _local3++;
            };
            _items = items.concat(_args);
            initChange("items", _local2, _items, this);
            return (_items);
        }
        protected function _addItem(_arg1){
            addListener(_arg1);
            concat(_arg1);
            return (_arg1);
        }
        protected function _removeItem(_arg1){
            var _local2:int = indexOf(_arg1, 0);
            _removeItemAt(_local2);
            return (null);
        }
        public function get enableTypeChecking():Boolean{
            return (_enableTypeChecking);
        }
        public function removeListener(_arg1):void{
            if ((_arg1 is IDegrafaObject)){
                IDegrafaObject(_arg1).parent = null;
                IDegrafaObject(_arg1).removeEventListener(PropertyChangeEvent.PROPERTY_CHANGE, propertyChangeHandler);
            };
        }
        public function push(... _args):uint{
            var _local2:Array = _items;
            checkValidTypes(_args);
            var _local3:int;
            var _local4:int = _args.length;
            while (_local3 < _local4) {
                addListener(_args[_local3]);
                items.push(_args[_local3]);
                _local3++;
            };
            initChange("items", _local2, _items, this);
            return (items.length);
        }
        public function removeListeners():void{
            var _local1:Object;
            for each (_local1 in items) {
                if ((_local1 is IDegrafaObject)){
                    IDegrafaObject(_local1).parent = null;
                    IDegrafaObject(_local1).removeEventListener(PropertyChangeEvent.PROPERTY_CHANGE, propertyChangeHandler);
                };
            };
        }
        protected function _setItemIndex(_arg1, _arg2:Number):Boolean{
            var _local3:Array = items.splice(items.indexOf(_arg1), 1);
            items.splice(_arg2, 0, _local3[0]);
            return (true);
        }
        protected function _getItemAt(_arg1:Number){
            return (items[_arg1]);
        }
        public function addListeners():void{
            var _local1:Object;
            if (enableEvents){
                for each (_local1 in items) {
                    if ((_local1 is IDegrafaObject)){
                        IDegrafaObject(_local1).parent = this.parent;
                        if (IDegrafaObject(_local1).enableEvents){
                            IDegrafaObject(_local1).addEventListener(PropertyChangeEvent.PROPERTY_CHANGE, propertyChangeHandler);
                        };
                    };
                };
            };
        }
        public function every(_arg1:Function, _arg2=null):Boolean{
            return (items.every(_arg1, _arg2));
        }
        public function map(_arg1:Function, _arg2=null):Array{
            return (items.map(_arg1, _arg2));
        }
        public function shift(){
            removeListener(items[0]);
            var _local1:Array = _items;
            var _local2:* = items.shift();
            initChange("items", _local1, _items, this);
            return (_local2);
        }
        public function propertyChangeHandler(_arg1:PropertyChangeEvent):void{
            if (!suppressEventProcessing){
                dispatchEvent(_arg1);
            };
        }
        protected function _getItemIndex(_arg1):int{
            return (indexOf(_arg1, 0));
        }
        protected function _removeItemAt(_arg1:Number){
            removeListener(items[_arg1]);
            return (splice(_arg1, 1)[1]);
        }
        public function forEach(_arg1:Function, _arg2=null):void{
            items.forEach(_arg1, _arg2);
        }
        public function sort(... _args):Array{
            return (items.sort(_args));
        }
        public function set enableTypeChecking(_arg1:Boolean):void{
            _enableTypeChecking = _arg1;
        }
        protected function _addItemAt(_arg1:Object, _arg2:Number){
            addListener(_arg1);
            splice(_arg2, 0, _arg1);
            return (_arg1);
        }
        public function join(_arg1):String{
            return (items.join(_arg1));
        }
        public function addListener(_arg1):void{
            if ((_arg1 is IDegrafaObject)){
                IDegrafaObject(_arg1).parent = this.parent;
                if (enableEvents){
                    if (IDegrafaObject(_arg1).enableEvents){
                        IDegrafaObject(_arg1).addEventListener(PropertyChangeEvent.PROPERTY_CHANGE, propertyChangeHandler);
                    };
                };
            };
        }
        public function filter(_arg1:Function, _arg2=null):Array{
            return (items.filter(_arg1, _arg2));
        }
        public function some(_arg1:Function, _arg2=null):Boolean{
            return (items.some(_arg1, _arg2));
        }
        public function splice(_arg1:int, _arg2:uint, ... _args):Array{
            var _local7:Array;
            checkValidTypes(_args);
            var _local4:int;
            var _local5:int = _args.length;
            while (_local4 < _local5) {
                addListener(_args[_local4]);
                _local4++;
            };
            var _local6:Array = _items;
            if (_args.length == 1){
                _local7 = _items.splice(_arg1, _arg2, _args[0]);
            } else {
                if (_args.length > 1){
                    _local7 = _items.splice(_arg1, _arg2, _args);
                } else {
                    _local7 = _items.splice(_arg1, _arg2);
                };
            };
            if (_local7){
                _local5 = _local7.length;
                _local4 = 0;
                while (_local4 < _local5) {
                    removeListener(_local7[_local4]);
                    _local4++;
                };
            };
            initChange("items", _local6, _items, this);
            return (_local7);
        }
        public function sortOn(_arg1:Object, _arg2:Object=null):Array{
            return (items.sortOn(_arg1, _arg2));
        }

    }
}//package com.degrafa.core.collections 
