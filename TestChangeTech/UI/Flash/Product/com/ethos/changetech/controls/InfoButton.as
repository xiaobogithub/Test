package com.ethos.changetech.controls{
	import flash.display.MovieClip;
	import flash.display.Sprite;
	import flash.events.MouseEvent;
	import flash.filters.BitmapFilterQuality;
	import flash.filters.GlowFilter;
	import flash.geom.ColorTransform;

	public class InfoButton extends Sprite {
		private var _filter:GlowFilter;
		private var _themeColor:uint = 0x666666;

		public function InfoButton():void {
			this.buttonMode = true;
			this.useHandCursor = true;
			this.addEventListener(MouseEvent.MOUSE_OVER, mouseOverHandler);
			this.addEventListener(MouseEvent.MOUSE_OUT, mouseOutHandler);
			initShadow();
			initColor();
		}
		public function get themeColor():uint {
			return _themeColor;
		}
		public function set themeColor(_value:uint):void {
			if (_value == _themeColor) {
				return;
			}
			_themeColor = _value;
			_filter.color = _themeColor;
			initColor();
		}
		private function initShadow():void {
			var _color:Number = _themeColor;
			var _alpha:Number = 1;
			var _blurX:Number = 2;
			var _blurY:Number = 2;
			var _strength:Number = 2;
			var _inner:Boolean = false;
			var _knockout:Boolean = false;
			var _quality:Number = BitmapFilterQuality.HIGH;

			_filter = new GlowFilter(_color, _alpha, _blurX, _blurY, _strength, _quality, _inner, _knockout);
		}
		private function initColor():void {
			var _colorTransform = new ColorTransform();
			_colorTransform.color = _themeColor;
			icon_mc.transform.colorTransform = _colorTransform;
		}
		private function mouseOverHandler(_event:MouseEvent):void {
			this.filters = new Array(_filter);
		}
		private function mouseOutHandler(_event:MouseEvent):void {
			this.filters = null;
		}
	}
}