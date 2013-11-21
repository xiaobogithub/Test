//Created by Action Script Viewer - http://www.buraks.com/asv
package org.papervision3d.materials.special {
    import flash.display.*;
    import org.papervision3d.core.proto.*;
    import org.papervision3d.core.render.data.*;
    import org.papervision3d.core.geom.renderables.*;
    import org.papervision3d.core.render.draw.*;

    public class LineMaterial extends MaterialObject3D implements ILineDrawer {

        public function LineMaterial(_arg1:Number=0xFF0000, _arg2:Number=1){
            this.lineColor = _arg1;
            this.lineAlpha = _arg2;
        }
        public function drawLine(_arg1:Line3D, _arg2:Graphics, _arg3:RenderSessionData):void{
            _arg2.lineStyle(_arg1.size, lineColor, lineAlpha);
            _arg2.moveTo(_arg1.v0.vertex3DInstance.x, _arg1.v0.vertex3DInstance.y);
            if (_arg1.cV){
                _arg2.curveTo(_arg1.cV.vertex3DInstance.x, _arg1.cV.vertex3DInstance.y, _arg1.v1.vertex3DInstance.x, _arg1.v1.vertex3DInstance.y);
            } else {
                _arg2.lineTo(_arg1.v1.vertex3DInstance.x, _arg1.v1.vertex3DInstance.y);
            };
            _arg2.moveTo(0, 0);
            _arg2.lineStyle();
        }

    }
}//package org.papervision3d.materials.special 
