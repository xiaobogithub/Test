package com.ethos.changetech.controls{
	import flash.display.*;
	import flash.events.*;
	import flash.text.*;
	import com.ethos.changetech.models.*;

	public class PlainPageContainer extends Sprite {
		private var _page:Page;
		private var _contentPadding:Number =13;
		private var _textMargin:Number = 5;
		private var _contentWidth:Number = 360;

		public function PlainPageContainer() {
			bodyTitleTextField.selectable = false;
			bodyTitleTextField.autoSize = TextFieldAutoSize.LEFT;
			bodyTitleTextField.wordWrap = true;
			bodyTextTextField.selectable = false;
			bodyTextTextField.autoSize = TextFieldAutoSize.LEFT;
			bodyTextTextField.wordWrap = true;
			bodyPanel.addEventListener(PageEvent.NEXTPAGE, nextPageHandler);
			bodyPanel.addEventListener(PageEvent.PREVIOUSPAGE, previousPageHandler);
		}
		public function get page():Page {
			return _page;
		}
		public function set page(_value:Page):void {
			if (_value == _page) {
				return;
			}
			_page = _value;

			bodyTitleTextField.text = _value.bodyTitle;
			bodyTextTextField.text = _value.bodyText;
			bodyPanel.buttonPrimaryCaption = _value.buttonPrimaryCaption;
			//_bodyPanel.buttonSecondaryCaption = _value.buttonSecondaryCaption;
			bodyPanel.buttonPrimaryAction = _value.buttonPrimaryAction;
			bodyPanel.buttonSecondaryAction = _value.buttonSecondaryAction;
			updateView();
		}
		public function get contentPadding():Number {
			return _contentPadding;
		}
		public function set contentPadding(_value:Number):void {
			if (_value == _contentPadding) {
				return;
			}
			_contentPadding = _value;
			updateView();
		}
		public function get textMargin():Number {
			return _textMargin;
		}
		public function set textMargin(_value:Number):void {
			if (_value == _textMargin) {
				return;
			}
			_textMargin = _value;
			updateView();
		}
		public function get contentWidth():Number {
			return _contentWidth;
		}
		public function set contentWidth(_value:Number):void {
			if (_value ==_contentWidth) {
				return;
			}
			_contentWidth = _value;
			updateView();
		}
		protected function updateView():void {
			bodyTitleTextField.x = _contentPadding;
			bodyTitleTextField.y = _contentPadding;
			bodyTitleTextField.width = _contentWidth;

			bodyTextTextField.x = _contentPadding;
			bodyTextTextField.y = bodyTitleTextField.y+bodyTitleTextField.height+_textMargin;
			bodyTextTextField.width = _contentWidth;

			bodyPanel.contentTopPadding = _contentPadding;
			bodyPanel.contentBottomPadding = _contentPadding;
			bodyPanel.contentLeftPadding = _contentPadding;
			bodyPanel.contentRightPadding = _contentPadding;
			bodyPanel.contentHeight = bodyTitleTextField.height+_textMargin+bodyTextTextField.height;
			bodyPanel.contentWidth = _contentWidth;
		}
		private function nextPageHandler(_event:PageEvent):void {
			this.dispatchEvent(new PageEvent("nextpage"));
		}
		private function previousPageHandler(_event:PageEvent):void {
			this.dispatchEvent(new PageEvent("previouspage"));
		}
	}
}