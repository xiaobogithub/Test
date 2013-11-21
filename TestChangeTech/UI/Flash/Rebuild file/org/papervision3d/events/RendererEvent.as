//Created by Action Script Viewer - http://www.buraks.com/asv
package org.papervision3d.events {
    import flash.events.*;
    import org.papervision3d.core.render.data.*;

    public class RendererEvent extends Event {

        public var renderSessionData:RenderSessionData

        public static const PROJECTION_DONE:String = "projectionDone";
        public static const RENDER_DONE:String = "renderDone";

        public function RendererEvent(_arg1:String, _arg2:RenderSessionData){
            super(_arg1);
            this.renderSessionData = _arg2;
        }
        public function clear():void{
            renderSessionData = null;
        }
        override public function clone():Event{
            return (new RendererEvent(type, renderSessionData));
        }

    }
}//package org.papervision3d.events 
