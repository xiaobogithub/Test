package com.ethos.changetech.controls{
	import flash.display.Sprite;

	public class DesignElement extends Sprite {
		protected var _type:String="ABSTRACT:abstract Designelement type";

		public function get type():String {
			return _type;
		}
		public function toXML():XML {
			trace("ABSTRACT: toXML");
			var _XML:XML=<Control></Control>;;
			return _XML;
		}
	}
}