//Created by Action Script Viewer - http://www.buraks.com/asv
package org.papervision3d.objects.primitives {
    import org.papervision3d.core.proto.*;
    import org.papervision3d.core.math.*;
    import org.papervision3d.core.geom.renderables.*;
    import org.papervision3d.core.geom.*;
    import org.papervision3d.*;

    public class Plane extends TriangleMesh3D {

        public var segmentsH:Number
        public var segmentsW:Number

        public static var DEFAULT_SCALE:Number = 1;
        public static var DEFAULT_SEGMENTS:Number = 1;
        public static var DEFAULT_SIZE:Number = 500;

        public function Plane(_arg1:MaterialObject3D=null, _arg2:Number=0, _arg3:Number=0, _arg4:Number=0, _arg5:Number=0){
            super(_arg1, new Array(), new Array(), null);
            this.segmentsW = ((_arg4) || (DEFAULT_SEGMENTS));
            this.segmentsH = ((_arg5) || (this.segmentsW));
            var _local6:Number = DEFAULT_SCALE;
            if (!_arg3){
                if (_arg2){
                    _local6 = _arg2;
                };
                if (((_arg1) && (_arg1.bitmap))){
                    _arg2 = (_arg1.bitmap.width * _local6);
                    _arg3 = (_arg1.bitmap.height * _local6);
                } else {
                    _arg2 = (DEFAULT_SIZE * _local6);
                    _arg3 = (DEFAULT_SIZE * _local6);
                };
            };
            buildPlane(_arg2, _arg3);
        }
        private function buildPlane(_arg1:Number, _arg2:Number):void{
            var _local14:NumberUV;
            var _local15:NumberUV;
            var _local16:NumberUV;
            var _local17:int;
            var _local18:Number;
            var _local19:Number;
            var _local20:Vertex3D;
            var _local21:Vertex3D;
            var _local22:Vertex3D;
            var _local3:Number = this.segmentsW;
            var _local4:Number = this.segmentsH;
            var _local5:Number = (_local3 + 1);
            var _local6:Number = (_local4 + 1);
            var _local7:Array = this.geometry.vertices;
            var _local8:Array = this.geometry.faces;
            var _local9:Number = (_arg1 / 2);
            var _local10:Number = (_arg2 / 2);
            var _local11:Number = (_arg1 / _local3);
            var _local12:Number = (_arg2 / _local4);
            var _local13:int;
            while (_local13 < (_local3 + 1)) {
                _local17 = 0;
                while (_local17 < _local6) {
                    _local18 = ((_local13 * _local11) - _local9);
                    _local19 = ((_local17 * _local12) - _local10);
                    _local7.push(new Vertex3D(_local18, _local19, 0));
                    _local17++;
                };
                _local13++;
            };
            _local13 = 0;
            while (_local13 < _local3) {
                _local17 = 0;
                while (_local17 < _local4) {
                    _local20 = _local7[((_local13 * _local6) + _local17)];
                    _local21 = _local7[((_local13 * _local6) + (_local17 + 1))];
                    _local22 = _local7[(((_local13 + 1) * _local6) + _local17)];
                    _local14 = new NumberUV((_local13 / _local3), (_local17 / _local4));
                    _local15 = new NumberUV((_local13 / _local3), ((_local17 + 1) / _local4));
                    _local16 = new NumberUV(((_local13 + 1) / _local3), (_local17 / _local4));
                    _local8.push(new Triangle3D(this, [_local20, _local22, _local21], material, [_local14, _local16, _local15]));
                    _local20 = _local7[(((_local13 + 1) * _local6) + (_local17 + 1))];
                    _local21 = _local7[(((_local13 + 1) * _local6) + _local17)];
                    _local22 = _local7[((_local13 * _local6) + (_local17 + 1))];
                    _local14 = new NumberUV(((_local13 + 1) / _local3), ((_local17 + 1) / _local4));
                    _local15 = new NumberUV(((_local13 + 1) / _local3), (_local17 / _local4));
                    _local16 = new NumberUV((_local13 / _local3), ((_local17 + 1) / _local4));
                    _local8.push(new Triangle3D(this, [_local20, _local22, _local21], material, [_local14, _local16, _local15]));
                    _local17++;
                };
                _local13++;
            };
            this.geometry.ready = true;
            if (Papervision3D.useRIGHTHANDED){
                this.geometry.flipFaces();
            };
        }

    }
}//package org.papervision3d.objects.primitives 
