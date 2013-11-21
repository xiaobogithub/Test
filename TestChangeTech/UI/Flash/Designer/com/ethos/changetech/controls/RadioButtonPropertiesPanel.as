package com.ethos.changetech.controls{
	import fl.controls.ColorPicker;
	import fl.events.ColorPickerEvent;
	import flash.events.Event;
	import flash.text.TextField;

	public class RadioButtonPropertiesPanel extends PropertiesPanel {

		public function RadioButtonPropertiesPanel() {
			fontFamilyComboBox.enabled = false;
			xTextField.restrict = "0-9";
			xTextField.addEventListener(Event.CHANGE, xChangeHandler);
			yTextField.restrict = "0-9";
			yTextField.addEventListener(Event.CHANGE, yChangeHandler);
			widthTextField.restrict = "0-9";
			widthTextField.addEventListener(Event.CHANGE, widthChangeHandler);
			heightTextField.backgroundColor = 0xdddddd;
			heightTextField.restrict = "";
			foregroundColorPicker.addEventListener(ColorPickerEvent.CHANGE, foregroundColorChangeHandler);
			backgroundColorPicker.addEventListener(ColorPickerEvent.CHANGE, backgroundColorChangeHandler);
			backgroundCheckBox.addEventListener(Event.CHANGE, updateBackgroundColorEnabled);
			fontSizeTextField.restrict = "0-9";
			fontSizeTextField.addEventListener(Event.CHANGE, fontSizeChangeHandler);
			labelTextField.addEventListener(Event.CHANGE, labelChangeHandler);
		}
		override protected function updateProperties():void {
			//trace("RadioButton properties panel updating.");
			updateLocation();
			widthTextField.text = target.width;
			heightTextField.text = target.height;
			foregroundColorPicker.selectedColor = target.foregroundColor;
			backgroundColorPicker.selectedColor = target.backgroundColor;
			backgroundColorPicker.enabled =backgroundCheckBox.selected = target.background;
			fontSizeTextField.text = target.fontSize;
			labelTextField.text = target.label;
		}
		override public function updateLocation():void {
			xTextField.text = target.x;
			yTextField.text = target.y;
		}
		private function updateHeight():void {
			heightTextField.text = target.height;
		}
		private function xChangeHandler(_event:Event):void {
			if (Number(xTextField.text)>(800-target.width)) {
				xTextField.text = (800-target.width).toString();
			}
			target.x = xTextField.text;
		}
		private function yChangeHandler(_event:Event):void {
			if (Number(yTextField.text)>(600-target.height)) {
				yTextField.text = (600-target.height).toString();
			}
			target.y = yTextField.text;
		}
		private function widthChangeHandler(_event:Event):void {
			if (Number(widthTextField.text)>(800-target.x)) {
				widthTextField.text = (800-target.x).toString();
			}
			target.width = widthTextField.text;
			updateHeight();
		}
		private function foregroundColorChangeHandler(_event:ColorPickerEvent):void {
			target.foregroundColor = _event.color;
		}
		private function backgroundColorChangeHandler(_event:ColorPickerEvent):void {
			target.backgroundColor = _event.color;
		}
		private function updateBackgroundColorEnabled(_event:Event):void {
			target.background = backgroundColorPicker.enabled = backgroundCheckBox.selected;
		}
		private function fontSizeChangeHandler(_event:Event):void {
			if (Number(fontSizeTextField.text)>100) {
				fontSizeTextField.text = "100";
			}
			target.fontSize = fontSizeTextField.text;
			updateHeight();
		}
		private function labelChangeHandler(_event:Event):void {
			target.label = labelTextField.text;
			updateHeight();
		}
	}
}