//Created by Action Script Viewer - http://www.buraks.com/asv
package org.papervision3d.core.view {
    import org.papervision3d.core.render.data.*;

    public interface IViewport3D {

        function updateAfterRender(_arg1:RenderSessionData):void;
        function updateBeforeRender(_arg1:RenderSessionData):void;

    }
}//package org.papervision3d.core.view 
