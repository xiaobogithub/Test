//Created by Action Script Viewer - http://www.buraks.com/asv
package com.degrafa.geometry.utilities {
    import flash.geom.*;
    import com.degrafa.geometry.command.*;

    public class GeometryUtils {

        private static var returnResult:Array;
        private static var m3:Number;
        private static var dx:Number;
        private static var dy:Number;
        private static var dx2:Number;
        private static var m1:Number;
        private static var sx:Number;
        private static var bezBoundsRect:Rectangle = new Rectangle();
        private static var half:Number = 0.5;
        private static var m2:Number;
        private static var sy:Number;
        private static var dx1:Number;

        public static function cubicToQuadratic(_arg1:Number, _arg2:Number, _arg3:Number, _arg4:Number, _arg5:Number, _arg6:Number, _arg7:Number, _arg8:Number, _arg9:Number, _arg10:CommandStack):Array{
            var _local11:Number;
            var _local12:Number;
            var _local13:Number;
            var _local14:Number;
            var _local15:Number;
            var _local16:Number;
            var _local17:Number;
            var _local18:Number;
            var _local19:Number;
            var _local20:Number;
            var _local21:Number;
            var _local22:Number;
            if (!returnResult){
                returnResult = [];
            };
            dx1 = (_arg3 - _arg1);
            dx2 = (_arg5 - _arg7);
            if ((((((_arg2 == _arg8)) && ((_arg4 == _arg2)))) && ((_arg6 == _arg8)))){
                returnResult.push(_arg10.addLineTo(_arg7, _arg8));
                return (returnResult);
            };
            if (((((!(dx1)) && (!(dx2)))) && ((_arg1 == _arg7)))){
                returnResult.push(_arg10.addLineTo(_arg7, _arg8));
                return (returnResult);
            };
            if (!dx1){
                sx = _arg1;
                sy = ((((_arg6 - _arg8) / dx2) * (_arg1 - _arg7)) + _arg8);
                dx = ((((_arg1 + _arg7) + (sx * 4)) - ((_arg3 + _arg5) * 3)) * 0.125);
                dy = ((((_arg2 + _arg8) + (sy * 4)) - ((_arg4 + _arg6) * 3)) * 0.125);
            } else {
                if (!dx2){
                    sx = _arg7;
                    sy = ((((_arg4 - _arg2) / dx1) * (_arg7 - _arg1)) + _arg2);
                    dx = ((((_arg1 + _arg7) + (sx * 4)) - ((_arg3 + _arg5) * 3)) * 0.125);
                    dy = ((((_arg2 + _arg8) + (sy * 4)) - ((_arg4 + _arg6) * 3)) * 0.125);
                } else {
                    m1 = ((_arg4 - _arg2) / dx1);
                    m2 = ((_arg6 - _arg8) / dx2);
                    if (Math.abs(m1) == Math.abs(m2)){
                        m3 = ((_arg8 - _arg2) / (_arg7 - _arg1));
                        if ((((m1 == m2)) && ((m3 == m1)))){
                            returnResult.push(_arg10.addLineTo(_arg7, _arg8));
                            return (returnResult);
                        };
                        if ((((((m1 > m3)) && ((m2 < m3)))) || ((((m1 < m3)) && ((m2 > m3)))))){
                            sx = ((_arg3 + _arg5) / 2);
                            sy = ((_arg4 + _arg6) / 2);
                            dx = ((((_arg1 + _arg7) + (sx * 4)) - ((_arg3 + _arg5) * 3)) * 0.125);
                            dy = ((((_arg2 + _arg8) + (sy * 4)) - ((_arg4 + _arg6) * 3)) * 0.125);
                        } else {
                            dx = _arg9;
                            dy = _arg9;
                        };
                    } else {
                        sx = (((((-(m2) * _arg7) + _arg8) + (m1 * _arg1)) - _arg2) / (m1 - m2));
                        sy = ((m1 * (sx - _arg1)) + _arg2);
                        dx = ((((_arg1 + _arg7) + (sx * 4)) - ((_arg3 + _arg5) * 3)) * 0.125);
                        dy = ((((_arg2 + _arg8) + (sy * 4)) - ((_arg4 + _arg6) * 3)) * 0.125);
                    };
                };
            };
            if (((dx * dx) + (dy * dy)) > _arg9){
                _local11 = ((_arg1 + _arg3) * half);
                _local12 = ((_arg2 + _arg4) * half);
                _local13 = ((_arg3 + _arg5) * half);
                _local14 = ((_arg4 + _arg6) * half);
                _local15 = ((_arg5 + _arg7) * half);
                _local16 = ((_arg6 + _arg8) * half);
                _local17 = ((_local11 + _local13) * half);
                _local18 = ((_local12 + _local14) * half);
                _local19 = ((_local13 + _local15) * half);
                _local20 = ((_local14 + _local16) * half);
                _local21 = ((_local17 + _local19) * half);
                _local22 = ((_local18 + _local20) * half);
                cubicToQuadratic(_arg1, _arg2, _local11, _local12, _local17, _local18, _local21, _local22, _arg9, _arg10);
                cubicToQuadratic(_local21, _local22, _local19, _local20, _local15, _local16, _arg7, _arg8, _arg9, _arg10);
            } else {
                returnResult.push(_arg10.addCurveTo(sx, sy, _arg7, _arg8));
            };
            return (returnResult);
        }
        public static function lineIntersects(_arg1:Point, _arg2:Point, _arg3:Point, _arg4:Point):Point{
            var _local5:Number = _arg1.x;
            var _local6:Number = _arg1.y;
            var _local7:Number = _arg4.x;
            var _local8:Number = _arg4.y;
            var _local9:Number = (_arg2.x - _local5);
            var _local10:Number = (_arg3.x - _local7);
            var _local11:Point = new Point();
            if (!((_local9) || (_local10))){
                _local11.x = 0;
                _local11.y = 0;
            };
            var _local12:Number = ((_arg2.y - _local6) / _local9);
            var _local13:Number = ((_arg3.y - _local8) / _local10);
            if (!_local9){
                _local11.x = _local5;
                _local11.y = ((_local13 * (_local5 - _local7)) + _local8);
                return (_local11);
            };
            if (!_local10){
                _local11.x = _local7;
                _local11.y = ((_local12 * (_local7 - _local5)) + _local6);
                return (_local11);
            };
            var _local14:Number = (((((-(_local13) * _local7) + _local8) + (_local12 * _local5)) - _local6) / (_local12 - _local13));
            var _local15:Number = ((_local12 * (_local14 - _local5)) + _local6);
            _local11.x = _local14;
            _local11.y = _local15;
            return (_local11);
        }
        public static function splitBezier(_arg1:Point, _arg2:Point, _arg3:Point, _arg4:Point):Object{
            var _local5:Point = midPoint(_arg1, _arg2);
            var _local6:Point = midPoint(_arg2, _arg3);
            var _local7:Point = midPoint(_arg3, _arg4);
            var _local8:Point = midPoint(_local5, _local6);
            var _local9:Point = midPoint(_local6, _local7);
            var _local10:Point = midPoint(_local8, _local9);
            return ({b0:{p1:_arg1, c1:_local5, c2:_local8, p2:_local10}, b1:{p1:_local10, c1:_local9, c2:_local7, p2:_arg4}});
        }
        public static function barycenter(_arg1:Number, _arg2:Number, _arg3:Number, _arg4:Number):Number{
            return ((((((1 - _arg4) * (1 - _arg4)) * _arg1) + (((2 * (1 - _arg4)) * _arg4) * _arg2)) + ((_arg4 * _arg4) * _arg3)));
        }
        public static function perimeter(_arg1:Number, _arg2:Number, _arg3:Number, _arg4:Number, _arg5:Number, _arg6:Number):Number{
            var _local10:Number;
            var _local11:Number;
            var _local12:Number;
            var _local13:Number;
            var _local14:Number;
            var _local7:Number = _arg1;
            var _local8:Number = _arg2;
            var _local9:Number = 0;
            var _local15:Number = 0;
            while (_local15 <= 1) {
                _local10 = barycenter(_arg1, _arg3, _arg5, _local15);
                _local11 = barycenter(_arg2, _arg4, _arg6, _local15);
                _local12 = Math.abs((_local10 - _local7));
                _local13 = Math.abs((_local11 - _local8));
                _local14 = Math.sqrt(((_local12 * _local12) + (_local13 * _local13)));
                _local9 = (_local9 + _local14);
                _local7 = _local10;
                _local8 = _local11;
                _local15 = (_local15 + 0.001);
            };
            return (_local9);
        }
        public static function rotatePointOnCenterPoint(_arg1:Point, _arg2:Point, _arg3:Number):Point{
            var _local4:Point = new Point();
            var _local5:Number = ((_arg3 / 180) * Math.PI);
            _local4.x = (_arg2.x + ((Math.cos(_local5) * (_arg1.x - _arg2.x)) - (Math.sin(_local5) * (_arg1.y - _arg2.y))));
            _local4.y = (_arg2.y + ((Math.sin(_local5) * (_arg1.x - _arg2.x)) + (Math.cos(_local5) * (_arg1.y - _arg2.y))));
            return (_local4);
        }
        public static function midPoint(_arg1:Point, _arg2:Point):Point{
            return (new Point(((_arg1.x + _arg2.x) * half), ((_arg1.y + _arg2.y) * half)));
        }
        public static function roundTo(_arg1:Number, _arg2:Number):Number{
            return ((Math.round((_arg1 * Math.pow(10, _arg2))) / Math.pow(10, _arg2)));
        }
        public static function radiusToDegress(_arg1:Number):Number{
            return ((_arg1 * (180 / Math.PI)));
        }
        public static function pointOnQuadraticCurve(_arg1:Number, _arg2:Number, _arg3:Number, _arg4:Number, _arg5:Number, _arg6:Number, _arg7:Number):Object{
            var _local8:Number = (_arg1 / 100);
            return ({x:(_arg2 + (_local8 * (((2 * (1 - _local8)) * (_arg4 - _arg2)) + (_local8 * (_arg6 - _arg2))))), y:(_arg3 + (_local8 * (((2 * (1 - _local8)) * (_arg5 - _arg3)) + (_local8 * (_arg7 - _arg3)))))});
        }
        public static function degressToRadius(_arg1:Number):Number{
            return ((_arg1 * (Math.PI / 180)));
        }
        public static function bezierBounds(_arg1:Number, _arg2:Number, _arg3:Number, _arg4:Number, _arg5:Number, _arg6:Number):Rectangle{
            var _local7:Number;
            if ((((_arg1 == _arg3)) && ((_arg3 == _arg5)))){
                bezBoundsRect.x = _arg1;
                bezBoundsRect.y = Math.min(_arg2, _arg6);
                bezBoundsRect.width = 0.0001;
                bezBoundsRect.height = Math.abs((_arg6 - _arg2));
                return (bezBoundsRect);
            };
            if ((((_arg2 == _arg4)) && ((_arg4 == _arg6)))){
                bezBoundsRect.x = Math.min(_arg1, _arg5);
                bezBoundsRect.y = _arg2;
                bezBoundsRect.width = Math.abs((_arg5 - _arg1));
                bezBoundsRect.height = 0.0001;
                return (bezBoundsRect);
            };
            if (_arg2 > _arg6){
                if (_arg4 > _arg6){
                    bezBoundsRect.y = _arg6;
                } else {
                    _local7 = (-((_arg4 - _arg2)) / ((_arg6 - (2 * _arg4)) + _arg2));
                    bezBoundsRect.y = (((((1 - _local7) * (1 - _local7)) * _arg2) + (((2 * _local7) * (1 - _local7)) * _arg4)) + ((_local7 * _local7) * _arg6));
                };
            } else {
                if (_arg4 > _arg2){
                    bezBoundsRect.y = _arg2;
                } else {
                    _local7 = (-((_arg4 - _arg2)) / ((_arg6 - (2 * _arg4)) + _arg2));
                    bezBoundsRect.y = (((((1 - _local7) * (1 - _local7)) * _arg2) + (((2 * _local7) * (1 - _local7)) * _arg4)) + ((_local7 * _local7) * _arg6));
                };
            };
            if (_arg2 > _arg6){
                if (_arg4 < _arg2){
                    bezBoundsRect.bottom = _arg2;
                } else {
                    _local7 = (-((_arg4 - _arg2)) / ((_arg6 - (2 * _arg4)) + _arg2));
                    bezBoundsRect.bottom = (((((1 - _local7) * (1 - _local7)) * _arg2) + (((2 * _local7) * (1 - _local7)) * _arg4)) + ((_local7 * _local7) * _arg6));
                };
            } else {
                if (_arg6 > _arg4){
                    bezBoundsRect.bottom = _arg6;
                } else {
                    _local7 = (-((_arg4 - _arg2)) / ((_arg6 - (2 * _arg4)) + _arg2));
                    bezBoundsRect.bottom = (((((1 - _local7) * (1 - _local7)) * _arg2) + (((2 * _local7) * (1 - _local7)) * _arg4)) + ((_local7 * _local7) * _arg6));
                };
            };
            if (_arg1 > _arg5){
                if (_arg3 > _arg5){
                    bezBoundsRect.x = _arg5;
                } else {
                    _local7 = (-((_arg3 - _arg1)) / ((_arg5 - (2 * _arg3)) + _arg1));
                    bezBoundsRect.x = (((((1 - _local7) * (1 - _local7)) * _arg1) + (((2 * _local7) * (1 - _local7)) * _arg3)) + ((_local7 * _local7) * _arg5));
                };
            } else {
                if (_arg3 > _arg1){
                    bezBoundsRect.x = _arg1;
                } else {
                    _local7 = (-((_arg3 - _arg1)) / ((_arg5 - (2 * _arg3)) + _arg1));
                    bezBoundsRect.x = (((((1 - _local7) * (1 - _local7)) * _arg1) + (((2 * _local7) * (1 - _local7)) * _arg3)) + ((_local7 * _local7) * _arg5));
                };
            };
            if (_arg1 > _arg5){
                if (_arg3 < _arg1){
                    bezBoundsRect.right = _arg1;
                } else {
                    _local7 = (-((_arg3 - _arg1)) / ((_arg5 - (2 * _arg3)) + _arg1));
                    bezBoundsRect.right = (((((1 - _local7) * (1 - _local7)) * _arg1) + (((2 * _local7) * (1 - _local7)) * _arg3)) + ((_local7 * _local7) * _arg5));
                };
            } else {
                if (_arg3 < _arg5){
                    bezBoundsRect.right = _arg5;
                } else {
                    _local7 = (-((_arg3 - _arg1)) / ((_arg5 - (2 * _arg3)) + _arg1));
                    bezBoundsRect.right = (((((1 - _local7) * (1 - _local7)) * _arg1) + (((2 * _local7) * (1 - _local7)) * _arg3)) + ((_local7 * _local7) * _arg5));
                };
            };
            return (bezBoundsRect);
        }
        public static function rotatePoint(_arg1:Point, _arg2:Number):Point{
            var _local3:Number = Math.sqrt((Math.pow(_arg1.x, 2) + Math.pow(_arg1.y, 2)));
            _arg2 = (Math.atan2(_arg1.y, _arg1.x) + degressToRadius(_arg2));
            return (new Point(roundTo((_local3 * Math.cos(_arg2)), 3), roundTo((_local3 * Math.sin(_arg2)), 3)));
        }

    }
}//package com.degrafa.geometry.utilities 
