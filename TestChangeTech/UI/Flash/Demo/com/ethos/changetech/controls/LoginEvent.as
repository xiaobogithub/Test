package com.ethos.changetech.controls{
	import flash.events.Event;

	public class LoginEvent extends Event {
		public static  const INVALID:String = "invalid";
		public static  const PASSED:String = "passed";

		public function LoginEvent(_type) {
			super(_type);
		}
		public override function clone():Event {
			return new LoginEvent(type);
		}
		public override function toString():String {
			return formatToString("LoginEvent","type","bubbles","cancelable","eventPhase");
		}
	}
}