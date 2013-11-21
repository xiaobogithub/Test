//Created by Action Script Viewer - http://www.buraks.com/asv
package org.papervision3d.materials.shaders {
    import flash.display.*;
    import flash.geom.*;
    import flash.utils.*;
    import org.papervision3d.materials.*;
    import org.papervision3d.core.render.data.*;
    import org.papervision3d.objects.*;
    import org.papervision3d.core.geom.renderables.*;
    import org.papervision3d.core.log.*;
    import org.papervision3d.core.render.material.*;
    import org.papervision3d.core.render.shader.*;
    import org.papervision3d.core.render.draw.*;
    import org.papervision3d.core.material.*;

    public class ShadedMaterial extends TriangleMaterial implements ITriangleDrawer, IUpdateBeforeMaterial, IUpdateAfterMaterial {

        public var shader:Shader
        private var _shaderCompositeMode:int
        public var material:BitmapMaterial
        public var shaderObjectData:Dictionary

        private static var bmp:BitmapData;

        public function ShadedMaterial(_arg1:BitmapMaterial, _arg2:Shader, _arg3:int=0){
            this.shader = _arg2;
            this.material = _arg1;
            shaderCompositeMode = _arg3;
            init();
        }
        override public function registerObject(_arg1:DisplayObject3D):void{
            super.registerObject(_arg1);
            var _local2:ShaderObjectData = (shaderObjectData[_arg1] = new ShaderObjectData(_arg1, material, this));
            _local2.shaderRenderer.inputBitmap = material.bitmap;
            shader.setContainerForObject(_arg1, _local2.shaderRenderer.getLayerForShader(shader));
        }
        public function updateAfterRender(_arg1:RenderSessionData):void{
            var _local2:ShaderObjectData;
            for each (_local2 in shaderObjectData) {
                shader.updateAfterRender(_arg1, _local2);
                if (shaderCompositeMode == ShaderCompositeModes.PER_LAYER){
                    _local2.shaderRenderer.render(_arg1);
                };
            };
        }
        private function init():void{
            shaderObjectData = new Dictionary();
        }
        public function set shaderCompositeMode(_arg1:int):void{
            _shaderCompositeMode = _arg1;
        }
        public function get shaderCompositeMode():int{
            return (_shaderCompositeMode);
        }
        public function getOutputBitmapDataFor(_arg1:DisplayObject3D):BitmapData{
            var _local2:ShaderObjectData;
            if (shaderCompositeMode == ShaderCompositeModes.PER_LAYER){
                if (shaderObjectData[_arg1]){
                    _local2 = ShaderObjectData(shaderObjectData[_arg1]);
                    return (_local2.shaderRenderer.outputBitmap);
                };
                PaperLogger.warning("object not registered with shaded material");
            } else {
                PaperLogger.warning("getOutputBitmapDataFor only works on per layer mode");
            };
            return (null);
        }
        override public function destroy():void{
            var _local1:ShaderObjectData;
            super.destroy();
            for each (_local1 in shaderObjectData) {
                _local1.destroy();
            };
            material = null;
            shader = null;
        }
        override public function drawTriangle(_arg1:Triangle3D, _arg2:Graphics, _arg3:RenderSessionData, _arg4:BitmapData=null, _arg5:Matrix=null):void{
            var _local6:ShaderObjectData = ShaderObjectData(shaderObjectData[_arg1.instance]);
            if (shaderCompositeMode == ShaderCompositeModes.PER_LAYER){
                material.drawTriangle(_arg1, _arg2, _arg3, _local6.shaderRenderer.outputBitmap);
                shader.renderLayer(_arg1, _arg3, _local6);
            } else {
                if (shaderCompositeMode == ShaderCompositeModes.PER_TRIANGLE_IN_BITMAP){
                    bmp = _local6.getOutputBitmapFor(_arg1);
                    material.drawTriangle(_arg1, _arg2, _arg3, bmp, (_local6.triangleUVS[_arg1]) ? _local6.triangleUVS[_arg1] : _local6.getPerTriUVForDraw(_arg1));
                    shader.renderTri(_arg1, _arg3, _local6, bmp);
                };
            };
        }
        override public function unregisterObject(_arg1:DisplayObject3D):void{
            super.unregisterObject(_arg1);
            var _local2:ShaderObjectData = shaderObjectData[_arg1];
            _local2.destroy();
            delete shaderObjectData[_arg1];
        }
        public function updateBeforeRender(_arg1:RenderSessionData):void{
            var _local2:ShaderObjectData;
            var _local3:ILightShader;
            for each (_local2 in shaderObjectData) {
                _local2.shaderRenderer.inputBitmap = material.bitmap;
                if (shaderCompositeMode == ShaderCompositeModes.PER_LAYER){
                    if (_local2.shaderRenderer.resizedInput){
                        _local2.shaderRenderer.resizedInput = false;
                        _local2.uvMatrices = new Dictionary();
                    };
                    _local2.shaderRenderer.clear();
                };
                if ((shader is ILightShader)){
                    _local3 = (shader as ILightShader);
                    _local3.updateLightMatrix(_local2, _arg1);
                };
            };
        }

    }
}//package org.papervision3d.materials.shaders 
