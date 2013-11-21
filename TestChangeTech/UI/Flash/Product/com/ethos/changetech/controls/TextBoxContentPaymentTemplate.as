package com.ethos.changetech.controls{
	import com.ethos.changetech.events.PageEvent;
	import com.ethos.changetech.events.SubmitEvent;
	import com.ethos.changetech.models.Page;
	import com.ethos.changetech.utils.PageVariableReplacer;
	import com.ethos.changetech.utils.WindowLocation;
	import com.ethos.changetech.xml.FeedbacksXML;
	import com.ethos.changetech.xml.FeedbackXMLNode;
	import com.ning.data.GlobalValue;
	import com.ning.text.StaticTextField;
	import com.ning.text.StringExtension;
	import fl.controls.CheckBox;
	import fl.controls.TextInput;
	import flash.errors.IllegalOperationError;
	import flash.events.MouseEvent;
	import flash.filters.BlurFilter;
	import flash.text.AntiAliasType;
	import flash.text.StyleSheet;
	import flash.text.TextFieldAutoSize;
	import flash.external.ExternalInterface;

	public class TextBoxContentPaymentTemplate extends TextBoxContentStandardBasedTemplate {

		private var _enableAlphaFilter:BlurFilter;		
		private var _inputFieldWidth:Number;
		
		public function TextBoxContentPaymentTemplate(_targetPage:Page, _w:Number, _minH:Number, _maxH:Number) {
			super(_targetPage, _w, _minH, _maxH);
		}
		override protected function updateInnerContent():void {
			var _buttonLabel:String = "<buttonName>" + PageVariableReplacer.replaceAll(_page.primaryButtonName) + "</buttonName>";
			this.dispatchEvent(new PageEvent("setbutton", {label:_buttonLabel}));			

			_textTextField.htmlText = "<pageText>" + PageVariableReplacer.replaceAll(_page.text) + "</pageText>";			
		}
		override protected function innerContainerLayout(_lastPositionY:Number):Number {		
			var _maxLabelWidth:Number = 0;
			var _inputFieldPositionX:Number = _horizontalPadding +  _maxLabelWidth + _textMargin;
			var _inputFieldWidth:Number = _contentWidth - _maxLabelWidth - _textMargin;			

			_lastPositionY += 2 * _textMargin;
			return _lastPositionY;
		}
		
		override public function primaryButtonClickedHandler(_event:MouseEvent):void {			
			var paymentURL = GlobalValue.getValue("paymentURL");
			var userGuid = GlobalValue.getValue("userGUID");
			var programGuid = GlobalValue.getValue("programGUID");
			trace(paymentURL);
			trace(userGuid);
			trace(programGuid);
			WindowLocation.href =paymentURL+"?ProgramGUID="+programGuid+"&UserGUID="+userGuid;			
		}				
	}
}