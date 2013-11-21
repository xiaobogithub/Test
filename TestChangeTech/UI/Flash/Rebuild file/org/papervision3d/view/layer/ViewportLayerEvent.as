//Created by Action Script Viewer - http://www.buraks.com/asv
package org.papervision3d.view.layer {
    import flash.events.*;
    import org.papervision3d.objects.*;

    public class ViewportLayerEvent extends Event {

        public var layer:ViewportLayer
        public var do3d:DisplayObject3D

        public static const CHILD_REMOVED:String = "childRemoved";
        public static const CHILD_ADDED:String = "childAdded";

        public function ViewportLayerEvent(_arg1:String, _arg2:DisplayObject3D=null, _arg3:ViewportLayer=null){
            super(_arg1, false, false);
            this.do3d = _arg2;
            this.layer = _arg3;
        }
    }
}//package org.papervision3d.view.layer 
