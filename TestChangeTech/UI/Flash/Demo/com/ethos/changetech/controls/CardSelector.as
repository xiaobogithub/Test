package com.ethos.changetech.controls{
	//import fl.controls.*;
	import flash.display.*;
	import flash.errors.IllegalOperationError;
	import flash.events.*;
	import flash.filters.*;
	import flash.text.*;
	//import com.ethos.changetech.models.*;

	public class CardSelector extends Sprite {
		private var _amount:int = 0;
		private var _variables:Array;
		private var _enabled:Boolean = false;
		private var _isSelected:Boolean = false;
		private var _titleDisabledColor:uint = 0x000000;
		private var _unitDisabledColor:uint = 0x000000;
		private var _amountDisabledColor:uint = 0x666666;
		private var _tempEnabledColor:uint = 0x33ccff;

		private var _dropShadowColor:Number = _tempEnabledColor;
		private var _dropShadowAngle:Number = 90;
		private var _dropShadowAlpha:Number = 0.9;
		private var _dropShadowBlurX:Number = 4;
		private var _dropShadowBlurY:Number = 4;
		private var _dropShadowDistance:Number = 0;
		private var _dropShadowStrength:Number = 1;
		private var _dropShadowInner:Boolean = false;
		private var _dropShadowKnockout:Boolean = false;
		private var _dropShadowQuality:Number = BitmapFilterQuality.HIGH;
		private var _dropShadowFilter:DropShadowFilter = new DropShadowFilter(_dropShadowDistance, _dropShadowAngle, _dropShadowColor, _dropShadowAlpha, _dropShadowBlurX, _dropShadowBlurY, _dropShadowStrength, _dropShadowQuality, _dropShadowInner, _dropShadowKnockout);
		private var _cardSelectedFilters:Array = new Array(_dropShadowFilter);

		public function CardSelector() {
			this.mouseChildren = false;

			nameTextField.selectable = false;
			unitTextField.selectable = false;
			amountTextField.selectable = false;
			amountTextField.text = _amount.toString();

			_variables = new Array();
			updateEnabledView();
			updateSelectedView();
		}
		public function get title():String {
			return nameTextField.text;
		}
		public function set title(_value:String):void {
			if (_value == nameTextField.text) {
				return;
			}
			nameTextField.text = _value;
		}
		public function get unit():String {
			return unitTextField.text;
		}
		public function set unit(_value:String):void {
			if (_value == unitTextField.text) {
				return;
			}
			unitTextField.text = _value;
		}
		public function get amount():int {
			return _amount;
		}
		public function set amount(_value:int):void {
			if (_value == _amount) {
				return;
			}
			_amount = _value;
			amountTextField.text = _value.toString();
		}
		public function get variables():Array {
			return _variables;
		}
		public function set variables(_value:Array):void {
			if (_value == variables) {
				return;
			}
			_variables = _value;
			var _sum:int = 0;
			for (var _i:int = 0; _i<_value.length; _i++) {
				_sum += (int)(_value[_i]);
			}
			amount = _sum;
		}
		public function get enabled():Boolean {
			return _enabled;
		}
		public function set enabled(_value:Boolean):void {
			if (_value == _enabled) {
				return;
			}
			_enabled = _value;
			updateEnabledView();
		}
		public function get isSelected():Boolean {
			return _isSelected;
		}
		public function set isSelected(_value:Boolean):void {
			if (_value == _isSelected) {
				return;
			}
			_isSelected = _value;
			updateSelectedView();
		}
		public function setEnabled(_value:String) {
			var _bool:Boolean;
			if (_value=="true"||_value=="True") {
				_bool = true;
			} else if (_value=="false"||_value=="False") {
				_bool = false;
			} else {
				throw new IllegalOperationError("Invalid cardSelector enabled value at card " + title);
			}
			enabled = _bool;
		}
		private function updateEnabledView():void {
			//set all font gray or colorful.
			if (_enabled) {
				nameTextField.textColor = _tempEnabledColor;
				unitTextField.textColor = _tempEnabledColor;
				amountTextField.textColor = _tempEnabledColor;
				mouseOverAni.gotoAndStop(1);
				mouseOverAni.visible = true;
				this.addEventListener(MouseEvent.MOUSE_OVER, enabledCardMouseOverHandler);
				this.addEventListener(MouseEvent.MOUSE_OUT, enabledCardMouseOutHandler);
				this.addEventListener(MouseEvent.CLICK, enabledCardMouseClickedHandler);
			} else {
				nameTextField.textColor = _titleDisabledColor;
				unitTextField.textColor = _unitDisabledColor;
				amountTextField.textColor = _amountDisabledColor;
				mouseOverAni.visible = false;
			}
		}
		private function updateSelectedView():void {

		}
		private function enabledCardMouseOverHandler(_event:MouseEvent):void {
			mouseOverAni.play();
		}
		private function enabledCardMouseOutHandler(_event:MouseEvent):void {
			mouseOverAni.gotoAndStop(1);
		}
		private function enabledCardMouseClickedHandler(_event:MouseEvent):void {
			this.removeEventListener(MouseEvent.MOUSE_OVER, enabledCardMouseOverHandler);
			this.removeEventListener(MouseEvent.MOUSE_OUT, enabledCardMouseOutHandler);
			this.removeEventListener(MouseEvent.CLICK, enabledCardMouseClickedHandler);
			this.filters = _cardSelectedFilters;
			this.dispatchEvent(new CardSelectorEvent("selected"));
		}
	}
}