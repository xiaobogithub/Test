package com.ethos.changetech.events{
	import flash.events.Event;

	public class PreferenceEvent extends Event {
		public static  const CHOOSED:String = "choosed";

		public function PreferenceEvent(_type:String) {
			super(_type);
		}
		public override function clone():Event {
			return new PreferenceEvent(type);
		}
		public override function toString():String {
			return formatToString("PreferenceEvent","type","bubbles","cancelable","eventPhase");
		}
	}
}