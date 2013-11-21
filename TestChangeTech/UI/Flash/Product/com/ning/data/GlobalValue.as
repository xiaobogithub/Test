package com.ning.data{

	public class GlobalValue {

		private static  var _instance:GlobalValue;
		private var _object:Object;

		public function GlobalValue() {
			_object = new Object();
		}
		public static function init():void {
			_instance = new GlobalValue();
		}
		public static function getInstance():GlobalValue {
			if (_instance == null) {
				init();
			}
			return _instance;
		}
		public static function setValue(_name:String, _value:Object):void {
			_instance._object[_name] = _value;
		}
		public static function getValue(_name:String):Object {
			if (_instance._object[_name] == undefined) {
				return null;
			}
			return _instance._object[_name];
		}
	}
}