package com.ethos.changetech.controls{
	import com.ethos.changetech.events.PageEvent;
	import com.ethos.changetech.events.SubmitEvent;
	import com.ethos.changetech.models.Help;
	import com.ethos.changetech.models.HelpItem;
	import com.ethos.changetech.models.MessageObject;
	import com.ning.data.GlobalValue;	
	import com.ning.text.StaticTextField;
	import fl.controls.Button;
	import flash.display.Sprite;
	import flash.errors.IllegalOperationError;
	import flash.events.MouseEvent;
	import flash.filters.BlurFilter;
	import flash.text.AntiAliasType;
	import flash.text.StyleSheet;
	import flash.text.TextFieldAutoSize;

	public class MessageBoxContentTimeZoneTemplate extends MessageBoxContentTemplate {
	
		private var _comboBox:ComboBoxTimeZone;
		private var _submitButton:Button;

		public function MessageBoxContentTimeZoneTemplate(_object:MessageObject, _w:Number) {
			super(_object, _w);
		}
		override protected function initModel(_object:MessageObject):void {
			
		}
		override protected function initContainer():void {	
			comboBox = new ComboBoxTimeZone();
			this.addChild(comboBox);
			
			_submitButton = new Button();
			_submitButton.textField.filters = [new BlurFilter(0, 0, 0)];
			
			var timeZoneMode:XML = XML(GlobalValue.getValue("timeZoneModel"));
			var submitLabel:String = timeZoneMode.@SubmitButtonName;
			if (timeZoneMode.@SubmitButtonName == undefined || timeZoneMode.@SubmitButtonName == "" || timeZoneMode.@SubmitButtonName == null)
			submitLabel = "submit";
			
			_submitButton.label = submitLabel;
			_submitButton.addEventListener(MouseEvent.CLICK, submitButtonClickHandler);
			addChild(_submitButton);
		}
		
		private function submitButtonClickHandler(e:MouseEvent):void 
		{
			_submitButton.enabled = false;
			var timeZoneXml:XML =   <TimeZone></TimeZone>;
			timeZoneXml.@Value = _comboBox.selectedItem.value;
			GlobalValue.setValue("CurrentTimeZone",String(_comboBox.selectedItem.value));
			this.dispatchEvent(new SubmitEvent("submit", timeZoneXml));
		}
		override protected function layout():void {
			var _lastPositionY:Number = 0;						
			comboBox.x = _horizontalPadding;
			comboBox.y = _lastPositionY;
			_lastPositionY = comboBox.y + comboBox.height;
			
			_submitButton.x = _contentWidth / 2 - _submitButton.width / 2+_horizontalPadding;
			_submitButton.y = _lastPositionY;
			_lastPositionY = _submitButton.y + _submitButton.height;
			
			height = _lastPositionY;
			this.dispatchEvent(new PageEvent("arranged", null));			
		}
		
		public function get comboBox():ComboBoxTimeZone 
		{
			return _comboBox;
		}
		
		public function set comboBox(value:ComboBoxTimeZone):void 
		{
			_comboBox = value;
		}
	}
}