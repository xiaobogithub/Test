package com.ethos.changetech.controls{
	import com.ning.display.ColorableSprite;
	import flash.display.MovieClip;
	import flash.geom.ColorTransform;

	public class PageContentBackground extends ColorableSprite {
		override public function set width(_value:Number):void {
			border.width = _value;
			bg.width = _value - 10;
		}
		override public function set height(_value:Number):void {
			border.height = _value;
			bg.height = _value - 10;
		}
		override protected function updatePrimaryThemeColor():void {
			var _colorTransform = new ColorTransform();
			_colorTransform.color = _primaryThemeColor;
			border.transform.colorTransform = _colorTransform;
		}
	}
}