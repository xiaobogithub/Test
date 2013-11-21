//Created by Action Script Viewer - http://www.buraks.com/asv
package org.papervision3d.view.layer {
    import org.papervision3d.objects.*;
    import org.papervision3d.view.*;

    public class ViewportBaseLayer extends ViewportLayer {

        public function ViewportBaseLayer(_arg1:Viewport3D){
            super(_arg1, null);
        }
        override public function getChildLayer(_arg1:DisplayObject3D, _arg2:Boolean=true, _arg3:Boolean=false):ViewportLayer{
            if (layers[_arg1]){
                return (layers[_arg1]);
            };
            if (((_arg2) || (_arg1.useOwnContainer))){
                return (getChildLayerFor(_arg1, _arg3));
            };
            return (this);
        }
        override public function updateBeforeRender():void{
            clear();
            var _local1:int = (childLayers.length - 1);
            while (_local1 >= 0) {
                if (childLayers[_local1].dynamicLayer){
                    removeLayerAt(_local1);
                };
                _local1--;
            };
            super.updateBeforeRender();
        }

    }
}//package org.papervision3d.view.layer 
