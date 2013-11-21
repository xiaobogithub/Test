//Created by Action Script Viewer - http://www.buraks.com/asv
package caurina.transitions {

    public class SpecialProperty {

        public var parameters:Array
        public var getValue:Function
        public var setValue:Function

        public function SpecialProperty(_arg1:Function, _arg2:Function, _arg3:Array=null){
            getValue = _arg1;
            setValue = _arg2;
            parameters = _arg3;
        }
        public function toString():String{
            var _local1 = "";
            _local1 = (_local1 + "[SpecialProperty ");
            _local1 = (_local1 + ("getValue:" + String(getValue)));
            _local1 = (_local1 + ", ");
            _local1 = (_local1 + ("setValue:" + String(setValue)));
            _local1 = (_local1 + ", ");
            _local1 = (_local1 + ("parameters:" + String(parameters)));
            _local1 = (_local1 + "]");
            return (_local1);
        }

    }
}//package caurina.transitions 
