package com.ethos.changetech.models{
	import com.ethos.changetech.data.PageVariables;
	import com.ning.text.StringExtension;
		
	public class Question {
		private var _page:Page;
		private var _guid:String;
		private var _type:String;
		private var _label:String;
		private var _isRequired:Boolean = false;
		private var _pageVariableName:String;		
		private var _questionItems:Array;

		private var _answer:Object = null;
		private var _answerString:String = "";

		public function Question(_parentPage:Page, _xml:XML) {
			_page = _parentPage;
			_questionItems = new Array();
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
		public function get label():String {
			return _label;
		}
		public function get isRequired():Boolean{
			return _isRequired;
		}
		public function get pageVariableName():String {
			return _pageVariableName;
		}
		public function get pageVariable():Object {
			return PageVariables.getProperty(_pageVariableName);
		}
		public function set pageVariable(_value:Object):void {
			if (_pageVariableName != null && _pageVariableName.length > 0) {
				PageVariables.setProperty(_pageVariableName, _value, PageVariables.getPropertyType(_pageVariableName));
			}
		}
		public function get questionItems():Array {
			return _questionItems;
		}
		public function get answer():Object {
			return _answer;
		}
		public function set answer(_value:Object):void {
			if (_value == _answer) {
				return;
			}
			_answer = _value;
		}
		public function get answerString():String {
			return _answerString;
		}
		public function set answerString(_value:String):void {
			if (_value == _answerString) {
				return;
			}
			_answerString = _value;
		}
		public function fromXML(_data:XML):void {
			_guid = _data.@GUID;
			_type = _data.@Type;
			_label = _data.@Caption;
			_isRequired = StringExtension.stringToBoolean(_data.@IsRequired);
			_pageVariableName = _data.@ProgramVariable;
			validation();
			var _questionItemsXMLList:XMLList = _data.Items;
			if ( _questionItemsXMLList.length()>0) {
				for each (var _questionItemNode:XML in _questionItemsXMLList[0].Item) {
					var _questionItem:QuestionItem = new QuestionItem();
					_questionItem.fromXML(_questionItemNode);
					addQuestionItem(_questionItem);
				}
				trace("        question item amount = " + _questionItems.length.toString());
			} else {
				trace("        No question item in this question!");
			}
		}
		private function validation():void {
			trace("    ###################################");
			trace("      --question GUID = " + _guid);
			trace("        question type = " + _type);
			trace("        question label = " + _label);
			trace("        question isRequired = " + isRequired);
			trace("        question pageVariableName = " + _pageVariableName);
		}
		private function addQuestionItem(_questionItem:QuestionItem):void {
			_questionItems.push(_questionItem);
		}
	}
}