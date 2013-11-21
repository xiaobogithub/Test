//Created by Action Script Viewer - http://www.buraks.com/asv
package mx.containers {
    import mx.core.*;
    import flash.events.*;
    import mx.styles.*;

    public class ApplicationControlBar extends ControlBar {

        private var dockChanged:Boolean = false
        private var _dock:Boolean = false

        mx_internal static const VERSION:String = "3.2.0.3958";

        public function set dock(_arg1:Boolean):void{
            if (_dock != _arg1){
                _dock = _arg1;
                dockChanged = true;
                invalidateProperties();
                dispatchEvent(new Event("dockChanged"));
            };
        }
        public function resetDock(_arg1:Boolean):void{
            _dock = !(_arg1);
            dock = _arg1;
        }
        public function get dock():Boolean{
            return (_dock);
        }
        override protected function commitProperties():void{
            super.commitProperties();
            if (dockChanged){
                dockChanged = false;
                if ((parent is Application)){
                    Application(parent).dockControlBar(this, _dock);
                };
            };
        }
        override public function set enabled(_arg1:Boolean):void{
            var _local2:Object = blocker;
            super.enabled = _arg1;
            if (((blocker) && (!((blocker == _local2))))){
                if ((blocker is IStyleClient)){
                    IStyleClient(blocker).setStyle("borderStyle", "applicationControlBar");
                };
            };
        }

    }
}//package mx.containers 
