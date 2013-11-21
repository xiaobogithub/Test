//Created by Action Script Viewer - http://www.buraks.com/asv
package org.papervision3d.materials {
    import flash.display.*;
    import flash.geom.*;
    import org.papervision3d.core.render.data.*;
    import org.papervision3d.core.geom.renderables.*;
    import org.papervision3d.core.render.draw.*;
    import org.papervision3d.core.material.*;

    public class WireframeMaterial extends TriangleMaterial implements ITriangleDrawer {

        public function WireframeMaterial(_arg1:Number=0xFF00FF, _arg2:Number=100, _arg3:Number=0){
            this.lineColor = _arg1;
            this.lineAlpha = _arg2;
            this.lineThickness = _arg3;
            this.doubleSided = false;
        }
        override public function toString():String{
            return (((("WireframeMaterial - color:" + this.lineColor) + " alpha:") + this.lineAlpha));
        }
        override public function drawTriangle(_arg1:Triangle3D, _arg2:Graphics, _arg3:RenderSessionData, _arg4:BitmapData=null, _arg5:Matrix=null):void{
            var _local6:Number = _arg1.v0.vertex3DInstance.x;
            var _local7:Number = _arg1.v0.vertex3DInstance.y;
            if (lineAlpha){
                _arg2.lineStyle(lineThickness, lineColor, lineAlpha);
                _arg2.moveTo(_local6, _local7);
                _arg2.lineTo(_arg1.v1.vertex3DInstance.x, _arg1.v1.vertex3DInstance.y);
                _arg2.lineTo(_arg1.v2.vertex3DInstance.x, _arg1.v2.vertex3DInstance.y);
                _arg2.lineTo(_local6, _local7);
                _arg2.lineStyle();
                _arg3.renderStatistics.triangles++;
            };
        }

    }
}//package org.papervision3d.materials 
