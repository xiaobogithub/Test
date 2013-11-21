//Created by Action Script Viewer - http://www.buraks.com/asv
package org.papervision3d.view {
    import flash.display.*;
    import flash.geom.*;
    import flash.events.*;
    import flash.utils.*;
    import org.papervision3d.core.render.data.*;
    import org.papervision3d.view.layer.*;
    import org.papervision3d.objects.*;
    import org.papervision3d.core.render.command.*;
    import org.papervision3d.core.culling.*;
    import org.papervision3d.core.log.*;
    import org.papervision3d.core.view.*;
    import org.papervision3d.core.utils.*;
    import org.papervision3d.core.render.*;

    public class Viewport3D extends Sprite implements IViewport3D {

        public var interactiveSceneManager:InteractiveSceneManager
        public var lastRenderList:Array
        public var cullingRectangle:Rectangle
        protected var _interactive:Boolean
        private var stageScaleModeSet:Boolean = false
        protected var _autoCulling:Boolean
        protected var _viewportObjectFilter:ViewportObjectFilter
        public var particleCuller:IParticleCuller
        protected var _height:Number
        protected var _width:Number
        public var lineCuller:ILineCuller
        protected var _layerInstances:Dictionary
        protected var _autoScaleToStage:Boolean
        public var triangleCuller:ITriangleCuller
        protected var _lastRenderer:IRenderEngine
        protected var _hWidth:Number
        protected var _containerSprite:ViewportBaseLayer
        protected var _hHeight:Number
        public var sizeRectangle:Rectangle
        protected var renderHitData:RenderHitData
        protected var _autoClipping:Boolean

        public function Viewport3D(_arg1:Number=640, _arg2:Number=480, _arg3:Boolean=false, _arg4:Boolean=false, _arg5:Boolean=true, _arg6:Boolean=true){
            init();
            this.interactive = _arg4;
            this.viewportWidth = _arg1;
            this.viewportHeight = _arg2;
            this.autoClipping = _arg5;
            this.autoCulling = _arg6;
            this.autoScaleToStage = _arg3;
            this._layerInstances = new Dictionary(true);
        }
        public function set viewportWidth(_arg1:Number):void{
            _width = _arg1;
            _hWidth = (_arg1 / 2);
            containerSprite.x = _hWidth;
            cullingRectangle.x = -(_hWidth);
            cullingRectangle.width = _arg1;
            sizeRectangle.width = _arg1;
            if (_autoClipping){
                scrollRect = sizeRectangle;
            };
        }
        public function get autoCulling():Boolean{
            return (_autoCulling);
        }
        protected function onStageResize(_arg1:Event=null):void{
            if (_autoScaleToStage){
                viewportWidth = stage.stageWidth;
                viewportHeight = stage.stageHeight;
            };
        }
        public function set autoCulling(_arg1:Boolean):void{
            if (_arg1){
                triangleCuller = new RectangleTriangleCuller(cullingRectangle);
                particleCuller = new RectangleParticleCuller(cullingRectangle);
                lineCuller = new RectangleLineCuller(cullingRectangle);
            } else {
                if (!_arg1){
                    triangleCuller = new DefaultTriangleCuller();
                    particleCuller = new DefaultParticleCuller();
                    lineCuller = new DefaultLineCuller();
                };
            };
            _autoCulling = _arg1;
        }
        public function getChildLayer(_arg1:DisplayObject3D, _arg2:Boolean=true, _arg3:Boolean=true):ViewportLayer{
            return (containerSprite.getChildLayer(_arg1, _arg2, _arg3));
        }
        protected function init():void{
            this.renderHitData = new RenderHitData();
            lastRenderList = new Array();
            sizeRectangle = new Rectangle();
            cullingRectangle = new Rectangle();
            _containerSprite = new ViewportBaseLayer(this);
            _containerSprite.doubleClickEnabled = true;
            addChild(_containerSprite);
            addEventListener(Event.ADDED_TO_STAGE, onAddedToStage);
            addEventListener(Event.REMOVED_FROM_STAGE, onRemovedFromStage);
        }
        public function get autoClipping():Boolean{
            return (_autoClipping);
        }
        public function updateAfterRender(_arg1:RenderSessionData):void{
            var _local2:ViewportLayer;
            if (interactive){
                interactiveSceneManager.updateAfterRender();
            };
            if (_arg1.renderLayers){
                for each (_local2 in _arg1.renderLayers) {
                    _local2.updateInfo();
                    _local2.sortChildLayers();
                    _local2.updateAfterRender();
                };
            } else {
                containerSprite.updateInfo();
                containerSprite.updateAfterRender();
            };
            containerSprite.sortChildLayers();
        }
        protected function onAddedToStage(_arg1:Event):void{
            if (_autoScaleToStage){
                setStageScaleMode();
            };
            stage.addEventListener(Event.RESIZE, onStageResize);
            onStageResize();
        }
        public function get containerSprite():ViewportLayer{
            return (_containerSprite);
        }
        public function set autoClipping(_arg1:Boolean):void{
            if (_arg1){
                scrollRect = sizeRectangle;
            } else {
                scrollRect = null;
            };
            _autoClipping = _arg1;
        }
        protected function setStageScaleMode():void{
            if (!stageScaleModeSet){
                PaperLogger.info("Viewport autoScaleToStage : Papervision has changed the Stage scale mode.");
                stage.align = StageAlign.TOP_LEFT;
                stage.scaleMode = StageScaleMode.NO_SCALE;
                stageScaleModeSet = true;
            };
        }
        public function accessLayerFor(_arg1:RenderableListItem, _arg2:Boolean=false):ViewportLayer{
            var _local3:DisplayObject3D;
            if (_arg1.renderableInstance){
                _local3 = _arg1.renderableInstance.instance;
                _local3 = (_local3.parentContainer) ? _local3.parentContainer : _local3;
                if (containerSprite.layers[_local3]){
                    if (_arg2){
                        _local3.container = containerSprite.layers[_local3];
                    };
                    return (containerSprite.layers[_local3]);
                } else {
                    if (_local3.useOwnContainer){
                        return (containerSprite.getChildLayer(_local3, true, true));
                    };
                };
            };
            return (containerSprite);
        }
        public function get viewportWidth():Number{
            return (_width);
        }
        public function set interactive(_arg1:Boolean):void{
            if (_arg1 != _interactive){
                if (((_interactive) && (interactiveSceneManager))){
                    interactiveSceneManager.destroy();
                    interactiveSceneManager = null;
                };
                _interactive = _arg1;
                if (_arg1){
                    interactiveSceneManager = new InteractiveSceneManager(this);
                };
            };
        }
        public function set viewportObjectFilter(_arg1:ViewportObjectFilter):void{
            _viewportObjectFilter = _arg1;
        }
        public function set autoScaleToStage(_arg1:Boolean):void{
            _autoScaleToStage = _arg1;
            if (((_arg1) && (!((stage == null))))){
                setStageScaleMode();
                onStageResize();
            };
        }
        public function set viewportHeight(_arg1:Number):void{
            _height = _arg1;
            _hHeight = (_arg1 / 2);
            containerSprite.y = _hHeight;
            cullingRectangle.y = -(_hHeight);
            cullingRectangle.height = _arg1;
            sizeRectangle.height = _arg1;
            if (_autoClipping){
                scrollRect = sizeRectangle;
            };
        }
        public function updateBeforeRender(_arg1:RenderSessionData):void{
            var _local2:ViewportLayer;
            lastRenderList.length = 0;
            if (_arg1.renderLayers){
                for each (_local2 in _arg1.renderLayers) {
                    _local2.updateBeforeRender();
                };
            } else {
                _containerSprite.updateBeforeRender();
            };
            _layerInstances = new Dictionary(true);
        }
        public function hitTestMouse():RenderHitData{
            var _local1:Point = new Point(containerSprite.mouseX, containerSprite.mouseY);
            return (hitTestPoint2D(_local1));
        }
        public function get interactive():Boolean{
            return (_interactive);
        }
        public function get autoScaleToStage():Boolean{
            return (_autoScaleToStage);
        }
        public function hitTestPoint2D(_arg1:Point):RenderHitData{
            var _local2:RenderableListItem;
            var _local3:RenderHitData;
            var _local4:IRenderListItem;
            var _local5:uint;
            renderHitData.clear();
            if (interactive){
                _local3 = renderHitData;
                _local5 = lastRenderList.length;
                while ((_local4 = lastRenderList[--_local5])) {
                    if ((_local4 is RenderableListItem)){
                        _local2 = (_local4 as RenderableListItem);
                        _local3 = _local2.hitTestPoint2D(_arg1, _local3);
                        if (_local3.hasHit){
                            return (_local3);
                        };
                    };
                };
            };
            return (renderHitData);
        }
        protected function onRemovedFromStage(_arg1:Event):void{
            stage.removeEventListener(Event.RESIZE, onStageResize);
        }
        public function get viewportHeight():Number{
            return (_height);
        }
        public function destroy():void{
            if (interactiveSceneManager){
                interactiveSceneManager.destroy();
                interactiveSceneManager = null;
            };
            lastRenderList = null;
        }
        public function get viewportObjectFilter():ViewportObjectFilter{
            return (_viewportObjectFilter);
        }

    }
}//package org.papervision3d.view 
