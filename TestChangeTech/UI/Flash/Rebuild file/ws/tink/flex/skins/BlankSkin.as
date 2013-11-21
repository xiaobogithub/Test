//Created by Action Script Viewer - http://www.buraks.com/asv
package ws.tink.flex.skins {
    import mx.core.*;
    import mx.skins.*;

    public class BlankSkin extends Border {

        private var _borderMetrics:EdgeMetrics

        public function BlankSkin(){
            _borderMetrics = new EdgeMetrics(1, 1, 1, 1);
            super();
        }
        override public function get measuredWidth():Number{
            return (UIComponent.DEFAULT_MEASURED_MIN_WIDTH);
        }
        override protected function updateDisplayList(_arg1:Number, _arg2:Number):void{
            super.updateDisplayList(_arg1, _arg2);
            var _local3:Number = getStyle("cornerRadius");
            _local3 = Math.max(0, _local3);
            graphics.clear();
            graphics.beginFill(0, 0);
            graphics.drawRoundRect(0, 0, _arg1, _arg2, _local3, _local3);
            graphics.endFill();
        }
        override public function get measuredHeight():Number{
            return (UIComponent.DEFAULT_MEASURED_MIN_HEIGHT);
        }
        override public function get borderMetrics():EdgeMetrics{
            return (_borderMetrics);
        }

    }
}//package ws.tink.flex.skins 
