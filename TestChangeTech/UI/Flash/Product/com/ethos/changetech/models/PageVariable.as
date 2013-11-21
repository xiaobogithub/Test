package com.ethos.changetech.models{
	public class PageVariable {

		private var _name:String;
		private var _value:Object;
		private var _type:String;

		public function PageVariable(_variableName:String, _variableValue:Object, _variableType:String) {
			_name = _variableName;
			_value = _variableValue;
			_type = _variableType;
		}
		public function get name():String {
			return _name;
		}
		public function get value():Object {
			return _value;
		}
		public function set value(_v:Object):void {
			if (_v == _value) {
				return;
			}
			_value = _v;
		}
		public function get type():String {
			return _type;
		}
	}
}