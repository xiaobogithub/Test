//Created by Action Script Viewer - http://www.buraks.com/asv
package org.papervision3d.core.proto {
    import org.papervision3d.materials.*;
    import org.papervision3d.objects.*;
    import org.papervision3d.core.math.*;
    import org.papervision3d.objects.primitives.*;

    public class LightObject3D extends DisplayObject3D {

        public var flipped:Boolean
        public var lightMatrix:Matrix3D
        private var _showLight:Boolean
        private var displaySphere:Sphere

        public function LightObject3D(_arg1:Boolean=false, _arg2:Boolean=false){
            this.lightMatrix = Matrix3D.IDENTITY;
            this.showLight = _arg1;
            this.flipped = _arg2;
        }
        public function get showLight():Boolean{
            return (_showLight);
        }
        public function set showLight(_arg1:Boolean):void{
            if (_showLight){
                removeChild(displaySphere);
            };
            if (_arg1){
                displaySphere = new Sphere(new WireframeMaterial(0xFFFF00), 10, 3, 2);
                addChild(displaySphere);
            };
            _showLight = _arg1;
        }

    }
}//package org.papervision3d.core.proto 
