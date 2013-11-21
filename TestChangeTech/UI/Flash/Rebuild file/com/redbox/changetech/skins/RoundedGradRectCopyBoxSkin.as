//Created by Action Script Viewer - http://www.buraks.com/asv
package com.redbox.changetech.skins {
    import flash.geom.*;
    import com.redbox.changetech.model.*;
    import com.redbox.changetech.vo.*;
    import mx.skins.*;
    import com.redbox.changetech.util.*;

    public class RoundedGradRectCopyBoxSkin extends ProgrammaticSkin {

        override public function get measuredWidth():Number{
            return (20);
        }
        override public function get measuredHeight():Number{
            return (20);
        }
        override protected function updateDisplayList(_arg1:Number, _arg2:Number):void{
            var _local3:RoomVO = RoomVO(Config.ROOM_CONFIGS.getItemAt(BalanceModelLocator.getInstance().room));
            var _local4 = "linear";
            var _local5:Number = 90;
            var _local6:Number = 0.5;
            var _local7:Array = [_local3.boxColour1, _local3.boxColour2];
            var _local8:Array = [1, 1];
            var _local9 = 20;
            var _local10:Number = _local3.boxColour2;
            var _local11:Number = 1;
            var _local12:Number = 2;
            var _local13:Number = _arg1;
            var _local14:Number = _arg2;
            var _local15:Matrix = new Matrix();
            _local15.createGradientBox(_local13, _local14, ((_local5 * Math.PI) / 180));
            graphics.clear();
            graphics.lineStyle(_local12, _local10, _local11);
            graphics.beginGradientFill(_local4, _local7, _local8, [0, 0xFF], _local15, "pad", "rgb", _local6);
            graphics.drawRoundRect(0, 0, _local13, _local14, _local9, _local9);
            graphics.endFill();
        }

    }
}//package com.redbox.changetech.skins 
