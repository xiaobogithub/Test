//Created by Action Script Viewer - http://www.buraks.com/asv
package org.papervision3d.core.math {
    import org.papervision3d.core.geom.renderables.*;

    public class AxisAlignedBoundingBox {

        public var minX:Number
        public var minY:Number
        public var minZ:Number
        public var maxX:Number
        public var maxY:Number
        public var maxZ:Number
        protected var _vertices:Array

        public function AxisAlignedBoundingBox(_arg1:Number, _arg2:Number, _arg3:Number, _arg4:Number, _arg5:Number, _arg6:Number){
            this.minX = _arg1;
            this.minY = _arg2;
            this.minZ = _arg3;
            this.maxX = _arg4;
            this.maxY = _arg5;
            this.maxZ = _arg6;
            createBoxVertices();
        }
        protected function createBoxVertices():void{
            _vertices = new Array();
            _vertices.push(new Vertex3D(minX, minY, minZ));
            _vertices.push(new Vertex3D(minX, minY, maxZ));
            _vertices.push(new Vertex3D(minX, maxY, minZ));
            _vertices.push(new Vertex3D(minX, maxY, maxZ));
            _vertices.push(new Vertex3D(maxX, minY, minZ));
            _vertices.push(new Vertex3D(maxX, minY, maxZ));
            _vertices.push(new Vertex3D(maxX, maxY, minZ));
            _vertices.push(new Vertex3D(maxX, maxY, maxZ));
        }
        public function getBoxVertices():Array{
            return (_vertices);
        }
        public function merge(_arg1:AxisAlignedBoundingBox):void{
            this.minX = Math.min(this.minX, _arg1.minX);
            this.minY = Math.min(this.minY, _arg1.minY);
            this.minZ = Math.min(this.minZ, _arg1.minZ);
            this.maxX = Math.max(this.maxX, _arg1.maxX);
            this.maxY = Math.max(this.maxY, _arg1.maxY);
            this.maxZ = Math.max(this.maxZ, _arg1.maxZ);
            createBoxVertices();
        }

        public static function createFromVertices(_arg1:Array):AxisAlignedBoundingBox{
            var _local8:Vertex3D;
            var _local2:Number = Number.MAX_VALUE;
            var _local3:Number = Number.MAX_VALUE;
            var _local4:Number = Number.MAX_VALUE;
            var _local5:Number = -(_local2);
            var _local6:Number = -(_local3);
            var _local7:Number = -(_local4);
            for each (_local8 in _arg1) {
                _local2 = Math.min(_local2, _local8.x);
                _local3 = Math.min(_local3, _local8.y);
                _local4 = Math.min(_local4, _local8.z);
                _local5 = Math.max(_local5, _local8.x);
                _local6 = Math.max(_local6, _local8.y);
                _local7 = Math.max(_local7, _local8.z);
            };
            return (new AxisAlignedBoundingBox(_local2, _local3, _local4, _local5, _local6, _local7));
        }

    }
}//package org.papervision3d.core.math 
