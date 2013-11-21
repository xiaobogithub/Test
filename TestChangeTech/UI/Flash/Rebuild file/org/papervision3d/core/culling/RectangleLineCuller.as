//Created by Action Script Viewer - http://www.buraks.com/asv
package org.papervision3d.core.culling {
    import flash.geom.*;
    import org.papervision3d.core.geom.renderables.*;
    import org.papervision3d.core.math.util.*;

    public class RectangleLineCuller implements ILineCuller {

        private var lineBoundsRect:Rectangle
        private var rectIntersection:Rectangle
        private var cullingRectangle:Rectangle

        public function RectangleLineCuller(_arg1:Rectangle=null):void{
            if (_arg1){
                this.cullingRectangle = _arg1;
            };
            lineBoundsRect = new Rectangle();
            rectIntersection = new Rectangle();
        }
        public function testLine(_arg1:Line3D):Boolean{
            if (((!(_arg1.v0.vertex3DInstance.visible)) || (!(_arg1.v1.vertex3DInstance.visible)))){
                return (false);
            };
            var _local2:Number = _arg1.v0.vertex3DInstance.x;
            var _local3:Number = _arg1.v0.vertex3DInstance.y;
            var _local4:Number = _arg1.v1.vertex3DInstance.x;
            var _local5:Number = _arg1.v1.vertex3DInstance.y;
            lineBoundsRect.width = Math.abs((_local4 - _local2));
            lineBoundsRect.height = Math.abs((_local5 - _local3));
            if (_local2 < _local4){
                lineBoundsRect.x = _local2;
            } else {
                lineBoundsRect.x = _local4;
            };
            if (_local3 < _local5){
                lineBoundsRect.y = _local3;
            } else {
                lineBoundsRect.y = _local5;
            };
            if (cullingRectangle.containsRect(lineBoundsRect)){
                return (true);
            };
            if (!FastRectangleTools.intersects(lineBoundsRect, cullingRectangle)){
                return (false);
            };
            rectIntersection = FastRectangleTools.intersection(lineBoundsRect, cullingRectangle);
            var _local6:Number = ((_local5 - _local3) / (_local4 - _local2));
            var _local7:Number = (_local3 - (_local6 * _local2));
            var _local8:Number = ((cullingRectangle.top - _local7) / _local6);
            if ((((_local8 > rectIntersection.left)) && ((_local8 < rectIntersection.right)))){
                return (true);
            };
            _local8 = ((cullingRectangle.bottom - _local7) / _local6);
            if ((((_local8 > rectIntersection.left)) && ((_local8 < rectIntersection.right)))){
                return (true);
            };
            var _local9:Number = ((_local6 * cullingRectangle.left) + _local7);
            if ((((_local9 > rectIntersection.top)) && ((_local9 < rectIntersection.bottom)))){
                return (true);
            };
            _local9 = ((_local6 * cullingRectangle.right) + _local7);
            if ((((_local9 > rectIntersection.top)) && ((_local9 < rectIntersection.bottom)))){
                return (true);
            };
            return (false);
        }

    }
}//package org.papervision3d.core.culling 
