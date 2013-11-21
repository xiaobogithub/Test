package com.ethos.changetech.xml{
	
	public class AssignmentXMLNode {

		var _xml:XML;

		public function AssignmentXMLNode(_variableName:String, _value:String) {
			_xml = <Assingment Variable={_variableName} Value={_value}></Assingment>;
		}
		public function get xml():XML {
			return _xml;
		}
		public function setVariableName(_variableName:String):void {
			_xml.@Variable = _variableName;
		}
		public function setValue(_value:String):void {
			_xml.@Value = _value;
		}
		public function toString():String {
			return _xml.toXMLString();
		}
	}
}