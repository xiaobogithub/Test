//Created by Action Script Viewer - http://www.buraks.com/asv
package org.papervision3d.core.render.material {
    import flash.utils.*;
    import org.papervision3d.core.proto.*;
    import org.papervision3d.core.render.data.*;

    public class MaterialManager {

        private var materials:Dictionary

        private static var instance:MaterialManager;

        public function MaterialManager():void{
            if (instance){
                throw (new Error("Only 1 instance of materialmanager allowed"));
            };
            init();
        }
        private function init():void{
            materials = new Dictionary(true);
        }
        private function _unRegisterMaterial(_arg1:MaterialObject3D):void{
            delete materials[_arg1];
        }
        public function updateMaterialsAfterRender(_arg1:RenderSessionData):void{
            var _local2:IUpdateAfterMaterial;
            var _local3:MaterialObject3D;
            for each (_local3 in materials) {
                if ((_local3 is IUpdateAfterMaterial)){
                    _local2 = (_local3 as IUpdateAfterMaterial);
                    _local2.updateAfterRender(_arg1);
                };
            };
        }
        private function _registerMaterial(_arg1:MaterialObject3D):void{
            materials[_arg1] = _arg1;
        }
        public function updateMaterialsBeforeRender(_arg1:RenderSessionData):void{
            var _local2:IUpdateBeforeMaterial;
            var _local3:MaterialObject3D;
            for each (_local3 in materials) {
                if ((_local3 is IUpdateBeforeMaterial)){
                    _local2 = (_local3 as IUpdateBeforeMaterial);
                    _local2.updateBeforeRender(_arg1);
                };
            };
        }

        public static function getInstance():MaterialManager{
            if (!instance){
                instance = new (MaterialManager);
            };
            return (instance);
        }
        public static function unRegisterMaterial(_arg1:MaterialObject3D):void{
            getInstance()._unRegisterMaterial(_arg1);
        }
        public static function registerMaterial(_arg1:MaterialObject3D):void{
            getInstance()._registerMaterial(_arg1);
        }

    }
}//package org.papervision3d.core.render.material 
