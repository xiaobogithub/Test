package com.ning.text{
	import flash.errors.IllegalOperationError;
	
	public class StringExtension {
		public static var emailPattern:RegExp = new RegExp("^[_a-z0-9-]+(\\.[_a-z0-9-]+)*@[a-z0-9-]+(\\.[a-z0-9-]+)+$","xi");
		
		public static function stringToBoolean(_string:String):Boolean {
			if (_string == null || _string == "") {
				return false;
			}
			var _lowerString:String = _string.toLowerCase();
			_lowerString = removeEndsSpace(_lowerString);
			if (_lowerString == "false" || _lowerString == "0") {
				return false;
			}
			if (_lowerString == "true" || _lowerString == "1") {
				return true;
			}
			throw new IllegalOperationError("Can not convert String \"" + _string + "\" to Boolean.");
		}
		public static function removeEndsSpace(_string:String):String {
			while (_string.charAt(0) === " ") {
				_string = _string.substr(1);
			}
			while (_string.charAt(_string.length - 1) === " ") {
				_string = _string.substr(0, _string.length - 1);
			}
			return _string;
		}
		public static function smartSplit(_string:String, _char:*, _limit:Number = 0x7fffffff):Array {
			var _array:Array = _string.split(_char);
			if (_array.length == 1) {
				return _array;
			}
			var _newArray:Array = new Array();
			var _lastItem:String = _array[0];
			_newArray.push(_lastItem);
			for (var _i:int = 1; _i < _array.length; _i++) {
				if(_lastItem.charAt(_lastItem.length - 1) == "\\") {
					_lastItem = _lastItem.substr(0, _lastItem.length - 1)+ _char + _array[_i]
					_newArray[_newArray.length - 1] = _lastItem;
				}else {
					_lastItem = _array[_i];
					_newArray.push(_lastItem);
				}
				
			}
			return _newArray;
		}
		public static function isEmail(_string:String):Boolean {
			return emailPattern.test(_string)
		}
	}
}