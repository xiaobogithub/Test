package com.ethos.changetech.models{
	import com.ning.math.Range;
	
	public class QuestionItem {
		private var _guid:String;
		private var _label:String;
		private var _range:Range;
		private var _feedback:String;
		private var _score:Number;

		public function get guid():String {
			return _guid;
		}
		public function get label():String {
			return _label;
		}
		public function get range():Range {
			return _range;
		}
		public function get feedback():String {
			return _feedback;
		}
		public function get score():Number {
			return _score;
		}
		public function fromXML(_data:XML):void {
			_guid = _data.@GUID;
			_label = _data.@Item;
			_range = Range.fromString(_data.@Range)
			_feedback = _data.@Feedback;
			_score = _data.@Score;
			validation();
		}
		private function validation():void {
			trace("        ******************************");
			trace("          question item GUID = " + _guid);
			trace("          question item label = " + _label);
			trace("          question item range = " + _range);
			trace("          question item feedback = " + _feedback);
			trace("          question item score = " + _score);

		}
	}
}