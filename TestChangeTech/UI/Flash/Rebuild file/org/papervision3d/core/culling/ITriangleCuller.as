//Created by Action Script Viewer - http://www.buraks.com/asv
package org.papervision3d.core.culling {
    import org.papervision3d.core.geom.renderables.*;

    public interface ITriangleCuller {

        function testFace(_arg1:Triangle3D, _arg2:Vertex3DInstance, _arg3:Vertex3DInstance, _arg4:Vertex3DInstance):Boolean;

    }
}//package org.papervision3d.core.culling 
