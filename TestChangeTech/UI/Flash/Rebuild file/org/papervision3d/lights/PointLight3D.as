//Created by Action Script Viewer - http://www.buraks.com/asv
package org.papervision3d.lights {
    import org.papervision3d.core.proto.*;
    import org.papervision3d.core.math.*;

    public class PointLight3D extends LightObject3D {

        public static var DEFAULT_POS:Number3D = new Number3D(0, 0, -1000);

        public function PointLight3D(_arg1:Boolean=false, _arg2:Boolean=false){
            super(_arg1, _arg2);
            x = DEFAULT_POS.x;
            y = DEFAULT_POS.y;
            z = DEFAULT_POS.z;
        }
    }
}//package org.papervision3d.lights 
