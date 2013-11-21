//Created by Action Script Viewer - http://www.buraks.com/asv
package org.papervision3d.core.render {
    import flash.events.*;
    import org.papervision3d.core.proto.*;
    import org.papervision3d.core.render.data.*;
    import org.papervision3d.core.render.command.*;
    import org.papervision3d.view.*;

    public class AbstractRenderEngine extends EventDispatcher implements IRenderEngine {

        public function AbstractRenderEngine(_arg1:IEventDispatcher=null){
            super(_arg1);
        }
        public function addToRenderList(_arg1:IRenderListItem):int{
            return (0);
        }
        public function removeFromRenderList(_arg1:IRenderListItem):int{
            return (0);
        }
        public function renderScene(_arg1:SceneObject3D, _arg2:CameraObject3D, _arg3:Viewport3D, _arg4:Boolean=true):RenderStatistics{
            return (null);
        }

    }
}//package org.papervision3d.core.render 
