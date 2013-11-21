//Created by Action Script Viewer - http://www.buraks.com/asv
package org.papervision3d.core.proto {
    import flash.events.*;
    import flash.utils.*;
    import org.papervision3d.objects.*;

    public class DisplayObjectContainer3D extends EventDispatcher {

        protected var _childrenByName:Object
        public var root:DisplayObjectContainer3D
        private var _childrenTotal:int
        protected var _children:Dictionary

        public function DisplayObjectContainer3D():void{
            this._children = new Dictionary(false);
            this._childrenByName = new Dictionary(true);
            this._childrenTotal = 0;
        }
        private function findChildByName(_arg1:String, _arg2:DisplayObject3D=null):DisplayObject3D{
            var _local3:DisplayObject3D;
            var _local4:DisplayObject3D;
            _arg2 = ((_arg2) || (DisplayObject3D(this)));
            if (!_arg2){
                return (null);
            };
            if (_arg2.name == _arg1){
                return (_arg2);
            };
            for each (_local3 in _arg2.children) {
                _local4 = findChildByName(_arg1, _local3);
                if (_local4){
                    return (_local4);
                };
            };
            return (null);
        }
        public function getChildByName(_arg1:String, _arg2:Boolean=false):DisplayObject3D{
            if (_arg2){
                return (findChildByName(_arg1));
            };
            return (this._childrenByName[_arg1]);
        }
        override public function toString():String{
            return (childrenList());
        }
        public function addChildren(_arg1:DisplayObject3D):DisplayObjectContainer3D{
            var _local2:DisplayObject3D;
            for each (_local2 in _arg1.children) {
                _arg1.removeChild(_local2);
                this.addChild(_local2);
            };
            return (this);
        }
        public function get numChildren():int{
            return (this._childrenTotal);
        }
        public function removeChild(_arg1:DisplayObject3D):DisplayObject3D{
            if (_arg1){
                delete this._childrenByName[this._children[_arg1]];
                delete this._children[_arg1];
                _arg1.parent = null;
                _arg1.root = null;
                _childrenTotal--;
                return (_arg1);
            };
            return (null);
        }
        public function removeChildByName(_arg1:String):DisplayObject3D{
            return (removeChild(getChildByName(_arg1)));
        }
        public function addChild(_arg1:DisplayObject3D, _arg2:String=null):DisplayObject3D{
            _arg2 = ((((_arg2) || (_arg1.name))) || (String(_arg1.id)));
            this._children[_arg1] = _arg2;
            this._childrenByName[_arg2] = _arg1;
            this._childrenTotal++;
            _arg1.parent = this;
            _arg1.root = this.root;
            return (_arg1);
        }
        public function childrenList():String{
            var _local2:String;
            var _local1 = "";
            for (_local2 in this._children) {
                _local1 = (_local1 + (_local2 + "\n"));
            };
            return (_local1);
        }
        public function get children():Object{
            return (this._childrenByName);
        }

    }
}//package org.papervision3d.core.proto 
