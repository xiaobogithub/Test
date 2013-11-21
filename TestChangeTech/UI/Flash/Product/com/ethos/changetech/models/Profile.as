package com.ethos.changetech.models{

	public class Profile {
		
		private var _title:String;
		private var _text:String;
		private var _submitButtonName:String;
		private var _backButtonName:String;
		private var _profileItems:Array;

		public function Profile(_xml:XML) {
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
			if(_value == _backButtonName) {
				return;
			}
			_backButtonName = _value;
		}
		public function get profileItems():Array {
			return _profileItems;
		}
		public function fromXML(_data:XML):void {
			_title = _data.@Title;
			_text = _data.@Text;
			_submitButtonName = _data.@SubmitButtonName;
			_backButtonName = _data.@BackButtonName;
			_profileItems = new Array();
			var _profileItemXMLList:XMLList = _data.Item;
			if ( _profileItemXMLList.length()>0) {
				for each (var _profileItemNode:XML in _profileItemXMLList) {
					addProfileItem(new ProfileItem(_profileItemNode));
				}
			}
		}
		private function addProfileItem(_profileItem:ProfileItem):void {
			_profileItems.push(_profileItem);
		}
	}
}