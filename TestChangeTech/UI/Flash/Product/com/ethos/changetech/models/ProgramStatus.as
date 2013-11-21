package com.ethos.changetech.models{

	public class ProgramStatus {

		private var _title:String;
		private var _text:String;
		private var _submitButtonName:String;
		private var _backButtonName:String;

		public function ProgramStatus(_xml:XML) {
			fromXML(_xml);
		}
		public function get title():String {
			return _title;
		}
		public function set title(_value:String):void {
			if (_value == _title) {
				return;
			}
			_title = _value;
		}
		public function get text():String {
			return _text;
		}
		public function set text(_value:String):void {
			if (_value == _text) {
				return;
			}
			_text = _value;
		}
		public function get submitButtonName():String {
			return _submitButtonName;
		}
		public function set submitButtonName(_value:String):void {
			if (_value == _submitButtonName) {
				return;
			}
			_submitButtonName = _value;
		}
		public function get backButtonName():String {
			return _backButtonName;
		}
		public function set backButtonName(_value:String):void {
			if (_value == _backButtonName) {
				return;
			}
			_backButtonName = _value;
		}
		public function fromXML(_data:XML):void {
			_title = _data.@Title;
			_text = _data.@Text;
			_submitButtonName = _data.@SubmitButtonName;
			_backButtonName = _data.@BackButtonName;
		}
	}
}