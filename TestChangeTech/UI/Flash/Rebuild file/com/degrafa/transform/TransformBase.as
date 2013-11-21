//Created by Action Script Viewer - http://www.buraks.com/asv
package com.degrafa.transform {
    import flash.geom.*;
    import com.degrafa.*;
    import com.degrafa.geometry.*;
    import com.degrafa.core.*;

    public class TransformBase extends DegrafaObject implements ITransform {

        public var invalidated:Boolean
        protected var _transformMatrix:Matrix
        protected var _skewX:Number = 0
        private var _data:String
        protected var _skewY:Number = 0
        protected var _tx:Number = 0
        protected var _ty:Number = 0
        protected var _angle:Number = 0
        protected var _registrationPoint:String
        protected var _centerX:Number = NAN
        protected var _centerY:Number = NAN
        protected var _scaleX:Number = 1
        protected var _scaleY:Number = 1

        private static var identity:Matrix = new Matrix();

        public function TransformBase(){
            _transformMatrix = new Matrix();
            super();
        }
        public function hasExplicitSetting():Boolean{
            return (((_registrationPoint) || (!(((isNaN(_centerX)) || (isNaN(_centerY)))))));
        }
        public function get isIdentity():Boolean{
            var _local1:Matrix;
            if (invalidated){
                _local1 = transformMatrix;
            };
            return ((((((((((((_transformMatrix.a == 1)) && (!(_transformMatrix.b)))) && (!(_transformMatrix.c)))) && ((_transformMatrix.d == 1)))) && (!(_transformMatrix.tx)))) && (!(_transformMatrix.ty))));
        }
        public function get skewX():Number{
            return (_skewX);
        }
        public function get skewY():Number{
            return (_skewY);
        }
        public function getRegPointForRectangle(_arg1:Rectangle):Point{
            var _local2:Point;
            if (_registrationPoint){
                _local2 = getRegistrationPoint(null, _arg1);
            } else {
                _local2 = new Point(((_arg1.x + (_arg1.width / 2)) + centerX), ((_arg1.y + (_arg1.height / 2)) + centerY));
            };
            return (_local2);
        }
        public function set transformMatrix(_arg1:Matrix):void{
            _transformMatrix = _arg1;
        }
        protected function getRegistrationPoint(_arg1:IGeometryComposition, _arg2:Rectangle=null):Point{
            var _local3:Point;
            if (_arg1){
                _arg2 = ((_arg1 as Geometry).hasLayout) ? Geometry(_arg1).layoutRectangle : Geometry(_arg1).bounds;
            };
            switch (_registrationPoint){
                case "topLeft":
                    _local3 = _arg2.topLeft;
                    break;
                case "centerLeft":
                    _local3 = new Point(_arg2.left, (_arg2.y + (_arg2.height / 2)));
                    break;
                case "bottomLeft":
                    _local3 = new Point(_arg2.left, _arg2.bottom);
                    break;
                case "centerTop":
                    _local3 = new Point((_arg2.x + (_arg2.width / 2)), _arg2.y);
                    break;
                case "center":
                    _local3 = new Point((_arg2.x + (_arg2.width / 2)), (_arg2.y + (_arg2.height / 2)));
                    break;
                case "centerBottom":
                    _local3 = new Point((_arg2.x + (_arg2.width / 2)), _arg2.bottom);
                    break;
                case "topRight":
                    _local3 = new Point(_arg2.right, _arg2.top);
                    break;
                case "centerRight":
                    _local3 = new Point(_arg2.right, (_arg2.y + (_arg2.height / 2)));
                    break;
                case "bottomRight":
                    _local3 = _arg2.bottomRight;
                    break;
            };
            return (_local3);
        }
        public function get data():String{
            return (_data);
        }
        public function get centerX():Number{
            return ((isNaN(_centerX)) ? 0 : _centerX);
        }
        public function get centerY():Number{
            return ((isNaN(_centerY)) ? 0 : _centerY);
        }
        public function set registrationPoint(_arg1:String):void{
            var _local2:String;
            if (_registrationPoint != _arg1){
                _local2 = _registrationPoint;
                _registrationPoint = _arg1;
            };
        }
        public function getTransformFor(_arg1:IGeometryComposition):Matrix{
            var _local2:Point = (_registrationPoint) ? getRegistrationPoint(_arg1) : new Point(centerX, centerY);
            var _local3:Geometry = (_arg1 as Geometry);
            var _local4:Matrix = _local3.transformContext;
            if (!_local4){
                while (_local3.parent) {
                    _local3 = (_local3.parent as Geometry);
                    if (_local3.transform){
                        _local4 = _local3.transform.getTransformFor(_local3);
                        break;
                    };
                };
            };
            var _local5:Matrix = new Matrix();
            _local5.translate(-(_local2.x), -(_local2.y));
            _local5.concat(transformMatrix);
            _local5.translate(_local2.x, _local2.y);
            if (_local4){
                _local5.concat(_local4);
            };
            return (_local5);
        }
        public function set centerY(_arg1:Number):void{
            if (_centerY != _arg1){
                _centerY = _arg1;
                invalidated = true;
            };
        }
        public function get transformMatrix():Matrix{
            var _local1:Matrix;
            if (!invalidated){
                return (_transformMatrix);
            };
            _transformMatrix.identity();
            if (((!((_scaleX == 1))) || (!((_scaleY == 1))))){
                _transformMatrix.scale(_scaleX, _scaleY);
            };
            if (((_skewX) || (_skewY))){
                _local1 = new Matrix();
                _local1.a = Math.cos(((_skewY * Math.PI) / 180));
                _local1.b = Math.sin(((_skewY * Math.PI) / 180));
                _local1.c = -(Math.sin(((_skewX * Math.PI) / 180)));
                _local1.d = Math.cos(((_skewX * Math.PI) / 180));
                _transformMatrix.concat(_local1);
            };
            if (_angle){
                _transformMatrix.rotate(((_angle * Math.PI) / 180));
            };
            if (((_tx) || (_ty))){
                _transformMatrix.translate(_tx, _ty);
            };
            invalidated = false;
            return (_transformMatrix);
        }
        public function get scaleX():Number{
            return (_scaleX);
        }
        public function getRegPoint(_arg1:IGeometryComposition):Point{
            if (_registrationPoint){
                return (getRegistrationPoint(_arg1));
            };
            return (new Point(centerX, centerY));
        }
        public function get angle():Number{
            return (_angle);
        }
        public function set centerX(_arg1:Number):void{
            if (_centerX != _arg1){
                _centerX = _arg1;
                invalidated = true;
            };
        }
        public function get scaleY():Number{
            return (_scaleY);
        }
        public function set data(_arg1:String):void{
            _data = _arg1;
        }
        public function get registrationPoint():String{
            return (_registrationPoint);
        }
        public function get x():Number{
            return (_tx);
        }
        public function get y():Number{
            return (_ty);
        }
        public function getTransformedBoundsFor(_arg1:IGeometryComposition):Rectangle{
            var _local2:Geometry = (_arg1 as Geometry);
            var _local3:Matrix = getTransformFor(_arg1);
            return (transformBounds(_local2.bounds, _local3));
        }

        public static function getRenderedBounds(_arg1:IGeometryComposition):Rectangle{
            var _local3:Matrix;
            var _local2:Geometry = (_arg1 as Geometry);
            if (_local2.transform){
                return ((_local2.transform as TransformBase).getTransformedBoundsFor(_arg1));
            };
            _local3 = _local2.transformContext;
            if (!_local3){
                while (_local2.parent) {
                    _local2 = (_local2.parent as Geometry);
                    if (_local2.transform){
                        _local3 = _local2.transform.getTransformFor(_local2);
                        break;
                    };
                };
            };
            if (_local3){
                return (transformBounds(_local2.bounds, _local3));
            };
            return (_local2.bounds.clone());
        }
        public static function transformBounds(_arg1:Rectangle, _arg2:Matrix):Rectangle{
            var _local6:Point;
            var _local7:Point;
            var _local9:Point;
            var _local3:Rectangle = _arg1.clone();
            var _local4:Point = _local3.topLeft;
            var _local5:Point = _local3.bottomRight;
            _local6 = _local4.clone();
            _local6.offset((_local5.x - _local4.x), 0);
            _local7 = _local4.clone();
            _local7.offset(0, (_local5.y - _local4.y));
            var _local8:Array = [_arg2.transformPoint(_local5), _arg2.transformPoint(_local6), _arg2.transformPoint(_local7)];
            _local3.setEmpty();
            _local3.topLeft = _arg2.transformPoint(_local4);
            for each (_local9 in _local8) {
                if (_local3.x > _local9.x){
                    _local3.x = _local9.x;
                };
                if (_local3.y > _local9.y){
                    _local3.y = _local9.y;
                };
                if (_local3.right < _local9.x){
                    _local3.right = _local9.x;
                };
                if (_local3.bottom < _local9.y){
                    _local3.bottom = _local9.y;
                };
            };
            return (_local3);
        }

    }
}//package com.degrafa.transform 
