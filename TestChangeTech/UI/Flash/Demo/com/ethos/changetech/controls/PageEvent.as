package com.ethos.changetech.controls{
	import flash.events.Event;

	public class PageEvent extends Event {
		public static  const CATEGORY:String = "category";
		public static  const NEXTPAGE:String = "nextpage";
		public static  const PREVIOUSPAGE:String = "previouspage";
		public static  const NEXTSEQUENCE:String = "nextsequence";
		public static  const PREVIOUSSEQUENCE:String = "previoussequence";

		public function PageEvent(_type) {
			super(_type);
		}
		public override function clone():Event {
			return new PageEvent(type);
		}
		public override function toString():String {
			return formatToString("PageEvent","type","bubbles","cancelable","eventPhase");
		}
	}
}