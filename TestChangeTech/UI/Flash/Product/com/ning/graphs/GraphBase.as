package com.ning.graphs{
	import com.ning.math.Range;
	import com.ning.text.StaticTextField;
	import fl.data.DataProvider;
	import flash.display.Graphics;
	import flash.display.Shape;
	import flash.display.Sprite;
	import flash.text.AntiAliasType;
	import flash.text.StyleSheet;
	import flash.text.TextFieldAutoSize;

	public class GraphBase extends Sprite {

		protected var _margin:Number = 15;
		protected var _horizontalAxisLabelWidth:Number = 0;
		protected var _verticalAxisLabelHeight:Number = 0;
		protected var _maxWidth:Number = 0;
		protected var _maxHeight:Number = 0;
		protected var _actualWidth:Number;
		protected var _actualHeight:Number;
		protected var _maxSymbolWidth:Number = 0;
		protected var _symbolWidth:Number;
		protected var _symbolHeight:Number;
		protected var _styleSheet:StyleSheet;

		protected var _caption:String;
		protected var _horizontalAxisRange:Range;
		protected var _verticalAxisRange:Range;
		protected var _horizontalAxisUnit:String;
		protected var _verticalAxisUnit:String;
		protected var _verticalAxisBaseUnit:String;
		protected var _horizontalAxisStep:Number;
		protected var _verticalAxisStep:Number;

		protected var _captionTextField:StaticTextField;
		protected var _graphContainer:Sprite;
		protected var _graphBackgroundContainer:Sprite;
		protected var _demoContainer:Sprite;
		protected var _horizontalAxisArray:Array;
		protected var _horizontalAxisLabelArray:Array;
		protected var _verticalAxisArray:Array;
		protected var _verticalAxisLabelArray:Array;
		protected var _horizontalRangeBackgroundArray:Array = new Array();

		protected var _dataProvider:DataProvider;

		protected var _hasSymbol:Boolean = true;

		public function GraphBase() {
			_dataProvider = new DataProvider();
			initCaption();
			initContainer();
		}
		public function get margin():Number {
			return _margin;
		}
		public function set margin(_value:Number):void {
			if (_value == _margin) {
				return;
			}
			_margin = _value;
		}
		public function get maxWidth():Number {
			return _maxWidth;
		}
		public function set maxWidth(_value:Number):void {
			if (_value == _maxWidth) {
				return;
			}
			_maxWidth=_value;
		}
		public function get maxHeight():Number {
			return _maxHeight;
		}
		public function set maxHeight(_value:Number):void {
			if (_value == _maxHeight) {
				return;
			}
			_maxHeight = _value;
		}
		public function get actualWidth():Number {
			return _actualWidth;
		}
		public function get actualHeight():Number {
			return _actualHeight;
		}
		public function get maxSymbolWidth():Number {
			return _maxSymbolWidth;
		}
		public function set maxSymbolWidth(_value:Number):void {
			if (_value == _maxSymbolWidth) {
				return;
			}
			_maxSymbolWidth = _value;
		}
		public function get styleSheet():StyleSheet {
			return _styleSheet;
		}
		public function set styleSheet(_value:StyleSheet):void {
			if (_value == _styleSheet) {
				return;
			}
			_styleSheet = _value;
			_captionTextField.styleSheet = _styleSheet;
		}
		public function get caption():String {
			return _caption;
		}
		public function set caption(_value:String):void {
			if (_value == _caption) {
				return;
			}
			_caption = _value;
		}
		public function get horizontalAxisRange():Range {
			return _horizontalAxisRange;
		}
		public function set horizontalAxisRange(_value:Range):void {
			if (_value == _horizontalAxisRange) {
				return;
			}
			_horizontalAxisRange = _value;
		}
		public function get verticalAxisRange():Range {
			return _verticalAxisRange;
		}
		public function set verticalAxisRange(_value:Range):void {
			if (_value == _verticalAxisRange) {
				return;
			}
			_verticalAxisRange = _value;
		}
		public function get horizontalAxisUnit():String {
			return _horizontalAxisUnit;
		}
		public function set horizontalAxisUnit(_value:String):void {
			if (_value == _horizontalAxisUnit) {
				return;
			}
			_horizontalAxisUnit = _value;
		}
		public function get verticalAxisUnit():String {
			return _verticalAxisUnit;
		}
		public function set verticalAxisUnit(_value:String):void {
			if (_value == _verticalAxisUnit) {
				return;
			}
			_verticalAxisUnit = _value;
		}
		public function get verticalAxisBaseUnit():String {
			return _verticalAxisBaseUnit;
		}
		public function set verticalAxisBaseUnit(_value:String):void {
			if(_value == _verticalAxisBaseUnit) {
				return;
			}
			_verticalAxisBaseUnit = _value;
		}
		public function get horizontalAxisStep():Number {
			return _horizontalAxisStep;
		}
		public function set horizontalAxisStep(_value:Number):void {
			if (_value == _horizontalAxisStep) {
				return;
			}
			_horizontalAxisStep = _value;
		}
		public function get verticalAxisStep():Number {
			return _verticalAxisStep;
		}
		public function set verticalAxisStep(_value:Number):void {
			if (_value == _verticalAxisStep) {
				return;
			}
			_verticalAxisStep = _value;
		}
		public function get hasSymbol():Boolean {
			return _hasSymbol;
		}
		public function set hasSymbol(_value:Boolean):void {
			if (_value == _hasSymbol) {
				return;
			}
			_hasSymbol = _value;
		}
		public function addRecord(_object:Object):void {
			_dataProvider.addItem(_object);
		}
		public function draw():void {
			_symbolWidth = 0;
			_symbolHeight = 0;
			removeChild(_graphBackgroundContainer);
			removeChild(_graphContainer);
			removeChild(_demoContainer);
			initContainer();
			var _lastPositionY:Number = 0;
			if (_caption.length > 0 ) {
				drawCaption();
			}
			_lastPositionY = _captionTextField.y + _captionTextField.textHeight + _margin;
			if (_hasSymbol) {
				drawSymbol();
				drawAxis(_maxWidth - _margin - _symbolWidth, _maxHeight - _lastPositionY);
				drawGraph(_maxWidth - _margin - _symbolWidth, _maxHeight - _lastPositionY);
			} else {
				drawAxis(_maxWidth, _maxHeight - _lastPositionY);
				drawGraph(_maxWidth, _maxHeight - _lastPositionY);
			}
			_actualWidth = _maxWidth;
			_actualHeight = _maxHeight;
			_graphContainer.x = _margin + _horizontalAxisLabelWidth;
			_graphContainer.y = _maxHeight - _margin - _verticalAxisLabelHeight;
			_graphBackgroundContainer.x = _graphContainer.x;
			_graphBackgroundContainer.y = _graphContainer.y;
			_demoContainer.x = _maxWidth - _symbolWidth;
			_demoContainer.y = _graphContainer.y - _graphContainer.height / 2 - _demoContainer.height / 2;
		}
		public function setHorizontalBackground(_range:Range, _color:uint):void {
			_horizontalRangeBackgroundArray.push({range:_range, color:_color});
		}
		protected function initCaption():void {
			_captionTextField = new StaticTextField(TextFieldAutoSize.LEFT, true, true, AntiAliasType.ADVANCED);
			_captionTextField.textColor = 0x000000;
			_captionTextField.styleSheet = _styleSheet;
			addChild(_captionTextField);
		}
		protected function initContainer():void {
			_graphBackgroundContainer = new Sprite();
			addChild(_graphBackgroundContainer);

			_graphContainer = new Sprite();
			addChild(_graphContainer);

			_demoContainer = new Sprite();
			addChild(_demoContainer);

			_horizontalAxisArray = new Array();
			_horizontalAxisLabelArray = new Array();
			_verticalAxisArray = new Array();
			_verticalAxisLabelArray = new Array();
		}
		protected function drawCaption():void {
			_captionTextField.htmlText = _caption.length > 0 ? "<graphTitle>" + _caption + "</graphTitle>" : _caption;
			_captionTextField.x = _margin;
			_captionTextField.y = 0;
			_captionTextField.width = _maxWidth;
		}
		protected function drawSymbol():void {
			trace("[ABSTRACTE: com.ning.graphs.GraphBase.drawSymbol()]");
		}
		protected function drawAxis(_width:Number, _height:Number):void {
			var _horizontalAxisAmount = Math.ceil(_horizontalAxisRange.diff / _horizontalAxisStep) + 1;
			var _verticalAxisAmount = Math.ceil(_verticalAxisRange.diff / _verticalAxisStep) + 1;

			// draw horizontal axis labels.
			for (var _i:int = 0; _i < _horizontalAxisAmount; _i++) {
				var _newLabel = new StaticTextField(TextFieldAutoSize.LEFT, false, true, AntiAliasType.ADVANCED);
				_newLabel.textColor = 0x000000;
				_newLabel.styleSheet = _styleSheet;
				_newLabel.htmlText = "<graphAxisScale>" + _horizontalAxisUnit + (_horizontalAxisRange.min + _i * _horizontalAxisStep).toString() + "</graphAxisScale>";
				_horizontalAxisLabelWidth = Math.max(_horizontalAxisLabelWidth, _newLabel.textWidth);
				_graphContainer.addChild(_newLabel);
				_horizontalAxisLabelArray.push(_newLabel);
			}
			// draw vertical axis labels.
			for (var _i:int = 0; _i < _verticalAxisAmount; _i++) {
				var _newLabel = new StaticTextField(TextFieldAutoSize.LEFT, true, true, AntiAliasType.ADVANCED);
				_newLabel.textColor = 0x000000;
				_newLabel.styleSheet = _styleSheet;
				var _labelNumber:Number = _verticalAxisRange.min + _i * _verticalAxisStep;
				if (_labelNumber == 0) {
					_newLabel.htmlText = "<graphAxisScale>" + _verticalAxisBaseUnit + "</graphAxisScale>";
				} else {
					_newLabel.htmlText = "<graphAxisScale>" + _verticalAxisUnit + _labelNumber.toString() + "</graphAxisScale>";
				}
				_verticalAxisLabelHeight = Math.max(_verticalAxisLabelHeight, _newLabel.textHeight);
				_graphContainer.addChild(_newLabel);
				_verticalAxisLabelArray.push(_newLabel);
			}
			var _horizontalAxisWidth = _width - _margin - _horizontalAxisLabelWidth;
			var _horizontalAxisHeight = (_height - _margin - _verticalAxisLabelHeight) / (_horizontalAxisAmount - 1);
			var _verticalAxisWidth = (_width - _margin - _horizontalAxisLabelWidth) / _verticalAxisAmount;
			var _verticalAxisHeight = _height - _margin - _verticalAxisLabelHeight;

			// position labels and draw axis.
			var _shape:Shape;
			for (var _i:int = 0; _i <_horizontalAxisLabelArray.length; _i++) {
				_shape = new Shape();
				_shape.graphics.lineStyle(1, 0x000000);
				_shape.graphics.moveTo(-5, -_i * _horizontalAxisHeight);
				if(_i==0 || _i==_horizontalAxisLabelArray.length-1)
				{				   
				   _shape.graphics.lineTo(_horizontalAxisWidth, -_i * _horizontalAxisHeight);				  
				}
				else
				{	
				  _shape.graphics.lineTo(0, -_i * _horizontalAxisHeight);
				}
				_graphContainer.addChild(_shape);
				_horizontalAxisLabelArray[_i].x = -_horizontalAxisLabelWidth - _margin;
				_horizontalAxisLabelArray[_i].y = -_horizontalAxisLabelArray[_i].textHeight / 2 - (_horizontalAxisHeight * _i);
			}
			_shape = new Shape();
			_shape.graphics.lineStyle(1, 0x000000);
			_shape.graphics.moveTo(0, 0);
			_shape.graphics.lineTo(0, -_verticalAxisHeight);
			_graphContainer.addChild(_shape);
			for (var _i:int = 0; _i < _verticalAxisLabelArray.length; _i++) {
				_shape = new Shape();
				_shape.graphics.lineStyle(1, 0x000000);
				_shape.graphics.moveTo(_i * _verticalAxisWidth, 0);
				_shape.graphics.lineTo(_i * _verticalAxisWidth, 5);
				_graphContainer.addChild(_shape);
				_verticalAxisLabelArray[_i].x = (_verticalAxisWidth - _verticalAxisLabelArray[_i].textWidth) / 2 + (_i * _verticalAxisWidth);
				_verticalAxisLabelArray[_i].y = _margin;
			}
			_shape = new Shape();
			_shape.graphics.lineStyle(1, 0x000000);
			_shape.graphics.moveTo(_verticalAxisLabelArray.length * _verticalAxisWidth, 5);
			_shape.graphics.lineTo(_verticalAxisLabelArray.length * _verticalAxisWidth, -_verticalAxisHeight);
			_graphContainer.addChild(_shape);

			// draw background.
			for (var _i:int = 0; _i < _horizontalRangeBackgroundArray.length; _i++) {
				_shape = new Shape();
				_shape.graphics.beginFill(_horizontalRangeBackgroundArray[_i].color, 0.3);
				_shape.graphics.drawRect(0, -(_horizontalRangeBackgroundArray[_i].range.max - _horizontalAxisRange.min) * _horizontalAxisHeight, _horizontalAxisWidth, _horizontalRangeBackgroundArray[_i].range.diff * _horizontalAxisHeight);
				_graphBackgroundContainer.addChild(_shape);
			}
		}
		protected function drawGraph(_width:Number, _height:Number):void {
			trace("[ABSTRACTE: com.ning.graphs.GraphBase.drawGraph()]");
		}
	}
}