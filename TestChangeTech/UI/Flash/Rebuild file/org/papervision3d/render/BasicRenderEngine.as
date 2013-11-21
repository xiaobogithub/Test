//Created by Action Script Viewer - http://www.buraks.com/asv
package org.papervision3d.render {
    import flash.geom.*;
    import org.papervision3d.core.proto.*;
    import org.papervision3d.core.render.data.*;
    import org.papervision3d.view.layer.*;
    import org.papervision3d.core.render.command.*;
    import org.papervision3d.view.*;
    import org.papervision3d.core.render.material.*;
    import org.papervision3d.core.utils.*;
    import org.papervision3d.core.render.*;
    import org.papervision3d.events.*;
    import org.papervision3d.core.render.sort.*;
    import org.papervision3d.core.render.project.*;
    import org.papervision3d.core.render.filter.*;

    public class BasicRenderEngine extends AbstractRenderEngine implements IRenderEngine {

        protected var renderDoneEvent:RendererEvent
        public var sorter:IRenderSorter
        public var projectionPipeline:ProjectionPipeline
        protected var renderList:Array
        protected var renderStatistics:RenderStatistics
        protected var cleanRHD:RenderHitData
        protected var projectionDoneEvent:RendererEvent
        protected var renderSessionData:RenderSessionData
        protected var stopWatch:StopWatch
        public var filter:IRenderFilter

        public function BasicRenderEngine():void{
            cleanRHD = new RenderHitData();
            super();
            init();
        }
        protected function doRender(_arg1:RenderSessionData, _arg2:Array=null):RenderStatistics{
            var _local3:RenderableListItem;
            var _local5:ViewportLayer;
            stopWatch.reset();
            stopWatch.start();
            MaterialManager.getInstance().updateMaterialsBeforeRender(_arg1);
            filter.filter(renderList);
            sorter.sort(renderList);
            var _local4:Viewport3D = _arg1.viewPort;
            while ((_local3 = renderList.pop())) {
                _local5 = _local4.accessLayerFor(_local3, true);
                _local3.render(_arg1, _local5.graphicsChannel);
                _local4.lastRenderList.push(_local3);
                _local5.processRenderItem(_local3);
            };
            MaterialManager.getInstance().updateMaterialsAfterRender(_arg1);
            _arg1.renderStatistics.renderTime = stopWatch.stop();
            _arg1.viewPort.updateAfterRender(_arg1);
            return (renderStatistics);
        }
        override public function removeFromRenderList(_arg1:IRenderListItem):int{
            return (renderList.splice(renderList.indexOf(_arg1), 1));
        }
        protected function init():void{
            renderStatistics = new RenderStatistics();
            projectionPipeline = new BasicProjectionPipeline();
            stopWatch = new StopWatch();
            sorter = new BasicRenderSorter();
            filter = new BasicRenderFilter();
            renderList = new Array();
            renderSessionData = new RenderSessionData();
            renderSessionData.renderer = this;
            projectionDoneEvent = new RendererEvent(RendererEvent.PROJECTION_DONE, renderSessionData);
            renderDoneEvent = new RendererEvent(RendererEvent.RENDER_DONE, renderSessionData);
        }
        public function hitTestPoint2D(_arg1:Point, _arg2:Viewport3D):RenderHitData{
            return (_arg2.hitTestPoint2D(_arg1));
        }
        override public function renderScene(_arg1:SceneObject3D, _arg2:CameraObject3D, _arg3:Viewport3D, _arg4:Boolean=true):RenderStatistics{
            _arg2.viewport = _arg3.sizeRectangle;
            renderSessionData.scene = _arg1;
            renderSessionData.camera = _arg2;
            renderSessionData.viewPort = _arg3;
            renderSessionData.container = _arg3.containerSprite;
            renderSessionData.triangleCuller = _arg3.triangleCuller;
            renderSessionData.particleCuller = _arg3.particleCuller;
            renderSessionData.renderObjects = _arg1.objects;
            renderSessionData.renderLayers = null;
            renderSessionData.renderStatistics.clear();
            _arg3.updateBeforeRender(renderSessionData);
            projectionPipeline.project(renderSessionData);
            if (hasEventListener(RendererEvent.PROJECTION_DONE)){
                dispatchEvent(projectionDoneEvent);
            };
            doRender(renderSessionData, null);
            if (hasEventListener(RendererEvent.RENDER_DONE)){
                dispatchEvent(renderDoneEvent);
            };
            return (renderSessionData.renderStatistics);
        }
        override public function addToRenderList(_arg1:IRenderListItem):int{
            return (renderList.push(_arg1));
        }
        private function getLayerObjects(_arg1:Array):Array{
            var _local3:ViewportLayer;
            var _local2:Array = new Array();
            for each (_local3 in _arg1) {
                _local2 = _local2.concat(_local3.getLayerObjects());
            };
            return (_local2);
        }
        public function destroy():void{
            renderDoneEvent = null;
            projectionDoneEvent = null;
            projectionPipeline = null;
            sorter = null;
            filter = null;
            renderStatistics = null;
            renderList = null;
            renderSessionData.destroy();
            renderSessionData = null;
            cleanRHD = null;
            stopWatch = null;
        }
        public function renderLayers(_arg1:SceneObject3D, _arg2:CameraObject3D, _arg3:Viewport3D, _arg4:Array=null, _arg5:Boolean=true):RenderStatistics{
            renderSessionData.scene = _arg1;
            renderSessionData.camera = _arg2;
            renderSessionData.viewPort = _arg3;
            renderSessionData.container = _arg3.containerSprite;
            renderSessionData.triangleCuller = _arg3.triangleCuller;
            renderSessionData.particleCuller = _arg3.particleCuller;
            renderSessionData.renderObjects = getLayerObjects(_arg4);
            renderSessionData.renderLayers = _arg4;
            renderSessionData.renderStatistics.clear();
            _arg3.updateBeforeRender(renderSessionData);
            projectionPipeline.project(renderSessionData);
            if (hasEventListener(RendererEvent.PROJECTION_DONE)){
                dispatchEvent(projectionDoneEvent);
            };
            doRender(renderSessionData);
            if (hasEventListener(RendererEvent.RENDER_DONE)){
                dispatchEvent(renderDoneEvent);
            };
            return (renderSessionData.renderStatistics);
        }

    }
}//package org.papervision3d.render 
