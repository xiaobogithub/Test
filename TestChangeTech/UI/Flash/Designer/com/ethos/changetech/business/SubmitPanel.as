package com.ethos.changetech.business{
	import fl.controls.*;
	import flash.display.Sprite;
	import flash.events.MouseEvent;
	import flash.text.*;
	import com.ethos.changetech.controls.*;

	public class SubmitPanel extends Sprite {
		//refactor this as SimpleMessagePanel,
		//create all the uielement in code.
		//dynamic message size and location according the text.
		//dynamic button label.
		//button event.
		public function get message():String {
			return messageTextField.text;
		}
		public function set message(_value:String):void {
			if (_value == messageTextField.text) {
				return;
			}
			messageTextField.text = _value;
			updateView();
		}
		public function SubmitPanel() {
			setupMessageTextField();
			setupButton();
		}
		private function setupMessageTextField():void {
			messageTextField.autoSize = TextFieldAutoSize.LEFT;
			messageTextField.border = false;
			messageTextField.borderColor = 0x999999;
			messageTextField.selectable = false;
			messageTextField.wordWrap = true;
		}
		private function setupButton():void {
			okButton.addEventListener(MouseEvent.CLICK, okClickedHandler);
		}
		private function updateView():void {
			okButton.y = messageTextField.y + messageTextField.height + 50;
		}
		private function okClickedHandler(_event:MouseEvent):void{
			this.dispatchEvent(new PanelEvent("close"));
		}
	}
}