package com.ethos.changetech.controls{
	import flash.display.*;
	import flash.text.TextField;

	public class CategoryButton extends Sprite {

		public function CategoryButton() {
			labelTextField.selectable = false;
			this.mouseChildren = false;
			this.buttonMode = true;
			this.useHandCursor = true;
		}
		public function get label():String {
			return labelTextField.text;
		}
		public function set label(_value:String):void {
			if (_value == labelTextField.text) {
				return;
			}
			labelTextField.text = _value;
			this.width = labelTextField.textWidth+20;
		}
	}
}