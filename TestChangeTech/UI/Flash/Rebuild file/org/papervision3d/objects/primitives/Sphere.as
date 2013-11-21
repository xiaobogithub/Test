//Created by Action Script Viewer - http://www.buraks.com/asv
package org.papervision3d.objects.primitives {
    import org.papervision3d.core.proto.*;
    import org.papervision3d.core.math.*;
    import org.papervision3d.core.geom.renderables.*;
    import org.papervision3d.core.geom.*;
    import org.papervision3d.*;

    public class Sphere extends TriangleMesh3D {

        private var segmentsH:Number
        private var segmentsW:Number

        public static var MIN_SEGMENTSW:Number = 3;
        public static var DEFAULT_SCALE:Number = 1;
        public static var DEFAULT_RADIUS:Number = 100;
        public static var DEFAULT_SEGMENTSH:Number = 6;
        public static var MIN_SEGMENTSH:Number = 2;
        public static var DEFAULT_SEGMENTSW:Number = 8;

        public function Sphere(_arg1:MaterialObject3D=null, _arg2:Number=100, _arg3:int=8, _arg4:int=6){
            super(_arg1, new Array(), new Array(), null);
            this.segmentsW = Math.max(MIN_SEGMENTSW, ((_arg3) || (DEFAULT_SEGMENTSW)));
            this.segmentsH = Math.max(MIN_SEGMENTSH, ((_arg4) || (DEFAULT_SEGMENTSH)));
            if (_arg2 == 0){
                _arg2 = DEFAULT_RADIUS;
            };
            var _local5:Number = DEFAULT_SCALE;
            buildSphere(_arg2);
        }
        private function buildSphere(_arg1:Number):void{
            var _local2:Number;
            var _local3:Number;
            var _local4:Number;
            var _local11:Number;
            var _local12:Number;
            var _local13:Number;
            var _local14:Array;
            var _local15:Vertex3D;
            var _local16:Number;
            var _local17:Number;
            var _local18:Number;
            var _local19:int;
            var _local20:Boolean;
            var _local21:Vertex3D;
            var _local22:Vertex3D;
            var _local23:Vertex3D;
            var _local24:Vertex3D;
            var _local25:Number;
            var _local26:Number;
            var _local27:Number;
            var _local28:Number;
            var _local29:NumberUV;
            var _local30:NumberUV;
            var _local31:NumberUV;
            var _local32:NumberUV;
            var _local5:Number = Math.max(3, this.segmentsW);
            var _local6:Number = Math.max(2, this.segmentsH);
            var _local7:Array = this.geometry.vertices;
            var _local8:Array = this.geometry.faces;
            var _local9:Array = new Array();
            _local3 = 0;
            while (_local3 < (_local6 + 1)) {
                _local11 = Number((_local3 / _local6));
                _local12 = (-(_arg1) * Math.cos((_local11 * Math.PI)));
                _local13 = (_arg1 * Math.sin((_local11 * Math.PI)));
                _local14 = new Array();
                _local2 = 0;
                while (_local2 < _local5) {
                    _local16 = Number(((2 * _local2) / _local5));
                    _local17 = (_local13 * Math.sin((_local16 * Math.PI)));
                    _local18 = (_local13 * Math.cos((_local16 * Math.PI)));
                    if (!(((((_local3 == 0)) || ((_local3 == _local6)))) && ((_local2 > 0)))){
                        _local15 = new Vertex3D(_local18, _local12, _local17);
                        _local7.push(_local15);
                    };
                    _local14.push(_local15);
                    _local2++;
                };
                _local9.push(_local14);
                _local3++;
            };
            var _local10:int = _local9.length;
            _local3 = 0;
            while (_local3 < _local10) {
                _local19 = _local9[_local3].length;
                if (_local3 > 0){
                    _local2 = 0;
                    while (_local2 < _local19) {
                        _local20 = (_local2 == (_local19 - 0));
                        _local21 = _local9[_local3][(_local20) ? 0 : _local2];
                        _local22 = _local9[_local3][(((_local2 == 0)) ? _local19 : _local2 - 1)];
                        _local23 = _local9[(_local3 - 1)][(((_local2 == 0)) ? _local19 : _local2 - 1)];
                        _local24 = _local9[(_local3 - 1)][(_local20) ? 0 : _local2];
                        _local25 = (_local3 / (_local10 - 1));
                        _local26 = ((_local3 - 1) / (_local10 - 1));
                        _local27 = ((_local2 + 1) / _local19);
                        _local28 = (_local2 / _local19);
                        _local29 = new NumberUV(_local27, _local26);
                        _local30 = new NumberUV(_local27, _local25);
                        _local31 = new NumberUV(_local28, _local25);
                        _local32 = new NumberUV(_local28, _local26);
                        if (_local3 < (_local9.length - 1)){
                            _local8.push(new Triangle3D(this, new Array(_local21, _local22, _local23), material, new Array(_local30, _local31, _local32)));
                        };
                        if (_local3 > 1){
                            _local8.push(new Triangle3D(this, new Array(_local21, _local23, _local24), material, new Array(_local30, _local32, _local29)));
                        };
                        _local2++;
                    };
                };
                _local3++;
            };
            this.geometry.ready = true;
            if (Papervision3D.useRIGHTHANDED){
                this.geometry.flipFaces();
            };
        }

    }
}//package org.papervision3d.objects.primitives 
