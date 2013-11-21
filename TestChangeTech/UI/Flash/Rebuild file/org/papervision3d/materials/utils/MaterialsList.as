//Created by Action Script Viewer - http://www.buraks.com/asv
package org.papervision3d.materials.utils {
    import flash.utils.*;
    import org.papervision3d.core.proto.*;

    public class MaterialsList {

        protected var _materials:Dictionary
        public var materialsByName:Dictionary
        private var _materialsTotal:int

        public function MaterialsList(_arg1=null):void{
            var _local2:String;
            var _local3:String;
            super();
            this.materialsByName = new Dictionary(true);
            this._materials = new Dictionary(false);
            this._materialsTotal = 0;
            if (_arg1){
                if ((_arg1 is Array)){
                    for (_local2 in _arg1) {
                        this.addMaterial(_arg1[_local2]);
                    };
                } else {
                    if ((_arg1 is Object)){
                        for (_local3 in _arg1) {
                            this.addMaterial(_arg1[_local3], _local3);
                        };
                    };
                };
            };
        }
        public function get numMaterials():int{
            return (_materialsTotal);
        }
        public function addMaterial(_arg1:MaterialObject3D, _arg2:String=null):MaterialObject3D{
            _arg2 = ((((_arg2) || (_arg1.name))) || (String(_arg1.id)));
            this._materials[_arg1] = _arg2;
            this.materialsByName[_arg2] = _arg1;
            this._materialsTotal++;
            return (_arg1);
        }
        public function removeMaterial(_arg1:MaterialObject3D):MaterialObject3D{
            if (this._materials[_arg1]){
                delete this.materialsByName[this._materials[_arg1]];
                delete this._materials[_arg1];
                _materialsTotal--;
            };
            return (_arg1);
        }
        public function toString():String{
            var _local2:MaterialObject3D;
            var _local1 = "";
            for each (_local2 in this.materialsByName) {
                _local1 = (_local1 + (this._materials[_local2] + "\n"));
            };
            return (_local1);
        }
        public function removeMaterialByName(_arg1:String):MaterialObject3D{
            return (removeMaterial(getMaterialByName(_arg1)));
        }
        public function clone():MaterialsList{
            var _local2:MaterialObject3D;
            var _local1:MaterialsList = new MaterialsList();
            for each (_local2 in this.materialsByName) {
                _local1.addMaterial(_local2.clone(), this._materials[_local2]);
            };
            return (_local1);
        }
        public function getMaterialByName(_arg1:String):MaterialObject3D{
            return ((this.materialsByName[_arg1]) ? this.materialsByName[_arg1] : this.materialsByName["all"]);
        }

    }
}//package org.papervision3d.materials.utils 
