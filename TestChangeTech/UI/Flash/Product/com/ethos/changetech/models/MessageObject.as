package com.ethos.changetech.models{
	public class MessageObject {

		private var _title:String;
		private var _backButtonLabel:String;
		private var _message:String;
		private var _messageImageURL:String;
		private var _type:String = "standard";// "help", "tip friend", "profile", "pause", "exit"
		private var _model:Object;

		public function MessageObject(_messageTitle:String, _buttonLabel:String) {
			_title = _messageTitle;
			_backButtonLabel = _buttonLabel;
		}
		public function get title():String {
			return _title;
		}
		public function set title(_value:String):void {
			_title = _value;
		}
		public function get backButtonLabel():String {
			return _backButtonLabel;
		}
		public function set backButtonLabel(_value:String):void {
			_backButtonLabel = _value;
		}
		public function get message():String {
			return _message;
		}
		public function set message(_value:String):void {
			_message = _value;
		}
		public function get messageImageURL():String {
			return _messageImageURL;
		}
		public function set messageImageURL(_value:String):void {
			_messageImageURL = _value;
		}
		public function get type():String {
			return _type;
		}
		public function set type(_value:String):void {
			_type = _value;
		}
		public function get model():Object {
			return _model;
		}
		public function set model(_value:Object):void {
			_model = _value;
		}
	}
}