package com.ethos.changetech.utils{
	import flash.errors.IllegalOperationError;

	public class PageExpressionFunctions {
		static public function getIndex(_expression:String):int {
			//throw new IllegalOperationError("GetIndex function is not supported in expression yet.");
			var _itemArray:Array = _expression.split(",");
			if (_itemArray.length < 3 || _itemArray[_itemArray.length - 1].match(/(min)|(max)/) == null) {
				throw new IllegalOperationError("Invalid parameters in GetIndex function.");
			}
			// Get option.
			var _optionMatch:Array = _itemArray.pop().match(/(max)|(min)/xi);
			if (_optionMatch == null) {
				throw new IllegalOperationError("Invalid option in GetIndex function, the option should be max or min.");
			}
			var _option:String = _optionMatch[0].toLowerCase();

			// Get sorted indes.
			var _sortedIndex:int = int(PageExpressionParser.parseMathExpression(_itemArray.pop()));
			if (isNaN(_sortedIndex)) {
				throw new IllegalOperationError("Invalid sorted index in GetIndex function.");
			}
			if (_sortedIndex <= 0 || _sortedIndex > _itemArray.length) {
				throw new IllegalOperationError("Sorted index in GetIndex function is out of range. The index should be from 1 to the amount of values.");
			}
			// Get sorted array.
			var _valuesArray:Array = _itemArray.map(expressionToValue);
			trace("values = " + _valuesArray);
			var _sortedArray:Array = new Array();
			switch (_option) {
				case "min" :
					_sortedArray = _valuesArray.sort(Array.NUMERIC | Array.RETURNINDEXEDARRAY);
					break;
				case "max" :
					_sortedArray = _valuesArray.sort(Array.NUMERIC | Array.DESCENDING | Array.RETURNINDEXEDARRAY);
					break;
				default :
					throw new IllegalOperationError("Invalid option in GetIndex function(internal error, please connect to the developer).");
			}
			trace("sorted index = " + _sortedArray);
			return _sortedArray[_sortedIndex - 1] + 1;
		}
		
		static private function expressionToValue(_item:*, _index:int, _array:Array):Number {
			return Number(PageExpressionParser.parseMathExpression(_item));
		}
		
		static public function round(_expression:String):Number {
			var _itemArray:Array = _expression.split(",");
			var _temp:int = 1;
			var _accuracyInt:int = parseInt(_itemArray[1]);
			var i:int = 0;
			for(i = 0; i < _accuracyInt; i++){
				_temp=_temp*10;
			}
            return Math.round(_itemArray[0]*_temp)/_temp;			
		}
	}
}