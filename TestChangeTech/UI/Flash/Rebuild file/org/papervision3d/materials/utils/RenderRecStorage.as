﻿//Created by Action Script Viewer - http://www.buraks.com/asv
package org.papervision3d.materials.utils {
    import flash.geom.*;
    import org.papervision3d.core.geom.renderables.*;

    public class RenderRecStorage {

        public var mat:Matrix
        public var v0:Vertex3DInstance
        public var v1:Vertex3DInstance
        public var v2:Vertex3DInstance

        public function RenderRecStorage(){
            v0 = new Vertex3DInstance();
            v1 = new Vertex3DInstance();
            v2 = new Vertex3DInstance();
            mat = new Matrix();
            super();
        }
    }
}//package org.papervision3d.materials.utils 
