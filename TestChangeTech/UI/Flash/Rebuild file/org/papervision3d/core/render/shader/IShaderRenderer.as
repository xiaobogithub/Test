//Created by Action Script Viewer - http://www.buraks.com/asv
package org.papervision3d.core.render.shader {
    import flash.display.*;
    import org.papervision3d.core.render.data.*;
    import org.papervision3d.materials.shaders.*;

    public interface IShaderRenderer {

        function destroy():void;
        function getLayerForShader(_arg1:Shader):Sprite;
        function clear():void;
        function render(_arg1:RenderSessionData):void;

    }
}//package org.papervision3d.core.render.shader 
