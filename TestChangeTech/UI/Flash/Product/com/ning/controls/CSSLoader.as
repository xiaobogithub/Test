package com.ning.controls{
	import flash.display.Sprite;
	import flash.errors.IllegalOperationError;
	import flash.events.Event;
	import flash.events.IOErrorEvent;
	import flash.net.URLLoader;
	import flash.net.URLRequest;
	import flash.text.StyleSheet;

	public class CSSLoader extends Sprite{

		private var _styleSheet:StyleSheet;
		private var _loaded:Boolean;

		public function get styleSheet():StyleSheet {
			if (!_loaded) {
				throw new IllegalOperationError("The CSS file has not loaded yet, cannot be accessed.");
			}
			return _styleSheet;
		}
		public function CSSLoader() {
			_styleSheet = new StyleSheet();
			_loaded = false;
		}
		public function load(_styleSheetURL:String):void {
			var _loader:StringLoader = new StringLoader(_styleSheetURL);
			_loader.showProgressBar = true;
			_loader.addEventListener(Event.COMPLETE, styleSheetLoadCompleteHandler);
			_loader.addEventListener(IOErrorEvent.IO_ERROR, styleSheetLoadErrorHandler);
			_loader.load();
			_loader.x = (width - _loader.width) / 2;
			_loader.y = (height - _loader.height) / 2;
			addChild(_loader);
		}
		private function styleSheetLoadCompleteHandler(_event:Event):void {
			trace("*************");
			trace("CSS loaded:");
			_styleSheet.parseCSS(_event.target.data);
			trace(_styleSheet.styleNames);
			trace("*************");
			_loaded = true;
			removeChild(StringLoader(_event.target));
			this.dispatchEvent(new Event("complete"));
		}
		private function styleSheetLoadErrorHandler(_event:IOErrorEvent):void {
			this.dispatchEvent(_event);
		}
	}
}