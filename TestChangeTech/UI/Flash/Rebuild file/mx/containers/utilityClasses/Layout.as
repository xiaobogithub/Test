//Created by Action Script Viewer - http://www.buraks.com/asv
package mx.containers.utilityClasses {
    import mx.core.*;
    import mx.resources.*;

    public class Layout {

        private var _target:Container
        protected var resourceManager:IResourceManager

        mx_internal static const VERSION:String = "3.2.0.3958";

        public function Layout(){
            resourceManager = ResourceManager.getInstance();
            super();
        }
        public function get target():Container{
            return (_target);
        }
        public function set target(_arg1:Container):void{
            _target = _arg1;
        }
        public function measure():void{
        }
        public function updateDisplayList(_arg1:Number, _arg2:Number):void{
        }

    }
}//package mx.containers.utilityClasses 
