//Created by Action Script Viewer - http://www.buraks.com/asv
package mx.controls.listClasses {
    import mx.collections.*;

    public class ListBaseSeekPending {

        public var offset:int
        public var bookmark:CursorBookmark

        mx_internal static const VERSION:String = "3.2.0.3958";

        public function ListBaseSeekPending(_arg1:CursorBookmark, _arg2:int){
            this.bookmark = _arg1;
            this.offset = _arg2;
        }
    }
}//package mx.controls.listClasses 
