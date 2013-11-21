package com.ethos.changetech.models{
	import com.hexagonstar.util.debug.Debug;
	public class Session {
		private var _guid:String;
		private var _logoURL:String;
		private var _name:String;
		private var _description:String;
		private var _day:int;
		private var _sequences:Array;
		private var _pages:Array;

		public function Session(_xml:XML) {
			_sequences = new Array();
			_pages = new Array();
			fromXML(_xml);
		}
		public function get guid():String {
			return _guid;
		}
		public function get logoURL():String{
			return _logoURL;
		}
		public function get name():String {
			return _name;
		}
		public function get description():String {
			return _description;
		}
		public function get day():int {
			return _day;
		}
		public function get sequences():Array {
			return _sequences;
		}
		public function get pages():Array {
			return _pages;
		}
		public function fromXML(_data:XML):void {
			_guid = _data.@GUID;
			_logoURL = _data.@Logo;
			_name = _data.@Name;
			_description = _data.@Description;
			_day = _data.@Day;
			//validation();
			var _pageSequenceXMLList:XMLList = _data.PageSequence;
			if ( _pageSequenceXMLList.length() > 0) {
				var i = 0;
				for each (var _pageSequenceNode:XML in _pageSequenceXMLList) {
					addSequence(new PageSequence(this, _pageSequenceNode, i, false));
					i++;
				}
				Debug.trace("page sequence amount = " + _sequences.length.toString());
			} else {
				Debug.trace("No page sequence in this session!");
			}
		}
		private function validation():void {
			Debug.trace("session guid = " + _guid);
			Debug.trace("session logo = " + _logoURL);
			Debug.trace("session name = " + _name);
			Debug.trace("session description = " + _description);
			Debug.trace("day = " + _day);
			Debug.trace("=========================================================\n");
		}
		private function addSequence(_sequence:PageSequence):void {
			_sequences.push(_sequence);
		}
	}
}