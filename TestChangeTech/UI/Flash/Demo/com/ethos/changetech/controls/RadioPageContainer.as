package com.ethos.changetech.controls{
	import fl.controls.*;
	//import flash.display.*;
	import flash.errors.IllegalOperationError;
	import flash.events.*;
	//import flash.text.*;
	import com.ethos.changetech.models.*;

	public class RadioPageContainer extends PlainPageContainer {

		private var _group:RadioButtonGroup = new RadioButtonGroup("question");

		override public function set page(_value:Page):void {
			if (_value == super.page) {
				return;
			}
			super.page = _value;
			createRadioButtons();
			updateView();
		}
		private function createRadioButtons():void {
			if (page.questions.length>1) {
				throw new IllegalOperationError("Not support more than one radio question in demo version - at page sequence No." + page.pageSequence.pageSequenceOrderNo.toString() + " page No." + page.pageOrderNo.toString());
			}
			var _radioButtonLabels:Array = page.questions[0].answerAlternatives.split(';');
			for (var _i:int = 0; _i<_radioButtonLabels.length; _i++) {
				var _radioButton:RadioButton = new RadioButton();
				_radioButton.group = _group;
				_radioButton.label = _radioButtonLabels[_i];
				_radioButton.addEventListener(MouseEvent.CLICK, radioButtonClicked);
				addChild(_radioButton);
			}
		}
		override protected function updateView():void {
			super.updateView();
			var _lastYPosition = bodyTextTextField.y + bodyTextTextField.height;
			var _radioButton:RadioButton;
			for (var _i:int = 0; _i<_group.numRadioButtons; _i++) {
				_radioButton = _group.getRadioButtonAt(_i);
				_radioButton.width = contentWidth;
				_radioButton.x = bodyTextTextField.x;
				_radioButton.y =_lastYPosition  + textMargin;
				_lastYPosition += textMargin + _radioButton.height;
			}			
			bodyPanel.contentHeight = _lastYPosition - bodyTitleTextField.y;
		}
		private function radioButtonClicked(_event:MouseEvent):void {
			if (_group.selection == null) {
				return;
			}
			//TODO:
		}
	}
}