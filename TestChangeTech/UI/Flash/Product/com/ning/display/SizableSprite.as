package com.ning.display{
	import flash.display.Sprite;

	public class SizableSprite extends Sprite {

		private var _width:Number;
		private var _height:Number;

		public function SizableSprite() {
		}
		override public function get width():Number {
			return _width;
		}
		override public function set width(_value:Number):void {
			_width = _value;
		}
		override public function get height():Number {
			return _height;
		}
		override public function set height(_value:Number):void {
			_height = _value;
		}
	}
}