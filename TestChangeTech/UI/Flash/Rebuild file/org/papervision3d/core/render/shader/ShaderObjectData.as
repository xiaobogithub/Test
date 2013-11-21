//Created by Action Script Viewer - http://www.buraks.com/asv
package org.papervision3d.core.render.shader {
    import flash.display.*;
    import flash.geom.*;
    import flash.utils.*;
    import org.papervision3d.materials.*;
    import org.papervision3d.objects.*;
    import org.papervision3d.core.geom.renderables.*;
    import org.papervision3d.materials.shaders.*;

    public class ShaderObjectData {

        public var shaderRenderer:ShaderRenderer
        public var triangleUVS:Dictionary
        public var renderTriangleUVS:Dictionary
        public var lightMatrices:Dictionary
        public var shadedMaterial:ShadedMaterial
        public var uvMatrices:Dictionary
        private var origin:Point
        public var material:BitmapMaterial
        public var triangleRects:Dictionary
        protected var triangleBitmaps:Dictionary
        public var object:DisplayObject3D

        public function ShaderObjectData(_arg1:DisplayObject3D, _arg2:BitmapMaterial, _arg3:ShadedMaterial):void{
            origin = new Point(0, 0);
            super();
            shaderRenderer = new ShaderRenderer();
            lightMatrices = new Dictionary();
            uvMatrices = new Dictionary();
            this.object = _arg1;
            this.material = _arg2;
            this.shadedMaterial = _arg3;
            triangleUVS = new Dictionary();
            renderTriangleUVS = new Dictionary();
            triangleBitmaps = new Dictionary();
            triangleRects = new Dictionary();
        }
        public function getPerTriUVForDraw(_arg1:Triangle3D):Matrix{
            var _local2:Matrix;
            var _local3:Number;
            var _local4:Number;
            var _local5:Number;
            var _local6:Number;
            var _local7:Number;
            var _local8:Number;
            var _local9:Number;
            var _local10:Number;
            var _local11:Rectangle;
            if (!triangleUVS[_arg1]){
                _local2 = (triangleUVS[_arg1] = new Matrix());
                _local3 = material.bitmap.width;
                _local4 = material.bitmap.height;
                _local5 = (_arg1.uv[0].u * _local3);
                _local6 = ((1 - _arg1.uv[0].v) * _local4);
                _local7 = (_arg1.uv[1].u * _local3);
                _local8 = ((1 - _arg1.uv[1].v) * _local4);
                _local9 = (_arg1.uv[2].u * _local3);
                _local10 = ((1 - _arg1.uv[2].v) * _local4);
                _local11 = getRectFor(_arg1);
                _local2.tx = (_local5 - _local11.x);
                _local2.ty = (_local6 - _local11.y);
                _local2.a = (_local7 - _local5);
                _local2.b = (_local8 - _local6);
                _local2.c = (_local9 - _local5);
                _local2.d = (_local10 - _local6);
                _local2.invert();
            };
            return (triangleUVS[_arg1]);
        }
        public function getRectFor(_arg1:Triangle3D):Rectangle{
            var _local2:Number;
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
            var _local15:Number;
            if (!triangleRects[_arg1]){
                _local2 = material.bitmap.width;
                _local3 = material.bitmap.height;
                _local4 = (_arg1.uv[0].u * _local2);
                _local5 = ((1 - _arg1.uv[0].v) * _local3);
                _local6 = (_arg1.uv[1].u * _local2);
                _local7 = ((1 - _arg1.uv[1].v) * _local3);
                _local8 = (_arg1.uv[2].u * _local2);
                _local9 = ((1 - _arg1.uv[2].v) * _local3);
                _local10 = Math.min(Math.min(_local4, _local6), _local8);
                _local11 = Math.min(Math.min(_local5, _local7), _local9);
                _local12 = Math.max(Math.max(_local4, _local6), _local8);
                _local13 = Math.max(Math.max(_local5, _local7), _local9);
                _local14 = (_local12 - _local10);
                _local15 = (_local13 - _local11);
                if (_local14 <= 0){
                    _local14 = 1;
                };
                if (_local15 <= 0){
                    _local15 = 1;
                };
                return ((triangleRects[_arg1] = new Rectangle(_local10, _local11, _local14, _local15)));
            };
            return (triangleRects[_arg1]);
        }
        private function perturbUVMatrix(_arg1:Matrix, _arg2:Triangle3D, _arg3:Number=2):void{
            var _local4:Number = material.bitmap.width;
            var _local5:Number = material.bitmap.height;
            var _local6:Number = _arg2.uv[0].u;
            var _local7:Number = (1 - _arg2.uv[0].v);
            var _local8:Number = _arg2.uv[1].u;
            var _local9:Number = (1 - _arg2.uv[1].v);
            var _local10:Number = _arg2.uv[2].u;
            var _local11:Number = (1 - _arg2.uv[2].v);
            var _local12:Number = (_local6 * _local4);
            var _local13:Number = (_local7 * _local5);
            var _local14:Number = (_local8 * _local4);
            var _local15:Number = (_local9 * _local5);
            var _local16:Number = (_local10 * _local4);
            var _local17:Number = (_local11 * _local5);
            var _local18:Number = (((_local10 + _local8) + _local6) / 3);
            var _local19:Number = (((_local11 + _local9) + _local7) / 3);
            var _local20:Number = (_local6 - _local18);
            var _local21:Number = (_local7 - _local19);
            var _local22:Number = (_local8 - _local18);
            var _local23:Number = (_local9 - _local19);
            var _local24:Number = (_local10 - _local18);
            var _local25:Number = (_local11 - _local19);
            var _local26:Number = ((_local20)<0) ? -(_local20) : _local20;
            var _local27:Number = ((_local21)<0) ? -(_local21) : _local21;
            var _local28:Number = ((_local22)<0) ? -(_local22) : _local22;
            var _local29:Number = ((_local23)<0) ? -(_local23) : _local23;
            var _local30:Number = ((_local24)<0) ? -(_local24) : _local24;
            var _local31:Number = ((_local25)<0) ? -(_local25) : _local25;
            var _local32:Number = ((_local26)>_local27) ? (1 / _local26) : (1 / _local27);
            var _local33:Number = ((_local28)>_local29) ? (1 / _local28) : (1 / _local29);
            var _local34:Number = ((_local30)>_local31) ? (1 / _local30) : (1 / _local31);
            _local12 = (_local12 - ((-(_local20) * _local32) * _arg3));
            _local13 = (_local13 - ((-(_local21) * _local32) * _arg3));
            _local14 = (_local14 - ((-(_local22) * _local33) * _arg3));
            _local15 = (_local15 - ((-(_local23) * _local33) * _arg3));
            _local16 = (_local16 - ((-(_local24) * _local34) * _arg3));
            _local17 = (_local17 - ((-(_local25) * _local34) * _arg3));
            _arg1.tx = _local12;
            _arg1.ty = _local13;
            _arg1.a = (_local14 - _local12);
            _arg1.b = (_local15 - _local13);
            _arg1.c = (_local16 - _local12);
            _arg1.d = (_local17 - _local13);
        }
        public function getOutputBitmapFor(_arg1:Triangle3D):BitmapData{
            var _local2:Rectangle;
            var _local3:BitmapData;
            var _local4:Rectangle;
            if (!triangleBitmaps[_arg1]){
                _local2 = getRectFor(_arg1);
                _local3 = (triangleBitmaps[_arg1] = new BitmapData(Math.ceil(_local2.width), Math.ceil(_local2.height), false, 0));
                _local4 = new Rectangle(0, 0, _local3.width, _local3.height);
                _local3.copyPixels(material.bitmap, _local4, origin);
            } else {
                _local2 = getRectFor(_arg1);
            };
            if (((material.bitmap) && (_local2))){
                triangleBitmaps[_arg1].copyPixels(material.bitmap, _local2, origin);
            };
            return (triangleBitmaps[_arg1]);
        }
        public function updateBeforeRender():void{
        }
        public function getPerTriUVForShader(_arg1:Triangle3D):Matrix{
            var _local2:Matrix;
            var _local3:Number;
            var _local4:Number;
            var _local5:Number;
            var _local6:Number;
            var _local7:Number;
            var _local8:Number;
            var _local9:Number;
            var _local10:Number;
            var _local11:Rectangle;
            if (!renderTriangleUVS[_arg1]){
                _local2 = (renderTriangleUVS[_arg1] = new Matrix());
                _local3 = material.bitmap.width;
                _local4 = material.bitmap.height;
                _local5 = (_arg1.uv[0].u * _local3);
                _local6 = ((1 - _arg1.uv[0].v) * _local4);
                _local7 = (_arg1.uv[1].u * _local3);
                _local8 = ((1 - _arg1.uv[1].v) * _local4);
                _local9 = (_arg1.uv[2].u * _local3);
                _local10 = ((1 - _arg1.uv[2].v) * _local4);
                _local11 = getRectFor(_arg1);
                _local2.tx = (_local5 - _local11.x);
                _local2.ty = (_local6 - _local11.y);
                _local2.a = (_local7 - _local5);
                _local2.b = (_local8 - _local6);
                _local2.c = (_local9 - _local5);
                _local2.d = (_local10 - _local6);
            };
            return (renderTriangleUVS[_arg1]);
        }
        public function getUVMatrixForTriangle(_arg1:Triangle3D, _arg2:Boolean=false):Matrix{
            var _local3:Matrix;
            var _local4:Number;
            var _local5:Number;
            var _local6:Number;
            var _local7:Number;
            var _local8:Number;
            var _local9:Number;
            var _local10:Number;
            var _local11:Number;
            _local3 = uvMatrices[_arg1];
            if (!_local3){
                _local3 = new Matrix();
                if (_arg2){
                    perturbUVMatrix(_local3, _arg1, 2);
                } else {
                    if (material.bitmap){
                        _local4 = material.bitmap.width;
                        _local5 = material.bitmap.height;
                        _local6 = (_arg1.uv[0].u * _local4);
                        _local7 = ((1 - _arg1.uv[0].v) * _local5);
                        _local8 = (_arg1.uv[1].u * _local4);
                        _local9 = ((1 - _arg1.uv[1].v) * _local5);
                        _local10 = (_arg1.uv[2].u * _local4);
                        _local11 = ((1 - _arg1.uv[2].v) * _local5);
                        _local3.tx = _local6;
                        _local3.ty = _local7;
                        _local3.a = (_local8 - _local6);
                        _local3.b = (_local9 - _local7);
                        _local3.c = (_local10 - _local6);
                        _local3.d = (_local11 - _local7);
                    };
                };
                if (material.bitmap){
                    uvMatrices[_arg1] = _local3;
                };
            };
            return (_local3);
        }
        public function destroy():void{
            var _local1:Object;
            for each (_local1 in uvMatrices) {
                uvMatrices[_local1] = null;
            };
            uvMatrices = null;
            shaderRenderer.destroy();
            shaderRenderer = null;
            lightMatrices = null;
        }

    }
}//package org.papervision3d.core.render.shader 
