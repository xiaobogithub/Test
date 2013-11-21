package com.ethos.changetech.controls{
	import flash.events.Event;

	public class PanelEvent extends Event {
		public static  const CLOSE:String = "close";

		public function PanelEvent(_type) {
			super(_type);
		}
		public override function clone():Event {
			return new PanelEvent(type);
		}
		public override function toString():String {
			return formatToString("PanelEvent","type","bubbles","cancelable","eventPhase");
		}
	}
}