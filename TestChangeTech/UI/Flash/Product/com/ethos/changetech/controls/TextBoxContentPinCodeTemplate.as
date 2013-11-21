package com.ethos.changetech.controls{
	import com.ethos.changetech.events.PageEvent;
	import com.ethos.changetech.events.SubmitEvent;
	import com.ethos.changetech.models.Page;
	import com.ethos.changetech.utils.PageVariableReplacer;
	import com.ethos.changetech.xml.FeedbacksXML;
	import com.ethos.changetech.xml.FeedbackXMLNode;
	import com.ning.data.GlobalValue;
	import com.ning.events.LoginEvent;
	import com.ning.text.StaticTextField;
	import com.ning.text.StringExtension;
	import fl.controls.CheckBox;
	import fl.controls.TextInput;
	import flash.display.Sprite;
	import flash.errors.IllegalOperationError;
	import flash.events.MouseEvent;
	import flash.filters.BlurFilter;
	import flash.text.AntiAliasType;
	import flash.text.StyleSheet;
	import flash.text.TextFieldAutoSize;

	public class TextBoxContentPinCodeTemplate extends TextBoxContentStandardBasedTemplate {

		private var _pinCodeLabelTextField:StaticTextField;
		private var _pinCodeTextInput:TextInput;
		private var _enableAlphaFilter:BlurFilter;		
		private var _inputFieldWidth:Number;
		private var _pinCodeReminderSprite:Sprite;
		
		public function TextBoxContentPinCodeTemplate(_targetPage:Page, _w:Number, _minH:Number, _maxH:Number) {
			super(_targetPage, _w, _minH, _maxH);
		}
		override protected function updateInnerContent():void {
			var _buttonLabel:String = "<buttonName>" + PageVariableReplacer.replaceAll(_page.primaryButtonName) + "</buttonName>";
			this.dispatchEvent(new PageEvent("setbutton", {label:_buttonLabel}));

			var _textArray:Array = StringExtension.smartSplit(_page.text, ";");

			_textTextField.htmlText = "<pageText>" + PageVariableReplacer.replaceAll(_textArray[0]) + "</pageText>";
			_footerTextTextField.htmlText = "<hyperlink>" + PageVariableReplacer.replaceAll(_textArray[_textArray.length-1]) + "</hyperlink>";
			
			_pinCodeLabelTextField = new StaticTextField(TextFieldAutoSize.LEFT, false, true, AntiAliasType.ADVANCED);
			_pinCodeLabelTextField.textColor = 0x999999;
			_pinCodeLabelTextField.styleSheet = StyleSheet(GlobalValue.getValue("css"));
			_pinCodeLabelTextField.htmlText = "<inputLabel>" + _textArray[1] + "</inputLabel>";
			addChild(_pinCodeLabelTextField);
			
			_enableAlphaFilter = new BlurFilter(0,0,0);

			_pinCodeTextInput = new TextInput();
			_pinCodeTextInput.textField.filters = [_enableAlphaFilter];
			addChild(_pinCodeTextInput);			
			
			_pinCodeReminderSprite = new Sprite();
			_pinCodeReminderSprite.buttonMode = true;
			_pinCodeReminderSprite.useHandCursor = true;			
			_pinCodeReminderSprite.addEventListener(MouseEvent.CLICK, pinCodeReminderHandler);
			addChild(_pinCodeReminderSprite);			
		}
		
		public function pinCodeReminderHandler(_event:MouseEvent):void
		{
			var _pinCodeReminderXML:XML = <PinCodeReminder></PinCodeReminder>;				
			this.dispatchEvent(new SubmitEvent("submit", _pinCodeReminderXML));
			startLoadingPage();
		}
		
		override protected function innerContainerLayout(_lastPositionY:Number):Number {
			var _maxLabelWidth:Number = 0;
			_pinCodeLabelTextField.x = _horizontalPadding;
			_pinCodeLabelTextField.y = _lastPositionY + 2 * _textMargin;
			_lastPositionY = _pinCodeLabelTextField.y + _pinCodeLabelTextField.height;
			_maxLabelWidth = Math.max(_maxLabelWidth, _pinCodeLabelTextField.textWidth);
			
			var _inputFieldPositionX:Number = _horizontalPadding +  _maxLabelWidth + _textMargin;
			var _inputFieldWidth:Number = _contentWidth - _maxLabelWidth - _textMargin;

			_pinCodeTextInput.x = _inputFieldPositionX;
			_pinCodeTextInput.y = _pinCodeLabelTextField.y - (_pinCodeTextInput.height - _pinCodeLabelTextField.height) / 2;
			_pinCodeTextInput.width = _inputFieldWidth;
			_lastPositionY = _pinCodeTextInput.y + _pinCodeTextInput.height;
			//_lastPositionY += 2 * _textMargin;
			return _lastPositionY;
		}
		
		override protected function footerTextFieldLayout(_lastPositionY:Number):Number {
			_footerTextTextField.x = _horizontalPadding;
			_footerTextTextField.y = _lastPositionY + 2 * _textMargin;
			_footerTextTextField.width = _contentWidth;
			
			_pinCodeReminderSprite.x = _horizontalPadding;
			_pinCodeReminderSprite.y = _lastPositionY + 2 * _textMargin;
			_pinCodeReminderSprite.graphics.clear();
			_pinCodeReminderSprite.graphics.beginFill(0xFFFFFF, 0);
			_pinCodeReminderSprite.graphics.drawRect(0, 0, _footerTextTextField.textWidth, _footerTextTextField.textHeight);
			_pinCodeReminderSprite.graphics.endFill();
			_lastPositionY = _pinCodeReminderSprite.y + _pinCodeReminderSprite.height;
			
			_lastPositionY += 5;
			return _lastPositionY;
		}
		
		private function setEnabled(_setCheckBox:Boolean):void {
			_pinCodeTextInput.enabled = true;
			_pinCodeTextInput.mouseChildren = true;			
			}
		private function setDisabled(_setCheckBox:Boolean):void {
			_pinCodeTextInput.enabled = false;
			_pinCodeTextInput.mouseChildren = false;			
		}
		
		override public function primaryButtonClickedHandler(_event:MouseEvent):void {			
				if (_pinCodeTextInput.text.length == 0) {
					infoPage(GlobalValue.getValue("messages")["RegisterInfoRequired"]);
					_event.target.enabled = true;
					return;
				}
				
				setDisabled(true);
				var _registerXML:XML = <PinCode></PinCode>;
				_registerXML.@PinCode = _pinCodeTextInput.text;
				this.dispatchEvent(new SubmitEvent("submit", _registerXML));
				startLoadingPage();
		}
		override public function submitSuccessfulCallBack(_message:String):void {
			/*stopLoadingPage();
			trace("pincode message:");
			trace(_message);
			var _flag:String = _message.charAt(0);
			switch (_flag) {
				case "0" :
					_page.pageVariable = -1;
					var _messageObject:MessageObject = GlobalValue.getValue("messages")["RegisterFailed"];
					_messageObject.message = _message.substr(2);
					infoPage(_messageObject);
					this.dispatchEvent(new PageEvent("setbutton", {enabled:true}));
					setEnabled(true);
					break;
				case "1" :
					_page.pageVariable = 1;
					if(_message.length > 2) {
						var _userGUID:String = _message.substr(2);
						GlobalValue.setValue("userGUID", _userGUID);
					}
					nextPage();
					break;
				default :
					throw new IllegalOperationError("Invalid register server state.");
			}*/
			stopLoadingPage();
			trace("pincode message:");
			trace(_message);
			var _flag:String = _message.charAt(0);
			switch (_flag) {
				case "0" :
					var _messageObject:MessageObject = GlobalValue.getValue("messages")["PinCodeWrong"];
					_messageObject.message = _message.substr(2);
					infoPage(_messageObject);
					this.dispatchEvent(new PageEvent("setbutton", {enabled:true}));
					_pinCodeTextInput.enabled = true;
					break;
				case "1" :
					this.dispatchEvent(new LoginEvent("complete", _message.substr(2)));
					break;
				case "3" :
					// resend pincode successfully
					var _messageObject:MessageObject = GlobalValue.getValue("messages")["SendPinCodeSuccess"];
					_messageObject.message = _message.substr(2);
					infoPage(_messageObject);
					this.dispatchEvent(new PageEvent("setbutton", {enabled:true}));
					break;
				case "4" :
					// resend pincode failed
					var _messageObject:MessageObject = GlobalValue.getValue("messages")["SendPinCodeFailed"];
					_messageObject.message = _message.substr(2);
					infoPage(_messageObject);
					this.dispatchEvent(new PageEvent("setbutton", {enabled:true}));
					break;
				default :
					throw new IllegalOperationError("Invalid login server state.");
			}
		}
		override public function submitFailedCallBack(_message:String):void {
			stopLoadingPage();
			infoPage(GlobalValue.getValue("messages")["NetworkError"]);
			this.dispatchEvent(new PageEvent("setbutton", {enabled:true}));
			setEnabled(true);
		}
	}
}