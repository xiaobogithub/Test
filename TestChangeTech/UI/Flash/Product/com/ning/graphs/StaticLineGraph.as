package com.ning.graphs{
	import com.ning.text.StaticTextField;
	import flash.display.Shape;
	import flash.display.Sprite;
	import flash.errors.IllegalOperationError;
	import flash.text.AntiAliasType;
	import flash.text.StyleSheet;
	import flash.text.TextFieldAutoSize;

	public class StaticLineGraph extends GraphBase {

		private var _pointSize:Number = 15;

		override protected function drawSymbol():void {
			var _rowDistance:Number = 5;

			for (var _i:int = 0; _i < _dataProvider.length; _i++) {
				// draw name.
				var _nameTextField:StaticTextField = new StaticTextField(TextFieldAutoSize.LEFT, true, true, AntiAliasType.ADVANCED);
				_nameTextField.textColor = 0x000000;
				_nameTextField.styleSheet = _styleSheet;
				_nameTextField.htmlText = "<graphSymbol>" + _dataProvider.getItemAt(_i).name + "</graphSymbol>";
				_nameTextField.x = 40;
				_nameTextField.y = _symbolHeight + _rowDistance;
				_nameTextField.width = _maxSymbolWidth - 45;
				_symbolWidth = Math.max(_symbolWidth, 40 + _nameTextField.textWidth);
				_symbolHeight = _nameTextField.y + _nameTextField.textHeight;
				_demoContainer.addChild(_nameTextField);

				// draw symbol
				var _shape:Shape = new Shape();
				_shape.graphics.lineStyle(1, _dataProvider.getItemAt(_i).color);
				_shape.graphics.moveTo(5, _nameTextField.y +  _nameTextField.textHeight / 2);
				_shape.graphics.lineTo(35, _nameTextField.y + _nameTextField.textHeight / 2);
				_demoContainer.addChild(_shape);
				_demoContainer.addChild(getPoint(20, _nameTextField.y + _nameTextField.textHeight / 2, _pointSize / 2, _dataProvider.getItemAt(_i).color, _dataProvider.getItemAt(_i).pointType));
			}
			_symbolWidth +=5;
			_symbolHeight +=5;

			var _frameShape:Shape = new Shape();
			_frameShape.graphics.lineStyle(1, 0x000000);
			_frameShape.graphics.drawRect(0, 0, _symbolWidth, _symbolHeight);
			_demoContainer.addChild(_frameShape);
		}
		override protected function drawGraph(_width:Number, _height:Number):void {
			var _horizontalAxisWidth = _width - _margin - _horizontalAxisLabelWidth;
			var _horizontalAxisHeight = (_height - _margin - _verticalAxisLabelHeight) / Math.ceil(_horizontalAxisRange.diff / _horizontalAxisStep);
			var _verticalAxisWidth = (_width - _margin - _horizontalAxisLabelWidth) / (Math.ceil(_verticalAxisRange.diff / _verticalAxisStep) + 1);
			var _verticalAxisHeight = _height - _margin - _verticalAxisLabelHeight;

			for (var _i:int = 0; _i < _dataProvider.length; _i++) {
				var _shape:Shape = new Shape();
				_shape.graphics.lineStyle(2, _dataProvider.getItemAt(_i).color);
				if (_dataProvider.getItemAt(_i).values[0] !== "") {
					_shape.graphics.moveTo(_verticalAxisWidth / 2, -(_dataProvider.getItemAt(_i).values[0] - _horizontalAxisRange.min) / _horizontalAxisStep * _horizontalAxisHeight);
					_graphContainer.addChild(getPoint(_verticalAxisWidth / 2, -(_dataProvider.getItemAt(_i).values[0] - _horizontalAxisRange.min) / _horizontalAxisStep * _horizontalAxisHeight, _pointSize, _dataProvider.getItemAt(_i).color, _dataProvider.getItemAt(_i).pointType));
				}
				for (var _j:int = 1; _j < _dataProvider.getItemAt(_i).values.length; _j++) {
					if (_dataProvider.getItemAt(_i).values[_j - 1] === "") {
						_shape.graphics.moveTo(_j * _verticalAxisWidth + _verticalAxisWidth / 2, -(_dataProvider.getItemAt(_i).values[_j] - _horizontalAxisRange.min) / _horizontalAxisStep * _horizontalAxisHeight);
					} else if (_dataProvider.getItemAt(_i).values[_j] !== "") {
						_shape.graphics.lineTo(_j * _verticalAxisWidth + _verticalAxisWidth / 2, -(_dataProvider.getItemAt(_i).values[_j] - _horizontalAxisRange.min) / _horizontalAxisStep * _horizontalAxisHeight);
					}
					if (_dataProvider.getItemAt(_i).values[_j] !== "") {
						_graphContainer.addChild(getPoint(_j * _verticalAxisWidth + _verticalAxisWidth / 2, -(_dataProvider.getItemAt(_i).values[_j] - _horizontalAxisRange.min) / _horizontalAxisStep * _horizontalAxisHeight, _pointSize, _dataProvider.getItemAt(_i).color, _dataProvider.getItemAt(_i).pointType));
					}
				}
				_graphContainer.addChild(_shape);
			}
		}
		private function getPoint(_xPosition:Number, _yPosition:Number, _size:Number, _color:uint, _pointType:int):Shape {
			var _shape:Shape = new Shape();
			_shape.graphics.beginFill(_color);
			switch (_pointType) {
				case 1 :// Square
					_shape.graphics.drawRect(_xPosition - _size / 2, _yPosition - _size / 2, _size, _size);
					break;
				case 2 :// triangle
					_shape.graphics.moveTo(_xPosition, _yPosition - _size * (5 - Math.sqrt(5)) / 4);
					_shape.graphics.lineTo(_xPosition + _size / 2, _yPosition + _size * (Math.sqrt(5) - 1) / 4);
					_shape.graphics.lineTo(_xPosition - _size / 2, _yPosition + _size * (Math.sqrt(5) - 1) / 4);
					break;
				case 3 :// diamond
					_shape.graphics.moveTo(_xPosition, _yPosition - _size / 2);
					_shape.graphics.lineTo(_xPosition + _size / 2, _yPosition);
					_shape.graphics.lineTo(_xPosition, _yPosition + _size / 2);
					_shape.graphics.lineTo(_xPosition - _size / 2, _yPosition);
					break;
				case 4 :// round
					_shape.graphics.drawCircle(_xPosition, _yPosition, _size / 2);
					break;
				case 5 :// up side down triangle
					_shape.graphics.moveTo(_xPosition, _yPosition + _size * (5 - Math.sqrt(5)) / 4);
					_shape.graphics.lineTo(_xPosition + _size / 2, _yPosition - _size * (Math.sqrt(5) - 1) / 4);
					_shape.graphics.lineTo(_xPosition - _size / 2, _yPosition - _size * (Math.sqrt(5) - 1) / 4);
					break;
				default :
					throw new IllegalOperationError("Invalid point type, only 1 - 5 is provided.");
			}
			_shape.graphics.endFill();
			return _shape;
		}
	}
}