//Created by Action Script Viewer - http://www.buraks.com/asv
package mx.containers.gridClasses {
    import mx.core.*;
    import mx.containers.utilityClasses.*;

    public class GridColumnInfo extends FlexChildInfo {

        public var x:Number

        mx_internal static const VERSION:String = "3.2.0.3958";

        public function GridColumnInfo(){
            min = 0;
            preferred = 0;
            max = UIComponent.DEFAULT_MAX_WIDTH;
            flex = 0;
            percent = 0;
        }
    }
}//package mx.containers.gridClasses 
