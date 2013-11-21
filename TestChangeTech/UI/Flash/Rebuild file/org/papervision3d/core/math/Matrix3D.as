//Created by Action Script Viewer - http://www.buraks.com/asv
package org.papervision3d.core.math {
    import org.papervision3d.*;

    public class Matrix3D {

        public var n31:Number
        public var n32:Number
        public var n11:Number
        public var n34:Number
        public var n13:Number
        public var n14:Number
        public var n33:Number
        public var n12:Number
        public var n41:Number
        public var n42:Number
        public var n21:Number
        public var n22:Number
        public var n23:Number
        public var n24:Number
        public var n44:Number
        public var n43:Number

        private static var _cos:Function = Math.cos;
        private static var _sin:Function = Math.sin;
        private static var temp:Matrix3D = Matrix3D.IDENTITY;
        private static var n3Di:Number3D = Number3D.ZERO;
        private static var n3Dj:Number3D = Number3D.ZERO;
        private static var n3Dk:Number3D = Number3D.ZERO;
        private static var toDEGREES:Number = 57.2957795130823;
        private static var toRADIANS:Number = 0.0174532925199433;

        public function Matrix3D(_arg1:Array=null){
            reset(_arg1);
        }
        public function calculateMultiply3x3(_arg1:Matrix3D, _arg2:Matrix3D):void{
            var _local3:Number = _arg1.n11;
            var _local4:Number = _arg2.n11;
            var _local5:Number = _arg1.n21;
            var _local6:Number = _arg2.n21;
            var _local7:Number = _arg1.n31;
            var _local8:Number = _arg2.n31;
            var _local9:Number = _arg1.n12;
            var _local10:Number = _arg2.n12;
            var _local11:Number = _arg1.n22;
            var _local12:Number = _arg2.n22;
            var _local13:Number = _arg1.n32;
            var _local14:Number = _arg2.n32;
            var _local15:Number = _arg1.n13;
            var _local16:Number = _arg2.n13;
            var _local17:Number = _arg1.n23;
            var _local18:Number = _arg2.n23;
            var _local19:Number = _arg1.n33;
            var _local20:Number = _arg2.n33;
            this.n11 = (((_local3 * _local4) + (_local9 * _local6)) + (_local15 * _local8));
            this.n12 = (((_local3 * _local10) + (_local9 * _local12)) + (_local15 * _local14));
            this.n13 = (((_local3 * _local16) + (_local9 * _local18)) + (_local15 * _local20));
            this.n21 = (((_local5 * _local4) + (_local11 * _local6)) + (_local17 * _local8));
            this.n22 = (((_local5 * _local10) + (_local11 * _local12)) + (_local17 * _local14));
            this.n23 = (((_local5 * _local16) + (_local11 * _local18)) + (_local17 * _local20));
            this.n31 = (((_local7 * _local4) + (_local13 * _local6)) + (_local19 * _local8));
            this.n32 = (((_local7 * _local10) + (_local13 * _local12)) + (_local19 * _local14));
            this.n33 = (((_local7 * _local16) + (_local13 * _local18)) + (_local19 * _local20));
        }
        public function calculateMultiply4x4(_arg1:Matrix3D, _arg2:Matrix3D):void{
            var _local3:Number = _arg1.n11;
            var _local4:Number = _arg2.n11;
            var _local5:Number = _arg1.n21;
            var _local6:Number = _arg2.n21;
            var _local7:Number = _arg1.n31;
            var _local8:Number = _arg2.n31;
            var _local9:Number = _arg1.n41;
            var _local10:Number = _arg2.n41;
            var _local11:Number = _arg1.n12;
            var _local12:Number = _arg2.n12;
            var _local13:Number = _arg1.n22;
            var _local14:Number = _arg2.n22;
            var _local15:Number = _arg1.n32;
            var _local16:Number = _arg2.n32;
            var _local17:Number = _arg1.n42;
            var _local18:Number = _arg2.n42;
            var _local19:Number = _arg1.n13;
            var _local20:Number = _arg2.n13;
            var _local21:Number = _arg1.n23;
            var _local22:Number = _arg2.n23;
            var _local23:Number = _arg1.n33;
            var _local24:Number = _arg2.n33;
            var _local25:Number = _arg1.n43;
            var _local26:Number = _arg2.n43;
            var _local27:Number = _arg1.n14;
            var _local28:Number = _arg2.n14;
            var _local29:Number = _arg1.n24;
            var _local30:Number = _arg2.n24;
            var _local31:Number = _arg1.n34;
            var _local32:Number = _arg2.n34;
            var _local33:Number = _arg1.n44;
            var _local34:Number = _arg2.n44;
            this.n11 = (((_local3 * _local4) + (_local11 * _local6)) + (_local19 * _local8));
            this.n12 = (((_local3 * _local12) + (_local11 * _local14)) + (_local19 * _local16));
            this.n13 = (((_local3 * _local20) + (_local11 * _local22)) + (_local19 * _local24));
            this.n14 = ((((_local3 * _local28) + (_local11 * _local30)) + (_local19 * _local32)) + _local27);
            this.n21 = (((_local5 * _local4) + (_local13 * _local6)) + (_local21 * _local8));
            this.n22 = (((_local5 * _local12) + (_local13 * _local14)) + (_local21 * _local16));
            this.n23 = (((_local5 * _local20) + (_local13 * _local22)) + (_local21 * _local24));
            this.n24 = ((((_local5 * _local28) + (_local13 * _local30)) + (_local21 * _local32)) + _local29);
            this.n31 = (((_local7 * _local4) + (_local15 * _local6)) + (_local23 * _local8));
            this.n32 = (((_local7 * _local12) + (_local15 * _local14)) + (_local23 * _local16));
            this.n33 = (((_local7 * _local20) + (_local15 * _local22)) + (_local23 * _local24));
            this.n34 = ((((_local7 * _local28) + (_local15 * _local30)) + (_local23 * _local32)) + _local31);
            this.n41 = (((_local9 * _local4) + (_local17 * _local6)) + (_local25 * _local8));
            this.n42 = (((_local9 * _local12) + (_local17 * _local14)) + (_local25 * _local16));
            this.n43 = (((_local9 * _local20) + (_local17 * _local22)) + (_local25 * _local24));
            this.n44 = ((((_local9 * _local28) + (_local17 * _local30)) + (_local25 * _local32)) + _local33);
        }
        public function get det():Number{
            return ((((((this.n11 * this.n22) - (this.n21 * this.n12)) * this.n33) - (((this.n11 * this.n32) - (this.n31 * this.n12)) * this.n23)) + (((this.n21 * this.n32) - (this.n31 * this.n22)) * this.n13)));
        }
        public function copy(_arg1:Matrix3D):Matrix3D{
            this.n11 = _arg1.n11;
            this.n12 = _arg1.n12;
            this.n13 = _arg1.n13;
            this.n14 = _arg1.n14;
            this.n21 = _arg1.n21;
            this.n22 = _arg1.n22;
            this.n23 = _arg1.n23;
            this.n24 = _arg1.n24;
            this.n31 = _arg1.n31;
            this.n32 = _arg1.n32;
            this.n33 = _arg1.n33;
            this.n34 = _arg1.n34;
            return (this);
        }
        public function copy3x3(_arg1:Matrix3D):Matrix3D{
            this.n11 = _arg1.n11;
            this.n12 = _arg1.n12;
            this.n13 = _arg1.n13;
            this.n21 = _arg1.n21;
            this.n22 = _arg1.n22;
            this.n23 = _arg1.n23;
            this.n31 = _arg1.n31;
            this.n32 = _arg1.n32;
            this.n33 = _arg1.n33;
            return (this);
        }
        public function calculateAdd(_arg1:Matrix3D, _arg2:Matrix3D):void{
            this.n11 = (_arg1.n11 + _arg2.n11);
            this.n12 = (_arg1.n12 + _arg2.n12);
            this.n13 = (_arg1.n13 + _arg2.n13);
            this.n14 = (_arg1.n14 + _arg2.n14);
            this.n21 = (_arg1.n21 + _arg2.n21);
            this.n22 = (_arg1.n22 + _arg2.n22);
            this.n23 = (_arg1.n23 + _arg2.n23);
            this.n24 = (_arg1.n24 + _arg2.n24);
            this.n31 = (_arg1.n31 + _arg2.n31);
            this.n32 = (_arg1.n32 + _arg2.n32);
            this.n33 = (_arg1.n33 + _arg2.n33);
            this.n34 = (_arg1.n34 + _arg2.n34);
        }
        public function calculateMultiply(_arg1:Matrix3D, _arg2:Matrix3D):void{
            var _local3:Number = _arg1.n11;
            var _local4:Number = _arg2.n11;
            var _local5:Number = _arg1.n21;
            var _local6:Number = _arg2.n21;
            var _local7:Number = _arg1.n31;
            var _local8:Number = _arg2.n31;
            var _local9:Number = _arg1.n12;
            var _local10:Number = _arg2.n12;
            var _local11:Number = _arg1.n22;
            var _local12:Number = _arg2.n22;
            var _local13:Number = _arg1.n32;
            var _local14:Number = _arg2.n32;
            var _local15:Number = _arg1.n13;
            var _local16:Number = _arg2.n13;
            var _local17:Number = _arg1.n23;
            var _local18:Number = _arg2.n23;
            var _local19:Number = _arg1.n33;
            var _local20:Number = _arg2.n33;
            var _local21:Number = _arg1.n14;
            var _local22:Number = _arg2.n14;
            var _local23:Number = _arg1.n24;
            var _local24:Number = _arg2.n24;
            var _local25:Number = _arg1.n34;
            var _local26:Number = _arg2.n34;
            this.n11 = (((_local3 * _local4) + (_local9 * _local6)) + (_local15 * _local8));
            this.n12 = (((_local3 * _local10) + (_local9 * _local12)) + (_local15 * _local14));
            this.n13 = (((_local3 * _local16) + (_local9 * _local18)) + (_local15 * _local20));
            this.n14 = ((((_local3 * _local22) + (_local9 * _local24)) + (_local15 * _local26)) + _local21);
            this.n21 = (((_local5 * _local4) + (_local11 * _local6)) + (_local17 * _local8));
            this.n22 = (((_local5 * _local10) + (_local11 * _local12)) + (_local17 * _local14));
            this.n23 = (((_local5 * _local16) + (_local11 * _local18)) + (_local17 * _local20));
            this.n24 = ((((_local5 * _local22) + (_local11 * _local24)) + (_local17 * _local26)) + _local23);
            this.n31 = (((_local7 * _local4) + (_local13 * _local6)) + (_local19 * _local8));
            this.n32 = (((_local7 * _local10) + (_local13 * _local12)) + (_local19 * _local14));
            this.n33 = (((_local7 * _local16) + (_local13 * _local18)) + (_local19 * _local20));
            this.n34 = ((((_local7 * _local22) + (_local13 * _local24)) + (_local19 * _local26)) + _local25);
        }
        public function reset(_arg1:Array=null):void{
            if (((!(_arg1)) || ((_arg1.length < 12)))){
                n11 = (n22 = (n33 = (n44 = 1)));
                n12 = (n13 = (n14 = (n21 = (n23 = (n24 = (n31 = (n32 = (n34 = (n41 = (n42 = (n43 = 0)))))))))));
            } else {
                n11 = _arg1[0];
                n12 = _arg1[1];
                n13 = _arg1[2];
                n14 = _arg1[3];
                n21 = _arg1[4];
                n22 = _arg1[5];
                n23 = _arg1[6];
                n24 = _arg1[7];
                n31 = _arg1[8];
                n32 = _arg1[9];
                n33 = _arg1[10];
                n34 = _arg1[11];
                if (_arg1.length == 16){
                    n41 = _arg1[12];
                    n42 = _arg1[13];
                    n43 = _arg1[14];
                    n44 = _arg1[15];
                } else {
                    n41 = (n42 = (n43 = 0));
                    n44 = 1;
                };
            };
        }
        public function invert():void{
            temp.copy(this);
            calculateInverse(temp);
        }
        public function calculateInverse(_arg1:Matrix3D):void{
            var _local3:Number;
            var _local4:Number;
            var _local5:Number;
            var _local6:Number;
            var _local7:Number;
            var _local8:Number;
            var _local9:Number;
            var _local10:Number;
            var _local11:Number;
            var _local12:Number;
            var _local13:Number;
            var _local14:Number;
            var _local2:Number = _arg1.det;
            if (Math.abs(_local2) > 0.001){
                _local2 = (1 / _local2);
                _local3 = _arg1.n11;
                _local4 = _arg1.n21;
                _local5 = _arg1.n31;
                _local6 = _arg1.n12;
                _local7 = _arg1.n22;
                _local8 = _arg1.n32;
                _local9 = _arg1.n13;
                _local10 = _arg1.n23;
                _local11 = _arg1.n33;
                _local12 = _arg1.n14;
                _local13 = _arg1.n24;
                _local14 = _arg1.n34;
                this.n11 = (_local2 * ((_local7 * _local11) - (_local8 * _local10)));
                this.n12 = (-(_local2) * ((_local6 * _local11) - (_local8 * _local9)));
                this.n13 = (_local2 * ((_local6 * _local10) - (_local7 * _local9)));
                this.n14 = (-(_local2) * (((_local6 * ((_local10 * _local14) - (_local11 * _local13))) - (_local7 * ((_local9 * _local14) - (_local11 * _local12)))) + (_local8 * ((_local9 * _local13) - (_local10 * _local12)))));
                this.n21 = (-(_local2) * ((_local4 * _local11) - (_local5 * _local10)));
                this.n22 = (_local2 * ((_local3 * _local11) - (_local5 * _local9)));
                this.n23 = (-(_local2) * ((_local3 * _local10) - (_local4 * _local9)));
                this.n24 = (_local2 * (((_local3 * ((_local10 * _local14) - (_local11 * _local13))) - (_local4 * ((_local9 * _local14) - (_local11 * _local12)))) + (_local5 * ((_local9 * _local13) - (_local10 * _local12)))));
                this.n31 = (_local2 * ((_local4 * _local8) - (_local5 * _local7)));
                this.n32 = (-(_local2) * ((_local3 * _local8) - (_local5 * _local6)));
                this.n33 = (_local2 * ((_local3 * _local7) - (_local4 * _local6)));
                this.n34 = (-(_local2) * (((_local3 * ((_local7 * _local14) - (_local8 * _local13))) - (_local4 * ((_local6 * _local14) - (_local8 * _local12)))) + (_local5 * ((_local6 * _local13) - (_local7 * _local12)))));
            };
        }
        public function calculateTranspose():void{
            var _local1:Number = this.n11;
            var _local2:Number = this.n21;
            var _local3:Number = this.n31;
            var _local4:Number = this.n41;
            var _local5:Number = this.n12;
            var _local6:Number = this.n22;
            var _local7:Number = this.n32;
            var _local8:Number = this.n42;
            var _local9:Number = this.n13;
            var _local10:Number = this.n23;
            var _local11:Number = this.n33;
            var _local12:Number = this.n43;
            var _local13:Number = this.n14;
            var _local14:Number = this.n24;
            var _local15:Number = this.n34;
            var _local16:Number = this.n44;
            this.n11 = _local1;
            this.n12 = _local2;
            this.n13 = _local3;
            this.n14 = _local4;
            this.n21 = _local5;
            this.n22 = _local6;
            this.n23 = _local7;
            this.n24 = _local8;
            this.n31 = _local9;
            this.n32 = _local10;
            this.n33 = _local11;
            this.n34 = _local12;
            this.n41 = _local13;
            this.n42 = _local14;
            this.n43 = _local15;
            this.n44 = _local16;
        }
        public function toString():String{
            var _local1 = "";
            _local1 = (_local1 + ((((((((int((n11 * 1000)) / 1000) + "\t\t") + (int((n12 * 1000)) / 1000)) + "\t\t") + (int((n13 * 1000)) / 1000)) + "\t\t") + (int((n14 * 1000)) / 1000)) + "\n"));
            _local1 = (_local1 + ((((((((int((n21 * 1000)) / 1000) + "\t\t") + (int((n22 * 1000)) / 1000)) + "\t\t") + (int((n23 * 1000)) / 1000)) + "\t\t") + (int((n24 * 1000)) / 1000)) + "\n"));
            _local1 = (_local1 + ((((((((int((n31 * 1000)) / 1000) + "\t\t") + (int((n32 * 1000)) / 1000)) + "\t\t") + (int((n33 * 1000)) / 1000)) + "\t\t") + (int((n34 * 1000)) / 1000)) + "\n"));
            _local1 = (_local1 + ((((((((int((n41 * 1000)) / 1000) + "\t\t") + (int((n42 * 1000)) / 1000)) + "\t\t") + (int((n43 * 1000)) / 1000)) + "\t\t") + (int((n44 * 1000)) / 1000)) + "\n"));
            return (_local1);
        }

        public static function rotationMatrixWithReference(_arg1:Number3D, _arg2:Number, _arg3:Number3D):Matrix3D{
            var _local4:Matrix3D = Matrix3D.translationMatrix(_arg3.x, -(_arg3.y), _arg3.z);
            Matrix3D.translationMatrix(_arg3.x, -(_arg3.y), _arg3.z).calculateMultiply(_local4, Matrix3D.rotationMatrix(_arg1.x, _arg1.y, _arg1.z, _arg2));
            _local4.calculateMultiply(_local4, Matrix3D.translationMatrix(-(_arg3.x), _arg3.y, -(_arg3.z)));
            return (_local4);
        }
        public static function multiplyVector(_arg1:Matrix3D, _arg2:Number3D):void{
            var _local4:Number;
            var _local5:Number;
            var _local3:Number = _arg2.x;
            _local4 = _arg2.y;
            _local5 = _arg2.z;
            _arg2.x = ((((_local3 * _arg1.n11) + (_local4 * _arg1.n12)) + (_local5 * _arg1.n13)) + _arg1.n14);
            _arg2.y = ((((_local3 * _arg1.n21) + (_local4 * _arg1.n22)) + (_local5 * _arg1.n23)) + _arg1.n24);
            _arg2.z = ((((_local3 * _arg1.n31) + (_local4 * _arg1.n32)) + (_local5 * _arg1.n33)) + _arg1.n34);
        }
        public static function multiplyVector4x4(_arg1:Matrix3D, _arg2:Number3D):void{
            var _local3:Number;
            var _local5:Number;
            var _local6:Number;
            _local3 = _arg2.x;
            var _local4:Number = _arg2.y;
            _local5 = _arg2.z;
            _local6 = (1 / ((((_local3 * _arg1.n41) + (_local4 * _arg1.n42)) + (_local5 * _arg1.n43)) + _arg1.n44));
            _arg2.x = ((((_local3 * _arg1.n11) + (_local4 * _arg1.n12)) + (_local5 * _arg1.n13)) + _arg1.n14);
            _arg2.y = ((((_local3 * _arg1.n21) + (_local4 * _arg1.n22)) + (_local5 * _arg1.n23)) + _arg1.n24);
            _arg2.z = ((((_local3 * _arg1.n31) + (_local4 * _arg1.n32)) + (_local5 * _arg1.n33)) + _arg1.n34);
            _arg2.x = (_arg2.x * _local6);
            _arg2.y = (_arg2.y * _local6);
            _arg2.z = (_arg2.z * _local6);
        }
        public static function multiply3x3(_arg1:Matrix3D, _arg2:Matrix3D):Matrix3D{
            var _local3:Matrix3D = new (Matrix3D);
            _local3.calculateMultiply3x3(_arg1, _arg2);
            return (_local3);
        }
        public static function normalizeQuaternion(_arg1:Object):Object{
            var _local2:Number = magnitudeQuaternion(_arg1);
            _arg1.x = (_arg1.x / _local2);
            _arg1.y = (_arg1.y / _local2);
            _arg1.z = (_arg1.z / _local2);
            _arg1.w = (_arg1.w / _local2);
            return (_arg1);
        }
        public static function multiplyVector3x3(_arg1:Matrix3D, _arg2:Number3D):void{
            var _local5:Number;
            var _local3:Number = _arg2.x;
            var _local4:Number = _arg2.y;
            _local5 = _arg2.z;
            _arg2.x = (((_local3 * _arg1.n11) + (_local4 * _arg1.n12)) + (_local5 * _arg1.n13));
            _arg2.y = (((_local3 * _arg1.n21) + (_local4 * _arg1.n22)) + (_local5 * _arg1.n23));
            _arg2.z = (((_local3 * _arg1.n31) + (_local4 * _arg1.n32)) + (_local5 * _arg1.n33));
        }
        public static function axis2quaternion(_arg1:Number, _arg2:Number, _arg3:Number, _arg4:Number):Object{
            var _local5:Number = Math.sin((_arg4 / 2));
            var _local6:Number = Math.cos((_arg4 / 2));
            var _local7:Object = new Object();
            _local7.x = (_arg1 * _local5);
            _local7.y = (_arg2 * _local5);
            _local7.z = (_arg3 * _local5);
            _local7.w = _local6;
            return (normalizeQuaternion(_local7));
        }
        public static function translationMatrix(_arg1:Number, _arg2:Number, _arg3:Number):Matrix3D{
            var _local4:Matrix3D = IDENTITY;
            _local4.n14 = _arg1;
            _local4.n24 = _arg2;
            _local4.n34 = _arg3;
            return (_local4);
        }
        public static function magnitudeQuaternion(_arg1:Object):Number{
            return (Math.sqrt(((((_arg1.w * _arg1.w) + (_arg1.x * _arg1.x)) + (_arg1.y * _arg1.y)) + (_arg1.z * _arg1.z))));
        }
        public static function rotationX(_arg1:Number):Matrix3D{
            var _local2:Matrix3D = IDENTITY;
            var _local3:Number = Math.cos(_arg1);
            var _local4:Number = Math.sin(_arg1);
            _local2.n22 = _local3;
            _local2.n23 = -(_local4);
            _local2.n32 = _local4;
            _local2.n33 = _local3;
            return (_local2);
        }
        public static function rotationY(_arg1:Number):Matrix3D{
            var _local2:Matrix3D = IDENTITY;
            var _local3:Number = Math.cos(_arg1);
            var _local4:Number = Math.sin(_arg1);
            _local2.n11 = _local3;
            _local2.n13 = -(_local4);
            _local2.n31 = _local4;
            _local2.n33 = _local3;
            return (_local2);
        }
        public static function rotationZ(_arg1:Number):Matrix3D{
            var _local2:Matrix3D = IDENTITY;
            var _local3:Number = Math.cos(_arg1);
            var _local4:Number = Math.sin(_arg1);
            _local2.n11 = _local3;
            _local2.n12 = -(_local4);
            _local2.n21 = _local4;
            _local2.n22 = _local3;
            return (_local2);
        }
        public static function clone(_arg1:Matrix3D):Matrix3D{
            return (new Matrix3D([_arg1.n11, _arg1.n12, _arg1.n13, _arg1.n14, _arg1.n21, _arg1.n22, _arg1.n23, _arg1.n24, _arg1.n31, _arg1.n32, _arg1.n33, _arg1.n34]));
        }
        public static function rotationMatrix(_arg1:Number, _arg2:Number, _arg3:Number, _arg4:Number, _arg5:Matrix3D=null):Matrix3D{
            var _local6:Matrix3D;
            if (!_arg5){
                _local6 = IDENTITY;
            } else {
                _local6 = _arg5;
            };
            var _local7:Number = Math.cos(_arg4);
            var _local8:Number = Math.sin(_arg4);
            var _local9:Number = (1 - _local7);
            var _local10:Number = ((_arg1 * _arg2) * _local9);
            var _local11:Number = ((_arg2 * _arg3) * _local9);
            var _local12:Number = ((_arg1 * _arg3) * _local9);
            var _local13:Number = (_local8 * _arg3);
            var _local14:Number = (_local8 * _arg2);
            var _local15:Number = (_local8 * _arg1);
            _local6.n11 = (_local7 + ((_arg1 * _arg1) * _local9));
            _local6.n12 = (-(_local13) + _local10);
            _local6.n13 = (_local14 + _local12);
            _local6.n14 = 0;
            _local6.n21 = (_local13 + _local10);
            _local6.n22 = (_local7 + ((_arg2 * _arg2) * _local9));
            _local6.n23 = (-(_local15) + _local11);
            _local6.n24 = 0;
            _local6.n31 = (-(_local14) + _local12);
            _local6.n32 = (_local15 + _local11);
            _local6.n33 = (_local7 + ((_arg3 * _arg3) * _local9));
            _local6.n34 = 0;
            return (_local6);
        }
        public static function add(_arg1:Matrix3D, _arg2:Matrix3D):Matrix3D{
            var _local3:Matrix3D = new (Matrix3D);
            _local3.calculateAdd(_arg1, _arg2);
            return (_local3);
        }
        public static function multiply(_arg1:Matrix3D, _arg2:Matrix3D):Matrix3D{
            var _local3:Matrix3D = new (Matrix3D);
            _local3.calculateMultiply(_arg1, _arg2);
            return (_local3);
        }
        public static function euler2quaternion(_arg1:Number, _arg2:Number, _arg3:Number, _arg4:Quaternion=null):Quaternion{
            var _local13:Quaternion;
            var _local5:Number = Math.sin((_arg1 * 0.5));
            var _local6:Number = Math.cos((_arg1 * 0.5));
            var _local7:Number = Math.sin((_arg2 * 0.5));
            var _local8:Number = Math.cos((_arg2 * 0.5));
            var _local9:Number = Math.sin((_arg3 * 0.5));
            var _local10:Number = Math.cos((_arg3 * 0.5));
            var _local11:Number = (_local6 * _local8);
            var _local12:Number = (_local5 * _local7);
            if (!_arg4){
                _local13 = new Quaternion();
            } else {
                _local13 = _arg4;
            };
            _local13.x = ((_local9 * _local11) - (_local10 * _local12));
            _local13.y = (((_local10 * _local5) * _local8) + ((_local9 * _local6) * _local7));
            _local13.z = (((_local10 * _local6) * _local7) - ((_local9 * _local5) * _local8));
            _local13.w = ((_local10 * _local11) + (_local9 * _local12));
            return (_local13);
        }
        public static function quaternion2matrix(_arg1:Number, _arg2:Number, _arg3:Number, _arg4:Number, _arg5:Matrix3D=null):Matrix3D{
            var _local15:Matrix3D;
            var _local6:Number = (_arg1 * _arg1);
            var _local7:Number = (_arg1 * _arg2);
            var _local8:Number = (_arg1 * _arg3);
            var _local9:Number = (_arg1 * _arg4);
            var _local10:Number = (_arg2 * _arg2);
            var _local11:Number = (_arg2 * _arg3);
            var _local12:Number = (_arg2 * _arg4);
            var _local13:Number = (_arg3 * _arg3);
            var _local14:Number = (_arg3 * _arg4);
            if (!_arg5){
                _local15 = IDENTITY;
            } else {
                _local15 = _arg5;
            };
            _local15.n11 = (1 - (2 * (_local10 + _local13)));
            _local15.n12 = (2 * (_local7 - _local14));
            _local15.n13 = (2 * (_local8 + _local12));
            _local15.n21 = (2 * (_local7 + _local14));
            _local15.n22 = (1 - (2 * (_local6 + _local13)));
            _local15.n23 = (2 * (_local11 - _local9));
            _local15.n31 = (2 * (_local8 - _local12));
            _local15.n32 = (2 * (_local11 + _local9));
            _local15.n33 = (1 - (2 * (_local6 + _local10)));
            return (_local15);
        }
        public static function inverse(_arg1:Matrix3D):Matrix3D{
            var _local2:Matrix3D = new (Matrix3D);
            _local2.calculateInverse(_arg1);
            return (_local2);
        }
        public static function euler2matrix(_arg1:Number3D):Matrix3D{
            temp.reset();
            var _local2:Matrix3D = temp;
            _local2 = temp;
            var _local3:Number = (_arg1.x * toRADIANS);
            var _local4:Number = (_arg1.y * toRADIANS);
            var _local5:Number = (_arg1.z * toRADIANS);
            var _local6:Number = Math.cos(_local3);
            var _local7:Number = Math.sin(_local3);
            var _local8:Number = Math.cos(_local4);
            var _local9:Number = Math.sin(_local4);
            var _local10:Number = Math.cos(_local5);
            var _local11:Number = Math.sin(_local5);
            var _local12:Number = (_local6 * _local9);
            var _local13:Number = (_local7 * _local9);
            _local2.n11 = (_local8 * _local10);
            _local2.n12 = (-(_local8) * _local11);
            _local2.n13 = _local9;
            _local2.n21 = ((_local13 * _local10) + (_local6 * _local11));
            _local2.n22 = ((-(_local13) * _local11) + (_local6 * _local10));
            _local2.n23 = (-(_local7) * _local8);
            _local2.n31 = ((-(_local12) * _local10) + (_local7 * _local11));
            _local2.n32 = ((_local12 * _local11) + (_local7 * _local10));
            _local2.n33 = (_local6 * _local8);
            return (_local2);
        }
        public static function scaleMatrix(_arg1:Number, _arg2:Number, _arg3:Number):Matrix3D{
            var _local4:Matrix3D = IDENTITY;
            _local4.n11 = _arg1;
            _local4.n22 = _arg2;
            _local4.n33 = _arg3;
            return (_local4);
        }
        public static function rotateAxis(_arg1:Matrix3D, _arg2:Number3D):void{
            var _local3:Number = _arg2.x;
            var _local4:Number = _arg2.y;
            var _local5:Number = _arg2.z;
            _arg2.x = (((_local3 * _arg1.n11) + (_local4 * _arg1.n12)) + (_local5 * _arg1.n13));
            _arg2.y = (((_local3 * _arg1.n21) + (_local4 * _arg1.n22)) + (_local5 * _arg1.n23));
            _arg2.z = (((_local3 * _arg1.n31) + (_local4 * _arg1.n32)) + (_local5 * _arg1.n33));
            _arg2.normalize();
        }
        public static function matrix2euler(_arg1:Matrix3D, _arg2:Number3D=null, _arg3:Number3D=null):Number3D{
            _arg2 = ((_arg2) || (new Number3D()));
            var _local4:Number = (((_arg3) && ((_arg3.x == 1)))) ? 1 : Math.sqrt((((_arg1.n11 * _arg1.n11) + (_arg1.n21 * _arg1.n21)) + (_arg1.n31 * _arg1.n31)));
            var _local5:Number = (((_arg3) && ((_arg3.y == 1)))) ? 1 : Math.sqrt((((_arg1.n12 * _arg1.n12) + (_arg1.n22 * _arg1.n22)) + (_arg1.n32 * _arg1.n32)));
            var _local6:Number = (((_arg3) && ((_arg3.z == 1)))) ? 1 : Math.sqrt((((_arg1.n13 * _arg1.n13) + (_arg1.n23 * _arg1.n23)) + (_arg1.n33 * _arg1.n33)));
            var _local7:Number = (_arg1.n11 / _local4);
            var _local8:Number = (_arg1.n21 / _local5);
            var _local9:Number = (_arg1.n31 / _local6);
            var _local10:Number = (_arg1.n32 / _local6);
            var _local11:Number = (_arg1.n33 / _local6);
            _local9 = ((_local9 > 1)) ? 1 : _local9;
            _local9 = ((_local9 < -1)) ? -1 : _local9;
            _arg2.y = Math.asin(-(_local9));
            _arg2.z = Math.atan2(_local8, _local7);
            _arg2.x = Math.atan2(_local10, _local11);
            if (Papervision3D.useDEGREES){
                _arg2.x = (_arg2.x * toDEGREES);
                _arg2.y = (_arg2.y * toDEGREES);
                _arg2.z = (_arg2.z * toDEGREES);
            };
            return (_arg2);
        }
        public static function multiplyQuaternion(_arg1:Object, _arg2:Object):Object{
            var _local3:Number = _arg1.x;
            var _local4:Number = _arg1.y;
            var _local5:Number = _arg1.z;
            var _local6:Number = _arg1.w;
            var _local7:Number = _arg2.x;
            var _local8:Number = _arg2.y;
            var _local9:Number = _arg2.z;
            var _local10:Number = _arg2.w;
            var _local11:Object = new Object();
            _local11.x = ((((_local6 * _local7) + (_local3 * _local10)) + (_local4 * _local9)) - (_local5 * _local8));
            _local11.y = ((((_local6 * _local8) + (_local4 * _local10)) + (_local5 * _local7)) - (_local3 * _local9));
            _local11.z = ((((_local6 * _local9) + (_local5 * _local10)) + (_local3 * _local8)) - (_local4 * _local7));
            _local11.w = ((((_local6 * _local10) - (_local3 * _local7)) - (_local4 * _local8)) - (_local5 * _local9));
            return (_local11);
        }
        public static function get IDENTITY():Matrix3D{
            return (new Matrix3D([1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1]));
        }

    }
}//package org.papervision3d.core.math 
