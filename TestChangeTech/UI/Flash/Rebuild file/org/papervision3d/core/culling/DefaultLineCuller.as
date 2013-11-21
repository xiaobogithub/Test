//Created by Action Script Viewer - http://www.buraks.com/asv
package org.papervision3d.core.culling {
    import org.papervision3d.core.geom.renderables.*;

    public class DefaultLineCuller implements ILineCuller {

        public function testLine(_arg1:Line3D):Boolean{
            return (((_arg1.v0.vertex3DInstance.visible) && (_arg1.v1.vertex3DInstance.visible)));
        }

    }
}//package org.papervision3d.core.culling 
