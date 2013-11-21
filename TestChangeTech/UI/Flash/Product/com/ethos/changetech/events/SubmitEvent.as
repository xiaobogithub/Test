package com.ethos.changetech.events{
	import flash.events.Event;

	public class SubmitEvent extends Event {
		public static  const SUBMIT:String = "submit";

		private var _xml:XML;
		private var _isFeedback:Boolean;
		private var _isSMSEvent:Boolean;
		
		public function get xml():XML {
			return _xml;
		}
		public function get isFeedback():Boolean {
			return _isFeedback;
		}
		public function set isSMSEvent(_value:Boolean):void {
			if (_value == _isSMSEvent) {
				return;
			}
			_isSMSEvent = _value;
		}
		public function get isSMSEvent():Boolean {
			return _isSMSEvent;
		}
		public function SubmitEvent(_type:String, _targetXML:XML, _feedback:Boolean = true) {
			super(_type);
			_xml = _targetXML;
			_isFeedback = _feedback;
			_isSMSEvent = false;
		}
		public override function clone():Event {
			return new SubmitEvent(type,_xml);
		}
		public override function toString():String {
			return formatToString("PageEvent","type","bubbles","cancelable","eventPhase");
		}
	}
}