package com.ethos.changetech.controls{
	import flash.display.*;
	import flash.errors.IllegalOperationError;
	import flash.events.*;

	public class PageContainerBodyPanel extends Sprite {
		private static var _buttonOverlapX:Number = 240;
		private static var _buttonOverlapY:Number = 30;

		private var _contentTopPadding:Number;
		private var _contentBottomPadding:Number;
		private var _contentLeftPadding:Number;
		private var _contentRightPadding:Number;
		private var _contentWidth:Number;
		private var _contentHeight:Number;

		private var _buttonPrimaryCaption:String;
		private var _buttonSecondaryCaption:String;
		private var _buttonPrimaryAction:int = 1;
		private var _buttonSecondaryAction:int = -1;

		public function PageContainerBodyPanel() {
			primaryButton.addEventListener(MouseEvent.CLICK, primaryButtonClicked);
			secondaryButton.addEventListener(MouseEvent.CLICK, secondaryButtonClicked);
		}

		public function get contentTopPadding():Number {
			return _contentTopPadding;
		}
		public function set contentTopPadding(_value:Number):void {
			if (_value == _contentTopPadding) {
				return;
			}
			_contentTopPadding = _value;
			updateHeight();
		}
		public function get contentBottomPadding():Number {
			return _contentBottomPadding;
		}
		public function set contentBottomPadding(_value:Number):void {
			if (_value== _contentBottomPadding) {
				return;
			}
			_contentBottomPadding = _value;
			updateHeight();
		}
		public function get contentLeftPadding():Number {
			return _contentLeftPadding;
		}
		public function set contentLeftPadding(_value:Number):void {
			if (_value == _contentLeftPadding) {
				return;
			}
			_contentLeftPadding = _value;
			updateWidth();
		}
		public function get contentRightPadding():Number {
			return _contentRightPadding;
		}
		public function set contentRightPadding(_value:Number):void {
			if (_value == _contentRightPadding) {
				return;
			}
			_contentRightPadding =_value;
			updateWidth();
		}
		public function get contentWidth():Number {
			return _contentWidth;
		}
		public function set contentWidth(_value:Number):void {
			if (_value == _contentWidth) {
				return;
			}
			_contentWidth = _value;
			updateWidth();
		}
		public function get contentHeight():Number {
			return _contentHeight;
		}
		public function set contentHeight(_value:Number):void {
			if (_value == _contentHeight) {
				return;
			}
			_contentHeight = _value;
			updateHeight();
		}
		public function get buttonPrimaryCaption():String {
			return primaryButton.caption;
		}
		public function set buttonPrimaryCaption(_value:String):void {
			if (_value == primaryButton.caption) {
				return;
			}
			primaryButton.caption = _value;
		}
		public function get buttonPrimaryAction():int {
			return _buttonPrimaryAction;
		}
		public function set buttonPrimaryAction(_value:int):void {
			if (_value == _buttonPrimaryAction) {
				return;
			}
			_buttonPrimaryAction = _value;
			primaryButton.removeEventListener(MouseEvent.CLICK, primaryButtonClicked);
			primaryButton.addEventListener(MouseEvent.CLICK, primaryButtonClicked);
		}
		public function get buttonSecondaryAction():int {
			return _buttonSecondaryAction;
		}
		public function set buttonSecondaryAction(_value:int):void {
			if (_value == _buttonSecondaryAction) {
				return;
			}
			_buttonSecondaryAction = _value;
			secondaryButton.removeEventListener(MouseEvent.CLICK, secondaryButtonClicked);
			secondaryButton.addEventListener(MouseEvent.CLICK, secondaryButtonClicked);
		}
		private function updateWidth():void{
			bodyBg.width = _contentLeftPadding + _contentWidth + _contentRightPadding;
			secondaryButton.x = bodyBg.width - _buttonOverlapX;
			primaryButton.x = secondaryButton.x+secondaryButton.width;
		}
		private function updateHeight():void {
			bodyBg.height = _contentTopPadding + _contentHeight + _contentBottomPadding+_buttonOverlapY;
			primaryButton.y = bodyBg.height-_buttonOverlapY;
			secondaryButton.y = bodyBg.height-_buttonOverlapY;
		}
		private function primaryButtonClicked(_event:MouseEvent):void {
			buttonAction(_buttonPrimaryAction);
		}
		private function secondaryButtonClicked(_event:MouseEvent):void {
			buttonAction(_buttonSecondaryAction);
		}
		private function buttonAction(_action:int):void {
			switch (_action) {
				case 0 :
					return;
				case 1 :
					this.dispatchEvent(new PageEvent("nextpage"));
					break;
				case -1 :
					this.dispatchEvent(new PageEvent("previouspage"));
					break;
				default :
					throw new IllegalOperationError("Invalid action type at page container body panel. action: " + _action.toString() + ".");

			}
		}
	}
}