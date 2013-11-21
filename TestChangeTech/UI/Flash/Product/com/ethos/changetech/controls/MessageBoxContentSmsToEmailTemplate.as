package com.ethos.changetech.controls{
	import com.ethos.changetech.events.PageEvent;
	import com.ethos.changetech.events.SubmitEvent;
	import com.ethos.changetech.models.Help;
	import com.ethos.changetech.models.HelpItem;
	import com.ethos.changetech.models.MessageObject;
	import com.ning.data.GlobalValue;	
	import com.ning.text.StaticTextField;
	import fl.controls.Button;
	import fl.controls.CheckBox;
	import flash.display.Sprite;
	import flash.errors.IllegalOperationError;
	import flash.events.MouseEvent;
	import flash.filters.BlurFilter;
	import flash.text.AntiAliasType;
	import flash.text.StyleSheet;
	import flash.text.TextFieldAutoSize;

	public class MessageBoxContentSmsToEmailTemplate extends MessageBoxContentTemplate {
	
		private var checkbox:CheckBox;
		private var _submitButton:Button;

		public function MessageBoxContentSmsToEmailTemplate(_object:MessageObject, _w:Number) {
			super(_object, _w);
		}
		override protected function initModel(_object:MessageObject):void {
			
		}
		override protected function initContainer():void {	
			//comboBox = new ComboBoxTimeZone();
			//this.addChild(comboBox);
			checkbox = new CheckBox();
			
			var text:String = XML(GlobalValue.getValue("smsToEmailModel")).@Text;
			
			if (text == "" || text == null || text == undefined)
			text = "No text here ,please check XML.";
			checkbox.label = text;
			
			
			if (GlobalValue.getValue("selectedSMSToEmail") == null)
			{
				checkbox.selected = GlobalValue.getValue("IsSMSToEmail") == "1"?true:false;
			}else {
				checkbox.selected = GlobalValue.getValue("selectedSMSToEmail");
			}
			
			GlobalValue.setValue("selectedSMSToEmail", checkbox.selected)
			
			
			
			this.addChild(checkbox);
			
			//checkbox.addEventListener(MouseEvent.CLICK, selectWay);
			
			
			_submitButton = new Button();
			_submitButton.textField.filters = [new BlurFilter(0, 0, 0)];
			
			var smsToEmailMode:XML = XML(GlobalValue.getValue("smsToEmailModel"));
			var submitLabel:String = smsToEmailMode.@SubmitButtonName;
			if (smsToEmailMode.@SubmitButtonName == undefined || smsToEmailMode.@SubmitButtonName == "" || smsToEmailMode.@SubmitButtonName == null)
			submitLabel = "submit";
			
			_submitButton.label = submitLabel;
			_submitButton.addEventListener(MouseEvent.CLICK, submitButtonClickHandler);
			addChild(_submitButton);
		}
		
		private function submitButtonClickHandler(e:MouseEvent):void 
		{
			_submitButton.enabled = false;
			var smsToEmailXml:XML =   <SMSToEmail></SMSToEmail>;
			smsToEmailXml.@Value = checkbox.selected;
			this.dispatchEvent(new SubmitEvent("submit", smsToEmailXml));
			GlobalValue.setValue("selectedSMSToEmail",checkbox.selected)
		}
		
		private function selectWay(e:MouseEvent):void 
		{
			//GlobalValue.setValue("selectedSMSToEmail",checkbox.selected)
		}
		override protected function layout():void {
			var _lastPositionY:Number = 0;						
			checkbox.x = _horizontalPadding;
			checkbox.y = _lastPositionY;
			_lastPositionY = checkbox.y + checkbox.height;
			
			_submitButton.x = _contentWidth / 2 - _submitButton.width / 2+_horizontalPadding;
			_submitButton.y = _lastPositionY;
			_lastPositionY = _submitButton.y + _submitButton.height;
			
			height = _lastPositionY;
			this.dispatchEvent(new PageEvent("arranged", null));			
		}
		
		
	}
}