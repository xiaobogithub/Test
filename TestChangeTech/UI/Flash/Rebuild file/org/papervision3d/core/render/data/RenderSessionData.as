//Created by Action Script Viewer - http://www.buraks.com/asv
package org.papervision3d.core.render.data {
    import flash.display.*;
    import org.papervision3d.core.proto.*;
    import org.papervision3d.core.culling.*;
    import org.papervision3d.view.*;
    import org.papervision3d.core.render.*;

    public class RenderSessionData {

        public var container:Sprite
        public var renderer:IRenderEngine
        public var particleCuller:IParticleCuller
        public var viewPort:Viewport3D
        public var triangleCuller:ITriangleCuller
        public var scene:SceneObject3D
        public var camera:CameraObject3D
        public var renderStatistics:RenderStatistics
        public var sorted:Boolean
        public var renderObjects:Array
        public var renderLayers:Array

        public function RenderSessionData():void{
            this.renderStatistics = new RenderStatistics();
        }
        public function clone():RenderSessionData{
            var _local1:RenderSessionData = new RenderSessionData();
            _local1.triangleCuller = triangleCuller;
            _local1.particleCuller = particleCuller;
            _local1.viewPort = viewPort;
            _local1.container = container;
            _local1.scene = scene;
            _local1.camera = camera;
            _local1.renderer = renderer;
            _local1.renderStatistics = renderStatistics.clone();
            return (_local1);
        }
        public function destroy():void{
            triangleCuller = null;
            particleCuller = null;
            viewPort = null;
            container = null;
            scene = null;
            camera = null;
            renderer = null;
            renderStatistics = null;
            renderObjects = null;
            renderLayers = null;
        }

    }
}//package org.papervision3d.core.render.data 
