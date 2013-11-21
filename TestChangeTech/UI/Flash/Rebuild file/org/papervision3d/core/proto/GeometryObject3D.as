//Created by Action Script Viewer - http://www.buraks.com/asv
package org.papervision3d.core.proto {
    import flash.events.*;
    import flash.utils.*;
    import org.papervision3d.objects.*;
    import org.papervision3d.core.math.*;
    import org.papervision3d.core.geom.renderables.*;

    public class GeometryObject3D extends EventDispatcher {

        protected var _boundingSphereDirty:Boolean = true
        public var dirty:Boolean
        protected var _aabbDirty:Boolean = true
        public var _ready:Boolean = false
        protected var _boundingSphere:BoundingSphere
        public var faces:Array
        private var _numInstances:uint = 0
        public var vertices:Array
        protected var _aabb:AxisAlignedBoundingBox

        public function GeometryObject3D():void{
            dirty = true;
        }
        public function transformVertices(_arg1:Matrix3D):void{
            var _local15:Vertex3D;
            var _local16:Number;
            var _local17:Number;
            var _local18:Number;
            var _local19:Number;
            var _local20:Number;
            var _local21:Number;
            var _local2:Number = _arg1.n11;
            var _local3:Number = _arg1.n12;
            var _local4:Number = _arg1.n13;
            var _local5:Number = _arg1.n21;
            var _local6:Number = _arg1.n22;
            var _local7:Number = _arg1.n23;
            var _local8:Number = _arg1.n31;
            var _local9:Number = _arg1.n32;
            var _local10:Number = _arg1.n33;
            var _local11:Number = _arg1.n14;
            var _local12:Number = _arg1.n24;
            var _local13:Number = _arg1.n34;
            var _local14:int = vertices.length;
            while ((_local15 = vertices[--_local14])) {
                _local16 = _local15.x;
                _local17 = _local15.y;
                _local18 = _local15.z;
                _local19 = ((((_local16 * _local2) + (_local17 * _local3)) + (_local18 * _local4)) + _local11);
                _local20 = ((((_local16 * _local5) + (_local17 * _local6)) + (_local18 * _local7)) + _local12);
                _local21 = ((((_local16 * _local8) + (_local17 * _local9)) + (_local18 * _local10)) + _local13);
                _local15.x = _local19;
                _local15.y = _local20;
                _local15.z = _local21;
            };
        }
        public function set ready(_arg1:Boolean):void{
            if (_arg1){
                createVertexNormals();
                this.dirty = false;
            };
            _ready = _arg1;
        }
        public function flipFaces():void{
            var _local1:Triangle3D;
            var _local2:Vertex3D;
            for each (_local1 in this.faces) {
                _local2 = _local1.v0;
                _local1.v0 = _local1.v2;
                _local1.v2 = _local2;
                _local1.uv = [_local1.uv2, _local1.uv1, _local1.uv0];
                _local1.createNormal();
            };
            this.ready = true;
        }
        private function createVertexNormals():void{
            var _local2:Triangle3D;
            var _local3:Vertex3D;
            var _local1:Dictionary = new Dictionary(true);
            for each (_local2 in faces) {
                _local2.v0.connectedFaces[_local2] = _local2;
                _local2.v1.connectedFaces[_local2] = _local2;
                _local2.v2.connectedFaces[_local2] = _local2;
                _local1[_local2.v0] = _local2.v0;
                _local1[_local2.v1] = _local2.v1;
                _local1[_local2.v2] = _local2.v2;
            };
            for each (_local3 in _local1) {
                _local3.calculateNormal();
            };
        }
        public function transformUV(_arg1:MaterialObject3D):void{
            var _local2:String;
            if (_arg1.bitmap){
                for (_local2 in this.faces) {
                    faces[_local2].transformUV(_arg1);
                };
            };
        }
        public function get boundingSphere():BoundingSphere{
            if (_boundingSphereDirty){
                _boundingSphere = BoundingSphere.getFromVertices(vertices);
                _boundingSphereDirty = false;
            };
            return (_boundingSphere);
        }
        public function clone(_arg1:DisplayObject3D=null):GeometryObject3D{
            var _local5:int;
            var _local6:MaterialObject3D;
            var _local7:Vertex3D;
            var _local8:Triangle3D;
            var _local9:Vertex3D;
            var _local10:Vertex3D;
            var _local11:Vertex3D;
            var _local2:Dictionary = new Dictionary(true);
            var _local3:Dictionary = new Dictionary(true);
            var _local4:GeometryObject3D = new GeometryObject3D();
            _local4.vertices = new Array();
            _local4.faces = new Array();
            _local5 = 0;
            while (_local5 < this.vertices.length) {
                _local7 = this.vertices[_local5];
                _local3[_local7] = _local7.clone();
                _local4.vertices.push(_local3[_local7]);
                _local5++;
            };
            _local5 = 0;
            while (_local5 < this.faces.length) {
                _local8 = this.faces[_local5];
                _local9 = _local3[_local8.v0];
                _local10 = _local3[_local8.v1];
                _local11 = _local3[_local8.v2];
                _local4.faces.push(new Triangle3D(_arg1, [_local9, _local10, _local11], _local8.material, _local8.uv));
                _local2[_local8.material] = _local8.material;
                _local5++;
            };
            for each (_local6 in _local2) {
                _local6.registerObject(_arg1);
            };
            return (_local4);
        }
        public function get ready():Boolean{
            return (_ready);
        }
        public function get aabb():AxisAlignedBoundingBox{
            if (_aabbDirty){
                _aabb = AxisAlignedBoundingBox.createFromVertices(vertices);
                _aabbDirty = false;
            };
            return (_aabb);
        }

    }
}//package org.papervision3d.core.proto 
