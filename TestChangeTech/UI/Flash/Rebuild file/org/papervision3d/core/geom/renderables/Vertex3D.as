//Created by Action Script Viewer - http://www.buraks.com/asv
package org.papervision3d.core.geom.renderables {
    import flash.utils.*;
    import org.papervision3d.core.render.command.*;
    import org.papervision3d.core.math.*;

    public class Vertex3D extends AbstractRenderable implements IRenderable {

        public var normal:Number3D
        protected var position:Number3D
        public var vertex3DInstance:Vertex3DInstance
        public var connectedFaces:Dictionary
        public var extra:Object
        public var x:Number
        public var y:Number
        public var z:Number

        public function Vertex3D(_arg1:Number=0, _arg2:Number=0, _arg3:Number=0){
            position = new Number3D();
            super();
            this.x = (position.x = _arg1);
            this.y = (position.y = _arg2);
            this.z = (position.z = _arg3);
            this.vertex3DInstance = new Vertex3DInstance();
            this.normal = new Number3D();
            this.connectedFaces = new Dictionary();
        }
        override public function getRenderListItem():IRenderListItem{
            return (null);
        }
        public function getPosition():Number3D{
            position.x = x;
            position.y = y;
            position.z = z;
            return (position);
        }
        public function toNumber3D():Number3D{
            return (new Number3D(x, y, z));
        }
        public function calculateNormal():void{
            var _local1:Triangle3D;
            var _local3:Number3D;
            var _local2:Number = 0;
            normal.reset();
            for each (_local1 in connectedFaces) {
                if (_local1.faceNormal){
                    _local2++;
                    normal.plusEq(_local1.faceNormal);
                };
            };
            _local3 = getPosition();
            _local3.x = (_local3.x / _local2);
            _local3.y = (_local3.y / _local2);
            _local3.z = (_local3.z / _local2);
            _local3.normalize();
            normal.plusEq(_local3);
            normal.normalize();
        }
        public function clone():Vertex3D{
            var _local1:Vertex3D = new Vertex3D(x, y, z);
            _local1.extra = extra;
            _local1.vertex3DInstance = vertex3DInstance.clone();
            _local1.normal = normal.clone();
            return (_local1);
        }

    }
}//package org.papervision3d.core.geom.renderables 
