//Created by Action Script Viewer - http://www.buraks.com/asv
package caurina.transitions {

    public class SpecialPropertySplitter {

        public var parameters:Array
        public var splitValues:Function

        public function SpecialPropertySplitter(_arg1:Function, _arg2:Array){
            splitValues = _arg1;
        }
        public function toString():String{
            var _local1 = "";
            _local1 = (_local1 + "[SpecialPropertySplitter ");
            _local1 = (_local1 + ("splitValues:" + String(splitValues)));
            _local1 = (_local1 + ", ");
            _local1 = (_local1 + ("parameters:" + String(parameters)));
            _local1 = (_local1 + "]");
            return (_local1);
        }

    }
}//package caurina.transitions 
