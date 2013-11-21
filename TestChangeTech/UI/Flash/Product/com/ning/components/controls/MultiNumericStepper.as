package com.ning.components.controls{
	import com.hexagonstar.util.debug.Debug;
	import com.ning.display.ColorableSprite;
	import com.ning.math.Range;
	import com.ning.text.StaticTextField;
	import fl.controls.NumericStepper;
	import fl.data.DataProvider;
	import flash.errors.IllegalOperationError;
	import flash.events.Event;
	import flash.external.ExternalInterface;
	import flash.filters.BlurFilter;
	import flash.text.AntiAliasType;
	import flash.text.StyleSheet;
	import flash.text.TextFieldAutoSize;

	public class MultiNumericStepper extends ColorableSprite {

		private var _dataProvider:DataProvider;
		private var _labels:Array;
		private var _numericSteppers:Array;
		private var _enableAlphaFilter:BlurFilter;

		private var _actualWidth:Number = 0;
		private var _actualHeight:Number = 0;

		private var _styleSheet:StyleSheet;
		private var _spacing:Number = 5;
		private var _delimiter:String = ":";

		public function MultiNumericStepper() {
			initial();
			updateNumericSteppers();
			layout();
		}
		override public function get width():Number {
			return _actualWidth;
		}
		override public function get height():Number {
			return _actualHeight;
		}
		public function get spacing():Number {
			return _spacing;
		}
		public function set spacing(_value:Number):void {
			if (_value == _spacing) {
				return;
			}
			_spacing = _value;
			layout();
		}
		public function get styleSheet():StyleSheet {
			return _styleSheet;
		}
		public function set styleSheet(_value:StyleSheet):void {
			if (_value == _styleSheet) {
				return;
			}
			_styleSheet = _value;
			for each (var _label:StaticTextField in _labels) {
				_label.styleSheet = _styleSheet;
			}
			layout();
		}
		public function get delimiter():String {
			return _delimiter;
		}
		public function set delimiter(_value:String):void {
			if (_value == _delimiter) {
				return;
			}
			_delimiter = _value;
		}
		public function get dataProvider():DataProvider {
			return _dataProvider;
		}
		public function set dataProvider(_value:DataProvider):void {
			if (_value == _dataProvider) {
				return;
			}
			_dataProvider = _value;
			updateNumericSteppers();
			layout();
		}
		public function get value():String {
			//var _value:String = "";
			//for(var _i:int = 0; _i < _numericSteppers.length; _i++) {
				//if(_value.length > 0){
					//_value = _value + delimiter;
				//}
				//_value = _value + _numericSteppers[_i].value.toString();
			//}			
			return (_numericSteppers[0].value * 60 + _numericSteppers[1].value).toString();
		}
		public function set value(_value:String) {
			var _number = Number(_value);
			if (_number == value) {
				return;
			}
			_numericSteppers[0].value = Math.floor(_number / 60);
			_numericSteppers[1].value = Math.floor(_number % 60);
		}
		/*override protected function updateSecondaryThemeColor():void {
			for each (var _label:StaticTextField in _labels) {
				_label.textColor = _secondaryThemeColor;
			}
		}*/
		private function initial():void {
			_dataProvider = new DataProvider();
			_dataProvider.addItem({label: "NumericStepper1", range:new Range(0,99)});
			_labels = new Array();
			_numericSteppers = new Array();
			_enableAlphaFilter = new BlurFilter(0,0,0);
		}
		private function updateNumericSteppers():void {
			for (var _i:int = 0; _i<_labels.length; _i++) {
				if (contains(_labels[_i])) {
					removeChild(_labels[_i]);
				}
			}
			for (var _i:int = 0; _i<_numericSteppers.length; _i++) {
				if (contains(_numericSteppers[_i])) {
					removeChild(_numericSteppers[_i]);
				}
			}
			_labels = new Array();
			_numericSteppers = new Array();
			
			for each (var _item:Object in _dataProvider.toArray()) {
				
				
				var _label = new StaticTextField(TextFieldAutoSize.LEFT, false, true, AntiAliasType.ADVANCED);
				//_label.textColor = _secondaryThemeColor;
				_label.styleSheet = _styleSheet;
				_label.htmlText = "<timePickerLabel>" + _item.label + "</timePickerLabel>";
				_labels.push(_label);
				addChild(_label);
				
				var _numericStepper:NumericStepper = new NumericStepper();
				_numericStepper.textField.filters = [_enableAlphaFilter];
				_numericStepper.stepSize = 1;
				_numericStepper.minimum = _item.range.min;
				_numericStepper.maximum = _item.range.max;
				_numericStepper.textField.maxChars = _item.range.max.toString().length;
				if (_item.enabled) {
					_numericStepper.addEventListener(Event.CHANGE, numericStepperChangeHandler);
				}
				else
				{
					_numericStepper.enabled = false;
				}
				_numericSteppers.push(_numericStepper);
				addChild(_numericStepper);
			}
			
		}
		private function layout():void {
			if(_labels.length != _numericSteppers.length) {
				throw new IllegalOperationError("Error in MultiNumericStepper, the amount of labels and numericSteppers are not match!");
			}
			var _lastPositionX:Number = 0;
			_actualHeight = 0;
			for (var _i:int = 0; _i < _numericSteppers.length; _i++) {				
				_numericSteppers[_i].x = _lastPositionX;
				_numericSteppers[_i].width = 45;
				_lastPositionX = _numericSteppers[_i].x + _numericSteppers[_i].width + _spacing;
				_actualHeight = Math.max(_actualHeight, _numericSteppers[_i].height);
				_labels[_i].x = _lastPositionX;
				_lastPositionX = _labels[_i].x + _labels[_i].textWidth + _spacing;
				_actualHeight = Math.max(_actualHeight, _labels[_i].height);
			}
			_actualWidth = _lastPositionX - _spacing;
			for (var _j:int = 0; _j < _numericSteppers.length; _j++) {
				_numericSteppers[_j].y = (_actualHeight - _numericSteppers[_j].height) / 2;
				_labels[_j].y = (_actualHeight - _labels[_j].height) / 2;
			}
		}
		private function numericStepperChangeHandler(_event:Event):void {
			this.dispatchEvent(new Event("change"));
		}
	}
}