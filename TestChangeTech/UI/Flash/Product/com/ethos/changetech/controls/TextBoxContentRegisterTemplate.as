package com.ethos.changetech.controls{
	import com.ethos.changetech.events.PageEvent;
	import com.ethos.changetech.events.SubmitEvent;
	import com.ethos.changetech.models.MessageObject;
	import com.ethos.changetech.models.Page;
	import com.ethos.changetech.utils.PageVariableReplacer;
	import com.ethos.changetech.utils.WindowLocation;
	import com.ethos.changetech.xml.FeedbacksXML;
	import com.ethos.changetech.xml.FeedbackXMLNode;
	import com.ning.data.GlobalValue;
	import com.ning.text.StaticTextField;
	import com.ning.text.StringExtension;
	import fl.controls.CheckBox;
	import fl.controls.ComboBox;
	import fl.controls.TextInput;
	import flash.display.MovieClip;
	import flash.display.Sprite;
	import flash.errors.IllegalOperationError;
	import flash.events.MouseEvent;
	import flash.filters.BlurFilter;
	import flash.text.AntiAliasType;
	import flash.text.StyleSheet;
	import flash.text.TextFieldAutoSize;

	public class TextBoxContentRegisterTemplate extends TextBoxContentStandardBasedTemplate {

		private var _emailLabelTextField:StaticTextField;
		private var _emailTextInput:TextInput;
		private var _passwordLabelTextField:StaticTextField;
		private var _passwordTextInput:TextInput;
		private var _conformPasswordLabelTextField:StaticTextField;
		private var _conformPasswordTextInput:TextInput;
		private var _mobileLabelTextField:StaticTextField;
		private var _mobileTextInput:TextInput;
		private var _serialNumberLabelTextField:StaticTextField;
		private var _serialNumberTextInput:TextInput;
		private var _timeZoneLabelTextField:StaticTextField;
		private var _timeZoneComboBox:ComboBoxTimeZone;
		//private var _genderDropdownList:ComboBox;
		private var _noRegisterCheckBox:CheckBox;
		private var _enableAlphaFilter:BlurFilter;

		private var _inputFieldWidth:Number;

		private var _isRegister:Boolean = true;

		public function TextBoxContentRegisterTemplate(_targetPage:Page, _w:Number, _minH:Number, _maxH:Number) {
			super(_targetPage, _w, _minH, _maxH);
		}
		override protected function updateInnerContent():void {
			trace("trace pincode *******************************************")
			trace(GlobalValue.getValue("IsNeedPinCode"));
			trace(GlobalValue.getValue("IsContainTwoParts"));
			trace(GlobalValue.getValue("IsNeedSerialNumber"));
			var _buttonLabel:String = "<buttonName>" + PageVariableReplacer.replaceAll(_page.primaryButtonName) + "</buttonName>";
			this.dispatchEvent(new PageEvent("setbutton", {label:_buttonLabel}));

			var _textArray:Array = StringExtension.smartSplit(_page.text, ";");

			_textTextField.htmlText = "<pageText>" + PageVariableReplacer.replaceAll(_textArray[0]) + "</pageText>";

			_emailLabelTextField = new StaticTextField(TextFieldAutoSize.LEFT, false, true, AntiAliasType.ADVANCED);
			_emailLabelTextField.textColor = 0x999999;
			_emailLabelTextField.styleSheet = StyleSheet(GlobalValue.getValue("css"));
			_emailLabelTextField.htmlText = "<inputLabel>" + _textArray[1] + "</inputLabel>";
			addChild(_emailLabelTextField);

			_passwordLabelTextField = new StaticTextField(TextFieldAutoSize.LEFT, false, true, AntiAliasType.ADVANCED);
			_passwordLabelTextField.textColor = 0x999999;
			_passwordLabelTextField.styleSheet = StyleSheet(GlobalValue.getValue("css"));
			_passwordLabelTextField.htmlText = "<inputLabel>" + _textArray[2] + "</inputLabel>";
			addChild(_passwordLabelTextField);

			_conformPasswordLabelTextField = new StaticTextField(TextFieldAutoSize.LEFT, false, true, AntiAliasType.ADVANCED);
			_conformPasswordLabelTextField.textColor = 0x999999;
			_conformPasswordLabelTextField.styleSheet = StyleSheet(GlobalValue.getValue("css"));
			_conformPasswordLabelTextField.htmlText = "<inputLabel>" + _textArray[3] + "</inputLabel>";
			addChild(_conformPasswordLabelTextField);
			
			if(GlobalValue.getValue("IsNeedPinCode")=="1" || GlobalValue.getValue("IsContainTwoParts")=="1")
			{
				_mobileLabelTextField = new StaticTextField(TextFieldAutoSize.LEFT, false, true, AntiAliasType.ADVANCED);
				_mobileLabelTextField.textColor = 0x999999;
				_mobileLabelTextField.styleSheet = StyleSheet(GlobalValue.getValue("css"));
				_mobileLabelTextField.htmlText = "<inputLabel>" + _textArray[4] + "</inputLabel>";
				addChild(_mobileLabelTextField);
			}
			if(GlobalValue.getValue("IsNeedSerialNumber")=="1")
			{
				_serialNumberLabelTextField = new StaticTextField(TextFieldAutoSize.LEFT, false, true, AntiAliasType.ADVANCED);
				_serialNumberLabelTextField.textColor = 0x999999;
				_serialNumberLabelTextField.styleSheet = StyleSheet(GlobalValue.getValue("css"));
				_serialNumberLabelTextField.htmlText = "<inputLabel>" + _textArray[_textArray.length-1] + "</inputLabel>";
				addChild(_serialNumberLabelTextField);
			}
			
			//DTD:1582,TimeZone, Add combo-box to account creation page
			if(GlobalValue.getValue("SupportTimeZone")=="1")
			{
				_timeZoneLabelTextField = new StaticTextField(TextFieldAutoSize.LEFT, false, true, AntiAliasType.ADVANCED);
				_timeZoneLabelTextField.textColor = 0x333333;
				_timeZoneLabelTextField.styleSheet = StyleSheet(GlobalValue.getValue("css"));
				_timeZoneLabelTextField.htmlText = "TimeZone"//"<inputLabel>" + _textArray[_textArray.length-1] + "</inputLabel>";
				addChild(_timeZoneLabelTextField);
				
				_timeZoneComboBox = new ComboBoxTimeZone();
				addChild(_timeZoneComboBox);
			}

			_enableAlphaFilter = new BlurFilter(0,0,0);

			_emailTextInput = new TextInput();
			_emailTextInput.textField.filters = [_enableAlphaFilter];
			addChild(_emailTextInput);
			_passwordTextInput = new TextInput();
			_passwordTextInput.displayAsPassword = true;
			_passwordTextInput.textField.filters = [_enableAlphaFilter];
			addChild(_passwordTextInput);
			_conformPasswordTextInput = new TextInput();
			_conformPasswordTextInput.displayAsPassword = true;
			_conformPasswordTextInput.textField.filters = [_enableAlphaFilter];
			addChild(_conformPasswordTextInput);
			if(GlobalValue.getValue("IsNeedPinCode")=="1" || GlobalValue.getValue("IsContainTwoParts")=="1")
			{
				_mobileTextInput = new TextInput();
				_mobileTextInput.textField.filters = [_enableAlphaFilter];
				addChild(_mobileTextInput);
			}
			if(GlobalValue.getValue("IsNeedSerialNumber")=="1")
			{
				_serialNumberTextInput = new TextInput();
				_serialNumberTextInput.textField.filters = [_enableAlphaFilter];
				addChild(_serialNumberTextInput);
			}
			
			_noRegisterCheckBox = new CheckBox();
			//if(GlobalValue.getValue("IsNeedPinCode")=="1")
			//{
				_noRegisterCheckBox.label = PageVariableReplacer.replaceAll(_textArray[5]);
			//}
			
			_noRegisterCheckBox.textField.filters = [_enableAlphaFilter];
			_noRegisterCheckBox.addEventListener(MouseEvent.CLICK, noRegisterCheckBoxClickHandler);
			addChild(_noRegisterCheckBox);
		}
		override protected function innerContainerLayout(_lastPositionY:Number):Number {
			var _maxLabelWidth:Number = 0;
			_emailLabelTextField.x = _horizontalPadding;
			_emailLabelTextField.y = _lastPositionY + 2 * _textMargin;
			_lastPositionY = _emailLabelTextField.y + _emailLabelTextField.height;
			_maxLabelWidth = Math.max(_maxLabelWidth, _emailLabelTextField.textWidth);

			_passwordLabelTextField.x = _horizontalPadding;
			_passwordLabelTextField.y = _lastPositionY + _textMargin;
			_lastPositionY = _passwordLabelTextField.y + _passwordLabelTextField.height;
			_maxLabelWidth = Math.max(_maxLabelWidth, _passwordLabelTextField.textWidth);

			_conformPasswordLabelTextField.x = _horizontalPadding;
			_conformPasswordLabelTextField.y = _lastPositionY + _textMargin;
			_lastPositionY = _conformPasswordLabelTextField.y + _conformPasswordLabelTextField.height;
			_maxLabelWidth = Math.max(_maxLabelWidth, _conformPasswordLabelTextField.textWidth);
			
			if(GlobalValue.getValue("IsNeedPinCode")=="1" || GlobalValue.getValue("IsContainTwoParts")=="1")
			{
				_mobileLabelTextField.x = _horizontalPadding;
				_mobileLabelTextField.y = _lastPositionY + _textMargin;
				_lastPositionY = _mobileLabelTextField.y + _mobileLabelTextField.height;
				_maxLabelWidth = Math.max(_maxLabelWidth, _mobileLabelTextField.textWidth);
			}
			
			if(GlobalValue.getValue("IsNeedSerialNumber")=="1")
			{
				_serialNumberLabelTextField.x = _horizontalPadding;
				_serialNumberLabelTextField.y = _lastPositionY + _textMargin;
				_lastPositionY = _serialNumberLabelTextField.y + _serialNumberLabelTextField.height;
				_maxLabelWidth = Math.max(_maxLabelWidth, _serialNumberLabelTextField.textWidth);
			}
			
			//DTD:1582,TimeZone, set _timeZoneLabelTextField postion
			if(GlobalValue.getValue("SupportTimeZone")=="1")
			{ 
				_timeZoneLabelTextField.x = _horizontalPadding;
				_timeZoneLabelTextField.y = _lastPositionY + _textMargin;
				_lastPositionY = _timeZoneLabelTextField.y + _timeZoneLabelTextField.height;
				_maxLabelWidth = Math.max(_maxLabelWidth, _timeZoneLabelTextField.textWidth);
			}

			var _inputFieldPositionX:Number = _horizontalPadding +  _maxLabelWidth + _textMargin;
			var _inputFieldWidth:Number = _contentWidth - _maxLabelWidth - _textMargin;

			_emailTextInput.x = _inputFieldPositionX;
			_emailTextInput.y = _emailLabelTextField.y - (_emailTextInput.height - _emailLabelTextField.height) / 2;
			_emailTextInput.width = _inputFieldWidth;
			_passwordTextInput.x =_inputFieldPositionX;
			_passwordTextInput.y = _passwordLabelTextField.y - (_passwordTextInput.height - _passwordLabelTextField.height) / 2;
			_passwordTextInput.width = _inputFieldWidth;
			_conformPasswordTextInput.x = _inputFieldPositionX;
			_conformPasswordTextInput.y = _conformPasswordLabelTextField.y - (_conformPasswordTextInput.height - _conformPasswordLabelTextField.height) / 2;
			_conformPasswordTextInput.width = _inputFieldWidth;
			
			if(GlobalValue.getValue("IsNeedPinCode")=="1" || GlobalValue.getValue("IsContainTwoParts")=="1")
			{
			_mobileTextInput.x=_inputFieldPositionX;
			_mobileTextInput.y = _mobileLabelTextField.y - (_mobileTextInput.height - _mobileLabelTextField.height) / 2;
			_mobileTextInput.width = _inputFieldWidth;
			}
			
			if(GlobalValue.getValue("IsNeedSerialNumber")=="1")
			{
			_serialNumberTextInput.x=_inputFieldPositionX;
			_serialNumberTextInput.y = _serialNumberLabelTextField.y - (_serialNumberTextInput.height - _serialNumberLabelTextField.height) / 2;
			_serialNumberTextInput.width = _inputFieldWidth;
			}
			
			
			//DTD:1582,TimeZone, set _timeZoneComboBox postion
			if(GlobalValue.getValue("SupportTimeZone")=="1")
			{ 
				_timeZoneComboBox.x = _inputFieldPositionX;
				_timeZoneComboBox.y = _timeZoneLabelTextField.y;
			}
			
			
			trace(_mobileTextInput);
			_noRegisterCheckBox.x = _horizontalPadding;
			_noRegisterCheckBox.y = _lastPositionY + _textMargin;
			_noRegisterCheckBox.width = _contentWidth;
			_lastPositionY = _noRegisterCheckBox.y + _noRegisterCheckBox.height;

			//_lastPositionY += 2 * _textMargin;
			return _lastPositionY;
		}
		private function setEnabled(_setCheckBox:Boolean):void {
			_emailTextInput.enabled = true;
			_emailTextInput.mouseChildren = true;
			_passwordTextInput.enabled = true;
			_passwordTextInput.mouseChildren = true;
			_conformPasswordTextInput.enabled = true;
			_conformPasswordTextInput.mouseChildren = true;
			if(_mobileTextInput!=null)
			{
				_mobileTextInput.enabled = true;
				_mobileTextInput.mouseChildren = true;
			}
			
			if(_serialNumberTextInput!=null)
			{
				_serialNumberTextInput.enabled = true;
				_serialNumberTextInput.mouseChildren = true;
			}
			
			if (_setCheckBox) {
				_noRegisterCheckBox.enabled = true;
				_noRegisterCheckBox.mouseChildren = true;
			}
			
			if (_timeZoneComboBox)
			{
				_timeZoneComboBox.enable = true;
				_timeZoneComboBox.mouseChildren = true;
			}
			
			_isRegister = true;
		}
		private function setDisabled(_setCheckBox:Boolean):void {
			_emailTextInput.enabled = false;
			_emailTextInput.mouseChildren = false;
			_passwordTextInput.enabled = false;
			_passwordTextInput.mouseChildren = false;
			_conformPasswordTextInput.enabled = false;
			_conformPasswordTextInput.mouseChildren = false;
			if(_mobileTextInput!=null)
			{
				_mobileTextInput.enabled = false;
				_mobileTextInput.mouseChildren = false;
			}
			
			if(_serialNumberTextInput!=null)
			{
				_serialNumberTextInput.enabled = false;
				_serialNumberTextInput.mouseChildren = false;
			}
			
			if (_setCheckBox) {
				_noRegisterCheckBox.enabled = false;
				_noRegisterCheckBox.mouseChildren = false;
			}
			
			if (_timeZoneComboBox)
			{
				_timeZoneComboBox.enable = false;
				_timeZoneComboBox.mouseChildren = false;
			}
			
			_isRegister = false;
		}
		private function noRegisterCheckBoxClickHandler(_event:MouseEvent):void {
			if (_noRegisterCheckBox.selected) {
				setDisabled(false);
			} else {
				setEnabled(false);
			}
		}
		override public function primaryButtonClickedHandler(_event:MouseEvent):void {
			if (!_isRegister) {
				_page.pageVariable = 0;
				var _feedbacksXML:FeedbacksXML = new FeedbacksXML();
				var _feedbackXMLNode:FeedbackXMLNode = new FeedbackXMLNode("", "0");
				_feedbacksXML.addFeedback(_feedbackXMLNode);
				//trace(_feedbacksXML.toString());
				this.dispatchEvent(new SubmitEvent("submit", _feedbacksXML.xml, false));
				nextPage();
			} else {
				if (_emailTextInput.text.length == 0 || _passwordTextInput.text.length == 0 || _conformPasswordTextInput.text.length == 0 || (_mobileTextInput!=null && _mobileTextInput.text.length ==0)) {
					infoPage(GlobalValue.getValue("messages")["RegisterInfoRequired"]);
					_event.target.enabled = true;
					return;
				}
				if (_passwordTextInput.text != _conformPasswordTextInput.text) {
					infoPage(GlobalValue.getValue("messages")["RegisterPasswordConfirm"]);
					_event.target.enabled = true;
					return;
				}
				if(_serialNumberTextInput!=null)
				{
					var flag:int = 0;
					if(_serialNumberTextInput.text.length == 11 || _serialNumberTextInput.text.length == 13)
					{
						var firstChar:String = _serialNumberTextInput.text.substr(0,1);
						var secondChar:String = _serialNumberTextInput.text.substr(1,1);
						trace("@@@@@@@@@@@@#####################$$$$$$$$$$$$$$$$$");
						trace(firstChar);
						trace(secondChar);
						
						if((firstChar=="X"||firstChar=="D"||firstChar=="C")&&(secondChar=="A"||secondChar=="B"||secondChar=="C"||secondChar=="D"||secondChar=="E"))
						{
							flag = 1;
						}
					}
					trace(flag);
					if(flag == 0)
					{
						infoPage(GlobalValue.getValue("messages")["SerialNumberFormatError"]);
						_event.target.enabled = true;
						return;
					}
				}
				
				setDisabled(true);
				var mobile = "";
				var serialNumber = "";
				if(_mobileTextInput!=null)
				{
					mobile = _mobileTextInput.text;
				}
				if(_serialNumberTextInput!=null)
				{
					serialNumber = _serialNumberTextInput.text;
				}
				var _registerXML:XML = <Register></Register>;
				_registerXML.@Email = _emailTextInput.text;
				_registerXML.@Password = _passwordTextInput.text;
				_registerXML.@Mobile= mobile;
				_registerXML.@SerialNumber = serialNumber;
				if(_timeZoneComboBox)
				_registerXML.@TimeZone = _timeZoneComboBox.selectedItem.value;
				this.dispatchEvent(new SubmitEvent("submit", _registerXML));
				startLoadingPage();
			}
		}
		override public function submitSuccessfulCallBack(_message:String):void {
			stopLoadingPage();
			trace("Register message:");
			trace(_message);
			var _flag:String = _message.charAt(0);
			switch (_flag) {
				case "0" :
					_page.pageVariable = -1;
					var _messageObject:MessageObject = new MessageObject("Error", "OK");
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
				case "2":
					var ctppURL = GlobalValue.getValue("ctppURL");
					WindowLocation.href = ctppURL + "?CTPP=" + _message.substr(2);
					break;
				default :
					throw new IllegalOperationError("Invalid register server state.");
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