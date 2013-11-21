package com.ethos.changetech.models{
	import flash.errors.IllegalOperationError;

	public class GraphItem {

		private var _name:String;
		private var _values:Array;
		private var _color:uint;
		private var _pointType:int;

		public function get name():String {
			return _name;
		}
		public function get values():Array {
			return _values;
		}
		public function get color():uint {
			return _color;
		}
		public function get pointType():int {
			return _pointType;
		}
		public function fromXML(_data:XML):void {
			_name = _data.@Name;
			_values = stringToValues(_data.@Values);
			_color = _data.@Color;
			_pointType = _data.@PointType;
			validation();
		}
		private function stringToValues(_string:String):Array {
			// value can be expression here too.
			return _string.split(";");
			//return _stringArray.map(toNumber);
		}
		/*private function toNumber(_element:*, _index:int, _array:Array):Number {
		var _number:Number = Number(_element);
		if (isNaN(_number)) {
		throw new IllegalOperationError("Invalid data format in graph item.");
		}
		return _number;
		}*/
		private function validation():void {
			trace("        ******************************");
			trace("          graph itme name = " + _name);
			trace("          graph item values = " + _values);
			trace("          graph item color = " + _color);
			trace("          graph item pointType = " + _pointType);
		}
	}
}