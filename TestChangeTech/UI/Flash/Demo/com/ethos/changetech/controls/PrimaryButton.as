package com.ethos.changetech.controls{
	import flash.display.*;
	import flash.text.*;

	public class PrimaryButton extends Sprite {
		public function PrimaryButton() {
			captionTextField.selectable = false;
			captionTextField.autoSize = TextFieldAutoSize.LEFT;
			captionTextField.wordWrap = true;
			this.mouseChildren = false;
			this.buttonMode = true;
			this.useHandCursor = true;
			///format...
		}
		public function get caption():String {
			return captionTextField.text;
		}
		public function set caption(_value:String):void {
			if (_value == captionTextField.text) {
				return;
			}
			captionTextField.text = _value;
			captionTextField.y = (this.height-captionTextField.height)/2;
		}
	}
}