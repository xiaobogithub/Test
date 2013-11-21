//Created by Action Script Viewer - http://www.buraks.com/asv
package org.papervision3d.core.culling {
    import flash.utils.*;
    import org.papervision3d.objects.*;

    public class ViewportObjectFilter implements IObjectCuller {

        protected var _mode:int
        protected var objects:Dictionary

        public function ViewportObjectFilter(_arg1:int):void{
            this.mode = _arg1;
            init();
        }
        public function addObject(_arg1:DisplayObject3D):void{
            objects[_arg1] = _arg1;
        }
        public function get mode():int{
            return (_mode);
        }
        public function set mode(_arg1:int):void{
            _mode = _arg1;
        }
        public function removeObject(_arg1:DisplayObject3D):void{
            delete objects[_arg1];
        }
        private function init():void{
            objects = new Dictionary(true);
        }
        public function testObject(_arg1:DisplayObject3D):int{
            if (objects[_arg1]){
                if (_mode == ViewportObjectFilterMode.INCLUSIVE){
                    return (1);
                };
                if (_mode == ViewportObjectFilterMode.EXCLUSIVE){
                    return (0);
                };
            } else {
                if (_mode == ViewportObjectFilterMode.INCLUSIVE){
                    return (0);
                };
                if (_mode == ViewportObjectFilterMode.EXCLUSIVE){
                    return (1);
                };
            };
            return (0);
        }
        public function destroy():void{
            objects = null;
        }

    }
}//package org.papervision3d.core.culling 
