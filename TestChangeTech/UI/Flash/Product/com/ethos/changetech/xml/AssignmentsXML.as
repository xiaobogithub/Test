package com.ethos.changetech.xml{
	
	public class AssignmentsXML {

		var _xml:XML;

		public function AssignmentsXML() {
			_xml = <Assignments></Assignments>
		}
		public function get xml():XML{
			return _xml;
		}
		public function addAssignment(_assignment:AssignmentXMLNode):void{
			_xml.appendChild(_assignment.xml);
		}
		public function toString():String {
			return _xml.toXMLString();
		}
	}
}