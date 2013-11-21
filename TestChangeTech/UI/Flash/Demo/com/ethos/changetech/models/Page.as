package com.ethos.changetech.models{
	public class Page {
		private var _pageSequence:PageSequence;
		private var _pageGUID:String;
		private var _pageOrderNo:int;
		private var _name:String;
		private var _bodyTitle:String;
		private var _bodyText:String;
		private var _buttonPrimaryCaption:String;
		private var _buttonSecondaryCaption:String;
		private var _buttonPrimaryAction:int;
		private var _buttonSecondaryAction:int;
		private var _questions:Array;

		public function Page(_parentPageSequence:PageSequence) {
			_pageSequence = _parentPageSequence;
			_questions = new Array();
		}
		public function get pageSequence():PageSequence {
			return _pageSequence;
		}
		public function get pageGUID():String {
			return _pageGUID;
		}
		public function set pageGUID(_value:String):void {
			if (_value == _pageGUID) {
				return;
			}
			_pageGUID = _value;
		}
		public function get pageOrderNo():int {
			return _pageOrderNo;
		}
		public function set pageOrderNo(_value:int):void {
			if (_value == _pageOrderNo) {
				return;
			}
			_pageOrderNo = _value;
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
		public function get bodyTitle():String {
			return _bodyTitle;
		}
		public function set bodyTitle(_value:String):void {
			if (_value==_bodyTitle) {
				return;
			}
			_bodyTitle = _value;
		}
		public function get bodyText():String {
			return _bodyText;
		}
		public function set bodyText(_value:String):void {
			if (_value == _bodyText) {
				return;
			}
			_bodyText = _value;
		}
		public function get buttonPrimaryCaption():String {
			return _buttonPrimaryCaption;
		}
		public function set buttonPrimaryCaption(_value:String):void {
			if (_value == _buttonPrimaryCaption) {
				return;
			}
			_buttonPrimaryCaption = _value;
		}
		public function get buttonSecondaryCaption():String {
			return _buttonSecondaryCaption;
		}
		public function set buttonSecondaryCaption(_value:String):void {
			if (_value == _buttonSecondaryCaption) {
				return;
			}
			_buttonSecondaryCaption = _value;
		}
		public function get buttonPrimaryAction():int {
			return _buttonPrimaryAction;
		}
		public function set buttonPrimaryAction(_value:int):void {
			if (_value == _buttonPrimaryAction) {
				return;
			}
			_buttonPrimaryAction = _value;
		}
		public function get buttonSecondaryAction():int {
			return _buttonSecondaryAction;
		}
		public function set buttonSecondaryAction(_value:int):void {
			if (_value == _buttonSecondaryAction) {
				return;
			}
			_buttonSecondaryAction = _value;
		}
		public function get questions():Array {
			return _questions;
		}
		public function fromXML(_data:XML):void {
			_pageGUID = _data.@PageGUID;
			_pageOrderNo = _data.@PageOrderNo;
			_name = _data.@Name;
			_bodyTitle = _data.@BodyTitle;
			_bodyText = _data.@BodyText;
			_buttonPrimaryCaption = _data.@ButtonPrimaryCaption;
			_buttonSecondaryCaption = _data.@ButtonSecondaryCaption;
			_buttonPrimaryAction = _data.@ButtonPrimaryAction;
			_buttonSecondaryAction = _data.@ButtonSecondaryAction;
			validation();
			for each (var _questionNode:XML in _data.Question) {
				var _question:Question = new Question();
				_question.fromXML(_questionNode);
				addQuestion(_question);
			}
		}
		private function validation():void {
			trace("    ******************************");
			trace("    --page order No. "+_pageOrderNo);
			trace("      page GUID = "+_pageGUID);
			trace("      page name = "+_name);
			trace("      page body title = "+_bodyTitle);
			trace("      page body text = "+_bodyText);
			trace("      page button primary caption = "+_buttonPrimaryCaption);
			trace("      page button secondary caption = "+_buttonSecondaryCaption);
			trace("      page button primary action = "+_buttonPrimaryAction);
			trace("      page button secondary action = "+_buttonSecondaryAction);
		}
		private function addQuestion(_question:Question):void {
			_questions.push(_question);
		}
	}
}