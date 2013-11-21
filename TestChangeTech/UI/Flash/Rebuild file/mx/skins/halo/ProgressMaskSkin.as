//Created by Action Script Viewer - http://www.buraks.com/asv
package mx.skins.halo {
    import flash.display.*;
    import mx.skins.*;

    public class ProgressMaskSkin extends ProgrammaticSkin {

        mx_internal static const VERSION:String = "3.2.0.3958";

        override protected function updateDisplayList(_arg1:Number, _arg2:Number):void{
            super.updateDisplayList(_arg1, _arg2);
            var _local3:Graphics = graphics;
            _local3.clear();
            _local3.beginFill(0xFFFF00);
            _local3.drawRect(1, 1, (_arg1 - 2), (_arg2 - 2));
            _local3.endFill();
        }

    }
}//package mx.skins.halo 
