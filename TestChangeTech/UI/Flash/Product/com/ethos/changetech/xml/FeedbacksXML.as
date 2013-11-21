package com.ethos.changetech.xml{
	
	public class FeedbacksXML {

		var _xml:XML;

		public function FeedbacksXML() {
			_xml = <Feedbacks></Feedbacks>
		}
		public function get xml():XML{
			return _xml;
		}
		public function addFeedback(_feedback:FeedbackXMLNode):void{
			_xml.appendChild(_feedback.xml);
		}
		public function toString():String {
			return _xml.toXMLString();
		}
	}
}