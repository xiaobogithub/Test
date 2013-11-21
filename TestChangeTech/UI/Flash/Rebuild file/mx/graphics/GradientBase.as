//Created by Action Script Viewer - http://www.buraks.com/asv
package mx.graphics {
    import flash.events.*;
    import mx.events.*;

    public class GradientBase extends EventDispatcher {

        mx_internal var ratios:Array
        mx_internal var colors:Array
        private var _entries:Array
        mx_internal var alphas:Array

        public function GradientBase(){
            colors = [];
            ratios = [];
            alphas = [];
            _entries = [];
            super();
        }
        public function set entries(_arg1:Array):void{
            var _local2:Array = _entries;
            _entries = _arg1;
            processEntries();
            dispatchGradientChangedEvent("entries", _local2, _arg1);
        }
        private function processEntries():void{
            var _local2:int;
            var _local4:GradientEntry;
            var _local5:int;
            var _local6:Number;
            var _local7:Number;
            var _local8:int;
            colors = [];
            ratios = [];
            alphas = [];
            if (((!(_entries)) || ((_entries.length == 0)))){
                return;
            };
            var _local1:Number = 0xFF;
            var _local3:int = _entries.length;
            _local2 = 0;
            while (_local2 < _local3) {
                _local4 = _entries[_local2];
                _local4.addEventListener(PropertyChangeEvent.PROPERTY_CHANGE, entry_propertyChangeHandler, false, 0, true);
                colors.push(_local4.color);
                alphas.push(_local4.alpha);
                ratios.push((_local4.ratio * _local1));
                _local2++;
            };
            if (isNaN(ratios[0])){
                ratios[0] = 0;
            };
            if (isNaN(ratios[(_local3 - 1)])){
                ratios[(_local3 - 1)] = 0xFF;
            };
            _local2 = 1;
            while (true) {
                while ((((_local2 < _local3)) && (!(isNaN(ratios[_local2]))))) {
                    _local2++;
                };
                if (_local2 == _local3){
                    break;
                };
                _local5 = (_local2 - 1);
                while ((((_local2 < _local3)) && (isNaN(ratios[_local2])))) {
                    _local2++;
                };
                _local6 = ratios[_local5];
                _local7 = ratios[_local2];
                _local8 = 1;
                while (_local8 < (_local2 - _local5)) {
                    ratios[_local8] = (_local6 + ((_local8 * (_local7 - _local6)) / (_local2 - _local5)));
                    _local8++;
                };
            };
        }
        mx_internal function dispatchGradientChangedEvent(_arg1:String, _arg2, _arg3):void{
            dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, _arg1, _arg2, _arg3));
        }
        private function entry_propertyChangeHandler(_arg1:Event):void{
            processEntries();
            dispatchGradientChangedEvent("entries", entries, entries);
        }
        public function get entries():Array{
            return (_entries);
        }

    }
}//package mx.graphics 
