package com.ethos.changetech.controls{
	import fl.transitions.easing.*;
	import fl.transitions.Tween;
	import flash.display.*;
	import flash.errors.IllegalOperationError;
	import flash.events.*;
	import flash.geom.Rectangle;
	import flash.text.*;

	public class CardSlider extends Sprite {
		private var _numberWidth:Number = 35;
		private var _from:int = 0;
		private var _to:int = 11;
		private var _step:int = 1;
		private var _contentWidth:Number = 300;
		private var _textMargin:Number = 5;
		private var _numberTextFields:Array;
		private var _numberFormat:TextFormat;
		private var _titleFormat:TextFormat;

		public function CardSlider() {
			_numberTextFields = new Array();
			_numberFormat = new TextFormat();
			//_numberFormat.font = "Verdana";
			_numberFormat.color = 0x33ccff;
			_numberFormat.size = 12;
			_numberFormat.align = TextFormatAlign.CENTER;

			titleTextField.wordWrap = true;
			titleTextField.autoSize = TextFieldAutoSize.LEFT;
			titleTextField.selectable = false;
			//leftLabelTextField.autoSize = TextFieldAutoSize.LEFT;
			//leftLabelTextField.selectable = false;
			//rightLabelTextField.autoSize = TextFieldAutoSize.RIGHT;
			//rightLabelTextField.selectable = false;

			drager.addEventListener(MouseEvent.MOUSE_DOWN, startDragHandler);
			drager.addEventListener(MouseEvent.MOUSE_UP, stopDragHandler);

			numberContainer.mask = numberMask;

			updateNumbers();
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
		//public function get leftLabel():String {
		//return leftLabelTextField.text;
		//}
		public function set leftLabel(_value:String):void {
			return;
			//if (_value == leftLabelTextField.text) {
			//return;
			//}
			//leftLabelTextField.text = _value;
			//updateView();
		}
		//public function get rightLabel():String {
		//return rightLabelTextField.text;
		//}
		public function set rightLabel(_value:String):void {
			return;
			//if (_value == rightLabelTextField.text) {
			//return;
			//}
			//rightLabelTextField.text = _value;
			//updateView();
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
		private function updateNumbers():void {
			for (var _i:int = 0; _i<_numberTextFields.length; _i++) {
				numberContainer.removeChild(_numberTextFields.pop());
			}
			var _currentNumber:int = _from;
			do {
				var _numberTextField = new TextField();
				_numberTextField.defaultTextFormat = _numberFormat;
				_numberTextField.selectable = false;
				_numberTextField.width = _numberWidth;
				_numberTextField.height = 20;
				_numberTextField.text = _currentNumber.toString();
				_numberTextField.addEventListener(MouseEvent.CLICK, numberClickedHandler);
				_numberTextFields.push(_numberTextField);
				numberContainer.addChild(_numberTextField);
				_currentNumber += _step;
			} while (_currentNumber <= _to);
		}
		private function updateView():void {
			titleTextField.x = 0;
			titleTextField.y = 0;
			titleTextField.width = _contentWidth;

			//var _distance:Number = (_contentWidth - _textMargin * 2 - _numberTextFields.length * 20)/(_numberTextFields.length - 1);
			var _numberTextField:TextField;
			for (var _i:int = 0; _i<_numberTextFields.length; _i++) {
				_numberTextField = _numberTextFields[_i];
				_numberTextField.x = _numberWidth*_i;
				_numberTextField.y = (bg.height - _numberTextField.height)/2;
			}
			bg.width = contentWidth;
			numberMask.width = contentWidth;
		}
		private function getNumberPosition(_dragerPosition:Number):Number {
			var _position:Number = _dragerPosition - _dragerPosition / (contentWidth - _numberWidth) * (numberContainer.width - _numberWidth);
			return _position;
		}
		private function getDragerPositionByIndex(_index:int):Number {
			var _position:Number = (contentWidth - _numberWidth) / (_numberTextFields.length - 1) * _index;
			return _position;
		}
		private function moveDragerToNearestNumber():void {
			var _minDifference:Number = contentWidth;
			var _dragerPosition:Number;
			var _nearestNumber:TextField;
			var _nearestDragerPosition:Number;
			for (var _i:int= 0; _i<_numberTextFields.length; _i++) {
				_dragerPosition = getDragerPositionByIndex(_i);
				if ( Math.abs(_dragerPosition - drager.x)< _minDifference) {
					_minDifference = Math.abs(_dragerPosition - drager.x);
					_nearestNumber = _numberTextFields[_i];
					_nearestDragerPosition = _dragerPosition;
				}
			}
			var _nearestNumberPosition:Number = getNumberPosition(_nearestDragerPosition);
			var _dragerTween:Tween = new Tween(drager, "x", Regular.easeOut, drager.x, _nearestDragerPosition, 0.3, true);
			var _numberTween:Tween = new Tween(numberContainer, "x", Regular.easeOut, numberContainer.x, _nearestNumberPosition, 0.3, true);

		}
		private function startDragHandler(_event:MouseEvent):void {
			drager.stage.addEventListener(MouseEvent.MOUSE_UP,stopDragHandler);
			drager.stage.addEventListener(MouseEvent.MOUSE_MOVE, dragMouseMoveHandler);
			drager.startDrag(false, new Rectangle(0, drager.y, _contentWidth - _numberWidth, 0));
		}
		private function stopDragHandler(_event:MouseEvent):void {
			drager.stage.removeEventListener(MouseEvent.MOUSE_UP,stopDragHandler);
			drager.stage.removeEventListener(MouseEvent.MOUSE_MOVE, dragMouseMoveHandler);
			drager.stopDrag();
			moveDragerToNearestNumber();
		}
		private function dragMouseMoveHandler(_event:MouseEvent):void {
			var _numberPosition:Number = getNumberPosition(drager.x);
			var _tween:Tween = new Tween(numberContainer, "x", Regular.easeOut, numberContainer.x, _numberPosition, 0.1, true);
		}
		private function numberClickedHandler(_event:MouseEvent):void {
			var _clickedNumber:TextField = (TextField)(_event.target);
			var _dragerPosition:Number = getDragerPositionByIndex(_numberTextFields.indexOf(_clickedNumber));
			var _numberPosition:Number = getNumberPosition(_dragerPosition);
			var _dragerTween:Tween = new Tween(drager, "x", Regular.easeOut, drager.x, _dragerPosition, 0.3, true);
			var _numberTween:Tween = new Tween(numberContainer, "x", Regular.easeOut, numberContainer.x, _numberPosition, 0.3, true);
		}
	}
}