package com.ethos.changetech.models{
	public class Help {
		private var _title:String;
		private var _backButtonName:String;
		private var _items:Array;

		public function Help(_xml:XML) {
			_items = new Array();
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
		public function get backButtonName():String {
			return _backButtonName;
		}
		public function set backButtonName(_value:String):void {
			if (_value == _backButtonName) {
				return;
			}
			_backButtonName = _value;
		}
		public function get items():Array {
			return _items;
		}
		public function fromXML(_data:XML):void {
			_title = _data.@Title;
			_backButtonName = _data.@BackButtonName;
			var _helpItemsXMLList:XMLList = _data.HelpItem;
			if (_helpItemsXMLList.length() > 0) {
				for each (var _helpItemNode:XML in _helpItemsXMLList) {
					addHelpItem(new HelpItem(_helpItemNode));
				}
			}
		}
		private function addHelpItem(_helpItem:HelpItem):void {
			_items.push(_helpItem);
		}
	}
}