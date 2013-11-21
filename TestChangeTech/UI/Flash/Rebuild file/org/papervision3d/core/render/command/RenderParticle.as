//Created by Action Script Viewer - http://www.buraks.com/asv
package org.papervision3d.core.render.command {
    import flash.display.*;
    import flash.geom.*;
    import org.papervision3d.core.render.data.*;
    import org.papervision3d.core.geom.renderables.*;
    import org.papervision3d.materials.special.*;

    public class RenderParticle extends RenderableListItem implements IRenderListItem {

        public var renderMat:ParticleMaterial
        public var particle:Particle

        public function RenderParticle(_arg1:Particle){
            this.particle = _arg1;
            this.renderableInstance = _arg1;
            this.renderable = Particle;
        }
        override public function render(_arg1:RenderSessionData, _arg2:Graphics):void{
            particle.material.drawParticle(particle, _arg2, _arg1);
        }
        override public function hitTestPoint2D(_arg1:Point, _arg2:RenderHitData):RenderHitData{
            renderMat = particle.material;
            if (renderMat.interactive){
                if (particle.renderRect.contains(_arg1.x, _arg1.y)){
                    _arg2.displayObject3D = particle.instance;
                    _arg2.material = renderMat;
                    _arg2.renderable = particle;
                    _arg2.hasHit = true;
                    _arg2.x = particle.x;
                    _arg2.y = particle.y;
                    _arg2.z = particle.z;
                    _arg2.u = 0;
                    _arg2.v = 0;
                    return (_arg2);
                };
            };
            return (_arg2);
        }

    }
}//package org.papervision3d.core.render.command 
