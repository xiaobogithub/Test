package com.ning.math{
	import flash.errors.IllegalOperationError;

	public class Range {

		private var _min:Number;
		private var _max:Number;

		public function Range(_numberA:Number, _numberB:Number) {
			_min = Math.min(_numberA, _numberB);
			_max = Math.max(_numberA, _numberB);
		}
		public function get min():Number {
			return _min;
		}
		public function set min(_value:Number):void {
			if (_value == _min) {
				return;
			}
			if (_value > _max) {
				throw new IllegalOperationError("min value must be not bigger than max value.");
			}
			_min = _value;
		}
		public function get max():Number {
			return _max;
		}
		public function set max(_value:Number):void {
			if (_value == _max) {
				return;
			}
			if (_value < _min) {
				throw new IllegalOperationError("max value must be not smaller than min value.");
			}
			_max = _value;
		}
		public function get diff():Number {
			return _max - _min;
		}
		static public function fromString(_string:String):Range {
			if(_string == null || _string.length == 0) {
				return null;
			}
			var _items:Array = _string.split("-");
			if (_items.length != 2) {
				throw new IllegalOperationError("Invalid format of string: " + _string + ". The format should be Number-Number");
			}
			return new Range(Number(_items[0]),Number(_items[1]));
		}
		public function toString():String {
			return _min + "-" + _max;
		}
	}
}