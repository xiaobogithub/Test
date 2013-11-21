//Created by Action Script Viewer - http://www.buraks.com/asv
package mx.graphics {
    import flash.events.*;
    import mx.events.*;

    public class GradientEntry extends EventDispatcher {

        private var _color:uint
        private var _ratio:Number
        private var _alpha:Number = 1

        mx_internal static const VERSION:String = "3.2.0.3958";

        public function GradientEntry(_arg1:uint=0, _arg2:Number=-1, _arg3:Number=1){
            this.color = _arg1;
            if (_arg2 >= 0){
                this.ratio = _arg2;
            };
            this.alpha = _arg3;
        }
        public function get color():uint{
            return (_color);
        }
        public function get alpha():Number{
            return (_alpha);
        }
        public function set color(_arg1:uint):void{
            var _local2:uint = _color;
            if (_arg1 != _local2){
                _color = _arg1;
                dispatchEntryChangedEvent("color", _local2, _arg1);
            };
        }
        public function get ratio():Number{
            return (_ratio);
        }
        private function dispatchEntryChangedEvent(_arg1:String, _arg2, _arg3):void{
            dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, _arg1, _arg2, _arg3));
        }
        public function set ratio(_arg1:Number):void{
            var _local2:Number = _ratio;
            if (_arg1 != _local2){
                _ratio = _arg1;
                dispatchEntryChangedEvent("ratio", _local2, _arg1);
            };
        }
        public function set alpha(_arg1:Number):void{
            var _local2:Number = _alpha;
            if (_arg1 != _local2){
                _alpha = _arg1;
                dispatchEntryChangedEvent("alpha", _local2, _arg1);
            };
        }

    }
}//package mx.graphics 
