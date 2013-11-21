//Created by Action Script Viewer - http://www.buraks.com/asv
package caurina.transitions {

    public class SpecialPropertyModifier {

        public var getValue:Function
        public var modifyValues:Function

        public function SpecialPropertyModifier(_arg1:Function, _arg2:Function){
            modifyValues = _arg1;
            getValue = _arg2;
        }
        public function toString():String{
            var _local1 = "";
            _local1 = (_local1 + "[SpecialPropertyModifier ");
            _local1 = (_local1 + ("modifyValues:" + String(modifyValues)));
            _local1 = (_local1 + ", ");
            _local1 = (_local1 + ("getValue:" + String(getValue)));
            _local1 = (_local1 + "]");
            return (_local1);
        }

    }
}//package caurina.transitions 
