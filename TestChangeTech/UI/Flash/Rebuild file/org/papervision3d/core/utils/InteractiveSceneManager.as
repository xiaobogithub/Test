//Created by Action Script Viewer - http://www.buraks.com/asv
package org.papervision3d.core.utils {
    import flash.display.*;
    import flash.geom.*;
    import flash.events.*;
    import org.papervision3d.core.proto.*;
    import org.papervision3d.materials.*;
    import org.papervision3d.core.render.data.*;
    import org.papervision3d.objects.*;
    import org.papervision3d.view.*;
    import org.papervision3d.core.geom.renderables.*;
    import org.papervision3d.events.*;
    import org.papervision3d.core.utils.virtualmouse.*;

    public class InteractiveSceneManager extends EventDispatcher {

        public var currentMaterial:MaterialObject3D
        public var container:Sprite
        public var currentMousePos:Point
        public var debug:Boolean = false
        public var mouse3D:Mouse3D
        public var enableOverOut:Boolean = true
        public var currentDisplayObject3D:DisplayObject3D
        public var _viewportRendered:Boolean = false
        public var virtualMouse:VirtualMouse
        public var lastMousePos:Point
        public var viewport:Viewport3D
        public var renderHitData:RenderHitData
        public var currentMouseDO3D:DisplayObject3D = null

        public static var MOUSE_IS_DOWN:Boolean = false;

        public function InteractiveSceneManager(_arg1:Viewport3D){
            virtualMouse = new VirtualMouse();
            mouse3D = new Mouse3D();
            currentMousePos = new Point();
            lastMousePos = new Point();
            super();
            this.viewport = _arg1;
            this.container = _arg1.containerSprite;
            init();
        }
        protected function handleMouseClick(_arg1:MouseEvent):void{
            if ((_arg1 is IVirtualMouseEvent)){
                return;
            };
            if (((renderHitData) && (renderHitData.hasHit))){
                dispatchObjectEvent(InteractiveScene3DEvent.OBJECT_CLICK, currentDisplayObject3D);
            };
        }
        protected function handleEnterFrame(_arg1:Event):void{
            var _local3:MovieMaterial;
            currentMousePos.x = container.mouseX;
            currentMousePos.y = container.mouseY;
            var _local2 = !(currentMousePos.equals(lastMousePos));
            if (((_local2) || (_viewportRendered))){
                updateRenderHitData();
                _viewportRendered = false;
                if ((_arg1 is IVirtualMouseEvent)){
                    return;
                };
                if (((virtualMouse) && (renderHitData))){
                    _local3 = (currentMaterial as MovieMaterial);
                    if (_local3){
                        virtualMouse.container = (_local3.movie as Sprite);
                    };
                    if (virtualMouse.container){
                        virtualMouse.setLocation(renderHitData.u, renderHitData.v);
                    };
                    if (((((Mouse3D.enabled) && (renderHitData))) && (renderHitData.hasHit))){
                        mouse3D.updatePosition(renderHitData);
                    };
                    dispatchObjectEvent(InteractiveScene3DEvent.OBJECT_MOVE, currentDisplayObject3D);
                } else {
                    if (((renderHitData) && (renderHitData.hasHit))){
                        dispatchObjectEvent(InteractiveScene3DEvent.OBJECT_MOVE, currentDisplayObject3D);
                    };
                };
            };
            lastMousePos.x = currentMousePos.x;
            lastMousePos.y = currentMousePos.y;
        }
        public function updateAfterRender():void{
            _viewportRendered = true;
        }
        public function initListeners():void{
            if (viewport.interactive){
                container.addEventListener(MouseEvent.MOUSE_DOWN, handleMousePress, false, 0, true);
                container.addEventListener(MouseEvent.MOUSE_UP, handleMouseRelease, false, 0, true);
                container.addEventListener(MouseEvent.CLICK, handleMouseClick, false, 0, true);
                container.addEventListener(MouseEvent.DOUBLE_CLICK, handleMouseDoubleClick, false, 0, true);
                container.stage.addEventListener(Event.ENTER_FRAME, handleEnterFrame);
            };
        }
        protected function initVirtualMouse():void{
            virtualMouse.stage = container.stage;
            virtualMouse.container = container;
        }
        protected function handleMouseOver(_arg1:DisplayObject3D):void{
            dispatchObjectEvent(InteractiveScene3DEvent.OBJECT_OVER, _arg1);
        }
        protected function resolveRenderHitData():void{
            renderHitData = (viewport.hitTestPoint2D(currentMousePos) as RenderHitData);
        }
        public function updateRenderHitData():void{
            resolveRenderHitData();
            currentDisplayObject3D = renderHitData.displayObject3D;
            currentMaterial = renderHitData.material;
            manageOverOut();
        }
        protected function dispatchObjectEvent(_arg1:String, _arg2:DisplayObject3D):void{
            var _local3:Number;
            var _local4:Number;
            if (((renderHitData) && (renderHitData.hasHit))){
                _local3 = (renderHitData.u) ? renderHitData.u : 0;
                _local4 = (renderHitData.v) ? renderHitData.v : 0;
                dispatchEvent(new InteractiveScene3DEvent(_arg1, _arg2, container, (renderHitData.renderable as Triangle3D), _local3, _local4));
                _arg2.dispatchEvent(new InteractiveScene3DEvent(_arg1, _arg2, container, (renderHitData.renderable as Triangle3D), _local3, _local4));
            } else {
                dispatchEvent(new InteractiveScene3DEvent(_arg1, _arg2, container));
                if (_arg2){
                    _arg2.dispatchEvent(new InteractiveScene3DEvent(_arg1, _arg2, container));
                };
            };
        }
        protected function handleMouseDoubleClick(_arg1:MouseEvent):void{
            if ((_arg1 is IVirtualMouseEvent)){
                return;
            };
            if (((renderHitData) && (renderHitData.hasHit))){
                dispatchObjectEvent(InteractiveScene3DEvent.OBJECT_DOUBLE_CLICK, currentDisplayObject3D);
            };
        }
        protected function handleMouseRelease(_arg1:MouseEvent):void{
            if ((_arg1 is IVirtualMouseEvent)){
                return;
            };
            MOUSE_IS_DOWN = false;
            if (virtualMouse){
                virtualMouse.release();
            };
            if (((((Mouse3D.enabled) && (renderHitData))) && (!((renderHitData.renderable == null))))){
                mouse3D.updatePosition(renderHitData);
            };
            if (((renderHitData) && (renderHitData.hasHit))){
                dispatchObjectEvent(InteractiveScene3DEvent.OBJECT_RELEASE, currentDisplayObject3D);
            };
        }
        protected function handleAddedToStage(_arg1:Event):void{
            initVirtualMouse();
            initListeners();
        }
        protected function handleMouseOut(_arg1:DisplayObject3D):void{
            var _local2:MovieMaterial;
            if (_arg1){
                _local2 = (_arg1.material as MovieMaterial);
                if (_local2){
                    virtualMouse.exitContainer();
                };
            };
            dispatchObjectEvent(InteractiveScene3DEvent.OBJECT_OUT, _arg1);
        }
        protected function manageOverOut():void{
            if (!enableOverOut){
                return;
            };
            if (((renderHitData) && (renderHitData.hasHit))){
                if (((!(currentMouseDO3D)) && (currentDisplayObject3D))){
                    handleMouseOver(currentDisplayObject3D);
                    currentMouseDO3D = currentDisplayObject3D;
                } else {
                    if (((currentMouseDO3D) && (!((currentMouseDO3D == currentDisplayObject3D))))){
                        handleMouseOut(currentMouseDO3D);
                        handleMouseOver(currentDisplayObject3D);
                        currentMouseDO3D = currentDisplayObject3D;
                    };
                };
            } else {
                if (currentMouseDO3D != null){
                    handleMouseOut(currentMouseDO3D);
                    currentMouseDO3D = null;
                };
            };
        }
        public function destroy():void{
            viewport = null;
            renderHitData = null;
            currentDisplayObject3D = null;
            currentMaterial = null;
            currentMouseDO3D = null;
            container.removeEventListener(MouseEvent.MOUSE_DOWN, handleMousePress);
            container.removeEventListener(MouseEvent.MOUSE_UP, handleMouseRelease);
            container.removeEventListener(MouseEvent.CLICK, handleMouseClick);
            container.removeEventListener(MouseEvent.DOUBLE_CLICK, handleMouseDoubleClick);
            if (container.stage){
                container.stage.removeEventListener(Event.ENTER_FRAME, handleEnterFrame);
            };
            container = null;
        }
        public function init():void{
            if (container){
                if (container.stage){
                    initVirtualMouse();
                    initListeners();
                } else {
                    container.addEventListener(Event.ADDED_TO_STAGE, handleAddedToStage);
                };
            };
        }
        protected function handleMousePress(_arg1:MouseEvent):void{
            if ((_arg1 is IVirtualMouseEvent)){
                return;
            };
            MOUSE_IS_DOWN = true;
            if (virtualMouse){
                virtualMouse.press();
            };
            if (((((Mouse3D.enabled) && (renderHitData))) && (!((renderHitData.renderable == null))))){
                mouse3D.updatePosition(renderHitData);
            };
            if (((renderHitData) && (renderHitData.hasHit))){
                dispatchObjectEvent(InteractiveScene3DEvent.OBJECT_PRESS, currentDisplayObject3D);
            };
        }

    }
}//package org.papervision3d.core.utils 
