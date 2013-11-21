//Created by Action Script Viewer - http://www.buraks.com/asv
package mx.graphics {
    import flash.display.*;
    import flash.geom.*;
    import mx.core.*;

    public class LinearGradient extends GradientBase implements IFill {

        private var matrix:Matrix
        private var _rotation:Number = 0

        mx_internal static const VERSION:String = "3.2.0.3958";

        public function LinearGradient(){
            matrix = new Matrix();
            super();
        }
        public function begin(_arg1:Graphics, _arg2:Rectangle):void{
            matrix.createGradientBox(_arg2.width, _arg2.height, _rotation, _arg2.left, _arg2.top);
            _arg1.beginGradientFill(GradientType.LINEAR, mx_internal::colors, mx_internal::alphas, mx_internal::ratios, matrix);
        }
        public function get angle():Number{
            return (((_rotation / Math.PI) * 180));
        }
        public function set angle(_arg1:Number):void{
            var _local2:Number = _rotation;
            _rotation = ((_arg1 / 180) * Math.PI);
            mx_internal::dispatchGradientChangedEvent("angle", _local2, _rotation);
        }
        public function end(_arg1:Graphics):void{
            _arg1.endFill();
        }

    }
}//package mx.graphics 
