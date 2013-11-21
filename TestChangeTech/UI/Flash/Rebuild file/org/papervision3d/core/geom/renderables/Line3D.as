//Created by Action Script Viewer - http://www.buraks.com/asv
package org.papervision3d.core.geom.renderables {
    import org.papervision3d.core.render.command.*;
    import org.papervision3d.materials.special.*;
    import org.papervision3d.core.geom.*;

    public class Line3D extends AbstractRenderable implements IRenderable {

        public var size:Number
        public var material:LineMaterial
        public var cV:Vertex3D
        public var renderCommand:RenderLine
        public var v0:Vertex3D
        public var v1:Vertex3D

        public function Line3D(_arg1:Lines3D, _arg2:LineMaterial, _arg3:Number, _arg4:Vertex3D, _arg5:Vertex3D){
            this.size = _arg3;
            this.material = _arg2;
            this.v0 = _arg4;
            this.v1 = _arg5;
            this.cV = _arg5;
            this.instance = _arg1;
            this.renderCommand = new RenderLine(this);
        }
        public function addControlVertex(_arg1:Number, _arg2:Number, _arg3:Number):void{
            cV = new Vertex3D(_arg1, _arg2, _arg3);
            if (instance.geometry.vertices.indexOf(cV) == -1){
                instance.geometry.vertices.push(cV);
            };
        }
        override public function getRenderListItem():IRenderListItem{
            return (this.renderCommand);
        }

    }
}//package org.papervision3d.core.geom.renderables 
