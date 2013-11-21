//Created by Action Script Viewer - http://www.buraks.com/asv
package org.papervision3d.core.culling {
    import flash.geom.*;
    import org.papervision3d.core.geom.renderables.*;
    import org.papervision3d.core.math.util.*;

    public class RectangleParticleCuller implements IParticleCuller {

        public var cullingRectangle:Rectangle

        private static var vInstance:Vertex3DInstance;
        private static var testPoint:Point;

        public function RectangleParticleCuller(_arg1:Rectangle=null){
            this.cullingRectangle = _arg1;
            testPoint = new Point();
        }
        public function testParticle(_arg1:Particle):Boolean{
            vInstance = _arg1.vertex3D.vertex3DInstance;
            if (_arg1.material.invisible == false){
                if (vInstance.visible){
                    if (FastRectangleTools.intersects(_arg1.renderRect, cullingRectangle)){
                        return (true);
                    };
                };
            };
            return (false);
        }

    }
}//package org.papervision3d.core.culling 
