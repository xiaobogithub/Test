//Created by Action Script Viewer - http://www.buraks.com/asv
package org.papervision3d.materials.special {
    import flash.display.*;
    import flash.geom.*;
    import org.papervision3d.core.proto.*;
    import org.papervision3d.core.render.data.*;
    import org.papervision3d.core.geom.renderables.*;
    import org.papervision3d.core.log.*;
    import org.papervision3d.core.render.draw.*;

    public class ParticleMaterial extends MaterialObject3D implements IParticleDrawer {

        public var shape:int

        public static var SHAPE_SQUARE:int = 0;
        public static var SHAPE_CIRCLE:int = 1;

        public function ParticleMaterial(_arg1:Number, _arg2:Number, _arg3:int=0){
            this.shape = _arg3;
            this.fillAlpha = _arg2;
            this.fillColor = _arg1;
        }
        public function drawParticle(_arg1:Particle, _arg2:Graphics, _arg3:RenderSessionData):void{
            _arg2.beginFill(fillColor, fillAlpha);
            var _local4:Rectangle = _arg1.renderRect;
            if (shape == SHAPE_SQUARE){
                _arg2.drawRect(_local4.x, _local4.y, _local4.width, _local4.height);
            } else {
                if (shape == SHAPE_CIRCLE){
                    _arg2.drawCircle((_local4.x + (_local4.width / 2)), (_local4.y + (_local4.width / 2)), (_local4.width / 2));
                } else {
                    PaperLogger.warning("Particle material has no valid shape - Must be ParticleMaterial.SHAPE_SQUARE or ParticleMaterial.SHAPE_CIRCLE");
                };
            };
            _arg2.endFill();
            _arg3.renderStatistics.particles++;
        }
        public function updateRenderRect(_arg1:Particle):void{
            var _local2:Rectangle = _arg1.renderRect;
            if (_arg1.size == 0){
                _local2.width = 1;
                _local2.height = 1;
            } else {
                _local2.width = (_arg1.renderScale * _arg1.size);
                _local2.height = (_arg1.renderScale * _arg1.size);
            };
            _local2.x = (_arg1.vertex3D.vertex3DInstance.x - (_local2.width / 2));
            _local2.y = (_arg1.vertex3D.vertex3DInstance.y - (_local2.width / 2));
        }

    }
}//package org.papervision3d.materials.special 
