package com.ethos.changetech.models{
	public class Session {
		private var _sessionGUID:String;
		private var _name:String;
		private var _description:String;
		private var _day:int;
		private var _sequences:Array;

		public function Session() {
			_sequences = new Array();
		}
		public function get sessionGUID():String {
			return _sessionGUID;
		}
		public function set sessionGUID(_value:String):void {
			if (_value == _sessionGUID) {
				return;
			}
			_sessionGUID = _value;
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
		public function get description():String {
			return _description;
		}
		public function set description(_value:String):void {
			if (_value == _description) {
				return;
			}
			_description = _value;
		}
		public function get day():int {
			return _day;
		}
		public function set day(_value:int):void {
			if (_value == _day) {
				return;
			}
			_day = _value;
		}
		public function get sequences():Array {
			return _sequences;
		}
		public function fromXML(_data:XML):void {
			_sessionGUID = _data.@SessionGUID;
			_name = _data.@Name;
			_description = _data.@Description;
			_day = _data.@Day;
			validation();
			for each (var _sessionSequenceNode:XML in _data.SessionContent) {
				var _pageSequence:PageSequence = new PageSequence(this);
				_pageSequence.fromXML(_sessionSequenceNode);
				addSequence(_pageSequence);
			}
		}
		private function validation():void {
			trace("session GUID = "+_sessionGUID);
			trace("session name = "+_name);
			trace("session description = "+ _description);
			trace("day = "+_day);
			trace("=========================================================\n");
		}
		private function addSequence(_sequence:PageSequence):void {
			_sequences.push(_sequence);
		}
	}
}