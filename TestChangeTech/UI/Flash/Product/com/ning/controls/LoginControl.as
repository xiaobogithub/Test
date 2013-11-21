package com.ning.controls{
	import com.ning.events.LoginEvent;
	import fl.controls.Button;
	import fl.controls.Label;
	import fl.controls.TextInput;
	import flash.display.Sprite;
	import flash.events.Event;
	import flash.events.IOErrorEvent;
	import flash.events.KeyboardEvent;
	import flash.events.MouseEvent;
	import flash.text.TextFieldAutoSize;
	import flash.ui.Keyboard;

	public class LoginControl extends Sprite {

		private var _loginURL:String;
		
		private var _username_ti:TextInput;
		private var _password_ti:TextInput;
		private var _username_label:Label;
		private var _password_label:Label;
		private var _status_label:Label;
		private var _submit_btn:Button;
		private var _reset_btn:Button;

		public function LoginControl(_targetURL:String) {
			_loginURL = _targetURL;
			initLabel();
			initInputField();
			initButton();
			initEvent();
		}
		public function get state():String{
			return _status_label.text;
		}
		public function set state(_value:String):void{
			if(_value == _status_label.text){return;}
			_status_label.text = _value;
		}
		private function initLabel():void {
			_username_label = new Label();
			_username_label.move(0,0);
			_username_label.text = "Username:";
			addChild(_username_label);

			_password_label = new Label();
			_password_label.move(0,30);
			_password_label.text = "Password:";
			addChild(_password_label);

			_status_label = new Label();
			_status_label.move(0,60);
			_status_label.autoSize = TextFieldAutoSize.LEFT;
			_status_label.text = "";
			addChild(_status_label);
		}
		private function initInputField():void {
			_username_ti =new TextInput();
			_username_ti.move(60,00);
			_username_ti.addEventListener(Event.CHANGE, textChangeHandler);
			addChild(_username_ti);

			_password_ti = new TextInput();
			_password_ti.move(60,30);
			_password_ti.displayAsPassword = true;
			_password_ti.addEventListener(Event.CHANGE, textChangeHandler);
			addChild(_password_ti);
		}
		private function initButton():void {
			_submit_btn = new Button();
			_submit_btn.move(60,80);
			_submit_btn.label = "Login";
			_submit_btn.enabled = false;
			_submit_btn.setSize(50,20);
			_submit_btn.addEventListener(MouseEvent.CLICK, loginHandler);
			addChild(_submit_btn);

			_reset_btn = new Button();
			_reset_btn.move(115,80);
			_reset_btn.label = "Reset";
			_reset_btn.enabled = false;
			_reset_btn.setSize(50,20);
			_reset_btn.addEventListener(MouseEvent.CLICK, resetHandler);
			addChild(_reset_btn);
		}
		private function initEvent():void {
			this.addEventListener(KeyboardEvent.KEY_DOWN, keyLoginHandler);
		}
		private function textChangeHandler(_event:Event):void {
			if (_username_ti.text.length > 0 && _password_ti.text.length > 0) {
				_submit_btn.enabled = true;
				_reset_btn.enabled = true;
			} else if (_username_ti.text.length == 0 && _password_ti.text.length == 0){
				_submit_btn.enabled = false;
				_reset_btn.enabled = false;
			} else {
				_submit_btn.enabled = false;
				_reset_btn.enabled = true;
			}
		}
		private function resetHandler(_event:Event):void {
			_username_ti.text ="";
			_password_ti.text ="";
			_submit_btn.enabled = false;
			_reset_btn.enabled = false;
		}
		private function loginHandler(_event:MouseEvent):void {
			validation();
		}
		private function keyLoginHandler(_event:KeyboardEvent):void {
			if (_event.keyCode == Keyboard.ENTER) {
				validation();
			}
		}
		private function validation():void {
			var _loginLoader = new StringLoader(_loginURL);
			_loginLoader.showProgressBar = true;
			_loginLoader.addEventListener(Event.COMPLETE,loginLoaderCompeleted);
			_loginLoader.addEventListener(IOErrorEvent.IO_ERROR,loginLoaderFailed);
			_loginLoader.send(_username_ti.text + ";" + _password_ti.text);			
			_status_label.text = "Connecting to server...";
			/*if (_username_ti.text == _username && _password_ti.text == _password) {
				_status_label.text = "Login has been submitted.";
				_submit_btn.removeEventListener(MouseEvent.CLICK, submitLogin);
				_reset_btn.removeEventListener(MouseEvent.CLICK, resetLogin);
				this.removeEventListener(KeyboardEvent.KEY_DOWN, keySubmitLogin);
				this.dispatchEvent(new LoginEvent("passed"));
			} else {
				_status_label.text ="Invalide username or password";
				this.dispatchEvent(new LoginEvent("invalid"));
			}*/
		}
		private function loginLoaderCompeleted(_event:Event):void{
			//trace("login gets response from server:");
			//trace(_event.data);
			//_status_label.text = "Login information has been submitted.";
			this.dispatchEvent(new LoginEvent("complete", _event.target.data));
		}
		private function loginLoaderFailed(_event:IOErrorEvent):void{
			//trace("Failed to connect with server.");
			_status_label.text = "Network error.";
			this.dispatchEvent(new LoginEvent("error", "Network error."));
		}
	}

}