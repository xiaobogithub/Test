//Created by Action Script Viewer - http://www.buraks.com/asv
package org.papervision3d.core.geom.renderables {
    import flash.geom.*;
    import org.papervision3d.core.render.command.*;
    import org.papervision3d.materials.special.*;

    public class Particle extends AbstractRenderable implements IRenderable {

        public var size:Number
        public var material:ParticleMaterial
        public var renderScale:Number
        public var vertex3D:Vertex3D
        public var renderRect:Rectangle
        public var renderCommand:RenderParticle

        public function Particle(_arg1:ParticleMaterial, _arg2:Number=1, _arg3:Number=0, _arg4:Number=0, _arg5:Number=0){
            this.material = _arg1;
            this.size = _arg2;
            this.renderCommand = new RenderParticle(this);
            this.renderRect = new Rectangle();
            vertex3D = new Vertex3D(_arg3, _arg4, _arg5);
        }
        public function updateRenderRect():void{
            material.updateRenderRect(this);
        }
        public function get z():Number{
            return (vertex3D.z);
        }
        public function set z(_arg1:Number):void{
            vertex3D.z = _arg1;
        }
        public function set x(_arg1:Number):void{
            vertex3D.x = _arg1;
        }
        public function set y(_arg1:Number):void{
            vertex3D.y = _arg1;
        }
        public function get x():Number{
            return (vertex3D.x);
        }
        public function get y():Number{
            return (vertex3D.y);
        }
        override public function getRenderListItem():IRenderListItem{
            return (renderCommand);
        }

    }
}//package org.papervision3d.core.geom.renderables 
