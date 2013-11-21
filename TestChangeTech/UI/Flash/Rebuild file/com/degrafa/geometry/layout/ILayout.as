//Created by Action Script Viewer - http://www.buraks.com/asv
package com.degrafa.geometry.layout {
    import flash.geom.*;
    import com.degrafa.core.*;

    public interface ILayout extends IDegrafaObject {

        function get layoutRectangle():Rectangle;
        function computeLayoutRectangle(_arg1:Rectangle, _arg2:Rectangle):Rectangle;

    }
}//package com.degrafa.geometry.layout 
