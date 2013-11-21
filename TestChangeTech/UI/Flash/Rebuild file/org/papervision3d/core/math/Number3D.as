//Created by Action Script Viewer - http://www.buraks.com/asv
package org.papervision3d.core.math {
    import org.papervision3d.*;

    public class Number3D {

        public var x:Number
        public var y:Number
        public var z:Number

        public static var toDEGREES:Number = 57.2957795130823;
        private static var temp:Number3D = Number3D.ZERO;
        public static var toRADIANS:Number = 0.0174532925199433;

        public function Number3D(_arg1:Number=0, _arg2:Number=0, _arg3:Number=0){
            this.x = _arg1;
            this.y = _arg2;
            this.z = _arg3;
        }
        public function isModuloLessThan(_arg1:Number):Boolean{
            return ((moduloSquared < (_arg1 * _arg1)));
        }
        public function rotateX(_arg1:Number):void{
            if (Papervision3D.useDEGREES){
                _arg1 = (_arg1 * toRADIANS);
            };
            var _local2:Number = Math.cos(_arg1);
            var _local3:Number = Math.sin(_arg1);
            temp.copyFrom(this);
            this.y = ((temp.y * _local2) - (temp.z * _local3));
            this.z = ((temp.y * _local3) + (temp.z * _local2));
        }
        public function rotateY(_arg1:Number):void{
            if (Papervision3D.useDEGREES){
                _arg1 = (_arg1 * toRADIANS);
            };
            var _local2:Number = Math.cos(_arg1);
            var _local3:Number = Math.sin(_arg1);
            temp.copyFrom(this);
            this.x = ((temp.x * _local2) + (temp.z * _local3));
            this.z = ((temp.x * -(_local3)) + (temp.z * _local2));
        }
        public function plusEq(_arg1:Number3D):void{
            x = (x + _arg1.x);
            y = (y + _arg1.y);
            z = (z + _arg1.z);
        }
        public function multiplyEq(_arg1:Number):void{
            x = (x * _arg1);
            y = (y * _arg1);
            z = (z * _arg1);
        }
        public function toString():String{
            return (((((("x:" + (Math.round((x * 100)) / 100)) + " y:") + (Math.round((y * 100)) / 100)) + " z:") + (Math.round((z * 100)) / 100)));
        }
        public function normalize():void{
            var _local1:Number = this.modulo;
            if (((!((_local1 == 0))) && (!((_local1 == 1))))){
                this.x = (this.x / _local1);
                this.y = (this.y / _local1);
                this.z = (this.z / _local1);
            };
        }
        public function rotateZ(_arg1:Number):void{
            if (Papervision3D.useDEGREES){
                _arg1 = (_arg1 * toRADIANS);
            };
            var _local2:Number = Math.cos(_arg1);
            var _local3:Number = Math.sin(_arg1);
            temp.copyFrom(this);
            this.x = ((temp.x * _local2) - (temp.y * _local3));
            this.y = ((temp.x * _local3) + (temp.y * _local2));
        }
        public function reset(_arg1:Number=0, _arg2:Number=0, _arg3:Number=0):void{
            x = _arg1;
            y = _arg2;
            z = _arg3;
        }
        public function get moduloSquared():Number{
            return ((((this.x * this.x) + (this.y * this.y)) + (this.z * this.z)));
        }
        public function get modulo():Number{
            return (Math.sqrt((((this.x * this.x) + (this.y * this.y)) + (this.z * this.z))));
        }
        public function copyTo(_arg1:Number3D):void{
            _arg1.x = x;
            _arg1.y = y;
            _arg1.z = z;
        }
        public function isModuloGreaterThan(_arg1:Number):Boolean{
            return ((moduloSquared > (_arg1 * _arg1)));
        }
        public function minusEq(_arg1:Number3D):void{
            x = (x - _arg1.x);
            y = (y - _arg1.y);
            z = (z - _arg1.z);
        }
        public function clone():Number3D{
            return (new Number3D(this.x, this.y, this.z));
        }
        public function isModuloEqualTo(_arg1:Number):Boolean{
            return ((moduloSquared == (_arg1 * _arg1)));
        }
        public function copyFrom(_arg1:Number3D):void{
            x = _arg1.x;
            y = _arg1.y;
            z = _arg1.z;
        }

        public static function sub(_arg1:Number3D, _arg2:Number3D):Number3D{
            return (new Number3D((_arg1.x - _arg2.x), (_arg1.y - _arg2.y), (_arg1.z - _arg2.z)));
        }
        public static function add(_arg1:Number3D, _arg2:Number3D):Number3D{
            return (new Number3D((_arg1.x + _arg2.x), (_arg1.y + _arg2.y), (_arg1.z + _arg2.z)));
        }
        public static function cross(_arg1:Number3D, _arg2:Number3D, _arg3:Number3D=null):Number3D{
            if (!_arg3){
                _arg3 = ZERO;
            };
            _arg3.reset(((_arg2.y * _arg1.z) - (_arg2.z * _arg1.y)), ((_arg2.z * _arg1.x) - (_arg2.x * _arg1.z)), ((_arg2.x * _arg1.y) - (_arg2.y * _arg1.x)));
            return (_arg3);
        }
        public static function dot(_arg1:Number3D, _arg2:Number3D):Number{
            return ((((_arg1.x * _arg2.x) + (_arg1.y * _arg2.y)) + (_arg2.z * _arg1.z)));
        }
        public static function get ZERO():Number3D{
            return (new Number3D(0, 0, 0));
        }

    }
}//package org.papervision3d.core.math 
