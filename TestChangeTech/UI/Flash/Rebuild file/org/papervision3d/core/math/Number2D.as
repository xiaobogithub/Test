//Created by Action Script Viewer - http://www.buraks.com/asv
package org.papervision3d.core.math {
    import org.papervision3d.*;

    public class Number2D {

        public var x:Number
        public var y:Number

        public static const DEGTORAD:Number = 0.0174532925199433;
        public static const RADTODEG:Number = 57.2957795130823;

        public function Number2D(_arg1:Number=0, _arg2:Number=0){
            this.x = _arg1;
            this.y = _arg2;
        }
        public function isModuloLessThan(_arg1:Number):Boolean{
            return ((moduloSquared < (_arg1 * _arg1)));
        }
        public function reverse():void{
            this.x = -(this.x);
            this.y = -(this.y);
        }
        public function divideEq(_arg1:Number):void{
            x = (x / _arg1);
            y = (y / _arg1);
        }
        public function plusEq(_arg1:Number2D):void{
            x = (x + _arg1.x);
            y = (y + _arg1.y);
        }
        public function multiplyEq(_arg1:Number):void{
            x = (x * _arg1);
            y = (y * _arg1);
        }
        public function isModuloGreaterThan(_arg1:Number):Boolean{
            return ((moduloSquared > (_arg1 * _arg1)));
        }
        public function toString():String{
            var _local1:Number = (Math.round((this.x * 1000)) / 1000);
            var _local2:Number = (Math.round((this.y * 1000)) / 1000);
            return ((((("[" + _local1) + ", ") + _local2) + "]"));
        }
        public function reset(_arg1:Number=0, _arg2:Number=0):void{
            this.x = _arg1;
            this.y = _arg2;
        }
        public function get moduloSquared():Number{
            return (((this.x * this.x) + (this.y * this.y)));
        }
        public function normalise():void{
            var _local1:Number = this.modulo;
            this.x = (this.x / _local1);
            this.y = (this.y / _local1);
        }
        public function get modulo():Number{
            return (Math.sqrt(((x * x) + (y * y))));
        }
        public function copyTo(_arg1:Number2D):void{
            _arg1.x = this.x;
            _arg1.y = this.y;
        }
        public function angle():Number{
            if (Papervision3D.useDEGREES){
                return ((RADTODEG * Math.atan2(y, x)));
            };
            return (Math.atan2(y, x));
        }
        public function rotate(_arg1:Number):void{
            var _local4:Number2D;
            if (Papervision3D.useDEGREES){
                _arg1 = (_arg1 * DEGTORAD);
            };
            var _local2:Number = Math.cos(_arg1);
            var _local3:Number = Math.sin(_arg1);
            _local4 = clone();
            this.x = ((_local4.x * _local2) - (_local4.y * _local3));
            this.y = ((_local4.x * _local3) + (_local4.y * _local2));
        }
        public function minusEq(_arg1:Number2D):void{
            x = (x - _arg1.x);
            y = (y - _arg1.y);
        }
        public function clone():Number2D{
            return (new Number2D(this.x, this.y));
        }
        public function isModuloEqualTo(_arg1:Number):Boolean{
            return ((moduloSquared == (_arg1 * _arg1)));
        }
        public function copyFrom(_arg1:Number2D):void{
            this.x = _arg1.x;
            this.y = _arg1.y;
        }

        public static function multiplyScalar(_arg1:Number2D, _arg2:Number):Number2D{
            return (new Number2D((_arg1.x * _arg2), (_arg1.y * _arg2)));
        }
        public static function add(_arg1:Number2D, _arg2:Number2D):Number2D{
            return (new Number2D((_arg1.x = (_arg1.x + _arg2.x)), (_arg1.y + _arg2.y)));
        }
        public static function dot(_arg1:Number2D, _arg2:Number2D):Number{
            return (((_arg1.x * _arg2.x) + (_arg1.y * _arg2.y)));
        }
        public static function subtract(_arg1:Number2D, _arg2:Number2D):Number2D{
            return (new Number2D((_arg1.x - _arg2.x), (_arg1.y - _arg2.y)));
        }

    }
}//package org.papervision3d.core.math 
