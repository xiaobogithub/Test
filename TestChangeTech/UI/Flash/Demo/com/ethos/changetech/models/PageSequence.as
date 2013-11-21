package com.ethos.changetech.models{
	public class PageSequence {
		private var _session:Session;
		private var _pageSequenceOrderNo:int;
		private var _name:String;
		private var _predictorCategoryName:String;
		private var _description:String;
		private var _foreground:String;
		private var _pages:Array;

		public function PageSequence(_parentSession:Session) {
			_session = _parentSession;
			_pages = new Array();
		}
		public function get session():Session {
			return _session;
		}
		public function get pageSequenceOrderNo():int {
			return _pageSequenceOrderNo;
		}
		public function set pageSequenceOrderNo(_value:int):void {
			if (_value == _pageSequenceOrderNo) {
				return;
			}
			_pageSequenceOrderNo = _value;
		}
		public function get name():String {
			return _name;
		}
		public function set name(_value:String):void {
			if (_value == _name) {
				return;
			}
			_name = _value;
		}
		public function get predictorCategoryName():String {
			return _predictorCategoryName;
		}
		public function set predictorCategoryName(_value:String):void {
			if (_value == _predictorCategoryName) {
				return;
			}
			_predictorCategoryName = _value;
		}
		public function get description():String {
			return _description;
		}
		public function set description(_value:String):void {
			if (_value == _description) {
				return;
			}
			_description = _value;
		}
		public function get foreground():String {
			return _foreground;
		}
		public function set foreground(_value:String):void {
			if (_value == _foreground) {
				return;
			}
			_foreground = _value;
		}
		public function get pages():Array {
			return _pages;
		}
		public function fromXML(_data:XML):void {
			_pageSequenceOrderNo = _data.@PageSequenceOrderNo;
			_data = _data.PageSequence[0];
			_name = _data.@Name;
			_predictorCategoryName = _data.@PredictorCategoryName;
			_description = _data.@Description;
			_foreground = _data.@Foreground;
			validation();
			for each (var pageNode:XML in _data.PAGE) {
				var _page:Page = new Page(this);
				_page.fromXML(pageNode);
				addPage(_page);
			}
		}
		private function validation():void {
			trace("----------------------------------------------------");
			trace("  **page sequcnce No. "+_pageSequenceOrderNo);
			trace("    page sequence name = "+_name);
			trace("    page sequence predictor category name = "+_predictorCategoryName);
			trace("    page sequence description = "+_description);
			trace("    page sequence foreground = "+_foreground);
		}
		private function addPage(_page:Page):void {
			_pages.push(_page);
		}
	}
}