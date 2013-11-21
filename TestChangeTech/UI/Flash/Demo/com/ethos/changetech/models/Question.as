package com.ethos.changetech.models{
	public class Question {
		private var _type:String;
		private var _answerAlternatives:String;
		private var _unit:String;
		private var _variables:String;
		private var _subQuestions:Array;

		public function Question() {
			_subQuestions = new Array();
		}
		public function get type():String {
			return _type;
		}
		public function set type(_value:String):void {
			if (_value == type) {
				return;
			}
			_type = _value;
		}
		public function get answerAlternatives():String {
			return _answerAlternatives;
		}
		public function set answerAlternatives(_value:String):void {
			if (_value == _answerAlternatives) {
				return;
			}
			_answerAlternatives =_value;
		}
		public function get unit():String {
			return _unit;
		}
		public function set unit(_value:String):void {
			if (_value == _unit) {
				return;
			}
			_unit = _value;
		}
		public function get variables():String {
			return _variables;
		}
		public function set variables(_value:String):void {
			if (_value == _variables) {
				return;
			}
			_variables = _value;
		}
		public function get subQuestions():Array {
			return _subQuestions;
		}
		public function fromXML(_data:XML):void {
			_type = _data.@Type;
			_answerAlternatives = _data.@QuestionAnswerAlternatives;
			_unit = _data.@Unit;
			_variables = _data.@QuestionVariables;
			for each (var _subQuestionNode:XML in _data.Question) {
				var _subQuestion:Question = new Question();
				_subQuestion.fromXML(_subQuestionNode);
				addSubQuestion(_subQuestion);
			}
		}
		private function addSubQuestion(_subQuestion:Question):void {
			_subQuestions.push(_subQuestion);
		}
	}
}