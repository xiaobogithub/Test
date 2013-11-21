//Created by Action Script Viewer - http://www.buraks.com/asv
package org.papervision3d.core.render.shader {
    import flash.display.*;
    import flash.geom.*;
    import flash.events.*;
    import flash.utils.*;
    import org.papervision3d.core.render.data.*;
    import org.papervision3d.materials.shaders.*;

    public class ShaderRenderer extends EventDispatcher implements IShaderRenderer {

        public var container:Sprite
        public var shadeLayers:Dictionary
        public var outputBitmap:BitmapData
        public var bitmapContainer:Bitmap
        public var resizedInput:Boolean = false
        public var bitmapLayer:Sprite
        private var _inputBitmapData:BitmapData

        public function ShaderRenderer(){
            container = new Sprite();
            bitmapLayer = new Sprite();
            bitmapContainer = new Bitmap();
            bitmapLayer.addChild(bitmapContainer);
            bitmapLayer.blendMode = BlendMode.NORMAL;
            shadeLayers = new Dictionary();
            container.addChild(bitmapLayer);
        }
        public function clear():void{
            var _local1:Sprite;
            for each (_local1 in shadeLayers) {
                if (((((inputBitmap) && ((inputBitmap.width > 0)))) && ((inputBitmap.height > 0)))){
                    _local1.graphics.clear();
                    _local1.graphics.beginFill(0, 1);
                    _local1.graphics.drawRect(0, 0, inputBitmap.width, inputBitmap.height);
                    _local1.graphics.endFill();
                };
            };
        }
        public function render(_arg1:RenderSessionData):void{
            if (outputBitmap){
                outputBitmap.fillRect(outputBitmap.rect, 0);
                bitmapContainer.bitmapData = inputBitmap;
                outputBitmap.draw(container, null, null, null, outputBitmap.rect, false);
                if (outputBitmap.transparent){
                    outputBitmap.copyChannel(inputBitmap, outputBitmap.rect, new Point(0, 0), BitmapDataChannel.ALPHA, BitmapDataChannel.ALPHA);
                };
            };
        }
        public function get inputBitmap():BitmapData{
            return (_inputBitmapData);
        }
        public function set inputBitmap(_arg1:BitmapData):void{
            if (_arg1 != null){
                if (_inputBitmapData != _arg1){
                    _inputBitmapData = _arg1;
                    if (outputBitmap){
                        if (((!((_inputBitmapData.width == outputBitmap.width))) || (!((_inputBitmapData.height == outputBitmap.height))))){
                            resizedInput = true;
                            outputBitmap.dispose();
                            outputBitmap = _inputBitmapData.clone();
                        };
                    } else {
                        resizedInput = true;
                        outputBitmap = _inputBitmapData.clone();
                    };
                };
            };
        }
        public function getLayerForShader(_arg1:Shader):Sprite{
            var _local2:Sprite = new Sprite();
            shadeLayers[_arg1] = _local2;
            var _local3:Sprite = new Sprite();
            _local2.addChild(_local3);
            if (inputBitmap != null){
                _local3.graphics.beginFill(0, 0);
                _local3.graphics.drawRect(0, 0, inputBitmap.width, inputBitmap.height);
                _local3.graphics.endFill();
            };
            container.addChild(_local2);
            _local2.blendMode = _arg1.layerBlendMode;
            return (_local2);
        }
        public function destroy():void{
            bitmapLayer = null;
            outputBitmap.dispose();
        }

    }
}//package org.papervision3d.core.render.shader 
