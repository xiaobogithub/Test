//Created by Action Script Viewer - http://www.buraks.com/asv
package org.papervision3d.materials {
    import flash.display.*;
    import flash.geom.*;
    import flash.utils.*;
    import org.papervision3d.core.proto.*;
    import org.papervision3d.core.render.data.*;
    import org.papervision3d.core.geom.renderables.*;
    import org.papervision3d.core.log.*;
    import org.papervision3d.materials.utils.*;
    import org.papervision3d.core.render.draw.*;
    import org.papervision3d.core.material.*;
    import org.papervision3d.*;

    public class BitmapMaterial extends TriangleMaterial implements ITriangleDrawer {

        protected var renderRecStorage:Array
        protected var dsbc:Number
        private var b2:Number
        public var uvMatrices:Dictionary
        protected var _precise:Boolean
        protected var faz:Number
        protected var dsca:Number
        protected var ax:Number
        protected var ay:Number
        protected var az:Number
        protected var tempPreGrp:Graphics
        protected var fbz:Number
        private var c2:Number
        protected var mcax:Number
        protected var mcay:Number
        protected var mcaz:Number
        private var d2:Number
        protected var bx:Number
        protected var by:Number
        protected var bz:Number
        protected var fcz:Number
        public var minimumRenderSize:Number = 4
        protected var dbcx:Number
        protected var dbcy:Number
        protected var cx:Number
        protected var cullRect:Rectangle
        protected var cy:Number
        protected var cz:Number
        protected var dmax:Number
        protected var dabx:Number
        protected var _perPixelPrecision:int = 8
        protected var daby:Number
        protected var tempPreRSD:RenderSessionData
        private var x0:Number
        private var x1:Number
        private var x2:Number
        protected var mbcy:Number
        protected var mbcz:Number
        protected var mbcx:Number
        protected var tempPreBmp:BitmapData
        private var y0:Number
        protected var focus:Number = 200
        private var y2:Number
        protected var _texture:Object
        private var y1:Number
        protected var tempTriangleMatrix:Matrix
        protected var maby:Number
        protected var mabz:Number
        protected var dsab:Number
        protected var mabx:Number
        protected var dcax:Number
        protected var dcay:Number
        private var a2:Number
        protected var _precision:int = 8

        protected static const DEFAULT_FOCUS:Number = 200;

        protected static var _triMatrix:Matrix = new Matrix();
        protected static var _triMap:Matrix;
        public static var AUTO_MIP_MAPPING:Boolean = false;
        public static var MIP_MAP_DEPTH:Number = 8;
        protected static var hitRect:Rectangle = new Rectangle();
        protected static var _localMatrix:Matrix = new Matrix();

        public function BitmapMaterial(_arg1:BitmapData=null, _arg2:Boolean=false){
            uvMatrices = new Dictionary();
            tempTriangleMatrix = new Matrix();
            super();
            if (_arg1){
                texture = _arg1;
            };
            this.precise = _arg2;
            createRenderRecStorage();
        }
        public function transformUV(_arg1:Triangle3D):Matrix{
            var _local2:Array;
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
            var _local15:Matrix;
            var _local16:Matrix;
            if (!_arg1.uv){
                PaperLogger.error("MaterialObject3D: transformUV() uv not found!");
            } else {
                if (bitmap){
                    _local2 = _arg1.uv;
                    _local3 = (bitmap.width * maxU);
                    _local4 = (bitmap.height * maxV);
                    _local5 = (_local3 * _arg1.uv0.u);
                    _local6 = (_local4 * (1 - _arg1.uv0.v));
                    _local7 = (_local3 * _arg1.uv1.u);
                    _local8 = (_local4 * (1 - _arg1.uv1.v));
                    _local9 = (_local3 * _arg1.uv2.u);
                    _local10 = (_local4 * (1 - _arg1.uv2.v));
                    if ((((((_local5 == _local7)) && ((_local6 == _local8)))) || ((((_local5 == _local9)) && ((_local6 == _local10)))))){
                        _local5 = (_local5 - ((_local5)>0.05) ? 0.05 : -0.05);
                        _local6 = (_local6 - ((_local6)>0.07) ? 0.07 : -0.07);
                    };
                    if ((((_local9 == _local7)) && ((_local10 == _local8)))){
                        _local9 = (_local9 - ((_local9)>0.05) ? 0.04 : -0.04);
                        _local10 = (_local10 - ((_local10)>0.06) ? 0.06 : -0.06);
                    };
                    _local11 = (_local7 - _local5);
                    _local12 = (_local8 - _local6);
                    _local13 = (_local9 - _local5);
                    _local14 = (_local10 - _local6);
                    _local15 = new Matrix(_local11, _local12, _local13, _local14, _local5, _local6);
                    if (Papervision3D.useRIGHTHANDED){
                        _local15.scale(-1, 1);
                        _local15.translate(_local3, 0);
                    };
                    _local15.invert();
                    _local16 = (uvMatrices[_arg1]) ? uvMatrices[_arg1] : uvMatrices[_arg1] = _local15.clone();
                    _local16.a = _local15.a;
                    _local16.b = _local15.b;
                    _local16.c = _local15.c;
                    _local16.d = _local15.d;
                    _local16.tx = _local15.tx;
                    _local16.ty = _local15.ty;
                } else {
                    PaperLogger.error("MaterialObject3D: transformUV() material.bitmap not found!");
                };
            };
            return (_local16);
        }
        protected function renderRec(_arg1:Matrix, _arg2:Vertex3DInstance, _arg3:Vertex3DInstance, _arg4:Vertex3DInstance, _arg5:Number):void{
            az = _arg2.z;
            bz = _arg3.z;
            cz = _arg4.z;
            if ((((((az <= 0)) && ((bz <= 0)))) && ((cz <= 0)))){
                return;
            };
            cx = _arg4.x;
            cy = _arg4.y;
            bx = _arg3.x;
            by = _arg3.y;
            ax = _arg2.x;
            ay = _arg2.y;
            if (cullRect){
                hitRect.x = ((bx < ax)) ? ((bx < cx)) ? bx : cx : ((ax < cx)) ? ax : cx;
                hitRect.width = (((bx > ax)) ? ((bx > cx)) ? bx : cx : ((ax > cx)) ? ax : cx + ((hitRect.x < 0)) ? -(hitRect.x) : hitRect.x);
                hitRect.y = ((by < ay)) ? ((by < cy)) ? by : cy : ((ay < cy)) ? ay : cy;
                hitRect.height = (((by > ay)) ? ((by > cy)) ? by : cy : ((ay > cy)) ? ay : cy + ((hitRect.y < 0)) ? -(hitRect.y) : hitRect.y);
                if (!(((hitRect.right < cullRect.left)) || ((hitRect.left > cullRect.right)))){
                    if (!(((hitRect.bottom < cullRect.top)) || ((hitRect.top > cullRect.bottom)))){
                    } else {
                        return;
                    };
                } else {
                    return;
                };
            };
            if ((((((((_arg5 >= 100)) || ((hitRect.width < minimumRenderSize)))) || ((hitRect.height < minimumRenderSize)))) || ((focus == Infinity)))){
                a2 = (_arg3.x - _arg2.x);
                b2 = (_arg3.y - _arg2.y);
                c2 = (_arg4.x - _arg2.x);
                d2 = (_arg4.y - _arg2.y);
                tempTriangleMatrix.a = ((_arg1.a * a2) + (_arg1.b * c2));
                tempTriangleMatrix.b = ((_arg1.a * b2) + (_arg1.b * d2));
                tempTriangleMatrix.c = ((_arg1.c * a2) + (_arg1.d * c2));
                tempTriangleMatrix.d = ((_arg1.c * b2) + (_arg1.d * d2));
                tempTriangleMatrix.tx = (((_arg1.tx * a2) + (_arg1.ty * c2)) + _arg2.x);
                tempTriangleMatrix.ty = (((_arg1.tx * b2) + (_arg1.ty * d2)) + _arg2.y);
                if (lineAlpha){
                    tempPreGrp.lineStyle(lineThickness, lineColor, lineAlpha);
                };
                tempPreGrp.beginBitmapFill(tempPreBmp, tempTriangleMatrix, tiled, smooth);
                tempPreGrp.moveTo(_arg2.x, _arg2.y);
                tempPreGrp.lineTo(_arg3.x, _arg3.y);
                tempPreGrp.lineTo(_arg4.x, _arg4.y);
                tempPreGrp.endFill();
                if (lineAlpha){
                    tempPreGrp.lineStyle();
                };
                tempPreRSD.renderStatistics.triangles++;
                return;
            };
            faz = (focus + az);
            fbz = (focus + bz);
            fcz = (focus + cz);
            mabz = (2 / (faz + fbz));
            mbcz = (2 / (fbz + fcz));
            mcaz = (2 / (fcz + faz));
            mabx = (((ax * faz) + (bx * fbz)) * mabz);
            maby = (((ay * faz) + (by * fbz)) * mabz);
            mbcx = (((bx * fbz) + (cx * fcz)) * mbcz);
            mbcy = (((by * fbz) + (cy * fcz)) * mbcz);
            mcax = (((cx * fcz) + (ax * faz)) * mcaz);
            mcay = (((cy * fcz) + (ay * faz)) * mcaz);
            dabx = ((ax + bx) - mabx);
            daby = ((ay + by) - maby);
            dbcx = ((bx + cx) - mbcx);
            dbcy = ((by + cy) - mbcy);
            dcax = ((cx + ax) - mcax);
            dcay = ((cy + ay) - mcay);
            dsab = ((dabx * dabx) + (daby * daby));
            dsbc = ((dbcx * dbcx) + (dbcy * dbcy));
            dsca = ((dcax * dcax) + (dcay * dcay));
            var _local6:int = (_arg5 + 1);
            var _local7:RenderRecStorage = RenderRecStorage(renderRecStorage[int(_arg5)]);
            var _local8:Matrix = _local7.mat;
            if ((((((dsab <= _precision)) && ((dsca <= _precision)))) && ((dsbc <= _precision)))){
                a2 = (_arg3.x - _arg2.x);
                b2 = (_arg3.y - _arg2.y);
                c2 = (_arg4.x - _arg2.x);
                d2 = (_arg4.y - _arg2.y);
                tempTriangleMatrix.a = ((_arg1.a * a2) + (_arg1.b * c2));
                tempTriangleMatrix.b = ((_arg1.a * b2) + (_arg1.b * d2));
                tempTriangleMatrix.c = ((_arg1.c * a2) + (_arg1.d * c2));
                tempTriangleMatrix.d = ((_arg1.c * b2) + (_arg1.d * d2));
                tempTriangleMatrix.tx = (((_arg1.tx * a2) + (_arg1.ty * c2)) + _arg2.x);
                tempTriangleMatrix.ty = (((_arg1.tx * b2) + (_arg1.ty * d2)) + _arg2.y);
                if (lineAlpha){
                    tempPreGrp.lineStyle(lineThickness, lineColor, lineAlpha);
                };
                tempPreGrp.beginBitmapFill(tempPreBmp, tempTriangleMatrix, tiled, smooth);
                tempPreGrp.moveTo(_arg2.x, _arg2.y);
                tempPreGrp.lineTo(_arg3.x, _arg3.y);
                tempPreGrp.lineTo(_arg4.x, _arg4.y);
                tempPreGrp.endFill();
                if (lineAlpha){
                    tempPreGrp.lineStyle();
                };
                tempPreRSD.renderStatistics.triangles++;
                return;
            };
            if ((((((dsab > _precision)) && ((dsca > _precision)))) && ((dsbc > _precision)))){
                _local8.a = (_arg1.a * 2);
                _local8.b = (_arg1.b * 2);
                _local8.c = (_arg1.c * 2);
                _local8.d = (_arg1.d * 2);
                _local8.tx = (_arg1.tx * 2);
                _local8.ty = (_arg1.ty * 2);
                _local7.v0.x = (mabx * 0.5);
                _local7.v0.y = (maby * 0.5);
                _local7.v0.z = ((az + bz) * 0.5);
                _local7.v1.x = (mbcx * 0.5);
                _local7.v1.y = (mbcy * 0.5);
                _local7.v1.z = ((bz + cz) * 0.5);
                _local7.v2.x = (mcax * 0.5);
                _local7.v2.y = (mcay * 0.5);
                _local7.v2.z = ((cz + az) * 0.5);
                renderRec(_local8, _arg2, _local7.v0, _local7.v2, _local6);
                _local8.tx = (_local8.tx - 1);
                renderRec(_local8, _local7.v0, _arg3, _local7.v1, _local6);
                _local8.ty = (_local8.ty - 1);
                _local8.tx = (_arg1.tx * 2);
                renderRec(_local8, _local7.v2, _local7.v1, _arg4, _local6);
                _local8.a = (-(_arg1.a) * 2);
                _local8.b = (-(_arg1.b) * 2);
                _local8.c = (-(_arg1.c) * 2);
                _local8.d = (-(_arg1.d) * 2);
                _local8.tx = ((-(_arg1.tx) * 2) + 1);
                _local8.ty = ((-(_arg1.ty) * 2) + 1);
                renderRec(_local8, _local7.v1, _local7.v2, _local7.v0, _local6);
                return;
            };
            dmax = ((dsca > dsbc)) ? ((dsca > dsab)) ? dsca : dsab : ((dsbc > dsab)) ? dsbc : dsab;
            if (dsab == dmax){
                _local8.a = (_arg1.a * 2);
                _local8.b = _arg1.b;
                _local8.c = (_arg1.c * 2);
                _local8.d = _arg1.d;
                _local8.tx = (_arg1.tx * 2);
                _local8.ty = _arg1.ty;
                _local7.v0.x = (mabx * 0.5);
                _local7.v0.y = (maby * 0.5);
                _local7.v0.z = ((az + bz) * 0.5);
                renderRec(_local8, _arg2, _local7.v0, _arg4, _local6);
                _local8.a = ((_arg1.a * 2) + _arg1.b);
                _local8.c = ((2 * _arg1.c) + _arg1.d);
                _local8.tx = (((_arg1.tx * 2) + _arg1.ty) - 1);
                renderRec(_local8, _local7.v0, _arg3, _arg4, _local6);
                return;
            };
            if (dsca == dmax){
                _local8.a = _arg1.a;
                _local8.b = (_arg1.b * 2);
                _local8.c = _arg1.c;
                _local8.d = (_arg1.d * 2);
                _local8.tx = _arg1.tx;
                _local8.ty = (_arg1.ty * 2);
                _local7.v2.x = (mcax * 0.5);
                _local7.v2.y = (mcay * 0.5);
                _local7.v2.z = ((cz + az) * 0.5);
                renderRec(_local8, _arg2, _arg3, _local7.v2, _local6);
                _local8.b = (_local8.b + _arg1.a);
                _local8.d = (_local8.d + _arg1.c);
                _local8.ty = (_local8.ty + (_arg1.tx - 1));
                renderRec(_local8, _local7.v2, _arg3, _arg4, _local6);
                return;
            };
            _local8.a = (_arg1.a - _arg1.b);
            _local8.b = (_arg1.b * 2);
            _local8.c = (_arg1.c - _arg1.d);
            _local8.d = (_arg1.d * 2);
            _local8.tx = (_arg1.tx - _arg1.ty);
            _local8.ty = (_arg1.ty * 2);
            _local7.v1.x = (mbcx * 0.5);
            _local7.v1.y = (mbcy * 0.5);
            _local7.v1.z = ((bz + cz) * 0.5);
            renderRec(_local8, _arg2, _arg3, _local7.v1, _local6);
            _local8.a = (_arg1.a * 2);
            _local8.b = (_arg1.b - _arg1.a);
            _local8.c = (_arg1.c * 2);
            _local8.d = (_arg1.d - _arg1.c);
            _local8.tx = (_arg1.tx * 2);
            _local8.ty = (_arg1.ty - _arg1.tx);
            renderRec(_local8, _arg2, _local7.v1, _arg4, _local6);
        }
        protected function createRenderRecStorage():void{
            this.renderRecStorage = new Array();
            var _local1:int;
            while (_local1 <= 100) {
                this.renderRecStorage[_local1] = new RenderRecStorage();
                _local1++;
            };
        }
        public function get texture():Object{
            return (this._texture);
        }
        public function resetUVS():void{
            uvMatrices = new Dictionary(false);
        }
        public function set pixelPrecision(_arg1:int):void{
            _precision = ((_arg1 * _arg1) * 1.4);
            _perPixelPrecision = _arg1;
        }
        protected function correctBitmap(_arg1:BitmapData):BitmapData{
            var _local2:BitmapData;
            var _local3:Number = (1 << MIP_MAP_DEPTH);
            var _local4:Number = (_arg1.width / _local3);
            _local4 = ((_local4 == uint(_local4))) ? _local4 : (uint(_local4) + 1);
            var _local5:Number = (_arg1.height / _local3);
            _local5 = ((_local5 == uint(_local5))) ? _local5 : (uint(_local5) + 1);
            var _local6:Number = (_local3 * _local4);
            var _local7:Number = (_local3 * _local5);
            var _local8:Boolean;
            if (_local6 > 2880){
                _local6 = _arg1.width;
                _local8 = false;
            };
            if (_local7 > 2880){
                _local7 = _arg1.height;
                _local8 = false;
            };
            if (!_local8){
                PaperLogger.warning((("Material " + this.name) + ": Texture too big for mip mapping. Resizing recommended for better performance and quality."));
            };
            if (((_arg1) && (((!(((_arg1.width % _local3) == 0))) || (!(((_arg1.height % _local3) == 0))))))){
                _local2 = new BitmapData(_local6, _local7, _arg1.transparent, 0);
                widthOffset = _arg1.width;
                heightOffset = _arg1.height;
                this.maxU = (_arg1.width / _local6);
                this.maxV = (_arg1.height / _local7);
                _local2.draw(_arg1);
                extendBitmapEdges(_local2, _arg1.width, _arg1.height);
            } else {
                this.maxU = (this.maxV = 1);
                _local2 = _arg1;
            };
            return (_local2);
        }
        protected function createBitmap(_arg1:BitmapData):BitmapData{
            var _local2:BitmapData;
            resetMapping();
            if (AUTO_MIP_MAPPING){
                _local2 = correctBitmap(_arg1);
            } else {
                this.maxU = (this.maxV = 1);
                _local2 = _arg1;
            };
            return (_local2);
        }
        public function get precise():Boolean{
            return (_precise);
        }
        public function set texture(_arg1:Object):void{
            if ((_arg1 is BitmapData) == false){
                PaperLogger.error("BitmapMaterial.texture requires a BitmapData object for the texture");
                return;
            };
            bitmap = createBitmap(BitmapData(_arg1));
            _texture = _arg1;
        }
        override public function clone():MaterialObject3D{
            var _local1:MaterialObject3D = super.clone();
            _local1.maxU = this.maxU;
            _local1.maxV = this.maxV;
            return (_local1);
        }
        public function resetMapping():void{
            uvMatrices = new Dictionary();
        }
        override public function drawTriangle(_arg1:Triangle3D, _arg2:Graphics, _arg3:RenderSessionData, _arg4:BitmapData=null, _arg5:Matrix=null):void{
            if (!_precise){
                if (lineAlpha){
                    _arg2.lineStyle(lineThickness, lineColor, lineAlpha);
                };
                if (bitmap){
                    _triMap = (_arg5) ? _arg5 : ((uvMatrices[_arg1]) || (transformUV(_arg1)));
                    x0 = _arg1.v0.vertex3DInstance.x;
                    y0 = _arg1.v0.vertex3DInstance.y;
                    x1 = _arg1.v1.vertex3DInstance.x;
                    y1 = _arg1.v1.vertex3DInstance.y;
                    x2 = _arg1.v2.vertex3DInstance.x;
                    y2 = _arg1.v2.vertex3DInstance.y;
                    _triMatrix.a = (x1 - x0);
                    _triMatrix.b = (y1 - y0);
                    _triMatrix.c = (x2 - x0);
                    _triMatrix.d = (y2 - y0);
                    _triMatrix.tx = x0;
                    _triMatrix.ty = y0;
                    _localMatrix.a = _triMap.a;
                    _localMatrix.b = _triMap.b;
                    _localMatrix.c = _triMap.c;
                    _localMatrix.d = _triMap.d;
                    _localMatrix.tx = _triMap.tx;
                    _localMatrix.ty = _triMap.ty;
                    _localMatrix.concat(_triMatrix);
                    _arg2.beginBitmapFill((_arg4) ? _arg4 : bitmap, _localMatrix, tiled, smooth);
                };
                _arg2.moveTo(x0, y0);
                _arg2.lineTo(x1, y1);
                _arg2.lineTo(x2, y2);
                _arg2.lineTo(x0, y0);
                if (bitmap){
                    _arg2.endFill();
                };
                if (lineAlpha){
                    _arg2.lineStyle();
                };
                _arg3.renderStatistics.triangles++;
            } else {
                if (bitmap){
                    _triMap = (_arg5) ? _arg5 : ((uvMatrices[_arg1]) || (transformUV(_arg1)));
                    focus = _arg3.camera.focus;
                    tempPreBmp = (_arg4) ? _arg4 : bitmap;
                    tempPreRSD = _arg3;
                    tempPreGrp = _arg2;
                    cullRect = _arg3.viewPort.cullingRectangle;
                    renderRec(_triMap, _arg1.v0.vertex3DInstance, _arg1.v1.vertex3DInstance, _arg1.v2.vertex3DInstance, 0);
                };
            };
        }
        public function get precision():int{
            return (_precision);
        }
        override public function copy(_arg1:MaterialObject3D):void{
            super.copy(_arg1);
            this.maxU = _arg1.maxU;
            this.maxV = _arg1.maxV;
        }
        override public function toString():String{
            return (((((("Texture:" + this.texture) + " lineColor:") + this.lineColor) + " lineAlpha:") + this.lineAlpha));
        }
        public function get pixelPrecision():int{
            return (_perPixelPrecision);
        }
        public function set precise(_arg1:Boolean):void{
            _precise = _arg1;
        }
        protected function extendBitmapEdges(_arg1:BitmapData, _arg2:Number, _arg3:Number):void{
            var _local6:int;
            var _local4:Rectangle = new Rectangle();
            var _local5:Point = new Point();
            if (_arg1.width > _arg2){
                _local4.x = (_arg2 - 1);
                _local4.y = 0;
                _local4.width = 1;
                _local4.height = _arg3;
                _local5.y = 0;
                _local6 = _arg2;
                while (_local6 < _arg1.width) {
                    _local5.x = _local6;
                    _arg1.copyPixels(_arg1, _local4, _local5);
                    _local6++;
                };
            };
            if (_arg1.height > _arg3){
                _local4.x = 0;
                _local4.y = (_arg3 - 1);
                _local4.width = _arg1.width;
                _local4.height = 1;
                _local5.x = 0;
                _local6 = _arg3;
                while (_local6 < _arg1.height) {
                    _local5.y = _local6;
                    _arg1.copyPixels(_arg1, _local4, _local5);
                    _local6++;
                };
            };
        }
        override public function destroy():void{
            super.destroy();
            if (uvMatrices){
                uvMatrices = null;
            };
            if (bitmap){
                bitmap.dispose();
            };
            this.renderRecStorage = null;
        }
        public function set precision(_arg1:int):void{
            _precision = _arg1;
        }

    }
}//package org.papervision3d.materials 
