//Created by Action Script Viewer - http://www.buraks.com/asv
package mx.effects.effectClasses {

    public class PropertyChanges {

        public var target:Object
        public var start:Object
        public var end:Object

        mx_internal static const VERSION:String = "3.2.0.3958";

        public function PropertyChanges(_arg1:Object){
            end = {};
            start = {};
            super();
            this.target = _arg1;
        }
    }
}//package mx.effects.effectClasses 
