//Created by Action Script Viewer - http://www.buraks.com/asv
package org.papervision3d.core.geom {
    import flash.utils.*;
    import org.papervision3d.core.proto.*;
    import org.papervision3d.core.render.data.*;
    import org.papervision3d.objects.*;
    import org.papervision3d.core.render.command.*;
    import org.papervision3d.core.culling.*;
    import org.papervision3d.core.math.*;
    import org.papervision3d.core.geom.renderables.*;
    import org.papervision3d.core.render.draw.*;

    public class TriangleMesh3D extends Vertices3D {

        public function TriangleMesh3D(_arg1:MaterialObject3D, _arg2:Array, _arg3:Array, _arg4:String=null){
            super(_arg2, _arg4);
            this.geometry.faces = ((_arg3) || (new Array()));
            this.material = ((_arg1) || (MaterialObject3D.DEFAULT));
        }
        public function mergeVertices():void{
            var _local3:Vertex3D;
            var _local4:Triangle3D;
            var _local5:Vertex3D;
            var _local1:Dictionary = new Dictionary();
            var _local2:Array = new Array();
            for each (_local3 in this.geometry.vertices) {
                for each (_local5 in _local1) {
                    if ((((((_local3.x == _local5.x)) && ((_local3.y == _local5.y)))) && ((_local3.z == _local5.z)))){
                        _local1[_local3] = _local5;
                        break;
                    };
                };
                if (!_local1[_local3]){
                    _local1[_local3] = _local3;
                    _local2.push(_local3);
                };
            };
            this.geometry.vertices = _local2;
            for each (_local4 in geometry.faces) {
                _local4.v0 = _local1[_local4.v0];
                _local4.v1 = _local1[_local4.v1];
                _local4.v2 = _local1[_local4.v2];
            };
        }
        public function projectTexture(_arg1:String="x", _arg2:String="y"):void{
            var _local10:String;
            var _local11:Triangle3D;
            var _local12:Array;
            var _local13:Vertex3D;
            var _local14:Vertex3D;
            var _local15:Vertex3D;
            var _local16:NumberUV;
            var _local17:NumberUV;
            var _local18:NumberUV;
            var _local3:Array = this.geometry.faces;
            var _local4:Object = this.boundingBox();
            var _local5:Number = _local4.min[_arg1];
            var _local6:Number = _local4.size[_arg1];
            var _local7:Number = _local4.min[_arg2];
            var _local8:Number = _local4.size[_arg2];
            var _local9:MaterialObject3D = this.material;
            for (_local10 in _local3) {
                _local11 = _local3[Number(_local10)];
                _local12 = _local11.vertices;
                _local13 = _local12[0];
                _local14 = _local12[1];
                _local15 = _local12[2];
                _local16 = new NumberUV(((_local13[_arg1] - _local5) / _local6), ((_local13[_arg2] - _local7) / _local8));
                _local17 = new NumberUV(((_local14[_arg1] - _local5) / _local6), ((_local14[_arg2] - _local7) / _local8));
                _local18 = new NumberUV(((_local15[_arg1] - _local5) / _local6), ((_local15[_arg2] - _local7) / _local8));
                _local11.uv = [_local16, _local17, _local18];
            };
        }
        override public function project(_arg1:DisplayObject3D, _arg2:RenderSessionData):Number{
            var _local3:Array;
            var _local4:Number;
            var _local5:Number;
            var _local6:ITriangleCuller;
            var _local7:Vertex3DInstance;
            var _local8:Vertex3DInstance;
            var _local9:Vertex3DInstance;
            var _local10:Triangle3DInstance;
            var _local11:Triangle3D;
            var _local12:MaterialObject3D;
            var _local13:RenderTriangle;
            super.project(_arg1, _arg2);
            if (!this.culled){
                _local3 = this.geometry.faces;
                _local4 = 0;
                _local5 = 0;
                _local6 = _arg2.triangleCuller;
                for each (_local11 in _local3) {
                    _local12 = (_local11.material) ? _local11.material : material;
                    _local10 = _local11.face3DInstance;
                    _local7 = _local11.v0.vertex3DInstance;
                    _local8 = _local11.v1.vertex3DInstance;
                    _local9 = _local11.v2.vertex3DInstance;
                    if ((_local10.visible = _local6.testFace(_local11, _local7, _local8, _local9))){
                        _local4 = (_local4 + (_local10.screenZ = (((_local7.z + _local8.z) + _local9.z) / 3)));
                        _local13 = _local11.renderCommand;
                        _local5++;
                        _local13.renderer = (_local12 as ITriangleDrawer);
                        _local13.screenDepth = _local10.screenZ;
                        _arg2.renderer.addToRenderList(_local13);
                    } else {
                        _arg2.renderStatistics.culledTriangles++;
                    };
                };
                return ((this.screenZ = (_local4 / _local5)));
                //unresolved jump
            };
            _arg2.renderStatistics.culledObjects++;
            return (0);
        }
        public function quarterFaces():void{
            var _local4:Triangle3D;
            var _local6:Vertex3D;
            var _local7:Vertex3D;
            var _local8:Vertex3D;
            var _local9:Vertex3D;
            var _local10:Vertex3D;
            var _local11:Vertex3D;
            var _local12:NumberUV;
            var _local13:NumberUV;
            var _local14:NumberUV;
            var _local15:NumberUV;
            var _local16:NumberUV;
            var _local17:NumberUV;
            var _local18:Triangle3D;
            var _local19:Triangle3D;
            var _local20:Triangle3D;
            var _local21:Triangle3D;
            var _local1:Array = new Array();
            var _local2:Array = new Array();
            var _local3:Array = this.geometry.faces;
            var _local5:int = _local3.length;
            while ((_local4 = _local3[--_local5])) {
                _local6 = _local4.v0;
                _local7 = _local4.v1;
                _local8 = _local4.v2;
                _local9 = new Vertex3D(((_local6.x + _local7.x) / 2), ((_local6.y + _local7.y) / 2), ((_local6.z + _local7.z) / 2));
                _local10 = new Vertex3D(((_local7.x + _local8.x) / 2), ((_local7.y + _local8.y) / 2), ((_local7.z + _local8.z) / 2));
                _local11 = new Vertex3D(((_local8.x + _local6.x) / 2), ((_local8.y + _local6.y) / 2), ((_local8.z + _local6.z) / 2));
                this.geometry.vertices.push(_local9, _local10, _local11);
                _local12 = _local4.uv[0];
                _local13 = _local4.uv[1];
                _local14 = _local4.uv[2];
                _local15 = new NumberUV(((_local12.u + _local13.u) / 2), ((_local12.v + _local13.v) / 2));
                _local16 = new NumberUV(((_local13.u + _local14.u) / 2), ((_local13.v + _local14.v) / 2));
                _local17 = new NumberUV(((_local14.u + _local12.u) / 2), ((_local14.v + _local12.v) / 2));
                _local18 = new Triangle3D(this, [_local6, _local9, _local11], _local4.material, [_local12, _local15, _local17]);
                _local19 = new Triangle3D(this, [_local9, _local7, _local10], _local4.material, [_local15, _local13, _local16]);
                _local20 = new Triangle3D(this, [_local11, _local10, _local8], _local4.material, [_local17, _local16, _local14]);
                _local21 = new Triangle3D(this, [_local9, _local10, _local11], _local4.material, [_local15, _local16, _local17]);
                _local2.push(_local18, _local19, _local20, _local21);
            };
            this.geometry.faces = _local2;
            this.mergeVertices();
            this.geometry.ready = true;
        }
        override public function clone():DisplayObject3D{
            var _local1:DisplayObject3D = super.clone();
            var _local2:TriangleMesh3D = new TriangleMesh3D(this.material, [], [], _local1.name);
            if (this.materials){
                _local2.materials = this.materials.clone();
            };
            if (_local1.geometry){
                _local2.geometry = _local1.geometry.clone(_local2);
            };
            _local2.copyTransform(this.transform);
            return (_local2);
        }
        override public function set material(_arg1:MaterialObject3D):void{
            var _local2:Triangle3D;
            super.material = _arg1;
            for each (_local2 in geometry.faces) {
                _local2.material = _arg1;
            };
        }

    }
}//package org.papervision3d.core.geom 
