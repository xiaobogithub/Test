//Created by Action Script Viewer - http://www.buraks.com/asv
package org.papervision3d.materials.shaders {
    import flash.display.*;
    import flash.events.*;
    import flash.utils.*;
    import org.papervision3d.core.render.data.*;
    import org.papervision3d.objects.*;
    import org.papervision3d.core.geom.renderables.*;
    import flash.filters.*;
    import org.papervision3d.core.render.shader.*;

    public class Shader extends EventDispatcher implements IShader {

        protected var layers:Dictionary
        protected var _blendMode:String = "multiply"
        protected var _filter:BitmapFilter
        protected var _object:DisplayObject3D

        public function Shader(){
            this.layers = new Dictionary(true);
        }
        public function set layerBlendMode(_arg1:String):void{
            _blendMode = _arg1;
        }
        public function setContainerForObject(_arg1:DisplayObject3D, _arg2:Sprite):void{
            layers[_arg1] = _arg2;
        }
        public function updateAfterRender(_arg1:RenderSessionData, _arg2:ShaderObjectData):void{
        }
        public function set filter(_arg1:BitmapFilter):void{
            _filter = _arg1;
        }
        public function get layerBlendMode():String{
            return (_blendMode);
        }
        public function get filter():BitmapFilter{
            return (_filter);
        }
        public function destroy():void{
        }
        public function renderTri(_arg1:Triangle3D, _arg2:RenderSessionData, _arg3:ShaderObjectData, _arg4:BitmapData):void{
        }
        public function renderLayer(_arg1:Triangle3D, _arg2:RenderSessionData, _arg3:ShaderObjectData):void{
        }

    }
}//package org.papervision3d.materials.shaders 
