package com.ethos.changetech.events{
	import flash.events.Event;

	public class PageEvent extends Event {
		public static  const INFO:String = "info";
		public static  const CATEGORY:String = "category";
		public static  const ARRANGED:String = "arranged";
		public static  const NEXTPAGE:String = "nextpage";
		public static  const PREVIOUSPAGE:String = "previouspage";
		public static  const END:String = "end";
		public static  const SETBUTTON:String = "setbutton";
		public static  const STARTLOADING:String = "startloading";
		public static  const STOPLOADING:String = "stoploading";

		private var _data:Object;
		
		public function get data():Object {
			return _data;
		}
		public function PageEvent(_type:String, _eventData:Object) {
			super(_type);
			_data = _eventData;
		}
		public override function clone():Event {
			return new PageEvent(type, _data);
		}
		public override function toString():String {
			return formatToString("PageEvent","type","bubbles","cancelable","eventPhase");
		}
	}
}