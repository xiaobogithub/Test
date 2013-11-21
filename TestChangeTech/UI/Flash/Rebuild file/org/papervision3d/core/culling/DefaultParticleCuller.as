//Created by Action Script Viewer - http://www.buraks.com/asv
package org.papervision3d.core.culling {
    import org.papervision3d.core.geom.renderables.*;

    public class DefaultParticleCuller implements IParticleCuller {

        public function testParticle(_arg1:Particle):Boolean{
            if (_arg1.material.invisible == false){
                if (_arg1.vertex3D.vertex3DInstance.visible == true){
                    return (true);
                };
            };
            return (false);
        }

    }
}//package org.papervision3d.core.culling 
