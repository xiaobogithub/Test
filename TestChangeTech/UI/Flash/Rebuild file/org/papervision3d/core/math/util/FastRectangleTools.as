//Created by Action Script Viewer - http://www.buraks.com/asv
package org.papervision3d.core.math.util {
    import flash.geom.*;

    public class FastRectangleTools {

        public static function intersection(_arg1:Rectangle, _arg2:Rectangle, _arg3:Rectangle=null):Rectangle{
            if (!_arg3){
                _arg3 = new Rectangle();
            };
            if (!intersects(_arg1, _arg2)){
                _arg3.x = (_arg3.y = (_arg3.width = (_arg3.height = 0)));
                return (_arg3);
            };
            _arg3.left = ((_arg1.left)>_arg2.left) ? _arg1.left : _arg2.left;
            _arg3.right = ((_arg1.right)<_arg2.right) ? _arg1.right : _arg2.right;
            _arg3.top = ((_arg1.top)>_arg2.top) ? _arg1.top : _arg2.top;
            _arg3.bottom = ((_arg1.bottom)<_arg2.bottom) ? _arg1.bottom : _arg2.bottom;
            return (_arg3);
        }
        public static function intersects(_arg1:Rectangle, _arg2:Rectangle):Boolean{
            if (!(((_arg1.right < _arg2.left)) || ((_arg1.left > _arg2.right)))){
                if (!(((_arg1.bottom < _arg2.top)) || ((_arg1.top > _arg2.bottom)))){
                    return (true);
                };
            };
            return (false);
        }

    }
}//package org.papervision3d.core.math.util 
