//Created by Action Script Viewer - http://www.buraks.com/asv
package org.papervision3d.core.material {
    import flash.utils.*;
    import org.papervision3d.core.proto.*;
    import org.papervision3d.core.render.data.*;
    import org.papervision3d.objects.*;
    import org.papervision3d.core.math.*;
    import org.papervision3d.core.render.material.*;
    import org.papervision3d.materials.utils.*;
    import org.papervision3d.core.render.draw.*;

    public class AbstractLightShadeMaterial extends TriangleMaterial implements ITriangleDrawer, IUpdateBeforeMaterial {

        public var lightMatrices:Dictionary
        private var _light:LightObject3D

        protected static var lightMatrix:Matrix3D;

        public function AbstractLightShadeMaterial(){
            init();
        }
        public function updateBeforeRender(_arg1:RenderSessionData):void{
            var _local2:DisplayObject3D;
            for each (_local2 in objects) {
                lightMatrices[_local2] = LightMatrix.getLightMatrix(light, _local2, _arg1, lightMatrices[_local2]);
            };
        }
        protected function init():void{
            lightMatrices = new Dictionary();
        }
        public function get light():LightObject3D{
            return (_light);
        }
        public function set light(_arg1:LightObject3D):void{
            _light = _arg1;
        }

    }
}//package org.papervision3d.core.material 
