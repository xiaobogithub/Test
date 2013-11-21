//Created by Action Script Viewer - http://www.buraks.com/asv
package org.papervision3d.core.math {
    import org.papervision3d.core.geom.renderables.*;

    public class BoundingSphere {

        public var maxDistance:Number
        public var radius:Number

        public function BoundingSphere(_arg1:Number){
            this.maxDistance = _arg1;
            this.radius = Math.sqrt(_arg1);
        }
        public static function getFromVertices(_arg1:Array):BoundingSphere{
            var _local3:Number;
            var _local4:Vertex3D;
            var _local2:Number = 0;
            for each (_local4 in _arg1) {
                _local3 = (((_local4.x * _local4.x) + (_local4.y * _local4.y)) + (_local4.z * _local4.z));
                _local2 = ((_local3)>_local2) ? _local3 : _local2;
            };
            return (new BoundingSphere(_local2));
        }

    }
}//package org.papervision3d.core.math 
