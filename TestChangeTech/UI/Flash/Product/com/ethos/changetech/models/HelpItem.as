package com.ethos.changetech.models{
	public class HelpItem {
		private var _title:String;
		private var _text:String;

		public function HelpItem(_xml:XML) {
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
		public function fromXML(_data:XML):void {
			_title = _data.@Title;
			_text = _data.@Text;
		}
	}
}