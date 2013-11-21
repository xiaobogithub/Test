package com.ning.events{
	import flash.events.Event;

	public class LoginEvent extends Event {
		public static  const INVALID:String = "invalid";
		public static  const PASSED:String = "passed";
		public static  const COMPLETE:String = "complete";
		public static  const TIMEOUT:String = "timeout";
		public static  const ERROR:String = "error";

		private var _message:String;
		
		public function LoginEvent(_type, _serverMessage) {
			super(_type);
			_message = _serverMessage;
		}
		public function get message():String {
			return _message;
		}
		public function set message(value:String):void {
			if (value == _message) {
				return;
			}
			_message = value;
		}
		public override function clone():Event {
			return new LoginEvent(type, message);
		}
		public override function toString():String {
			return formatToString("LoginEvent","type","bubbles","cancelable","eventPhase");
		}
	}
}