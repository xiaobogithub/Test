//Created by Action Script Viewer - http://www.buraks.com/asv
package mx.core {

    public class ClassFactory implements IFactory {

        public var properties:Object = null
        public var generator:Class

        mx_internal static const VERSION:String = "3.2.0.3958";

        public function ClassFactory(_arg1:Class=null){
            this.generator = _arg1;
        }
        public function newInstance(){
            var _local2:String;
            var _local1:Object = new generator();
            if (properties != null){
                for (_local2 in properties) {
                    _local1[_local2] = properties[_local2];
                };
            };
            return (_local1);
        }

    }
}//package mx.core 
