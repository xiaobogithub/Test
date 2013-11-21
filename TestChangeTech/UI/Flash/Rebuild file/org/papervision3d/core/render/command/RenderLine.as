//Created by Action Script Viewer - http://www.buraks.com/asv
package org.papervision3d.core.render.command {
    import flash.display.*;
    import flash.geom.*;
    import org.papervision3d.core.render.data.*;
    import org.papervision3d.core.math.*;
    import org.papervision3d.core.geom.renderables.*;
    import org.papervision3d.materials.special.*;

    public class RenderLine extends RenderableListItem implements IRenderListItem {

        public var renderer:LineMaterial
        private var l1:Number2D
        private var l2:Number2D
        private var p:Number2D
        public var line:Line3D
        private var v:Number2D
        private var cp3d:Number3D

        private static var mouseVector:Number3D = Number3D.ZERO;
        private static var lineVector:Number3D = Number3D.ZERO;

        public function RenderLine(_arg1:Line3D){
            this.renderable = Line3D;
            this.renderableInstance = _arg1;
            this.line = _arg1;
            p = new Number2D();
            l1 = new Number2D();
            l2 = new Number2D();
            v = new Number2D();
            cp3d = new Number3D();
        }
        override public function hitTestPoint2D(_arg1:Point, _arg2:RenderHitData):RenderHitData{
            var _local3:Number;
            var _local4:Number;
            var _local5:Number;
            if (renderer.interactive){
                _local3 = line.size;
                p.reset(_arg1.x, _arg1.y);
                l1.reset(line.v0.vertex3DInstance.x, line.v0.vertex3DInstance.y);
                l2.reset(line.v1.vertex3DInstance.x, line.v1.vertex3DInstance.y);
                v.copyFrom(l2);
                v.minusEq(l1);
                _local4 = ((((p.x - l1.x) * (l2.x - l1.x)) + ((p.y - l1.y) * (l2.y - l1.y))) / ((v.x * v.x) + (v.y * v.y)));
                if ((((_local4 > 0)) && ((_local4 < 1)))){
                    v.multiplyEq(_local4);
                    v.plusEq(l1);
                    v.minusEq(p);
                    _local5 = ((v.x * v.x) + (v.y * v.y));
                    if (_local5 < (_local3 * _local3)){
                        _arg2.displayObject3D = line.instance;
                        _arg2.material = renderer;
                        _arg2.renderable = line;
                        _arg2.hasHit = true;
                        cp3d.reset((line.v1.x - line.v0.x), (line.v1.y - line.v0.y), (line.v1.x - line.v0.x));
                        cp3d.x = (cp3d.x * _local4);
                        cp3d.y = (cp3d.y * _local4);
                        cp3d.z = (cp3d.z * _local4);
                        cp3d.x = (cp3d.x + line.v0.x);
                        cp3d.y = (cp3d.y + line.v0.y);
                        cp3d.z = (cp3d.z + line.v0.z);
                        _arg2.x = cp3d.x;
                        _arg2.y = cp3d.y;
                        _arg2.z = cp3d.z;
                        _arg2.u = 0;
                        _arg2.v = 0;
                        return (_arg2);
                    };
                };
            };
            return (_arg2);
        }
        override public function render(_arg1:RenderSessionData, _arg2:Graphics):void{
            renderer.drawLine(line, _arg2, _arg1);
        }

    }
}//package org.papervision3d.core.render.command 
