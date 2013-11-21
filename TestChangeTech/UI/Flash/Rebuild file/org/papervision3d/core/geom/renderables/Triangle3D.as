//Created by Action Script Viewer - http://www.buraks.com/asv
package org.papervision3d.core.geom.renderables {
    import org.papervision3d.core.proto.*;
    import org.papervision3d.objects.*;
    import org.papervision3d.core.render.command.*;
    import org.papervision3d.core.math.*;

    public class Triangle3D extends AbstractRenderable implements IRenderable {

        public var _uvArray:Array
        public var face3DInstance:Triangle3DInstance
        public var renderCommand:RenderTriangle
        public var id:Number
        public var material:MaterialObject3D
        public var faceNormal:Number3D
        public var screenZ:Number
        public var uv0:NumberUV
        public var uv1:NumberUV
        public var _materialName:String
        public var visible:Boolean
        public var uv2:NumberUV
        public var vertices:Array
        public var v0:Vertex3D
        public var v1:Vertex3D
        public var v2:Vertex3D

        private static var _totalFaces:Number = 0;

        public function Triangle3D(_arg1:DisplayObject3D, _arg2:Array, _arg3:MaterialObject3D=null, _arg4:Array=null){
            this.instance = _arg1;
            this.renderCommand = new RenderTriangle(this);
            face3DInstance = new Triangle3DInstance(this, _arg1);
            faceNormal = new Number3D();
            if (((_arg2) && ((_arg2.length == 3)))){
                this.vertices = _arg2;
                v0 = _arg2[0];
                v1 = _arg2[1];
                v2 = _arg2[2];
                createNormal();
            } else {
                _arg2 = new Array();
                v0 = (_arg2[0] = new Vertex3D());
                v1 = (_arg2[1] = new Vertex3D());
                v2 = (_arg2[2] = new Vertex3D());
            };
            this.material = _arg3;
            this.uv = _arg4;
            this.id = _totalFaces++;
        }
        public function set uv(_arg1:Array):void{
            if (((_arg1) && ((_arg1.length == 3)))){
                uv0 = NumberUV(_arg1[0]);
                uv1 = NumberUV(_arg1[1]);
                uv2 = NumberUV(_arg1[2]);
            };
            _uvArray = _arg1;
        }
        override public function getRenderListItem():IRenderListItem{
            return (renderCommand);
        }
        public function updateVertices():void{
            v0 = vertices[0];
            v1 = vertices[1];
            v2 = vertices[2];
        }
        public function createNormal():void{
            var _local1:Number3D = v0.getPosition();
            var _local2:Number3D = v1.getPosition();
            var _local3:Number3D = v2.getPosition();
            _local2.minusEq(_local1);
            _local3.minusEq(_local1);
            faceNormal = Number3D.cross(_local2, _local3, faceNormal);
            faceNormal.normalize();
        }
        public function get uv():Array{
            return (_uvArray);
        }

    }
}//package org.papervision3d.core.geom.renderables 
