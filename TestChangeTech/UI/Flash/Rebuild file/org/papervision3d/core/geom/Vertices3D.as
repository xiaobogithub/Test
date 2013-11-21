//Created by Action Script Viewer - http://www.buraks.com/asv
package org.papervision3d.core.geom {
    import org.papervision3d.core.proto.*;
    import org.papervision3d.core.render.data.*;
    import org.papervision3d.objects.*;
    import org.papervision3d.core.culling.*;
    import org.papervision3d.core.math.*;
    import org.papervision3d.core.geom.renderables.*;

    public class Vertices3D extends DisplayObject3D {

        public function Vertices3D(_arg1:Array, _arg2:String=null){
            super(_arg2, new GeometryObject3D());
            this.geometry.vertices = ((_arg1) || (new Array()));
        }
        public function projectFrustum(_arg1:DisplayObject3D, _arg2:RenderSessionData):Number{
            return (0);
        }
        override public function project(_arg1:DisplayObject3D, _arg2:RenderSessionData):Number{
            super.project(_arg1, _arg2);
            if (this.culled){
                return (0);
            };
            if ((_arg2.camera is IObjectCuller)){
                return (projectFrustum(_arg1, _arg2));
            };
            return (_arg2.camera.projectVertices(this, _arg2));
        }
        public function transformVertices(_arg1:Matrix3D):void{
            geometry.transformVertices(_arg1);
        }
        override public function clone():DisplayObject3D{
            var _local1:DisplayObject3D = super.clone();
            var _local2:Vertices3D = new Vertices3D(null, _local1.name);
            _local2.material = _local1.material;
            if (_local1.materials){
                _local2.materials = _local1.materials.clone();
            };
            if (this.geometry){
                _local2.geometry = this.geometry.clone(_local2);
            };
            _local2.copyTransform(this.transform);
            return (_local2);
        }
        public function boundingBox():Object{
            var _local3:Vertex3D;
            var _local1:Array = this.geometry.vertices;
            var _local2:Object = new Object();
            _local2.min = new Number3D(Number.MAX_VALUE, Number.MAX_VALUE, Number.MAX_VALUE);
            _local2.max = new Number3D(-(Number.MAX_VALUE), -(Number.MAX_VALUE), -(Number.MAX_VALUE));
            _local2.size = new Number3D();
            for each (_local3 in _local1) {
                _local2.min.x = Math.min(_local3.x, _local2.min.x);
                _local2.min.y = Math.min(_local3.y, _local2.min.y);
                _local2.min.z = Math.min(_local3.z, _local2.min.z);
                _local2.max.x = Math.max(_local3.x, _local2.max.x);
                _local2.max.y = Math.max(_local3.y, _local2.max.y);
                _local2.max.z = Math.max(_local3.z, _local2.max.z);
            };
            _local2.size.x = (_local2.max.x - _local2.min.x);
            _local2.size.y = (_local2.max.y - _local2.min.y);
            _local2.size.z = (_local2.max.z - _local2.min.z);
            return (_local2);
        }

    }
}//package org.papervision3d.core.geom 
