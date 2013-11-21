//Created by Action Script Viewer - http://www.buraks.com/asv
package org.papervision3d.view {
    import flash.display.*;
    import flash.events.*;
    import org.papervision3d.core.proto.*;
    import org.papervision3d.core.view.*;
    import org.papervision3d.render.*;
    import org.papervision3d.scenes.*;

    public class AbstractView extends Sprite implements IView {

        public var renderer:BasicRenderEngine
        protected var _camera:CameraObject3D
        protected var _width:Number
        public var scene:Scene3D
        protected var _height:Number
        public var viewport:Viewport3D

        public function set viewportWidth(_arg1:Number):void{
            _width = _arg1;
            viewport.width = _arg1;
        }
        public function singleRender():void{
            onRenderTick();
        }
        public function startRendering():void{
            addEventListener(Event.ENTER_FRAME, onRenderTick);
            viewport.containerSprite.cacheAsBitmap = false;
        }
        public function get viewportWidth():Number{
            return (_width);
        }
        protected function onRenderTick(_arg1:Event=null):void{
            renderer.renderScene(scene, _camera, viewport);
        }
        public function set viewportHeight(_arg1:Number):void{
            _height = _arg1;
            viewport.height = _arg1;
        }
        public function get camera():CameraObject3D{
            return (_camera);
        }
        public function get viewportHeight():Number{
            return (_height);
        }
        public function stopRendering(_arg1:Boolean=false, _arg2:Boolean=false):void{
            removeEventListener(Event.ENTER_FRAME, onRenderTick);
            if (_arg1){
                onRenderTick();
            };
            if (_arg2){
                viewport.containerSprite.cacheAsBitmap = true;
            } else {
                viewport.containerSprite.cacheAsBitmap = false;
            };
        }

    }
}//package org.papervision3d.view 
