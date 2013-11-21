//Created by Action Script Viewer - http://www.buraks.com/asv
package org.papervision3d.core.render.command {
    import flash.geom.*;
    import org.papervision3d.core.render.data.*;
    import org.papervision3d.objects.*;
    import org.papervision3d.core.geom.renderables.*;

    public class RenderableListItem extends AbstractRenderListItem {

        public var renderable:Class
        public var instance:DisplayObject3D
        public var renderableInstance:AbstractRenderable

        public function hitTestPoint2D(_arg1:Point, _arg2:RenderHitData):RenderHitData{
            return (_arg2);
        }

    }
}//package org.papervision3d.core.render.command 
