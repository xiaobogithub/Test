package com.ethos.changetech.controls{
	import flash.display.*;
	import flash.events.*;
	import flash.text.*;
	//import com.ethos.changetech.models.*;

	public class CategoryPanel extends Sprite {

		private var _descriptionTextFieldBottomPadding:Number = 50;

		public function CategoryPanel() {
			titleTextField.selectable = false;
			descriptionTextField.wordWrap = true;
			descriptionTextField.autoSize = TextFieldAutoSize.LEFT;
			descriptionTextField.selectable = false;
			backButton.buttonMode = true;
			backButton.useHandCursor = true;
			backButton.mouseChildren = false;
			backButton.addEventListener(MouseEvent.CLICK, backButtonClickedHandler);
			updateView();
		}
		public function get title():String {
			return titleTextField.text;
		}
		public function set title(_value:String):void {
			if (_value == titleTextField.text) {
				return;
			}
			titleTextField.text = _value;
			updateView();
		}
		public function get description():String {
			return descriptionTextField.text;
		}
		public function set description(_value):void {
			if (_value == descriptionTextField.text) {
				return;
			}
			descriptionTextField.text = _value;
			updateView();
		}
		private function updateView():void {
			backButton.y = descriptionTextField.y +descriptionTextField.height + _descriptionTextFieldBottomPadding;
			bgPanel.height = backButton.y + backButton.height + 15;
		}
		private function backButtonClickedHandler(_event:MouseEvent):void {
			this.dispatchEvent(new PageEvent("category"));
		}
	}
}