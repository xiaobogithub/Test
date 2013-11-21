//Created by Action Script Viewer - http://www.buraks.com/asv
package com.degrafa.decorators {
    import flash.display.*;

    public interface IRenderDecorator extends IDecorator {

        function curveTo(_arg1:Number, _arg2:Number, _arg3:Number, _arg4:Number, _arg5:Graphics):void;
        function moveTo(_arg1:Number, _arg2:Number, _arg3:Graphics):void;
        function lineTo(_arg1:Number, _arg2:Number, _arg3:Graphics):void;

    }
}//package com.degrafa.decorators 
