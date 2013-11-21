package com.ning.effects{
	import flash.display.Bitmap;
	import flash.display.BitmapData;
	import flash.display.DisplayObject;
	import flash.display.GradientType;
	import flash.display.SpreadMethod;
	import flash.display.Sprite;
	import flash.geom.Matrix;
	import flash.geom.Rectangle;

	public class Reflection extends Sprite {

		private var _target:Sprite;
		private var _rect:Rectangle;
		private var _alpha:Number = 0.5;
		private var _ratio:Number = 100;
		private var _distance:Number = 0;
		private var _dropOff:Number = 1;
		private var _orientation:String = "vertical";
		// add orientation horizontal.

		private var _bitmapData:BitmapData;
		private var _reflectionBitmap:Bitmap;
		private var _gradientMask:Sprite;

		private var _isDrawn:Boolean = false;

		public function Reflection(_targetObject:Sprite) {
			_target = _targetObject;
		}
		public function get target():Sprite {
			return _target;
		}
		public function get rect():Rectangle {
			return _rect;
		}
		public function set rect(_value:Rectangle):void {
			if (_value == _rect) {
				return;
			}
			_rect = _value;
			if (_isDrawn) {
				// clear and redraw.
			}
		}
		override public function get alpha():Number {
			return _alpha;
		}
		override public function set alpha(_value:Number):void {
			if (_value == _alpha) {
				return;
			}
			_alpha = _value;
			if (_isDrawn) {
				dispose();
				draw();
			}
		}
		public function get ratio():Number {
			return _ratio;
		}
		public function set ratio(_value:Number):void {
			if (_value == _ratio) {
				return;
			}
			_ratio = _value;
			if (_isDrawn) {
				dispose();
				draw();
			}
		}
		public function get distance():Number {
			return _distance;
		}
		public function set distance(_value:Number):void {
			if (_value == _distance) {
				return;
			}
			_distance = _value;
			if (_isDrawn) {
				_reflectionBitmap.y = _target.height + distance;
				_gradientMask.y = _reflectionBitmap.y;
			}
		}
		public function get dropOff():Number {
			return _dropOff;
		}
		public function set dropOff(_value:Number):void {
			if (_value == _dropOff) {
				return;
			}
			_dropOff = _value;
			if (_isDrawn) {
				dispose();
				draw();
			}
		}
		public function draw():void {
			var _bitmapMatrix:Matrix = new Matrix(1, 0, 0, -1, _rect.x, _target.height);
			_bitmapData = new BitmapData(_rect.width, _rect.height, true, 0);
			_bitmapData.draw(_target, _bitmapMatrix);

			_reflectionBitmap = new Bitmap(_bitmapData);
			_reflectionBitmap.cacheAsBitmap = true;
			_reflectionBitmap.x = _rect.x;
			_reflectionBitmap.y = _target.height + distance;
			_target.addChild(_reflectionBitmap);

			var _gradientMatrix = new Matrix();
			var _matrixHeight:Number = _dropOff <= 0 ? _rect.height : _rect.height / _dropOff;
			_gradientMatrix.createGradientBox(_rect.width, _matrixHeight, Math.PI / 2, 0, 0);
			_gradientMask = new Sprite();
			_gradientMask.graphics.beginGradientFill(GradientType.LINEAR, [0xFFFFFF,0xFFFFFF], [_alpha,0], [0,_ratio], _gradientMatrix, SpreadMethod.PAD);
			_gradientMask.graphics.drawRect(0, 0, _rect.width, _rect.height);
			_gradientMask.cacheAsBitmap = true;
			_gradientMask.x = _reflectionBitmap.x;
			_gradientMask.y = _reflectionBitmap.y;
			_target.addChild(_gradientMask);
			_reflectionBitmap.mask = _gradientMask;
			_isDrawn = true;
		}
		public function dispose():void {
			if(!_isDrawn) {
				return;
			}
			_target.removeChild(_reflectionBitmap);
			_reflectionBitmap = null;
			_target.removeChild(_gradientMask);
			_bitmapData.dispose();
			_isDrawn = false;
		}
	}
}