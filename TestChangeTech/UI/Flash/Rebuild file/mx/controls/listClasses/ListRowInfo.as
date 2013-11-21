//Created by Action Script Viewer - http://www.buraks.com/asv
package mx.controls.listClasses {

    public class ListRowInfo {

        public var itemOldY:Number
        public var height:Number
        public var uid:String
        public var data:Object
        public var oldY:Number
        public var y:Number

        mx_internal static const VERSION:String = "3.2.0.3958";

        public function ListRowInfo(_arg1:Number, _arg2:Number, _arg3:String, _arg4:Object=null){
            this.y = _arg1;
            this.height = _arg2;
            this.uid = _arg3;
            this.data = _arg4;
        }
    }
}//package mx.controls.listClasses 
