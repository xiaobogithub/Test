//Created by Action Script Viewer - http://www.buraks.com/asv
package mx.core {

    public class UIComponentDescriptor extends ComponentDescriptor {

        mx_internal var instanceIndices:Array
        public var stylesFactory:Function
        public var effects:Array
        mx_internal var repeaters:Array
        mx_internal var repeaterIndices:Array

        mx_internal static const VERSION:String = "3.2.0.3958";

        public function UIComponentDescriptor(_arg1:Object){
            super(_arg1);
        }
        override public function toString():String{
            return (("UIComponentDescriptor_" + id));
        }

    }
}//package mx.core 
