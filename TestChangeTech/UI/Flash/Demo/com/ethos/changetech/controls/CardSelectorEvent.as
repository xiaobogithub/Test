package com.ethos.changetech.controls{
	import flash.events.Event;

	public class CardSelectorEvent extends Event {
		public static  const SELECTED:String = "selected";
		public static  const UNSELECTED:String = "unselected";

		public function CardSelectorEvent(_type) {
			super(_type);
		}
		public override function clone():Event {
			return new CardSelectorEvent(type);
		}
		public override function toString():String {
			return formatToString("CardSelectorEvent","type","bubbles","cancelable","eventPhase");
		}
	}
}