//Created by Action Script Viewer - http://www.buraks.com/asv
package com.degrafa.paint {
    import flash.display.*;
    import flash.geom.*;
    import mx.events.*;
    import com.degrafa.*;
    import com.degrafa.core.*;
    import com.degrafa.paint.palette.*;
    import com.degrafa.core.utils.*;

    public class SolidFill extends DegrafaObject implements IGraphicsFill {

        private var _paletteEntry:PaletteEntry
        protected var _colorFunction:Function
        protected var _alpha:Number
        protected var _color:Object
        private var _requester:IGeometryComposition

        public function SolidFill(_arg1:Object=null, _arg2:Number=NaN){
            this.alpha = _arg2;
            this.color = _arg1;
        }
        public function set color(_arg1:Object):void{
            var _local2:uint;
            if ((_arg1 is PaletteEntry)){
                paletteEntry = (_arg1 as PaletteEntry);
            } else {
                paletteEntry = null;
            };
            _arg1 = ColorUtil.resolveColor(_arg1);
            if (_color != _arg1){
                _local2 = (_color as uint);
                _color = (_arg1 as uint);
                initChange("color", _local2, _color, this);
            };
        }
        private function onPaletteEntryChange(_arg1:PropertyChangeEvent):void{
            if ((((_arg1.property == "value")) && ((_arg1.kind == "update")))){
                color = _arg1.source;
            };
        }
        public function set derive(_arg1:SolidFill):void{
            if (!_color){
                _color = uint(_arg1.color);
            };
            if (isNaN(_alpha)){
                _alpha = _arg1.alpha;
            };
        }
        public function end(_arg1:Graphics):void{
            _arg1.endFill();
        }
        public function get alpha():Number{
            if (isNaN(_alpha)){
                return (1);
            };
            return (_alpha);
        }
        public function get color():Object{
            if (colorFunction != null){
                return (ColorUtil.resolveColor(colorFunction()));
            };
            if (!_color){
                return (0);
            };
            return (_color);
        }
        public function get colorFunction():Function{
            return (_colorFunction);
        }
        private function set paletteEntry(_arg1:PaletteEntry):void{
            if (_arg1){
                if (_paletteEntry !== _arg1){
                    if (_paletteEntry){
                        if (_paletteEntry.hasEventListener(PropertyChangeEvent.PROPERTY_CHANGE)){
                            _paletteEntry.removeEventListener(PropertyChangeEvent.PROPERTY_CHANGE, onPaletteEntryChange);
                        };
                    };
                    _paletteEntry = _arg1;
                    if (_paletteEntry.enableEvents){
                        _paletteEntry.addEventListener(PropertyChangeEvent.PROPERTY_CHANGE, onPaletteEntryChange);
                    };
                };
            } else {
                if (_paletteEntry){
                    if (_paletteEntry.hasEventListener(PropertyChangeEvent.PROPERTY_CHANGE)){
                        _paletteEntry.removeEventListener(PropertyChangeEvent.PROPERTY_CHANGE, onPaletteEntryChange);
                    };
                    _paletteEntry = null;
                };
            };
        }
        public function begin(_arg1:Graphics, _arg2:Rectangle):void{
            if (isNaN(_alpha)){
                _alpha = 1;
            };
            _arg1.beginFill((color as uint), alpha);
        }
        public function set colorFunction(_arg1:Function):void{
            var _local2:Function;
            if (_colorFunction != _arg1){
                _local2 = (_colorFunction as Function);
                _colorFunction = (_arg1 as Function);
                initChange("colorFunction", _local2, _colorFunction, this);
            };
        }
        public function set requester(_arg1:IGeometryComposition):void{
            _requester = _arg1;
        }
        public function set alpha(_arg1:Number):void{
            var _local2:Number;
            if (_alpha != _arg1){
                _local2 = _alpha;
                _alpha = _arg1;
                initChange("alpha", _local2, _alpha, this);
            };
        }

    }
}//package com.degrafa.paint 
