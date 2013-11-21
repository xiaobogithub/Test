//Created by Action Script Viewer - http://www.buraks.com/asv
package org.papervision3d.core.render.draw {
    import flash.display.*;
    import flash.geom.*;
    import org.papervision3d.core.render.data.*;
    import org.papervision3d.core.geom.renderables.*;

    public interface ITriangleDrawer {

        function drawTriangle(_arg1:Triangle3D, _arg2:Graphics, _arg3:RenderSessionData, _arg4:BitmapData=null, _arg5:Matrix=null):void;

    }
}//package org.papervision3d.core.render.draw 
