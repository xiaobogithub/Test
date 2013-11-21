//Created by Action Script Viewer - http://www.buraks.com/asv
package org.papervision3d.materials.shaders {
    import flash.display.*;
    import org.papervision3d.core.render.data.*;
    import org.papervision3d.core.geom.renderables.*;
    import org.papervision3d.core.render.shader.*;

    public interface IShader {

        function updateAfterRender(_arg1:RenderSessionData, _arg2:ShaderObjectData):void;
        function destroy():void;
        function renderLayer(_arg1:Triangle3D, _arg2:RenderSessionData, _arg3:ShaderObjectData):void;
        function renderTri(_arg1:Triangle3D, _arg2:RenderSessionData, _arg3:ShaderObjectData, _arg4:BitmapData):void;

    }
}//package org.papervision3d.materials.shaders 
