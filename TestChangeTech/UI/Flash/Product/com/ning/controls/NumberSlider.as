package com.ning.controls{
	import com.ning.text.StaticTextField;
	import fl.data.DataProvider;
	import fl.transitions.easing.*;
	import fl.transitions.Tween;
	import flash.display.MovieClip;
	import flash.display.Sprite;
	import flash.errors.IllegalOperationError;
	import flash.events.Event;
	import flash.events.MouseEvent;
	import flash.filters.BlurFilter;
	import flash.geom.ColorTransform;
	import flash.geom.Rectangle;
	import flash.text.AntiAliasType;
	import flash.text.StyleSheet;
	import flash.text.TextFieldAutoSize;

	public class NumberSlider extends Sprite {

		private var _from:int;
		private var _to:int;
		private var _step:int;
		private var _dataProvider:DataProvider;
		private var _selectedIndex:int;
		private var _selectedNumber:int;
		private var _selectedItem:Object;

		private var _numberTextFields:Array;
		private var _titleTextField:StaticTextField;
		private var _leftLabelTextField:StaticTextField;
		private var _rightLabelTextField:StaticTextField;
		private var _styleSheet:StyleSheet;

		private var _contentWidth:Number = 400;
		private var _actualHeight:Number = 0;
		private var _textMargin:Number = 5;
		private var _primaryThemeColor:uint = 0x000000;
		private var _secondaryThemeColor:uint = 0x666666;
		private var _enableAlphaFilter:BlurFilter;

		public function NumberSlider() {
			trace("TODO:[ning.controls.NumberSlider] Needs to be refactored. Create swf for this component.");
			initProperty();
			initFilter();
			initTextField();
			initDrager();
			layout();
		}
		override public function get width():Number {
			return _contentWidth;
		}
		override public function set width(_value:Number):void {
			contentWidth = _value;
		}
		override public function get height():Number {
			return _actualHeight;
		}
		override public function set height(_value:Number):void {
			throw new IllegalOperationError("Can not set height for com.ning.controls.NumberSlider");
		}
		public function get contentWidth():Number {
			return _contentWidth;
		}
		public function set contentWidth(_value:Number):void {
			if (_value == _contentWidth) {
				return;
			}
			_contentWidth = _value;
			layout();
		}
		public function get textMargin():Number {
			return _textMargin;
		}
		public function set textMargin(_value:Number):void {
			if (_value == _textMargin) {
				return;
			}
			_textMargin = _value;
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
			_titleTextField.styleSheet = _styleSheet;
			_leftLabelTextField.styleSheet = _styleSheet;
			_rightLabelTextField.styleSheet = _styleSheet;
			for each (var _numberTextField:StaticTextField in _numberTextFields) {
				_numberTextField.styleSheet = _styleSheet;
			}
		}
		public function get primaryThemeColor():uint {
			return _primaryThemeColor;
		}
		public function set primaryThemeColor(_value:uint):void {
			if (_value == _primaryThemeColor) {
				return;
			}
			_primaryThemeColor = _value;
			//_titleTextField.textColor = _primaryThemeColor;
			//_leftLabelTextField.textColor = _primaryThemeColor;
			//_rightLabelTextField.textColor = _primaryThemeColor;
			var _colorTransform = new ColorTransform();
			_colorTransform.color = _primaryThemeColor;
			drager_mc.transform.colorTransform = _colorTransform;
		}
		public function get secondaryThemeColor():uint {
			return _secondaryThemeColor;
		}
		public function set secondaryThemeColor(_value:uint):void {
			if (_value == _secondaryThemeColor) {
				return;
			}
			_secondaryThemeColor = _value;
			for each (var _numberTextField:StaticTextField in _numberTextFields) {
				//_numberTextField.textColor = _secondaryThemeColor;
			}
		}
		public function get title():String {
			return _titleTextField.htmlText;
		}
		public function set title(_value:String):void {
			trace("slider title = " + _value);
			if (_value == _titleTextField.htmlText) {
				return;
			}
			_titleTextField.htmlText = _value;
			trace("slider title htmlText = " + _titleTextField.htmlText);
			layout();
		}
		public function get leftLabel():String {
			return _leftLabelTextField.htmlText;
		}
		public function set leftLabel(_value:String):void {
			if (_value == _leftLabelTextField.htmlText) {
				return;
			}
			_leftLabelTextField.htmlText = _value;
			layout();
		}
		public function get rightLabel():String {
			return _rightLabelTextField.htmlText;
		}
		public function set rightLabel(_value:String):void {
			if (_value == _rightLabelTextField.htmlText) {
				return;
			}
			_rightLabelTextField.htmlText = _value;
			layout();
		}
		public function get from():int {
			return _from;
		}
		public function set from(_value:int):void {
			if (_value == _from) {
				return;
			}
			_from = _value;
			if (selectedNumber < _from) {
				_selectedIndex = 0;
			}
			updateDataProvider();
			updateNumberTextFields();
			layout();
		}
		public function get to():int {
			return _to;
		}
		public function set to(_value:int):void {
			if (_value == _to) {
				return;
			}
			_to = _value;
			var _isOutRange:Boolean = false;
			if (selectedNumber > _to) {
				_isOutRange = true;
			}
			updateDataProvider();
			updateNumberTextFields();
			if (_isOutRange) {
				_selectedIndex = _dataProvider.length - 1;
			}
			layout();
		}
		public function get step():int {
			return _step;
		}
		public function set step(_value:int):void {
			if (_value == _step) {
				return;
			}
			_step = _value;
			_selectedIndex = 0;
			updateDataProvider();
			updateNumberTextFields();
			layout();
		}
		public function get dataProvider():DataProvider {
			return _dataProvider;
		}
		public function set dataProvider(_value:DataProvider):void {
			if (_value == _dataProvider) {
				return;
			}
			_value.sortOn("number", Array.NUMERIC);
			trace("[TODO:com.ning.controls.NumberSlider set dataProvider] - validate value.");
			_dataProvider = _value;
			_selectedIndex = 0;
			_from = _dataProvider.getItemAt(0).number;
			_to = _dataProvider.getItemAt(_dataProvider.length - 1).number;
			_step = _to - _from;
			/*for (var _i:int = 0; _i < _dataProvider.length - 1; _i++) {
			_step = Math.max(1, Math.min(_step, _dataProvider.getItemAt(_i + 1).number - _dataProvider.getItemAt(_i).number));
			}
			trace("step = " + _step.toString());
			var _amount = (_to - _from) / _step + 1;
			if ( _amount > _dataProvider.length) {
			var _index:int = 0;
			for (var _number:int = _from; _number <= _to; _number +=_step) {
			if (_number < _dataProvider.getItemAt(_index).number) {
			_dataProvider.addItem({number:_number, enabled:false});
			} else if (_number == _dataProvider.getItemAt(_index).number) {
			_index++;
			} else {
			throw new IllegalOperationError("set dataProvider at [com.ning.controls.NumberSlider] - Invalid number " + _number.toString() + ".");
			}
			}
			_dataProvider.sortOn("number", Array.NUMERIC);
			}*/
			updateNumberTextFields();
			layout();
		}
		public function get selectedIndex():int {
			return _selectedIndex;
		}
		public function set selectedIndex(_value:int):void {
			if (_value == _selectedIndex) {
				return;
			}
			_selectedIndex = _value;
			layout();
		}
		public function get selectedNumber():int {
			return _dataProvider.getItemAt(_selectedIndex).number;
		}
		public function set selectedNumber(_value:int) {
			if (_value == _dataProvider.getItemAt(_selectedIndex).number) {
				return;
			}
			for (var _i:int = 0; _i < _dataProvider.length; _i++) {
				if(Number(_dataProvider.getItemAt(_i).number) == _value) {
					_selectedIndex = _i;
					layout();
					return;
				}
			}
			/*if (_value < _from || _value > _to) {
				throw new IllegalOperationError("set selectedNumber at [com.ning.controls.NumberSlider] - value out of range.");
			}
			var _index:int = (_value - _from) / _step;
			if (_value != _dataProvider.getItemAt(_index).number) {
				throw new IllegalOperationError("set selectedNumber at [com.ning.controls.NumberSlider] - Invalid value.");
			}
			_selectedIndex = _index;
			layout();*/
			throw new IllegalOperationError("set selectedNumber at [com.ning.controls.NumberSlider] - Invalid selectedNumber value.");
		}
		public function get selectedItem():Object {
			return _dataProvider.getItemAt(_selectedIndex);
		}
		public function set selectedItem(_value:Object):void {
			if (_value == _dataProvider.getItemAt(_selectedIndex)) {
				return;
			}
			var _index:int = -1;
			for (var _i:int = 0; _i < _dataProvider.length; _i++) {
				if (_value == _dataProvider.getItemAt(_i)) {
					_index = _i;
				}
			}
			if (_index == -1) {
				throw new IllegalOperationError("set selectedItem at [com.ning.controls.NumberSlider] - value out of range.");
			}
			_selectedIndex = _index;
		}
		private function initProperty():void {
			_selectedIndex = 0;
			_from = 1;
			_to = 7;
			_step = 1;
			updateDataProvider();
		}
		private function updateDataProvider():void {
			_dataProvider = new DataProvider();
			for (var _i:int = _from; _i <= _to; _i += _step) {
				_dataProvider.addItem({number: _i, enabled:true});
			}
		}
		private function initFilter():void {
			_enableAlphaFilter = new BlurFilter(0,0,0);
		}
		private function initTextField():void {
			_numberTextFields = new Array();
			updateNumberTextFields();

			_titleTextField = new StaticTextField(TextFieldAutoSize.LEFT, true, true, AntiAliasType.ADVANCED);
			//_titleTextField.textColor = _primaryThemeColor;
			_titleTextField.styleSheet = _styleSheet;
			addChild(_titleTextField);

			_leftLabelTextField = new StaticTextField(TextFieldAutoSize.LEFT, false, true, AntiAliasType.ADVANCED);
			//_leftLabelTextField.textColor = _primaryThemeColor;
			_leftLabelTextField.styleSheet = _styleSheet;
			addChild(_leftLabelTextField);

			_rightLabelTextField = new StaticTextField(TextFieldAutoSize.LEFT, false, true, AntiAliasType.ADVANCED);
			//_rightLabelTextField.textColor = _primaryThemeColor;
			_rightLabelTextField.styleSheet = _styleSheet;
			addChild(_rightLabelTextField);
		}
		private function updateNumberTextFields():void {
			for (var _i:int = 0; _i<_numberTextFields.length; _i++) {
				if (contains(_numberTextFields[_i])) {
					removeChild(_numberTextFields[_i]);
				}
			}
			_numberTextFields = new Array();
			for each (var _item:Object in _dataProvider.toArray()) {
				var _numberTextField = new StaticTextField(TextFieldAutoSize.NONE, false, true, AntiAliasType.ADVANCED);
				//_numberTextField.textColor = _secondaryThemeColor;
				_numberTextField.styleSheet = _styleSheet;
				_numberTextField.htmlText = "<sliderNumber>" + _item.number.toString() + "</sliderNumber>";
				_numberTextField.width = 20;
				_numberTextField.height = 20;
				_numberTextField.filters = [_enableAlphaFilter];
				if (_item.enabled) {
					_numberTextField.alpha = 1;
					_numberTextField.addEventListener(MouseEvent.CLICK, numberClickedHandler);
				} else {
					_numberTextField.alpha = 0.5;
				}
				_item.control = _numberTextField;
				_numberTextFields.push(_numberTextField);
				addChild(_numberTextField);
			}
		}
		private function initDrager():void {
			drager_mc.addEventListener(MouseEvent.MOUSE_DOWN, startDragHandler);
			drager_mc.addEventListener(MouseEvent.MOUSE_UP, stopDragHandler);
			addChild(drager_mc);
		}
		private function moveDrager():void {
			var _tween:Tween = new Tween(drager_mc, "x", Regular.easeOut, drager_mc.x, _numberTextFields[_selectedIndex].x, 0.5, true);
		}
		private function layout():void {
			var _lastPositionY:Number = 0;
			_titleTextField.x = 0;
			_titleTextField.y = 0;
			_titleTextField.width = _contentWidth;
			if (_titleTextField.htmlText.length > 0) {
				_lastPositionY = _titleTextField.height;
			}
			var _distance:Number = (_contentWidth - _textMargin * 2 - _numberTextFields.length * 20)/(_numberTextFields.length - 1);
			var _numberTextField:StaticTextField;
			for (var _i:int = 0; _i<_numberTextFields.length; _i++) {
				_numberTextField = _numberTextFields[_i];
				_numberTextField.x = _textMargin + (20+_distance)*_i;
				_numberTextField.y = _lastPositionY;
			}
			removeChild(drager_mc);
			drager_mc.x = _numberTextFields[_selectedIndex].x;
			drager_mc.y = _lastPositionY;
			addChild(drager_mc);
			_lastPositionY = drager_mc.y + drager_mc.height;

			_leftLabelTextField.x = 0;
			_leftLabelTextField.y = _lastPositionY;
			_rightLabelTextField.x = _contentWidth - _rightLabelTextField.textWidth;
			_rightLabelTextField.y = _lastPositionY;
			_lastPositionY = _rightLabelTextField.y + Math.max(_rightLabelTextField.height, _leftLabelTextField.height);

			_actualHeight = _lastPositionY;
		}
		private function dispatchChangeEvent():void {
			this.dispatchEvent(new Event("change"));
		}
		private function moveDragerToNearestNumber():void {
			var _previousSelectedIndex = _selectedIndex;
			var _minDifference:int = _contentWidth;
			var _nearestNumber:StaticTextField;
			for (var _i:int = 0; _i < _dataProvider.length; _i++) {
				if (_dataProvider.getItemAt(_i).enabled == false) {
					continue;
				}
				if (Math.abs(drager_mc.x - _numberTextFields[_i].x)<_minDifference) {
					_minDifference = Math.abs(drager_mc.x - _numberTextFields[_i].x);
					_nearestNumber = _numberTextFields[_i];
					_selectedIndex = _i;
				}
			}
			if (_selectedIndex != _previousSelectedIndex) {
				dispatchChangeEvent();
			}
			moveDrager();
		}
		private function startDragHandler(_event:MouseEvent):void {
			drager_mc.stage.addEventListener(MouseEvent.MOUSE_UP,stopDragHandler);
			drager_mc.startDrag(false, new Rectangle(_textMargin, drager_mc.y, _contentWidth - 2 * _textMargin - 20, 0));
		}
		private function stopDragHandler(_event:MouseEvent):void {
			drager_mc.stage.removeEventListener(MouseEvent.MOUSE_UP,stopDragHandler);
			drager_mc.stopDrag();
			moveDragerToNearestNumber();
		}
		private function numberClickedHandler(_event:MouseEvent):void {
			var _previousSelectedIndex = _selectedIndex;
			var _clickedNumber:StaticTextField = StaticTextField(_event.target);
			_selectedIndex = _numberTextFields.indexOf(_clickedNumber);
			if (_selectedIndex != _previousSelectedIndex) {
				dispatchChangeEvent();
			}
			moveDrager();
		}
	}
}