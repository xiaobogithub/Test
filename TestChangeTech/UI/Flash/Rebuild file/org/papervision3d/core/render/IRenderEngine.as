//Created by Action Script Viewer - http://www.buraks.com/asv
package org.papervision3d.core.render {
    import org.papervision3d.core.proto.*;
    import org.papervision3d.core.render.data.*;
    import org.papervision3d.core.render.command.*;
    import org.papervision3d.view.*;

    public interface IRenderEngine {

        function addToRenderList(_arg1:IRenderListItem):int;
        function removeFromRenderList(_arg1:IRenderListItem):int;
        function renderScene(_arg1:SceneObject3D, _arg2:CameraObject3D, _arg3:Viewport3D, _arg4:Boolean=true):RenderStatistics;

    }
}//package org.papervision3d.core.render 
