//Created by Action Script Viewer - http://www.buraks.com/asv
package org.papervision3d.core.proto {
    import org.papervision3d.objects.*;
    import org.papervision3d.core.log.*;
    import org.papervision3d.materials.utils.*;
    import org.papervision3d.*;

    public class SceneObject3D extends DisplayObjectContainer3D {

        public var objects:Array
        public var materials:MaterialsList

        public function SceneObject3D(){
            this.objects = new Array();
            this.materials = new MaterialsList();
            PaperLogger.info((((((Papervision3D.NAME + " ") + Papervision3D.VERSION) + " (") + Papervision3D.DATE) + ")\n"));
            this.root = this;
        }
        override public function removeChild(_arg1:DisplayObject3D):DisplayObject3D{
            super.removeChild(_arg1);
            var _local2:int;
            while (_local2 < this.objects.length) {
                if (this.objects[_local2] === _arg1){
                    this.objects.splice(_local2, 1);
                    return (_arg1);
                };
                _local2++;
            };
            return (_arg1);
        }
        override public function addChild(_arg1:DisplayObject3D, _arg2:String=null):DisplayObject3D{
            var _local3:DisplayObject3D = super.addChild(_arg1, (_arg2) ? _arg2 : _arg1.name);
            _arg1.scene = this;
            _arg1.parent = null;
            this.objects.push(_local3);
            return (_local3);
        }

    }
}//package org.papervision3d.core.proto 
