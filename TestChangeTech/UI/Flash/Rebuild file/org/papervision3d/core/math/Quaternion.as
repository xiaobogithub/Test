//Created by Action Script Viewer - http://www.buraks.com/asv
package org.papervision3d.core.math {

    public class Quaternion {

        private var _matrix:Matrix3D
        public var w:Number
        public var x:Number
        public var y:Number
        public var z:Number

        public static const EPSILON:Number = 1E-6;
        public static const DEGTORAD:Number = 0.0174532925199433;
        public static const RADTODEG:Number = 57.2957795130823;

        public function Quaternion(_arg1:Number=0, _arg2:Number=0, _arg3:Number=0, _arg4:Number=1){
            this.x = _arg1;
            this.y = _arg2;
            this.z = _arg3;
            this.w = _arg4;
            _matrix = Matrix3D.IDENTITY;
        }
        public function get matrix():Matrix3D{
            var _local1:Number = (x * x);
            var _local2:Number = (x * y);
            var _local3:Number = (x * z);
            var _local4:Number = (x * w);
            var _local5:Number = (y * y);
            var _local6:Number = (y * z);
            var _local7:Number = (y * w);
            var _local8:Number = (z * z);
            var _local9:Number = (z * w);
            _matrix.n11 = (1 - (2 * (_local5 + _local8)));
            _matrix.n12 = (2 * (_local2 - _local9));
            _matrix.n13 = (2 * (_local3 + _local7));
            _matrix.n21 = (2 * (_local2 + _local9));
            _matrix.n22 = (1 - (2 * (_local1 + _local8)));
            _matrix.n23 = (2 * (_local6 - _local4));
            _matrix.n31 = (2 * (_local3 - _local7));
            _matrix.n32 = (2 * (_local6 + _local4));
            _matrix.n33 = (1 - (2 * (_local1 + _local5)));
            return (_matrix);
        }
        public function setFromEuler(_arg1:Number, _arg2:Number, _arg3:Number, _arg4:Boolean=false):void{
            if (_arg4){
                _arg1 = (_arg1 * DEGTORAD);
                _arg2 = (_arg2 * DEGTORAD);
                _arg3 = (_arg3 * DEGTORAD);
            };
            var _local5:Number = Math.sin((_arg1 * 0.5));
            var _local6:Number = Math.cos((_arg1 * 0.5));
            var _local7:Number = Math.sin((_arg2 * 0.5));
            var _local8:Number = Math.cos((_arg2 * 0.5));
            var _local9:Number = Math.sin((_arg3 * 0.5));
            var _local10:Number = Math.cos((_arg3 * 0.5));
            var _local11:Number = (_local6 * _local8);
            var _local12:Number = (_local5 * _local7);
            this.x = ((_local9 * _local11) - (_local10 * _local12));
            this.y = (((_local10 * _local5) * _local8) + ((_local9 * _local6) * _local7));
            this.z = (((_local10 * _local6) * _local7) - ((_local9 * _local5) * _local8));
            this.w = ((_local10 * _local11) + (_local9 * _local12));
        }
        public function setFromAxisAngle(_arg1:Number, _arg2:Number, _arg3:Number, _arg4:Number):void{
            var _local5:Number = Math.sin((_arg4 / 2));
            var _local6:Number = Math.cos((_arg4 / 2));
            this.x = (_arg1 * _local5);
            this.y = (_arg2 * _local5);
            this.z = (_arg3 * _local5);
            this.w = _local6;
            this.normalize();
        }
        public function calculateMultiply(_arg1:Quaternion, _arg2:Quaternion):void{
            this.x = ((((_arg1.w * _arg2.x) + (_arg1.x * _arg2.w)) + (_arg1.y * _arg2.z)) - (_arg1.z * _arg2.y));
            this.y = ((((_arg1.w * _arg2.y) - (_arg1.x * _arg2.z)) + (_arg1.y * _arg2.w)) + (_arg1.z * _arg2.x));
            this.z = ((((_arg1.w * _arg2.z) + (_arg1.x * _arg2.y)) - (_arg1.y * _arg2.x)) + (_arg1.z * _arg2.w));
            this.w = ((((_arg1.w * _arg2.w) - (_arg1.x * _arg2.x)) - (_arg1.y * _arg2.y)) - (_arg1.z * _arg2.z));
        }
        public function toString():String{
            return (((((((("Quaternion: x:" + this.x) + " y:") + this.y) + " z:") + this.z) + " w:") + this.w));
        }
        public function normalize():void{
            var _local2:Number;
            var _local1:Number = this.modulo;
            if (Math.abs(_local1) < EPSILON){
                x = (y = (z = 0));
                w = 1;
            } else {
                _local2 = (1 / _local1);
                x = (x * _local2);
                y = (y * _local2);
                z = (z * _local2);
                w = (w * _local2);
            };
        }
        public function toEuler():Number3D{
            var _local1:Number3D = new Number3D();
            var _local2:Quaternion = this;
            var _local3:Number = ((_local2.x * _local2.y) + (_local2.z * _local2.w));
            if (_local3 > 0.499){
                _local1.x = (2 * Math.atan2(_local2.x, _local2.w));
                _local1.y = (Math.PI / 2);
                _local1.z = 0;
                return (_local1);
            };
            if (_local3 < -0.499){
                _local1.x = (-2 * Math.atan2(_local2.x, _local2.w));
                _local1.y = (-(Math.PI) / 2);
                _local1.z = 0;
                return (_local1);
            };
            var _local4:Number = (_local2.x * _local2.x);
            var _local5:Number = (_local2.y * _local2.y);
            var _local6:Number = (_local2.z * _local2.z);
            _local1.x = Math.atan2((((2 * _local2.y) * _local2.w) - ((2 * _local2.x) * _local2.z)), ((1 - (2 * _local5)) - (2 * _local6)));
            _local1.y = Math.asin((2 * _local3));
            _local1.z = Math.atan2((((2 * _local2.x) * _local2.w) - ((2 * _local2.y) * _local2.z)), ((1 - (2 * _local4)) - (2 * _local6)));
            return (_local1);
        }
        public function get modulo():Number{
            return (Math.sqrt(((((x * x) + (y * y)) + (z * z)) + (w * w))));
        }
        public function clone():Quaternion{
            return (new Quaternion(this.x, this.y, this.z, this.w));
        }
        public function mult(_arg1:Quaternion):void{
            var _local2:Number = this.w;
            var _local3:Number = this.x;
            var _local4:Number = this.y;
            var _local5:Number = this.z;
            x = ((((_local2 * _arg1.x) + (_local3 * _arg1.w)) + (_local4 * _arg1.z)) - (_local5 * _arg1.y));
            y = ((((_local2 * _arg1.y) - (_local3 * _arg1.z)) + (_local4 * _arg1.w)) + (_local5 * _arg1.x));
            z = ((((_local2 * _arg1.z) + (_local3 * _arg1.y)) - (_local4 * _arg1.x)) + (_local5 * _arg1.w));
            w = ((((_local2 * _arg1.w) - (_local3 * _arg1.x)) - (_local4 * _arg1.y)) - (_local5 * _arg1.z));
        }

        public static function sub(_arg1:Quaternion, _arg2:Quaternion):Quaternion{
            return (new Quaternion((_arg1.x - _arg2.x), (_arg1.y - _arg2.y), (_arg1.z - _arg2.z), (_arg1.w - _arg2.w)));
        }
        public static function add(_arg1:Quaternion, _arg2:Quaternion):Quaternion{
            return (new Quaternion((_arg1.x + _arg2.x), (_arg1.y + _arg2.y), (_arg1.z + _arg2.z), (_arg1.w + _arg2.w)));
        }
        public static function createFromEuler(_arg1:Number, _arg2:Number, _arg3:Number, _arg4:Boolean=false):Quaternion{
            if (_arg4){
                _arg1 = (_arg1 * DEGTORAD);
                _arg2 = (_arg2 * DEGTORAD);
                _arg3 = (_arg3 * DEGTORAD);
            };
            var _local5:Number = Math.sin((_arg1 * 0.5));
            var _local6:Number = Math.cos((_arg1 * 0.5));
            var _local7:Number = Math.sin((_arg2 * 0.5));
            var _local8:Number = Math.cos((_arg2 * 0.5));
            var _local9:Number = Math.sin((_arg3 * 0.5));
            var _local10:Number = Math.cos((_arg3 * 0.5));
            var _local11:Number = (_local6 * _local8);
            var _local12:Number = (_local5 * _local7);
            var _local13:Quaternion = new (Quaternion);
            _local13.x = ((_local9 * _local11) - (_local10 * _local12));
            _local13.y = (((_local10 * _local5) * _local8) + ((_local9 * _local6) * _local7));
            _local13.z = (((_local10 * _local6) * _local7) - ((_local9 * _local5) * _local8));
            _local13.w = ((_local10 * _local11) + (_local9 * _local12));
            return (_local13);
        }
        public static function createFromMatrix(_arg1:Matrix3D):Quaternion{
            var _local3:Number;
            var _local5:int;
            var _local6:int;
            var _local7:int;
            var _local9:Array;
            var _local10:Array;
            var _local2:Quaternion = new (Quaternion);
            var _local4:Array = new Array(4);
            var _local8:Number = ((_arg1.n11 + _arg1.n22) + _arg1.n33);
            if (_local8 > 0){
                _local3 = Math.sqrt((_local8 + 1));
                _local2.w = (_local3 / 2);
                _local3 = (0.5 / _local3);
                _local2.x = ((_arg1.n32 - _arg1.n23) * _local3);
                _local2.y = ((_arg1.n13 - _arg1.n31) * _local3);
                _local2.z = ((_arg1.n21 - _arg1.n12) * _local3);
            } else {
                _local9 = [1, 2, 0];
                _local10 = [[_arg1.n11, _arg1.n12, _arg1.n13, _arg1.n14], [_arg1.n21, _arg1.n22, _arg1.n23, _arg1.n24], [_arg1.n31, _arg1.n32, _arg1.n33, _arg1.n34]];
                _local5 = 0;
                if (_local10[1][1] > _local10[0][0]){
                    _local5 = 1;
                };
                if (_local10[2][2] > _local10[_local5][_local5]){
                    _local5 = 2;
                };
                _local6 = _local9[_local5];
                _local7 = _local9[_local6];
                _local3 = Math.sqrt(((_local10[_local5][_local5] - (_local10[_local6][_local6] + _local10[_local7][_local7])) + 1));
                _local4[_local5] = (_local3 * 0.5);
                if (_local3 != 0){
                    _local3 = (0.5 / _local3);
                };
                _local4[3] = ((_local10[_local7][_local6] - _local10[_local6][_local7]) * _local3);
                _local4[_local6] = ((_local10[_local6][_local5] + _local10[_local5][_local6]) * _local3);
                _local4[_local7] = ((_local10[_local7][_local5] + _local10[_local5][_local7]) * _local3);
                _local2.x = _local4[0];
                _local2.y = _local4[1];
                _local2.z = _local4[2];
                _local2.w = _local4[3];
            };
            return (_local2);
        }
        public static function dot(_arg1:Quaternion, _arg2:Quaternion):Number{
            return (((((_arg1.x * _arg2.x) + (_arg1.y * _arg2.y)) + (_arg1.z * _arg2.z)) + (_arg1.w * _arg2.w)));
        }
        public static function multiply(_arg1:Quaternion, _arg2:Quaternion):Quaternion{
            var _local3:Quaternion = new (Quaternion);
            _local3.x = ((((_arg1.w * _arg2.x) + (_arg1.x * _arg2.w)) + (_arg1.y * _arg2.z)) - (_arg1.z * _arg2.y));
            _local3.y = ((((_arg1.w * _arg2.y) - (_arg1.x * _arg2.z)) + (_arg1.y * _arg2.w)) + (_arg1.z * _arg2.x));
            _local3.z = ((((_arg1.w * _arg2.z) + (_arg1.x * _arg2.y)) - (_arg1.y * _arg2.x)) + (_arg1.z * _arg2.w));
            _local3.w = ((((_arg1.w * _arg2.w) - (_arg1.x * _arg2.x)) - (_arg1.y * _arg2.y)) - (_arg1.z * _arg2.z));
            return (_local3);
        }
        public static function createFromAxisAngle(_arg1:Number, _arg2:Number, _arg3:Number, _arg4:Number):Quaternion{
            var _local5:Quaternion = new (Quaternion);
            _local5.setFromAxisAngle(_arg1, _arg2, _arg3, _arg4);
            return (_local5);
        }
        public static function slerp(_arg1:Quaternion, _arg2:Quaternion, _arg3:Number):Quaternion{
            var _local5:Number;
            var _local6:Number;
            var _local7:Number;
            var _local8:Number;
            var _local4:Number = ((((_arg1.w * _arg2.w) + (_arg1.x * _arg2.x)) + (_arg1.y * _arg2.y)) + (_arg1.z * _arg2.z));
            if (_local4 < 0){
                _arg1.x = (_arg1.x * -1);
                _arg1.y = (_arg1.y * -1);
                _arg1.z = (_arg1.z * -1);
                _arg1.w = (_arg1.w * -1);
                _local4 = (_local4 * -1);
            };
            if ((_local4 + 1) > EPSILON){
                if ((1 - _local4) >= EPSILON){
                    _local7 = Math.acos(_local4);
                    _local8 = (1 / Math.sin(_local7));
                    _local5 = (Math.sin((_local7 * (1 - _arg3))) * _local8);
                    _local6 = (Math.sin((_local7 * _arg3)) * _local8);
                } else {
                    _local5 = (1 - _arg3);
                    _local6 = _arg3;
                };
            } else {
                _arg2.y = -(_arg1.y);
                _arg2.x = _arg1.x;
                _arg2.w = -(_arg1.w);
                _arg2.z = _arg1.z;
                _local5 = Math.sin((Math.PI * (0.5 - _arg3)));
                _local6 = Math.sin((Math.PI * _arg3));
            };
            return (new Quaternion(((_local5 * _arg1.x) + (_local6 * _arg2.x)), ((_local5 * _arg1.y) + (_local6 * _arg2.y)), ((_local5 * _arg1.z) + (_local6 * _arg2.z)), ((_local5 * _arg1.w) + (_local6 * _arg2.w))));
        }
        public static function createFromOrthoMatrix(_arg1:Matrix3D):Quaternion{
            var _local2:Quaternion = new (Quaternion);
            _local2.w = (Math.sqrt(Math.max(0, (((1 + _arg1.n11) + _arg1.n22) + _arg1.n33))) / 2);
            _local2.x = (Math.sqrt(Math.max(0, (((1 + _arg1.n11) - _arg1.n22) - _arg1.n33))) / 2);
            _local2.y = (Math.sqrt(Math.max(0, (((1 - _arg1.n11) + _arg1.n22) - _arg1.n33))) / 2);
            _local2.z = (Math.sqrt(Math.max(0, (((1 - _arg1.n11) - _arg1.n22) + _arg1.n33))) / 2);
            _local2.x = (((_arg1.n32 - _arg1.n23) < 0)) ? ((_local2.x < 0)) ? _local2.x : -(_local2.x) : ((_local2.x < 0)) ? -(_local2.x) : _local2.x;
            _local2.y = (((_arg1.n13 - _arg1.n31) < 0)) ? ((_local2.y < 0)) ? _local2.y : -(_local2.y) : ((_local2.y < 0)) ? -(_local2.y) : _local2.y;
            _local2.z = (((_arg1.n21 - _arg1.n12) < 0)) ? ((_local2.z < 0)) ? _local2.z : -(_local2.z) : ((_local2.z < 0)) ? -(_local2.z) : _local2.z;
            return (_local2);
        }
        public static function conjugate(_arg1:Quaternion):Quaternion{
            var _local2:Quaternion = new (Quaternion);
            _local2.x = -(_arg1.x);
            _local2.y = -(_arg1.y);
            _local2.z = -(_arg1.z);
            _local2.w = _arg1.w;
            return (_local2);
        }
        public static function slerpOld(_arg1:Quaternion, _arg2:Quaternion, _arg3:Number):Quaternion{
            var _local4:Quaternion = new (Quaternion);
            var _local5:Number = ((((_arg1.w * _arg2.w) + (_arg1.x * _arg2.x)) + (_arg1.y * _arg2.y)) + (_arg1.z * _arg2.z));
            if (Math.abs(_local5) >= 1){
                _local4.w = _arg1.w;
                _local4.x = _arg1.x;
                _local4.y = _arg1.y;
                _local4.z = _arg1.z;
                return (_local4);
            };
            var _local6:Number = Math.acos(_local5);
            var _local7:Number = Math.sqrt((1 - (_local5 * _local5)));
            if (Math.abs(_local7) < 0.001){
                _local4.w = ((_arg1.w * 0.5) + (_arg2.w * 0.5));
                _local4.x = ((_arg1.x * 0.5) + (_arg2.x * 0.5));
                _local4.y = ((_arg1.y * 0.5) + (_arg2.y * 0.5));
                _local4.z = ((_arg1.z * 0.5) + (_arg2.z * 0.5));
                return (_local4);
            };
            var _local8:Number = (Math.sin(((1 - _arg3) * _local6)) / _local7);
            var _local9:Number = (Math.sin((_arg3 * _local6)) / _local7);
            _local4.w = ((_arg1.w * _local8) + (_arg2.w * _local9));
            _local4.x = ((_arg1.x * _local8) + (_arg2.x * _local9));
            _local4.y = ((_arg1.y * _local8) + (_arg2.y * _local9));
            _local4.z = ((_arg1.z * _local8) + (_arg2.z * _local9));
            return (_local4);
        }

    }
}//package org.papervision3d.core.math 
