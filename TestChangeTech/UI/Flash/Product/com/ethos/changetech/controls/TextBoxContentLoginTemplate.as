package com.ethos.changetech.controls{
	import com.ethos.changetech.events.PageEvent;
	import com.ethos.changetech.events.SubmitEvent;
	import com.ethos.changetech.models.MessageObject;
	import com.ethos.changetech.models.Page;
	import com.ethos.changetech.utils.PageVariableReplacer;
	import com.ethos.changetech.utils.WindowLocation;
	import com.ning.data.GlobalValue;
	import com.ning.events.LoginEvent;
	import com.ning.text.StaticTextField;
	import com.ning.text.StringExtension;
	import fl.controls.TextInput;
	import flash.display.Sprite;
	import flash.errors.IllegalOperationError;
	import flash.events.Event;
	import flash.events.MouseEvent;
	import flash.filters.BlurFilter;
	import flash.text.AntiAliasType;
	import flash.text.StyleSheet;
	import flash.text.TextFieldAutoSize;

	public class TextBoxContentLoginTemplate extends TextBoxContentStandardBasedTemplate {

		private var _usernameLabelTextField:StaticTextField;
		private var _passwordLabelTextField:StaticTextField;
		private var _usernameTextInput:TextInput;
		private var _passwordTextInput:TextInput;
		private var _passwordReminderSprite:Sprite;
		private var _enableAlphaFilter:BlurFilter;

		public function TextBoxContentLoginTemplate(_targetPage:Page, _w:Number, _minH:Number, _maxH:Number) {
			super(_targetPage, _w, _minH, _maxH);
			this.addEventListener(Event.ADDED_TO_STAGE, addToStage);
		}
		
		private function addToStage(e:Event):void 
		{
			removeEventListener(Event.ADDED_TO_STAGE, addToStage);
			stage.focus = _usernameTextInput;//DTD154
		}
		override protected function updateInnerContent():void {
			var _textArray:Array = StringExtension.smartSplit(_page.text, ";");
			_textTextField.htmlText = "<pageText>" + PageVariableReplacer.replaceAll(_textArray[0]) + "</pageText>";
			_footerTextTextField.htmlText = "<hyperlink>" + PageVariableReplacer.replaceAll(_page.footerText) + "</hyperlink>";

			_usernameLabelTextField = new StaticTextField(TextFieldAutoSize.LEFT, false, true, AntiAliasType.ADVANCED);
			_usernameLabelTextField.textColor = 0x999999;
			_usernameLabelTextField.styleSheet = StyleSheet(GlobalValue.getValue("css"));
			_usernameLabelTextField.htmlText = "<inputLabel>" + _textArray[1] + "</inputLabel>";
			addChild(_usernameLabelTextField);

			_passwordLabelTextField = new StaticTextField(TextFieldAutoSize.LEFT, false, true, AntiAliasType.ADVANCED);
			_passwordLabelTextField.textColor = 0x999999;
			_passwordLabelTextField.styleSheet = StyleSheet(GlobalValue.getValue("css"));
			_passwordLabelTextField.htmlText = "<inputLabel>" + _textArray[2] + "</inputLabel>";
			addChild(_passwordLabelTextField);

			_enableAlphaFilter = new BlurFilter(0,0,0);
			
			_usernameTextInput = new TextInput();
			_usernameTextInput.textField.filters = [_enableAlphaFilter];
			addChild(_usernameTextInput);
			_passwordTextInput = new TextInput();
			_passwordTextInput.displayAsPassword = true;
			_passwordTextInput.textField.filters = [_enableAlphaFilter];
			addChild(_passwordTextInput);

			_passwordReminderSprite = new Sprite();
			_passwordReminderSprite.buttonMode = true;
			_passwordReminderSprite.useHandCursor = true;
			_passwordReminderSprite.addEventListener(MouseEvent.CLICK, passwordReminderClickHandler);
			addChild(_passwordReminderSprite);
			
			
		}
		override protected function innerContainerLayout(_lastPositionY:Number):Number {
			var _maxLabelWidth:Number = 0;
			_usernameLabelTextField.x = _horizontalPadding;
			_usernameLabelTextField.y = _lastPositionY + 2 * _textMargin;
			_lastPositionY = _usernameLabelTextField.y + _usernameLabelTextField.height;
			_maxLabelWidth = Math.max(_maxLabelWidth, _usernameLabelTextField.textWidth);

			_passwordLabelTextField.x = _horizontalPadding;
			_passwordLabelTextField.y = _lastPositionY + _textMargin;
			_lastPositionY = _passwordLabelTextField.y + _passwordLabelTextField.height;
			_maxLabelWidth = Math.max(_maxLabelWidth, _passwordLabelTextField.textWidth);
			
			var _inputFieldPositionX:Number = _maxLabelWidth + _horizontalPadding + _textMargin;
			var _inputFieldWidth:Number = _contentWidth - _maxLabelWidth - _textMargin;

			_usernameTextInput.x = _inputFieldPositionX;
			_usernameTextInput.y = _usernameLabelTextField.y - (_usernameTextInput.height - _usernameLabelTextField.height) / 2;
			_usernameTextInput.width = _inputFieldWidth;
			_passwordTextInput.x = _inputFieldPositionX;
			_passwordTextInput.y = _passwordLabelTextField.y - (_passwordTextInput.height - _passwordLabelTextField.height) / 2;
			_passwordTextInput.width = _inputFieldWidth;
			
			
			
			return _lastPositionY;
		}
		override protected function footerTextFieldLayout(_lastPositionY:Number):Number {
			_footerTextTextField.x = _horizontalPadding;
			_footerTextTextField.y = _lastPositionY + 2 * _textMargin;
			_footerTextTextField.width = _contentWidth;
			
			_passwordReminderSprite.x = _horizontalPadding;
			_passwordReminderSprite.y = _lastPositionY + 2 * _textMargin;
			_passwordReminderSprite.graphics.clear();
			_passwordReminderSprite.graphics.beginFill(0xFFFFFF, 0);
			_passwordReminderSprite.graphics.drawRect(0, 0, _footerTextTextField.textWidth, _footerTextTextField.textHeight);
			_passwordReminderSprite.graphics.endFill();
			_lastPositionY = _passwordReminderSprite.y + _passwordReminderSprite.height;
			
			_lastPositionY += 5;
			return _lastPositionY;
		}
		override public function primaryButtonClickedHandler(_event:MouseEvent):void {
			if (_usernameTextInput.text.length == 0 || _passwordTextInput.text.length == 0) {
				infoPage(GlobalValue.getValue("messages")["LoginInfoRequired"]);
				this.dispatchEvent(new PageEvent("setbutton", {enabled:true}));
				return;
			}
			_usernameTextInput.enabled = false;
			_passwordTextInput.enabled = false;
			var _loginXML:XML = <Login></Login>;
			_loginXML.@Username = _usernameTextInput.text;
			_loginXML.@Password = _passwordTextInput.text;
			this.dispatchEvent(new SubmitEvent("submit", _loginXML));
		}
		override public function submitSuccessfulCallBack(_message:String):void {
			trace("login message:");
			trace(_message);
			var _flag:String = _message.charAt(0);
			switch (_flag) {
				case "0" :
					var _messageObject:MessageObject = new MessageObject("Error", "OK");
					_messageObject.message = _message.substr(2);
					infoPage(_messageObject);
					this.dispatchEvent(new PageEvent("setbutton", {enabled:true}));
					_usernameTextInput.enabled = true;
					_passwordTextInput.enabled = true;
					break;
				case "1" :
					this.dispatchEvent(new LoginEvent("complete", _message.substr(2)));
					break;
				case "2":
					var ctppURL = GlobalValue.getValue("ctppURL");
					WindowLocation.href = ctppURL + "?CTPP=" + _message.substr(2);
					break;
				default :
					throw new IllegalOperationError("Invalid login server state.");
			}
		}
		override public function submitFailedCallBack(_message:String):void {
			infoPage(GlobalValue.getValue("messages")["NetworkError"]);
			this.dispatchEvent(new PageEvent("setbutton", {enabled:true}));
		}
		private function passwordReminderClickHandler(_event:MouseEvent):void {
			nextPage();
		}
	}
}