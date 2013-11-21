//Created by Action Script Viewer - http://www.buraks.com/asv
package org.papervision3d.core.render.data {

    public class RenderStatistics {

        public var renderTime:int = 0
        public var culledObjects:int = 0
        public var shadedTriangles:int = 0
        public var culledParticles:int = 0
        public var culledTriangles:int = 0
        public var triangles:int = 0
        public var particles:int = 0
        public var rendered:int = 0
        public var projectionTime:int = 0
        public var filteredObjects:int = 0
        public var lines:int = 0

        public function clear():void{
            projectionTime = 0;
            renderTime = 0;
            rendered = 0;
            particles = 0;
            triangles = 0;
            culledTriangles = 0;
            culledParticles = 0;
            lines = 0;
            shadedTriangles = 0;
            filteredObjects = 0;
            culledObjects = 0;
        }
        public function clone():RenderStatistics{
            var _local1:RenderStatistics = new RenderStatistics();
            _local1.projectionTime = projectionTime;
            _local1.renderTime = renderTime;
            _local1.rendered = rendered;
            _local1.particles = particles;
            _local1.triangles = triangles;
            _local1.culledTriangles = culledTriangles;
            _local1.lines = lines;
            _local1.shadedTriangles = shadedTriangles;
            _local1.filteredObjects = filteredObjects;
            _local1.culledObjects = culledObjects;
            return (_local1);
        }
        public function toString():String{
            return (new String((((((((((((((((((("ProjectionTime:" + projectionTime) + " RenderTime:") + renderTime) + " Particles:") + particles) + " CulledParticles :") + culledParticles) + " Triangles:") + triangles) + " ShadedTriangles :") + shadedTriangles) + " CulledTriangles:") + culledTriangles) + " FilteredObjects:") + filteredObjects) + " CulledObjects:") + culledObjects) + "")));
        }

    }
}//package org.papervision3d.core.render.data 
