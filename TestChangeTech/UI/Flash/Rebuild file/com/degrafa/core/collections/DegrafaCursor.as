//Created by Action Script Viewer - http://www.buraks.com/asv
package com.degrafa.core.collections {
    import mx.collections.*;

    public class DegrafaCursor {

        public var currentIndex:int
        public var source:Array

        protected static const BEFORE_FIRST_INDEX:int = -1;
        protected static const AFTER_LAST_INDEX:int = -2;

        public function DegrafaCursor(_arg1:Array){
            this.source = _arg1;
            currentIndex = BEFORE_FIRST_INDEX;
        }
        public function insert(_arg1):void{
            var _local2:int;
            if (((afterLast) || (beforeFirst))){
                source.push(_arg1);
            } else {
                source.splice(currentIndex, 0, _arg1);
            };
        }
        public function get afterLast():Boolean{
            return ((((currentIndex == AFTER_LAST_INDEX)) || ((source.length == 0))));
        }
        public function moveNext():Boolean{
            if (afterLast){
                return (false);
            };
            var _local1:int = (beforeFirst) ? 0 : (currentIndex + 1);
            if (_local1 >= source.length){
                _local1 = AFTER_LAST_INDEX;
            };
            currentIndex = _local1;
            return (!(afterLast));
        }
        public function moveLast():void{
            currentIndex = source.length;
        }
        public function movePrevious():Boolean{
            if (beforeFirst){
                return (false);
            };
            var _local1:int = (afterLast) ? (source.length - 1) : (currentIndex - 1);
            currentIndex = _local1;
            return (!(beforeFirst));
        }
        public function get beforeFirst():Boolean{
            return ((((currentIndex == BEFORE_FIRST_INDEX)) || ((source.length == 0))));
        }
        public function get nextObject(){
            if (afterLast){
                return (null);
            };
            var _local1:int = (beforeFirst) ? 0 : (currentIndex + 1);
            if (_local1 >= source.length){
                return (null);
            };
            return (source[_local1]);
        }
        public function remove(){
            var _local1:Object = source[currentIndex];
            source = source.splice(currentIndex, 1);
            return (_local1);
        }
        public function get current(){
            if (currentIndex > BEFORE_FIRST_INDEX){
                return (source[currentIndex]);
            };
            return (null);
        }
        public function seek(_arg1:CursorBookmark, _arg2:int=0):void{
            if (source.length == 0){
                currentIndex = AFTER_LAST_INDEX;
                return;
            };
            var _local3:int = currentIndex;
            if (_arg1 == CursorBookmark.FIRST){
                _local3 = 0;
            } else {
                if (_arg1 == CursorBookmark.LAST){
                    _local3 = (source.length - 1);
                };
            };
            _local3 = (_local3 + _arg2);
            if (_local3 >= source.length){
                currentIndex = AFTER_LAST_INDEX;
            } else {
                if (_local3 < 0){
                    currentIndex = BEFORE_FIRST_INDEX;
                } else {
                    currentIndex = _local3;
                };
            };
        }
        public function get previousObject(){
            if (beforeFirst){
                return (null);
            };
            var _local1:int = (afterLast) ? (source.length - 1) : (currentIndex - 1);
            if (_local1 == BEFORE_FIRST_INDEX){
                return (null);
            };
            return (source[_local1]);
        }
        public function moveFirst():void{
            currentIndex = BEFORE_FIRST_INDEX;
        }

    }
}//package com.degrafa.core.collections 
