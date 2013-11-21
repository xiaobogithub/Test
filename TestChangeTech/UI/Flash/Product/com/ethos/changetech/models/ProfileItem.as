package com.ethos.changetech.models{
	
	public class ProfileItem {
		
		private var _name:String;
		private var _oldValue:String;
		private var _format:String;
		
		public function ProfileItem(_xml:XML){
			fromXML(_xml);
		}
		public function get name():String {
			return _name;
		}
		public function set name(_value:String):void {
			if(_value == _name) {
				return;
			}
			_name = _value;
		}
		public function get oldValue():String {
			return _oldValue;
		}
		public function set oldValue(_value:String):void {
			if(_value == _oldValue) {
				return;
			}
			_oldValue = _value;
		}
		public function get format():String {
			return _format;
		}
		public function set format(_value:String):void {
			if(_value == _format){
				return;
			}
			_format = _value;
		}
		public function fromXML(_data:XML):void {
			_name = _data.@Name;
			_oldValue = _data.@OldValue;
			_format = _data.@Format;
		}
	}
}