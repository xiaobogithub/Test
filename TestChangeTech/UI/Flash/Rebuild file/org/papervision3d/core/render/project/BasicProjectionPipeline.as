//Created by Action Script Viewer - http://www.buraks.com/asv
package org.papervision3d.core.render.project {
    import org.papervision3d.core.render.data.*;
    import org.papervision3d.objects.*;

    public class BasicProjectionPipeline extends ProjectionPipeline {

        public function BasicProjectionPipeline(){
            init();
        }
        protected function init():void{
        }
        override public function project(_arg1:RenderSessionData):void{
            var _local3:DisplayObject3D;
            _arg1.camera.transformView();
            var _local2:Array = _arg1.renderObjects;
            var _local4:Number = _local2.length;
            if (_arg1.camera.useProjectionMatrix){
                for each (_local3 in _local2) {
                    if (_local3.visible){
                        if (_arg1.viewPort.viewportObjectFilter){
                            if (_arg1.viewPort.viewportObjectFilter.testObject(_local3)){
                                projectObject(_local3, _arg1);
                            } else {
                                _arg1.renderStatistics.filteredObjects++;
                            };
                        } else {
                            projectObject(_local3, _arg1);
                        };
                    };
                };
            } else {
                for each (_local3 in _local2) {
                    if (_local3.visible){
                        if (_arg1.viewPort.viewportObjectFilter){
                            if (_arg1.viewPort.viewportObjectFilter.testObject(_local3)){
                                projectObject(_local3, _arg1);
                            } else {
                                _arg1.renderStatistics.filteredObjects++;
                            };
                        } else {
                            projectObject(_local3, _arg1);
                        };
                    };
                };
            };
        }
        protected function projectObject(_arg1:DisplayObject3D, _arg2:RenderSessionData):void{
            _arg1.project(_arg2.camera, _arg2);
        }

    }
}//package org.papervision3d.core.render.project 
