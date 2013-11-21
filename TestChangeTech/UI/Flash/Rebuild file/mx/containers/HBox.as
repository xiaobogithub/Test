//Created by Action Script Viewer - http://www.buraks.com/asv
package mx.containers {
    import mx.core.*;

    public class HBox extends Box {

        mx_internal static const VERSION:String = "3.2.0.3958";

        public function HBox(){
            mx_internal::layoutObject.direction = BoxDirection.HORIZONTAL;
        }
        override public function set direction(_arg1:String):void{
        }

    }
}//package mx.containers 
