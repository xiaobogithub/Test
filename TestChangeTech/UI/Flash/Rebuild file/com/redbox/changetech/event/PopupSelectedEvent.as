//Created by Action Script Viewer - http://www.buraks.com/asv
package com.redbox.changetech.event {
    import flash.events.*;

    public class PopupSelectedEvent extends Event {

        public var id:String

        public static const POP_UP_SELECTED:String = "popupSelected";
        public static const POP_UP_CLOSE:String = "POP_UP_CLOSE";

        public function PopupSelectedEvent(_arg1:String, _arg2:Boolean, _arg3:Boolean, _arg4:String){
            super(_arg1, _arg2, _arg3);
            this.id = _arg4;
        }
    }
}//package com.redbox.changetech.event 
