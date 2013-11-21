package com.ethos.changetech.models{
	import com.ethos.changetech.data.PageVariables;
	
	public class Preference {
		private var _page:Page;
		private var _guid:String;
		private var _imageURL:String;
		private var _name:String;
		private var _description:String;
		private var _answer:String;
		private var _buttonName:String;
		private var _pageVariableName:String; 
		
		public function Preference(_parentPage:Page, _xml:XML) {
			_page = _parentPage;
			fromXML(_xml);
		}
		public function get page():Page {
			return _page;
		}
		public function get guid():String{
			return _guid;
		}
		public function get imageURL():String {
			return _imageURL;
		}
		public function get name():String {
			return _name;
		}
		public function get description():String {
			return _description;
		}
		public function get answer():String {
			return _answer;
		}
		public function get buttonName():String {
			return _buttonName;
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
		
		public function fromXML(_data:XML):void {
			_guid = _data.@GUID;
			_imageURL = _data.@Image;
			_name = _data.@Name;
			_description = _data.@Description;
			_answer = _data.@Answer;
			_buttonName = _data.@ButtonName;
			_pageVariableName = _data.@ProgramVariable;
			validation();
		}
		private function validation():void {
			trace("    ###################################");
			trace("      --preference GUID = " + _guid);
			trace("        preference image URL = " + _imageURL);
			trace("        preference name = " + _name);
			trace("        preference description = " + _description);
			trace("        preference answer = " + _answer);
			trace("        preference button name = " + _buttonName);
			trace("        preference pageVariableName = " + _pageVariableName);
		}
	}
}