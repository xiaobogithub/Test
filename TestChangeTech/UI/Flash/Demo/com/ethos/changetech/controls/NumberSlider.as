package com.ethos.changetech.controls{
	import fl.controls.*;
	import fl.transitions.easing.*;
	import fl.transitions.Tween;
	import flash.display.*;
	//import flash.errors.IllegalOperationError;
	import flash.events.*;
	import flash.geom.Rectangle;
	import flash.text.*;
	//import com.ethos.changetech.models.*;

	public class NumberSlider extends Sprite {
		private var _from:int = 1;
		private var _to:int = 7;
		private var _step:int = 1;
		private var _contentWidth:Number = 360;
		private var _textMargin:Number = 5;
		private var _numberTextFields:Array;
		private var _numberFormat:TextFormat;
		private var _titleFormat:TextFormat;

		public function NumberSlider() {
			_numberTextFields = new Array();
			_numberFormat = new TextFormat();
			//_numberFormat.font = "Verdana";
			//format.color = 0xFF0000;
			_numberFormat.size = 12;
			_numberFormat.align = TextFormatAlign.CENTER;

			titleTextField.wordWrap = true;
			titleTextField.autoSize = TextFieldAutoSize.LEFT;
			titleTextField.selectable = false;
			leftLabelTextField.autoSize = TextFieldAutoSize.LEFT;
			leftLabelTextField.selectable = false;
			rightLabelTextField.autoSize = TextFieldAutoSize.RIGHT;
			rightLabelTextField.selectable = false;

			drager.addEventListener(MouseEvent.MOUSE_DOWN, startDragHandler);
			drager.addEventListener(MouseEvent.MOUSE_UP, stopDragHandler);

			updateNumbers();
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
		public function get leftLabel():String {
			return leftLabelTextField.text;
		}
		public function set leftLabel(_value:String):void {
			if (_value == leftLabelTextField.text) {
				return;
			}
			leftLabelTextField.text = _value;
			updateView();
		}
		public function get rightLabel():String {
			return rightLabelTextField.text;
		}
		public function set rightLabel(_value:String):void {
			if (_value == rightLabelTextField.text) {
				return;
			}
			rightLabelTextField.text = _value;
			updateView();
		}
		public function get from():int {
			return _from;
		}
		public function set from(_value:int):void {
			if (_value == _from) {
				return;
			}
			_from = _value;
			updateNumbers();
		}
		public function get to():int {
			return _to;
		}
		public function set to(_value:int):void {
			if (_value == _to) {
				return;
			}
			_to = _value;
			updateNumbers();
		}
		public function get step():int {
			return _step;
		}
		public function set step(_value:int):void {
			if (_value == _step) {
				return;
			}
			_step = _value;
			updateNumbers();
		}
		public function get contentWidth():Number {
			return _contentWidth;
		}
		public function set contentWidth(_value:Number):void {
			if (_value == _contentWidth) {
				return;
			}
			_contentWidth = _value;
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
		private function updateView():void {
			titleTextField.x = 0;
			titleTextField.y = 0;
			titleTextField.width = _contentWidth;

			var _lastYPosition:Number = titleTextField.height;
			var _distance:Number = (_contentWidth - _textMargin * 2 - _numberTextFields.length * 20)/(_numberTextFields.length - 1);
			var _numberTextField:TextField;
			for (var _i:int = 0; _i<_numberTextFields.length; _i++) {
				_numberTextField = _numberTextFields[_i];
				_numberTextField.x = _textMargin + (20+_distance)*_i;
				_numberTextField.y = _lastYPosition;//  + _textMargin;
			}
			drager.x = _textMargin;
			drager.y = _lastYPosition;
			_lastYPosition = _numberTextField.y + _numberTextField.height;

			leftLabelTextField.x = _textMargin;
			leftLabelTextField.y = _lastYPosition;

			rightLabelTextField.x = _contentWidth - _textMargin - rightLabelTextField.width;
			rightLabelTextField.y = _lastYPosition;
		}
		private function updateNumbers():void {
			for (var _i:int = 0; _i<_numberTextFields.length; _i++) {
				removeChild(_numberTextFields[_i]);
			}
			_numberTextFields = new Array();
			var _currentNumber:int = _from;
			do {
				var _numberTextField = new TextField();
				_numberTextField.defaultTextFormat = _numberFormat;
				_numberTextField.selectable = false;
				_numberTextField.width = 20;
				_numberTextField.height = 20;
				_numberTextField.text = _currentNumber.toString();
				_numberTextField.addEventListener(MouseEvent.CLICK, numberClickedHandler);
				_numberTextFields.push(_numberTextField);
				addChild(_numberTextField);
				_currentNumber += _step;
			} while (_currentNumber <= _to);

			addChild(drager);
			updateView();
		}
		private function moveDragerToNearestNumber():void {
			var _minDifference:int = _contentWidth;
			var _nearestNumber:TextField;
			for each (var _numberTextField:TextField in _numberTextFields) {
				if (Math.abs(drager.x - _numberTextField.x)<_minDifference) {
					_minDifference = Math.abs(drager.x - _numberTextField.x);
					_nearestNumber = _numberTextField;
				}
			}
			var _tween:Tween = new Tween(drager, "x", Regular.easeOut, drager.x, _nearestNumber.x, 0.5, true);
		}
		private function startDragHandler(_event:MouseEvent):void {
			trace("mouse down");
			drager.stage.addEventListener(MouseEvent.MOUSE_UP,stopDragHandler);
			drager.startDrag(false, new Rectangle(_textMargin, drager.y, _contentWidth - 2 * _textMargin - 20, 0));
		}
		private function stopDragHandler(_event:MouseEvent):void {
			trace("mouse up");
			drager.stage.removeEventListener(MouseEvent.MOUSE_UP,stopDragHandler);
			drager.stopDrag();
			moveDragerToNearestNumber();
		}
		private function numberClickedHandler(_event:MouseEvent):void{
			var _clickedNumber:TextField = (TextField)(_event.target);
			var _tween:Tween = new Tween(drager, "x", Regular.easeOut, drager.x, _clickedNumber.x, 0.5, true);
		}
	}
}