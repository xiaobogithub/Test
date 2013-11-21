//Created by Action Script Viewer - http://www.buraks.com/asv
package org.papervision3d.core.render.data {
    import org.papervision3d.core.proto.*;
    import org.papervision3d.objects.*;
    import org.papervision3d.core.geom.renderables.*;

    public class RenderHitData {

        public var y:Number
        public var z:Number
        public var endTime:int = 0
        public var startTime:int = 0
        public var displayObject3D:DisplayObject3D
        public var hasHit:Boolean = false
        public var material:MaterialObject3D
        public var renderable:IRenderable
        public var u:Number
        public var v:Number
        public var x:Number

        public function RenderHitData():void{
        }
        public function clear():void{
            startTime = 0;
            endTime = 0;
            hasHit = false;
            displayObject3D = null;
            material = null;
            renderable = null;
            u = 0;
            v = 0;
            x = 0;
            y = 0;
            z = 0;
        }
        public function clone():RenderHitData{
            var _local1:RenderHitData = new RenderHitData();
            _local1.startTime = startTime;
            _local1.endTime = endTime;
            _local1.hasHit = hasHit;
            _local1.displayObject3D = displayObject3D;
            _local1.material = material;
            _local1.renderable = renderable;
            _local1.u = u;
            _local1.v = v;
            _local1.x = x;
            _local1.y = y;
            _local1.z = z;
            return (_local1);
        }
        public function toString():String{
            return (((displayObject3D + " ") + renderable));
        }

    }
}//package org.papervision3d.core.render.data 
