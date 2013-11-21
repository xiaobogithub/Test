//Created by Action Script Viewer - http://www.buraks.com/asv
package mx.containers {

    public class GridItem extends HBox {

        var colIndex:int = 0
        private var _rowSpan:int = 1
        private var _colSpan:int = 1

        mx_internal static const VERSION:String = "3.2.0.3958";

        public function set rowSpan(_arg1:int):void{
            _rowSpan = _arg1;
            invalidateSize();
        }
        public function get colSpan():int{
            return (_colSpan);
        }
        public function set colSpan(_arg1:int):void{
            _colSpan = _arg1;
            invalidateSize();
        }
        public function get rowSpan():int{
            return (_rowSpan);
        }

    }
}//package mx.containers 
