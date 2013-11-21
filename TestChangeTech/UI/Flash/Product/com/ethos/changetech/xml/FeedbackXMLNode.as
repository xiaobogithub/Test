package com.ethos.changetech.xml{
	
	public class FeedbackXMLNode {

		var _xml:XML;

		public function FeedbackXMLNode(_guid:String, _value:String) {
			_xml = <Feedback GUID={_guid} Value={_value}></Feedback>;
		}
		public function get xml():XML {
			return _xml;
		}
		public function setGUID(_guid:String):void {
			_xml.@GUID = _guid;
		}
		public function setValue(_value:String):void {
			_xml.@Value = _value;
		}
		public function toString():String {
			return _xml.toXMLString();
		}
	}
}