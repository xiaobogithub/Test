package com.ethos.changetech.controls{
	import fl.controls.RadioButton;
	import fl.events.ComponentEvent;
	import flash.display.Sprite;
	import flash.text.*;

	public class RadioButtonDesignElement extends DesignElement {

		private var _radioButton:RadioButton;
		private var _textFormat:TextFormat;
		private var _widthDiff:Number = 38;

		override public function set width(_value:Number):void {
			if (_value == _radioButton.width) {
				return;
			}
			_radioButton.width = _value;
			applyTextFormat();
		}
		override public function get height():Number {
			return _radioButton.height;
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
			return _radioButton.textField.backgroundColor;
		}
		public function set backgroundColor(_value:uint):void {
			if (_value == _radioButton.textField.backgroundColor) {
				return;
			}
			_radioButton.textField.backgroundColor = _value;
		}
		public function get background():Boolean {
			return _radioButton.textField.background;
		}
		public function set background(_value:Boolean):void {
			if (_value == _radioButton.textField.background) {
				return;
			}
			_radioButton.textField.background = _value;
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
			return _radioButton.label;
		}
		public function set label(_value:String):void {
			if (_value == _radioButton.label) {
				return;
			}
			_radioButton.textField.defaultTextFormat = _textFormat;
			_radioButton.label = _value;
			applyTextFormat();
		}
		public function RadioButtonDesignElement() {
			mouseChildren = false;
			_type = "RadioButton";
			setupTextFormat();
			setupRadioButton();
		}
		private function setupTextFormat():void {
			_textFormat = new TextFormat();
			_textFormat.color = 0x000000;
			_textFormat.size = 12;
		}
		private function setupRadioButton():void {
			_radioButton = new RadioButton();
			_radioButton.label = "RadioButton";
			_radioButton.textField.autoSize = TextFieldAutoSize.LEFT;
			_radioButton.textField.border = false;
			_radioButton.textField.borderColor = 0x999999;
			_radioButton.textField.selectable = false;
			_radioButton.textField.wordWrap = true;
			_radioButton.textField.defaultTextFormat = _textFormat;
			addChild(_radioButton);
		}
		private function applyTextFormat():void {
			_radioButton.textField.setTextFormat(_textFormat);
			_radioButton.textField.width =( _radioButton.width - _widthDiff);
			_radioButton.height = _radioButton.textField.height;			
		}
		override public function toXML():XML {
			var _controlXML:XML=<Control Name="RadioButton"></Control>;;
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
			_eventXML = <Event Name="Checked" ActionExpression="" />;
			_eventsXML.appendChild(_eventXML);
			
			_controlXML.appendChild(_propertiesXML);
			_controlXML.appendChild(_eventsXML);
			return _controlXML;
		}
	}
}