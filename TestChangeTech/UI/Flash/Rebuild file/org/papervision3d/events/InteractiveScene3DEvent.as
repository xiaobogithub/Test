//Created by Action Script Viewer - http://www.buraks.com/asv
package org.papervision3d.events {
    import flash.display.*;
    import flash.events.*;
    import org.papervision3d.core.render.data.*;
    import org.papervision3d.objects.*;
    import org.papervision3d.core.geom.renderables.*;

    public class InteractiveScene3DEvent extends Event {

        public var y:Number = 0
        public var sprite:Sprite = null
        public var renderHitData:RenderHitData
        public var face3d:Triangle3D = null
        public var x:Number = 0
        public var displayObject3D:DisplayObject3D = null

        public static const OBJECT_ADDED:String = "objectAdded";
        public static const OBJECT_PRESS:String = "mousePress";
        public static const OBJECT_RELEASE:String = "mouseRelease";
        public static const OBJECT_CLICK:String = "mouseClick";
        public static const OBJECT_RELEASE_OUTSIDE:String = "mouseReleaseOutside";
        public static const OBJECT_OUT:String = "mouseOut";
        public static const OBJECT_MOVE:String = "mouseMove";
        public static const OBJECT_OVER:String = "mouseOver";
        public static const OBJECT_DOUBLE_CLICK:String = "mouseDoubleClick";

        public function InteractiveScene3DEvent(_arg1:String, _arg2:DisplayObject3D=null, _arg3:Sprite=null, _arg4:Triangle3D=null, _arg5:Number=0, _arg6:Number=0, _arg7:RenderHitData=null, _arg8:Boolean=false, _arg9:Boolean=false){
            super(_arg1, _arg8, _arg9);
            this.displayObject3D = _arg2;
            this.sprite = _arg3;
            this.face3d = _arg4;
            this.x = _arg5;
            this.y = _arg6;
            this.renderHitData = _arg7;
        }
        override public function toString():String{
            return (((((((("Type : " + type) + ", DO3D : ") + displayObject3D) + " Sprite : ") + sprite) + " Face : ") + face3d));
        }

    }
}//package org.papervision3d.events 
