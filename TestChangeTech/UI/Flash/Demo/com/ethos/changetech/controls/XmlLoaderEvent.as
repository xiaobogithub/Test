package com.ethos.changetech.controls{
	import flash.events.Event;

	public class XmlLoaderEvent extends Event {
		public static  const COMPELETED:String = "compeleted";
		public static  const FAILD:String = "failed";
		private var _data:String;

		public function XmlLoaderEvent(_type, _loadedData:String) {
			super(_type);
			_data = _loadedData;
		}
		public function get data():String {
			return _data;
		}
		public function set data(value:String):void {
			if (value == _data) {
				return;
			}
			_data = value;
		}
		public override function clone():Event {
			return new XmlLoaderEvent(type,data);
		}
		public override function toString():String {
			return formatToString("XmlLoaderEvent","type","bubbles","cancelable","eventPhase");
		}
	}
}