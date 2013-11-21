package com.ethos.changetech.controls{
	import fl.controls.Button;
	import fl.events.ComponentEvent;
	import flash.display.Sprite;
	import flash.text.*;

	public class ButtonDesignElement extends DesignElement {

		private var _button:Button;
		private var _textFormat:TextFormat;

		override public function set width(_value:Number):void {
			if (_value == _button.width) {
				return;
			}
			_button.width = _value;
			applyTextFormat();
		}
		override public function get height():Number {
			return _button.height;
		}
		override public function set height(_value:Number):void {
			if (_value == _button.height) {
				return;
			}
			_button.height = _value;
			applyTextFormat();
		}
		public function get foregroundColor():uint {
			return uint(_textFormat.color);
		}
		public function set foregroundColor(_value:uint):void {
			if (_value == uint(_textFormat.color)) {
				return;
			}
			_textFormat.color = _value;
			applyTextFormat();
		}
		public function get backgroundColor():uint {
			return _button.textField.backgroundColor;
		}
		public function set backgroundColor(_value:uint):void {
			if (_value == _button.textField.backgroundColor) {
				return;
			}
			_button.textField.backgroundColor = _value;
		}
		public function get background():Boolean {
			return _button.textField.background;
		}
		public function set background(_value:Boolean):void {
			if (_value == _button.textField.background) {
				return;
			}
			_button.textField.background = _value;
		}
		public function get fontSize():Object {
			return _textFormat.size;
		}
		public function set fontSize(_value:Object):void {
			if (_value == _textFormat.size) {
				return;
			}
			_textFormat.size = _value;
			applyTextFormat();
		}
		public function get label():String {
			return _button.label;
		}
		public function set label(_value:String):void {
			if (_value == _button.label) {
				return;
			}
			_button.textField.defaultTextFormat = _textFormat;
			_button.label = _value;
			applyTextFormat();
		}
		public function ButtonDesignElement() {
			mouseChildren = false;
			_type = "Button";
			setupTextFormat();
			setupButton();
		}
		private function setupTextFormat():void {
			_textFormat = new TextFormat();
			_textFormat.color = 0x000000;
			_textFormat.size = 12;
			//_textFormat.align = "center"
		}
		private function setupButton():void {
			_button = new Button();
			_button.label = "Button";
			//_button.textField.autoSize = TextFieldAutoSize.LEFT;
			_button.textField.border = false;
			_button.textField.borderColor = 0x999999;
			_button.textField.selectable = false;
			_button.textField.wordWrap = true;
			_button.textField.defaultTextFormat = _textFormat;
			addChild(_button);
		}
		private function applyTextFormat():void {
			_button.textField.setTextFormat(_textFormat);
			_button.textField.width =_button.width;
			_button.textField.height = _button.height;
			_button.textField.x =Math.max(0, (_button.width - _button.textField.textWidth)/2 - 2);
			_button.textField.y =Math.max(0, (_button.height - _button.textField.textHeight)/2 - 2);
		}
		override public function toXML():XML {
			var _controlXML:XML=<Control Name="Button"></Control>;;
			var _propertiesXML:XML=<Properties></Properties>;
			var _propertyXML:XML;
			//property PositionX.
			_propertyXML = <Property Name="PositionX" Value=""></Property>;
			_propertyXML.@Value = x;
			_propertiesXML.appendChild(_propertyXML);
			//property PositionY.
			_propertyXML = <Property Name="PositionY" Value=""></Property>;
			_propertyXML.@Value = y;
			_propertiesXML.appendChild(_propertyXML);
			//property Width.
			_propertyXML = <Property Name="Width" Value=""></Property>;
			_propertyXML.@Value = width;
			_propertiesXML.appendChild(_propertyXML);
			//property Height.
			_propertyXML = <Property Name="Height" Value=""></Property>;
			_propertyXML.@Value = height;
			_propertiesXML.appendChild(_propertyXML);
			//property ForegroundColor.
			_propertyXML = <Property Name="ForegroundColor" Value=""></Property>;
			_propertyXML.@Value = foregroundColor;
			_propertiesXML.appendChild(_propertyXML);
			//property BackgroundColor.
			_propertyXML = <Property Name="BackgroundColor" Value=""></Property>;
			_propertyXML.@Value = backgroundColor;
			_propertiesXML.appendChild(_propertyXML);
			//property Background.
			_propertyXML = <Property Name="Background" Value=""></Property>;
			_propertyXML.@Value = background;
			_propertiesXML.appendChild(_propertyXML);
			//property FontFamily.
			_propertyXML = <Property Name="FontFamily" Value="default"></Property>;
			//_propertyXML.@Value = background;
			_propertiesXML.appendChild(_propertyXML);
			//property FontSize.
			_propertyXML = <Property Name="FontSize" Value=""></Property>;
			_propertyXML.@Value = fontSize;
			_propertiesXML.appendChild(_propertyXML);
			//property Label.
			_propertyXML = <Property Name="Label" Value=""></Property>;
			_propertyXML.@Value = label;
			_propertiesXML.appendChild(_propertyXML);

			var _eventsXML:XML=<Events></Events>;
			var _eventXML:XML;
			//event Checked.
			_eventXML = <Event Name="OnClick" ActionExpression="" />;
			_eventsXML.appendChild(_eventXML);

			_controlXML.appendChild(_propertiesXML);
			_controlXML.appendChild(_eventsXML);
			return _controlXML;
		}
	}
}