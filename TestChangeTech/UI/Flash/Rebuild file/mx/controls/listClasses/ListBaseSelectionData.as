//Created by Action Script Viewer - http://www.buraks.com/asv
package mx.controls.listClasses {

    public class ListBaseSelectionData {

        public var data:Object
        mx_internal var prevSelectionData:ListBaseSelectionData
        mx_internal var nextSelectionData:ListBaseSelectionData
        public var approximate:Boolean
        public var index:int

        mx_internal static const VERSION:String = "3.2.0.3958";

        public function ListBaseSelectionData(_arg1:Object, _arg2:int, _arg3:Boolean){
            this.data = _arg1;
            this.index = _arg2;
            this.approximate = _arg3;
        }
    }
}//package mx.controls.listClasses 
