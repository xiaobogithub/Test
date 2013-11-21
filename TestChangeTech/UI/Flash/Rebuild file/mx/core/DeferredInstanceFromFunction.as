//Created by Action Script Viewer - http://www.buraks.com/asv
package mx.core {

    public class DeferredInstanceFromFunction implements IDeferredInstance {

        private var generator:Function
        private var instance:Object = null

        mx_internal static const VERSION:String = "3.2.0.3958";

        public function DeferredInstanceFromFunction(_arg1:Function){
            this.generator = _arg1;
        }
        public function getInstance():Object{
            if (!instance){
                instance = generator();
            };
            return (instance);
        }

    }
}//package mx.core 
