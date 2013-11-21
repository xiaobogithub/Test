package com.ning.controls{
	import fl.controls.CheckBox;
	
	public class GroupableCheckBox extends CheckBox {
		private var _value:Object;

		public function get value():Object {
			return _value;
		}
		public function set value(_v:Object):void {
			if (_v == _value) {
				return;
			}
			_value = _v;
		}
		//group
		//groupName 
		//selected
	}
}