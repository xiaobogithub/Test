//Created by Action Script Viewer - http://www.buraks.com/asv
package com.redbox.changetech.util {
    import flash.net.*;

    public class JavaScriptUtils {

        static var JS_SET_UP_TOGGLE = "setUpToggle";
        static var JS_TOGGLE_WINDOW = "toggleWindowSize";
        static var JS_OPEN_WINDOW_FULL_SCREEN = "openNewWindowFullScreen";
        static var JS_CLOSE_WINDOW = "closeWindow";

        public function closeWindow():void{
            callJavaScriptNoArgs(JS_CLOSE_WINDOW);
        }
        public function setUpToggle():void{
            callJavaScriptNoArgs(JS_SET_UP_TOGGLE);
        }
        public function openWindow():void{
            callJavaScriptNoArgs(JS_OPEN_WINDOW_FULL_SCREEN);
        }
        public function toggleWindowSize():void{
            callJavaScriptNoArgs(JS_TOGGLE_WINDOW);
        }
        public function callJavaScriptNoArgs(_arg1){
            var _local2 = (("javascript:" + _arg1) + "()");
            var _local3:URLRequest = new URLRequest(_local2);
            navigateToURL(_local3, "_self");
        }

    }
}//package com.redbox.changetech.util 
