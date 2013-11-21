//Created by Action Script Viewer - http://www.buraks.com/asv
package org.papervision3d.core.render.command {
    import flash.display.*;
    import flash.geom.*;
    import org.papervision3d.core.proto.*;
    import org.papervision3d.materials.*;
    import org.papervision3d.core.render.data.*;
    import org.papervision3d.core.math.*;
    import org.papervision3d.core.geom.renderables.*;
    import org.papervision3d.core.render.draw.*;

    public class RenderTriangle extends RenderableListItem implements IRenderListItem {

        public var container:Sprite
        public var renderer:ITriangleDrawer
        public var triangle:Triangle3D
        private var position:Number3D
        public var renderMat:MaterialObject3D
        protected var vx0:Vertex3DInstance
        protected var vx1:Vertex3DInstance
        protected var vx2:Vertex3DInstance
        protected var vPointL:Vertex3DInstance

        protected static var resPA:Vertex3DInstance = new Vertex3DInstance();
        protected static var resBA:Vertex3DInstance = new Vertex3DInstance();
        protected static var vPoint:Vertex3DInstance = new Vertex3DInstance();
        protected static var resRA:Vertex3DInstance = new Vertex3DInstance();

        public function RenderTriangle(_arg1:Triangle3D):void{
            position = new Number3D();
            super();
            this.triangle = _arg1;
            renderableInstance = _arg1;
            renderable = Triangle3D;
        }
        override public function hitTestPoint2D(_arg1:Point, _arg2:RenderHitData):RenderHitData{
            renderMat = triangle.material;
            if (!renderMat){
                renderMat = triangle.instance.material;
            };
            if (renderMat.interactive){
                vPointL = RenderTriangle.vPoint;
                vPointL.x = _arg1.x;
                vPointL.y = _arg1.y;
                vx0 = triangle.v0.vertex3DInstance;
                vx1 = triangle.v1.vertex3DInstance;
                vx2 = triangle.v2.vertex3DInstance;
                if (sameSide(vPointL, vx0, vx1, vx2)){
                    if (sameSide(vPointL, vx1, vx0, vx2)){
                        if (sameSide(vPointL, vx2, vx0, vx1)){
                            return (deepHitTest(triangle, vPointL, _arg2));
                        };
                    };
                };
            };
            return (_arg2);
        }
        private function deepHitTest(_arg1:Triangle3D, _arg2:Vertex3DInstance, _arg3:RenderHitData):RenderHitData{
            var _local44:MovieMaterial;
            var _local45:Rectangle;
            var _local4:Vertex3DInstance = _arg1.v0.vertex3DInstance;
            var _local5:Vertex3DInstance = _arg1.v1.vertex3DInstance;
            var _local6:Vertex3DInstance = _arg1.v2.vertex3DInstance;
            var _local7:Number = (_local6.x - _local4.x);
            var _local8:Number = (_local6.y - _local4.y);
            var _local9:Number = (_local5.x - _local4.x);
            var _local10:Number = (_local5.y - _local4.y);
            var _local11:Number = (_arg2.x - _local4.x);
            var _local12:Number = (_arg2.y - _local4.y);
            var _local13:Number = ((_local7 * _local7) + (_local8 * _local8));
            var _local14:Number = ((_local7 * _local9) + (_local8 * _local10));
            var _local15:Number = ((_local7 * _local11) + (_local8 * _local12));
            var _local16:Number = ((_local9 * _local9) + (_local10 * _local10));
            var _local17:Number = ((_local9 * _local11) + (_local10 * _local12));
            var _local18:Number = (1 / ((_local13 * _local16) - (_local14 * _local14)));
            var _local19:Number = (((_local16 * _local15) - (_local14 * _local17)) * _local18);
            var _local20:Number = (((_local13 * _local17) - (_local14 * _local15)) * _local18);
            var _local21:Number = (_arg1.v2.x - _arg1.v0.x);
            var _local22:Number = (_arg1.v2.y - _arg1.v0.y);
            var _local23:Number = (_arg1.v2.z - _arg1.v0.z);
            var _local24:Number = (_arg1.v1.x - _arg1.v0.x);
            var _local25:Number = (_arg1.v1.y - _arg1.v0.y);
            var _local26:Number = (_arg1.v1.z - _arg1.v0.z);
            var _local27:Number = ((_arg1.v0.x + (_local21 * _local19)) + (_local24 * _local20));
            var _local28:Number = ((_arg1.v0.y + (_local22 * _local19)) + (_local25 * _local20));
            var _local29:Number = ((_arg1.v0.z + (_local23 * _local19)) + (_local26 * _local20));
            var _local30:Array = _arg1.uv;
            var _local31:Number = _local30[0].u;
            var _local32:Number = _local30[1].u;
            var _local33:Number = _local30[2].u;
            var _local34:Number = _local30[0].v;
            var _local35:Number = _local30[1].v;
            var _local36:Number = _local30[2].v;
            var _local37:Number = ((((_local32 - _local31) * _local20) + ((_local33 - _local31) * _local19)) + _local31);
            var _local38:Number = ((((_local35 - _local34) * _local20) + ((_local36 - _local34) * _local19)) + _local34);
            if (triangle.material){
                renderMat = _arg1.material;
            } else {
                renderMat = _arg1.instance.material;
            };
            var _local39:BitmapData = renderMat.bitmap;
            var _local40:Number = 1;
            var _local41:Number = 1;
            var _local42:Number = 0;
            var _local43:Number = 0;
            if ((renderMat is MovieMaterial)){
                _local44 = (renderMat as MovieMaterial);
                _local45 = _local44.rect;
                if (_local45){
                    _local42 = _local45.x;
                    _local43 = _local45.y;
                    _local40 = _local45.width;
                    _local41 = _local45.height;
                };
            } else {
                if (_local39){
                    _local40 = (BitmapMaterial.AUTO_MIP_MAPPING) ? renderMat.widthOffset : _local39.width;
                    _local41 = (BitmapMaterial.AUTO_MIP_MAPPING) ? renderMat.heightOffset : _local39.height;
                };
            };
            _arg3.displayObject3D = _arg1.instance;
            _arg3.material = renderMat;
            _arg3.renderable = _arg1;
            _arg3.hasHit = true;
            position.x = _local27;
            position.y = _local28;
            position.z = _local29;
            Matrix3D.multiplyVector(_arg1.instance.world, position);
            _arg3.x = position.x;
            _arg3.y = position.y;
            _arg3.z = position.z;
            _arg3.u = ((_local37 * _local40) + _local42);
            _arg3.v = ((_local41 - (_local38 * _local41)) + _local43);
            return (_arg3);
        }
        public function sameSide(_arg1:Vertex3DInstance, _arg2:Vertex3DInstance, _arg3:Vertex3DInstance, _arg4:Vertex3DInstance):Boolean{
            Vertex3DInstance.subTo(_arg4, _arg3, resBA);
            Vertex3DInstance.subTo(_arg1, _arg3, resPA);
            Vertex3DInstance.subTo(_arg2, _arg3, resRA);
            return (((Vertex3DInstance.cross(resBA, resPA) * Vertex3DInstance.cross(resBA, resRA)) >= 0));
        }
        override public function render(_arg1:RenderSessionData, _arg2:Graphics):void{
            renderer.drawTriangle(triangle, _arg2, _arg1);
        }

    }
}//package org.papervision3d.core.render.command 
