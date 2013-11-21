package com.ethos.changetech.models{
	import com.ning.math.Range;
	import flash.errors.IllegalOperationError;

	public class Graph {
		private var _page:Page;
		private var _guid:String;
		private var _type:String;
		private var _caption:String;
		private var _scoreRange:Range;
		private var _badScoreRanges:Array;
		private var _mediumScoreRanges:Array;
		private var _goodScoreRanges:Array;
		private var _timeRange:Range;
		private var _timeUnit:String;
		private var _timeBaselineUnit:String;
		private var _graphItems:Array;

		public function Graph(_parentPage:Page, _xml:XML) {
			_page = _parentPage;
			_badScoreRanges = new Array();
			_mediumScoreRanges = new Array();
		 	_goodScoreRanges = new Array();
			_graphItems = new Array();
			fromXML(_xml);
		}
		public function get page():Page {
			return _page;
		}
		public function get guid():String {
			return _guid;
		}
		public function get type():String {
			return _type;
		}
		public function get caption():String {
			return _caption;
		}
		public function get scoreRange():Range {
			return _scoreRange;
		}
		public function get badScoreRanges():Array {
			return _badScoreRanges;
		}
		public function get mediumScoreRanges():Array {
			return _mediumScoreRanges;
		}
		public function get goodScoreRanges():Array {
			return _goodScoreRanges;
		}
		public function get timeRange():Range {
			return _timeRange;
		}
		public function get timeUnit():String {
			return _timeUnit;
		}
		public function get timeBaselineUnit():String {
			return _timeBaselineUnit;
		}
		public function get graphItems():Array {
			return _graphItems;
		}
		public function fromXML(_data:XML):void {
			_guid = _data.@GUID;
			_type = _data.@Type;
			_caption = _data.@Caption;
			_scoreRange = getRanges(_data.@ScoreRange)[0];
			_badScoreRanges = getRanges(_data.@BadScoreRange);
			_mediumScoreRanges = getRanges(_data.@MediumScoreRange);
			_goodScoreRanges = getRanges(_data.@GoodScoreRange);
			_timeRange = getRanges(_data.@TimeRange)[0];
			_timeUnit = _data.@TimeUnit;
			_timeBaselineUnit = _data.@TimeBaselineUnit
			validation();
			var _graphItemsXMLList:XMLList = _data.Items;
			if ( _graphItemsXMLList.length()>0) {
				for each (var _graphItemNode:XML in _graphItemsXMLList[0].Item) {
					var _graphItem:GraphItem = new GraphItem();
					_graphItem.fromXML(_graphItemNode);
					addGraphItem(_graphItem);
				}
				trace("        graph item amount = " + _graphItems.length.toString());
			} else {
				trace("        No graph item in this graph!");
			}
		}
		private function validation():void {
			trace("    ###################################");
			trace("      --graph GUID = " + _guid);
			trace("        graph type = " + _type);
			trace("        graph caption = " + _caption);
			trace("        graph scroe range = " + _scoreRange);
			trace("        graph bad scroe range = " + _badScoreRanges);
			trace("        graph medium scroe range = " + _mediumScoreRanges);
			trace("        graph good scroe range = " + _goodScoreRanges);
			trace("        graph time range = " + _timeRange);
			trace("        graph time unit = " + _timeUnit);
			trace("        graph time base line unit = " + _timeBaselineUnit);			
		}
		private function getRanges(_string:String):Array {
			var _ranges:Array = new Array();
			if(_string.length > 0) {
				var _rangeStrings:Array = _string.split(";");
				for(var _i:int = 0; _i < _rangeStrings.length; _i++) {
					_ranges.push(Range.fromString(_rangeStrings[_i]));
				}
			}
			return _ranges;
		}
		private function addGraphItem(_graphItem:GraphItem):void {
			_graphItems.push(_graphItem);
		}
	}
}