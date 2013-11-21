//Created by Action Script Viewer - http://www.buraks.com/asv
package org.papervision3d.core.geom.renderables {
    import flash.display.*;
    import org.papervision3d.objects.*;
    import org.papervision3d.core.math.*;

    public class Triangle3DInstance {

        public var container:Sprite
        public var instance:DisplayObject3D
        public var visible:Boolean = false
        public var faceNormal:Number3D
        public var screenZ:Number

        public function Triangle3DInstance(_arg1:Triangle3D, _arg2:DisplayObject3D){
            this.instance = _arg2;
            faceNormal = new Number3D();
        }
    }
}//package org.papervision3d.core.geom.renderables 
