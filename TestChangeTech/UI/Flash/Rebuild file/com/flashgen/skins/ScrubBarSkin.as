//Created by Action Script Viewer - http://www.buraks.com/asv
package com.flashgen.skins {
    import flash.display.*;
    import mx.core.*;
    import mx.skins.*;

    public class ScrubBarSkin extends ProgrammaticSkin {

        override protected function updateDisplayList(_arg1:Number, _arg2:Number):void{
            super.updateDisplayList(_arg1, _arg2);
            if ((!(parent) is UIComponent)){
                return;
            };
            var _local3:Graphics = graphics;
            var _local4:uint = getStyle("borderColor");
            var _local5:Number = getStyle("borderAlpha");
            var _local6:Number = getStyle("backgroundColor");
            var _local7:Number = getStyle("backgroundAlpha");
            _local3.clear();
            _local3.beginFill(_local4, _local5);
            _local3.drawRect(0, 0, _arg1, _arg2);
            _local3.endFill();
            _local3.beginFill(_local6, _local7);
            _local3.drawRect(1, 1, (_arg1 - 2), (_arg2 - 2));
            _local3.endFill();
        }

    }
}//package com.flashgen.skins 
