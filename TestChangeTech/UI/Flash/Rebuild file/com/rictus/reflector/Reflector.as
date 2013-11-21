//Created by Action Script Viewer - http://www.buraks.com/asv
package com.rictus.reflector {
    import flash.display.*;
    import flash.geom.*;
    import mx.core.*;
    import mx.events.*;

    public class Reflector extends UIComponent {

        private var _resultBitmap:BitmapData
        private var _target:UIComponent
        private var _falloff:Number = 0.6
        private var _alphaGradientBitmap:BitmapData
        private var _targetBitmap:BitmapData

        private function handleTargetUpdate(_arg1:FlexEvent):void{
            invalidateDisplayList();
        }
        private function set _880905839target(_arg1:UIComponent):void{
            if (_target != null){
                _target.removeEventListener(FlexEvent.UPDATE_COMPLETE, handleTargetUpdate, true);
                _target.removeEventListener(MoveEvent.MOVE, handleTargetMove);
                _target.removeEventListener(ResizeEvent.RESIZE, handleTargetResize);
                clearCachedBitmaps();
            };
            _target = _arg1;
            if (_target != null){
                _target.addEventListener(FlexEvent.UPDATE_COMPLETE, handleTargetUpdate, true);
                _target.addEventListener(MoveEvent.MOVE, handleTargetMove);
                _target.addEventListener(ResizeEvent.RESIZE, handleTargetResize);
                invalidateDisplayList();
            };
        }
        private function createBitmaps(_arg1:UIComponent):void{
            var _local2:Matrix;
            var _local3:Sprite;
            if (_alphaGradientBitmap == null){
                _alphaGradientBitmap = new BitmapData(_arg1.width, _arg1.height, true, 0);
                _local2 = new Matrix();
                _local3 = new Sprite();
                _local2.createGradientBox(_arg1.width, (_arg1.height * _falloff), (Math.PI / 2), 0, (_arg1.height * (1 - _falloff)));
                _local3.graphics.beginGradientFill(GradientType.LINEAR, [0xFFFFFF, 0xFFFFFF], [0, 1], [0, 0xFF], _local2);
                _local3.graphics.drawRect(0, (_arg1.height * (1 - _falloff)), _arg1.width, (_arg1.height * _falloff));
                _local3.graphics.endFill();
                _alphaGradientBitmap.draw(_local3, new Matrix());
            };
            if (_targetBitmap == null){
                _targetBitmap = new BitmapData(_arg1.width, _arg1.height, true, 0);
            };
            if (_resultBitmap == null){
                _resultBitmap = new BitmapData(_arg1.width, _arg1.height, true, 0);
            };
        }
        public function set target(_arg1:UIComponent):void{
            var _local2:Object = this.target;
            if (_local2 !== _arg1){
                this._880905839target = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "target", _local2, _arg1));
            };
        }
        private function handleTargetResize(_arg1:ResizeEvent):void{
            clearCachedBitmaps();
            width = _target.width;
            height = _target.height;
            invalidateDisplayList();
        }
        private function clearCachedBitmaps():void{
            _alphaGradientBitmap = null;
            _targetBitmap = null;
            _resultBitmap = null;
        }
        public function set falloff(_arg1:Number):void{
            var _local2:Object = this.falloff;
            if (_local2 !== _arg1){
                this._1083809772falloff = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "falloff", _local2, _arg1));
            };
        }
        public function get target():UIComponent{
            return (_target);
        }
        public function get falloff():Number{
            return (_falloff);
        }
        override protected function updateDisplayList(_arg1:Number, _arg2:Number):void{
            var _local3:Rectangle;
            var _local4:Matrix;
            if (_target != null){
                createBitmaps(_target);
                _local3 = new Rectangle(0, 0, _target.width, _target.height);
                _targetBitmap.fillRect(_local3, 0);
                _targetBitmap.draw(_target, new Matrix());
                _resultBitmap.fillRect(_local3, 0);
                _resultBitmap.copyPixels(_targetBitmap, _local3, new Point(), _alphaGradientBitmap);
                _local4 = new Matrix();
                _local4.scale(1, -1);
                _local4.translate(0, _target.height);
                graphics.clear();
                graphics.beginBitmapFill(_resultBitmap, _local4, false);
                graphics.drawRect(0, 0, _arg1, _arg2);
            };
        }
        private function handleTargetMove(_arg1:MoveEvent):void{
            move(_target.x, (_target.y + _target.height));
        }
        private function set _1083809772falloff(_arg1:Number):void{
            _falloff = _arg1;
            _alphaGradientBitmap = null;
            invalidateDisplayList();
        }

    }
}//package com.rictus.reflector 
