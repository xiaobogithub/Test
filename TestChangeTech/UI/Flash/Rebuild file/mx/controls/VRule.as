//Created by Action Script Viewer - http://www.buraks.com/asv
package mx.controls {
    import flash.display.*;
    import mx.core.*;

    public class VRule extends UIComponent {

        mx_internal static const VERSION:String = "3.2.0.3958";
        private static const DEFAULT_PREFERRED_HEIGHT:Number = 100;

        override protected function measure():void{
            super.measure();
            measuredWidth = getStyle("strokeWidth");
            measuredHeight = DEFAULT_PREFERRED_HEIGHT;
        }
        override protected function updateDisplayList(_arg1:Number, _arg2:Number):void{
            super.updateDisplayList(_arg1, _arg2);
            var _local3:Graphics = graphics;
            _local3.clear();
            var _local4:Number = getStyle("strokeColor");
            var _local5:Number = getStyle("shadowColor");
            var _local6:Number = getStyle("strokeWidth");
            if (_local6 > _arg1){
                _local6 = _arg1;
            };
            var _local7:Number = ((_arg1 - _local6) / 2);
            var _local8:Number = 0;
            var _local9:Number = (_local7 + _local6);
            var _local10:Number = _arg2;
            if (_local6 == 1){
                _local3.beginFill(_local4);
                _local3.drawRect(_local7, _local8, (_local9 - _local7), _arg2);
                _local3.endFill();
            } else {
                if (_local6 == 2){
                    _local3.beginFill(_local4);
                    _local3.drawRect(_local7, _local8, 1, _arg2);
                    _local3.endFill();
                    _local3.beginFill(_local5);
                    _local3.drawRect((_local9 - 1), _local8, 1, _arg2);
                    _local3.endFill();
                } else {
                    if (_local6 > 2){
                        _local3.beginFill(_local4);
                        _local3.drawRect(_local7, _local8, ((_local9 - _local7) - 1), 1);
                        _local3.endFill();
                        _local3.beginFill(_local5);
                        _local3.drawRect((_local9 - 1), _local8, 1, (_arg2 - 1));
                        _local3.drawRect(_local7, (_local10 - 1), (_local9 - _local7), 1);
                        _local3.endFill();
                        _local3.beginFill(_local4);
                        _local3.drawRect(_local7, (_local8 + 1), 1, (_arg2 - 2));
                        _local3.endFill();
                    };
                };
            };
        }

    }
}//package mx.controls 
