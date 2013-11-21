//Created by Action Script Viewer - http://www.buraks.com/asv
package org.papervision3d.core.render.draw {
    import flash.display.*;
    import org.papervision3d.core.render.data.*;
    import org.papervision3d.core.geom.renderables.*;

    public interface IParticleDrawer {

        function drawParticle(_arg1:Particle, _arg2:Graphics, _arg3:RenderSessionData):void;
        function updateRenderRect(_arg1:Particle):void;

    }
}//package org.papervision3d.core.render.draw 
