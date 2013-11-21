package com.ethos.changetech.controls{
	import flash.display.Sprite;
	import flash.text.*;

	public class TextBlockDesignElement extends DesignElement {

		private var _textFieldInitialWidth:Number=100;
		private var _textFieldInitialHeight:Number=100;
		private var _textField:TextField;
		private var _textFormat:TextFormat;

		override public function set width(_value:Number):void {
			if (_value == _textField.width) {
				return;
			}
			_textField.width=_value;
		}
		public function get foregroundColor():uint {
			return _textField.textColor;
		}
		public function set foregroundColor(_value:uint):void {
			if (_value == _textField.textColor) {
				return;
			}
			_textField.textColor=_value;
		}
		public function get backgroundColor():uint {
			return _textField.backgroundColor;
		}
		public function set backgroundColor(_value:uint):void {
			if (_value == _textField.backgroundColor) {
				return;
			}
			_textField.backgroundColor=_value;
		}
		public function get background():Boolean {
			return _textField.background;
		}
		public function set background(_value:Boolean):void {
			if (_value == _textField.background) {
				return;
			}
			_textField.background=_value;
		}
		public function get fontSize():Object {
			return _textField.defaultTextFormat.size;
		}
		public function set fontSize(_value:Object):void {
			if (_value == _textFormat.size) {
				return;
			}
			_textFormat.size=_value;
			_textField.setTextFormat(_textFormat);
		}
		public function get text():String {
			return _textField.text;
		}
		public function set text(_value:String):void {
			if (_value == _textField.text) {
				return;
			}
			_textField.text=_value;
		}
		public function TextBlockDesignElement() {
			mouseChildren=false;
			_type="TextBlock";
			setupTextFormat();
			setupTextField();
		}
		private function setupTextFormat():void {
			_textFormat=new TextFormat();
		}
		private function setupTextField():void {
			_textField=new TextField  ;
			_textField.width=_textFieldInitialWidth;
			_textField.height=_textFieldInitialHeight;
			_textField.autoSize=TextFieldAutoSize.LEFT;
			_textField.border=false;
			_textField.borderColor=0x999999;
			_textField.selectable=false;
			_textField.wordWrap=true;
			_textField.text="TextBlock";
			addChild(_textField);
		}
		override public function toXML():XML {
			var _controlXML:XML=<Control Name="TextBlock"></Control>;;
			var _propertiesXML:XML=<Properties></Properties>;;
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
			//property Text.
			_propertyXML = <Property Name="Text" Value=""></Property>;
			_propertyXML.@Value = text;
			_propertiesXML.appendChild(_propertyXML);
			
			_controlXML.appendChild(_propertiesXML);
			return _controlXML;
		}
	}
}