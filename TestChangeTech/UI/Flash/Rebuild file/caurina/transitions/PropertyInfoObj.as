//Created by Action Script Viewer - http://www.buraks.com/asv
package caurina.transitions {

    public class PropertyInfoObj {

        public var modifierParameters:Array
        public var valueComplete:Number
        public var modifierFunction:Function
        public var hasModifier:Boolean
        public var valueStart:Number

        public function PropertyInfoObj(_arg1:Number, _arg2:Number, _arg3:Function, _arg4:Array){
            valueStart = _arg1;
            valueComplete = _arg2;
            hasModifier = Boolean(_arg3);
            modifierFunction = _arg3;
            modifierParameters = _arg4;
        }
        public function toString():String{
            var _local1 = "\n[PropertyInfoObj ";
            _local1 = (_local1 + ("valueStart:" + String(valueStart)));
            _local1 = (_local1 + ", ");
            _local1 = (_local1 + ("valueComplete:" + String(valueComplete)));
            _local1 = (_local1 + ", ");
            _local1 = (_local1 + ("modifierFunction:" + String(modifierFunction)));
            _local1 = (_local1 + ", ");
            _local1 = (_local1 + ("modifierParameters:" + String(modifierParameters)));
            _local1 = (_local1 + "]\n");
            return (_local1);
        }
        public function clone():PropertyInfoObj{
            var _local1:PropertyInfoObj = new PropertyInfoObj(valueStart, valueComplete, modifierFunction, modifierParameters);
            return (_local1);
        }

    }
}//package caurina.transitions 
