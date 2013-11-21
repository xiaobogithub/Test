//Created by Action Script Viewer - http://www.buraks.com/asv
package mx.containers.gridClasses {
    import mx.core.*;

    public class GridRowInfo {

        public var y:Number
        public var preferred:Number
        public var max:Number
        public var height:Number
        public var flex:Number
        public var min:Number

        mx_internal static const VERSION:String = "3.2.0.3958";

        public function GridRowInfo(){
            min = 0;
            preferred = 0;
            max = UIComponent.DEFAULT_MAX_HEIGHT;
            flex = 0;
        }
    }
}//package mx.containers.gridClasses 
