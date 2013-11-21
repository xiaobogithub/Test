//Created by Action Script Viewer - http://www.buraks.com/asv
package org.papervision3d.core.culling {
    import flash.geom.*;
    import org.papervision3d.core.geom.renderables.*;
    import org.papervision3d.core.math.util.*;

    public class RectangleTriangleCuller extends DefaultTriangleCuller implements ITriangleCuller {

        public var cullingRectangle:Rectangle

        private static const DEFAULT_RECT_X:Number = -((DEFAULT_RECT_W / 2));
        private static const DEFAULT_RECT_W:Number = 640;
        private static const DEFAULT_RECT_H:Number = 480;
        private static const DEFAULT_RECT_Y:Number = -((DEFAULT_RECT_H / 2));

        private static var hitRect:Rectangle = new Rectangle();

        public function RectangleTriangleCuller(_arg1:Rectangle=null):void{
            cullingRectangle = new Rectangle(DEFAULT_RECT_X, DEFAULT_RECT_Y, DEFAULT_RECT_W, DEFAULT_RECT_H);
            super();
            if (_arg1){
                this.cullingRectangle = _arg1;
            };
        }
        override public function testFace(_arg1:Triangle3D, _arg2:Vertex3DInstance, _arg3:Vertex3DInstance, _arg4:Vertex3DInstance):Boolean{
            if (super.testFace(_arg1, _arg2, _arg3, _arg4)){
                hitRect.x = Math.min(_arg4.x, Math.min(_arg3.x, _arg2.x));
                hitRect.width = (Math.max(_arg4.x, Math.max(_arg3.x, _arg2.x)) + Math.abs(hitRect.x));
                hitRect.y = Math.min(_arg4.y, Math.min(_arg3.y, _arg2.y));
                hitRect.height = (Math.max(_arg4.y, Math.max(_arg3.y, _arg2.y)) + Math.abs(hitRect.y));
                return (FastRectangleTools.intersects(cullingRectangle, hitRect));
            };
            return (false);
        }

    }
}//package org.papervision3d.core.culling 
