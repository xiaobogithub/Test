package com.ethos.changetech.controls{
	import com.ethos.changetech.models.Page;
	import flash.events.MouseEvent;
	import flash.external.ExternalInterface;
	
	public class TextBoxContentSessionEndingTemplate extends TextBoxContentStandardBasedTemplate {
		
		public function TextBoxContentSessionEndingTemplate(_targetPage:Page, _w:Number, _minH:Number, _maxH:Number) {
			super(_targetPage, _w, _minH, _maxH);
		}
		
		override public function primaryButtonClickedHandler(_event:MouseEvent):void {
			trace("try close");
			ExternalInterface.call("windowClose");
		}
	}
}