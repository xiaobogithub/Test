package com.ethos.changetech.controls{
	import fl.controls.Button;
	import fl.controls.Label;
	import fl.controls.TextInput;
	import flash.display.Sprite;
	import flash.events.Event;
	import flash.events.KeyboardEvent;
	import flash.events.MouseEvent;
	import flash.text.TextFieldAutoSize;
	import flash.ui.Keyboard;

	public class LoginControl extends Sprite {

		private static  var _username:String = "changetech";
		private static  var _password:String = "ethos";

		private var _usernameTextInput:TextInput;
		private var _passwordTextInput:TextInput;
		private var _usernameLabel:Label;
		private var _passwordLabel:Label;
		private var _statusLabel:Label;
		private var _submitBtn:Button;
		private var _resetBtn:Button;

		public function LoginControl() {
			setupLabels();
			setupInputFields();
			setupButton();
			setupKeyEvents();
		}
		private function setupLabels():void {
			_usernameLabel = new Label();
			_usernameLabel.move(0,0);
			_usernameLabel.text = "Username:";
			addChild(_usernameLabel);

			_passwordLabel = new Label();
			_passwordLabel.move(0,30);
			_passwordLabel.text = "Password:";
			addChild(_passwordLabel);

			_statusLabel = new Label();
			_statusLabel.move(0,60);
			_statusLabel.autoSize = TextFieldAutoSize.LEFT;
			_statusLabel.text = "";
			addChild(_statusLabel);
		}
		private function setupInputFields():void {
			_usernameTextInput =new TextInput();
			_usernameTextInput.move(60,00);
			_usernameTextInput.addEventListener(Event.CHANGE, textEntered);
			addChild(_usernameTextInput);

			_passwordTextInput = new TextInput();
			_passwordTextInput.move(60,30);
			_passwordTextInput.displayAsPassword = true;
			_passwordTextInput.addEventListener(Event.CHANGE, textEntered);
			addChild(_passwordTextInput);
		}
		private function setupButton():void {
			_submitBtn = new Button();
			_submitBtn.move(60,80);
			_submitBtn.label = "OK";
			_submitBtn.enabled = false;
			_submitBtn.setSize(50,20);
			_submitBtn.addEventListener(MouseEvent.CLICK, submitLogin);
			addChild(_submitBtn);

			_resetBtn = new Button();
			_resetBtn.move(115,80);
			_resetBtn.label = "Cancel";
			_resetBtn.enabled = false;
			_resetBtn.setSize(50,20);
			_resetBtn.addEventListener(MouseEvent.CLICK, resetLogin);
			addChild(_resetBtn);
		}
		private function setupKeyEvents():void {
			this.addEventListener(KeyboardEvent.KEY_DOWN, keySubmitLogin);
		}
		private function textEntered(_event:Event):void {
			if (_usernameTextInput.text != "" && _passwordTextInput.text != "") {
				_submitBtn.enabled = true;
				_resetBtn.enabled = true;
			} else {
				_submitBtn.enabled = false;
				_resetBtn.enabled = false;
			}
		}
		private function resetLogin(_event:Event):void {
			_usernameTextInput.text ="";
			_passwordTextInput.text ="";
			_submitBtn.enabled = false;
			_resetBtn.enabled = false;
		}
		private function submitLogin(_event:MouseEvent):void {
			validation();
		}
		private function keySubmitLogin(_event:KeyboardEvent):void {
			if (_event.keyCode == Keyboard.ENTER) {
				//trace("Key");
				validation();
			}
		}
		private function validation():void {
			// TODO: show validating message.
			if (_usernameTextInput.text == _username && _passwordTextInput.text == _password) {
				_statusLabel.text = "Login has been submitted.";
				_submitBtn.removeEventListener(MouseEvent.CLICK, submitLogin);
				_resetBtn.removeEventListener(MouseEvent.CLICK, resetLogin);
				this.removeEventListener(KeyboardEvent.KEY_DOWN, keySubmitLogin);
				this.dispatchEvent(new LoginEvent("passed"));
			} else {
				_statusLabel.text ="Invalide username or password";
				this.dispatchEvent(new LoginEvent("invalid"));
			}
		}
	}

}