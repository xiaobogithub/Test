package com.ning.display{
	import flash.display.Sprite;

	public class ColorableSprite extends Sprite {
		
		protected var _primaryThemeColor:uint = 0x000000;
		protected var _secondaryThemeColor:uint = 0x666666;
		
		public function get primaryThemeColor():uint {
			return _primaryThemeColor;
		}
		public function set primaryThemeColor(_value:uint):void {
			if (_value == _primaryThemeColor) {
				return;
			}
			_primaryThemeColor = _value;
			updatePrimaryThemeColor();
		}
		public function get secondaryThemeColor():uint {
			return _secondaryThemeColor;
		}
		public function set secondaryThemeColor(_value:uint):void {
			if (_value == _secondaryThemeColor) {
				return;
			}
			_secondaryThemeColor = _value;
			updateSecondaryThemeColor();
		}
		protected function updatePrimaryThemeColor():void {
			trace("[ABSTRACTE: com.ning.display.ColorableButton.updatePrimaryThemeColor()]");
		}
		protected function updateSecondaryThemeColor():void {
			trace("[ABSTRACTE: com.ning.display.ColorableButton.updateSecondaryThemeColor()]");
		}
	}
}